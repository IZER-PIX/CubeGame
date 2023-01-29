using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _mazeCell;

    void Start()
    {
        MazeGenerator mazeGenerator = new MazeGenerator();
        var maze = mazeGenerator.GenerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell cell = Instantiate(_mazeCell, new Vector2(x, y), Quaternion.identity).GetComponent<Cell>();

                cell.LeftWall.SetActive(maze[x, y].LeftWall);
                cell.ButtomWall.SetActive(maze[x, y].ButtomWall);
            }
        }
    }
}
