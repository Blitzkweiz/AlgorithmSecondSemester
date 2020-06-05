using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Packing.Tests
{
    [TestClass]
    public class PackingTaskTests
    {
        private static double[] weights;
        public static readonly int n = 1000;

        [ClassInitialize]
        public static void Initialization(TestContext testContext)
        {
            Random random = new Random();
            weights = Enumerable.Repeat(0, n).Select(i => random.NextDouble()).ToArray();
        }
        
        [TestMethod]
        public void NextFitTest()
        {
            DateTime start = DateTime.Now;
            var actual = PackingTask.NextFit(weights).Where(x => x.Count > 0).ToArray();
            DateTime end = DateTime.Now;

            Console.WriteLine("TIME: " + (end - start).TotalMilliseconds);
            Console.WriteLine("Containers number :" + actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.IsTrue(actual[i].Sum(x => x) < 1);
                Console.WriteLine(i + " container capacity is " + actual[i].Sum(x => x));
            }
        }

        [TestMethod]
        public void FirstFitTest()
        {
            DateTime start = DateTime.Now;
            var actual = PackingTask.FirstFit(weights).Where(x => x.Count > 0).ToArray();
            DateTime end = DateTime.Now;

            Console.WriteLine("TIME: " + (end - start).TotalMilliseconds);
            Console.WriteLine("Containers number :" + actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.IsTrue(actual[i].Sum(x => x) < 1);
                Console.WriteLine(i + " container capacity is " + actual[i].Sum(x => x));
            }
        }

        [TestMethod]
        public void OrderedFirstFitTest()
        {
            DateTime start = DateTime.Now;
            var actual = PackingTask.OrderedFirstFit(weights).Where(x => x.Count > 0).ToArray();
            DateTime end = DateTime.Now;

            Console.WriteLine("TIME: " + (end - start).TotalMilliseconds);

            Console.WriteLine("Containers number :" + actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.IsTrue(actual[i].Sum(x => x) < 1);
                Console.WriteLine(i + " container capacity is " + actual[i].Sum(x => x));
            }
        }

        [TestMethod]
        public void BestFitTest()
        {
            DateTime start = DateTime.Now;
            var actual = PackingTask.BestFit(weights).Where(x => x.Count > 0).ToArray();
            DateTime end = DateTime.Now;

            Console.WriteLine("TIME: " + (end - start).TotalMilliseconds);

            Console.WriteLine("Containers number :" + actual.Length);
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.IsTrue(actual[i].Sum(x => x) < 1);
                Console.WriteLine(i + " container capacity is " + actual[i].Sum(x => x));
            }
        }
    }
}
