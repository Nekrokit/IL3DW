using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Threading;
using Coating;

public partial class services : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string path = Server.MapPath("Clients");
            double i;
            for (i = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds; ; i++)
            {
                if (!Directory.Exists(path + "\\" + i.ToString()))
                {
                    Directory.CreateDirectory(path + "\\" + i.ToString());
                    break;
                }
            }
            try
            {
                DBCon connection = DBCon.GetConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString);
                HttpFileCollection allFiles = Request.Files;
                HttpPostedFile uploadedFile = allFiles[0];
                FileInfo uploadedFileInfo = new FileInfo(uploadedFile.FileName);
                uploadedFile.SaveAs(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name);
                int ID = connection.Insert(uploadedFileInfo.Name, i.ToString());
                Work tmp = new Work(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name, ID, System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString, System.Configuration.ConfigurationManager.AppSettings["WorkBegin"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Done"].ToString(), System.Configuration.ConfigurationManager.AppSettings["Error"].ToString());
                Thread ModelTransform = new Thread(new ThreadStart(tmp.DoWork));
                ModelTransform.Start();
            }
            catch(Exception ex) { }
            Response.Redirect("History.aspx");
        }
    }
}