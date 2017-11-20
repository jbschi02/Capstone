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
    public partial class DeleteEmployee : System.Web.UI.Page
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
                fillTable();
            }
        }

        private void fillTable()
        {
            deleteButton.Visible = true;
            confirmButton.Visible = false;
            cancelButton.Visible = false;

            DataTable dt = new DataTable();
            dt.Columns.Add("Technician", typeof(string));
            dt.Columns.Add("Technician ID", typeof(string));


            string username = "";
            string fullname = "";
            var myConn = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            using (System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(myConn))
            {
                SqlCommand myCmd = new SqlCommand("SELECT username, fname + ' ' + lname as fullname FROM Technician WHERE mid = @mid", myConnection);
                myCmd.Parameters.AddWithValue("@mid", Context.User.Identity.Name.ToString());
                myConnection.Open();

                using (SqlDataReader oReader = myCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        username = oReader[0].ToString();
                        fullname = oReader[1].ToString();
                        dt.Rows.Add(fullname, username);
                    }
                }
                myConnection.Close();
            }


            deleteGridView.DataSource = dt;
            deleteGridView.Enabled = true;
            deleteGridView.DataBind();
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            int c = 0;
            ArrayList technames = new ArrayList();
            ArrayList tids = new ArrayList();
            pickOneLabel.Visible = false;
            successfulDeleteLabel.Visible = false;
            foreach (GridViewRow gvrow in deleteGridView.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                if (chk.Checked)
                {
                    c++;
                    technames.Add(gvrow.Cells[1].Text);
                    tids.Add(gvrow.Cells[2].Text);
                }
            }

            if (c == 0)
            {
                pickOneLabel.Visible = true;
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Technician", typeof(string));
            dt.Columns.Add("Technician ID", typeof(string));

            for (int i = 0; i < tids.Count; i++)
            {
                dt.Rows.Add(technames[i], tids[i]);
            }

            deleteGridView.DataSource = dt;
            deleteGridView.DataBind();
            deleteButton.Visible = false;
            confirmButton.Visible = true;
            cancelButton.Visible = true;
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            fillTable();
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {
            string tid = "";
            var con = ConfigurationManager.ConnectionStrings["ServiceTracker"].ToString();

            foreach (GridViewRow gvrow in deleteGridView.Rows)
            {
                tid = (gvrow.Cells[2].Text);
                using (SqlConnection myConnection = new SqlConnection(con))
                {
                    string oString = "DELETE FROM Users WHERE Username = @tid";
                    myConnection.Open();
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@tid", tid);
                    oCmd.ExecuteNonQuery();

                    oString = "DELETE FROM Technician  WHERE username = @tid";
                    oCmd = new SqlCommand(oString, myConnection);
                    oCmd.Parameters.AddWithValue("@tid", tid);
                    oCmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            successfulDeleteLabel.Visible = true;
            fillTable();
        }
    }
}