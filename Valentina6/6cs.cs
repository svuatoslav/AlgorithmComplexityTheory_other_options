using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valentina6
{
    internal class _6cs
    {
        private Status _status = Status.H0W0;
        internal char[,] Field = new char[6, 6]
        {
            { ' ','A','B','C','D','#'},
            { '1',' ','#','#','#','#'},
            { '2',' ','X',' ',' ','#'},
            { '3','#',' ',' ',' ','#'},
            { '4','#',' ',' ',' ','#'},
            { '#','#','#','#','#','#'}
        };
        private enum Status
        {
            H0W0,
            H1W0,
            H1W1,
            H2W0,
            H2W1,
            H2W2,
            H2W3
        }
        public _6cs()
        {
            bool play = true;
            Print();
            do
            {
                Console.WriteLine("Ваш ход");
                play = Compute(Console.ReadLine());
                Print();
            }
            while (play);
            Console.WriteLine("Вы проиграли!");
            Console.ReadKey();
        }
        internal bool Compute(string playerMove)
        {
            char[] chars = playerMove.ToCharArray();
            int i = chars[1] - '0';
            int j = 0;
            if (playerMove[0] == 'A')
                j = 1;
            else if (playerMove[0] == 'B')
                j = 2;
            else if (playerMove[0] == 'C')
                j = 3;
            else if (playerMove[0] == 'D')
                j = 4;
            else
                Console.WriteLine("Ошибка!");
            Field[i, j] = 'O';
            if (_status == Status.H0W0)
            {
                if (playerMove.Equals("A2") || playerMove.Equals("C2") || playerMove.Equals("D2"))
                {
                    _status = Status.H1W0;
                    Field[3, 3] = 'X';
                }
                else
                {
                    _status = Status.H1W1;
                    Field[2, 3] = 'X';
                }
            }
            else if (_status == Status.H1W0)
            {
                if (playerMove.Equals("A1"))
                {
                    _status = Status.H2W1;
                    Field[4, 4] = 'X';
                }
                else
                {
                    _status = Status.H2W0;
                    Field[1, 1] = 'X';
                }
                return false;
            }
            else if (_status == Status.H1W1)
            {
                if (playerMove.Equals("A2"))
                {
                    _status = Status.H2W2;
                    Field[2, 4] = 'X';
                }
                else
                {
                    _status = Status.H2W3;
                    Field[2, 1] = 'X';
                }
                return false;
            }
            return true;
        }
        void Print()
        {
            int rows = Field.GetUpperBound(0) + 1;
            int columns = Field.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{Field[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
