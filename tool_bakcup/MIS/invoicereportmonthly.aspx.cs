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
using System.Xml;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


public partial class invoicereportmonthly : System.Web.UI.Page
{
    int iRowNumber = 0;
    string sSUMPages = "";
    double invTotal = 0;
    double dollartal = 0;
    double eurotal = 0;
    double inrtal = 0;
    //double invTotal2 = 0;
    DateTime dtTo;
   // string sFromDate;
    int cellID = 0;
    int formulacnt = 4;
    XmlDocument JCRDom = new XmlDocument();
    string sSortExp = "";
    string sSortDir = "desc";
    TandFInvoiceDS ds = new TandFInvoiceDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DDMonthList.SelectedValue = DateTime.Now.Month.ToString();
            DDYearList.SelectedValue = DateTime.Now.Year.ToString();

            ViewState["sortOrder"] = "desc";
            sSortExp = "";
            sSortDir = "desc";
            if (Session["CustomerName"] != null)
            {
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
        dtTo = new DateTime(Convert.ToInt32(DDYearList.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), 1);
        dtTo = dtTo.AddMonths(1);
        dtTo = dtTo.AddDays(-(dtTo.Day));

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Invoiced_IBSQL bib = new Invoiced_IBSQL();
        //DataSet ds = new DataSet();
        
        invTotal = 0;

        adgdispatchedlist.Visible = false;
        adgdispatchedlist.Caption = "";
        XmlNode oNode = null;
        DataTable dt = new DataTable();
        try
        {
            //ds = bib.GetInvoicedJobs3(Convert.ToInt16(ddlcustomer.SelectedValue), Convert.ToInt16(ddljobtype.SelectedValue), 0, 0, fromdate.Text, todate.Text);
            ds = bib.GetInvoicedJobs3(Convert.ToInt16(ddlcustomer.SelectedValue), Convert.ToInt16(ddljobtype.SelectedValue), 0, 0, DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue, dtTo.ToShortDateString(),Convert.ToInt32(RB_location.SelectedValue));
            string slocationid = "i";
            string sInvValue = "0";
            string sCurrency = "";
            div_monthlyreport.Visible = true;
            lblMessage.Visible = false;
            if (Session["location"] != null)
                slocationid = Session["location"].ToString();
            else
            {
                lblMessage.Text = "Session has Expired ,please Relogin";
                lblMessage.Visible = true;
                adgdispatchedlist.Visible = false;
            }

            if (ds != null)
            {
                DataView dv = ds.Tables[1].DefaultView;
                dv.RowFilter = "CATEGORY<>'Article'";
                dt = dv.ToTable();
                //dt = ds.Tables[1]; //ds is DataSet

                DataColumn dc = new DataColumn("Euroval", System.Type.GetType("System.Decimal"));
                dt.Columns.Add(dc);
                DataColumn pounddc = new DataColumn("PoundVal", System.Type.GetType("System.Decimal"));
                dt.Columns.Add(pounddc);
                DataColumn inrdc = new DataColumn("InrVal", System.Type.GetType("System.Decimal"));
                dt.Columns.Add(inrdc);
                DataColumn dollardc = new DataColumn("DollarVal", System.Type.GetType("System.Decimal"));
                dt.Columns.Add(dollardc);
                DataColumn curdc = new DataColumn("CurrencyStr", System.Type.GetType("System.String"));
                dt.Columns.Add(curdc);
                DataRow dr;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dr = dt.Rows[j];
                    oNode = GetInvoiceXML(dr["IINNO"].ToString(), dr["JOURCODE"].ToString().Trim() + dr["IISSUENO"].ToString().Trim(), RB_invoiceval.SelectedValue.ToString());
                    sInvValue = bib.GetAttributeValue(oNode, "INVOICEVALUE");
                    sCurrency = bib.GetCurrencyType(oNode);
                    dr["InrVal"] = 0;
                    dr["EuroVal"] = 0;
                    dr["DollarVal"] = 0;
                    dr["PoundVal"] = 0;
                    if (sCurrency == "INR")
                        dr["InrVal"] = sInvValue;
                    else if (sCurrency == "&euro;")
                        dr["EuroVal"] = sInvValue;
                    else if (sCurrency == "&#36;")
                        dr["DollarVal"] = sInvValue;
                    else if (sCurrency == "&pound;")
                        dr["PoundVal"] = sInvValue;


                    dr["CurrencyStr"] = sCurrency;
                    dr.AcceptChanges();
                }
                dt.AcceptChanges();

                ReportDocument MntRptdoc = new ReportDocument();
                MntRptdoc.FileName = Server.MapPath("") + @"\CrystalReports\TFMonthlyInvReport.rpt";
                MntRptdoc.SetDataSource(dt);
                MntRptdoc.DataDefinition.FormulaFields["Header1"].Text = "'All Accounts : Invoice Detail Report (" + DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue + " To " + dtTo.ToShortDateString() + ")'";
                MntRptdoc.DataDefinition.FormulaFields["Footer1"].Text = "'Total Invoiced for " + Convert.ToDateTime(DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue).Year.ToString() + "'";
                ExportOptions Eopt = new ExportOptions();
                Eopt.ExportDestinationType = ExportDestinationType.DiskFile;
                Eopt.ExportFormatType = ExportFormatType.Excel;
                DiskFileDestinationOptions DFopt = new DiskFileDestinationOptions();
                //DFopt.DiskFileName = Server.MapPath("") + @"\CrystalReports\TandFMonthlyInvReport_" + Session["employeeid"].ToString() + ".xls";
                DFopt.DiskFileName = @"\\192.9.201.222\db\Monthly_Sales\TandFMonthlyInvReport_" + Session["employeeid"].ToString() + ".xls";
                Eopt.ExportDestinationOptions = DFopt;
                MntRptdoc.Export(Eopt);
                //MntRptdoc.ExportToDisk(ExportFormatType.Excel, Server.MapPath("") + @"\CrystalReports\TandFMonthlyInvReport.xls");


                MntRptdoc = null; dc = null;
                dollardc = null; pounddc = null; inrdc = null;

                Report InvExl = new Report();
                //if(File.Exists(Server.MapPath("") + @"\CrystalReports\TandFMonthlyInvReport.xls"))
                //        File.Delete(Server.MapPath("") + @"\CrystalReports\TandFMonthlyInvReport.xls");
                //InvExl.InvRptExcelSetting(Server.MapPath("") + @"\CrystalReports\TandFMonthlyInvReport_" + Session["employeeid"].ToString() + ".xls", "false");
                ////////string spath = @"\\192.9.201.222\db\Monthly_Sales\TandFMonthlyInvReport_" + Session["employeeid"].ToString() + ".xls";
                ////////InvExl.InvRptExcelSetting(spath, "false");
                ////////InvExl = null;

                //if (ds.Tables[1].Rows.Count > 0)
                if(dt.Rows.Count>0)
                {
                    adgdispatchedlist.Caption = "<SPAN style='height:30px;font-weight:bold'>All Accounts : Invoice Detail Report(" + DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue + " To " + dtTo.ToShortDateString() + ")</span><br>";
                    adgdispatchedlist.Visible = true;
                    sSUMPages = ds.Tables[1].Compute("SUM(TOTALPAGES)", string.Empty).ToString();
                    //adgdispatchedlist.DataSource = ds.Tables[1];
                    adgdispatchedlist.DataSource = dt;
                    adgdispatchedlist.DataBind();
                    lblMessage.Visible = false;
                    Session["invDS"] = ds;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "No Records Found";
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "No Records Found";
            }
            //bib = null;
            //ds = null;
        }
        catch (Exception ex)
        {
            div_monthlyreport.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = ex.Message.ToString();
        }
        finally
        {
            oNode = null;

            dt = null;
            bib = null;
            ds = null;
        }
    }

