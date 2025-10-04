namespace practik_b3
{
    class Statistics
    {
        public static int numberOfCharacters = 0;
        public static int numberOfDigits = 0;
        public static int numberOfPunctuationMarks = 0;
    }
    internal class Program
    {
        static void textAnalyse(object obj)
        {
            string text = (string)obj;
            foreach (char item in text)
            {
                if (Char.IsLetter(item)) { Statistics.numberOfCharacters++; }
                if (Char.IsDigit(item)) { Statistics.numberOfDigits++; }
                if (Char.IsPunctuation(item)) { Statistics.numberOfPunctuationMarks++; }
            }
        }
        static void Main(string[] args)
        {
            Statistics statistics = new Statistics();

            string[] files = Directory.GetFiles(@$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\test");
            Thread[] threads = new Thread[files.Length];
            int a = 0;
            foreach (var file in files)
            {
                string text = File.ReadAllText(file);
                ParameterizedThreadStart threadStart = new ParameterizedThreadStart(textAnalyse!);
                threads[a] = new Thread(threadStart);
                threads[a].Start(text);
                a++;
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"number of characters: {Statistics.numberOfCharacters}");
            Console.WriteLine($"number of digits: {Statistics.numberOfDigits}");
            Console.WriteLine($"number of punctuation marks: {Statistics.numberOfPunctuationMarks}");
        }
    }
}
