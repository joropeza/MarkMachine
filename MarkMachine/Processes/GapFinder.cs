using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkMachine.Processes
{
    public class GapFinder
    {

        public void FindAllOpenGaps(DateTime sd, DateTime ed, int MarketId)
        {

            //look up all of the daily candles in this timespan

            //start from the first date-wise, find opened gaps

            //for each gap, is this gap in the db? if not, put it in


        }

        public void FillAllGaps(DateTime sd, DateTime ed, int MarketId)
        {
            //look up all of the daily candles in this timespan

            //start from the first date-wise. Look to see if any currently open gaps were filled

            //if an open gap was filled, mark the date it happened


        }
    }
}