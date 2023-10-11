using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{
    public class ThreadingHandler
    {
        public bool ThreadsActive { get; set; }

        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private CancellationTokenSource? CTS { get; set; }
        private Action<int, string> ModifyList { get; set; }

        public ThreadingHandler(Action<int, string> modList)
        {
            ModifyList = modList;
        }

        public void StartThreading(int count)
        {
            CTS = new CancellationTokenSource();
            int id = 1;
            for (int i = 0; i < count; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ThreadWork));
                t.Start(new Tuple<CancellationToken, int>(CTS.Token, id++));
            }
            ThreadsActive = true;
        }

        public void StopThreading()
        {
            CTS.Cancel();
            ThreadsActive = false;
        }

        public void DisposeToken()
        {
            CTS.Dispose();
        }

        public void ThreadWork(object? data)
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

                string line = GenerateRandomLine(rng);
                ModifyList(tuple.Item2, line);
                DatabaseHandler.StoreData(tuple.Item2, line);
            }
        }

        private string GenerateRandomLine(Random rng)
        {
            int length = 5 + rng.Next(5);
            string line = "";
            for (int i = 0; i < length; i++)
                line += Chars[rng.Next(Chars.Length)];
            return line;
        }
    }
}
