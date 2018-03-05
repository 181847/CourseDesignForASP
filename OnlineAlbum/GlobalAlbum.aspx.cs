using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnlineAlbum.Helpers;

namespace OnlineAlbum
{
    public partial class GlobalAlbum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ImagePath.DEBUG_ADMIN_IMAGE_NUM; ++i)
            {
                Image newImage = new Image();

                newImage.ImageUrl = ImagePath.GenerateAdminPath(i);
                newImage.Width = Unit.Percentage(50);
                newImage.Height = Unit.Percentage(50);

                PictureHolder.Controls.Add(newImage);
            }
        }
    }
}