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
using System.Text;
using System.Diagnostics;
using System.IO;

/*
/// <summary>
/// Created by: Royson
/// Creation Date: Thursday, August 05, 2010
/// </summary>
 * */
public partial class outstanding_invoices : System.Web.UI.Page
{
    private static XmlDocument oXmlDoc = null;
    private static string sInvFile = ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString();
    private static DataTable dtable = null;
    private static string RepID = "", sCustID = "";
    protected static decimal tEuro = 0.00M, tDollar = 0.00M, tPound = 0.00M, tCAD = 0.00M;
    private Invoiced_IBSQL oIB = new Invoiced_IBSQL();
    private static string sPath = ConfigurationManager.AppSettings.GetValues("Outstanding_Export_Path").GetValue(0).ToString(), sFile = "";
    private static string sTemplatePath = ConfigurationManager.AppSettings.GetValues("Outstanding_Export_Template_Path").GetValue(0).ToString();
    private static DateTime dtNow = DateTime.Now;
    private static string sNowDate = "", sMonthEnd = "", sCustCurr = "";
    private static string[,] aCurrentTot = new string[5, 2];
    private static string[] aIndexCol = new string[6];
    private static bool ValidUser = false;

    string sCustName = "", sCustAdd11 = "", sCustAdd21 = "", sCustAdd31 = "", sCustAdd41 = "", sCustAdd51 = "",
                            sBankName1 = "", sBankAddr11 = "", sBankAddr21 = "", sBankAC1 = "", sBankACType1 = "",
                            sBankSortCode1 = "", sBankSwiftCode1 = "", sBIC1 = "", sIBAN1 = "", sPayCurr1 = "";
    DataSet dsCustRef1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    void popScreen()
    {
        dtable = new DataTable();
        RepID = "";
        sMonthEnd = DateTime.Parse(DateTime.Now.Month + "/01/" + DateTime.Now.Year).AddMonths(1).AddDays(-1).ToString("MM-dd-yyyy");
        tblReport.Visible = false;
        if (Session["customerName"] != null)
        {
            DataSet oDS = new DataSet();
            oDS = (DataSet)(Session["customerName"]);
            drpCustomer.DataSource = oDS;
            string myvalue = "10066";
            drpCustomer.SelectedValue = myvalue.ToString();
            drpCustomer.DataBind();
            oDS = null;
        }
        else throw new Exception("Session Expired!");
        if (DateTime.Now.Year.Equals(2010)) for (int o = 0; o <= 7; o++) drpMonths.Items.Remove(drpMonths.Items.FindByValue(o.ToString()));
        for (int j = 2010; j <= DateTime.Now.Year; j++) drpYears.Items.Insert(0, new ListItem(j.ToString(), j.ToString()));
        drpMonths.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        if (Request.QueryString["repid"] != null)
        {
            if (Request.QueryString["repid"].ToString() == "1")
            {
                lblTitle.Text = "Payment Received Report";
                RepID = "1";
                gvOutstandingInv.Columns[8].Visible = true;
                gvOutstandingInv.Columns[9].HeaderText = "Payment yet to be received";
                imgbtnSave.OnClientClick = "return vbconfirm('Payment yet to be received?');";
                ImageButton1.OnClientClick = "return vbconfirm('Payment yet to be received?');";
            }
        }
        else
        {
            this.hideDates();
            lblTitle.Text = "Outstanding Invoices Report";
            gvOutstandingInv.Columns[8].Visible = false;
            gvOutstandingInv.Columns[9].HeaderText = "Payment received";
            imgbtnSave.OnClientClick = "return vbconfirm('Confirm payment received?');";
            ImageButton1.OnClientClick = "return vbconfirm('Confirm payment received?');";
            drpCustomer.Items.Insert(drpCustomer.Items.IndexOf(new ListItem("Taylor and Francis                                ", "2556")) + 1
                , new ListItem("Taylor and Francis & Psychology Press", "255610040"));
            drpCustomer.Items.Insert(drpCustomer.Items.IndexOf(new ListItem("Informa                                           ", "10031")) + 1
                , new ListItem("Informa - BTN", "1003110004"));
            drpCustomer.Items.Insert(drpCustomer.Items.IndexOf(new ListItem("Informa                                           ", "10031")) + 1
                , new ListItem("Informa - BPI", "1003110119"));
        }
        if (Session["locationid"].ToString() == "1"//Dublin
            || Session["employeeteamid"].ToString() == "1"//Software
            || Session["employeeid"].ToString() == "1116" //Nisha
            || Session["employeeid"].ToString() == "986")
        {//Suresh
            gvOutstandingInv.Columns[9].Visible = true; ValidUser = true;
            imgbtnSave.Visible = true;
            ImageButton1.Visible = true;
            imgPayment.Visible = true;
            imgPayment1.Visible = true;
        }
        else
        {
            gvOutstandingInv.Columns[9].Visible = false;
            imgbtnSave.Visible = false;
            ImageButton1.Visible = false;
            imgPayment.Visible = false;
            imgPayment1.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        tEuro = 0.00M; tDollar = 0.00M; tPound = 0.00M; tCAD = 0.00M;
        sCustID = drpCustomer.SelectedItem.Value.Trim();
        sCustCurr = "";
        dtable = new DataTable();
        dtNow = DateTime.Now;
        sNowDate = dtNow.ToString("MM-dd-yyyy");
        sMonthEnd = DateTime.Parse(DateTime.Now.Month + "/01/" + DateTime.Now.Year).AddMonths(1).AddDays(-1).ToString("MM-dd-yyyy");
        if (sCustID != "10066" && sCustID != "255610040") gvOutstandingInv.Columns[2].Visible = false;
        else gvOutstandingInv.Columns[2].Visible = true;
        if (Session["customerName"] == null) throw new Exception("Session Expired!");
        DataSet ds = new DataSet();
        oXmlDoc = new XmlDocument();
        string sStartDate = "", sEndDate = "";
        try
        {
            if (RepID == "1")
            {
                imgPayment.Visible = false;
                imgPayment1.Visible = false;
                if (drpMonths.SelectedItem.Value == "0")
                {
                    sStartDate = "1/1/" + drpYears.SelectedItem.Value;
                    sEndDate = DateTime.Parse(sStartDate).AddMonths(12).AddDays(-1).ToString("M/d/yyyy");
                }
                else
                {
                    sStartDate = drpMonths.SelectedItem.Value + "/1/" + drpYears.SelectedItem.Value;
                    sEndDate = DateTime.Parse(sStartDate).AddMonths(1).AddDays(-1).ToString("M/d/yyyy");
                }
            }
            else
            {
                if (ValidUser && oIB.HasPayments(sCustID))
                {
                    imgPayment.Visible = true;
                    imgPayment1.Visible = true;
                }
                else
                {
                    imgPayment.Visible = false;
                    imgPayment1.Visible = false;
                }
            }
            oXmlDoc.Load(sInvFile);
            gvOutstandingInv.Columns[4].Visible = false;
            gvOutstandingInv.Columns[5].Visible = false;
            gvOutstandingInv.Columns[6].Visible = false;
            gvOutstandingInv.Columns[7].Visible = false;
            if (RepID == "")
            {
                ds = oIB.GetInvoicedJobs3Outstanding(Convert.ToInt32(drpCustomer.SelectedItem.Value), Convert.ToInt32(drpJobType.SelectedItem.Value), 0, 0);
            }
            else
            {
                ds = oIB.GetInvoicedJobs3PaymentReceived(Convert.ToInt32(drpCustomer.SelectedItem.Value), Convert.ToInt32(drpJobType.SelectedItem.Value), 0, 0, sStartDate, sEndDate);
            }
            tblReport.Visible = true;
            if (ds != null)
            {
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    dtable = ds.Tables[1].Copy();
                    dtable.Columns.Add("EUROVALUE", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("EUROVALUE_ACTUAL", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("CUMULATIVE", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("POUNDVALUE", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("USDVALUE", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("CADVALUE", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("INRVALUE", Type.GetType("System.Decimal"));
                    dtable.Columns.Add("TOTALPAGES_FINAL", Type.GetType("System.Int32"));
                    dtable.Columns.Add("MONTHNUMBER", Type.GetType("System.Int32"));
                    if (dtable.Columns.Contains("COLUMN1")) dtable.Columns["COLUMN1"].ColumnName = "OUTPRINTAREA";
                    else if (!dtable.Columns.Contains("OUTPRINTAREA")) dtable.Columns.Add("OUTPRINTAREA", Type.GetType("System.String"));
                    if (dtable.Columns.Contains("COLUMN2")) dtable.Columns["COLUMN2"].ColumnName = "BISBN";
                    else if (!dtable.Columns.Contains("BISBN")) dtable.Columns.Add("BISBN", Type.GetType("System.String"));
                    if (dtable.Columns.Contains("COLUMN3")) dtable.Columns["COLUMN3"].ColumnName = "PONUMBER";
                    else if (!dtable.Columns.Contains("PONUMBER")) dtable.Columns.Add("PONUMBER", Type.GetType("System.String"));
                    if (dtable.Columns.Contains("COLUMN4")) dtable.Columns["COLUMN4"].ColumnName = "PROJECTNUMBER";
                    else if (!dtable.Columns.Contains("PROJECTNUMBER")) dtable.Columns.Add("PROJECTNUMBER", Type.GetType("System.String"));
                    if (dtable.Columns.Contains("COLUMN5")) dtable.Columns["COLUMN5"].ColumnName = "CREDIT";
                    DataRow row = null; decimal conv = 0, cum = 0;
                    string sVal = "", sCurr = "", sCurRate = "1";
                    for (int x = 0; x < dtable.Rows.Count; x++)
                    {
                        sVal = ""; sCurr = ""; sCurRate = "1";
                        row = dtable.Rows[x]; row.BeginEdit();
                        if (row["CATEGORY"].ToString().Trim() != "Article")//Added by subbu on 11th July 2011 for wip article details
                        {
                            string sData = "";
                            if (row["CATEGORY"].ToString().Trim() == "Credit")
                            {
                                //row["IINNO"] = "0";
                                row["JOBTITLE"] = "Payment on Account";
                                sVal = row["CREDIT"].ToString().Trim();
                                switch (row["CURRNO"].ToString().Trim())
                                {
                                    case "3":
                                        sCurr = "stg";
                                        break;
                                    case "8":
                                        sCurr = "dollar";
                                        break;
                                    case "9":
                                        sCurr = "euro";
                                        break;
                                    case "11":
                                        sCurr = "cad";
                                        break;
                                }
                            }
                            else
                            {
                                sData = this.getInvoiceNode(row["IINNO"].ToString().Trim(),
                                    row["JOURCODE"].ToString().Trim(), row["IISSUENO"].ToString().Trim());
                            }
                            //[Disabled]
                            /*
                            if (row["TOTALPAGES_NOCOVER"].ToString().Trim() == "") row["TOTALPAGES_NOCOVER"] = "0";
                            if (row["TOTALPAGES"].ToString().Trim() == "") row["TOTALPAGES"] = "0";
                            //Added extra field for noprelims by subbu
                            if (row["INO"].ToString().Trim() == "23433" || row["INO"].ToString().Trim() == "23612")//For  Psychology Press
                                row["TOTALPAGES_FINAL"] = row["TOTALPAGES_NOCOVER"].ToString().Trim();

                            else if (row["INO"].ToString().Trim() == "23705" || row["INO"].ToString().Trim() == "23743" || row["INO"].ToString().Trim() == "23648" || row["INO"].ToString().Trim() == "23667" || row["INO"].ToString().Trim() == "23650" || row["INO"].ToString().Trim() == "23480" || row["INO"].ToString().Trim() == "23757" || row["INO"].ToString().Trim() == "23686")//All r TandF
                                row["TOTALPAGES_FINAL"] = row["EDITEDDISKTFNOPRELIMS"].ToString().Trim();

                            else if (row["custno1"].ToString().Trim() == "10040" && Convert.ToInt32(row["IINNO"].ToString().Trim()) > 26064 && Convert.ToInt32(row["IINNO"].ToString().Trim()) < 27115)
                                row["TOTALPAGES_FINAL"] = row["TOTALPAGES_PSY"].ToString().Trim();

                            else if (row["custno1"].ToString().Trim() == "10040" && Convert.ToInt32(row["IINNO"].ToString().Trim()) > 27115)
                                row["TOTALPAGES_FINAL"] = row["TOTALPAGES_PSY_NOPRELIMS"].ToString().Trim();
                            else if (row["IINNO"].ToString().Trim() == "26800")//For RAPL 47/1
                                row["TOTALPAGES_FINAL"] = int.Parse(row["EDITEDDISK_RAPL"].ToString().Trim()) + int.Parse(row["EDITEDDISKTF"].ToString().Trim());
                            else if (row["journo"].ToString().Trim() == "2683")//For RAPL
                                row["TOTALPAGES_FINAL"] = int.Parse(row["EDITEDDISK_RAPL"].ToString().Trim()) + int.Parse(row["EDITEDDISKTFNOPRELIMS"].ToString().Trim());
                            else if (row["CUSTNO1"].ToString().Trim() == "2556" &&
                           Convert.ToInt32(row["IINNO"].ToString().Trim()) > 26064 && Convert.ToInt32(row["IINNO"].ToString().Trim()) < 27115)
                                row["TOTALPAGES_FINAL"] = row["EDITEDDISKTF"].ToString().Trim();
                            else if (row["custno1"].ToString().Trim() == "2556" && Convert.ToInt32(row["IINNO"].ToString().Trim()) > 27115)
                                row["TOTALPAGES_FINAL"] = row["EDITEDDISKTFNOPRELIMS"].ToString().Trim();
                            else row["TOTALPAGES_FINAL"] = row["TOTALPAGES"].ToString().Trim();
                            */
                            if (sData.LastIndexOf("INVOICEVALUE=\"") != -1)
                            {
                                sVal = sData.Substring(sData.LastIndexOf("INVOICEVALUE=\"") + "INVOICEVALUE=\"".Length);
                                sVal = sVal.Substring(0, sVal.IndexOf("\""));
                            }
                            if (sData.LastIndexOf("INVOICECURRENCY=\"") != -1)
                            {
                                sCurr = sData.Substring(sData.LastIndexOf("INVOICECURRENCY=\"") + "INVOICECURRENCY=\"".Length);
                                sCurr = sCurr.Substring(0, sCurr.IndexOf("\""));
                            }
                            if (sData.LastIndexOf("CURRENCYRATE=\"") != -1)
                            {
                                sCurRate = sData.Substring(sData.LastIndexOf("CURRENCYRATE=\"") + "CURRENCYRATE=\"".Length);
                                sCurRate = sCurRate.Substring(0, sCurRate.IndexOf("\""));
                            }
                            //row["EUROVALUE"] = 0;
                            //row["EUROVALUE_ACTUAL"] = 0;
                            //row["POUNDVALUE"] = 0;
                            //row["USDVALUE"] = 0;
                            //row["CADVALUE"] = 0;
                            //row["INRVALUE"] = 0;                    
                            sVal = string.Format("{0:0.00}", sVal);
                            if (sCurr != "")
                            {
                                switch (sCurr.ToLower())
                                {
                                    case "stg":
                                        row["POUNDVALUE"] = sVal;
                                        tPound = tPound + decimal.Parse(sVal);
                                        gvOutstandingInv.Columns[5].Visible = true;
                                        sCustCurr = "£";
                                        break;
                                    case "dollar":
                                        row["USDVALUE"] = sVal;
                                        tDollar = tDollar + decimal.Parse(sVal);
                                        gvOutstandingInv.Columns[6].Visible = true;
                                        sCustCurr = "$";
                                        break;
                                    case "euro":
                                        row["EUROVALUE"] = sVal;
                                        tEuro = tEuro + decimal.Parse(sVal);
                                        row["EUROVALUE_ACTUAL"] = sVal;
                                        gvOutstandingInv.Columns[4].Visible = true;
                                        sCustCurr = "€";
                                        break;
                                    case "cad":
                                        row["CADVALUE"] = sVal;
                                        tCAD = tCAD + decimal.Parse(sVal);
                                        gvOutstandingInv.Columns[7].Visible = true;
                                        sCustCurr = "CAD$";
                                        break;
                                }
                                conv = Math.Round(decimal.Parse(sVal) * decimal.Parse(sCurRate));
                                if (Convert.ToDateTime(row["IDATE"]).Year >= 2012 && (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                                    row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland"))
                                {
                                    conv = Math.Round((conv / 123) * 100);
                                    row["EUROVALUE"] = conv;
                                }
                                else if (Convert.ToDateTime(row["IDATE"]).Year >= 2010 && (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                                    row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland"))
                                {
                                    conv = Math.Round((conv / 121) * 100);
                                    row["EUROVALUE"] = conv;
                                }
                                else if (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                                    row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland")
                                {
                                    conv = Math.Round((conv / Convert.ToDecimal(121.5)) * 100);
                                    row["EUROVALUE"] = conv;
                                }
                                else row["EUROVALUE"] = conv;
                                cum = conv + cum;
                                row["CUMULATIVE"] = cum;
                            }
                            else row["CUMULATIVE"] = cum;
                            /*
                            row["MONTHNUMBER"] = DateTime.Parse(row["IDATE"].ToString().Trim()).Month.ToString();
                            string sJobTitle = row["JOBTITLE"].ToString().Trim().ToLower();
                            if (sJobTitle.Contains("taxpoint") && (sJobTitle.Contains("issue") || sJobTitle.Contains("online")))
                                row["GROUPBY"] = "Software";
                            else if (sJobTitle.Contains("gaa") && (sJobTitle.Contains("issue") || sJobTitle.Contains("web") || sJobTitle.Contains("online")))
                                row["GROUPBY"] = "Software";
                             * */
                            row.EndEdit();
                            row.AcceptChanges();
                        }
                        else
                        {
                            //sivaraj testing
                        }
                    }
                    dtable.AcceptChanges();
                    ds.Tables[1].Rows.Clear();
                    ds.Tables[1].Merge(dtable);
                    //Session["dsYTD"] = ds;
                    gvOutstandingInv.DataSource = dtable;
                    gvOutstandingInv.DataBind();
                }
                else
                {
                    gvOutstandingInv.DataBind();
                }
                // this.GenerateExcel();
            }
            else
            {
                gvOutstandingInv.DataBind();
            }
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
        finally { oXmlDoc = null; }
    }
    public void Alert(string sMessage)
    {
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        sMessage = sMessage.Replace("'", "\'");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    private string getInvoiceNode(string sInvNo, string sInvItem, string sIssueNo)
    {
        try
        {
            XmlNode oNode = null;
            if (sInvItem.Length > 50) sInvItem = sInvItem.Remove(50);
            oNode = oXmlDoc.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICENO='" + sInvNo + "']");
            if (oNode == null) oNode = oXmlDoc.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM='" + sInvItem + sIssueNo + "']");
            return oNode.OuterXml;
        }
        catch
        { return ""; }
    }
    protected void gvOutstandingInv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            if (((HiddenField)e.Row.FindControl("hfgvJobCategory")).Value.Trim().ToLower() == "credit")
            {
                ((Label)e.Row.FindControl("lblgvInvoiceNo")).Text = ""; e.Row.ForeColor = System.Drawing.Color.MediumBlue;
            }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList list = new ArrayList();
        try
        {
            foreach (GridViewRow ro in gvOutstandingInv.Rows)
            {
                if (((CheckBox)ro.FindControl("chkgvPayment")).Checked)
                {
                    list.Add(new ListItem(((HiddenField)ro.FindControl("hfgvJobCategory")).Value.Trim().ToLower(),
                        ((HiddenField)ro.FindControl("hfgvJobID")).Value.Trim()));
                }
            }
            if (list.Count > 0)
            {
                if (RepID == "")
                {
                    if (oIB.ApprovePayments(
                        list, Session["fullname"].ToString().Trim()
                        ))
                    {
                        btnSubmit_Click(null, null);
                        Alert("Successfully Saved");
                    }
                }
                else
                {
                    if (oIB.CancelPayments(
                        list, Session["fullname"].ToString().Trim()
                        ))
                    {
                        btnSubmit_Click(null, null);
                        Alert("Successfully Saved");
                    }
                }
            }
            else Alert("Please select a record");
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
        finally { list = null; }
    }
    private void hideDates()
    {
        tdMonth1.Visible = false;
        tdMonth2.Visible = false;
        tdYear1.Visible = false;
        tdYear2.Visible = false;
    }

    #region Old Excel Export
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        if (tblReport.Visible)
        {
            string sFileName = "";
            int itemp = 0;
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1' style='font-family:Calibri;font-size:11pt'");
            // 03nov2010 Disabled for fixed header excel export
            //if (sCustID == "2556" && drpJobType.SelectedItem.Value == "1"){
            //    sbData.Append("<tr valign='top'><td><b>Date of invoice</b></td><td><b>Invoice number</b></td><td><b>CATS ID number</b></td><td><b>Journal acronym</b></td><td><b>Volume & Issue</b></td><td align='center'><b>Production editor</b></td><td><b>Amount in &pound;GBP</b></td></tr>");
            //    foreach (DataRow r in dtable.Rows){
            //        if (r["INVTYPE"].ToString().Trim() == "INV"){
            //            sbData.Append("<tr valign='top'>");
            //            sbData.Append("<td align='right'>" + DateTime.Parse(r["IDATE"].ToString()).ToString("MM/dd/yyyy") + "</td>");
            //            sbData.Append("<td align='right'>" + r["IINNO"] + "</td>");
            //            sbData.Append("<td align='left'>&nbsp;</td>");
            //            sbData.Append("<td align='left'>" + r["JOURCODE"] + "</td>");
            //            sbData.Append("<td align='left'>&nbsp;" + r["IISSUENO"] + "&nbsp;</td>");
            //            sbData.Append("<td align='left'>" + r["INVDNAME"] + "</td>");
            //            sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["POUNDVALUE"] + "</td>");
            //            sbData.Append("</tr>");
            //        }
            //        else itemp++;
            //    }
            //    sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td align='center'></td><td></td></tr>");
            //    //sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td></td><td align='right'><b>=char(163) &\"\"& sum(G2:G" + (dtable.Rows.Count + 2) + ")</b></td></tr>");
            //    sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td></td><td align='right' style=\"mso-number-format:&pound;\\##\\,\\##\\##0\\.00\"><b>=sum(G2:G" + ((dtable.Rows.Count + 2) - itemp) + ")</b></td></tr>");
            //    if (RepID == "") sFileName = "Outstanding Invoices " + sNowDate;
            //    else sFileName = "Payments Received " + sNowDate;
            //}
            //else
            //{
            sbData.Append("<tr valign='top'><td><b>Date of invoice</b></td><td><b>Invoice number</b></td><td><b>Customer</b></td><td><b>Job title</b></td><td><b>Category</b></td><td align='center'><b>Production editor</b></td><td align='center'><b>&euro;</b></td><td align='center'><b>&pound;</b></td><td align='center'><b>$</b></td><td align='center'><b>CAD$</b></td><td align='center'><b>Date of payment</b></td></tr>");
            if (Session["locationid"].ToString() == "1"//Dublin
            || Session["employeeteamid"].ToString() == "1"//Software
            || Session["employeeid"].ToString() == "1116" //Nisha
            || Session["employeeid"].ToString() == "986")
            {
                foreach (DataRow r in dtable.Rows)
                {
                    if (r["EUROVALUE"].ToString() != "0")
                    {
                        if (r["INVTYPE"].ToString().Trim() == "INV")
                            sbData.Append("<tr valign='top'>");
                        else sbData.Append("<tr valign='top' style='color:red;'>");
                        sbData.Append("<td align='right'>" + DateTime.Parse(r["IDATE"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                        if (r["INVTYPE"].ToString().Trim() == "INV") sbData.Append("<td align='right'>" + r["IINNO"] + "</td>");
                        else sbData.Append("<td align='right'>&nbsp;</td>");
                        sbData.Append("<td align='left'>" + r["CNAME"] + "</td>");
                        sbData.Append("<td align='left'>" + r["JOBTITLE"] + "</td>");
                        sbData.Append("<td align='left'>" + r["CATEGORY"] + "</td>");
                        sbData.Append("<td align='left'>" + r["INVDNAME"] + "</td>");
                        sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["EUROVALUE_ACTUAL"] + "</td>");
                        sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["POUNDVALUE"] + "</td>");
                        sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["USDVALUE"] + "</td>");
                        sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["CADVALUE"] + "</td>");
                        if (r["PAYMENT_DATE"].ToString().Trim() == "") sbData.Append("<td align='right'>&nbsp;</td>");
                        else sbData.Append("<td align='right'>" + DateTime.Parse(r["PAYMENT_DATE"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                        sbData.Append("</tr>");
                    }
                }
            }
            else

                foreach (DataRow r in dtable.Rows)
                {
                    if (r["INVTYPE"].ToString().Trim() == "INV")
                        sbData.Append("<tr valign='top'>");
                    else sbData.Append("<tr valign='top' style='color:red;'>");
                    sbData.Append("<td align='right'>" + DateTime.Parse(r["IDATE"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                    if (r["INVTYPE"].ToString().Trim() == "INV") sbData.Append("<td align='right'>" + r["IINNO"] + "</td>");
                    else sbData.Append("<td align='right'>&nbsp;</td>");
                    sbData.Append("<td align='left'>" + r["CNAME"] + "</td>");
                    sbData.Append("<td align='left'>" + r["JOBTITLE"] + "</td>");
                    sbData.Append("<td align='left'>" + r["CATEGORY"] + "</td>");
                    sbData.Append("<td align='left'>" + r["INVDNAME"] + "</td>");
                    sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["EUROVALUE_ACTUAL"] + "</td>");
                    sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["POUNDVALUE"] + "</td>");
                    sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["USDVALUE"] + "</td>");
                    sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["CADVALUE"] + "</td>");
                    if (r["PAYMENT_DATE"].ToString().Trim() == "") sbData.Append("<td align='right'>&nbsp;</td>");
                    else sbData.Append("<td align='right'>" + DateTime.Parse(r["PAYMENT_DATE"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                    sbData.Append("</tr>");
                }

            sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td><td align='center'></td></tr>");
            sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td align='right'><b>Total: </b></td><td align='right' style=\"mso-number-format:&euro;\\##\\,\\##\\##0\\.00\"><b>=sum(G2:G" + (dtable.Rows.Count + 2) + ")</b></td><td align='right' style=\"mso-number-format:&pound;\\##\\,\\##\\##0\\.00\"><b>=sum(H2:H" + (dtable.Rows.Count + 2) + ")</b></td><td align='right' style=\"mso-number-format:$\\##\\,\\##\\##0\\.00\"><b>=sum(I2:I" + (dtable.Rows.Count + 2) + ")</b></td><td align='right' style=\"mso-number-format:$\\##\\,\\##\\##0\\.00\"><b>=sum(J2:J" + (dtable.Rows.Count + 2) + ")</b></td><td align='center'></td></tr>");
            if (RepID == "") sFileName = "Outstanding Invoices " + sNowDate;
            else sFileName = "Payments Received " + sNowDate;
            //}
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", sFileName + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    #endregion

    protected void cmd_Excel_Export_Click1(object sender, ImageClickEventArgs e)
    {
        int rowIndex = 0;
        aIndexCol[0] = ""; aIndexCol[1] = ""; aIndexCol[2] = ""; aIndexCol[3] = ""; aIndexCol[4] = ""; aIndexCol[5] = "";
        sFile = "Outstanding-" + Session["employeeid"].ToString().Trim() + "-" + System.Guid.NewGuid().ToString();
        //if (sCustID == "10066" || RepID != "" || (sCustID == "2556" && drpJobType.SelectedItem.Value == "1")){
        if (sCustID == "10066" || RepID != "")
        {
            cmd_Excel_Export_Click(null, null);
            return;
        }
        else if (sCustID == "2556")
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            //sbData.Append("<tr valign='top'><td colspan='6' align='center'><h4>Launch OverAll Report</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Date of invoice</b></td><td bgcolor='silver'><b>Invoice number</b></td><td bgcolor='silver'><b>Journal acronym</b></td><td bgcolor='silver'><b>CATS ID number</b></td><td bgcolor='silver'><b>Production Editor</b></td><td bgcolor='silver'><b>Amount in £GBP</b></td></tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td align='left'>" + DateTime.Parse(r["IDATE"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                sbData.Append("<td align='left'>" + r["IINNO"] + "</td>");
                sbData.Append("<td align='left'>" + r["JOURCODE"] + "</td>");
                sbData.Append("<td align='left'> " + "&nbsp;" + r["IISSUENO"].ToString() + " </td>");
                sbData.Append("<td align='left'>" + r["INVDNAME"] + "</td>");
                sbData.Append("<td align='right' style=\"mso-number-format:0\\.00\">" + r["POUNDVALUE"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
            sbData.Append("<tr valign='top'><td></td><td></td><td></td><td></td><td align='right'><b>Total: </b></td><td align='right' style=\"mso-number-format:&pound;\\##\\,\\##\\##0\\.00\"><b>=sum(F2:F" + (dtable.Rows.Count + 2) + ")</b></td></tr>");
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Outstanding_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            //Response.ContentEncoding = Encoding.Unicode;
            //Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
        }
        else
        {
            dsCustRef1 = new CustomerBase().getCustomerReferences("", drpCustomer.SelectedItem.Value.Trim());
            if (dsCustRef1.Tables[0].Rows.Count > 0)
            {
                sCustName = dsCustRef1.Tables[0].Rows[0]["cust_name"].ToString().Trim();
                sCustAdd11 = dsCustRef1.Tables[0].Rows[0]["address1"].ToString().Trim();
                sCustAdd21 = dsCustRef1.Tables[0].Rows[0]["address2"].ToString().Trim();
                sCustAdd31 = dsCustRef1.Tables[0].Rows[0]["address3"].ToString().Trim();
                sCustAdd41 = dsCustRef1.Tables[0].Rows[0]["address4"].ToString().Trim();
                sCustAdd51 = dsCustRef1.Tables[0].Rows[0]["address5"].ToString().Trim();
                sBankName1 = dsCustRef1.Tables[0].Rows[0]["bank_name"].ToString().Trim();
                sBankAddr11 = dsCustRef1.Tables[0].Rows[0]["bank_address1"].ToString().Trim();
                sBankAddr21 = dsCustRef1.Tables[0].Rows[0]["bank_address2"].ToString().Trim();
                sBankAC1 = dsCustRef1.Tables[0].Rows[0]["bank_acno"].ToString().Trim();
                sBankACType1 = dsCustRef1.Tables[0].Rows[0]["bank_ac_type"].ToString().Trim();
                sBankSortCode1 = dsCustRef1.Tables[0].Rows[0]["bank_sort_code"].ToString().Trim();
                sBankSwiftCode1 = dsCustRef1.Tables[0].Rows[0]["bank_swift_code"].ToString().Trim();
                sBIC1 = dsCustRef1.Tables[0].Rows[0]["BIC"].ToString().Trim();
                sIBAN1 = dsCustRef1.Tables[0].Rows[0]["IBAN"].ToString().Trim();
                sPayCurr1 = dsCustRef1.Tables[0].Rows[0]["pay_currency"].ToString().Trim();
            }

            StringBuilder strVal = new StringBuilder();
            strVal.Append("<table width='100%' align='Center' style='border:1px solid green;'>");
            rowIndex = 1;

            strVal.Append("<tr><td colspan='11' rowspan='2' style='font-size:16px;border:1px solid green;' align='center'><b>STATEMENT OF ACCOUNT</b></td></tr>");

            strVal.Append("<tr></tr>");

            strVal.Append("<tr>");
            strVal.Append("<td style='border:1px solid green; border-bottom:0px;' align='left'>Customer</td><td style='border:1px solid green;' align='left'>" + sCustName + "</td>");
            strVal.Append("<td></td><td></td><td></td><td></td><td></td><td colspan='2' style='border:1px solid green;' align='left'><b>Datapage International Ltd</b></td>");
            strVal.Append("</tr>");

            strVal.Append("<tr>");
            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd11 + "</td>");
            strVal.Append("<td></td><td></td><td></td><td></td><td></td><td colspan='2' style='border:1px solid green;' align='left'><b>Docklands Innovation Park</b></td>");
            strVal.Append("</tr>");

            strVal.Append("<tr>");
            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd21 + "</td>");
            strVal.Append("<td></td><td></td><td></td><td></td><td></td><td colspan='2' style='border:1px solid green;' align='left'><b>128-130 East Wall Road</b></td>");
            strVal.Append("</tr>");

            strVal.Append("<tr>");
            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd31 + "</td>");
            strVal.Append("<td></td><td></td><td></td><td></td><td></td><td colspan='2' style='border:1px solid green;' align='left'><b>Dublin 3</b></td>");
            strVal.Append("</tr>");

            strVal.Append("<tr>");
            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd41 + "</td>");
            strVal.Append("<td></td><td></td><td></td><td></td><td></td><td colspan='2' style='border:1px solid green;' align='left'><b>Phone:00353 1 4960628</b></td>");
            strVal.Append("</tr>");

            strVal.Append("<tr>");
            strVal.Append("<td style='border:1px solid green; border-bottom:1px;border-top:0px; solid green;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd51 + "</td>");
            strVal.Append("<td></td><td></td><td></td><td></td><td></td><td colspan='2' style='border:1px solid green;' align='left'><b>E-Mail: accounts@datapage.org</b></td>");
            strVal.Append("</tr>");

            strVal.Append("<tr></tr>");

            strVal.Append("<tr><td style='font-weight:bold;border:1px solid green;'>Invoice Date</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Type</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Job Title</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Production Editor</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Invoice No.</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>ISBN</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>PO Number</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Project Number</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Debit</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Credit</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Outstanding</td>");
            strVal.Append("</tr>");
            double dblInvDebitTotal = 0;
            //double dblInvCreditTotal = 0;
            //double dblInvStgTotal = 0;
            rowIndex = 11;
            foreach (DataRow r in dtable.Rows)
            {

                string strINNO = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                strVal.Append("<tr><td style='border:1px solid green;' align='left'>" + r["IDATE"].ToString().Substring(1, 10).Trim() + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left' >" + r["INVTYPE"].ToString().Trim() + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["JOBTITLE"].ToString().Trim() + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["INVDNAME"].ToString().Trim() + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left'>" + strINNO + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["BISBN"].ToString().Trim() + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["PONUMBER"].ToString().Trim() + "</td>");
                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["PROJECTNUMBER"].ToString().Trim() + "</td>");

                if (r["EUROVALUE_ACTUAL"].ToString() != "")
                {
                    if (r["INVTYPE"].ToString().Trim() == "INV")
                    {
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["EUROVALUE_ACTUAL"].ToString().Trim() + "</td>");
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        dblInvDebitTotal = dblInvDebitTotal + Convert.ToDouble(r["EUROVALUE_ACTUAL"]);
                    }
                    else
                    {
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["EUROVALUE_ACTUAL"].ToString().Trim() + "</td>");
                    }
                    strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["EUROVALUE_ACTUAL"].ToString().Trim() + "</td>");
                }
                else if (r["CADVALUE"].ToString() != "")
                {
                    if (r["INVTYPE"].ToString().Trim() == "INV")
                    {
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["CADVALUE"].ToString().Trim() + "</td>");
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        dblInvDebitTotal = dblInvDebitTotal + Convert.ToDouble(r["CADVALUE"]);
                    }
                    else
                    {
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["CADVALUE"].ToString().Trim() + "</td>");
                    }
                    strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["CADVALUE"].ToString().Trim() + "</td>");
                }
                else if (r["USDVALUE"].ToString() != "")
                {
                    if (r["INVTYPE"].ToString().Trim() == "INV")
                    {
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["USDVALUE"].ToString().Trim() + "</td>");
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        dblInvDebitTotal = dblInvDebitTotal + Convert.ToDouble(r["USDVALUE"]);
                    }
                    else
                    {
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["USDVALUE"].ToString().Trim() + "</td>");
                    }
                    strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["USDVALUE"].ToString().Trim() + "</td>");
                }
                else if (r["POUNDVALUE"].ToString() != "")
                {
                    if (r["INVTYPE"].ToString().Trim() == "INV")
                    {
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        dblInvDebitTotal = dblInvDebitTotal + Convert.ToDouble(r["POUNDVALUE"]);
                    }
                    else
                    {
                        strVal.Append("<td style='border:1px solid green;'></td>");
                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                    }
                    strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                }
                else
                {
                    strVal.Append("<td style='border:1px solid green;'></td>");
                    strVal.Append("<td style='border:1px solid green;'></td>");
                    strVal.Append("<td style='border:1px solid green;'></td>");
                }

                this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                rowIndex++;

            }

            if (sCustCurr == "€")
                sCustCurr = "&euro;";
            else if (sCustCurr == "£")
                sCustCurr = "&pound;";
            else
                sCustCurr = "$";

            strVal.Append("<tr>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;' Colspan='10'; align='right'>Total :</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;' style=\"mso-number-format:" + sCustCurr + "\\##\\,\\##\\##0\\.00\";' align='right'>" + dblInvDebitTotal.ToString() + "</td>");
            strVal.Append("</tr>");

            rowIndex++;
            string strCurrent = "", str30Days = "", str60Days = "", str90Days = "", str120Days = "";
             strCurrent = this.getSelectionTotal(aIndexCol[0], "K");
             str30Days = this.getSelectionTotal(aIndexCol[1], "K");
             str60Days = this.getSelectionTotal(aIndexCol[2], "K");
             str90Days = this.getSelectionTotal(aIndexCol[3], "K");
             str120Days = this.getSelectionTotal(aIndexCol[4], "K");

            strVal.Append("<tr></tr>");
            rowIndex++;
            strVal.Append("<tr></tr>");
            rowIndex++;
            strVal.Append("<tr></tr>");
            rowIndex++;




            strVal.Append("<tr><td style='font-weight:bold;border:1px solid green;'>Description</td>");
            strVal.Append("<td style='font-weight:bold;border:1px solid green;'  align='center'>Value(" + sCustCurr + ")</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td colspan='3' style='border:1px solid green;' align='center'>Electronic Banking Details</td>");
            strVal.Append("</tr>");
            rowIndex++;

            int intTotalStart = rowIndex;
            strVal.Append("<tr><td style='border:1px solid green;'>Current</td><td style='border:1px solid green;' style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' align='right'>" + strCurrent + "</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;'>Bank Name:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sBankName1 + "</td>");
            strVal.Append("</tr>");
            rowIndex++;

            strVal.Append("<tr><td style='border:1px solid green;'>Over 30 Days</td><td style='bold;border:1px solid green;' style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' align='right'>" + str30Days + "</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green; border-bottom:0px;'>Bank Address:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sBankAddr11 + "</td>");
            strVal.Append("</tr>");
            rowIndex++;

            strVal.Append("<tr><td style='border:1px solid green;'>Over 60 Days</td><td style='border:1px solid green;' style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' align='right'>" + str60Days + "</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;border-top:0px;'></td><td colspan='2' style='border:1px solid green;' align='left'>" + sBankAddr21 + "</td>");
            strVal.Append("</tr>");
            rowIndex++;

            strVal.Append("<tr><td style='border:1px solid green;'>Over 90 Days</td><td style='border:1px solid green;' style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' align='right'>" + str90Days + "</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;'>Bank Account No.:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sBankAC1 + "</td>");
            strVal.Append("</tr>");
            rowIndex++;

            strVal.Append("<tr><td style='border:1px solid green;'>Over 120 Days</td><td style='border:1px solid green;' style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' align='right'>" + str120Days + "</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;'>Account Type:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sBankACType1 + "</td>");
            strVal.Append("</tr>");
            int intTotalEnd = rowIndex;
            rowIndex++;


            strVal.Append("<tr><td style='font-weight:bold;border:1px solid green;'>Total :</td><td style='font-weight:bold;border:1px solid green;' style=\"mso-number-format:" + sCustCurr + "\\##\\,\\##\\##0\\.00\";' align='right'>=SUM(B" + intTotalStart.ToString() + ":B" + intTotalEnd.ToString() + ")</td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;'>Bank Sort Code:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sBankSortCode1 + "</td>");
            strVal.Append("</tr>");

            strVal.Append("<tr><td></td><td></td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;'>BIC:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sBIC1 + "</td>");
            strVal.Append("</tr>");

            strVal.Append("<tr><td></td><td></td>");
            strVal.Append("<td></td>");
            strVal.Append("<td style='border:1px solid green;'>IBAN:</td><td colspan='2' style='border:1px solid green;' align='left'>" + sIBAN1 + "</td>");
            strVal.Append("</tr>");


            strVal.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", sFile + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(strVal.ToString());
            Response.End();
        }
    }
    // Royson 20aug2010 - for better excel presentation
    private void GenerateExcel()
    {
        //if (sCustID == "10066" || RepID != "" || (sCustID == "2556" && drpJobType.SelectedItem.Value == "1")){
        if (sCustID == "10066" || RepID != "")
        {
            return;
        }
        aCurrentTot = new string[5, 2];
        aIndexCol[0] = ""; aIndexCol[1] = ""; aIndexCol[2] = ""; aIndexCol[3] = ""; aIndexCol[4] = ""; aIndexCol[5] = "";
        sFile = "Outstanding-" + Session["employeeid"].ToString().Trim() + "-" + System.Guid.NewGuid().ToString() + ".xls";
        if (tblReport.Visible)
        {
            Excel.Application xlApp = null;
            Excel.Workbooks xlWorkBooks = null;
            Excel.Workbook xlWorkBook = null;
            Excel.Worksheet xlWorkSheet = null;
            Excel.Range range = null;
            Excel.Range row = null;
            Excel.Range top = null;
            object oMissing = System.Reflection.Missing.Value;
            try
            {
                xlApp = new Excel.ApplicationClass();
                xlApp.Visible = false;
                xlWorkBooks = xlApp.Workbooks;
                string sCust = drpCustomer.SelectedItem.Value.Trim();
                string sExcelTemp = "";
                if (sCust == "2556" && drpJobType.SelectedItem.Value == "1") sExcelTemp = sTemplatePath + "2556_1.xlt";
                else sExcelTemp = sTemplatePath + drpCustomer.SelectedItem.Value + ".xlt";
                if (!File.Exists(sExcelTemp)) sExcelTemp = sTemplatePath + "default.xlt";
                xlWorkBook = xlApp.Workbooks.Add(sExcelTemp);
                xlWorkBook.SaveAs(sPath + sFile, Excel.XlFileFormat.xlWorkbookNormal, oMissing,
                    oMissing, oMissing, oMissing, Excel.XlSaveAsAccessMode.xlExclusive, oMissing,
                    oMissing, oMissing, oMissing, oMissing);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int rowIndex = 0;
                if (sCust == "1003110004" || sCust == "1003110119")
                    sCust = "10031";
                switch (sCust)
                {
                    case "2556": //T&F(Informa UK)
                        if (drpJobType.SelectedItem.Value == "1")
                        {
                            rowIndex = 2;
                            range = xlWorkSheet.Cells;

                            foreach (DataRow r in dtable.Rows)
                            {
                                range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                                range[rowIndex, 2] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                                //range[rowIndex, 3] = "";
                                range[rowIndex, 3] = r["JOURCODE"].ToString();
                                range[rowIndex, 4] = " " + r["IISSUENO"].ToString().Trim() + " ";
                                range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                                range[rowIndex, 6] = r["POUNDVALUE"].ToString().Trim();
                                //this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                                rowIndex++;
                                row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                                row.Select();
                                row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            }


                        }
                        else
                        {
                            //rowIndex = 19;
                            //range = xlWorkSheet.Cells;
                            //foreach (DataRow r in dtable.Rows){
                            //    //if (!htMonths.ContainsKey(DateTime.Parse(r["IDATE"].ToString().Trim()).Month + "-"
                            //    //    + DateTime.Parse(r["IDATE"].ToString().Trim()).Year)){
                            //    //        htMonths.Add(DateTime.Parse(r["IDATE"].ToString().Trim()).Month + "-"
                            //    //        + DateTime.Parse(r["IDATE"].ToString().Trim()).Year, "");
                            //    //}
                            //    range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            //    range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            //    range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            //    range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            //    range[rowIndex, 6] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            //    if (r["INVTYPE"].ToString().Trim() == "INV")
                            //        range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                            //    else range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                            //    range[rowIndex, 10] = r["POUNDVALUE"].ToString().Trim();
                            //    //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            //    this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            //    rowIndex++;
                            //    row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            //    row.Select();
                            //    row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            //}
                            //range[14, 2] = dtNow.ToString();
                            //range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[0], "J");
                            //range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[1], "J");
                            //range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[2], "J");
                            //range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[3], "J");
                            //range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[4], "J");
                            ////range[rowIndex + 13, 3] = this.getSelectionTotal(aIndexCol[5], "J");

                            dsCustRef1 = new CustomerBase().getCustomerReferences("", drpCustomer.SelectedItem.Value.Trim());
                            if (dsCustRef1.Tables[0].Rows.Count > 0)
                            {
                                sCustName = dsCustRef1.Tables[0].Rows[0]["cust_name"].ToString().Trim();
                                sCustAdd11 = dsCustRef1.Tables[0].Rows[0]["address1"].ToString().Trim();
                                sCustAdd21 = dsCustRef1.Tables[0].Rows[0]["address2"].ToString().Trim();
                                sCustAdd31 = dsCustRef1.Tables[0].Rows[0]["address3"].ToString().Trim();
                                sCustAdd41 = dsCustRef1.Tables[0].Rows[0]["address4"].ToString().Trim();
                                sCustAdd51 = dsCustRef1.Tables[0].Rows[0]["address5"].ToString().Trim();
                                sBankName1 = dsCustRef1.Tables[0].Rows[0]["bank_name"].ToString().Trim();
                                sBankAddr11 = dsCustRef1.Tables[0].Rows[0]["bank_address1"].ToString().Trim();
                                sBankAddr21 = dsCustRef1.Tables[0].Rows[0]["bank_address2"].ToString().Trim();
                                sBankAC1 = dsCustRef1.Tables[0].Rows[0]["bank_acno"].ToString().Trim();
                                sBankACType1 = dsCustRef1.Tables[0].Rows[0]["bank_ac_type"].ToString().Trim();
                                sBankSortCode1 = dsCustRef1.Tables[0].Rows[0]["bank_sort_code"].ToString().Trim();
                                sBankSwiftCode1 = dsCustRef1.Tables[0].Rows[0]["bank_swift_code"].ToString().Trim();
                                sBIC1 = dsCustRef1.Tables[0].Rows[0]["BIC"].ToString().Trim();
                                sIBAN1 = dsCustRef1.Tables[0].Rows[0]["IBAN"].ToString().Trim();
                                sPayCurr1 = dsCustRef1.Tables[0].Rows[0]["pay_currency"].ToString().Trim();
                            }

                            StringBuilder strVal = new StringBuilder();
                            strVal.Append("<table width='100%' align='Center' style='border:1px solid green;'>");
                            rowIndex = 1;

                            strVal.Append("<tr></tr>");

                            strVal.Append("<tr></tr>");

                            strVal.Append("<tr>");
                            strVal.Append("<td style='border:1px solid green; border-bottom:0px;' align='left'>Customer</td><td style='border:1px solid green;' align='left'>" + sCustName + "</td>");
                            strVal.Append("<td></td><td></td><td></td><td style='color:white;backcolor:green'></td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr>");
                            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd11 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr>");
                            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd21 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr>");
                            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd31 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr>");
                            strVal.Append("<td style='border:1px solid green;border-bottom:0px;border-top:0px;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd41 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr>");
                            strVal.Append("<td style='border:1px solid green; border-bottom:1px solid green;' align='left'></td><td style='border:1px solid green;' align='left'>" + sCustAdd51 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr><td style='font-weight:bold;border:1px solid green;'>Date</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Type</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Journal</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Production Editor</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Ref</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Debit</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Credit</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Outstanding</td>");
                            strVal.Append("</tr>");
                            double dblInvDebitTotal = 0;
                            double dblInvCreditTotal = 0;
                            double dblInvStgTotal = 0;
                            rowIndex = 9;
                            foreach (DataRow r in dtable.Rows)
                            {

                                string strINNO = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                                strVal.Append("<tr><td style='border:1px solid green;' align='left'>" + r["IDATE"].ToString().Substring(1, 10).Trim() + "</td>");
                                strVal.Append("<td style='border:1px solid green;' align='left' >" + r["INVTYPE"].ToString().Trim() + "</td>");
                                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["JOBTITLE"].ToString().Trim() + "</td>");
                                strVal.Append("<td style='border:1px solid green;' align='left'>" + r["INVDNAME"].ToString().Trim() + "</td>");
                                strVal.Append("<td style='border:1px solid green;' align='left'>" + strINNO + "</td>");

                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                {

                                    if (r["POUNDVALUE"] != null && Convert.ToString(r["POUNDVALUE"]).Trim() != "")
                                    {
                                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                                        strVal.Append("<td style='border:1px solid green;'></td>");
                                        dblInvDebitTotal = dblInvDebitTotal + Convert.ToDouble(r["POUNDVALUE"]);
                                    }
                                    else
                                    {
                                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                                        strVal.Append("<td style='border:1px solid green;'></td>");
                                    }
                                }

                                else
                                {

                                    if (r["POUNDVALUE"] != null && Convert.ToString(r["POUNDVALUE"]).Trim() != "")
                                    {
                                        strVal.Append("<td style='border:1px solid green;'></td>");
                                        strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                                        dblInvCreditTotal = dblInvCreditTotal + Convert.ToDouble(r["POUNDVALUE"]);
                                    }
                                    else
                                    {
                                        strVal.Append("<td style='border:1px solid green;'></td>");
                                        strVal.Append("<td style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                                    }
                                }


                                if (r["POUNDVALUE"] != null && Convert.ToString(r["POUNDVALUE"]).Trim() != "")
                                {
                                    strVal.Append("<td style=\"mso-number-format:\\##\\,\\##\\##0\\.00\";' style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td>");
                                    dblInvStgTotal = dblInvStgTotal + Convert.ToDouble(r["POUNDVALUE"]);
                                }
                                else
                                {
                                    strVal.Append("<td style='border:1px solid green;'>" + r["POUNDVALUE"].ToString().Trim() + "</td></tr>");
                                }



                                this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());

                                rowIndex++;


                            }
                            strVal.Append("<tr>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;' Colspan='7'; align='right'>Total</td>");
                            strVal.Append("<td style='mso-number-format:0.00;font-weight:bold;border:1px solid green;' align='right'>" + dblInvStgTotal.ToString() + "</td>");
                            strVal.Append("</tr>");

                            rowIndex++;

                            string strCurrent = this.getSelectionTotal(aIndexCol[0], "H");
                            string str30Days = this.getSelectionTotal(aIndexCol[1], "H");
                            string str60Days = this.getSelectionTotal(aIndexCol[2], "H");
                            string str90Days = this.getSelectionTotal(aIndexCol[3], "H");
                            string str120Days = this.getSelectionTotal(aIndexCol[4], "H");

                            strVal.Append("<tr></tr>");
                            rowIndex++;
                            strVal.Append("<tr></tr>");
                            rowIndex++;
                            strVal.Append("<tr></tr>");
                            rowIndex++;




                            strVal.Append("<tr><td style='font-weight:bold;border:1px solid green;'>Description</td>");
                            strVal.Append("<td style='font-weight:bold;border:1px solid green;'>Value</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td colspan='2' style='border:1px solid green;' align='center'>Electronic Banking Details</td>");
                            strVal.Append("</tr>");
                            rowIndex++;

                            int intTotalStart = rowIndex;
                            strVal.Append("<tr><td style='border:1px solid green;'>Current</td><td style='mso-number-format:0.00;border:1px solid green;'>" + strCurrent + "</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;'>Bank Name:</td><td style='border:1px solid green;' align='left'>" + sBankName1 + "</td>");
                            strVal.Append("</tr>");
                            rowIndex++;

                            strVal.Append("<tr><td style='border:1px solid green;'>Over 30 Days</td><td style='mso-number-format:0.00;border:1px solid green;'>" + str30Days + "</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green; border-bottom:0px;'>Bank Address:</td><td style='border:1px solid green;' align='left'>" + sBankAddr11 + "</td>");
                            strVal.Append("</tr>");
                            rowIndex++;

                            strVal.Append("<tr><td style='border:1px solid green;'>Over 60 Days</td><td style='mso-number-format:0.00;border:1px solid green;'>" + str60Days + "</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;border-top:0px;'></td><td style='border:1px solid green;' align='left'>" + sBankAddr21 + "</td>");
                            strVal.Append("</tr>");
                            rowIndex++;

                            strVal.Append("<tr><td style='border:1px solid green;'>Over 90 Days</td><td style='mso-number-format:0.00;border:1px solid green;'>" + str90Days + "</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;'>Bank Account No.:</td><td style='border:1px solid green;' align='left'>" + sBankAC1 + "</td>");
                            strVal.Append("</tr>");
                            rowIndex++;

                            strVal.Append("<tr><td style='border:1px solid green;'>Over 120 Days</td><td style='mso-number-format:0.00;border:1px solid green;'>" + str120Days + "</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;'>Account Type:</td><td style='border:1px solid green;' align='left'>" + sBankACType1 + "</td>");
                            strVal.Append("</tr>");
                            int intTotalEnd = rowIndex;
                            rowIndex++;


                            strVal.Append("<tr><td style='border:1px solid green;'>Total :</td><td style='mso-number-format:0.00;border:1px solid green;' align='right'>=SUM(B" + intTotalStart.ToString() + ":B" + intTotalEnd.ToString() + ")</td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;'>Bank Sort Code:</td><td style='border:1px solid green;' align='left'>" + sBankSortCode1 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr><td></td><td></td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;'>BIC:</td><td style='border:1px solid green;' align='left'>" + sBIC1 + "</td>");
                            strVal.Append("</tr>");

                            strVal.Append("<tr><td></td><td></td>");
                            strVal.Append("<td></td>");
                            strVal.Append("<td style='border:1px solid green;'>IBAN:</td><td style='border:1px solid green;' align='left'>" + sIBAN1 + "</td>");
                            strVal.Append("</tr>");


                            strVal.Append("</table>");

                            string strPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "") + ".xls";
                            System.IO.StreamWriter file = new System.IO.StreamWriter(strPath);
                            file.WriteLine(strVal);
                            file.Close();

                            //Response.Clear();
                            //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "test_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "") + ".xls"));
                            //Response.ContentType = "application/ms-excel";
                            //Response.Write(strVal.ToString());


                        }
                        break;
                    case "10039": // World Bank
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["PONUMBER"].ToString().Trim();
                            range[rowIndex, 4] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 5] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 6] = r["INVDNAME"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["USDVALUE"].ToString().Trim();
                            else range[rowIndex, 8] = r["USDVALUE"].ToString().Trim();
                            range[rowIndex, 9] = r["USDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[18, 1] = dtNow.ToString();
                        range[rowIndex + 6, 4] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 7, 4] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 8, 4] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 9, 4] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 10, 4] = this.getSelectionTotal(aIndexCol[4], "I");
                        //range[rowIndex + 11, 4] = this.getSelectionTotal(aIndexCol[5], "I");
                        break;
                    case "10028": // Thomson Round
                        rowIndex = 21;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 5] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 6] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[15, 2] = dtNow.ToString();
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[0], "G");
                        range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[1], "G");
                        range[rowIndex + 13, 3] = this.getSelectionTotal(aIndexCol[2], "G");
                        range[rowIndex + 14, 3] = this.getSelectionTotal(aIndexCol[3], "G");
                        range[rowIndex + 15, 3] = this.getSelectionTotal(aIndexCol[4], "G");
                        //range[rowIndex + 16, 3] = this.getSelectionTotal(aIndexCol[5], "G");
                        break;
                    case "10040": //Psychology press
                        rowIndex = 19;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            range[rowIndex, 6] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                            else range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                            range[rowIndex, 10] = r["POUNDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[14, 2] = dtNow.ToString();
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[0], "J");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[1], "J");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[2], "J");
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[3], "J");
                        range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[4], "J");
                        //range[rowIndex + 13, 3] = this.getSelectionTotal(aIndexCol[5], "J");
                        break;
                    case "10105": //San Lucas 
                        rowIndex = 24;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 2] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 4] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 5] = r["JOBTITLE"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["POUNDVALUE"].ToString().Trim();
                            else range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                            range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 9]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                        range[19, 3] = dtNow.ToString();
                        range[rowIndex + 7, 4] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 8, 4] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 9, 4] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 10, 4] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 11, 4] = this.getSelectionTotal(aIndexCol[4], "I");
                        range[rowIndex + 12, 4] = this.getSelectionTotal(aIndexCol[5], "I");
                        break;
                    case "10110": //Polyphonia
                        rowIndex = 24;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? r["JOBTITLE"].ToString().Trim() : r["PONUMBER"].ToString().Trim();
                            range[rowIndex, 4] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 6] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[19, 2] = dtNow.ToString();
                        range[rowIndex + 10, 2] = this.getSelectionTotal(aIndexCol[0], "H");
                        range[rowIndex + 11, 2] = this.getSelectionTotal(aIndexCol[1], "H");
                        range[rowIndex + 12, 2] = this.getSelectionTotal(aIndexCol[2], "H");
                        range[rowIndex + 13, 2] = this.getSelectionTotal(aIndexCol[3], "H");
                        range[rowIndex + 14, 2] = this.getSelectionTotal(aIndexCol[4], "H");
                        //range[rowIndex + 15, 2] = this.getSelectionTotal(aIndexCol[5], "H");
                        break;
                    case "10111": // MO groups
                        rowIndex = 19;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 4] = r["PROJECTNUMBER"].ToString().Trim();
                            range[rowIndex, 5] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 6] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[14, 2] = dtNow.ToString();
                        range[rowIndex + 9, 2] = this.getSelectionTotal(aIndexCol[0], "H");
                        range[rowIndex + 10, 2] = this.getSelectionTotal(aIndexCol[1], "H");
                        range[rowIndex + 11, 2] = this.getSelectionTotal(aIndexCol[2], "H");
                        range[rowIndex + 12, 2] = this.getSelectionTotal(aIndexCol[3], "H");
                        range[rowIndex + 13, 2] = this.getSelectionTotal(aIndexCol[4], "H");
                        //range[rowIndex + 14, 2] = this.getSelectionTotal(aIndexCol[5], "H");
                        break;
                    case "10103": // Logoscript
                        rowIndex = 19;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 4] = r["PONUMBER"].ToString().Trim();
                            range[rowIndex, 5] = r["PROJECTNUMBER"].ToString().Trim();
                            range[rowIndex, 6] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 10] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[14, 2] = dtNow.ToString();
                        range[rowIndex + 6, 3] = this.getSelectionTotal(aIndexCol[0], "J");
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[1], "J");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[2], "J");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[3], "J");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[4], "J");
                        //range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[5], "J");
                        break;
                    case "10031": // Informa
                        rowIndex = 25;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 4] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 5] = r["JOBTITLE"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 6] = r["USDVALUE"].ToString().Trim();
                            else range[rowIndex, 7] = r["USDVALUE"].ToString().Trim();
                            range[rowIndex, 8] = r["USDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[20, 2] = dtNow.ToString();
                        range[rowIndex + 6, 3] = this.getSelectionTotal(aIndexCol[0], "H");
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[1], "H");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[2], "H");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[3], "H");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[4], "H");
                        //range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[5], "H");
                        break;
                    case "2558": // CRC press
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 4] = r["BISBN"].ToString().Trim();
                            range[rowIndex, 5] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 6] = r["INVDNAME"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["USDVALUE"].ToString().Trim();
                            else range[rowIndex, 8] = r["USDVALUE"].ToString().Trim();
                            range[rowIndex, 9] = r["USDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[18, 2] = dtNow.ToString();
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[4], "I");
                        //range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[5], "I");
                        break;
                    case "10035": // Clarus press
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 4] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 5] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 6] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                        range[18, 2] = dtNow.ToString();
                        range[rowIndex + 7, 2] = this.getSelectionTotal(aIndexCol[0], "G");
                        range[rowIndex + 8, 2] = this.getSelectionTotal(aIndexCol[1], "G");
                        range[rowIndex + 9, 2] = this.getSelectionTotal(aIndexCol[2], "G");
                        range[rowIndex + 10, 2] = this.getSelectionTotal(aIndexCol[3], "G");
                        range[rowIndex + 11, 2] = this.getSelectionTotal(aIndexCol[4], "G");
                        range[rowIndex + 12, 2] = this.getSelectionTotal(aIndexCol[5], "G");
                        break;
                    case "10091": // Channel view
                        rowIndex = 24;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["POUNDVALUE"].ToString().Trim();
                            else range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                            range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[19, 2] = dtNow.ToString();
                        range[rowIndex + 6, 3] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[4], "I");
                        //range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[5], "I");
                        break;
                    case "10037": // CAI
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 5] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[18, 2] = dtNow.ToString();
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[4], "I");
                        //range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[5], "I");
                        break;
                    case "10069": // By The Way
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            range[rowIndex, 6] = r["OUTPRINTAREA"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["USDVALUE"].ToString().Trim();
                            else
                                range[rowIndex, 8] = r["USDVALUE"].ToString().Trim();
                            range[rowIndex, 9] = r["USDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 9]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                        range[18, 1] = dtNow.ToString();

                        range[rowIndex + 5, 3] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 6, 3] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[4], "I");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[5], "I");
                        break;
                    case "10089": // AIC
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 6] = r["CADVALUE"].ToString().Trim();
                            else range[rowIndex, 7] = r["CADVALUE"].ToString().Trim();
                            range[rowIndex, 8] = r["CADVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[18, 1] = dtNow.ToString();
                        range[rowIndex + 5, 3] = this.getSelectionTotal(aIndexCol[0], "H");
                        range[rowIndex + 6, 3] = this.getSelectionTotal(aIndexCol[1], "H");
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[2], "H");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[3], "H");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[4], "H");
                        //range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[5], "H");
                        break;
                    case "10094": // Tek-Trans
                        rowIndex = 19;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range = xlWorkSheet.Cells;
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = "=DATE(YEAR(A" + rowIndex + "),1+MONTH(A" + rowIndex + "),0)";
                            range[rowIndex, 3] = "=B" + rowIndex + "+120";
                            range[rowIndex, 4] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVTYPE"].ToString().Trim().Equals("INV") ? r["PONUMBER"].ToString().Trim() : "Payment on Account";
                            range[rowIndex, 6] = r["PROJECTNUMBER"].ToString().Trim();
                            range[rowIndex, 7] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 10] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 11] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals120Daysof(r["IDATE"].ToString(), rowIndex);
                            this.SeperateTotals120DaysOf(r["IDATE"].ToString(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 9]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 10]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 11]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                        range[14, 3] = dtNow.ToString();
                        //range[rowIndex + 6, 4] = String.IsNullOrEmpty(aCurrentTot[0, 0]) ? "00.0" : "=Sum(K" + aCurrentTot[0, 0] + ":K" + aCurrentTot[0, 1] + ")";                        
                        //range[rowIndex + 8, 4] = String.IsNullOrEmpty(aCurrentTot[4, 0]) ? "00.0" : "=Sum(K" + aCurrentTot[4, 0] + ":K" + aCurrentTot[4, 1] + ")";
                        range[rowIndex + 6, 4] = this.getSelectionTotal(aIndexCol[0], "K");
                        range[rowIndex + 8, 4] = this.getSelectionTotal(aIndexCol[4], "K");
                        if (!String.IsNullOrEmpty(aIndexCol[5]))
                            range[rowIndex + 10, 4] = this.getSelectionTotal(aIndexCol[5], "K");
                        else
                        {
                            range[rowIndex + 10, 1] = "";
                            range[rowIndex + 10, 4] = "";
                        }
                        break;
                    case "10108": //expertTranslator                    
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 5] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 6] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[18, 2] = dtNow.ToString();
                        range[rowIndex + 6, 3] = this.getSelectionTotal(aIndexCol[0], "G");
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[1], "G");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[2], "G");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[3], "G");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[4], "G");
                        //range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[5], "G");
                        break;
                    case "255610040"://T&F and Psychology press
                        rowIndex = 19;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            range[rowIndex, 6] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                            else range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                            range[rowIndex, 10] = r["POUNDVALUE"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[14, 2] = dtNow.ToString();
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[0], "J");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[1], "J");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[2], "J");
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[3], "J");
                        range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[4], "J");
                        //range[rowIndex + 13, 3] = this.getSelectionTotal(aIndexCol[5], "J");
                        rowIndex = 19;
                        foreach (DataRow r in dtable.Rows)
                        {
                            if (r["CUSTNO1"].ToString().Trim() == "10040")
                            {
                                //row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                                //row.Select();
                                //row.Font.Bold = "true";
                                //row.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon);                                
                                ((Excel.Range)range[rowIndex, 1]).Font.Bold = "true";
                                ((Excel.Range)range[rowIndex, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon);
                                ((Excel.Range)range[rowIndex, 3]).Font.Bold = "true";
                                ((Excel.Range)range[rowIndex, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon);
                                ((Excel.Range)range[rowIndex, 6]).Font.Bold = "true";
                                ((Excel.Range)range[rowIndex, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon);
                                ((Excel.Range)range[rowIndex, 8]).Font.Bold = "true";
                                ((Excel.Range)range[rowIndex, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon);
                                ((Excel.Range)range[rowIndex, 10]).Font.Bold = "true";
                                ((Excel.Range)range[rowIndex, 10]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Maroon);
                            } rowIndex++;
                        }
                        break;
                    case "10085": //Independent colleges                    
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            //range[rowIndex, 2] = "";
                            range[rowIndex, 3] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 4] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 5] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 6] = r["BISBN"].ToString().Trim();
                            range[rowIndex, 7] = r["PONUMBER"].ToString().Trim();
                            range[rowIndex, 8] = r["PROJECTNUMBER"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 10] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 11] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[18, 1] = sNowDate;
                        range[rowIndex + 7, 4] = this.getSelectionTotal(aIndexCol[0], "K");
                        range[rowIndex + 8, 4] = this.getSelectionTotal(aIndexCol[1], "K");
                        range[rowIndex + 9, 4] = this.getSelectionTotal(aIndexCol[2], "K");
                        range[rowIndex + 10, 4] = this.getSelectionTotal(aIndexCol[3], "K");
                        range[rowIndex + 11, 4] = this.getSelectionTotal(aIndexCol[4], "K");
                        break;
                    case "10123": //Lionbridge Espana S.L.                    
                    case "10152": //Lionbridge Italy
                    case "10163": //Lionbridge Ireland
                    case "10167": //Lionbridge Mumbai
                    case "10170": //Lionbridge S.R.O
                        rowIndex = 21;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            //range[rowIndex, 2] = "";
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            //range[rowIndex, 5] = r["BISBN"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            range[rowIndex, 6] = r["PONUMBER"].ToString().Trim();
                            //range[rowIndex, 7] = r["PROJECTNUMBER"].ToString().Trim();
                            if (r["INVTYPE"].ToString().Trim() == "INV")
                                range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            else range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                        }
                        range[16, 1] = sNowDate;
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[1], "I");
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[2], "I");
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[3], "I");
                        range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[4], "I");
                        range[6, 2] = "1104";//For customernumber set
                        break;
                    case "10087"://Add by subbu for VWM 
                    case "10102": //Add by subbu for JMedCBR Defense
                    case "2052"://Add by subbu for LUND - Ecology Building
                    case "10081": //Add by subbu for CoAction
                        string sCustAdd1 = "", sCustAdd2 = "", sCustAdd3 = "", sCustAdd4 = "", sCustAdd5 = "",
                            sBankName = "", sBankAddr1 = "", sBankAddr2 = "", sBankAC = "", sBankACType = "",
                            sBankSortCode = "", sBankSwiftCode = "", sBIC = "", sIBAN = "", sPayCurr = "";
                        DataSet dsCustRef = new CustomerBase().getCustomerReferences("", drpCustomer.SelectedItem.Value.Trim());
                        if (dsCustRef.Tables[0].Rows.Count > 0)
                        {
                            sCustAdd1 = dsCustRef.Tables[0].Rows[0]["address1"].ToString().Trim();
                            sCustAdd2 = dsCustRef.Tables[0].Rows[0]["address2"].ToString().Trim();
                            sCustAdd3 = dsCustRef.Tables[0].Rows[0]["address3"].ToString().Trim();
                            sCustAdd4 = dsCustRef.Tables[0].Rows[0]["address4"].ToString().Trim();
                            sCustAdd5 = dsCustRef.Tables[0].Rows[0]["address5"].ToString().Trim();
                            sBankName = dsCustRef.Tables[0].Rows[0]["bank_name"].ToString().Trim();
                            sBankAddr1 = dsCustRef.Tables[0].Rows[0]["bank_address1"].ToString().Trim();
                            sBankAddr2 = dsCustRef.Tables[0].Rows[0]["bank_address2"].ToString().Trim();
                            sBankAC = dsCustRef.Tables[0].Rows[0]["bank_acno"].ToString().Trim();
                            sBankACType = dsCustRef.Tables[0].Rows[0]["bank_ac_type"].ToString().Trim();
                            sBankSortCode = dsCustRef.Tables[0].Rows[0]["bank_sort_code"].ToString().Trim();
                            sBankSwiftCode = dsCustRef.Tables[0].Rows[0]["bank_swift_code"].ToString().Trim();
                            sBIC = dsCustRef.Tables[0].Rows[0]["BIC"].ToString().Trim();
                            sIBAN = dsCustRef.Tables[0].Rows[0]["IBAN"].ToString().Trim();
                            sPayCurr = dsCustRef.Tables[0].Rows[0]["pay_currency"].ToString().Trim();
                        }
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            //range[rowIndex, 2] = "";
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            //range[rowIndex, 7] = r["BISBN"].ToString().Trim();
                            //range[rowIndex, 8] = r["PONUMBER"].ToString().Trim();
                            //range[rowIndex, 9] = r["PROJECTNUMBER"].ToString().Trim();
                            if (r["EUROVALUE_ACTUAL"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    //range[rowIndex, 10] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                    range[rowIndex, 6] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                else range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();//range[rowIndex, 11] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                //range[rowIndex, 12] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            }
                            else if (r["CADVALUE"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 6] = r["CADVALUE"].ToString().Trim();
                                else range[rowIndex, 7] = r["CADVALUE"].ToString().Trim();
                                range[rowIndex, 8] = r["CADVALUE"].ToString().Trim();
                            }
                            else if (r["USDVALUE"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 6] = r["USDVALUE"].ToString().Trim();
                                else range[rowIndex, 7] = r["USDVALUE"].ToString().Trim();
                                range[rowIndex, 8] = r["USDVALUE"].ToString().Trim();
                            }
                            else
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 6] = r["POUNDVALUE"].ToString().Trim();
                                else range[rowIndex, 7] = r["POUNDVALUE"].ToString().Trim();
                                range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                            }
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 9]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 10]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 11]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 12]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }

                        if (sCust == "10081")
                        { range[6, 1] = "Customer"; range[6, 2] = "'0204"; }
                        else if (sCust == "2052")
                        { range[6, 1] = "Customer"; range[6, 2] = "'0109"; }
                        else if (sCust == "10102")
                        { range[6, 1] = "Customer"; range[6, 2] = "'0903"; }
                        else if (sCust == "10087")
                        { range[6, 1] = "Customer"; range[6, 2] = "2100"; }
                        if (sCust != "10087")//For vwm (hardcode customername in template file so need not this line)
                            range[8, 1] = drpCustomer.SelectedItem.Text.Trim();

                        range[9, 1] = sCustAdd1;
                        range[10, 1] = sCustAdd2;
                        range[11, 1] = sCustAdd3;
                        range[12, 1] = sCustAdd4;
                        range[13, 1] = sCustAdd5;
                        range[18, 1] = "Date:";
                        range[18, 2] = sNowDate;
                        //range[20, 12] = sCustCurr;
                        range[20, 8] = sCustCurr;
                        range[rowIndex + 5, 3] = sCustCurr;
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[0], "H");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[1], "H");
                        range[rowIndex + 8, 5] = sBankName;
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[2], "H");
                        range[rowIndex + 9, 5] = sBankAddr1;
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[3], "H");
                        range[rowIndex + 10, 5] = sBankAddr2;
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[4], "H");
                        range[rowIndex + 11, 5] = sBankAC;
                        if (oIB.HasPayments(drpCustomer.SelectedItem.Value.Trim()))
                        {
                            range[rowIndex + 12, 1] = "Payments on Account";
                            range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[5], "H");
                        }

                        /*
                        //range[rowIndex + 12, 6] = sBankACType;
                        range[rowIndex + 13, 6] = sBankSortCode;
                        //range[rowIndex + 14, 6] = sBankSwiftCode;
                        range[rowIndex + 15, 6] = sBIC;
                        range[rowIndex + 16, 6] = sIBAN;
                        range[rowIndex + 17, 6] = sPayCurr;
                         * */
                        range[rowIndex + 12, 5] = sBankSortCode;
                        range[rowIndex + 13, 5] = sBIC;
                        range[rowIndex + 14, 5] = sIBAN;
                        range[rowIndex + 15, 5] = sPayCurr;
                        break;

                    case "10115"://Add by subbu for thebigword 
                    case "10159"://Add by subbu for thebigword UK
                    case "10160"://Add by subbu for thebigword US
                    case "10161"://Add by subbu for thebigword Japan 
                    case "10172"://Add by subbu for thebigword China 
                        sCustAdd1 = ""; sCustAdd2 = ""; sCustAdd3 = ""; sCustAdd4 = ""; sCustAdd5 = "";
                        sBankName = ""; sBankAddr1 = ""; sBankAddr2 = ""; sBankAC = ""; sBankACType = "";
                        sBankSortCode = ""; sBankSwiftCode = ""; sBIC = ""; sIBAN = ""; sPayCurr = "";
                        dsCustRef = new CustomerBase().getCustomerReferences("", drpCustomer.SelectedItem.Value.Trim());
                        if (dsCustRef.Tables[0].Rows.Count > 0)
                        {
                            sCustAdd1 = dsCustRef.Tables[0].Rows[0]["address1"].ToString().Trim();
                            sCustAdd2 = dsCustRef.Tables[0].Rows[0]["address2"].ToString().Trim();
                            sCustAdd3 = dsCustRef.Tables[0].Rows[0]["address3"].ToString().Trim();
                            sCustAdd4 = dsCustRef.Tables[0].Rows[0]["address4"].ToString().Trim();
                            sCustAdd5 = dsCustRef.Tables[0].Rows[0]["address5"].ToString().Trim();
                            sBankName = dsCustRef.Tables[0].Rows[0]["bank_name"].ToString().Trim();
                            sBankAddr1 = dsCustRef.Tables[0].Rows[0]["bank_address1"].ToString().Trim();
                            sBankAddr2 = dsCustRef.Tables[0].Rows[0]["bank_address2"].ToString().Trim();
                            sBankAC = dsCustRef.Tables[0].Rows[0]["bank_acno"].ToString().Trim();
                            sBankACType = dsCustRef.Tables[0].Rows[0]["bank_ac_type"].ToString().Trim();
                            sBankSortCode = dsCustRef.Tables[0].Rows[0]["bank_sort_code"].ToString().Trim();
                            sBankSwiftCode = dsCustRef.Tables[0].Rows[0]["bank_swift_code"].ToString().Trim();
                            sBIC = dsCustRef.Tables[0].Rows[0]["BIC"].ToString().Trim();
                            sIBAN = dsCustRef.Tables[0].Rows[0]["IBAN"].ToString().Trim();
                            sPayCurr = dsCustRef.Tables[0].Rows[0]["pay_currency"].ToString().Trim();
                        }
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            //range[rowIndex, 2] = "";
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            //range[rowIndex, 7] = r["BISBN"].ToString().Trim();
                            range[rowIndex, 6] = r["PONUMBER"].ToString().Trim();
                            //range[rowIndex, 9] = r["PROJECTNUMBER"].ToString().Trim();
                            if (r["EUROVALUE_ACTUAL"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    //range[rowIndex, 10] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                    range[rowIndex, 7] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                else range[rowIndex, 8] = r["EUROVALUE_ACTUAL"].ToString().Trim();//range[rowIndex, 11] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                //range[rowIndex, 12] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            }
                            else if (r["CADVALUE"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 7] = r["CADVALUE"].ToString().Trim();
                                else range[rowIndex, 8] = r["CADVALUE"].ToString().Trim();
                                range[rowIndex, 9] = r["CADVALUE"].ToString().Trim();
                            }
                            else if (r["USDVALUE"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 7] = r["USDVALUE"].ToString().Trim();
                                else range[rowIndex, 8] = r["USDVALUE"].ToString().Trim();
                                range[rowIndex, 9] = r["USDVALUE"].ToString().Trim();
                            }
                            else
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 7] = r["POUNDVALUE"].ToString().Trim();
                                else range[rowIndex, 8] = r["POUNDVALUE"].ToString().Trim();
                                range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                            }
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 9]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 10]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 11]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 12]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                        range[6, 1] = "Customer";
                        range[6, 2] = "1909";
                        range[8, 1] = drpCustomer.SelectedItem.Text.Trim();
                        range[9, 1] = sCustAdd1;
                        range[10, 1] = sCustAdd2;
                        range[11, 1] = sCustAdd3;
                        range[12, 1] = sCustAdd4;
                        range[13, 1] = sCustAdd5;
                        range[18, 1] = sNowDate;

                        //range[20, 12] = sCustCurr;
                        range[20, 9] = sCustCurr;
                        range[rowIndex + 5, 3] = sCustCurr;
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[0], "I");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[1], "I");
                        //range[rowIndex + 8, 6] = sBankName;
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[2], "I");
                        //range[rowIndex + 9, 6] = sBankAddr1;
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[3], "I");
                        //range[rowIndex + 10, 6] = sBankAddr2;
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[4], "I");
                        //range[rowIndex + 11, 6] = sBankAC;
                        if (oIB.HasPayments(drpCustomer.SelectedItem.Value.Trim()))
                        {
                            range[rowIndex + 12, 1] = "Payments on Account";
                            range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[5], "I");
                        }

                        /*
                        //range[rowIndex + 12, 6] = sBankACType;
                        range[rowIndex + 13, 6] = sBankSortCode;
                        //range[rowIndex + 14, 6] = sBankSwiftCode;
                        range[rowIndex + 15, 6] = sBIC;
                        range[rowIndex + 16, 6] = sIBAN;
                        range[rowIndex + 17, 6] = sPayCurr;
                         * */
                        //Comment by subbu for loarine mail request
                        //range[rowIndex + 12, 6] = sBankSortCode;
                        //range[rowIndex + 13, 6] = sBIC;
                        //range[rowIndex + 14, 6] = sIBAN;
                        //range[rowIndex + 15, 6] = sPayCurr;
                        break;
                    default: // Other Customers
                        //string sCustAdd11 = "", sCustAdd21 = "", sCustAdd31 = "", sCustAdd41 = "", sCustAdd51 = "",
                        //    sBankName1 = "", sBankAddr11 = "", sBankAddr21 = "", sBankAC1 = "", sBankACType1 = "",
                        //    sBankSortCode1 = "", sBankSwiftCode1 = "", sBIC1 = "", sIBAN1 = "", sPayCurr1 = "";
                        dsCustRef1 = new CustomerBase().getCustomerReferences("", drpCustomer.SelectedItem.Value.Trim());
                        if (dsCustRef1.Tables[0].Rows.Count > 0)
                        {
                            sCustAdd11 = dsCustRef1.Tables[0].Rows[0]["address1"].ToString().Trim();
                            sCustAdd21 = dsCustRef1.Tables[0].Rows[0]["address2"].ToString().Trim();
                            sCustAdd31 = dsCustRef1.Tables[0].Rows[0]["address3"].ToString().Trim();
                            sCustAdd41 = dsCustRef1.Tables[0].Rows[0]["address4"].ToString().Trim();
                            sCustAdd51 = dsCustRef1.Tables[0].Rows[0]["address5"].ToString().Trim();
                            sBankName1 = dsCustRef1.Tables[0].Rows[0]["bank_name"].ToString().Trim();
                            sBankAddr11 = dsCustRef1.Tables[0].Rows[0]["bank_address1"].ToString().Trim();
                            sBankAddr21 = dsCustRef1.Tables[0].Rows[0]["bank_address2"].ToString().Trim();
                            sBankAC1 = dsCustRef1.Tables[0].Rows[0]["bank_acno"].ToString().Trim();
                            sBankACType1 = dsCustRef1.Tables[0].Rows[0]["bank_ac_type"].ToString().Trim();
                            sBankSortCode1 = dsCustRef1.Tables[0].Rows[0]["bank_sort_code"].ToString().Trim();
                            sBankSwiftCode1 = dsCustRef1.Tables[0].Rows[0]["bank_swift_code"].ToString().Trim();
                            sBIC1 = dsCustRef1.Tables[0].Rows[0]["BIC"].ToString().Trim();
                            sIBAN1 = dsCustRef1.Tables[0].Rows[0]["IBAN"].ToString().Trim();
                            sPayCurr1 = dsCustRef1.Tables[0].Rows[0]["pay_currency"].ToString().Trim();
                        }
                        rowIndex = 23;
                        range = xlWorkSheet.Cells;
                        foreach (DataRow r in dtable.Rows)
                        {
                            range[rowIndex, 1] = r["IDATE"].ToString().Trim();
                            //range[rowIndex, 2] = "";
                            range[rowIndex, 2] = r["INVTYPE"].ToString().Trim();
                            range[rowIndex, 3] = r["IINNO"].ToString().Trim().Equals("0") ? "" : r["IINNO"].ToString().Trim();
                            range[rowIndex, 4] = r["JOBTITLE"].ToString().Trim();
                            range[rowIndex, 5] = r["INVDNAME"].ToString().Trim();
                            range[rowIndex, 6] = r["BISBN"].ToString().Trim();
                            range[rowIndex, 7] = r["PONUMBER"].ToString().Trim();
                            range[rowIndex, 8] = r["PROJECTNUMBER"].ToString().Trim();
                            if (r["EUROVALUE_ACTUAL"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 9] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                else range[rowIndex, 10] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                                range[rowIndex, 11] = r["EUROVALUE_ACTUAL"].ToString().Trim();
                            }
                            else if (r["CADVALUE"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 9] = r["CADVALUE"].ToString().Trim();
                                else range[rowIndex, 10] = r["CADVALUE"].ToString().Trim();
                                range[rowIndex, 11] = r["CADVALUE"].ToString().Trim();
                            }
                            else if (r["USDVALUE"].ToString() != "")
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 9] = r["USDVALUE"].ToString().Trim();
                                else range[rowIndex, 10] = r["USDVALUE"].ToString().Trim();
                                range[rowIndex, 11] = r["USDVALUE"].ToString().Trim();
                            }
                            else
                            {
                                if (r["INVTYPE"].ToString().Trim() == "INV")
                                    range[rowIndex, 9] = r["POUNDVALUE"].ToString().Trim();
                                else range[rowIndex, 10] = r["POUNDVALUE"].ToString().Trim();
                                range[rowIndex, 11] = r["POUNDVALUE"].ToString().Trim();
                            }
                            //this.AssignTotals(r["IDATE"].ToString().Trim(), rowIndex);
                            this.SeperateTotals(r["IDATE"].ToString().Trim(), rowIndex, r["INVTYPE"].ToString().Trim());
                            rowIndex++;
                            row = ((Excel.Range)xlWorkSheet.Cells[rowIndex, 1]).EntireRow;
                            row.Select();
                            row.Insert(Excel.XlInsertShiftDirection.xlShiftDown, oMissing);
                            if (r["INVTYPE"].ToString().Trim().Equals("PAY"))
                            {
                                ((Excel.Range)range[rowIndex - 1, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 2]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 3]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 4]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 5]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 6]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 8]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 9]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 10]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 11]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                ((Excel.Range)range[rowIndex - 1, 12]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

                            }
                        }
                        range[6, 1] = drpCustomer.SelectedItem.Text.Trim();
                        range[7, 1] = sCustAdd11;
                        range[8, 1] = sCustAdd21;
                        range[9, 1] = sCustAdd31;
                        range[10, 1] = sCustAdd41;
                        range[11, 1] = sCustAdd51;
                        range[18, 1] = sNowDate;
                        range[20, 11] = sCustCurr;
                        range[rowIndex + 5, 3] = sCustCurr;
                        range[rowIndex + 7, 3] = this.getSelectionTotal(aIndexCol[0], "K");
                        range[rowIndex + 8, 3] = this.getSelectionTotal(aIndexCol[1], "K");
                        range[rowIndex + 8, 5] = sBankName1;
                        range[rowIndex + 9, 3] = this.getSelectionTotal(aIndexCol[2], "K");
                        range[rowIndex + 9, 5] = sBankAddr11;
                        range[rowIndex + 10, 3] = this.getSelectionTotal(aIndexCol[3], "K");
                        range[rowIndex + 10, 5] = sBankAddr21;
                        range[rowIndex + 11, 3] = this.getSelectionTotal(aIndexCol[4], "K");
                        range[rowIndex + 11, 5] = sBankAC1;
                        if (oIB.HasPayments(drpCustomer.SelectedItem.Value.Trim()))
                        {
                            range[rowIndex + 12, 1] = "Payments on Account";
                            range[rowIndex + 12, 3] = this.getSelectionTotal(aIndexCol[5], "K");
                        }

                        /*
                        //range[rowIndex + 12, 6] = sBankACType;
                        range[rowIndex + 13, 6] = sBankSortCode;
                        //range[rowIndex + 14, 6] = sBankSwiftCode;
                        range[rowIndex + 15, 6] = sBIC;
                        range[rowIndex + 16, 6] = sIBAN;
                        range[rowIndex + 17, 6] = sPayCurr;
                         * */
                        range[rowIndex + 12, 5] = sBankSortCode1;
                        range[rowIndex + 13, 5] = sBIC1;
                        range[rowIndex + 14, 5] = sIBAN1;
                        range[rowIndex + 15, 5] = sPayCurr1;
                        break;
                }
                top = (Excel.Range)xlWorkSheet.Cells[1, 1];
                top.Select();
                xlWorkBook.Save();
                dtable.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xlWorkBook.Close(true, oMissing, oMissing);
                xlWorkBooks.Close();
                xlApp.Quit();
                releaseObject(range);
                releaseObject(row);
                releaseObject(top);
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlWorkBooks);
                releaseObject(xlApp);
            }
        }
    }
    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            //obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            //Alert("Server Error " + ex.Message);
        }
        finally
        {
            //GC.GetTotalMemory(false);  
            //GC.Collect();  
            //GC.WaitForPendingFinalizers();  
            //GC.Collect();  
            //GC.GetTotalMemory(true);
        }
    }
    private void AssignTotals(string InvDate, int curIndex)
    {
        int daycnt = ((TimeSpan)dtNow.Subtract(DateTime.Parse(InvDate))).Days;
        switch (daycnt <= 30 ? 29 : (daycnt > 30 && daycnt <= 60 ? 30 : (daycnt > 60 && daycnt <= 90 ? 60 : (daycnt > 90 && daycnt <= 120 ? 90 : 120))))
        {
            case 29:
                if (String.IsNullOrEmpty(aCurrentTot[0, 0])) aCurrentTot[0, 0] = curIndex.ToString();
                aCurrentTot[0, 1] = curIndex.ToString();
                break;
            case 30:
                if (String.IsNullOrEmpty(aCurrentTot[1, 0])) aCurrentTot[1, 0] = curIndex.ToString();
                aCurrentTot[1, 1] = curIndex.ToString();
                break;
            case 60:
                if (String.IsNullOrEmpty(aCurrentTot[2, 0])) aCurrentTot[2, 0] = curIndex.ToString();
                aCurrentTot[2, 1] = curIndex.ToString();
                break;
            case 90:
                if (String.IsNullOrEmpty(aCurrentTot[3, 0])) aCurrentTot[3, 0] = curIndex.ToString();
                aCurrentTot[3, 1] = curIndex.ToString();
                break;
            default:
                if (String.IsNullOrEmpty(aCurrentTot[4, 0])) aCurrentTot[4, 0] = curIndex.ToString();
                aCurrentTot[4, 1] = curIndex.ToString();
                break;
        }
    }
    private void AssignTotals120Daysof(string Invdate, int curIndex)
    {
        Invdate = DateTime.Parse(Invdate).Month + "/01/" + DateTime.Parse(Invdate).Year;
        DateTime dt120 = DateTime.Parse(Invdate).AddMonths(1).AddDays(-1).AddDays(120);
        if (dt120 > dtNow)
        {
            if (String.IsNullOrEmpty(aCurrentTot[0, 0])) aCurrentTot[0, 0] = curIndex.ToString();
            aCurrentTot[0, 1] = curIndex.ToString();
        }
        else
        {
            if (String.IsNullOrEmpty(aCurrentTot[4, 0])) aCurrentTot[4, 0] = curIndex.ToString();
            aCurrentTot[4, 1] = curIndex.ToString();
        }
    }
    private void SeperateTotals(string InvDate, int curIndex, string sType)
    {
        if (sType == "INV")
        {
            int daycnt = ((TimeSpan)dtNow.Subtract(DateTime.Parse(InvDate))).Days;
            switch (daycnt <= 30 ? 29 : (daycnt > 30 && daycnt <= 60 ? 30 : (daycnt > 60 && daycnt <= 90 ? 60 : (daycnt > 90 && daycnt <= 120 ? 90 : 120))))
            {
                case 29:
                    aIndexCol[0] = aIndexCol[0] + curIndex.ToString() + ",";
                    break;
                case 30:
                    aIndexCol[1] = aIndexCol[1] + curIndex.ToString() + ",";
                    break;
                case 60:
                    aIndexCol[2] = aIndexCol[2] + curIndex.ToString() + ",";
                    break;
                case 90:
                    aIndexCol[3] = aIndexCol[3] + curIndex.ToString() + ",";
                    break;
                default:
                    aIndexCol[4] = aIndexCol[4] + curIndex.ToString() + ",";
                    break;
            }
        }
        else aIndexCol[5] = aIndexCol[5] + curIndex.ToString() + ",";
    }
    private void SeperateTotals120DaysOf(string InvDate, int curIndex, string sType)
    {
        if (sType == "INV")
        {
            InvDate = DateTime.Parse(InvDate).Month + "/01/" + DateTime.Parse(InvDate).Year;
            DateTime dt120 = DateTime.Parse(InvDate).AddMonths(1).AddDays(-1).AddDays(120);
            if (dt120 > dtNow)
            {
                aIndexCol[0] = aIndexCol[0] + curIndex.ToString() + ",";
            }
            else
            {
                aIndexCol[4] = aIndexCol[4] + curIndex.ToString() + ",";
            }
        }
        else aIndexCol[5] = aIndexCol[5] + curIndex.ToString() + ",";
    }

    private string getSelectionTotal(string sNums, string sColName)
    {
        string sData = null;
        if (!String.IsNullOrEmpty(sNums))
        {
            sData = "";
            string[] aColIndex = sNums.TrimEnd(',').Split(',');
            int p = 0;
            foreach (string num in aColIndex)
            {
                int i = int.Parse(num);
                if (sData != "")
                {
                    if (i == (p + 1))
                    {
                        if (sData.Contains(":" + sColName + p))
                            sData = sData.Replace(":" + sColName + p, ":" + sColName + num);
                        else sData = sData + ":" + sColName + num;
                    }
                    else sData = sData + "," + sColName + num;
                }
                else sData = sColName + num;
                p = i;
            } sData = "=Sum(" + sData + ")";
        }
        return sData
            ?? "0.00";
    }
}
