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

public partial class invoice_sales_analysis_preview : System.Web.UI.Page
{
    private ProductivityBase oProdv = new ProductivityBase();
    private ReportDocument oRpt = new ReportDocument();
    private static DataSet ds;
    protected void Page_Load(object sender, EventArgs e){
        this.popScreen();
    }
    void popScreen()
    {
        if (Session["dsYTD"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            btnExport.Enabled = true;
            btnExportData.Enabled = true;
            ds = (DataSet)Session["dsYTD"];
            string sYear = Session["YTDYear"].ToString();
            if (Session["YTDID"].ToString() == "1"){
                this.Title = "Sales Analysis(YTD Euro) - Preview";
                oRpt.FileName = Server.MapPath("~/ProductivityReport/rptYTD_Euro.rpt");
                oRpt.SetDatabaseLogon("sa", "masterkey");

                oRpt.SetDataSource(ds);
                oRpt.SetParameterValue("pJan", "Jan " + sYear);
                oRpt.SetParameterValue("pFeb", "Feb " + sYear);
                oRpt.SetParameterValue("pMar", "Mar " + sYear);
                oRpt.SetParameterValue("pApr", "Apr " + sYear);
                oRpt.SetParameterValue("pMay", "May " + sYear);
                oRpt.SetParameterValue("pJun", "Jun " + sYear);
                oRpt.SetParameterValue("pJul", "Jul " + sYear);
                oRpt.SetParameterValue("pAug", "Aug " + sYear);
                oRpt.SetParameterValue("pSep", "Sept " + sYear);
                oRpt.SetParameterValue("pOct", "Oct " + sYear);
                oRpt.SetParameterValue("pNov", "Nov " + sYear);
                oRpt.SetParameterValue("pDec", "Dec " + sYear);
                
            }
            else if (Session["YTDID"].ToString() == "2"){
                this.Title = "Sales Analysis(YTD Pages) - Preview";
                oRpt.FileName = Server.MapPath("~\\ProductivityReport\\rptYTD_Pages.rpt");
                oRpt.SetDataSource(ds);
                oRpt.SetParameterValue("pJan", "Jan " + sYear);
                oRpt.SetParameterValue("pFeb", "Feb " + sYear);
                oRpt.SetParameterValue("pMar", "Mar " + sYear);
                oRpt.SetParameterValue("pApr", "Apr " + sYear);
                oRpt.SetParameterValue("pMay", "May " + sYear);
                oRpt.SetParameterValue("pJun", "Jun " + sYear);
                oRpt.SetParameterValue("pJul", "Jul " + sYear);
                oRpt.SetParameterValue("pAug", "Aug " + sYear);
                oRpt.SetParameterValue("pSep", "Sept " + sYear);
                oRpt.SetParameterValue("pOct", "Oct " + sYear);
                oRpt.SetParameterValue("pNov", "Nov " + sYear);
                oRpt.SetParameterValue("pDec", "Dec " + sYear);
            }
            else if (Session["YTDID"].ToString() == "3"){
                this.Title = "Sales Analysis(YTD Currency) - Preview";
                oRpt.FileName = Server.MapPath("~\\ProductivityReport\\rptYTD_Currency.rpt");
                oRpt.SetDataSource(ds);

            }
            else Alert("Error loading report, Invalid request!");

            CrystalReportViewer1.ReportSource = oRpt;
            //CrystalReportViewer1.DataBind();
            string strFile = @"\\192.9.201.222\dp\MIS\MonthlyPDF";
            string strFileName = "Report" + DateTime.Now.ToString().Trim().Replace(" ", "").Replace(":", "").Replace(@"\", "").Replace(@"/", "")+".pdf";
           // oRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "Report");
            strFile = strFile + @"\" + strFileName;
            Label1.Text = ""; //strFile;
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, strFile);

            string strFileS = @"//192.9.201.222/db/SalesPDF/" + strFileName;

            //ShowPdf1.FilePath="~\MonthlyPDF\";
            ShowPdf1.FilePath = "/MonthlyPDF/" + strFileName;
            //CrystalReportViewer1.DataBind();
        }
    }
    public void Alert(string sMessage)
    {
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btnExport_Click(object sender, EventArgs e){
        string sFname = DateTime.Now.ToString("MM_dd_yyyy");
        if (Session["YTDID"].ToString() == "1")
            sFname = "YTD_Euro_" + sFname;
        else if (Session["YTDID"].ToString() == "2")
            sFname = "YTD_Pages_" + sFname;
        else
            sFname = "YTD_Currency_" + sFname;
        oRpt.ExportToHttpResponse(this.ReportExportOption(oRpt, ExportFormatType.Excel),
            Response, true, sFname);
    }
    protected void btnExportData_Click(object sender, EventArgs e){
        string sFname = DateTime.Now.ToString("MM_dd_yyyy");
        if (Session["YTDID"].ToString() == "1")
            sFname = "YTD_Euro_" + sFname;
        else if (Session["YTDID"].ToString() == "1")
            sFname = "YTD_Pages_" + sFname;
        else
            sFname = "YTD_Currency_" + sFname;
        oRpt.ExportToHttpResponse(this.ReportExportOption(oRpt, ExportFormatType.ExcelRecord),
            Response, true, sFname);
    }
    public ExportOptions ReportExportOption(ReportDocument oReport, ExportFormatType oExportTypes)
    {
        return this.ReportExportOption(oReport, "", oExportTypes, 0, 0);
    }
    public ExportOptions ReportExportOption(ReportDocument oReport, string cFileName, ExportFormatType oExportTypes, int nFirstPage, int nLastPage)
    {
        try
        {
            ExportOptions oExportOptions = new ExportOptions();
            PdfRtfWordFormatOptions oFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions();
            DiskFileDestinationOptions oDestinationOptions = ExportOptions.CreateDiskFileDestinationOptions();
            
            switch (oExportTypes)
            {
                case ExportFormatType.PortableDocFormat:
                    oExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

                    //if (nFirstPage > 0 && nLastPage > 0)
                    //{
                    //    oPDFFormatOptions.FirstPageNumber = nFirstPage;
                    //    oPDFFormatOptions.LastPageNumber = nLastPage;
                    //    oPDFFormatOptions.UsePageRange = true;
                    //}

                    PdfRtfWordFormatOptions oPDFFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions();
                    oExportOptions.ExportFormatOptions = oPDFFormatOptions;
                    break;

                case ExportFormatType.WordForWindows:
                    oExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
                    PdfRtfWordFormatOptions oWordFormatOptions = ExportOptions.CreatePdfRtfWordFormatOptions();
                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oWordFormatOptions.FirstPageNumber = nFirstPage;
                        oWordFormatOptions.LastPageNumber = nLastPage;
                        oWordFormatOptions.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oWordFormatOptions;
                    break;

                case ExportFormatType.Excel:                    
                    oExportOptions.ExportFormatType = ExportFormatType.Excel;
                    ExcelFormatOptions oExcelFormatOptions = ExportOptions.CreateExcelFormatOptions();
                    oExcelFormatOptions.ExcelConstantColumnWidth = 4500;
                    oExcelFormatOptions.ExcelUseConstantColumnWidth = true;

                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oExcelFormatOptions.FirstPageNumber = nFirstPage;
                        oExcelFormatOptions.LastPageNumber = nLastPage;
                        oExcelFormatOptions.UsePageRange = true;
                        oExcelFormatOptions.ExcelTabHasColumnHeadings = false;
                    }
                    oExportOptions.ExportFormatOptions = oExcelFormatOptions;
                    break;

                case ExportFormatType.ExcelRecord:
                    oExportOptions.ExportFormatType = ExportFormatType.ExcelRecord;                    
                    ExcelFormatOptions oExcelFormatOptions1 = ExportOptions.CreateExcelFormatOptions();
                    oExcelFormatOptions1.ExcelUseConstantColumnWidth = false;
                    
                    //oExcelFormatOptions1.ExcelConstantColumnWidth = Convert.ToDouble("39");
                    //oExcelFormatOptions1.ExcelAreaType = AreaSectionKind.WholeReport;


                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oExcelFormatOptions1.FirstPageNumber = nFirstPage;
                        oExcelFormatOptions1.LastPageNumber = nLastPage;
                        oExcelFormatOptions1.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oExcelFormatOptions1;
                    break;

                case ExportFormatType.HTML40:
                    oExportOptions.ExportFormatType = ExportFormatType.HTML40;
                    HTMLFormatOptions oHTMLFormatOptions = ExportOptions.CreateHTMLFormatOptions();
                    if (nFirstPage > 0 && nLastPage > 0)
                    {
                        oHTMLFormatOptions.FirstPageNumber = nFirstPage;
                        oHTMLFormatOptions.LastPageNumber = nLastPage;
                        oHTMLFormatOptions.UsePageRange = true;
                    }
                    oExportOptions.ExportFormatOptions = oHTMLFormatOptions;
                    break;
            }
            oExportOptions.ExportDestinationOptions = oDestinationOptions;
            oExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            return oExportOptions;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}
