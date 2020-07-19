using System;
using System.IO;
using System.Text;

namespace Task1
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("=== Task 1 ===\n");

            StreamReader reader = null;
            string fileName = @"..\..\files\in.csv";
            bool firstNumber = true;
            int errorLines = 0;
            string line = null;
            string[] str = null;
            int index = 0;
            double number = 0;
            double sum = 0;
            string sumbol = null;

            try
            {
                reader = new StreamReader(fileName);

                StringBuilder result = new StringBuilder("result(");

                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        str = line.Split(';');
                        index = Int32.Parse(str[0]);
                        number = Double.Parse(str[index]);
                        sum += number;
                        sumbol = number >= 0 ? " + " : " - ";
                        if (firstNumber)
                        {
                            result.AppendFormat("{0:0.0#}", number);
                        }
                        else
                        {
                            result.Append(sumbol).AppendFormat("{0:0.0#}", Math.Abs(number));
                        }

                        firstNumber = false;

                    }
                    catch (FormatException)
                    {
                        errorLines++;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        errorLines++;
                    }
                }

                result.AppendFormat(") = {0:0.0#}", sum);
                reader.Close();

                Console.WriteLine(result);
                Console.WriteLine("error-lines = {0}", errorLines);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unable open file.");
            }

            Console.ReadKey();
        }
    }
}
