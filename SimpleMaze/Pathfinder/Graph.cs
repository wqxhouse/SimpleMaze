using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaze.Pathfinder
{
    public class Graph
    {
        public List<List<int> > vtx_db; //store adjacency list
        List<Edge> e_db;

        public int Count
        {
            get
            {
                return vtx_db.Count;
            }
        }

        public Graph()
        {
            vtx_db = new List<List<int> >();
            e_db = new List<Edge>();

            //the index of adj_list is the vertex id;
        }

        public void Add()
        {
            //var q_id = from v in vtx_db
            //           orderby v.vid descending
            //           select v;

            //var largest_v = q_id.FirstOrDefault();
            //if (largest_v == null)
            //{
            //    throw new Exception("new_id error");
            //}

            //int new_id = largest_v.vid + 1;
            //vtx_db.Add(new Vertex(name, new_id));


            //return new_id;
            vtx_db.Add(new List<int>());
        }

        public int Link(int vid1, int vid2)
        {
            // guess: if vid1 > vid2, it indicates a reverse edge
            if (vid1 >= vid2)
            {
                return -3;
            }

            //if (vid1 >= Count || vid2 >= Count)
            //{
            //    return -2;
            //}

            //reverse edge
            //bool isReverse = vtx_db[vid1].Any(vid => vid == vid2);
            //if (isReverse)
            //{
            //    return -3;
            //}

            Edge newEdge = new Edge(vid1, vid2);
            e_db.Add(newEdge);

            //process adjacency list
            vtx_db[vid1].Add(vid2);

            return 1;
        }

        
        public void FixAdjList()
        {
            foreach (Edge e in e_db)
            {
                if (e.vid1 < e.vid2)
                {
                    vtx_db[e.vid2].Add(e.vid1);
                }
            }
        }

        public void DFSIter(int start, int end, Action<int, int> drawPath_callback)
        {
            Stack<int> s = new Stack<int>();
            Dictionary<int, bool> visited = new Dictionary<int,bool>();
            for (int i = 0; i < this.Count; i++)
            {
                visited.Add(i, false);
            }


            s.Push(start);
            while (s.Count != 0)
            {
                int top = s.Peek();
                if (top == end)
                {
                    return;
                }

                for (int k = 0; k < vtx_db[top].Count; k++)
                {
                    int i = vtx_db[top][k];

                    if (visited[i] == true)
                    {
                        if (k == vtx_db[top].Count - 1)
                        {
                            //pop explored/done node
                            s.Pop();
                        }

                        continue;

                    }


                    //if adj not visited
                    else
                    {
                        visited[i] = true;
                        s.Push(i);
                        drawPath_callback(top, i);
                        break;
                    }
                }

               
            }
        }

        public void DFSRecur()
        {
        }

        /// <summary>
        /// Inner classes: Edge
        /// </summary>
        //class Vertex
        //{
        //    //public string name;
        //    //public int vid;



        //    //public Vertex(string name, int id)
        //    //{
        //    //    this.name = name;
        //    //    this.vid = id;
        //    //}
            

        //}


        class Edge
        {
            public int vid1;
            public int vid2;

            public Edge(int vid1, int vid2)
            {
                this.vid1 = vid1;
                this.vid2 = vid2;
            }


        }
    }
}
