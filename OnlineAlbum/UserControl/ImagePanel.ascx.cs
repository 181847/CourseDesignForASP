using OnlineAlbum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum.UserControl
{
    public partial class ImagePanel : System.Web.UI.UserControl
    {
        protected List<ServerImage> m_imgList;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetTagName(string tagName)
        {
            tagNameText.Text = tagName;
        }

        public void SetImgs(List<ServerImage> imgList)
        {
            m_imgList = imgList;

            foreach (var serverImg in m_imgList)
            {
                mainPanel.Controls.Add(serverImg.ToWebImage(Page));
            }
        }
    }
}