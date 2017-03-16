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
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class TimeSheetSummary : System.Web.UI.Page
{
    OdbcConnection con = null;
    string connectionstring = "";
    string query = "";
    OdbcCommand cmd = new OdbcCommand();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            connectionstring = ConfigurationManager.ConnectionStrings["conStrIB"].ToString();
            con = new OdbcConnection(connectionstring);
            // Articles
            query = "SELECT l.LEDate, l.LEndDate, e.Emp_FName, CAST(a.AArticleCode AS CHAR(50)) as AArticleCode, l.SNo, s.SName, e.DNo, s.SNO_Timesheet FROM loggedevents_dp l";
            query = query + " LEFT OUTER JOIN article_dp a ON l.Ano = a.Ano";
            query = query + " INNER JOIN Employee_dp e ON (e.EmpNo = l.EmpNo OR l.EmpNo IS NULL)";
            query = query + " INNER JOIN Stage_dp s ON l.SNo = s.SNo";
            query = query + " WHERE l.IsTimeSheet = 'Y' AND LEndDate IS NOT NULL";
            query = query + " AND EmpNo in (SELECT EmpNo FROM Employee_dp WHERE DNO = " + Session["WSDNo"].ToString() + ") and status = 1 and (Emp_id is not NULL and Emp_id <> 0) ";
            query = query + " AND (LEDATE >= '" + Session["WSStartDate"].ToString() + " 00:00:00' or ledate is null)";
            query = query + " AND LENDDATE <= '" + Session["WSEndDate"].ToString() + " 23:59:59'";
            // Projects
            query = query + " UNION ";
            query = query + "SELECT l.LEDate, l.LEndDate, e.Emp_FName, a.MPCode AS AArticleCode, l.SNo, s.SName, e.DNo, s.SNO_Timesheet FROM PLogevents_dp l";
            query = query + " LEFT OUTER JOIN Project_Modules_dp a ON l.MProjno = a.MProjno";
            query = query + " INNER JOIN Employee_dp e ON (e.EmpNo = l.EmpNo OR l.EmpNo IS NULL)";
            query = query + " INNER JOIN Stage_dp s ON l.SNo = s.SNo";
            query = query + " WHERE LEndDate IS NOT NULL";
            query = query + " AND EmpNo in (SELECT EmpNo FROM Employee_dp WHERE DNO = " + Session["WSDNo"].ToString() + ") and status = 1 and (Emp_id is not NULL and Emp_id <> 0)";
            query = query + " AND (LEDATE >= '" + Session["WSStartDate"].ToString() + " 00:00:00' or ledate is null)";
            query = query + " AND LENDDATE <= '" + Session["WSEndDate"].ToString() + " 23:59:59'";
            // Books
            query = query + " UNION ";
            query = query + "SELECT l.LEDate, l.LEndDate, e.Emp_FName, CAST(a.BNumber AS CHAR(50)) AS AArticleCode, l.SNo, s.SName, e.DNo, s.SNO_Timesheet FROM BLogevents_dp l";
            query = query + " LEFT OUTER JOIN Book_dp a ON l.Bno = a.Bno";
            query = query + " INNER JOIN Employee_dp e ON (e.EmpNo = l.EmpNo OR l.EmpNo IS NULL)";
            query = query + " INNER JOIN Stage_dp s ON l.SNo = s.SNo";
            query = query + " WHERE LEndDate IS NOT NULL";
            query = query + " AND EmpNo in (SELECT EmpNo FROM Employee_dp WHERE DNO = " + Session["WSDNo"].ToString() + ") and status = 1 and (Emp_id is not NULL and Emp_id <> 0)";
            query = query + " AND (LEDATE >= '" + Session["WSStartDate"].ToString() + " 00:00:00' or ledate is null)";
            query = query + " AND LENDDATE <= '" + Session["WSEndDate"].ToString() + " 23:59:59'";
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            OdbcDataAdapter da = new OdbcDataAdapter(query, con);
            da.Fill(ds);
            ReportDocument rpt = new ReportDocument();
            CrystalReportViewer1.Visible = true;
            rpt.FileName = Server.MapPath(@"ProductivityReport\SummaryReport.rpt");
            rpt.SetDataSource(ds.Tables[0]);
            //rpt.SetParameterValue("RecordCount", ds.Tables[0].Rows.Count);
            rpt.SetParameterValue("StartDate", Convert.ToDateTime(Session["WSStartDate"].ToString()));
            rpt.SetParameterValue("EndDate", Convert.ToDateTime(Session["WSEndDate"].ToString()));
            rpt.SetParameterValue("DName", Session["WSDName"].ToString());
            CrystalReportViewer1.ReportSource = rpt;
            //rpt.ExportToDisk(ExportFormatType.Excel, Server.MapPath("") + @"\" + Session.SessionID.ToString() + ".xls");  
            con.Close();
        }
        catch (Exception exp)
        {
            lblError.Visible = true;
            lblError.Text = exp.InnerException.Message;
        }
        finally
        {
            con.Dispose();
            cmd.Dispose();
            ds.Dispose();
        } 
    }
}
