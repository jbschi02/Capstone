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
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

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
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void addEmployee(object sender, EventArgs e)
        {
            string email = emailField.Text;
            string fnameChar = fnameField.Text;
            fnameChar = fnameChar.Substring(0, 1);
            string lname = lnameField.Text.Substring(0, 4);
            string name = fnameChar + lname;
            string tempPswd = generatePassword(name);
            string username = fnameChar + lname;

            username = addEmployeeToDB(username, tempPswd, email, fnameField.Text, lnameField.Text);

            if (!username.Equals("error"))
            {
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
                msg.Body = fnameField.Text + ", please follow the link to register for Service Tracker!<br/><br/> Username: " + username + "<br/>Password: " + tempPswd;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //String userState = "Sending...";
                client.Send(msg);

                Response.Redirect("EmployeeAddedConfirmation.aspx");
            }
        }

        private string addEmployeeToDB(string username, string tempPswd, string email, string fname, string lname)
        {
            string salt = GetUniqueKey(256);
            string temp = salt + tempPswd;
            string read = "";

            string hash = sha256(temp);

            var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            bool isAlreadyMember = true;
            for (int i = 0; isAlreadyMember; i++)
            {
                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    string fString = "SELECT Username FROM Users WHERE Username = @uname";
                    SqlCommand fCmd = new SqlCommand(fString, myConnection);
                    fCmd.Parameters.AddWithValue("@uname", username);
                    myConnection.Open();
                    using (SqlDataReader oReader = fCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            read = oReader[0].ToString();
                        }
                    }
                    myConnection.Close();
                }

                if (string.IsNullOrWhiteSpace(read))
                {
                    isAlreadyMember = false;
                }
                else
                {
                    username = getNewUserName(username, i);
                    read = "";
                }
            }
            using (SqlConnection myConnection = new SqlConnection(con))
            { 
                if (string.IsNullOrWhiteSpace(read))
                {
                    string oString = "INSERT INTO Users VALUES(@uname, @pswd, @salt, 0)";
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@uname", username);
                    oCmd.Parameters.AddWithValue("@pswd", hash);
                    oCmd.Parameters.AddWithValue("@salt", salt);
                    myConnection.Open();
                    oCmd.ExecuteNonQuery();

                    oString = "Insert INTO Technician VALUES(@uname, @fname, @lname, @email, @mid)";
                    oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@uname", username);
                    oCmd.Parameters.AddWithValue("@fname", fname);
                    oCmd.Parameters.AddWithValue("@lname", lname);
                    oCmd.Parameters.AddWithValue("@email", email);
                    oCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name);
                    oCmd.ExecuteNonQuery();

                    oString = "INSERT INTO Goals VALUES(3000, 1000000, @tid, 0, 0, 90000, 90000, 90000, 90000, 90000, 90000, 90000, 90000, 90000, 90000, 90000, 90000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)";
                    oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@tid", username);
                    oCmd.ExecuteNonQuery();

                    myConnection.Close();
                    return username;
                }
                else
                {
                    myConnection.Close();
                    return "error";
                }
            }
        }

        private string getNewUserName(string username, int i)
        {
            if (i == 0)
            {
                return username + (i + 1).ToString();
            }
            else
            {
                username = username.Substring(0, username.Length - 1);
                return username + (i + 1).ToString();
            }
        }

        private string generatePassword(string username)
        {
            var bytes = new UTF8Encoding().GetBytes(username);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            string x = Convert.ToBase64String(hashBytes);
            return x.Substring(0, 8);
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

        //from https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        //from https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
        static string sha256(string randomString)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString), 0, Encoding.UTF8.GetByteCount(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}