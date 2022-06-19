namespace Notino.Homework.Api
{
    public interface ISimpleMailManager
    {
        Task SendFile(string recipient, string fileName, Stream fileData);
    }
}
