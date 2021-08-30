using System.IO;
using Command_Line_Interface;
using Ini_lib;

namespace Log_file
{
    public class LogFile
    {
        public static void RecordAction(string path, string info)
        {
            LogFile logFile = new LogFile();
            logFile.LogFileWriter(path, info);
        }

        public void LogFileWriter(string path, string String)
        {
            if (path == null) return;

            string[] parametrs = String.Split(',', '/');
            using (FileStream fileWrite = new FileStream(path, FileMode.Append))
            {
                for (int i = 0; i < parametrs.Length; ++i)
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(parametrs[i] + '\n');
                    fileWrite.Write(array, 0, array.Length);
                }
            }
        }
    }
}