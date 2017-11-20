using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceTracker
{
    public partial class OppDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!Page.IsPostBack)
            {
                string oppid = Request.QueryString["oppid"];
                var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

                string type = "";
                int age = 0;
                string custname = "";
                string tech = "";
                bool status = false;
                DateTime date = DateTime.Now;
                bool quoteGiven = false;
                double quoteAmount = 0;

                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    string oString = "SELECT * FROM Opportunity WHERE oppid = @oppid";
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@oppid", oppid);
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            type = oReader.GetString(0);
                            age = oReader.GetInt32(1);
                            custname = oReader.GetString(2);
                            tech = oReader.GetString(3);
                            status = oReader.GetBoolean(4);
                            date = oReader.GetDateTime(5);
                            quoteGiven = oReader.GetBoolean(7);
                            quoteAmount = oReader.GetDouble(8);
                        }
                        oReader.Close();
                    }
                    myConnection.Close();

                    typeLabel.Text = type;
                    Label5.Text = age.ToString();
                    Label11.Text = custname;
                    Label12.Text = tech;
                    if (status)
                    {
                        Label13.Text = "Closed";
                    }
                    else Label13.Text = "Open";
                    Label14.Text = date.ToString();
                    if (quoteGiven)
                    {
                        Label15.Text = "Yes";
                        Label16.Text = "$" + quoteAmount.ToString();
                    }
                    else
                    {
                        Label15.Text = "No";
                        Label16.Text = "N/A";
                    }
                }
            }
        }
    }
}