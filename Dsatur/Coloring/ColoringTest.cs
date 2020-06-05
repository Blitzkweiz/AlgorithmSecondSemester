using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coloring;
using System.Collections.Generic;
using System;

namespace Coloring
{
    [TestClass]
    public class ColoringTest
    {
        private static Dsatur.Coloring.Graph test;
        [ClassInitialize]
        public static void Init(TestContext textContext)
        {
            test = new Coloring.Graph(15);
        }
        
        [TestMethod]
        public void DsaturTest()
        {
            int result = Coloring.DsaturAlg(test);
            Console.Write(result+" ");
        }
    }
}