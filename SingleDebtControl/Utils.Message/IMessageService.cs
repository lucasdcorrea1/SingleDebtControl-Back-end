using System.Collections.Generic;

namespace Utils.Message
{
    public interface IMessageService
    {
        void Add(string message, string type);
        List<DomainMessage> GetMessageError();
        bool Valid(bool condition, string message, string type);
        T AddWithReturn<T>(string message, string type);
        bool Any();
    }
}
