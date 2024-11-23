<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="UserManagement.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>User Registration</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />

    
   <style>
    body {

        background-color: #f8f9fa;
    }

    .registration-container {
        display: flex;
        justify-content: center;
        align-items: center;
        min-height: 100vh;
    }

    .registration-form {
        background-color: #ffffff;
        padding: 20px;
        border-radius: 10px;
        box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        width: 30%;
        max-width: 432px;
    }

    .registration-form h1 {
        margin-bottom: 15px; 
        color: #007bff;
        font-size: 42px; 
        font-weight: bold;
        text-align: center;
    }

    .form-group {
        margin-bottom: 15px; 
    }
     .form-control {
        height: 38px;
        padding: 5px 10px;
    }
    .form-group label {
        font-weight: 600;
    }

    .btn-primary {
        width: 50%;
        color: #fff;
        background-color: #2cc354;
        border-color: #2cc354;
        text-align: center;
    }

    .form-control-file {
        margin-top: 5px;
    }

    .profile-picture-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 15px;
    }

    .profile-picture-preview {
        height: 100px;
        width: 100px;
        border-radius: 50%;
        object-fit: cover;
        display: block;
        background-color: #e9ecef;
        margin-bottom: 10px;
    }

    .text-danger {
        margin-top: 10px;
    }

    .form-footer {
        text-align: center;
        margin-top: 10px; 
    }

    .form-footer a {
        color: #007bff;
        font-weight: 600;
    }

    hr { 
        display: block;
        margin-top: 0.5em;
        margin-bottom: 0.5em;
        margin-left: auto;
        margin-right: auto;
        border-style: inset;
        border-width: 1px;
    }
    #fileUpload {
        display: none;
    }
          .password-wrapper {
    position: relative;
          }


.password-wrapper input {
    padding-right: 40px;
}


.password-wrapper .toggle-password {
    position: absolute;
    top: 50%;
    right: 10px; 
    transform: translateY(-50%);
    cursor: pointer;
    color: #333;
    display: none;
}

        .eye-icon {
            font-size: 0.8rem;
        }

