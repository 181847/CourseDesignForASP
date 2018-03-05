using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    /*!
        \brief 所有数据库连接类的基类
    */
    public abstract class BaseDataBase
    {
        /*!
            \brief 这个应用中用到的连接字符串时同一个
        */
        private const string m_connectString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBForASPCourseDesign;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        protected SqlConnection m_connection;
        
        /*!
            \brief 初始化
        */
        public BaseDataBase()
        {
            m_connection = new SqlConnection(m_connectString);

            BuildSqlCmds();
        }

        /*!
            \brief 创建SqlCommand
        */
        abstract protected void BuildSqlCmds();
    }
}