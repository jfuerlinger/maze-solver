using Maze.Core;
using Maze.Core.Contracts;
using Maze.Core.MazeGenerators;
using Maze.Core.MazeSolvers;
using Maze.Core.Model;
using System.Text;

namespace Maze.Solver
{
  internal class Program
  {
    static void Main(string[] args)
    {
      IMazeGenerator mazeGenerator = new SimpleMazeGenerator();
      MazePixel[,] maze = mazeGenerator.GeneratorMaze();

      IMazeSolver solver = new SimpleMazeSolver(PrintMaze, 5);

      Console.ReadKey();
      solver.Solve(maze);
    }

    private static void PrintMaze(MazePixel[,] maze)
    {
      Console.Clear();

      StringBuilder sb = new();
      for (int row = 0; row < maze.GetLength(0); row++)
      {
        sb.Clear();
        for (int col = 0; col < maze.GetLength(1); col++)
        {
          sb.Append(maze[row, col] switch
          {
            MazePixel.Wall => "░",
            MazePixel.Empty => " ",
            MazePixel.Visited => ".",
            _ => throw new Exception("Unknown MazePixel type!")
          });
        }

        Console.WriteLine(sb.ToString());
      }
    }
  }
}