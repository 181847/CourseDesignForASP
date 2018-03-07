<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineAlbum.Register" %>
<!--新用户注册页面-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>注册界面</p>
        <table>
            <tr>
                <td>
                    用户名：
                </td>
                <td>
                    <!--输入用户ID-->
                    <asp:TextBox ID="userNameText" runat="server"></asp:TextBox>
                    <!--验证控件，确保用户ID不为空-->
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="用户名必填" ForeColor="Red" ControlToValidate="userNameText" />
                    <!--验证控件，确保用户ID尚未注册-->
                    <asp:CustomValidator ID="userNameValidator" runat="server" ControlToValidate="userNameText" OnServerValidate="userNameValidator_ServerValidate" ForeColor="Red"/>
                </td>
            </tr>
            <tr>
                <td>
                    昵称：
                </td>
                <td>
                    <!--用户昵称，昵称和用户ID不同，不同用户之间的昵称允许重复,而且注册的时候不强制用户一定要写一个昵称，如果不填的话，昵称默认为用户ID-->
                    <asp:TextBox ID="nickNameText" runat="server"></asp:TextBox>
            </tr>
            <tr>
                <td class="auto-style1">
                    密码：
                </td>
                <td class="auto-style1">
                    <!--密码输入框-->
                    <asp:TextBox ID="passwordText" runat="server"></asp:TextBox>
                    <!--验证密码不为空-->
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="密码必填" ForeColor="Red" ControlToValidate="passwordText" />
                    <!--确保密码符合正则表达式，“[a-zA-Z0-9]{6,12}”，即6至12为字母或数字-->
                    <asp:RegularExpressionValidator ID="passwordValidator" ValidationExpression="[a-zA-Z0-9]{6,12}" runat="server" ControlToValidate="passwordText" ErrorMessage="密码格式错误" ForeColor="Red"/>
                </td>
            </tr>
            <tr>
                <td>
                    确认密码：
                </td>
                <td>
                    <!--确认密码框-->
                    <asp:TextBox ID="confirmPasswordText" runat="server"></asp:TextBox>
                    <!--验证控件，保证 密码框 和 确认密码框 的内容一样-->
                    <asp:CompareValidator ID="passwordEqualValidator" runat="server" Type="String" ControlToValidate="passwordText" ControlToCompare="confirmPasswordText" Operator="Equal" ErrorMessage="密码不一致" ForeColor="Red"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <!--注册按钮-->
                    <asp:Button ID="RegisterConfirmBtn" runat="server" Text="确认注册" OnClick="RegisterConfirmBtn_Click"/>
                </td>
                <td>
                    <!--转到登陆页面，“~\Login.aspx”-->
                    <asp:Button ID="GoToLoginBtn" runat="server" Text="我已有账号，直接登陆" OnClick="GoToLoginBtn_Click" CausesValidation="false"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
