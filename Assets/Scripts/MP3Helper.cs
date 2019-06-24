/*
 * 作用：用于获取 mp3 文件的信息
 * https://www.jianshu.com/p/0fef74f3f886
 * 
 */

using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace mp3infos
{
    public struct MP3Info
    {
        public string Title;
        public string Singer;
        public string Album;
        public string Year;
        public string Comment;

        public override string ToString()
        {
            return "MP3附加信息:" + Environment.NewLine +
             "-----------------------------" + Environment.NewLine +
             "标 题: " + Title + Environment.NewLine +
             "歌 手: " + Singer + Environment.NewLine +
             "唱片集: " + Album + Environment.NewLine +
             "出版期: " + Year + Environment.NewLine +
             "备　注: " + Comment;
        }
    }

    public class MP3Helper
    {
        public static MP3Info ReadMP3Info(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return StreamHandler(fs);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            return default(MP3Info);
        }
        public static MP3Info ReadMP3Info(Stream stream)
        {
            return StreamHandler(stream);
        }
        private static MP3Info StreamHandler(Stream stream)
        {
            byte[] b = new byte[128];
            MP3Info mp3struct = new MP3Info();
            stream.Seek(-128, SeekOrigin.End);
            stream.Read(b, 0, 128);
            string MP3Flag = System.Text.Encoding.Default.GetString(b, 0, 3);
            if (MP3Flag == "TAG")
            {
                mp3struct.Title = System.Text.Encoding.Default.GetString(b, 3, 30);
                mp3struct.Singer = System.Text.Encoding.Default.GetString(b, 33, 30);
                mp3struct.Album = System.Text.Encoding.Default.GetString(b, 63, 30);
                mp3struct.Year = System.Text.Encoding.Default.GetString(b, 93, 4);
                mp3struct.Comment = System.Text.Encoding.Default.GetString(b, 97, 30);
            }
            return mp3struct;
        }
    }
  
}
