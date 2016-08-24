using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyAspNetIdentity.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task<dynamic> configSendGridasync(IdentityMessage message)
        {
            //string apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY", EnvironmentVariableTarget.User);
            var apiKey = "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";
            dynamic sg = new SendGridAPIClient(apiKey);

           

            Email from = new Email(message.Destination);
            string subject = message.Subject;
            Email to = new Email(message.Destination);
            Content content = new Content("text/html", message.Body);
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
            return response;

            //depricated
            //var myMessage = new SendGridMessage();


            //myMessage.AddTo(message.Destination);
            //myMessage.From = new System.Net.Mail.MailAddress("jonathan.beresford86@gmail.com", "Jonny");
            //myMessage.Subject = message.Subject;
            //myMessage.Text = message.Body;
            //myMessage.Html = message.Body;

            //var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
            //                                        ConfigurationManager.AppSettings["emailService:Password"]);

            //// Create a Web transport for sending email.
            ////var transportWeb = new Web(credentials);

            //// Send the email.
            //if (transportWeb != null)
            //{
            //    await transportWeb.DeliverAsync(myMessage);
            //}
            //else
            //{
            //    //Trace.TraceError("Failed to create Web transport.");
            //    await Task.FromResult(0);
            //}
        }
    }
}