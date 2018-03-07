<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageSortPanel.ascx.cs" Inherits="OnlineAlbum.UserControl.ImageSortPanel" %>

<!--此页面用于内嵌到主页和个人页面中，包括一个标签列表、标签全选按钮、图片显示面板-->

<style>
    table td, table td * {
        vertical-align: top;
    }

</style>

<div>
    <table>
        <tr>
            <td>
                <div>
                    请选择要显示的所有标签：<br/>
                    <!--标签选择列表-->
                    <asp:ListBox ID="tagsShowList" runat="server" SelectionMode="Multiple" AutoPostBack="true" Rows="8" OnSelectedIndexChanged="tagsShowList_SelectedIndexChanged"/><br/>
                    <!--全选按钮，点击这个按钮就会默认选择显示所有标签，方便操作-->
                    <asp:Button ID="selectAllBtn" runat="server" Text="选择所有类型" OnClick="selectAllBtn_Click"/>
                </div>
            </td>
            <td>
                <!--显示图片的面板，将图片按照所属的标签分类显示，每一种标签的图像放在一个区域内，这个区域就是“~\UserControl\ImagePanel.ascx”，是另一个内嵌的页面-->
                <!--有的图片同时有多个标签，这些图片会在不同的区域中重复出现-->
                <asp:Panel ID="mainPanel" runat="server" />
            </td>
        </tr>
    </table>
</div>