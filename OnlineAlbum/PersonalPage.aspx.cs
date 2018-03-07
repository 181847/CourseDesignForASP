using OnlineAlbum.Helpers;
using OnlineAlbum.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum
{
    public partial class PersonalPage : System.Web.UI.Page
    {
        protected string m_userID;
        protected string m_userName;
        protected static UserDB m_userDB = new UserDB();
        protected static ImageDB m_imgDB = new ImageDB();
        protected List<ServerImage> m_tempImgList;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_userID = Session["userID"].ToString();
            m_userName = Session["userName"].ToString();

            wellcomeUserLbl.Text = "欢迎您" + m_userID + "/" + m_userName;
            
            if (m_userID == "")
            {
                throw new Exception("错误，尚未登陆任何用户，无法查看个人网页");
            }
            
            ReadImages();
        }

        /*!
            \brief 读取这个用户的所有图像，显示到个人页面上。
        */
        protected void ReadImages()
        {
            m_tempImgList = m_imgDB.GetOneUsersImgs(m_userID);  // 获取这个用户的所有图像

            //userSortedImgPan.SetImgs(m_tempImgList);

            userImgPan.Controls.Clear();    // 清空现在userImgPan中的控件

            // 创建新的标签分类控件。
            ImageSortPanel newImgSortPanel = (ImageSortPanel)Page.LoadControl("~\\UserControl\\ImageSortPanel.ascx");

            newImgSortPanel.SetImgs(m_tempImgList);     // 向分类面板添加所有图像

            userImgPan.Controls.Add(newImgSortPanel);   // 将分类面板添加到userImgPan中
        }

        protected void UploadImgBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\UploadImage.aspx");
        }

        protected void goToMainPageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\GlobalAlbum.aspx");
        }
    }
}