using System.ServiceModel.Channels;
using System.Text;

namespace Ruhnow.ServiceModel.Core.Pipeline
{
    public interface IPipelineEventHandler
    {
        void Process(PipelineEventStage eventStage, Message message);
    }
}
