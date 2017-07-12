<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="ServiceTracker.ViewUsers" %>

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
        <asp:Label ID="Label1" runat="server" Text="Organizations" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <div style="width:850px;height:150px;border:1px solid #eaeaea; background-color: #eaeaea">

            &nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Distributor"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="distributorDropDownList" runat="server">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Manager"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="managerDropDownList" runat="server">
            </asp:DropDownList>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Sort by"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList3" runat="server">
            </asp:DropDownList>

        &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Reload Data" />

        </div>
    </form>
</body>
</html>
