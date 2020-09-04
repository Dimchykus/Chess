using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Figures;

namespace Chess
{
    public partial class Form1 : Form
    {
        const int count = 8;
        const int cell = 50;
        int r_select;
        int c_select;
        bool Checked = false;
        int side = 0;
        int count_steps_0 = 0;
        int count_steps_1 = 0;
        int[,] arr_move = new int[8, 8];
        Button[,] buttons = new Button[count, count];
        Figure[,] figures = new Figure[8, 8];
        Button pressedButton;
        
        public Form1()
        {
            InitializeComponent();
            CreateDesk();
        }

        public void CreateDesk()
        {
            int w = this.tableLayoutPanel1.Width/8;
            int h = this.tableLayoutPanel1.Height/8;
            Console.WriteLine(w + " " + h);
            int width = count * w ;
            int height = count * h;

            for (int i = 0; i< count;i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Button button = new Button();
                    if((i+j) % 2 == 0)
                    {
                        button.BackColor = Color.Gray;
                    }else 
                    {
                        button.BackColor = Color.White;
                    }
                    button.Click += new EventHandler(OnFigurePress);
                    button.Location = new Point(j * w, i * h);
                    button.Size = new Size(w, h);
                    button.Name = i.ToString() + " " + j.ToString();
                    button.Padding = new Padding(0, 0, 0, 0);
                    button.Margin = new Padding(0, 0, 0, 0);
                    button.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right );
                    buttons[i, j] = button;
                    //this.Controls.Add(button);
                    this.tableLayoutPanel1.Controls.Add(button);
                }
            }
            
