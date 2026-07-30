using Spectre.Console;

namespace Tetris
{
    public static class Constants
    {

        public const ConsoleColor BASE_COLOR = ConsoleColor.White;


        public const char BORDER_CORNER_BOTTOM_LEFT = '┗';
        public const char BORDER_CORNER_BOTTOM_RIGHT = '┛';
        public const char BORDER_CORNER_TOP_LEFT = '┏';
        public const char BORDER_CORNER_TOP_RIGHT = '┓';
        public const char BORDER_VERTICAL = '┃';

        public const char BORDER_HORIZONTAL = '━';




        //Board
        public const int BOARD_HEIGHT = 20;
        public const int BOARD_WIDTH = 10;
        public const char BOARD_BOTTOM = '▀';

        public const string BLOCK = "██";
        public const string GHOST_BLOCK = "░░";
        public const string EMPTY = "  ";


        public const double START_WAIT_TIME = 400; //ms
        public const double MIN_WAIT_TIME = 100;
        public const double MULTIPLICATOR = .99;
        public const int TICK_SPEED = 16; //approximately 60fps


        public static ConsoleColor[] COLORS =  [ConsoleColor.Blue, ConsoleColor.DarkMagenta, ConsoleColor.DarkBlue, ConsoleColor.DarkYellow, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Red];
        public static int NUM_OF_COLORS = COLORS.Length;

        //Score
        public const int SCORE_X_OFFSET = 10;
        public const int SCORE_HEIGHT = 3;
        public const int SCORE_WIDTH = 20;

        public static int[] POINTS_PER_MULTIPLE_LINES = [0, 100, 300, 500, 800];
        public const int POINTS_SOFT_DROP_PER_LINE = 1;
        public const int POINTS_HARD_DROP_PER_LINE = 2;



        //NextBlock

        public const int NEXT_BLOCK_HEIGHT = 4;
        public const int NEXT_BLOCK_WIDTH = 7;
        


        // Menu
        public const int TICKS_PER_ANIMATION = 30;
        public const ConsoleColor HIGHLIGHT_COLOR = ConsoleColor.Cyan;
        public const string SELECTED = "> ";

        public static string[] ASCII_TETRIS = [
            "  _____ ___ _____ ___ ___ ___ ",
            " |_   _| __|_   _| _ \\_ _/ __|",
            "   | | | _|  | | |   /| |\\__ \\",
            "   |_| |___| |_| |_|_\\___|___/"
            ];


        //Credits
        public const int TICKS_PER_LINE = 20;
        public static string[] CREDITS = [
            "  _____ ___ _____ ___ ___ ___ ",
            " |_   _| __|_   _| _ \\_ _/ __|",
            "   | | | _|  | | |   /| |\\__ \\",
            "   |_| |___| |_| |_|_\\___|___/",
            "AUTHOR: MATEJ PUČKO",
            " ",
            "ORIGINAL AUTHOR OF TETRIS: ALEXEY PAJTINOV",
            " ",
            " ",
            "THIS IS A SCHOOL PROJECT FROM THE SUMMER SEMESTER OF MY FIRST YEAR IN UNIVERSITY",
            " ",
            "2026",
            " ",
            " ",
            "THANKS FOR PLAYING"
            ];


        //LoseScreen
        public static string[] ASCII_LOSE = [
            "__   _____  _   _   _    ___  ___ _____ _ ",
            "\\ \\ / / _ \\| | | | | |  / _ \\/ __|_   _| |",
            " \\ V / (_) | |_| | | |_| (_) \\__ \\ | | |_|",
            "  |_| \\___/ \\___/  |____\\___/|___/ |_| (_)"
                                           
        ];

    }
}