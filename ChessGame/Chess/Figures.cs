using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures {
    public abstract class Figure
    {
        public Image img;
        public int side;
        int steps = 0;
        public abstract int[,] Move(int i, int j);
        public  Image Image()
        {
            return img;
        }
        public int Side()
        {
            return side;
        }
        public abstract string Name();
        public int Steps()
        {
            return steps;
        }
        public void PlusStep()
        {
            steps++;
        }
    }

    public class Pawn :Figure //пешка
    {
        public Pawn(int _side) { side = _side; }
        public Pawn(int _side,Image _img) { side = _side; img = _img; }
        public Pawn() {}
        public override int[,] Move(int row, int col)
        {
            int[,] arr = new int[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[row, col] = 0;
                }
            }
            
            if(side == 1)
            {
                if (row + 1 < 8)
                {
                    arr[row + 1, col] = 1;
                }
            }
            else
            {
                if (row - 1 >= 0)
                {
                    arr[row - 1, col] = 1;
                }
            }
            arr[row, col] = 0;
            return arr;
        }
        public override string Name()
        {
            return "Pawn";
        }
    }

    class King:Figure//король
    {
        public King() {}
        public King(int _side) { side = _side; }
        public King(int _side, Image _img) { side = _side; img = _img; }
        public override int[,] Move(int row, int col)
        {
            int[,] arr = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[row, col] = 0;
                }
            }
            Console.WriteLine("solger");
            
            if (row + 1 < 8)
            {
                arr[row + 1, col] = 1;
            }
            if (row - 1 >= 0)
            {
                arr[row - 1, col] = 1;
            }
            if (row + 1 < 8 && col - 1 >= 0)
            {
                arr[row + 1, col -1 ] = 1;
            }
            if (row - 1 >= 0 && col + 1 < 8)
            {
                arr[row - 1, col + 1] = 1;
            }
            if (row + 1 < 8 && col + 1 < 8)
            {
                arr[row + 1, col + 1] = 1;
            }
            if (row - 1 >= 0 && col - 1 >= 0)
            {
                arr[row - 1, col - 1] = 1;
            }
            if (col + 1 < 8)
            {
                arr[row, col + 1] = 1;
            }
            if (col - 1 >= 0)
            {
                arr[row, col - 1] = 1;
            }
            arr[row, col] = 0;
            return arr;
        }
        public override string Name()
        {
            return "King";
        }
    }

    class Queen:Figure//дама
    {
        public Queen() { }
        public Queen(int _side) { side = _side;}
        public Queen(int _side, Image _img) { side = _side; img = _img; }

        public override int[,] Move(int row, int col)
        {
            int[,] arr = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[row, col] = 0;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                arr[i, col] = 1;
            }
            for (int i = 0; i < 8; i++)
            {
                arr[row, i] = 1;
            }

            int r = row;
            int c = col;
            while(r < 8 && c < 8 && r >= 0 && c >= 0)
            {
                arr[r, c] = 1;
                r++;
                c++;
            }

            r = row;
            c = col;
            while (r >= 0 && c >= 0 && r < 8 && c < 8)
            {
                arr[r, c] = 1;
                r--;
                c--;
                
            }

            r = row;
            c = col;
            while (r >= 0 && c >= 0 && r < 8 && c < 8)
            {
                arr[r, c] = 1;
                r++;
                c--;

            }

            r = row;
            c = col;
            while (r >= 0 && c >= 0 && r < 8 && c < 8)
            {
                arr[r, c] = 1;
                r--;
                c++;

            }
            arr[row, col] = 0;
            return arr;
        }
        public override string Name()
        {
            return "Queen";
        }
    }

    class Rook : Figure//тура
    {
        public Rook() { }
        public Rook(int _side) { side = _side; }
        public Rook(int _side, Image _img) { side = _side; img = _img; }
        public override int[,] Move(int row, int col)
        {
            int[,] arr = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[row, col] = 0;
                }
            }
            int r = row;
            int c = col;
            while(r<8)
            {
                arr[r, c] = 1;
                r++;
            }

            r = row;
            c = col;
            while (c < 8)
            {
                arr[r, c] = 1;
                c++;
            }

            r = row;
            c = col;
            while (c >= 0)
            {
                arr[r, c] = 1;
                c--;
            }

            r = row;
            c = col;
            while (r >= 0)
            {
                arr[r, c] = 1;
                r--;
            }


            arr[row, col] = 0;
            return arr;
        }
        public override string Name()
        {
            return "Rook";
        }
    }

    class Bishop : Figure//слон
    {
        public Bishop() { }
        public Bishop(int _side) { side = _side; }
        public Bishop(int _side, Image _img) { side = _side; img = _img; }
        public override int[,] Move(int row, int col)
        {
            int[,] arr = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[row, col] = 0;
                }
            }
            int r = row;
            int c = col;
            r = row;
            c = col;
            while (r >= 0 && c >= 0 && r < 8 && c < 8)
            {
                arr[r, c] = 1;
                r++;
                c--;

            }

            r = row;
            c = col;
            while (r >= 0 && c >= 0 && r < 8 && c < 8)
            {
                arr[r, c] = 1;
                r--;
                c++;

            }
            r = row;
            c = col;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {
                arr[r, c] = 1;
                r++;
                c++;
            }

            r = row;
            c = col;
            while (r >= 0 && c >= 0 && r < 8 && c < 8)
            {
                arr[r, c] = 1;
                r--;
                c--;

            }

            arr[row, col] = 0;
            return arr;
        }
        public override string Name()
        {
            return "Bishop";
        }
    }

    class Knight : Figure//конь
    {
        public Knight() { }
        public Knight(int _side) { side = _side; }
        public Knight(int _side, Image _img) { side = _side; img = _img; }
        public override int[,] Move(int row, int col)
        {
            int[,] arr = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[row, col] = 0;
                }
            }

            if(row + 2 < 8 && col + 1 < 8)
            {
                arr[row+2, col+1] = 1;
            }
            if (row + 2 < 8 && col - 1 >= 0)
            {
                arr[row + 2, col - 1] = 1;
            }
            if (row + 1 < 8 && col - 2 >= 0)
            {
                arr[row + 1, col - 2] = 1;
            }
            if (row + 1 < 8 && col + 2 < 8)
            {
                arr[row +1, col + 2] = 1;
            }

            if (row - 1 >= 0 && col + 2 < 8)
            {
                arr[row - 1, col + 2] = 1;
            }
            if (row - 1 >= 0 && col - 2 >= 0)
            {
                arr[row - 1, col - 2] = 1;
            }

            if (row - 2 >= 0 && col + 1 < 8)
            {
                arr[row - 2, col + 1] = 1;
            }
            if (row - 2 >= 0 && col - 1 >= 0)
            {
                arr[row - 2, col - 1] = 1;
            }
            arr[row, col] = 0;
            return arr;
        }
        public override string Name()
        {
            return "Knight";
        }
    }
}
