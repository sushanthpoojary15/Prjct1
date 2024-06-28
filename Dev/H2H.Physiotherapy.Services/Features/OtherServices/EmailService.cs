using H2H.Physiotherapy.Services.Configs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using Microsoft.IdentityModel.Tokens;

namespace H2H.Physiotherapy.Services.Features.OtherServices
{
    public class EmailService : IEmailService
    {

        private readonly IHostingEnvironment _hostEnvironment;
        private readonly EmailSettings _emailSettings;
        private SmtpClient _smtpClient;
        private readonly IFileUploadService _fileUploadService;

        public EmailService(IHostingEnvironment hostEnvironment, IOptions<PhysioTheraphyConfigurations> configuration, IFileUploadService fileUploadService)
        {
            _hostEnvironment = hostEnvironment;
            _emailSettings = configuration.Value.EmailSettings;
            ConfigureSMPTPClient();
            _fileUploadService = fileUploadService;
        }

        public async Task SendAutomatedMail(string to, string Subject, string HTMLBody, bool IsCC = false)
        {
            //string filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Error.txt");

            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.IsBodyHtml = true;



                var attachPath = "Content/Images/H2HLogo.png";
                string path1 = Path.Combine("Content", "Images/H2HLogo.png");
                LinkedResource res = new LinkedResource(attachPath, MediaTypeNames.Image.Jpeg);
                res.ContentId = Guid.NewGuid().ToString();
                string htmlBody = HTMLBody + "<br>" + @"<img src='cid:" + res.ContentId + @"'/>";
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody,
                 null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(res);

                mailMessage.AlternateViews.Add(alternateView);
                mailMessage.From = new MailAddress(_emailSettings.UserName);
                mailMessage.Subject = Subject;

                if (to.IndexOf(",") == -1)
                {
                    if (to != "")
                    {
                        mailMessage.To.Add(to);
                    }
                }
                else
                {
                    var emailList = to.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var email in emailList)
                    {
                        mailMessage.To.Add(email);
                    }
                }

                if (IsCC)
                {
                    if (!_emailSettings.CRFDemandCCListRecipients.IsNullOrEmpty())
                    {
                        var ccList = _emailSettings.CRFDemandCCListRecipients;
                        if (ccList != "")
                        {
                            if (ccList.IndexOf(",") == -1)
                                mailMessage.To.Add(ccList.Trim());
                            else
                            {
                                var emailList = ccList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var email in emailList)
                                {
                                    if (email.Trim() != "")
                                        mailMessage.CC.Add(email.Trim());
                                }
                            }
                        }
                    }
                }
                _smtpClient.Send(mailMessage);

                string dataToAppend = "\"----------------------Success-------------------------------------------------------\n";
                var blobdata = new BlobFileData() { blobName = "logs/Error.txt" };
                await _fileUploadService.AppendData(blobdata, dataToAppend);

            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder("-----------------------------------------------------------------------------\n", 1000);

                sb.Append($"Date : {DateTime.Now.ToString()}\n");

                while (ex != null)
                {
                    sb.Append($"{ex.GetType().FullName}\n");
                    sb.Append($"Message : {ex.Message}\n");
                    sb.Append($"StackTrace : {ex.StackTrace}\n");

                    ex = ex.InnerException;
                }

                var blobdata = new BlobFileData() { blobName = "logs/Error.txt" };
                await _fileUploadService.AppendData(blobdata, sb.ToString());

            }
            return;
        }

        public async Task SendAutomatedMailwithCustomCC(string to, string Subject, string HTMLBody, bool IsCC = false, string cc = "")
        {
            //string filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Error.txt");
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;

                var attachPath = "Content/Images/H2HLogo.png";
                string path1 = Path.Combine("Content", "Images/H2HLogo.png");
                LinkedResource res = new LinkedResource(attachPath, mediaType: MediaTypeNames.Image.Jpeg);
                res.ContentId = Guid.NewGuid().ToString();
                string htmlBody = HTMLBody + @"<img src='cid:" + res.ContentId + @"'/>";
                AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody,
                 null, MediaTypeNames.Text.Html);
                alternateView.LinkedResources.Add(res);

                mailMessage.AlternateViews.Add(alternateView);
                mailMessage.From = new MailAddress(_emailSettings.UserName);
                mailMessage.Subject = Subject;

                if (to.IndexOf(",") == -1)
                {
                    if (to != "")
                    {
                        mailMessage.To.Add(to);
                    }
                }
                else
                {
                    var emailList = to.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var email in emailList)
                    {
                        mailMessage.To.Add(email);
                    }
                }

                if (IsCC)
                {

                    var ccList = cc;
                    if (ccList != "")
                    {
                        if (ccList.IndexOf(",") == -1)
                            mailMessage.CC.Add(ccList.Trim());
                        else
                        {
                            var emailList = ccList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var email in emailList)
                            {
                                if (email.Trim() != "")
                                    mailMessage.CC.Add(email.Trim());
                            }
                        }
                    }
                    string dataToAppend = "\"----------------------Success-------------------------------------------------------\n";
                   var blobdata = new BlobFileData() { blobName = "logs/Error.txt" };
                   await _fileUploadService.AppendData(blobdata, dataToAppend);

                }
                _smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder("-----------------------------------------------------------------------------\n", 1000);

                sb.Append($"Date : {DateTime.Now.ToString()}\n");

                while (ex != null)
                {
                    sb.Append($"{ex.GetType().FullName}\n");
                    sb.Append($"Message : {ex.Message}\n");
                    sb.Append($"StackTrace : {ex.StackTrace}\n");

                    ex = ex.InnerException;
                }

                var blobdata = new BlobFileData() { blobName = "logs/Error.txt" };
                await _fileUploadService.AppendData(blobdata, sb.ToString());

            }

            return;
        }

        private void ConfigureSMPTPClient()
        {
            _smtpClient = new SmtpClient()
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential() { UserName = _emailSettings.UserName, Password = _emailSettings.Password },
                Host = _emailSettings.Host,
                EnableSsl = true,
            };
        }
    }

}
