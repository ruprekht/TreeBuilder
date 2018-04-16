using System.Collections.Generic;
using System.IO;
using static TreeBuilder.Helpers.ValidationHelper;
using static TreeBuilder.Helpers.CreationHelper;

namespace TreeBuilder
{
    public class Tree
    {
        public Tree Left { get; private set; }
        public Tree Right { get; private set; }
        public string Name { get; private set; }

        public Tree(string nodeName, Dictionary<string, string[]> data)
        {
            if (!IsCorrectNodeName(nodeName))
                throw new InvalidDataException($"Node has incorrect name {nodeName}");

            Name = nodeName;

            if (data.ContainsKey(nodeName))
            {
                if (!AreNodeBranchesValid(data[nodeName][0], data[nodeName][1]))
                    throw new InvalidDataException($"Node {nodeName} has invalid branches");

                if (data[nodeName][0] != EMPTY_CHILD_NAME)
                    Left = new Tree(data[nodeName][0], data);
                if (data[nodeName][1] != EMPTY_CHILD_NAME)
                    Right = new Tree(data[nodeName][1], data);
            }
        }

    }
}
