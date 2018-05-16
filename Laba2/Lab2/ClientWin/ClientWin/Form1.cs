using System;
using System.Data;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace ClientWin
{
    public partial class Form1 : Form
    {
        public byte[] bytes = new byte[1024];
        BindingSource masterBS = new BindingSource();
        BindingSource studentBS = new BindingSource();
        BindingSource mastersBS = new BindingSource();
        BindingSource holdingBS = new BindingSource();
        DataSet data = new DataSet();
        public Form1()
        {
            InitializeComponent();

        }
        void SendMessageFromSocket(int port)
        {
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            sender.Connect(ipEndPoint);
            sender.Receive(bytes);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }


        public DataSet Decompress(Byte[] data)
        {
            MemoryStream mem = new MemoryStream(data);
            GZipStream zip = new GZipStream(mem, CompressionMode.Decompress);
            DataSet dataset = new DataSet();
            dataset.ReadXml(zip, XmlReadMode.ReadSchema);
            zip.Close();
            return dataset;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = masterBS;
            dataGridView2.DataSource = studentBS;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Фамилия";
            dataGridView1.Columns[2].HeaderText = "Имя";
            dataGridView1.Columns[3].HeaderText = "Адрес";
            dataGridView1.Columns[4].HeaderText = "Город";
            dataGridView1.Columns[5].HeaderText = "Область/Штат";
            dataGridView1.Columns[6].HeaderText = "Код";
            dataGridView1.Columns[7].HeaderText = "Телефон";
            dataGridView1.Columns[8].HeaderText = "Дата открытия";
            dataGridView1.Columns[9].HeaderText = "Серв. номер";
            dataGridView1.Columns[10].HeaderText = "Дата рождения";
            dataGridView1.Columns[11].HeaderText = "Уровень риска";
            dataGridView1.Columns[12].HeaderText = "Занятие";
            dataGridView1.Columns[13].HeaderText = "Цели";
            dataGridView1.Columns[14].HeaderText = "Интересы";
            dataGridView1.Columns[15].HeaderText = "Изображение";

            SetColumnHeaders(dataGridView2);
        }

        private void SetColumnHeaders(DataGridView d)
        {
            d.Columns[0].HeaderText = "ID";
            d.Columns[1].HeaderText = "ID аккаунта";
            d.Columns[2].HeaderText = "Символ";
            d.Columns[3].HeaderText = "Акции";
            d.Columns[4].HeaderText = "Цена";
            d.Columns[5].HeaderText = "Дата";
        }

        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
            try
            {
                data = Decompress(bytes);
            }
            catch
            { MessageBox.Show("Неполадки с сервером."); }

            GetData();


        }
        private void GetData()
        {
            try
            {
                try
                {
                    DataRelation relation = new DataRelation("ClientsHoldings",
                       data.Tables["Clients"].Columns["ACCT_NUMB"], data.Tables["Holdings"].Columns["ACCT_NUMB"]);
                    data.Relations.Add(relation);
                    DataRelation relation1 = new DataRelation("MasterHoldings",
                      data.Tables["Master"].Columns["SYMBOL"], data.Tables["Holdings"].Columns["SYMBOL"]);
                    data.Relations.Add(relation1);
                }
                catch
                {
                    MessageBox.Show("Что то со связями");
                }

                masterBS.DataSource = data;
                masterBS.DataMember = "Clients";
                studentBS.DataSource = masterBS;
                studentBS.DataMember = "ClientsHoldings";
                mastersBS.DataSource = data;
                mastersBS.DataMember = "Master";
                holdingBS.DataSource = mastersBS;
                holdingBS.DataMember = "MasterHoldings";

            }
            catch
            {
                MessageBox.Show("Ну не получилось((");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView3.DataSource = mastersBS;
            dataGridView4.DataSource = holdingBS;
            dataGridView3.Columns[0].HeaderText = "Символ";
            dataGridView3.Columns[1].HeaderText = "Название компании";
            dataGridView3.Columns[2].HeaderText = "Обмен";
            dataGridView3.Columns[3].HeaderText = "Текущая цена";
            SetColumnHeaders(dataGridView4);
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessageFromSocket(11000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }

            data = Decompress(bytes);



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Лабораторная работа №2\nВыполнил:\nстудент 327ст группы Рец Владимир", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string p = dataGridView1[15, dataGridView1.CurrentRow.Index].Value.ToString();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.ImageLocation = p;
        }

        private void загрузитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(this, null);
            button2_Click(this, null);
        }
    }
}
