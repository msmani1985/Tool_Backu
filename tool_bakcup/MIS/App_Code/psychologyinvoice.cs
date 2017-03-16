using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

/// <summary>
/// Summary description for psychologyinvoice
/// </summary>
public class psychologyinvoice
{

    string InvoiceNo = "";
    string InvoiceDate = "";
    string InvItem = "";
	public psychologyinvoice()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public ReportDocument CreateReport(Invoice_Details psyds, string location, string Currency,double PAmount,double CAmount,double SAMAmount)
    {
        ReportDocument psydoc = new ReportDocument();
        ReportDocument typesetsub = null;
        ReportDocument copyeditsub = null;
        ReportDocument typesetcopyedit = null;

        Report robj = new Report();
        string sFilePath = "";
        string filename = "";
        try
        {
            //string tandfname = FName.Replace(".rpt", "all.rpt");
            //string psyfname = rptpath.ToString();
            psydoc.FileName = HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\PsychologyReportall.rpt";
            psydoc.SetDataSource(psyds.Tables[1]);
            typesetcopyedit = psydoc.Subreports["XlsTFReport.rpt"];
            typesetcopyedit.SetDataSource(psyds.Tables[1]);

            typesetsub = psydoc.Subreports["ArticlesandPagesRpt.rpt"];
            robj.IssueNo = Convert.ToInt32(psyds.Tables[1].Rows[0]["INO1"]);
            string cqry = "SELECT ANO,JOURNO,INO,AMANUSCRIPTID,ADNO, CATNO, AREALNOOFPAGES,AINVOICEPAGES,JOURCODE,ISARTICLE_BASED,IINVOICENO,IINVOICEDATE,ARTICLE_DP.invno FROM ARTICLE_DP " +
                          "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO JOIN ISSUE_DP ON ISSUE_DP.JOURNO=JOURNAL_DP.JOURNO AND ISSUE_DP.INO=" + robj.IssueNo + " WHERE ARTICLE_DP.INO IN(" + robj.IssueNo + ") AND ADNO NOT IN (10031,12,13,1,4,5,2)";//12 and 13 blank pages,1 and 5 covers
            ArticleandPages ads = robj.ExcelArticlesandPages("not excel", cqry);
            typesetsub.SetDataSource(ads.Tables[1]);
            copyeditsub = psydoc.Subreports["ArticlesandPagesRpt1.rpt"];
            cqry = "SELECT ANO,JOURNO,INO,AMANUSCRIPTID,ADNO, CATNO, AREALNOOFPAGES,AINVOICEPAGES,JOURCODE,ISARTICLE_BASED,IINVOICENO,IINVOICEDATE,ARTICLE_DP.invno FROM ARTICLE_DP " +
                          "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO JOIN ISSUE_DP ON ISSUE_DP.JOURNO=JOURNAL_DP.JOURNO AND ISSUE_DP.INO=" + robj.IssueNo + " WHERE ARTICLE_DP.INO IN(" + robj.IssueNo + ") AND ADNO NOT IN (10031,12,13,1,4,5,2) and aextra_copy_edit='Y'";
            ads = robj.ExcelArticlesandPages("not excel",cqry);
            copyeditsub.SetDataSource(ads.Tables[1]);

            psydoc.SetParameterValue("TSAmount", PAmount);
            psydoc.SetParameterValue("TSAmount", PAmount, "XlsTFReport.rpt");
            psydoc.SetParameterValue("TSAmount", PAmount, "ArticlesandPagesRpt.rpt");
            
            psydoc.SetParameterValue("CEAmount", CAmount);
            psydoc.SetParameterValue("CEAmount", CAmount, "XlsTFReport.rpt");
            psydoc.SetParameterValue("CEAmount", CAmount, "ArticlesandPagesRpt1.rpt");
            psydoc.SetParameterValue("SAMAmount", SAMAmount);
            psydoc.SetParameterValue("SAMAmount", SAMAmount, "XlsTFReport.rpt");

            //This is For Report Test(Common Report)
            psydoc.SetParameterValue("Country", location);
            psydoc.SetParameterValue("Country", location, "XlsTFReport.rpt");
            psydoc.SetParameterValue("CurrencyStr", Currency);
            psydoc.SetParameterValue("CurrencyStr", Currency, "XlsTFReport.rpt");

            sFilePath = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
            if (location == "d")
                sFilePath = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();

            InvoiceNo = psyds.Tables[1].Rows[0]["INVNO"].ToString();
            InvoiceDate = psyds.Tables[1].Rows[0]["INVDATE"].ToString();
            InvItem = psyds.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + psyds.Tables[1].Rows[0]["IISSUENO"].ToString().Trim();

            filename = sFilePath + psyds.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + psyds.Tables[1].Rows[0]["IISSUENO"].ToString().Trim() + ".pdf";
            filename = filename.Trim().Replace("/", "_");

            psydoc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
            return psydoc;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { psydoc = null; typesetsub = null; robj = null; copyeditsub = null; }
        //}//For check InvoiceNo != ""
                       
    }
}
