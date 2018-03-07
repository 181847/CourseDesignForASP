using OnlineAlbum.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum
{
    public partial class AlterImage : System.Web.UI.Page
    {
        protected ServerImage m_serverImg;
        protected string m_extensionNameWithDot;
        protected static ImageDB m_imgDB = new ImageDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            m_serverImg = (ServerImage)Session["serverImg"];

            imgNameLbl.Text = m_serverImg.m_imgName;
            userName.Text = Session["userName"].ToString();

            imgPreview.ImageUrl = m_serverImg.ToImageUrl();
            imgReplace.Visible = false;
            
            if ( ! Page.IsPostBack)
            {
                renameText.Text = m_serverImg.m_imgName;
            }
        }

        protected void renameConfirmBtn_Click(object sender, EventArgs e)
        {
            string renameString = renameText.Text;

            if (
                renameString == m_serverImg.m_imgName
                || m_imgDB.Rename(m_serverImg.m_imgID, m_serverImg.m_userID, renameString))
            {
                Response.Redirect("~\\PersonalPage.aspx");
                Response.Write("重命名成功");
            }
            else
            {
                Response.Write("重命名失败");
            }
        }

        protected void replaceConfirmBtn_Click(object sender, EventArgs e)
        {
            if ( ! replaceImgUpload.HasFile)
            {
                replaceImgLackLbl.Visible = true;
            }

            string oldImgName = renameText.Text;
            // 如果用户没有填写重命名的名字
            if (oldImgName == "")
            {
                // 则使用图片原来的名字。
                oldImgName = m_serverImg.m_imgName;
            }
            string newImgID = m_imgDB.GenerateImgID();

            // 在图片ID后面添加扩展名
            newImgID += ImagePath.GetExtensionNameWithDot(replaceImgUpload.FileName);

            if (
                m_imgDB.Delete(m_serverImg.m_imgID, m_serverImg.m_userID)
                && m_imgDB.AddTo(newImgID, m_serverImg.m_userID, oldImgName))
            {
                // 删除之前的文件
                File.Delete(Request.PhysicalApplicationPath + ImagePath.IMAGE_STORAGE_PATH + m_serverImg.m_imgID);
                // 保存新文件。
                replaceImgUpload.SaveAs(Request.PhysicalApplicationPath + ImagePath.IMAGE_STORAGE_PATH + newImgID);
                Response.Redirect("~\\PersonalPage.aspx");
            }
            else
            {
                throw new Exception("图片上传失败");
            }
        }

        protected void deleteConfirmBtn_Click(object sender, EventArgs e)
        {
            // 删除数据库记录
            m_imgDB.Delete(m_serverImg.m_imgID, m_serverImg.m_userID);
            // 删除文件
            File.Delete(Request.PhysicalApplicationPath + ImagePath.IMAGE_STORAGE_PATH + m_serverImg.m_imgID);
            Response.Redirect("~\\Personalpage.aspx");
        }

        protected void returnToPersonalPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\Personalpage.aspx");
        }
        
        protected void returnToMainPageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~\\GlobalAlbum.aspx");
        }
    }
}