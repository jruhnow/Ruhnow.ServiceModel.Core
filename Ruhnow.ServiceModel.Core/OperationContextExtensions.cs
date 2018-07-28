using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Ruhnow.ServiceModel.Core
{
    public static class OperationContextExtensions
    {
        public static MessageHeaders GetMessageHeaders(this OperationContext context,
            OperationDirection direction)
        {
            MessageHeaders headers = null;

            if (context != null)
            {
                if (direction == OperationDirection.Incoming)
                {
                    headers = context.IncomingMessageHeaders;
                }
                if (direction == OperationDirection.Outgoing)
                {
                    headers = context.OutgoingMessageHeaders;
                }
            }

            return headers;
        }


        public static T GetHeader<T>(this OperationContext context, OperationDirection direction, string name)
        {
            return context.GetMessageHeaders(direction).GetHeader<T>(name);
        }

        public static T GetHeader<T>(this OperationContext context)
        {
            // incoming by default
            return context.GetHeader<T>(OperationDirection.Incoming, null);
        }

        public static T GetHeader<T>(this OperationContext context, string name)
        {
            // incoming by default
            return context.GetHeader<T>(OperationDirection.Incoming, name);
        }

        public static void AddHeader<T>(this OperationContext context, T value)
        {
            AddHeader<T>(context, null, value);
        }
        public static void AddHeader<T>(this OperationContext context, string name, T value)
        {
            MessageHeaders headers = context.GetMessageHeaders(OperationDirection.Outgoing);
            headers.AddHeader(name, value);
        }
        public static void AddHeader<T>(this MessageHeaders headers, T value)
        {
            AddHeader(headers, null, value);
        }

        public static void AddHeader<T>(this MessageHeaders headers, string name, T value)
        {
            if (headers == null)
            {
                throw new InvalidOperationException("No headers could be found in the OperationContext, or the OperationContext does not exist.");
            }
            bool headerExists = false;
            try
            {
                var existingHeader = headers.GetHeader<T>(Header<T>.GetFullName(name),
                    Header<T>.TypeNamespace);
                if (existingHeader != null)
                {
                    headerExists = true;
                }
            }
            catch (MessageHeaderException)
            {
                // Debug.Assert(IsHeaderNotExistsException(exception));
            }

            if (headerExists)
            {
                throw new InvalidOperationException("A header with name " + Header<T>.GetFullName(name) + " and namespace " + Header<T>.TypeNamespace + " already exists in the message.");
            }

            MessageHeader<Header<T>> genericHeader = new MessageHeader<Header<T>>(new Header<T>(value));
            headers.Add(genericHeader.GetUntypedHeader(Header<T>.GetFullName(name), Header<T>.TypeNamespace));
        }

        public static T GetHeader<T>(this MessageHeaders headers)
        {
            return GetHeader<T>(headers, null);
        }

        public static T GetHeader<T>(this MessageHeaders headers, string name)
        {
            try
            {
                return headers.GetHeader<Header<T>>(Header<T>.GetFullName(name), Header<T>.TypeNamespace).Value;
            }
            catch (MessageHeaderException)
            {
                return default(T);
            }
        }
    }
}
