using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Ruhnow.ServiceModel.Core.Serialization;

namespace Ruhnow.ServiceModel.Server
{
    public static class ServiceHostExtensions
    {
        public static void AddContractResolver(this ServiceHostBase host)
        {
            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                endpoint.AddContractResolver();

            }
        }
        public static void RunAsMicroservice(this ServiceHostBase host)
        {
            // Use the custom contract resolver to ensure we can resolve all contracts and not just those natively discoverable by WCF
            host.AddContractResolver();
            // This allows the incoming source address to not be a match for the hosted address - required when running through a load balancer
            host.AllowAnySourceAddress();
            // Opt into extensible processing pipeline 
            host.AddPipeline();

        }
        public static void SetAddressFilterMode(this ServiceHostBase host, AddressFilterMode mode)
        {
            ServiceBehaviorAttribute existingBehavior = host.Description.Behaviors.FirstOrDefault(b => b.GetType() == typeof(ServiceBehaviorAttribute)) as ServiceBehaviorAttribute;
            if (existingBehavior == null)
            {
                existingBehavior = new ServiceBehaviorAttribute();
                host.Description.Behaviors.Add(existingBehavior);
            }

            existingBehavior.AddressFilterMode = mode;
        }

        public static void AllowAnySourceAddress(this ServiceHostBase host)
        {
            host.SetAddressFilterMode(AddressFilterMode.Any);
        }


    }
}
