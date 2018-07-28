using Ruhnow.ServiceModel.Core.Pipeline;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Ruhnow.ServiceModel.Server
{
    public class ServerMessagePipeline : IDispatchMessageInspector, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            foreach (var item in MessagePipeline.RequestHandlers)
            {
                item.Handler.Process(PipelineEventStage.ServerRequest, request);
            }
            return null;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            foreach (var item in MessagePipeline.ResponseHandlers)
            {
                item.Handler.Process(PipelineEventStage.ServerResponse, reply);
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
}
