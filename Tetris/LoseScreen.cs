using System.Reflection;
using System.Runtime.CompilerServices;
using Spectre.Console;

namespace Tetris
{
    class LoseScreen : MenuManager
    {
        const string text = "YOU LOST";

        static string[] options = [
            "PLAY AGAIN",
            "MAIN MENU"
        ];


        static Action[] actions = [
            PlayAgain,
            MainMenu
        ];

        public new static void StartupMenu()
        {
            StartMenuFuncionality(options, actions, Constants.ASCII_LOSE);
        }

        static void PlayAgain()
        {
            Game.StartGame();
        }

        static void MainMenu()
        {
            MenuManager.StartupMenu();
        }

    }
}