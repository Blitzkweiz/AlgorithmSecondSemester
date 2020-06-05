using System;
using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public static class LocalMinimum
    {
        public static List<int> LocalSearch(AdjacencyGraph graph, out float weigth)
        {
            if (graph.Vertexes.Count <= 3)
            {
                weigth = graph.CountWeight(graph.Vertexes);
                return graph.Vertexes;
            }

            var currentSolution = new List<int>(graph.Vertexes);
            float currentSolutionWeight = graph.CountWeight(currentSolution);

            var bestNeighbor = new List<int>(currentSolution);
            float bestNeighborWeigth = currentSolutionWeight;
            for (int k = 0; k < graph.Vertexes.Count; k++)
            {
                for (int j = k + 2; j < graph.Vertexes.Count + k - 1; j++)
                {
                    var neighbor = new List<int>(currentSolution);
                    var f = neighbor[k];
                    neighbor[k] = neighbor[j % graph.Vertexes.Count];
                    neighbor[j % graph.Vertexes.Count] = f;

                    float neighborWeigth = graph.CountWeight(neighbor);
                    bestNeighborWeigth = graph.CountWeight(bestNeighbor);
                    if (neighborWeigth < bestNeighborWeigth)
                    {
                        bestNeighbor = new List<int>(neighbor);
                        bestNeighborWeigth = neighborWeigth;
                    }

                }
                if (bestNeighborWeigth < currentSolutionWeight)
                {
                    currentSolution = new List<int>(bestNeighbor);
                    currentSolutionWeight = bestNeighborWeigth;
                }
                else
                    break;
            }

            weigth = currentSolutionWeight;
            return currentSolution;
        }
    }
}
