using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagement
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                int userId = Convert.ToInt32(Request.QueryString["id"]);
                LoadUserData(userId);
            }
        }

        private void LoadUserData(int userId)
        {
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "EXEC LoadUsersEditUsers @Id";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "LoadUsersEdit");
                    cmd.Parameters.AddWithValue("@Id", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        hdnUserId.Value = reader["Id"].ToString();
                        txtName.Text = reader["Name"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtMobile.Text = reader["Mobile"].ToString();
                        ddlGender.SelectedValue = reader["Gender"].ToString();
                        ddlUserType.SelectedValue = reader["UserType"].ToString();

                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) ||
              string.IsNullOrEmpty(txtMobile.Text) ||
              ddlGender.SelectedValue == "0" ||
              ddlUserType.SelectedValue == "")
            {
                lblError.Text = "All fields are required.";
                return;
            }

            if (!Regex.IsMatch(txtMobile.Text, @"^\d{10}$"))
            {
                lblError.Text = "Please enter a valid 10-digit mobile number.";
                return;
            }
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "EXEC UpdateUserDetails @Id, @Name, @Mobile, @Gender, @UserType";  
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateUserDetails");
                    cmd.Parameters.AddWithValue("@Id", hdnUserId.Value);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@userType", ddlUserType.SelectedValue);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            Response.Redirect("UserList.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx");
        }
    }
}  