using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MarketMachineCore.Data;
using MarketMachine.Models.ViewModels;

namespace MarketMachine.Controllers
{
    public class MarketsController : ApiController
    {
        marketdbEntities mdb = new marketdbEntities();


        [Route("Markets")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage MarketsGetAll()
        {
            var Markets = mdb.Markets;

            List<MarketListItemViewModel> lvm = new List<MarketListItemViewModel>();

            foreach (var Market in Markets)
            {
                MarketListItemViewModel mlivm = new MarketListItemViewModel();
                mlivm.Name = Market.Name;
                lvm.Add(mlivm);
            }

            return Request.CreateResponse(HttpStatusCode.OK, lvm);


        }


        [Route("Markets/{MarketId}")]
        [AcceptVerbs("GET")]
        public HttpResponseMessage MarketsGetOne(int MarketId)
        {
            var Market = mdb.Markets.Find(MarketId);

            
            MarketListItemViewModel mlivm = new MarketListItemViewModel();
            mlivm.Name = Market.Name;


            return Request.CreateResponse(HttpStatusCode.OK, mlivm);


        }

       

    }

}
