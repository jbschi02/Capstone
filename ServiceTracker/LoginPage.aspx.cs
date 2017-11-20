using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace ServiceTracker
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            if (!IsPostBack)
            {
                loginTab.CssClass = "Clicked";
                TabView.ActiveViewIndex = 0;
            }
        }

        protected void Login_Tab_Click(object sender, EventArgs e)
        {
            loginTab.CssClass = "Clicked";
            registerTab.CssClass = "Initial";
            TabView.ActiveViewIndex = 0;
        }

        protected void Register_Tab_Click(object sender, EventArgs e)
        {
            loginTab.CssClass = "Initial";
            registerTab.CssClass = "Clicked";
            TabView.ActiveViewIndex = 1;
        }

        //login
        protected void DoLoginStuff(object sender, EventArgs e)
        {
            noMIDLabel.Visible = false;
            noPasswordLabel.Visible = false;
            string MID = MIDText.Text;
            string password = MPasswordText.Text;

            if (string.IsNullOrWhiteSpace(MID))
            {
                noMIDLabel.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                noPasswordLabel.Visible = true;
            }
            else
            {
                doLogin();
            }
        }

        protected void RegisterManager(object sender, EventArgs e)
        {
            noMIDRegisterLabel.Visible = false;
            noFirstPasswordLabel.Visible = false;
            noSecondPasswordLabel.Visible = false;
            passwordsDifferentLabel.Visible = false;
            userExistsLabel.Visible = false;
            passwordReqLabel.Visible = false;

            string MID = newMIDText.Text;
            string password = newPasswordText.Text;
            string confirmPassword = confirmPasswordText.Text;

            if (string.IsNullOrWhiteSpace(MID))
            {
                noMIDRegisterLabel.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                noFirstPasswordLabel.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                noSecondPasswordLabel.Visible = true;
            }
            else if (password.Length < 8)
            {
                passwordReqLabel.Visible = true;
            }
            else
            {
                if (password.Equals(confirmPassword))
                {
                    createNewManager(MID, password);
                }
                else
                {
                    passwordsDifferentLabel.Visible = true;
                }
            }
        }

        private void doLogin()
        {
            string salt = "";
            string userName = MIDText.Text;
            string dbPwd = "";
            string userPwd = MPasswordText.Text;
            bool isManager = false;

            var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = "SELECT PasswordHash, Salt, Manager FROM Users WHERE Username = @uname";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                oCmd.Parameters.AddWithValue("@uname", userName);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        dbPwd = oReader[0].ToString();
                        salt = oReader[1].ToString();
                        isManager = oReader.GetBoolean(2);
                    }
                }
                myConnection.Close();
            }

            string temp = salt + userPwd;
            string hash = sha256(temp);

            if (hash.Equals(dbPwd) && isManager)
            {
                FormsAuthentication.SetAuthCookie(userName, true);
                Response.Redirect("HomePage.aspx?Username=" + MIDText.Text);
            }
            else
            {
                incorrectPasswordLabel.Visible = true;
            }
        }

        private void createNewManager(string mID, string password)
        {
            //send to database
            string pwd = newPasswordText.Text;
            string fname = fnameTextBox.Text;
            string lname = lnameTextBox.Text;
            string userName = newMIDText.Text;
            string email = emailTextBox.Text;
            string compid = compDropDownList.SelectedItem.ToString();
            string salt = GetUniqueKey(256);
            string temp = salt + pwd;
            string read = "";

            string hash = sha256(temp);

            var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string fString = "SELECT Username FROM Users WHERE Username = @uname";
                SqlCommand fCmd = new SqlCommand(fString, myConnection);
                fCmd.Parameters.AddWithValue("@uname", userName);
                myConnection.Open();
                using (SqlDataReader oReader = fCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        read = oReader[0].ToString();
                    }
                }

                if (string.IsNullOrWhiteSpace(read))
                {
                    string oString = "INSERT INTO Users VALUES(@uname, @pswd, @salt, 1)";
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@uname", userName);
                    oCmd.Parameters.AddWithValue("@pswd", hash);
                    oCmd.Parameters.AddWithValue("@salt", salt);
                    oCmd.ExecuteNonQuery();

                    oString = "INSERT INTO Manager VALUES(@username, @fname, @lname, @email, @compid)";
                    oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@username", userName);
                    oCmd.Parameters.AddWithValue("@fname", fname);
                    oCmd.Parameters.AddWithValue("@lname", lname);
                    oCmd.Parameters.AddWithValue("@email", email);
                    oCmd.Parameters.AddWithValue("@compid", compid);
                    oCmd.ExecuteNonQuery();
                    //redirect to AddEmployees page
                    FormsAuthentication.SetAuthCookie(userName, true);
                    Response.Redirect("AddEmployees.aspx?Username=" + userName);
                }
                else
                {
                    userExistsLabel.Visible = true;
                }
                myConnection.Close();
            }
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