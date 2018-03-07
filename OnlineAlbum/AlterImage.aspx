<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlterImage.aspx.cs" Inherits="OnlineAlbum.AlterImage" %>
<!--
    针对单个图像进行修改,包括重命名、更换、删除
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
                    <asp:Label ID="userName" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="imgNameLbl" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <span>
                        <asp:Image ID="imgPreview" runat="server" Width="50%" Height="50%"/>
                    </span>
                    <span>
                         <asp:Image ID="imgReplace" runat="server" Width="50%" Height="50%"/>
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    选择替换图像：
                    <asp:FileUpload ID="replaceImgUpload" runat="server" />
                    <asp:Label ID="replaceImgLackLbl" runat="server" Text="请选择要替换的图像"/>
                </td>
            </tr>
            <tr>
                <td>
                    重命名：
                    <asp:TextBox ID="renameText" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="renameText" ErrorMessage="名称不能为空" ForeColor="Red" ValidationGroup="RenameGroup" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:Button ID="replaceConfirmBtn" runat="server" Text="确认更换" OnClick="replaceConfirmBtn_Click" OnClientClick="return confirm('是否确认替换?')" />
                        <asp:Button ID="renameConfirmBtn" runat="server" Text="确认重命名" ValidationGroup="RenameGroup" OnClick="renameConfirmBtn_Click" OnClientClick="return confirm('是否确认重命名?')" />
                        <asp:Button ID="deleteConfirmBtn" runat="server" Text="确认删除" OnClick="deleteConfirmBtn_Click"  OnClientClick="return confirm('是否确认删除?')" />
                        <asp:Button ID="returnToPersonalPageBtn" runat="server" Text="返回个人中心" OnClick="returnToPersonalPage_Click" />
                        <asp:Button ID="returnToMainPageBtn" runat="server" Text="返回主页" OnClick="returnToMainPageBtn_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
