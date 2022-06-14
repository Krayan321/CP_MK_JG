using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Data
{
    public class Logger
    {
        private Queue<BallLogData> logs = new Queue<BallLogData>();
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

        private Queue<BallLogData> GetLogs()
        {
            mutex.WaitOne();
            Queue<BallLogData> tempLogs = new Queue<BallLogData>(logs);
            logs.Clear();
            mutex.ReleaseMutex();
            return tempLogs;
        }

        public void Logging()
        {
            while(true)
            {
                Queue<BallLogData> tempLogs = GetLogs();

                while(tempLogs.Count > 0)
                {
                    BallLogData temp = tempLogs.Dequeue();
                    StringBuilder builder = new StringBuilder();
                    builder.Append($"Ball ID: {temp.Id} ");
                    builder.Append($"Ball X: {temp.Position_X} ");
                    builder.Append($"Ball Y: {temp.Position_Y} ");
                    builder.Append($"Ball SpeedX: {temp.Movement[0]} ");
                    builder.Append($"Ball SpeedY: {temp.Movement[1]}");
                    File.AppendAllText(filename, builder.ToString() + "\n");
                }

                Thread.Sleep(this.delay);
            }
        }

        public void AddLog(BallLogData log)
        {
            mutex.WaitOne();
            logs.Enqueue(log);
            mutex.ReleaseMutex();
        }
    }
}
