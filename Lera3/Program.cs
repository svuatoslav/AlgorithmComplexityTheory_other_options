using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lera3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[] Alphabet = { '0', '1' };
            const int Q = 31;
            const string DeltaPath = "C:\\Users\\svyatoslav\\Desktop\\магистратура\\1 курс 2 семестр\\Теория сложности алгоритмов\\AlgorithmComplexityTheory\\bin\\Debug\\Delta.xlsx";
            const int StartState = 0;
            int[] EndStates = new int[] { 3, 9, 30 };
            _ = new _3(Q, Alphabet, DeltaPath, StartState, EndStates);
        }
    }
}
