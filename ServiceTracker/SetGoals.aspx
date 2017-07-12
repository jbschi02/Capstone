<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetGoals.aspx.cs" Inherits="ServiceTracker.SetGoals" %>

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
        <br />
        <asp:Label ID="Label1" runat="server" Text="Set Goals" Font-Size="XX-Large"></asp:Label>
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
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="loadDataButon" runat="server" Text="Edit" OnClick ="loadData"/>

        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="needTechLabel" runat="server" Text="*Please select a technician" CssClass ="missingInfo" Visible ="false"></asp:Label>

        </div>
        <br />
        <br />
        <div>
            <asp:Label ID="janLabel" runat="server" Text="January" Visible ="false"></asp:Label> 
            <asp:Label ID="successLabel" runat="server" Text="Goals successfully updated!" Visible ="false" CssClass ="success"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="janGoalsTextBox" runat="server" Textmode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="febLabel" runat="server" Text="February" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="febGoalsTextBox" runat="server" TextMode="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="marLabel" runat="server" Text="March" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="marGoalsTextBox" runat="server" TextMode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
                        <asp:Label ID="Label4" runat="server" Text="April" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="May" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" TextMode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
                        <asp:Label ID="Label7" runat="server" Text="June" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox3" runat="server" TextMode="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="July" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox4" runat="server" TextMode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
                        <asp:Label ID="Label9" runat="server" Text="August" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox5" runat="server" TextMode="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label10" runat="server" Text="September" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox6" runat="server" TextMode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
                        <asp:Label ID="Label11" runat="server" Text="October" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox7" runat="server" TextMode="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label12" runat="server" Text="November" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox8" runat="server" TextMode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
                        <asp:Label ID="Label13" runat="server" Text="December" Visible ="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox9" runat="server" TextMode ="Number" Visible ="false"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="cancelButton" runat="server" Text="Cancel" Visible ="false" OnClick ="cancelEditing"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="saveButton" runat="server" Text="Save" Visible ="false" OnClick="saveButton_Click"/>
        </div>
    </form>
</body>
</html>
