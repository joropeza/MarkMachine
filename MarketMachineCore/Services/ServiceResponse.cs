using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMachineCore.Services
{
    public interface IServiceArgument
    {
        object ArgumentValue { get; set; }
    }

    public class ServiceArgument<T> : IServiceArgument
    {
        object IServiceArgument.ArgumentValue
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = (T)value;
            }
        }

        public T Value { get; set; }

        public ServiceArgument(object newValue)
        {

        }

    }

    public interface IServiceResponseMessage
    {

    }

    public class ServiceResponseMessage<T> : IServiceResponseMessage
    {
        public T RequestObject { get; set; }
        public Exception Exception { get; set; }
        public int ElapsedTime { get; set; }
        public IServiceArgument Argument { get; set; }
    }
}
