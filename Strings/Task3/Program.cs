using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Task3
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                string inFileName = @"..\..\files\input.txt";
                string outFileName = @"..\..\files\output.txt";

                string file = File.ReadAllText(inFileName);

                //   /\*[\W\w]*?\*/  -  комментарии, которые нужно удалить
                //   //([\W\w]*?)\r?\n  -  заменяем на \r\n
                //   (\W*\w*=\s*""(.*?)"";)|(\W*\(\s*""(.*?)""\);)  -  строка с кавычками 

                string regular = @"/\*[\W\w]*?\*/|//([\W\w]*?)\r?\n|(\W*\w*=\s*""(.*?)"";)|(\W*\(\s*""(.*?)""\);)";

                Regex regex = new Regex(regular);
                string output = regex.Replace(file, ReplaceFormat);

                File.WriteAllText(outFileName, output);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }

            Console.ReadKey();
        }

        public static string ReplaceFormat(Match match)
        {
            if (match.Value.StartsWith("/*"))
                return string.Empty;
            else if (match.Value.StartsWith("//"))
                return "\r\n";

            return match.Value;
        }
    }
}
