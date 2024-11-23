using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using OfficeOpenXml;
using System.Net.Mime;

namespace UserManagement
{
    public partial class AdminProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                BindAdminDetails();
            }
        }

        private void BindAdminDetails()
        {
            int adminId = Convert.ToInt32(Session["UserId"]);
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //String query = "EXEC LoadUsers @Id";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "LoadUser");
                    cmd.Parameters.AddWithValue("@Id", adminId);
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

       
        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateUser.aspx");
        }

        protected void btnManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx"); 
        }

      /*protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx");
        }
      */

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }
        protected void btnDownloadProfile_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Admin Profile";

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Times New Roman", 20);

            double centerX = page.Width / 2;

            string profileImageFileName = Path.GetFileName(imgProfilePicture.ImageUrl);
            string profileImagePath = Server.MapPath("~/profilepictures/" + profileImageFileName);

            if (File.Exists(profileImagePath))
            {
                XImage image = XImage.FromFile(profileImagePath);
                
                double imageWidth = 150; 
                double imageHeight = 150; 
                double imageX = (page.Width - imageWidth) / 2; 
                gfx.DrawImage(image, imageX, 20, imageWidth, imageHeight); 
            }
            else
            {
                gfx.DrawString("Profile picture not found.", font, XBrushes.Red, new XPoint(centerX, 20), XStringFormats.Center);
            }

           
            double detailsYPosition = 180; 
            gfx.DrawString("Admin Profile", font, XBrushes.Black, new XPoint(centerX, detailsYPosition), XStringFormats.Center);
            gfx.DrawString("Name: " + lblName.Text, font, XBrushes.Black, new XPoint(centerX, detailsYPosition + 40), XStringFormats.Center);
            gfx.DrawString("Email: " + lblEmail.Text, font, XBrushes.Black, new XPoint(centerX, detailsYPosition + 80), XStringFormats.Center);
            gfx.DrawString("Mobile: " + lblMobile.Text, font, XBrushes.Black, new XPoint(centerX, detailsYPosition + 120), XStringFormats.Center);
            gfx.DrawString("Gender: " + lblGender.Text, font, XBrushes.Black, new XPoint(centerX, detailsYPosition + 160), XStringFormats.Center);
            gfx.DrawString("User Type: " + lblUserType.Text, font, XBrushes.Black, new XPoint(centerX, detailsYPosition + 200), XStringFormats.Center);

            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            byte[] pdfBytes = stream.ToArray();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=AdminProfile.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }

        //protected void btnDownloadProfileExcel_Click(object sender, EventArgs e)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using (ExcelPackage excelPackage = new ExcelPackage())
        //    {
        //        var worksheet = excelPackage.Workbook.Worksheets.Add("Admin Profile");

        //        worksheet.Cells[1, 1].Value = "Admin Profile";
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
        //        Response.AddHeader("content-disposition", "attachment; filename=AdminProfile.xlsx");

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