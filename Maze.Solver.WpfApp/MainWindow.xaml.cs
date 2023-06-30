using Maze.Core.Contracts;
using Maze.Core.MazeGenerators;
using Maze.Core.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maze.Solver.WpfApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MazePixel[,]? _maze;

    public MainWindow()
    {
      InitializeComponent();
      GenerateMaze();
      DrawMaze(_maze!);
    }

    private void GenerateMaze()
    {
      IMazeGenerator mazeGenerator = new ImageMazeGenerator();
      _maze = mazeGenerator.GeneratorMaze();

    }

    private void DrawPoint(int x, int y, Color color, Brush brush)
    {
      Ellipse myEllipse = new Ellipse();
      SolidColorBrush mySolidColorBrush = new SolidColorBrush();
      mySolidColorBrush.Color = color;
      myEllipse.Fill = mySolidColorBrush;
      myEllipse.StrokeThickness = 1;
      myEllipse.Stroke = brush;
      myEllipse.Width = 1;
      myEllipse.Height = 1;
      Canvas.SetTop(myEllipse, y);
      Canvas.SetLeft(myEllipse, x);
      cvDrawing.Children.Add(myEllipse);
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
      Task.Run(() =>
      {
        SolveMaze(_maze!, 3, 5);
      });
    }

    private bool SolveMaze(MazePixel[,] preMaze, int row, int col)
    {
      var maze = CopyMaze(preMaze);

      if (IsOutOfArray(preMaze, row, col))
      {
        return true;
      }

      if (maze[row, col] == MazePixel.Empty)
      {
        maze[row, col] = MazePixel.Visited;
        DrawMaze(maze);

        if (SolveMaze(maze, row - 1, col) == true) return true;
        if (SolveMaze(maze, row, col + 1) == true) return true;
        if (SolveMaze(maze, row + 1, col) == true) return true;
        if (SolveMaze(maze, row, col - 1) == true) return true;
      }

      // there is no way out -> go back!
      return false;
    }

    private bool IsOutOfArray(MazePixel[,] preMaze, int row, int col)
    {
      if (row < 0 || row > preMaze.GetLength(0))
      {
        return true;
      }
      else if (col < 0 || col > preMaze.GetLength(1))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    private MazePixel[,] CopyMaze(MazePixel[,] maze)
    {
      MazePixel[,] result = new MazePixel[maze.GetLength(0), maze.GetLength(1)];
      Array.Copy(maze, result, maze.Length);

      return result;
    }

    private void DrawMaze(MazePixel[,] maze)
    {
      Application.Current.Dispatcher.Invoke(new Action(() =>
      {
        if (maze == null)
        {
          throw new Exception("_maze is null!");
        }

        cvDrawing.Children.Clear();

        for (int row = 0; row < maze.GetLength(0); row++)
        {
          for (int col = 0; col < maze.GetLength(1); col++)
          {
            switch (maze[row, col])
            {
              case MazePixel.Wall:
                DrawPoint(col, row, Colors.Black, Brushes.Black);
                break;
              case MazePixel.Visited:
                DrawPoint(col, row, Colors.Red, Brushes.Red);
                break;
              case MazePixel.Empty:
                DrawPoint(col, row, Colors.White, Brushes.White);
                break;
            };
          }
        }
      }));


    }
  }
}
