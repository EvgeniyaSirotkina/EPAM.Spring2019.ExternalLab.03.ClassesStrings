using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Task2
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== Task 2 ===\n");

            StreamReader reader = null;
            StreamWriter writer = null;
            string inFileName = @"..\..\files\in.txt";
            string outFileName = @"..\..\files\out.txt";
            StringBuilder result = null;
            string line = null;

            // months whith 31 days
            string patterbPart1 = @"(\b([1-9]|[0-2][0-9]|3[0-1])(\.)(0?[13578]|1[02])(\.)([1-2][0-9]{3}|[0-9]{2})\b)|(\b([1-9]|[0-2][0-9]|3[0-1])(-)(0?[13578]|1[02])(-)([1-2][0-9]{3}|[0-9]{2})\b)|(\b([1-9]|[0-2][0-9]|3[0-1])(\/)(0?[13578]|1[02])(\/)([1-2][0-9]{3}|[0-9]{2})\b)|";
            // months whith 31 days
            string patterbPart2 = @"(\b([1-9]|[0-2][0-9]|30)(\.)(0?[469]|11)(\.)([1-2][0-9]{3}|[0-9]{2})\b)|(\b([1-9]|[0-2][0-9]|30)(-)(0?[469]|11)(-)([1-2][0-9]{3}|[0-9]{2})\b)|(\b([1-9]|[0-2][0-9]|30)(\/)(0?[469]|11)(\/)([1-2][0-9]{3}|[0-9]{2})\b)|";
            // february
            string patterbPart3 = @"(\b([1-9]|[0-1][0-9]|2[0-8])(\.)(02|2)(\.)([1-2][0-9]{3}|[0-9]{2})\b)|(\b([1-9]|[0-1][0-9]|2[0-8])(-)(02|2)(-)([1-2][0-9]{3}|[0-9]{2})\b)|(\b([1-9]|[0-1][0-9]|2[0-8])(\/)(02|2)(\/)([1-2][0-9]{3}|[0-9]{2})\b)|";
            // leap-year
            string patterbPart4 = @"(\b(29)(\.)(02|2)(\.)([1-2][0-9])?(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)\b)|(\b(29)(-)(02|2)(-)([1-2][0-9])?(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)\b)|(\b(29)(\/)(02|2)(\/)([1-2][0-9])?(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)\b)";
            string pattern = patterbPart1 + patterbPart2 + patterbPart3 + patterbPart4;

            //Regex dateRegex = new Regex(@"([1-9]|[0-2][0-9]|3[0-1])(\/|-|\.)(0[1-9]|[1-9]|1[0-2])(\/|-|\.)([1-2][0-9]{3}|[0-9]{2})");
            Regex dateRegex = new Regex(pattern);
            Regex moneyRegex = new Regex(@"\b(\d{1,3}\s+)?(\d{3}(\s+))*(blr|belarusian roubles)\b");
            MatchEvaluator dateEvaluator = new MatchEvaluator(ReplaceDate);
            MatchEvaluator moneyEvaluator = new MatchEvaluator(RemoveTrim);

            try
            {
                reader = new StreamReader(inFileName);
                result = new StringBuilder();

                while ((line = reader.ReadLine()) != null)
                {
                    line = dateRegex.Replace(line, dateEvaluator);
                    line = moneyRegex.Replace(line, moneyEvaluator);
                    result.AppendLine(line);
                }
                Console.WriteLine("Read successfully");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable open file.");
            }
            finally
            {
                reader.Close();
            }

            try
            {
                writer = new StreamWriter(outFileName);
                writer.Write(result.ToString());
                Console.WriteLine("Write successfully");
            }
            catch (IOException)
            {
                Console.WriteLine("Unable open file.");
            }
            finally
            {
                writer.Close();
            }

            Console.ReadKey();
        }

        public static string ReplaceDate(Match m)
        {
            try
            {
                DateTime result = DateTime.Parse(m.Value);
                return (result.ToString("MMMM dd, yyyy", CultureInfo.CreateSpecificCulture("en-US")));
            }
            catch (FormatException)
            {
                return m.Value;
            }
        }

        public static string RemoveTrim(Match m)
        {
            StringBuilder builder = new StringBuilder();

            int pos = m.Value.IndexOf('b');

            for (int i = 0; i < pos - 1; i++)
            {
                if (m.Value[i] != ' ')
                    builder.Append(m.Value[i]);
            }
            builder.Append(m.Value, pos - 1, m.Value.Length - pos + 1);

            return (builder.ToString());
        }

    }
}
