using System.Collections.Generic;
using System.Linq;

namespace Utils.Message
{
    public class Notification : INotification
    {
        private List<DomainNotification> _messagesErrors;
        public Notification()
        {
            _messagesErrors = new List<DomainNotification>();
        }

        public virtual void Add(string message, string type)
        {
            _messagesErrors.Add(new DomainNotification(message, type));
        }

        public virtual List<DomainNotification> GetMessageError() => _messagesErrors;

        public virtual T AddWithReturn<T>(string message, string type)
        {
            _messagesErrors.Add(new DomainNotification(message, type));

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
    public class DomainNotification
    {
        public string Value { get; }
        public string Type { get; }
        public DomainNotification(string error, string type)
        {
            Value = error;
            Type = type;
        }
    }
}
