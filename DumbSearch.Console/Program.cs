using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumbSearch.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("DumbSearch Command Line Utility");

            var s = new DumbSearch.Service.Searcher();

            System.Console.WriteLine();
            System.Console.WriteLine("Press any key...");
            System.Console.ReadKey();
        }
    }
}
