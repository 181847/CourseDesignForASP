using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineAlbum.Helpers
{
    /*!
        \brief 简单地存储从数据库中获取的标签ID和标签名。
    */
    public class ServerTag
    {
        public string m_tagID;
        public string m_tagName;

        public ServerTag(string id, string name)
        {
            m_tagID = id;
            m_tagName = name;
        }
    }
}