using Maze.Core.Model;

namespace Maze.Solver
{
  internal class MazeSolver
  {
    private Action<MazePixel[,]> _printMaze;

    public MazeSolver(Action<MazePixel[,]> printMaze)
    {
      _printMaze = printMaze;
    }
  }
}