using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceTracker
{
    public partial class Opportunities : System.Web.UI.Page
    {

        public void Page_Init(object o, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!Page.IsPostBack)
            {
                //loadOpps();
            }
        }

        private void loadOpps()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Equipment Type", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Customer Name", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Date Opened", typeof(string));
            string mid = Context.User.Identity.Name.ToString();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
        }

        protected void oppsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void oppsGridView_DataBinding(object sender, EventArgs e)
        {
            
        }
    }
}