using System.Collections.Generic;
using System.IO;
using System.Linq;
using static TreeBuilder.Helpers.ValidationHelper;

namespace TreeBuilder.Helpers
{
    public static class CreationHelper
    {
        public const char SEPARATOR = ',';
        public const string EMPTY_CHILD_NAME = "#";

        public static Tree BuildTree(StreamReader reader)
        {
            var stringListFromStream = new List<string>();

            while (reader.Peek() >= 0)
            {
                stringListFromStream.Add(reader.ReadLine());
            }

            if (stringListFromStream.Count < 1)                                     //Checking for non empty source of data
                throw new InvalidDataException("Source hasn't any strings");

            string root = GetRootNode(stringListFromStream);
            var data = PrepareDictionatyForTree(stringListFromStream);

            return new Tree(root, data);
        }

        public static string GetRootNode(List<string> stringListFromStream)        //Method should select the unique value from node names. Root name shouldn't be found among left and right branches
        {
            if (!AreAllNodeNamesUnique(stringListFromStream))
                throw new InvalidDataException("Node names are not unique");
            if (!AreAllBrancheNamesUnique(stringListFromStream))
                throw new InvalidDataException("Branch names are not unique");

            var root = new List<string>();
            try
            {
                root = stringListFromStream.Select(x => x.Split(SEPARATOR)[0].Trim()).
                                            Where(r => stringListFromStream.Select(source => source.Split(SEPARATOR)[1].Trim()).Where(left => left.Contains(r)).Count() == 0
                                                    && stringListFromStream.Select(source => source.Split(SEPARATOR)[2].Trim()).Where(right => right.Contains(r)).Count() == 0).ToList();
            }
            catch
            {
                throw new InvalidDataException("Can't define root node");
            }

            return root[0];
        }

        public static Dictionary<string, string[]> PrepareDictionatyForTree(List<string> stringListFromStream)        //Returns dict: Key - node name, value - names of left and right branches
        {
            var data = new Dictionary<string, string[]>();

            foreach (var item in stringListFromStream)
            {
                if (!IsCorrectNumberOfElements(item))
                    throw new InvalidDataException($"Current string {item} is incorrect. Can't separate this into parent, left and right");

                var node = item.Split(SEPARATOR);
                data.Add(node[0].Trim(), new string[] { node[1].Trim(), node[2].Trim() });
            }

            return data;
        }
    }
}
