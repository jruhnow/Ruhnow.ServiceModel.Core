using System;
using System.ServiceModel.Configuration;

namespace Ruhnow.ServiceModel.Server
{
    public class MicroserviceBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(MicroserviceBehavior);

        protected override object CreateBehavior()
        {
            return new MicroserviceBehavior();
        }
    }
}
