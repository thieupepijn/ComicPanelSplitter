using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace ComicPanelsSplitter
{
    public class LogLine
    {
        public string Message { get; private set; }
        public int Number { get; private set; }

        public LogLine(string message, int number)
        {
            Message = message;
            Number = number;
        }


        public static string WriteLog(List<LogLine> loglines, string exportPath)
        {
            string logFileName = Path.Join(exportPath, "log.txt");
            loglines = loglines.OrderBy(l => l.Number).ToList();
            foreach(LogLine line in loglines)
            {
                File.AppendAllText(logFileName, line.Message);
                File.AppendAllText(logFileName, Environment.NewLine);
            }
            return logFileName;

        }

    }
}
