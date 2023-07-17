using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReplaceUnicodeMegaHopex.Tables;
namespace ReplaceUnicodeMegaHopex
{
    public class ReplaceText
    {
        public void Replace(string path = null)
        {
            if (path == null)
            {
                var CurrentDirectoryPath = Environment.CurrentDirectory;// сюда писать путь до папки
                DirectoryInfo dir = new DirectoryInfo(CurrentDirectoryPath);
                if (!(dir.Exists))
                {
                    //_logger.LogError($"Error folder path not found: {nameof(WathJsonFileHostedService)},{CurrentDirectoryPath}");
                    // Environment.Exit(1);
                }

                foreach (var file in dir.EnumerateFiles("*.json", SearchOption.AllDirectories))
                {
                    try
                    {
                        EnumerateFiles(file);//.ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError($"Error folder path not foreach: {nameof(WathJsonFileHostedService)},{ex.StackTrace}");
                    }
                }

            }
        }
        private void EnumerateFiles(FileInfo fileInfo)
        {
            try
            {
                var @string = fileInfo.ReadTextReadLines();//    Dictionary.lang;
                var newFolder =fileInfo.DirectoryName.Replace("job-auto-en", "job-auto-ru");
                //C:\Users\user\source\repos\TestReplaceUnicode\TestReplaceUnicode\bin\Debug
                if (!(Directory.Exists(newFolder))) Directory.CreateDirectory(newFolder);

                File.WriteAllText(Path.Combine(newFolder, fileInfo.Name), @string.ToString(), Encoding.UTF8);
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
