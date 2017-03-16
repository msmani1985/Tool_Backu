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
//using System.Linq;


/// <summary>
/// Summary description for Invoiced_IBSQL
/// </summary>
public class Invoiced_IBSQL
{
	public Invoiced_IBSQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataSet GetDespatchedJobs1(int custno, int journo)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        DataSet ds = new DataSet();
        ds = dib.GetDespatchedJobs1(custno, journo);
        dib = null;
        return ds;
    }

    public DataSet GetDespatchedJobs2(int custno, int journo)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        DataSet ds = new DataSet();
        ds = dib.GetDespatchedJobs2(custno, journo);
        dib = null;
        return ds;
    }

    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, int bookno, int projectno, string fDate, string tDate)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }
    public TandFInvoiceDS GetInvoicedJobs_YTD(int custno, int journo, int bookno, int projectno, string fDate, string tDate)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs_YTD(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }
    public TandFInvoiceDS GetInvoicedJobs4(int custno, int journo, int bookno, int projectno, string fDate, string tDate)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs4(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }

    //For location
    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, int bookno, int projectno, string fDate, string tDate, int location)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3(custno, journo, fDate, tDate, location);
        dib = null;
        return ds;
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

    public TandFInvoiceDS GetInvoicedJobs3Outstanding(int custno, int journo, int bookno, int projectno)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3Outstanding(custno, journo);
        dib = null;
        return ds;
    }

    public TandFInvoiceDS GetInvoicedJobs3PaymentReceived(int custno, int journo, int bookno, int projectno, string fDate, string tDate)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
        ds = dib.GetInvoicedJobs3PaymentReceived(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }

    public bool HasPayments(string sCustno)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        return dib.HasPayments(sCustno);
    }

    public bool ApprovePayments(System.Collections.ArrayList acceptlist, string sModifiedBy)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        return dib.ApprovePayments(acceptlist, sModifiedBy);
    }
    public bool CancelPayments(System.Collections.ArrayList cancellist, string sModifiedBy)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
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
}