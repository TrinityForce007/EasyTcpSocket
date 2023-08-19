using EasyTcpSocket;
using System.Text;

namespace EasyTcpSocketTestClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 客户端socket list
        /// </summary>
        private Dictionary<string, TcpSocketClient> ClientSocketList = new Dictionary<string, TcpSocketClient>();

        public Form1()
        {
            InitializeComponent();

            clientList.SelectedIndex = 0;
            consecuteiveSendTimes.SelectedIndex = 0;
            sendInterval.SelectedIndex = 0;
        }

        private void btnStartConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(serverIP.Text) || string.IsNullOrEmpty(serverPort.Text) || string.IsNullOrEmpty(threadCount.Text))
            {
                MessageBox.Show("请输入正确的值");
                return;
            }

            if (!int.TryParse(threadCount.Text, out int intThreadCount))
            {
                MessageBox.Show("请输入正确的值");
                return;
            }

            for (int i = 0; i < intThreadCount; i++)
            {
                string client = $"client{ClientSocketList.Keys.Count + 1}";

                TcpSocketClient clientSocket = new TcpSocketClient("127.0.0.1", 3333, Event_ReceivedMessage);
                if (clientSocket.Connect(out string errorMessage))
                {
                    ClientSocketList.Add(client, clientSocket);
                    clientList.Items.Add(client);
                    AddRowToDataGrid(client, $"{client}连接成功");
                }
                else
                {
                    AddRowToDataGrid(client, $"{client}连接失败,{errorMessage}");
                }
            }
        }

        private void Event_ReceivedMessage(string serverIP, byte[] content, int length)
        {
            Task.Run(() =>
            {
                string str = Encoding.Default.GetString(content, 0, length);
                AddRowToDataGrid(serverIP, $"收到{serverIP}的消息：{str}");
            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (clientList.SelectedIndex == -1 || consecuteiveSendTimes.SelectedIndex == -1 || sendInterval.SelectedIndex == -1)
            {
                MessageBox.Show("请输入正确的值");
                return;
            }

            if (!int.TryParse(consecuteiveSendTimes.SelectedItem.ToString(), out int times))
            {
                MessageBox.Show("请输入正确的值");
                return;
            }

            if (!int.TryParse(sendInterval.SelectedItem.ToString(), out int interval))
            {
                MessageBox.Show("请输入正确的值");
                return;
            }

            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("请输入正确的值");
                return;
            }

            if (clientList.SelectedIndex == 0)
            {
                foreach (var item in ClientSocketList.Keys)
                {
                    Task task = new Task(() =>
                    {
                        Send(item, times, interval);
                    });
                    task.Start();
                }
            }
            else
            {
                Send(clientList.SelectedItem.ToString(), times, interval);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="key">clientID</param>
        /// <param name="times">重复多少次</param>
        /// <param name="interval">每次的间隔</param>
        private void Send(string key, int times, int interval)
        {
            for (int i = 0; i < times; i++)
            {
                string text = string.Empty;

                this.Invoke((MethodInvoker)delegate
                {
                    text = richTextBox1.Text;
                });

                byte[] data = Encoding.Default.GetBytes(text.ToCharArray());
                ClientSocketList[key].Send(DataType.Message, data);

                AddRowToDataGrid(key, $"发送成功,times={times},interval={interval}");

                if (interval > 0)
                {
                    Thread.Sleep(interval);
                }
            }
        }

        /// <summary>
        /// 添加内容到dataGrid
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        private void AddRowToDataGrid(string client, string message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridView1.Rows.Add(new string[] { (dataGridView1.Rows.Count).ToString(), $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}", client, message });
            });
        }

        /// <summary>
        /// 清空dataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridView1.Rows.Clear();
            });
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //dataGrid自动滚动到最底部
            this.dataGridView1.FirstDisplayedScrollingRowIndex = this.dataGridView1.Rows.Count - 1;
        }
    }
}