<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyOTP.aspx.cs" Inherits="UserManagement.VerifyOTP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verify OTP</title>
    <link rel="stylesheet" type="text/css" href="Styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Verify Your OTP</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <label for="txtOTP">Enter OTP:</label>
            <asp:TextBox ID="txtOTP" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnVerify" runat="server" Text="Verify OTP" OnClick="btnVerify_Click" />
        </div>
    </form>
</body>
</html>
