using System;
using System.Threading;

namespace snakesharp
{
    class Program
    {
        public static char movement = 'r';
        public static int x = 5;
        public static int y = 5;
        public static int length = 5;
        public static int[,] a = new int[256, 2];
        public static int bx;
        public static int by;
        public static bool bonus = true;
        public static Random rand = new Random();
        public static Thread t = new Thread(new ThreadStart(SnakeMovement));
        static void ScorePlus()
        {
            length++;
            bx = rand.Next(1, 15);
            by = rand.Next(1, 15);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(bx * 2, by);
            Console.Write("  ");
            bonus = false;
        }

        static void Death()
        {
            Thread.Sleep(2000);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Game over\n" +
                $"Your score is {length - 6}");

        }
        static void SnakeMovement()
        {
            while (true)
            {
                if (bonus)
                    ScorePlus();

                for (int i = 1; i < length; i++)
                {
                    if (bx == a[i, 0] && by == a[i, 1])
                    {
                        ScorePlus();
                        length--;
                    }
                }
                switch (movement)
                {
                    case 'u':
                        y--;
                        break;

                    case 'd':
                        y++;
                        break;

                    case 'l':
                        x--;
                        break;

                    case 'r':
                        x++;
                        break;
                }
                Console.SetCursorPosition(x * 2, y);
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  ");
                for (int i = length - 1; i > 0; i--)
                {
                    a[i, 0] = a[i - 1, 0];
                    a[i, 1] = a[i - 1, 1];
                }
                a[0, 0] = x;
                a[0, 1] = y;

                if (x == bx && y == by)
                {
                    bonus = true;
                }

                for (int i = 1; i < length; i++)
                {
                    if (a[0, 0] == a[i, 0] && a[0, 1] == a[i, 1])
                    {
                        Death();
                        Thread.Sleep(int.MaxValue);
                    }
                }

                if (a[0, 0] < 1 ||
                    a[0, 1] < 1 ||
                    a[0, 0] > 15 ||
                    a[0, 1] > 14)
                {
                    Death();
                    Thread.Sleep(int.MaxValue);
                }
                Console.SetCursorPosition(a[length - 2, 0] * 2, a[length - 2, 1]);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  ");
                Console.SetCursorPosition(0, 0);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("  ");
                Console.CursorLeft--;
                Thread.Sleep(200);
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(34, 16);
            //top line
            Console.BackgroundColor = ConsoleColor.Red;
            for (int i = 0; i < 17; i++)
            {
                Console.Write("  ");
            }

            Console.WriteLine();

            //left line
            for (int i = 0; i < 14; i++)
            {
                Console.WriteLine("  ");
            }

            //bottom line
            for (int i = 0; i < 17; i++)
            {
                Console.Write("  ");
            }

            //right line
            Console.SetCursorPosition(32, 1);
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine("  ");
                Console.SetCursorPosition(32, i + 1);
            }

            //field
            Console.SetCursorPosition(2, 1);
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.Write("  ");
                }
                Console.SetCursorPosition(2, i + 1);
            }

            Console.SetCursorPosition(10, 5);

            t.Start();

            while (true)
            {
                ConsoleKey c = Console.ReadKey(true).Key;

                switch (c)
                {
                    case ConsoleKey.UpArrow:
                        movement = 'u';
                        break;

                    case ConsoleKey.DownArrow:
                        movement = 'd';
                        break;

                    case ConsoleKey.LeftArrow:
                        movement = 'l';
                        break;

                    case ConsoleKey.RightArrow:
                        movement = 'r';
                        break;
                }
            }
        }

    }
}