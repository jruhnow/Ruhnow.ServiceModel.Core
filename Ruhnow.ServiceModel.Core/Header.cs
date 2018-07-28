using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Ruhnow.ServiceModel.Core
{
    [DataContract]
    public class Header<T>
    {
        public Header(T value)
        {
            this.Value = value;
        }

        public static string GetFullName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return TypeName + "-" + name;
            }
            return TypeName;
        }

        public static string TypeName
        { get; private set; }
        public static string TypeNamespace
        { get; private set; }
        static Header()
        {
            // Verify [DataContract] or [Serializable] on T
            Debug.Assert(IsDataContract(typeof(T)) || typeof(T).IsSerializable, "Header must be a data contract or serializable.");
            TypeNamespace = "net.clr:" + typeof(T).FullName;
            TypeName = "Header";
        }

        static bool IsDataContract(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(DataContractAttribute), false);
            return attributes.Length == 1;
        }

        [DataMember]
        public T Value { get; set; }
    }
}
