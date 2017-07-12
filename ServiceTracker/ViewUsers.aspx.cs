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
    public partial class ViewUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }

            var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
            using (SqlConnection myConnection = new SqlConnection(myConn))
            {
                SqlCommand myCmd = new SqlCommand("SELECT compname FROM Company", myConnection);
                myConnection.Open();
                SqlDataReader myReader = myCmd.ExecuteReader();

                //Set up the data binding.
                distributorDropDownList.DataSource = myReader;
                distributorDropDownList.DataTextField = "compname";
                distributorDropDownList.DataValueField = "compname";
                distributorDropDownList.DataBind();
                myReader.Close();

                myCmd = new SqlCommand("SELECT fname + ' ' + lname as fullname FROM Manager", myConnection);
                myReader = myCmd.ExecuteReader();

                managerDropDownList.DataSource = myReader;
                managerDropDownList.DataTextField = "fullname";
                managerDropDownList.DataValueField = "fullname";
                managerDropDownList.DataBind();

                //Close the connection.
                myConnection.Close();
                myReader.Close();
            }

            managerDropDownList.Items.Insert(0, "All");
            distributorDropDownList.Items.Insert(0, "All");
        }
    }
}