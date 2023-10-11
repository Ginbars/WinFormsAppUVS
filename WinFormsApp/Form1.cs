using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Data.Odbc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private readonly Random Rng = new();
        private ThreadingHandler Threading { get; set; }

        public Form1()
        {
            InitializeComponent();
            var items = new BindingList<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("2", 2),
                new KeyValuePair<string, int>("3", 3),
                new KeyValuePair<string, int>("4", 4),
                new KeyValuePair<string, int>("5", 5),
                new KeyValuePair<string, int>("6", 6),
                new KeyValuePair<string, int>("7", 7),
                new KeyValuePair<string, int>("8", 8),
                new KeyValuePair<string, int>("9", 9),
                new KeyValuePair<string, int>("10", 10),
                new KeyValuePair<string, int>("11", 11),
                new KeyValuePair<string, int>("12", 12),
                new KeyValuePair<string, int>("13", 13),
                new KeyValuePair<string, int>("14", 14),
                new KeyValuePair<string, int>("15", 15)
            };

            comboBox1.DataSource = items;
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Key";

            Threading = new((x, y) => listView1.Invoke(() => AddToListView(x, y)));
            RestoreList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Threading.ThreadsActive)
            {
                Threading.StopThreading();
                button1.Text = "Stopping";
                button1.Enabled = false;
                Thread.Sleep(2000);
                button1.Text = "Start";
                button1.Enabled = true;
            }
            else
            {
                Threading.StartThreading((int)comboBox1.SelectedValue);
                button1.Text = "Stop";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Threading.StopThreading();
        }

        private void AddToListView(int thread, string data)
        {
            if (listView1.Items.Count == 20)
            {
                listView1.Items.RemoveAt(19);
            }
            ListViewItem entry = new(new string[2] { thread.ToString(), data });
            listView1.Items.Insert(0, entry);
        }

        private void RestoreList()
        {
            var data = DatabaseHandler.FetchLatestData();
            foreach (var entry in data)
            {
                AddToListView(entry.Item1, entry.Item2);
            }
        }
    }
}