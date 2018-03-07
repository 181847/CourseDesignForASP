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
                    <!--显示用户名和ID，实际作用不大-->
                    <asp:Label ID="userTitle" runat="server" style="float:left"></asp:Label>
                    <!--转到个人页面按钮，这里面的style信息可以自行修改，我目前让他靠向右侧-->
                    <asp:Button ID="goToPersonPageBtn" runat="server" Text="个人中心" OnClick="goToPersonPageBtn_Click" style="float:right"/> 
                    <!--退出登陆按钮，这里面的style信息可以自行修改，我目前让他靠向右侧-->
                    <asp:Button ID="logoutBtn" runat="server" Text="退出登陆" OnClick="logoutBtn_Click" style="float:right"/>
                </td>
            </tr>
            <tr>
                <td>
                    <!--显示全局图片的面板，在运行的过程中由代码填充内容物：标签和图片，参考"~\UserControl\ImageSortPanel.ascx"-->
                    <asp:Panel ID="allUserImgPan" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
