using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            double tester = 2.9e12;

            double result = tester + 15;

            Console.WriteLine(result);

            Console.WriteLine(Char.IsSymbol('*') + "* is a symobl");

            double resultTwo = 0.0;

            if (Double.TryParse("2.9e2", out resultTwo))
            {
                Console.WriteLine(resultTwo);
            }

            Console.WriteLine(Char.IsSymbol('x') + "x is a symbol");
        }
    }
}
