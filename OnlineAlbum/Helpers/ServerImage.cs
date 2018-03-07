using OnlineAlbum.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum.Helpers
{
    /*！
        \brief 用于保存从数据库中获取的图像信息
        包括图像ID、图像名、拥有这张图的用户ID。
        并且提供一个函数，将这个信息转换为一个Web上的控件，能够添加到网页上显示成图像。
    */
    public class ServerImage
    {
        public const string IMAGE_STORAGE_PATH = "~\\Images\\UserData\\";

        public string m_imgID;
        public string m_imgName;
        public string m_userID;

        /*!
            \brief 构造函数
            \param id 图像ID
            \param name 图像名
            \param userID 用户ID 
        */
        public ServerImage(string id, string name, string userID)
        {
            m_imgID = id;
            m_imgName = name;
            m_userID = userID;
        }

        /*!
            \brief 将图片信息转换为Web网页控件，能够添加到网页上面显示出来。 
            \param thePage 用于加载UserControl所需的网页内置对象
            这里生成的网页控件是一种UserControl，他需要使用ASP内置对象Page来进行加载，
            普通的C#类里面没有Page对象，但是在普通的网页代码里面（比如“Login.aspx.cs”）文件里面可以看到Page对象，
            所以需要从外部传递这个Page对象，才能够正常加载。
            关于这里面用到的UserControl，他就是另一个Html文件的组合，相当于一个内嵌的小页面，参考文件 “~\UserControl\SingleImageOverView.ascx”。
        */
        public SingleImageOverView ToWebImage(Page thePage)
        {

            var newImgOverview = (SingleImageOverView)thePage.
                LoadControl("~\\UserControl\\SingleImageOverView.ascx");

            newImgOverview.Initialize(this);

            return newImgOverview;
        }
    }
}