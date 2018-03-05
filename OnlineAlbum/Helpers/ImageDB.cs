using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    public class ImageDB: BaseDataBase
    {

        /*!
            \brief 生成随机GUID，生成的序列号几乎不会有重复的
            将这些随机的序列号作为图像的ID值放在数据库中，并且作为本地文件名，保存起来。
        */
        public string GenerateImgID()
        {
            return "ERROR";
        }

        /*!
            \brief 在数据库中记录这个图像文件的信息
            \param imgID 随机GUID，每个图像的ID都不相同，而且一经记录就不再更改
            \param userID 添加到这个用户的图库中
            \param imgName 给这个图像起一个名字
            返回是否成功。
        */
        public bool AddTo(string imgID, string userID, string imgName)
        {
            return false;
        }

        /*!
            \brief 重命名一个用户的某个图像
            \param imgID 随机GUID，每个图像的ID都不相同，而且一经记录就不再更改
            \param userID 添加到这个用户的图库中
            \param newName 新名字
            返回是否成功。
        */
        public bool Rename(string imgID, string userID, string newName)
        {
            return false;
        }

        /*!
            \brief 将某张照片从该用户中删除
            \param imgID 照片的ID
            \param userID 指定的用户ID
            返回是否成功。
        */
        public bool Delete(string imgID, string userID)
        {
            return false;
        }
    }
}