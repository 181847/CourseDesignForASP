using OnlineAlbum.Helpers;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            m_userID = Session["userID"].ToString();
            m_userName = m_userDB.GetNickNameOf(m_userID);

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

        }
    }
}