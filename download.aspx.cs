using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string id = Request.Params[0];
            int ID = Convert.ToInt32(id);
            MemoryStream name = new MemoryStream();
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Response.ContentType = "text/plain";
            string path = Server.MapPath("Clients");

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString;
            SqlConnection db = new SqlConnection(connectionString);
            db.Open();
            SqlCommand select = new SqlCommand(string.Format("SELECT FileFolder + '/' + FileName FROM MainTable WHERE ID = {0}",ID), db);
            object fileSource = select.ExecuteScalar();
            string fileName = path + "//" + fileSource;
            fileName = fileName.Substring(0, fileName.Length - 4) + "_Result.obj";
            db.Close();


            Response.AddHeader("Content-Disposition",
                                       "attachment; filename=" + Path.GetFileName(fileName) + ";");
            Response.TransmitFile(fileName);
            Page.Response.Flush();
            Response.SuppressContent = true;
            Context.ApplicationInstance.CompleteRequest();
            name.Dispose();
        }
        catch { }
    }
}