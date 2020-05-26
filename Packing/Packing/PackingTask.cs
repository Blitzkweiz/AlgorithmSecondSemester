using System;
using System.Collections.Generic;
using System.Linq;

namespace Packing
{
    public static class PackingTask
    {
        public static List<double>[] NextFit(double[] weights, int c = 1)
        {
            var currentContainer = 0;
            List<List<double>> containers = new List<List<double>>();
            containers.Add(new List<double>());
            foreach(double weight in weights)
            {
                if (containers[currentContainer].Sum(x => x) + weight > c)
                {
                    currentContainer += 1;
                    containers.Add(new List<double>());
                }
                containers[currentContainer].Add(weight);
            }
            return containers.ToArray();
        }

        public static List<double>[] FirstFit(double[] weights, int c = 1)
        {
            //List<double>[] containers = new List<double>[weights.Length];
            List<List<double>> containers = new List<List<double>>();
            containers.Add(new List<double>());
            var i = 0;
            var j = 0;

            while(i < weights.Length)
            {
                while(j < weights.Length)
                {
                    if (containers[j].Sum(x => x) + weights[i] <= c)
                    {
                        containers[j].Add(weights[i]);
                        i++;
                        j--;
                        if(i == weights.Length)
                        {
                            break;
                        }
                    }
                    j++;
                }
            }

            return containers.ToArray();
        }

        public static List<double>[] OrderedFirstFit(double[] weights, int c = 1)
        {
            List<double>[] containers = new List<double>[weights.Length];
            return containers;
        }

        public static List<double>[] BestFit(double[] weights, int c = 1)
        {
            List<double>[] containers = new List<double>[weights.Length];
            var i = 0;
            var j = 0;
            while(i < weights.Length)
            {
                while(j < containers.Length)
                {
                    var freeSpaces = new List<double>();
                    foreach(var container in containers)
                    {
                        freeSpaces.Add(c - weights[i] - container.Sum(x => x));
                    }
                    for(var k = 0; k < freeSpaces.Count; k++)
                    {
                        if(freeSpaces[k] < 0)
                        {
                            freeSpaces[k] = c + 0.01;
                        }
                    }
                    containers[freeSpaces.IndexOf(freeSpaces.Min())].Add(weights[i]);
                    i++;
                    j--;
                    if(i == weights.Length)
                    {
                        break;
                    }
                }
                i++;
            }
            return containers;
        }
    }
}
