<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ServiceTracker.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="NavigationBar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
        <ul>
            <li><a href="#">Dashboard</a></li>
            <li><a href="#">Users</a>
                <ul>
                    <li><a href="AddEmployees.aspx">Add Employees</a></li>
                    <li><a href="#">View Employees</a></li>
                </ul>
            </li>
            <li><a href="#">Organizations</a></li>
            <li><a href="#">Career</a></li>
            <li><a href="#">Goals</a></li>
            <li><a href="#">Jobs</a></li>
            <li><a href="#">Progress Tracker</a></li>
            <li><a href="LoginPage.aspx">Logout</a></li>
        </ul>
    </div>
    </form>
</body>
</html>
