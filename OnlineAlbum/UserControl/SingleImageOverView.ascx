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
    <asp:ImageButton ID="imageBtn" runat="server" Height ="150px"/>
    <div>
        <asp:Label ID="imageName" runat="server" />
    </div>
    <div>
        <asp:Label ID="userNameLbl" runat="server" />
    </div>
</div>
