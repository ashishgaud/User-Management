using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagement
{
    public partial class UpdateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserData();
            }

           
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.Cache.SetNoStore();

            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                ddlUserType.Enabled = false;
            }
        }

        private void LoadUserData()
        {
            if (Session["UserId"] == null)
            {
               
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = (int)Session["UserId"];
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "SELECT Name, Email, Mobile, Gender, UserType,ProfilePicture  FROM Users WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateUserProfile1");
                    cmd.Parameters.AddWithValue("@Id", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtMobile.Text = reader["Mobile"].ToString();
                            ddlGender.SelectedValue = reader["Gender"].ToString();
                            ddlUserType.SelectedValue = reader["UserType"].ToString();
                            imgProfilePicture.ImageUrl = "~/ProfilePictures/" + reader["ProfilePicture"].ToString(); 

                        }
                    }
                }
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtMobile.Text) ||
                ddlGender.SelectedValue == "0" ||
                string.IsNullOrEmpty(ddlUserType.SelectedValue))
            {
                lblError.Text = "All fields are required.";
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "Please enter a valid email address.";
                return;
            }

            if (!Regex.IsMatch(txtMobile.Text, @"^\d{10}$"))
            {
                lblError.Text = "Please enter a valid 10-digit mobile number.";
                return;
            }

            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = (int)Session["UserId"];
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;
            string profilePicture = fileUpload.HasFile ? fileUpload.FileName : imgProfilePicture.ImageUrl.Replace("~/ProfilePictures/", "");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn) )
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateUserProfile");
                    cmd.Parameters.AddWithValue("@Id", userId);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@Gender", ddlGender.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserType", ddlUserType.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProfilePicture", profilePicture);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        if (fileUpload.HasFile)
                        {
                            string savePath = Server.MapPath("~/ProfilePictures/") + profilePicture;
                            fileUpload.SaveAs(savePath);
                        }
          
                        Response.Write("<script>alert('Profile updated successfully.');</script>");
                        Response.Redirect(Session["UserType"].ToString() == "Admin" ? "AdminProfile.aspx" : "UserProfile.aspx");
                    }
                    else
                    {
                        lblError.Text = "Update failed. Please try again.";
                    }
                }
            }
        }
        private void UpdateProfilePicture(int userId, string fileName)
        {
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "UPDATE Users SET ProfilePicture = @ProfilePicture WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateProfilePicture");
                    cmd.Parameters.AddWithValue("@ProfilePicture", fileName);
                    cmd.Parameters.AddWithValue("@Id", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Text = string.Empty;
            txtEmail.Text= string.Empty;    
            txtMobile.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            ddlUserType.SelectedIndex = 0;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
      
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                
                Response.Redirect("AdminProfile.aspx");
            }
            else
            {
                Response.Redirect("UserProfile.aspx");
            }
        }

    }
}