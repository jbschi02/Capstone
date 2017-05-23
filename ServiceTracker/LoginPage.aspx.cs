using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceTracker
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

        //login stuff
        protected void DoLoginStuff(object sender, EventArgs e)
        {
            string MID = MIDText.Text;
            string password = MPasswordText.Text;

            if (string.IsNullOrWhiteSpace(MID))
            {
                noPasswordLabel.Visible = false;
                noMIDLabel.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                noMIDLabel.Visible = false;
                noPasswordLabel.Visible = true;
            }
            else
            {
                noMIDLabel.Visible = false;
                noPasswordLabel.Visible = false;
            }
        }

        protected void RegisterManager(object sender, EventArgs e)
        {
            string MID = newMIDText.Text;
            string password = newPasswordText.Text;
            string confirmPassword = confirmPasswordText.Text;

            if (string.IsNullOrWhiteSpace(MID))
            {
                passwordsDifferentLabel.Visible = false;
                noFirstPasswordLabel.Visible = false;
                noSecondPasswordLabel.Visible = false;
                noMIDRegisterLabel.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                passwordsDifferentLabel.Visible = false;
                noSecondPasswordLabel.Visible = false;
                noMIDRegisterLabel.Visible = false;
                noFirstPasswordLabel.Visible = true;
            }
            else if(string.IsNullOrWhiteSpace(confirmPassword))
            {
                passwordsDifferentLabel.Visible = false;
                noFirstPasswordLabel.Visible = false;
                noMIDRegisterLabel.Visible = false;
                noSecondPasswordLabel.Visible = true;
            }
            else if(password.Length < 8)
            {
                passwordReqLabel.Visible = true;
            }
            else
            {
                passwordReqLabel.Visible = false;
                passwordsDifferentLabel.Visible = false;
                noFirstPasswordLabel.Visible = false;
                noMIDRegisterLabel.Visible = false;
                noSecondPasswordLabel.Visible = false;
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

        private void createNewManager(string mID, string password)
        {

        }
    }
}