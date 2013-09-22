using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMaze
{
    //public class DSet
    //{
    //    public int GetSize()
    //    {
    //        return size;
    //    }

    //    private int[] set;
    //    private int[] sizes;
    //    private int size;
    //    public DSet(int size)
    //    {
    //        this.set = new int[size];
    //        for (int i = 0; i < size; i++) { this.set[i] = i; }
    //        this.sizes = new int[size];
    //        for (int i = 0; i < size; i++) { this.sizes[i] = 1; }
    //        this.size = size;
    //    }
    //    public int Find(int item)
    //    {
    //        int root = item;
    //        // find the root
    //        while (set[root] != root)
    //        {
    //            root = set[root];
    //        }
    //        // now shorten the paths
    //        int curr = item;
    //        while (set[curr] != root)
    //        {
    //            set[curr] = root;
    //        }
    //        return root;
    //    }
    //    public int Union(int item1, int item2)
    //    {
    //        int group1 = Find(item1);
    //        int group2 = Find(item2);
    //        --size;
    //        if (sizes[group1] > sizes[group2])
    //        {
    //            set[group2] = group1;
    //            sizes[group1] += sizes[group2];
    //            return group1;
    //        }
    //        else
    //        {
    //            set[group1] = group2;
    //            sizes[group2] += sizes[group1];
    //            return group2;
    //        }
    //    }
    //}  
    public class DSet
    {
        public int GetSize()
        {
            return _size;
        }

        int[] db;
        int _size;
        public int Size
        {
            get
            {
                return _size;
            }
        }

        public DSet(int n)
        {
            db = new int[n];
            _size = db.Length;
            for (int i = 0; i < n; i++)
            {
                db[i] = -1;
            }
        }

        public int Find(int m)
        {
            if (db[m] < 0)
            {
                return m;
            }
            else
            {
                return db[m] = Find(db[m]);
            }
        }

        public int Union(int a, int b)
        {
            int aRoot = Find(a);
            int bRoot = Find(b);
            _size--;
            //abs <
            if (db[aRoot] > db[bRoot])
            {
                db[aRoot] = bRoot;
                return bRoot;
            }
            else if (db[aRoot] < db[bRoot])
            {
                db[bRoot] = aRoot;
                return aRoot;
            }
            else
            {
                db[bRoot] = aRoot;
                db[aRoot]--;
                return aRoot;
            }
        }

    }
}
