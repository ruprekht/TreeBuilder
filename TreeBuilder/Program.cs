using System;
using System.IO;
using static TreeBuilder.Helpers.CreationHelper;

namespace TreeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"\SourceFiles\initialSource.txt";
            Tree result;
            using (StreamReader reader = new StreamReader(filePath))
            {
                result = BuildTree(reader);
            }

        }
    }
}
