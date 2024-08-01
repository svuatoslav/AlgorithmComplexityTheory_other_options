using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class _6
    {
        internal char[,] Field = new char[6, 6]
        {
            { '#','#','#','#','#' ,'#'},
            { '1','#','#','#',' ','#'},
            { '2','#',' ','X',' ','#'},
            { '3','#',' ',' ',' ','#'},
            { '4',' ',' ',' ',' ','#'},
            { ' ','A','B','C','D','#'},
        };
        string Comp = "H0W0";

        public _6()
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
            char[] chars= playerMove.ToCharArray();
            int i = chars[1] - '0';
            int j = 0;
            if (playerMove[0] == 'A')
                j = 1;
            else if (playerMove[0] == 'B')
                j = 2;
            else if (playerMove[0] == 'C')
                j = 3;
            else if (playerMove[0] == 'D')
            {
                j = 4;
            }
            Field[i, j] = 'O';
            if (Comp == "H0W0")
            {
                if (playerMove == "A4")
                {
                    Comp = "H1W2";
                    Field[3, 2] = 'X';
                }
                else if (playerMove == "B3" || playerMove == "D1")
                {
                    Comp = "H1W1";
                    Field[3, 3] = 'X';
                }
                else
                {
                    Comp = "H1W0";
                    Field[3, 2] = 'X';
                }
            }
            else if (Comp == "H1W0")
            {
                if (playerMove == "A4")
                {
                    Comp = "H2W0";
                    Field[1, 4] = 'X';
                }
                else
                {
                    Comp = "H2W1";
                    Field[4, 1] = 'X';
                }
                return false;
            }
            else if (Comp == "H1W1")
            {
                if (playerMove == "C4")
                {
                    Comp = "H2W2";
                    Field[2, 4] = 'X';
                }
                else
                {
                    Comp = "H2W3";
                    Field[4, 3] = 'X';
                    return false;
                }
            }
            else if (Comp == "H2W2")
            {
                if (playerMove == "B2")
                {
                    Comp = "H3W0";
                    Field[4, 2] = 'X';
                }
                else
                {
                    Comp = "H3W1";
                    Field[2, 2] = 'X';
                }
                return false;
            }
            else if (Comp == "H1W2")
            {
                if (playerMove == "D1")
                {
                    Comp = "H2W5";
                    Field[3, 3] = 'X';
                }
                else
                {
                    Comp = "H2W4";
                    Field[1, 4] = 'X';
                    return false;
                }
            }
            else if (Comp == "H2W5")
            {
                if (playerMove == "C4")
                {
                    Comp = "H3W2";
                    Field[3, 4] = 'X';
                }
                else
                {
                    Comp = "H3W3";
                    Field[4, 3] = 'X';
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
