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
    public partial class SetGoals : System.Web.UI.Page
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
                    SqlCommand myCmd = new SqlCommand("SELECT compid FROM Manager WHERE username = @mid", myConnection);
                    myCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name.ToString());
                    myConnection.Open();
                    SqlDataReader myReader = myCmd.ExecuteReader();

                    //Set up the data binding.
                    distributorDropDownList.DataSource = myReader;
                    distributorDropDownList.DataTextField = "compid";
                    distributorDropDownList.DataValueField = "compid";
                    distributorDropDownList.DataBind();
                    myReader.Close();
                    myConnection.Close();

                    myCmd = new SqlCommand("SELECT fname + ' ' + lname AS fullname FROM Manager WHERE username = @mid", myConnection);
                    myCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name.ToString());
                    myConnection.Open();
                    myReader = myCmd.ExecuteReader();

                    managerDropDownList.DataSource = myReader;
                    managerDropDownList.DataTextField = "fullname";
                    managerDropDownList.DataValueField = "fullname";
                    managerDropDownList.DataBind();
                    myReader.Close();
                    myConnection.Close();

                    myCmd = new SqlCommand("SELECT username,  fname + ' ' + lname AS fullname FROM Technician WHERE mid = @mid", myConnection);
                    myCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name.ToString());
                    myConnection.Open();
                    myReader = myCmd.ExecuteReader();

                    technicianDropDownList.DataSource = myReader;
                    technicianDropDownList.DataTextField = "fullname";
                    technicianDropDownList.DataValueField = "username";
                    technicianDropDownList.DataBind();
                    myReader.Close();


                    myCmd = new SqlCommand("SELECT type FROM JobTypes", myConnection);
                    myReader = myCmd.ExecuteReader();

                    myReader.Close();
                }

                //technicianDropDownList.Items.Insert(0, "All");
                //managerDropDownList.Items.Insert(0, "All");
                //distributorDropDownList.Items.Insert(0, "All");
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
            successLabel.Visible = false;
            userGoalsLabel.Visible = false;
            if (technicianDropDownList.SelectedValue.ToString().Equals("All"))
            {
                needTechLabel.Visible = true;
            }
            else
            {
                userGoalsLabel.Text = "Editing Goals for " + technicianDropDownList.SelectedItem.ToString();
                userGoalsLabel.Visible = true;
                needTechLabel.Visible = false;

                double[] doubleMonths = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                TextBox[] textboxes = { janGoalsTextBox, febGoalsTextBox, marGoalsTextBox, TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8, TextBox9 };

                var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    string oString = "SELECT jan, feb, mar, apr, may, jun, jul, aug, sep, oct, nov, dec FROM Goals WHERE tid = @tid";
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@tid", technicianDropDownList.SelectedValue.ToString());
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                doubleMonths[i] = oReader.GetDouble(i); 
                            }
                        }
                        oReader.Close();
                    }
                    myConnection.Close();
                }
                for (int j = 0; j < 12; j++)
                {
                    textboxes[j].Text = doubleMonths[j].ToString();
                }
                reverse(true);
            }
        }
        protected void cancelEditing(object sender, EventArgs e)
        {
            reverse(false);
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            string check = technicianDropDownList.SelectedValue.ToString();
            Label[] months = { janLabel, febLabel, marLabel, Label4, Label6, Label7, Label8, Label9, Label10, Label11, Label12, Label13 };
            TextBox[] textboxes = { janGoalsTextBox, febGoalsTextBox, marGoalsTextBox, TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8, TextBox9 };
            double[] doubleMonths = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < 12; i++)
            {
                doubleMonths[i] = Convert.ToDouble(textboxes[i].Text);
            }

            if (check.Equals("All"))
            {
                needTechLabel.Visible = true;
            }
            else
            {
                needTechLabel.Visible = false;

                var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    string fString = "UPDATE Goals SET jan = @January, feb = @February, mar = @March, apr = @April, may = @May, jun = @June, jul = @July, aug = @August, sep = @September, oct = @October, nov = @November, dec = @December WHERE tid = @tid";
                    SqlCommand fCmd = new SqlCommand(fString, myConnection);
                    fCmd.Parameters.AddWithValue("@tid", check);
                    for (int j = 0; j < 12; j++)
                    {
                        fCmd.Parameters.AddWithValue("@" + months[j].Text, doubleMonths[j]);
                    }

                    myConnection.Open();
                    fCmd.ExecuteNonQuery();
                    myConnection.Close();
                }

                reverse(false);
                successLabel.Visible = true;
                userGoalsLabel.Visible = false;
            }
        }

        protected void reverse(bool vis)
        {
            Label[] months = { janLabel, febLabel, marLabel, Label4, Label6, Label7, Label8, Label9, Label10, Label11, Label12, Label13 };
            TextBox[] textboxes = { janGoalsTextBox, febGoalsTextBox, marGoalsTextBox, TextBox1, TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8, TextBox9 };
            foreach (Label l in months)
            {
                l.Visible = vis;
            }
            foreach (TextBox t in textboxes)
            {
                t.Visible = vis;
            }

            cancelButton.Visible = vis;
            saveButton.Visible = vis;
        }
    }
}