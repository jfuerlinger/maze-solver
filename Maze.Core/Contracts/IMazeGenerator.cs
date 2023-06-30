using Maze.Core.Model;

namespace Maze.Core.Contracts
{
  public interface IMazeGenerator
  {
    MazePixel[,] GeneratorMaze();
  }
}
