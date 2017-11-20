<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Opportunities.aspx.cs" Inherits="ServiceTracker.Opportunities" %>



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
            <asp:Label ID="Label1" runat="server" Text ="Opportunities" Font-Size ="XX-Large"></asp:Label>
            <asp:TextBox ID="endDateTextBox" runat="server" TextMode ="Date" Visible = "false" Text='%# System.DateTime.Now%'></asp:TextBox>
            <asp:TextBox ID="startDateTextBox" runat="server" TextMode ="Date" Visible = "false"></asp:TextBox>
            <asp:GridView ID="oppsGridView" runat="server" CssClass ="mGrid" AutoGenerateColumns="False" DataSourceID="oppsDataSource">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="oppid" DataNavigateUrlFormatString="~\OppDetails.aspx?oppid={0}" DataTextField="oppid" HeaderText="Opportunity ID" SortExpression="oppid" />
                    <asp:BoundField DataField="equipmenttype" HeaderText="Equipment Type" SortExpression="equipmenttype" />
                    <asp:BoundField DataField="age" HeaderText="Age" SortExpression="age" />
                    <asp:BoundField DataField="custname" HeaderText="Customer Name" SortExpression="custname" />
                    <asp:TemplateField HeaderText="Status" SortExpression="Active">
                        <ItemTemplate><%# (Boolean.Parse(Eval("isClosed").ToString())) ? "Closed" : "Open" %></ItemTemplate>
                        </asp:TemplateField>
                    <asp:BoundField DataField="date" HeaderText="Date Opened" SortExpression="date" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="oppsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ServiceTrackerConnectionString %>" SelectCommand="SELECT [equipmenttype], [age], [custname], [isClosed], [date], [oppid] FROM [Opportunity] JOIN [Technician] ON Opportunity.tid = Technician.username WHERE (Technician.mid = 'jbschi02') AND ([isClosed] = @isClosed) AND date >= DATEADD(HOUR, -48, GETDATE()) AND date <  DATEADD(HOUR, 0, GETDATE())">
                <SelectParameters>
                    <asp:Parameter DefaultValue="false" Name="isClosed" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>
        &nbsp;&nbsp;&nbsp;
        </div>
        <br />
        <br />
    </form>
</body>
</html>