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
    public partial class Login : System.Web.UI.Page
    {

        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#"; 
        private static  byte[] iv = new byte[16];
        private static string generatedOTP;         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
            }

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredPassword = txtPassword.Text.Trim(); 
            string storedEncryptedPassword = string.Empty;  
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "Login");
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            storedEncryptedPassword = reader["Password"].ToString();

                           
                            string decryptedPassword = Decrypt(storedEncryptedPassword);

                           
                            if (decryptedPassword == enteredPassword)
                            {
                               
                                Session["UserId"] = reader["Id"];
                                Session["UserType"] = reader["UserType"].ToString();

                               
                                string userEmail = txtEmail.Text.Trim();
                                generatedOTP = GenerateOTP();
                                SendEmail(userEmail, generatedOTP);

                                
                                txtEmail.Visible = false;
                                txtPassword.Visible = false;
                                btnLogin.Visible = false;

                                lblMessage.Text = "OTP sent to your email. Please enter the OTP.";
                                string script = "showOTPSection();";
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowOTPSection", script, true);
                            }
                            else
                            {
                                lblMessage.Text = "Invalid email or password.";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Invalid email or password.";
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

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {

            if (txtOTP.Text == generatedOTP)
            {
                if (Session["UserType"].ToString() == "Admin")
                {
                    Response.Redirect("UserList.aspx");
                }
                else
                {
                    Response.Redirect("UserProfile.aspx");
                }
            }
        

            else
            {
                lblOTMessage.Text = "Invalid OTP. Please try again.";
            } 
            

        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); 
        }

        private void SendEmail(string toEmail, string otp)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ashishgaud579@gmail.com"); 
                mail.To.Add(toEmail);
                mail.Subject = "Your OTP Code";
                mail.Body = $"Your OTP code is: {otp}";
                mail.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) 
                {
                    smtp.Credentials = new NetworkCredential("ashishgaud579@gmail.com", "iixy otko miri dycz"); 
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {

            Response.Redirect("Register.aspx");
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }

        //protected void btnForgotPassword_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("ForgotPassword.aspx");
        //}


    }
}