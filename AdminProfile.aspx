<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminProfile.aspx.cs" Inherits="UserManagement.AdminProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Profile</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <style>
        body {
            background-color: #dee2e6;
            font-family: 'Arial', sans-serif;
        }
        .profile-header {
            background-color: #007bff;
            color: white;
            padding: 20px;
            border-bottom: 1px solid #dee2e6;
            text-align: center;
            position: relative;
        }
        .profile-picture {
            border-radius: 50%;
            width: 150px;
            height: 150px;
            object-fit: cover;
            border: 4px solid white;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.5);
            position: absolute;
            top: 100px;
            left: 50%;
            transform: translateX(-50%);
        }
        .profile-info {
            margin-top: 70px;
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
        }
        .info-label {
            font-weight: bold;
            margin-right: 5px;
        }
        .button-group {
            margin-top: 20px;
            text-align: center;
        }
        .btn {
            margin: 0 10px;
            padding: 10px 20px;
        }
        .footer {
            background-color: #faeeee;
            text-align: center;
            padding: 10px;
            position: fixed;
            bottom: 0;
            width: 100%;
        }
        .social-icons {
            margin-top: 20px;
        }
        .social-icons a {
            color: #007bff;
            font-size: 24px;
            margin: 0 10px;
            transition: color 0.3s;
        }
        .social-icons a:hover {
            color: #0056b3;
        }
        @media (max-width: 1000x){
            .profile-header {
                text-align: center;
            }
            .profile-picture {
                top: 90px;
            }
        }
        .auto-style1 {
            color: white;
            text-align: center;
            position: relative;
            left: 3px;
            top: -54px;
            height: 2px;
            padding: 20px;
            background-color: white;
        }
        .auto-style2 {
            border-radius: 50%;
            width: 222px;
            height: 210px;
            object-fit: cover;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.5);
            position: absolute;
            top: 100px;
            left: 52%;
            transform: translateX(-50%);
            border: 4px solid white;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="auto-style1">
            
                <asp:Image ID="imgProfilePicture" runat="server" CssClass="auto-style2" />
            </div>
            <div class="profile-info">
                <p><span class="info-label">Name:</span> <asp:Label ID="lblName" runat="server"></asp:Label></p>
                <p><span class="info-label">Email:</span> <asp:Label ID="lblEmail" runat="server"></asp:Label></p>
                <p><span class="info-label">Mobile:</span> <asp:Label ID="lblMobile" runat="server"></asp:Label></p>
                <p><span class="info-label">Gender:</span> <asp:Label ID="lblGender" runat="server"></asp:Label></p>
                <p><span class="info-label">User Type:</span> <asp:Label ID="lblUserType" runat="server"></asp:Label></p>

                <div class="social-icons">
                    <h5>Connect with me:</h5>
                    <a href="#" title="Facebook"><i class="fab fa-facebook"></i></a>
                    <a href="#" title="Twitter"><i class="fab fa-twitter"></i></a>
                    <a href="#" title="Instagram"><i class="fab fa-instagram"></i></a>
                    <a href="#" title="LinkedIn"><i class="fab fa-linkedin"></i></a>
                </div>
            </div>
            <div class="button-group">
                <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdateProfile_Click" />
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-secondary" OnClick="btnChangePassword_Click" />
                <asp:Button ID="btnManageUsers" runat="server" Text="Manage Users" CssClass="btn btn-secondary" OnClick="btnManageUsers_Click" />
<%--                <asp:Button ID="btnDownloadPDF" runat="server" Text="Download Profile as PDF" OnClick="btnDownloadPDF_Click" CssClass="btn btn-primary" />--%>
<%--                <asp:Button ID="btnDownloadExcel" runat="server" Text="Download Profile as Excel" OnClick="btnDownloadExcel_Click" CssClass="btn btn-secondary" />--%>
                <asp:Button ID="btnDownloadProfile" runat="server" Text="Download Profile" OnClick="btnDownloadProfile_Click" CssClass="btn btn-primary" />
<%--                <asp:Button ID="btnDownloadProfileExcel" runat="server" Text="Download Excel" OnClick="btnDownloadProfileExcel_Click" />--%>


            </div>
        </div>
      
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
