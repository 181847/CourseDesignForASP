using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected string m_userID;
        protected string m_userName;

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateUser();
        }

        /*!
            \brief 从Session信息中更新当前用户名
        */
        protected void UpdateUser()
        {
            m_userID = Session["userID"].ToString();
            m_userName = Session["userName"].ToString();
            showUserNameLbl.Text = "用户：" + m_userID + "/" + m_userName + "正在上传新图像";
        }

        protected void imgFileUploadBtn_Unload(object sender, EventArgs e)
        {
            
        }
    }
}