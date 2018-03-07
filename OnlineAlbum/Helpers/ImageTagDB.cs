using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    /*!
        \brief 管理图片以及标签之间的联系 
    */
    public class ImageTagDB:BaseDataBase
    {
        protected ImageDB m_imgDB = new ImageDB();
        protected TagDB m_tagDB = new TagDB();
        protected UserDB m_userDB = new UserDB();

        protected SqlCommand m_tagOperationCmd;
        protected SqlCommand m_checkImgTagCmd;
        protected SqlCommand m_getTagsCmd;
        protected SqlCommand m_removeOneTagCmd;
        protected SqlCommand m_removeAllTagCmd;

        public ImageTagDB()
        {
        }

        protected override void BuildSqlCmds()
        {
            m_tagOperationCmd = new SqlCommand("INSERT INTO ImgTagTable VALUES (@imgID, @tagID, @userID)", m_connection);
            m_tagOperationCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_tagOperationCmd.Parameters.Add("@tagID", SqlDbType.Char);
            m_tagOperationCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_checkImgTagCmd = new SqlCommand("SELECT count(*) FROM ImgTagTable, TagTable WHERE ImgTagTable.TAG_ID = TagTable.TAG_ID AND IMG_ID = @imgID AND TAG_NAME = @tagName AND USER_ID = @userID", m_connection);
            m_checkImgTagCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_checkImgTagCmd.Parameters.Add("@tagName", SqlDbType.NChar);
            m_checkImgTagCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_getTagsCmd = new SqlCommand("SELECT TagTable.TAG_ID AS TAG_ID, TagTable.TAG_NAME AS TAG_NAME FROM ImgTagTable, TagTable WHERE ImgTagTable.TAG_ID = TagTable.TAG_ID AND IMG_ID = @imgID AND USER_ID = @userID", m_connection);
            m_getTagsCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_getTagsCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_removeOneTagCmd = new SqlCommand("DELETE FROM ImgTagTable WHERE IMG_ID = @imgID AND TAG_ID = @tagID and USER_ID = @userID", m_connection);
            m_removeOneTagCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_removeOneTagCmd.Parameters.Add("@tagID", SqlDbType.Char);
            m_removeOneTagCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_removeAllTagCmd = new SqlCommand("DELETE FROM ImgTagTable WHERE IMG_ID = @imgID AND USER_ID = @userID", m_connection);
            m_removeAllTagCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_removeAllTagCmd.Parameters.Add("@userID", SqlDbType.Char);
        }


        /*!
            \brief  给某个用户的某个图片添加一个标签
            \param imgID 图片ID
            \param tagID 标签ID
            \param userID 用户ID
            以下情况返回false
            {
                1. 图片不存在
                2. 标签不存在
                3. 图片在此之前就已经被打上指定的标签
                4. 与数据库的通信发生错误
            }
            添加成功返回true
        */
        public bool TagImg(string imgID, string tagID, string userID)
        {
            bool success = false;

            if ( ! m_imgDB.ExistImg(imgID, userID)  // 不存在这个用户的图片
                || ! m_tagDB.AlreadyHaveID(tagID)   // 不存在这个标签
                || ! m_userDB.AlreadyHave(userID)   // 不存在这个用户
                || AlreadyHasBeenTagged(imgID, tagID, userID))  // 图片已经被打上标签。
            {
                // 添加标签失败
                return false;
            }

            try
            {
                m_connection.Open();
                m_tagOperationCmd.Parameters["@imgID"].Value = imgID;
                m_tagOperationCmd.Parameters["@tagID"].Value = tagID;
                m_tagOperationCmd.Parameters["@userID"].Value = userID;

                int count = m_tagOperationCmd.ExecuteNonQuery();

                if (count == 1)
                {
                    success = true;
                }
            }
            catch (SqlException ex)
            {
                success = false;
            }
            finally
            {
                m_connection.Close();
            }

            return success;
        }

        /*!
            \brief 检查图片是否已经被打上标签
            \param imgID 图片ID
            \param tagID 标签ID
            \param userID 用户ID
        */
        public bool AlreadyHasBeenTagged(string imgID, string tagName, string userID)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_checkImgTagCmd.Parameters["@imgID"].Value = imgID;
                m_checkImgTagCmd.Parameters["@tagName"].Value = tagName;
                m_checkImgTagCmd.Parameters["@userID"].Value = userID;

                int searchCount = (int)m_checkImgTagCmd.ExecuteScalar();

                if (searchCount == 1)
                {
                    success = true;
                }
            }
            catch (SqlException ex)
            {
                success = false;
            }
            finally
            {
                m_connection.Close();
            }

            return success;
        }

        /*!
            \brief 获取一个图片的所有标签
            \param imgID 图片ID
            \param userID 用户ID
        */
        public List<ServerTag> GetTagsOf(string imgID, string userID)
        {
            var tagList = new List<ServerTag>();

            try
            {
                m_connection.Open();
                m_getTagsCmd.Parameters["@imgID"].Value = imgID;
                m_getTagsCmd.Parameters["@userID"].Value = userID;

                SqlDataReader dr = m_getTagsCmd.ExecuteReader();

                while (dr.Read())
                {
                    tagList.Add(new ServerTag(dr["TAG_ID"].ToString(), dr["TAG_NAME"].ToString()));
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                // 什么也不做，tagList为空
            }
            finally
            {
                m_connection.Close();
            }

            return tagList;
        }

        /*!
            \brief 去除一个图片上的标签
            \param imgID 图片ID
            \param tagID 标签ID
            \param userID 用户ID
            以下情况返回false
            {
                1. 图片不存在
                2. 标签不存在
                3. 图片在此之前就没有这样的标签
                4. 与数据库的通信发生错误
            }
            其他成功情况返回true。
        */
        public bool RemoveTag(string imgID, string tagID, string userID)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_removeOneTagCmd.Parameters["@imgID"].Value = imgID;
                m_removeOneTagCmd.Parameters["@tagID"].Value = tagID;
                m_removeOneTagCmd.Parameters["@userID"].Value = userID;

                int deleteCount = m_removeOneTagCmd.ExecuteNonQuery();

                if (deleteCount == 1)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (SqlException ex)
            {
                success = false;
            }
            finally
            {
                m_connection.Close();
            }

            return success;
        }

        /*!
            \brief 去除一个图片的所有标签
            \param imgID 图片ID
            \param userID 用户ID
            此方法不检查图片是否存在，只是删除对应用户图片的标签
            如果图片不存在的时候，依然返回true。
            只有数据库通信发生错误的时候才会返回false。
        */
        public bool RemoveAllTag(string imgID, string userID)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_removeAllTagCmd.Parameters["@imgID"].Value = imgID;
                m_removeAllTagCmd.Parameters["@userID"].Value = userID;

                m_removeAllTagCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                success = false;
            }
            finally
            {
                m_connection.Close();
            }

            return success;
        }
    }
}