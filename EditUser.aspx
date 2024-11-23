<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="UserManagement.EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Edit User</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <style>
        body {
            background-color: #f8f9fa;
        }

        .edit-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .edit-form {
            background-color: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 600px;
        }

        .edit-form h2 {
            margin-bottom: 30px;
            color: #007bff;
        }

        .form-group label {
            font-weight: 600;
        }

        .btn-primary, .btn-secondary {
            width: 100%;
            margin-top: 10px;
        }

        .btn-secondary {
            background-color: #6c757d;
            border-color: #6c757d;
        }

        .form-control[readonly] {
            background-color: #e9ecef;
            cursor: not-allowed;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div class="edit-container">
            <div class="edit-form">
                <h2 class="text-center">Edit User</h2>

                <asp:HiddenField ID="hdnUserId" runat="server" />

                <div class="form-group">
                    <asp:Label ID="lblName" runat="server" Text="Name" AssociatedControlID="txtName"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblMobile" runat="server" Text="Mobile" AssociatedControlID="txtMobile"></asp:Label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" />
                </div>

                <div class="form-group">
                    <asp:Label ID="lblGender" runat="server" Text="Gender" AssociatedControlID="ddlGender"></asp:Label>
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Male">Male</asp:ListItem>
                        <asp:ListItem Value="Female">Female</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblUserType" runat="server" Text="User Type" AssociatedControlID="ddlUserType"></asp:Label>
                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Select" Value="" />
                        <asp:ListItem Text="Admin" Value="Admin" />
                        <asp:ListItem Text="User" Value="User" />
                    </asp:DropDownList>
                </div>

                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />

                <div class="form-group">
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
                </div>
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#form1").submit(function (e) {
                let isValid = true;
                let name = $("#<%= txtName.ClientID %>").val();
            let mobile = $("#<%= txtMobile.ClientID %>").val();
            let gender = $("#<%= ddlGender.ClientID %>").val();
            let userType = $("#<%= ddlUserType.ClientID %>").val();

            if (name === "") {
                alert("Please enter the name.");
                isValid = false;
            }

            const mobilePattern = /^[0-9]{10}$/;
            if (!mobilePattern.test(mobile)) {
                alert("Please enter a valid 10-digit mobile number.");
                isValid = false;
            }

            if (gender === "") {
                alert("Please select your gender.");
                isValid = false;
            }

            if (userType === "") {
                alert("Please select a User Type.");
                isValid = false;
            }

            if (!isValid) {
                e.preventDefault();
            }
        });
    });
    </script>
</body>
</html>
