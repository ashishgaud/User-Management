using OfficeOpenXml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace UserManagement
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                BindUserDetails();
                LoadOtherUsers();
            }
        }

        private void BindUserDetails()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "EXEC GetUserDetailsById @Id";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GetUserDetailsById");
                    cmd.Parameters.AddWithValue("@Id", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblName.Text = reader["Name"].ToString();
                            lblEmail.Text = reader["Email"].ToString();
                            lblMobile.Text = reader["Mobile"].ToString();
                            lblGender.Text = reader["Gender"].ToString();
                            lblUserType.Text = reader["UserType"].ToString();
                            string profilePicturePath = reader["ProfilePicture"].ToString();
                            imgProfilePicture.ImageUrl = "~/profilepictures/" + profilePicturePath;
                           
                        }
                    }
                }
            }
        }

      

        private void LoadOtherUsers()
        {
            int currentUserId = Convert.ToInt32(Session["UserId"]);
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "GetOtherUsers");
                    cmd.Parameters.AddWithValue("@UserType", "User");
                    cmd.Parameters.AddWithValue("@Id", currentUserId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        rptUsers.DataSource = reader;
                        rptUsers.DataBind();
                    }
                }
            }
        }
      

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateUser.aspx");


        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");

        }
        protected void btnDownloadProfilePDF_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "User Profile";

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            
            XFont font = new XFont("Arial", 20);

            
            gfx.DrawString("User Profile", font, XBrushes.Black, new XPoint(page.Width / 2, 40), XStringFormats.Center);
            gfx.DrawString("Name: " + lblName.Text, font, XBrushes.Black, new XPoint(20, 100));
            gfx.DrawString("Email: " + lblEmail.Text, font, XBrushes.Black, new XPoint(20, 140));
            gfx.DrawString("Mobile: " + lblMobile.Text, font, XBrushes.Black, new XPoint(20, 180));
            gfx.DrawString("Gender: " + lblGender.Text, font, XBrushes.Black, new XPoint(20, 220));
            gfx.DrawString("User Type: " + lblUserType.Text, font, XBrushes.Black, new XPoint(20, 260));



         
            string profileImageFileName = Path.GetFileName(imgProfilePicture.ImageUrl);
            string profileImagePath = Server.MapPath("~/profilepictures/" + profileImageFileName);

            if (File.Exists(profileImagePath))
            {
                XImage image = XImage.FromFile(profileImagePath);
                gfx.DrawImage(image, (page.Width - 150) / 2, 300, 150, 150); 
            }

           
            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] pdfBytes = stream.ToArray();

           
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=UserProfile.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }

        //protected void btnDownloadProfileExcel_Click(object sender, EventArgs e)
        //{
            
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using (ExcelPackage excelPackage = new ExcelPackage())
        //    {
        //        var worksheet = excelPackage.Workbook.Worksheets.Add("User Profile");

        //        worksheet.Cells[1, 1].Value = "User Profile";
        //        worksheet.Cells[1, 1].Style.Font.Bold = true;
        //        worksheet.Cells[1, 1].Style.Font.Size = 20;

        //        worksheet.Cells[3, 1].Value = "Name:";
        //        worksheet.Cells[3, 2].Value = lblName.Text;
        //        worksheet.Cells[4, 1].Value = "Email:";
        //        worksheet.Cells[4, 2].Value = lblEmail.Text;
        //        worksheet.Cells[5, 1].Value = "Mobile:";
        //        worksheet.Cells[5, 2].Value = lblMobile.Text;
        //        worksheet.Cells[6, 1].Value = "Gender:";
        //        worksheet.Cells[6, 2].Value = lblGender.Text;
        //        worksheet.Cells[7, 1].Value = "User Type:";
        //        worksheet.Cells[7, 2].Value = lblUserType.Text;

        //        string profileImageFileName = Path.GetFileName(imgProfilePicture.ImageUrl);
        //        string profileImagePath = Server.MapPath("~/profilepictures/" + profileImageFileName);

        //        if (File.Exists(profileImagePath))
        //        {
        //            var image = new FileInfo(profileImagePath);
        //            var excelImage = worksheet.Drawings.AddPicture("ProfilePicture", image);
        //            excelImage.SetPosition(1, 0, 2, 0);  
        //            excelImage.SetSize(100, 100); 
        //        }

        //        Response.Clear();
        //        Response.ContentType = MediaTypeNames.Application.Octet;
        //        Response.AddHeader("content-disposition", "attachment; filename=UserProfile.xlsx");

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            excelPackage.SaveAs(memoryStream);
        //            memoryStream.WriteTo(Response.OutputStream);
        //        }

        //        Response.End();

        //    }
        //}
    }
}