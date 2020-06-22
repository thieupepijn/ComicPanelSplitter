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
        public FileInfo FileInfo { get; private set; }
        public int NumberOfPanels { get; private set; }
        public string Message { get; private set; }
       
        public LogLine(FileInfo fileInfo, int numberOfPanels)
        {
            FileInfo = fileInfo;
            NumberOfPanels = numberOfPanels;
            Message = string.Format("split {0} into {1} panels", FileInfo.Name, numberOfPanels);
        }


        public static string WriteLog(List<LogLine> loglines, string exportPath)
        {
            string logFileName = Path.Join(exportPath, "log.txt");
            loglines = loglines.OrderBy(l => l.FileInfo.DirectoryName).ThenBy(l => l.FileInfo.Name.Length).ThenBy(l => l.FileInfo.Name).ToList(); 
                for (int counter = 0; counter<loglines.Count; counter++)
            {
                LogLine line = loglines[counter];
                File.AppendAllText(logFileName, line.Message);
                File.AppendAllText(logFileName, Environment.NewLine);
            }
            return logFileName;

        }

    }
}
