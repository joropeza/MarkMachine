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
            DataIntegrityChecks dc = new DataIntegrityChecks();

            //pr.GenerateMonthlyCandles(1);

            pr.FirstAndLastDates();

            /*
            pr.UpdateMarkets();
            pr.CandleCatchupAll();
            pr.GapFinders();
            
            pr.FillUnfilledGaps();

            pr.GetCurrentPrices();

            pr.GapStats();

            dc.DailyCandleCheck(1);
            */

            Console.ReadLine();
        }
    }
}
