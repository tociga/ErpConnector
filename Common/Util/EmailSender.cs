using ErpConnector.Common.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace ErpConnector.Common.Util
{
    public static class EmailSender
    {
        private enum SendEmailType { Always=0, OnError, Never};
        public static void SendEmail(int actionId, DateTime date)
        {
            try
            {
                var sendEmailStr = ConfigurationManager.AppSettings["SendEmail"];
                var emailFromAddress = ConfigurationManager.AppSettings["NotificationSendFrom"];
                var pwd = ConfigurationManager.AppSettings["NotificationPassword"];
                SendEmailType sendEmail = SendEmailType.Always;
                if (!Enum.TryParse<SendEmailType>(sendEmailStr, out sendEmail))
                {
                    // parsing failed, abort sending
                    return;
                }
                if (sendEmail== SendEmailType.Never)
                {
                    return;
                }

                //Create body
                StringBuilder sb = new StringBuilder();
                var erpActionStep = DataWriter.GetActionSteps(actionId);
                bool success = erpActionStep.Count(x => x.Success.HasValue && x.Success.Value) == erpActionStep.Count;
                if (sendEmail == SendEmailType.Always || (sendEmail == SendEmailType.OnError && !success))
                {
                    var environment = System.Configuration.ConfigurationManager.AppSettings["NotificationEnvironment"];
                    sb.AppendLine(string.Format("The D365 transfer started at {0} on environment <b>{2}</b> was a <b>{1}</b>,", date, success ? "success" : "failure", environment));
                    sb.AppendLine(string.Format("for the action id:{0}<br>", actionId));


                    if (!success)
                    {
                        sb.AppendLine("The following steps failed:<br>");
                        sb.AppendLine(CreateErrorBody(erpActionStep.Where(x => x.Success.HasValue && x.Success.Value == false).ToList()));
                    }


                    var emailToAddresses = System.Configuration.ConfigurationManager.AppSettings["NotificationEmailAddresses"];
                    string[] emailAddressArray = emailToAddresses.Split(',');
                    MailAddress emailFrom = new MailAddress(emailFromAddress);
                    foreach (var email in emailAddressArray)
                    {
                        MailAddress emailTo = new MailAddress(email);
                        MailMessage mail = new MailMessage(emailFrom, emailTo);
                        mail.ReplyToList.Add(new MailAddress("servicedesk@agrdynamics.com"));
                        SmtpClient client = new SmtpClient();
                        NetworkCredential c = new NetworkCredential(emailFromAddress, pwd);

                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "smtp.office365.com";
                        client.Credentials = c;
                        client.EnableSsl = true;
                        client.Port = 587;
                        mail.Subject = "D365 Datatransfer status";
                        mail.Body = sb.ToString();
                        mail.IsBodyHtml = true;
                        client.Send(mail);
                    }
                }
            }
            catch (Exception e)
            {
                DataWriter.LogCommError(e.Message, e.StackTrace, "Email Sender", e.HResult);
            }
        }
        private static string CreateErrorBody(List<ErpActionStep> steps )
        {
            StringBuilder sb = new StringBuilder();
            if (steps.Count > 0)
            {                
                sb.AppendLine("<table>");
                sb.AppendLine(CreateTableline(steps[0], true));
                foreach(var s in steps)
                {
                    sb.AppendLine(CreateTableline(s, false));
                }
                sb.AppendLine("</table>");
            }

            return sb.ToString();
        }
        private static string CreateTableline(ErpActionStep step, bool isHeader)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<tr>");
            foreach(PropertyInfo pi in step.GetType().GetProperties(BindingFlags.Public| BindingFlags.Instance))
            {
                if (isHeader)
                {                   
                    if (pi.Name =="ErrorMessage")
                    {
                        sb.AppendLine(string.Format("<td width=\"40%\" style=\"vertical-align:top\">{0}</td>", pi.Name));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("<td width=\"10%\" style=\"vertical-align:top\">{0}</td>", pi.Name));
                    }
                }
                else
                {
                    if (pi.Name == "ErrorMessage")
                    {
                        sb.AppendLine(string.Format("<td width=\"40%\" style=\"vertical-align:top\">{0}</td>", pi.GetValue(step)));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("<td width=\"10%\" style=\"vertical-align:top\">{0}</td>", pi.GetValue(step)));
                    }
                }
            }
            sb.AppendLine("</tr>");
            return sb.ToString();
        }
    }
}
