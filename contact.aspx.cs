using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.ToString()[4] == '1')
            {
                contactform.Visible = false;
                thanksDiv.Visible = true;
            }
        }
        catch { }
    }

    protected void SaveMessage(object sender, EventArgs e)
    {
        if (name.Value != "" && email.Value != "" && message.Value != "")
        {
            Encoding byt = Encoding.UTF8;
            string messages = "####Guest:" + '\n';
            messages += name.Value + '\n';
            messages += "####Email:" + '\n' + email.Value + '\n';
            messages += "####Entity:" + '\n' + company.Value + '\n';
            messages += "####Message:" + '\n' + message.Value + '\n';
            string way = MapPath("Messages");
            way += "\\guest" + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + ".txt";
            FileStream newMes = new FileStream(way, FileMode.CreateNew);
            newMes.Write(byt.GetBytes(messages), 0, messages.Length);
            newMes.Close();
            contactform.Visible = false;
            thanksDiv.Visible = true;
            Page.Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().Length - 6) + "?vis=1");
        }
    }
}