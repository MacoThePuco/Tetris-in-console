using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net.Http.Headers;


namespace Tetris
{
    public static class Utils
    {
        public static Random rnd = new Random();
        public static Stopwatch stepStopwatch = new Stopwatch();

        public static void ResetStopwatch()
        {
            stepStopwatch.Reset();
            stepStopwatch.Start();
        }

    }

    class Program
    {
        static void Main()
        {
            Tiskarna.Setup();

            //This code captures ctrl-c signal
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                Exit();
            };

            MenuManager.StartupMenu();
        }

        public static void Exit()
        {
            Console.CursorVisible = true;
            Tiskarna.Clear();
            Environment.Exit(0);
        }
    }

    class Position
    {
        public int x; public int y;
        public Position(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public Position(Position oldPosition)
        {
            x = oldPosition.x;
            y = oldPosition.y;
        }

        public bool IsOutOfBounds()
        {
            if(x < 0 || x >= Constants.BOARD_WIDTH || y >= Constants.BOARD_HEIGHT)
            {
                return true;
            }
            return false;
        }

        public Position AddPosition(Position pos)
        {
            return new Position(y + pos.y, x + pos.x);
        }

        public Position AddPosition(int y, int x)
        {
            return new Position(this.y + y, this.x + x);
        }
    }
}