namespace Utils.Message
{
    public interface IMessageErrorService
    {
        void Add(string message);
        string GetMessageError();
        bool Valid(bool condition, string value);
        T AddWithReturn<T>(string message);
        bool Any();
    }
}
