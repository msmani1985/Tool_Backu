using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for wip_projectionclass
/// </summary>
public class wip_projectionclass
{
    OdbcConnection ocon = null;
	public wip_projectionclass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private OdbcConnection conopen(OdbcConnection ocn)
    {

        if (ocn != null && ocn.State == ConnectionState.Open)
            return ocn;
        ocn = new OdbcConnection(@"Driver={Easysoft Interbase ODBC};DB=192.9.200.148:e:\db\TRACKING.GDB;UID=sysdba;pwd=masterkey;ROLE=sysdba");
        ocn.Open();
        return ocn;
    }
    private OdbcConnection conclose(OdbcConnection ocn)
    {
        if (ocn != null)
        {
            if (ocn.State != ConnectionState.Closed)
                ocn.Close();
            ocn.Dispose();
            return ocn;
        }
        return ocn;
    }
            
    public ReportDocument Generate_ProjectionRpt(string custno, string forload)
    {
        ReportDocument prodoc = new ReportDocument();
        try
        {
            prodoc.Load(HttpContext.Current.Server.MapPath("") + @"\\jobstatusrpt.rpt");
            prodoc.SetParameterValue("CUSTNO", (custno == "10066") ? "0" : custno);
            prodoc.SetDatabaseLogon("SYSDBA", "masterkey");
            if (forload == "true")
                return prodoc;
            //else
            //{
            //    StreamReader sr=new StreamReader(prodoc.ExportToStream(ExportFormatType.HTML40));
            //    return sr.ReadToEnd();
            //}
            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

            prodoc = null;
        }

        
    }
    public string Generate_ProjectionRpt_Email(string forload,string custno )
    {
        ReportDocument pdoc = new ReportDocument();
        pdoc.Load(@"\\jobstatusrpt.rpt");
        pdoc.SetParameterValue("CUSTNO", (custno == "10066") ? "0" : custno);
        pdoc.SetDatabaseLogon("sysdba", "masterkey");
        if (forload == "false")
        {
            MemoryStream ostream = new MemoryStream();
            ostream= (MemoryStream)pdoc.ExportToStream(ExportFormatType.HTML40);
            ostream.Position=0;
            StreamReader sr=new StreamReader(ostream);
            return sr.ReadToEnd();
            //string b = ostream.ToString();
        }
        return "Authentication Required";
    }

}
