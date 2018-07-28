using System.ServiceModel;
using System.ServiceModel.Description;

namespace Ruhnow.ServiceModel.Server
{
    public static partial class PipelineExtensions
    {
        public static void AddPipeline(this ServiceHostBase host)
        {
            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                endpoint.EndpointBehaviors.Add(new ServerMessagePipeline());
            }
        }
    }
}
