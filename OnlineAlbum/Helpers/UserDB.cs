using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    /*
        \brief 管理用户的注册以及同名用户检查
     */
    public class UserDB: BaseDataBase
    {
        private SqlCommand m_searchCmd;
        private SqlCommand m_insertCmd;
        private SqlCommand m_checkUserAndPasswordCmd;
        private SqlCommand m_getNickNameCmd;

        /*!
            \brief 创建SqlCommand
        */
        protected override void BuildSqlCmds()
        {
            m_searchCmd = new SqlCommand("SELECT count(*) FROM UserTable WHERE USER_ID = @userID", m_connection);
            m_searchCmd.Parameters.Add("@userID", SqlDbType.Char);

            m_insertCmd = new SqlCommand("INSERT INTO UserTable VALUES (@userID, @userName, @password)", m_connection);
            m_insertCmd.Parameters.Add("@userID", SqlDbType.Char);
            m_insertCmd.Parameters.Add("@userName", SqlDbType.NChar);
            m_insertCmd.Parameters.Add("@password", SqlDbType.Char);

            m_checkUserAndPasswordCmd = new SqlCommand("SELECT count(*) FROM UserTable WHERE USER_ID = @userID AND PASSWORD = @password", m_connection);
            m_checkUserAndPasswordCmd.Parameters.Add("@userID", SqlDbType.Char);
            m_checkUserAndPasswordCmd.Parameters.Add("@password", SqlDbType.Char);

            m_getNickNameCmd = new SqlCommand("SELECT USER_NAME FROM UserTable WHERE USER_ID = @userID", m_connection);
            m_getNickNameCmd.Parameters.Add("@userID", SqlDbType.Char);
        }

        /*!
            \brief 注册新用户，注册成功返回true，失败返回false。
            \param userID 用户的ID，由用户注册时提供
            \param nickName 用户昵称，用户可以之后修改
            \param password 密码
        */
        public bool Register(string userID, string nickName, string password)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_insertCmd.Parameters["@userID"].Value = userID;
                m_insertCmd.Parameters["@userName"].Value = nickName;
                m_insertCmd.Parameters["@password"].Value = password;

                int count = m_insertCmd.ExecuteNonQuery();

                if (count == 1)
                {
                    success = true;
                }
            }
            catch(SqlException ex)
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
            \brief 检查数据库中是否已经含有同名用户
            \param userID 用户ID
        */
        public bool AlreadyHave(string userID)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_searchCmd.Parameters["@userID"].Value = userID;

                int userCount = (int)m_searchCmd.ExecuteScalar();

                if (userCount == 1)
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
            \brief 检查用户名是否和密码匹配
            \param userID 用户的ID，由用户注册时提供
            \param password 密码
        */
        public bool CheckUserPassword(string userID, string password)
        {
            bool success = false;

            try
            {
                m_connection.Open();
                m_checkUserAndPasswordCmd.Parameters["@userID"].Value = userID;
                m_checkUserAndPasswordCmd.Parameters["@password"].Value = password;

                int count = (int)m_checkUserAndPasswordCmd.ExecuteScalar();

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
            \brief 获取某个用户的昵称
            \param userID 用户的唯一标识ID。
            如果用户不存在将会返回null。
        */
        public string GetNickNameOf(string userID)
        {
            string nickName = null;

            try
            {
                m_connection.Open();
                m_getNickNameCmd.Parameters["@userID"].Value = userID;

                SqlDataReader searchResult = m_getNickNameCmd.ExecuteReader();

                if (searchResult.Read())
                {
                    nickName = searchResult["USER_NAME"].ToString();
                }
                
            }
            catch (SqlException ex)
            {
                // 什么也不做，nickName为null
            }
            finally
            {
                m_connection.Close();
            }

            return nickName;
        }
    }
}