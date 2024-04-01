using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3(120, 700);
            Task4();
            Task5();
        }

        static void Task1()
        {
            Task t = new Task(() => Console.WriteLine($"\nStart: {DateTime.Now}\n"));
            t.Start();
            Task.Factory.StartNew(() => Console.WriteLine($"\nFactory.StartNew: {DateTime.Now}\n"));
            Task.Run(() => Console.WriteLine($"\nRun: {DateTime.Now} \n"));
        }

        static void Task2()
        {
            Task t = Task.Run(() =>
            {
                Console.Write("\nPrime numbers:");
                for (int i = 2; i <= 1000; i++)
                {
                    if (checkIsPrime(i))
                        Console.Write($"{i, -5}");
                }
                Console.WriteLine();
            });

            t.Wait(); 
        }
        static bool checkIsPrime(int number)
        {
            if (number < 2)
                return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        static void Task3(int begin, int end)
        {
            Task<int> t = Task.Run(() =>
            {
                int count = 0;
                for (int i = begin; i <= end; i++)
                {
                    if (checkIsPrime(i))
                        count++;
                }
                return count;
            });
            t.Wait();
            Console.WriteLine($"\n\nCount of prime numbers: {t.Result}");
        }

        static void Task4()
        {
            float[] arr = { 4,45,58,5,0,-347,415,12,14,78,102 };

            Task<float> min = Task.Run(() => arr.Min());
            Task<float> max = Task.Run(() => arr.Max());
            Task<float> avg = Task.Run(() => arr.Average());
            Task<float> sum = Task.Run(() => arr.Sum());

            Task.WaitAll(min, max, avg,sum);
            Console.WriteLine($"\nMIN: {min.Result}\nMAX: {max.Result}\nAVG: {avg.Result}\nSUM: {sum.Result}");
        }

        static void Task5()
        {
            int[] arr = { 4, 45, 58, 5, 0, -347, 415, 12, 4, 78, 12 };

            Task t = Task.Run(() => { arr = arr.Distinct().ToArray();})
                .ContinueWith((previous) =>
            {
                Array.Sort(arr);
            }).ContinueWith((prevTask) =>
            {
                int n = 12;
                int index = Array.BinarySearch(arr, n);
                if (index >= 0)
                    Console.WriteLine($"\n{n} found at index {index}");
                else
                    Console.WriteLine($"\n{n} not found");
            });

            t.Wait();
        }
    }
}

    

