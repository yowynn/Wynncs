using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wynncs.Util;
using Wynncs.Entry;

namespace Wynncs
{
    public class TestEntry
    {
        public static void Main()
        {
            //Console.WriteLine(Path.GetExtension("a/b/c/v"));
            var count = 100;
            var dis = 1f / count;
            CubicBezier b = CubicBezier.EaseInOutBack;
            for (int i = 0; i <= count; i++)
            {
                var x = dis * i;
                //Console.WriteLine($"{x}\t{b.Lerp(x)}\t{b.SolveT(x)}");
                Console.WriteLine($"{x}\t{b.Lerp(x)}");
            }


            Console.ReadKey();
        }
    }
}
