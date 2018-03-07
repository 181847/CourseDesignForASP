<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImagePanel.ascx.cs" Inherits="OnlineAlbum.UserControl.ImagePanel" %>
<!--ImagePanel用于显示包含同一个标签的多个图像-->

<table>
    <tr>
        <td>
            <!--显示标签名-->
            <asp:Label ID="tagNameText" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <!--显示图像的面板-->
            <asp:Panel ID="mainPanel" runat="server" />
        </td>
    </tr>
</table>
