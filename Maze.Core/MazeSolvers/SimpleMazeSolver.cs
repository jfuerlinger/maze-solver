using Maze.Core.Contracts;
using Maze.Core.Model;

namespace Maze.Core.MazeSolvers
{
  public class SimpleMazeSolver : IMazeSolver
  {
    private readonly DrawMazeDelegate _drawMazeDelegate;
    private readonly int _delayBetweenStepsInMSec;

    public delegate void DrawMazeDelegate(MazePixel[,] maze);

    public SimpleMazeSolver(DrawMazeDelegate drawMazeDelegate, int delayBetweenStepsInMSec = 1000)
    {
      _drawMazeDelegate = drawMazeDelegate;
      _delayBetweenStepsInMSec = delayBetweenStepsInMSec;
    }

    public void Solve(MazePixel[,] maze)
    {
      (int row, int col) = FindStartPositionInMaze(maze);
      if (row == -1 || col == -1)
      {
        throw new Exception("There is no starting point!");
      }

      SolveMaze(maze, row, col);
    }

    private bool SolveMaze(MazePixel[,] preMaze, int row, int col)
    {
      var maze = CopyMaze(preMaze);

      if (IsExit(preMaze, row, col))
      {
        return true;
      }

      if (maze[row, col] == MazePixel.Empty)
      {
        maze[row, col] = MazePixel.Visited;
        Thread.Sleep(_delayBetweenStepsInMSec);

        _drawMazeDelegate(maze);

        if (SolveMaze(maze, row - 1, col) == true) return true; // we found the exit
        if (SolveMaze(maze, row, col + 1) == true) return true; // we found the exit
        if (SolveMaze(maze, row + 1, col) == true) return true; // we found the exit
        if (SolveMaze(maze, row, col - 1) == true) return true; // we found the exit
      }

      // there is no way out -> go back!
      return false;
    }

    private static (int row, int col) FindStartPositionInMaze(MazePixel[,] maze)
    {
      for (int i = 0; i < maze.GetLength(0); i++)
      {
        if (maze[0, i] == MazePixel.Empty)
        {
          return (0, i);
        }

        if (maze[maze.GetLength(0) - 1, i] == MazePixel.Empty)
        {
          return (maze.GetLength(0) - 1, i);
        }
      }

      for (int i = 0; i < maze.GetLength(1); i++)
      {
        if (maze[i, 0] == MazePixel.Empty)
        {
          return (i, 0);
        }

        if (maze[i, maze.GetLength(1) - 1] == MazePixel.Empty)
        {
          return (i, maze.GetLength(1) - 1);
        }
      }

      return (-1, -1);
    }

    private static bool IsExit(MazePixel[,] preMaze, int row, int col)
    {
      if (row < 0 || row > preMaze.GetLength(0) - 1)
      {
        return true;
      }
      else if (col < 0 || col > preMaze.GetLength(1) - 1)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private static MazePixel[,] CopyMaze(MazePixel[,] maze)
    {
      MazePixel[,] result = new MazePixel[maze.GetLength(0), maze.GetLength(1)];
      Array.Copy(maze, result, maze.Length);

      return result;
    }
  }
}
