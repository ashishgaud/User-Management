using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserType"] == null || Session["UserType"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                BindGrid();
            }

        }

        private void BindGrid()
        {
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "EXEC BindGridUserList";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action","BindGridUserList");
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int userId = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect($"EditUser.aspx?id={userId}");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int userId = Convert.ToInt32(btn.CommandArgument);
            DeleteUser(userId);
            BindGrid();
        }

        private void DeleteUser(int userId)
        {
            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                //string query = "EXEC DeleteUserUserList @Id";
                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DeleteUser");
                    cmd.Parameters.AddWithValue("@Id", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnAdminProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminProfile.aspx");
        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "User List";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 12);
            gfx.DrawString("User List", new XFont("Arial", 16), XBrushes.Black, new XPoint(page.Width / 2, 40), XStringFormats.Center);

            int yPoint = 80;

            string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                
               // string query = "SELECT Name, Email, Mobile, Gender, UserType FROM Users WHERE UserType = 'User'";

                using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action","GetUserList");
                    cmd.Parameters.AddWithValue("@UserType", "User");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            gfx.DrawString("Name: " + reader["Name"].ToString(), font, XBrushes.Black, new XPoint(20, yPoint));
                            yPoint += 20;
                            gfx.DrawString("Email: " + reader["Email"].ToString(), font, XBrushes.Black, new XPoint(20, yPoint));
                            yPoint += 20;
                            gfx.DrawString("Mobile: " + reader["Mobile"].ToString(), font, XBrushes.Black, new XPoint(20, yPoint));
                            yPoint += 20;
                            gfx.DrawString("Gender: " + reader["Gender"].ToString(), font, XBrushes.Black, new XPoint(20, yPoint));
                            yPoint += 20;
                            gfx.DrawString("User Type: " + reader["UserType"].ToString(), font, XBrushes.Black, new XPoint(20, yPoint));
                            yPoint += 30; 

                            
                            if (yPoint > page.Height - 50) 
                            {
                                page = document.AddPage();
                                gfx = XGraphics.FromPdfPage(page);
                                yPoint = 40; 
                            }
                        }
                    }
                }
            }

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                byte[] pdfBytes = stream.ToArray();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=UserList.pdf");
                Response.BinaryWrite(pdfBytes);
                Response.End();
            }
        }

        protected void btnDownloadExcel_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
              
                var worksheet = excelPackage.Workbook.Worksheets.Add("User List");

             
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Email";
                worksheet.Cells[1, 3].Value = "Mobile";
                worksheet.Cells[1, 4].Value = "Gender";
                worksheet.Cells[1, 5].Value = "UserType";

               
                string connString = WebConfigurationManager.ConnectionStrings["UserManagementDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    
                    
                    using (SqlCommand cmd = new SqlCommand("UserManagementOperations", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Action", "GetUserList");
                        cmd.Parameters.AddWithValue("UserType","User");
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int row = 2; 
                            while (reader.Read())
                            {
                               
                                worksheet.Cells[row, 1].Value = reader["Name"].ToString();
                                worksheet.Cells[row, 2].Value = reader["Email"].ToString();
                                worksheet.Cells[row, 3].Value = reader["Mobile"].ToString();
                                worksheet.Cells[row, 4].Value = reader["Gender"].ToString();
                                worksheet.Cells[row, 5].Value = reader["UserType"].ToString();
                                row++; 
                            }
                        }
                    }
                }

             
                using (MemoryStream stream = new MemoryStream())
                {
                    excelPackage.SaveAs(stream);
                    byte[] excelBytes = stream.ToArray();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=UserList.xlsx");
                    Response.BinaryWrite(excelBytes);
                    Response.End();
                }
            }
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            FileUpload fileUpload = (FileUpload)row.FindControl("fileUpload");

            if (fileUpload.HasFile)
            {
                string userId = btn.CommandArgument;
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] allowedExtensions = { ".pdf", ".xlsx", ".jpg", ".jpeg" };

                if (allowedExtensions.Contains(fileExtension))
                {
                    string folderPath = Server.MapPath("~/UploadedFiles/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                  
                    string fileName = $"{userId}_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(fileUpload.FileName)}";
                    string filePath = Path.Combine(folderPath, fileName);

                    fileUpload.SaveAs(filePath);

                 
                    Session[$"UploadedFile_{userId}"] = filePath; 
                    //lblMessage.Text = "File uploaded successfully for user " + userId;
                    Response.Write("<script>alert('File uploaded successfully for user');</script>");
                }
                else
                {
                    //lblMessage.Text = "Only PDF, Excel, and JPG files are allowed.";
                    Response.Write("<script>alert('Only PDF, Excel, and JPG files are allowed.');</script>");
                }
            }
            else
            {
                //lblMessage.Text = "Please select a file to upload.";
                Response.Write("<script>alert('Please select a file to upload.');</script>");
            }
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string userId = btn.CommandArgument;

          
            string filePath = Session[$"UploadedFile_{userId}"]?.ToString();

          
            if (string.IsNullOrEmpty(filePath))
            {
                string folderPath = Server.MapPath("~/UploadedFiles/");
                
                string[] existingFiles = Directory.GetFiles(folderPath, $"{userId}_*")
                                                   .OrderByDescending(f => new FileInfo(f).CreationTime)
                                                   .ToArray();
                if (existingFiles.Length > 0)
                {
                    filePath = existingFiles[0]; 
                }
            }

            
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                Response.ContentType = GetContentType(filePath);
                Response.AddHeader("content-disposition", $"inline;filename={fileName}");
                Response.TransmitFile(filePath);
                Response.End();
            }
            else
            {
                lblMessage.Text = "No file available for viewing for user " + userId + ". Please import a file first.";
            }
        }


        private string GetContentType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                default:
                    return "application/octet-stream";
            }
        }




    }
}
