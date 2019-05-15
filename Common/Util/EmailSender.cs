using ErpConnector.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace ErpConnector.Common.Util
{
    public static class EmailSender
    {

        public static void SendEmail(int actionId, DateTime date)
        {
            try
            {
                var sendEmailStr = System.Configuration.ConfigurationManager.AppSettings["SendEmail"];
                bool sendEmail = false;
                if (!Boolean.TryParse(sendEmailStr, out sendEmail))
                {
                    // parsing failed, abort sending
                    return;
                }
                if (!sendEmail)
                {
                    return;
                }

                //Create body
                StringBuilder sb = new StringBuilder();
                var erpActionStep = DataWriter.GetActionSteps(actionId);
                bool success = erpActionStep.Count(x => x.Success.HasValue && x.Success.Value) == erpActionStep.Count;
                sb.AppendLine(string.Format("The D365 transfer started at {0} was a <b>{1}</b>,", date, success ? "success" : "failure"));
                sb.AppendLine(string.Format("for the action id:{0}<br>" ,actionId));
                

                if (!success)
                {
                    sb.AppendLine("The following steps failed:<br>");
                    sb.AppendLine(CreateErrorBody(erpActionStep.Where(x => x.Success.HasValue && x.Success.Value == false).ToList()));
                }


                var emailFromAddress = "erpconnector@agrdynamics.com";
                var emailToAddresses = System.Configuration.ConfigurationManager.AppSettings["NotificationEmailAddresses"];
                string[] emailAddressArray = emailToAddresses.Split(',');                
                MailAddress emailFrom = new MailAddress(emailFromAddress);
                foreach (var email in emailAddressArray)
                {
                    MailAddress emailTo = new MailAddress(email);
                    MailMessage mail = new MailMessage(emailFrom, emailTo);
                    mail.ReplyToList.Add(new MailAddress("servicedesk@agrdynamics.com"));
                    SmtpClient client = new SmtpClient();
                    NetworkCredential c = new NetworkCredential(emailFromAddress, "!ErpNotification2019");
                    
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
            catch (Exception)
            {
                //do nothing
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
