<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAddedConfirmation.aspx.cs" Inherits="ServiceTracker.EmployeeAddedConfirmation" %>

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
    <div>
    
        <br />
        <br />
        <br />
        <br />
    
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
