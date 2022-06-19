using System.Net.Mail;

namespace Notino.Homework.Api
{
    public class SimpleMaiManager : ISimpleMailManager
    {
        const string SenderAddress = "sender@notino.eu";
        const string EmailSubject = "File <file>";
        const string EmailBody = "Hello we are sending you this sepcial file as an attachment do whatever you want with it...";

        private string _serverAddress;
        private int _port = 25;

        private string _login;
        private string _pass;

        public SimpleMaiManager(IConfiguration configuration)
        {
            _serverAddress = configuration["MailServer:Address"];
            int.TryParse(configuration["MailServer:Port"], out _port);
            _login = configuration["MailServer:Login"];
            _pass = configuration["MailServer:Pass"];
        }

        public async Task SendFile(string recipient, string fileName, Stream fileData)
        {
            using (var smtpClient = new SmtpClient(_serverAddress, _port))
            using (var message = new MailMessage(SenderAddress, recipient, EmailSubject.Replace("<file>", fileName), EmailBody))
            {
                //if credentials are necessary
                //smtpClient.Credentials = new NetworkCredential(_login, _pass);
                message.Attachments.Add(new Attachment(fileData, fileName));
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
