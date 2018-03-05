using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using OnlineAlbum.Helpers;

namespace OnlineAlbum
{
    public partial class Register : System.Web.UI.Page
    {
        const string USER_VALIDATE_RX_STRING = @"^[a-zA-Z0-9]{6,10}$";
        static Regex userValidateRx = new Regex(USER_VALIDATE_RX_STRING);
        static UserDB m_userDB = new UserDB();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*!
            \brief 检查用户名是否规范
         */
        protected void userNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string userID = userNameText.Text;

            // 用户名格式检查
            if ( ! userValidateRx.IsMatch(userID))
            {
                args.IsValid = false;
                userNameValidator.ErrorMessage = "用户名格式错误";
                return;
            }

            // 查找是否存在同名用户
            if (m_userDB.AlreadyHave(userID))
            {
                args.IsValid = false;
                userNameValidator.ErrorMessage = "用户名已存在";
                return;
            }

            // 到这里就意味着没有任何问题
            args.IsValid = true;
            userNameValidator.ErrorMessage = "";
        }

        protected void RegisterConfirmBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (m_userDB.Register(userNameText.Text, userNameText.Text, passwordText.Text))
                {
                    Response.Write("注册成功<br>");
                }
                else
                {
                    Response.Write("注册失败<br>");
                }
            }
        }
    }
}