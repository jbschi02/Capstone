using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceTracker
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void reloadData(object sender, EventArgs e)
        {
        }
    }
}