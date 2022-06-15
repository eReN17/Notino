using System.Net.Mail;

namespace Notino.Homework.Api
{
    public class SimpleMaiManager : ISimpleMailManager
    {
        const string SenderAddress = "sender@notino.sk";

        private string _serverAddress;
        private int _port;

        private string _login;
        private string _pass;

        public SimpleMaiManager(IConfiguration configuration)
        {
            //configuration.get
        }

        public async Task SednFileTroughMail(string recipient, byte[] file)
        {
            using (var smtpClient = new SmtpClient())
            using (var message = new MailMessage(SenderAddress, ""))
            {
                



                //await smtpClient.SendAsync(message, null);
            }
        }
    }
}
