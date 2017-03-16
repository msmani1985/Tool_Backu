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


public partial class agedanalysis : System.Web.UI.Page
{
    DataSet oSet = null;
    DataSet oSet1 = null;
    XmlDocument oInvValues = null;
    XmlNode oElem = null;
    XmlNode oRowData = null;
    string sCurrencyReport = "CURRENCY", sReturnTable = "", sMyCurrency = "", sCurrP = "", sCurrD = "", sCurrE = "", sCurrC = "", sCurrU = "", sErrorRow = "";
    Double sTotalBal = 0.00, sTotalCurr = 0.00, sTotal30 = 0.00, sTotal60 = 0.00, sTotal90 = 0.00, sTotal120 = 0.00, sTotalPay = 0.00;
    Double sTotalColBal = 0.00, sTotalColCurr = 0.00, sTotalCol30 = 0.00, sTotalCol60 = 0.00, sTotalCol90 = 0.00, sTotalCol120 = 0.00, sTotalColPay = 0.00;
    Double oTotalEuro = 0.00;
    Double oTotalCAD = 0.00;
    Double oTotalDOLL = 0.00;
    Double oTotalPOUND = 0.00;
    Double oTotalunknown = 0.00;
    int iRowID = 8;

    datasourceIBSQL obj = new datasourceIBSQL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
            throw new Exception("Session Expired");
        
