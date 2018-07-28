using System;
using System.ServiceModel;

namespace Ruhnow.ServiceModel.Server
{
    public class MicroserviceHost<TService> : ServiceHost where TService : class
    {
        public MicroserviceHost() : base(typeof(TService)) { }

        public MicroserviceHost(Func<TService> hostFactory) : base(hostFactory.Invoke()) { }

        public MicroserviceHost(TService instance) : base(instance) { }

        protected override void OnOpening()
        {
            this.RunAsMicroservice();
            base.OnOpening();
        }
    }
}
