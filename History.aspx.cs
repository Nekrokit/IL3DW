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
                        newPair.First = temp.ToString(); ;
                        newPair.Second = System.Configuration.ConfigurationManager.AppSettings[newPair.First.ToString()].ToString();
                        statuses.Add(newPair);
                    }
                    foreach(Pair temp in statuses)
                    {
                        if(tmp.Cells[1].Text == temp.Second.ToString())
                        {
                            tmp.Cells[1].Text = temp.First.ToString();
                        }
                    }
                    switch (tmp.Cells[1].Text)
                    {
                        case "FileGet": tmp.BackColor = Color.Brown; break;
                        case "WorkBegin": tmp.BackColor = Color.Gold; break;
                        case "Done": tmp.BackColor = Color.YellowGreen; break;
                        case "Error": tmp.BackColor = Color.Tomato; break;
                        default:
                            tmp.BackColor = Color.White; break;
                    }
                    if (tmp.Cells[1].Text != "Done")
                    {
                        tmp.Cells[0].Enabled = false;
                    }
                }
                catch { }
            }
        }
    }

    private void BindData()
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnecStr"].ConnectionString;
        SqlConnection db = new SqlConnection(connectionString);
        db.Open();
        SqlCommand select = new SqlCommand("SELECT ID, FileName, Status FROM MainTable ORDER BY Status DESC, FileName", db);
        da = new SqlDataAdapter(select);
        da.Fill(ds);
        select.ExecuteNonQuery();
        grid.DataSource = ds;
        grid.DataBind();
        db.Close();
    }
}