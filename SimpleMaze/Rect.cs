using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace SimpleMaze
{
    public enum OPTIONS
    {
        UpperLeft, 
        UpperRight, 
        LowerLeft, 
        LowerRight, 
        Up, 
        Down, 
        Left, 
        Right, 
        Default
    }
    public class Rect
    {
        public List<int> walls;
        public OPTIONS Type
        {
            get;
            set;
        }
        public List<int> removedWalls;


        public Rect(OPTIONS o)
        {
            switch (o)
            {
                case OPTIONS.Default:
                    walls = new List<int> { 0, 1, 2, 3 };
                    break;
                case OPTIONS.UpperLeft:
                    walls = new List<int> { 1, 2 };
                    break;
                case OPTIONS.UpperRight:
                    walls = new List<int> { 2, 3 };
                    break;
                case OPTIONS.LowerLeft:
                    walls = new List<int> { 0, 1 };
                    break;
                case OPTIONS.LowerRight:
                    walls = new List<int> { 0, 3 };
                    break;
                case OPTIONS.Up:
                    walls = new List<int> { 1, 2, 3 };
                    break;
                case OPTIONS.Down:
                    walls = new List<int> { 0, 1, 3 };
                    break;
                case OPTIONS.Left:
                    walls = new List<int> { 0, 1, 2 };
                    break;
                case OPTIONS.Right:
                    walls = new List<int> { 0, 2, 3 };
                    break;
                default:
                    walls = new List<int> { 0, 1, 2, 3 };
                    break;
            }
            Type = o;
            removedWalls = new List<int>();

            
        }

       
    }
}
