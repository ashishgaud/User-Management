using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace UserManagement
{

    public partial class Register : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                Session.Clear();

                Response.Cache.SetExpires(DateTime.UtcNow.AddSeconds(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                          
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
           
            if (fileUpload.HasFile)
            {
                string folderPath = Server.MapPath("~/ProfilePictures/");
                string fileName = Path.GetFileName(fileUpload.FileName);
                string filePath = Path.Combine(folderPath, fileName);
                fileUpload.SaveAs(filePath);
                imgProfile.ImageUrl = "~/ProfilePictures/" + fileName;
                imgProfile.Visible = true;
            }

         
            string sname = txtName.Text;
            if (System.Text.RegularExpressions.Regex.IsMatch(sname, @"\d"))
            {
                lblError.Text = "Name cannot contain numbers.";
                return;
            }
            else if (sname.Length > 30)
            {
                lblError.Text = "Name cannot exceed 30 characters.";
                return;
            }

            string emobile = txtMobile.Text;
            if (!System.Text.RegularExpressions.Regex.IsMatch(emobile, @"^\d{10}$"))
            {
                lblMobileError.Text = "Please enter a valid 10-digit mobile number.";
                return;
            }

            string Memail = txtEmail.Text;
            if (Memail.Length >= 30)
            {
                lblserror.Text = "Email cannot exceed 30 characters.";
                return;
            }


            string email = txtEmail.Text.Trim();
            string name = txtName.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            string gender = ddlGender.SelectedValue;
            string userType = ddlUserType.SelectedValue;
            string profilePicture = fileUpload.FileName;


            string   password = GenerateStrongPassword();
            //string password = txtPassword.Text;
            string encyptedpassword = Encrypt(password);
            //string hashedPassword = HashPassword(password);


            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "EXEC RegisterUser @Name, @Email, @Password, @Mobile, @Gender, @UserType, @ProfilePicture";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "RegisterUser");
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password",encyptedpassword);   
                    cmd.Parameters.AddWithValue("@Mobile", mobile);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@UserType", userType);
                    cmd.Parameters.AddWithValue("@ProfilePicture", profilePicture);

                    conn.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());

                    if (result == -1)
                    {
                        lblMessage.Text = "This email is already registered. Please use a different email.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else if (result == 1)
                       {
                        
                        SendEmailWithPassword(email,password);

                        lblMessage.Text = "Registration successful. Your password has been sent to your email.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        Session.Clear();
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Error during registration. Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        private static string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";

        private static byte[] iv = new byte[16];


        public string Encrypt(string plainText)
        {

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(secretKey);
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                //byte[] encryptedBytes = null;
                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);

                }

            }
        }


        private string GenerateStrongPassword()
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@$?_-";
            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();
            int length = 12;

            for (int i = 0; i < length; i++)
            {
                passwordBuilder.Append(validChars[random.Next(validChars.Length)]);
            }

            return passwordBuilder.ToString();
        }

        private void SendEmailWithPassword(string email, string password)
        {
           

            string fromEmail = "ashishgaud579@gmail.com"; 
            string fromPassword = "iixy otko miri dycz";

            string subject = "Your Registration Password";
            string body = $"Dear User,\n\nYour registration is successful. Your password is: {password}\n\nPlease log in using this password.";

        
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromEmail);
            message.To.Add(email);
            message.Subject = subject;
            message.Body = body;

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true;  
                smtp.Send(message); 
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            //txtPassword.Text = string.Empty;
            txtMobile.Text = string.Empty;
            ddlGender.SelectedIndex = 0;
            ddlUserType.SelectedIndex = 0;
            Session.Clear();

            ClientScript.RegisterStartupScript(this.GetType(), "ClearSessionStorage", "sessionStorage.clear();", true);


        }
        //private void ClearFields()
        //{

        //    txtName.Text = string.Empty;
        //    txtEmail.Text = string.Empty;
        //     txtPassword.Text = string.Empty;
        //    txtMobile.Text = string.Empty;
        //    ddlGender.SelectedIndex = 0;
        //    ddlUserType.SelectedIndex = 0;
        //}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");

        }


    }
  


}
