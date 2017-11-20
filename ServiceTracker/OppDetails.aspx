<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OppDetails.aspx.cs" Inherits="ServiceTracker.OppDetails" %>

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
        <asp:Label ID="Label1" runat="server" Text ="Opportunity Details" Font-Size ="XX-Large"></asp:Label>
                <br />
        <br />
                <div style="width:525px;height:850px;">
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label2" runat="server" Text="Type" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="typeLabel" runat="server" Text="Type" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label3" runat="server" Text="Age" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label5" runat="server" Text="Age" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label10" runat="server" Text="Customer" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label11" runat="server" Text="Customer" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label4" runat="server" Text="Technician" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label12" runat="server" Text="Tech" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label6" runat="server" Text="Status" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label13" runat="server" Text="Status" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label7" runat="server" Text="Date" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label14" runat="server" Text="Date" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:100px;">
                <asp:Label ID="Label8" runat="server" Text="Quote Given" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label15" runat="server" Text="Yes/No" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
            <div style ="float:left; width:130px;">
                <asp:Label ID="Label9" runat="server" Text="Quote Amount" Visible ="true" Font-Bold ="true"></asp:Label> 
            </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <div style ="float:right; width:350px">
                <asp:Label ID="Label16" runat="server" Text="Quote Amount" Visible ="true"></asp:Label> 
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
