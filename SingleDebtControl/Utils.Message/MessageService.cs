using System.Collections.Generic;
using System.Linq;

namespace Utils.Message
{
    public class MessageService : IMessageService
    {
        private List<DomainMessage> _messagesErrors;
        public MessageService()
        {
            _messagesErrors = new List<DomainMessage>();
        }

        public virtual void Add(string message, string type)
        {
            _messagesErrors.Add(new DomainMessage(message, type));
        }

        public virtual List<DomainMessage> GetMessageError() => _messagesErrors;

        public virtual T AddWithReturn<T>(string message, string type)
        {
            _messagesErrors.Add(new DomainMessage(message, type));

            return default(T);
        }

        public bool Any() => _messagesErrors.Any();

        public bool Valid(bool condition, string message, string type)
        {
            if (condition)
                return AddWithReturn<bool>(message, type);

            return default;
        }
    }
    public class DomainMessage
    {
        public string Value { get; }
        public string Type { get; }
        public DomainMessage(string error, string type)
        {
            Value = error;
            Type = type;
        }
    }
}
