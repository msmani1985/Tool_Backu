using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class WordCount_Report : System.Web.UI.Page
{
    datasourceSQL dsSql = new datasourceSQL();
    private static DataTable dtable4 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = dsSql.WordCount(SDate.Text, EDate.Text);
            WordCount.DataSource = ds;
            WordCount.DataBind();
            dtable4 = ds.Tables[0].Copy();
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = dsSql.WordCount(SDate.Text, EDate.Text);
        WordCount.DataSource = ds;
        WordCount.DataBind();
        dtable4 = ds.Tables[0].Copy();
    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
         int i = 1;
         if (dtable4 != null && dtable4.Rows.Count > 0)
         {
             StringBuilder sbData = new StringBuilder();
             sbData.Append("<table border='1'>");
             if(SDate.Text!="" && EDate.Text!="")
                 sbData.Append("<tr valign='top'><td colspan='8' align='center'><h4>Word Count Report From "+DateTime.Parse(SDate.Text).ToString("dd MMM yyyy")+" To "+DateTime.Parse(EDate.Text).ToString("dd MMM yyyy")+" </h4></td></tr>");
             else
                 sbData.Append("<tr valign='top'><td colspan='8' align='center'><h4>Word Count Report</h4></td></tr>");
             sbData.Append("<tr valign='top'><td colspan='8' align='center'><b>Pre-editing</b></td></tr>");
             sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Journal</b></td><td bgcolor='silver'><b>Article</b></td><td bgcolor='silver'><b>MS Pages</b></td><td bgcolor='silver'><b>Typeset Pages</b></td><td bgcolor='silver'><b>Word Count without References</b></td><td bgcolor='silver'><b>Word Count with References</b></td><td bgcolor='silver'><b>Uploaded Date</b></td></tr>");
             foreach (DataRow r in dtable4.Rows)
             {
                 sbData.Append("<tr valign='top'>");
                 sbData.Append("<td>" + i + "</td>");
                 sbData.Append("<td>" + r["JOURCODE"] + "</td>");
                 sbData.Append("<td>" + r["AMANUSCRIPTID"] + "</td>");
                 sbData.Append("<td>" + r["ms_pages"] + "</td>");
                 sbData.Append("<td>" + r["AREALNOOFPAGES"] + "</td>");
                 sbData.Append("<td>" + r["WCWithRef"] + "</td>");
                 sbData.Append("<td>" + r["WCWithOutRef"] + "</td>");
                 sbData.Append("<td>" + r["UpdatedDate"] + "</td>");
                 sbData.Append("</tr>");
                 i = i + 1;
             }
             sbData.Append("</table>");
             Response.Clear();
             Response.ContentType = "application/vnd.ms-excel";
             Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Word_Count_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
             Response.ContentEncoding = Encoding.Unicode;
             Response.BinaryWrite(Encoding.Unicode.GetPreamble());
             Response.Write(sbData.ToString());
             Response.Flush();
             Response.Close();
         }
    }
}