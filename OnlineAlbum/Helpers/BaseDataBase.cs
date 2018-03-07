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
            \brief 当前网站用到的数据库连接字符串，要改数据库的话，直接改这个字符串就可以了。
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
            \brief 创建SqlCommand，虚函数，由子类具体实现
        */
        abstract protected void BuildSqlCmds();
    }
}