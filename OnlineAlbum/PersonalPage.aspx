<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalPage.aspx.cs" Inherits="OnlineAlbum.PersonalPage" %>
<!--
    PersonalPage 用于给用户管理自己的图片，上传、删除、重命名图片...
    -->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="wellcomeUserLbl" runat="server" />
        <asp:Panel ID="userImgPan" runat="server" />
        <asp:Button ID="goToMainPageBtn" runat="server" Text="转到主页" />
        <asp:Button ID="UploadImgBtn" runat="server" Text="上传新图像" />
    </div>
    </form>
</body>
</html>
