<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="UserManagement.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Forgot Password</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <style>
        body {
            background-color: #f7f7f7;
            line-height:0.3;
            color:#1877F2;
        }
        .container {
            max-width: 400px;
            margin-top: 100px;
            background: white;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            
        }
        .form-header {
            margin-bottom: 20px;
            text-align: center;
        }
        .form-header h2 {
            font-size: 24px;
            margin-bottom: 10px;
            
        }
        .form-header p {
            color: #777;
            font-size: 14px;
        }
        .btn-primary {
            width: 100%;
            background-color:#1877F2;
            border-color:#1877F2;

        }
       

       
    </style>
</head>
<body>
   

    <form id="form1" runat="server">
        <div class="container">
            <div class="form-header">
                <h2><i class="fas fa-lock"></i> Forgot Password</h2>
                <p>Enter your email to reset your password.</p>
            </div>
            <div class="form-group">
                <label for="txtEmail"></label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="example@example.com" />
            </div>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                        <br />


            <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2" />
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger mt-2" />
            <br />

            <div>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" 
                style="
    margin-left: 157px;
    margin-top: 12px; height:20px; font-size:Medium"/>
                </div>
           
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
