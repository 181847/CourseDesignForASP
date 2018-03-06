using OnlineAlbum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected ImageDB m_imgDB = new ImageDB();
        protected string m_sUserID;
        protected string m_sUserName;

        private const string GET_IMG_EXTEND_NAME_PATTERN = @"^.*(\.[a-zA-Z]{2,4})$";

        private const string SAVE_DIR = "\\Images\\UserData\\";
        private static string APP_PATH;
        private static string SAVE_PATH;

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateUser();
            // 更新上传路径。
            BuildSavePath();
        }

        /*!
            \brief 从Session信息中更新当前用户名
        */
        protected void UpdateUser()
        {
            m_sUserID = Session["userID"].ToString();
            m_sUserName = Session["userName"].ToString();
            showUserNameLbl.Text = "用户：" + m_sUserID + "/" + m_sUserName + "正在上传新图像";
        }

        /*!
            \brief 更新上传文件路径 
        */
        protected void BuildSavePath()
        {
            APP_PATH = Request.PhysicalApplicationPath;
            SAVE_PATH = APP_PATH + SAVE_DIR;
        }

        /*!
            \brief 确认上传图像，检查是否上传了文件。
        */
        protected void confirmUpload_Click(object sender, EventArgs e)
        {
            if ( ! imgFileUploadBtn.HasFile)
            {
                WarningNoImageUploaded();
                return;
            }

            string oldImgName = renameImgTextBox.Text;
            // 如果用户没有填写重命名的
            if (oldImgName == "")
            {
                // 则使用图片原来的名字。
                oldImgName = imgFileUploadBtn.FileName;
            }
            string newImgID   = m_imgDB.GenerateImgID();

            // 获取图片的扩展名。
            Match matchResult = Regex.Match(imgFileUploadBtn.FileName, GET_IMG_EXTEND_NAME_PATTERN);
            string extendNameWithDot = matchResult.Groups[1].Value;

            // 在图片ID后面添加扩展名
            newImgID += extendNameWithDot;

            if (m_imgDB.AddTo(newImgID, m_sUserID, oldImgName))
            {
                UploadImgSuccess(newImgID);
            }
            else
            {
                throw new Exception("图片上传失败");
            }
        }

        /*!
            \brief 当图像信息已经更新到数据库中的时候，调用此方法来实际地存储图像文件到本地服务器上。
            \param newImgID 用以在服务器端保存用户图片的图片ID，形如"fdk3430-343-243004334-342.jpg"，扩展名不限。
        */
        private void UploadImgSuccess(string newImgID)
        {
            imgFileUploadBtn.SaveAs(SAVE_PATH + newImgID);
            // 上传完成，返回个人主页面
            Response.Redirect("~\\PersonalPage.aspx");
        }

        /*!
            \brief 当确认上传按钮按下，但是没有实际的图像文件被提交的时候触发此方法，并且拒绝上传文件。 
        */
        private void WarningNoImageUploaded()
        {
            Response.Write("没有上传任何图像");
        }

        protected void imgFileUploadBtn_Unload(object sender, EventArgs e)
        {
            renameImgTextBox.Text = imgFileUploadBtn.FileName;
        }
    }
}