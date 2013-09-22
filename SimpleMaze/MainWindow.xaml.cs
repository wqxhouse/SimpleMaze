using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;

namespace SimpleMaze
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MazeVM vm;
        int col_size;
        int cell_size;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        void drawPathFindingLine(int startCell, int endCell)
        {
            double startX = startCell % col_size * cell_size + cell_size * 0.5;
            double endX = endCell % col_size * cell_size + cell_size * 0.5;
            double startY = startCell / col_size * cell_size + cell_size * 0.5;
            double endY = endCell / col_size * cell_size + cell_size * 0.5;

            Line line = new Line();
            line.Stroke = Brushes.BlueViolet;
            line.X1 = startX;
            line.X2 = endX;
            line.Y1 = startY;
            line.Y2 = endY;
            line.StrokeThickness = 4;
            line.Visibility = Visibility.Visible;

            mazeGrid.Children.Add(line);
        }

        void InitWindowSize(int cellSize, int cellNumber)
        {
            int w = cellSize * cellNumber;
            int h = w;

            this.Width = w + 200;
            this.Height = h + 200;

            ////mazeGrid.HorizontalAlignment = HorizontalAlignment.Center;
            ////mazeGrid.VerticalAlignment = VerticalAlignment.Center;
            mazeGrid.Width = w;
            mazeGrid.Height = h;

            
        }

        void DrawBoundary(int cellSize, int cellNumber)
        {
            Rectangle r = new Rectangle();
            r.Width = cellSize * cellNumber;
            r.Height = r.Width;

            r.Stroke = Brushes.Blue;
            r.HorizontalAlignment = HorizontalAlignment.Left;
            r.VerticalAlignment = VerticalAlignment.Top;
            mazeGrid.Children.Add(r);
        }

        void DrawCells(Rect[] rectArr, int cellColNumber, int cellSize)
        {

            for (int i = 0; i < cellColNumber; i++)
            {
                for (int j = 0; j < cellColNumber; j++)
                {
                    DrawOneCell(rectArr[i * cellColNumber + j], cellSize, cellSize * j, cellSize * i);
                }
            }
        }

        void DrawOneCell(Rect r, int pixels, int posX = 0, int posY = 0)
        {
            Line line;
            foreach (int i in r.walls)
            {
                switch (i)
                {
                    case 0:
                        line = new Line();
                        mazeGrid.Children.Add(line);
                        line.X1 = posX;
                        line.Y1 = posY;
                        line.X2 = pixels + posX;
                        line.Y2 = posY;
                        line.StrokeThickness = 1;
                        line.SnapsToDevicePixels = true;
                        line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                        line.Stroke = Brushes.Red;
                        line.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        line = new Line();
                        mazeGrid.Children.Add(line);
                        line.X1 = pixels + posX;
                        line.Y1 = posY;
                        line.X2 = pixels + posX;
                        line.Y2 = pixels + posY;
                        line.StrokeThickness = 1;
                        line.SnapsToDevicePixels = true;
                        line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                        line.Stroke = Brushes.Red;
                        line.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        line = new Line();
                        mazeGrid.Children.Add(line);
                        line.X1 = posX;
                        line.Y1 = pixels + posY;
                        line.X2 = pixels + posX;
                        line.Y2 = pixels + posY;
                        line.StrokeThickness = 1;
                        line.SnapsToDevicePixels = true;
                        line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                        line.Stroke = Brushes.Red;
                        line.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        line = new Line();
                        mazeGrid.Children.Add(line);
                        line.X1 = posX;
                        line.Y1 = posY;
                        line.X2 = posX;
                        line.Y2 = pixels + posY;
                        line.StrokeThickness = 1;
                        line.SnapsToDevicePixels = true;
                        line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
                        line.Stroke = Brushes.Red;
                        line.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void GenerateMaze(object sender, RoutedEventArgs e)
        {
            mazeGrid.Children.Clear();
            int size;
            bool sz_result = Int32.TryParse(size_input.Text, out size);
            col_size = size;


            if (!sz_result)
            {
                throw new Exception("Invalid number");
            }

            int block_size;
            bool pad_result = Int32.TryParse(pad_input.Text, out block_size);
            if (!pad_result)
            {
                throw new Exception("Invalid number: pad");
            }
            cell_size = block_size;

            vm = new MazeVM(size);
            vm.BreakWalls();
            InitWindowSize(block_size, vm.Size);
            DrawCells(vm.Maze, vm.Size, block_size);
            DrawBoundary(block_size, vm.Size);

            ExploreGrid.Visibility = Visibility.Visible;
        }

        private void ExploreMaze(object sender, RoutedEventArgs e)
        {
            if (vm == null)
            {
                throw new Exception("VM not initialized");
            }

            Pathfinder.Finder f = new Pathfinder.Finder(vm.Maze);
            f.CreateGraph();
            f.FindPath(drawPathFindingLine);

        }

        
    }


}
