using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
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
                Bools bools = new Bools();
                bools = fillServiceRevenueTable(false, bools);
                fillSystemRevenueTable(false, bools);
                fillCombinedRevenueTable(false);
            }
        }

        private void fillCombinedRevenueTable(bool v)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Actual", typeof(string));
            dt.Columns.Add("Goal", typeof(string));
            if (v)
            {

            }
            else
            {
                dt.Rows.Add("", "");
            }
            totalRevenueGridView.DataSource = dt;
            totalRevenueGridView.DataBind();
            //ViewState["totalDataTable"] = totalRevenueGridView;
        }

        private void fillSystemRevenueTable(bool v, Bools bools)
        {
            if (bools.safety)
            {
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Opportunities Ran", typeof(string));
            dt.Columns.Add("Sold", typeof(string));
            dt.Columns.Add("Closing Rate", typeof(string));
            dt.Columns.Add("Average", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            if (v)
            {
                int opps = 0;
                int oppsClosed = 0;
                double closingRate = 0;
                string jobType = "";
                double total = 0;
                double average = 0;
                string compid = distributorDropDownList.SelectedValue.ToString();
                string mid = managerDropDownList.SelectedValue.ToString();
                string tid = technicianDropDownList.SelectedValue.ToString();
                bool whereStatementNeeded = false;
                bool betweenStatementNeeded = false;
                if (bools.dateNeeded)
                {
                    betweenStatementNeeded = true;
                }
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                bool validStartDate = DateTime.TryParse(startDateTextBox.Text, out startDate);
                bool validEndDate = DateTime.TryParse(endDateTextBox.Text, out endDate);
                if (validEndDate)
                {
                    endDate = endDate.AddDays(1);
                }


                var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
                String oString = "";
                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    if (compid.Equals("All"))
                    {
                        oString = "SELECT isClosed FROM Opportunity";
                        whereStatementNeeded = true;
                    }
                    else if (!compid.Equals("All") && mid.Equals("All"))
                    {
                        oString = "SELECT isClosed FROM Opportunity JOIN Technician ON Opportunity.tid = Technician.username JOIN Manager ON Technician.mid = Manager.username WHERE Manager.compid = @compid";
                    }
                    else if (!compid.Equals("All") && !mid.Equals("All") && tid.Equals("All"))
                    {
                        oString = "SELECT isClosed FROM Opportunity JOIN Technician ON Opportunity.tid = Technician.username WHERE Technician.mid = @mid";
                    }
                    else
                    {
                        oString = "SELECT isClosed FROM Opportunity WHERE tid = @tid";
                    }
                    if (whereStatementNeeded && betweenStatementNeeded)
                    {
                        oString += " WHERE date BETWEEN @start AND @end";
                    }
                    if (betweenStatementNeeded && !whereStatementNeeded)
                    {
                        oString += " AND date BETWEEN @start AND @end";
                    }

                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("compid", compid);
                    oCmd.Parameters.AddWithValue("mid", mid);
                    oCmd.Parameters.AddWithValue("tid", tid);
                    if (betweenStatementNeeded)
                    {
                        oCmd.Parameters.AddWithValue("@start", startDate.Date);
                        oCmd.Parameters.AddWithValue("@end", endDate.Date);
                    }
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            if (oReader.GetBoolean(0))
                            {
                                oppsClosed++;
                            }
                            opps++;
                        }
                    }
                    if (compid.Equals("All"))
                    {
                        oString = "SELECT servicetype, cost FROM Jobs";
                        whereStatementNeeded = true;
                    }
                    else if (!compid.Equals("All") && mid.Equals("All"))
                    {
                        oString = "SELECT servicetype, cost FROM Jobs  JOIN Technician ON Jobs.tid = Technician.username JOIN Manager ON Technician.mid = Manager.username WHERE Manager.compid = @compid";
                    }
                    else if (!compid.Equals("All") && !mid.Equals("All") && tid.Equals("All"))
                    {
                        oString = "SELECT servicetype, cost FROM Jobs JOIN Technician ON Jobs.tid = Technician.username WHERE Technician.mid = @mid";
                    }
                    else
                    {
                        oString = "SELECT servicetype, cost FROM Jobs WHERE tid = @tid";
                    }
                    if (whereStatementNeeded && betweenStatementNeeded)
                    {
                        oString += " WHERE date BETWEEN @start AND @end";
                    }
                    if (betweenStatementNeeded && !whereStatementNeeded)
                    {
                        oString += " AND date BETWEEN @start AND @end";
                    }
                    oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("compid", compid);
                    oCmd.Parameters.AddWithValue("mid", mid);
                    oCmd.Parameters.AddWithValue("tid", tid);
                    if (betweenStatementNeeded)
                    {
                        oCmd.Parameters.AddWithValue("@start", startDate.Date);
                        oCmd.Parameters.AddWithValue("@end", endDate.Date);
                    }
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            jobType = oReader[0].ToString();
                            if (jobType.Contains("Equipment"))
                            {
                                total += oReader.GetDouble(1);
                            }
                        }
                    }
                }
                if (opps == 0)
                {
                    closingRate = 0;
                }
                else
                {
                    double oc = Convert.ToDouble(oppsClosed);
                    double o = Convert.ToDouble(opps);
                    closingRate = oc / o;
                    closingRate = 100 * closingRate;
                    if (oc == 0)
                    {
                        average = 0;
                    }
                    else
                    {
                        average = total / oc;
                    }
                }
                dt.Rows.Add(opps.ToString(), oppsClosed.ToString(), closingRate.ToString() + "%", "$" + average, "$" + total);
            }
            else
            {
                dt.Rows.Add("", "", "", "", "");
            }
            systemRevenueGridView.DataSource = dt;
            systemRevenueGridView.DataBind();
            //ViewState["systemDataTable"] = systemRevenueGridView;
        }

        private Bools fillServiceRevenueTable(bool v, Bools bools)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(" ", typeof(string));
            dt.Columns.Add("Demand Service", typeof(string));
            dt.Columns.Add("Service Agreement", typeof(string));
            dt.Columns.Add("Tune-up", typeof(string));
            dt.Columns.Add("Warranty", typeof(string));
            dt.Columns.Add("IAQ", typeof(string));
            dt.Columns.Add("Total", typeof(string));

            if (v)
            {
                double[] sales = { 0, 0, 0, 0, 0, 0 };
                int[] jobsRan = { 0, 0, 0, 0, 0, 0 };
                double[] avg = { 0, 0, 0, 0, 0, 0 };
                string[] saleStrings = new string[6];
                string[] avgStrings = new string[6];
                string servicetype;
                double cost;
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                bool validStartDate = DateTime.TryParse(startDateTextBox.Text, out startDate);
                bool validEndDate = DateTime.TryParse(endDateTextBox.Text, out endDate);
                bool whereStatementNeeded = false;
                bool betweenStatementNeeded = false;
                if (!((validEndDate && validStartDate) || (!validStartDate && !validEndDate)))
                {
                    dateLabel.Visible = true;
                    bools.safety = true;
                    return bools;
                }
                else if (validStartDate && validEndDate)
                {
                    betweenStatementNeeded = true;
                    bools.dateNeeded = true;
                }
                if (validEndDate)
                {
                    endDate = endDate.AddDays(1);
                }

                string compid = distributorDropDownList.SelectedValue.ToString();
                string mid = managerDropDownList.SelectedValue.ToString();
                string tid = technicianDropDownList.SelectedValue.ToString();

                var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    string oString = "";
                    if (compid.Equals("All"))
                    {
                        oString = "SELECT cost, servicetype FROM Jobs";
                        whereStatementNeeded = true;
                    }
                    else if (!compid.Equals("All") && mid.Equals("All"))
                    {
                        oString = "SELECT cost, servicetype FROM Jobs JOIN Technician ON Jobs.tid = Technician.username JOIN Manager ON Technician.mid = Manager.username WHERE Manager.compid = @compid";
                    }
                    else if (!compid.Equals("All") && !mid.Equals("All") && tid.Equals("All"))
                    {
                        oString = "SELECT cost, servicetype FROM Jobs JOIN Technician ON Jobs.tid = Technician.username WHERE Technician.mid = @mid";
                    }
                    else
                    {
                        oString = "SELECT cost, servicetype FROM Jobs WHERE tid = @tid";
                    }
                    if (whereStatementNeeded && betweenStatementNeeded)
                    {
                        oString += " WHERE date BETWEEN @start AND @end";
                    }
                    if (betweenStatementNeeded && !whereStatementNeeded)
                    {
                        oString += " AND date BETWEEN @start AND @end";
                    }

                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("compid", compid);
                    oCmd.Parameters.AddWithValue("mid", mid);
                    oCmd.Parameters.AddWithValue("tid", tid);
                    if (betweenStatementNeeded)
                    {
                        oCmd.Parameters.AddWithValue("@start", startDate.Date);
                        oCmd.Parameters.AddWithValue("@end", endDate.Date);
                    }

                    //oCmd.Parameters.AddWithValue("@uname", userName);
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            cost = oReader.GetDouble(0);
                            servicetype = oReader[1].ToString();
                            switch (servicetype)
                            {
                                case "Demand Service":
                                    sales[0] += cost;
                                    jobsRan[0]++;
                                    break;
                                case "Service Agreement - New":
                                    sales[1] += cost;
                                    jobsRan[1]++;
                                    break;
                                case "Service Agreement - Renewal":
                                    sales[1] += cost;
                                    jobsRan[1]++;
                                    break;
                                case "Tune-up":
                                    sales[2] += cost;
                                    jobsRan[2]++;
                                    break;
                                case "Warranty":
                                    sales[3] += cost;
                                    jobsRan[3]++;
                                    break;
                                case "IAQ":
                                    sales[4] += cost;
                                    jobsRan[4]++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    myConnection.Close();
                }
                sales[5] = sales[0] + sales[1] + sales[2] + sales[3] + sales[4];
                jobsRan[5] = jobsRan[0] + jobsRan[1] + jobsRan[2] + jobsRan[3] + jobsRan[4];
                for (int i = 0; i < avg.Length; i++)
                {
                    if (jobsRan[i] == 0)
                    {
                        avg[i] = 0;
                    }
                    else
                    {
                        avg[i] = sales[i] / jobsRan[i];
                    }
                    if (sales[i] == 0)
                    {
                        saleStrings[i] = "0";
                        avgStrings[i] = "0";
                    }
                    else
                    {
                        saleStrings[i] = Convert.ToDecimal(sales[i]).ToString("#.##");
                        avgStrings[i] = Convert.ToDecimal(avg[i]).ToString("#.##");
                    }
                }

                dt.Rows.Add("Sales", "$" + saleStrings[0], "$" + saleStrings[1], "$" + saleStrings[2], "$" + saleStrings[3], "$" + saleStrings[4], "$" + saleStrings[5]);
                dt.Rows.Add("Jobs Ran", jobsRan[0].ToString(), jobsRan[1].ToString(), jobsRan[2].ToString(), jobsRan[3].ToString(), jobsRan[4].ToString(), jobsRan[5].ToString());
                dt.Rows.Add("Avg/Job", "$" + avgStrings[0], "$" + avgStrings[1], "$" + avgStrings[2], "$" + avgStrings[3], "$" + avgStrings[4], "$" + avgStrings[5]);
            }
            else
            {
                dt.Rows.Add("Sales", "", "", "", "", "");
                dt.Rows.Add("Jobs Ran", "", "", "", "", "");
                dt.Rows.Add("Avg/Job", "", "", "", "", "");
            }
            serviceRevenueGridView.DataSource = dt;
            serviceRevenueGridView.DataBind();
            Session["theDataTable"] = serviceRevenueGridView;
            bools.setData(dt);
            return bools;
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
            dateLabel.Visible = false;
            Bools bools = new Bools();
            bools = fillServiceRevenueTable(true, bools);
            fillSystemRevenueTable(true, bools);
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

        protected void printServiceRevenueButton_Click(object sender, EventArgs e)
        {
            GridView serviceRevenueGridView2 = (GridView)Session["theDataTable"];

            Bools bools = new Bools();
            bools = fillServiceRevenueTable(true, bools);

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
    }
}