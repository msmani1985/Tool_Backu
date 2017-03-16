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
using System.IO;

public partial class PerformanceReport : System.Web.UI.Page
{
    ReportDocument rpt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            btn_Excelexport.Enabled = false;
        if (Page.IsPostBack)
        {
            jobperformanceds pds = new jobperformanceds();
            Datasource_IBSQL ibobj = new Datasource_IBSQL();
            try
            {
                string[,] param = { { "@jobsdate", Convert.ToDateTime(dd_monthlist.SelectedValue.ToString() + "/01/" + dd_yearlist.SelectedValue.ToString()).ToShortDateString() }, 
                                    { "@jobedate", Convert.ToDateTime(dd_monthlist.SelectedValue.ToString() + "/" + DateTime.DaysInMonth(Convert.ToInt32(dd_yearlist.SelectedValue), Convert.ToInt32(dd_monthlist.SelectedValue)) + "/" + dd_yearlist.SelectedValue.ToString()).ToShortDateString() } };
                pds = ibobj.jobperformance("SP_GET_JOBREPORTS", param, CommandType.StoredProcedure);
                rpt = new ReportDocument();
                rpt.Load(Server.MapPath("") + @"/CrystalReports/jobperformancerpt.rpt");
                rpt.SetDataSource(pds.Tables[1]);
                CV_performancerpt.ReportSource = rpt;
                btn_Excelexport.Enabled = true;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { pds = null; ibobj = null; }

        }
        
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //ConnectionInfo cinfo = new ConnectionInfo();
        //TableLogOnInfo tinfo = new TableLogOnInfo();
        //try
        //{
        //rpt = new ReportDocument();
        //rpt.Load(Server.MapPath("") + @"/CrystalReports/jobperformancereport.rpt");
        //rpt.SetParameterValue("JOBSDATE", Convert.ToDateTime(dd_monthlist.SelectedValue.ToString() + "/01/" + dd_yearlist.SelectedValue.ToString()));
        //rpt.SetParameterValue("JOBEDATE", Convert.ToDateTime(dd_monthlist.SelectedValue.ToString() + "/" + DateTime.DaysInMonth(Convert.ToInt32(dd_yearlist.SelectedValue), Convert.ToInt32(dd_monthlist.SelectedValue)) + "/" + dd_yearlist.SelectedValue.ToString()));
        //rpt.SetDatabaseLogon("sysdba", "masterkey");
        //CV_performancerpt.ReportSource = rpt;
        //    btn_Excelexport.Enabled = true;
        //}
        //catch (Exception ex)
        //{ throw ex; }
        //finally
        //{ rpt = null; }


        //Using Schema
        jobperformanceds pds = new jobperformanceds();
        Datasource_IBSQL ibobj = new Datasource_IBSQL();
        try
        {
            string[,] param = { { "@jobsdate", Convert.ToDateTime(dd_monthlist.SelectedValue.ToString() + "/01/" + dd_yearlist.SelectedValue.ToString()).ToShortDateString() }, 
                                    { "@jobedate", Convert.ToDateTime(dd_monthlist.SelectedValue.ToString() + "/" + DateTime.DaysInMonth(Convert.ToInt32(dd_yearlist.SelectedValue), Convert.ToInt32(dd_monthlist.SelectedValue)) + "/" + dd_yearlist.SelectedValue.ToString()).ToShortDateString() } };
            pds = ibobj.jobperformance("SP_GET_JOBREPORTS", param, CommandType.StoredProcedure);
            rpt = new ReportDocument();
            rpt.Load(Server.MapPath("") + @"/CrystalReports/jobperformancerpt.rpt");
            rpt.SetDataSource(pds.Tables[1]);
            CV_performancerpt.ReportSource = rpt;
            btn_Excelexport.Enabled = true;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { pds = null; ibobj = null; }


    }
    protected void btn_Excelexport_Click(object sender, EventArgs e)
    {
        ExportOptions eop=new ExportOptions();
        ExcelFormatOptions excelfo = ExportOptions.CreateExcelFormatOptions();
        DiskFileDestinationOptions dop = new DiskFileDestinationOptions();
        excelfo.ExcelConstantColumnWidth = 1200;
        excelfo.ExcelTabHasColumnHeadings = true;
        excelfo.ExcelUseConstantColumnWidth = true;
        eop.ExportFormatType = ExportFormatType.Excel;
        eop.ExportFormatOptions = excelfo;
        eop.ExportDestinationType = ExportDestinationType.NoDestination;
        dop.DiskFileName = "Performance_Report.xls";
        eop.ExportDestinationOptions = dop;
        rpt.ExportToHttpResponse(eop, Response, true, "Performance_Report");
    }
    protected void Page_unLoad(object sender, EventArgs e)
    {
        rpt = null;
    }
}
