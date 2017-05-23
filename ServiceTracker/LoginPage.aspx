<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="ServiceTracker.LoginPage" %>
<link href="LoginPage.css" rel="stylesheet" />

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class ="center">
        <asp:Label ID="LoginTitleLabel" runat="server" Text="Service Tracker" Font-Size="XX-Large"></asp:Label>
        <table class = "centerLogin">
            <tr>
                <td>
                    <asp:Button Text="Login" ID="loginTab" CssClass="Initial" runat="server"
                        OnClick="Login_Tab_Click" />
                    <asp:Button Text="Register" ID="registerTab" CssClass="Initial" runat="server"
                        OnClick="Register_Tab_Click" />
                    <asp:MultiView ID="TabView" runat="server">
                        <asp:View ID="LoginView" runat="server">
                            <asp:Panel runat="server" DefaultButton="LoginButton">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                <tr>
                                    <td>
                                        <h3>
                                            <asp:Label ID="LoginLabel" runat="server" Text="Login"></asp:Label>
                                        </h3>
                                        <p>
                                            <asp:Label ID="MIDLabel" runat="server" Text="Manager ID"></asp:Label>
                                            :&nbsp;
                                            <asp:TextBox ID="MIDText" runat="server"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:Label ID="MPasswordLabel" runat="server" Text="Password"></asp:Label>
                                            :&nbsp; &nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="MPasswordText" runat="server" TextMode ="Password"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick = "DoLoginStuff"/>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="noMIDLabel" runat="server" Text="Please enter a Manager ID." CssClass ="missingInfo" Visible ="false"></asp:Label>
                                            <asp:Label ID="noPasswordLabel" runat="server" Text="Please enter a password." CssClass = "missingInfo" Visible ="false"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="RegisterView" runat="server">
                            <asp:Panel runat="server" DefaultButton="RegisterButton">
                            <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                                <tr>
                                    <td class="auto-style1">
                                        <h3>
                                            <asp:Label ID="RegisterLabel" runat="server" Text="Register New Manager"></asp:Label>
                                        </h3>
                                        <p>
                                            <asp:Label ID="newMIDLabel" runat="server" Text="Manager ID:"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="newMIDText" runat="server"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:Label ID="newPasswordLabel" runat="server" Text="Password:"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                            <asp:TextBox ID="newPasswordText" runat="server" style="margin-bottom: 0px" TextMode ="Password"></asp:TextBox>
                                            &nbsp;
                                            <asp:Label ID="passwordReqLabel" runat="server" Text="*Password must be at least 8 characters." Visible ="false" CssClass ="missingInfo"></asp:Label>
                                        </p>
                                        <p>
                                            <asp:Label ID="confirmPasswordLabel" runat="server" Text="Confirm Password:"></asp:Label>
                                            &nbsp;
                                            <asp:TextBox ID="confirmPasswordText" runat="server" TextMode ="Password"></asp:TextBox>
                                        </p>
                                        <p>
                                            <asp:Button ID="RegisterButton" runat="server" Text="Register" OnClick = "RegisterManager"/>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="noMIDRegisterLabel" runat="server" Text="Please enter a Managaer ID." Visible = "false" CssClass = "missingInfo"></asp:Label>
                                            <asp:Label ID="noFirstPasswordLabel" runat="server" Text="Please enter your password." Visible = "false" CssClass =" missingInfo"></asp:Label>
                                            <asp:Label ID="noSecondPasswordLabel" runat="server" Text="Please confirm your password." Visible = "false" CssClass =" missingInfo"></asp:Label>
                                            <asp:Label ID="passwordsDifferentLabel" runat="server" Text="The two passwords entered were not the same." Visible ="false" CssClass ="missingInfo"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
