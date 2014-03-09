using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketMachineProcedures;

namespace MarketMachineConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessRunner pr = new ProcessRunner();
            pr.CandleCatchupAll();

            Console.ReadLine();
        }
    }
}
