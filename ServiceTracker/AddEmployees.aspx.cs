using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.ComponentModel;

namespace ServiceTracker
{
    public partial class AddEmployees : System.Web.UI.Page
    {
        SmtpClient client;
        MailMessage msg;
        NetworkCredential login;
        const string mailingpswd = "ServiceTrackingApplication";
        const string mailingaddr = "servicetrackermailing";
        const string mailingSMTP = "smtp.gmail.com";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void addEmployee(object sender, EventArgs e)
        {
            string email = emailField.Text;

            login = new NetworkCredential(mailingaddr, mailingpswd);
            client = new SmtpClient(mailingSMTP);
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            msg = new MailMessage
            {
                From = new MailAddress(mailingaddr + "@gmail.com", "Service Tracking")
            };
            msg.To.Add(new MailAddress(email));
            msg.Subject = "Your manager has invited you to join Service Tracker";
            msg.Body = fnameField.Text + ", please follow the link to register for Service Tracker!";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            //String userState = "Sending...";
            client.Send(msg);

            Response.Redirect("EmployeeAddedConfirmation.aspx");
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
            }
        }

        protected void GoToHomePage(Object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}