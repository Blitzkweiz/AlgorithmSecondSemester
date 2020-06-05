using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public static class Coloring
    {
        public static Node GetVertexToColor(List<Node> notColoredVertices, Dictionary<int, List<int>> adjacentVerticesColors)
        {
            var vertexToColor = notColoredVertices.First();
            foreach(var vertex in notColoredVertices)
            {
                var vertexLength = adjacentVerticesColors[vertex.Number].Count;
                var vertexToColorLength = adjacentVerticesColors[vertexToColor.Number].Count;
                if(vertexLength > vertexToColorLength || (vertexLength == vertexToColorLength && vertex.GetDegree() > vertexToColor.GetDegree()))
                {
                    vertexToColor = vertex;
                }
            }

            return vertexToColor;
        }

        public static Dictionary<int, int> DSATUR(ListGraph graph)
        {
            var verticesColors = new Dictionary<int, int>();
            var notColoredVertices = graph.Nodes;
            var adjacentVerticesColors = Enumerable.Range(0, notColoredVertices.Count).ToDictionary(k => k, v => new List<int>());
            var usedColors = 1;

            while (notColoredVertices.Count != 0)
            {
                var vertexToColor = GetVertexToColor(notColoredVertices, adjacentVerticesColors);
                var availableColors = new HashSet<int>(Enumerable.Range(0, usedColors).Where(x => !adjacentVerticesColors[vertexToColor.Number].Contains(x)));

                var color = availableColors.Count != 0 ? availableColors.Min() : usedColors;
                usedColors = availableColors.Count != 0 ? usedColors : usedColors + 1;

                verticesColors[vertexToColor.Number] = color;
                notColoredVertices.Remove(vertexToColor);

                foreach (var neighbor in vertexToColor.Neighbors)
                {
                    adjacentVerticesColors[neighbor.Number].Add(color);
                }
            }

            return verticesColors;
        }

        public static List<Node> GetVerticesToColor(List<Node> notColoredVertices, Dictionary<int, List<int>> adjacentVerticesColors, int color)
        {
            var vertices = new List<Node>();
            foreach (var vertex in notColoredVertices)
            {
                if (!adjacentVerticesColors[vertex.Number].Contains(color))
                {
                    vertices.Add(vertex);
                }
            }
            return vertices;
        }

        public static Dictionary<int, int> GIS(ListGraph graph)
        {
            var verticesColors = new Dictionary<int, int>();
            var notColoredVertices = graph.Nodes;
            var adjacentVerticesColors = Enumerable.Range(0, notColoredVertices.Count).ToDictionary(k => k, v => new List<int>());
            var color = 1;

            while (notColoredVertices.Count != 0)
            {
                var available = GetVerticesToColor(notColoredVertices, adjacentVerticesColors, color);
                var availableDegrees = Enumerable.Range(0, available.Count).ToDictionary(k => available[k], v => graph.Nodes[v].GetDegree());

                while (available.Count != 0)
                {
                    var minimum = availableDegrees.Aggregate((l, r) => l.Value < r.Value ? l : r);
                    var minVertex = minimum.Key;
                    var degree = minimum.Value;

                    if (!adjacentVerticesColors[minVertex.Number].Contains(color))
                    {
                        verticesColors[minVertex.Number] = color;
                        notColoredVertices.Remove(minVertex);

                        foreach (var neighbor in minVertex.Neighbors)
                        {
                            adjacentVerticesColors[neighbor.Number].Add(color);
                            if (available.Contains(neighbor))
                            {
                                available.Remove(neighbor);
                                availableDegrees.Remove(neighbor);
                                foreach(var neig in neighbor.Neighbors)
                                {
                                    availableDegrees[neig]--;
                                }
                            }
                            graph.RemoveDoubleEdge(new List<Node>() { neighbor, minVertex});
                        }
                    }
                    availableDegrees.Remove(minVertex);
                    available.Remove(minVertex);
                }

                color++;
            }

            return verticesColors;
        }
    }
}
