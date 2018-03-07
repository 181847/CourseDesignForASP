using OnlineAlbum.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineAlbum.UserControl
{
    public partial class ImageSortPanel : System.Web.UI.UserControl
    {
        /*!
            \brief 将未分配任何标签的图像统一显示到名为这个字符串的面板下面。 
        */
        protected const string UNSORTED_TAG_NAME = "未分类";

        /*!
            \brief 下面两个List的每一个元素相互对应，即一个Image对应一个List<ServerTag>
        */
        protected List<ServerImage> m_imgList ;
        protected List<List<ServerTag>> m_tagsListForTheImgs = new List<List<ServerTag>>();

        protected Dictionary<string, List<ServerImage>> m_tagImgDict = new Dictionary<string, List<ServerImage>>();

        protected static ImageTagDB m_imgTagDb = new ImageTagDB();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /*!
            \brief 选择要在这个面板下显示的所有图片。
        */
        public void SetImgs(List<ServerImage> showImagesList)
        {
            m_imgList = showImagesList;
            // 更新所有图像的标签
            UpdateImgTags();

            // 强制选择显示所有标签
            SetTagSelections(new int[0], true);
            
            // 把所有的图像都按照标签顺序显示出来
            UpdateShowedImgs();
        }

        /*!
            \brief 将所有的图片按照选择的标签进行摆放。 
        */
        private void UpdateShowedImgs()
        {
            mainPanel.Controls.Clear();
            // 遍历每一个被选择的标签。
            foreach (var selectedItem in tagsShowList.GetSelectedIndices())
            {
                // 获取标签名
                string tagName = tagsShowList.Items[selectedItem].Text;
                // 获取这个标签下的所有图像
                List<ServerImage> imgsForThisTag = m_tagImgDict[tagName];

                ImagePanel newImagePanel = (ImagePanel)Page.LoadControl("~\\UserControl\\ImagePanel.ascx");
                newImagePanel.SetTagName(tagName);
                newImagePanel.SetImgs(imgsForThisTag);

                mainPanel.Controls.Add(newImagePanel);
            }
        }

        /*!
            \brief 更新所有图像对应的标签。 
        */
        private void UpdateImgTags()
        {
            m_tagsListForTheImgs.Clear();   // 清空本地标签列表
            m_tagImgDict.Clear();         // 清空本地的按照标签分类的图像

            // 确保没有标签标记的图像有一个默认的标签面板可以显示。
            m_tagImgDict.Add(UNSORTED_TAG_NAME, new List<ServerImage>());

            // 遍历每一张图像
            foreach (var serverImg in m_imgList)
            {
                List<ServerTag> tagListForThisImage = m_imgTagDb.GetTagsOf(serverImg.m_imgID, serverImg.m_userID);
                m_tagsListForTheImgs.Add(tagListForThisImage);

                // 遍历这个图像的每一个标签
                foreach (var tag in tagListForThisImage)
                {
                    // 检查标签列表是否存在
                    if ( ! m_tagImgDict.Keys.Contains(tag.m_tagName))
                    {
                        m_tagImgDict.Add(tag.m_tagName, new List<ServerImage>());
                    }

                    // 向标签下的图像列表中添加此图像
                    m_tagImgDict[tag.m_tagName].Add(serverImg);
                }

                // 检查是否为尚未被分配任何标签的图像
                if (tagListForThisImage.Count == 0)
                {
                    // 向标签下的图像列表中添加此图像
                    m_tagImgDict[UNSORTED_TAG_NAME].Add(serverImg);
                }
            }

            // 更新网页端显示的标签。
            UpdateWebTagList();
        }

        /*!
            \brief 将所有图像的标签更新到网页上的标签列表中。 
        */
        private void UpdateWebTagList()
        {
            tagsShowList.Items.Clear();
            foreach (string tagName in m_tagImgDict.Keys)
            {
                tagsShowList.Items.Add(new ListItem(tagName));
            }
        }

        /*!
            \brief 手动选择要显示标签
            \param selectIndices 要修改的列表序号数组
            \param isSelected 要设置的选择值
            如果selectIndices为空的话，就默认是对所有的标签进行选择。
        */
        public void SetTagSelections(int[] selectIndices, bool isSeleted = true)
        {
            if (selectIndices.Length == 0)
            {
                for (int i = tagsShowList.Items.Count - 1; i >= 0; --i)
                {
                    tagsShowList.Items[i].Selected = isSeleted;
                }
                return;
            }

            foreach (int index in selectIndices)
            {
                tagsShowList.Items[index].Selected = isSeleted;
            }
        }

        /*!
            \brief 每当标签选项改变的时候调用此方法更新显示的图像。 
        */
        protected void tagsShowList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateShowedImgs();
        }

        /*!
            \brief 选择显示所有的类型标签。 
        */
        protected void selectAllBtn_Click(object sender, EventArgs e)
        {
            SetTagSelections(new int[0], true);
        }
    }
}