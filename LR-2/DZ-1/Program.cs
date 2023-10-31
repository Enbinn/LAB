using System;
using System.IO;

namespace DZ_1
{



    internal class Program
    {
        class Drav0 : Exception
        {

        }
        class Dmin0 : Exception
        {

        }

        static void Main(string[] args)
        {
            int a;
            int b;
            int c;
            double D;

            try
            {
                using (TextReader reader = File.OpenText("test.txt"))
                {
                    string text = reader.ReadLine();
                    string[] bits = text.Split(' ');

                    try
                    {
                        a = int.Parse(bits[0]);
                        b = int.Parse(bits[1]);
                        c = int.Parse(bits[2]);
                        try
                        {
                            D = b * b - 4 * a * c;
                            Console.WriteLine("Дискриминант = "+D);
                            if (D < 0)
                            {
                                throw new Dmin0();
                            }
                            if (D == 0)
                            {
                                throw new Drav0();
                            }

                        }
                        catch (Drav0)
                        {
                            Console.WriteLine("ошибка: дискриминант равен нулю");
                        }
                        catch (Dmin0)
                        {
                            Console.WriteLine("ошибка: дискриминант меньше нуля");
                        }

                    }
                    catch
                    {
                        Console.WriteLine("ошибка: неверный формат данных");
                    }
                }
            }
            catch
            {
                Console.WriteLine("ошибка: файл отсутствует");
            }
            Console.ReadLine();
        }
    }
}
