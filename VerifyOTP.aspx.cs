using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserManagement
{
    public partial class VerifyOTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["OTP"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string sessionOtp = Session["OTP"].ToString();
            if (txtOTP.Text == sessionOtp)
            {
                
                string userType = Session["UserType"].ToString();
                if (userType == "Admin")
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
                lblMessage.Text = "Invalid OTP. Please try again.";
            }
        }
    }
}