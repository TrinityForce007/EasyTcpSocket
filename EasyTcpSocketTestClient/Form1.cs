using EasyTcpSocket;
using System.Collections.Concurrent;
using System.Text;

namespace EasyTcpSocketTestClient
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// �ͻ���socket list
        /// </summary>
        private ConcurrentDictionary<string, TcpSocketClient> ClientSocketList = new ConcurrentDictionary<string, TcpSocketClient>();

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
                MessageBox.Show("��������ȷ��ֵ");
                return;
            }

            if (!int.TryParse(serverPort.Text, out int intServerPort))
            {
                MessageBox.Show("��������ȷ��ֵ");
                return;
            }

            if (!int.TryParse(threadCount.Text, out int intThreadCount))
            {
                MessageBox.Show("��������ȷ��ֵ");
                return;
            }

            for (int i = 0; i < intThreadCount; i++)
            {
                string client = $"client{ClientSocketList.Keys.Count + 1}";

                TcpSocketClient clientSocket = new TcpSocketClient(Event_ReceivedMessage);
                if (clientSocket.Connect(serverIP.Text, intServerPort, out string errorMessage))
                {
                    ClientSocketList.TryAdd(client, clientSocket);
                    clientList.Items.Add(client);
                    AddRowToDataGrid(client, $"{client}���ӳɹ�");
                }
                else
                {
                    AddRowToDataGrid(client, $"{client}����ʧ��,{errorMessage}");
                }
            }
        }

        private void Event_ReceivedMessage(string serverIP, byte[] content, int length)
        {
            string str = Encoding.Default.GetString(content, 0, length);
            AddRowToDataGrid(serverIP, $"�յ�{serverIP}����Ϣ��{str}");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (clientList.SelectedIndex == -1 || consecuteiveSendTimes.SelectedIndex == -1 || sendInterval.SelectedIndex == -1)
            {
                MessageBox.Show("��������ȷ��ֵ");
                return;
            }

            if (!int.TryParse(consecuteiveSendTimes.SelectedItem.ToString(), out int times))
            {
                MessageBox.Show("��������ȷ��ֵ");
                return;
            }

            if (!int.TryParse(sendInterval.SelectedItem.ToString(), out int interval))
            {
                MessageBox.Show("��������ȷ��ֵ");
                return;
            }

            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                MessageBox.Show("��������ȷ��ֵ");
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
        /// ������Ϣ
        /// </summary>
        /// <param name="key">clientID</param>
        /// <param name="times">�ظ����ٴ�</param>
        /// <param name="interval">ÿ�εļ��</param>
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

                AddRowToDataGrid(key, $"���ͳɹ�,times={times},interval={interval}");

                if (interval > 0)
                {
                    Thread.Sleep(interval);
                }
            }
        }

        /// <summary>
        /// ������ݵ�dataGrid
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        private void AddRowToDataGrid(string client, string message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                dataGridView1.Rows.Add(new string[] { (dataGridView1.Rows.Count + 1).ToString(), $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}", client, message });
                //�Զ����������һ��
                int lastRowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.CurrentCell = dataGridView1.Rows[lastRowIndex].Cells[1];
            });
        }

        /// <summary>
        /// ���dataGrid
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
    }
}