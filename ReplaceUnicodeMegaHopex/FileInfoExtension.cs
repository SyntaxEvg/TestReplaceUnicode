using ReplaceUnicodeMegaHopex.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReplaceUnicodeMegaHopex
{
    public static class FileInfoExtension
    {
        /// <summary>
        /// Чтобы не перегружать память при считывание огромный файлов, читаем его построчно
        /// </summary>
        public static IEnumerable<string> ReadLines(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("File not found");
            using ( var stream = fileInfo.OpenText())
            {
                while (!stream.EndOfStream)
                    yield return stream.ReadLine();

            }
           
        }
        /// <summary>
        /// Чтобы не перегружать память при считывание огромный файлов, читаем его построчно
        /// </summary>
        public static IEnumerable<string> ReadLines(this StreamReader stream)
        {          
                while (!stream.EndOfStream)
                    yield return stream.ReadLine();
          
        }
        /// <summary>
        /// Cчитывает весь текст и помещает в StringBuilder
        /// </summary>
        public async static Task<StringBuilder> ReadTextBuilderAsync(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("File not found");
            StringBuilder @string = new StringBuilder();
            using (var stream = fileInfo.OpenText())
            {
                var t = await stream.ReadToEndAsync();
                //var t =await fileInfo.OpenText().ReadToEndAsync();
                return @string.Append(t);
            }
               
        }
        /// <summary>
        /// Cчитывает весь текст и помещает в StringBuilder
        /// </summary>
        public static StringBuilder ReadTextBuilder(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("File not found");
            StringBuilder @string = new StringBuilder();
            var lang = Dictionarys.lang;
            using (var stream = fileInfo.OpenText())
            {
                var str = stream.ReadToEnd();
                string decodedSequence = Regex.Unescape(str);
                Console.WriteLine(decodedSequence);
                return @string.Append(decodedSequence);
            }
               
        }
        /// <summary>
        /// Cчитывает весь текст и по строкам в StringBuilder
        /// </summary>
        public static StringBuilder ReadTextReadLines(this FileInfo fileInfo)
        {
            if (!fileInfo.Exists) throw new FileNotFoundException("File not found");
            StringBuilder @string = new StringBuilder();
            //var lang = Dictionarys.lang;
            using (var stream = fileInfo.OpenText())
            {
                //var str = stream.ReadLines();
                foreach (var str in stream.ReadLines())
                {
                    string decodedSequence = Regex.Unescape(str);
                    @string.AppendLine(decodedSequence);
                    Console.WriteLine(decodedSequence);
                }
                //var t =await fileInfo.OpenText().ReadToEndAsync();
                //var build = @string.Append(t);



                //foreach (KeyValuePair<string, string> kvp in lang)
                //{
                //    build.Replace(kvp.Value, kvp.Key);
                //}
                return @string;
            }

        }

        /// <summary>Проверка на существование файла. Если файл не существует, то генерируется исключение</summary>
        /// <param name="file">Проверяемый файл</param>
        /// <param name="Message">Сообщение, добавляемое в исключение</param>
        /// <returns>Информация о файле</returns>
        /// <exception cref="FileNotFoundException">если файл не существует</exception>
        public static FileInfo ThrowIfNotFound(this FileInfo file, string Message = null)
        {
            file.Refresh();
            return file.Exists ? file : throw new FileNotFoundException(Message ?? $"Файл {file} не найден");
        }

    }
}
