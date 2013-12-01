using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MarkMachine.api
{
    public class MarketsController : ApiController
    {
        [AcceptVerbs("GET")]
        [ActionName("Update")]
        public async Task<string> Update(int id)
        {
            return await UpdateMarket(id);
        }

        private async Task<string> UpdateMarket(int MarketId)
        {
            string status = "Started";

            MarketMachineClassLibrary.MarketUpdates mu = new MarketMachineClassLibrary.MarketUpdates();


            var task = Task.Run(() => mu.UpdateMarketHistorical(DateTime.Now.AddDays(-180), DateTime.Now, MarketId));

            await Task.WhenAll(task);

            status = "Updated!";

            return status;
        }

        
    }

   

}