.password-wrapper .toggle-password:hover {
    color: #007bff;
}
</style>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return validateForm();">

        <div class="registration-container">
            <div class="registration-form">
                <h1 class="text-center">zingster</h1>
                <div class="mb-3 d-flex flex-column align-items-center">
                    <div class="font-weight-bold">Create a new account</div>
                    <div>It's quick and easy.</div>
                    <hr style="width:100%"/>
                </div>

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="text-danger" />
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="text-danger" />
                <asp:Label ID="lblMobileError" runat="server" ForeColor="Red" CssClass="text-danger" />
                <asp:Label ID="lblserror" runat="server" ForeColor="Red" CssClass="text-center" />
                


           
                <div class="profile-picture-container">
                    <asp:Image ID="imgProfile" runat="server" CssClass="profile-picture-preview" ImageUrl="~/ProfilePictures/default.jpg" />
                    <label for="fileUpload" class="btn btn-outline-primary btn-sm">Upload Profile Picture</label>
                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" OnChange="showPreview(this)" />
                </div>


               
                <div class="form-group" style="margin-bottom: -16px">
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Placeholder="Full name" onkeypress="validateNameInput(event)" onpaste="preventPaste(event)" MaxLength="30" />
                </div>

                <div class="form-group" style="margin-bottom: -16px">
                    <label for="txtEmail"></label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email address" onkeypress="validateEmailInput(event)" MaxLength="30"/>
                </div>

               <%-- 
                <div class="form-group" style="margin-bottom: -16px">
                    <label for="txtPassword"></label>
                    <div class="password-wrapper">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="New password"  MaxLenth="30" onkeyup="checkPassword()"/>
                    <span class="toggle-password eye-icon" onclick="togglePassword()">
                                    <i class="fas fa-eye"></i> 
                 
                        </span>
                </div>
                    </div>--%>
                <div class="form-group">
                    <label for="txtMobile"></label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" Placeholder="Mobile number" onkeypress="validateMobileInput(event)" onpaste="preventPaste(event)" MaxLength="10"/>
                </div>

      
                <div class="form-group">
                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Gender" Value=""></asp:ListItem>
                        <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>

          
                <div class="form-group">
                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="User Type" Value=""></asp:ListItem>
                        <asp:ListItem Text="User" Value="User"></asp:ListItem>
                        <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                    </asp:DropDownList>
                </div>

     
                <div class="form-footer">
                    <asp:Button ID="btnRegister" runat="server" Text="Sign Up" CssClass="btn btn-primary mt-3" OnClick="btnRegister_Click" />
                </div>

                <div class="form-footer">
                    <p>Already have an account? <a href="Login.aspx">Login here</a></p>
                </div>
            </div>
        </div>
    </form>






    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <script type="text/javascript">


            function validateNameInput(event) {
            var charCode = event.keyCode || event.which;
            var input = event.target.value;

            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                event.preventDefault();
            return false;
            }

            if (input.length >= 30 && charCode != 8 && charCode != 46) { 
                event.preventDefault(); 
            return false;
            }

            return true;
            }


        function validateMobileInput(event) {
            var charCode = event.keyCode || event.which;
            var input = event.target.value;

            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                event.preventDefault();
                return false;
            }

            if (input.length >= 10 && charCode != 8 && charCode != 46) { 
                event.preventDefault();
                return false;
            }

            return true;
        }

        function preventPaste(event) {
            event.preventDefault();
        }

        function validateEmailInput(event) {
            if (input.length >= 30) {
                event.preventDefault();
                return false;
            }

            return true;
        }




        function validateForm() {
            var name = document.getElementById('<%=txtName.ClientID%>').value;
            var email = document.getElementById('<%=txtEmail.ClientID%>').value;
            var password = document.getElementById('<%//=txtPassword.ClientID%>').value;
            var mobile = document.getElementById('<%=txtMobile.ClientID%>').value;
            var gender = document.getElementById('<%=ddlGender.ClientID%>').value;
            var userType = document.getElementById('<%=ddlUserType.ClientID%>').value;

            var namePattern = /^[A-Za-z\s]+$/;
            if (name.trim() === "" || !namePattern.test(name)) {
                alert("Name should only contain alphabets.");
                return false;
            }

            var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (email.trim() === "" || !emailPattern.test(email) || email.length >= 30) {
                alert("Please enter a valid email address not exceeding 30 characters.");
                return false;
            }

            if (password.trim() === "" || password.length < 6 || password.length > 30) {
                alert("Password is required and must be between 6 and 25 characters long.");
                return false;
            }

            var mobilePattern = /^[0-9]{10}$/;
            if (mobile.trim() === "" || !mobilePattern.test(mobile)) {
                alert("Mobile numbers is required 10 ");
                return false;
            }

            if (gender === "") {
                alert("Please select a gender.");
                return false;
            }

            if (userType === "") {
                alert("Please select a user type.");
                return false;
            }

            return true;
        }

        function showPreview(fileUpload) {
            const file = fileUpload.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    const imgProfile = document.getElementById('<%=imgProfile.ClientID%>');
                    imgProfile.src = e.target.result;
                }
                reader.readAsDataURL(file);
            }
        }

 <%--function togglePassword() {
            var passwordInput = document.getElementById('<%= txtPassword.ClientID %>');
           var eyeIcon = document.querySelector('.toggle-password i');
           var type = passwordInput.getAttribute("type");

           if (type === "password") {
               passwordInput.setAttribute("type", "text");
               eyeIcon.classList.remove("fa-eye");
               eyeIcon.classList.add("fa-eye-slash"); 
           } else {
               passwordInput.setAttribute("type", "password");
               eyeIcon.classList.remove("fa-eye-slash");
               eyeIcon.classList.add("fa-eye"); 
           }
         }
      function checkPassword() {
           var passwordInput = document.getElementById('<%= txtPassword.ClientID %>');
           var eyeIcon = document.querySelector('.toggle-password');

           if (passwordInput.value.trim() !== "") {
               eyeIcon.style.display = "inline";
           } else {
               eyeIcon.style.display = "none";
           }
       }--%>
    </script>
</body>
</html>
