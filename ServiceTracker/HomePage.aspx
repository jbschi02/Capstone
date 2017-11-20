<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ServiceTracker.HomePage" %>

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
        <div>
            <asp:Label ID="Label4" runat="server" Text="Dashboard" Font-Size ="XX-Large"></asp:Label>
            <br />
            <br />
        </div>
        <div style="width:850px;height:150px;border:1px solid #eaeaea; background-color: #eaeaea">

            &nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Distributor"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="distributorDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="distributorDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Manager"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="managerDropDownList" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="managerDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Technician"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="technicianDropDownList" runat="server">
            </asp:DropDownList>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="From"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="startDateTextBox" runat="server" TextMode ="Date"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="endDateTextBox" runat="server" TextMode ="Date"></asp:TextBox>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="loadDataButon" runat="server" Text="Reload Data" OnClick ="loadData"/>
        &nbsp;&nbsp;&nbsp;
            <asp:Label ID="dateLabel" runat="server" Text="*Please enter a valid date." CssClass ="missingInfo" Visible ="false"></asp:Label>
        </div>
        <br />
        <br />
        <div>
            <asp:Label ID="serviceRevenueLabel" runat="server" Text="Service Revenue" Font-Size ="X-Large"></asp:Label>
            <br />
            <asp:GridView ID="serviceRevenueGridView" runat="server" CssClass ="mGrid">
            </asp:GridView>
            <br />
            <asp:Button ID="printServiceRevenueButton" Width ="75px" runat="server" Text="Print" style ="float:right" OnClick ="printServiceRevenueButton_Click"/>
        </div>
        <br />
        <br />
        <div>
            <asp:Label ID="Label1" runat="server" Text="New System Revenue" Font-Size ="X-Large"></asp:Label>
            <div style ="width:750px">
                <asp:GridView ID="systemRevenueGridView" runat="server" CssClass ="mGrid">
                </asp:GridView>
            </div>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Combined Revenue" Font-Size ="X-Large" Visible ="false"></asp:Label>
            <div style ="width:300px">
                <asp:GridView ID="totalRevenueGridView" runat="server" CssClass ="mGrid" Visible ="false"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
