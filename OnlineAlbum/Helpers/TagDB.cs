using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    /*!
        \brief 标签数据库管理 
    */
    public class TagDB:BaseDataBase
    { 
        /*!
            \brief GUID字符串的格式化方法。
        */
        private const string GUID_STRING_FORMAT = "N";

        protected SqlCommand m_addCmd;
        protected SqlCommand m_getNameCmd;
        protected SqlCommand m_getIDCmd;

        /*!
            \brief 构建所有需要的SqlCommand 
        */
        protected override void BuildSqlCmds()
        {
            m_addCmd = new SqlCommand("INSERT INTO TagTable VALUES (@id, @name)", m_connection);
            m_addCmd.Parameters.Add("@id", SqlDbType.Char);
            m_addCmd.Parameters.Add("@name", SqlDbType.NChar);

            m_getNameCmd = new SqlCommand("SELECT TAG_NAME FROM TagTable WHERE TAG_ID = @id", m_connection);
            m_getNameCmd.Parameters.Add("@id", SqlDbType.Char);

            m_getIDCmd = new SqlCommand("SELECT TAG_ID FROM TagTable WHERE TAG_NAME = @name", m_connection);;
            m_getIDCmd.Parameters.Add("@name", SqlDbType.NChar);
        }

        /*!
            \brief 生成随机的TagID
        */
        public static string GenerateTagID()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid.ToString(GUID_STRING_FORMAT);
        }

        /*!
            \brief 尝试向数据库中添加一个新的Tag
            \param tagName 标签名
            如果失败或者存在同名Tag，返回null，如果成功返回新标签的ID。
        */
        public string TryAddTag(string tagName)
        {
            string itsTagID = GetTagID(tagName);
            if (itsTagID != null)
            {
                // 已经包含同名标签，添加失败。
                return null;
            }
            else
            {
                itsTagID = GenerateTagID();
            }

            try
            {
                m_connection.Open();
                m_addCmd.Parameters["@id"].Value = itsTagID;
                m_addCmd.Parameters["@name"].Value = tagName;

                int count = m_addCmd.ExecuteNonQuery();

                if (count != 1)
                {
                    // 如果插入失败 强制把返回值ID设为null。
                    itsTagID = null;
                }
            }
            catch (SqlException ex)
            {
                itsTagID = null;
            }
            finally
            {
                m_connection.Close();
            }

            return itsTagID;
        }

        /*
            \brief 获取某个tagID对应的标签名。
            \param tagID 标签ID 
        */
        public string GetTagName(string tagID)
        {
            string name = null;

            try
            {
                m_connection.Open();
                m_getNameCmd.Parameters["@id"].Value = tagID;

                object shouldBeTheName = m_getNameCmd.ExecuteScalar();

                if (shouldBeTheName != null)
                {
                    // 返回值不是null，说明找到了对应的名字。
                    name = shouldBeTheName.ToString();
                }
                
            }
            catch (SqlException ex)
            {
                // 什么也不做，name == null
            }
            finally
            {
                m_connection.Close();
            }

            // 如果失败，name == null
            // 如果成功，name == "..."(字符串)
            return name;
        }

        /*!
            \brief 获取某个标签名对应的ID
            \param tagName 标签的名字。 
        */
        public String GetTagID(string tagName)
        {
            string id = null;

            try
            {
                m_connection.Open();
                m_getIDCmd.Parameters["@name"].Value = tagName;

                object shouldBeTheID = m_getIDCmd.ExecuteScalar();

                if (shouldBeTheID != null)
                {
                    // 返回值不是null，说明找到了对应的ID。
                    id = shouldBeTheID.ToString();
                }

            }
            catch (SqlException ex)
            {
                // 什么也不做，id == null
            }
            finally
            {
                m_connection.Close();
            }

            // 如果失败，id == null
            // 如果成功，id == "..."(字符串)
            return id;
        }

        /*!
            \brief 检查数据库中是否已经存在同名标签。 
        */
        public bool AlreadyHaveName(string tagName)
        {
            // 尝试从标签名获取标签ID
            if (GetTagID(tagName) == null)
            {
                // 标签ID为null，说明没有同名标签。
                return false;
            }
            else
            {
                // 标签ID不为null， 说明已存在同名标签。
                return true;
            }
        }
        
        /*!
            \brief 检查数据库中是否已经存在这个标签ID的标签。 
        */
        public bool AlreadyHaveID(string tagID)
        {
            // 尝试从标签ID获取标签名
            if (GetTagName(tagID) == null)
            {
                // 标签名为null，说明没有这个ID。
                return false;
            }
            else
            {
                // 标签名不为null， 说明已存在这个ID。
                return true;
            }
        }

    }
}