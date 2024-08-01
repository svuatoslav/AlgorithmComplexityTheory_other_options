using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main()
        {
            Stopwatch _stopwatch = new Stopwatch();
            string number = Console.ReadLine();
            _stopwatch.Start();
            bool result = prime(number);
            _stopwatch.Stop();
            Console.WriteLine(result);
            Console.WriteLine($"число: {Convert.ToUInt64(number, 2)}");
            Console.WriteLine($"Практическая сложность: {_stopwatch.ElapsedMilliseconds} миллисекунд");
            Console.WriteLine($"Теоретическая сложность: O(log(N)) = O({Math.Log(Convert.ToUInt64(number, 2))})");
            //_stopwatch.Reset();
            Console.ReadKey();
        }
        static bool prime(string number)
        {
            double root = Math.Sqrt(Convert.ToUInt64(number, 2));
            ulong binary = Convert.ToUInt64(number);
            for (int i = 2; i <= root ; i++)
                if (binary % Convert.ToUInt64(Convert.ToString(i, 2)) == 0)
                    return false;
            return true;
        }
    }
}
