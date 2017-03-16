using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.SessionState;  
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for IB_bs_ds
/// </summary>
public class biz_IB
{
    string sTestFile = "test.html";
    string sVirtualPath = @"D:\sivaraj\projects\dotnet web app\datapagemis\InvoiceTemplates\";

	public biz_IB()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void sServerPath(string sSerPath)
    {
        sVirtualPath = sSerPath;
    }
    
    //invoice report  screen 3
    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, int bookno, int projectno, string fDate, string tDate )
    {
        datasourceIB dib = new datasourceIB();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }
    public TandFInvoiceDS Getsalesjob(int custno, int journo, string fdate, string tdate)
    {
        datasourceIB dib = new datasourceIB();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.Getsalesjob(custno, journo, fdate, tdate);
        dib = null;
        return ds;
    }
    //For location
    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, int bookno, int projectno, string fDate, string tDate,int location)
    {
        datasourceIB dib = new datasourceIB();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3(custno, journo, fDate, tDate,location);
        dib = null;
        return ds;
    }
    
    public bool HasPayments(string sCustno)
    {
        datasourceIB dib = new datasourceIB();
        return dib.HasPayments(sCustno);
    }
    public TandFInvoiceDS GetInvoicedJobs3Outstanding(int custno, int journo, int bookno, int projectno)
    {
        datasourceIB dib = new datasourceIB();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3Outstanding(custno, journo);
        dib = null;
        return ds;
    }

    public TandFInvoiceDS GetInvoicedJobs3PaymentReceived(int custno, int journo, int bookno, int projectno, string fDate, string tDate)
    {
        datasourceIB dib = new datasourceIB();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3PaymentReceived(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }
    public bool ApprovePayments(System.Collections.ArrayList acceptlist, string sModifiedBy)
    {
        datasourceIB dib = new datasourceIB();
        return dib.ApprovePayments(acceptlist, sModifiedBy);
    }
    public bool CancelPayments(System.Collections.ArrayList cancellist, string sModifiedBy)
    {
        datasourceIB dib = new datasourceIB();
        return dib.CancelPayments(cancellist, sModifiedBy);
    }
    public DataSet getCrediOnAccount(string sCustno)
    {
        datasourceIB dib = new datasourceIB();
        return dib.getCustomerPaymentOnAccount(sCustno);
    }
    public bool AddUpdatePaymentOnAccount(System.Collections.ArrayList list, string sModifiedBy)
    {
        datasourceIB dib = new datasourceIB();
        return dib.AddUpdatePaymentOnAccount(list, sModifiedBy);
    }
    public bool DeletePaymentOnAccount(string sCreditID, string sModifiedBy)
    {
        datasourceIB dib = new datasourceIB();
        return dib.DeletePaymentOnAccount(sCreditID, sModifiedBy);
    }
    public string GetCurrencyConversion(string sCurrencyType)
    {
        datasourceIB dib = new datasourceIB();
        DataSet ds = new DataSet();
        try
        {
            string sValue = "0";
            if (sCurrencyType.ToLower().Trim() == "euro" || sCurrencyType.ToLower().Trim() == "&euro;" || sCurrencyType.ToLower().Trim() == "inr")
                sValue = "1";
            else if (sCurrencyType.ToLower().Trim() == "dollar" || sCurrencyType.ToLower().Trim() == "&dollar;")
                sValue = "4";
            else if (sCurrencyType.ToLower().Trim() == "stg" || sCurrencyType.ToLower().Trim() == "&pound;" || sCurrencyType.ToLower().Trim() == "&stg;")
                sValue = "5";
            else if (sCurrencyType.ToLower().Trim() == "cad" || sCurrencyType.ToLower().Trim() == "&cad")
                sValue = "8";
            if (sValue != "0" && sValue != "1")
            {
                ds = dib.GetCurrencyConversion(Convert.ToInt16(sValue));
                dib = null;
                sValue = ds.Tables[0].Rows[0][2].ToString();
            }
            if (sValue == "0")
                throw new ArgumentException("Currency rate is 0, So not enable to generate invoice");
            return sValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ds = null;
            dib = null;
        }
    }

    // for approval screen 1 
    public DataSet GetDespatchedJobs1(int custno, int journo)
    {
        datasourceIB dib = new datasourceIB();
        DataSet ds = new DataSet();
        ds = dib.GetDespatchedJobs1(custno, journo);
        dib = null;
        return ds;
    }
    // set approval screen 1
    public bool SetApprovedJobs(int empid, int iInumber,int Category,string comname)
    {
        bool bApproved = true;
        datasourceIB dib = new datasourceIB();
        bApproved = dib.UpdateApproveName(empid, iInumber,Category,comname);
        dib = null;
        return bApproved;
    }
    // for approved screen 2 
    public DataSet GetDespatchedJobs2(int custno, int journo)
    {
        datasourceIB dib = new datasourceIB();
        DataSet ds = new DataSet();
        ds = dib.GetDespatchedJobs2(custno, journo);
        dib = null;
        return ds;
    }

    public DataSet Get_Query_Result()
    {
        datasourceIB dib = new datasourceIB();
        DataSet ds = new DataSet();
        //ds = dib.Get_Query_Result();
        dib = null;
        return ds;
    }

    public string GetInvoiceHTML(string sHTMLContent, int custno, int journo, string iissueno, int ino, string sLocation)
    {
        return "test";
    }

    public string GetInvHeaders(string sHTMLContent, int custno, int journo, string iissueno, int ino, string sLocation)
    {
        datasourceIB dib = new datasourceIB();
        XmlDocument oDom = new XmlDocument ();
        XmlNode oTestNode = null;

        string jcnoCode = "";
        string SamPriceCode = "";
        string CopyEditCode = "";
        string jcpPrice = "";
        string sCurrencyType = "&euro;";
        string sPageFormat = "";
        string sRefReDraw = "";
        string sEditedPages = "";
        string sCopyEditPages = "";
        string sCopyEditPages1 = "";
        string sUnEditedPages = "";
        string sManuscript = "";
        string sNoofArticles = "";
        string sArticleBased = "";
        double sCalculateTotal = 0.0;
        string sJourcode = "";
        string sSite = @"India\";
        string sInvoiceDate = "";
        string sInvoiceNo = "";
        string sCurrencyRate = "";

        oDom = dib.GetInvHeaders(custno, journo);

        if (sLocation == "d")
        {
            dib.UpdateInvDate(ino);
        }
        if (sLocation != "i")
            sSite = @"Dublin\";
        if (oDom != null)
        {
            foreach(XmlNode oNode in oDom.DocumentElement.FirstChild.ChildNodes)
            {
                sHTMLContent = sHTMLContent.Replace("[" + oNode.Name + "]", oNode.InnerText.Trim());
            }

        }
        //GET SAM PRICE VALUE, JCNO VALUE, COYEDIT VALUE FROM

        //jcno value

        oTestNode = oDom.DocumentElement.FirstChild.SelectSingleNode("JCNO" + DateTime.Now.Year);
        if (oTestNode == null)
            return "Price Code Not Found in DB for Journal/Job" + iissueno;
        else
        {
            if (oTestNode.InnerText == "")
                return "Price Code Not Found in DB for Journal/Job" + iissueno;
            else
                jcnoCode = oTestNode.InnerText.Trim();
        }
        SamPriceCode = GetNullValue(oDom.DocumentElement.SelectSingleNode("//SAMCODE"));
        CopyEditCode = GetNullValue(oDom.DocumentElement.SelectSingleNode("//ISCOPYEDITED"));
        sPageFormat = GetNullValue(oDom.DocumentElement.SelectSingleNode("//PAGEFORMAT")).ToUpper() ;
        sArticleBased = GetNullValue(oDom.DocumentElement.SelectSingleNode("//ISARTICLE_BASED"));
        sJourcode = GetNullValue(oDom.DocumentElement.SelectSingleNode("//JOURCODE"));

        // values from journal price xml //
        if (CopyEditCode == "1")
        {
            if (sPageFormat == "small".ToUpper())
                oTestNode = GetPriceCode(sLocation, 140);
            else if (sPageFormat == "large".ToUpper())
                oTestNode = GetPriceCode(sLocation, 139);
            if (oTestNode == null)
                return "Price Code for copy-editing not found in XML file for Journal/Job" + iissueno;
            if (sLocation == "i")
                CopyEditCode = oTestNode.Attributes.GetNamedItem("INDIAPRICE").Value;
            else
                CopyEditCode = oTestNode.Attributes.GetNamedItem("JCPPRICE").Value;
        }

        if (SamPriceCode != "" && SamPriceCode != "0")
        {
            oTestNode = GetPriceCode(sLocation, Convert.ToInt16(SamPriceCode));
            if (oTestNode == null)
                return "SAM Price Code Not Found in XML file for Journal/Job" + iissueno;
            if (sLocation == "i")
                SamPriceCode = oTestNode.Attributes.GetNamedItem("INDIAPRICE").Value;
            else
                SamPriceCode = oTestNode.Attributes.GetNamedItem("JCPPRICE").Value;
        }

        oTestNode = GetPriceCode(sLocation ,Convert.ToInt16(jcnoCode));
        if (oTestNode == null)
            return "Price Code Not Found in XML file for Journal/Job" + iissueno;

        if (sLocation != "i")
            jcpPrice = oTestNode.Attributes.GetNamedItem("JCPPRICE").Value;
        else
            jcpPrice = oTestNode.Attributes.GetNamedItem("INDIAPRICE").Value;

        if (sLocation != "i")
            sCurrencyType = GetCurrencyType(oTestNode);


        oDom = getInvDetails(custno, iissueno, ino);

        if (oDom != null)
        {

            sRefReDraw = GetNullValue(oDom.DocumentElement.SelectSingleNode("//REDRAWCOUNT"));
            sEditedPages = GetNullValue(oDom.DocumentElement.SelectSingleNode("//EDITEDDISK"));
            sCopyEditPages = GetNullValue(oDom.DocumentElement.SelectSingleNode("//COPYEDITPAGES"));
            sNoofArticles = GetNullValue(oDom.DocumentElement.SelectSingleNode("//NOOFARTICLE"));
            sManuscript = GetNullValue(oDom.DocumentElement.SelectSingleNode("//MANUSCRIPT"));
            sCopyEditPages1 = GetNullValue(oDom.DocumentElement.SelectSingleNode("//COPYEDITPAGES1"));
            sUnEditedPages = GetNullValue(oDom.DocumentElement.SelectSingleNode("//UNEDITEDDISK"));
            sInvoiceNo = GetNullValue(oDom.DocumentElement.SelectSingleNode("//INVNO"));
            sInvoiceDate = GetNullValue(oDom.DocumentElement.SelectSingleNode("//INVDATE"));

        }
        if (sInvoiceDate != "" && sInvoiceDate != "0")
        {
            DateTime oDT = Convert.ToDateTime(sInvoiceDate);
            sInvoiceDate = oDT.Day + String.Format("{0: MMMM }",oDT) + oDT.Year ; 
            sHTMLContent = sHTMLContent.Replace("[INVOICE_DATE]", sInvoiceDate);
        }
        if (sInvoiceNo != "" && sInvoiceNo != "0")
            sHTMLContent = sHTMLContent.Replace("[INVOICE_NUMBER]", sInvoiceNo);
        
        if (CopyEditCode == "" || CopyEditCode == "0")
        {
            sCopyEditPages = "";
            sRefReDraw = "";
        }
        string sHTMLRow = "";
        string sExcelRow = "";
        string sVolume = "";
        string sIssue = "";
        string sJourcodeRev = "";
        int iLen = 0;
        //String.Format("{0:0.00}", 123.4567);
        if (iissueno.IndexOf("/") > 0)
        {

            sVolume = iissueno.Substring(0, iissueno.IndexOf("/")).Trim();
            sIssue = iissueno.Substring(iissueno.IndexOf("/") + 1).Trim();
        }
        else
        {
            sVolume = iissueno;
            sIssue = "";
        }
        
        //XLS Table starts here for T&F
        iLen = sJourcode.Length;
        while (iLen > 0)
        {
            sJourcodeRev += sJourcode.Substring(iLen - 1, 1);
            iLen--;
        }
        

        sExcelRow += "<table width='100%'  border='1' cellspacing='0' cellpadding='0' height='360px'>";
        sExcelRow += "<tr>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;border-top:2px solid black;border-bottom:2px solid black;height:50px;font-weight:bold'>Element 1 </TH>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;border-top:2px solid black;border-bottom:2px solid black;height:50px;font-weight:bold'>Element 2 </TH>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;border-top:2px solid black;border-bottom:2px solid black;height:50px;font-weight:bold'>Element 3 </TH>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;border-top:2px solid black;border-bottom:2px solid black;height:50px;font-weight:bold'>Element 4 </TH>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;border-top:2px solid black;border-bottom:2px solid black;height:50px;font-weight:bold'>Element 5 </TH>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;text-align:right;padding-right:20px;width:80px;font-weight:bold;border-top:2px solid black;border-bottom:2px solid black'>Value</TH>";
        sExcelRow += "<TH align='left' style='vertical-align:middle;border-top:2px solid black;border-bottom:2px solid black;height:50px;font-weight:bold'>Description</TH>";
        sExcelRow += "</tr>";
        
        sHTMLContent = sHTMLContent.Replace("[INVOICE_DATE]", DateTime.Now.Day + " " + DateTime.Now.ToString("MMMMMMMMMMMM") + " " + DateTime.Now.Year);  
        sHTMLRow = sHTMLContent;

        //draw rows and create excel//
        if (sArticleBased == "1") // article based
        {

        }
        else // pages based
        {
            sExcelRow += ""; //"<TR><TD style=\"font-weight:bold;vertical-align:middle\" colspan=\"7\" height=\"100px\">" + sJourcode + " Volume " + sVolume + ", Issue " + sIssue;
            sExcelRow += ""; //"<BR/>" + sEditedPages + " Pages @ " + sCurrencyType + jcpPrice + "</TD></TR>";
        }

        sHTMLRow = sHTMLRow.Replace("[TITLE_ROW]", sJourcode + " Volume " + sVolume + ", Issue " + sIssue + "<BR/>" + sEditedPages + " Pages @ " + sCurrencyType + jcpPrice);

        if (sVolume.Length == 1)
            sVolume = "00" + sVolume;
        else if (sVolume.Length == 2)
            sVolume = "0" + sVolume;
        if (sIssue.Length == 1)
            sIssue = "0" + sIssue;

        sHTMLRow = sHTMLRow.Replace("[TYPESET_1]", "3300").Replace("[TYPESET_2]", "PU" + sJourcode).Replace("[TYPESET_3]", "MXXX").Replace("[TYPESET_4]", "V" + sVolume + sIssue).Replace("[TYPESET_5]", "WU60").Replace("[TYPESET_6]", sCurrencyType + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(jcpPrice) * Convert.ToDouble(sEditedPages))));


        sExcelRow += "<TR style=\"height:40px\"><TD style='text-align:left' align='left' height=\"40px\">3300</TD><TD >PU" + sJourcode + "</TD><TD >MXXX</TD><TD >V" + sVolume + sIssue + "</TD><TD >WU60</TD><TD style=\"text-align:right;padding-right:20px;width:80px\">&nbsp;" + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(jcpPrice) * Convert.ToDouble(sEditedPages))) + "</TD><TD>" + sJourcodeRev + sVolume + sIssue + "</TD></TR>";
        sCalculateTotal = sCalculateTotal + (Convert.ToDouble(jcpPrice) * Convert.ToDouble(sEditedPages));

        if (CopyEditCode != "" && CopyEditCode != "0")
        {
            sHTMLRow = sHTMLRow.Replace("[COPYEDIT_1]", "3300").Replace("[COPYEDIT_2]", "PU" + sJourcode).Replace("[COPYEDIT_3]", "MXXX").Replace("[COPYEDIT_4]", "V" + sVolume + sIssue).Replace("[COPYEDIT_5]", "WU74").Replace("[COPYEDIT_6]", sCurrencyType + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(CopyEditCode) * Convert.ToDouble(sEditedPages))));

            sExcelRow += "<TR style=\"height:40px\"><TD style='text-align:left;' height=\"40px\">3300</TD><TD >PU" + sJourcode + "</TD><TD >MXXX</TD><TD >V" + sVolume + sIssue + "</TD><TD >WU74</TD><TD style=\"text-align:right;padding-right:20px;width:80px\">&nbsp;" + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(CopyEditCode) * Convert.ToDouble(sEditedPages))) + "</TD><TD>" + sJourcodeRev + sVolume + sIssue + "</TD></TR>";
            sCalculateTotal = sCalculateTotal + (Convert.ToDouble(CopyEditCode) * Convert.ToDouble(sCopyEditPages));
        }
        else
            sHTMLRow = sHTMLRow.Replace("[COPYEDIT_1]", "&nbsp;").Replace("[COPYEDIT_2]", "&nbsp;").Replace("[COPYEDIT_3]", "&nbsp;").Replace("[COPYEDIT_4]", "&nbsp;").Replace("[COPYEDIT_5]", "&nbsp;").Replace("[COPYEDIT_6]", "&nbsp;");

        if (SamPriceCode != "" && SamPriceCode != "0")
        {
            sHTMLRow = sHTMLRow.Replace("[SAMTITLE]", "Additional SAM Charges<BR/>" + sEditedPages + " Pages @ " + sCurrencyType + SamPriceCode).Replace("[SAM_VALUE]", sCurrencyType + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(SamPriceCode) * Convert.ToDouble(sEditedPages))));

            sExcelRow += "<TR><TD colspan=\"5\" style=\"font-weight:bold;vertical-align:bottom\" height=\"100px\">Additional SAM Charages";
            //sExcelRow += "<BR/>" + sEditedPages + " Pages @ " + sCurrencyType + SamPriceCode ;
            sExcelRow += "</TD><TD style=\"vertical-align:bottom;text-align:right;padding-right:20px;width:80px\">&nbsp;" + String.Format("{0:0.00}", Convert.ToDouble(Convert.ToDouble(SamPriceCode) * Convert.ToDouble(sEditedPages))) + "</TD><TD>" + sJourcodeRev + sVolume + sIssue + "</TD></TR>";
            sCalculateTotal = sCalculateTotal + (Convert.ToDouble(SamPriceCode) * Convert.ToDouble(sEditedPages ));
        }
        else
            sHTMLRow = sHTMLRow.Replace("[SAMTITLE]", "&nbsp;").Replace("[SAM_VALUE]", "&nbsp;");
        
        sHTMLRow = sHTMLRow.Replace("[TOTAL_VALUE]", sCurrencyType + String.Format("{0:0.00}", Convert.ToDouble(sCalculateTotal)).ToString());

        oDom = null;
        dib = null;
        //create HTML file here
        //sHTMLRow += sTitleRow + sTypeSetRow + sCopyRow + sSAMRow ;
        sExcelRow += "<tr><td COLSPAN=\"7\" >&nbsp;</td></tr>";
        sExcelRow += "<tr><td COLSPAN=\"7\" >&nbsp;</td></tr>";
        sExcelRow += "<tr><td COLSPAN=\"7\" >&nbsp;</td></tr>";
        sExcelRow += "<tr><td COLSPAN=\"4\"></td><td>&nbsp;</td><td style=\"border-top:2px solid black;padding-right:20px;width:80px;text-align:right;\">&nbsp;[TOTAL_VALUE]</td></tr></table>";
        sExcelRow = sExcelRow.Replace("[TOTAL_VALUE]", String.Format("{0:0.00}", Convert.ToDouble(sCalculateTotal)).ToString());
        WriteFile(sVirtualPath + sSite + "\\output\\" + sJourcode.Trim() + iissueno.Replace("/", "_").Trim() + ".xls", sExcelRow);
        WriteFile(sVirtualPath + sSite + "\\output\\" + sJourcode.Trim() + iissueno.Replace("/", "_").Trim() + ".doc", sHTMLRow);

        UpdateInvoiceXML(sLocation, sInvoiceNo, sInvoiceDate, sCalculateTotal.ToString(), GetCurrencyName(sCurrencyType), "0", GetCurrencyConversion(sCurrencyType), sJourcode + iissueno);

        //return CreatePDFandXLS(sHTMLContent, sVirtualPath + sSite + "\\output\\" + sJourcode.Trim() + iissueno.Replace("/", "_").Trim() + ".doc", iissueno, sJourcode) + sHTMLRow;
        return sHTMLRow;
    }
    
    private XmlDocument getInvDetails(int custno, string iissueno, int ino)
    {
        datasourceIB dib = new datasourceIB();
        return dib.GetInvDetails(custno, iissueno, ino);
    }

    public XmlNode GetPriceCode(string sLocation, int jcpno)
    {
        XmlDocument oDom = new XmlDocument();
        XmlNode oNode = null;
        string sSite = @"India\";
        try
        {
            if (sLocation != "i")
                sSite = @"Dublin\";

            oDom.Load(sVirtualPath + sSite + "journal_prices_2008.xml");
            oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@JCPNO = '" + jcpno + "']");
            return oNode;
        }
        catch (Exception oex)
        {
           return null;
        }
        finally
        {
            oDom = null;
        }
    }

