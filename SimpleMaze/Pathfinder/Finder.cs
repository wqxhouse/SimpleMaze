using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaze.Pathfinder
{
    public class Finder
    {
        Graph g;
        Rect[] maze;
        public Finder(Rect[] maze)
        {
            g = new Graph();
            this.maze = maze;
        }

        public void CreateGraph()
        {
            int col_size = (int)Math.Sqrt(maze.Length);
            for (int i = 0; i < maze.Length; i++)
            {
                g.Add();
                foreach (int removedWallIndex in maze[i].removedWalls)
                {
                    int adjCellIndex = -1;
                    switch (removedWallIndex)
                    {
                        case 0:
                            adjCellIndex = i - col_size;
                            break;
                        case 1:
                            adjCellIndex = i + 1;
                            break;
                        case 2:
                            adjCellIndex = i + col_size;
                            break;
                        case 3:
                            adjCellIndex = i - 1;
                            break;
                    }

                    if (adjCellIndex < 0)
                    {
                        throw new Exception("Something wrong");
                    }

                    int result = g.Link(i, adjCellIndex);
                    //if (result == -1 || result == -2)
                    //{
                    //    throw new Exception("g.Link() return -1 / -2");
                    //}  
                }
            }

            g.FixAdjList();
        }


        public void FindPath(Action<int, int> drawFunction)
        {
            g.DFSIter(0, maze.Length - 1, drawFunction);
        }
    }
}
