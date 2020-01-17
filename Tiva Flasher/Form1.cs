using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Tiva_Flasher
{
    public partial class Form1 : Form
    {
        private static AutoResetEvent ackRcvd = new AutoResetEvent(false);
        private static AutoResetEvent writeResultEvent = new AutoResetEvent(false);
        private static AutoResetEvent eraseResultEvent = new AutoResetEvent(false);
        private static AutoResetEvent crcResultEvent = new AutoResetEvent(false);

        private static byte writeResult = 0;
        private static byte eraseResult = 0;
        private static byte crcCheckResult = 0;


        private static byte lastSentOpcode = 0;
    
        private static int crcRes = 0;

        private static Stopwatch sw; 
        public Form1()
        {
            InitializeComponent();
        }

        private void flashBtn_Click(object sender, EventArgs e)
        {
            int index = 0;
            bool ack = false;

            if (!File.Exists(openFileDialog.FileName))
            {
                MessageBox.Show("Please select valid binary file.");
                return;
            }

            var buff = new byte[] { 0, 1, 0xA5 };

            // length 
            // opcode 
            // data
            // 0xA5

            ack = SendPacketAck(buff, 3);
            if (!ack)
            {
                log.AppendText("Erase request no ack!!" + Environment.NewLine);
                return;
            }

            if (!eraseResultEvent.WaitOne(5000))
            {
                log.AppendText("Erase timed out!!" + Environment.NewLine);
                return;
            }

            if (eraseResult != 1)
            {
                log.AppendText("Erase failed!!" + Environment.NewLine);
                return;
            }



            using (Stream fs = openFileDialog.OpenFile())
            {

                sw = new Stopwatch();
                sw.Start();
                
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    buff = new byte[7];
                    buff[index++] = 0x04; // length
                    buff[index++] = 0x02; // opcode

                    UInt32 len = (UInt32)reader.BaseStream.Length;
                    buff[index++] = (byte)((len & 0xFF000000U) >> 24);
                    buff[index++] = (byte)((len & 0x00FF0000U) >> 16);
                    buff[index++] = (byte)((len & 0x0000FF00U) >> 8);
                    buff[index++] = (byte)len;

                    buff[index] = 0xA5; // terminator

                    
                    ack = SendPacketAck(buff, 7);

                    if (!ack)
                    {
                        log.AppendText("Programming request not ack!!" + Environment.NewLine);
                    }

                    progressBar1.Maximum = (int)len;
                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            DisableControls();
            LoadPorts();
        }

        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int index = 0;
            var data_len = serialPort.ReadByte();
            if (data_len < 256)
            {
                var buff = new byte[data_len + 3];
                buff[index++] = (byte)data_len; // length
                buff[index++] = (byte)serialPort.ReadByte(); // opcode

                for (int i = 0; i < data_len; i++) // data
                {
                    buff[index++] = (byte)serialPort.ReadByte();
                }

                buff[index] = (byte)serialPort.ReadByte(); // terminator 
                if (buff[index] == 0xA5)
                {
                    HandleDataRx(buff);
                }
                else
                {
                    MessageBox.Show("Corrupted message received!");
                }
            }
        }

        private void HandleDataRx(byte[] buff)
        {
            byte len = buff[0];
            byte opcode = buff[1];
            byte[] data = new byte[len];
            Array.ConstrainedCopy(buff, 2, data, 0, len);
            switch (opcode)
            {
                // Ack
                case 0xAA:
                    if (buff[2] == lastSentOpcode)
                    {
                        // Successfully received ack.
                        ackRcvd.Set();
                    }
                    break;
                // Erase response
                case 0xA1:
                    eraseResult = data[0];
                    log.AppendText(string.Format("Erased application with result:{0}" + Environment.NewLine,eraseResult));
                    eraseResultEvent.Set();
                    break;

                // Flashing response
                case 0xA2:
                    if (data[0] == 1)
                    {
                        sw = new Stopwatch();
                        sw.Start();
                        new Thread(SendData).Start();
                        log.AppendText(string.Format("Flashing started!" + Environment.NewLine));
                       
                    } else
                    {
                        log.AppendText(string.Format("Flashing failed! Too big application." + Environment.NewLine));
                    }
                    break;

                case 0xA3:
                    writeResult = data[0];
                    writeResultEvent.Set();
                    break;

                case 0xA4:
                    crcCheckResult = data[0];
                    crcResultEvent.Set();
                    break;

                default:
                    log.AppendText(string.Format("{0}" + Environment.NewLine, BitConverter.ToString(buff, 0, len + 3)));
                    break;

                
            }
        }

        private void SendData()
        {
            var packetBuffer = new byte[255]; // 128 bytes data + 1 length + 1 opcode + 1 terminator
            var dataBuffer = new byte[252];

            packetBuffer[0] = 252;
            packetBuffer[1] = 0x03;
            packetBuffer[254] = 0xA5;
            using (Stream fs = openFileDialog.OpenFile())
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        //textBox1.AppendText(string.Format("{0}th chunk of data being transmitted" + Environment.NewLine,i++));

                        dataBuffer = new byte[252];
                        var bytesRead = reader.BaseStream.Read(dataBuffer, 0, 252);
                        if (bytesRead == 252)
                        {
                            Array.ConstrainedCopy(dataBuffer, 0, packetBuffer, 2, dataBuffer.Length);
                        }
                        else // readjust size (this is the last chunk!)
                        {
                            Array.ConstrainedCopy(dataBuffer, 0, packetBuffer, 2, bytesRead);
                            packetBuffer[2 + bytesRead] = 0xA5; // readjust end byte
                            packetBuffer[0] = (byte)bytesRead; // readjust size byte
                        }                            

                        SendPacketAck(packetBuffer, bytesRead + 3);

                        if (!writeResultEvent.WaitOne(1000))
                        {
                            log.AppendText("Write timeout!");
                            break;
                        } 
                        
                        if (writeResult != 0x01 && writeResult != 0x02)
                        {
                            log.AppendText(string.Format("Flashing failed with result = {0}!" + Environment.NewLine, writeResult));
                            break;
                        }
                        progressBar1.Value = (int)reader.BaseStream.Position;
                    }
                }
            }


            if (writeResult == 0x02)
            {
                log.AppendText(string.Format("Flashing finished in {0} msecs! Sending CRC check." + Environment.NewLine, sw.ElapsedMilliseconds));
                packetBuffer = new byte[7] {
                    0x04, // length
                    0x04, // opcode
                    (byte)((crcRes & 0xFF000000U) >> 24),
                    (byte)((crcRes & 0x00FF0000U) >> 16),
                    (byte)((crcRes & 0x0000FF00U) >> 8),
                    (byte)crcRes,
                    0xA5 // terminator
                };

                SendPacketAck(packetBuffer, 7);

                if (!crcResultEvent.WaitOne(1000))
                {
                    log.AppendText("CRC timed out!!" + Environment.NewLine);
                    return;
                }

                if (crcCheckResult == 1)
                {
                    log.AppendText("Flashing completed successfully! Application verified." + Environment.NewLine);
                }
                else
                {
                    log.AppendText("CRC mismatch!! " + Environment.NewLine);
                }
            }
        }

        private void fileTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            new Thread(UpdateCRC).Start();
        }

        private void UpdateCRC()
        {
            try
            {
                using (Stream fs = openFileDialog.OpenFile())
                {
                    var crc = new CRC32();
                    var calc_crc = crc.GetCrc32(fs);
                    crcTxt.Text = string.Format("0x{0:X}", calc_crc);
                    crcRes = (int)calc_crc;
                }
            } catch (IOException)
            {
                Thread.Sleep(500);
                UpdateCRC();
            }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            fileTxt.Text = openFileDialog.FileName;
            fileSystemWatcher.Path = Path.GetDirectoryName(openFileDialog.FileName);
            fileSystemWatcher.Filter = Path.GetFileName(openFileDialog.FileName);
            fileSystemWatcher.EnableRaisingEvents = true;
            new Thread(UpdateCRC).Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            var buff = new byte[3] { 0x00, 0x05, 0xA5 };
            SendPacketAck(buff, 3);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var buff = new byte[3] { 0x00, 0x06, 0xA5 };
            SendPacketAck(buff, 3);
        }

        private bool SendPacketAck(byte[] buffer, int len)
        {
            lastSentOpcode = buffer[1];
            serialPort.Write(buffer, 0, len);
            if (ackRcvd.WaitOne(500))
            {
                return true; 
            }
            return false;
        }

        private void eraseBtn_Click(object sender, EventArgs e)
        {
            var buff = new byte[] { 0, 1, 0xA5 };
            bool ack = SendPacketAck(buff, 3);
            if (ack)
            {
                if (!eraseResultEvent.WaitOne(5000))
                {
                    log.AppendText("Erase timed out!!" + Environment.NewLine);
                    return;
                }

                if (eraseResult != 1)
                {
                    log.AppendText("Erase failed!!" + Environment.NewLine);
                    return;
                }

                log.AppendText("Erasing succeeded" + Environment.NewLine);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == "Open")
            {                
                if (comPortsLst.Text == "")
                {
                    MessageBox.Show("Please select a COM port","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    serialPort = new System.IO.Ports.SerialPort(comPortsLst.Text, 115200);
                    serialPort.Open();
                    serialPort.DataReceived += SerialPort_DataReceived;
                    btnOpen.Text = "Close";
                    EnableControls();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't open COM port due to error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            else
            {
                try
                {
                    serialPort.DiscardInBuffer();
                    serialPort.DiscardOutBuffer();
                    serialPort.Close();
                    serialPort.Dispose();
                    btnOpen.Text = "Open";
                    DisableControls();
                }
                catch { }
                finally {
                    serialPort.Dispose();
                    btnOpen.Text = "Open";
                    DisableControls();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPorts();
        }

        private void LoadPorts()
        {
            comPortsLst.Items.Clear();
            comPortsLst.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (comPortsLst.Items.Count > 0)
            {
                comPortsLst.SelectedIndex = 0;
            }
        }

        private void DisableControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name != "comPortsLst"
                    && c.Name != "btnOpen"
                    && c.Name != "btnRefresh"
                    && c.Name != "label3")
                {
                    c.Enabled = false;
                }
            }
        }

        private void EnableControls()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            log.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var buff = new byte[3] { 0x00, 0x06, 0xA5 };
            SendPacketAck(buff, 3);
        }
    }
}
