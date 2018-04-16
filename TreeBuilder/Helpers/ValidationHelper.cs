using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static TreeBuilder.Helpers.CreationHelper;

namespace TreeBuilder.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsCorrectNodeName(string name)
        {
            //I supposed 'A word is combination of uppercase and lowercase English letters' means string can contain any letters.
            //If name have to start exactly with uppercase use this expression and fix test cases
            // Regex.IsMatch(name, @"^[a-zA-Z]+$") && char.IsUpper(name[0])
            return Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        public static bool AreAllNodeNamesUnique(List<string> stringListFromStream)        //Method validate if all nodes name are unique
        {
            var names = stringListFromStream.Select(x => x.Split(SEPARATOR)[0].Trim());
            return names.Count() == names.Distinct().Count();
        }

        public static bool AreAllBrancheNamesUnique(List<string> stringListFromStream)      //Method validate if all branches name are unique
        {
            var names = stringListFromStream.Select(x => x.Split(SEPARATOR)[1].Trim()).ToList();        //left names
            var rightNames = stringListFromStream.Select(x => x.Split(SEPARATOR)[2].Trim()).ToList();   //right names
            names.AddRange(rightNames);
            names.RemoveAll(x => x == EMPTY_CHILD_NAME);
            return names.Count() == names.Distinct().Count();
        }

        public static bool AreNodeBranchesValid(string left, string right)                  //Branches are invalid if at least one child has incorrect name or both equal '#'
        {
            return (left != EMPTY_CHILD_NAME || right != EMPTY_CHILD_NAME) && IsCorrectChildName(left) && IsCorrectChildName(right);
        }

        public static bool IsCorrectChildName(string name)                                  //The same rule as for node name but value can be equal  EMPTY_CHILD_NAME
        {
            
            return IsCorrectNodeName(name) || name == EMPTY_CHILD_NAME;
        }

        public static bool IsCorrectNumberOfElements(string stringForValidation)          //Method validates: current string has exactly three separated elements
        {
            return stringForValidation.Split(SEPARATOR).Length == 3;
        }
    }
}
