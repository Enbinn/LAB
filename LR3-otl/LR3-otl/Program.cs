using System;

namespace LR3_otl
{

    delegate double Lamda(double num1, double num2);
    internal class Program
    {
        static void Table(double x, double y, Lamda lamda, string name)
        {
            Console.WriteLine();
            Console.WriteLine(name);
            for (int i = 1; i <= x; i++)
            {

                Console.Write($"\t{i}");
            }
            Console.WriteLine();
            for (int i = 1; i <= y; i++)
            {

                Console.Write(i);
                for (int j = 1; j <= x; j++)
                {

                    Console.Write($"\t{lamda(i, j)}");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Lamda umnoj = (num1, num2) => num1 * num2;
            Lamda stepen = (num1, num2) => Math.Pow(num2, num1);
            Table(10, 10, umnoj, "Таблица умножения");
            Table(10, 5, stepen, "Таблица степеней");
        }


    }
}

