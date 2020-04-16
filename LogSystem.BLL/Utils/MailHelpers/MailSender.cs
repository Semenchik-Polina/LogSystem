using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Quartz;
using LogSystem.BLL.Services;
using LogSystem.Common.Enums;
using LogSystem.BLL.DTO.UserDTO;

namespace LogSystem.BLL.Utils
{
    public class MailSender: IJob
    {
        const string defaultSubject = "LogSystem report";
        const string testUserEmail = "semenchik.polina@mail.ru";
        const string defaultMessageText = "Here is your daily report of user's actions in LogSystem.";


        public async Task Execute(IJobExecutionContext context)
        {
            // get all admins
            UserService userService = new UserService();
            var users = await userService.GetAllUsersByType(UserType.Admin);
            
            // send the message for all of them
            foreach(UserGetDetailDTO user in users)
            {
                await SendMessageAsync(user.Email);
            }
            
        }

        public async Task SendMessageAsync(string to = testUserEmail, string subject = defaultSubject, 
            string messageBody = defaultMessageText)
        {
            // Creare message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(MailSettings.Email));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;

            //create text
            var bodyText = new TextPart("plain")
            {
                Text = MessageBuilder(messageBody)
            };

            // create and get path to the file 
            UserActionReportHelper reportHelper = new UserActionReportHelper();
            await reportHelper.GenerateReportFile();
            string path = reportHelper.GetReportPath();

            // create an attachment
            var attachment = new MimePart()
            {
                Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(path)
            };

            // create the multipart/mixed container
            var multipart = new Multipart("mixed")
            {
                bodyText,
                attachment
            };

            message.Body = multipart;

            // Send the message
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates 
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(MailSettings.SMTPServer, Int32.Parse(MailSettings.Port), MailKit.Security.SecureSocketOptions.SslOnConnect);
                client.Authenticate(MailSettings.Email, MailSettings.Password);

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        private string MessageBuilder(string message)
        {
            StringBuilder sb = new StringBuilder("Hello, ");
            sb.AppendLine("");
            sb.AppendLine(message);
            sb.AppendLine("");
            sb.AppendLine("-- LogSystem");
            return sb.ToString();
        }
    }
}
