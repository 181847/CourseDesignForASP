using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    /*!
        \brief 图片信息数据库 
    */
    public class ImageDB: BaseDataBase
    {
        /*!
            \brief GUID字符串的格式，参考 https://docs.microsoft.com/zh-cn/dotnet/api/system.guid.tostring?view=netcore-2.0#System_Guid_ToString_System_String_
        */
        private const string GUID_STRING_FORMAT = "N";
        
        protected SqlCommand m_addCmd;
        protected SqlCommand m_renameCmd;
        protected SqlCommand m_deleteCmd;
        protected SqlCommand m_findUserImgsCmd;
        protected SqlCommand m_findAllImgsCmd;
        protected SqlCommand m_existOneImgCmd;

        protected override void BuildSqlCmds()
        {
            m_addCmd = new SqlCommand("INSERT INTO ImageTable VALUES (@imgID, @userID, @imgName)", m_connection);
            m_addCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_addCmd.Parameters.Add("@userID", SqlDbType.Char);
            m_addCmd.Parameters.Add("@imgName", SqlDbType.NChar);


            m_renameCmd = new SqlCommand("UPDATE ImageTable SET IMG_NAME = @imgName WHERE IMG_ID = @imgID AND USER_ID = @userID", m_connection);
            m_renameCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_renameCmd.Parameters.Add("@userID", SqlDbType.Char);
            m_renameCmd.Parameters.Add("@imgName", SqlDbType.NChar);

            m_deleteCmd = new SqlCommand("DELETE FROM ImageTable WHERE IMG_ID = @imgID AND USER_ID = @userID", m_connection);
            m_deleteCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_deleteCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_findUserImgsCmd = new SqlCommand("SELECT * FROM ImageTable WHERE USER_ID = @userID", m_connection);
            m_findUserImgsCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_findAllImgsCmd = new SqlCommand("SELECT * FROM ImageTable", m_connection);

            m_existOneImgCmd = new SqlCommand("SELECT count(*) FROM ImageTable WHERE IMG_ID = @imgID and USER_ID = @userID", m_connection);
            m_existOneImgCmd.Parameters.Add("@imgID", SqlDbType.Char);
            m_existOneImgCmd.Parameters.Add("@userID", SqlDbType.Char);
        }

        /*!
            \brief 生成随机GUID，生成的序列号几乎不会有重复的
            将这些随机的序列号作为图像的ID值放在数据库中，并且作为本地文件名，保存起来。
        */
        public string GenerateImgID()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid.ToString(GUID_STRING_FORMAT);
        }

        /*!
            \brief 在数据库中记录这个图像文件的信息
            \param imgID 图像的ID，同时也是这个图像的文件名，形如“2f0be454d467440e8916f208d7d5ad19.jpg”，有扩展名。
            \param userID 添加到这个用户的图库中
            \param imgName 给这个图像起一个名字
            返回是否成功。
        */
        public bool AddTo(string imgID, string userID, string imgName)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_addCmd.Parameters["@userID"].Value = userID;
                m_addCmd.Parameters["@imgID"].Value = imgID;
                m_addCmd.Parameters["@imgName"].Value = imgName;

                int count = m_addCmd.ExecuteNonQuery();

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
            \brief 重命名一个用户的某个图像
            \param imgID 被修改的图像ID
            \param userID 包含这个图像的用户ID
            \param newName 新名字
            返回是否成功。
        */
        public bool Rename(string imgID, string userID, string newName)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_renameCmd.Parameters["@userID"].Value = userID;
                m_renameCmd.Parameters["@imgID"].Value = imgID;
                m_renameCmd.Parameters["@imgName"].Value = newName;

                int count = m_renameCmd.ExecuteNonQuery();

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
            \brief 将某张照片从该用户中删除
            \param imgID 照片的ID
            \param userID 指定的用户ID
            返回是否成功。
        */
        public bool Delete(string imgID, string userID)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_deleteCmd.Parameters["@userID"].Value = userID;
                m_deleteCmd.Parameters["@imgID"].Value = imgID;

                int count = m_deleteCmd.ExecuteNonQuery();

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
            \brief 获取某一个用户的所有图片信息，图片信息内部包括图片ID、图片名、所属的用户ID，
            图片信息的结构参考“~\Helpers\ServerImage.cs”
        */
        public List<ServerImage> GetOneUsersImgs(string userID)
        {
            List<ServerImage> imgList = new List<ServerImage>();

            try
            {
                m_connection.Open();
                m_findUserImgsCmd.Parameters["@userID"].Value = userID;

                SqlDataReader dr = m_findUserImgsCmd.ExecuteReader();

                while(dr.Read())
                {
                    imgList.Add(new ServerImage(
                        dr["IMG_ID"].ToString(), 
                        dr["IMG_NAME"].ToString(), 
                        dr["USER_ID"].ToString()));
                }

                dr.Close();
            }
            catch(SqlException ex)
            {
                throw new Exception("读取用户的图片时出错");
            }
            finally
            {
                m_connection.Close();
            }

            return imgList;
        }

        /*!
            \brief 获取所有用户的所有图像
        */
        public List<ServerImage> GetAllUsersImgs()
        {
            List<ServerImage> imgList = new List<ServerImage>();

            try
            {
                m_connection.Open();

                SqlDataReader dr = m_findAllImgsCmd.ExecuteReader();

                while (dr.Read())
                {
                    imgList.Add(new ServerImage(
                        dr["IMG_ID"].ToString(),
                        dr["IMG_NAME"].ToString(),
                        dr["USER_ID"].ToString()));
                }

                dr.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("读取用户的图片时出错");
            }
            finally
            {
                m_connection.Close();
            }

            return imgList;
        }

        /*!
            \brief 检查某个用户是否拥有这张图片。
        */
        public bool ExistImg(string imgID, string userID)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_existOneImgCmd.Parameters["@userID"].Value = userID;
                m_existOneImgCmd.Parameters["@imgID"].Value = imgID;

                int imgCount = (int)m_existOneImgCmd.ExecuteScalar();

                if (imgCount == 1)
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
        
    }
}