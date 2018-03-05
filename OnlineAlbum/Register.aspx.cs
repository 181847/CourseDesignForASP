using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace OnlineAlbum
{
    public partial class Register : System.Web.UI.Page
    {
        const string USER_VALIDATE_RX_STRING = @"^[a-zA-Z0-9]{7, 10}$";
        static Regex userValidateRx = new Regex(USER_VALIDATE_RX_STRING);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*!
            \brief 检查用户名是否规范
         */
        protected void userNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string userName = userNameText.Text;

            // 用户名格式检查
            if ( ! userValidateRx.IsMatch(userName))
            {
                args.IsValid = false;
                userNameValidator.ErrorMessage = "用户名格式错误";
            }
            
            // 查找是否存在同名用户
            
        }
    }
}