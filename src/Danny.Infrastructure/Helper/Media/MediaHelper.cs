using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Danny.Infrastructure.Helper.Media
{
    public class MediaHelper
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        private static string _durLength;

        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
            string lpstrCommand,
            string lpstrReturnString,
            int uReturnLength,
            int hwndCallback
            );

        ///<summary>
        /// 获取媒体时长(秒),不支持中文文件名
        ///</summary>
        ///<returns></returns>
        public static double GetFileDurationByMci(string fileName)
        {
            _durLength = "".PadLeft(128, Convert.ToChar(" "));
            mciSendString(string.Format("status {0} length", fileName), _durLength, _durLength.Length, 0);
            _durLength = _durLength.Trim().TrimEnd('\0');
            return double.Parse(_durLength) / 1000;
        }

        ///<summary>
        /// 获取媒体时长(秒)
        ///</summary>
        ///<returns></returns>
        public static double GetFileDuration(string fileName, string ffmpegPath)
        {
            double fileDuration = 0;
            using (var pro = new Process())
            {
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.ErrorDialog = false;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.FileName = ffmpegPath;
                pro.StartInfo.Arguments = " -i " + fileName;
                pro.Start();
                StreamReader sr = pro.StandardError;
                pro.WaitForExit(1000);
                string result = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(result))
                {
                    string durationStr = result.Substring(result.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00.00").Length);
                    TimeSpan span = TimeSpan.Parse(durationStr);
                    fileDuration = span.TotalSeconds;
                }
            }
            return fileDuration;
        }

        ///<summary>
        /// 获取媒体格式
        ///</summary>
        ///<returns>空值表示未识别媒体</returns>
        public static string GetFileFormat(string fileName, string ffmpegPath)
        {
            string fileFormat = string.Empty;
            using (System.Diagnostics.Process pro = new System.Diagnostics.Process())
            {
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.ErrorDialog = false;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.FileName = ffmpegPath;
                pro.StartInfo.Arguments = " -i " + fileName;
                pro.Start();
                StreamReader sr = pro.StandardError;
                pro.WaitForExit(1000);
                string result = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(result))
                {

                    if (result.IndexOf("Header missing") > -1)
                    {
                        fileFormat = "error";
                    }
                    else
                    {
                        string pattern = @"([a-z0-9]+\s@\s[a-z0-9]+)";
                        Match m = Regex.Match(result, pattern);
                        if (m.Success)
                        {
                            fileFormat = m.ToString();
                            if (!string.IsNullOrEmpty(fileFormat))
                            {
                                fileFormat = fileFormat.Split('@')[0].Trim();
                            }
                        }
                        var fileAudio = result.Substring(result.IndexOf("Audio: ") + ("Audio: ").Length, ("mp3").Length);
                        if (fileFormat == "mp3" && fileAudio == "mp3")
                        {
                            fileFormat = "mp3";
                        }
                        else
                        {
                            fileFormat = fileAudio;
                        }
                    }
                }
            }
            return fileFormat;
        }

        ///<summary>
        /// 获取媒体信息
        ///</summary>
        ///<returns>FFmpeg信息</returns>
        public static string GetFileInfoByFFmpeg(string fileName, string ffmpegPath)
        {
            string fileInfo = string.Empty;
            using (System.Diagnostics.Process pro = new System.Diagnostics.Process())
            {
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.ErrorDialog = false;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.FileName = ffmpegPath;
                pro.StartInfo.Arguments = " -i " + fileName;
                pro.Start();
                StreamReader sr = pro.StandardError;
                pro.WaitForExit(1000);
                fileInfo = sr.ReadToEnd();
            }
            return fileInfo;
        }

        /// <summary>
        /// 支持wav、mp3转换成mp3
        /// </summary>
        /// <param name="fullPathFile"></param>
        /// <param name="lamePath"></param>
        public static string ConvertToMp3(string fullPathFile, string lamePath)
        {
            string afterFullPathFile = fullPathFile + ".mp3";
            System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
            processStartInfo.FileName = lamePath;
            processStartInfo.Arguments = " -a --resample 44.1 -b32 " + fullPathFile + " " + afterFullPathFile;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            System.Diagnostics.Process procDecode = System.Diagnostics.Process.Start(processStartInfo);
            while (!procDecode.HasExited)
            {
                procDecode.WaitForExit(3000);
            }
            return afterFullPathFile;
        }

        /// <summary>
        /// 支持wav、mp3转换成wav
        /// </summary>
        /// <param name="fullPathFile"></param>
        /// <param name="lamePath"></param>
        public static string ConvertToWav(string fullPathFile, string lamePath)
        {
            string afterFullPathFile = fullPathFile + ".wav";
            System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
            processStartInfo.FileName = lamePath;
            processStartInfo.Arguments = " --decode " + fullPathFile + " " + afterFullPathFile;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            System.Diagnostics.Process procDecode = System.Diagnostics.Process.Start(processStartInfo);
            while (!procDecode.HasExited)
            {
                procDecode.WaitForExit(3000);
            }
            return afterFullPathFile;
        }

        ///<summary>
        /// 用ffmpeg转音频格式为Mp3
        ///</summary>
        ///<returns></returns>
        public static void ConvertToMp3ByFFmpeg(string fullPathFile, string ffmpegPath)
        {
            string afterFullPathFile = fullPathFile + ".mp3";
            using (var pro = new System.Diagnostics.Process())
            {
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.ErrorDialog = false;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.FileName = ffmpegPath;
                pro.StartInfo.Arguments = string.Format("-i {0} -acodec libmp3lame -ar 44100 -ab 32k -ac 2 {1}", fullPathFile, afterFullPathFile);
          
                pro.Start();
                StreamReader sr = pro.StandardError;
                for (int i = 0; i < 4; i++)
                {
                    pro.WaitForExit(500);
                    string result = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(result))
                    {
                        //Loger.Current.Write("MediaHelper.ConvertToMp3ByFFmpeg() result=" + result);
                        break;
                    }
                }
            }
        }

  
       

    }
}
