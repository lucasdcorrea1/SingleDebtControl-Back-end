namespace Utils.Message
{
    public interface IMessageErrorService
    {
        void Add(string message);
        string GetMessageError();
        T AddWithReturn<T>(string message);
        bool Any();
    }
}
