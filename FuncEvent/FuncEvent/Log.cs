using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncEvent
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs()
        {

        }

        public bool HasException { get; set; }
        public LogType LogType { get; set; }
        public int LogLevel { get; set; }
        public int ID { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
    public enum LogType
    {
        Critical = 1,
        Error = 2,
        Warning = 4,
        Information = 8,
        Verbose = 16,
        Program = 32,
        Result = 64,
        ETC1 = 128,
        ETC2 = 256
    }
    public class LogM
    {
        //public LogM()
        //{

        //}
        //public LogM(string logDirectory)
        //{
        //    if (Directory.Exists(logDirectory))
        //    {
        //        Directory.CreateDirectory(logDirectory);
        //    }
        //}
        //public void AddLogMessage(LogType type, int id, Exception e, bool flush = true)
        //{

        //}

    }
    public partial class LogDbItem
    {
        public LogType IntLogType { get; set; }
    }

    public class Log
    {
        public event EventHandler<LogEventArgs> OnLogEvent = delegate { };

        string LogDirectory;
        public Dictionary<int, string> LogFiles { get; private set; }

        private List<LogDbItem> items = new List<LogDbItem>();
        private object l = new object();
        private bool useDb = false;
        private bool useFile = false;
        LogEntities logDb;
        public string CurFilePath = string.Empty;
        private Log()
        {
            throw new Exception("This routine cannot be executed");
        }

        public Log(string logDirectory)
        {
            SetFileLog(logDirectory);
        }

        public void SetFileLog(string logDirectory)
        {
            LogFiles = new Dictionary<int, string>();
            this.LogDirectory = logDirectory;
            LogFiles.Add(0xff, "Log");
            Directory.CreateDirectory(LogDirectory);
            Init();
            useFile = true;
        }

        public void SetDbLog(bool use)
        {
            if (use == true)
            {
                logDb = new LogEntities();
                Init();
            }
            this.useDb = use;
        }

        public Log(bool useDb)
        {
            SetDbLog(useDb);
        }

        private void Init()
        {
            AddLogMessage(LogType.Program, 0, "Start Program");
        }

        /// <summary>
        /// 종류별로 만들 파일을 추가 한다.
        /// 기본으로는 모든 파일에 대한 로그를 만든다.
        /// </summary>
        public void AddLogFileList(string fileName, params LogType[] types)
        {
            int result = 0;
            foreach (LogType t in types)
            {
                result |= (int)t;
            }
            LogFiles.Add(result, fileName);
        }

        public void ClearLogFileList()
        {
            LogFiles.Clear();
        }

        public void AddLogMessage(LogType type, int id, string strtmp)
        {
            LogDbItem item = new LogDbItem() { LogDate = DateTime.Now, LogText = strtmp, IntLogType = type, LogType = type.ToString() };
            if (useFile)
            {
                lock (l)
                {
                    items.Add(item);
                }
            }
            if (useDb)
            {
                logDb.AddToLog(item);
            }

            OnLogEvent(null, new LogEventArgs() { LogType = type, ID = id, Message = strtmp });
            FlushLogFile();
        }
        public void Information(string strtmp)
        {
            LogType type = LogType.Information;
            int id = 0;
            LogDbItem item = new LogDbItem() { LogDate = DateTime.Now, LogText = strtmp, IntLogType = type, LogType = type.ToString() };
            if (useFile)
            {
                lock (l)
                {
                    items.Add(item);
                }
            }
            if (useDb)
            {
                logDb.AddToLog(item);
            }

            OnLogEvent(null, new LogEventArgs() { LogType = type, ID = id, Message = strtmp });
            FlushLogFile();
        }

        public void AddLogMessage(LogType type, int id, Exception e)
        {
            while (e != null)
            {
                AddLogMessage(LogType.Error, 0, e.StackTrace);
                AddLogMessage(LogType.Error, 0, e.Message);
                e = e.InnerException;
            }
        }

        public void FlushLogFile()
        {
            if (useFile)
            {
                DateTime dt = DateTime.Now;

                try
                {
                    lock (l)
                    {
                        foreach (KeyValuePair<int, string> logFile in LogFiles)
                        {
                            var log = from p in items
                                      where ((int)p.IntLogType & logFile.Key) != 0
                                      select p;
                            if (log.Count() == 0)
                                continue;

                            string fileName = Path.Combine(LogDirectory, dt.ToString("yyyy-MM-dd_") + logFile.Value + ".txt");
                            CurFilePath = fileName;
                            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.Default))
                            {
                                foreach (LogDbItem item in log)
                                {
                                    sw.WriteLine(string.Format("{0}: {1}, {2}",
                                        item.LogDate.Value.ToString("MM-dd HH:mm:ss.FFF"),
                                        item.LogType,
                                        item.LogText));
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            if (useDb)
            {
                try
                {
                    logDb.SaveChanges();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Fail To Write Log:" + ee.Message);
                }
            }
            items.Clear();
        }

        public void CloseLog()
        {
            FlushLogFile();
        }
    }

}
