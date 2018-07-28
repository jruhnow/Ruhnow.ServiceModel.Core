using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Ruhnow.ServiceModel.Core.Serialization
{
    public static class ContractResolverExtensions
    {
        public static void AddContractResolver(this ServiceEndpoint endpoint)
        {
            foreach (OperationDescription operation in endpoint.Contract.Operations)
            {
                DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                ContractResolver resolver = new ContractResolver();
                behavior.DataContractResolver = resolver;
            }
        }
        public static void AddContractResolver<T>(this ClientBase<T> proxy) where T : class
        {
            AddContractResolver(proxy.Endpoint);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AddContractResolver<T>(this ChannelFactory<T> factory) where T : class
        {
            AddContractResolver(factory.Endpoint);
        }
    }
}