            SetFigures();
            SetImages();

        }

        public void OnFigurePress(object sender, EventArgs e)
        {
            pressedButton = sender as Button;
            int col = 0, row = 0;
            Int32.TryParse(Char.ToString(pressedButton.Name[0]), out row);
            Int32.TryParse(Char.ToString(pressedButton.Name[2]), out col);
            Console.WriteLine(row+ " " + col);

            ClearDesk();
            SetImages();
            if(Checked == true && arr_move[row,col] == 1)
            {
                if (figures[row, col] == null)
                {
                    MakeMove(row, col, r_select, c_select);
                    this.label1.Text += "Player " + side + ": " + figures[row, col].Name() + " to " + row + " " + col + "\n";
                }
                else if (figures[row, col] != null)
                {
                    this.label1.Text += "Player " + side + ": hit "+ figures[r_select, c_select].Name()+" with " + figures[row, col].Name() + " to " + row + " " + col + "\n";
                    figures[row, col] = null;
                    MakeMove(row, col, r_select, c_select);

                }
                if (figures[row, col].Name() == "Pawn") {
                    if (figures[row, col].Side() == 0)
                    {
                        if(row == 0)
                        {
                            Image img;
                            img = Image.FromFile(".//Photos//Queen_0.gif");
                            figures[row, col] = new Queen(0, img);
                        }
                    }
                    else
                    {
                        if (row == 7)
                        {
                            Image img;
                            img = Image.FromFile(".//Photos//Queen_1.gif");
                            figures[row, col] = new Queen(1, img);
                        }
                    }
                }
                figures[row, col].PlusStep();
                if (side == 1)
                {
                    side = 0;
                }
                else
                {
                    side = 1;
                }
                Checked = false;
                InfoTextUpdate();
                ClearDesk();
                SetImages();
                if(WinCheck() == true)
                {
                    label1.Text = "Player " + figures[row, col].Side() + " win";
                    label2.Text = "Player " + figures[row, col].Side() + " win";
                }

            }
            else if (figures[row, col] != null && side == figures[row, col].Side())
            {
                Console.WriteLine(figures[row, col].Steps());
                int[,] arr = new int[8, 8];
                arr = figures[row, col].Move(row, col);
                arr = Correction(arr, row, col);
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if (arr[i, j] == 1)
                        {
                           buttons[i, j].BackColor = Color.Red;
                        }
                    }
                }
                Checked = true;
                arr_move = arr;
            }
            else
            {
                Checked = false;
            }
            r_select = row;
            c_select = col;
        }

        void SetFigures()
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    figures[i, j] = null;
                }
            }
            Image img;
            img = Image.FromFile(".//Photos//Pawn_0.png");


            for (int i = 0; i < count; i++)
            {
                figures[6, i] = new Pawn(0,img);
            }
            img = Image.FromFile(".//Photos//Pawn_1.gif");

            for (int i = 0; i < count; i++)
            {
                figures[1, i] = new Pawn(1,img);
            }

            img = Image.FromFile(".//Photos//Queen_0.gif");
            figures[7, 3] = new Queen(0,img);
            img = Image.FromFile(".//Photos//Queen_1.gif");
            figures[0, 4] = new Queen(1,img);

            img = Image.FromFile(".//Photos//Rook_1.gif");
            figures[0, 0] = new Rook(1, img);
            img = Image.FromFile(".//Photos//Rook_1.gif");
            figures[0, 7] = new Rook(1, img);

            img = Image.FromFile(".//Photos//Rook_0.gif");
            figures[7, 7] = new Rook(0, img);
            img = Image.FromFile(".//Photos//Rook_0.gif");
            figures[7, 0] = new Rook(0, img);

            img = Image.FromFile(".//Photos//Bishop_1.gif");
            figures[0, 2] = new Bishop(1, img);
            img = Image.FromFile(".//Photos//Bishop_1.gif");
            figures[0, 5] = new Bishop(1, img);

            img = Image.FromFile(".//Photos//Bishop_0.gif");
            figures[7, 5] = new Bishop(0, img);
            img = Image.FromFile(".//Photos//Bishop_0.gif");
            figures[7, 2] = new Bishop(0, img);

            img = Image.FromFile(".//Photos//Knight_1.gif");
            figures[0, 1] = new Knight(1, img);
            img = Image.FromFile(".//Photos//Knight_1.gif");
            figures[0, 6] = new Knight(1, img);

            img = Image.FromFile(".//Photos//Knight_0.gif");
            figures[7, 6] = new Knight(0, img);
            img = Image.FromFile(".//Photos//Knight_0.gif");
            figures[7, 1] = new Knight(0, img);

            img = Image.FromFile(".//Photos//King_0.gif");
            figures[7, 4] = new King(0, img);
            img = Image.FromFile(".//Photos//King_1.gif");
            figures[0, 3] = new King(1, img);

        }

        void ClearDesk()
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        buttons[i, j].BackColor = Color.Gray;
                    }
                    else
                    {
                        buttons[i, j].BackColor = Color.White;
                    }
                }
            }
        }

        void SetImages()
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (figures[i, j] != null)
                    {
                        buttons[i, j].BackgroundImage = figures[i, j].Image();
                        buttons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        buttons[i, j].BackgroundImage = null;
                    }
                }
            }
        }

        int[,] Correction(int[,] arr,int row, int col)
        {
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (arr[i,j] == 1 && figures[i, j] != null && figures[i,j].Side() == figures[row, col].Side())
                    {
                        arr[i,j] = 0;
                    }
                }
            }
            int r = row+1;
            int c = col+1;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {
               
                if (figures[r,c] != null)
                {
                    r++;
                    c++;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        r++;
                        c++;
                    }
                }
                r++;
                c++;
            }

            r = row-1;
            c = col-1;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {

                if (figures[r, c] != null)
                {
                    r--;
                    c--;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        r--;
                        c--;
                    }
                }
                r--;
                c--;
            }

            r = row + 1;
            c = col - 1;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {

                if (figures[r, c] != null)
                {
                    r++;
                    c--;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        r++;
                        c--;
                    }
                }
                r++;
                c--;
            }

            r = row - 1;
            c = col + 1;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {

                if (figures[r, c] != null)
                {
                    r--;
                    c++;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        r--;
                        c++;
                    }
                }
                r--;
                c++;
            }

            r = row + 1;
            c = col;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {
                if (figures[r, c] != null)
                {
                    r++;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        r++; 
                    }
                }
                r++;
            }

            r = row - 1;
            c = col;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {
                if (figures[r, c] != null)
                {
                    r--;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        r--;
                    }
                }
                r--;
            }

            r = row;
            c = col-1;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {
                if (figures[r, c] != null)
                {
                    c--;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        c--;
                    }
                }
                c--;
            }

            r = row;
            c = col + 1;
            while (r < 8 && c < 8 && r >= 0 && c >= 0)
            {
                if (figures[r, c] != null)
                {
                    c++;
                    while (r < 8 && c < 8 && r >= 0 && c >= 0)
                    {
                        arr[r, c] = 0;
                        c++;
                    }
                }
                c++;
            }

            if(figures[row,col].Name() == "Pawn")
            {
                if(figures[row, col].Side() == 0)
                {
                    if (row - 1 >= 0) {
                        if (figures[row - 1, col] != null)
                        {
                            if (figures[row - 1, col].Side() == 1)
                            {
                                arr[row - 1, col] = 0;
                            }
                        }
                        if(col + 1 < 8)
                        {
                            if (figures[row - 1, col+1] != null)
                            {
                                if (figures[row - 1, col+1].Side() == 1)
                                {
                                    arr[row - 1, col+1] = 1;
                                }
                            }
                        }
                        if (col - 1 >= 0)
                        {
                            if (figures[row - 1, col - 1] != null)
                            {
                                if (figures[row - 1, col - 1].Side() == 1)
                                {
                                    arr[row - 1, col-1] = 1;
                                }
                            }
                        }
                    }
                    if (figures[row, col].Steps() == 0)
                    {
                        if(figures[row-2,col] == null)
                        {
                            arr[row - 2, col] = 1;
                        }
                       
                    }
                }
                else
                {
                    if (row + 1 < 8)
                    {
                        if (figures[row + 1, col] != null)
                        {
                            if (figures[row + 1, col].Side() == 0)
                            {
                                arr[row + 1, col] = 0;
                            }
                        }
                        if (col + 1 < 8)
                        {
                            if (figures[row + 1, col + 1] != null)
                            {
                                if (figures[row + 1, col + 1].Side() == 0)
                                {
                                    arr[row + 1, col + 1] = 1;
                                }
                            }
                        }
                        if (col - 1 >= 0)
                        {
                            if (figures[row + 1, col - 1] != null)
                            {
                                if (figures[row + 1, col - 1].Side() == 0)
                                {
                                    arr[row + 1, col - 1] = 1;
                                }
                            }
                        }
                    }
                    if (figures[row, col].Steps() == 0)
                    {
                        if (figures[row + 2, col] == null)
                        {
                            arr[row + 2, col] = 1;
                        }
                    }
                }
            }
            return arr;
        }

        void MakeMove(int row, int col, int row_new, int col_new)
        {
            Figure temp = figures[row, col];
            figures[row, col] = figures[row_new, col_new];
            figures[row_new, col_new] = temp;
        }

        void InfoTextUpdate()
        {
            label2.Text = "Side 0:\n";
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (figures[i, j] != null)
                    {
                        if (figures[i, j].Side() == 0)
                        {
                            label2.Text += figures[i, j].Name() + ": " + figures[i, j].Steps() + "\n";
                        }
                    }
                }
            }

            label2.Text += "\nSide 1:\n";
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (figures[i, j] != null)
                    {
                        if (figures[i, j].Side() == 1)
                        {
                            label2.Text += figures[i, j].Name() + ": " + figures[i, j].Steps() + "\n";
                        }
                    }
                }
            }
        }

        bool WinCheck()
        {
            int king = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (figures[i, j] != null)
                    {
                        if (figures[i, j].Name() == "King")
                        {
                            king++;
                        }
                    }
                }
            }
            Console.WriteLine("king: "+king);
            if(king == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}
