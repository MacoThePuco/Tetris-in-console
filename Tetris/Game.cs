namespace Tetris
{
static class Game
    {
        public static double waitTime;
        public static Board board;

        public static bool lost = false;


        public static void StartGame()
        {
            Tiskarna.SetupGame();

            ResetGameVariables();
            SetupBoard();
            SetupScore();
            SetupNextBlock();

            Tiskarna.Draw(board);

            Utils.stepStopwatch.Start();
            Play();
        }

        static void SetupScore()
        {
            Score.points = 0;
            Tiskarna.DrawScore();
        }

        static void SetupBoard()
        {
            for (int y = 0; y < board.height; y++)
            {
                for (int x = 0; x < board.width; x++)
                {
                    board.grid[y,x] = -1;
                }
            }
        }

        static void SetupNextBlock()
        {
            Tiskarna.DrawNextBlockPanel(board.nextBlock);
        }

        static void ResetGameVariables()
        {
            waitTime = Constants.START_WAIT_TIME;
            lost = false;
            board = new Board(Constants.BOARD_HEIGHT, Constants.BOARD_WIDTH);
        }

        static void Play()
        {
            while (!lost)
            {
                ConsoleKey? key = null;
                if(Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }

                board.fallingBlock.MoveDownwards(key);
                board.fallingBlock.MoveSideways(key);
                board.fallingBlock.Rotate(key);

                Thread.Sleep(Constants.TICK_SPEED);
            }

            //we only get here after we lose
            Lose();

        }

        static void Lose()
        {
            Tiskarna.SetupLoseScreen(board, board.fallingBlock, board.nextBlock);
            LoseScreen.StartupMenu();
        }
    }

    static class Score
    {
        public static int points = 0;
        
        static public void AddPoints(int pointsToAdd)
        {
            points += pointsToAdd;
            Tiskarna.DrawScore();
        }

        static public void LinesCleared(int numOfLines)
        {
            AddPoints(Constants.POINTS_PER_MULTIPLE_LINES[numOfLines]);
        }
    }

    class Board
    {
        public int height; public int width;
        public Block fallingBlock;
        BlockFactory blockFactory;
        
        public Block nextBlock;

        public int[,] grid;

        public Board(int height, int width)
        {
            this.height = height;
            this.width = width;
            grid = new int[height, width];

            blockFactory = new BlockFactory(this);

            nextBlock = blockFactory.NewBlock();
            SpawnBlock();
        }

        public void SpawnBlock()
        {
            fallingBlock = new Block(nextBlock);
            nextBlock = blockFactory.NewBlock();
            Tiskarna.DrawNextBlockPanel(nextBlock);
        }

        public void StopFallingBlock()
        {
            foreach(Position pos in fallingBlock.GetBlockPositions())
            {
                if(pos.y < 0)
                {
                    Game.lost = true;
                    return;
                }
                grid[pos.y, pos.x] = fallingBlock.color;
            }

            SpawnBlock();
            CheckAndDeleteRows();
            Tiskarna.Draw(this);
        }

        void CheckAndDeleteRows()
        {
            int rowsDeleted = 0;
            for (int y = 0; y < Constants.BOARD_HEIGHT; y++)
            {
                if (RowFull(y))
                {
                    DeleteRow(y);
                    rowsDeleted++;
                }
            }

            if(rowsDeleted >= 1)
            {
                Game.waitTime *= Constants.MULTIPLICATOR;
                if(Game.waitTime < Constants.MIN_WAIT_TIME)
                {
                    Game.waitTime = Constants.MIN_WAIT_TIME;
                }
            }

            Score.LinesCleared(rowsDeleted);
        }

        bool RowFull(int row)
        {
            for (int x = 0; x < Constants.BOARD_WIDTH; x++)
            {
                if(grid[row, x] == -1)
                {
                    return false;
                }
            }
            return true;
        }

        void DeleteRow(int row)
        {
            for (int x = 0; x < Constants.BOARD_WIDTH; x++)
            {
                grid[row, x] = -1;
            }

            for (int y = row - 1; y >= 0 ; y--)
            {
                for (int x = 0; x < Constants.BOARD_WIDTH; x++)
                {
                    grid[y + 1,x] = grid[y,x];
                }
            }

            for (int x = 0; x < Constants.BOARD_WIDTH; x++)
            {
                grid[0, x] = -1;
            }
        }

        public bool BlockWillOverlap(Block block, int yMovement, int xMovement)
        {
            foreach(Position pos in block.GetBlockPositions())
            {
                Position newPos = pos.AddPosition(yMovement, xMovement);

                if (CheckOvelap(newPos))
                {
                    return true;
                }
 
            }

            return false;
        }

        public bool BlockWillOverlap(Block block, int newRotation)
        {
            foreach(Position pos in block.GetBlockPositions(newRotation))
            {
                if (CheckOvelap(pos))
                {
                    return true;
                }
            }

            return false;
        }

        bool CheckOvelap(Position pos)
        {
            if (pos.IsOutOfBounds() || pos.y <= 0)
            {
                return false;
            }

            if(grid[pos.y, pos.x] != -1)
            {
                return true;
            }

            return false;
        }
    }

    class BlockFactory
    {
        List<Func<Block>> originalConstructors;
        List<Func<Block>> nowConstructors;
        
        public BlockFactory(Board board)
        {
            
            originalConstructors= new()
            {
                () => new BlockI(board),
                () => new BlockT(board),
                () => new BlockJ(board),
                () => new BlockL(board),
                () => new BlockO(board),
                () => new BlockZ(board),
                () => new BlockS(board)
            };

            ResetBag();
        }

        void ResetBag()
        {
            nowConstructors = new List<Func<Block>>(originalConstructors);
        }

        public Block NewBlock()
        {
            int indexInList = Utils.rnd.Next(nowConstructors.Count());
            
            Block block = nowConstructors![indexInList]();
            nowConstructors.RemoveAt(indexInList);
            Console.SetCursorPosition(50, nowConstructors.Count());

            if(nowConstructors.Count == 0)
            {
                ResetBag();
            }

            return block;
        }
    }

    class Block
    {
        public Position position;
        public int rotation = 0;

        public List<Position[]> blockPositionsAndRotations = new List<Position[]>();
        public int color = 0;
        
        Board board;

        double timeSinceLastStep = 0f;

        public Block(Board board)
        {
            position = new Position(-1 , Constants.BOARD_WIDTH/ 2);

            this.board = board;

            blockPositionsAndRotations = [[new Position(0,0)]];
        }

        public Block(Block oldBlock)
        {
            position = new Position(oldBlock.position);
            rotation = oldBlock.rotation;
            blockPositionsAndRotations = oldBlock.blockPositionsAndRotations;
            color = oldBlock.color;

            board = oldBlock.board;
        }

        public void Fall()
        {
            position.y += 1;
        }

        public bool ShouldStop()
        {
            foreach(Position pos in GetBlockPositions())
            {
                if(pos.y >= Constants.BOARD_HEIGHT - 1)
                {
                    return true;
                }
            }

            if (board.BlockWillOverlap(this, 1, 0))
            {
                return true;
            }

            return false;
        }
        public List<Position> GetBlockPositions()
        {
            return GetBlockPositionBase(position, rotation);
        }

        public List<Position> GetBlockPositions(Position newPosition)
        {

            return GetBlockPositionBase(newPosition, rotation);
        }


        public List<Position> GetBlockPositions(int newRotation)
        {

            return GetBlockPositionBase(position, newRotation);
        }

        public List<Position> GetBlockPositionBase(Position position, int rotation)
        {
            List<Position> positions = new List<Position>();
            foreach(Position pos in blockPositionsAndRotations[rotation])
            {
                positions.Add(position.AddPosition(pos));
            }

            return positions;
        }

        public void MoveSideways(ConsoleKey? key)
        {
            int sidewaysMovement = GetSidewaysMovement(key);
            if(sidewaysMovement == 0)
            {
                return;
            }
            if (board.BlockWillOverlap(board.fallingBlock, 0, sidewaysMovement))
            {
                return;
            }

            Position newPosition = position.AddPosition(0, sidewaysMovement);
            foreach(Position pos in GetBlockPositions(newPosition))
            {
                if (pos.IsOutOfBounds())
                {
                    return;
                }                    
            }

            position = newPosition;
            Tiskarna.Draw(board);
        }

        int GetSidewaysMovement(ConsoleKey? key)
        {
            if(key == ConsoleKey.LeftArrow)
            {
                return -1;
            }else if (key == ConsoleKey.RightArrow)
            {
                return 1;
            }

            return 0;
        }

        public void Rotate(ConsoleKey? key)
        {
            int rotationToAdd = GetRotation(key);
            if(rotationToAdd == 0)
            {
                return;
            }

            int newRotation = rotation + rotationToAdd;
            if(newRotation >= 4)
            {
                newRotation %= 4;
            }else if (rotation < 0)
            {
                newRotation += 4;
            }

            foreach(Position pos in GetBlockPositions(newRotation))
            {
                if (pos.IsOutOfBounds())
                {
                    return;
                }
            }
            
            if(board.BlockWillOverlap(this, newRotation))
            {
                return;
            }

            rotation = newRotation;

            Tiskarna.Draw(board);
        }

        int GetRotation(ConsoleKey? key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                return 1;
            }

            return 0;
        }

        public void MoveDownwards(ConsoleKey? key)
        {

            if(key is not null && key == ConsoleKey.Spacebar)
            {
                while (!ShouldStop())
                {
                    Fall();
                    Score.AddPoints(Constants.POINTS_HARD_DROP_PER_LINE);
                }

                Tiskarna.Draw(board);

                Utils.ResetStopwatch();

                board.StopFallingBlock();

                return;
            }



            timeSinceLastStep += Utils.stepStopwatch.Elapsed.TotalMilliseconds;
            Utils.ResetStopwatch();

            bool fastDrop = key is not null && key == ConsoleKey.DownArrow;
            if (timeSinceLastStep >= Game.waitTime || fastDrop)
            {
                if (ShouldStop())
                {
                    board.StopFallingBlock();
                    return;
                }
                Fall();

                if (!fastDrop)
                {
                   timeSinceLastStep %= Game.waitTime;
                }

                Tiskarna.Draw(board);

                if (fastDrop)
                {
                    Score.AddPoints(Constants.POINTS_SOFT_DROP_PER_LINE);
                }
            }
        }

        public List<Position> GetGhostBlockPositions()
        {            

            int y = 0;
            while(!board.BlockWillOverlap(this, y + 1, 0)) // this returns true only if it already overlaps
            {

                y++;

                foreach(Position pos in GetBlockPositions(position.AddPosition(y, 0)))
                {
                    if(pos.y >= Constants.BOARD_HEIGHT)
                    {
                        return GetBlockPositions(new Position(y - 1, 0));
                    }                    
                }

            }

            return GetBlockPositions(new Position(y, 0));
        }
        
    }
}