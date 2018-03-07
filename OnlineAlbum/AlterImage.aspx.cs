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
        protected static ImageTagDB m_imgTagDB = new ImageTagDB();
        protected static TagDB m_tagDB = new TagDB();

        protected void Page_Load(object sender, EventArgs e)
        {
            m_serverImg = (ServerImage)Session["serverImg"];

            if ( ! Page.IsPostBack)
            {

                imgNameLbl.Text = m_serverImg.m_imgName;
                userName.Text = Session["userName"].ToString();

                imgPreview.ImageUrl = ImagePath.IMAGE_STORAGE_PATH + m_serverImg.m_imgID;

                UpdateImgTags();
            
                renameText.Text = m_serverImg.m_imgName;
            }
        }

        /*!
            \brief 从数据库中更新图片的标签。 
        */
        private void UpdateImgTags()
        {
            tagList.Items.Clear();

            var newTagList = m_imgTagDB.GetTagsOf(m_serverImg.m_imgID, m_serverImg.m_userID);

            foreach (var tag in newTagList)
            {
                tagList.Items.Add(new ListItem(tag.m_tagName, tag.m_tagName));
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
            //if ( ! replaceImgUpload.HasFile)
            //{
            //    return;
            //}
            
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

            // 检查图片数据库信息是否更新成功
            if (
                m_imgDB.Delete(m_serverImg.m_imgID, m_serverImg.m_userID)       // 删除老信息
                && m_imgDB.AddTo(newImgID, m_serverImg.m_userID, oldImgName))   // 添加新信息
            {
                // 图片数据库更新成功。

                // 将原有的标签信息迁移到新图片上。
                var oldTagList = m_imgTagDB.GetTagsOf(m_serverImg.m_imgID, m_serverImg.m_userID);
                foreach (var tag in oldTagList)
                {
                    m_imgTagDB.TagImg(newImgID, tag.m_tagID, m_serverImg.m_userID);
                }
                
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
            // 删除这张图片的所有标签
            m_imgTagDB.RemoveAllTag(m_serverImg.m_imgID, m_serverImg.m_userID);
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

        protected void addTagBtn_Click(object sender, EventArgs e)
        {
            string tagName = addTagText.Text;
            string tagID = null;

            if (m_imgTagDB.AlreadyHasBeenTagged(m_serverImg.m_imgID, tagName, m_serverImg.m_userID))
            {
                return;
            }

            tagID = m_tagDB.GetTagID(tagName);
            if (tagID == null)
            {
                tagID = m_tagDB.TryAddTag(tagName);

                // 再次检查一遍新添加之后的ID
                if (tagID == null)
                {
                    throw new Exception("添加新标签" + tagName + "失败");
                }
            }

            if (m_imgTagDB.TagImg(m_serverImg.m_imgID, tagID, m_serverImg.m_userID))
            {
                // 添加成功
                UpdateImgTags();
                return;
            }
            else
            {
                throw new Exception("向图像添加新标签失败, tagName:" + tagName);
            }
        }

        protected void deleteTagBtn_Click(object sender, EventArgs e)
        {
            foreach (int selectedTagIndex in tagList.GetSelectedIndices())
            {
                string tagName = tagList.Items[selectedTagIndex].Text;
                string tagID = m_tagDB.GetTagID(tagName);

                if ( ! m_imgTagDB.RemoveTag(m_serverImg.m_imgID, tagID, m_serverImg.m_userID))
                {
                    throw new Exception("标签删除失败");
                }
            }

            UpdateImgTags();
        }
    }
}