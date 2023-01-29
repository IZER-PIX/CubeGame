using UnityEngine;
using static MazeGenerator;

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
                Instantiate(_mazeCell, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}
