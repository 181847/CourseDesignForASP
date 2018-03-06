using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    public class ImageDB: BaseDataBase
    {
        /*!
            \brief GUID字符串的格式化方法。
        */
        private const string GUID_STRING_FORMAT = "N";

        protected SqlCommand m_addCmd;
        protected SqlCommand m_renameCmd;
        protected SqlCommand m_deleteCmd;
        protected SqlCommand m_findUserImgsCmd;
        protected SqlCommand m_findAllImgsCmd;

        protected override void BuildSqlCmds()
        {
            m_addCmd = new SqlCommand("INSERT INTO ImageTable VALUES (@id, @userID, @name)", m_connection);
            m_addCmd.Parameters.Add("@id", SqlDbType.Char);
            m_addCmd.Parameters.Add("@userID", SqlDbType.Char);
            m_addCmd.Parameters.Add("@name", SqlDbType.NChar);


            m_renameCmd = new SqlCommand("UPDATE ImageTable SET NAME = @name WHERE ID = @id AND USER_ID = @userID", m_connection);
            m_renameCmd.Parameters.Add("@id", SqlDbType.Char);
            m_renameCmd.Parameters.Add("@userID", SqlDbType.Char);
            m_renameCmd.Parameters.Add("@name", SqlDbType.NChar);

            m_deleteCmd = new SqlCommand("DELETE FROM ImageTable WHERE ID = @id AND USER_ID = @userID", m_connection);
            m_deleteCmd.Parameters.Add("@id", SqlDbType.Char);
            m_deleteCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_findUserImgsCmd = new SqlCommand("SELECT * FROM ImageTable WHERE USER_ID = @userID", m_connection);
            m_findUserImgsCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_findAllImgsCmd = new SqlCommand("SELECT * FROM ImageTable", m_connection);
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
            \param imgID 随机GUID，每个图像的ID都不相同，而且一经记录就不再更改
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
                m_addCmd.Parameters["@id"].Value = imgID;
                m_addCmd.Parameters["@name"].Value = imgName;

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
            \param imgID 随机GUID，每个图像的ID都不相同，而且一经记录就不再更改
            \param userID 添加到这个用户的图库中
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
                m_renameCmd.Parameters["@id"].Value = imgID;
                m_renameCmd.Parameters["@name"].Value = newName;

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
                m_deleteCmd.Parameters["@id"].Value = imgID;

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
            \brief 获取某一个用户的所有图片信息。 
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
                        dr["ID"].ToString(), 
                        dr["NAME"].ToString(), 
                        dr["USER_ID"].ToString()));
                }
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
                        dr["ID"].ToString(),
                        dr["NAME"].ToString(),
                        dr["USER_ID"].ToString()));
                }
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
    }
}