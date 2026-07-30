namespace Tetris
{
    class BlockI : Block
    {
        public BlockI(Board board) : base(board)
        {
            color = 0;
            blockPositionsAndRotations = [
                [new Position(0, -1), new Position(0, 0), new Position(0, 1), new Position(0,2)],
                [new Position(0, 1), new Position(1, 1), new Position(2, 1), new Position(-1, 1)],
                [new Position(1, -1), new Position(1, 0), new Position(1, 1), new Position(1,2)],
                [new Position(0,0), new Position(1,0), new Position(2, 0), new Position(-1,0)]];
        }
    }

    class BlockT : Block
    {

        public BlockT(Board board) : base(board)
        {
            color = 1;
            blockPositionsAndRotations = [
                [new Position(0, 0), new Position(0, 1), new Position(0, -1), new Position(-1, 0)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, 0), new Position(0, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(0, -1), new Position(0, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(0, -1), new Position(-1, 0)]];
        }
    }

    class BlockJ : Block
    {

        public BlockJ(Board board) : base(board)
        {
            color = 2;
            blockPositionsAndRotations = [

                [new Position(0, 0), new Position(0, 1), new Position(0, -1), new Position(-1, -1)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, 0), new Position(-1, 1)],
                [new Position(0, 0), new Position(0, 1), new Position(0, -1), new Position(1, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, 0), new Position(1, -1)]];
        }
    }

    class BlockL : Block
    {

        public BlockL(Board board) : base(board)
        {
            color = 3;
            blockPositionsAndRotations = [
                [new Position(0, 0), new Position(0, 1), new Position(0, -1), new Position(-1, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, 0), new Position(1, 1)],
                [new Position(0, 0), new Position(0, 1), new Position(0, -1), new Position(1, -1)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, 0), new Position(-1, -1)]];
        }
    }

    class BlockO : Block
    {

        public BlockO(Board board) : base(board)
        {
            color = 4;
            blockPositionsAndRotations = [
                [new Position(-1, 0), new Position(0, 0), new Position(0, 1), new Position(-1, 1)],
                [new Position(-1, 0), new Position(0, 0), new Position(0, 1), new Position(-1, 1)],
                [new Position(-1, 0), new Position(0, 0), new Position(0, 1), new Position(-1, 1)],
                [new Position(-1, 0), new Position(0, 0), new Position(0, 1), new Position(-1, 1)]];


        }
    }

    class BlockS : Block
    {

        public BlockS(Board board) : base(board)
        {
            color = 5;
            blockPositionsAndRotations = [
                [new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -1)],
                [new Position(0, 0), new Position(-1, 0), new Position(1, 1), new Position(0, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, -1), new Position(0, -1)]];
        }
    }

    class BlockZ : Block
    {

        public BlockZ(Board board) : base(board)
        {
            color = 6;
            blockPositionsAndRotations = [
                [new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(-1, 1), new Position(0, 1)],
                [new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -1)],
                [new Position(0, 0), new Position(-1, 0), new Position(1, -1), new Position(0, -1)]];
        }
    }

}