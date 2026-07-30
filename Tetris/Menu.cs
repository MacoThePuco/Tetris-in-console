using System.Formats.Tar;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Tetris
{
    class MenuManager()
    {
        static string[] options = [
            "START GAME",
            "CREDITS",
            "EXIT"
        ];

        static Action[] actions = [
            StartGame,
            Credits,
            Program.Exit
        ];

        static int numOfOptions;

        static int pointer = 0;

        public static void StartupMenu()
        {

            Tiskarna.Clear();
            StartMenuFuncionality(options, actions, Constants.ASCII_TETRIS);

        }

        protected static void StartMenuFuncionality(string[] options, Action[] actions, string[] asciiText)
        {
            numOfOptions = options.Length;

            int ticks = 0;
            bool farba = true;
            while (true)
            {


                ConsoleKey? key = null;
                if(Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }

                if(key is not null)
                {
                    bool moved = MovePointer(key);
                    if (moved)
                    {
                        ticks = 0;
                        farba = true;
                    }
                }


                if(ticks >= Constants.TICKS_PER_ANIMATION)
                {
                    farba = !farba;
                    ticks = 0;
                }
                Tiskarna.DrawMenu(options, pointer, farba, asciiText);
                Thread.Sleep(Constants.TICK_SPEED);
                ticks += 1;

                if(key is not null && (key == ConsoleKey.Spacebar || key == ConsoleKey.Enter))
                {
                    break;
                }
            }

            //we only get to this part, if while loop was broken -- if spacebar was pressed
            DoPointerAction(actions);
        }

        static bool MovePointer(ConsoleKey? key)
        {
            bool moved = false;
            if(key == ConsoleKey.UpArrow)
            {
                pointer += numOfOptions - 1; //in modulo, this is -1;
                moved = true;
            }
            else if(key == ConsoleKey.DownArrow)
            {
                moved = true;
                pointer++;
            }
            pointer %= numOfOptions;

            return moved;
        }

        static void DoPointerAction(Action[] actions)
        {
            actions[pointer]();
        }

        static void StartGame()
        {
            Game.StartGame();
        }

        static void Credits()
        {
            int y = Console.WindowHeight;
            while (true)
            {
                y --;
                Tiskarna.DrawCredits(y);

                if(y <= -Constants.CREDITS.Length)
                {
                    break;
                }

                if(Console.KeyAvailable)
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.Spacebar || Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }

                Thread.Sleep(Constants.TICKS_PER_LINE * Constants.TICK_SPEED);
            }

            StartupMenu();
        }

    }
}