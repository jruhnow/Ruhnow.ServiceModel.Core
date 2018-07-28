using Ruhnow.ServiceModel.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Ruhnow.ServiceModel.Client.Pipeline
{
    public class ClientMessagePipeline : IClientMessageInspector, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            foreach (var item in MessagePipeline.ResponseHandlers)
            {
                item.Handler.Process(PipelineEventStage.ClientResponse, reply);
            }
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            foreach (var item in MessagePipeline.RequestHandlers)
            {
                item.Handler.Process(PipelineEventStage.ClientRequest, request);
            }

            return null;
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}
