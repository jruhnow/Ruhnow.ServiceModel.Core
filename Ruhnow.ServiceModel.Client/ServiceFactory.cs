using System;
using System.ServiceModel;

namespace Ruhnow.ServiceModel.Client
{
    public class ServiceFactory {
        public static TService Proxy<TService>() where TService : class
        {
            return Proxy<TService>("*");
        }
        public static TService Proxy<TService>(string name) where TService : class
        {
            var factory = new ChannelFactory<TService>(name);
            return factory.CreateChannel();
        }
    }

    public static class ServiceProviderExtensions
    {
        public static TType GetServiceOrDefault<TType, TDefault>(this IServiceProvider provider) where TType : class where TDefault : TType
        {
            TType serviceInstance = provider.GetService(typeof(TType)) as TType;
            if (serviceInstance == null)
            {
                return Activator.CreateInstance<TDefault>();
            }
            else
            {
                return serviceInstance;
            }
        }
    }
}
