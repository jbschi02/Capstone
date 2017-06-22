<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployees.aspx.cs" Inherits="ServiceTracker.AddEmployees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="addButton">
    <div>
    
        <asp:Label ID="fnameLabel" runat="server" Text="Employee First Name:"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="fnameField" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lnameLabel" runat="server" Text="Employee Last Name:"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="lnameField" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="emailLabel" runat="server" Text="Employee Email:"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="emailField" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick ="GoToHomePage"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="addButton" runat="server" Text="Add Employee" OnClick ="addEmployee"/>
    </div>
    </form>
</body>
</html>
