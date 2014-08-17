using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MarketMachineCore.Data;

namespace MarketMachineBackEnd.Controllers
{
    public class MarketDTO
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
    }

    

    public class MarketsController : ApiController
    {
        MarketsDBEntities mdb = new MarketsDBEntities();

        [AcceptVerbs("GET")]
        [Route("Markets")]
        public HttpResponseMessage Index()
        {
            //returns a list of all user videos
          
                var Markets = mdb.Markets;
                List<MarketDTO> lm = new List<MarketDTO>();
                foreach (var Market in Markets)
                {
                    lm.Add(new MarketDTO { Name = Market.Name, Symbol = Market.Symbol });
                }
                return Request.CreateResponse(HttpStatusCode.OK, lm);
            
        }
    }
}
