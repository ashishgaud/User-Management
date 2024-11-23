using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace UserManagement
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#"; 
        private static byte[] iv = new byte[16];
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Please enter your email address.";
                return;
            }

            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "SELECT Password FROM Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ForgotPassword");
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string password = reader["Password"].ToString();
                            SendPasswordByEmail(email, Decrypt(password));
                            //lblMessage.Text = "Your password has been sent to your email.";
                            Response.Write("<script>alert('Your password has been sent to your email.'); window.location='Login.aspx';</script>");
                            //Response.Redirect("Login.aspx");
                        }
                        else
                        {
                           // lblMessage.Text = "Email not found.";
                            Response.Write("<script>alert('Email not found.')</script>");
                        }
                    }
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

                //byte[] encryptedBytes = null;
                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);

                }

            }
        }

        private void SendPasswordByEmail(string email, string password)
        {
            try
            {
                string fromEmail = "ashishgaud579@gmail.com";  
                string fromPassword = "iixy otko miri dycz";  

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(email);
                mail.Subject = "Your Password";
                mail.Body = $"Your password is: {password}";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";  
                smtp.EnableSsl = true;
                smtp.Port = 587; 
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error sending email: " + ex.Message;
            }
        }
        //protected void btnLogin_Click(object sender, EventArgs e)
        //{

        //    Response.Redirect("Login.aspx");
        //}

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}