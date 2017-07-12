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
    public partial class Jobs : System.Web.UI.Page
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
                var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
                //Label1.Text = myConn.ToString();
                using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
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

                    myCmd = new SqlCommand("SELECT type FROM JobTypes", myConnection);
                    myReader = myCmd.ExecuteReader();

                    //Set up the data binding.
                    jobTypeDropDownList.DataSource = myReader;
                    jobTypeDropDownList.DataTextField = "type";
                    jobTypeDropDownList.DataValueField = "type";
                    jobTypeDropDownList.DataBind();
                    myReader.Close();
                }

                technicianDropDownList.Items.Insert(0, "All");
                managerDropDownList.Items.Insert(0, "All");
                distributorDropDownList.Items.Insert(0, "All");
                jobTypeDropDownList.Items.Insert(0, "All");
            }
        }

        protected void distributorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string compid = distributorDropDownList.SelectedValue.ToString();
            string mid = managerDropDownList.SelectedValue.ToString();
            string tid = technicianDropDownList.SelectedValue.ToString();

            if (!compid.Equals("All"))
            {
                var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
                using (SqlConnection myConnection = new SqlConnection(myConn))
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd = new SqlCommand("SELECT username, fname + ' ' + lname as fullname FROM Manager WHERE compid = @compid", myConnection);
                    myCmd.Parameters.AddWithValue("@compid", compid);

                    myConnection.Open();
                    SqlDataReader myReader = myCmd.ExecuteReader();

                    managerDropDownList.DataSource = myReader;
                    managerDropDownList.DataTextField = "fullname";
                    managerDropDownList.DataValueField = "username";
                    managerDropDownList.DataBind();
                    myConnection.Close();
                    myReader.Close();
                }
                managerDropDownList.Items.Insert(0, "All");
                technicianDropDownList.Items.Clear();
                technicianDropDownList.Items.Insert(0, "All");
            }
            else
            {
                managerDropDownList.Items.Clear();
                technicianDropDownList.Items.Clear();
                managerDropDownList.Items.Insert(0, "All");
                technicianDropDownList.Items.Insert(0, "All");
            }
        }

        protected void managerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string compid = distributorDropDownList.SelectedValue.ToString();
            string mid = managerDropDownList.SelectedValue.ToString();
            string tid = technicianDropDownList.SelectedValue.ToString();

            //before
            if (!mid.Equals("All"))
            {
                var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
                using (SqlConnection myConnection = new SqlConnection(myConn))
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd = new SqlCommand("SELECT username, fname + ' ' + lname as fullname FROM Technician WHERE mid = @mid", myConnection);
                    myCmd.Parameters.AddWithValue("@mid", mid);

                    myConnection.Open();
                    SqlDataReader myReader = myCmd.ExecuteReader();

                    technicianDropDownList.DataSource = myReader;
                    technicianDropDownList.DataTextField = "fullname";
                    technicianDropDownList.DataValueField = "username";
                    technicianDropDownList.DataBind();
                    myConnection.Close();
                    myReader.Close();
                }
                technicianDropDownList.Items.Insert(0, "All");
            }
            else
            {
                technicianDropDownList.Items.Clear();
                technicianDropDownList.Items.Insert(0, "All");
            }
        }

        protected void loadData(object sender, EventArgs e)
        {
            string compid = distributorDropDownList.SelectedValue.ToString();
            string tid = technicianDropDownList.SelectedValue.ToString();
            string mid = managerDropDownList.SelectedValue.ToString();
            string serviceType = jobTypeDropDownList.SelectedValue.ToString();
            DateTime startDate = DateTime.MinValue;
            bool validStartDate = DateTime.TryParse(startDateTextBox.Text, out startDate);
            DateTime endDate = DateTime.MinValue;
            bool validEndDate = DateTime.TryParse(endDateTextBox.Text, out endDate);
            bool whereStatementNeeded = false;

            string cs = ConfigurationManager.ConnectionStrings["ServiceTracker"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (compid.Equals("All"))
            {
                cmd.CommandText = "SELECT * FROM Jobs";
                whereStatementNeeded = true;
            }
            else if (!compid.Equals("All") && mid.Equals("All"))
            {
                cmd.CommandText = "SELECT custname, servicetype, cost, date, Jobs.tid FROM Jobs JOIN Technician ON Jobs.tid = Technician.username JOIN Manager ON Technician.mid = Manager.username WHERE Manager.compid = @compid";
                cmd.Parameters.AddWithValue("@compid", compid);
            }
            else if (!compid.Equals("All") && !mid.Equals("All") && tid.Equals("All"))
            {
                cmd.CommandText = "SELECT custname, servicetype, cost, date, Jobs.tid FROM Jobs JOIN Technician ON Jobs.tid = Technician.username WHERE Technician.mid = @mid";
                cmd.Parameters.AddWithValue("@mid", mid);
            }
            else
            {
                cmd.CommandText = "SELECT * FROM Jobs WHERE tid = @tid";
                cmd.Parameters.AddWithValue("@tid", tid);
            }

            if (!serviceType.Equals("All"))
            {
                if (whereStatementNeeded)
                {
                    cmd.CommandText += " WHERE servicetype = @servicetype";
                    cmd.Parameters.AddWithValue("@servicetype", serviceType);
                }
                else
                {
                    cmd.CommandText += " and servicetype = @servicetype";
                    cmd.Parameters.AddWithValue("@servicetype", serviceType);
                }
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            jobsGridView.DataSource = dt;
            jobsGridView.DataBind();
            con.Close();
            
        }
    }
}