<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OnlineAlbum.Login" %>
<!--登陆页面-->
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
                    <!--用户ID输入框-->
                    <asp:TextBox ID="userIDText" runat="server"></asp:TextBox>
                    <!--验证控件，保证用户ID不为空-->
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="用户名必填" ForeColor="Red" ControlToValidate="userIDText" />
                    <!--自定义验证控件，用户ID已经注册-->
                    <asp:CustomValidator ID="userIDValidator" runat="server" ControlToValidate="userIDText" OnServerValidate="userIDValidator_ServerValidate" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    密码:
                </td>
                <td>
                    <!--密码输入框-->
                    <asp:TextBox ID="passwordText" runat="server" />
                    <!--保证密码不为空-->
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="密码必填" ForeColor="Red" ControlToValidate="passwordText" />
                    <!--用于显示密码错误的文本提示信息，这个地方我不想用CustomValidator，由代码直接控制这个标签的显示和隐藏-->
                    <asp:Label ID="passwordWarningLbl" runat="server" Text="密码错误" ForeColor="Red" Visible="false"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <!--转到注册页面，“~\Register.aspx”-->
                    <asp:Button ID="registerBtn" runat="server" Text="注册新用户"  OnClick="registerBtn_Click" CausesValidation="false"/>
                </td>
                <td>
                    <!--确认登陆-->
                    <asp:Button ID="loginBtn" runat="server" Text="登陆" OnClick="loginBtn_Click"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
