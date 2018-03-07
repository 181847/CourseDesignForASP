using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace OnlineAlbum.Helpers
{
    /*!
        \brief 工具类，包括一点和图像路径有关的操作以及数据。
    */
    public class ImagePath
    {
        /*!
            \brief 用于获取图片扩展名的正则表达式。 
        */
        private const string GET_IMG_EXTEND_NAME_PATTERN = @"^.*(\.[a-zA-Z]{2,4})$";

        /*!
            \brief 图片的存储位置。 
        */
        public const string IMAGE_STORAGE_PATH = "\\Images\\UserData\\";

        /*!
            \brief 获取图像的扩展名，带 点号，例如输入“img\user\00f3.jpg”返回“.jpg”
        */
        public static string GetExtensionNameWithDot(string fullName)
        {
            Match matchResult = Regex.Match(fullName, GET_IMG_EXTEND_NAME_PATTERN);
            return matchResult.Groups[1].Value;
        }
    }
}