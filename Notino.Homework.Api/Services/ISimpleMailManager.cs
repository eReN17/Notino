namespace Notino.Homework.Api
{
    public interface ISimpleMailManager
    {
        Task SednFileTroughMail(string recipient, byte[] file);
    }
}
