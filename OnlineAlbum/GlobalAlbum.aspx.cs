using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineAlbum.Helpers;
using OnlineAlbum.UserControl;

namespace OnlineAlbum
{
    public partial class GlobalAlbum : System.Web.UI.Page
    {
        protected ImageDB m_imgDB = new ImageDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            userTitle.Text = "当前用户为:" + Session["userID"].ToString() + "/" + Session["userName"];
            // 读取并显示所有用户的图片。
            UpdateAllUsersImgs();
        }

        /*!
            \brief 读取并在首页显示所有用户的信息
        */
        private void UpdateAllUsersImgs()
        {
            var imgList = m_imgDB.GetAllUsersImgs();

            allUserImgPan.Controls.Clear();    // 清空现在userImgPan中的控件

            // 创建新的标签分类控件。
            ImageSortPanel newImgSortPanel = (ImageSortPanel)Page.LoadControl("~\\UserControl\\ImageSortPanel.ascx");

            newImgSortPanel.SetImgs(imgList);     // 向分类面板添加所有图像

            allUserImgPan.Controls.Add(newImgSortPanel);   // 将分类面板添加到userImgPan中
        }

        /*!
            \brief 跳转到用户个人页面。 
        */
        protected void goToPersonPageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\PersonalPage.aspx");
        }

        protected void logoutBtn_Click(object sender, EventArgs e)
        {
            Session["userID"] = null;
            Session["userName"] = null;
            Response.Redirect("~\\Login.aspx");
        }
    }
}