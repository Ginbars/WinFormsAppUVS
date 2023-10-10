using System.Diagnostics;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartThreading(4);
            Debug.WriteLine("omegalul");
        }

        void StartThreading(int count)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            int id = 1;
            for (int i = 0; i < count; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadWork));
                t.Start(new Tuple<CancellationToken, int>(cts.Token, id++));
            }
        }

        void ThreadWork(object? data)
        {
            Debug.WriteLine("lol");
            if (data == null || data is not Tuple<CancellationToken, int> tuple)
                return;

            Debug.WriteLine("lmao" + tuple.Item2);

            while (true)
            {
                var rng = new Random();
                Thread.Sleep(500 + rng.Next(1500));

                Debug.WriteLine("aware" + tuple.Item2);
                if (tuple.Item1.IsCancellationRequested)
                {
                    Debug.WriteLine("rip" + tuple.Item2);
                    break;
                }

                string line = GenerateRandomLine();
                StoreData(tuple.Item2, line);
            }
        }

        void StoreData(int thread, string data)
        {
            DateTime time = DateTime.Now;
            //To-do
            Debug.WriteLine("Thread nr: " + thread + ", Time: " + time + ", Data: " + data);
        }

        string GenerateRandomLine()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var rng = new Random();
            int length = 5 + rng.Next(5);
            string line = "";
            for (int i = 0; i < length; i++)
                line += chars[rng.Next(chars.Length)];
            return line;
        }
    }
}