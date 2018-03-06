using OnlineAlbum.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum.Helpers
{
    public class ServerImage
    {
        public const string IMAGE_STORAGE_PATH = "~\\Images\\UserData\\";
        public const double DEFAULT_WIDTH_PERCENTAGE = 100;
        public const double DEFAULT_HEIGHT_PERCENTAGE = 100;

        public string m_imgID;
        public string m_imgName;
        public string m_userID;

        public ServerImage(string id, string name, string userID)
        {
            m_imgID = id;
            m_imgName = name;
            m_userID = userID;
        }

        /*!
            \brief 将图片信息转换为Web网页控件，能够添加到网页上面显示出来。 
        */
        public SingleImageOverView ToWebImage(Page thePage)
        {

            var newImgOverview = (SingleImageOverView)thePage.
                LoadControl("~\\UserControl\\SingleImageOverView.ascx");

            newImgOverview.Initialize(this, Unit.Percentage(DEFAULT_WIDTH_PERCENTAGE), Unit.Percentage(DEFAULT_HEIGHT_PERCENTAGE));

            return newImgOverview;
        }

        /*!
            \brief 获取所表示的图像在服务器上的路径，用于作为Image控件的显示图像。 
        */
        public string ToImageUrl()
        {
            return IMAGE_STORAGE_PATH + m_imgID;
        }
    }
}