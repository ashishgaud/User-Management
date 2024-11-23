using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Messaging;

namespace UserManagement
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#"; // Encryption key
        private static byte[] iv = new byte[16];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtOldPassword.Text.Trim();
            string storedEncryptedPassword = string.Empty;
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmNewPassword.Text.Trim();

            if (newPassword != confirmPassword)
            {
                lblMessage.Text = "New password and confirm password do not match.";
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open(); // Open connection before using it

                // First command: Get current password
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GetPassword");
                    cmd.Parameters.AddWithValue("@Id", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            storedEncryptedPassword = reader["Password"].ToString();
                            string decryptedPassword = Decrypt(storedEncryptedPassword);

                            if (currentPassword == decryptedPassword)
                            {
                                string encryptedNewPassword = Encrypt(newPassword);

                                // Close the reader before executing another command
                                reader.Close();

                                // Second command: Update password
                                using (SqlCommand updateCmd = new SqlCommand("UserManagementOperations", conn))
                                {
                                    updateCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    updateCmd.Parameters.AddWithValue("@Action", "ChangePassword2");
                                    updateCmd.Parameters.AddWithValue("@Id", userId);
                                    updateCmd.Parameters.AddWithValue("@NewPassword", encryptedNewPassword);

                                    int rowsAffected = updateCmd.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        lblMessage.Text = "Password changed successfully.";
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Error changing password. Please try again.";
                                    }
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Current password is incorrect.";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Error in fetching password.";
                        }
                    }
                }
            }
        }

        public string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
       
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["UserType"] != null)
            {
                string userType = Session["UserType"].ToString();

                if (userType == "Admin")
                {
                    Response.Redirect("AdminProfile.aspx");
                }
                else
                {
                    Response.Redirect("UserProfile.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}
