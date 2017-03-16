using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;

//Naresh 17-04-2014
/// <summary>
/// Summary description for datasourceIBSQL
/// </summary>
public class datasourceIBSQL
{
	SqlConnection oConn = null;
	string sConStr = "";
	SqlCommand ocmd;
	SqlTransaction oTran;
	public datasourceIBSQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	private void opencon()
	{
		//sConStr = ConfigurationManager.ConnectionStrings["conStrIB"].ToString();
		sConStr = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ToString();
		oConn = new SqlConnection(sConStr);
		oConn.Open();
	}

	private void closecon()
	{
		if (oConn != null)
		{
			if (oConn.State != ConnectionState.Closed)
				oConn.Close();
			oConn.Dispose();
		}
	}

	private void addParamsSTR(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, int sLen, ParameterDirection sDirection)
	{
		oCmmd.Parameters.Add(new SqlParameter(sName, sDBType, sLen));
		oCmmd.Parameters[sName].Value = sValue;
		oCmmd.Parameters[sName].Direction = sDirection;
	}

	private void addParamsDateTime(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, ParameterDirection sDirection)
	{
		oCmmd.Parameters.Add(new SqlParameter(sName, sValue));
		//oCmmd.Parameters[sName].Value = DateTime.Parse(sValue).ToShortDateString() ;
		//oCmmd.Parameters[sName].Direction = sDirection;
	}

	//public TandFInvoiceDS Getsalesjob(int custno, int journo, string fdate, string tdate)
	//{
	//    string sCustno = custno.ToString();
	//    if (custno == 10066)
	//        sCustno = "NULL";
	//    if (journo == 1)///////////////For Journal
	//        return GetUserDefineDs("SELECT * FROM SP_SALESANALYSIS_JOURNAL (" + sCustno + ",'" + fdate + "','" + tdate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//    return null;
	//}

