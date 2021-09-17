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
            XmlUtil.Statistics(@"C:\Users\Wynn\Desktop\book\story_0036\cocosstudio\scenes\story\0036\content\layout\page_1\p01.csd", "in/out.xml");
            XmlUtil.Statistics(@"C:\Users\Wynn\Desktop\book\story_0036\cocosstudio\scenes\story\0036\content\layout\page_1\s0036_h001_konglong_0.csi", "in/out.xml", true);


            Console.ReadKey();
        }
    }
}