//    public XmlNode UpdateInvoiceXML (string sLocation, string invoiceno, string dInvDate, string sInvValue, string sInvCurrency, string sPrintValue, string sCurrRate, string sInvItem)
    public string UpdateInvoiceXML(string sLocation, string invoiceno, string dInvDate, string sInvValue, string sInvCurrency, string sPrintValue, string sCurrRate, string sInvItem)
    {
        XmlDocument oDom = new XmlDocument();
        XmlNode oNode = null;
        XmlElement oElem = null;
        int sNodeValue = 0;
        string sSite = @"India\";
        try
        {
            //Subbu
            /*if (sLocation != "i")
                sSite = @"Dublin\";
             oDom.Load(sVirtualPath + sSite + "invoice_values.xml");
            */
            oDom.Load(sLocation);

            oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM = '" + sInvItem.Trim() + "']");

            if (oNode != null)
                oDom.DocumentElement.SelectSingleNode("ROWDATA").RemoveChild(oNode);

            oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").LastChild;
            sNodeValue = Convert.ToInt16(oNode.Attributes.GetNamedItem("INVNO").Value) + 1;
            oElem = oDom.CreateElement("ROW");
            oElem.SetAttribute("INVNO", sNodeValue.ToString() );
            oElem.SetAttribute("INVOICEITEM", sInvItem.Trim());
            oElem.SetAttribute("INVOICENO", invoiceno);
            if (dInvDate.ToString() != "")
            {
                DateTime oDT = Convert.ToDateTime(dInvDate.ToString());
                //dInvDate = Convert.ToString(oDT.Year + oDT.Month + oDT.Day);
                //dInvDate = Convert.ToString(oDT.Year) + Convert.ToString(oDT.Month) + Convert.ToString(oDT.Day);
                dInvDate = string.Format("{0:yyyy}", oDT) + string.Format("{0:MM}", oDT) + string.Format("{0:dd}", oDT);
            }
            oElem.SetAttribute("INVOICEDATE", dInvDate);
            oElem.SetAttribute("INVOICEVALUE", sInvValue.Trim());
            oElem.SetAttribute("INVOICECURRENCY", sInvCurrency.Trim());
            oElem.SetAttribute("PRINTVALUE", sPrintValue.Trim());
            oElem.SetAttribute("CURRENCYRATE", sCurrRate.Trim());
            oDom.DocumentElement.SelectSingleNode("//ROWDATA").AppendChild(oElem);
            //oDom.Save(sVirtualPath + sSite + "invoice_values.xml");
            oDom.Save(sLocation);
            //return oNode;
            return "";
        }
        catch (Exception oex)
        {
            return "Unable to update Invoice value, Please relogin and generate";
            //return oex.Message.ToString();
        }
        finally
        {
            oDom = null;
        }
    }

    public string UpdateInvoicevalueonly(string sLocation, string invoiceno, string dInvDate, string sInvValue, string sInvCurrency, string sPrintValue, string sCurrRate, string sInvItem)
    {
        XmlDocument oDom = new XmlDocument();
        XmlNode oNode = null;
        int sNodeValue = 0;
        XmlElement oElem = null;
        try
        {
            oDom.Load(sLocation);
            
            if(invoiceno!="" && invoiceno!=null)
                oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICENO='" + invoiceno.Trim() + "']");
            if(oNode==null)
                oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM = '" + sInvItem.Trim() + "']");
            if (dInvDate.ToString() != "")
            {
                DateTime oDT = Convert.ToDateTime(dInvDate.ToString());
                //dInvDate = Convert.ToString(oDT.Year + oDT.Month + oDT.Day);
                //dInvDate = Convert.ToString(oDT.Year) + Convert.ToString(oDT.Month) + Convert.ToString(oDT.Day);
                dInvDate = string.Format("{0:yyyy}", oDT) + string.Format("{0:MM}", oDT) + string.Format("{0:dd}", oDT);
            }
            if (oNode != null)
            {
                //oNode.Attributes.Item["INVOICEVALUE"].Value = sInvValue;//Update Invoicevalue
                oNode.Attributes["INVOICEVALUE"].Value = sInvValue.Trim();//Update Invoicevalue
                oNode.Attributes["INVOICEDATE"].Value = dInvDate;
                oNode.Attributes["INVOICENO"].Value = invoiceno;
                oNode.Attributes["INVOICEITEM"].Value = sInvItem.Trim();
                oNode.Attributes["INVOICECURRENCY"].Value = sInvCurrency.Trim();

                oDom.Save(sLocation);
            }
            else //if not exist create new entry
            {
                oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").LastChild;
                if (oNode != null)
                    sNodeValue = Convert.ToInt16(oNode.Attributes.GetNamedItem("INVNO").Value) + 1;
                else
                    sNodeValue = 1;
                oElem = oDom.CreateElement("ROW");
                oElem.SetAttribute("INVNO", sNodeValue.ToString());
                oElem.SetAttribute("INVOICEITEM", sInvItem.Trim());
                oElem.SetAttribute("INVOICENO", invoiceno);
                
                oElem.SetAttribute("INVOICEDATE", dInvDate);
                oElem.SetAttribute("INVOICEVALUE", sInvValue.Trim());
                oElem.SetAttribute("INVOICECURRENCY", sInvCurrency.Trim());
                oElem.SetAttribute("PRINTVALUE", sPrintValue.Trim());
                oElem.SetAttribute("CURRENCYRATE", sCurrRate.Trim());
                oDom.DocumentElement.SelectSingleNode("//ROWDATA").AppendChild(oElem);
                //oDom.Save(sVirtualPath + sSite + "invoice_values.xml");
                oDom.Save(sLocation);
            }
            //return oNode;
            return "";
        }
        catch (Exception oex)
        {
            return "Unable to update Invoice value, Please relogin and generate";
            //return oex.Message.ToString();
        }
        finally
        {
            oDom = null;
        }
    }

    public string GetCurrencyType(XmlNode oONode)
    {
        string sCcode = "";
        if (oONode != null)
        {
            try
            {
                sCcode = oONode.Attributes.GetNamedItem("CURRENCY").Value.ToUpper();
            }
            catch (Exception oex)
            {
                sCcode = oONode.Attributes.GetNamedItem("INVOICECURRENCY").Value.ToUpper(); 
            }

            if (sCcode == "Stg".ToUpper())
                sCcode = "&pound;";
            if (sCcode == "Dollar".ToUpper() || sCcode == "CAD".ToUpper())
                sCcode = "&#36;";
            if (sCcode == "Euro".ToUpper())
                sCcode = "&euro;";
            if (sCcode == "INR".ToUpper())
                sCcode = "INR";
        }
        return sCcode;
    }

    public string GetCurrencyName(string sCurrencyType)
    {
        if (sCurrencyType == "&pound;".ToUpper())
            sCurrencyType = "Stg";
        else if (sCurrencyType == "&#36;" )
            sCurrencyType = "Dollar";
        else if (sCurrencyType == "&euro;" || sCurrencyType=="euro")
            sCurrencyType = "Euro";
        else if (sCurrencyType == "INR".ToUpper())
            sCurrencyType = "INR";
        else
            sCurrencyType="";
        return sCurrencyType;
    }

    public string GetNullValue(XmlNode oNullNode)
    {
        if (oNullNode != null)
            return oNullNode.InnerText.Trim();
        else
            return "";

    }

    public string GetAttributeValue(XmlNode oINode, string sAttrName)
    {
        string sCcode = "0.00";
        if (oINode != null)
        {
            try
            {
                sCcode = oINode.Attributes.GetNamedItem(sAttrName).Value;

            }
            catch (Exception oex)
            {
                try
                {
                    sCcode = oINode.OuterXml;
                    sCcode = sCcode.Substring(sCcode.LastIndexOf("INVOICEVALUE=\"") + "INVOICEVALUE=\"".Length);
                    sCcode = sCcode.Substring(0, sCcode.IndexOf("\""));
                }
                catch (Exception oxex)
                { sCcode = "0.00"; }
            }
        }
        return sCcode;
    }

    public string oldCreatePDFandXLS(string sContent, string sFileName, string iissueno, string jourcode)
    {

        Type WordType = Type.GetTypeFromProgID("Word.Application");
        Object WordApp = Activator.CreateInstance(WordType);
        try
        {
            string printFileName = sFileName.Replace(".doc","") ;

            if (File.Exists(printFileName + ".pdf"))
                File.Delete(printFileName + ".pdf"); 
            WordType.InvokeMember("Visible", BindingFlags.SetProperty, null, WordApp, new object[] { true });
            Object WordDoc = WordApp.GetType().InvokeMember("Documents", BindingFlags.GetProperty, null, WordApp, null);

            WordDoc = WordDoc.GetType().InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc, new Object[] { sFileName, false, false });

            //WordDoc = WordDoc.GetType().InvokeMember("Add", BindingFlags.InvokeMethod,  null, WordDoc, null);
            //WordType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc, new object[] { sFileName, 0 });

            WordDoc.GetType().InvokeMember("PrintOut", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc, new object[] { false, false, new object[] { 1 }, printFileName.Replace(".html", "") });

            WordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, WordApp, null);
            //    this.doReplace(saveFileName);

            return "successfully PDF Generated!";
        }
        catch (Exception oex) 
        {
            WordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, WordApp, null);
            return oex.InnerException.Message; 
        }
    }

    public string CreatePDFandXLS(string sContent, string sFileName, string iissueno, string jourcode)
    {
        return "no";
        /*
        Word.ApplicationClass oWord;
        oWord = new Word.ApplicationClass();
        Word.Document oDoc;
        object oMissing = Type.Missing;
        object oTrue = true;
        object oFalse = false;

        try
        {
            string printFileName = sFileName.Replace(".doc", "");

            if (File.Exists(printFileName + ".pdf"))
                File.Delete(printFileName + ".pdf");
            oWord.ScreenUpdating = true;
            oWord.Visible = true;
            object oInFile = sFileName;
            object oOutFile = printFileName ;
            oDoc = oWord.Documents.Open(ref oInFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            oDoc.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oOutFile, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            //oDoc.Close(ref oFalse, ref oMissing, ref oMissing);
            if (oWord != null)
            {
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
                oWord = null;
            }
            return "PDF Successfully Created";
        }
        catch (Exception oex)
        {
            if (oWord != null)
            {
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
                oWord = null;
            }
            return "Error in Creating PDF, contact software team. " ;

        }
        finally
        {
            if (oWord != null)
            {
                oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
                oWord = null;
            }
        }
          */

    }

    private bool WriteFile(string sFilename, string sContent)
    {
        TextWriter oTW = null;
        try
        {
            //if (sFilename.IndexOf(".html") > 0)
            //    sFilename = sFilename.Replace(".html", ".doc");
            if (File.Exists(sFilename))
                File.Delete(sFilename);
            oTW = new StreamWriter(sFilename);
            oTW.Write(sContent);
            oTW.Close();
            oTW = null;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally { oTW = null; }
    }

    //public bool UpdateInvCompleted(int ino, string sjourcode, string issueno, string sLocation)
    public bool UpdateInvCompleted(string ino, int custno, int Email_Flag)
    {
        datasourceIB dib = new datasourceIB();
        try
        {
            return dib.UpdateInvCompleted(ino, custno, Email_Flag);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { dib = null; }

    }
    public bool UpdatewipInvCompleted(string wno, int custno, int Email_Flag)
    {
        datasourceIB dib = new datasourceIB();
        try
        {
            return dib.UpdatewipInvCompleted(wno, custno, Email_Flag);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { dib = null; }

    }


}
