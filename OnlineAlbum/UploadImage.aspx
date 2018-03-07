<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="OnlineAlbum.UploadImage" %>
<!--用户上传新图像的界面，没有太多的编辑功能（只能重命名图像），没有上传图像的预览（因为暂时没有找到图像上传成功后自动触发代码的功能，
    如果要用户手动点个按钮，然后再显示图像的话 感觉不太方便，所以就没加）-->
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
                    <!--显示用户名和用户ID-->
                    <asp:Label ID="showUserNameLbl" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <!--上传图像控件-->
                    <asp:FileUpload ID="imgFileUploadBtn" runat="server" />
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Image ID="previewImg" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    重命名：
                    <!--重命名文本框，如果用户没有填的话，默认 图像名 为 上传文件的名字-->
                    <asp:TextBox ID="renameImgTextBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <!--确认上传按钮-->
                    <asp:Button ID="confirmUpload" runat="server" Text="确认上传，完成后返回个人页面" OnClick="confirmUpload_Click"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
