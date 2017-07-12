<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Jobs.aspx.cs" Inherits="ServiceTracker.Jobs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="NavigationBar.css" rel="stylesheet" />
    <link href="Table.css" rel="stylesheet" />
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
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <asp:Label ID="Label1" runat="server" Text="Jobs" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <div style="width:850px;height:150px;border:1px solid #eaeaea; background-color: #eaeaea">

            &nbsp;&nbsp;
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
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Service Type"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="jobTypeDropDownList" runat="server">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="From"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="startDateTextBox" runat="server" TextMode ="Date"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label8" runat="server" Text="To"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="endDateTextBox" runat="server" TextMode ="Date"></asp:TextBox>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Sort by"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList3" runat="server">
            </asp:DropDownList>

        &nbsp;&nbsp;&nbsp;
            <asp:Button ID="loadDataButon" runat="server" Text="Reload Data" OnClick ="loadData"/>

        </div>
        <br />
        <br />
        <div>
            <asp:GridView ID="jobsGridView" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                    <asp:BoundField DataField="custname" HeaderText="Customer Name" SortExpression="custname" />
                    <asp:BoundField DataField="servicetype" HeaderText="Service Type" SortExpression="servicetype" />
                    <asp:BoundField DataField="cost" HeaderText="Cost" SortExpression="cost" />
                    <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" />
                    <asp:BoundField DataField="tid" HeaderText="Technician" SortExpression="tid" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
