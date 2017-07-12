using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceTracker
{
    public partial class EmployeeAddedConfirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void AnotherEmployee(object sender, EventArgs e)
        {
            Response.Redirect("AddEmployees.aspx");
        }

        protected void GoToHomePage(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}