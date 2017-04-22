using Moy.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Moy.BookShop.Model.Enum;

namespace Moy.BookShop.UI.test
{
    public partial class ConvertVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var video = new Video();
            video.Status = (short)VideoStatus.上传中;
            video.Title = txtTitle.Text;
            video.FileExt = System.IO.Path.GetExtension(FileUpload.FileName);

            //TODO 插入一条记录.并返回记录行数
            //bll.Add(model)

            string url = "http://localhost:6242/UploadVideo.ashx?Id=" + video.Id + "&fileExt=" + video.FileExt;
            System.Net.WebClient wc = new System.Net.WebClient();
            wc.UploadData(url, FileUpload.FileBytes);
            video.Status = (short)VideoStatus.上传成功;
            //更新回数据库..
        }


    }
}