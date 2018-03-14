<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SingleImageOverView.ascx.cs" Inherits="OnlineAlbum.UserControl.SingleImageOverView" %>

<!--
    SingleImageOverView 用于显示单张图片，在日后可以添加诸如标签显示等选项
    -->
<style>
    .OutlineSize
    {
        width:auto;
        height:200px;
        margin: 5px;
        float:left;
    }
</style>
<div class="OutlineSize">
    <!--图片按钮，显示图片，并且当图片被点击的时候自动跳转到修改图像的页面。
        但是，如果当前的登陆用户 不是 这个图片的拥有者的话，不会发生跳转。-->
    <asp:ImageButton ID="imageBtn" runat="server" Height ="150px" OnClick="imageBtn_Click"/>
    <div>
        <!--显示图片名-->
        <asp:Label ID="imageName" runat="server" />
    </div>
    <div>
        <!--显示所属的用户名，目前显示的是用户的ID，测试用-->
        <asp:Label ID="userNameLbl" runat="server" />
    </div>
    <div>
        <!--显示图片的上传日期-->
        <asp:Label ID="uploadDTLbl" runat="server" />
    </div>
</div>
