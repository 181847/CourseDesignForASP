<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineAlbum.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>登陆界面</p>
        <table>
            <tr>
                <td>
                    用户名：
                </td>
                <td>
                    <asp:TextBox ID="userIDText" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="用户名必填" ForeColor="Red" ControlToValidate="userIDText" />
                    <asp:CustomValidator ID="userIDValidator" runat="server" ControlToValidate="userIDText" OnServerValidate="userIDValidator_ServerValidate" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    密码:
                </td>
                <td>
                    <asp:TextBox ID="passwordText" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="密码必填" ForeColor="Red" ControlToValidate="passwordText" />
                    <asp:Label ID="passwordWarningLbl" runat="server" Text="密码错误" ForeColor="Red" Visible="false"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="registerBtn" runat="server" Text="注册新用户"  OnClick="registerBtn_Click" CausesValidation="false"/>
                </td>
                <td>
                    <asp:Button ID="loginBtn" runat="server" Text="登陆" OnClick="loginBtn_Click"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