        if (Page.IsPostBack)
        {
            if (getType.SelectedIndex == 1)
                sCurrencyReport = "EURO";
            else
                sCurrencyReport = "CURRENCY";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        oSet = new DataSet();
        oSet1 = new DataSet();
        int iCustomer = 0;


        string sQry = "sp_Get_Aged_Analysis";
        oSet = obj.GetDataSet(sQry, "listtedd", CommandType.Text);
        //oSet1 = obj.InvoiceDataSet("SELECT CUSTNO1, SUM(F_DOUBLEABS(CREDITED_VALUE)) AS COLUMN1 FROM P_CREDITED_ON_ACCOUNT(NULL) GROUP BY CUSTNO1 ORDER BY CUSTNO1", "payment", CommandType.Text);
        oSet1 = obj.GetDataSet("P_CREDITED_ON_ACCOUNT 10066", "payment", CommandType.Text);

        if (oSet != null && oSet.Tables[0].Rows.Count > 0)
        {
            oSet.Tables[0].Columns.Add("PAYMENT");

            foreach (DataRow oR in oSet.Tables[0].Rows)
            {
                if (oSet1 != null && oSet1.Tables[0].Rows.Count <= 0)
                    break;
                if (Convert.ToInt32(oR["CUSTNO1"]) != iCustomer)
                {
                    foreach (DataRow oP in oSet1.Tables[0].Rows)
                        if (Convert.ToInt32(oR["CUSTNO1"]) == Convert.ToInt32(oP["CUSTNO1"]))
                        {
                            oR["PAYMENT"] = oP["CREDITED_VALUE"].ToString().Replace("-", "");
                            oP.Delete();
                            oSet1.AcceptChanges();
                            break;
                        }
                    iCustomer = Convert.ToInt32(oR["CUSTNO1"]);
                }
            }
            if (oSet1 != null && oSet1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow eRow in oSet1.Tables[0].Rows)
                {
                    oSet.Tables[0].ImportRow(eRow);
                    oSet.Tables[0].AcceptChanges();
                    oSet.Tables[0].Rows[oSet.Tables[0].Rows.Count - 1]["PAYMENT"] = eRow["CREDITED_VALUE"];
                    oSet.Tables[0].Rows[oSet.Tables[0].Rows.Count - 1]["IDATE"] = DateTime.Now.ToString();
                    oSet.Tables[0].Rows[oSet.Tables[0].Rows.Count - 1]["IINNO"] = "-1";
                    oSet.Tables[0].Rows[oSet.Tables[0].Rows.Count - 1]["CURRNO"] = eRow["CURRNO"];
                    oSet.Tables[0].AcceptChanges();
                }
            }
            oSet1 = null;
            excelTable.InnerHtml = sHTMLTable();
            btnExport.Visible = true;
            oSet = null;
            //msg.InnerHtml += DateTime.Now.TimeOfDay + " last line <br />";

        }
        else
            msg.InnerHtml += "<br/><br/>No records found from MIS !!!";
    }

    protected void getType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (getType.SelectedIndex == 1)
            sCurrencyReport = "EURO";
        else
            sCurrencyReport = "CURRENCY";
    }

    private string sSetMyCurrency(string value)
    {
        switch (value.Trim().ToUpper())
        {
            case "EURO": sMyCurrency = "&#8364;";
                break;
            case "DOLLAR": sMyCurrency = "$";
                break;
            case "STG": sMyCurrency = "&pound;";
                break;
            case "CAD": sMyCurrency = "CAD$";
                break;
        }
        return sMyCurrency;
    }

    private string sHTMLTable()
    {
        oInvValues = new XmlDocument();
        oInvValues.Load(ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString());
        if (oInvValues == null)
        {
            msg.InnerHtml += "<br/><br/>Unable to find/load invoice values file";
            return "";
        }
        oRowData = oInvValues.DocumentElement.SelectSingleNode("ROWDATA");
        oInvValues = null;
        //msg.InnerHtml += DateTime.Now.TimeOfDay + " DOM ready<br />";
        int iCustno = 0;
        oTotalEuro = 0;
        oTotalCAD = 0;
        oTotalDOLL = 0;
        oTotalPOUND = 0;
        sReturnTable = "";
        sErrorRow = "";
        sReturnTable = "<table id=\"exportExcel\" runat=\"server\" cellspacing='0' cellpadding='2' width='100%'>";
        sReturnTable += "<tr><td colspan='2' style=\"font-weight:bold;font-style:italic\">DATAPAGE INTERNATIONAL LIMITED</td>" + CreateEmptyCells(8) + "</tr>";
        sReturnTable += "<tr><td colspan='2'>Aged Analysis - " + sCurrencyReport + "</td>" + CreateEmptyCells(8) + "</tr>";
        sReturnTable += "<tr><td colspan='2'>Report Date: " + DateTime.Today.ToShortDateString() + "</td>" + CreateEmptyCells(8) + "</tr>";
        sReturnTable += "<tr>" + CreateEmptyCells(10) + "</tr>";
        sReturnTable += "<tr class='darkbackground'><td class='cellhead'>Customer</td>";
        sReturnTable += "<td class='cellhead'>Name</td>";
        if (getType.SelectedIndex == 0)
            sReturnTable += "<td class='cellhead'>Currency</td>";
        sReturnTable += "<td class='cellhead' style='text-align:right'>Balance</td>";
        sReturnTable += "<td class='cellhead' style='text-align:right'>Current</td>";
        sReturnTable += "<td class='cellhead' style='text-align:right'>Over 30 <br />days</td>";
        sReturnTable += "<td class='cellhead'>Over 60 <br />days</td>";
        sReturnTable += "<td class='cellhead'>Over 90 <br />days</td>";
        sReturnTable += "<td class='cellhead'>Over 120 <br />days</td>";
        sReturnTable += "<td class='cellhead'>Payment on <br />Account</td>";
        sReturnTable += "</tr>";
        sReturnTable += "<tr>" + CreateEmptyCells(10) + "</tr>";
        // first customer return by default;
        sReturnTable += "<tr class='dullbackground'>";
        sReturnTable += "<td style='text-align:left'>" + oSet.Tables[0].Rows[0]["LEDGERID"] + "</td>";
        sReturnTable += "<td style='text-align:left'>" + oSet.Tables[0].Rows[0]["CNAME"] + "</td>";
        //msg.InnerHtml += DateTime.Now.TimeOfDay + " start row looping<br />";
        //msg.InnerHtml = oSet.Tables[0].Rows.Count.ToString() + DateTime.Now.TimeOfDay + " rows start row looping<br />";

        //Added by subbu on 11th july 2011 for filter wip articles
        //DataView dv = oSet.Tables[0].DefaultView;
        //dv.RowFilter = "CATEGORY<>'Article'";
        //dt = dv.ToTable();

        foreach (DataRow oRow in oSet.Tables[0].Rows)
        {
            if (oRow["CNAME"].ToString().ToUpper() != "ARTICLE")
            {
                if (Convert.ToInt32(oRow["CUSTNO1"]) != iCustno)
                {
                    if (iCustno != 0)
                    {
                        bindcells("");
                        sReturnTable += "</tr>";
                        if ((iRowID % 2) != 0)
                            sReturnTable += "<tr class='dullbackground'>";
                        else
                            sReturnTable += "<tr class='lightbackground'>";
                        sReturnTable += "<td style='text-align:left'>" + oRow["LEDGERID"] + "</td>";
                        sReturnTable += "<td style='text-align:left'>" + oRow["CNAME"] + "</td>";
                    }
                    sRowData(oRow, true);
                }
                else
                    sRowData(oRow, false);
                iCustno = Convert.ToInt32(oRow["CUSTNO1"]);
            }

        }
        //msg.InnerHtml += DateTime.Now.TimeOfDay + " end loops here<br />";
        bindcells("");
        sReturnTable += "</tr>";
        sReturnTable += "<tr>" + CreateEmptyCells(10) + "</tr>";
        // need to work around here for total row for euro currency only
        if (getType.SelectedIndex == 1)
        {
            sReturnTable += "<tr class='dullbackground'><td>&nbsp;</td><td style='font-weight:bold'>Total</td>";
            sReturnTable += CreateEmptyCells(1);
            sReturnTable += "<td  class='currency' style='font-weight:bold'  x:fmla=\"=SUM(C8:C" + iRowID + ")\">" + (sTotalColBal == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalColBal).ToString()) + "</td>";
            sReturnTable += "<td class='currency' style='font-weight:bold' x:fmla=\"=SUM(D8:D" + iRowID + ")\">" + (sTotalColCurr == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalColCurr).ToString()) + "</td>";
            sReturnTable += "<td class='currency' style='font-weight:bold' x:fmla=\"=SUM(E8:E" + iRowID + ")\">" + (sTotalCol30 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalCol30).ToString()) + "</td>";
            sReturnTable += "<td class='currency' style='font-weight:bold' x:fmla=\"=SUM(F8:F" + iRowID + ")\">" + (sTotalCol60 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalCol60).ToString()) + "</td>";
            sReturnTable += "<td class='currency' style='font-weight:bold' x:fmla=\"=SUM(G8:G" + iRowID + ")\">" + (sTotalCol90 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalCol90).ToString()) + "</td>";
            sReturnTable += "<td class='currency' style='font-weight:bold' x:fmla=\"=SUM(H8:H" + iRowID + ")\">" + (sTotalCol120 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalCol120).ToString()) + "</td>";
            //sReturnTable += "<td class='currency' style='font-weight:bold' >&nbsp;</td>";
            sReturnTable += "<td class='currency' style='font-weight:bold' x:fmla=\"=SUM(I8:I" + iRowID + ")\">" + (sTotalColPay == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalColPay).ToString()) + "</td>";
            sReturnTable += "</tr>";
            // end of total work
        }
        else
        {
            //sReturnTable += "<tr>" + CreateEmptyCells(10) + "</tr>";
            sReturnTable += "<tr class='dullbackground'>" + CreateEmptyCells(10) + "</tr>";
            sReturnTable += "<tr><td style='font-weight:bold'>Currency</td><td style='font-weight:bold'>Total Values</td>" + CreateEmptyCells(8) + "</tr>";
            if (oTotalEuro != 0.0)
                sReturnTable += "<tr class='dullbackground'><td>Euro:</td><td class='currency' style='text-align:left'  x:fmla=\"=SUM(" + sCurrE.Substring(1) + ")\">" + (oTotalEuro == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", oTotalEuro).ToString()) + "</td>" + CreateEmptyCells(8) + "</tr>";
            if (oTotalDOLL != 0.0)
                sReturnTable += "<tr class='dullbackground'><td>Dollar:</td><td class='currency' style='text-align:left' x:fmla=\"=SUM(" + sCurrD.Substring(1) + ")\">" + (oTotalDOLL == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", oTotalDOLL).ToString()) + "</td>" + CreateEmptyCells(8) + "</tr>";
            if (oTotalCAD != 0.0)
                sReturnTable += "<tr class='dullbackground'><td>CAD:</td><td class='currency' style='text-align:left'  x:fmla=\"=SUM(" + sCurrC.Substring(1) + ")\">" + (oTotalCAD == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", oTotalCAD).ToString()) + "</td>" + CreateEmptyCells(8) + "</tr>";
            if (oTotalPOUND != 0.0)
                sReturnTable += "<tr class='dullbackground'><td>Stg:</td><td class='currency' style='text-align:left'  x:fmla=\"=SUM(" + sCurrP.Substring(1) + ")\">" + (oTotalPOUND == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", oTotalPOUND).ToString()) + "</td>" + CreateEmptyCells(8) + "</tr>";
            if (oTotalunknown != 0.0)
                sReturnTable += "<tr class='dullbackground'><td>Error Value:</td><td  x:fmla=\"=SUM(" + sCurrU + ")\">" + String.Format("{0:0.00}", oTotalunknown).ToString() + "</td><td colspan='7'>Please inform software team to fix this issue.</td>" + CreateEmptyCells(1) + "</tr>";
            //sReturnTable += "</tr>";
        }
        if (sErrorRow != "")
            sReturnTable += "<tr><td colspan='5' style='color:red'>Unable to find values for Invoices: " + sErrorRow + " contact software team to solve this issue.</td>" + CreateEmptyCells(5) + "</tr>";
        sReturnTable += "</table>";
        oInvValues = null;
        return sReturnTable;
    }

    private void sRowData(DataRow oRowed, bool bSameCustno)
    {

        Double oInvValue = 0.0;
        string sCurrency = "";
        try
        {
            int iDayCount = ((TimeSpan)DateTime.Now.Date.Subtract(((DateTime)oRowed["IDATE"]))).Days;
            oElem = oRowData.SelectSingleNode("ROW[@INVOICENO = '" + oRowed["IINNO"] + "']");
            if (oRowData.SelectSingleNode("ROW[@INVOICENO = '" + oRowed["IINNO"] + "']") == null)
                oElem = oRowData.SelectSingleNode("ROW[@INVOICEITEM = '" + oRowed["JOURCODE"].ToString().Trim() + "']");

            if (oElem != null)
            {
                oInvValue = Convert.ToDouble(oElem.Attributes.GetNamedItem("INVOICEVALUE").Value.Trim());
                sCurrency = oElem.Attributes.GetNamedItem("INVOICECURRENCY").Value.ToUpper().Trim();
                if (getType.SelectedIndex == 1 && sCurrency.ToUpper().Trim() != "EURO") // euro, do conversion here
                    oInvValue = Convert.ToDouble(decimal.Parse(oInvValue.ToString()) * decimal.Parse(oElem.Attributes.GetNamedItem("CURRENCYRATE").Value.Trim()));
            }
            else
            {   // values missing xml file;
                if (oRowed["IINNO"].ToString() != "-1")
                    sErrorRow += oRowed["IINNO"].ToString() + "; ";
            }
            if (oRowed["PAYMENT"] != DBNull.Value && oRowed["CURRNO"].ToString() != "" && oRowed["CURRNO"].ToString() != "0")
            {
                switch (Convert.ToInt16(oRowed["CURRNO"]))
                {
                    case 3:
                        sCurrency = "Stg";
                        break;
                    case 8:
                        sCurrency = "Dollar";
                        break;
                    case 11:
                        sCurrency = "CAD";
                        break;
                    case 9:
                        sCurrency = "Euro";
                        break;
                }
            }
            if (oRowed["IINNO"].ToString() != "-1")
                switch (iDayCount <= 30 ? 0 : (iDayCount <= 60 ? 30 : (iDayCount <= 90 ? 60 : (iDayCount <= 120 ? 90 : 120))))
                {
                    case 0:
                        sTotalCurr = sTotalCurr + oInvValue;
                        break;
                    case 30:
                        sTotal30 = sTotal30 + oInvValue;
                        break;
                    case 60:
                        sTotal60 = sTotal60 + oInvValue;
                        break;
                    case 90:
                        sTotal90 = sTotal90 + oInvValue;
                        break;
                    default:
                        sTotal120 = sTotal120 + oInvValue;
                        break;
                }
            if (oRowed["PAYMENT"] != DBNull.Value)
            {
                sTotalPay = Convert.ToDouble(oRowed["PAYMENT"].ToString());
                if (getType.SelectedIndex == 1 && sCurrency.ToUpper().Trim() != "EURO") // euro, do conversion here
                    sTotalPay = Convert.ToDouble(decimal.Parse(sTotalPay.ToString()) * decimal.Parse(sGetCurrencyConversion(sCurrency)));
                oInvValue = oInvValue - sTotalPay;
            }
            if (getType.SelectedIndex == 1 && sCurrency.ToUpper().Trim() != "EURO")
                sCurrency = "Euro";
            switch (sCurrency.ToUpper().Trim())
            {
                case "EURO":
                    oTotalEuro += oInvValue;
                    break;
                case "STG":
                    oTotalPOUND += oInvValue;
                    break;
                case "DOLLAR":
                    oTotalDOLL += oInvValue;
                    break;
                case "CAD":
                    oTotalCAD += oInvValue;
                    break;
                default:
                    oTotalunknown += oInvValue;
                    break;
            }
            sSetMyCurrency(sCurrency);
        }
        catch (Exception oex)
        {
            throw oex;
        }
    }

    private void bindcells(string scol)
    {
        sTotalBal = (Convert.ToDouble(String.Format("{0:0.00}", sTotalCurr)) + Convert.ToDouble(String.Format("{0:0.00}", sTotal30)) + Convert.ToDouble(String.Format("{0:0.00}", sTotal60)) + Convert.ToDouble(String.Format("{0:0.00}", sTotal90)) + Convert.ToDouble(String.Format("{0:0.00}", sTotal120))) - Convert.ToDouble(String.Format("{0:0.00}", sTotalPay));
        if (getType.SelectedIndex == 0)
        {
            sReturnTable += "<td style='text-align:center;'>" + sMyCurrency + "</td>";
            sReturnTable += "<td class='currency' x:fmla=\"=SUM(E" + iRowID + ":I" + iRowID + ")-(J" + iRowID + ")\">" + (sTotalBal == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalBal).ToString()) + "</td>";
        }
        else
            sReturnTable += "<td class='currency' x:fmla=\"=SUM(D" + iRowID + ":H" + iRowID + ")-(I" + iRowID + ")\">" + (sTotalBal == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalBal).ToString()) + "</td>";
        sReturnTable += "<td class='currency'  border='1|0'>" + (sTotalCurr == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalCurr).ToString()) + "</td>";
        sReturnTable += "<td class='currency' >" + (sTotal30 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotal30).ToString()) + "</td>";
        sReturnTable += "<td class='currency' >" + (sTotal60 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotal60).ToString()) + "</td>";
        sReturnTable += "<td class='currency' >" + (sTotal90 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotal90).ToString()) + "</td>";
        sReturnTable += "<td class='currency' >" + (sTotal120 == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotal120).ToString()) + "</td>";
        sReturnTable += "<td class='currency' >" + (sTotalPay == 0.0 ? "&nbsp;" : String.Format("{0:0.00}", sTotalPay).ToString()) + "</td>";
        if (getType.SelectedIndex != 0)
            sMyCurrency = "&#8364;";
        // column total count
        sTotalColPay = sTotalColPay + Convert.ToDouble(String.Format("{0:0.00}", sTotalPay));
        sTotalColCurr = sTotalColCurr + Convert.ToDouble(String.Format("{0:0.00}", sTotalCurr));
        sTotalColBal = sTotalColBal + Convert.ToDouble(String.Format("{0:0.00}", sTotalBal));
        sTotalCol90 = sTotalCol90 + Convert.ToDouble(String.Format("{0:0.00}", sTotal90));
        //sTotalCol60 = sTotalCol60 + sTotal60;
        sTotalCol60 = sTotalCol60 + Convert.ToDouble(String.Format("{0:0.00}", sTotal60));
        sTotalCol30 = sTotalCol30 + Convert.ToDouble(String.Format("{0:0.00}", sTotal30));
        sTotalCol120 = sTotalCol120 + Convert.ToDouble(String.Format("{0:0.00}", sTotal120)); ;
        // total row count reset
        sTotalPay = 0.0;
        sTotalCurr = 0.0;
        sTotalBal = 0.0;
        sTotal90 = 0.0;
        sTotal60 = 0.0;
        sTotal30 = 0.0;
        sTotal120 = 0.0;
        switch (sMyCurrency)
        {
            case "&#8364;": sCurrE += ",D" + iRowID.ToString();
                break;
            case "$": sCurrD += ",D" + iRowID.ToString();
                break;
            case "&pound;": sCurrP += ",D" + iRowID.ToString();
                break;
            case "CAD$": sCurrC += ",D" + iRowID.ToString(); ;
                break;
            default: sCurrU += ",D" + iRowID.ToString();
                break;
        }
        iRowID++;
    }

    private string CreateEmptyCells(int icount)
    {
        string scells = "";
        if (getType.SelectedIndex == 1)
            icount = icount - 1;
        for (int i = 0; i < icount; i++)
            scells += "<td>&nbsp;</td>";
        return scells;
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=Aged Analysis - " + sCurrencyReport + ".xls");
        this.EnableViewState = false;
        Response.Write("<HTML xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\"><head>");
        Response.Write("<style>table{border:.5pt solid windowtext;font-family:verdana, arial; font-size:10px;}");
        Response.Write("td{border-collapse:collapse; border:.5pt solid windowtext;}.currency{mso-number-format:Fixed;text-align:right;}.cellhead{text-align:center; vertical-align:middle;font-weight:bold;background:lightgrey}</style>");
        Response.Write("</style>");
        Response.Write("</head><body>");
        Response.Write(excelTable.InnerHtml);
        Response.Write("</body>");
        Response.Write("</html>");
        Response.End();
    }

    private string sGetCurrencyConversion(string sCurrencyType)
    {
        DataSet ds = new DataSet();
        string sValue = "0";
        if (sCurrencyType.ToLower() == "euro" || sCurrencyType.ToLower() == "&euro;")
            sValue = "1";
        else if (sCurrencyType.ToLower() == "dollar" || sCurrencyType.ToLower() == "&dollar;")
            sValue = "4";
        else if (sCurrencyType.ToLower() == "stg" || sCurrencyType.ToLower() == "&pound;" || sCurrencyType.ToLower() == "&stg;")
            sValue = "5";
        else if (sCurrencyType.ToLower() == "cad" || sCurrencyType.ToLower() == "&cad")
            sValue = "8";
        if (sValue != "0" && sValue != "1")
        {
            ds = obj.GetCurrencyConversion(Convert.ToInt16(sValue));
            sValue = ds.Tables[0].Rows[0][2].ToString();
        }
        ds = null;
        return sValue;
    }
}
