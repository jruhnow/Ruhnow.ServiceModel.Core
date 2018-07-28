using System;
using System.ServiceModel;

namespace Ruhnow.ServiceModel.Server
{
    public static class ErrorHandlerExtensions
    {
        public static void AddErrorHandler(this ServiceHostBase host, Action<Exception> action)
        {
            host.Description.Behaviors.Add(new ErrorHandlerBehavior(action));
        }
    }
}
