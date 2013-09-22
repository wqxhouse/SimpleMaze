using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SimpleMaze
{
    public class MazeVM
    {     
        struct CornerIndex
        {
            public int UL;
            public int UR;
            public int LL;
            public int LR;
        }

        private int _size;
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value != _size)
                {
                    _size = value;
                }
            }
        }

        Rect[] maze;
        CornerIndex c;

        public Rect[] Maze
        {
            get { return maze; }
        }

        public MazeVM(int size)
        {
            Size = size;
            maze = new Rect[size * size];
            c = new CornerIndex();
            c.UL = 0;
            c.UR = size - 1;
            c.LL = size * (size - 1);
            c.LR = size * size - 1;
            
            Init();
        }

        public void Init()
        {
            //Outer corner cases
            maze[c.UL] = new Rect(OPTIONS.UpperLeft);
            maze[c.UR] = new Rect(OPTIONS.UpperRight);
            maze[c.LL] = new Rect(OPTIONS.LowerLeft);
            maze[c.LR] = new Rect(OPTIONS.LowerRight);

            for (int i = c.UL + 1; i < c.UR; i++)
            {   //upper & lower

                maze[i] = new Rect(OPTIONS.Up);
                maze[c.LL + i] = new Rect(OPTIONS.Down);

                //left & right
                maze[i * _size] = new Rect(OPTIONS.Left);
                maze[i * _size + c.UR] = new Rect(OPTIONS.Right);
            }

            //Normal -- center bricks
            for (int i = 1; i < _size - 1; i++)
            {
                for (int j = 1; j < _size - 1; j++)
                {
                    maze[i * _size + j] = new Rect(OPTIONS.Default);
                }
            }
        }

        public void SetSize(int size)
        {
            Size = size;
        }

        //List<int> visitedList;

        public void BreakWalls()
        {

            DSet ds = new DSet(_size * _size);
            Random rnd = new Random();
            Random rnd2 = new Random();
            //visitedList = new List<int>();

            while (ds.GetSize() > 1)
            {
                int currCellIndex = rnd.Next(maze.Length);
                //adding the following will infinite loop the program
                //if (visitedList.Any(a => a == currCellIndex))
                //{
                //    continue;
                //}

                //visitedList.Add(currCellIndex);

                Rect currCell = maze[currCellIndex];
                if (currCell.walls.Count == 0)
                {
                    continue;
                }

                int randCurrWallIndex = rnd2.Next(currCell.walls.Count);
                int wallIndex = currCell.walls[randCurrWallIndex];
                int adjCellIndex = 0;
                int adjWallIndex = 0;

                switch (wallIndex)
                {
                    case 0:
                        adjCellIndex = currCellIndex - Size;
                        adjWallIndex = 2;
                        break;
                    case 1:
                        adjCellIndex = currCellIndex + 1;
                        adjWallIndex = 3;
                        break;
                    case 2:
                        adjCellIndex = currCellIndex + Size;
                        adjWallIndex = 0;
                        break;
                    case 3:
                        adjCellIndex = currCellIndex - 1;
                        adjWallIndex = 1;
                        break;
                }
                Rect adjCell = maze[adjCellIndex];

                if (ds.Find(currCellIndex) != ds.Find(adjCellIndex))
                {
                    currCell.walls.Remove(wallIndex);
                    currCell.removedWalls.Add(wallIndex);
                    adjCell.walls.Remove(adjWallIndex);
                    adjCell.removedWalls.Add(adjWallIndex);

                    ds.Union(currCellIndex, adjCellIndex); 
                }

            }

        


        }


        public void Print()
        {
            int counter = 1;
            foreach (Rect r in maze)
            {
                foreach (int i in r.walls)
                {
                    Trace.Write(i);
                }
                Trace.Write("          ");

                if (counter % _size == 0)
                {
                    Trace.WriteLine("");
                }
                counter++;

            }
        }
    }
}
