using System.Collections.Generic;

public class MazeGenerator
{
    public class MazeCell
    {
        public int DistanceFromStart;

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
                maze[x, y] = new MazeCell() { X = x, Y = y };   // заполн€ем массив клетками
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
            maze[x, Height - 1].LeftWall = false;
        for (int y = 0; y < maze.GetLength(0); y++)     // убираем стенки с карЄв
            maze[Width - 1, y].ButtomWall = false;

        RemoveWallBacktracker(maze);
        PlaceExit(maze);

        return maze;
    }

    private void PlaceExit(MazeCell[,] maze)
    {
        var furthest = maze[0, 0];

        // находим дальнюю клетку по ’
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[x, Height - 2]; 
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[x, 0];
        }

        // находим дальнюю клетку по ”
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[0, y];
        }

        if (furthest.X == 0)    // если стена на краю лабиринта, то убираем еЄ
            furthest.LeftWall = false;
        else if (furthest.Y == 0)
            furthest.ButtomWall = false;
        else if (furthest.X == Width - 2)
            maze[furthest.X + 1, furthest.Y].LeftWall = false;
        else if (furthest.Y == Height - 2)
            maze[furthest.X, furthest.Y + 1].ButtomWall = false;
    }

    private void RemoveWallBacktracker(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];  // начальна€ точка лабиринта
        current.Visited = true;
        current.DistanceFromStart = 0;

        var stack = new Stack<MazeCell>();  // помещаем в стек клетки которые были выбраны

        do
        {
            var notVisited = new List<MazeCell>();

            var x = current.X;
            var y = current.Y;

            // добавл€ем непосещЄнную стену в список
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
                chosen.DistanceFromStart = stack.Count; // находим дальнюю точку
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
            if (current.Y > chosen.Y)   // если клетка 1 над клеткой 2, то убираем нижнюю стену
                current.ButtomWall = false;
            else
                chosen.ButtomWall = false;
        }
        else
        {
            if (current.X > chosen.X)   // если клетка 1 правее 2, то убираем левую стену
                current.LeftWall = false;
            else
                chosen.LeftWall = false;
        }
    }
}
