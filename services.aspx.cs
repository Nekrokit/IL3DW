using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Threading;
using Coating;

public class Work
{
    private int ID;
    private string source;
    public Work(string _source, int _ID)
    {
        source = _source;
        ID = _ID;
    }
    public void DoWork()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString;
        try
        {
            DBCon.SetStatus(connectionString, System.Configuration.ConfigurationManager.AppSettings["WorkBegin"].ToString(),ID);
            IL3DCDLL temp = new IL3DCDLL();
            temp.TryTransform(source);
            DBCon.SetStatus(connectionString, System.Configuration.ConfigurationManager.AppSettings["Done"].ToString(), ID);
        }
        catch
        {
            DBCon.SetStatus(connectionString, System.Configuration.ConfigurationManager.AppSettings["Error"].ToString(), ID);
        }
        Thread.CurrentThread.Abort();
    }
}
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
                HttpFileCollection allFiles = Request.Files;
                HttpPostedFile uploadedFile = allFiles[0];
                FileInfo uploadedFileInfo = new FileInfo(uploadedFile.FileName);
                uploadedFile.SaveAs(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name);
                int ID = DBCon.Insert(System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString, uploadedFileInfo.Name, i.ToString());
                Work tmp = new Work(path + "\\" + i.ToString() + "\\" + uploadedFileInfo.Name, ID);
                Thread ModelTransform = new Thread(new ThreadStart(tmp.DoWork));
                ModelTransform.Start();
            }
            catch(Exception ex) { }
            Response.Redirect("History.aspx");
        }
    }
}