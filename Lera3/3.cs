using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Lera3
{
    internal class _3
    {
        private Vertex[] Vertexes = null;
        private List<List<int>> Paths = new List<List<int>>();
        private List<List<char>> TransitionConditions = new List<List<char>>();
        List<Loop> Loops = new List<Loop>();
        private sealed class Vertex
        {
            readonly internal int?[] IndexVertexes = null;
            readonly internal char[] TransitionCondition = null;
            readonly internal bool[] MarkedTransitionCondition = null;
            readonly internal bool End = false;
            internal bool Marked = false;
            public Vertex(int?[] indexVertexes, char[] transitionCondition, bool end)
            {
                IndexVertexes = new int?[indexVertexes.Length];
                Array.Copy(indexVertexes, IndexVertexes, indexVertexes.Length);
                TransitionCondition = new char[transitionCondition.Length];
                Array.Copy(transitionCondition, TransitionCondition, transitionCondition.Length);
                End = end;
                MarkedTransitionCondition = new bool[transitionCondition.Length];
                for (int i = 0; i < indexVertexes.Length; i++)
                    if (indexVertexes[i] == null)
                        MarkedTransitionCondition[i] = true;
            }
        }
        private sealed class Loop
        {
            readonly internal int[] IndexVertexes = null;
            readonly internal char[] TransitionCondition = null;//for comparison
            readonly internal int Quantity = 0;
            readonly internal LoopStatus State = LoopStatus.Equally;
            public Loop((int[], char[]) data)
            {
                IndexVertexes = data.Item1;
                TransitionCondition = data.Item2;
                int one = 0;
                int zero = 0;
                for (int i = 0; i < data.Item2.Length; i++)
                {
                    if (data.Item2[i] == 0)
                        zero++;
                    else if (data.Item2[i] == 1)
                        one++;
                    else
                        Console.WriteLine("Loop Create Error");
                }
                if (one > zero)
                    State = LoopStatus.One;
                else if (one < zero)
                    State = LoopStatus.Zero;
                Quantity = Math.Abs(one - zero);
            }
        }
        public _3(int Q, char[] Alphabet, string DeltaPath, int StartState, int[] EndStates)
        {
            Vertexes = new Vertex[Q];
            Recognizer(Q, Alphabet, DeltaPath, EndStates);

            if (Vertexes[StartState].End == true)
            {
                Console.WriteLine(true);
                return;
            }
            List<int> path = new List<int>();
            List<char> transitionCondition = new List<char>();
            path.Add(StartState);
            bool EndFind = false;
            FindInDepth(StartState, path, transitionCondition, false, new List<Vertex>(), ref EndFind);
            Console.WriteLine(Analis());
        }
        private void FindInDepth(int State, List<int> path, List<char> transitionCondition, bool back, List<Vertex> ReplyVertexes, ref bool EndFind)//State = path.Count - 1 - нынешний индех 
        {
            Vertexes[State].Marked = true;
            if (Vertexes[State].End && !back)
            {
                if (Paths.Count == 0)
                {
                    Paths.Add(new List<int>(path));
                    TransitionConditions.Add(new List<char>(transitionCondition));
                }
                else
                {
                    bool uniq = true;
                    foreach (var p in Paths)
                        if (p.SequenceEqual(path))
                        {
                            uniq = false;
                            break;
                        }
                    if (uniq)
                    {
                        Paths.Add(new List<int>(path));
                        TransitionConditions.Add(new List<char>(transitionCondition));
                    }
                }
            }
            for (int i = 0; i < Vertexes[State].MarkedTransitionCondition.Length; i++)
            {
                if (Vertexes[State].MarkedTransitionCondition[i] == false)//поиск связи
                {
                    back = false;
                    transitionCondition.Add(Vertexes[State].TransitionCondition[i]);
                    Vertexes[State].MarkedTransitionCondition[i] = true;
                    path.Add((int)Vertexes[State].IndexVertexes[i]);// добавим след
                    if (Vertexes[(int)Vertexes[State].IndexVertexes[i]].Marked)//пройдена следующая?
                    {
                        if (path.IndexOf((int)Vertexes[State].IndexVertexes[i]) != path.Count - 1)
                        {
                            (int[], char[]) data = CheckLoops(path, transitionCondition);
                            if (data != (null, null))
                                Loops.Add(new Loop(data));
                            path.RemoveAt(path.Count - 1);
                            transitionCondition.RemoveAt(transitionCondition.Count - 1);
                            back = true;
                            FindInDepth(path[path.Count - 1], path, transitionCondition, back, ReplyVertexes, ref EndFind);
                            if (EndFind)
                                return;
                        }
                    }
                    FindInDepth((int)Vertexes[State].IndexVertexes[i], path, transitionCondition, back, ReplyVertexes, ref EndFind);
                    if (EndFind)
                        return;
                }
                else if (i == Vertexes[State].MarkedTransitionCondition.Length - 1)// не удалось найти
                {
                    for (int j = 0; j < Vertexes[State].MarkedTransitionCondition.Length; j++)
                    {
                        if (Vertexes[State].IndexVertexes[j] != null)
                        {
                            Vertexes[State].MarkedTransitionCondition[j] = false;//////
                        }
                    }
                    path.RemoveAt(path.Count - 1);
                    if (path.Count != 0)
                    {
                        back = true;
                        transitionCondition.RemoveAt(transitionCondition.Count - 1);
                        FindInDepth(path[path.Count - 1], path, transitionCondition, back, ReplyVertexes, ref EndFind);
                        if (EndFind)
                            return;
                    }
                    else
                    {
                        EndFind = true;
                        return;
                    }
                }
            }
            return;
        }
        private (int[], char[]) CheckLoops(List<int> indexVertexes, List<char> transitionCondition)
        {//List<object>
            int[] IndexVertexes;
            char[] TransitionCondition;
            //(List<int>, List<char>) tempary
            for (int i = 0; true; i++)
                if (indexVertexes[i] == indexVertexes[indexVertexes.Count - 1])
                {
                    IndexVertexes = new int[indexVertexes.Count - i];
                    TransitionCondition = new char[transitionCondition.Count - i];
                    indexVertexes.CopyTo(i, IndexVertexes, 0, indexVertexes.Count - i);
                    transitionCondition.CopyTo(i, TransitionCondition, 0, transitionCondition.Count - i);
                    break;
                }
            Loop lastLoop = null;
            if (Loops.Count != 0)
                lastLoop = Loops.Last();
            foreach (Loop loop in Loops)
            {
                if (loop.IndexVertexes.Length == IndexVertexes.Length)
                {
                    for (int i = 0; i < IndexVertexes.Length - 1; i++)//последний повторяеься!
                        if (!loop.IndexVertexes.Contains(IndexVertexes[i]))
                            break;
                        else if (i == IndexVertexes.Length - 2)
                            if (loop.IndexVertexes[loop.IndexVertexes.Length - 1] == IndexVertexes[IndexVertexes.Length - 1])
                            {
                                if (loop.TransitionCondition[loop.TransitionCondition.Length - 1] == TransitionCondition[TransitionCondition.Length - 1])
                                    return (null, null);
                            }
                            else
                                return (null, null);
                }
                if (loop == lastLoop)
                    return (IndexVertexes, TransitionCondition);
            }
            if (Loops.Count != 0)
                return (null, null);
            else
                return (IndexVertexes, TransitionCondition);
        }
        private bool Analis()
        {
            for (int i = 0; i < Paths.Count; i++)
            {
                int zero = 0;
                int one = 0;
                for (int j = 0; j < TransitionConditions[i].Count; j++)
                {
                    if (TransitionConditions[i][j] == 0)
                        zero++;
                    else
                        one++;
                }
                if (zero < one)
                    return true;
                else if (Loops.Count == 0)
                    return false;
                else
                {
                    List<Loop> list = new List<Loop>();
                    foreach (Loop loop in Loops)
                        foreach (int indexVertex in loop.IndexVertexes)
                            if (Paths[i].Contains(indexVertex)) //list.Add(loop);
                                if (loop.State == LoopStatus.One)
                                    return true;
                                else
                                    break;
                }
            }
            return false;
        }
        private void Recognizer(int Q, char[] Alphabet, string DeltaPath, int[] EndStates)//сортируем связи по вершинам
        {
            var excelApp = new Application();
            Workbook delta = excelApp.Workbooks.Open(DeltaPath);
            _Worksheet workSheet = excelApp.ActiveSheet;
            char[] transitFunct = new char[Alphabet.Length];
            int?[] indexVertexes = new int?[Alphabet.Length];
            string input;
            bool end;
            for (int i = 2; i <= Q + 1; i++)
            {
                for (int j = 2; j <= Alphabet.Length + 1; j++)
                {
                    //var tim = workSheet.Cells[i, j].Value;
                    if (workSheet.Cells[i, j].Value != null)//null
                    {
                        input = workSheet.Cells[i, j].Value;
                        input = input.TrimStart('q');
                        indexVertexes[j - 2] = Int32.Parse(input);//t[1] - '0';
                        transitFunct[j - 2] = (char)workSheet.Cells[1, j].Value;//достаточно одного цикла
                    }
                    else
                        indexVertexes[j - 2] = null;
                }
                end = false;
                foreach (int numb in EndStates)
                {
                    if (i - 2 == numb)
                        end = true;
                }
                Vertexes[i - 2] = new Vertex(indexVertexes, transitFunct, end);
            }
        }
        enum LoopStatus
        {
            Zero,
            One,
            Equally
        }
    }
}
