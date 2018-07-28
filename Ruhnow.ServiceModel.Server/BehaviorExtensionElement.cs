using System;
using System.Collections.Generic;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Ruhnow.ServiceModel.Server
{
    public class BehaviorExtensionElement<TBehavior> : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(TBehavior); }
        }

        protected override object CreateBehavior()
        {
            return Activator.CreateInstance<TBehavior>();
        }
    }
}
