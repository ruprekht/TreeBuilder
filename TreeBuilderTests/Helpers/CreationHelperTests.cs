using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static TreeBuilder.Helpers.CreationHelper;
using System.IO;

namespace TreeBuilder.Helpers.Tests
{
    [TestClass()]
    public class CreationHelperTests
    {
        const string CORRECT_SOURCE_FILE = @"\TestFiles\correctSource.txt";
        const string CORRECT_SOURCE_SINGLENODE_FILE = @"\TestFiles\correctSourceSingleNode.txt";
        const string INCORRECT_SOURCE_CHILDLESS_FILE = @"\TestFiles\incorrectSourceСhildless.txt";
        const string INCORRECT_SOURCE_EMPTY_FILE = @"\TestFiles\incorrectSourceEmpty.txt";

        [TestMethod()]
        public void BuildTreeTest_Success()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + CORRECT_SOURCE_FILE;
            Tree tree;
            using (StreamReader reader = new StreamReader(filePath))
            {
                tree = BuildTree(reader);
            }
            string expected = "The";
            string result = tree.Left.Left.Left.Name;
            Assert.AreEqual(expected, result, $"Expexted result '{expected}', actual result '{result}'");
        }

        [TestMethod()]
        public void BuildTreeTest_SingleNode_Success()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + CORRECT_SOURCE_SINGLENODE_FILE;
            Tree tree;
            using (StreamReader reader = new StreamReader(filePath))
            {
                tree = BuildTree(reader);
            }
            string expected = "Quick";
            string result = tree.Left.Name;
            Assert.AreEqual(expected, result, $"Expexted result '{expected}', actual result '{result}'");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public void BuildTreeTest_Childless_Failed()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + INCORRECT_SOURCE_CHILDLESS_FILE;
            Tree tree;
            using (StreamReader reader = new StreamReader(filePath))
            {
                tree = BuildTree(reader);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public void BuildTreeTest_Empty_Failed()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + INCORRECT_SOURCE_EMPTY_FILE;
            Tree tree;
            using (StreamReader reader = new StreamReader(filePath))
            {
                tree = BuildTree(reader);
            }
        }

        [TestMethod()]
        public void GetRootNodeTest_Success()
        {
            List<string> source = new List<string> { "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            string result = GetRootNode(source);
            string expected = "A";
            Assert.AreEqual(expected, result, $"Expexted result '{expected}', actual result '{result}'");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public void GetRootNodeTest_Failed()
        {
            List<string> source = new List<string> { "A, Quick, Brown", "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            string result = GetRootNode(source);
        }

        [TestMethod()]
        public void PrepareDictionatyForTreeTest_Success()
        {
            List<string> source = new List<string> { "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            var result = PrepareDictionatyForTree(source);
            int expected = 5;
            Assert.AreEqual(expected, result.Count, $"Expexted result '{expected}', actual result '{result.Count}'");
            expected = 2;
            foreach (var pair in result)
            {
                Assert.AreEqual(expected, pair.Value.Length, $"Expexted result '{expected}', actual result '{pair.Value.Length}'");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidDataException))]
        public void PrepareDictionatyForTreeTest_Failed()
        {
            List<string> source = new List<string> { "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #, 14", "Brown, #, Over", "A, Quick, Brown" };
            var result = PrepareDictionatyForTree(source);
        }
    }
}