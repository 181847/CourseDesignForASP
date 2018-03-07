<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GlobalAlbum.aspx.cs" Inherits="OnlineAlbum.GlobalAlbum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>欢迎光临本站</p>
        <table style="width:100%">
            <tr>
                <td>
                    <asp:Label ID="userTitle" runat="server" style="float:left"></asp:Label>
                    <asp:Button ID="goToPersonPageBtn" runat="server" Text="个人中心" OnClick="goToPersonPageBtn_Click" style="float:right"/> 
                    <asp:Button ID="logoutBtn" runat="server" Text="退出登陆" OnClick="logoutBtn_Click" style="float:right"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="allUserImgPan" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
