using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScreamMagic
{
    public static class LogData
    {
        public static string logString = "";
        public static void Add(string str)
        {
            logString += str;
        }

        public static void AddLine(string str)
        {
            logString += str + "\r\n";
        }

        public static void Clear()
        {
            logString = "";
        }

        public static void SaveToFile(string filename)
        {
            File.WriteAllText(filename, logString);
        }
    }
}
