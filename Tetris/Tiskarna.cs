using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Tetris
{
    
    class Tiskarna
    {

        static Position center;
        static Position borderStart;
        static Position boardStart;
        static Position sidePanelStart;

        public static void Setup()
        {
            Clear();
            Console.CursorVisible = false;

            center = new Position(Console.WindowHeight/ 2, Console.BufferWidth / 2);
        }

        public static void Clear()
        {
            Console.Clear();
        }

        static void ClearLine(Position start, int width, bool scaleWithBlockSzie)
        {
            int scaling = 1;
            if (scaleWithBlockSzie)
            {
                scaling = Constants.BLOCK.Length;
            }
            Console.SetCursorPosition(start.x, start.y);
            Console.Write(new string(' ', width * scaling));    
        }

        static void SetRelativeCursorPosition(Position position, Position relativeTo, bool scaleWithBlockSize = true)
        {
            Position pos = new Position(position);
            if (scaleWithBlockSize)
            {
                pos.x *= Constants.BLOCK.Length;
            }
            Position newPos = relativeTo.AddPosition(pos);
            Console.SetCursorPosition(newPos.x, newPos.y);
        }

        public static void SetupGame()
        {
            Console.Clear();
            borderStart = center.AddPosition(-Constants.BOARD_HEIGHT/2, -Constants.BOARD_WIDTH/2 * Constants.BLOCK.Length - 1);
            boardStart = borderStart.AddPosition(0, 1);
            sidePanelStart = boardStart.AddPosition(0, Constants.BOARD_WIDTH + 1 + Constants.SCORE_X_OFFSET);
        }

        public static void Draw(Board board)
        {
 
            ClearLine(borderStart.AddPosition(-1, 0),  Constants.BOARD_WIDTH + 1, true);
            ClearLine(borderStart.AddPosition(-2, 0),  Constants.BOARD_WIDTH + 1, true);

            SetRelativeCursorPosition(new Position(0,0), borderStart);
            for (int y = 0; y < Constants.BOARD_HEIGHT; y++)
            {
                DrawGameRow(board, y);
            }

            DrawBlock(board.fallingBlock);

            SetRelativeCursorPosition(new Position(Constants.BOARD_HEIGHT, 0), borderStart);
            Console.Write(Constants.BORDER_CORNER_BOTTOM_LEFT);
            for (int x = 0; x < Constants.BOARD_WIDTH * Constants.BLOCK.Length; x++)
            {
                Console.Write(Constants.BOARD_BOTTOM);
            }
            Console.Write(Constants.BORDER_CORNER_BOTTOM_RIGHT);
        }

        static void DrawBlock(Block block)
        {
            Position cursorOriginal = new Position(Console.GetCursorPosition().Top, Console.GetCursorPosition().Left);

            DrawGhostBlock(block);

            foreach(Position realPosition in block.GetBlockPositions())
            {


                SetRelativeCursorPosition(realPosition, boardStart);
                DrawPartOfBlock(block.color, Constants.BLOCK);
            }
                

            Console.SetCursorPosition(cursorOriginal.x, cursorOriginal.y);
        }

        static void DrawGhostBlock(Block block)
        {
            foreach(Position pos in block.GetGhostBlockPositions())
            {
                Position realPosition = block.position.AddPosition(pos);
                SetRelativeCursorPosition(realPosition, boardStart);
                DrawPartOfBlock(block.color, Constants.GHOST_BLOCK);
            }
        }

        static void DrawPartOfBlock(int color, string fill)
        {
            Console.ForegroundColor = Constants.COLORS[color];
            Console.Write(fill);
            Console.ForegroundColor = Constants.BASE_COLOR;

        }

        static void DrawGameRow(Board board, int row)
        {
            SetRelativeCursorPosition(new Position(row, 0), borderStart);
            Console.Write(Constants.BORDER_VERTICAL);
            for (int x = 0; x < Constants.BOARD_WIDTH; x++)
            {
                if(board.grid[row,x] >= 0)
                {
                    DrawPartOfBlock(board.grid[row,x], Constants.BLOCK);
                }
                else
                {
                    Console.Write(Constants.EMPTY);
                }
            }
            

            Console.Write(Constants.BORDER_VERTICAL);
        }

        public static void DrawScore()
        {
            DrawRectangle(sidePanelStart, Constants.SCORE_HEIGHT, Constants.SCORE_WIDTH, false);

            SetRelativeCursorPosition(new Position(Constants.SCORE_HEIGHT/2 + 1, 1), sidePanelStart);
            string text = "SCORE: ";
            Console.Write($"{text}{Score.points.ToString($"D{Constants.SCORE_WIDTH - text.Length - 2}")}");
        }

        public static void DrawNextBlockPanel(Block block)
        {

            Position start = sidePanelStart.AddPosition(Constants.SCORE_HEIGHT + 2,0);
            DrawRectangle(start, Constants.NEXT_BLOCK_HEIGHT, Constants.NEXT_BLOCK_WIDTH, true);
            
            SetRelativeCursorPosition(new Position(0, 1), start, false);
            Console.Write("NEXT BLOCK");

            Position centerOfBlock = start.AddPosition(Constants.NEXT_BLOCK_HEIGHT / 2 + 1, Constants.NEXT_BLOCK_WIDTH / 2 * Constants.BLOCK.Length);

            foreach(Position pos in block.blockPositionsAndRotations[block.rotation])
            {
                SetRelativeCursorPosition(pos, centerOfBlock);
                DrawPartOfBlock(block.color, Constants.BLOCK);
            }
        }

        static void DrawRectangle(Position start, int height, int width, bool scaleWithBlockSize)
        {
            Console.SetCursorPosition(start.x, start.y);

            Console.Write(Constants.BORDER_CORNER_TOP_LEFT);
            for (int i = 0; i < width; i++)
            {
                int count = 1;
                if (scaleWithBlockSize)
                {
                    count = Constants.BLOCK.Length;
                }
                for(int x = 0; x < count; x++)
                {
                    Console.Write(Constants.BORDER_HORIZONTAL);
                }
            }
            Console.Write(Constants.BORDER_CORNER_TOP_RIGHT);

            for (int y = 1; y <= height; y++)
            {
                SetRelativeCursorPosition(new Position(y, 0), start, scaleWithBlockSize);
                Console.Write(Constants.BORDER_VERTICAL);

                int count = width;
                if (scaleWithBlockSize)
                {
                    count *= Constants.BLOCK.Length;
                }

                for(int x = 0; x < count; x++)
                {
                    Console.Write(" ");
                }

                Console.Write(Constants.BORDER_VERTICAL);

            }

            SetRelativeCursorPosition(new Position(height + 1, 0), start, scaleWithBlockSize);
            Console.Write(Constants.BORDER_CORNER_BOTTOM_LEFT);
            for (int i = 0; i < width; i++)
            {
                int count = 1;
                if (scaleWithBlockSize)
                {
                    count = Constants.BLOCK.Length;
                }
                for(int x = 0; x < count; x++)
                {
                    Console.Write(Constants.BORDER_HORIZONTAL);
                }
            }
            Console.Write(Constants.BORDER_CORNER_BOTTOM_RIGHT);
        }

        //
        // LOSE SCREEN
        //
        public static void SetupLoseScreen(Board board, Block fallingBlock, Block nextBlock)
        {
            
            Clear();
            borderStart.x -= board.width * Constants.BLOCK.Length * 2;
            boardStart.x = borderStart.x + 1;
            sidePanelStart.x += board.width * Constants.BLOCK.Length;
            Draw(board);
            DrawScore();
            DrawNextBlockPanel(nextBlock);
            DrawBlock(fallingBlock);

        }
    
        //
        //MENU
        //
        static public void DrawMenu(string[] options, int pointer, bool color, string[] asciiText)
        {

            int longestStringLenght = 0;
            foreach(string option in options)
            {
                if(option.Length > longestStringLenght)
                {
                    longestStringLenght = option.Length;
                }
            }

            int numOfOptions = options.Length;
            int moveUp = numOfOptions - 1;

            DrawAsciiHeader(-(moveUp + asciiText.Length + 3), asciiText);

            Position start = new Position(-moveUp, -longestStringLenght/2);

            for(int i = 0; i < numOfOptions; i++)
            {
                Position nowPos = start.AddPosition(i * 2,  -Constants.SELECTED.Length); // - for SELECTED string '> '

                SetRelativeCursorPosition(nowPos, center, false);
            
                if(i == pointer)
                {
                    if (color)
                    {
                        Console.ForegroundColor = Constants.HIGHLIGHT_COLOR;
                    }
                    Console.Write(Constants.SELECTED);
                }   
                else
                {
                    for(int l = 0; l < Constants.SELECTED.Length; l++)
                    {
                        Console.Write(" ");
                    }
                }

                Console.Write(options[i]);
                Console.ForegroundColor = Constants.BASE_COLOR;

            }
        }


        static void DrawAsciiHeader(int yMovement, string[] asciiText)
        {
            Position pos = new Position(yMovement, -asciiText[0].Length/2);

            foreach(string row in asciiText)
            {
                SetRelativeCursorPosition(pos, center, false);
                Console.Write(row);
                pos.y += 1;
            }
        }
    
        public static void DrawCredits(int y)
        {
            Position start = new Position(y, center.x);

            Clear();
            foreach(string line in Constants.CREDITS)
            {
                start.y += 1;
                if(start.y >= Console.WindowHeight || start.y < 0)
                {
                    continue;
                }
                Position nowPos = start.AddPosition(0, -line.Length/2);
                Console.SetCursorPosition(nowPos.x, nowPos.y);
                Console.Write(line);
            }
        }
    }
}