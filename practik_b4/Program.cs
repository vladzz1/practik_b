namespace practik_b4
{
    class MyArray
    {
        static public int[]? Array { get; set; }
    }
    internal class Program
    {
        static int binarySearch(int[] arr, int target)
        {
            int left = 0;
            int right = arr.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (arr[mid] == target) { return mid; }
                else if (arr[mid] < target) { left = mid + 1; }
                else { right = mid - 1; }
            }
            return -1;
        }
        static void task4(Task prevTask)
        {
            int[] arr = MyArray.Array!;
            arr.Order();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("\n");
            MyArray.Array = arr;
        }
        static void task5(Task prevTask)
        {
            try
            {
                int[] arr = MyArray.Array!;
                Console.Write("enter the number you want to find: ");
                int number = int.Parse(Console.ReadLine()!);
                int result = binarySearch(arr, number);
                if (result == -1) { Console.WriteLine("there is no such number in the array"); }
                else { Console.WriteLine($"number {arr[result]} found at index {result}"); }
            }
            catch (Exception x)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(x.Message);
                Console.ResetColor();
            }
        }
        static bool isPrime(int number)
        {
            if (number == 0 || number == 1)
            {
                return false;
            }
            else
            {
                for (int i = 2; i <= number / 2; i++)
                {
                    if (number % i == 0)
                    {
                        return false;
                    }

                }
                return true;
            }
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("when finished, press any button");
            Console.ResetColor();
            Task task1 = new Task(() => Console.WriteLine(DateTime.Now));
            task1.Start();
            Task.Factory.StartNew(() => Console.WriteLine(DateTime.Now));
            Task.Run(() => Console.WriteLine(DateTime.Now));
            Console.ReadLine();

            //-----------------------

            Console.WriteLine("-----------------------\n");

            Task task2 = new Task(() =>
            {
                try
                {
                    Console.Write("enter start: ");
                    int start = int.Parse(Console.ReadLine()!);
                    Console.Write("enter end: ");
                    int end = int.Parse(Console.ReadLine()!);
                    if (start < end)
                    {
                        for (int i = start; i <= end; i++)
                        {
                            if (isPrime(i)) { Console.Write(i + " "); }
                        }
                    }
                    else if (start > end)
                    {
                        for (int i = start; i >= end; i--)
                        {
                            if (isPrime(i)) { Console.Write(i + " "); }
                        }
                    }
                    else { if (isPrime(start)) { Console.WriteLine(start); } }
                }
                catch (Exception x)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(x.Message);
                    Console.ResetColor();
                }
            });
            task2.Start();
            task2.Wait();

            //-----------------------

            Console.WriteLine("\n\n-----------------------\n");

            int[] arr = new int[50];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Random().Next(20);
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("\n");
            Task[] tasks =
            [
               new Task(() => Console.WriteLine($"min: {arr.Min()}")),
               new Task(() => Console.WriteLine($"max: {arr.Max()}")),
               new Task(() => Console.WriteLine($"average: {arr.Average()}")),
               new Task(() => Console.WriteLine($"sum: {arr.Sum()}"))
            ];
            foreach (var task in tasks) { task.Start(); }
            Task.WaitAll(tasks);

            //-----------------------

            Console.WriteLine();

            Task task3 = new Task(() =>
            {
                arr.Distinct();
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
                Console.WriteLine("\n");
            });
            MyArray.Array = arr;
            task3.ContinueWith(task4).ContinueWith(task5);
            task3.Start();
            task3.Wait();
        }
    }
}