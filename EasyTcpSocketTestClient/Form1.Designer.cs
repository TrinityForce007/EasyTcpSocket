namespace EasyTcpSocketTestClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            btnStartConnect = new Button();
            threadCount = new TextBox();
            label3 = new Label();
            serverPort = new TextBox();
            label2 = new Label();
            label1 = new Label();
            serverIP = new TextBox();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            dataGridView1 = new DataGridView();
            RN = new DataGridViewTextBoxColumn();
            time = new DataGridViewTextBoxColumn();
            clientID = new DataGridViewTextBoxColumn();
            message = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            clearToolStripMenuItem = new ToolStripMenuItem();
            groupBox3 = new GroupBox();
            label7 = new Label();
            sendInterval = new ComboBox();
            label8 = new Label();
            label6 = new Label();
            consecuteiveSendTimes = new ComboBox();
            label5 = new Label();
            f = new Label();
            btnSend = new Button();
            clientList = new ComboBox();
            richTextBox1 = new RichTextBox();
            panel2 = new Panel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            groupBox3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnStartConnect);
            groupBox1.Controls.Add(threadCount);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(serverPort);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(serverIP);
            groupBox1.Controls.Add(panel1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(702, 64);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnStartConnect
            // 
            btnStartConnect.Location = new Point(613, 22);
            btnStartConnect.Name = "btnStartConnect";
            btnStartConnect.Size = new Size(75, 23);
            btnStartConnect.TabIndex = 8;
            btnStartConnect.Text = "连接";
            btnStartConnect.UseVisualStyleBackColor = true;
            btnStartConnect.Click += btnStartConnect_Click;
            // 
            // threadCount
            // 
            threadCount.Location = new Point(507, 22);
            threadCount.Name = "threadCount";
            threadCount.Size = new Size(100, 23);
            threadCount.TabIndex = 7;
            threadCount.Text = "1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(421, 25);
            label3.Name = "label3";
            label3.Size = new Size(80, 17);
            label3.TabIndex = 6;
            label3.Text = "启动几个线程";
            // 
            // serverPort
            // 
            serverPort.Location = new Point(296, 22);
            serverPort.Name = "serverPort";
            serverPort.Size = new Size(119, 23);
            serverPort.TabIndex = 5;
            serverPort.Text = "3333";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(217, 25);
            label2.Name = "label2";
            label2.Size = new Size(73, 17);
            label2.TabIndex = 4;
            label2.Text = "Server Port";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 25);
            label1.Name = "label1";
            label1.Size = new Size(60, 17);
            label1.TabIndex = 3;
            label1.Text = "Server IP";
            // 
            // serverIP
            // 
            serverIP.Location = new Point(87, 22);
            serverIP.Name = "serverIP";
            serverIP.Size = new Size(124, 23);
            serverIP.TabIndex = 2;
            serverIP.Text = "127.0.0.1";
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 105);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 100);
            panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(702, 432);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "收到消息";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { RN, time, clientID, message });
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 19);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(696, 410);
            dataGridView1.TabIndex = 0;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // RN
            // 
            RN.HeaderText = "序号";
            RN.Name = "RN";
            RN.ReadOnly = true;
            // 
            // time
            // 
            time.HeaderText = "时间";
            time.Name = "time";
            time.ReadOnly = true;
            // 
            // clientID
            // 
            clientID.HeaderText = "Client";
            clientID.Name = "clientID";
            clientID.ReadOnly = true;
            // 
            // message
            // 
            message.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            message.HeaderText = "内容";
            message.Name = "message";
            message.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { clearToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(107, 26);
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(106, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(sendInterval);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(consecuteiveSendTimes);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(f);
            groupBox3.Controls.Add(btnSend);
            groupBox3.Controls.Add(clientList);
            groupBox3.Controls.Add(richTextBox1);
            groupBox3.Dock = DockStyle.Bottom;
            groupBox3.Location = new Point(0, 496);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(702, 186);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "发送消息";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(580, 159);
            label7.Name = "label7";
            label7.Size = new Size(25, 17);
            label7.TabIndex = 9;
            label7.Text = "ms";
            // 
            // sendInterval
            // 
            sendInterval.FormattingEnabled = true;
            sendInterval.Items.AddRange(new object[] { "0", "10", "100", "1000" });
            sendInterval.Location = new Point(495, 153);
            sendInterval.Name = "sendInterval";
            sendInterval.Size = new Size(79, 25);
            sendInterval.TabIndex = 8;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(433, 159);
            label8.Name = "label8";
            label8.Size = new Size(56, 17);
            label8.TabIndex = 7;
            label8.Text = "发送间隔";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(407, 159);
            label6.Name = "label6";
            label6.Size = new Size(20, 17);
            label6.TabIndex = 6;
            label6.Text = "次";
            // 
            // consecuteiveSendTimes
            // 
            consecuteiveSendTimes.FormattingEnabled = true;
            consecuteiveSendTimes.Items.AddRange(new object[] { "1", "10", "100", "1000", "10000" });
            consecuteiveSendTimes.Location = new Point(303, 153);
            consecuteiveSendTimes.Name = "consecuteiveSendTimes";
            consecuteiveSendTimes.Size = new Size(98, 25);
            consecuteiveSendTimes.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(241, 159);
            label5.Name = "label5";
            label5.Size = new Size(56, 17);
            label5.TabIndex = 4;
            label5.Text = "连续发送";
            // 
            // f
            // 
            f.AutoSize = true;
            f.Location = new Point(65, 159);
            f.Name = "f";
            f.Size = new Size(44, 17);
            f.TabIndex = 3;
            f.Text = "发送人";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(611, 153);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 2;
            btnSend.Text = "发送";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // clientList
            // 
            clientList.FormattingEnabled = true;
            clientList.Items.AddRange(new object[] { "All" });
            clientList.Location = new Point(114, 153);
            clientList.Name = "clientList";
            clientList.Size = new Size(121, 25);
            clientList.TabIndex = 1;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Top;
            richTextBox1.Location = new Point(3, 19);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(696, 128);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "Hello";
            // 
            // panel2
            // 
            panel2.Controls.Add(groupBox2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 64);
            panel2.Name = "panel2";
            panel2.Size = new Size(702, 432);
            panel2.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 682);
            Controls.Add(panel2);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "EasyTcpSocketTestClient";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private TextBox serverIP;
        private Button btnStartConnect;
        private TextBox threadCount;
        private Label label3;
        private TextBox serverPort;
        private Label label2;
        private GroupBox groupBox2;
        private DataGridView dataGridView1;
        private GroupBox groupBox3;
        private RichTextBox richTextBox1;
        private Button btnSend;
        private ComboBox clientList;
        private Label label7;
        private ComboBox sendInterval;
        private Label label8;
        private Label label6;
        private ComboBox consecuteiveSendTimes;
        private Label label5;
        private Label f;
        private DataGridViewTextBoxColumn RN;
        private DataGridViewTextBoxColumn time;
        private DataGridViewTextBoxColumn clientID;
        private DataGridViewTextBoxColumn message;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem clearToolStripMenuItem;
        private Panel panel1;
        private Panel panel2;
    }
}