using OnlineAlbum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum.UserControl
{
    public partial class SingleImageOverView : System.Web.UI.UserControl
    {
        public static string APP_PATH;

        /*!
            \brief 保存单张图片的ID、名称、所属用户。
        */
        public ServerImage m_serverImg;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            APP_PATH = Request.PhysicalApplicationPath;
        }

        /*!
            \brief 通过调用这个方法来显示设置这个图片模板的数据。
            \param showImg 被显示的图片信息，包括图片的ID、名称、所属用户。 
        */
        public void Initialize(ServerImage showImg, Unit imgWidth, Unit imgHeight)
        {
            m_serverImg = showImg;
            UpdateImg();
        }

        /*!
            \brief 根据内部存储的信息，更新显示的数据。
            \param showImg 被显示的图片信息，包括图片的ID、名称、所属用户。 
        */
        private void UpdateImg()
        {
            imageBtn.ImageUrl = m_serverImg.ToImageUrl();
            imageName.Text = m_serverImg.m_imgName;
            userNameLbl.Text = "ID:" + m_serverImg.m_userID;
            
            //System.Drawing.Image img = System.Drawing.Image.FromFile(Request.PhysicalApplicationPath + "Images\\UserData\\" + m_serverImg.m_imgID);
            //if (img.Width > img.Height)
            //{
            //    imageBtn.Width = Unit.Percentage(100);
            //}
            //else
            //{
            //    imageBtn.Height = Unit.Percentage(100);
            //}

        }
    }
}