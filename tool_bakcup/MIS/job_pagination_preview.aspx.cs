using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class job_pagination_preview : System.Web.UI.Page
{
    ReportDocument oRpt = new ReportDocument();
    Pagination oPgntn = new Pagination();
    private static DataSet dsPage = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.loadReport();
    }
    private void loadReport(){
        if (Request.QueryString["jno"] != null && Request.QueryString["jno"].ToString().Trim() != ""){
            string sJobno = Request.QueryString["jno"].ToString().Trim();
            dsPage = oPgntn.getJobPagination(sJobno);
            oRpt.FileName = Server.MapPath("~\\ProductivityReport\\rptPagination.rpt");
            dsPage.Tables[0].TableName = "dtParentJob";
            dsPage.Tables[1].TableName = "dtPagination";
            oRpt.SetDataSource(dsPage);
            //oRpt.PrintOptions.PaperSize = PaperSize.PaperA4;
            oRpt.PrintOptions.PrinterName = "";
            CrystalReportViewer1.ReportSource = oRpt;
            CrystalReportViewer1.DataBind();            
        }
    }
}
