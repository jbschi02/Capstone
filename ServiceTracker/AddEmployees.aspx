﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployees.aspx.cs" Inherits="ServiceTracker.AddEmployees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="NavigationBar.css" rel="stylesheet" />
    <link href="Table.css" rel="stylesheet" />
    <link href="LoginPage.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id ="menu">
            <ul>
                <li><a href="HomePage.aspx">Dashboard</a></li>
                <li><a href="#">Technicians</a>
                    <ul>
                        <li><a href="AddEmployees.aspx">Add Technician</a></li>
                        <li><a href="ViewUsers.aspx">Statistics</a></li>
                        <li><a href="DeleteEmployee.aspx">Delete Technician</a></li>
                    </ul>
                </li>
                <li><a href="SetGoals.aspx">Goals</a>
                    <ul>
                        <li><a href="SetGoals.aspx">Set/Edit Goals</a></li>
                        <li><a href="ProgressTracker.aspx">Progress Tracker</a></li>
                    </ul>
                </li>
                <li><a href="#">Jobs</a>
                    <ul>
                        <li><a href="Jobs.aspx">Jobs</a></li>
                        <li><a href="Opportunities.aspx">Opportunities</a></li>
                    </ul>
                </li>
                <li><a href="#">User</a>
                    <ul>
                        <li><a href="Settings.aspx">Settings</a></li>
                        <li><a href="LoginPage.aspx">Logout</a></li>
                    </ul>
                </li>
            </ul>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Add Technician" Font-Size ="XX-Large"></asp:Label>
        <br />
        <br />
    <div>
        <div style ="width:350px">
        <asp:Label ID="fnameLabel" runat="server" Text="Employee First Name:"></asp:Label>
        <asp:TextBox ID="fnameField" runat="server" style="float:right"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lnameLabel" runat="server" Text="Employee Last Name:"></asp:Label>
        <asp:TextBox ID="lnameField" runat="server" style="float:right"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="emailLabel" runat="server" Text="Employee Email:"></asp:Label>
        <asp:TextBox ID="emailField" runat="server" style="float:right"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="cancelButton" runat="server" Text="Cancel" OnClick ="GoToHomePage"/>
        <asp:Button ID="addButton" runat="server" Text="Add Employee" OnClick ="addEmployee" style="float:right"/>
        </div>
    </div>
    </form>
</body>
</html>
