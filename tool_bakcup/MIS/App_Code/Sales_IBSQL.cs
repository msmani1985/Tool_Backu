using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Sales_IBSQL
/// </summary>
public class Sales_IBSQL
{
	public Sales_IBSQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, int bookno, int projectno, string fDate, string tDate)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
        TandFInvoiceDS ds = new TandFInvoiceDS();
       // ds = dib.GetInvoicedJobs3(custno, journo, fDate, tDate);
        dib = null;
        return ds;
    }
}