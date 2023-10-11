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
        private CancellationTokenSource? CTS { get; set; }
        private readonly Random Rng = new();

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

            RestoreList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThreading((int)comboBox1.SelectedValue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CTS is CancellationTokenSource source)
            {
                source.Cancel();
            }
        }

        private void StartThreading(int count)
        {
            if (CTS is CancellationTokenSource source)
            {
                source.Dispose();
            }
            CTS = new CancellationTokenSource();
            int id = 1;
            for (int i = 0; i < count; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadWork));
                t.Start(new Tuple<CancellationToken, int>(CTS.Token, id++));
            }
        }

        private void ThreadWork(object? data)
        {
            if (data == null || data is not Tuple<CancellationToken, int> tuple)
                return;

            var rng = new Random();
            while (true)
            {
                Thread.Sleep(500 + rng.Next(1500));

                if (tuple.Item1.IsCancellationRequested)
                {
                    break;
                }

                string line = GenerateRandomLine();
                listView1.Invoke(() => AddToListView(tuple.Item2, line));
                DatabaseHandler.StoreData(tuple.Item2, line);
            }
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

        private string GenerateRandomLine()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 5 + Rng.Next(5);
            string line = "";
            for (int i = 0; i < length; i++)
                line += chars[Rng.Next(chars.Length)];
            return line;
        }
    }
}