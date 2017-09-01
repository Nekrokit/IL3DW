using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Coating;

public partial class History : System.Web.UI.Page
{
    SqlDataAdapter da;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindData();
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].HeaderStyle.ForeColor = Color.White;
                grid.Columns[i].HeaderStyle.BorderColor = Color.Black;
            }
            foreach (DataGridItem tmp in grid.Items)
            {
                try
                {
                    List<Pair> statuses = new List<Pair>();
                    foreach(var temp in System.Configuration.ConfigurationManager.AppSettings)
                    {
                        Pair newPair = new Pair();
                        newPair.First = temp.ToString();
                        newPair.Second = System.Configuration.ConfigurationManager.AppSettings[newPair.First.ToString()].ToString();
                        statuses.Add(newPair);
                    }
                    foreach(Pair temp in statuses)
                    {
                        if(tmp.Cells[1].Text == temp.Second.ToString().Split(';')[0])
                        {
                            tmp.Cells[1].Text = temp.First.ToString();
                        }
                    }
                    switch (tmp.Cells[1].Text)
                    {
                        case "FileGet": tmp.BackColor = Color.White; tmp.Cells[1].Text = GetValueFromKey("FileGet", 1); break;
                        case "WorkBegin": tmp.BackColor = Color.Gold; tmp.Cells[1].Text = GetValueFromKey("WorkBegin", 1); break;
                        case "Done": tmp.BackColor = Color.YellowGreen; tmp.Cells[1].Text = GetValueFromKey("Done", 1); break;
                        case "Error": tmp.BackColor = Color.Red; tmp.Cells[1].Text = GetValueFromKey("Error", 1); break;
                        default:
                            tmp.BackColor = Color.White; break;
                    }
                    if (tmp.Cells[1].Text != GetValueFromKey("done", 1))
                    {
                        tmp.Cells[0].CssClass = "hyperlinkH";
                        tmp.Cells[0].Enabled = false;
                    }
                }
                catch { }
            }
        }
    }
    private string GetValueFromKey(string Key, int Number)
    {
        return System.Configuration.ConfigurationManager.AppSettings[Key].ToString().Split(';')[Number];
    }
    private void BindData()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString;
        SqlConnection db = new SqlConnection(connectionString);
        db.Open();
        SqlCommand select = new SqlCommand("SELECT ID, FileName, Status FROM MainTable ORDER BY DataAdd, Status DESC", db);
        da = new SqlDataAdapter(select);
        da.Fill(ds);
        select.ExecuteNonQuery();
        grid.DataSource = ds;
        grid.DataBind();
        db.Close();
    }
}