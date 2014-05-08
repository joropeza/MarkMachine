using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMachineCore.Services
{
    public abstract class ICoreService
    {
        public IServiceResponseMessage request;
    }

    public class MarketsService : ICoreService
    {

        public IServiceResponseMessage List(ServiceResponseMessage<int> newRequest)
        {
            newRequest.RequestObject = 5;

            this.request = newRequest;
                     
            return request;

        }

    }
}
