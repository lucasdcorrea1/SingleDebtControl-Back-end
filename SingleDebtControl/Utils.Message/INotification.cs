using System.Collections.Generic;

namespace Utils.Message
{
    public interface INotification
    {
        void Add(string message, string type);
        List<DomainNotification> GetMessageError();
        bool Valid(bool condition, string message, string type);
        T AddWithReturn<T>(string message, string type);
        bool Any();
    }
}
