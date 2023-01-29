public class MazeGenerator
{
    public class MazeCell
    {
        public int X;
        public int Y;

        public bool LeftWall = true;
        public bool ButtomWall = true;
    }

    public int Width { get; private set; } = 5;
    public int Height { get; private set; } = 5;

    public MazeCell[,] GenerateMaze()
    {
        var maze = new MazeCell[Width, Height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeCell() { X = x, Y = y };
            }
        }

        return maze;
    }
}
