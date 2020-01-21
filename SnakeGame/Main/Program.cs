using System;
using System.Text;


namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(90, 25);
            board.Draw();
        }

    }   
    
    // Seleve "spillebrættet". Iden med koden er at definerer et "grid" (x/y koordinatsystem), der efterfølgende bliver
    // fyldt ud af "felter" (blokke), som er defineret i koden class block. 
    public class Board                   
    {
        public int xBoard; // x koordinaten for brættet.
        public int yBoard; // y Koordinaten for brættet.

        private int leftEdgeX => 0;
        private int rightEdgeX => xBoard - 1;
        private int topEdgeY => 0;
        private int bottomEdgeY => yBoard - 1;

        private Block[,] gridBlock;

        // constructer til brættet, der skal kaldes af main programmet for at lave brættet.
        // Ved hjælp af input arumenterne er det muligt at styre størrelsen på brættet.
        public Board(int x, int y)
        {
            xBoard = x;
            yBoard= y;
            gridBlock = new Block[xBoard, yBoard]; // Definere vi en vari
            InitGrid();
        }

        private void InitGrid()
        {
            for (int y = 0; y < yBoard; y++)
            {
                for (int x = 0; x < xBoard; x++)
                {
                    Block block = new Block(x, y);

                    AddBlock(block);

                    if (IsBorder(block))
                    {
                        block.SetBorder();
                    }
                    else
                    {
                        block.SetEmpty();
                    }
                }
            }
        }

        private bool IsBorder(Block block)
        {
            if(block.X == leftEdgeX || block.X >= rightEdgeX || block.Y == topEdgeY || block.Y >= bottomEdgeY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddBlock(Block block) 
        {
            gridBlock[block.X, block.Y] = block;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            // skriver rækkevis
            for (int y = 0; y < yBoard; y++)
            {
                for (int x = 0; x < xBoard; x++)
                {
                    sb.Append(gridBlock[x, y].Value);
                }
                sb.Append("\n"); //terminate row
            }
            return sb.ToString();
        }
    }

    public class Block
    {
        private char emptyToken = ' ';
        private char borderToken = '#';
        private char foodToken = '*';
        private char headToken = 'o';
        private char bodyToken = '+';

        public int X;
        public int Y;
        public char Value;

        public Block(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetBorder()
        {
            Update(borderToken);
        }

        public void SetEmpty()
        {
            Update(emptyToken);
        }

        public void Update(char newVal)
        {
            Value = newVal;
        }
    }
    
}
