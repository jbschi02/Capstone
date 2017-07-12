﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployees.aspx.cs" Inherits="ServiceTracker.AddEmployees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="NavigationBar.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id ="menu">
            <ul>
                <li><a href="HomePage.aspx">Dashboard</a></li>
                <li><a href="#">Users</a>
                    <ul>
                        <li><a href="AddEmployees.aspx">Add Employees</a></li>
                        <li><a href="#">Delete Employees</a></li>
                    </ul>
                </li>
                <li><a href="ViewUsers.aspx">Organizations</a></li>
                <li><a href="SetGoals.aspx">Goals</a>
                    <ul>
                        <li><a href="SetGoals.aspx">Set/Edit Goals</a></li>
                        <li><a href="ProgressTracker.aspx">Progress Tracker</a></li>
                    </ul>
                </li>
                <li><a href="Jobs.aspx">Jobs</a></li>
                <li><a href="LoginPage.aspx">Logout</a></li>
            </ul>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
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
