<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalPage.aspx.cs" Inherits="OnlineAlbum.PersonalPage" %>
<%@ Register Src="~/UserControl/ImageSortPanel.ascx" TagPrefix="uc1" TagName="ImageSortPanel" %>  

  
<!--PersonalPage 用于给用户管理自己的图片，上传、删除、重命名图片...-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <!--显示用户名和ID-->
                        <asp:Label ID="wellcomeUserLbl" runat="server" />
                        <!--转到主页的按钮，主页位置"~\GlobalAlbum.aspx"-->
                        <asp:Button ID="goToMainPageBtn" runat="server" Text="转到主页" OnClick="goToMainPageBtn_Click"/>
                        <!--添加新图像按钮，打开页面“~\UploadImage.aspx”-->
                        <asp:Button ID="UploadImgBtn" runat="server" Text="上传新图像" OnClick="UploadImgBtn_Click"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--图片面板，显示用户的所有图片以及标签，和主页上面显示所有图像的功能一样，内部由代码填充内容，内容格式参考“~\UserControl\ImageSortPanel.ascx”-->
                        <asp:Panel ID="userImgPan" runat="server"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
