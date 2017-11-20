<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteEmployee.aspx.cs" Inherits="ServiceTracker.DeleteEmployee" %>

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
            <asp:Label ID="Label1" runat="server" Text="Delete Technician" Font-Size ="XX-Large"></asp:Label>
            <asp:GridView ID="deleteGridView" runat="server" CssClass ="mGrid">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="deleteButton" runat="server" Text="Delete" OnClick ="deleteButton_Click"/>
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" Visible ="false" OnClick ="cancelButton_Click"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="confirmButton" runat="server" Text="Confirm" Visible ="false" OnClick ="confirmButton_Click"/>
        &nbsp;&nbsp;&nbsp;
            <asp:Label ID="pickOneLabel" runat="server" Text="*Please choose at least one employee to delete" Visible ="false" CssClass ="missingInfo"></asp:Label>
        </div>
        <br />
        <br />
        <asp:Label ID="successfulDeleteLabel" runat="server" Text="Technician successfully deleted!" Visible ="false"></asp:Label>
    </form>
</body>
</html>
