using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataStructure_Algorithms
{
    interface IConnectedPaths
    {
        bool IsConnected(int point1, int point2);
        void Connect(int point1, int point2);
    }

    /// <summary>
    /// Abstract class used by the three different implementations to connect and find if
    /// two points/paths are connected. Also called union Find problems.
    /// </summary>
    public abstract class ConnectedPaths : IConnectedPaths
    {
        public ConnectedPaths(int numberOfObjects)
        {
            _objects = new int[numberOfObjects];
        }


        public abstract bool IsConnected(int point1, int point2);

        public abstract void Connect(int point1, int point2);

        protected void Validate(int point)
        {
            if (point < 0 || point > _objects.Length)
                throw new IndexOutOfRangeException(nameof(point) + " is not a valid index");
        }

        protected void Initialize()
        {
            for (int i = 0; i < _objects.Length; i++)
                _objects[i] = i;
        }

        protected readonly int[] _objects;
    }

    /// <summary>
    /// Connection between two points (direct or indirect)
    /// One of the ways to do it is by array. The array indices represents the 
    /// object and the values in the indices will initially be same as the indices.
    /// When we need to connect two points then change the value of the indices based on the point it 
    /// needs to connect to.
    /// Finding is quick here but connecting is slow.
    /// </summary>
    class ConnectedPathsQuickFind : ConnectedPaths
    {
        public ConnectedPathsQuickFind(int numberOfObjects) : base(numberOfObjects)
        {
            Initialize();
        }

        /// <summary>
        /// This is O(1). If the value of the indices are the same then they are connected.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public override bool IsConnected(int point1, int point2)
        {
            Validate(point1);
            Validate(point2);
            return _objects[point1] == _objects[point2];
        }

        /// <summary>
        /// Basically take the value of point2 index and set that value to the indices
        /// which holds the value same as the value at point1 index
        /// This is O(N).
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public override void Connect(int point1, int point2)
        {
            Validate(point1);
            Validate(point2);
            if (IsConnected(point1, point2))
                return;
            int previousValue = _objects[point1];
            int newValue = _objects[point2];
            for (int i = 0; i < _objects.Length; i++)
            {
                if (_objects[i] == previousValue)
                    _objects[i] = newValue;
            }
        }
    }

    /// <summary>
    /// This is another implementation for connected and find paths solution.
    /// Here we are creating a tree structure. Whenever we need to connect two points
    /// we make the root of first point to point to point2's root.
    /// </summary>
    class ConnectedPathsQuickConnect : ConnectedPaths
    {
        public ConnectedPathsQuickConnect(int numberOfObjects) : base(numberOfObjects)
        {
            Initialize();
        }

        /// <summary>
        /// Find the roots and then get the answer.
        /// This could end up being O(N) if the tree becomes tall and skinny.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public override bool IsConnected(int point1, int point2)
        {
            return GetRoot(point1) == GetRoot(point2);
        }

        /// <summary>
        /// Find the roots and make the root of point1 point to root of Point2.
        /// This could be O(N) while finding roots.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public override void Connect(int point1, int point2)
        {
            Validate(point1);
            Validate(point2);
            int rootP1 = GetRoot(point1);
            int rootP2 = GetRoot(point2);
            _objects[rootP1] = rootP2;

        }

        protected virtual int GetRoot(int point)
        {
            while (point != _objects[point])
                point = _objects[point];
            return point;
        }

    }

    /// <summary>
    /// This is an improvement over ConnectedPathsQuickConnect where in the trees are connected such that
    /// the tree with less children are connected to tree with more children. This way the tree do not become
    /// tall and skinny. This kind of connection is called weighted connection.
    /// </summary>
    class ConnectedPathsQuickConnectWeighted : ConnectedPathsQuickConnect
    {
        public ConnectedPathsQuickConnectWeighted(int numberOfObjects) : base(numberOfObjects)
        {
            // Now we need to keep track of the number of items in the tree so that
            // smaller tree is connected to larger tree.
            _sizeOfTree = new int[numberOfObjects];
            for (int i = 0; i < _objects.Length; i++)
            {
                _objects[i] = i;
                _sizeOfTree[i] = 1; // Initially all are its own roots. So the size is 1.
            }
        }

        /// <summary>
        /// Because of weighted connection, the depth of any node is log base 2 (n)
        /// So finding the root will take ln(n).
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public override void Connect(int point1, int point2)
        {
            int rootP1 = GetRoot(point1);
            int rootP2 = GetRoot(point2);
            if (_sizeOfTree[rootP1] >= _sizeOfTree[rootP2])
            {
                _objects[rootP2] = rootP1;
                _sizeOfTree[rootP1] += _sizeOfTree[rootP2];
            }
            else
            {
                _objects[rootP1] = rootP2;
                _sizeOfTree[rootP2] += _sizeOfTree[rootP1];
            }
        }

        private readonly int[] _sizeOfTree;
    }

    /// <summary>
    /// This is an improvement over ConnectedPathsQuickConnectWeighted where in all the trees with the common top
    /// root are made to point to the root directly so that the tree is well balanced and the depth of the tree
    /// is reduced.
    /// </summary>
    class ConnectedPathsQuickConnectWeightedPathCompression : ConnectedPathsQuickConnectWeighted
    {
        public ConnectedPathsQuickConnectWeightedPathCompression(int numberOfObjects) : base(numberOfObjects)
        {
        }

        protected override int GetRoot(int point)
        {
            while (point != _objects[point])
            {
                _objects[point] = _objects[_objects[point]];
                point = _objects[point];

            }
            return point;
        }
    }

}