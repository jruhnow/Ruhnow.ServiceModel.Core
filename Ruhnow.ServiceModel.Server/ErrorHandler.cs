using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Ruhnow.ServiceModel.Server
{
    public class ErrorHandler : IErrorHandler
    {
        public ErrorHandler(Action<Exception> handler)
        {
            this.Handler = handler;
        }

        protected Action<Exception> Handler { get; private set; }
        public bool HandleError(Exception error)
        {
            try
            {
                Handler.Invoke(error);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

        }
    }
}
