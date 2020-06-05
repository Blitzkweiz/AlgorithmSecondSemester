using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graph;
using System.Collections.Generic;
using System;

namespace GraphTests
{
    [TestClass]
    public class GraphTest
    {

        private static AdjacencyGraph testGraph;

        [ClassInitialize]
        public static void Init(TestContext textContext)
        {
            Random random = new Random(10);
            int vertexNum = 100;
            float[,] weight = new float[vertexNum, vertexNum];
            for (int i = 0; i < vertexNum; i++)
            {
                for (int j = 0; j < vertexNum; j++)
                {
                    if (i == j)
                    {
                        weight[i, j] = int.MaxValue;
                        continue;
                    }
                    weight[i, j] = random.Next(1, 10);
                }
            }
            testGraph = new AdjacencyGraph(weight, vertexNum);

        }

        [TestMethod]
        public void DSATURTest()
        {
            Node vertex0 = new Node();
            Node vertex1 = new Node();
            Node vertex2 = new Node();
            Node vertex3 = new Node();
            Node vertex4 = new Node();

            List<Node> nodes = new List<Node> { vertex0, vertex1, vertex2, vertex3, vertex4 };
            ListGraph graph = new ListGraph(nodes);
            graph.AddDoubleEdge(new List<Node> { vertex0, vertex1 });
            graph.AddDoubleEdge(new List<Node> { vertex1, vertex2 });
            graph.AddDoubleEdge(new List<Node> { vertex1, vertex3 });
            graph.AddDoubleEdge(new List<Node> { vertex3, vertex4 });
            graph.AddDoubleEdge(new List<Node> { vertex0, vertex4 });
            
            Dictionary<int, int> actual = Coloring.DSATUR(graph);

            foreach (var color in actual)
            {
                Console.WriteLine(color.Key + ": " + color.Value);
            }
        }

        [TestMethod]
        public void GISTest()
        {
            Node vertex0 = new Node();
            Node vertex1 = new Node();
            Node vertex2 = new Node();
            Node vertex3 = new Node();
            Node vertex4 = new Node();

            List<Node> nodes = new List<Node> { vertex0, vertex1, vertex2, vertex3, vertex4 };
            ListGraph graph = new ListGraph(nodes);
            graph.AddDoubleEdge(new List<Node> { vertex0, vertex1 });
            graph.AddDoubleEdge(new List<Node> { vertex1, vertex2 });
            graph.AddDoubleEdge(new List<Node> { vertex1, vertex3 });
            graph.AddDoubleEdge(new List<Node> { vertex3, vertex4 });
            graph.AddDoubleEdge(new List<Node> { vertex0, vertex4 });

            Dictionary<int, int> actual = Coloring.GIS(graph);

            foreach (var color in actual)
            {
                Console.WriteLine(color.Key + ": " + color.Value);
            }
        }

        [TestMethod]
        public void LocalSearchTest()
        {
            float weigth;
            var result = LocalMinimum.LocalSearch(testGraph, out weigth);
            var fullCycleWeight = testGraph.CountWeight(testGraph.Vertexes);
            Console.WriteLine("Full cycle weight is " + fullCycleWeight);
            Console.WriteLine("Locmin cycle weight is " + weigth);
            Console.WriteLine("Result cycle vertexies: ");
            for (int i = 0; i < result.Count; i++)
            {
                Console.Write(result[i] + " ");
            }
        }
    }
}
