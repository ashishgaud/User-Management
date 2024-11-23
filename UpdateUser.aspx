<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateUser.aspx.cs" Inherits="UserManagement.UpdateUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update User Profile</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        body {
            background-color: #f7f7f7; 
        }
        .container {
            background-color: #fff; 
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            padding: 30px;
            margin-top: 50px;
            max-width: 600px; 
        }
        h2 {
            color: #333;
            text-align: center;
            margin-bottom: 30px; 
        }
        .profile-picture {
            display: block;
            margin: 0 auto 20px;
            border-radius: 50%; 
            border: 2px solid #ddd; 
        }
        .btn-container {
            display: flex;
            justify-content: center; 
            margin-top: 20px; 
        }
        .btn {
            margin: 0 10px; 
        }
        .form-group label {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="container">

          
            <asp:Image ID="imgProfilePicture" runat="server" Width="150" Height="150" CssClass="profile-picture" />
            
            <div class="form-group">
                <label for="fileUpload">Upload New Profile Picture:</label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
            </div>

         
            <div class="form-group">
                <label for="txtName">Name:</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter your name" onkeypress="validateNameInput(event)" onpaste="preventPaste(event)" MaxLength="30"></asp:TextBox>
            </div>

           
            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"  />
            </div>

         
            <div class="form-group">
                <label for="txtMobile">Mobile:</label>
                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter your mobile number" onkeypress="validateMobileInput(event)" onpaste="preventPaste(event)" MaxLength="10"></asp:TextBox>
            </div>

         
            <div class="form-group">
                <label for="ddlGender">Gender:</label>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select" Value="" />
                    <asp:ListItem Text="Male" Value="Male" />
                    <asp:ListItem Text="Female" Value="Female" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlUserType">User Type:</label>
                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select" Value="" />
                    <asp:ListItem Text="Admin" Value="Admin" />
                    <asp:ListItem Text="User" Value="User" />
                </asp:DropDownList>
            </div>


          
            <div class="btn-container">
<asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdateProfile_Click" OnClientClick="return validateForm();" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
            </div>
             <div class="form-group">
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
</div>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
   <script type="text/javascript">
       function validateForm() {
           var isValid = true;
           var errorMessage = "";

           var name = document.getElementById('<%= txtName.ClientID %>').value.trim();
        var email = document.getElementById('<%= txtEmail.ClientID %>').value.trim();
        var mobile = document.getElementById('<%= txtMobile.ClientID %>').value.trim();
        var gender = document.getElementById('<%= ddlGender.ClientID %>').value;
        var userType = document.getElementById('<%= ddlUserType.ClientID %>').value;

       
        if (name === "") {
            errorMessage += "Name is required.\n";
            isValid = false;
        }

    
        var emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
        if (!emailPattern.test(email)) {
            errorMessage += "Please enter a valid email address.\n";
            isValid = false;
        }

        if (!/^\d{10}$/.test(mobile)) {
            errorMessage += "Please enter a valid 10-digit mobile number.\n";
            isValid = false;
        }

        if (gender === "0") {
            errorMessage += "Please select a gender.\n";
            isValid = false;
        }

    
        if (userType === "") {
            errorMessage += "Please select a user type.\n";
            isValid = false;
        }

        var fileInput = document.getElementById('<%= fileUpload.ClientID %>');
           if (fileInput.value !== "") {
               var fileExtension = fileInput.value.split('.').pop().toLowerCase();
               var allowedExtensions = ['jpg', 'jpeg', 'png', 'gif'];
               if (!allowedExtensions.includes(fileExtension)) {
                   errorMessage += "Invalid file type. Only images are allowed.\n";
                   isValid = false;
               }

               if (fileInput.files[0].size > 2048576) { 
                   errorMessage += "File size exceeds the 2MB limit.\n";
                   isValid = false;
               }
           }

           
           if (!isValid) {
               alert(errorMessage);
           }

           return isValid;
       }


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
</script>


</body>
</html>
