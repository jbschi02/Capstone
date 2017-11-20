<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="ServiceTracker.ViewUsers" %>

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
        <asp:Label ID="Label1" runat="server" Text="View Statistics" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <div style="width:850px;height:70px;border:1px solid #eaeaea; background-color: #eaeaea">

            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="startDateTextBox" runat="server" TextMode ="Date"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="endDateTextBox" runat="server" TextMode ="Date"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Reload Data" OnClick ="loadData_Click"/>

        &nbsp;&nbsp;&nbsp;
            <asp:Label ID="invalidDateLabel" runat="server" Text="*Please enter a valid date range" CssClass ="missingInfo" Visible ="false"></asp:Label>

        </div>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Revenue by Technician" Font-Size ="X-Large"></asp:Label>
        <asp:GridView ID="techGridView1" runat="server" CssClass ="mGrid"></asp:GridView>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Revenue by Job" Font-Size="X-Large"></asp:Label>
        <asp:GridView ID="jobGridView" runat="server" CssClass ="mGrid"></asp:GridView>
    </form>
</body>
</html>
