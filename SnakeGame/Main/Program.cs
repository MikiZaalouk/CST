using System;
using System.Text;
using System.Threading;


namespace Main
{
    class Program
    {
        static void Main()
        {
            Board board = new Board(90, 25); // testkode kå gerne slettes..... lav bræt
            board.AddFood(); // testkode kå gerne slettes..... tilføj mad ti bræt
            board.Draw(); // testkode kå gerne slettes..... tegn bræt
        }
    }   
    
    // Seleve "spillebrættet". Iden med koden er at definerer et "grid" (x/y koordinatsystem), der efterfølgende bliver
    // fyldt ud af "felter" (blokke), som er defineret i koden class block. 
    public class Board                   
    {
        public int xBoard; // x koordinaten for brættet.
        public int yBoard; // y Koordinaten for brættet.

        private int leftEdgeX => 0; // 0 koordinaten i X-aksen venstre hjørne.
        private int rightEdgeX => xBoard - 1; // X koordinaten til kanten.
        private int topEdgeY => 0; // O koordinaten til Y-akssen øverste hjørne.
        private int bottomEdgeY => yBoard - 1; // Y koordinaten til kanten.

        private Block[,] gridBlock; // Her defineres et tomt grid uden spcefik størrelse.. altså selve "rammen" for spillebrættet.

        private Random random = new Random(); //variabel til at generer random værdier... bruges når der skal spawnes mad.

        // constructer til brættet, der skal kaldes af main programmet for at lave brættet.
        // Ved hjælp af input arumenterne er det muligt at styre størrelsen på brættet.
        public Board(int x, int y)
        {
            xBoard = x; //overfører vi brugers ønske for størrelse af bræt, til bræt variablerne.
            yBoard = y; //overfører vi brugers ønske for størrelse af bræt, til bræt variablerne. 
            gridBlock = new Block[xBoard, yBoard]; // Her laves selve brætrammen i størrelsen ønsket fra brugeren.
            InitGrid(); // Her kalder vi funtionen der initialisere brættet... altså fylder brættet ud med felter.
        }

        // Funktionen der fylder brættet ud med felter.
        private void InitGrid() 
        {
            for (int y = 0; y < yBoard; y++)
            {
                for (int x = 0; x < xBoard; x++)
                {
                    Block block = new Block(x, y); // Her laves ny block for hver X/Y koordinat i brættet.

                    AddBlock(block); // Blokken eller "feltet" tilføjes til brættet.

                    if (IsBorder(block)) // Her undersøges om feltet ligger i kanten af spillepladen, eller inde på pladen.
                    {
                        block.SetBorder(); // Hvis det er et felt i kanten, kald funktionen der udfyler kanter.
                    }
                    else
                    {
                        block.SetEmpty(); // Hvis det er et "arbitrært felt", kald da funktionen der sætter feltet til tomt.
                    }
                }
            }
        }

        //funktion til at undersøge om feltet ligger i kanten af spillepladen elelr ej.
        private bool IsBorder(Block block) 
        {
            if(block.X == leftEdgeX || block.X >= rightEdgeX || block.Y == topEdgeY || block.Y >= bottomEdgeY) // trigger kriterie.. Et stort OR statement.
            {
                return true; // Hvis ja, returnere sandt.
            }
            else
            {
                return false; // Hvis nej, returnere sandt.
            }
        }

        // Funktionen til at tilføje et færdigt felt til brættet.
        private void AddBlock(Block block) 
        {
            gridBlock[block.X, block.Y] = block; //Den block funktionen kaldes med, overføres som felt på brættet i den givede X/Y position.
        }
        // Funktionen til at tilføje mad til slangen på brættet.
        public void AddFood()
        {
            RandomBlock().SetFood(); // her kalder viførst en funktion, der vælger et tilfældigt tomt felt. Derefter placere vi fad i feltet
        }

        // Funktion til at finde et tilfældigt tomt felt.
        private Block RandomBlock()
        {
            bool isBlockEmpty; // Her definere vi en sandt/flask variabel, til at undersøge om et felt er tomt eller ej.

            Block block = new Block(-1, -1); // Her laver vi en test block, som vi kan undersøge vores felter på brættet med.

            do
            {
                block = gridBlock[random.Next(xBoard), random.Next(yBoard)]; // Her "mappes"
                isBlockEmpty = block.IsEmpty();

            } while (!isBlockEmpty);  // Så længe 

            return block;
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
        private char foodToken = '¤';
        private char headToken = 'o';
        private char bodyToken = '+';

        public int X;
        public int Y;
        public char Value;

        public bool IsEmpty()
        {
            
            return true;
        }

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

        public void SetFood()
        {
            Update(foodToken);
        }

        public void Update(char newVal)
        {
            Value = newVal;
        }
    }
    
}