    protected void adgdispatchedlist_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
       // string pdfname = "";
      //  string pdfnamedub = "";

        if (e.Item.DataItem != null)
        {
            try
            {
                if (DataBinder.Eval(e.Item.DataItem, "IDATE") != null)
                    e.Item.Cells[2].Text = ChangeDateFormat(DataBinder.Eval(e.Item.DataItem, "IDATE").ToString());
                string s = e.Item.ItemType.ToString();
                /* e.Item.Cells[7].Text = "0"; // INR
                 e.Item.Cells[8].Text = "0"; // euro
                 e.Item.Cells[9].Text = "0"; // dollar
                 e.Item.Cells[10].Text = "0"; // pound
                 biz_IB bib = new biz_IB();
                 XmlNode oNode = null;
                string slocationid = "i";
                string sInvValue = "0";
                string sCurrency = "";


                if (Session["location"] != null)
                    slocationid = Session["location"].ToString();
                 * */

                /*oNode = GetInvoiceXML(DataBinder.Eval(e.Item.DataItem, "IINNO").ToString(), DataBinder.Eval(e.Item.DataItem, "JOURCODE").ToString().Trim() + DataBinder.Eval(e.Item.DataItem, "IISSUENO").ToString().Trim(), slocationid);
                sInvValue = bib.GetAttributeValue(oNode, "INVOICEVALUE");
                sCurrency = bib.GetCurrencyType(oNode);
                
                bib = null;
                cellID = 7; //by default INR value
                if (sCurrency == "&pound;")
                    cellID = 10;
                else if (sCurrency == "&#36;" || sCurrency == "CAD")
                    cellID = 9;
                else if (sCurrency == "&euro;")
                    cellID = 8;
                //e.Item.Cells[cellID].Text = String.Format("{0:0.00}", Convert.ToDouble(sInvValue)).ToString() ;
                e.Item.Cells[cellID].Text = String.Format("{0:0.00}", Convert.ToDouble(sInvValue)).ToString();
                //e.Item.Cells[cellID].Attributes.Add("style", "mso-number-format:\"0.00\"");
                e.Item.Cells[cellID].Attributes.Add("class", "Style1");
                */
                //HyperLink Hl = (HyperLink)e.Item.Cells[13].FindControl("DublinPreviewHL");
                //HyperLink H2 = (HyperLink)e.Item.Cells[12].FindControl("IndiaPreviewHL");
                //if (DataBinder.Eval(e.Item.DataItem, "PAGEFORMAT").ToString().Length > 0)
                    //e.Item.Cells[5].Text = DataBinder.Eval(e.Item.DataItem, "PAGEFORMAT").ToString().Substring(0, 1);
                e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES").ToString();
                if (DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23433" || DataBinder.Eval(e.Item.DataItem, "INO").ToString() == "23612")//For  Psychology Press
                    e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES_NOCOVER").ToString();
                else if(DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23705" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23743" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23648" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23667" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23650" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23480" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23757" || DataBinder.Eval(e.Item.DataItem,"INO").ToString()=="23686" )//All r TandF
                    e.Item.Cells[6].Text="P:" + DataBinder.Eval(e.Item.DataItem,"EDITEDDISKTFNOPRELIMS").ToString();
                else if (DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "10040" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 26064 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) < 27115)
                    e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES_PSY").ToString();
                else if(DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "10040" &&  Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 27115)
                    e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES_PSY_NOPRELIMS").ToString();
                else if (DataBinder.Eval(e.Item.DataItem, "JOURNO").ToString().Trim() != "0" && DataBinder.Eval(e.Item.DataItem, "JOURNO") != DBNull.Value)////   Journal
                {
                    if (DataBinder.Eval(e.Item.DataItem, "journo").ToString() == "2683" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) == 26800)//For RAPL 47/1
                        e.Item.Cells[6].Text = "P:" + (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISK_RAPL")) + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"EDITEDDISKTF")) );
                    else if (DataBinder.Eval(e.Item.DataItem, "journo").ToString() == "2683")//For other RAPL except invoiceno 26800
                        e.Item.Cells[6].Text = "P:" + (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "EDITEDDISK_RAPL")) + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"EDITEDDISKTFNOPRELIMS")) );
                    else if (DataBinder.Eval(e.Item.DataItem, "ISARTICLEBASED").ToString() == "1")
                    {
                        e.Item.Cells[6].Text = "A:" + DataBinder.Eval(e.Item.DataItem, "NOOFARTICLES").ToString();
                    }
                    else
                    {
                        e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "TOTALPAGES").ToString();
                        if (DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "2556" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 26064 && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) < 27115)
                            e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTF").ToString();
                        else if(DataBinder.Eval(e.Item.DataItem, "custno1").ToString() == "2556" && Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IINNO")) > 27115)
                            e.Item.Cells[6].Text = "P:" + DataBinder.Eval(e.Item.DataItem, "EDITEDDISKTFNOPRELIMS").ToString();
                    }

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
            case ListItemType.Item:
            case ListItemType.AlternatingItem:
                //invTotal += Convert.ToDouble(e.Item.Cells[cellID].Text);
                invTotal += Convert.ToDouble(e.Item.Cells[10].Text);
                inrtal += Convert.ToDouble(e.Item.Cells[7].Text);
                eurotal += Convert.ToDouble(e.Item.Cells[8].Text);
                dollartal += Convert.ToDouble(e.Item.Cells[9].Text);
                formulacnt = formulacnt + 1;
                break;

            case ListItemType.Footer:
                //e.Item.Cells[cellID].Text = "<B>&pound;" + String.Format("{0:0.00}", invTotal).ToString()  + "</B>";
                e.Item.Cells[10].Text = "<B>&pound;" + String.Format("{0:0.00}", invTotal).ToString() + "</B>";
                e.Item.Cells[7].Text = "<B>INR" + String.Format("{0:0.00}", inrtal).ToString() + "</B>";
                e.Item.Cells[8].Text = "<B>€" + String.Format("{0:0.00}", eurotal).ToString() + "</B>";
                e.Item.Cells[9].Text = "<B>$" + String.Format("{0:0.00}", dollartal).ToString() + "</B>";
                //e.Item.Cells[cellID].Attributes.Add("align", "right");
                //e.Item.Cells[cellID].Attributes.Add("class", "CurrencyType");
                e.Item.Cells[10].Attributes.Add("align", "right");
                e.Item.Cells[10].Attributes.Add("class", "CurrencyType");
                e.Item.Cells[7].Attributes.Add("class", "InrType");
                e.Item.Cells[8].Attributes.Add("class", "EuroType");
                e.Item.Cells[9].Attributes.Add("class", "DollarType");
                formulacnt = formulacnt - 3;
                //e.Item.Cells[cellID].Attributes.Add("x:num", invTotal.ToString());
                //e.Item.Cells[cellID].Attributes.Add("x:fmla", "=SUM(L2:L" + formulacnt + ")");
                e.Item.Cells[10].Attributes.Add("x:num", invTotal.ToString());
                e.Item.Cells[10].Attributes.Add("x:fmla", "=SUM(L2:L" + formulacnt + ")");
                e.Item.Cells[7].Attributes.Add("x:num", inrtal.ToString());
                e.Item.Cells[7].Attributes.Add("x:fmla", "=SUM(H2:H" + formulacnt + ")");
                e.Item.Cells[8].Attributes.Add("x:num", eurotal.ToString());
                e.Item.Cells[8].Attributes.Add("x:fmla", "=SUM(I2:I" + formulacnt + ")");
                e.Item.Cells[9].Attributes.Add("x:num", dollartal.ToString());
                e.Item.Cells[9].Attributes.Add("x:fmla", "=SUM(J2:J" + formulacnt + ")");

                e.Item.Cells[4].Text = "<B>Total Invoiced for " + Convert.ToDateTime(DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue).Year.ToString() + "</B>";
                e.Item.Cells[4].ColumnSpan = 3;
                e.Item.Cells[5].Visible = false;
                e.Item.Cells[6].Visible = false;
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
            string sFileName = @"\\192.9.201.222\db\Monthly_Sales\TandFMonthlyInvReport_" + Session["employeeid"].ToString() + ".xls";
            Response.Buffer = true;
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(Server.MapPath("") + @"\CrystalReports\TandFMonthlyInvReport.xls"));

            FileStream fs;
            BinaryReader br;
            fs = new FileStream(sFileName, FileMode.Open);
            br = new BinaryReader(fs);
            Byte[] dataBytes = br.ReadBytes((int)(fs.Length - 1));
            Response.BinaryWrite(dataBytes);
            br.Close();
            fs.Close();
            Response.Flush();
        }
        catch (Exception ex)
        { }
        //Response.Close();

        
        //export to excel
       /* string sFileName = "Datapage " + Convert.ToDateTime(DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue).ToString("MMMMMMMMM") + " " + Convert.ToDateTime(DDMonthList.SelectedValue + "/1/" + DDYearList.SelectedValue).Year.ToString() + ".xls";
        Response.Buffer = true;
        Response.Clear();
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + sFileName);
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //adgdispatchedlist.Columns[13].Visible = false;
        //adgdispatchedlist.Columns[14].Visible = false;
        adgdispatchedlist.RenderControl(oHtmlTextWriter);

        //This Is For Name Space In Excel
        Response.Write("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"" +
               " xmlns=\"http://www.w3.org/TR/REC-html40\">");
        //This is for number format (ex- "0.00" and also apply Currency Type)
        Response.Write("<style>.style1 { mso-number-format:Fixed; }");
        Response.Write(".CurrencyType {font-weight: bold; mso-number-format:\"\\[$\\00A3-809\\]\\#\\,\\#\\#0\\.00\\;\\[Red\\]\\[$\\00A3-809\\]\\#\\,\\#\\#0\\.00\"; }");
        Response.Write(".InrType {font-weight: bold; mso-number-format:\"\\[$INR\\]\\\\ \\#\\,\\#\\#0\\.00\\;\\[Red\\]\\[$INR\\]\\\\ \\#\\,\\#\\#0\\.00\"; }");
        Response.Write(".EuroType {font-weight: bold; mso-number-format:\"\\[$€-1809\\]\\#\\,\\#\\#0\\.00\\;\\[Red\\]\\[$€-1809\\]\\#\\,\\#\\#0\\.00\"; } ");
        Response.Write(".DollarType {font-weight: bold; mso-number-format:\"\\0022$\\0022\\#\\,\\#\\#0\\.00\"; } </style>");

        Response.Write(oStringWriter.ToString());
        Response.End();*/
    }

    protected XmlNode GetInvoiceXML(string invoceno, string invoiceitem, string sLocation)
    {
        try
        {
            
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

