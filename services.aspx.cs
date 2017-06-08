using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class services : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(IsPostBack)
        {
            string path = Server.MapPath("Clients");
            int i;
            for (i = 1; ; i++)
            {
                if (!Directory.Exists(path + "\\" + i.ToString()))
                {
                    Directory.CreateDirectory(path + "\\" + i.ToString());
                    goto m;
                }
            }
        m: ;
            HttpFileCollection allFiles = Request.Files;
            HttpPostedFile uploadedFile1 = allFiles[0];
            HttpPostedFile uploadedFile2 = allFiles[1];
            FileInfo uploadedFileInfo = new FileInfo(uploadedFile1.FileName);
            uploadedFile1.SaveAs(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name);
            uploadedFileInfo = new FileInfo(uploadedFile2.FileName);
            uploadedFile2.SaveAs(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name);
        }
    }
}