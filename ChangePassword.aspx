     <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="UserManagement.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Change Password</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />


    <style>
        body {
            background-color: #f8f9fa;
        }
        .password-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }
        .password-form {
            background-color: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
        }
        .password-form h2 {
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
        .text-danger {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm()">
        <div class="password-container">
            <div class="password-form">
                <h2 class="text-center">Change Password</h2>
                
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

                 <div class="form-group">
                    <div class="input-group">
                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Placeholder="Old Password" CssClass="form-control" ValidationGroup="ChangePasswordGroup" />
                        <div class="input-group-append">
                            <span class="input-group-text">
                                <i class="fas fa-eye toggle-password" data-toggle="#txtOldPassword"></i>
                            </span>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="input-group">
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="New Password" CssClass="form-control" ValidationGroup="ChangePasswordGroup" />
                        <div class="input-group-append">
                            <span class="input-group-text">
                                <i class="fas fa-eye toggle-password" data-toggle="#txtNewPassword"></i>
                            </span>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="input-group">
                        <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" Placeholder="Confirm New Password" CssClass="form-control" ValidationGroup="ChangePasswordGroup"/>
                        <div class="input-group-append">
                            <span class="input-group-text">
                                <i class="fas fa-eye toggle-password" data-toggle="#txtConfirmNewPassword"></i>
                            </span>
                        </div>
                    </div>
                </div>

                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn btn-primary mt-3" OnClick="btnChangePassword_Click" ValidationGroup="ChangePasswordGroup"/>
          <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-secondary mt-3" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

   <script>
       function validateForm() {
           var clickedButton = document.activeElement;
           if (clickedButton && clickedButton.id === '<%= btnCancel.ClientID %>') {
            return true; 
           }

        var oldPassword = document.getElementById('<%= txtOldPassword.ClientID %>').value.trim();
        var newPassword = document.getElementById('<%= txtNewPassword.ClientID %>').value.trim();
        var confirmNewPassword = document.getElementById('<%= txtConfirmNewPassword.ClientID %>').value.trim();
        var messageLabel = document.getElementById('<%= lblMessage.ClientID %>');

           messageLabel.innerText = "";

           
           if (oldPassword === "") {
               messageLabel.innerText = "Old password is required.";
               return false;
           }

           
           if (newPassword === "" || newPassword.length < 6) {
               messageLabel.innerText = "New password must be at least 6 characters long.";
               return false;
           }

           if (confirmNewPassword === "") {
               messageLabel.innerText = "Please confirm your new password.";
               return false;
           }

           if (newPassword !== confirmNewPassword) {
               messageLabel.innerText = "New password and confirm password do not match.";
               return false;
           }

           return true; 



       }        

   </script>

     <script>
         $(document).ready(function () {
             $(".toggle-password").click(function () {
                 var inputSelector = $(this).attr("data-toggle");
                 var $input = $(inputSelector);
                 if ($input.attr("type") === "password") {
                     $input.attr("type", "text");
                     $(this).removeClass("fa-eye").addClass("fa-eye-slash");
                 } else {
                     $input.attr("type", "password");
                     $(this).removeClass("fa-eye-slash").addClass("fa-eye");
                 }
             });
         });
    </script>

 
</body>
</html>

