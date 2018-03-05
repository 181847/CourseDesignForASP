<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OnlineAlbum.Register" %>

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
                    <asp:TextBox ID="userNameText" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="userNameValidator" runat="server" ControlToValidate="userNameText" OnServerValidate="userNameValidator_ServerValidate" ForeColor="Red"/>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    密码：
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="passwordText" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="passwordValidator" ValidationExpression="[a-zA-Z0-9]{6,12}" runat="server" ControlToValidate="passwordText" ErrorMessage="密码格式错误" ForeColor="Red"/>
                </td>
            </tr>
            <tr>
                <td>
                    确认密码：
                </td>
                <td>
                    <asp:TextBox ID="confirmPasswordText" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="passwordEqualValidator" runat="server" Type="String" ControlToValidate="passwordText" ControlToCompare="confirmPasswordText" Operator="Equal" ErrorMessage="密码不一致" ForeColor="Red"/>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="RegisterConfirmBtn" runat="server" Text="确认注册" OnClick="RegisterConfirmBtn_Click"/>
                </td>
                <td>
                    <asp:Button ID="GoToLoginBtn" runat="server" Text="我已有账号，直接登陆" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
