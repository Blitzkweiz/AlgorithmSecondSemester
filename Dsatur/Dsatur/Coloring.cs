using System;
using System.Collections.Generic;
using System.Linq;
namespace Dsatur
{
    public class Coloring
    {
        public class Graph
        {
            public int vertexNum;
            public int[,] adjacency;

            public Graph(int[,] adjacency)
            {
                this.vertexNum = adjacency.Length;
                this.adjacency = adjacency;
            }

            public Graph(int vertexNum)
            {
                this.vertexNum = vertexNum;
                adjacency = new int[vertexNum, vertexNum];
                Random random = new Random();
                for (int i = 0; i < vertexNum; i++)
                {
                    adjacency[i, i] = 0;
                    for (int j = i + 1; j < vertexNum; j++)
                    {
                        adjacency[i, j] = random.Next(2);
                        adjacency[j, i] = adjacency[i, j];
                    }
                }
            }



            public void printAdjacencyMatrix()
            {
                Console.WriteLine("Adjacency matrix:");
                for (int i = 0; i < vertexNum; i++)
                {
                    for (int j = 0; j < vertexNum; j++)
                    {
                        Console.WriteLine(adjacency[i,j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        // 0 0 1 0 1
        // 0 0 1 1 1    
        // 1 1 0 0 0
        // 0 1 0 0 0
        // 1 1 0 0 0       


        public void GIS(Graph graph)
        {
            int coloursNum = 1;
            int[] vertexColours = new int[graph.vertexNum];
            int[] availableVertex = new int[graph.vertexNum];
            int c = 1, p;
            int k = 1;
            while (k != 0)
            {
                availableVertex = GetUncoloredVertexsWithMinimalNeghbors(graph, vertexColours, out p);
                if (p == -1)
                    break;
                for(int i = 0; i < p; i++)
                {
                    vertexColours[availableVertex[i]] = c;
                }
                c++;
            }
            
        }
        public bool IsNeiborTheSameColor(Graph graph, int i,int color)
        {
            for (int j = 0; j < graph.vertexNum; j++)
                if (graph.adjacency[j, i] == color)
                    return true;
            return false;
        }
        
        public int[] GetUncoloredVertexsWithMinimalNeghbors(Graph graph,int[] vertexColours, out int p)
        {
            int currentDeg, minDeg = graph.vertexNum;
            int[] availableVertex = new int[graph.vertexNum];
            p = -1;
            for (int i = 0; i < graph.vertexNum; i++)
            {
                if (vertexColours[i] != 0)
                    continue;
                currentDeg = 0;
                for (int j = 0; j < graph.vertexNum; j++)
                {
                    currentDeg += graph.adjacency[i, j];
                }
                
                if (currentDeg < minDeg)
                {
                    p = 0;
                    minDeg = currentDeg;
                    availableVertex[p] = i;
                    p++;
                }
                else if (currentDeg == minDeg)
                     {
                    availableVertex[p] = i;
                    p++;
                     }
            }
            return availableVertex;
        }
        //public int[] GetAvailableVertexs(Graph graph,int[] vertexColours)
        //{
        //    int[] availableVertex = new int[graph.vertexNum];
        //    int k = 0;
        //    for(int i = 0; i < graph.vertexNum; i++)
        //    {
        //        if (vertexColours[i] == 0)
        //        {
        //            availableVertex[k] = i;
        //            k++;
        //        }
                
        //    }
        //    return availableVertex; 
        //}


        public static int DsaturAlg(Graph graph)
        {
            int coloursNum = 0;
            int[] vertexColours = new int[graph.vertexNum];
            int[] saturation = new int[graph.vertexNum];

            int vertexWithMaxDegree = getVertexWithMaxDegree(graph);
          
            coloursNum++;
            
            for (int i = 0; i < graph.vertexNum; i++)
            {
                if (graph.adjacency[vertexWithMaxDegree,i] == 1)
                    saturation[i] = 1;
            }

            int currentVertex = getUncolouredVertexWithMaxSaturation(graph, saturation, vertexColours);
            while (currentVertex != -1)
            {
                int colour = chooseColourForVertex(graph, currentVertex, vertexColours);
                vertexColours[currentVertex] = colour;
                if (colour > coloursNum)
                    coloursNum++;
                changeSaturation(graph, currentVertex, vertexColours, saturation);
                currentVertex = getUncolouredVertexWithMaxSaturation(graph, saturation, vertexColours);
            }
            
            printVertexColours(graph, vertexColours);
            return coloursNum;
        }

        public static int getVertexWithMaxDegree(Graph graph)
        {
            int vertexWithMaxDegree = 0;
            int maxDeg = 0, currentDeg;
            for (int i = 0; i < graph.vertexNum; i++)
            {
                currentDeg = 0;
                for (int j = 0; j < graph.vertexNum; j++)
                {
                    currentDeg += graph.adjacency[i,j];
                }
                if (currentDeg > maxDeg)
                {
                    maxDeg = currentDeg;
                    vertexWithMaxDegree = i;
                }
            }
            return vertexWithMaxDegree;
        }


        public static int getUncolouredVertexWithMaxSaturation(Graph graph, int[] saturation, int[] vertexColours)
        {
            int vertex = -1;
            int maxSaturation = 0;
            for (int i = 0; i < graph.vertexNum; i++)
            {
                if (vertexColours[i] == 0 && saturation[i] >= maxSaturation)
                {
                    maxSaturation = saturation[i];
                    vertex = i;
                }
            }
            return vertex;
        }
        public static int chooseColourForVertex(Graph graph, int vertex, int[] vertexColours)
        {
            int colour = 1;
            bool canUseCurrentColour = false;
            while (!canUseCurrentColour)
            {
                for (int i = 0; i < graph.vertexNum; i++)
                {
                    if (graph.adjacency[vertex,i] == 1 && vertexColours[i] == colour)
                    {
                        colour++;
                        break;
                    }
                    if (i == graph.vertexNum - 1)
                        canUseCurrentColour = true;
                }
            }
            return colour;
        }
        public static void changeSaturation(Graph graph, int vertex, int[] vertexColours, int[] saturation)
        {
            for (int i = 0; i < graph.vertexNum; i++)
            {
                if (graph.adjacency[vertex,i] == 1)
                {
                    for (int j = 0; j < graph.vertexNum; j++)
                    {
                        if (j != vertex && graph.adjacency[i,j] == 1 && vertexColours[j] == vertexColours[vertex])
                            break;
                        if (j == graph.vertexNum - 1)
                            saturation[i]++;
                    }

                }
            }
        }

        public static void printVertexColours(Graph graph, int[] vertexColours)
        {
            Console.Write("Colours: ");
            for (int i = 0; i < graph.vertexNum; i++)
                Console.Write(vertexColours[i] + " ");
            Console.WriteLine();
        }
    }
}
