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

            m_getNameCmd = new SqlCommand("SELECT NAME FROM TagTable WHERE ID = @id", m_connection);
            m_getNameCmd.Parameters.Add("@id", SqlDbType.Char);

            m_getIDCmd = new SqlCommand("SELECT ID FROM TagTable WHERE NAME = @name", m_connection);;
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
            \brief 向数据库中添加一个新的Tag
            \param tagName 标签名
            如果已经存在同名Tag就直接返回false。
            系统自动生成标签ID。
        */
        public bool AddTag(string tagName)
        {
            bool success = false;

            if (AlreadyHave(tagName))
            {
                // 已经包含同名标签。
                return false;
            }

            try
            {
                m_connection.Open();
                m_addCmd.Parameters["@id"].Value = GenerateTagID();
                m_addCmd.Parameters["@name"].Value = tagName;

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

        /*
            \brief 后去某个tagID对应的标签名。
            \param tagID 标签ID 
        */
        public string GetName(string tagID)
        {
            string name = null;

            try
            {
                m_connection.Open();
                m_getNameCmd.Parameters["@id"].Value = tagID;

                object shouldBeTheName = m_getNameCmd.ExecuteNonQuery();

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
            \brief 获取某个标签名的ID
            \param tagName 标签的名字。 
        */
        public String GetTagId(string tagName)
        {
            string id = null;

            try
            {
                m_connection.Open();
                m_getIDCmd.Parameters["@name"].Value = tagName;

                object shouldBeTheID = m_getIDCmd.ExecuteNonQuery();

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
        public bool AlreadyHave(string tagName)
        {
            // 尝试从标签名获取标签ID
            if (GetTagId(tagName) == null)
            {
                // 标签为null，说明没有同名标签。
                return false;
            }
            else
            {
                // 标签不为null， 说明已存在同名标签。
                return true;
            }
        }
    }
}