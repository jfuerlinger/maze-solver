using Maze.Core.Contracts;
using Maze.Core.Model;

namespace Maze.Core.MazeGenerators
{
  public class SimpleMazeGenerator : IMazeGenerator
  {
    private const string InputFile = @"./Data/Text/maze_01.txt";

    public MazePixel[,] GeneratorMaze()
    {
      string[] lines = File.ReadAllLines(InputFile);

      if(!lines.Any())
      {
        throw new Exception("There is no content in the input file!");
      }
      

      MazePixel[,] result = new MazePixel[lines.Length, lines[0].Length];

      for (int row = 0; row < lines.Length; row++)
      {
        for (int col = 0; col < lines[row].Length; col++)
        {
          result[row, col] = lines[row][col] switch
          {
            'X' => MazePixel.Wall,
            ' ' => MazePixel.Empty,
             _  => throw new Exception($"Unknown data point '{lines[row][col]}' at [{row}/{col}]")
          };
        }
      }

      return result;
    }
  }
}
