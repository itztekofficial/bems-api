using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Core.Util
{
    /// <summary>
    /// The mail configuration.
    /// </summary>
    public class MailConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailConfiguration"/> class.
        /// </summary>
        public MailConfiguration()
        {
            Port = 25;
            IsSSL = false;
            SMPTServer = "";
            UserDisplayName = "";
            UserEMail = "";
            UserEMailPassword = "";
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the s m p t server.
        /// </summary>
        public string SMPTServer { get; set; }

        /// <summary>
        /// Gets or sets the user display name.
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user e mail.
        /// </summary>
        public string UserEMail { get; set; }

        /// <summary>
        /// Gets or sets the user e mail password.
        /// </summary>
        public string UserEMailPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether s s is l.
        /// </summary>
        public bool IsSSL { get; set; }
    }

    /// <summary>
    /// The message data.
    /// </summary>
    public class MessageData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageData"/> class.
        /// </summary>
        public MessageData()
        {
            IsHtmlMessage = false;
            Message = "";
            Subject = "";
            AttachmentPath = new List<string>();
            ToMail = new List<string>();
            CCMail = new List<string>();
            BCCMail = new List<string>();
        }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether html is message.
        /// </summary>
        public bool IsHtmlMessage { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the attachment path.
        /// </summary>
        public List<string> AttachmentPath { get; set; }

        /// <summary>
        /// Gets or sets the to mail.
        /// </summary>
        public List<string> ToMail { get; set; }

        /// <summary>
        /// Gets or sets the c c mail.
        /// </summary>
        public List<string> CCMail { get; set; }

        /// <summary>
        /// Gets or sets the b c c mail.
        /// </summary>
        public List<string> BCCMail { get; set; }
    }

    /// <summary>
    /// The mail sms.
    /// </summary>
    public class MailSMS
    {
        /// <summary>
        /// Sends the sms.
        /// </summary>
        /// <param name="SendTo">The send to.</param>
        /// <param name="URL">The u r l.</param>
        /// <param name="sentmessage">The sentmessage.</param>
        /// <returns>A string.</returns>
        public static string SendSMS(string SendTo, string URL, string sentmessage)
        {
            const string TransportURL = "http://203.212.70.200/smpp/sendsms?";
            string strPost = "username=uname&password=upwd&to=" + SendTo.Trim() + "&from=frm&text=" + sentmessage + "&rpt=1";

            if (!string.IsNullOrEmpty(URL))
                strPost += "&url=" + URL;

            WebRequest request = WebRequest.Create(TransportURL);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(strPost);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            if (responseFromServer.Length > 0)
            {
                return responseFromServer;
            }
            else
            {
                return ((HttpWebResponse)response).StatusDescription;
            }
        }

        /// <summary>
        /// Send the mail.
        /// </summary>
        /// <param name="messageColl">The message coll.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="result">The result.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>A bool.</returns>
        public static bool SendEMail(List<MessageData> messageColl, MailConfiguration configuration, out Dictionary<object, Exception> result, ref Exception ex)
        {
            SmtpClient smtpClient = null; bool isSuccess = true; ex = null;
            result = new Dictionary<object, Exception>();
            object code = 0;
            try
            {
                smtpClient = new SmtpClient(configuration.SMPTServer, configuration.Port)
                {
                    EnableSsl = configuration.IsSSL,
                    // Timeout = 900000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(configuration.UserEMail, configuration.UserEMailPassword)
                };

                MailMessage mailMessage = new();
                foreach (MessageData messageData in messageColl)
                {
                    code = messageData.Tag;
                    try
                    {
                        mailMessage = new()
                        {
                            From = new MailAddress(configuration.UserEMail, configuration.UserDisplayName)
                        };
                        if (messageData.CCMail.Count > 0)
                            foreach (var cc in messageData.CCMail)
                                mailMessage.CC.Add(new MailAddress(cc));

                        if (messageData.BCCMail.Count > 0)
                            foreach (var bcc in messageData.BCCMail)
                                mailMessage.Bcc.Add(new MailAddress(bcc));

                        if (messageData.ToMail.Count > 0)
                            foreach (var to in messageData.ToMail)
                                mailMessage.To.Add(new MailAddress(to));

                        mailMessage.Subject = messageData.Subject;

                        if (messageData.AttachmentPath.Count > 0)
                            foreach (var att in messageData.AttachmentPath)
                                mailMessage.Attachments.Add(new Attachment(att));

                        mailMessage.Body = messageData.Message;
                        mailMessage.IsBodyHtml = messageData.IsHtmlMessage;
                        mailMessage.Priority = MailPriority.High;
                        mailMessage.BodyEncoding = Encoding.UTF8;
                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        mailMessage.ReplyTo = new MailAddress(configuration.UserEMail);
                        ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

                        smtpClient.Send(mailMessage);
                        mailMessage.Dispose();
                        mailMessage = null;
                        result.Add(code, null);
                    }
                    catch (Exception ex2)
                    {
                        result.Add(code, ex2);
                    }
                }
            }
            catch (Exception ex1)
            {
                ex = ex1;
                isSuccess = false;
            }
            finally
            {
                smtpClient.Dispose();
                smtpClient = null;
                messageColl = null;
                configuration = null;
            }
            return isSuccess;
        }

        /// <summary>
        /// Validates the server certificate.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The ssl policy errors.</param>
        /// <returns>A bool.</returns>
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="to">The to.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="file">The file.</param>
        /// <param name="stream">The stream.</param>
        public static bool SendEMail(EmailSetupModel emailSetup, EmailModel emailModel)
        {
            bool IsSendEMail;
            try
            {
                MailMessage mail = new();
                SmtpClient SmtpServer = new(emailSetup.SMTPServer);

                mail.From = new MailAddress(emailSetup.From);
                mail.To.Add(emailModel.To);
                mail.Subject = emailModel.Subject;
                mail.Body = emailModel.Body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                if (emailModel.FileStream != null)
                    mail.Attachments.Add(new Attachment(emailModel.FileStream, emailModel.FileName));

                SmtpServer.Port = emailSetup.Port;
                SmtpServer.Credentials = new NetworkCredential(emailSetup.UserId, emailSetup.UsrPass);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                IsSendEMail = true;
            }
            catch { throw; }
            return IsSendEMail;
        }

        // Below Method created for Create Email template as on 25/Dec/23
        public static string CrateEmailHTMLTemplate(EmailHtmlBodyData emailHtmlBodyData)
        {
            string vResponse = "";
            try
            {
                //string WelcomeMessage1 = "Congratulations on successfully";
                string htmldata = "" +
                    "<tr width=\"80%\">\r\n    " +
                    "<td align=\"center\" valign=\"top\" height=\"200\" style=\"background: linear-gradient(to right, #6aafe1 0%, #29bdd1 100%);\">\r\n       " +
                    " <h1 style=\"color: #fff;\">" + emailHtmlBodyData.Header + "</h1>\r\n " +
                    //  "<p style=\"color: #fff;\"> <img src=\"https://drive.google.com/uc?id=1houwW1_JIu44bpfdoxj9gW20eQOcsIhR\" alt=\"no\" />" +
                    // " <p style=\"color: #fff;\" ><span style=\"font-weight: bold;font-size: 20px;\">" + emailHtmlBodyData.WelcomeMessage + "</span> , </p>\r\n        " +
                    "<p align=\"left\">" + emailHtmlBodyData.WelcomeMessage + " </p>" +
                    " <p style=\"color: #fff;\">  " + emailHtmlBodyData.MailBodyMessage + " </p>\r\n ";
                if (emailHtmlBodyData.IdMessage != "")
                {
                    htmldata += " <p style=\"color: #fff;\">  " + emailHtmlBodyData.IdMessage + ": " + emailHtmlBodyData.IdNumber + " </p>\r\n ";
                }
                //newStr1 += "<a href=\"https://www.google.com/intl/en-GB/gmail/about/\" target=\"_blank\" style=\"color: #fff;text-decoration: underline;\">Please Check Here</a>\r\n    " +

                htmldata += "<p align=\"left\">Cheers. <br />  <strong> Team AKMUS  </strong> </p>" +
                "</td>\r\n   \r\n" +
                "</tr>";
                vResponse = htmldata;
            }
            catch (Exception ex)
            {
                vResponse = ex.Message.ToString();
            }
            return vResponse;
        }
    }

    // <summary>
    // EmailModel
    // </summary>
    public class EmailSetupModel
    {
        public string SMTPServer { get; set; }
        public string From { get; set; }
        public string UserId { get; set; }
        public string UsrPass { get; set; }
        public int Port { get; set; }
    }

    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FileName { get; set; }
        public MemoryStream FileStream { get; set; }
    }

    /// <summary>
    /// The email html template data.
    /// </summary>
    public class EmailHtmlBodyData
    {
        public string Header { get; set; }
        public string WelcomeMessage { get; set; }
        public string MailBodyMessage { get; set; }
        public string IdMessage { get; set; }
        public string IdNumber { get; set; }
    }
}
