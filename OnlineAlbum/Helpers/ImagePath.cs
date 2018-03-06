using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OnlineAlbum.Helpers
{
    public class ImagePath
    {
        public const int DEBUG_ADMIN_IMAGE_NUM = 6;

        /*!
            \brief 根据用户名和图片的ID生成对应图片的路径
            \param userName 用户名
            \param imageID 图片的ID，这个ID存储在数据库中用于唯一标识一个用户的一张布片。
        */
        public static string GeneratePath(string userName, string imageID)
        {
            return "~\\ImagePath\\userName\\imageID";
        }

        /*!
            \brief 测试专用，使用预先存在的图片进行界面的测试
            \param imageNum 测试使用的图像序号，序号从0开始，最多有DEBUG_ADMIN_IMAGE_NUM张。
         */
        public static string GenerateAdminPath(int imageNum)
        {
            // 检查序号的范围，序号从0开始。
            if (imageNum < 0 || imageNum >= DEBUG_ADMIN_IMAGE_NUM)
            {
                // 错误的调试图像序号
                throw new Exception("调试图像序号超出范围。");
            }

            return "~\\Images\\Administrator\\" + imageNum + ".jpg";
        }
    }
}