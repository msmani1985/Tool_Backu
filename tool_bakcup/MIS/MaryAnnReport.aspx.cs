using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MaryAnnReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridData();

        }
    }
    protected void AutoRefreshGrid(object sender, EventArgs e)
    {
        BindGridData();
       // Label1.Text = DateTime.Now.ToString();
    }
    public void BindGridData()
    {
        SqlConnection conn = new SqlConnection("server=192.9.201.222;database=MaryAnn;uid=sa;pwd=masterkey");
        conn.Open();
        SqlCommand command = new SqlCommand("Select * from file_list where date_processed  is not null and date_received > '2015-08-01 00:00:00.000' order by date_received", conn);
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(ds);
        gridview.DataSource = ds.Tables[0];
        gridview.DataBind();
    }
    protected void exportExcel_selectedcolumns_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=WIPtodayReport.xls");
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter strwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
            HtmlForm htmlfrm = new HtmlForm();
            gridview.Parent.Controls.Add(htmlfrm);
            htmlfrm.Attributes["runat"] = "server";
            htmlfrm.Controls.Add(gridview);
            htmlfrm.RenderControl(txtwriter);
            Response.Write(strwriter.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            // Response.Write(ex.Message);
        }
    }
}