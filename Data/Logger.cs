using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Data
{
    public class Logger
    {
        private Queue<string> logs = new Queue<string>();
        private Mutex mutex = new Mutex();
        private String filename;
        private Thread thread;
        private int delay = 100;

        public Logger()
        {
            filename = $"Logs {DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss")}.txt";
            thread = new Thread(Logging);
            thread.IsBackground = true;
        }

        public void StartLogging()
        {
            thread.Start();
        }

        private Queue<String> GetLogs()
        {
            mutex.WaitOne();
            Queue<String> tempLogs = new Queue<String>(logs);
            logs.Clear();
            mutex.ReleaseMutex();
            return tempLogs;
        }

        public void Logging()
        {
            while(true)
            {
                Queue<String> tempLogs = GetLogs();

                while(tempLogs.Count > 0)
                {
                    File.AppendAllText(filename, tempLogs.Dequeue() + "\n");
                }

                Thread.Sleep(this.delay);
            }
        }

        public void AddLog(String log)
        {
            mutex.WaitOne();
            logs.Enqueue(log);
            mutex.ReleaseMutex();
        }
    }
}
