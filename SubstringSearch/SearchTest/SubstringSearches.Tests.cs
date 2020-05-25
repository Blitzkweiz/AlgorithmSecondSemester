using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubstringSearch;
using System;
using System.Collections.Generic;
using System.IO;

namespace SearchTest
{
    [TestClass]
    public class SubstringSearchesTests
    {
        private static string pattern;
        private static string text;

        [ClassInitialize]
        public static void Initialiation(TestContext testContext)
        {
            text = File.ReadAllText(@"C:\Users\Blitzkweiz_Ithore\AlgorithmSecondSemester\SubstringSearch\SearchTest\Source\war-peace.txt");
            pattern = "death";
        }

        [TestMethod]
        public void RabinKarpTest()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("RK:  " + SubstringSearches.RabinKarp(text, pattern).Count);
            DateTime end = DateTime.Now;
            Console.WriteLine("Time: " + (end - start).TotalMilliseconds);
        }

        [TestMethod]
        public void KnuthMorrisPrattTest()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("KMP: " + SubstringSearches.KnuthMorrisPratt(text, pattern).Count);
            DateTime end = DateTime.Now;
            Console.WriteLine("Time: " + (end - start).TotalMilliseconds);
        }

        [TestMethod]
        public void BoyerMooreTest()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("BM:  " + SubstringSearches.BoyerMoore(text, pattern).Count);
            DateTime end = DateTime.Now;
            Console.WriteLine("Time: " + (end - start).TotalMilliseconds);
        }
    }
}
