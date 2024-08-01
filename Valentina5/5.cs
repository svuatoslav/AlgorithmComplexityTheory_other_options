using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentina5
{
    internal class _5
    {
        private readonly List<List<int>> _s = new List<List<int>>();
        private readonly List<List<List<int>>> _c = new List<List<List<int>>>();
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly List<Dictionary<int, Color>> _certificates = new List<Dictionary<int, Color>>();
        public _5()
        {
            Data();
            for (int i = 0; i < _s.Count; i++)
            {
                _stopwatch.Start();
                bool result = Verifier(_s[i], _c[i], _certificates[i]);
                _stopwatch.Stop();
                Console.WriteLine(result);
                Console.WriteLine($"Практическая сложность: {_stopwatch.ElapsedMilliseconds} миллисекунд");//{stopwatch.ElapsedMilliseconds} миллисекунд {stopwatch.ElapsedTicks} тактов
                Console.WriteLine($"Теоретическая сложность: O({_c[i].Count} множеств) = O({_c[i].Count})");
                _stopwatch.Reset();
            }
        }
        private bool Verifier(List<int> S, List<List<int>> C, Dictionary<int, Color> certificate)
        {
            foreach (var set in C)
                if (set.Count <= 2)
                {
                    if (certificate[set[0]] == certificate[set[1]])
                        return false;
                }
                else
                    return false;
            return true;
        }
        private void Data()
        {
            List<int> S;
            List<List<int>> C;
            Dictionary<int, Color> certificate;
            S = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            C = new List<List<int>>
            {
                new List<int>{ 1, 2},
                new List<int>{ 1, 2, 3},
                new List<int>{ 3, 4},
                new List<int>{ 4, 5, 6},
                new List<int>{ 5, 6},
                new List<int>{ 7, 8, 9, 10},
            };//C=false, certificate=false
            certificate = new Dictionary<int, Color>()
            {
                [1] = Color.Red,
                [2] = Color.Blue,
                [3] = Color.Red,
                [4] = Color.Blue,
                [5] = Color.Red,
                [6] = Color.Blue,
                [7] = Color.Blue,
                [8] = Color.Red,
                [9] = Color.Red,
                [10] = Color.Blue
            };
            _s.Add(S);
            _c.Add(C);
            _certificates.Add(certificate);
            S = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            C = new List<List<int>>
            {
                new List<int>{ 1, 2},
                new List<int>{ 2, 3},
                new List<int>{ 3, 4},
                new List<int>{ 4, 5},
                new List<int>{ 5, 6},
                new List<int>{ 6, 7},
                new List<int>{ 7, 8},
                new List<int>{ 8, 9},
                new List<int>{ 9, 10},
            };//C=True, certificate=True
            certificate = new Dictionary<int, Color>()
            {
                [1] = Color.Red,
                [2] = Color.Blue,
                [3] = Color.Red,
                [4] = Color.Blue,
                [5] = Color.Red,
                [6] = Color.Blue,
                [7] = Color.Red,
                [8] = Color.Blue,
                [9] = Color.Red,
                [10] = Color.Blue
            };
            _s.Add(S);
            _c.Add(C);
            _certificates.Add(certificate);
            S = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            C = new List<List<int>>
            {
                new List<int>{ 1, 2},
                new List<int>{ 1, 3},
                new List<int>{ 1, 4},
                new List<int>{ 1, 5},
                new List<int>{ 1, 6},
                new List<int>{ 1, 7},
                new List<int>{ 1, 8},
                new List<int>{ 1, 9},
                new List<int>{ 1, 10},
            };//C=True, certificate=False
            certificate = new Dictionary<int, Color>()
            {
                [1] = Color.Red,
                [2] = Color.Blue,
                [3] = Color.Red,
                [4] = Color.Blue,
                [5] = Color.Red,
                [6] = Color.Blue,
                [7] = Color.Red,
                [8] = Color.Blue,
                [9] = Color.Red,
                [10] = Color.Blue
            };
            _s.Add(S);
            _c.Add(C);
            _certificates.Add(certificate);
        }
        private enum Color
        {
            Red,
            Blue
        }
    }
}
