<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgressTracker.aspx.cs" Inherits="ServiceTracker.ProgressTracker" %>

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
        <br />
        <asp:Label ID="Label1" runat="server" Text="Progress Tracker" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <div style="width:850px;height:150px;border:1px solid #eaeaea; background-color: #eaeaea">

            &nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Distributor"></asp:Label>
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
&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="monthLabel" runat="server" Text="Month" Visible ="true"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="monthDropDownList" runat="server" Visible ="true">
                <asp:ListItem>January</asp:ListItem>
                <asp:ListItem>February</asp:ListItem>
                <asp:ListItem>March</asp:ListItem>
                <asp:ListItem>April</asp:ListItem>
                <asp:ListItem>May</asp:ListItem>
                <asp:ListItem>June</asp:ListItem>
                <asp:ListItem>July</asp:ListItem>
                <asp:ListItem>August</asp:ListItem>
                <asp:ListItem>September</asp:ListItem>
                <asp:ListItem>October</asp:ListItem>
                <asp:ListItem>November</asp:ListItem>
                <asp:ListItem>December</asp:ListItem>
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="loadDataButon" runat="server" Text="View" OnClick ="loadData"/>

        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="needTechLabel" runat="server" Text="*Please select a technician" CssClass ="missingInfo" Visible ="false"></asp:Label>

        </div>
        <br />
        <br />
        <br />
        <asp:Label ID="userGoalsLabel" runat="server" Text="userGoals" Font-Size ="X-Large" Visible ="false" CssClass ="title"></asp:Label>
        <br />
        <br />
        <div>
            <div style="width:850px; text-align:center">
                <asp:Label ID="Label7" runat="server" Text="Daily Progress" Font-Size ="X-Large"></asp:Label>
            </div>
            <div style="width:850px">
                <asp:Label ID="dailyGoalActualLabel" runat="server" style ="float:left" Text="GoalActualLabel" Visible ="false"></asp:Label>
                <asp:Label ID="dailyGoalRemaingLabel" runat="server" style ="float:right" Text="GoalRemainingLabel" Visible ="false"></asp:Label>
            </div>
            <br />
                <div id="rcorners2" style="width:850px;height:15px;border:1px solid #000000; background-color: #ffffff" runat ="server">
                <div id="rcorners1" style="width:0px;height:15px;border:1px solid #000000; background-color: #1a158f" runat ="server" visible ="false">
                </div>
                </div>
        <br />
        <div style ="width:850px; text-align:center">
            <asp:Label ID="dailyPercentageLabel" runat="server" Text="Percentage" Visible ="false"></asp:Label>
        </div>
        </div>
        <br />
        <br />
                <div>
            <div style="width:850px; text-align:center">
                <asp:Label ID="Label4" runat="server" Text="Monthly Progress" Font-Size ="X-Large"></asp:Label>
            </div>
            <div style="width:850px">
                <asp:Label ID="monthlyGoalActualLabel" runat="server" style ="float:left" Text="GoalActualLabel" Visible ="false"></asp:Label>
                <asp:Label ID="monthlyGoalRemainingLabel" runat="server" style ="float:right" Text="GoalRemainingLabel" Visible ="false"></asp:Label>
            </div>
            <br />
                <div id="rcorners3" style="width:850px;height:15px;border:1px solid #000000; background-color: #ffffff" runat ="server">
                <div id="rcorners4" style="width:0px;height:15px;border:1px solid #000000; background-color: #1a158f" runat ="server" visible ="false">
                </div>
                </div>
        <br />
        <div style ="width:850px; text-align:center">
            <asp:Label ID="percentageMonthlyLabel" runat="server" Text="Percentage" Visible ="false"></asp:Label>
        </div>
        </div>
        <br />
        <br />
                <div>
            <div style="width:850px; text-align:center">
                <asp:Label ID="Label11" runat="server" Text="Yearly Progress" Font-Size ="X-Large"></asp:Label>
            </div>
            <div style="width:850px">
                <asp:Label ID="yearlyGoalActualLabel" runat="server" style ="float:left" Text="GoalActualLabel" Visible ="false"></asp:Label>
                <asp:Label ID="yearlGoalRemainingLabel" runat="server" style ="float:right" Text="GoalRemainingLabel" Visible ="false"></asp:Label>
            </div>
            <br />
                <div id="rcorners5" style="width:850px;height:15px;border:1px solid #000000; background-color: #ffffff" runat ="server">
                <div id="rcorners6" style="width:0px;height:15px;border:1px solid #000000; background-color: #1a158f" runat ="server" visible ="false">
                </div>
                </div>
        <br />
        <div style ="width:850px; text-align:center">
            <asp:Label ID="percentageYearlyLabel" runat="server" Text="Percentage" Visible ="false"></asp:Label>
        </div>
        </div>
    </form>
</body>
</html>