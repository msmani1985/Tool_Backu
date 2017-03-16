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
using System.Xml;
using System.IO;
using System.Text;

public partial class invoicedreport : System.Web.UI.Page
{
    int iRowNumber = 0;
    string sSUMPages = "";
    double invTotal = 0;
    double dollartal = 0;
    double eurotal = 0;
    double inrtal = 0;
    //double invTotal2 = 0;
    string sFromDate;
    int cellID = 0;
    int formulacnt = 4;
    XmlDocument JCRDom = new XmlDocument();
    ReportDocument TFDoc = new ReportDocument();
    string sSortExp = "";
    string sSortDir = "desc";
    TandFInvoiceDS ds = new TandFInvoiceDS();
    public string strTotal = "";
    private static DataTable dtable = null;
    int NA = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            fromdate.Text = DateTime.Now.ToShortDateString();
            todate.Text = DateTime.Now.ToShortDateString();
            if (Session["CustomerName"] != null)
            {
                ViewState["sortOrder"] = "desc";
                sSortExp = "";
                sSortDir = "desc";
                DataSet oDS = new DataSet();
                oDS = (DataSet)(Session["CustomerName"]);
                ddlcustomer.DataSource = oDS;
                ddlcustomer.DataBind();
                oDS = null;
            }
            else
            {
                string sHTML = "";
                Page page = HttpContext.Current.Handler as Page;

                sHTML += "<script language='javascript'>";
                sHTML += "window.open('Login.aspx','_top')";
                sHTML += "</script>";

                //page.RegisterStartupScript("script", sHTML);
                ClientScript.RegisterStartupScript(this.GetType(), "script", sHTML);
            }
             
        }
        //else
        //{
        //    TFDoc.FileName = Server.MapPath("") + @"\CrystalReports\TandFInvoicedReport.rpt";
        //    PDFTandFCReportViewer.ReportSource = TFDoc;

        //}
        
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Invoiced_IBSQL bib = new Invoiced_IBSQL();
        //DataSet ds = new DataSet();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        try
        {
            invTotal = 0;
            fromdate.CssClass = "";
            todate.CssClass = "";
            lblMessage.Visible = false;
            div_reportdetails.Visible = true;
            sFromDate = fromdate.Text.ToString();
            if (fromdate.Text == "")
                fromdate.CssClass = "error";

            if (todate.Text == "")
                todate.CssClass = "error";

            if (fromdate.Text == "" || todate.Text == "")
                return;
            adgdispatchedlist.Visible = false;
            adgdispatchedlist.Caption = "";
            ds = bib.GetInvoicedJobs4(Convert.ToInt16(ddlcustomer.SelectedValue), Convert.ToInt16(ddljobtype.SelectedValue), 0, 0, fromdate.Text, todate.Text);

            TFDoc.FileName = Server.MapPath("") + @"\CrystalReports\TFInvReport.rpt";

            DataTable dt = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[1];
                DataRow dr;
                XmlNode oNode = null;
                string slocationid = "i";
                string sInvValue = "0";
                lblMessage.Visible = false;
                adgdispatchedlist.Visible = true;
                if (Session["location"] != null)
                    slocationid = Session["location"].ToString();
                else
                {
                    lblMessage.Text = "Session has Expired ,please Relogin";
                    lblMessage.Visible = true;
                    adgdispatchedlist.Visible = false;
                }
                DataColumn dc = new DataColumn("EuroVal", System.Type.GetType("System.Decimal"));
                dt.Columns.Add(dc);
                DataColumn dc2 = new DataColumn("PoundVal", System.Type.GetType("System.Decimal"));
                dt.Columns.Add(dc2);
                invTotal = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (dr["CATEGORY"].ToString().Trim() != "Article")
                    {
                        oNode = GetInvoiceXML(dr["IINNO"].ToString(), dr["JOURCODE"].ToString().Trim() + dr["IISSUENO"].ToString().Trim(), RB_invoiceval.SelectedValue.ToString());

                        sInvValue = bib.GetAttributeValue(oNode, "INVOICEVALUE");

                        dr["EuroVal"] = sInvValue;
                        dr["PoundVal"] = sInvValue;
                        invTotal = invTotal + Convert.ToDouble(sInvValue);
                        dr.AcceptChanges();
                    }
                }
                dt.AcceptChanges();
                DataView dv = dt.DefaultView;
                dv.Sort = "idate,IINNO";
                TFDoc.SetDataSource(dv.ToTable());//dt is datatable can u give datatable or dataset
                TFDoc.DataDefinition.FormulaFields["Header1"].Text = "'Journal Invoiced Report between " + ChangeDateFormat(fromdate.Text.ToString()) + " and " + ChangeDateFormat(todate.Text.ToString()) + "'";
                TFDoc.DataDefinition.FormulaFields["Header2"].Text = "'MONTHLY INVOICING  - " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate.ToString()).Year.ToString() + "'";
                TFDoc.DataDefinition.FormulaFields["Footer1"].Text = "'Total Invoiced for " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate).Year.ToString() + "'";

                TFDoc.SetParameterValue("LocationStr", RB_invoiceval.SelectedValue.ToString());
                string ipath = @"\\192.9.201.222\db\Monthly_Sales\";//ConfigurationManager.AppSettings["invoicedrptpath"].ToString();
                TFDoc.ExportToDisk(ExportFormatType.Excel, ipath + "TandFInvoiced_" + Session["employeeid"].ToString() + ".xls");




                //MemoryStream oStream;
                //Response.Clear();
                //Response.Buffer = true;
                //oStream = (MemoryStream)TFDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
                //Response.ContentType = "application/vnd.ms-excel";
                //Response.BinaryWrite(oStream.ToArray());
                //Response.End();
                //oStream.Flush();
                //oStream.Close();
                //oStream.Dispose();



                //PDFTandFCReportViewer.ReportSource = TFDoc;
                dc = null; dc2 = null; oNode = null; dt = null; dr = null;
                TFDoc = null;
            }
            //For Setting Excel Cell border, margin ,set autofit row
            Report InvExl = new Report();
            string ipath2 = @"\\192.9.201.222\db\Monthly_Sales\"; //ConfigurationManager.AppSettings["invoicedrptpath"].ToString();
            //InvExl.InvRptExcelSettingTF(ipath2 + "TandFInvoiced_" + Session["employeeid"].ToString() + ".xls", "true");
            //InvExl.InvRptExcelSetting(Server.MapPath("") + @"\CrystalReports\TandFInvoiced_" + Session["employeeid"].ToString() + ".xls", "true");
            InvExl = null;
            if (ds != null)
            {
                //if (ds.Tables[0].Rows.Count > 0)
                if (ds.Tables[1].Rows.Count > 0)
                {

                    //TFDoc = new ReportDocument();
                    //TFDoc.PrintOptions.PaperSize = PaperSize.PaperA4;
                    //TFDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //TFDoc.FileName = Server.MapPath("") + @"\CrystalReports\TandFInvoicedReport.rpt";  

                    //TFDoc.SetDataSource(ds.Tables[0]);
                    //PDFTandFCReportViewer.ReportSource = TFDoc; 


                    //adgdispatchedlist.Columns[11].Visible = false;
                    adgdispatchedlist.Caption = "<SPAN style='height:30px;font-weight:bold'>" + ddljobtype.SelectedItem.Text + " Invoiced Report between " + ChangeDateFormat(fromdate.Text.ToString()) + " and " + ChangeDateFormat(todate.Text.ToString()) + "</span><br>";
                    adgdispatchedlist.Caption += "<SPAN style='height:30px;font-weight:bold'>MONTHLY INVOICING - " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate.ToString()).Year.ToString() + "</SPAN><BR>";
                    adgdispatchedlist.Caption += "<SPAN style='height:30px;font-weight:bold'>T&F UK&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NAME OF TYPESETTER: DATAPAGE</span><br>";
                    adgdispatchedlist.Visible = true;
                    //adgdispatchedlist.BorderWidth = new System.Web.UI.WebControls.Unit("5px"); 
                    /*
                    adgdispatchedlist.Columns[3].Visible = false;
                    adgdispatchedlist.Columns[4].Visible = false;
                    adgdispatchedlist.Columns[11].Visible = false;
                    adgdispatchedlist.Columns[12].Visible = false;
                    adgdispatchedlist.Columns[13].Visible = false;
                     * 
                    */
                    //03 March 2009
                    //adgdispatchedlist.Columns[12].Visible = false;
                    //adgdispatchedlist.Columns[13].Visible = false;

                    if (ddljobtype.SelectedValue == "1")
                    {
                        adgdispatchedlist.Columns[2].HeaderText = "4 Letter Acronym";
                        adgdispatchedlist.Columns[3].Visible = true;
                        adgdispatchedlist.Columns[4].Visible = true;

                    }
                    else if (ddljobtype.SelectedValue == "2")
                    {
                        adgdispatchedlist.Columns[2].HeaderText = "Book";
                        adgdispatchedlist.Columns[3].HeaderText = "Book Title";
                        //adgdispatchedlist.Columns[4].Visible = false;
                        adgdispatchedlist.Columns[6].Visible = false;
                        adgdispatchedlist.Columns[7].Visible = false;
                        adgdispatchedlist.Columns[8].HeaderText = "Pages";
                    }
                    else if (ddljobtype.SelectedValue == "3")
                    {
                        adgdispatchedlist.Columns[2].HeaderText = "Project";
                        adgdispatchedlist.Columns[3].HeaderText = "Project Title";


                    }
                    sSUMPages = ds.Tables[1].Compute("SUM(TOTALPAGES)", string.Empty).ToString();
                    strTotal = sSUMPages;
                    //adgdispatchedlist.DataSource = ds.Tables[0];
                    adgdispatchedlist.DataSource = ds.Tables[1];
                    adgdispatchedlist.DataBind();
                    Session["invDS"] = ds;

                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "No Records Found";
                    adgdispatchedlist.Visible = false;
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "No Records Found";
                adgdispatchedlist.Visible = false;
            }
            adgdispatchedlist.Columns[9].Visible = false; // INR 
            adgdispatchedlist.Columns[10].Visible = false; // EURO
            adgdispatchedlist.Columns[11].Visible = false; // dollar
            adgdispatchedlist.Columns[12].Visible = false; // pound
            //adgdispatchedlist.Columns[13].Visible = false; // indian preview
            //adgdispatchedlist.Columns[14].Visible = false; // dublin preview
            adgdispatchedlist.Columns[13].Visible = true; // indian preview
            adgdispatchedlist.Columns[14].Visible = true; // dublin preview

            ds = null;
            bib = null;


            if (RB_invoiceval.SelectedValue.ToString() == "i")
            {
                adgdispatchedlist.Columns[9].Visible = true; // INR 
                adgdispatchedlist.Columns[10].Visible = true; // EURO
            }
            else if (RB_invoiceval.SelectedValue.ToString() == "d")
            {
                // adgdispatchedlist.Columns[10].Visible = true; // EURO
                // adgdispatchedlist.Columns[11].Visible = true; // dollar
                adgdispatchedlist.Columns[12].Visible = true; // pound
            }

        }
        catch (Exception ex)
        {
            div_reportdetails.Visible = false;
            adgdispatchedlist.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = ex.Message.ToString();
        }
    }

    protected void adgdispatchedlist_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string pdfname = "";
        string pdfnamedub = "";

        if (e.Item.DataItem != null)
        {
            try
            {
                if (DataBinder.Eval(e.Item.DataItem, "IDATE") != null)
                    e.Item.Cells[0].Text = ChangeDateFormat(DataBinder.Eval(e.Item.DataItem, "IDATE").ToString());
                string s = e.Item.ItemType.ToString();


                e.Item.Cells[1].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CNAME"));

                e.Item.Cells[2].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "JOURCODE"));

                e.Item.Cells[3].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "JOURTITLE"));

                e.Item.Cells[4].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "INVDNAME"));

                e.Item.Cells[5].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "IINNO"));                

                e.Item.Cells[6].Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "IISSUENO"));  


                /*e.Item.Cells[9].Text = "0"; // INR
                e.Item.Cells[10].Text = "0"; // euro
                e.Item.Cells[11].Text = "0"; // dollar
                e.Item.Cells[12].Text = "0"; // pound
                 * */
                
                /*     biz_IB bib = new biz_IB();
                     XmlNode oNode = null;
                     string slocationid = "i";
                     string sInvValue = "0";
                     string sCurrency = "";


                     if (Session["location"] != null)
                         slocationid = Session["location"].ToString();

                     oNode = GetInvoiceXML(DataBinder.Eval(e.Item.DataItem, "IINNO").ToString(), DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim(), slocationid);
                     sInvValue = bib.GetAttributeValue(oNode, "INVOICEVALUE");
                     sCurrency = bib.GetCurrencyType(oNode);
                     bib = null;
                     cellID = 9; //by default INR value
                     if (sCurrency == "&pound;")
                         cellID = 12;
                     else if (sCurrency == "&#36;" || sCurrency == "CAD")
                         cellID = 11;
                     else if (sCurrency == "&euro;")
                         cellID = 10;
                     //e.Item.Cells[cellID].Text = String.Format("{0:0.00}", Convert.ToDouble(sInvValue)).ToString() ;
                     e.Item.Cells[cellID].Text = String.Format("{0:0.00}", Convert.ToDouble(sInvValue)).ToString();
                     //e.Item.Cells[cellID].Attributes.Add("style", "mso-number-format:\"0.00\"");
                     e.Item.Cells[cellID].Attributes.Add("class", "Style1");

                     //e.Item.Cells[cellID].Text = sInvValue;
                     //e.Item.Cells[cellID].Attributes.Add("class", "mystyle1");     
                 * */
                HyperLink Hl = (HyperLink)e.Item.Cells[13].FindControl("DublinPreviewHL");
                HyperLink H2 = (HyperLink)e.Item.Cells[12].FindControl("IndiaPreviewHL");
                if (DataBinder.Eval(e.Item.DataItem, "PAGEFORMAT").ToString().Length > 0)
                    e.Item.Cells[7].Text = DataBinder.Eval(e.Item.DataItem, "PAGEFORMAT").ToString().Substring(0, 1);

                e.Item.Cells[8].Text = DataBinder.Eval(e.Item.DataItem, "TOTALPAGES").ToString();
                if (DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23433" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23612")//For  Psychology Press
                    e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES_NOCOVER").ToString();
                else if (DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23705" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23743" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23648" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23667" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23650" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23480" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23757" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23686")//All r TandF
                    e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTFNOPRELIMS").ToString();
                else if (DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "10040" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 26064 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) < 27115)
                    e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES_PSY").ToString();
                else if (DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "10040" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 27115)
                    e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES_PSY_NOPRELIMS").ToString();

                else if (DataBinder.Eval(e.Item.DataItem, "journo").ToString() == "2683" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) == 26800)//For RAPL 47/1
                    e.Item.Cells[8].Text = "P:" + (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISK_RAPL")) + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTF")));
                else if (DataBinder.Eval(e.Item.DataItem, "journo").ToString() == "2683")//For other RAPL except invoiceno 26800
                    e.Item.Cells[8].Text = "P:" + (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISK_RAPL")) + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTFNOPRELIMS")));

                else if (DataBinder.Eval(e.Item.DataItem, "ISARTICLEBASED").ToString() == "1")
                {
                    string sIsArticle = DataBinder.Eval(e.Item.DataItem, "NOOFARTICLES").ToString();
                    e.Item.Cells[8].Text = "A:" + sIsArticle;
                }
                else if(ddljobtype.SelectedValue == "1")//// Journal
                {
                    //For Remove Cover
                    //string sIsPages="0";
                    e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES").ToString();
                    if (DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "2556" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 26064 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) < 27115)
                        e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTF").ToString();
                    else if (DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "2556" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 27115)
                        e.Item.Cells[8].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTFNOPRELIMS").ToString();
                }

                if (ddljobtype.SelectedValue == "1")////   Journal
                {
                    pdfname = Session["PDFFilePathInd"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf";
                    pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf";
                    H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=i&pdfname=" + "Datapage_" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + "Datapage_" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    
                    
                    // comment request by sivaraj
                    /*
                    if (File.Exists(pdfname))//check in India
                        H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    else
                        //H2.Visible s= false;
                        H2.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=i&issueno=" + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim() + "&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&jourcode=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + "&journo=" + DataBinder.Eval(e.Item.DataItem, "JOURNO").ToString().Trim() + "&ino=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=1','PreviewInvoice');");

                    if (File.Exists(pdfnamedub))//check in Dublin
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    else
                        //Hl.Visible = false;
                        Hl.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=d&issueno=" + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim() + "&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&jourcode=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + "&journo=" + DataBinder.Eval(e.Item.DataItem, "JOURNO").ToString().Trim() + "&ino=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=1','PreviewInvoice');");
                    
                    */
                    //pdfname = DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim() + ".pdf";
                    //pdfname = pdfname.Trim().Replace("/", "_");
                    // Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + pdfname + "','previewpdf');"  );
                }
                else if (ddljobtype.SelectedValue == "2")//// Book
                {
                    if ( //Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) != 27163 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) != 27164 && 
                        Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) >= 27122 && DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() == "10085")//For Independent College
                    {
                        pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "IINNO").ToString().Trim().Replace("/", "_") + ".pdf";
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "IINNO").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }
                    else
                    {
                        pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }

                    pdfname = Session["PDFFilePathInd"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                    H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=i&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    
                    
                    
                    //Commented request by sivaraj
                    /*
                    if (File.Exists(pdfname))
                        H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    else
                        //H2.Visible = false;
                        H2.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=i&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&bno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=2','PreviewInvoice');");

                    if (File.Exists(pdfnamedub))
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    else
                        //Hl.Visible = false;
                        Hl.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=d&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&bno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=2','PreviewInvoice');");
                    */
                     
                    //pdfname = Server.MapPath("") + @"\InvoiceTemplates\India\output\" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + ".pdf";
                    //pdfname = DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + ".pdf";
                    //pdfname = pdfname.Trim().Replace("/", "_");
                    //Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname="+ pdfname +"','PreviewInvoice');");
                }
                else if (ddljobtype.SelectedValue == "3")//// Project
                {
                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) >= 27122 && DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() == "10085")//For Independent College
                    {
                        pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "IINNO").ToString().Trim().Replace("/", "_") + ".pdf";
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "IINNO").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }
                    else
                    {
                        pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }
                    pdfname = Session["PDFFilePathInd"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                    H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=i&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    /*
                    if (File.Exists(pdfname))
                        H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    else
                        //H2.Visible = false;
                        H2.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=i&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&projectno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=3','PreviewInvoice');");
                    if (File.Exists(pdfnamedub))
                        Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    else
                        //Hl.Visible = false;
                        Hl.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=d&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&projectno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=3','PreviewInvoice');");
                    */
                     
                    //pdfname = Server.MapPath("") + @"\InvoiceTemplates\India\output\" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + ".pdf";
                    //pdfname = DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + ".pdf";
                    //pdfname = pdfname.Trim().Replace("/", "_");
                    // Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname="+ pdfname +"','PreviewInvoice');");
                }
            }
            catch (Exception oex)
            {
                lblMessage.Text = oex.Message.ToString();
            }

        }
        switch (e.Item.ItemType)
        {
            case ListItemType.Header:
                e.Item.Attributes.Add("bgcolor", "lightgrey");
                break;
           /* case ListItemType.Item:
            case ListItemType.AlternatingItem:
                //invTotal += Convert.ToDouble(e.Item.Cells[cellID].Text);
                invTotal += Convert.ToDouble(e.Item.Cells[12].Text);
                inrtal += Convert.ToDouble(e.Item.Cells[9].Text);
                eurotal += Convert.ToDouble(e.Item.Cells[10].Text);
                dollartal += Convert.ToDouble(e.Item.Cells[11].Text);
                formulacnt = formulacnt + 1;
                break;
                */
            case ListItemType.Footer:
                //e.Item.Cells[cellID].Text = "<B>&pound;" + String.Format("{0:0.00}", invTotal).ToString()  + "</B>";
                e.Item.Cells[12].Text = "<B>&pound;" + String.Format("{0:0.00}", invTotal).ToString() + "</B>";
                e.Item.Cells[9].Text = "<B>INR" + String.Format("{0:0.00}", inrtal).ToString() + "</B>";
                e.Item.Cells[10].Text = "<B>€" + String.Format("{0:0.00}", invTotal).ToString() + "</B>";
                e.Item.Cells[11].Text = "<B>$" + String.Format("{0:0.00}", dollartal).ToString() + "</B>";
                //e.Item.Cells[cellID].Attributes.Add("align", "right");
                //e.Item.Cells[cellID].Attributes.Add("class", "CurrencyType");
                e.Item.Cells[3].Text = "<B>Total Invoiced for " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate).Year.ToString() + "</B>";
                e.Item.Cells[3].ColumnSpan = 3;
                e.Item.Cells[4].Visible = false;
                e.Item.Cells[5].Visible = false;
                break;
        }

    }

    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            //sbData.Append("<tr valign='top'><td colspan='6' align='center'><h4>Launch OverAll Report</h4></td><tr>");
            //sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Date of invoice</b></td><td bgcolor='silver'><b>Invoice number</b></td><td bgcolor='silver'><b>Journal acronym</b></td><td bgcolor='silver'><b>CATS ID number</b></td><td bgcolor='silver'><b>Production Editor</b></td><td bgcolor='silver'><b>Amount in £GBP</b></td></tr>");
            sFromDate = fromdate.Text.ToString();
            int intFirstTow = 0;
            int intLastTow = 0;
            intFirstTow = intFirstTow + 1;
            sbData.Append("<tr><td style='width:100%;font-weight: bold;font-size: 13px;font-family: Arial;' colspan='10'>Journal Invoiced Report between " + ChangeDateFormat(fromdate.Text.ToString()) + " and " + ChangeDateFormat(todate.Text.ToString()) + " </td></tr>");
            intFirstTow = intFirstTow + 1;
            sbData.Append("<tr><td style='width:100%;font-weight: bold;font-size: 13px;font-family: Arial;' colspan='10'>MONTHLY INVOICING  - " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate.ToString()).Year.ToString() + " </td></tr>");
            intFirstTow = intFirstTow + 1;
            sbData.Append("<tr><td style='width:100%;font-weight: bold;font-size: 13px;font-family: Arial;' colspan='10'>T&F UK NAME OF TYPESETTER: DATAPAGE</td></tr>");
            intFirstTow = intFirstTow + 1;
            sbData.Append("<tr>");
            int m = 0;
            double dblTotal = 0;
            string strCurrency="";
            for (int j = 0; j < adgdispatchedlist.Columns.Count; j++)
            {
                if (adgdispatchedlist.Columns[j].Visible == true && j != 13 && j != 14)
                {
                    //string strColumnName = "";

                    //if (adgdispatchedlist.Columns[j].HeaderText.ToString() == "Invoice Date")
                    //{ 
                    //    strColumnName="Invoice "+"&nbsp;"+"Date";
                    //}

                    m = m + 1;
                    if (m==7 || m==8 || m==5)
                    {
                        sbData.Append("<td  style='width:100px;font-weight: bold;font-size: 12px;font-family: Arial;background:#C0C0C0;'>" + "&nbsp;" + adgdispatchedlist.Columns[j].HeaderText.ToString() + "</td>");
                    }
                    else if (m == 9)
                    {
                        strCurrency = adgdispatchedlist.Columns[j].HeaderText.ToString();
                        sbData.Append("<td align='right' style='width:100px;font-weight: bold;font-size: 12px;font-family: Arial;background:#C0C0C0;'>" + "&nbsp;" + adgdispatchedlist.Columns[j].HeaderText.ToString() + "</td>");
                        
                    }
                    else
                    {
                        sbData.Append("<td  style='width:100%;font-weight: bold;font-size: 12px;font-family: Arial;background:#C0C0C0;'>" + "&nbsp;" + adgdispatchedlist.Columns[j].HeaderText + "</td>");
                    }
                }
            }
            sbData.Append("</tr>");    


            for (int i = 0; i < adgdispatchedlist.Items.Count; i++)
            {
                sbData.Append("<tr>");

                int n = 0;
              
                for (int j = 0; j < adgdispatchedlist.Columns.Count; j++)
                {
                    if (adgdispatchedlist.Columns[j].Visible == true && j!=13 && j!=14)
                    {
                        n = n + 1;
                        if (n == 7 || n == 8)
                        {
                            sbData.Append("<td>" + "&nbsp;" + adgdispatchedlist.Items[i].Cells[j].Text.ToString() + "</td>");
                        }
                        else if (n == 9)
                        {
                            sbData.Append("<td align='right' style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";>" + adgdispatchedlist.Items[i].Cells[j].Text.ToString() + "</td>");
                            if (Convert.ToString(adgdispatchedlist.Items[i].Cells[j].Text.Replace("&nbsp;", "")).Trim() != "")
                            {
                                dblTotal = dblTotal + Convert.ToDouble(adgdispatchedlist.Items[i].Cells[j].Text);
                            }
                            
                        }
                        else
                        {
                            sbData.Append("<td>" + "&nbsp;" + adgdispatchedlist.Items[i].Cells[j].Text.ToString() + "</td>");
                        }
                    }
                }
                sbData.Append("</tr>");    
            }
            intFirstTow = intFirstTow + 1;
            intLastTow = intFirstTow + adgdispatchedlist.Items.Count - 1;
            strCurrency = strCurrency.Replace("Invoice Total", "");
            strCurrency = strCurrency.Replace("(", "");
            strCurrency = strCurrency.Replace(")", "");
            sbData.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            sbData.Append("<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            sbData.Append("<tr><td></td><td></td><td colspan='4' align='left'  style='font-weight: bold;font-size: 12px;font-family: Arial;'> Total Invoice for " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate.ToString()).Year.ToString() + "</td><td></td><td></td><td align='right' style='font-weight:bold;' style=\"mso-number-format: " + strCurrency + "\\##\\,\\##\\##0\\.00\";'>" + dblTotal.ToString() + "<td></tr>");    
            

                //sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
                //sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td align='right'><b>Total: </b></td><td align='right' style=\"mso-number-format:&pound;\\##\\,\\##\\##0\\.00\"><b>=sum(F2:F" + (dtable.Rows.Count + 2) + ")</b></td></tr>");
                sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + "_" + Convert.ToDateTime(sFromDate.ToString()).Year.ToString() + ".xls"));
            //Response.ContentEncoding = Encoding.Unicode;
            //Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
            //Response.Close();
            

           /* FileInfo file = new FileInfo(sFileName);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            */


        }
        catch (Exception ex)
        {
            Session["error"] = ex.Message.ToString();
            throw ex; }
        //Response.Flush();
        
        /*System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        adgdispatchedlist.Font.Size = 8;
        //adgdispatchedlist.HeaderStyle.Height = 60;
        //adgdispatchedlist.HeaderStyle.Width = 2;
        //adgdispatchedlist.ItemStyle.Height = 22;
        //adgdispatchedlist.ItemStyle.Width = 5;
        adgdispatchedlist.Font.Name = "Tahoma";
        adgdispatchedlist.Columns[13].Visible = false;
        adgdispatchedlist.Columns[14].Visible = false;
        adgdispatchedlist.Columns[9].Visible = false;
        adgdispatchedlist.Columns[11].Visible = false;
        if (Session["location"].ToString() == "i")
            adgdispatchedlist.Columns[12].Visible = false;
        else
            adgdispatchedlist.Columns[10].Visible = false;
        adgdispatchedlist.RenderControl(oHtmlTextWriter);
        //This Is For Name Space In Excel
        Response.Write("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" +
               " xmlns=\"http://www.w3.org/TR/REC-html40\">");
        //This is for number format (ex- "0.00" and also apply Currency Type)
        Response.Write("<style>@page{font-family:Tahoma;font-size: 8pt;}.style1 { mso-number-format:Fixed; }");
        Response.Write(".CurrencyType {font-weight: bold; mso-number-format:\"\\[$\\00A3-809\\]\\#\\,\\#\\#0\\.00\\;\\[Red\\]\\[$\\00A3-809\\]\\#\\,\\#\\#0\\.00\"; }");
        Response.Write(".InrType {font-weight: bold; mso-number-format:\"\\[$INR\\]\\\\ \\#\\,\\#\\#0\\.00\\;\\[Red\\]\\[$INR\\]\\\\ \\#\\,\\#\\#0\\.00\"; }");
        Response.Write(".EuroType {font-weight: bold; mso-number-format:\"\\[$€-1809\\]\\#\\,\\#\\#0\\.00\\;\\[Red\\]\\[$€-1809\\]\\#\\,\\#\\#0\\.00\"; } ");
        Response.Write(".DollarType {font-weight: bold; mso-number-format:\"\\0022$\\0022\\#\\,\\#\\#0\\.00\"; } </style>");

        Response.Write(oStringWriter.ToString().Replace("Acronym", "<br><span style='mso-spacerun:yes'> </span>Acronym"));*/
        //Response.End();
        
    }

    protected XmlNode GetInvoiceXML(string invoceno, string invoiceitem, string sLocation)
    {
        try
        {
            //Session["indiaINVfile"] = null;
            if (Session["indiaINVfile"] == null)
                throw new ArgumentException("Session has been Expired, Please relogin");
            else
            {
                string sSite = Session["indiaINVfile"].ToString();

                if (sLocation != "i")
                    sSite = Session["dublinINVfile"].ToString();

                XmlNode JCRNode = null;
                if (iRowNumber == 0)
                    JCRDom.Load(sSite);
                iRowNumber++;
                JCRNode = JCRDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICENO='" + invoceno + "']");

                if (JCRNode == null)
                    JCRNode = JCRDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM='" + invoiceitem + "']");

                if (JCRNode == null)
                    return null;
                else
                    return JCRNode;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    private string ChangeDateFormat(string sText)
    {
        if (sText != "")
        {
            DateTime sDateFormat = Convert.ToDateTime(sText);
            sText = sDateFormat.Day.ToString() + "/" + sDateFormat.Month.ToString() + "/" + sDateFormat.Year.ToString();
        }
        return sText;
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }
    private void DataBind(DataGrid oGrid, TandFInvoiceDS oDs)
    {
        DataView oView = new DataView();
        oView = oDs.Tables[1].DefaultView;
        if (sSortExp != "")
            oView.Sort = string.Format("{0} {1}", sSortExp, sSortDir);
        oGrid.DataSource = oView;
        oGrid.DataBind();
        //pnlContainer.Controls.Clear();
        //pnlContainer.Controls.Add(oGrid);
        //HideShowColumns(true, oGrid);
    }
    protected void Grid_SortCommand(object sender, DataGridSortCommandEventArgs e)
    {
        sSortExp = e.SortExpression;
        sSortDir = sortOrder;
        ds = new TandFInvoiceDS();
        if (Session["invDS"] != null)
            ds = (TandFInvoiceDS)Session["invDS"];
        if (ds != null && ds.Tables[1].Rows.Count > 0)
        {
            /*if (Convert.ToInt16(ddljobtype.SelectedValue) == 2)///////////// 2 For Books
                DataBind(BooksInvoiceList, ds);
            else if (Convert.ToInt16(ddljobtype.SelectedValue) == 3)/////////// 3 For Projects
                DataBind(ProjectInvoiceList, ds);
            else if (Convert.ToInt16(ddljobtype.SelectedValue) == 1)//////////// 1 For Journal
             * */
            DataBind(adgdispatchedlist, ds);
        }
        else
        {
            btnSubmit_Click(null, null);
        }

    }


}
