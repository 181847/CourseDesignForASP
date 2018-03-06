<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="OnlineAlbum.UploadImage" %>
<!--
    让一个用户上传图像
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
        <table>
            <tr>
                <td>
                    <asp:Label ID="showUserNameLbl" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
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
                    <asp:TextBox ID="renameImgTextBox" runat="server" />
                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="renameImgTextBox" ErrorMessage="图像名字不能为空" ForeColor="Red"/>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="confirmUpload" runat="server" Text="确认上传，完成后返回个人页面" OnClick="confirmUpload_Click"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
