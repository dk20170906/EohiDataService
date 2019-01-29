using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class CLog
{
    public static string DefaultLogDirectory = "C:/LOG/";
    public static string DefaultLogFile = "Log.txt";
    public static object Lock = new object();
    public static void SaveFullErrInfo(Exception exin)
    {
        try
        {
            lock (Lock)
            {
                string[] ErrorInfo = GetExceptionInfo(exin);
                if (!Directory.Exists(DefaultLogDirectory))
                {
                    Directory.CreateDirectory(DefaultLogDirectory);
                }
                string LogFile = "Exception.txt";

                string LogPath = String.Format(DefaultLogDirectory + LogFile);
                if (File.Exists(LogPath))
                {
                    string[] logLines = File.ReadAllLines(LogPath);
                    if (logLines.Length > 2000)
                    {
                        string[] rsltLines = new string[logLines.Length - 1500];
                        Array.Copy(logLines, logLines.Length - rsltLines.Length, rsltLines, 0, rsltLines.Length);
                        File.WriteAllLines(LogPath, rsltLines);
                    }
                }

                StreamWriter LogWriteFile = new System.IO.StreamWriter(LogPath, true);
                for (int i = 0; i < ErrorInfo.Length; i++)
                {
                    LogWriteFile.WriteLine(ErrorInfo[i]);
                }

                LogWriteFile.WriteLine("******************************************************************************************************");
                LogWriteFile.WriteLine("");
                LogWriteFile.Close();
                LogWriteFile.Dispose();
            }
        }
        catch(Exception ex)
        {
            CLog.SaveFullErrInfo(ex);
        }
    }

    public static string[] GetExceptionInfo(Exception exin)
    {
        System.DateTime Now = System.DateTime.Now;
        string[] ErrorInfo = new string[5];
        ErrorInfo[0] = String.Format("{0}.{1:D3} Exception Found:", Now, Now.Millisecond);
        ErrorInfo[1] = String.Format("错误类型: {0}", exin.GetType().FullName);
        ErrorInfo[2] = String.Format("错误信息: {0}", exin.Message);
        ErrorInfo[3] = String.Format("错误代码: {0}", exin.Source);
        ErrorInfo[4] = String.Format("堆栈信息: {0}", exin.StackTrace);
        return ErrorInfo;
    }

    public static void WriteLog(string LogDirectory, string LogFile, string LogInfo)
    {
        try
        {
            if(LogDirectory!="")
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
            FileStream objFileStream = new FileStream(LogDirectory + LogFile, FileMode.Append, FileAccess.Write, FileShare.None);
            StreamWriter objStreamWriter = new StreamWriter(objFileStream);
            System.DateTime Now = System.DateTime.Now;
            string strout = string.Format("{0}:{1:D3} * ", Now, Now.Millisecond) + LogInfo;
            objStreamWriter.WriteLine(strout);
            objStreamWriter.Close();
        }
        catch (Exception ex)
        {
            SaveFullErrInfo(ex);
        }
    }

    public static void WriteLog(string LogInfo)
    {
        WriteLog(AppDomain.CurrentDomain.BaseDirectory, DefaultLogFile, LogInfo);
    }

    public static string PutDownErrInfo(string LogInfo)
    {
        WriteLog(AppDomain.CurrentDomain.BaseDirectory, DefaultLogFile, LogInfo);
        return LogInfo;
    }
}
