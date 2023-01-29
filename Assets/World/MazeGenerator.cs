using System;
using System.Collections.Generic;
using System.Threading;

public class MazeGenerator
{
    public class MazeCell
    {
        public int X;
        public int Y;

        public bool LeftWall = true;
        public bool ButtomWall = true;

        public bool Visited = false;
    }

    public int Width { get; private set; } = 15;
    public int Height { get; private set; } = 15;

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
        RemoveWallBacktracker(maze);

        return maze;
    }

    private void RemoveWallBacktracker(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        current.Visited = true;

        var stack = new Stack<MazeCell>();

        do
        {
            var notVisited = new List<MazeCell>();

            var x = current.X;
            var y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited)
                notVisited.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited)
                notVisited.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited)
                notVisited.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited)
                notVisited.Add(maze[x, y + 1]);

            if(notVisited.Count > 0)
            {
                var chosen = notVisited[UnityEngine.Random.Range(0, notVisited.Count)];

                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(current);
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeCell current, MazeCell chosen)
    {
        if (current.X == chosen.X)
        {
            if (current.Y > chosen.Y)
                current.ButtomWall = false;
            else
                chosen.ButtomWall = false;
        }
        else
        {
            if (current.X > chosen.X)
                current.LeftWall = false;
            else
                chosen.LeftWall = false;
        }
    }
}
