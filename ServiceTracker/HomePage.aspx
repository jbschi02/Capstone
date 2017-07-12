<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ServiceTracker.HomePage" %>

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
            <asp:Label ID="Label4" runat="server" Text="Dashboard" Font-Size ="XX-Large"></asp:Label>
            <br />
            <br />
        </div>
        <div style="width:850px;height:150px;border:1px solid #eaeaea; background-color: #eaeaea">

        &nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp; <asp:Label ID="Label5" runat="server" Text="Distributor"></asp:Label>

        &nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ServiceTracker" DataTextField="compname" DataValueField="compname">
            </asp:DropDownList>
            <asp:SqlDataSource ID="ServiceTracker" runat="server" ConnectionString="<%$ ConnectionStrings:ServiceTrackerConnectionString %>" SelectCommand="SELECT [compname] FROM [Company]"></asp:SqlDataSource>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Contractor"></asp:Label>

        &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Employee"></asp:Label>

        &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" DataSourceID="Employees" DataTextField="fname" DataValueField="fname">
            </asp:DropDownList>
            <asp:SqlDataSource ID="Employees" runat="server" ConnectionString="<%$ ConnectionStrings:ServiceTrackerConnectionString %>" SelectCommand="SELECT [fname], [lname] FROM [Technician]"></asp:SqlDataSource>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label8" runat="server" Text="From"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" Width="135px" TextMode="Date"></asp:TextBox>
&nbsp;&nbsp;&nbsp; To&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" Width="135px" TextMode ="Date"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="resetRangeButton" runat="server" Text="Reset Range" />

            <br />
&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="resetDataButton" runat="server" Text="Reload Data" OnClick ="reloadData"/>

        </div>
    </form>
</body>
</html>
