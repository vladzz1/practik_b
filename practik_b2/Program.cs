using System;

namespace practik_b2
{
    internal class Program
    {
        static void method(object? obj)
        {
            Tuple<int, int> tuple = (Tuple<int, int>)obj!;
            if (tuple.Item1 < tuple.Item2)
            {
                for (int i = tuple.Item1; i <= tuple.Item2; i++)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(50);
                }
            }
            else if (tuple.Item1 > tuple.Item2)
            {
                for (int i = tuple.Item1; i >= tuple.Item2; i--)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(50);
                }
            }
            else
            {
                Console.WriteLine(tuple.Item1);
            }
        }
        static void methodMax(object? array)
        {
            int[] arr = (int[])array!;
            Console.WriteLine($"maximum number: {arr.Max()}");
        }
        static void methodMin(object? array)
        {
            int[] arr = (int[])array!;
            Console.WriteLine($"minimum number: {arr.Min()}");
        }
        static void methodAverage(object? array)
        {
            int[] arr = (int[])array!;
            Console.WriteLine($"arithmetic mean: {arr.Average()}");
        }
        static void writeFile(object? array)
        {
            int[] arr = (int[])array!;
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                str += arr[i] + " ";
            }
            File.WriteAllText("file_b2", str);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.Write("enter start: ");
                int start = int.Parse(Console.ReadLine()!);
                Console.Write("enter end: ");
                int end = int.Parse(Console.ReadLine()!);
                Console.Write("enter how many threads to run: ");
                int number = int.Parse(Console.ReadLine()!);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("when finished, press any button");
                Console.ResetColor();
                Thread.Sleep(3000);
                Tuple<int, int> tuple = new Tuple<int, int>(start, end);
                if (number > 0)
                {
                    for (int i = 0; i < number; i++)
                    {
                        ParameterizedThreadStart threadStart = new ParameterizedThreadStart(method);
                        Thread thread = new Thread(threadStart);
                        thread.Start(tuple);
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("something went wrong");
                Console.ResetColor();
            }

            //----------------------------------------------

            Console.ReadKey();
            int[] arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                arr[i] = new Random().Next(201);
            }
            Console.WriteLine("------------------------------------------------");
            for (int i = 0; i < 100; i += 10)
            {
                Console.WriteLine($"{arr[i],-5}{arr[i + 1],-5}{arr[i + 2],-5}{arr[i + 3],-5}{arr[i + 4],-5}{arr[i + 5],-5}{arr[i + 6],-5}{arr[i + 7],-5}{arr[i + 8],-5}{arr[i + 9]}");
            }
            Console.WriteLine("------------------------------------------------");
            ParameterizedThreadStart threadStart1 = new ParameterizedThreadStart(methodMax);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start(arr);
            ParameterizedThreadStart threadStart2 = new ParameterizedThreadStart(methodMin);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start(arr);
            ParameterizedThreadStart threadStart3 = new ParameterizedThreadStart(methodAverage);
            Thread thread3 = new Thread(threadStart3);
            thread3.Start(arr);
            ParameterizedThreadStart threadStart4 = new ParameterizedThreadStart(writeFile);
            Thread thread4 = new Thread(threadStart4);
            thread4.Start(arr);
        }
    }
}
