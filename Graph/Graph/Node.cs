using System;
using System.Collections.Generic;

namespace Graph
{
    public class Node
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public List<Node> Neighbors { get; set; }

        public Node GetNeighborByName(string name)
        {
            foreach(var neighbor in Neighbors)
            {
                if(neighbor.Name == name)
                {
                    return neighbor;
                }
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            return obj is Node node &&
                   Name == node.Name &&
                   Number == node.Number &&
                   EqualityComparer<List<Node>>.Default.Equals(Neighbors, node.Neighbors);
        }

        public Node(string name, int number)
        {
            Name = name;
            Number = number;
            Neighbors = new List<Node>();
        }

        public Node()
        {
            Neighbors = new List<Node>();
        }

        public int GetDegree()
        {
            return Neighbors.Count;
        }

        public override string ToString()
        {
            return "Node: " + Name + " №" + Number;
        }
    }
}
