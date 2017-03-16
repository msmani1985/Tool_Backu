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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

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
        invTotal = 0;
        fromdate.CssClass = "";
        todate.CssClass = "";
        lblMessage.Visible = false;
        sFromDate = fromdate.Text.ToString();
        if (fromdate.Text == "")
            fromdate.CssClass = "error";

        if (todate.Text == "")
            todate.CssClass = "error";

        if (fromdate.Text == "" || todate.Text == "")
            return;
        adgdispatchedlist.Visible = false;
        adgdispatchedlist.Caption = "";
        ds = bib.GetInvoicedJobs3(Convert.ToInt16(ddlcustomer.SelectedValue), Convert.ToInt16(ddljobtype.SelectedValue), 0, 0, fromdate.Text, todate.Text);

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
                oNode = GetInvoiceXML(dr["IINNO"].ToString(), dr["JOURCODE"].ToString().Trim() + dr["IISSUENO"].ToString().Trim(), slocationid);

                sInvValue = bib.GetAttributeValue(oNode, "INVOICEVALUE");

                dr["EuroVal"] = sInvValue;
                dr["PoundVal"] = sInvValue;
                invTotal = invTotal + Convert.ToDouble(sInvValue);
                dr.AcceptChanges();
            }
            dt.AcceptChanges();

            TFDoc.SetDataSource(dt);//dt is datatable can u give datatable or dataset
            TFDoc.DataDefinition.FormulaFields["Header1"].Text = "'Journal Invoiced Report between " + ChangeDateFormat(fromdate.Text.ToString()) + " and " + ChangeDateFormat(todate.Text.ToString()) + "'";
            TFDoc.DataDefinition.FormulaFields["Header2"].Text = "'MONTHLY INVOICING  - " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate.ToString()).Year.ToString() + "'";
            TFDoc.DataDefinition.FormulaFields["Footer1"].Text = "'Total Invoiced for " + Convert.ToDateTime(sFromDate).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(sFromDate).Year.ToString() + "'";

            TFDoc.SetParameterValue("LocationStr", slocationid);
            TFDoc.ExportToDisk(ExportFormatType.Excel, Server.MapPath("") + @"\CrystalReports\TandFInvoiced_" + Session["employeeid"].ToString() + ".xls");




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
        InvExl.InvRptExcelSetting(Server.MapPath("") + @"\CrystalReports\TandFInvoiced_" + Session["employeeid"].ToString() + ".xls", "true");
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

        if (Session["location"] != null)
        {
            if (Session["location"].ToString() == "i")
            {
                adgdispatchedlist.Columns[9].Visible = true; // INR 
                adgdispatchedlist.Columns[10].Visible = true; // EURO
            }
            else
            {
                // adgdispatchedlist.Columns[10].Visible = true; // EURO
                // adgdispatchedlist.Columns[11].Visible = true; // dollar

                adgdispatchedlist.Columns[12].Visible = true; // pound
            }
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
                    e.Item.Cells[8].Text = "P:" + (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISK_RAPL")) + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"EDITEDDISKTF")));
                else if (DataBinder.Eval(e.Item.DataItem, "journo").ToString() == "2683")//For other RAPL except invoiceno 26800
                    e.Item.Cells[8].Text = "P:" + (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISK_RAPL")) + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"EDITEDDISKTFNOPRELIMS")));
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
                    //H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=i&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    //Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    
                    
                    // comment request by sivaraj
                    
                    //////if (File.Exists(pdfname))//check in India
                    //////    H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    //////else
                        //H2.Visible s= false;
                    H2.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=i&issueno=" + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim() + "&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&jourcode=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + "&journo=" + DataBinder.Eval(e.Item.DataItem, "JOURNO").ToString().Trim() + "&ino=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=1&INVRpt=withpreview','PreviewInvoice');");

                    //if (File.Exists(pdfnamedub))//check in Dublin
                    //    Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim().Replace("/", "_") + ".pdf','previewpdf');");
                    //else
                        //Hl.Visible = false;
                        Hl.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=d&issueno=" + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim() + "&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&jourcode=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + "&journo=" + DataBinder.Eval(e.Item.DataItem, "JOURNO").ToString().Trim() + "&ino=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=1&INVRpt=withpreview','PreviewInvoice');");
                    
                    
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
                        //Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "IINNO").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }
                    else
                    {
                        pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                        //Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }

                    pdfname = Session["PDFFilePathInd"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                    //H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=i&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    
                    
                    
                    //Commented request by sivaraj
                    
                    //if (File.Exists(pdfname))
                    //    H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    //else
                        //H2.Visible = false;
                    H2.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=i&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&bno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=2&INVRpt=withpreview','PreviewInvoice');");

                    //if (File.Exists(pdfnamedub))
                    //    Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    //else
                        //Hl.Visible = false;
                        Hl.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=d&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&bno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=2&INVRpt=withpreview','PreviewInvoice');");
                    
                     
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
                        //Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "IINNO").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }
                    else
                    {
                        pdfnamedub = Session["PDFFilePathDub"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                        //Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=d&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    }
                    pdfname = Session["PDFFilePathInd"].ToString() + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf";
                    //H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?location=i&pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    
                    //if (File.Exists(pdfname))
                    //    H2.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    //else
                        //H2.Visible = false;
                    H2.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=i&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&projectno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=3&INVRpt=withpreview','PreviewInvoice');");
                    //if (File.Exists(pdfnamedub))
                    //    Hl.Attributes.Add("onClick", "window.open('pdfpreview.aspx?pdfname=" + DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim().Replace("/", "_") + ".pdf" + "','PreviewInvoice');");
                    //else
                        //Hl.Visible = false;
                        Hl.Attributes.Add("onClick", "window.open('previewinvoice.aspx?location=d&custno=" + DataBinder.Eval(e.Item.DataItem, "CUSTNO1").ToString() + "&projectno=" + DataBinder.Eval(e.Item.DataItem, "INO").ToString().Trim() + "&category=3&INVRpt=withpreview','PreviewInvoice');");
                    
                     
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

       
        


        //export to excel
        //string sFileName = "Datapage " + Convert.ToDateTime(fromdate.Text.Trim()).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(fromdate.Text.Trim()).Year.ToString() + ".xls";
        try
        {
            this.EnableViewState = false;
            string sFileName = Server.MapPath("") + @"\CrystalReports\TandFInvoiced_" + Session["employeeid"].ToString() + ".xls";
            Response.Buffer = true;
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(Server.MapPath("") + @"\CrystalReports\TandFInvoiced.xls"));

            FileStream fs;
            BinaryReader br;
            fs = new FileStream(sFileName, FileMode.Open);
            br = new BinaryReader(fs);
            Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));
            Response.BinaryWrite(dataBytes);
            br.Close();
            fs.Close();
            Response.Flush();
            //Response.Close();
        }
        catch (Exception ex)
        { }
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
        catch (Exception ex)
        {
            return null;
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
