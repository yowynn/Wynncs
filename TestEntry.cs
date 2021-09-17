using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wynncs
{
    public class TestEntry
    {
        public static void Main()
        {
            //Console.WriteLine(Path.GetExtension("a/b/c/v"));
            Console.WriteLine(FileSystem.GetFolderPath("a/b"));
            Console.ReadKey();
        }
    }
}
