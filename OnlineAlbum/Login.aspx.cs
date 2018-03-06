using OnlineAlbum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum
{
    public partial class Login : System.Web.UI.Page
    {
        static UserDB m_userDB = new UserDB();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void registerBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\Register.aspx");
        }

        protected void userIDValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string userID = args.Value;

            if ( ! m_userDB.AlreadyHave(userID))
            {
                // 用户名不存在的错误。
                userIDValidator.ErrorMessage = "用户名不存在";
                args.IsValid = false;
                // 隐藏密码错误信息
                passwordWarningLbl.Visible = false;
                return;
            }

            // 已知用户名存在，检查密码是否正确
            if ( ! m_userDB.CheckUserPassword(userID, passwordText.Text))
            {
                // 密码错误
                userIDValidator.ErrorMessage = "";
                args.IsValid = false;
                // 显示密码错误信息。
                passwordWarningLbl.Visible = true;
                return;
            }

            // 一切正常。
            userIDValidator.IsValid = true;
            passwordWarningLbl.Visible = false;
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["userID"] = userIDText.Text;
                Session["userName"] = m_userDB.GetNickNameOf(userIDText.Text);
                Response.Redirect("~\\PersonalPage.aspx");
            }
            else
            {
                Response.Write("登陆失败<br>");
            }
        }
    }
}