<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlterImage.aspx.cs" Inherits="OnlineAlbum.AlterImage" %>
<!--针对单个图像进行修改,包括重命名、更换、删除、编辑标签-->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table>
                <tr>
                    <td>
                        <!---用于在编辑界面显示用户名，作用不是太大，我主要把它当作测试用的信息，检查当前用户是否正确-->
                        <asp:Label ID="userName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--显示图像的名称-->
                        <asp:Label ID="imgNameLbl" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>
                            <!--预览被修改的图像-->
                            <asp:Image ID="imgPreview" runat="server" Height="150"/>
                        </span>
                        <span>
                            <!--如果用户选择上传新图像，新图像会在这里显示出来-->
                             <asp:Image ID="imgReplace" runat="server" Height="150"/>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        选择替换图像：
                        <!--上传替换用的图像-->
                        <asp:FileUpload ID="replaceImgUpload" runat="server" />
                        <!--验证控件，当替换按钮被按下时（注意ValidationGroup属性），确保用户已经上传了用于替换的图像-->
                        <asp:RequiredFieldValidator ID="requireReplaceImgFileUploadValidator" runat="server" ValidationGroup="replaceImgValidationGroup" ControlToValidate="replaceImgUpload" ErrorMessage="*请上传替换图像" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        重命名：
                        <!--重命名文本输入框-->
                        <asp:TextBox ID="renameText" runat="server" />
                        <!--验证控件，当重命名按钮按下时，确保文本框中有内容-->
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="renameText" ErrorMessage="名称不能为空" ForeColor="Red" ValidationGroup="RenameGroup" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--下面是主要的操作区域，都是按钮控件，有的按钮控件有ValidationGroup属性，保证对应的上传控件、文本框不为空-->
                        <div>
                            <asp:Button ID="replaceConfirmBtn" runat="server" Text="确认更换" ValidationGroup="replaceImgValidationGroup" OnClick="replaceConfirmBtn_Click" OnClientClick="return confirm('是否确认替换?')" />
                            <asp:Button ID="renameConfirmBtn" runat="server" Text="确认重命名" ValidationGroup="RenameGroup" OnClick="renameConfirmBtn_Click" OnClientClick="return confirm('是否确认重命名?')" />
                            <asp:Button ID="deleteConfirmBtn" runat="server" Text="确认删除" OnClick="deleteConfirmBtn_Click"  OnClientClick="return confirm('是否确认删除?')" />
                            <asp:Button ID="returnToPersonalPageBtn" runat="server" Text="返回个人中心" OnClick="returnToPersonalPage_Click" />
                            <asp:Button ID="returnToMainPageBtn" runat="server" Text="返回主页" OnClick="returnToMainPageBtn_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--标签修改部分-->
        <div>
            <div>
                <p>标签列表：</p>
                <!--显示当前图片已有的标签-->
                <asp:ListBox ID="tagList" runat="server" SelectionMode="Multiple"/>
            </div>
            <div>
                <div>
                    标签名:
                    <!--当添加新标签的时候在这里输入标签名-->
                    <asp:TextBox ID="addTagText" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="addTagText" ValidationGroup="tagNameRequireGroup" ErrorMessage="请填写标签名" ForeColor="Red"/>
                </div>
                <!--标签操作区域-->
                <div>
                    <asp:Button ID="addTagBtn" runat="server" Text="添加标签" ValidationGroup="tagNameRequireGroup" OnClick="addTagBtn_Click"/>
                    <asp:Button ID="deleteTagBtn" runat="server" Text="删除标签" OnClick="deleteTagBtn_Click" OnClientClick="return confirm('确认删除标签？');"/>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
