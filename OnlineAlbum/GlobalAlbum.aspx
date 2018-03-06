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
        <div>
            <asp:Label ID="userTitle" runat="server" ></asp:Label>
            <asp:Button ID="goToPersonPageBtn" runat="server" OnClick="goToPersonPageBtn_Click"/> 
        </div>
        <asp:Panel ID="allUserImgPan" runat="server" />
    </div>
    </form>
</body>
</html>
