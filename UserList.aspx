<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="UserManagement.UserList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>User List</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to delete this user?");
        }
    </script>

  <style>
    body {
        background-color: #f0f2f5;
    }

    .container {
        padding: 60px 20px;
    }

    .header-title, h2 {
        color: #007bff;
        font-weight: 600;
        margin-bottom: 30px;
        text-align: center;
        font-size: 2rem;
    }

    .user-list-container, .table-container {
        padding: 20px;
        border-radius: 10px;
        background-color: #fff;
        box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.1);
        margin-top: 30px;
    }

    .table {
        width: 100%;
        color: #333;
    }

    .table thead th {
        background-color: #343a40;
        color: #fff;
        border: none;
    }

    .table tbody tr {
        transition: background-color 0.3s;
    }

    .table tbody tr:hover {
        background-color: #f8f9fa;
    }

    .action-buttons, .footer-actions {
        display: flex;
        justify-content: center;
        gap: 10px;
        flex-wrap: wrap;
    }

    .btn {
        font-size: 0.875rem;
        flex: 1 0 auto;
    }

    .btn-primary {
        background-color: #007bff;
        border: none;
    }

    .btn-danger {
        background-color: #dc3545;
        border: none;
    }

    .gridview-actions {
        text-align: center;
    }

    .gridview-actions a {
        text-decoration: none;
        color: #007bff;
    }

    .gridview-actions a:hover {
        color: #0056b3;
    }

    .file-upload input[type="file"] {
        display: inline-block;
        font-size: 0.875rem;
        padding: 2px 6px;
        margin-top: 10px;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container user-list-container">
            <div class="table-responsive user-list-table">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                        <asp:BoundField DataField="Gender" HeaderText="Gender" />
                        <asp:BoundField DataField="UserType" HeaderText="User Type" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="action-buttons">
                                    <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Edit" OnClick="btnEdit_Click" CssClass="btn btn-primary btn-sm" />
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Delete" OnClientClick="return confirmDelete();" OnClick="btnDelete_Click" CssClass="btn btn-danger btn-sm" />
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="file-upload btn-sm" />
                                    <asp:LinkButton ID="btnImport" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Import" OnClick="btnImport_Click" CssClass="btn btn-info btn-sm" />
                                    <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%# Eval("Id") %>' Text="View" OnClick="btnView_Click" CssClass="btn btn-success btn-sm" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>

            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
            <asp:LinkButton ID="btnDownloadPDF" runat="server" OnClick="btnDownloadPDF_Click" CommandArgument='<%# Eval("Id") %>' Text="Export PDF" CssClass="btn btn-primary" />
            <asp:LinkButton ID="btnDownloadExcel" runat="server" OnClick="btnDownloadExcel_Click" CommandArgument='<%# Eval("Id") %>' Text="Export Excel" CssClass="btn btn-primary" />
            <asp:Button ID="btnAdminProfile" runat="server" Text="Admin Profile" CssClass="btn btn-primary" OnClick="btnAdminProfile_Click" />
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
