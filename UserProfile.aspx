<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="UserManagement.UserProfile" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Profile</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />
    <style>
        body {
            background-color: #f8f9fa;
            font-family: 'Arial', sans-serif;
        }
        .profile-header {
            background-color: #ffffff;
            color: #121111;
            padding: 30px;
            text-align: center;
            border-radius: 10px;
            position: relative;
        }
        .profile-picture {
            border-radius: 50%;
            width: 150px;
            height: 150px;
            object-fit: cover;
            border: 5px solid white;
            margin-top: -75px;
        }
        .card {
            margin-top: 20px;
            border: none;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
        }
        .table thead {
            background-color: #007bff;
            color: white;
        }
        .table tbody tr:hover {
            background-color: #f1f1f1;
        }
        .profile-picture-table {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            object-fit: cover;
        }
        .button-group {
            margin-top: 20px;
            text-align: center;
        }
        .btn-custom {
            margin: 5px;
            min-width: 150px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="profile-header">
                <asp:Image ID="imgProfilePicture" runat="server" CssClass="profile-picture" />
                <h2><asp:Label ID="lblName" runat="server"></asp:Label></h2>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <p><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server"></asp:Label></p>
                            <p><strong>Mobile:</strong> <asp:Label ID="lblMobile" runat="server"></asp:Label></p>
                        </div>
                        <div class="col-md-6">
                            <p><strong>Gender:</strong> <asp:Label ID="lblGender" runat="server"></asp:Label></p>
                            <p><strong>User Type:</strong> <asp:Label ID="lblUserType" runat="server"></asp:Label></p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive mt-4">
                <asp:Repeater ID="rptUsers" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Profile Picture</th>
                                    <th>Name</th>
                                    <th>Email</th>
                                    <th>Mobile</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <img src='<%# ResolveUrl("~/profilepictures/" + Eval("ProfilePicture")) %>' 
                                     class="profile-picture-table" alt='<%# Eval("Name") %>' />
                            </td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("Mobile") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <div class="button-group">
                <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn btn-primary btn-custom" OnClick="btnUpdateProfile_Click" />
                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-secondary btn-custom" OnClick="btnChangePassword_Click" />
                <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger btn-custom" OnClick="btnLogout_Click" />
                <asp:Button ID="btnDownloadProfilePDF" runat="server" Text="Download PDF" CssClass="btn btn-info btn-custom" OnClick="btnDownloadProfilePDF_Click" />
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>
</body>
</html>
