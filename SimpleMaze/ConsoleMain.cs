using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaze
{
    class ConsoleMain
    {
        static void Main()
        {
            MazeVM vm = new MazeVM(3);
            vm.BreakWalls();

            Pathfinder.Finder f = new Pathfinder.Finder(vm.Maze);
            f.CreateGraph();
            //f.FindPath();
        }
    }
}
