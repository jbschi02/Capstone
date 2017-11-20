using System;
using System.Collections;
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
    public partial class ViewUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string check = Context.User.Identity.Name.ToString();
            if (string.IsNullOrWhiteSpace(check))
            {
                Response.Redirect("LoginPage.aspx");
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Technician", typeof(string));
            dt.Columns.Add("Total Revenue", typeof(string));
            dt.Rows.Add(" ", " ");
            techGridView1.DataSource = dt;
            techGridView1.DataBind();

            dt = new DataTable();
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Total Revenue", typeof(string));
            dt.Columns.Add("% of Total", typeof(string));
            dt.Rows.Add(" ", " ", " ");
            jobGridView.DataSource = dt;
            jobGridView.DataBind();
        }

        protected void loadData_Click(object sender, EventArgs e)
        {
            invalidDateLabel.Visible = false;
            string mid = Context.User.Identity.Name.ToString();
            string compid = getCompID(mid);
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            bool validStartDate = DateTime.TryParse(startDateTextBox.Text, out startDate);
            bool validEndDate = DateTime.TryParse(endDateTextBox.Text, out endDate);
            bool dateNeeded = false;
            bool validDate = true;

            if (!((validEndDate && validStartDate) || (!validStartDate && !validEndDate)))
            {
                invalidDateLabel.Visible = true;
                validDate = false;
            }
            else if (validStartDate && validEndDate)
            {
                dateNeeded = true;
                validDate = true;
            }
            if (validEndDate)
            {
                endDate = endDate.AddDays(1);
            }

            if (validDate)
            {
                fillTechTable(dateNeeded, compid, mid, startDate, endDate);
                fillJobTable(dateNeeded, compid, mid, startDate, endDate);
            }
        }

        private void fillJobTable(bool dateNeeded, string compid, string mid, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Total Revenue", typeof(string));
            dt.Columns.Add("% of Total", typeof(string));

            double[] totals = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] percentageDoubles = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string [] percentages = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            double cost = 0;
            string servicetype = "";
            int index = 0;
            double totalAll = 0;

            var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
            {
                SqlCommand myCmd = new SqlCommand();
                if (dateNeeded)
                {
                    myCmd = new SqlCommand("SELECT cost, servicetype FROM Jobs JOIN Technician ON Technician.username = Jobs.tid JOIN Manager ON Manager.username = Technician.mid JOIN Company ON Company.compname = Manager.compid WHERE Company.compname = @compid and date between @startDate and @endDate", myConnection);
                }
                else
                {
                    myCmd = new SqlCommand("SELECT cost, servicetype FROM Jobs JOIN Technician ON Technician.username = Jobs.tid JOIN Manager ON Manager.username = Technician.mid JOIN Company ON Company.compname = Manager.compid WHERE Company.compname = @compid", myConnection);
                }
                myCmd.Parameters.AddWithValue("@compid", compid);
                if (dateNeeded)
                {
                    myCmd.Parameters.AddWithValue("@startDate", startDate);
                    myCmd.Parameters.AddWithValue("@endDate", endDate);
                }
                myConnection.Open();
                using (SqlDataReader oReader = myCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        cost = oReader.GetDouble(0);
                        servicetype = oReader[1].ToString();
                        index = getIndex(cost, servicetype);
                        totals[index] += cost;
                        totalAll += cost;
                    }
                }
                myConnection.Close();
            }
            if (totalAll == 0)
            {
                totalAll = 1;
            }

            for (int i = 0; i < 13; i++)
            {
                percentageDoubles[i] = totals[i] / totalAll;
                percentageDoubles[i] *= 100;
                percentages[i] = String.Format("{0:0.00}", percentageDoubles[i]);
            }

            dt.Rows.Add("Demand Service", "$" + totals[0], percentages[0] + "%");
            dt.Rows.Add("Maintenance", "$" + totals[1], percentages[1] + "%");
            dt.Rows.Add("Tune-up", "$" + totals[2], percentages[2] + "%");
            dt.Rows.Add("IAQ", "$" + totals[3], percentages[3] + "%");
            dt.Rows.Add("Warranty", "$" + totals[4], percentages[4] + "%");
            dt.Rows.Add("Service Agreement - New", "$" + totals[5], percentages[5] + "%");
            dt.Rows.Add("Service Agreement - Renewal", "$" + totals[6], percentages[6] + "%");
            dt.Rows.Add("Air Handler", "$" + totals[7], percentages[7] + "%");
            dt.Rows.Add("AC & Coil", "$" + totals[8], percentages[8] + "%");
            dt.Rows.Add("Heat Pump", "$" + totals[9], percentages[9] + "%");
            dt.Rows.Add("Gas Furnace", "$" + totals[10], percentages[10] + "%");
            dt.Rows.Add("Packaged Unit", "$" + totals[11], percentages[11] + "%");
            dt.Rows.Add("Geothermal", "$" + totals[12], percentages[12] + "%");

            jobGridView.DataSource = dt;
            jobGridView.DataBind();
        }

        private int getIndex(double cost, string servicetype)
        {
            switch (servicetype)
            {
                case "Demand Service":
                    return 0;
                case "Maintenance":
                    return 1;
                case "Tune-up":
                    return 2;
                case "IAQ":
                    return 3;
                case "Warranty":
                    return 4;
                case "Service Agreement - New":
                    return 5;
                case "Service Agreement - Renewal":
                    return 6;
                case "Equipment - Air Handler":
                    return 7;
                case "Equipment - AC & Coil":
                    return 8;
                case "Equipment - Heat Pump System":
                    return 9;
                case "Equipment - Gas Furnace":
                    return 10;
                case "Equipment - Packaged Unit":
                    return 11;
                case "Equipment - Geothermal":
                    return 12;
                default:
                    return 0;
            }
        }

        private void fillTechTable(bool dateNeeded, string compid, string mid, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Technician", typeof(string));
            dt.Columns.Add("Total Revenue", typeof(string));

            ArrayList tids = new ArrayList();

            var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
            {
                SqlCommand myCmd = new SqlCommand();
                myCmd = new SqlCommand("SELECT Technician.username FROM Technician JOIN Manager ON Technician.mid = Manager.username JOIN Company ON Manager.compid = Company.compname WHERE Company.compname = @compid", myConnection);
                myCmd.Parameters.AddWithValue("@compid", compid);
                myConnection.Open();
                using (SqlDataReader oReader = myCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        tids.Add(oReader[0].ToString());
                    }
                }
                myConnection.Close();

                foreach (string tid in tids)
                {
                    double total = 0;
                    string name = "";
                    myCmd = new SqlCommand("SELECT fname + ' ' + lname as fullname FROM Technician WHERE username = @tid", myConnection);
                    myCmd.Parameters.AddWithValue("@tid", tid);
                    myConnection.Open();
                    using (SqlDataReader oReader = myCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            name = oReader[0].ToString();
                        }
                    }
                    myConnection.Close();
                    if (dateNeeded)
                    {
                        myCmd = new SqlCommand("SELECT cost FROM Jobs  JOIN Technician on Jobs.tid = Technician.username WHERE tid = @tid and date between @startDate and @endDate", myConnection);
                    }
                    else
                    {
                        myCmd = new SqlCommand("SELECT cost FROM Jobs JOIN Technician on Jobs.tid = Technician.username WHERE tid = @tid", myConnection);
                    }
                    myCmd.Parameters.AddWithValue("@tid", tid);
                    if (dateNeeded)
                    {
                        myCmd.Parameters.AddWithValue("@startDate", startDate);
                        myCmd.Parameters.AddWithValue("@endDate", endDate);
                    }
                    myConnection.Open();
                    using (SqlDataReader oReader = myCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            total += oReader.GetDouble(0);
                        }
                    }
                    myConnection.Close();

                    dt.Rows.Add(name, "$" + total);
                }
            }
            techGridView1.DataSource = dt;
            techGridView1.DataBind();
        }

        private string getCompID(string mid)
        {
            string compid = "";
            var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
            {
                SqlCommand myCmd = new SqlCommand("SELECT compid FROM Manager WHERE username = @mid", myConnection);
                myCmd.Parameters.AddWithValue("@mid", mid);
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

            return compid;
        }
    }
}