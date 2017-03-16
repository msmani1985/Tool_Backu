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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.Odbc;

public partial class JobCostReport : System.Web.UI.Page
{
    OdbcCommand ocmd = new OdbcCommand();
    OdbcConnection oconn;
    OdbcDataAdapter da;
    DataSet ds = new DataSet(); 
    Report ObjRpt = new Report();
    String sSqlquery;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["custname"] != null && Request.QueryString["jobtype"] != null && Request.QueryString["jobid"] != null && Request.QueryString["pages"] != null && Request.QueryString["invNO"] != null && Request.QueryString["manhours"] != null && Request.QueryString["jcE"] != null && Request.QueryString["jcI"] != null && Request.QueryString["jrE"] != null && Request.QueryString["jrI"] != null && Request.QueryString["plE"] != null && Request.QueryString["plI"] != null && Request.QueryString["INo"] != null)
                {
                    ReportDocument oQRT = new ReportDocument();
                    oQRT.FileName = Server.MapPath("") + "/JobCostDetailsReport.rpt";
                  //  oconn = ObjRpt.DBLiveOpen();
                    ocmd.Connection = oconn;
                    sSqlquery = "SELECT SUM(Cast(((cast(Lenddate as time) - cast(Ledate as time)))/60 as INTEGER)), F_RTrim(Emp_Fname) || ' ' || F_RTrim(Emp_SName)";
                    sSqlquery += " from loggedevents_dp l inner join Employee_dp e on l.EmpNo = e.EmpNo";
                    sSqlquery += " where ((Ino =" + Request.QueryString["INo"].ToString() + ") or (l.ANo IN (Select Ano from article_dp where INO = l.INo))) and IsTimeSheet = 'Y' and LEndDate is not null";
                    sSqlquery += " Group by Emp_FName, Emp_SName Order by Emp_FName ";
                    da = new OdbcDataAdapter(sSqlquery, oconn);
                    da.Fill(ds);
                    oQRT.SetDataSource(ds.Tables[0]);
                    oQRT.SetParameterValue("CustName", Request.QueryString["custname"].ToString());
                    oQRT.SetParameterValue("JCCode", Request.QueryString["jobtype"].ToString());
                    oQRT.SetParameterValue("JCJobId", Request.QueryString["jobid"].ToString());
                    oQRT.SetParameterValue("JCPages", Request.QueryString["pages"].ToString());
                    oQRT.SetParameterValue("ManHours", Request.QueryString["manhours"].ToString());
                    oQRT.SetParameterValue("JCEuro", Request.QueryString["jcE"].ToString());
                    oQRT.SetParameterValue("JCInr", Request.QueryString["jcI"].ToString());
                    oQRT.SetParameterValue("JREuro", Request.QueryString["jrE"].ToString());
                    oQRT.SetParameterValue("JRInr", Request.QueryString["jrI"].ToString());
                    oQRT.SetParameterValue("PLEuro", Request.QueryString["plE"].ToString());
                    oQRT.SetParameterValue("PLInr", Request.QueryString["plI"].ToString());

                    CrystalReportViewer1.ReportSource = oQRT;
                    oconn.Close();
                }
            }
            catch
            {

            }
            finally
            {
                oconn.Dispose();
                ds.Dispose();
                ocmd.Dispose();
                da.Dispose();
            }
        }

    }

    private bool QV(string sRQTest)
    {

        if (sRQTest != null)
            return true;
        else
            return false;
    }
}
