using Maze.Core.Model;

namespace Maze.Core.Contracts
{
  public interface IMazeSolver
  {
    void Solve(MazePixel[,] maze);
  }
}