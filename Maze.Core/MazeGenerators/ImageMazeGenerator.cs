using Maze.Core.Contracts;
using Maze.Core.Model;
using System.Drawing;

namespace Maze.Core.MazeGenerators
{
  public class ImageMazeGenerator : IMazeGenerator
  {
    private const string inputFile = "./Data/Images/15x15.png";
    public MazePixel[,] GeneratorMaze()
    {
      var image = new Bitmap(inputFile);

      MazePixel[,] maze = new MazePixel[image.Height, image.Width];

      // Access the pixel data
      for (int y = 0; y < image.Height; y++)
      {
        for (int x = 0; x < image.Width; x++)
        {
          maze[y, x] = image.GetPixel(x, y).Name switch
          {
            "ff000000" => MazePixel.Wall,
            "ffffffff" => MazePixel.Empty,
            _ => throw new Exception("unknown pixel")
          };
        }
      }

      return maze;
    }
  }
}