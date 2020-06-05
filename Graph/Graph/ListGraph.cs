using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public class ListGraph
    {
        public List<Node> Nodes { get; set; }

        public ListGraph(List<Node> nodes, char firstNodeName = 'a')
        {
            Nodes = nodes;
            var i = 0;
            foreach(var node in Nodes)
            {
                var chr = (char)((int)firstNodeName + i);
                node.Name = "" + chr;
                node.Number = i;
                i++;
            }
        }

        public Node FindByName(string name)
        {
            return Nodes.Where(x => x.Name == name).First();
        }

        private bool GraphContainsNodes(List<Node> nodes)
        {
            foreach(var vertix in nodes)
            {
                if (!Nodes.Contains(vertix))
                {
                    return false;
                }
            }

            return true;
        }

        public void AddEdge(List<Node> edge)
        {
            if (GraphContainsNodes(edge))
            {
                edge[0].Neighbors.Add(edge[1]);
            }

        }

        public void AddDoubleEdge(List<Node> edge)
        {
            if (GraphContainsNodes(edge))
            {
                edge[0].Neighbors.Add(edge[1]);
                edge[1].Neighbors.Add(edge[0]);
            }
        }

        public List<List<Node>> GetEdges()
        {
            var edges = new List<List<Node>>(); 
            foreach(var node in Nodes)
            {
                foreach(var neighbor in node.Neighbors)
                {
                    edges.Add(new List<Node> { node, neighbor});
                }
            }
            return edges;
        }

        public void RemoveEdge(List<Node> edge)
        {
            if (!GraphContainsNodes(edge))
            {
                if (isAdjacent(edge))
                {
                    foreach (var vertix in edge[0].Neighbors)
                    {
                        if (vertix == edge[1])
                        {
                            edge[0].Neighbors.Remove(edge[1]);
                        }
                    }
                }
            }
        }

        public void RemoveDoubleEdge(List<Node> edge)
        {
            if (GraphContainsNodes(edge))
            {
                if (isAdjacent(edge))
                {
                    foreach (var vertix in edge[0].Neighbors)
                    {
                        if (vertix == edge[1])
                        {
                            edge[0].Neighbors.Remove(edge[1]);
                            edge[1].Neighbors.Remove(edge[0]);
                        }
                    }
                }
            }
        }

        private bool isAdjacent(List<Node> edge)
        {
            if (GraphContainsNodes(edge))
            {
                return edge[1] == edge[0].GetNeighborByName(edge[1].Name);
            }
            return false;
        }

        public void AddVertex()
        {
            Nodes.Add(new Node("" + (char)((int)Nodes[-1].Name.ToCharArray()[0] + 1), Nodes.Count));
        }

        public void RemoveVertex(Node removeNode)
        {
            if (Nodes.Contains(removeNode))
            {
                foreach(var node in Nodes)
                {
                    if (node.Neighbors.Contains(removeNode))
                    {
                        node.Neighbors.Remove(removeNode);
                    }
                }
                Nodes.Remove(removeNode);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is ListGraph graph &&
                   EqualityComparer<List<Node>>.Default.Equals(Nodes, graph.Nodes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nodes);
        }

        public override string ToString()
        {
            string totalString = "";
            foreach (var node in Nodes) 
            {
                totalString += "[ " + node.ToString() + " ] ";
            }
            totalString += " Edges: ";
            foreach (var node in Nodes)
            {
                foreach(var neighbor in node.Neighbors)
                {
                    totalString += "[ " + node.Number + ", " + neighbor.Number + " ] ";
                }
            }
            return "Graph: " + totalString;
        }
    }
}
