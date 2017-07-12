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
    public partial class ProgressTracker : System.Web.UI.Page
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

                    myReader.Close();
                }

                technicianDropDownList.Items.Insert(0, "All");
                managerDropDownList.Items.Insert(0, "All");
                distributorDropDownList.Items.Insert(0, "All");
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
            string tid = technicianDropDownList.SelectedValue.ToString();
            double[] goal = { 0, 0, 0 };
            double[] actual = { 0, 0, 0 };
            double[] percentage = { 0, 0, 0 };
            double[] width = { 0, 0, 0 };
            Label[] actualLabels = { dailyGoalActualLabel, monthlyGoalActualLabel, yearlyGoalActualLabel };
            Label[] goalLabels = { dailyGoalRemaingLabel, monthlyGoalRemainingLabel, yearlGoalRemainingLabel };
            Label[] percentageLabels = { dailyPercentageLabel, percentageMonthlyLabel, percentageYearlyLabel };
            double remaining = 0;

            if (tid.Equals("All"))
            {
                needTechLabel.Visible = true;
            }
            else
            {
                needTechLabel.Visible = false;
                userGoalsLabel.Text = "Progress for " + technicianDropDownList.SelectedItem.ToString();
                userGoalsLabel.Visible = true;
                var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();
                for (int i = 0; i < 3; i++)
                {
                    using (SqlConnection myConnection = new SqlConnection(con))
                    {
                        string fString = "SELECT " + dbStringBuilder(i) + " FROM Goals WHERE tid = @tid";
                        SqlCommand fCmd = new SqlCommand(fString, myConnection);
                        fCmd.Parameters.AddWithValue("@tid", tid);
                        myConnection.Open();
                        using (SqlDataReader oReader = fCmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                goal[i] = oReader.GetDouble(0);
                                actual[i] = oReader.GetDouble(1);
                            }
                            oReader.Close();
                        }
                        myConnection.Close();
                    }
                }
                for (int j = 0; j < 3; j++)
                {
                    actualLabels[j].Text = "Actual: $" + actual[j].ToString();
                    remaining = goal[j] - actual[j];
                    goalLabels[j].Text = "Goal: $" + goal[j].ToString();
                    goalLabels[j].Visible = true;
                    actualLabels[j].Visible = true;

                    percentage[j] = actual[j] / goal[j];

                    if (percentage[j] >= 1)
                    {
                        width[j] = 850;
                        percentage[j] = 1;
                    }
                    else if (percentage[j] < 1)
                    {
                        width[j] = percentage[j] * 850;
                    }
                    percentage[j] = 100 * percentage[j];
                    percentage[j] = Math.Round(percentage[j], MidpointRounding.AwayFromZero);
                    percentageLabels[j].Text = percentage[j].ToString() + "%";
                    percentageLabels[j].Visible = true;
                }

                rcorners1.Style["width"] = width[0].ToString() + "px";
                rcorners4.Style["width"] = width[1].ToString() + "px";
                rcorners6.Style["width"] = width[2].ToString() + "px";
                rcorners1.Visible = true;
                rcorners2.Visible = true;
                rcorners3.Visible = true;
                rcorners4.Visible = true;
                rcorners5.Visible = true;
                rcorners6.Visible = true;
            }
        }

        private string dbStringBuilder(int i)
        {
            if (i == 0)
            {
                return "daily, dailyactual";
            }
            else if (i == 1)
            {
                int selectedMonth = monthDropDownList.SelectedIndex + 1;
                switch (selectedMonth)
                {
                    case 1:
                        return "jan, janActual";
                    case 2:
                        return "feb, febActual";
                    case 3:
                        return "mar, marActual";
                    case 4:
                        return "apr, aprActual";
                    case 5:
                        return "may, mayActual";
                    case 6:
                        return "jun, junActual";
                    case 7:
                        return "jul, julActual";
                    case 8:
                        return "aug, augActual";
                    case 9:
                        return "sep, sepActual";
                    case 10:
                        return "oct, octActual";
                    case 11:
                        return "nov, novActual";
                    case 12:
                        return "dec, decActual";
                    default:
                        return "jan, janActual";
                }
            }
            else
            {
                return "ytd, ytdactual";
            }
        }
    }
}