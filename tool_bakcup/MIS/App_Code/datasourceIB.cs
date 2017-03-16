using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;


/// <summary>
/// Summary description for DatasourceIB
/// </summary>

public class datasourceIB
{
    OdbcConnection oConn = null;
    string sConStr = "";

	public datasourceIB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private void opencon()
    {
        //sConStr = ConfigurationManager.ConnectionStrings["conStrIB"].ToString();
        sConStr = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ToString();
        oConn = new OdbcConnection(sConStr);
        oConn.Open();
    }

    private void closecon()
    {
        if (oConn != null)
        {
            if (oConn.State != ConnectionState.Closed ) 
                oConn.Close();
            oConn.Dispose();
        }
    }



    private void addParamsSTR(OdbcCommand oCmmd, string sName, string sValue, OdbcType sDBType, int sLen, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new OdbcParameter(sName, sDBType, sLen));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }

    private void addParamsINT(OdbcCommand oCmmd, string sName, int sValue, OdbcType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new OdbcParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }

    private void addParamsDateTime(OdbcCommand oCmmd, string sName, string sValue, OdbcType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new OdbcParameter(sName, sValue ));
        //oCmmd.Parameters[sName].Value = DateTime.Parse(sValue).ToShortDateString() ;
        //oCmmd.Parameters[sName].Direction = sDirection;
    }

    public TandFInvoiceDS Getsalesjob(int custno, int journo, string fdate, string tdate)
    {
        string sCustno = custno.ToString();
        if (custno == 10066)
            sCustno = "NULL";
        if (journo == 1)///////////////For Journal
            return GetUserDefineDs("SELECT * FROM SP_SALESANALYSIS_JOURNAL (" + sCustno + ",'" + fdate + "','" + tdate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
        return null;
    }

    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate)
    {
        string sCustno = custno.ToString();
        if (custno == 10066)
            sCustno = "NULL";
        if (journo == 0)
        {
            string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE,FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE, FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')";
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE,FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_B(" + sCustno + ",'" + fdate + "','" + tDate + "')";
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE,FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_P(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 2,3  ";

            //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else if (journo == 2)////////////For Books
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_B (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
        else if (journo == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_P (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
        else
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);

       /* if (custno == 10066)
            return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
        else
            return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
        */ 
    }
    //Added by subbu - 14th July 2011
    public DataSet Get_WipArticledetails(int category, int custno)
    {
        string scustno=custno.ToString();
        if(custno==10066)
            scustno="NULL";
        if (category == 1)//For Journal
            return GetDataSet("select * from SP_GET_INVOICE_WIP_ARTICLES(" + scustno + ")", "WIP_Articles", CommandType.StoredProcedure); ;
        return null;
    }
    //Subbu 20 Sep 2010
    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate,int location)
    {
        string sCustno = custno.ToString();
        if (custno == 10066)
            sCustno = "NULL";
        string condition = "";
        if (location != 0)
            condition = "where locationid=" + location;
        if (journo == 0)
        {
            string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition;
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_B(" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition;
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_P(" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " order by 2,3  ";

            //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else if (journo == 2)////////////For Books
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_B (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
        else if (journo == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_P (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
        else
            return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);

        /* if (custno == 10066)
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         else
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         */
    }    
    public bool HasPayments(string sCustno)
    {
        bool status=false;
        switch (sCustno)
        {
            case "NULL":
                status = true;
                break;
            case "10069":
                status = true;
                break;
            case "10035":
                status = true;
                break;
            case "10094":
                status = true;
                break;
            case "10113":
                status = true;
                break;
            case "10115": //thebigword 
                status = true;
                break;
                //Added by subbu on 05 July 2012
            case "10106": //Interdisciplinary Toxicology  
                status = true;
                break;
            case "10121": //Tehran University of Medical Sciences
                status = true;
                break;
            case "10105": //For San Lucas Medical Limited
                status = true;
                break;
            case "10059": //For St. Augustian Press
                status = true;
                break;
            case "10174": //For Journal of Mobile Technology in Medicine
                status = true;
                break;
            default:
                status = false;
                break;
        }
        return status;
    }
    //Royson 06 Aug 2010
    public TandFInvoiceDS GetInvoicedJobs3Outstanding(int custno, int journo)
    {
        string sCustno = custno.ToString();
        if (custno == 10066)
            sCustno = "NULL";
        if (journo == 0)
        {
            string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,0 as BTYPE FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ") UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,0 as BTYPE FROM P_INVOICED_ITEMS_OUTS_WIP_J (" + sCustno + ")";
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , OUTPRINTAREA, BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,BTYPE FROM P_INVOICED_ITEMS_OUTSTANDING_B(" + sCustno + ")";
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,0 as BTYPE FROM P_INVOICED_ITEMS_OUTSTANDING_P(" + sCustno + ") ";
            if (this.HasPayments(sCustno))
            {
                Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME" +
                    " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO,0 as BTYPE FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
            }
            else Qry += " order by 3,7,2";
            
            //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else if (journo == 2)////////////For Books
        {
            string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast(OUTPRINTAREA as CHAR(50)) as OUTPRINTAREA, cast(BISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER,cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTSTANDING_B(" + sCustno + ")";
            if (this.HasPayments(sCustno))
            {
                Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
            }
            else Qry += " order by 3,7,2";
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_B (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
        {
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast(PROJECTNUMBER as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTSTANDING_P(" + sCustno + ") ";
            if (this.HasPayments(sCustno))
            {
                Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
            }
            else Qry += " order by 3,7,2";
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_P (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else ///////////////For Journal
        {
            string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ") UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTS_WIP_J (" + sCustno + ") ";
            if (this.HasPayments(sCustno))
            {
                Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
            }
            else Qry += " order by 3,2,10,7";
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        

        /* if (custno == 10066)
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         else
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         */
    }
    //Royson 06 Aug 2010
    public TandFInvoiceDS GetInvoicedJobs3PaymentReceived(int custno, int journo, string fdate, string tDate)
    {
        string sCustno = custno.ToString();
        if (custno == 10066)
            sCustno = "NULL";
        if (journo == 0)
        {
            //string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME FROM P_INVOICED_ITEMS_PAID_J (" + sCustno + ",'" + fdate + "','" + tDate + "')";
            //Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME FROM P_INVOICED_ITEMS_PAID_B(" + sCustno + ",'" + fdate + "','" + tDate + "')";
            //Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME FROM P_INVOICED_ITEMS_PAID_P(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 2,3  ";
            string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')";

            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , OUTPRINTAREA, BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_B(" + sCustno + ",'" + fdate + "','" + tDate + "')";
            Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_P(" + sCustno + ",'" + fdate + "','" + tDate + "') ";
            Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME" +
                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
            //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else if (journo == 2)////////////For Books
        {
            string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast(OUTPRINTAREA as CHAR(50)) as OUTPRINTAREA, cast(BISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER,cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_B(" + sCustno + ",'" + fdate + "','" + tDate + "')" +
                " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_B (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
        {
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast(PROJECTNUMBER as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_P(" + sCustno + ",'" + fdate + "','" + tDate + "') " +
                        " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_P (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }
        else ///////////////For Journal
        {
            string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" +
                        " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
        }

        /* if (custno == 10066)
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         else
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         */
    }
    //Royson 06 Aug 2010
    public bool ApprovePayments(System.Collections.ArrayList acceptlist, string sModifiedBy)
    {
        int i = 0;
        object[] oQrys=new object[acceptlist.Count];
        bool status = false;
        string sSql = "", sDateNow = DateTime.Now.ToString("yyyy-MM-dd");
        foreach (System.Web.UI.WebControls.ListItem item in acceptlist){
            sSql = "";
            switch (item.Text){
                case "journal":
                    sSql = "UPDATE ISSUE_DP SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE INO=" + item.Value + ";";
                    break;
                case "book":
                    sSql = "UPDATE BOOK_DP SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE BNO=" + item.Value + ";";
                    break;
                case "project":
                    sSql = "UPDATE PROJECTS_DP SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE PROJECTNO=" + item.Value + ";";
                    break;
                case "wip": //For TandF and Psychology Press WIP  added by subbu
                    sSql = "UPDATE WIPARTICLES_DP SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE WNO=" + item.Value + ";";
                    break;
                default:
                    sSql = "UPDATE CREDIT_ON_ACCOUNT SET PAYMENT_DATE='" + sDateNow + "',PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE CREDITID=" + item.Value + ";";
                    break;
            }
            oQrys[i] = sSql; i++;
        }
        if (oQrys.Length > 0 && this.Execute_Sql(oQrys))
            status = true;
            
        return status;
    }
    //Royson 06 Aug 2010
    public bool CancelPayments(System.Collections.ArrayList cancellist, string sModifiedBy)
    {
        int i = 0;
        object[] oQrys = new object[cancellist.Count];
        bool status = false; string sSql = "";
        foreach (System.Web.UI.WebControls.ListItem item in cancellist){
            sSql = "";
            switch (item.Text){
                case "journal":
                    sSql = "UPDATE ISSUE_DP SET PAYMENT_DATE=NULL,PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE INO=" + item.Value + ";";
                    break;
                case "book":
                    sSql = "UPDATE BOOK_DP SET PAYMENT_DATE=NULL,PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE BNO=" + item.Value + ";";
                    break;
                case "project":
                    sSql = "UPDATE PROJECTS_DP SET PAYMENT_DATE=NULL,PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE PROJECTNO=" + item.Value + ";";
                    break;
                case "wip":
                    sSql = "UPDATE WIPARTICLES_DP SET PAYMENT_DATE=NULL,PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE WNO=" + item.Value + ";";
                    break;
                default:
                    sSql = "UPDATE CREDIT_ON_ACCOUNT SET PAYMENT_DATE=NULL,PAYMENT_CONFIRM_BY='" + sModifiedBy + "' WHERE CREDITID=" + item.Value + ";";
                    break;
            }
            oQrys[i] = sSql; i++;
        }
        if (oQrys.Length > 0 && this.Execute_Sql(oQrys))
            status = true;

        return status;
    }
    public DataSet getCustomerPaymentOnAccount(string sCustno){
        return this.GetDataSet1("select * from credit_on_account where custno="+sCustno+" and payment_date is null and obsolete is null order by credited_date");
    }
    public bool AddUpdatePaymentOnAccount(System.Collections.ArrayList list, string sModifiedBy)
    {
        int i = 0;
        object[] oQrys = new object[list.Count];
        bool status = false; string sSql = "";
        foreach (System.Web.UI.WebControls.ListItem item in list){
            if(item.Text=="insert")
                sSql = "insert into credit_on_account(custno,credited_value,credited_date)values(" + item.Value.Split('|')[1] + "," + item.Value.Split('|')[2] + ",'" + item.Value.Split('|')[3] + "')";
            else
                sSql = "update credit_on_account set custno = " + item.Value.Split('|')[1] + ",credited_value=" + item.Value.Split('|')[2] + ",credited_date='" + item.Value.Split('|')[3] + "' where creditid=" + item.Value.Split('|')[0];
            oQrys[i] = sSql; i++;
        }
        if (oQrys.Length > 0 && this.Execute_Sql(oQrys))
            status = true;

        return status;
    }    
    public bool DeletePaymentOnAccount(string sCreditID, string sModifiedBy){        
        object[] oQrys = new object[1];        
        oQrys[0] = "update credit_on_account set obsolete = F_CURRENTDATE('MM/dd/yyyy hh:mm:ss'), PAYMENT_CONFIRM_BY='" + sModifiedBy + "' where creditid=" + sCreditID;
        return this.Execute_Sql(oQrys);
    }
    /// <summary>
    /// Created by Royson on 06 Aug 2010 for multiple query collection
    /// </summary>
    /// <param name="oSaveitems">Query Collection</param>
    /// <returns></returns>
    private bool Execute_Sql(object[] oSaveitems)
    {
        OdbcConnection cnn = null;
        OdbcCommand cmd = null;
        OdbcTransaction trans = null;
        bool blnCheck = false;
        string strSql = "";
        int cnt = 0;
        string connString = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
        try
        {
            using (cnn = new OdbcConnection(connString))
            {
                cnn.Open();
                trans = cnn.BeginTransaction();
                foreach (object obj in oSaveitems)
                {
                    try
                    {
                        strSql = obj.ToString();
                        cmd = new OdbcCommand(strSql, cnn, trans);
                        cnt = cnt + cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception(ex.ToString());
                    }
                }
                trans.Commit();
                if (cnt > 0)
                    blnCheck = true;
            }
            return blnCheck;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
            if (cmd != null) cmd.Dispose();
            //   if (trans != null) trans.Dispose();
        }
    }


    public DataSet GetDespatchedJobs2(int custno, int journo)
    {
        string sCustno = custno.ToString();  
        if (custno == 10066)
            sCustno = "NULL";
        if (journo == 4)//For WIP
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_WIP (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if (journo == 2)////////////For Books
        {
            if(sCustno=="NULL")
            //This is comment for Test -- now live
            return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='Y' and bdespatched1='Y' AND BEMAIL_FLAG1 = 'N' AND BDISPATCH1 IS NOT NULL order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
                //return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='N' and bdespatched1='Y' AND APPROVEEMPNO  IS NULL  AND BDISPATCH1 IS NOT NULL order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
            else
            return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='Y' and bdespatched1='Y' AND BEMAIL_FLAG1 = 'N' AND BDISPATCH1 IS NOT NULL AND CUSTNO1 = " + sCustno + " order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_P (" + sCustno + ") ORDER BY PDISPATCHDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_P (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);

    }
    

    //Created By Subbu - 30 June 2010
    public DataSet GetEmailconfigDetails(int Projectorbookno, int category)
    {
        if (category == 3)//For project
            return GetDataSet("SELECT PCODE,PTITLE FROM PROJECTS_DP WHERE PROJECTNO=" + Projectorbookno, "PROJECTMAILSENT", CommandType.Text);
        else if (category == 2)//For Book
            return GetDataSet("SELECT BNUMBER,BTITLE FROM BOOK_DP WHERE BNO=" + Projectorbookno, "BOOKMAILSENT", CommandType.Text);
        else
            return  null; 
    }
    //Created By Subbu - 05 May 2010
    public DataSet GetDispatchedjobMail(int custno, int category,int conno,int flg)
    {
        string sCustno = custno.ToString();
        //Sent Email for PEName wise and get Email details only journal
        if ((category == 1 || category == 4) && flg == 1)///////////////For Journal
            return GetDataSet("SELECT DISTINCT JOURCODE , IISSUENO, INO, IINNO, INV_EMAIL_SENT1, INV_EMAIL_SENT2 FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") where conno=" + conno + "and INV_EMAIL_SENT1='N' UNION SELECT DISTINCT JOURCODE, IISSUENO, INO, INVNO, INV_EMAIL_SENT1, INV_EMAIL_SENT2 FROM P_DISPATCHED_APPROVED_ITEMS_WIP (" + sCustno + ") where conno=" + conno + " and INV_EMAIL_SENT1='N'", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if ((category == 1 || category == 4) && flg == 2)
            return GetDataSet("SELECT DISTINCT JOURCODE, IISSUENO, INO, IINNO,cno, INV_EMAIL_SENT1, INV_EMAIL_SENT2 FROM P_DISPATCHED_APPROVED_ITEMS_J (NULL) where cno in (2556,10040) and INV_EMAIL_SENT2='N'  UNION SELECT DISTINCT JOURCODE,IISSUENO, INO, INVNO,WCUSTNO, INV_EMAIL_SENT1, INV_EMAIL_SENT2 FROM P_DISPATCHED_APPROVED_ITEMS_WIP (" + sCustno + ") where INV_EMAIL_SENT2='N' order by 5", "INVOICEABLEJOBS", CommandType.StoredProcedure);

        else if (category == 2)
        {
            //conno have bno value
            return GetDataSet("SELECT BEMAIL_FLAG,BEMAIL_FLAG_IND FROM BOOK_DP WHERE BEMAIL_FLAG='N' and BEMAIL_FLAG_IND='N' and BNO=" + conno, "BOOKMAILSENT", CommandType.Text);
        }
        else if (category == 3)
        {
            //conno have projectno value
            return GetDataSet("SELECT PROJECTNO,PTITLE,PCODE,PEMAIL_FLAG,PEMAIL_FLAG_IND FROM PROJECTS_DP WHERE PEMAIL_FLAG='N' and PEMAIL_FLAG_IND='N' and projectno in(" + conno + ")", "PROJECTMAILSENT", CommandType.Text);
        }
        else
            return null;

        /*
        if (custno == 10066)
            sCustno = "NULL";
        if (category == 2)////////////For Books
        {
            if (sCustno == "NULL")
                //This is comment for Test -- now live
                return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='Y' and bdespatched1='Y' AND BEMAIL_FLAG1 = 'N' AND BDISPATCH1 IS NOT NULL order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
            //return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='N' and bdespatched1='Y' AND APPROVEEMPNO  IS NULL  AND BDISPATCH1 IS NOT NULL order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
            else
                return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='Y' and bdespatched1='Y' AND BEMAIL_FLAG1 = 'N' AND BDISPATCH1 IS NOT NULL AND CUSTNO1 = " + sCustno + " AND CONNO1=" + conno + " order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
        }
        else if (category == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_P (" + sCustno + ") ORDER BY PDISPATCHDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_P (" + sCustno + ") where conno="+ conno , "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if (category == 1)///////////////For Journal
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") where conno=" + conno, "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
         
         * */
    }
    public DataSet GetDispatchedjobMail_coaction(int custno, int category, int conno, int flg)
    {
        return GetDataSet("SELECT PROJECTNO,PTITLE,PCODE,invno,PEMAIL_FLAG,PEMAIL_FLAG_IND FROM PROJECTS_DP WHERE INVNO IS NOT NULL AND PEMAIL_FLAG='N' and PEMAIL_FLAG_IND='N' and conno in(" + conno + ")", "PROJECTMAILSENT", CommandType.Text);
    }
    public DataSet GetDespatchedJobs1(int custno, int journo)
    {
        string sCustno = custno.ToString();
        if (custno == 10066)
            sCustno = "NULL";
        if (journo == 4)/////////////For WIP
            return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_WIP (" + sCustno + ")  ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if (journo == 2)////////////For Books
        {
            //return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_B (" + sCustno + ")", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            
            if (sCustno == "NULL")
                //STYPENO1=10079 this is for FINAL PROOF
                return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='N' AND  bdespatched1='Y' AND BDISPATCH1 IS NOT NULL AND STYPENO1=10079 order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
            else
                //STYPENO1=10079 this is for FINAL PROOF
                return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='N' and bdespatched1='Y' AND BDISPATCH1 IS NOT NULL AND STYPENO1=10079 AND CUSTNO1 = " + sCustno + " order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_P (" + sCustno + ")  ORDER BY PDISPATCHDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else
            return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
    }
    public DataSet GetDSvalue(string Qry, string procName, CommandType ctype)
    {
        DataSet ds = new DataSet();
        ds = GetDataSet(Qry, procName, ctype);
        return ds;
    }

    public bool UpdateApproveName(int empid, int ino, int Category,string comname)
    {
        OdbcCommand oCmd = new OdbcCommand();
        OdbcTransaction oTran = null;
        DataSet upds = new DataSet();
        OdbcCommand acmd = new OdbcCommand();
        try
        {
            string SelectQry = "";
            string UpdateQry = "";
            int invno=0;
            DataSet ds = new DataSet();
            if (comname == "saveprint")
            {
               

                //if (Category == 1)///////////////For Journal
                //    SelectQry = "SELECT MAX(IINVOICENO) AS LASTINVOICENO FROM ISSUE_DP WHERE IINVOICENO IS NOT NULL";
                //else if (Category == 2)////////////For Books
                //    SelectQry = "SELECT MAX(BINVOICENO) AS LASTINVOICENO FROM BOOK_DP WHERE BINVOICENO IS NOT NULL";
                //else if(Category==3)/////////For Projects
                //    SelectQry = "SELECT MAX(INVNO) AS LASTINVOICENO FROM PROJECTS_DP WHERE INVNO IS NOT NULL";
                //ds = GetDataSet(SelectQry, "INVNO", CommandType.StoredProcedure);

                //int invno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) + 1;
                //ds = null;

                //SelectQry = "SELECT newinvoiceno as INVNO FROM SP_GET_NEXTINVOICE";
                if (Category != 4)
                {
                    SelectQry = "select INVID from p_inc_invoiceseq_dp";
                    ds = GetDataSet(SelectQry, "INVNO", CommandType.StoredProcedure);
                    invno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    ds = null;
                }
                
            }
            opencon();
            //oTran = oConn.BeginTransaction(); 
            if (Category == 1 && comname == "saveprint")///////////////For Journal
                //22/12/2008  Comment for Test
                //UpdateQry = "UPDATE ISSUE_DP SET APPROVEDBY_EMPID = " + empid + ", IINVOICENO =" + invno + " WHERE INO = " + ino;
                
                UpdateQry = "UPDATE ISSUE_DP SET  IINVOICEDATE = '" + DateTime.Now.ToShortDateString() + "',IINVOICENO =" + invno + ", IINVOICED = 'Y', APPROVEDBY_EMPID = " + empid + " WHERE INO = " + ino;
            else if(Category == 1 && comname == "Approve")
                UpdateQry = "UPDATE ISSUE_DP SET APPROVEDBY_EMPID = " + empid + " WHERE INO = " + ino;

            else if (Category == 2 && comname == "saveprint")////////////For Books
                //22/12/2008  Comment for Test
                //UpdateQry = "UPDATE BOOK_DP SET APPROVEEMPNO = " + empid + ", BINVOICENO =" + invno + " WHERE BNO = " + ino;
                UpdateQry = "UPDATE BOOK_DP SET BINVOICEDATE = '" + DateTime.Now.ToShortDateString() + "',BINVOICENO =" + invno + ", BINVOICED = 'Y',  APPROVEEMPNO = " + empid + " WHERE BNO = " + ino;
            else if (Category == 2 && comname == "Approve")////////////For Books
                UpdateQry = "UPDATE BOOK_DP SET APPROVEEMPNO = " + empid + " WHERE BNO = " + ino;
            else if (Category == 3 && comname == "saveprint")
            {
                //22/12/2008  Comment for Test
                //UpdateQry = "UPDATE PROJECTS_DP SET APPROVEEMPNO = " + empid + ", INVNO =" + invno + " WHERE PROJECTNO = " + ino;
                UpdateQry = "UPDATE PROJECTS_DP SET PINVOICEDDATE = '" + DateTime.Now.ToShortDateString() + "',INVNO =" + invno + ", PINVOICED = 'Y',   APPROVEEMPNO = " + empid + " WHERE PROJECTNO = " + ino;
                DataSet glds = new DataSet();
                try
                {
                    glds = GetDataSet("select * from projects_dp WHERE PARENT_PROJECTNO =" + ino, "Projects", CommandType.Text);
                    if (glds != null && glds.Tables[0].Rows.Count > 0)
                    {
                        //For Global Language if it has child project then asssign invoicedate, parentinvno, etc
                        if (oConn.State.ToString() == "Closed")
                            opencon();
                        oCmd.Connection = oConn;
                        oCmd.Transaction = oTran;
                        oCmd.CommandType = CommandType.Text;
                        oCmd.CommandText = "UPDATE PROJECTS_DP SET PINVOICEDDATE='" + DateTime.Now.ToShortDateString() + "',PARENT_INVNO=" + invno + ", PINVOICED='Y', APPROVEEMPNO=" + empid + " WHERE PARENT_PROJECTNO =" + ino;
                        oCmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                { throw ex; }
                finally
                { glds = null; }
            }
            else if (Category == 3 && comname == "Approve")
                UpdateQry = "UPDATE PROJECTS_DP SET APPROVEEMPNO = " + empid + " WHERE PROJECTNO = " + ino;

            else if (Category == 4 && comname == "saveprint")
            {
                UpdateQry = "select ano from SP_GET_INVOICE_WIP(" + ino + ")";
                acmd.Connection = oConn;
                //oCmd.Transaction = oTran;
                acmd.CommandType = CommandType.StoredProcedure;
                acmd.CommandText = UpdateQry;
                //oCmd.CommandTimeout = 600;

                OdbcDataAdapter da = new OdbcDataAdapter(acmd);
                da.Fill(upds, "Article");

                string ano = "";
                if (upds != null && upds.Tables[0].Rows.Count > 0)
                {
                    SelectQry = "select INVID from p_inc_invoiceseq_dp";
                    ds = GetDataSet(SelectQry, "INVNO", CommandType.StoredProcedure);
                    invno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    ds = null;

                    for (int dscnt = 0; dscnt < upds.Tables[0].Rows.Count; dscnt++)
                    {
                        if (ano == "")
                            ano = upds.Tables[0].Rows[dscnt]["ano"].ToString();
                        else
                            ano = ano + "," + upds.Tables[0].Rows[dscnt]["ano"].ToString();
                    }

                    string upqry = "update article_dp set ainvoiceddate='" + DateTime.Now.ToShortDateString() + "',invno=" + invno + ",ce_pages=f_doubleabs(ce_pages),SAM_PAGES=f_doubleabs(SAM_PAGES) where ano in( ";
                    UpdateQry = upqry + ano + " )";
                }
                else
                {
                    throw new ArgumentException("No article has been found, Please check");
                }

                //                UpdateQry += upqry + " select distinct ano " +
                //" from journal_dp join article_dp on journal_dp.journo=article_dp.journo and article_dp.invno is null and journal_dp.jprodedno=" + ino +
                //" join issue_dp on issue_dp.ino = article_dp.ino and issue_dp.iinvoiceno is null and ino in (select distinct ino from loggedevents_dp where evno = 10076) " +
                //" join customer_dp on journal_dp.custno=customer_dp.custno and customer_dp.custno in(2556,10040) join loggedevents_dp on article_dp.ano=loggedevents_dp.ano " +
                //" where article_dp.adno = 3 and evno=10054  and article_dp.anon_article = 0  AND ARTICLE_DP.AREALNOOFPAGES > 0) ";                 
                opencon();
                oTran = oConn.BeginTransaction();
                oCmd.Connection = oConn;
                oCmd.Transaction = oTran;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = UpdateQry;
                oCmd.ExecuteNonQuery();

                UpdateQry = " insert into WIPARTICLES_DP(CONNO,WINVOICED,WINVOICENO,WINVOICEDATE,CUSTNO,WIP_ITEMNAME,APPROVEEMPNO) values(" + ino + ",'Y'," + invno + ",'" + DateTime.Now.ToShortDateString() + "',2556,'WIP_" + invno + "'," + empid + ")";
                oCmd.Connection = oConn;
                oCmd.Transaction = oTran;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = UpdateQry;
                oCmd.ExecuteNonQuery();
            }

            if (Category != 4 && comname == "saveprint")
            {
                if (oConn.State.ToString() == "Closed")
                    opencon();
                oTran = oConn.BeginTransaction(); 
                oCmd.Connection = oConn;
                oCmd.Transaction = oTran;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = UpdateQry;
                oCmd.ExecuteNonQuery();
                // oCmd.CommandText = "select INVID from p_inc_invoiceseq_dp";
            }
            if (oTran != null)
                oTran.Commit();
            return true;
        }
        catch (Exception oex)
        {
            if (oTran != null)
                oTran.Rollback();
            throw oex;
            //return false;
        }
        finally
        {
            closecon();
            oCmd = null;
            upds = null;
            acmd.Dispose(); acmd = null;
        }
    }

    public bool UpdateInvDate(int ino)
    {
        OdbcCommand oCmd = new OdbcCommand();
        try
        {
            opencon();
            oCmd.CommandType = CommandType.Text;
            //oCmd.Transaction.Connection.BeginTransaction(); 
            oCmd.CommandText = "UPDATE ISSUE_DP SET IINVOICEDATE = '" + DateTime.Now.ToShortDateString()  + "' WHERE INO = " + ino;
            oCmd.Connection = oConn;
            oCmd.ExecuteNonQuery();
            //oCmd.Transaction.Commit;
            return true;
        }
        catch (Exception oex)
        {
            return false;
            //oCmd.Transaction.Rollback();   
        }
        finally
        {
            closecon();
            oCmd = null;
        }
    }

    public bool UpdateInvCompleted(string ino,int custno,int Email_Flg) //int ino)
    {
        //OdbcCommand oCmd = new OdbcCommand();
       // OdbcTransaction otrans = null;
        DataSet Eflg_Ds = new DataSet();
        try
        {
            /*opencon();
            otrans = oConn.BeginTransaction();
            oCmd.Connection = oConn;
            oCmd.Transaction = otrans;
            oCmd.CommandType = CommandType.Text;
            //oCmd.Transaction.Connection.BeginTransaction(); 
            //oCmd.CommandText = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y' WHERE INO = " + ino;
            
            
             * */
            //oCmd.CommandText = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y' WHERE ino in( " + ino + ")";
           /* oCmd.CommandText = Qry;
            
            oCmd.ExecuteNonQuery();
            */
            
           

            if (custno == 2556)//For only TandF
            {
                string Qry = "UPDATE ISSUE_DP SET  ";
                if (custno == 2556 && Email_Flg == 1)
                    Qry += "INV_EMAIL_SENT1='Y' ";
                else if (custno == 2556 && Email_Flg == 2)
                    Qry += "INV_EMAIL_SENT2='Y' ";
                
                Qry += " WHERE ino in( " + ino + ")";
                ExcuteProc(Qry);


                string[] ino_array = ino.Split(',');
                for (int ino_array_cnt = 0; ino_array_cnt < ino_array.GetLength(0); ino_array_cnt++)
                {
                    Eflg_Ds = GetDataSet("select INV_EMAIL_SENT1,INV_EMAIL_SENT2 from issue_dp where ino in(" + ino_array[ino_array_cnt].ToString() + ")", "Email_FlagDetails", CommandType.Text);
                    if (Eflg_Ds != null)
                    {
                        if (Eflg_Ds.Tables[0].Rows[0]["INV_EMAIL_SENT1"].ToString() == "Y" && Eflg_Ds.Tables[0].Rows[0]["INV_EMAIL_SENT2"].ToString() == "Y")
                        {
                            string uqry = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y' WHERE ino in( " + ino_array[ino_array_cnt].ToString() + ")";
                            ExcuteProc(uqry);
                            /*oCmd.CommandText = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y' WHERE ino in( " + ino_array[ino_array_cnt].ToString() + ")";

                            //oCmd.Connection = oConn;
                            oCmd.ExecuteNonQuery();*/
                        }
                    }
                }
            }
            else
                ExcuteProc("UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y' WHERE ino in( " + ino.ToString() + ")");

            //otrans.Commit();
            return true;
           
        }
        catch (Exception oex)
        {
           // otrans.Rollback();
            return false;
            //oCmd.Transaction.Rollback();   
        }
        finally
        {
           // closecon();
           // oCmd = null;
            Eflg_Ds = null;
        }
    
    }

    public bool UpdatewipInvCompleted(string wno, int custno, int Email_Flg) //int ino)
    {
        //OdbcCommand oCmd = new OdbcCommand();
        // OdbcTransaction otrans = null;
        DataSet Eflg_Ds = new DataSet();
        try
        {
            if (custno == 2556)//For only TandF
            {
                string Qry = "UPDATE wiparticles_dp SET  ";
                if (custno == 2556 && Email_Flg == 1)
                    Qry += "INV_EMAIL_SENT1='Y' ";
                else if (custno == 2556 && Email_Flg == 2)
                    Qry += "INV_EMAIL_SENT2='Y' ";

                Qry += " WHERE wno in( " + wno + ")";
                ExcuteProc(Qry);


                string[] wno_array = wno.Split(',');
                for (int wno_array_cnt = 0; wno_array_cnt < wno_array.GetLength(0); wno_array_cnt++)
                {
                    Eflg_Ds = GetDataSet("select INV_EMAIL_SENT1,INV_EMAIL_SENT2 from wiparticles_dp where wno in(" + wno_array[wno_array_cnt].ToString() + ")", "Email_FlagDetails", CommandType.Text);
                    if (Eflg_Ds != null)
                    {
                        if (Eflg_Ds.Tables[0].Rows[0]["INV_EMAIL_SENT1"].ToString() == "Y" && Eflg_Ds.Tables[0].Rows[0]["INV_EMAIL_SENT2"].ToString() == "Y")
                        {
                            string uqry = "UPDATE wiparticles_dp SET EMAIL_FLAG='Y' WHERE wno in( " + wno_array[wno_array_cnt].ToString() + ")";
                            ExcuteProc(uqry);
                            /*oCmd.CommandText = "UPDATE ISSUE_DP SET IINVOICED = 'Y',IEMAIL_FLAG='Y',IEMAIL_FLAG_IND='Y' WHERE ino in( " + ino_array[ino_array_cnt].ToString() + ")";

                            //oCmd.Connection = oConn;
                            oCmd.ExecuteNonQuery();*/
                        }
                    }
                }
            }

            //otrans.Commit();
            return true;

        }
        catch (Exception oex)
        {
            // otrans.Rollback();
            return false;
            //oCmd.Transaction.Rollback();   
        }
        finally
        {
            // closecon();
            // oCmd = null;
            Eflg_Ds = null;
        }

    }
    //Create on 10 May 2010 by Subbu
    public void ExcuteProc(string Qry)
    {
        OdbcCommand cmdExc = new OdbcCommand();
        OdbcTransaction otrans = null;
        try
        {
            opencon();
            otrans = oConn.BeginTransaction();
            cmdExc.Connection = oConn;
            cmdExc.Transaction = otrans;
            cmdExc.CommandType = CommandType.Text;
            cmdExc.CommandText = Qry;
            cmdExc.ExecuteNonQuery();
            otrans.Commit();
        }
        catch (Exception ex)
        {
            otrans.Rollback();
            throw ex;
        }
        finally
        {
            closecon();
            cmdExc = null;
            otrans = null;
        }
    }
    public void UpdatePreviewDate(string location_preview,string category,string idvalue,string preview_date)
    {   
        switch(category)
        {
            case "1": //For journal
                    if(location_preview.ToString().ToUpper()=="I")
                        ExcuteProc("update issue_dp set IndiaInv_preview=" + preview_date + " where ino=" + idvalue);
                    else if(location_preview.ToString().ToUpper()=="D")
                        ExcuteProc("update issue_dp set DublinInv_preview=" + preview_date + " where ino=" + idvalue);
                    else if(location_preview.ToString().ToUpper()=="SAVEPRINT")
                        ExcuteProc("update issue_dp set IndiaInv_preview=NULL,DublinInv_preview=NULL where ino=" + idvalue);
                    break;
            case "2"://For Book
                    if (location_preview.ToString().ToUpper() == "I")
                        ExcuteProc("update book_dp set IndiaInv_preview=" + preview_date + " where bno=" + idvalue);
                    else if (location_preview.ToString().ToUpper() == "D")
                        ExcuteProc("update book_dp set DublinInv_preview=" + preview_date + " where bno=" + idvalue);
                    else if (location_preview.ToString().ToUpper() == "SAVEPRINT")
                        ExcuteProc("update book_dp set IndiaInv_preview=NULL,DublinInv_preview=NULL where bno=" + idvalue);
                    break;
            case "3"://For Projects
                    if(location_preview.ToString().ToUpper() == "I")
                        ExcuteProc("update projects_dp set IndiaInv_preview=" + preview_date + " where projectno=" + idvalue);
                    else if (location_preview.ToString().ToUpper() == "D")
                        ExcuteProc("update projects_dp set DublinInv_preview=" + preview_date + " where projectno=" + idvalue);
                    else if (location_preview.ToString().ToUpper() == "SAVEPRINT")
                        ExcuteProc("update projects_dp set IndiaInv_preview=NULL,DublinInv_preview=NULL where projectno=" + idvalue);
                    break;
                case "4"://For WIP
                    if (location_preview.ToString().ToUpper() == "I")
                        ExcuteProc("update wiparticles_dp set Indiainv_preview=" + preview_date + " where winvoiceno=" + idvalue);
                    else if (location_preview.ToString().ToUpper() == "D")
                        ExcuteProc("update wiparticles_dp set Dublininv_preview=" + preview_date + " where winvoiceno=" + idvalue);
                    else if (location_preview.ToString().ToUpper() == "SAVEPRINT")
                        ExcuteProc("update wiparticles_dp set Indiainv_preview=NULL,Dublininv_preview=NULL where winvoiceno=" + idvalue);
                    break;
        }
        
    }
    //Create on 20 June 2009 by Subbu
    public DataSet InvoiceDataSet(string procname, string dsname, CommandType cmdtype)
    {
        return GetDataSet(procname, dsname, cmdtype);
    }

    private DataSet GetDataSet(string sProcName, string sDSName, CommandType sCmdType)
    {

        try
        {

            opencon();
            OdbcCommand oCmd = new OdbcCommand();
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcName;
            oCmd.CommandTimeout = 600; 
            oCmd.Connection = oConn;
            OdbcDataAdapter da = new OdbcDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, sDSName);
            return ds;
        }
        catch (Exception oex)
        {
            return null;
        }
        finally
        {
            closecon();
        }

    }

    //Create On 06/07/2009 By Subbu
    private TandFInvoiceDS GetUserDefineDs(string sProcName, string sDSName, CommandType sCmdType)
    {
        try
        {

            opencon();
            OdbcCommand oCmd = new OdbcCommand();
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcName;
            oCmd.CommandTimeout = 600;
            oCmd.Connection = oConn;
            OdbcDataAdapter da = new OdbcDataAdapter(oCmd);
            TandFInvoiceDS userDs = new TandFInvoiceDS();
            da.Fill(userDs, sDSName);
            return userDs;
        }
        catch (Exception oex)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }
    //CREATE ON 02/03/2009 BY SUBBU

    public DataSet GetJournalcode(string Qry)
    {
        return GetDataSet(Qry, "Journal", CommandType.Text);
    }

    public DataSet Getinvoiceconfig(string qry, CommandType cmdtype)
    {
        return GetDataSet(qry, "InvoiceConfig", cmdtype);
    }
    public bool RunQuery(string Qry,string dsName)
    {
        return GetBoolean(Qry, dsName, CommandType.Text);
    }
    private bool GetBoolean(string sProcName, string sDSName, CommandType sCmdType)
    {
        OdbcCommand oCmd = new OdbcCommand();
        //OdbcTransaction OTran = null;
        try
        {
            opencon();
            //OTran = oConn.BeginTransaction();
            oCmd.CommandType = sCmdType;
            //oCmd.Transaction = OTran;
            oCmd.CommandText = sProcName;
            oCmd.Connection = oConn;
            oCmd.ExecuteNonQuery();
            //OTran.Commit();
            return true;
        }
        catch (Exception oex)
        {
            return false;
            //OTran.Rollback();   
        }
        finally
        {
            closecon();
            oCmd = null;
        }
    }

    public DataSet GetDataSet1(string sSQL)
    {
        try
        {
            opencon();
            OdbcCommand oCmd = new OdbcCommand(sSQL);
            oCmd.CommandTimeout = 600;
            oCmd.Connection = oConn;
            OdbcDataAdapter da = new OdbcDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        catch (Exception oex)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }

    public XmlDocument GetInvHeaders(int custno, int journo)
    {
        DataSet ds = new DataSet();
        ds = GetDataSet("SELECT * FROM SP_GET_INVOICE_HEADERS (" + journo + ", " + custno + ")", "INVHEADER", CommandType.StoredProcedure);
        XmlDocument odom = new XmlDocument();
        if (ds != null)
            odom.LoadXml(ds.GetXml());
        ds = null;
        return odom;
    }

    public XmlDocument GetInvDetails(int custno, string iissueno, int ino)
    {
        DataSet ds = new DataSet();
        ds = GetDataSet("SELECT * FROM SP_GET_INVOICE_DETAILS (" + ino + ")", "INVDETAILS", CommandType.StoredProcedure);
        XmlDocument odom = new XmlDocument();
        odom.LoadXml(ds.GetXml());
        ds = null;
        return odom;
    }

    public DataSet GetCurrencyConversion(int iCurrencyNumber)
    {
        return GetDataSet("SELECT * FROM CURRENCYCONVERSION_DP WHERE CURRNO = " + iCurrencyNumber, "CURRENCY", CommandType.Text);
    }

    public DataSet GetAllCustomers()
    {
        return GetDataSet("SELECT CUSTNO, CUSTNAME,FINSITENO FROM CUSTOMER_DP ORDER BY CUSTNAME", "CUSTOMERS", CommandType.Text);
    }
    public DataSet GetFor_Invoiceconfig(string typeid)
    {
        return GetDataSet("select * from P_INVOICECONFIGURATION_PB(" + typeid + ")", "InvoiceConfig", CommandType.StoredProcedure);
    }
    public bool updateinvconfig(string costtype,string invdescription,string recordno,string category)
    {
        string insertqry = string.Empty;
        if(category=="1")//For Projects
            insertqry = "update projects_dp set inv_costtypeid=" + costtype + ",inv_descriptionid=" + invdescription + " where projectno=" + recordno ;
        else //For Books
            insertqry = "update book_dp set inv_costtypeid=" + costtype + ",inv_descriptionid=" + invdescription + " where bno=" + recordno;
        return GetBoolean(insertqry, "Projects", CommandType.Text);
    }

    public DataSet GetAllTeamMember(int teamLeadID)
    {
        string sQuery = " select E.empno column1, f_rtrim(E.emp_fname) || ' ' || f_rtrim(E.emp_sname) column2, DNAME, EMPPOSITION ";
        sQuery += " from employee_dp e ";
        sQuery += " JOIN DEPARTMENT_DP D ON D.DNO = E.DNO ";
        sQuery += " where  (dno in (select dno from DEPARTMENT_GROUPS where EMPNO = " + teamLeadID + ") ";
        sQuery += " OR DNO IN (SELECT DNO FROM EMPLOYEE_DP WHERE EMPNO = " + teamLeadID + ")) ";
        sQuery += " and E.status = 1 and E.emp_id > 0 and empno <> " + teamLeadID + " order by E.emp_fname ";

        return GetDataSet(sQuery, "TEAMMEMBERS", CommandType.Text);
    }

    public DataSet GetLogEvents(int iEmpNo)
    {
        string empno = iEmpNo.ToString() ;
        //DataSet oTemp = new DataSet();
        //oTemp = GetDataSet("SELECT EMPNO FROM EMPLOYEE_DP WHERE EMP_ID = " + iEmpNo, "EMPNO", CommandType.Text);
        //if (oTemp != null)
        //    empno = oTemp.Tables[0].Rows[0]["EMPNO"].ToString(); 
        string query = "select cast('ARTICLE' as char(7)) as Column1, '1' as Column2," +
        " cast(F_LRTrim(jourcode) || ' ' || F_LRTrim(aarticlecode) as char(50)) as Coumn3, leno,cast(ledate as char(30)) as Column4,cast(lenddate as char(30)) as Column5 ,sname ,x.signoffid " +
        " , CAST(PAGESCOMPLETED AS CHAR(5)) AS COLUMN6, CAST(COMMENTS AS VARCHAR(250)) COLUMN7 from loggedevents_dp x JOin stage_dp y on x.sno=y.sno join article_dp z on x.ano=z.ano join journal_dp p on z.journo=p.journo where x.istimesheet='Y' and x.empno = " + empno + " and (ledate>='" + DateTime.Now.ToShortDateString() + " 00:00:00'  or lenddate is null)";
        query += "union" +
          " select cast('ISSUE' as char(7)) as Column1, '2' as Column2, cast(F_LRTrim(jourcode) || ' ' || F_LRTrim(iissueno) as char(50)) as Coumn3, leno,cast(ledate as char(30)) as Column4,cast(lenddate as char(30)) as Column5, " +
          " sname, x.signoffid,  CAST(PAGESCOMPLETED AS CHAR(5)) AS COLUMN6, CAST(COMMENTS AS VARCHAR(250)) COLUMN7  from loggedevents_dp x JOin stage_dp y on x.sno=y.sno join issue_dp p on x.ino=p.ino join journal_dp j on p.journo=j.journo " +
          " where x.istimesheet='Y' and ano is null and x.empno  = " + empno + " and (ledate>='" + DateTime.Now.ToShortDateString() + " 00:00:00'  or lenddate is null)";
        query += "union " +
                " SELECT cast('ISSUE' as char(7)) as Column1, '2' as Column2, CAST('' AS CHAR(50)) AS Coumn3, leno," +
                " cast(ledate as char(30)) AS Column4, " +
                " cast(lenddate as char(30)) as Column5 , sname , " +
                " x.signoffid  , CAST('' AS CHAR(5)) AS COLUMN6, CAST(COMMENTS AS VARCHAR(250)) COLUMN7 from " +
                " loggedevents_dp X JOin stage_dp y on x.sno=y.sno " +
                " WHERE X.ANO IS NULL AND X.INO IS NULL AND X.EMPNO = " + empno + " AND (ledate>='" + DateTime.Now.ToShortDateString() + " 00:00:00' or lenddate is null) ";

        query += " union " +
            " select cast('MODULE' as char(7)) as Column1 ,'3' as column2,mpcode as Column3,leno,cast(ledate as char(30)) as Column4,cast(lenddate as char(30)) as Column5" +
            " ,SNAME, l.signoffid, CAST(IMAGESCOMPLETED AS CHAR(5)) AS COLUMN6, CAST(COMMENTS AS VARCHAR(250)) COLUMN7 from PLogEvents_dp l" +
            " inner Join project_modules_dp p on l.mprojno = p.mprojno" +
            " JOin Stage_dp st ON l.SNo = st.SNO" +
            " where l.istimesheet='Y' and l.empno = " + empno + " and (ledate>='" + DateTime.Now.ToShortDateString() + " 00:00:00'  or lenddate is null)";
        query += " union " +
            " select cast('PROJECT' as char(7)) as Column1 ,'3' as column2,pcode as Column3,leno,cast(ledate as char(30)) as Column4,cast(lenddate as char(30)) as Column5" +
            " ,SNAME, l.signoffid, CAST(IMAGESCOMPLETED AS CHAR(5)) AS COLUMN6, CAST(COMMENTS AS VARCHAR(250)) COLUMN7 from PLogEvents_dp l" +
            " inner Join projects_dp p on l.projectno = p.projectno" +
            " JOin Stage_dp st ON l.SNo = st.SNO" +
            " where l.istimesheet='Y' and l.mprojno is null and l.empno = " + empno + " and (ledate>='" + DateTime.Now.ToShortDateString() + " 00:00:00'  or lenddate is null)";


        query += "union";
        query += " select cast('BOOKS' as char(7)) Column1, '4' as Column2,cast(BNUMBER as char(50)) as Column3,leno," +
            " cast(ledate as char(30)) as Column4,cast(lenddate as char(30)) as Column5," +
            " SNAME, l.signoffid, CAST(PAGESCOMPLETED AS CHAR(5)) AS COLUMN6, CAST(COMMENTS AS VARCHAR(250)) COLUMN7  from BLogEvents_dp l" +
            " inner Join Book_dp b on l.BNo = b.BNo" +
            " JOin Stage_dp st ON l.SNo = st.SNO" +
            " where l.istimesheet='Y' and l.empno  = " + empno + " and (ledate>='" + DateTime.Now.ToShortDateString() + " 00:00:00'  or lenddate is null)";
   
        return GetDataSet(query, "LOGEVENTS", CommandType.Text);
 
    }
    public DataSet GetCoactionsubmission(DateTime fdate,DateTime tdate,string acode,string duesub)
    {
        //string qry = "select journal_title,jourcode,aarticlecode,adespatchdates200,stypename,porticosubmission,pmcsubmission,doajsubmission,doisubmission,psycinfo_submission,isitr_submission,crossref_submission from article_dp join journal_dp on article_dp.journo=journal_dp.journo " +
        //            "join stype_dp on article_dp.stypeno=stype_dp.stypeno " +
        //       "where adespatchdates200 between '" + fdate.ToShortDateString() + "' and '" + tdate.ToShortDateString() + "' and custno=10081 ";
        string qry = "select journal_title,jourcode,aarticlecode,adespatchdates200,stypename,porticosubmission,pmcsubmission,doajsubmission,doisubmission,psycinfo_submission,isitr_submission,crossref_submission,DUESUBMISSION,DUESUBMISSION1,porticodue_submission, " +
            "PMCDUE_SUBMISSION,DOAJDUE_SUBMISSION,DOIDUE_SUBMISSION,PSYCINFODUE_SUBMISSION,ISITRDUE_SUBMISSION,CROSSREFDUE_SUBMISSION,JGATE_SUBMISSION,JGATEDUE_SUBMISSION " +
        " from SP_GET_COACTIONSUBMISSION('" + fdate.ToShortDateString() + "','" + tdate.ToShortDateString() + "')";
               if (duesub != "")
                    qry += " where " + duesub + " is null ";
                if (acode != "")
                {
                    qry += (qry.Contains("where"))? " and " : " where ";
                    qry += " aarticlecode like '" + acode.ToUpper().ToString() + "%' ";
                }
          return GetDataSet(qry, "Coactionsub", CommandType.Text);
    }

    public DataSet ExcueQueryString(string sQuery, string TableName)
    {
        return GetDataSet(sQuery, TableName, CommandType.Text);
    }

    public bool AddNewLog(string sJobNumber, string sStageID, string sDeptID, string sEmpno, int sLogTable)
    {
        string sSqlQuery = "";
        if (sJobNumber == null) { sJobNumber = "null"; }
        if (sLogTable == 1)
            sSqlQuery = "INSERT INTO LOGGEDEVENTS_DP (LEDATE ,ANO, INO, DNO, SNO, EMPNO, ISTIMESHEET) VALUES ('" + DateTime.Now.GetDateTimeFormats()[74].ToString()  + "', NULL ," + sJobNumber + "," + sDeptID + "," + sStageID + "," + sEmpno + ",'Y')";
        else if (sLogTable == 2)
            sSqlQuery = "INSERT INTO LOGGEDEVENTS_DP (LEDATE ,ANO, INO, DNO, SNO, EMPNO, ISTIMESHEET) VALUES ('" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "'," + sJobNumber + ", NULL ," + sDeptID + "," + sStageID + "," + sEmpno + ",'Y')";
        else if (sLogTable == 3)
            sSqlQuery = "INSERT INTO BLOGEVENTS_DP(LEDATE, BNO, EMPNO, DNO, SNO, ISTIMESHEET) VALUES('" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "'," + sJobNumber + "," + sEmpno + "," + sDeptID + "," + sStageID + ",'Y')";
        else if (sLogTable == 4) // chapters
            sSqlQuery = ""; // no values now
        else if (sLogTable == 5) // project
            sSqlQuery = "INSERT INTO PLOGEVENTS_DP(LEDATE, PROJECTNO, EMPNO, DNO, SNO, ISTIMESHEET) VALUES('" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "'," + sJobNumber + "," + sEmpno + "," + sDeptID + "," + sStageID + ",'Y')";
        else if (sLogTable == 6) // modules
            sSqlQuery = "INSERT INTO PLOGEVENTS_DP(LEDATE, MPROJNO, EMPNO, DNO, SNO, ISTIMESHEET) VALUES('" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "'," + sJobNumber + "," + sEmpno + "," + sDeptID + "," + sStageID + ",'Y')";
        return GetBoolean(sSqlQuery, "ADDLOG", CommandType.Text);
    }

    public bool EndOpenLog(string sleno, int iPages, string sComment, string sLogTable)
    {
        string sSqlQuery = "";
        if (sLogTable == "A" || sLogTable == "I")
            sSqlQuery = "UPDATE LOGGEDEVENTS_DP SET LENDDATE = '" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "', PAGESCOMPLETED = " + iPages + ", COMMENTS = '" + sComment + "' WHERE LENO = " + sleno;
        else if (sLogTable == "B")
            sSqlQuery = "UPDATE BLOGEVENTS_DP SET LENDDATE = '" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "', PAGESCOMPLETED = " + iPages + ", COMMENTS = '" + sComment + "' WHERE LENO = " + sleno;
        else if (sLogTable == "P" || sLogTable == "M")
            sSqlQuery = "UPDATE PLOGEVENTS_DP SET LENDDATE = '" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "', IMAGESCOMPLETED = " + iPages + ", COMMENTS = '" + sComment + "' WHERE LENO = " + sleno;
        return GetBoolean(sSqlQuery, "ENDLOG", CommandType.Text);
    }

    public DataSet ExcuteProc(string sProc, string TableName)
    {
        return GetDataSet(sProc, TableName, CommandType.StoredProcedure);
    }
    public DataSet Getproject_details()
    {
        return GetDataSet("select * from P_GET_PROJECTFORMODULE", "Projects", CommandType.Text); 
    }
    public bool insertprojectmodule(string projectno,string custno,string nitems,string ponumber)
    {
        string inqry=string.Empty;
        for (int i = 0; i < Convert.ToInt32(nitems); i++)
        {
            inqry = "insert into pro_modules_dp (PROJECTNO,CUSTNO,DNO,STNO,STYPENO,moponumber) values(" + projectno + "," + custno.ToString() + ",11,15,10077,'" + ponumber + "')";
            if (!GetBoolean(inqry, "Modules", CommandType.Text))
                return false;
        }
        return true;
    }
    public DataSet GetPEContact()
    {
        return GetDataSet("select conno,F_Rtrim(confirstname) || ' ' || F_Rtrim(consurname) as invdisplayname from contact_dp order by confirstname", "Contacts", CommandType.Text);
    }
    public DataSet GetProject_Modules(string projectno)
    {
        return GetDataSet("select * FROM P_GET_PROJECTMODULE(" + projectno + ")", "ProjectModule", CommandType.Text);
    }
    public bool Update_ProjectModule(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            OdbcCommand ocmd = new OdbcCommand();
            OdbcTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    umodule = "update pro_modules_dp set MPTITLE='" + ht["MPTITLE"].ToString() + "',NUMPAGES="
                    + ht["NUMPAGES"].ToString() + ",PRICECODE=" + ht["PRICECODE"].ToString() + ",COSTTYPEID="
                    + ht["COSTTYPEID"].ToString() + ",PAGEDESCRIPTIONID=" + ht["PAGEDESCRIPTIONID"].ToString()
                    + ",CONNO=" + ht["CONNO"].ToString() + ",MOPONUMBER='" + ht["MOPONUMBER"].ToString() + "' where MPROJNO=" + ht["MPROJNO"].ToString();
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd=null; closecon();
            }
        }
        return true;

    }
    public bool DeleteModules(string moduleno)
    {
        string dmod = "update pro_modules_dp set obsolete='" + DateTime.Now.ToShortDateString() + "' where MPROJNO in(" + moduleno + ")";
        return GetBoolean(dmod, "Module", CommandType.Text);
    }
    public DataSet GetsDataSet(string qry,CommandType ctype)
    {
        return GetDataSet(qry, "DataSet", ctype);
    }
    public bool UpdateCatsFailureLog(string uqry,string wqry)
    {
        string upqry = "update cats_booking_failure set " + uqry + " where CBF_ID in(" + wqry + ")";
        return GetBoolean(upqry, "CATS", CommandType.Text);
    }
    public bool updatesampemailsent(string samqry, CommandType cmdtype)
    {
        return GetBoolean(samqry, "SAMFollowup", cmdtype);
    }
    public jobperformanceds jobperformance(string qry, CommandType ctype)
    {
        OdbcCommand pcmd = new OdbcCommand();
        jobperformanceds jds=new jobperformanceds();
        try
        {
            opencon();
            pcmd.Connection = oConn;
            pcmd.CommandText = qry;
            pcmd.CommandType = ctype;
            OdbcDataAdapter pda = new OdbcDataAdapter(pcmd);
            pda.Fill(jds);
            return jds;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { pcmd = null; closecon(); }
    }
    public projectmoduleds projectmodulelist(string qry, CommandType ctype)
    {
        OdbcCommand pcmd = new OdbcCommand();
        projectmoduleds pds = new projectmoduleds();
        try
        {
            opencon();
            pcmd.Connection = oConn;
            pcmd.CommandText = qry;
            pcmd.CommandType = ctype;
            OdbcDataAdapter pda = new OdbcDataAdapter(pcmd);
            pda.Fill(pds);
            return pds;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { pcmd = null; closecon(); }
    }
    public DataSet GetJobsDue(string qry, CommandType ctype)
    {
        return GetDataSet(qry, "DataSet", ctype);
    }
    public bool updateSAMAuthormailsent(string uqry, CommandType ct)
    {
        return GetBoolean(uqry, "SAMFollowup", ct);
    }
    public void Updatebookcomplexity(string cmpqry)
    {
        string[] cqry = null;
        cqry = cmpqry.Split(';');
        for (int qrycnt = 0; cqry.GetLength(0) > qrycnt;qrycnt++ )
            if (cqry[qrycnt]!=null && cqry[qrycnt].ToString()!="") ExcuteProc(cqry[qrycnt]);
    }
    public DataSet getcombinedissue(string qry,CommandType ctype)
    {
        return GetDataSet(qry, "Details", ctype);
    }
    public bool assignchildtoparentproject(string projectno,string parent_projectno)
    {
        return GetBoolean("update projects_dp set parent_projectno = "+ parent_projectno +" where projectno in(" + projectno + ")", "Projects", CommandType.Text);
    }
}
