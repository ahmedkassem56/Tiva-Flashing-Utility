namespace Tiva_Flasher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flashBtn = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.log = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileTxt = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.eraseBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.crcTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.readBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comPortsLst = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // flashBtn
            // 
            this.flashBtn.Location = new System.Drawing.Point(631, 47);
            this.flashBtn.Name = "flashBtn";
            this.flashBtn.Size = new System.Drawing.Size(136, 34);
            this.flashBtn.TabIndex = 3;
            this.flashBtn.Text = "Flash";
            this.flashBtn.UseVisualStyleBackColor = true;
            this.flashBtn.Click += new System.EventHandler(this.flashBtn_Click);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 115200;
            this.serialPort.PortName = "COM17";
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(12, 91);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.log.Size = new System.Drawing.Size(560, 290);
            this.log.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "File to flash:";
            // 
            // fileTxt
            // 
            this.fileTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileTxt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.fileTxt.HideSelection = false;
            this.fileTxt.Location = new System.Drawing.Point(121, 53);
            this.fileTxt.Name = "fileTxt";
            this.fileTxt.ReadOnly = true;
            this.fileTxt.Size = new System.Drawing.Size(451, 22);
            this.fileTxt.TabIndex = 11;
            this.fileTxt.TextChanged += new System.EventHandler(this.fileTxt_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(578, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // eraseBtn
            // 
            this.eraseBtn.Location = new System.Drawing.Point(6, 59);
            this.eraseBtn.Name = "eraseBtn";
            this.eraseBtn.Size = new System.Drawing.Size(165, 32);
            this.eraseBtn.TabIndex = 5;
            this.eraseBtn.Text = "Erase application";
            this.eraseBtn.UseVisualStyleBackColor = true;
            this.eraseBtn.Click += new System.EventHandler(this.eraseBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Location = new System.Drawing.Point(6, 21);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(165, 32);
            this.resetBtn.TabIndex = 6;
            this.resetBtn.Text = "Reset target";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.crcTxt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.readBtn);
            this.groupBox1.Controls.Add(this.resetBtn);
            this.groupBox1.Controls.Add(this.eraseBtn);
            this.groupBox1.Location = new System.Drawing.Point(590, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 290);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tools";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 32);
            this.button1.TabIndex = 10;
            this.button1.Text = "Clear logs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // crcTxt
            // 
            this.crcTxt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.crcTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crcTxt.ForeColor = System.Drawing.SystemColors.InfoText;
            this.crcTxt.Location = new System.Drawing.Point(6, 262);
            this.crcTxt.Name = "crcTxt";
            this.crcTxt.ReadOnly = true;
            this.crcTxt.Size = new System.Drawing.Size(109, 22);
            this.crcTxt.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "CRC";
            // 
            // readBtn
            // 
            this.readBtn.Enabled = false;
            this.readBtn.Location = new System.Drawing.Point(6, 97);
            this.readBtn.Name = "readBtn";
            this.readBtn.Size = new System.Drawing.Size(165, 32);
            this.readBtn.TabIndex = 7;
            this.readBtn.Text = "Dump application";
            this.readBtn.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 396);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(755, 23);
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Binary file|*.bin";
            this.openFileDialog.Title = "Select file to flash";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // comPortsLst
            // 
            this.comPortsLst.FormattingEnabled = true;
            this.comPortsLst.Location = new System.Drawing.Point(121, 12);
            this.comPortsLst.Name = "comPortsLst";
            this.comPortsLst.Size = new System.Drawing.Size(176, 24);
            this.comPortsLst.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "COM Port";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(401, 9);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(92, 29);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(303, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 29);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 428);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comPortsLst);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.fileTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.log);
            this.Controls.Add(this.flashBtn);
            this.Name = "Form1";
            this.Text = "Tiva Flashing Utility";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button flashBtn;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fileTxt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button eraseBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button readBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox crcTxt;
        private System.Windows.Forms.Label label2;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comPortsLst;
        private System.Windows.Forms.Button button1;
    }
}

