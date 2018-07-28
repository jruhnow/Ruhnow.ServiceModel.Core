using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ruhnow.ServiceModel.Core.Pipeline
{
    public class MessagePipeline
    {
        public static IEnumerable<PipelineRequestEventRegistration> RequestHandlers { get { return requestHandlers.Values; } }
        public static IEnumerable<PipelineResponseEventRegistration> ResponseHandlers { get { return responseHandlers.Values; } }

        private static ConcurrentDictionary<string, PipelineRequestEventRegistration> requestHandlers = new ConcurrentDictionary<string, PipelineRequestEventRegistration>();
        private static ConcurrentDictionary<string, PipelineResponseEventRegistration> responseHandlers = new ConcurrentDictionary<string, PipelineResponseEventRegistration>();

        public static void RemoveRequestHandler(string name)
        {
            requestHandlers.TryRemove(name, out PipelineRequestEventRegistration reg);
        }

        public static void RemoveResponseHandler(string name)
        {
            responseHandlers.TryRemove(name, out PipelineResponseEventRegistration reg);
        }

        public static void AddRequestHandler<TRequestHandler>(string name) where TRequestHandler : IPipelineEventHandler
        {
            if (typeof(TRequestHandler).GetConstructor(new Type[0]) == null)
            {
                throw new InvalidOperationException("Request handler must have a parameterless constructor.");
            }

            TRequestHandler handler = Activator.CreateInstance<TRequestHandler>();
            PipelineRequestEventRegistration registration = new PipelineRequestEventRegistration()
            {
                Handler = handler
            };

            requestHandlers.AddOrUpdate(name, registration, (nme, reg) => registration);
        }

        public static void AddResponseHandler<TResponseHandler>(string name) where TResponseHandler : IPipelineEventHandler
        {
            if (typeof(TResponseHandler).GetConstructor(new Type[0]) == null)
            {
                throw new InvalidOperationException("Response handler must have a parameterless constructor.");
            }

            TResponseHandler handler = Activator.CreateInstance<TResponseHandler>();
            PipelineResponseEventRegistration registration = new PipelineResponseEventRegistration()
            {
                Handler = handler
            };

            responseHandlers.AddOrUpdate(name, registration, (nme, reg) => registration);
        }

    }
}
