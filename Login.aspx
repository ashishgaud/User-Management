<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserManagement.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>User Login</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
              <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Poppins:wght@600&display=swap"/>
<%--        <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Pacifico&display=swap" />--%>




    
    <style>
        body {
            background-color: #ffffff;
        }

        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .login-form {
            background-color: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
        }

        .login-form h1 {
            margin-bottom: 7px;
/*            color: #000000;*/
            color: #1877F2;
/**/            font-weight: 700; 
                font-family: 'Poppins', sans-serif;

        }

        .btn-primary {
            width: 100%;
        }

        .form-group label {
            font-weight: 600;
        }

        .text-danger {
            display: block;
            margin-top: 10px;
        }

        .register-link {
            margin-top: 20px;
            text-align: center;
        }

        .register-link a {
            color: #007bff;
            font-weight: 600;
        }

        .form-group {
    margin-bottom: -1px;
}
       
        .signin-image {
            display: flex;
            justify-content: center;
            align-items: center;
            background-color: #ffffff; 
        }

        .signin-image img {
            width: 100%;
            max-width: 300px;
            height: auto;
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

.link-section {
    margin-top: 20px;
     text-align:center
}

.register-link,
.forgot-password-button {
    margin: 10px 0; 
   
}

.text-center {
    text-align: center; 
}

.forgot-password-button {
    display: block; 
    margin-top: 10px;
    border: none;
    background: none;
    color: #007bff; 
    text-decoration: underline;
    cursor: pointer;
}

.forgot-password-button:hover {
    text-decoration: none; 
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container login-container">
            <div class="row w-100">
                
                <div class="col-md-6 signin-image">
                    <figure>
                        <img src="ProfilePictures/signin-image.jpg" />
                    </figure>
                </div>
              
                <div class="col-md-6">
                    <div class="login-form">
                        <h1 class="text-center">zingster</h1>
                        <div class="form-group">
                            <label for="txtEmail"></label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email address" />
                        </div>
                       <div class="form-group">
                            <label for="txtPassword"></label>
                              <div class="password-wrapper">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Password" onkeyup="checkPassword()" />                                <!-- Eye icon toggles between open and closed state -->
                                <span class="toggle-password eye-icon" onclick="togglePassword()">
                                    <i class="fas fa-eye"></i> 
                                </span>
                            </div>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary mt-3" OnClick="btnLogin_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />


                         <div class="otp-section" style="display:none;">
                            <div class="form-group">
                                <label for="txtOTP">Enter OTP</label>
                                <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" Placeholder="Enter the OTP sent to your email" />
                            </div>
                            <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" CssClass="btn btn-primary mt-3" OnClick="btnVerifyOTP_Click" />
                            <asp:Label ID="lblOTMessage" runat="server" CssClass="text-danger" />
                        </div>

                        <div class="register-link">
                            <p>Don't have an account? <a href="Register.aspx">Register here</a></p>
                        </div>
                       
    <asp:Button ID="btnForgotPassword" runat="server" Text="Forgot Password?" CssClass="btn btn-link forgot-password-button" OnClick="btnForgotPassword_Click" style="
    margin-left: 89px;
    margin-top: -13px;"/>

                    </div>
                </div>
            </div>
        </div>


    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

   <script>
       function togglePassword() {
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
       }
       function showOTPSection() {
           document.querySelector('.otp-section').style.display = 'block';
           display: none;
       }

   </script>
           
   
    </form>


    </body>
</html>



