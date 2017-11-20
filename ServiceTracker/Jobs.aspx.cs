using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                bool isPlumberSupply = false;
                var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
                //Label1.Text = myConn.ToString();
                using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
                {
                    isPlumberSupply = authPS(Context.User.Identity.Name.ToString());
                    SqlCommand myCmd = new SqlCommand();
                    if (isPlumberSupply)
                    {
                        myCmd = new SqlCommand("SELECT compname FROM Company", myConnection);
                        myConnection.Open();
                    }
                    else
                    {
                        myCmd = new SqlCommand("SELECT compid FROM Manager WHERE username = @mid", myConnection);
                        myCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name.ToString());
                        myConnection.Open();
                    }

                    //Set up the data binding.
                    SqlDataReader myReader = myCmd.ExecuteReader();
                    distributorDropDownList.DataSource = myReader;
                    if (isPlumberSupply)
                    {
                        distributorDropDownList.DataTextField = "compname";
                        distributorDropDownList.DataValueField = "compname";
                    }
                    else
                    {
                        distributorDropDownList.DataTextField = "compid";
                        distributorDropDownList.DataValueField = "compid";
                    }
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

                if (isPlumberSupply)
                {
                    distributorDropDownList.Items.Insert(0, "All");
                    managerDropDownList.Items.Insert(0, "All");
                    technicianDropDownList.Items.Insert(0, "All");
                }
                else
                {
                    distributorDropDownList_SelectedIndexChanged(sender, e);
                }
                jobTypeDropDownList.Items.Insert(0, "All");
            }
        }

        private bool authPS(string v)
        {
            string compid = "";
            var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
            //Label1.Text = myConn.ToString();
            using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
            {
                SqlCommand myCmd = new SqlCommand("SELECT compid FROM Manager WHERE username = @mid", myConnection);
                myCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name.ToString());
                myConnection.Open();
                using (SqlDataReader oReader = myCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        compid = oReader[0].ToString();
                    }
                }
                myConnection.Close();
            }

            if (compid.Equals("Plumber Supply"))
            {
                return true;
            }
            else return false;
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
            Bools bools = new Bools();
            dateLabel.Visible = false;
            string compid = distributorDropDownList.SelectedValue.ToString();
            string tid = technicianDropDownList.SelectedValue.ToString();
            string mid = managerDropDownList.SelectedValue.ToString();
            string serviceType = jobTypeDropDownList.SelectedValue.ToString();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool validStartDate = DateTime.TryParse(startDateTextBox.Text, out startDate);
            bool validEndDate = DateTime.TryParse(endDateTextBox.Text, out endDate);
            bool whereStatementNeeded = false;
            bool betweenStatementNeeded = false;
            if (!((validEndDate && validStartDate) || (!validStartDate && !validEndDate)))
            {
                dateLabel.Visible = true;
                return;
            }
            else if (validStartDate && validEndDate)
            {
                betweenStatementNeeded = true;
            }
            if (validEndDate)
            {
                endDate = endDate.AddDays(1);
            }

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
                    whereStatementNeeded = false;
                }
                else
                {
                    cmd.CommandText += " and servicetype = @servicetype";
                    cmd.Parameters.AddWithValue("@servicetype", serviceType);
                }
            }
            if (betweenStatementNeeded)
            {
                if (whereStatementNeeded)
                {
                    cmd.CommandText += " WHERE " + getDateString(betweenStatementNeeded, startDate, endDate);
                }
                else
                {
                    cmd.CommandText += " AND " + getDateString(betweenStatementNeeded, startDate, endDate);
                }
                cmd.Parameters.AddWithValue("@start", startDate.Date);
                cmd.Parameters.AddWithValue("@end", endDate.Date);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            jobsGridView.DataSource = dt;
            jobsGridView.DataBind();
            bools.setData(dt);
            con.Close();
        }

        protected Bools loadData2(object sender, EventArgs e)
        {
            Bools bools = new Bools();
            dateLabel.Visible = false;
            string compid = distributorDropDownList.SelectedValue.ToString();
            string tid = technicianDropDownList.SelectedValue.ToString();
            string mid = managerDropDownList.SelectedValue.ToString();
            string serviceType = jobTypeDropDownList.SelectedValue.ToString();
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool validStartDate = DateTime.TryParse(startDateTextBox.Text, out startDate);
            bool validEndDate = DateTime.TryParse(endDateTextBox.Text, out endDate);
            bool whereStatementNeeded = false;
            bool betweenStatementNeeded = false;
            if (!((validEndDate && validStartDate) || (!validStartDate && !validEndDate)))
            {
                dateLabel.Visible = true;
                return bools;
            }
            else if (validStartDate && validEndDate)
            {
                betweenStatementNeeded = true;
            }
            if (validEndDate)
            {
                endDate = endDate.AddDays(1);
            }

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
                    whereStatementNeeded = false;
                }
                else
                {
                    cmd.CommandText += " and servicetype = @servicetype";
                    cmd.Parameters.AddWithValue("@servicetype", serviceType);
                }
            }
            if (betweenStatementNeeded)
            {
                if (whereStatementNeeded)
                {
                    cmd.CommandText += " WHERE " + getDateString(betweenStatementNeeded, startDate, endDate);
                }
                else
                {
                    cmd.CommandText += " AND " + getDateString(betweenStatementNeeded, startDate, endDate);
                }
                cmd.Parameters.AddWithValue("@start", startDate.Date);
                cmd.Parameters.AddWithValue("@end", endDate.Date);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            jobsGridView.DataSource = dt;
            jobsGridView.DataBind();
            bools.setData(dt);
            con.Close();
            return bools;
        }

        private string getDateString(bool betweenStatementNeeded, DateTime startDate, DateTime endDate)
        {
            if (betweenStatementNeeded)
            {
                return "date BETWEEN @start AND @end";
            }
            else
            {
                return "";
            }
        }

        protected void exportDataButton_Click(object sender, EventArgs e)
        {
            Bools bools = new Bools();
            bools = loadData2(exportDataButton, e);

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=GridViewExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            GridView GridView1 = new GridView();
            GridView1.AutoGenerateColumns = true;
            GridView1.DataSource = bools.printableDT;
            GridView1.DataBind();

            //GridView1.AllowPaging = false;
            //GridView1.DataBind();

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < bools.printableDT.Columns.Count; k++)
            {
                //add separator
                sb.Append(bools.printableDT.Columns[k].ToString() + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < bools.printableDT.Rows.Count; i++)
            {
                for (int k = 0; k < bools.printableDT.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(bools.printableDT.Rows[i][k].ToString() + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void jobsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            jobsGridView.EditIndex = e.NewEditIndex;
            loadData(sender, e);
            DataBind();
        }

        protected void jobsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            jobsGridView.EditIndex = -1;
            loadData(sender, e);
            //Bind data to the GridView control.
            DataBind();
        }

        protected void jobsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            string tid = jobsGridView.Rows[index].Cells[1].Text;
            dateLabel.Text = index.ToString();
            dateLabel.Visible = true;

            tid = jobsGridView.Rows[index].Cells[1].Text;

            /*
            var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string fString = "UPDATE Jobs SET custname = @custname, servicetype = @servicetype, cost = @cost, tid = @tid, date = @date WHERE tid = ";
                SqlCommand fCmd = new SqlCommand(fString, myConnection);
                fCmd.Parameters.AddWithValue("@tid", check);
                for (int j = 0; j < 12; j++)
                {
                    fCmd.Parameters.AddWithValue("@" + months[j].Text, doubleMonths[j]);
                }

                myConnection.Open();
                fCmd.ExecuteNonQuery();
                myConnection.Close();
            }*/

            jobsGridView.EditIndex = -1;
            loadData(sender, e);
            //Bind data to the GridView control.
            DataBind();
        }

        protected void jobsGridView_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            int index = jobsGridView.EditIndex;
            string tid = jobsGridView.Rows[index].Cells[0].Text;
            dateLabel.Text = tid;
            dateLabel.Visible = true;
        }
    }
}