	//public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate)
	//{
	//    string sCustno = custno.ToString();
	//    if (custno == 10066)
	//        sCustno = "NULL";
	//    if (journo == 0)
	//    {
	//        string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE,FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE, FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')";
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE,FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_B(" + sCustno + ",'" + fdate + "','" + tDate + "')";
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,BTYPE,COMPLEXITYTYPE,FINNO, SALES_SPLIT FROM P_INVOICED_ITEMS_P(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 2,3  ";

	//        //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else if (journo == 2)////////////For Books
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_B (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//    else if (journo == 3)/////////For Projects
	//        //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_P (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//    else if (journo == 1)///////////////For Journal
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//    else
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);

	//    /* if (custno == 10066)
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     else
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     */
	//}

	public DataSet Customer()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select custno,custname from customer_dp  order by CUSTNAME", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet JourInfo(string custname)
	{
		DataSet ds = new DataSet();
		try
		{
			if (custname.Contains("10066"))
			{
				return ExcProcedure("SELECT JOURNO, JOURCODE, JOURNAME, INVDISPLAYNAME AS JPRODEDITOR, INVCONEMAIL AS PEEMAIL, C.CUSTNO, CUSTNAME, TRIMCODE, FORMAT, TRIMSIZE, JCNO_2009 PCODE, CASE WHEN ISCOPYEDIT=0 THEN 'No' ELSE 'Yes'END ISCOPYEDIT, CASE WHEN ISSAM= 0 THEN 'No' ELSE 'Yes'END ISSAM,CASE WHEN J.ISSENSITIVE =0 THEN 'No' ELSE 'Yes'END ISSENSITIVE, CASE WHEN ISFPM =0 THEN 'No' ELSE 'Yes'END ISFPM, FOLLOW_DAYS,FOLLOW_DAYS_QRY,'Yes' Live_Journal FROM JOURNAL_DP J  " +
                                     " LEFT JOIN FINANCIALSITE_DP F ON F.FINSITENO = J.FINSITENO  JOIN CONTACT_DP ON J.JPRODEDNO=CONTACT_DP.CONNO JOIN CUSTOMER_DP C ON C.CUSTNO = J.CUSTNO  LEFT JOIN PAGETRIM_DP P ON P.PAGETRIMNO = J.PAGETRIMNO  " +
                                     " WHERE J.OBSOLETE IS NULL  ", null, CommandType.Text);
			}
			else
			{
				return ExcProcedure("SELECT JOURNO, JOURCODE, JOURNAME, INVDISPLAYNAME AS JPRODEDITOR, INVCONEMAIL AS PEEMAIL, C.CUSTNO, CUSTNAME, TRIMCODE, FORMAT, TRIMSIZE, JCNO_2009 PCODE, CASE WHEN ISCOPYEDIT=0 THEN 'No' ELSE 'Yes'END ISCOPYEDIT, CASE WHEN ISSAM= 0 THEN 'No' ELSE 'Yes'END ISSAM,CASE WHEN J.ISSENSITIVE =0 THEN 'No' ELSE 'Yes'END ISSENSITIVE, CASE WHEN ISFPM =0 THEN 'No' ELSE 'Yes'END ISFPM, FOLLOW_DAYS,FOLLOW_DAYS_QRY,'Yes' Live_Journal FROM JOURNAL_DP J  " +
                                    " LEFT JOIN FINANCIALSITE_DP F ON F.FINSITENO = J.FINSITENO  JOIN CONTACT_DP ON J.JPRODEDNO=CONTACT_DP.CONNO JOIN CUSTOMER_DP C ON C.CUSTNO = J.CUSTNO  LEFT JOIN PAGETRIM_DP P ON P.PAGETRIMNO = J.PAGETRIMNO  " +
                                    " WHERE J.OBSOLETE IS NULL AND C.CUSTNO in (" + custname + ")  ", null, CommandType.Text);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet Get_WipArticledetails(int category, int custno)
	{
		string scustno = custno.ToString();
		if (custno == 10066)
			scustno = "NULL";
		if (category == 1)//For Journal
            return GetDataSet("select * from SP_GET_INVOICE_WIP_ARTICLES(" + scustno + ")", "WIP_Articles", CommandType.StoredProcedure);;
		return null;
	}

	public DataSet ProductivityDetailsGrid(string qry, string[, ] param)
	{
		DataSet prods = new DataSet();
		SqlDataAdapter proDA = null;
		ocmd = new SqlCommand();
		try
		{
			opencon();

			proDA = new SqlDataAdapter(qry, oConn);
			proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
			if (param != null)
			{
				for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
				{
					if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
						proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
					else
						proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
				}
			}

			proDA.Fill(prods);
			return prods;
		}
		catch (Exception ex)
		{
			throw ex; }
		finally
		{
			proDA.Dispose();
			closecon();
		}
	}

	public DataSet GetJobReportDetails()
	{
        return ExcProcedure("select * from customer_dp order by custname", null, CommandType.Text);
	}

	public DataSet GetReprtDetails(string[, ] sparameter, CommandType cmdtype)
	{
		return ExcProcedure("spGet_RptDetails", sparameter, cmdtype);
	}
    public DataSet GetReprtDetailsCatstime(string[,] sparameter, CommandType cmdtype)
    {
        return ExcProcedure("spGet_RptDetails_Catstime", sparameter, cmdtype);
    }
	//public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate, int location)
	//{
	//    string sCustno = custno.ToString();
	//    if (custno == 10066)
	//        sCustno = "NULL";
	//    string condition = "";
	//    if (location != 0)
	//        condition = "where locationid=" + location;
	//    if (journo == 0)
	//    {
	//        string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition;
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_B(" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition;
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_P(" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " order by 2,3  ";

	//        //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else if (journo == 2)////////////For Books
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_B (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    else if (journo == 3)/////////For Projects
	//        //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_P (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    else if (journo == 1)///////////////For Journal
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    else
	//        return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT * FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);

	//    /* if (custno == 10066)
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     else
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     */
	//}
	public bool HasPayments(string sCustno)
	{
		bool status = false;
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

	//public TandFInvoiceDS GetInvoicedJobs3Outstanding(int custno, int journo)
	//{
	//    string sCustno = custno.ToString();
	//    if (custno == 10066)
	//        sCustno = "NULL";
	//    if (journo == 0)
	//    {
	//        string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,0 as BTYPE FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ") UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,0 as BTYPE FROM P_INVOICED_ITEMS_OUTS_WIP_J (" + sCustno + ")";
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , OUTPRINTAREA, BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,BTYPE FROM P_INVOICED_ITEMS_OUTSTANDING_B(" + sCustno + ")";
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO,0 as BTYPE FROM P_INVOICED_ITEMS_OUTSTANDING_P(" + sCustno + ") ";
	//        if (this.HasPayments(sCustno))
	//        {
	//            Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME" +
	//                " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO,0 as BTYPE FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
	//        }
	//        else Qry += " order by 3,7,2";

	//        //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else if (journo == 2)////////////For Books
	//    {
	//        string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast(OUTPRINTAREA as CHAR(50)) as OUTPRINTAREA, cast(BISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER,cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTSTANDING_B(" + sCustno + ")";
	//        if (this.HasPayments(sCustno))
	//        {
	//            Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
	//        }
	//        else Qry += " order by 3,7,2";
	//        //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_B (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else if (journo == 3)/////////For Projects
	//    {
	//        //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
	//        string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast(PROJECTNUMBER as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTSTANDING_P(" + sCustno + ") ";
	//        if (this.HasPayments(sCustno))
	//        {
	//            Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
	//        }
	//        else Qry += " order by 3,7,2";
	//        //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_P (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else ///////////////For Journal
	//    {
	//        string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ") UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_OUTS_WIP_J (" + sCustno + ") ";
	//        if (this.HasPayments(sCustno))
	//        {
	//            Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT(" + sCustno + ") order by 3,7,2";
	//        }
	//        else Qry += " order by 3,2,10,7";
	//        //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }

	//    /* if (custno == 10066)
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     else
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     */
	//}

	//public TandFInvoiceDS GetInvoicedJobs3PaymentReceived(int custno, int journo, string fdate, string tDate)
	//{
	//    string sCustno = custno.ToString();
	//    if (custno == 10066)
	//        sCustno = "NULL";
	//    if (journo == 0)
	//    {
	//        //string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME FROM P_INVOICED_ITEMS_PAID_J (" + sCustno + ",'" + fdate + "','" + tDate + "')";
	//        //Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME FROM P_INVOICED_ITEMS_PAID_B(" + sCustno + ",'" + fdate + "','" + tDate + "')";
	//        //Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME FROM P_INVOICED_ITEMS_PAID_P(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 2,3  ";
	//        string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,INVTYPE, iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')";

	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , OUTPRINTAREA, BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_B(" + sCustno + ",'" + fdate + "','" + tDate + "')";
	//        Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE,INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_P(" + sCustno + ",'" + fdate + "','" + tDate + "') ";
	//        Qry += " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME" +
	//            " , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
	//        //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else if (journo == 2)////////////For Books
	//    {
	//        string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast(OUTPRINTAREA as CHAR(50)) as OUTPRINTAREA, cast(BISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER,cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_B(" + sCustno + ",'" + fdate + "','" + tDate + "')" +
	//            " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
	//        //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_B (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else if (journo == 3)/////////For Projects
	//    {
	//        //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
	//        string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast(PISBN as CHAR(50)) as BISBN, cast(PONUMBER as VARCHAR(55)) as PONUMBER, cast(PROJECTNUMBER as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_P(" + sCustno + ",'" + fdate + "','" + tDate + "') " +
	//                    " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
	//        //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_P (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }
	//    else ///////////////For Journal
	//    {
	//        string Qry = "SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_J (" + sCustno + ",'" + fdate + "','" + tDate + "') UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,INVTYPE, IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,PAYMENT_DATE,INVDNAME,cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, cast(0 as float) as CREDIT, CURRNO FROM P_INVOICED_ITEMS_PAID_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" +
	//                    " UNION SELECT cast('' as VARCHAR(50)) as JOBNO, 0 as IINNO,cast(CREDITED_DATE as date) as IDATE,CNAME, cast('' as VARCHAR(255)) as JOBTITLE,CATEGORY, INVTYPE, cast('' as CHAR(20)) as IISSUENO,CREDITID as INO, cast('' as CHAR(100)) as JOURCODE, cast('' as VARCHAR(255)) as CONCOUNTRY, 0 as TOTALPAGES,0 as ISARTICLEBASED,0 as NOOFARTICLES,0 as JOURNO,0 as TOTALPAGES_NOCOVER,CUSTNO1, cast('' as VARCHAR(50)) as GROUPBY,PAYMENT_DATE, cast('' as CHAR(50)) as INVDNAME , cast('' as CHAR(50)) as OUTPRINTAREA, cast('' as CHAR(50)) as BISBN, cast('' as VARCHAR(55)) as PONUMBER, cast('' as VARCHAR(50)) as PROJECTNUMBER, CREDITED_VALUE as CREDIT, CURRNO FROM P_CREDITED_ON_ACCOUNT_PAID(" + sCustno + ",'" + fdate + "','" + tDate + "') order by 3,7,2";
	//        //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_J (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
	//        return GetUserDefineDs(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
	//    }

	//    /* if (custno == 10066)
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     else
	//         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
	//     */
	//}

	public bool ApprovePayments(System.Collections.ArrayList acceptlist, string sModifiedBy)
	{
		int i = 0;
		object[] oQrys = new object[acceptlist.Count];
		bool status = false;
		string sSql = "", sDateNow = DateTime.Now.ToString("yyyy-MM-dd");
		foreach (System.Web.UI.WebControls.ListItem item in acceptlist)
		{
			sSql = "";
			switch (item.Text)
			{
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

	public bool CancelPayments(System.Collections.ArrayList cancellist, string sModifiedBy)
	{
		int i = 0;
		object[] oQrys = new object[cancellist.Count];
		bool status = false; string sSql = "";
		foreach (System.Web.UI.WebControls.ListItem item in cancellist)
		{
			sSql = "";
			switch (item.Text)
			{
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

	public DataSet getCustomerPaymentOnAccount(string sCustno)
	{
		return this.GetDataSet1("select * from credit_on_account where custno=" + sCustno + " and payment_date is null and obsolete is null order by credited_date");
	}

	public bool AddUpdatePaymentOnAccount(System.Collections.ArrayList list, string sModifiedBy)
	{
		int i = 0;
		object[] oQrys = new object[list.Count];
		bool status = false; string sSql = "";
		foreach (System.Web.UI.WebControls.ListItem item in list)
		{
			if (item.Text == "insert")
				sSql = "insert into credit_on_account(custno,credited_value,credited_date)values(" + item.Value.Split('|')[1] + "," + item.Value.Split('|')[2] + ",'" + item.Value.Split('|')[3] + "')";
			else
				sSql = "update credit_on_account set custno = " + item.Value.Split('|')[1] + ",credited_value=" + item.Value.Split('|')[2] + ",credited_date='" + item.Value.Split('|')[3] + "' where creditid=" + item.Value.Split('|')[0];
			oQrys[i] = sSql; i++;
		}

		if (oQrys.Length > 0 && this.Execute_Sql(oQrys))
			status = true;

		return status;
	}

	public bool DeletePaymentOnAccount(string sCreditID, string sModifiedBy)
	{
		object[] oQrys = new object[1];
		oQrys[0] = "update credit_on_account set obsolete = F_CURRENTDATE('MM/dd/yyyy hh:mm:ss'), PAYMENT_CONFIRM_BY='" + sModifiedBy + "' where creditid=" + sCreditID;
		return this.Execute_Sql(oQrys);
	}

	//private bool Execute_Sql(object[] oSaveitems)
	//{
	//    SqlConnection cnn = null;
	//    SqlCommand cmd = null;
	//    SqlTransaction trans = null;
	//    bool blnCheck = false;
	//    string strSql = "";
	//    int cnt = 0;
	//    string connString = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
	//    try
	//    {
	//        using (cnn = new SqlConnection(connString))
	//        {
	//            cnn.Open();
	//            trans = cnn.BeginTransaction();
	//            foreach (object obj in oSaveitems)
	//            {
	//                try
	//                {
	//                    strSql = obj.ToString();
	//                    cmd = new SqlCommand(strSql, cnn, trans);
	//                    cnt = cnt + cmd.ExecuteNonQuery();
	//                }
	//                catch (Exception ex)
	//                {
	//                    trans.Rollback();
	//                    throw new Exception(ex.ToString());
	//                }
	//            }
	//            trans.Commit();
	//            if (cnt > 0)
	//                blnCheck = true;
	//        }
	//        return blnCheck;
	//    }
	//    catch (Exception ex)
	//    {
	//        throw new Exception(ex.ToString());
	//    }
	//    finally
	//    {
	//        if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
	//        if (cmd != null) cmd.Dispose();
	//        //   if (trans != null) trans.Dispose();
	//    }
	//}

	public DataSet GetDespatchedJobs2(int custno, int journo)
	{
		string sCustno = custno.ToString();
		if (custno == 10066)
			sCustno = "NULL";
		if (journo == 4)//For WIP
            return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_WIP (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
		else if (journo == 2)////////////For Books
		{
			if (sCustno == "NULL")
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
			return null;
	}

	//Created By Subbu - 05 May 2010
    public DataSet GetDispatchedjobMail(int custno, int category, int conno, int flg)
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

	public bool UpdateApproveName(int empid, int ino, int Category, string comname)
	{
		SqlCommand oCmd = new SqlCommand();
		SqlTransaction oTran = null;
		DataSet upds = new DataSet();
		SqlCommand acmd = new SqlCommand();
		try
		{
			string SelectQry = "";
			string UpdateQry = "";
			int invno = 0;
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
			else if (Category == 1 && comname == "Approve")
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
				{
					throw ex; }
				finally
				{
					glds = null; }
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

				SqlDataAdapter da = new SqlDataAdapter(acmd);
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

	public DataSet ProdCustomer()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select custno,custname from customer_dp where custno not in (0,10066) order by custname", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdJournal(string customer)
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select   journo,jourcode,journame,issensitive from journal_dp where custno=" + customer + " order by jourcode,journame", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdStyle()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select typestyleno,typestyle from typestyle_dp order by typestyle", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdCoverStock()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select covmatno,material from covermaterial_dp order by material", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdTrimSize()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select pagetrimno,trimsize from pagetrim_dp order by trimsize", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdPaperType()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select papertypeno,papertype from papertype_dp order by papertype", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdPrinterCode()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select printno,printname,conno from printer_dp order by printname", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdTrimCode()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select   pagetrimno,trimcode from pagetrim_dp order by trimcode", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdPaperGSM()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select papergsmno,gsmweight from papergsm_dp order by gsmweight", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdPressType()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select presstypeno,presstypename from presstype_dp order by presstypename", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdContactName()
	{
		DataSet ds = new DataSet();
		try
		{
            return ExcProcedure("select conno,displayname from  contact_dp order by displayname", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet ProdPageStart()
	{
		DataSet ds = new DataSet();
		try
		{
			return ExcProcedure("select psno,psname from pagestyle_dp order by psno", null, CommandType.Text);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}

		return ds;
	}

	public DataSet GetJournalInfo(string Journo)
	{
		return GetDataSet("Exec spGet_Journal_Prod " + Journo + "", "JobInfo", CommandType.Text);
	}

	public string InsertProdJournal(string[] aJournal)
	{
		string Status = "";
		string sSql = "";
		SqlCommand oCmd = new SqlCommand();
		try
		{
			opencon();
			oCmd.CommandType = CommandType.Text;
			oCmd.CommandText = " spADD_Journal '"+ aJournal[0] +"','"+ aJournal[1] +"' ,'"+ aJournal[2] +"' ,'"+ aJournal[3] +"' ,'"+ aJournal[4] +"' ,'"+ aJournal[5] +"' ,'"+ aJournal[6] +"' ,'"+ aJournal[7] +"' ,'"+ aJournal[8] +"' ,'"+ aJournal[9] +"' ,'"+ aJournal[10] +"' ,'"+ aJournal[11] +"' ,'"+ aJournal[12] +"' ,'"+ aJournal[13] +"' ,'"+ aJournal[14] +"' ,'"+ aJournal[15] +"' ,'"+ aJournal[16] +"' ,'"+ aJournal[17] +"' ,'"+ aJournal[18] +"' ,'"+ aJournal[19] +"' ,'"+ aJournal[20] +"','"+ aJournal[21] +"' ,'"+ aJournal[22] +"' ,'"+ aJournal[23] +"' ,'"+ aJournal[24] +"' ,'"+ aJournal[25] +"' ,'"+ aJournal[26] +"' ,'"+ aJournal[27] +"' ,'"+ aJournal[28] +"' ,'"+ aJournal[29] +"', ,'"+ aJournal[30] +"', '"+ aJournal[31] +"'  ";
			oCmd.Connection = oConn;
			oCmd.ExecuteNonQuery();
			Status = "Saved";
		}
		catch (Exception oex)
		{
		}

		finally
		{
			closecon();
			oCmd = null;
		}

		return Status;
	}
	 public DataSet GetProductivityReport(string EmpId, string team_id, DateTime strStartDate, DateTime strEndDate)
    {
        try
        {
            
            string[,] param = { { "@employee_id", EmpId.ToString() },{ "@startdate", strStartDate.ToString() }, { "@enddate", strEndDate.ToString() }, { "@teamid", team_id.ToString() } };
            return ExcProcedurePrdJL("SPGet_ProductivityDetails", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
	
    private int AddDataset(string sQry, CommandType oType, string[,] oParams, Boolean bCommit)
    {
        try
        {
            SetTrans(oType, bCommit);
            if (oParams != null)
            {
                ocmd.Parameters.Clear();
                for (int i = 0; i <= oParams.GetUpperBound(0); i++)
                {
                    if (oParams[i, 1] == null || oParams[i, 1].ToString() == "" || oParams[i, 1].ToString() == "0" || oParams[i, 1].ToString().IndexOf("1/0001") > 0)
                        ocmd.Parameters.AddWithValue(oParams[i, 0].ToString(), null);
                    else
                    {
                        if (oParams[i, 1].ToString().ToLower().IndexOf(":00:00") != -1 || oParams[i, 1].ToString().ToLower().EndsWith(" pm") || oParams[i, 1].ToString().ToLower().EndsWith(" am"))
                        {
                            ocmd.Parameters.AddWithValue(oParams[i, 0].ToString(), oParams[i, 1].ToString().Substring(0, 10));
                            ocmd.Parameters[i].SqlDbType = SqlDbType.DateTime;
                        }
                        else
                            ocmd.Parameters.AddWithValue(oParams[i, 0].ToString(), oParams[i, 1].ToString());
                    }
                }
            }
            ocmd.CommandText = sQry;
        }
        catch (Exception oex)
        {
            RollbackTrans();
        }
        finally
        {
            if (bCommit)
                CommitTrans(bCommit);
        }
        return Convert.ToInt32(ocmd.ExecuteScalar());
    }
    private void RollbackTrans()
    {
        if (oTran != null)
        {
            oTran.Rollback();
            oTran.Dispose();
            oTran = null;
        }
        closecon();
        ocmd = null;
    }
    private void CommitTrans(Boolean bCommit)
    {
        try
        {
            if (bCommit)
            {
                if (oTran != null)
                {
                    oTran.Commit();
                    oTran.Dispose();
                    oTran = null;
                }
                closecon();
            }
        }
        catch (Exception oex) { }
    }
    private void SetTrans(CommandType oType, Boolean bSetTrans)
    {
        opencon();
        if (bSetTrans)
        {
            if (oTran != null)
            {
                try
                {
                    oTran.Commit();
                    oTran.Dispose();
                    oTran = null;
                }
                finally
                {
                    oTran.Dispose();
                    oTran = null;
                }
            }
            if (oTran == null)
            {
                oTran = oConn.BeginTransaction();
                ocmd.Transaction = oTran;
            }
        }
        ocmd.Connection = oConn;
        ocmd.CommandType = oType;
        ocmd.Parameters.Clear();
    }

    public bool UpdateInvDate(int ino)
    {
        SqlCommand oCmd = new SqlCommand();
        try
        {
            opencon();
            oCmd.CommandType = CommandType.Text;
            //oCmd.Transaction.Connection.BeginTransaction(); 
            oCmd.CommandText = "UPDATE ISSUE_DP SET IINVOICEDATE = '" + DateTime.Now.ToShortDateString() + "' WHERE INO = " + ino;
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

    public bool UpdateInvCompleted(string ino, int custno, int Email_Flg) //int ino)
    {
        //SqlCommand oCmd = new SqlCommand();
        // SqlTransaction otrans = null;
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
        //SqlCommand oCmd = new SqlCommand();
        // SqlTransaction otrans = null;
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
        SqlCommand cmdExc = new SqlCommand();
        SqlTransaction otrans = null;
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
    public void UpdatePreviewDate(string location_preview, string category, string idvalue, string preview_date)
    {
        switch (category)
        {
            case "1": //For journal
                if (location_preview.ToString().ToUpper() == "I")
                    ExcuteProc("update issue_dp set IndiaInv_preview=" + preview_date + " where ino=" + idvalue);
                else if (location_preview.ToString().ToUpper() == "D")
                    ExcuteProc("update issue_dp set DublinInv_preview=" + preview_date + " where ino=" + idvalue);
                else if (location_preview.ToString().ToUpper() == "SAVEPRINT")
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
                if (location_preview.ToString().ToUpper() == "I")
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

    public DataSet GetDataSet(string sProcName, string sDSName, CommandType sCmdType)
    {

        try
        {

            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcName;
            oCmd.CommandTimeout = 600;
            oCmd.Connection = oConn;
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
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

    //private TandFInvoiceDS GetUserDefineDs(string sProcName, string sDSName, CommandType sCmdType)
    //{
    //    try
    //    {

    //        opencon();
    //        SqlCommand oCmd = new SqlCommand();
    //        oCmd.CommandType = sCmdType;
    //        oCmd.CommandText = sProcName;
    //        oCmd.CommandTimeout = 600;
    //        oCmd.Connection = oConn;
    //        SqlDataAdapter da = new SqlDataAdapter(oCmd);
    //        TandFInvoiceDS userDs = new TandFInvoiceDS();
    //        da.Fill(userDs, sDSName);
    //        return userDs;
    //    }
    //    catch (Exception oex)
    //    {
    //        return null;
    //    }
    //    finally
    //    {
    //        closecon();
    //    }
    //}


    public DataSet GetJournalcode(string Qry)
    {
        return GetDataSet(Qry, "Journal", CommandType.Text);
    }

    public DataSet Getinvoiceconfig(string qry, CommandType cmdtype)
    {
        return GetDataSet(qry, "InvoiceConfig", cmdtype);
    }
    public bool RunQuery(string Qry, string dsName)
    {
        return GetBoolean(Qry, dsName, CommandType.Text);
    }
    private bool GetBoolean(string sProcName, string sDSName, CommandType sCmdType)
    {
        SqlCommand oCmd = new SqlCommand();
        //SqlTransaction OTran = null;
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
            SqlCommand oCmd = new SqlCommand(sSQL);
            oCmd.CommandTimeout = 600;
            oCmd.Connection = oConn;
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
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
    public DataSet GetAllCustomers_JobAllocation()
    {
        return GetDataSet("SELECT CUSTNO, CUSTNAME,FINSITENO FROM CUSTOMER_DP where CUSTNAME not like '%--ALL CUSTOMERS--%' ORDER BY CUSTNAME", "CUSTOMERS", CommandType.Text);
    }
    public DataSet GetJobInfo(string CustNo)
    {
        return GetDataSet("Exec sp_GetJobDet " + CustNo + "", "JobInfo", CommandType.Text);
    }
    public DataSet GetFor_Invoiceconfig(string typeid)
    {
        return GetDataSet("select * from P_INVOICECONFIGURATION_PB(" + typeid + ")", "InvoiceConfig", CommandType.StoredProcedure);
    }
    public bool updateinvconfig(string costtype, string invdescription, string recordno, string category)
    {
        string insertqry = string.Empty;
        if (category == "1")//For Projects
            insertqry = "update projects_dp set inv_costtypeid=" + costtype + ",inv_descriptionid=" + invdescription + " where projectno=" + recordno;
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
        string empno = iEmpNo.ToString();
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
    public DataSet GetCoactionsubmission(DateTime fdate, DateTime tdate, string acode, string duesub)
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
            qry += (qry.Contains("where")) ? " and " : " where ";
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
            sSqlQuery = "INSERT INTO LOGGEDEVENTS_DP (LEDATE ,ANO, INO, DNO, SNO, EMPNO, ISTIMESHEET) VALUES ('" + DateTime.Now.GetDateTimeFormats()[74].ToString() + "', NULL ," + sJobNumber + "," + sDeptID + "," + sStageID + "," + sEmpno + ",'Y')";
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
        return GetDataSet("exec P_GET_PROJECTFORMODULE", "Projects", CommandType.Text);
    }
    public bool insertprojectmodule(string projectno, string custno, string nitems, string ponumber)
    {
        string inqry = string.Empty;
        for (int i = 0; i < Convert.ToInt32(nitems); i++)
        {
            inqry = "insert into pro_modules_dp (PROJECTNO,CUSTNO,DNO,STNO,STYPENO,moponumber) values(" + projectno + "," + custno.ToString() + ",11,15,10077,'" + ponumber + "')";
            if (!GetBoolean(inqry, "Modules", CommandType.Text))
                return false;
        }
        return true;
    }


    public DataSet insertprojectmodule(string[,] sparameter, CommandType cmdtype)
    {
        return ExcProcedure("spInsertModule", sparameter, cmdtype);
    }


    public DataSet GetPEContact()
    {
        return GetDataSet("select conno,Rtrim(confirstname) + ' ' + Rtrim(consurname) as invdisplayname from contact_dp order by confirstname", "Contacts", CommandType.Text);
    }
    public DataSet GetProject_Modules(string projectno)
    {
        return GetDataSet("exec P_GET_PROJECTMODULE " + projectno + "", "ProjectModule", CommandType.Text);
    }
    public bool Update_ProjectModule(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
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
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public bool DeleteModules(string moduleno)
    {
        string dmod = "update pro_modules_dp set obsolete='" + DateTime.Now.ToShortDateString() + "' where MPROJNO in(" + moduleno + ")";
        return GetBoolean(dmod, "Module", CommandType.Text);
    }
    public DataSet GetsDataSet(string qry, CommandType ctype)
    {
        return GetDataSet(qry, "DataSet", ctype);
    }
    public bool UpdateCatsFailureLog(string uqry, string wqry)
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
        SqlCommand pcmd = new SqlCommand();
        jobperformanceds jds = new jobperformanceds();
        try
        {
            opencon();
            pcmd.Connection = oConn;
            pcmd.CommandText = qry;
            pcmd.CommandType = ctype;
            SqlDataAdapter pda = new SqlDataAdapter(pcmd);
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
        SqlCommand pcmd = new SqlCommand();
        projectmoduleds pds = new projectmoduleds();
        try
        {
            opencon();
            pcmd.Connection = oConn;
            pcmd.CommandText = qry;
            pcmd.CommandType = ctype;
            SqlDataAdapter pda = new SqlDataAdapter(pcmd);
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
        for (int qrycnt = 0; cqry.GetLength(0) > qrycnt; qrycnt++)
            if (cqry[qrycnt] != null && cqry[qrycnt].ToString() != "") ExcuteProc(cqry[qrycnt]);
    }
    public DataSet getcombinedissue(string qry, CommandType ctype)
    {
        return GetDataSet(qry, "Details", ctype);
    }
    public bool assignchildtoparentproject(string projectno, string parent_projectno)
    {
        return GetBoolean("update projects_dp set parent_projectno = " + parent_projectno + " where projectno in(" + projectno + ")", "Projects", CommandType.Text);
    }

    public DataSet GetTaskItem()
    {
        return GetDataSet("select EMPNO task_id,EMPNAME task_name from EMPLOYEE_DP where EMPPOSITION  like '%Department%' and EMPBARCODE >  6 ", "Task", CommandType.Text);
    }

    public DataSet GetEmpDet(int eid)
    {
        return GetDataSet("select EMPNO,EMPNAME,DNO from  employee_dp where  EMPNO=" + eid + "  ", "Employee", CommandType.Text);
    }

    public SqlParameter NewParameter(string parameterId, SqlDbType sqlType, int parameterSize, ParameterDirection parameterDirection, object parameterValue)
    {
        if (parameterId == null)
            throw (new ArgumentNullException("parameterId"));
        if (parameterId.Length == 0)
            throw (new ArgumentOutOfRangeException("parameterId"));

        SqlParameter newSqlParam = new SqlParameter();
        newSqlParam.ParameterName = parameterId;
        newSqlParam.SqlDbType = sqlType;
        newSqlParam.Direction = parameterDirection;

        if (parameterSize > 0)
            newSqlParam.Size = parameterSize;

        if (parameterValue != null)
            newSqlParam.Value = parameterValue;

        return newSqlParam;
    }

    public DataSet FillDataSet_SP(string sSPName, SqlParameter[] sqlParamColl)
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataAdapter adap = null;
        DataSet ds = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            ds = new DataSet();
            cmd = new SqlCommand(sSPName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParamColl != null && sqlParamColl.Length > 0)
            {
                IEnumerator Paraenum1 = sqlParamColl.GetEnumerator();
                while (Paraenum1.MoveNext())
                {
                    cmd.Parameters.Add(Paraenum1.Current);
                }
            }
            adap = new SqlDataAdapter(cmd);
            adap.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
        }
    }



    public bool Execute_SP(string sSPName, SqlParameter[] sqlParamColl)
    {
        int cnt = 0;
        bool blnCheck = false;
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            trans = cnn.BeginTransaction();
            cmd = new SqlCommand(sSPName, cnn, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParamColl != null && sqlParamColl.Length > 0)
            {
                IEnumerator Paraenum1 = sqlParamColl.GetEnumerator();
                while (Paraenum1.MoveNext())
                {
                    cmd.Parameters.Add(Paraenum1.Current);
                }
            }
            cnt = cmd.ExecuteNonQuery();
            trans.Commit();
            if (cnt > 0)
                blnCheck = true;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw new Exception(ex.ToString());
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
        }
        return blnCheck;
    }

    public string Execute_SP(string sSPName, SqlParameter[] sqlParamCol2, SqlParameter sqlParamOutput)
    {
        int cnt = 0;
        string sOutput = "";
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            trans = cnn.BeginTransaction();
            cmd = new SqlCommand(sSPName, cnn, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParamCol2 != null && sqlParamCol2.Length > 0)
            {
                IEnumerator Paraenum2 = sqlParamCol2.GetEnumerator();
                while (Paraenum2.MoveNext())
                {
                    cmd.Parameters.Add(Paraenum2.Current);
                }
            }
            cmd.Parameters.Add(sqlParamOutput);
            cmd.ExecuteNonQuery();
            trans.Commit();
            sOutput = sqlParamOutput.Value.ToString();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw new Exception(ex.ToString());
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
        }
        return sOutput;
    }
    public bool Execute_Sql(object[] oSaveitems)
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        bool blnCheck = false;
        string strSql = "";
        int cnt = 0;
        string connString = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
        try
        {
            using (cnn = new SqlConnection(connString))
            {
                cnn.Open();
                trans = cnn.BeginTransaction();
                foreach (object obj in oSaveitems)
                {
                    try
                    {
                        strSql = obj.ToString();
                        cmd = new SqlCommand(strSql, cnn, trans);
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

        }
    }




    public int ExcuteProcedure(string sProcName, string paramcollection, string paramnames, string paramtypes, string paramdirections, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;
        string[] ParamType;
        string[] ParamDir;
        bool flg = false;
        string OutparamName = "";
        ParamVal = paramcollection.Split(separator);
        ParamName = paramnames.Split(separator);
        ParamType = paramtypes.Split(separator);
        ParamDir = paramdirections.Split(separator);

        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.Parameters.Clear();
            int i;
            for (i = 0; i < ParamName.GetLength(0); i++)
            {

                if (ParamType[i].ToString().ToUpper() == "INT" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsINT(ocmd, ParamName[i].ToString(), Convert.ToInt32(ParamVal[i]), SqlDbType.Int, ParameterDirection.Input);
                if (ParamType[i].ToString().ToUpper() == "INT" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                {
                    addOutPutParams(ocmd, ParamName[i].ToString(), SqlDbType.Int, ParameterDirection.Output);
                    flg = true;
                    OutparamName = ParamName[i].ToString();
                }
                if (ParamType[i].ToString().ToUpper() == "VARCHAR" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsSTR(ocmd, ParamName[i].ToString(), ParamVal[i].ToString(), SqlDbType.VarChar, 600, ParameterDirection.Input);
                if (ParamType[i].ToString().ToUpper() == "VARCHAR" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addOutPutParams(ocmd, ParamName[i].ToString(), SqlDbType.VarChar, ParameterDirection.Output);
                if (ParamType[i].ToString().ToUpper() == "DATE" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsDATE(ocmd, ParamName[i].ToString(), Convert.ToDateTime(ParamVal[i]), SqlDbType.DateTime, ParameterDirection.Input);
                if (ParamType[i].ToString().ToUpper() == "DATE" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addParamsDATE(ocmd, ParamName[i].ToString(), Convert.ToDateTime(ParamVal[i]), SqlDbType.DateTime, ParameterDirection.Output);
                if (ParamType[i].ToString().ToUpper() == "DECIMAL" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addParamsDecimal(ocmd, ParamName[i].ToString(), Convert.ToDecimal(ParamVal[i]), SqlDbType.Decimal, ParameterDirection.Output);
                if (ParamType[i].ToString().ToUpper() == "DECIMAL" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsDecimal(ocmd, ParamName[i].ToString(), Convert.ToDecimal(ParamVal[i]), SqlDbType.Decimal, ParameterDirection.Input);
            }
            ocmd.ExecuteNonQuery();
            if (flg == true)
                return Convert.ToInt32(ocmd.Parameters[OutparamName].Value);
            else
                return 0;
        }
        catch (Exception ex)
        {
            ocmd = null;
            closecon();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }
    private void addParamsINT(SqlCommand oCmmd, string sName, int sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    private void addParamsDATE(SqlCommand oCmmd, string sName, DateTime sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    private void addParamsDecimal(SqlCommand oCmmd, string sName, decimal sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }

    private void addOutPutParams(SqlCommand oCmmd, string sName, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        //oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;

    }

    public DataSet ExcProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;

        //string[] ParamType;
        //string[] ParamDir;
        // ParamVal = paramcollection.Split(separator);
        //ParamName = paramnames.Split(separator);
        //ParamType = paramtypes.Split(separator);
        //ParamDir = paramdirections.Split(separator);

        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.CommandTimeout = 600000;
            ocmd.Parameters.Clear();
            int i;
          
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);    
                }
            }
            
            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            sqlad.Fill(ods, "GetDetails");
            if (ods == null || ods.Tables[0].Rows.Count <= 0)
            {
                ods = null;
            }
            return ods;
        }
        catch (Exception ex)
        {
            ocmd = null;
            closecon();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }
    //kalimuthu 06/22/2014
    public int ExcSProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        SqlTransaction sqltrans = null;
        char[] separator = new char[] { ',' };
        bool flg = false;
        string OutparamName = "";
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            sqltrans = oConn.BeginTransaction();
            ocmd.Transaction = sqltrans;
            ocmd.Parameters.Clear();
            int i;
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {

                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        //ocmd.Parameters[sparameter[i, 0]].Direction = ParameterDirection.Output;
                        flg = true;
                        OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);
                }
            }
            ocmd.ExecuteNonQuery();
            sqltrans.Commit();
            if (flg == true)
                return Convert.ToInt32(ocmd.Parameters[OutparamName].Value);
            else
                return 0;
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }

    //T.Subash Mathankumar 09/25/2014

    public string GetDownloadPath(GridView ProductionReport, string strFileName)
    {
        string strPath = "";
        StringBuilder strVal = new StringBuilder();
        int intColumns = 0;
        try
        {

            for (int i = 0; i < ProductionReport.Columns.Count; i++)
            {
                if (ProductionReport.Columns[i].Visible == true)
                {
                    intColumns = intColumns + 1;
                }
            }

            strVal.Append("<table width='100%' align='Center' style='border:1px solid green;'>");
            strVal.Append("<tr><td align='center'  style='font-weight: bold;font-size: 11px;font-family: Segoe UI;'colspan=" + intColumns.ToString() + ">" + strFileName + "</td></tr>");
            strVal.Append("<tr>");
            for (int i = 0; i < ProductionReport.Columns.Count; i++)
            {
                if (ProductionReport.Columns[i].Visible == true)
                {
                    strVal.Append("<td align='center' width='" + ProductionReport.Columns[i].ItemStyle.Width + "px'  style='font-weight: bold;font-size: 11px;font-family: Segoe UI;color: #FFF;background:green;'>" + ProductionReport.Columns[i].HeaderText + "</td>");
                }
            }
            strVal.Append("</tr>");

            for (int j = 0; j < ProductionReport.Rows.Count; j++)
            {

                strVal.Append("<tr>");


                for (int i = 0; i < ProductionReport.Columns.Count; i++)
                {
                    if (ProductionReport.Columns[i].Visible == true)
                    {

                        if ((j + 1) % 2 == 0)
                        {
                            if (i == 0)
                            {
                                strVal.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                            }
                            else
                            {
                                strVal.Append("<td align='left' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                strVal.Append("<td align='center' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                            }
                            else
                            {
                                strVal.Append("<td align='left' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                            }
                        }



                    }
                }
                strVal.Append("</tr>");
            }
            strVal.Append("</table>");

            strPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + strFileName + "_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "") + ".xls";
            System.IO.StreamWriter file = new System.IO.StreamWriter(strPath);
            file.WriteLine(strVal);
            file.Close();
            return strPath;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    public string AddCustomerDetails(DataSet dsCustomer, int intMode, int intofficeMode, int intFinanceMode)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        SqlParameter sqlParm = new SqlParameter();
        string strStatus = "";
        try
        {
            opencon();
            ocmd.CommandType = CommandType.StoredProcedure;
            ocmd.CommandText = "mis_sp_add_customer";
            ocmd.Connection = oConn;
            ocmd.Parameters.Clear();

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.Int;
            sqlParm.ParameterName = "@intMode";
            sqlParm.Value = intMode;
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.Int;
            sqlParm.ParameterName = "@intOfficeMode";
            sqlParm.Value = intofficeMode;
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.Int;
            sqlParm.ParameterName = "@intFinanceMode";
            sqlParm.Value = intFinanceMode;
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.Structured;
            sqlParm.ParameterName = "@tblCONTACTSITE_DP";
            sqlParm.Value = dsCustomer.Tables["CONTACTSITE_DP"];
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.Structured;
            sqlParm.ParameterName = "@tblCUSTOMER_DP";
            sqlParm.Value = dsCustomer.Tables["CUSTOMER_DP"];
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.Structured;
            sqlParm.ParameterName = "@tblFINANCIALSITE_DP";
            sqlParm.Value = dsCustomer.Tables["FINANCIALSITE_DP"];
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);

            sqlParm = new SqlParameter();
            sqlParm.SqlDbType = SqlDbType.NVarChar;
            sqlParm.Size = -1;
            sqlParm.ParameterName = "@strStatus";
            sqlParm.Value = strStatus;
            sqlParm.Direction = ParameterDirection.Output;
            ocmd.Parameters.Add(sqlParm);

            ocmd.ExecuteNonQuery();
            strStatus = Convert.ToString(ocmd.Parameters["@strStatus"].Value);
            return strStatus;

        }
        catch (Exception ex)
        {
            ocmd = null;
            closecon();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }

    public DataSet GetEmployeeDetails()
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        SqlParameter sqlParm = new SqlParameter();
        string strStatus = "";
        SqlDataAdapter daAdapt = new SqlDataAdapter();
        try
        {
            openconvote();
            ocmd.CommandType = CommandType.StoredProcedure;
            ocmd.CommandText = "hrm_sp_get_emp_dtl";
            ocmd.Connection = oConn;
            daAdapt = new SqlDataAdapter(ocmd);
            daAdapt.Fill(ods,"EmployeeDetails");


            return ods;

        }
        catch (Exception ex)
        {
            ocmd = null;
            closecon();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }

    public DataSet GetOfficeDetails(int intConsiteNo)
    {
        try
        {

            string[,] param = { { "@intOffice", intConsiteNo.ToString() } };
            return ExcProcedure("mis_sp_get_office_dtl", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetFinanceDetails(int intFinanceNo)
    {
        try
        {

            string[,] param = { { "@intFinance", intFinanceNo.ToString() } };
            return ExcProcedure("mis_sp_get_finance_dtl", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetCustomerDetails(int intMode)
    {
        try
        {

            string[,] param = { { "@intMode", intMode.ToString() } };
            return ExcProcedure("mis_sp_pr_customer", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetDepartmentDetails(int intMode)
    {
        try
        {

            string[,] param = { { "@intMode", intMode.ToString() } };
            return ExcProcedure("mis_sp_get_department", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetProductionReport(int intMode,string strStartDate,string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_ARTICLE_RECEIVED", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetIssueDispatched(int intMode, string strStartDate, string strEndDate, string strCustomer, string opt , string optissue)
    {
        try
        {
            string[,] param = { { "@intMode", intMode.ToString() }, { "@stdate", strStartDate }, { "@eddate", strEndDate }, { "@strCusno", strCustomer }, { "@optissue", optissue }, { "@opt", opt } };
            return ExcProcedurePrdJL("mis_sp_get_issue_dispatched", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetIssueReceived(int intMode, string strStartDate, string strEndDate, string strCustomer, string opt, string strInHouse)
    {
        try
        {
            string[,] param = { { "@intMode", intMode.ToString() }, { "@stdate", strStartDate }, { "@eddate", strEndDate }, { "@strCusno", strCustomer }, { "@opt", opt }, { "@inhouse", strInHouse } };
            return ExcProcedurePrdJL("mis_sp_get_issue_received", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetProductionReportNIA(int intMode, string strInvoicing, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STRINVOICING", strInvoicing }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_ARTICLE_RECEIVED", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetProductionReportDespatched(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_ARTICLE_DISPATCH", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetProductionReportArticleDue(int intMode, string strStartDate, string strEndDate, string strCountry, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRCOUNTRY", strCountry }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_ARTICLE_DUE", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetProductionReportArticleDepartment(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRDNO", strCustomer } };
            return ExcProcedurePrdJL("mis_sp_get_pr_article_department", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetProductionReportArticleOffhold(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRDNO", strCustomer } };
            return ExcProcedurePrdJL("mis_sp_get_pr_article_offhold", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetProductionReportArticleOnhold(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRDNO", strCustomer } };
            return ExcProcedurePrdJL("mis_sp_get_article_onhold", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet GetProductionReportIssueDepartment(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRDNO", strCustomer } };
            return ExcProcedurePrdJL("mis_sp_get_pr_issue_department", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  public DataSet GetProductionReportBookRec(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_Book_Received", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetProductionReportBookProgress(int intMode, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_Book_Progress", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetProductionReportProjectRec(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_Project_Received", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetProductionReportProjectProgress(int intMode, string strStartDate, string strEndDate, string strCustomer)
    {
        try
        {

            string[,] param = { { "@INTMODE", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRFINNO", strCustomer } };
            return ExcProcedurePrdJL("MIS_SP_GET_PR_Project_Progress", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    

    private void openconvote()
    {
        //sConStr = ConfigurationManager.ConnectionStrings["conStrIB"].ToString();
        sConStr = ConfigurationManager.ConnectionStrings["conStrVote"].ToString();
        oConn = new SqlConnection(sConStr);
        oConn.Open();
    }
	
	 public DataSet ExcProcedurePrdJL(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;

        //string[] ParamType;
        //string[] ParamDir;
        // ParamVal = paramcollection.Split(separator);
        //ParamName = paramnames.Split(separator);
        //ParamType = paramtypes.Split(separator);
        //ParamDir = paramdirections.Split(separator);

        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.CommandTimeout = 60000;
            ocmd.Parameters.Clear();
            int i;

            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;

                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);
                }
            }

            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            sqlad.Fill(ods, "GetDetails");
            if (ods.Tables.Count < 0)
            {
                if (ods == null || ods.Tables[0].Rows.Count <= 0 )
                {
                    ods = null;
                }
            }
            return ods;
        }
        catch (Exception ex)
        {
            ocmd = null;
            closecon();
            throw ex;
        }
        finally
        {
            ocmd = null;
            closecon();
        }

    }
     public void UpdateDollarValues(string invNo, string Doolar)
     {
         ExcuteProc("update Projects_dp set usconvertvalue=" + Doolar + " where invno=" + invNo);
     }
     public void InsertPEName(string Fname, string SName, string Email)
     {
         ExcuteProc("insert into CONTACT_DP(CUSTNO,CONSURNAME,CONFIRSTNAME,CONEMAIL,DISPLAYNAME,INVCONEMAIL,INVDISPLAYNAME) values (2556,'" + SName + "','" + Fname + "','" + Email + "','" + Fname + " " + SName + "','" + Email + "','" + Fname + " " + SName + "')");
     }
	 
	  public DataSet GetContactCategory(string strSearch)
     {
         try
         {

             string[,] param = { { "@strSearch", strSearch } };
             return ExcProcedurePrdJL("sp_mis_contacttype", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     public DataSet GetContactName(int intConsiteNo,int intCategory, string strSearch)
     {
         try
         {

             string[,] param = { { "@strSearch", strSearch }, { "@intConsiteno", intConsiteNo.ToString() }, { "@intConCategory", intCategory.ToString() } };
             return ExcProcedurePrdJL("sp_mis_contactname", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     public DataSet GetContactCompany(string strSearch)
     {
         try
         {

             string[,] param = { { "@strSearch", strSearch } };
             return ExcProcedurePrdJL("SP_MIS_CONTACTCOMPANY", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     public DataSet GetContactResponsibilty(string strSearch)
     {
         try
         {

             string[,] param = { { "@strSearch", strSearch } };
             return ExcProcedurePrdJL("SP_MIS_CONTACTRESPONSIBILITY", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     public DataSet GetContactRecords(string intCategory, string intConsiteNo, string intConno)
     {
         try
         {

             string[,] param = { { "@intConnno", intConno.ToString() }, { "@intConsiteNo", intConsiteNo.ToString() }, { "@intCategory", intCategory.ToString() } };
             return ExcProcedurePrdJL("sp_mis_get_contact_dtl", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     public DataSet GetContactTable(string strConType)
     {
         try
         {

             string[,] param = { { "@intConTypeNo", strConType } };
             return ExcProcedurePrdJL("sp_mis_contact_structure", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
	 
	  public DataSet GetCustomerCheck(string strCustomer)
     {
         try
         {

             string[,] param = { { "@strCustomer", strCustomer } };
             return ExcProcedurePrdJL("sp_get_customer_details", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

     public Int32 GetCustNo(string strConsiteNo)
     {
         SqlCommand ocmd = new SqlCommand();
         DataSet ods = new DataSet();
         char[] separator = new char[] { ',' };
         string[] ParamVal;
         string[] ParamName;

         try
         {
             opencon();
             ocmd.CommandType = CommandType.StoredProcedure;
             ocmd.CommandText = "sp_mis_get_custno";
             ocmd.Connection = oConn;
             ocmd.CommandTimeout = 60000;
             ocmd.Parameters.Clear();
             int intCustNo;

             SqlParameter sqlparam1 = new SqlParameter("@intConsiteNo", SqlDbType.Int, 100);
             sqlparam1.Direction = ParameterDirection.Input;
             sqlparam1.Value = Convert.ToInt32(strConsiteNo);

             SqlParameter sqlparam2 = new SqlParameter("@intCustNo", SqlDbType.Int, 0);
             sqlparam2.Direction = ParameterDirection.Output;
             sqlparam2.Value = Convert.ToInt32(0);

             ocmd.Parameters.Add(sqlparam1);
             ocmd.Parameters.Add(sqlparam2);
             ocmd.ExecuteNonQuery();

             intCustNo = Convert.ToInt32(ocmd.Parameters["@intCustNo"].Value);

             return intCustNo;
         }
         catch (Exception ex)
         {
             throw ex;
         }
         finally
         {
             ocmd = null;
             closecon();
         }
     }

     public string AddContactDetails(DataSet dsContact, int intMode,int conType)
     {
         SqlCommand ocmd = new SqlCommand();
         DataSet ods = new DataSet();
         SqlParameter sqlParm = new SqlParameter();
         string strStatus = "";
         try
         {
             opencon();
             ocmd.CommandType = CommandType.StoredProcedure;
             ocmd.CommandText = "sp_mis_add_contact_dtls";
             ocmd.Connection = oConn;
             ocmd.Parameters.Clear();

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Int;
             sqlParm.ParameterName = "@intMode";
             sqlParm.Value = intMode;
             sqlParm.Direction = ParameterDirection.Input;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Int;
             sqlParm.ParameterName = "@intConType";
             sqlParm.Value = conType;
             sqlParm.Direction = ParameterDirection.Input;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Structured;
             sqlParm.ParameterName = "@tblContact";
             sqlParm.Value = dsContact.Tables[1];
             sqlParm.Direction = ParameterDirection.Input;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Structured;
             sqlParm.ParameterName = "@tblPdfContact";
             sqlParm.Value = dsContact.Tables[0];
             sqlParm.Direction = ParameterDirection.Input;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Structured;
             sqlParm.ParameterName = "@tblRole";
             sqlParm.Value = dsContact.Tables[2];
             sqlParm.Direction = ParameterDirection.Input;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Int;
             sqlParm.ParameterName = "@intResult";
             sqlParm.Value = -1;
             sqlParm.Direction = ParameterDirection.Output;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.Int;
             sqlParm.ParameterName = "@intCono";
             sqlParm.Value = 0;
             sqlParm.Direction = ParameterDirection.Output;
             ocmd.Parameters.Add(sqlParm);

             sqlParm = new SqlParameter();
             sqlParm.SqlDbType = SqlDbType.NVarChar;
             sqlParm.Size = -1;
             sqlParm.ParameterName = "@strResult";
             sqlParm.Value = "";
             sqlParm.Direction = ParameterDirection.Output;
             ocmd.Parameters.Add(sqlParm);

             ocmd.ExecuteNonQuery();
             
             int intResult = Convert.ToInt32(ocmd.Parameters["@intResult"].Value);
             
             strStatus = Convert.ToString(ocmd.Parameters["@strResult"].Value);

             if (intResult == 1 || intResult == 3)
             {
                 strStatus = "Successfully saved.";
             }
             else if (intResult == 2)
             {
                 strStatus = strStatus;
             }
             else
             {
                 strStatus = "Error.";
             }

             return strStatus;

         }
         catch (Exception ex)
         {
             ocmd = null;
             closecon();
             throw ex;
         }
         finally
         {
             ocmd = null;
             closecon();
         }

     }
     public DataSet GetProductionReportBookDesp(string intMode, string strStartDate, string strEndDate, string strCustomer)
     {
         try
         {

             string[,] param = { { "@Invoiced", intMode.ToString() }, { "@STARTDATE", strStartDate }, { "@ENDDATE", strEndDate }, { "@STRFINNO", strCustomer } };
             return ExcProcedurePrdJL("MIS_SP_GET_PR_Book_DesPatched", param, CommandType.StoredProcedure);

         }
         catch (Exception ex)
         {
             throw ex;
         }
     }
    
}



