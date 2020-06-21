using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace ComicPanelsSplitter
{
    public class LogLine
    {
        public string FileName { get; set; }
        public string Message { get; private set; }
        public int Number { get; private set; }

        public LogLine(string fileName, string message, int number)
        {
            FileName = fileName;
            Message = message;
            Number = number;
        }


        public static string WriteLog(List<LogLine> loglines, string exportPath)
        {
            string logFileName = Path.Join(exportPath, "log.txt");
            loglines = loglines.OrderBy(l => l.FileName.Length).ThenBy(l => l.FileName).ToList(); //, new NumberComparer()).ToList(); 
                for (int counter = 0; counter<loglines.Count; counter++)
            {
                LogLine line = loglines[counter];
                File.AppendAllText(logFileName, line.Message);
                File.AppendAllText(logFileName, Environment.NewLine);
            }
            return logFileName;

        }

    }

    public class NumberComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x > y)
            {
                return 1;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
                    
        }
    }


}
