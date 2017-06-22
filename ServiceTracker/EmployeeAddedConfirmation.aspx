<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAddedConfirmation.aspx.cs" Inherits="ServiceTracker.EmployeeAddedConfirmation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="successLabel" runat="server" Text="Success! You have successfully invited a new employee to join."></asp:Label>
        <br />
        <br />
        <asp:Button ID="goToHomeButton" runat="server" Text="Dashboard" OnClick ="GoToHomePage"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="anotherEmpButton" runat="server" Text="Add More Employees" OnClick ="AnotherEmployee"/>
    
    </div>
    </form>
</body>
</html>
