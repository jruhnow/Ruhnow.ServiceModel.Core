using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Ruhnow.ServiceModel.Core.Serialization
{
    public class ContractResolver : DataContractResolver
    {
        const string DefaultNamespace = "global";

        static Dictionary<Type, Tuple<string, string>> typeToNames = new Dictionary<Type, Tuple<string, string>>();
        static Dictionary<string, Dictionary<string, Type>> namesToType = new Dictionary<string, Dictionary<string, Type>>();

        public static void RegisterType<TContract>()
        {
            Type type = typeof(TContract);
            RegisterType(type);
        }

        public static void RegisterTypes(IEnumerable<Type> types)
        {
            foreach (Type type in types)
            {
                RegisterType(type);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RegisterType(Type type)
        {
            string typeNamespace = type.Namespace ?? DefaultNamespace;
            string typeName = type.Name;

            typeToNames[type] = new Tuple<string, string>(typeNamespace, typeName);

            if (namesToType.ContainsKey(typeNamespace) == false)
            {
                namesToType[typeNamespace] = new Dictionary<string, Type>();
            }
            namesToType[typeNamespace][typeName] = type;

            if (!type.IsArray)
            {
                RegisterType(type.MakeArrayType());
            }
        }


        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            if (namesToType.ContainsKey(typeNamespace))
            {
                if (namesToType[typeNamespace].ContainsKey(typeName))
                {
                    return namesToType[typeNamespace][typeName];
                }
            }
            return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
        }

        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            if (typeToNames.ContainsKey(type))
            {
                XmlDictionary dictionary = new XmlDictionary();
                typeNamespace = dictionary.Add(typeToNames[type].Item1);
                typeName = dictionary.Add(typeToNames[type].Item2);

                return true;
            }
            else
            {
                return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
            }
        }
    }
}
