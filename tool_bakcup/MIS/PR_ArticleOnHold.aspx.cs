using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PR_ArticleOnHold : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        datasourceIBSQL ibSql = new datasourceIBSQL();
        DataSet dsProductionReport = new DataSet();
        try
        {
            dsProductionReport = ibSql.GetProductionReportArticleOnhold(0, "", "", "");
            grdProductionReport.DataSource = dsProductionReport;
            grdProductionReport.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            datasourceIBSQL ibSql = new datasourceIBSQL();
            string strDownloadPath = ibSql.GetDownloadPath(grdProductionReport, "Article On Hold");
            lblError.Text = "File download in to " + strDownloadPath.ToString();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error in file downloading";
        }
        //Response.Clear();
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ArticleReceived_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
        //Response.ContentType = "application/ms-excel";
        //Response.Write(strVal.ToString());
        //Response.End();

    }
}