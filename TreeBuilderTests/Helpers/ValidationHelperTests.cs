using Microsoft.VisualStudio.TestTools.UnitTesting;
using static TreeBuilder.Helpers.ValidationHelper;
using System.Collections.Generic;

namespace TreeBuilder.Helpers.Tests
{
    [TestClass()]
    public class ValidationHelperTests
    {
        [TestMethod()]
        public void IsCorrectNodeNameTest()
        {
            Assert.AreEqual(true, IsCorrectNodeName("Name"));
            Assert.AreEqual(true, IsCorrectNodeName("NAME"));

            Assert.AreEqual(false, IsCorrectNodeName(string.Empty));
            Assert.AreEqual(false, IsCorrectNodeName("NAME_"));
            Assert.AreEqual(false, IsCorrectNodeName("NA7ME"));
            Assert.AreEqual(false, IsCorrectNodeName("#"));
        }

        [TestMethod()]
        public void IsCorrectChildNameTest()
        {
            Assert.AreEqual(true, IsCorrectChildName("name"));
            Assert.AreEqual(true, IsCorrectChildName("NAME"));
            Assert.AreEqual(true, IsCorrectChildName("#"));

            Assert.AreEqual(false, IsCorrectChildName(string.Empty));
            Assert.AreEqual(false, IsCorrectChildName("NAME_"));
            Assert.AreEqual(false, IsCorrectChildName("NA7ME"));
        }

        [TestMethod()]
        public void AreAllNodeNamesUniqueTest()
        {
            List<string> source = new List<string> { "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            Assert.AreEqual(true, AreAllNodeNamesUnique(source));

            List<string> source_2 = new List<string> { "Fox, The, Lazy", "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            Assert.AreEqual(false, AreAllNodeNamesUnique(source_2));

            List<string> source_3 = new List<string> { "Fox, The, Lazy" };
            Assert.AreEqual(true, AreAllNodeNamesUnique(source_3));
        }

        [TestMethod()]
        public void AreAllBrancheNamesUniqueTest()
        {
            List<string> source = new List<string> { "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            Assert.AreEqual(true, AreAllBrancheNamesUnique(source));

            List<string> source_2 = new List<string> { "Fox, The, Lazy" };
            Assert.AreEqual(true, AreAllBrancheNamesUnique(source_2));

            List<string> source_3 = new List<string> { "Fox, The, Lazy", "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            Assert.AreEqual(false, AreAllBrancheNamesUnique(source_3));

            List<string> source_4 = new List<string> { ",,", "Fox, The, Lazy", "Quick, Fox, Jumps", "Jumps, Dog, #", "Brown, #, Over", "A, Quick, Brown" };
            Assert.AreEqual(false, AreAllBrancheNamesUnique(source_4));
        }

        [TestMethod()]
        public void IsCorrectNumberOfElementsTest()
        {
            Assert.AreEqual(true, IsCorrectNumberOfElements("Fox, The, Lazy"));
            Assert.AreEqual(false, IsCorrectNumberOfElements("Fox, The, Lazy, #"));
            Assert.AreEqual(false, IsCorrectNumberOfElements("Fox, The"));
        }

        [TestMethod()]
        public void AreNodeBranchesValidTest()
        {
            Assert.AreEqual(true, AreNodeBranchesValid("left", "right"));
            Assert.AreEqual(true, AreNodeBranchesValid("left", "#"));
            Assert.AreEqual(true, AreNodeBranchesValid("left", "LEFT"));

            Assert.AreEqual(false, AreNodeBranchesValid("#", "#"));
            Assert.AreEqual(false, AreNodeBranchesValid("left", "right_"));
        }
    }
}