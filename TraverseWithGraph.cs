using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public class TraverseWithGraph
    {
        public TraverseWithGraph()
        {
        }
    }


    public static class Mazes
    {
        // Collection of arrays to be used with Graph

        static int[,,] mazeSingleLevel = new int[1, 20, 20]
        {{
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,8,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 }
        } };
        static int[,,] maze3d = new int[2, 20, 20]
        {
            {
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            },

            {
                {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0 },
                {0,1,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0 },
                {0,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,0,1,0 },
                {0,1,0,1,0,0,1,0,0,1,0,0,1,0,1,0,1,0,0,0 },
                {0,0,0,1,0,0,1,0,0,1,0,0,1,0,1,0,1,1,1,0 },
                {0,0,0,1,0,0,1,0,0,1,1,1,1,0,1,0,0,0,1,0 },
                {0,0,0,1,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0 },
                {0,0,0,1,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0 },
                {0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0 },
                {0,0,0,1,1,0,1,1,1,1,0,1,0,0,0,0,1,0,0,0 },
                {0,0,0,1,0,0,0,0,0,1,1,1,1,1,1,0,1,0,0,0 },
                {0,0,0,1,0,0,0,0,0,1,0,1,0,0,1,0,1,0,0,0 },
                {0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,1,0,0,0 },
                {0,0,0,1,1,1,1,1,0,8,1,1,1,0,0,0,1,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            }
        };

        public static int[,,] MazeSingleLevel => mazeSingleLevel;
        public static int[,,] Maze3D => maze3d;
    }

    public class Edge
    {
        private List<string> steps = new List<string>();

        public void AddStep(string step) => steps.Add(step);

        public override string ToString() => string.Join("", steps);

        public string Path() => this.ToString();
    }

    public class Node
    {
        private string id;
        private string value;
        private IDictionary<string, string> connections = new Dictionary<string, string>();

        public Node(string key, string value)
        {
            id = key;
            this.value = value ?? "";
        }

        public void AddConnection(string key, string edge) => connections[key] = edge;

        public IDictionary<string, string> GetConnections() => connections;

        public string GetId() => id;
        public string GetValue() => value;
    }

    public class Graph 
    {
        private IDictionary<string, Node> nodes = new Dictionary<string, Node>();

        public void AddNode(string key, string value = "")
        {
            if (!nodes.ContainsKey(key))
            {
                var node = new Node(key, value);
                nodes.Add(key, node);
            }
        }

        public Node GetNode(string key) => nodes.ContainsKey(key) ? nodes[key] : null;

        public void AddEdge(string fromNode, string toNode, string edge)
        {
            if (!nodes.ContainsKey(fromNode)) AddNode(fromNode);
            if (!nodes.ContainsKey(toNode)) AddNode(toNode);
            nodes[fromNode].AddConnection(toNode, edge);
        }

        public IList<string> GetNodes() => new List<string>(nodes.Keys);

        public bool Contains(string key) => nodes.Keys.Contains(key);

        public IDictionary<string, Node> GetAllNodes() => nodes;

        // ToJson was not implemented following YAGNE principle. We may get to it in the next video
    }

    public class TraverseMap3D
    {
        private int[,,] data;
        private Graph graph = new Graph();
        private int startLevel;
        private int startRow;
        private int startCol;

        public TraverseMap3D(int level = 0, int row = 0, int col = 0)
        {
            startLevel = level;
            startRow = row;
            startCol = col;
        }

        public Graph Traverse(int[,,] mapData)
        {
            data = mapData;
            var startValue = data[startLevel, startRow, startCol] == 8 ? "G" : "B";
            var startKey = $"{startLevel}-{startRow}-{startCol}";
            graph.AddNode(startKey, startValue);
            var node = graph.GetNode(startKey);
            var edge = new Edge();
            var point = new ValueTuple<int, int, int, string>(startLevel, startRow, startCol, startValue);
            Traverse(point, edge, node);
            return graph;
        }

        private void Traverse(ValueTuple<int, int, int, string> point, Edge currentEdge, Node fromNode)
        {
            var (level, row, col, direction) = point;
            var currentKey = $"{level}-{row}-{col}";
            var fromKey = fromNode.GetId();

            var availablePoints = GetChildPoints(level, row, col);
            var childPoints = availablePoints.Where(p => p.Item4 != ReversePath(direction)).ToList();

            void _markSpot()
            {
                if (fromKey == currentKey) return;
                var currentPath = currentEdge.Path();
                graph.AddEdge(fromKey, currentKey, currentPath);
                graph.AddEdge(currentKey, fromKey, ReversePath(currentPath));
            };

            if (graph.Contains(currentKey))
            {
                _markSpot();
                if (direction != "B") return;
            }

            if(data[level, row, col] == 8)
            {
                graph.AddNode(currentKey, "G");
                _markSpot();
                return;
            }

            if(childPoints.Count == 0)
            {
                graph.AddNode(currentKey, "DE");
                _markSpot();
                return;
            }
            else if(childPoints.Count == 1)
            {
                var singlePoint = childPoints.First();
                var (_, _, _, currentDirection) = singlePoint;
                currentEdge.AddStep(currentDirection);
                Traverse(singlePoint, currentEdge, fromNode);
            }
            else
            {
                _markSpot();

                foreach (var pt in childPoints)
                {
                    var childEdge = new Edge();
                    childEdge.AddStep(pt.Item4);
                    Traverse(pt, childEdge, graph.GetNode(currentKey));
                }
            }
        }

        private bool IsOpen(int level, int row, int col) =>
            row >= 0 && col >= 0 && level >= 0 &&
            level < data.GetLength(0) &&
            row < data.GetLength(1) &&
            col < data.GetLength(2) &&
            data[level, row, col] != 0;

        private IList<ValueTuple<int, int, int, string>> GetChildPoints(int level, int row, int col)
        {
            var childPoints = new List<ValueTuple<int, int, int, string>>();

            if (IsOpen(level, row - 1, col)) childPoints.Add(new(level, row - 1, col, "N"));
            if (IsOpen(level, row + 1, col)) childPoints.Add(new(level, row + 1, col, "S"));
            if (IsOpen(level, row, col + 1)) childPoints.Add(new(level, row, col + 1, "E"));
            if (IsOpen(level, row, col - 1)) childPoints.Add(new(level, row, col - 1, "W"));
            if (IsOpen(level - 1, row, col)) childPoints.Add(new(level - 1, row, col, "U"));
            if (IsOpen(level + 1, row, col)) childPoints.Add(new(level + 1, row, col, "D"));

            return childPoints;
        }

        private string ReverseOf(string direction)
        {
            if (direction == "U") return "D";
            if (direction == "D") return "U";
            if (direction == "N") return "S";
            if (direction == "S") return "N";
            if (direction == "E") return "W";
            if (direction == "W") return "E";
            return direction;
        }

        private string ReversePath(string path)
        {
            var lst = path.Select(l => ReverseOf(l.ToString())).ToList();
            lst.Reverse();
            return string.Join("", lst);
        }
    }
}
