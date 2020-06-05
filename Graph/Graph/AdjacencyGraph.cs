using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class AdjacencyGraph
    {
        public List<int> Vertexes { get; set; }
        public Edge[,] Adjacency { get; set; }
        public AdjacencyGraph(Edge[,] adjacency)
        {
            for (int i = 0; i < adjacency.Length; i++)
            {
                Vertexes[i] = adjacency[i, 0].num;

            }
            Adjacency = adjacency;
        }

        public AdjacencyGraph(float[,] weigths, int numVertex)
        {
            Vertexes = new List<int>();
            Adjacency = new Edge[numVertex, numVertex];
            for (int i = 0; i < numVertex; i++)
            {

                for (int j = 0; j < numVertex; j++)
                {
                    Adjacency[i, j].weight = weigths[i, j];
                    if (weigths[i, j] > 0)
                    {
                        Adjacency[i, j].num = 1;

                    }
                }
            }
            for (int i = 0; i < numVertex; i++)
            {
                Vertexes.Add(i);

            }
        }

        public AdjacencyGraph(List<int> vertex)
        {
            Vertexes = new List<int>(vertex);
            Adjacency = new Edge[vertex.Count, vertex.Count];
            Random random = new Random();
            for (int i = 0; i < vertex.Count; i++)
            {
                Adjacency[i, i].num = 0;
                Adjacency[i, i].weight = int.MaxValue;
                for (int j = i + 1; j < vertex.Count; j++)
                {
                    Adjacency[i, j].num = random.Next(2);
                    Adjacency[i, j].weight = random.Next(1, 15);
                    Adjacency[j, i] = Adjacency[i, j];
                }
            }
        }
        public float CountWeight(List<int> vertex)
        {
            float weight = 0;
            for (int j = 0; j < vertex.Count; j++)
            {
                weight += Adjacency[vertex[j], vertex[(j + 1) % vertex.Count]].weight;
            }
            return weight;
        }
        public struct Edge
        {
            public int num;
            public float weight;
        }
    }
}
