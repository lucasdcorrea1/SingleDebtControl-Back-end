using System.Collections.Generic;
using System.Linq;

namespace Utils.Message
{
    public class MessageErrorService : IMessageErrorService
    {
        private List<DomainMessageError> _messagesErrors;
        public MessageErrorService()
        {
            _messagesErrors = new List<DomainMessageError>();
        }

        public virtual void Add(string message)
        {
            var notification = _messagesErrors.FirstOrDefault();

            _messagesErrors.Remove(notification);

            _messagesErrors.Add(new DomainMessageError(message));
        }

        public virtual string GetMessageError() => _messagesErrors.FirstOrDefault()?.Error;

        public virtual T AddWithReturn<T>(string message)
        {
            var errors = _messagesErrors.FirstOrDefault();

            _messagesErrors.Remove(errors);

            _messagesErrors.Add(new DomainMessageError(message));

            return default(T);
        }

        public bool Any() => _messagesErrors.Any();

        public bool Valid(bool condition, string message)
        {
            if (condition)
                return AddWithReturn<bool>(message);

            return false;
        }
    }
    public class DomainMessageError
    {
        public string Error { get; }
        public DomainMessageError(string error)
        {
            Error = error;
        }
    }
}
