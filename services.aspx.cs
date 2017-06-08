using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coating;

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
            FileInfo uploadedFileInfo = new FileInfo(uploadedFile2.FileName);
            uploadedFile2.SaveAs(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name);
            FileInfo uploadedFile2Info = new FileInfo(uploadedFile1.FileName);
            uploadedFile1.SaveAs(path + "\\" + i.ToString() + "\\" + uploadedFile2Info.Name);
            IL3DCDLL temp = new IL3DCDLL();
            temp.TryTransform(path + "\\" + i.ToString() + "\\" + uploadedFile2Info.Name);

            try
            {
                System.String filename = path + "\\" + i.ToString() + "\\" + uploadedFile2Info.Name;
                filename = filename.Substring(0, filename.Length - 4) + "_Result.obj";
                
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition",
                                   "attachment; filename=" + Path.GetFileName(filename) + ";");
                response.TransmitFile(filename);
                response.Flush();
                response.End();
                //System.String filename = path + "\\" + i.ToString() + "\\" + uploadedFile2Info.Name;
                //filename = filename.Substring(0, filename.Length - 4) + "_Result.obj";

                //// set the http content type to "APPLICATION/OCTET-STREAM
                //Response.ContentType = "APPLICATION/OCTET-STREAM";

                //// initialize the http content-disposition header to
                //// indicate a file attachment with the default filename
                //// "myFile.txt"
                //System.String disHeader = "Attachment; Filename=\"" + filename +
                //   "\"";
                //Response.AppendHeader("Content-Disposition", disHeader);

                //// transfer the file byte-by-byte to the response object
                //System.IO.FileInfo fileToDownload = new
                //   System.IO.FileInfo(filename);
                //Response.Flush();
                //Response.WriteFile(fileToDownload.FullName);
            }
            catch
            // file IO errors
            {
                
            }
        }
    }
}