using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.IO;
using System.Xml;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Text;
using System.Diagnostics;
using Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Collections.Generic;
//using System.Linq;


/// <summary>
/// Summary description for ReportView_IBSQL
/// </summary>
public class ReportView_IBSQL
{

    //------updated by Mugundhan for XML---------------

    int i = 1;
    string field = "field";
    string name = "name";

    string Description = "Description";
    string AccountCode1 = "AccountCode1";
    string AccountCode2 = "AccountCode2";
    string AccountCode3 = "AccountCode3";
    string AccountCode4 = "AccountCode4";
    string AccountCode5 = "AccountCode5";
    string AccountCode6 = "AccountCode6";
    string AccountCode7 = "AccountCode7";
    string AccountCode8 = "AccountCode8";
    string LineTax = "LineTax";
    string LineValue = "LineValue";
    string LineSense = "LineSense";

    //--------------------------------------------------

    SqlConnection RptConnection;
    SqlDataAdapter RptAdaptor;
    //SqlDataAdapter RptAdaptor2;
    //SqlDataAdapter BHORptAdaptor;
    BHODataSet BHRptDataSet;
    Invoice_Details RptDataSet;
    InvoiceBooks RptDataSet2;
    InvoiceBooksAddress RptDataSet3;
    InvoiceProjects RptDataSet4;
    public ReportDocument RptDoc, MedicalEduSub, SubRptDoc;
    string InvoiceNo;
    public int IssueNo;
    double PAmount;
    double CAmount;
    double SAMAmount;
    //ReportDocument XlsDoc;
    public string location = "";
    string Currency = "euro";
    string Xmllocation = "";
    string FName = string.Empty;
    string MPath;
    int CustCategory;
    //string XlsRptName = "";
    string BindQry = "";
    string RptType = "";
    string iInvoiceNo = "";
    string InvoiceDate = "";
    string InvItem = "";
    string filename = "";
    public string sFilePath = "";
    Datasource_IBSQL DB_IBSQL = new Datasource_IBSQL();
    ConnectionInfo ConnInfo = new ConnectionInfo();
    //
    // TODO: Add constructor logic here
    //
    //public Report()
    //{
    //    RptConnection = DBOpen();
    //}
    //~Report()
    //{
    //    DBClose(RptConnection);
    //}


    /// Win32 API import for getting the process Id.
    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowThreadProcessId(IntPtr hwnd, out IntPtr ProcessId);

    public string sInvoiceNo
    {
        get { return iInvoiceNo; }
        set { iInvoiceNo = value; }
    }
    public SqlConnection DBOpen()
    {
        SqlConnection DBCon = null;
        DBCon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive"].ToString());
        DBCon.Open();

        return DBCon;
    }
    public SqlConnection DBLiveOpen()
    {
        SqlConnection DBCon = null;
        DBCon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive"].ToString());
        DBCon.Open();

        return DBCon;
    }
    public SqlConnection DBInvoice()
    {
        SqlConnection DBCon = null;
        //DBCon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrInvoice"].ToString());
        DBCon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrIBLive"].ToString());
        DBCon.Open();

        return DBCon;
    }

    public void DBClose(SqlConnection DBCon)
    {
        if (DBCon != null)
        {
            if (DBCon.State != ConnectionState.Closed)
                DBCon.Close();
            DBCon.Dispose();
        }
    }

    public void InvoiceReport(int RIssueNo, string Location, string MapPath, int Category)
    {
        MPath = MapPath;
        location = Location;
        CustCategory = Category;
        IssueNo = RIssueNo;
        try
        {
            RptConnection = DBInvoice();
            if (Category == 2)/////////For Books
            {
                RptAdaptor = new SqlDataAdapter("Select * from SP_SELECT_BOOKS_INVOICE_NEW S LEFT JOIN CONTACTSITE_DP C ON S.CUSTNO1=C.CUSTNO where BNO1=" + IssueNo + "", RptConnection);
                RptDataSet2 = new InvoiceBooks();
                RptAdaptor.Fill(RptDataSet2);
            }
            else if (Category == 3)/////For Projects
            {

                /*BindQry = "SELECT  PCREDITED_IND,PCCNO,PCREDITED,PEMAIL_FLAG_IND,PINVOICED_IND,PINVOICEDDATE_IND,CONTACT_DP.CONEMAIL," +
                        " CONTACT_DP.CONEMAIL2, FINANCIALSITE_DP.FINSITEMAIL, FINANCIALSITE_DP.FINSITEMAIL1," +
                        " FINANCIALSITE_DP.FINSITECONTACT,FINANCIALSITE_DP.FINSITECONTACT1,FINANCIALSITE_DP.FINSITEMAILOPTION,PEMAIL_FLAG,customer_Dp.custemail," +
                        " PNOOFPAGES, (PNOOFCHARGEDPAGES) AS PCPAGES, (PNOOFCHARGEDARTICLES) AS PCARTICLES, PCOST, PROJECTNO," +
                        " PROJECTS_DP.CUSTNO, CUSTOMER_DP.CUSTNAME, PCODE, PTITLE, PRECEIVEDDATE, PDUEDATE, PCOMPLETEDDATE, PRINTNO, PPRODEDNO," +
                        " PPRODMANNO, PADDITEMS, PADDCHARGES, PINVOICED, PINVOICEDDATE, INVNO, PDESCRIPTION, PCOMMENTS, PDIGITAL," +
                        " PROJECTS_DP.DIGITALPRODNO, PROJECTS_DP.TPLATNO, FINSITENO, DNO, STNO, EMPNO, STYPENO, PCNO_2007, PCNO_2008,PCNO_2009,PROJECTS_DP.CONNO," +
                        " PDESPATCHED, PACOSTDESC1, PACOSTDESC2, PACOSTDESC3, PACOSTDESC4, PACOSTDESC5, PACNO1, PACNO2, PACNO3, PACNO4, PACNO5," +
                        " PROJECTS_DP.PAQTY1, PROJECTS_DP.PAQTY2, PROJECTS_DP.PAQTY3, PROJECTS_DP.PAQTY4, PROJECTS_DP.PAQTY5, " +
                        " F_ROUNDFLOAT(PROJECTS_DP.PAQTY1,0.5) AS PAQTY1, F_ROUNDFLOAT(PROJECTS_DP.PAQTY2,0.5) AS PAQTY2," +
                        " F_ROUNDFLOAT(PROJECTS_DP.PAQTY3,0.5) AS PAQTY3, F_ROUNDFLOAT(PROJECTS_DP.PAQTY4,0.5) AS PAQTY4," +
                        " F_ROUNDFLOAT(PROJECTS_DP.PAQTY5,0.5) AS PAQTY5, F_LRTRIM(CONTACT_DP.CONFIRSTNAME) || ' ' || F_LRTRIM(CONTACT_DP.CONSURNAME) " +
                        " as CONFULLNAME,DIGITALPRODUCTS_DP.PRODCODE, TYPESETPLATFORM_DP.TPLATCODE, DEPARTMENT_DP.DNAME, STATUS_DP.STDESCRIPTION, " +
                        " EMPLOYEE_DP.EMPNAME, STYPE_DP.STYPENAME, PROJECTS_DP.PONUMBER, PROJECTS_DP.PNOOFCHARACTERS, " +
                        " financialsite_dp.FINSITENAME,financialsite_dp.FINSITEADDRESS1,financialsite_dp.FINSITEADDRESS2," +
                        " financialsite_dp.FINSITEADDRESS3,financialsite_dp.FINSITEADDRESS4,financialsite_dp.FINSITEADDRESS5," +
                        " financialsite_dp.FINSITEADDRESS6,financialsite_dp.FINSITEVATNO,PJOBNUMBER,PODATE,PISBN,INVDISPLAYNAME,CONCOUNTRY,PROJECTNUMBER " +
                        " FROM PROJECTS_DP LEFT JOIN CUSTOMER_DP ON PROJECTS_DP.CUSTNO=CUSTOMER_DP.CUSTNO LEFT JOIN DIGITALPRODUCTS_DP ON " +
                        " PROJECTS_DP.DIGITALPRODNO=DIGITALPRODUCTS_DP.DIGITALPRODNO LEFT JOIN TYPESETPLATFORM_DP ON " +
                        " PROJECTS_DP.TPLATNO=TYPESETPLATFORM_DP.TPLATNO LEFT JOIN CONTACT_DP ON PROJECTS_DP.CONNO=CONTACT_DP.CONNO" +
                        " LEFT JOIN CONTACTSITE_DP ON CUSTOMER_DP.CUSTNO=CONTACTSITE_DP.CUSTNO LEFT JOIN " +
                        " DEPARTMENT_DP ON PROJECTS_DP.DNO=DEPARTMENT_DP.DNO LEFT JOIN STATUS_DP ON PROJECTS_DP.STNO=STATUS_DP.STNO LEFT JOIN " +
                        " EMPLOYEE_DP ON PROJECTS_DP.EMPNO=EMPLOYEE_DP.EMPNO LEFT JOIN STYPE_DP ON PROJECTS_DP.STYPENO=STYPE_DP.STYPENO LEFT JOIN " +
                        " FINANCIALSITE_DP ON PROJECTS_DP.FINSITENO=FINANCIALSITE_DP.FINSITENO WHERE PROJECTS_DP.projectno=" + IssueNo;
                 */


                BindQry = "SELECT * FROM SP_GET_STARTECH_PROJECTSINV(" + IssueNo + ")";

                RptAdaptor = new SqlDataAdapter(BindQry, RptConnection);
                RptDataSet4 = new InvoiceProjects();
                RptAdaptor.Fill(RptDataSet4);

            }
            else////////////////////For Journal
            {
                RptAdaptor = new SqlDataAdapter("Select * from SP_GET_INVOICE_DETAILS(" + IssueNo.ToString() + ")  a join financialsite_dp  b on B.finsiteno=a.finno", RptConnection);
                RptDataSet = new Invoice_Details();
                RptAdaptor.Fill(RptDataSet);
            }
            PAmount = GetPriceValue('T');

        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            //DBClose(RptConnection);
        }

    }

    //public ReportDocument GetReport(string CustNo,string RptType1)
    public string GetReport(string CustNo, string RptType1, string InvValupdatestr)
    {

        try
        {
            //Comment on 10 May 2010 For Generate XLS File in Both(India and Dublin) Invoice.
            //if (RptType1 == "XLS" && InvoiceNo != null && InvoiceNo == "")
            //{
            //    return "";
            //}
            RptType = RptType1;
            RptDoc = new ReportDocument();
            RptDoc.PrintOptions.PaperSize = PaperSize.PaperA4;
            if (CustCategory == 2)////////////For Books
            {
                //RptAdaptor.Fill(RptDataSet2);
                FName = GetReportName(RptDataSet2.Tables[1].Rows[0]["BNO1"].ToString(), CustNo);
                RptDoc.FileName = FName;
                RptDoc.SetDataSource(RptDataSet2.Tables[1]);

                if (location == "d")/////////////For SubReport(SubReport use in Dublin Report)
                {
                    SubRptDoc = new ReportDocument();
                    RptDataSet3 = new InvoiceBooksAddress();
                    //RptAdaptor = new SqlDataAdapter("select * from customer_dp x,financialsite_dp y where x.finsiteno=y.finsiteno and x.custno='" + CustNo + "'", RptConnection);
                    RptAdaptor = new  SqlDataAdapter("select * from financialsite_dp a,book_dp b  where a.finsiteno=b.finsiteno and b.bno=" + RptDataSet2.Tables[1].Rows[0]["BNO1"].ToString() + " and a.finsiteno=" + RptDataSet2.Tables[1].Rows[0]["FINSITENO1"].ToString(), RptConnection);
                    RptAdaptor.Fill(RptDataSet3);
                    DataColumn dc = new DataColumn("Btype1", typeof(int));
                    RptDataSet3.Tables[1].Columns.Add(dc);
                    DataRow dr;
                    dr = RptDataSet3.Tables[1].Rows[0];
                    dr["Btype1"] = Convert.ToInt32(RptDataSet2.Tables[1].Rows[0]["Btype1"]);
                    dr.AcceptChanges();
                    RptDataSet3.AcceptChanges();

                    SubRptDoc = RptDoc.Subreports[0];
                    SubRptDoc.SetDataSource(RptDataSet3.Tables[1]);
                }

            }
            else if (CustCategory == 3)////////////For Projects
            {

                //if (RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10104" || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10123")//For StarTechnology || Lionbridge
                RptDataSet4.Tables[1].Columns.Add("INDIA_PRICEVALUE"); RptDataSet4.Tables[1].Columns.Add("INDIA_CURRENCY");
                RptDataSet4.Tables[1].Columns.Add("DUBLIN_PRICEVALUE"); RptDataSet4.Tables[1].Columns.Add("DUBLIN_CURRENCY");
                if (RptDataSet4.Tables[1].Rows[0]["MPROJNO"] != DBNull.Value && RptDataSet4.Tables[1].Rows[0]["MPROJNO"].ToString() != "")//For which one has module - (eg) StarTechnology || Lionbridge || Techtrans
                {

                    wip_invoice sobj = new wip_invoice();
                    string pval = string.Empty;
                    try
                    {
                        for (int cnt = 0; cnt < RptDataSet4.Tables[1].Rows.Count; cnt++)
                        {
                            pval = "";
                            pval = sobj.getjournalcodeprice(RptDataSet4.Tables[1].Rows[cnt]["PRICECODE"].ToString(), "i", RptDataSet4.Tables[1].Rows[cnt]["MPTITLE"].ToString());
                            if (!string.IsNullOrEmpty(pval))
                            {
                                RptDataSet4.Tables[1].Rows[cnt]["INDIA_PRICEVALUE"] = pval.Split(',').GetValue(0).ToString();
                                RptDataSet4.Tables[1].Rows[cnt]["INDIA_CURRENCY"] = pval.Split(',').GetValue(1).ToString();
                            }
                            pval = "";
                            pval = sobj.getjournalcodeprice(RptDataSet4.Tables[1].Rows[cnt]["PRICECODE"].ToString(), "d", RptDataSet4.Tables[1].Rows[cnt]["MPTITLE"].ToString());
                            if (!string.IsNullOrEmpty(pval))
                            {
                                RptDataSet4.Tables[1].Rows[cnt]["DUBLIN_PRICEVALUE"] = pval.Split(',').GetValue(0).ToString();
                                RptDataSet4.Tables[1].Rows[cnt]["DUBLIN_CURRENCY"] = pval.Split(',').GetValue(1).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    { throw ex; }
                    finally
                    { sobj = null; }

                }
                SubRptDoc = new ReportDocument();
                //RptAdaptor.Fill(RptDataSet4);

                ////////This Is For BHO Customer/////
                string des = "";
                int Deslen;

                des = RptDataSet4.Tables[1].Rows[0]["PDESCRIPTION"].ToString();
                string PTitle = RptDataSet4.Tables[1].Rows[0]["PTITLE"].ToString().Trim();
                //int pa = Convert.ToInt32(RptDataSet4.Tables[1].Rows[0]["PCARTICLES"]);
                ///////This Is For T and F and Project Title Is XML HEADER
                int CPos = PTitle.ToUpper().IndexOf("XML HEADERS AND REFERENCE");
                if (CPos >= 0)
                {
                    //des = des.Replace(";", "\n");
                    //des = des.Replace("-", ",");
                    CustNo = CustNo + "XML";

                }

                FName = GetReportName(RptDataSet4.Tables[1].Rows[0]["PCODE"].ToString(), CustNo);
                RptDoc.FileName = FName;
                if (RptDataSet4.Tables[1].Rows[0]["PNOOFPAGES"].ToString() == "")
                    RptDataSet4.Tables[1].Rows[0]["PNOOFPAGES"] = 0;

                RptDoc.SetDataSource(RptDataSet4.Tables[1]);
                DataSet BHRptDataSet1 = new DataSet();

                //XLS File article details added for The African Field Epidemiology Network (10124)
                if (RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10124" || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10102" || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10106" || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10081" || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10121"
                    || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10130" || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10119"
                    || RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10168")
                {

                    //DataSet xlds = new DataSet();
                    Articledetails xlds = new Articledetails();
                    OleDbConnection olcon = null;
                    ReportDocument article_rpt = new ReportDocument();
                    try
                    {
                        if (System.IO.File.Exists(ConfigurationManager.AppSettings["Article_Details_Project"].ToString() + RptDataSet4.Tables[1].Rows[0]["PCODE"].ToString().Trim() + ".xls"))
                            olcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + ConfigurationManager.AppSettings["Article_Details_Project"].ToString() + RptDataSet4.Tables[1].Rows[0]["PCODE"].ToString().Trim() + ".xls; Extended Properties='Excel 8.0;HDR=No;';");
                        else
                            //For Null(dummy excelfile) value maintenance. \\dpserver2\db\Article_details\dondelete.xls 
                            olcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + ConfigurationManager.AppSettings["Article_Details_Project"].ToString() + "Dontdelete.xls; Extended Properties='Excel 8.0;HDR=No;';");
                        olcon.Open();
                        string sql = "select * from [Sheet1$] ";
                        OleDbDataAdapter oda = new OleDbDataAdapter(sql, olcon);
                        oda.Fill(xlds);
                        article_rpt = RptDoc.Subreports[0];
                        article_rpt.SetDataSource(xlds.Tables[1]);
                        //}
                        //else
                        //{
                        //    //xlds = (Articledetails)null;
                        //    Dontdelete.xls
                        //    article_rpt = RptDoc.Subreports[0];
                        //    article_rpt.SetDataSource(null);
                        //    //article_rpt.SetDatabaseLogon("sa", "masterkey", "192.9.200.148", "TRACKING.GDB");
                        //}

                    }
                    catch (Exception ex)
                    { throw ex; }
                    finally
                    {
                        if (olcon != null) olcon.Close();
                    }

                }
                if (RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "2556" && CPos >= 0)
                {
                    char[] separator = new char[] { ';' };
                    string[] PDes = des.Split(separator);
                    Deslen = PDes.GetLength(0);
                    //BHRptDataSet1 is DataSet
                    BHRptDataSet1.Tables.Add("BHOCust");
                    BHRptDataSet1.Tables["BHOCust"].Columns.Add("Des1");
                    BHRptDataSet1.Tables["BHOCust"].Columns.Add("Des2");

                    for (int j = 0; j < Deslen; j++)
                    {
                        char[] seperator2 = new char[] { '(' };
                        string[] Descnt = PDes[j].Split(seperator2);
                        //string Desc1 = "";
                        //string Desc2 = "";
                        if (Descnt.GetLength(0) == 2)
                        {
                            Descnt[0] = Descnt[0].Replace(".", "");
                            Descnt[0] = Descnt[0].Replace(")", "");
                            Descnt[0] = Descnt[0].Replace("-", "");
                            Descnt[1] = Descnt[1].Replace(")", "");
                            Descnt[1] = Descnt[1].Replace(".", "");
                            Descnt[1] = Descnt[1].Replace("-", "");
                            DataRow row = BHRptDataSet1.Tables["BHOCust"].NewRow();
                            row["Des1"] = (Descnt[0]).ToString();
                            row["Des2"] = (Descnt[1]).ToString();
                            BHRptDataSet1.Tables["BHOCust"].Rows.Add(row);

                        }
                    }

                    SubRptDoc = RptDoc.Subreports[0];
                    SubRptDoc.SetDataSource(BHRptDataSet1.Tables["BHOCust"]);

                }
                if (RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10090")
                {
                    char[] separator = new char[] { '\n' };
                    string[] PDes = des.Split(separator);
                    Deslen = PDes.GetLength(0);
                    BHRptDataSet1.Tables.Add("BHOCust");
                    BHRptDataSet1.Tables["BHOCust"].Columns.Add("Des1");
                    BHRptDataSet1.Tables["BHOCust"].Columns.Add("Des2");
                    foreach (string SplitDes in PDes)
                    {
                        char[] seperator2 = new char[] { ',' };
                        string[] Descnt = SplitDes.Split(seperator2);

                        if (Descnt.GetLength(0) == 2)
                        {
                            DataRow row = BHRptDataSet1.Tables["BHOCust"].NewRow();
                            row["Des1"] = (Descnt[0]).ToString();
                            row["Des2"] = (Descnt[1]).ToString();
                            BHRptDataSet1.Tables["BHOCust"].Rows.Add(row);

                        }
                    }
                    SubRptDoc = RptDoc.Subreports[0];
                    SubRptDoc.SetDataSource(BHRptDataSet1.Tables["BHOCust"]);

                }
            }

            else // JOURNALS
            {
                //RptAdaptor.Fill(RptDataSet);

                FName = GetReportName(RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString(), CustNo);
                RptDoc.FileName = FName;
                RptDoc.SetDataSource(RptDataSet.Tables[1]);

                if (RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10092")
                {
                    MedicalEduSub = new ReportDocument();
                    MedicalEduOnlineAP MedRptDataSet = new MedicalEduOnlineAP();
                    SqlDataAdapter MedAdapter;

                    string MedQry = "select arealnoofpages,f_rtrim(jourcode) || amanuscriptid as amanuscriptid from article_dp A,journal_dp J where A.journo=J.journo and adno=3 and ino=" + IssueNo;
                    try
                    {
                        MedAdapter = new SqlDataAdapter(MedQry, RptConnection);
                        MedAdapter.Fill(MedRptDataSet);
                        MedicalEduSub = RptDoc.Subreports[0];
                        MedicalEduSub.SetDataSource(MedRptDataSet.Tables[1]);
                    }
                    catch (Exception ex)
                    { throw ex; }
                    finally
                    {
                        /*if (MedicalEduSub != null)
                        {
                            MedicalEduSub.Close();
                            MedicalEduSub.Dispose();
                            GC.Collect();
                        }
                        MedicalEduSub = null;
                         * */
                        MedRptDataSet = null;
                        MedAdapter = null;
                    }


                }
                if (RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10081" || RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10105" || RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10099" || RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10117" || RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10132")
                //For Co-Action, 10105 SanLucas , 10099 For IJAL(International Journal of Ageing and Later Life || 10117 For Medical Student Research Journal || 10132 Consejo Superior de Investigaciones Cientificas )
                {
                    string aqry = "SELECT 0 as ano,journo,INO, cast(amanuscriptid as varchar(20)) as amanuscriptid, " +
                                  "0 as ADNO,CATNO,sum(arealnoofpages) as arealnoofpages ,AINVOICEPAGES,jourcode, " +
                                  "isarticle_based FROM ARTICLE_DP  " +
                                  "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO where adno=3 and  " +
                                  "ino =" + IssueNo + " group by amanuscriptid,journo,INO,CATNO,AINVOICEPAGES,jourcode, " +
                                  "isarticle_based union SELECT 0 as ano,journo,INO, " +
                                  "CAST('Covers' AS VARCHAR(20)) as amanuscriptid,0 as ADNO,CATNO," +
                                  "sum(arealnoofpages) as arealnoofpages,AINVOICEPAGES ,jourcode,isarticle_based " +
                                  "FROM ARTICLE_DP JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO " +
                                  "where ino=" + IssueNo + " and adno in(1,5) group by journo,INO,CATNO,AINVOICEPAGES,jourcode, " +
                                  "isarticle_based union SELECT 0 as ano,journo,INO,CAST('Prelims' AS VARCHAR(20)) as amanuscriptid, " +
                                  "0 as ADNO,CATNO,sum(arealnoofpages) as arealnoofpages ,AINVOICEPAGES ,jourcode, " +
                                  "isarticle_based  FROM ARTICLE_DP JOIN JOURNAL_DP ON " +
                                  "JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO where ino=" + IssueNo + " and " +
                                  "adno in(2) group by journo,INO,CATNO,AINVOICEPAGES,jourcode,isarticle_based " +
                                  "union SELECT 0 as ano,journo,INO,CAST('Postlims' AS VARCHAR(20)) as amanuscriptid, " +
                                  "0 as ADNO,CATNO,sum(arealnoofpages) as arealnoofpages ,AINVOICEPAGES ,jourcode, " +
                                  "isarticle_based  FROM ARTICLE_DP JOIN JOURNAL_DP ON " +
                                  "JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO where ino=" + IssueNo + " and " +
                                  "adno in(4) group by journo,INO,CATNO,AINVOICEPAGES,jourcode,isarticle_based ";



                    /*string aqry = "SELECT 0 as ano,journo,INO, cast(amanuscriptid as varchar(20)) as amanuscriptid, " +
                    "0 as ADNO,CATNO,sum(arealnoofpages) as arealnoofpages ,AINVOICEPAGES,jourcode,isarticle_based FROM ARTICLE_DP" +
                    "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO where adno=3 and ino =" + IssueNo +
                    "group by amanuscriptid,journo,INO,CATNO,AINVOICEPAGES,jourcode,isarticle_based" +
                    " union SELECT 0 as ano,journo,INO,CAST('Covers' AS VARCHAR(20)) as amanuscriptid,0 as ADNO," +
                    "CATNO,sum(arealnoofpages) as arealnoofpages ,AINVOICEPAGES ,jourcode,isarticle_based  FROM ARTICLE_DP" +
                    "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO where ino=" + IssueNo +" and adno in(1,5) " +
                    "group by journo,INO,CATNO,AINVOICEPAGES,jourcode,isarticle_based " +
                    "union SELECT 0 as ano,journo,INO,CAST('Prelim' AS VARCHAR(20)) as amanuscriptid,0 as ADNO, " +
                    "CATNO,sum(arealnoofpages) as arealnoofpages ,AINVOICEPAGES ,jourcode,isarticle_based " +
                    "FROM ARTICLE_DP JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO where ino=" + IssueNo +
                    "and adno in(2) group by journo,INO,CATNO,AINVOICEPAGES,jourcode,isarticle_based";*/


                    /* string aqry = "SELECT ANO,JOURNO,INO,AMANUSCRIPTID,ADNO, CATNO, AREALNOOFPAGES,AINVOICEPAGES,JOURCODE,ISARTICLE_BASED FROM ARTICLE_DP " +
                        "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO WHERE INO IN(" + IssueNo + ") AND ADNO in(3)";//3 for article */
                    ReportDocument articledoc = new ReportDocument();
                    ArticleandPages ads = new ArticleandPages();
                    SqlDataAdapter articleadapter;
                    try
                    {
                        articleadapter = new SqlDataAdapter(aqry, RptConnection);
                        articleadapter.Fill(ads);
                        articledoc = RptDoc.Subreports[0];
                        ads.Tables[1].Columns[3].ColumnName = "AMANUSCRIPTID";
                        ads.Tables[1].Columns[6].ColumnName = "arealnoofpages";
                        ads.Tables[1].AcceptChanges();
                        articledoc.SetDataSource(ads.Tables[1]);
                    }
                    catch (Exception ex)
                    { throw ex; }
                    finally
                    {
                        articledoc = null;
                        ads = null;
                        articleadapter = null;
                    }

                }



                //if (RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "2556" || RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10040")
                if (RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "2556")
                {
                    //For Combined Issue
                    if (RptType == "PDF")
                    {
                        ReportDocument combineddoc = new ReportDocument();
                        combineddoc = RptDoc.Subreports["CombinedIssue.rpt"];
                        datasourceIB ibobj = new datasourceIB();
                        CombinedIssue cds = new CombinedIssue();
                        cds.Merge(ibobj.getcombinedissue("select * from SP_GET_INV_COMBINED_ISSUE(" + RptDataSet.Tables[1].Rows[0]["INO1"].ToString() + ")", CommandType.StoredProcedure));
                        for (int rcnt = 0; cds.Tables[1].Rows.Count > rcnt; rcnt++)
                            cds.Tables[1].Rows[rcnt]["RATE"] = PAmount.ToString();
                        combineddoc.SetDataSource(cds.Tables[1]);

                    }
                    //For psychologypress
                    if (RptType == "XLS")
                    {
                        //comment on 10 May 2010 for Excel file will create both(india and dublin)
                        //if (InvoiceNo != "")
                        //{
                        ReportDocument tandfdoc = new ReportDocument();
                        ReportDocument pdftandfsub = null;
                        ReportDocument articlepagesub = null;
                        try
                        {
                            string tandfname = FName.Replace(".rpt", "all.rpt");
                            //string tandfname = FName.ToString();
                            tandfdoc.FileName = tandfname;
                            tandfdoc.SetDataSource(RptDataSet.Tables[1]);
                            pdftandfsub = new ReportDocument();
                            pdftandfsub = tandfdoc.Subreports["XlsTFReport.rpt"];
                            pdftandfsub.SetDataSource(RptDataSet.Tables[1]);
                            articlepagesub = tandfdoc.Subreports["ArticlesandPagesRpt.rpt"];
                            ArticleandPages ads = ExcelArticlesandPages("not excel", "");
                            articlepagesub.SetDataSource(ads.Tables[1]);

                            //For Combined Issue
                            ReportDocument combineddoc = new ReportDocument();
                            combineddoc = tandfdoc.Subreports["CombinedIssue.rpt"];
                            datasourceIB ibobj = new datasourceIB();
                            CombinedIssue cds = new CombinedIssue();
                            cds.Merge(ibobj.getcombinedissue("select * from SP_GET_INV_COMBINED_ISSUE(" + RptDataSet.Tables[1].Rows[0]["INO1"].ToString() + ")", CommandType.StoredProcedure));
                            for (int rcnt = 0; cds.Tables[1].Rows.Count > rcnt; rcnt++)
                                cds.Tables[1].Rows[rcnt]["RATE"] = PAmount.ToString();
                            combineddoc.SetDataSource(cds.Tables[1]);


                            //ReportDocument copyeditsub = new ReportDocument();
                            //copyeditsub = tandfdoc.Subreports["ArticlesandPagesRpt1.rpt"];
                            //string qry = "SELECT ANO,JOURNO,INO,AMANUSCRIPTID,ADNO, CATNO, AREALNOOFPAGES,AINVOICEPAGES,JOURCODE,ISARTICLE_BASED,IINVOICENO,IINVOICEDATE FROM ARTICLE_DP " +
                            //                "JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO JOIN ISSUE_DP ON ISSUE_DP.JOURNO=JOURNAL_DP.JOURNO AND ISSUE_DP.INO=" + IssueNo + " WHERE ARTICLE_DP.INO IN(" + IssueNo + ") AND ADNO NOT IN (12,13,1,5) and aextra_copy_edit='Y'";//12 and 13 blank pages,1 and 5 covers
                            //ads = ExcelArticlesandPages("not excel",qry);

                            //copyeditsub.SetDataSource(ads.Tables[1]);


                            tandfdoc.SetParameterValue("TSAmount", PAmount);
                            tandfdoc.SetParameterValue("TSAmount", PAmount, "XlsTFReport.rpt");
                            tandfdoc.SetParameterValue("TSAmount", PAmount, "ArticlesandPagesRpt.rpt");

                            CAmount = 0;
                            tandfdoc.SetParameterValue("CEAmount", CAmount);
                            tandfdoc.SetParameterValue("CEAmount", CAmount, "XlsTFReport.rpt");
                            if (RptDataSet.Tables[1].Rows[0]["ISCOPYEDITED"].ToString() != "" && RptDataSet.Tables[1].Rows[0]["ISCOPYEDITED"].ToString() != "0")
                            {
                                CAmount = GetPriceValue('C');
                                tandfdoc.SetParameterValue("CEAmount", CAmount);
                                tandfdoc.SetParameterValue("CEAmount", CAmount, "XlsTFReport.rpt");
                                //tandfdoc.SetParameterValue("TSAmount", CAmount, "ArticlesandPagesRpt1.rpt");
                            }
                            SAMAmount = 0;
                            tandfdoc.SetParameterValue("SAMAmount", SAMAmount);
                            tandfdoc.SetParameterValue("SAMAmount", SAMAmount, "XlsTFReport.rpt");
                            if ((RptDataSet.Tables[1].Rows[0]["ISSAMS"].ToString() != "0") && (RptDataSet.Tables[1].Rows[0]["ISSAMS"].ToString() != ""))
                            {
                                SAMAmount = GetPriceValue('S');
                                tandfdoc.SetParameterValue("SAMAmount", SAMAmount);
                                tandfdoc.SetParameterValue("SAMAmount", SAMAmount, "XlsTFReport.rpt");
                            }
                            //This is For Report Test(Common Report)
                            tandfdoc.SetParameterValue("Country", location);
                            tandfdoc.SetParameterValue("Country", location, "XlsTFReport.rpt");
                            tandfdoc.SetParameterValue("CurrencyStr", Currency);
                            tandfdoc.SetParameterValue("CurrencyStr", Currency, "XlsTFReport.rpt");
                            sFilePath = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
                            if (location == "d")
                                sFilePath = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();

                            string fname = sFilePath + RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().Trim() + " - ";
                            fname += (location.ToUpper() == "I" && string.IsNullOrEmpty(InvoiceNo)) ? "India" : ((location.ToUpper() == "D" && string.IsNullOrEmpty(InvoiceNo))) ? "Dublin" : "";
                            filename = string.IsNullOrEmpty(InvoiceNo) ? fname + ".pdf" : fname + InvoiceNo.ToString() + ".pdf";
                            filename = filename.Trim().Replace("/", "_");
                            tandfdoc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                        }
                        catch (Exception ex)
                        { throw ex; }
                        finally
                        { tandfdoc = null; pdftandfsub = null; }
                        //}//For check InvoiceNo != ""
                    }//For psychologypress
                    else //For PDF
                    {
                        ReportDocument articlepagesub = null;
                        articlepagesub = RptDoc.Subreports["ArticlesandPagesRpt.rpt"];
                        ArticleandPages ads = ExcelArticlesandPages("not excel", "");
                        articlepagesub.SetDataSource(ads.Tables[1]);
                        RptDoc.SetParameterValue("TSAmount", PAmount, "ArticlesandPagesRpt.rpt");
                    }
                }


            }

            //PAmount = GetPriceValue('T');

            RptDoc.SetParameterValue("TSAmount", PAmount);
            //RptDoc.SetParameterValue("TotalVal", 1);

            //This is for VWM Customer ---- books
            // if (RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10087")
            //RptDoc.SetParameterValue("DPAmount", 100);
            //XlsDoc.SetParameterValue("TSAmount", PAmount);


            if (CustCategory == 1)///////////For Journal
            {
                CAmount = 0;
                //Need Not to Check this bcz all reports have 'CEAMOUNT' parameter
                RptDoc.SetParameterValue("CEAmount", CAmount);
                if (RptDataSet.Tables[1].Rows[0]["ISCOPYEDITED"].ToString() != "" && RptDataSet.Tables[1].Rows[0]["ISCOPYEDITED"].ToString() != "0")
                {
                    CAmount = GetPriceValue('C');
                    RptDoc.SetParameterValue("CEAmount", CAmount);
                }
                SAMAmount = 0;
                RptDoc.SetParameterValue("SAMAmount", SAMAmount);
                if ((RptDataSet.Tables[1].Rows[0]["ISSAMS"].ToString() != "0") && (RptDataSet.Tables[1].Rows[0]["ISSAMS"].ToString() != ""))
                {
                    SAMAmount = GetPriceValue('S');
                    RptDoc.SetParameterValue("SAMAmount", SAMAmount);
                }
                //This is For Report Test(Common Report)
                RptDoc.SetParameterValue("Country", location);
            }
            if (CustCategory == 2)////////////For Books
            {
                double baamt = 0;

                for (int i = 1; i < 6; i++)
                {
                    if (RptDataSet2.Tables[1].Rows[0]["BACNO" + i + "1"].ToString() != "")
                        baamt = PriceCode(Convert.ToInt16(RptDataSet2.Tables[1].Rows[0]["BACNO" + i + "1"].ToString()), 'T');
                    RptDoc.SetParameterValue("BACOST" + i + "Amount", baamt);
                    baamt = 0;
                }
                //This is For Report Test(Common Report)
                RptDoc.SetParameterValue("Country", location);

            }
            if (CustCategory == 3)////////////For Projects
            {
                double baamt = 0;

                for (int i = 1; i < 6; i++)
                {
                    if (RptDataSet4.Tables[1].Rows[0]["PACNO" + i].ToString() != "")
                        baamt = PriceCode(Convert.ToInt32(RptDataSet4.Tables[1].Rows[0]["PACNO" + i].ToString()), 'T');
                    RptDoc.SetParameterValue("PACNO" + i + "Amount", baamt);
                    baamt = 0;
                }
                RptDoc.SetParameterValue("Country", location);

                //if (location == "d" && RptType == "PDF")
                if (RptType == "PDF")
                    RptDoc.SetParameterValue("CurrencyStr", Currency);

            }
            //Comment For Report test 15/07/09
            //if (location == "d" && RptType == "PDF")
            RptDoc.SetParameterValue("CurrencyStr", Currency);



            if (CustCategory == 2)
            {
                InvoiceNo = RptDataSet2.Tables[1].Rows[0]["BINVOICENO1"].ToString();
                InvoiceDate = RptDataSet2.Tables[1].Rows[0]["BINVOICEDATE1"].ToString();
                InvItem = RptDataSet2.Tables[1].Rows[0]["BNUMBER1"].ToString();
            }
            else if (CustCategory == 3)
            {
                InvoiceNo = RptDataSet4.Tables[1].Rows[0]["INVNO"].ToString();
                InvoiceDate = RptDataSet4.Tables[1].Rows[0]["PINVOICEDDATE"].ToString();
                InvItem = RptDataSet4.Tables[1].Rows[0]["PCODE"].ToString().Trim();
            }
            else
            {
                InvoiceNo = RptDataSet.Tables[1].Rows[0]["INVNO"].ToString();
                InvoiceDate = RptDataSet.Tables[1].Rows[0]["INVDATE"].ToString();
                InvItem = RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().Trim();
            }
            if (InvoiceDate != null && InvoiceDate != "")
                InvoiceDate = Convert.ToDateTime(InvoiceDate).ToShortDateString();
            sInvoiceNo = InvoiceNo;

            sFilePath = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();
            if (location == "d")
                sFilePath = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();

            //Invoice value update only in finalinvoice screen sivaraj request 12 Nov 2010
            //So invoiceno checked 
            if (CustCategory == 3)//For Projects - Global Language customer
            {
                InvoiceNo = ((DataRow)RptDataSet4.Tables[1].Select("PARENT_PROJECTNO is null").GetValue(0))["INVNO"].ToString();
                InvItem = ((DataRow)RptDataSet4.Tables[1].Select("PARENT_PROJECTNO is null").GetValue(0))["PCODE"].ToString().Trim();
            }
            if (RptType == "PDF" && InvoiceNo != "" && InvoiceNo != null)//Only one time update in invoice_value xml file when the pdf created 
            {
                string exception_msg = InvoiceValueFromCR(RptDoc, InvItem, InvValupdatestr, InvoiceNo, InvoiceDate, Currency, InvItem); // update invoice values  
                //string exception_msg = "";
                /* zero based inoivces to be removed from email list of T&F */
                double dResult = 0;
                if (Double.TryParse(exception_msg, out dResult) == true && (CustNo == "2556" || CustNo == "10040") && location == "d") //&& (exception_msg == "0.00" || exception_msg == "0.0" || exception_msg == "0"))
                {
                    if (exception_msg == "0.00" || exception_msg == "0.0" || exception_msg == "0")//(CustNo == "2556" && location == "d")
                    {
                        if (!StringExecute("UPDATE ISSUE_DP SET IEMAIL_FLAG = 'Y', IEMAIL_FLAG_IND = 'Y', INV_EMAIL_SENT1 = 'Y', INV_EMAIL_SENT2 = 'Y', DUBLIN_INVOICE_VALUE='" + exception_msg + "' WHERE IINVOICENO =" + InvoiceNo.ToString(), CommandType.Text))
                            throw new Exception("UNABLE TO REMOVE ZERO VALUE INVOICE FROM LIST, PLEASE TRY AGAIN");
                    }
                }
                else if (Double.TryParse(exception_msg, out dResult) == false && exception_msg != "")
                    throw new ArgumentException(exception_msg);
            }


            if (CustCategory == 2) // For Books
            {
                //Srikanth request all independent college invoice have filename as invoiceno
                if (RptDataSet2.Tables[1].Rows[0]["CUSTNO1"].ToString() == "10085" && RptDataSet2.Tables[1].Rows[0]["BINVOICENO1"] != DBNull.Value &&
                    //Convert.ToInt32(RptDataSet2.Tables[1].Rows[0]["BINVOICENO1"].ToString()) != 27163 && Convert.ToInt32(RptDataSet2.Tables[1].Rows[0]["BINVOICENO1"].ToString()) != 27164 && 
                    (Convert.ToInt32(RptDataSet2.Tables[1].Rows[0]["BINVOICENO1"].ToString()) >= 27122))//For Independent Colleges
                    filename = sFilePath + RptDataSet2.Tables[1].Rows[0]["BINVOICENO1"].ToString().Trim() + ".PDF";
                else
                    filename = sFilePath + RptDataSet2.Tables[1].Rows[0]["BNUMBER1"].ToString().Trim() + ".pdf";
            }
            else if (CustCategory == 3) // For Projects
            {

                if (RptDataSet4.Tables[1].Rows[0]["INVNO"] != DBNull.Value && Convert.ToInt32(RptDataSet4.Tables[1].Rows[0]["INVNO"]) >= 27122 && RptDataSet4.Tables[1].Rows[0]["CUSTNO"].ToString() == "10085")//For Independent college
                    filename = sFilePath + RptDataSet4.Tables[1].Rows[0]["INVNO"].ToString().Trim() + ".pdf";
                else
                {
                    DataRow dr;
                    dr = (DataRow)RptDataSet4.Tables[1].Select("PARENT_PROJECTNO is null").GetValue(0);
                    //filename = sFilePath + RptDataSet4.Tables[1].Rows[0]["PCODE"].ToString().Trim() + ".pdf";
                    filename = sFilePath + dr["pcode"].ToString().Trim() + ".pdf";
                }
            }
            else // journals
                filename = sFilePath + RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().Trim() + ".pdf";
            filename = filename.Trim().Replace("/", "_");


            if (RptType == "PDF")
            {
                RptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                //For 3page pdf display only in dublin
                if (CustCategory == 1 && RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "2556" && location.ToUpper() == "D")
                {
                    filename = sFilePath + RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().Trim() + " - ";
                    filename += (location.ToUpper() == "D" && string.IsNullOrEmpty(InvoiceNo)) ? "Dublin" : "";
                    filename = string.IsNullOrEmpty(InvoiceNo) ? filename + ".pdf" : filename + InvoiceNo.ToString() + ".pdf";
                    filename = filename.Trim().Replace("/", "_");
                }
            }
            if (RptType == "XLS")
            {
                //comment on 10 May 2010 For excel file create in both(india and dublin)
                //if (InvoiceNo != "")
                //{
                filename = (string.IsNullOrEmpty(InvoiceNo)) ? sFilePath + RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().Trim().Replace("/", "_") + ".xls" : sFilePath + InvoiceNo.ToString() + ".xls";

                //ExportOptions oExOption = new ExportOptions();
                //oExOption.ExportFormatOptions = ExportOptions.CreateExcelFormatOptions();
                //RptDoc.Export(oExOption.ExportFormatOptions);
                ExportOptions oEOption = new ExportOptions();
                oEOption.ExportDestinationType = ExportDestinationType.DiskFile;
                oEOption.ExportFormatType = ExportFormatType.Excel;
                DiskFileDestinationOptions DFOption = new DiskFileDestinationOptions();
                DFOption.DiskFileName = filename;
                oEOption.ExportDestinationOptions = DFOption;
                RptDoc.Export(oEOption);
                //RptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, , true, filename);
                //RptDoc.ExportToDisk(ExportFormatType.Excel, filename);

                ExcelSetting(filename, null);
                //For Cretae Breakdown sheet for TandF
                //if (InvoiceDate == "" || Convert.ToDateTime(InvoiceDate).Year >= 2010 || System.Convert.IsDBNull(InvoiceDate))
                //    ExcelArticlesandPages("Excel","");

                //ProcessStartInfo processStartInfo = new ProcessStartInfo();
                //processStartInfo.FileName = @"e:\dbInvoicePDF\Dublin\ExcelObject_Console.exe";
                //processStartInfo.Arguments = filename;
                //Process.Start(processStartInfo);
                //Process.Start(@"\\dpserver2\dbInvoicePDF\Dublin\ExcelObject_Console.exe", filename);
            }
            if (CustCategory == 1)///////////For Journal
            {
                if (RptDataSet.Tables[1].Rows[0]["custno"].ToString() == "10040")//For Psychologypress with details
                {
                    //PsyDoc.FileName = GetReport(RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString(), CustNo);
                    //PsyDoc.SetDataSource(RptDataSet.Tables[0]);

                    psychologyinvoice pobj = new psychologyinvoice();
                    FName = GetReportName(RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString(), CustNo);
                    RptDoc = pobj.CreateReport(RptDataSet, location, Currency, PAmount, CAmount, SAMAmount);

                }
            }
            return filename;
        }
        catch (ArgumentException e)
        { throw e; }
        catch (Exception exp1)
        {
            throw exp1;
        }
        finally
        {
            DBClose(RptConnection);
            RptConnection.Close();
            if (RptConnection != null)
                RptConnection.Dispose();
            if (RptAdaptor != null)
                RptAdaptor.Dispose();
            if (RptDataSet != null)
                RptDataSet.Dispose();
            if (RptDataSet2 != null)
                RptDataSet2.Dispose();
            if (RptDataSet3 != null)
                RptDataSet3.Dispose();
            if (RptDataSet4 != null)
                RptDataSet4.Dispose();

            /*if (RptDoc != null)
            {
                RptDoc.Close();
                RptDoc.Dispose();
                GC.Collect();
            }
            if (MedicalEduSub != null)
            {
                MedicalEduSub.Close();
                MedicalEduSub.Dispose();
                GC.Collect();
            }
            if (SubRptDoc != null)
            {
                SubRptDoc.Close();
                SubRptDoc.Dispose();
                GC.Collect();
            }

            RptDoc = null;
            SubRptDoc = null;
            MedicalEduSub = null;
             * */
            RptConnection = null;

        }
    }

    public ArticleandPages ExcelArticlesandPages(string export, string cpyeditqry)
    {
        string aqry = "";
        if (string.IsNullOrEmpty(cpyeditqry))
        {
            //aqry = "SELECT ANO,JOURNO,INO,AMANUSCRIPTID,ADNO, CATNO, AREALNOOFPAGES,AINVOICEPAGES,JOURCODE,ISARTICLE_BASED,IINVOICENO,IINVOICEDATE,ARTICLE_DP.invno FROM ARTICLE_DP " +
            //"JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO JOIN ISSUE_DP ON ISSUE_DP.JOURNO=JOURNAL_DP.JOURNO AND ISSUE_DP.INO=" + IssueNo + " WHERE ARTICLE_DP.INO IN(" + IssueNo + ") AND ADNO NOT IN (10031,10032,12,13,1,4,5,2)";//12 and 13 blank pages,1 and 5 covers
            aqry = "SELECT ANO,JOURNO,INO,AMANUSCRIPTID,ADNO, CATNO, AREALNOOFPAGES,AINVOICEPAGES,JOURCODE,ISARTICLE_BASED,IINVOICENO,IINVOICEDATE,INVNO FROM SP_GET_ARTICLESANDPAGES(" + IssueNo + ")";
        }
        else
        {
            aqry = cpyeditqry.ToString();
        }
        SqlConnection apcon = new SqlConnection();
        ArticleandPages apds = new ArticleandPages();
        try
        {
            apcon = DBOpen();

            //ArticleandPages apds = new ArticleandPages();
            SqlDataAdapter ada = new SqlDataAdapter(aqry, apcon);
            ada.Fill(apds);
            DBClose(apcon);
            ReportDocument apdoc = new ReportDocument();
            apdoc.FileName = HttpContext.Current.Application["spath"].ToString() + @"\InvoiceReports\ArticlesandPagesRpt.rpt";
            apdoc.SetDataSource(apds.Tables[1]);
            apdoc.SetParameterValue("TSAmount", PAmount);
            if (export == "Excel")
            {
                string pname = (string.IsNullOrEmpty(InvoiceNo)) ? RptDataSet.Tables[1].Rows[0]["JOURCODE"].ToString().Trim() + RptDataSet.Tables[1].Rows[0]["IISSUENO"].ToString().Trim() : InvoiceNo;
                apdoc.ExportToDisk(ExportFormatType.Excel, sFilePath + pname.Replace("/", "_") + "Breakdown.xls");
                ExcelSetting(sFilePath + pname.Replace("/", "_") + "Breakdown.xls", "ArticlesandPagesExl");
            }
            return apds;
        }
        catch (Exception ex)
        { throw ex; }
        finally { apcon = null; apds = null; }
    }
    private double GetPriceValue(char Type)
    {
        if (Type == 'T')        //////This is for TypeSetting
        {
            if (CustCategory == 2)/////// 2 For Book
                return PriceCode(Convert.ToInt32(RptDataSet2.Tables[1].Rows[0]["BCNO_20091"].ToString()), Type);
            else if (CustCategory == 3)//// 3 For Projects
                return PriceCode(Convert.ToInt32(RptDataSet4.Tables[1].Rows[0]["PCNO_2009"].ToString()), Type);
            else  ////////////// For Journal
            {
                if (RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "470" || RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "10020") //For TandF cutomer and Psychology Press Ltd
                {
                    if (!(System.Convert.IsDBNull(RptDataSet.Tables[1].Rows[0]["INVNO"])) && (Convert.ToInt64(RptDataSet.Tables[1].Rows[0]["INVNO"].ToString()) == 26920 || Convert.ToInt64(RptDataSet.Tables[1].Rows[0]["INVNO"].ToString()) == 26945))// 26920 For RBER 29/2 issue only, others have jcno_2010 field value(180) || 26945 For RUSI 155/2 
                        return PriceCode(165, Type);
                    else if (System.Convert.IsDBNull(RptDataSet.Tables[1].Rows[0]["INVNO"]) || Convert.ToInt64(RptDataSet.Tables[1].Rows[0]["INVNO"].ToString()) >= 26484)
                        return PriceCode(Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["JCNO2010"].ToString()), Type);
                }
                return PriceCode(Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["JCNO2009"].ToString()), Type);
            }
        }
        else if (Type == 'C')   //////This is for CopyEditing
        {
            //This is for Co-Action(10081) Copy Editing and Medical Education Online(10103) for medline submission and 10118 for IJAL 10131 For San Lucas Medical Limited
            if (RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "10081" || RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "10103" || RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "10118" || RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "10131")
                return PriceCode(Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["SAMCODE"]), Type);
            else if (RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "470" || RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "10020" || RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "472")
            //For Taylor and Francis || Psychology Press Ltd || Taylor and Francis Scandinavia
            {
                if (RptDataSet.Tables[1].Rows[0]["PAGEFORMAT"].ToString().ToUpper() == "SMALL")
                    // if ((RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "470") && location.ToUpper() == "D" && (System.Convert.IsDBNull(RptDataSet.Tables[1].Rows[0]["INVNO"]) || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) == 25709 || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) == 25728 || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) > 25840))//TandF NewFormat For Small (only TandF)
                    //     return PriceCode(289);
                    // else
                    return PriceCode(140, Type);
                else if (RptDataSet.Tables[1].Rows[0]["PAGEFORMAT"].ToString().ToUpper() == "LARGE")
                    //if ((RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "470") && location.ToUpper() == "D" && (System.Convert.IsDBNull(RptDataSet.Tables[1].Rows[0]["INVNO"]) || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) == 25709 || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) == 25728 || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) > 25840))//TandF NewFormat For Large (only TandF)
                    //    return PriceCode(290);
                    //else
                    return PriceCode(139, Type);
                else
                    return 0;
            }
            else
                return PriceCode(Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["SAMCODE"]), Type);
        }
        else if (Type == 'S')     ////// This is For SAMCODE
        {
            //if (RptDataSet.Tables[1].Rows[0]["SAMCODE"].ToString() == "" || (RptDataSet.Tables[1].Rows[0]["FINSITENO"].ToString() == "470") && location.ToUpper() == "D" && (System.Convert.IsDBNull(RptDataSet.Tables[1].Rows[0]["INVNO"]) || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) == 25709 || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) == 25728 || Convert.ToInt32(RptDataSet.Tables[1].Rows[0]["INVNO"]) > 25840))//470 for TandF
            if (RptDataSet.Tables[1].Rows[0]["SAMCODE"].ToString() == "")
                //return PriceCode(0);
                return 0;
            else
                return PriceCode(Convert.ToInt16(RptDataSet.Tables[1].Rows[0]["SAMCODE"].ToString()), 'T');
        }
        else
            return 0;
    }

    private double PriceCode(int CodeNo, char type)
    {
        XmlDocument odom = new XmlDocument();
        XmlNode oNode = null;
        string XMLPath = string.Empty;
        try
        {
            if (CodeNo == 0)
                //throw new ArgumentException("Invalid PriceCode");
                return 0.00;
            if (location == "i")
            {
                //XMLPath = MPath + @"\InvoiceTemplates\India\journal_prices_2008.xml";
                XMLPath = ConfigurationManager.ConnectionStrings["indiaPCXML"].ToString();
                odom.Load(XMLPath);
                oNode = odom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@JCPNO = '" + CodeNo + "']");
                if (oNode == null)
                    throw new ArgumentException(CodeNo + " This JCPNO is not available in " + Path.GetFileName(XMLPath));
                if (type == 'T')//This is for only set typeseting for journal 
                    Currency = Convert.ToString(oNode.Attributes.GetNamedItem("CURRENCY").Value);
                return Convert.ToDouble(oNode.Attributes.GetNamedItem("INDIAPRICE").Value);
            }
            else if (location == "d")
            {
                //XMLPath = MPath + @"\InvoiceTemplates\Dublin\journal_prices_2008.xml";
                XMLPath = ConfigurationManager.ConnectionStrings["dublinPCXML"].ToString();
                odom.Load(XMLPath);
                oNode = odom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@JCPNO = '" + CodeNo + "']");
                if (oNode == null)
                    throw new ArgumentException(CodeNo + " This JCPNO is not available in " + Path.GetFileName(XMLPath));
                if (type == 'T')
                    Currency = Convert.ToString(oNode.Attributes.GetNamedItem("CURRENCY").Value);
                return Convert.ToDouble(oNode.Attributes.GetNamedItem("JCPPRICE").Value);
            }
            else
                return 0.00;
        }
        catch (ArgumentException e)
        {
            throw e;
        }
        catch (Exception exp)
        {
            throw exp;
        }

    }
    private string GetReportName(string jourcode, string CustNo)
    {
        XmlDocument Dom = new XmlDocument();
        XmlNode RNode = null;
        XmlNode oAttr = null;
        string RptXMLPath = string.Empty;
        string sCryFileName = "";
        string RptPath = string.Empty;
        try
        {
            if (CustCategory == 2)//////////For Books
                RptXMLPath = MPath + @"\InvoiceTemplates\BooksInvoiceTemplate.xml";
            else if (CustCategory == 3)///////For Projects
                RptXMLPath = MPath + @"\InvoiceTemplates\ProjectTemplate.xml";
            else
                RptXMLPath = MPath + @"\InvoiceTemplates\InvoiceReports.xml";
            Dom.Load(RptXMLPath);
            RNode = Dom.SelectSingleNode("//customer[@custno = '" + CustNo + "']");
            if (RNode == null)
                //throw new ArgumentException("The Customer No is" + CustNo + ". This Customer is not in XML File(" + Path.GetFileName(RptXMLPath) + ")");
                throw new ArgumentException("This is New Customer,The Customer No is" + CustNo + ". This Customer is not in Report List");
            oAttr = RNode.Attributes.GetNamedItem("filename");
            if (oAttr != null)
                sCryFileName = oAttr.Value;
            /*
            //if (CustCategory == 1)///////////For Journals
            //    RNode = RNode.SelectSingleNode("//jourcode[@jourcode ='" + jourcode.ToUpper().Trim() + "']");
            //else if (CustCategory == 2)//////////For Books
            //  RNode = RNode.SelectSingleNode("//bookcode[@bookcode ='" + jourcode.ToUpper() + "']");
            //else if (CustCategory == 3)////////For Projects
            //RNode = RNode.SelectSingleNode("//projectcode[@projectcode ='" + jourcode.ToUpper() + "']");

            if (RNode != null)
            {
                oAttr = RNode.Attributes.GetNamedItem("filename");
                if (oAttr != null)
                    sCryFileName = oAttr.Value;
            }
            */
            //RNode = Dom.DocumentElement.SelectSingleNode("customer").SelectSingleNode("jourcode[@jourcode='" + jourcode + "']");

            //RptPath = @"E:\Dot Net Source\datapagemis-copy\InvoiceReports\";

            if (CustCategory == 2)//////For Books
                RptPath = MPath + @"\BooksReports\";
            else if (CustCategory == 3)////For Projects
                RptPath = MPath + @"\ProjectReports\";
            else
                RptPath = MPath + @"\InvoiceReports\";

            //XlsRptName = RptPath + @"India\Xls" + sCryFileName;
            if (location == "i" && RptType == "PDF")
                //return RptPath + @"India\Ind" + sCryFileName;
                //This is for Report Test (Common)
                return RptPath + @"\" + sCryFileName;
            else if (location == "d" && RptType == "PDF")
                //return RptPath + @"Dublin\Dub" + sCryFileName;
                //This is for Report Test (Common)
                return RptPath + @"\" + sCryFileName;
            //else if (location == "i" && RptType == "XLS")
            //return RptPath + @"India\Xls" + sCryFileName;
            //    return RptPath + @"\Xls" + sCryFileName;
            //else if (location == "d" && RptType == "XLS")
            else if (RptType == "XLS")
                return RptPath + @"\Xls" + sCryFileName;
            else
                return "";

        }
        catch (ArgumentException ex)
        {
            throw ex;
        }

        catch (Exception e)
        {
            throw e;
        }
    }
    public bool UpdateFinalInvoicedBooks(int bno)
    {
        SqlCommand ucmd = new SqlCommand();
        try
        {
            RptConnection = DBInvoice();
            ucmd.CommandType = CommandType.Text;
            //ucmd.CommandText = "UPDATE BOOK_DP SET BINVOICED = 'Y',BEMAIL_FLAG='Y',BEMAIL_FLAG_IND='Y' WHERE BNO = " + bno;
            ucmd.CommandText = "UPDATE BOOK_DP SET BEMAIL_FLAG='Y',BEMAIL_FLAG_IND='Y' WHERE BNO = " + bno;
            ucmd.Connection = RptConnection;
            ucmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;

        }
        finally
        {
            DBClose(RptConnection);
            ucmd = null;
        }
    }

    public bool UpdateFinalInvoicedProjects(string projectno)
    {
        SqlCommand ucmd = new SqlCommand();
        try
        {
            RptConnection = DBInvoice();
            ucmd.CommandType = CommandType.Text;
            //ucmd.CommandText = "UPDATE PROJECTS_DP SET PINVOICED = 'Y',PEMAIL_FLAG='Y',PEMAIL_FLAG_IND='Y' WHERE PROJECTNO = " + projectno;
            ucmd.CommandText = "UPDATE PROJECTS_DP SET PEMAIL_FLAG='Y',PEMAIL_FLAG_IND='Y' WHERE PROJECTNO in(" + projectno + ")";
            ucmd.Connection = RptConnection;
            ucmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;

        }
        finally
        {
            DBClose(RptConnection);
            ucmd = null;
        }
    }

    public bool StringExecute(string Qry, CommandType CType)
    {
        SqlCommand oCmd = new SqlCommand();
        SqlTransaction OTran = null;
        try
        {
            RptConnection = DBInvoice();
            OTran = RptConnection.BeginTransaction();
            oCmd.Connection = RptConnection;
            oCmd.Transaction = OTran;
            oCmd.CommandType = CType;
            oCmd.CommandText = Qry;
            oCmd.ExecuteNonQuery();
            OTran.Commit();
            return true;
        }
        catch (Exception oex)
        {
            OTran.Rollback();
            return false;
        }
        finally
        {
            DBClose(RptConnection);
            if (OTran != null)
                OTran.Dispose();
            oCmd = null;
            OTran = null;
        }
    }

    public string InvoiceValueFromCR(ReportDocument oRpt, string sFileName, string Invvalupdateorinsert, string iinvno, string iinvdate, string iinvcurrency, string iinvitem)
    {
        double dValue = 0;

        try
        {
            oRpt.ExportToDisk(ExportFormatType.HTML32, sFilePath + sFileName.Replace("/", "_") + ".htm");

            FileStream fileStream = new FileStream(sFilePath + sFileName.Replace("/", "_") + ".htm", System.IO.FileMode.Open);

            StreamReader oReader = new StreamReader(fileStream);

            string sContent = oReader.ReadToEnd();

            oReader.Close();

            fileStream.Close();

            fileStream = null;

            oReader.Dispose();

            oReader = null;

            sContent = sContent.Substring(sContent.IndexOf("InvTotalSubbu\">") + 15);

            sContent = sContent.Substring(0, sContent.IndexOf("<"));

            sContent = sContent.Replace("&nbsp;", "").Trim();
            sContent = sContent.Replace(">", "").Trim();
            sContent = sContent.Replace("<", "").Trim();
            while (!double.TryParse(sContent, out dValue))  //some time sContent come with currency Sysmbol, so  
            {
                //if(sContent!="")
                sContent = sContent.Substring(1);
            }

            oRpt = null;
            try
            {
                File.Delete(sFilePath + sFileName.Replace("/", "_") + ".htm");
            }
            catch (Exception oex) { }

            //dValue = Convert.ToDouble(sContent);

            //update invoice values xml file //
            biz_IB ReportXml = new biz_IB();
            if (System.Web.HttpContext.Current.Session["employeeid"].ToString() == "1280" && location == "i")
                Xmllocation = ConfigurationManager.ConnectionStrings["indTestINVXML"].ToString();
            else if (System.Web.HttpContext.Current.Session["employeeid"].ToString() != "1280" && location == "i")
                Xmllocation = ConfigurationManager.ConnectionStrings["indiaINVXML"].ToString();
            else if (System.Web.HttpContext.Current.Session["employeeid"].ToString() == "1280" && location == "d")
                Xmllocation = ConfigurationManager.ConnectionStrings["dubTestINVXML"].ToString();
            else
                Xmllocation = ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString();

            //All india invoice have Euro and INR currency
            if (location == "i" && Currency.ToUpper().ToString() != "INR")
                Currency = "Euro";
            string excep_msg = "";
            if (Invvalupdateorinsert != "" && Invvalupdateorinsert == "withpreview")
            {
                if (location == "i")
                    Xmllocation = ConfigurationManager.ConnectionStrings["indiaINVXML"].ToString();
                else
                    Xmllocation = ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString();
                //  excep_msg = ReportXml.UpdateInvoicevalueonly(Xmllocation, InvoiceNo, InvoiceDate, dValue.ToString(), Currency, "0", ReportXml.GetCurrencyConversion(Currency), InvItem);
            }
            /*
        else
            excep_msg = ReportXml.UpdateInvoiceXML(Xmllocation, InvoiceNo, InvoiceDate, dValue.ToString(), Currency, "0", ReportXml.GetCurrencyConversion(Currency), InvItem);
         * */
            //Only update or insert not delete in invoice_value.xml file
            excep_msg = ReportXml.UpdateInvoicevalueonly(Xmllocation, iinvno, iinvdate, dValue.ToString(), iinvcurrency, "0", ReportXml.GetCurrencyConversion(iinvcurrency), iinvitem);
            if (excep_msg != "")
                throw new ArgumentException(excep_msg);
            ReportXml = null;
            //return dValue;
            return dValue.ToString();
        }

        catch (ArgumentException ae)
        { return ae.Message.ToString(); }
        catch (Exception oex)
        {
            return "Unable to update Invoice value, Please relogin and generate";
            //return oex.Message.ToString();
        }
        finally { oRpt = null; }

    }
    public void ExcelSetting(string sFileName, string gridstr)
    {
        //This is for only in server bcz in DPIUser68 remove Excel application
        if (ConfigurationManager.ConnectionStrings["urladd"].ToString() == "server")
        {
            Excel.Application excelApp = new Excel.ApplicationClass();
            try
            {
                excelApp.Visible = true;
                Excel.Workbook newWorkbook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(sFileName, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                foreach (Worksheet oWS in excelApp.ActiveWorkbook.Worksheets)
                {
                    //Console.WriteLine(sFileName);
                    //Console.WriteLine("Applying Page Size as A4");
                    oWS.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    //Console.WriteLine("Applying Page Orientation as Landscape");
                    oWS.PageSetup.Orientation = XlPageOrientation.xlLandscape;

                    //Range ra =(Range)oWS.Cells["A4", "J4"];
                    //ra.Interior.Color = System.Drawing.Color.Gray;
                    //Console.WriteLine("Applying Printer Settings");
                    oWS.PageSetup.Zoom = false;
                    if (gridstr == "ArticlesandPagesExl")
                    {
                        oWS.PageSetup.PrintTitleRows = "$1:$1";
                        Range rngap = oWS.get_Range("A1", "E1");
                        rngap.Interior.ColorIndex = 15;
                    }
                    if (string.IsNullOrEmpty(gridstr))//For Invocie Exl(TandF)
                    {
                        oWS.PageSetup.FitToPagesWide = 1;
                        oWS.PageSetup.FitToPagesTall = 1;
                        oWS.Columns.AutoFit();
                    }
                    if (!string.IsNullOrEmpty(gridstr))//For Invoiced Report From GridView
                        oWS.Rows.AutoFit();
                    oWS.PageSetup.Application.Rows.Hidden = false;
                    oWS.PageSetup.Application.Columns.Hidden = false;
                    //oWS.PageSetup.Application.Rows.MergeCells = false;
                    oWS.PageSetup.PrintGridlines = true;
                    //Console.WriteLine("Applying margins");
                    oWS.PageSetup.TopMargin = excelApp.InchesToPoints(1);
                    oWS.PageSetup.RightMargin = excelApp.InchesToPoints(0.75);
                    oWS.PageSetup.BottomMargin = excelApp.InchesToPoints(1);
                    oWS.PageSetup.LeftMargin = excelApp.InchesToPoints(0.75);
                    oWS.PageSetup.HeaderMargin = excelApp.InchesToPoints(0.5);
                    oWS.PageSetup.FooterMargin = excelApp.InchesToPoints(0.5);
                    //Console.WriteLine("Applying autofit to columns and rows");


                    oWS.Cells.Borders.Value = 1;
                    //Console.WriteLine("Saving file: " + sFileName);
                    excelWorkbook.Save();
                    excelWorkbook = null;
                    newWorkbook = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                /*
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                System.GC.Collect();
                excelApp = null;
                */
                IntPtr hwnd = new IntPtr(excelApp.Hwnd);
                IntPtr ProcessId;
                IntPtr foo = GetWindowThreadProcessId(hwnd, out ProcessId);
                Process proc = Process.GetProcessById(ProcessId.ToInt32());
                proc.Kill();
                //excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                System.GC.Collect();
                excelApp = null;

            }

            //excelWorkbook.ReadOnly = false;



            //Console.WriteLine(sFileName);
            //Console.Read();
        }
    }


    public void InvRptExcelSetting(string sFileName, string gridstr)
    {
        //This is for only in server bcz in DPIUser68 remove Excel application
        if (ConfigurationManager.ConnectionStrings["urladd"].ToString() == "server")
        {
            Excel.Application excelApp = new Excel.ApplicationClass();
            try
            {
                excelApp.Visible = true;
                Excel.Workbook newWorkbook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(sFileName, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                foreach (Worksheet oWS in excelApp.ActiveWorkbook.Worksheets)
                {
                    //Console.WriteLine(sFileName);
                    //Console.WriteLine("Applying Page Size as A4");
                    oWS.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    //Console.WriteLine("Applying Page Orientation as Landscape");
                    oWS.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    if (gridstr == "true")
                    {
                        oWS.PageSetup.PrintTitleRows = "$1:$4";
                        Range rng = oWS.get_Range("A4", "J4");
                        rng.Interior.ColorIndex = 15;
                        oWS.Name = oWS.get_Range("A2", "A2").Text.ToString().Trim().Replace("  ", " ").Replace("MONTHLY INVOICING - ", "").Trim();
                    }
                    else if (gridstr == "false")
                    {
                        //oWS.Name = oWS.get_Range("A2", "A2").Text.ToString().Trim().Replace("  ", " ").Replace("MONTHLY INVOICING - ", "").Trim();
                        oWS.PageSetup.PrintTitleRows = "$1:$3";
                        //Range rng = oWS.get_Range("A3", "K3");
                        //rng.Interior.ColorIndex = 15;
                    }

                    //Range ra =(Range)oWS.Cells["A4", "J4"];
                    //ra.Interior.Color = System.Drawing.Color.Gray;
                    //Console.WriteLine("Applying Printer Settings");
                    oWS.PageSetup.Zoom = false;
                    //if (string.IsNullOrEmpty(gridstr))//For Invocie Exl(TandF)
                    //{
                    oWS.PageSetup.FitToPagesWide = 1;
                    //  oWS.PageSetup.FitToPagesTall = 1;


                    //oWS.Columns.AutoFit();


                    //}
                    // if (!string.IsNullOrEmpty(gridstr))//For Invoiced Report From GridView
                    oWS.Rows.AutoFit();
                    oWS.PageSetup.Application.Rows.Hidden = false;
                    oWS.PageSetup.Application.Columns.Hidden = false;
                    //oWS.PageSetup.Application.Rows.MergeCells = false;
                    oWS.PageSetup.PrintGridlines = true;
                    //Console.WriteLine("Applying margins");
                    oWS.PageSetup.TopMargin = excelApp.InchesToPoints(1);
                    oWS.PageSetup.RightMargin = excelApp.InchesToPoints(0.75);
                    oWS.PageSetup.BottomMargin = excelApp.InchesToPoints(1);
                    oWS.PageSetup.LeftMargin = excelApp.InchesToPoints(0.75);
                    oWS.PageSetup.HeaderMargin = excelApp.InchesToPoints(0.5);
                    oWS.PageSetup.FooterMargin = excelApp.InchesToPoints(0.5);
                    //Console.WriteLine("Applying autofit to columns and rows");
                    oWS.UsedRange.Borders.Value = 1;
                    //oWS.Name = oWS.get_Range("A2", "A2").Text.ToString().Trim().Replace("  ", " ").Replace("MONTHLY INVOICING - ", "").Trim();
                    //oWS.Cells.Borders.Value = 1;
                    //Console.WriteLine("Saving file: " + sFileName);

                    excelWorkbook.Save();
                    //newWorkbook.Close(true);
                    //excelWorkbook.Close 
                    excelWorkbook.Close(false, null, null);
                    NAR(excelWorkbook);
                    excelApp.Quit();
                    NAR(excelApp);
                    excelWorkbook = null;
                    newWorkbook = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //excelWorkbook.Close(false, null, null);
                //NAR(excelWorkbook);
                /*excelApp.Quit();
                NAR(excelApp);
                System.GC.Collect();
                excelApp = null;
                */
                IntPtr hwnd = new IntPtr(excelApp.Hwnd);
                IntPtr ProcessId;
                IntPtr foo = GetWindowThreadProcessId(hwnd, out ProcessId);
                Process proc = Process.GetProcessById(ProcessId.ToInt32());
                proc.Kill();
                //excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                System.GC.Collect();
                excelApp = null;
            }


            //excelWorkbook.ReadOnly = false;



            //Console.WriteLine(sFileName);
            //Console.Read();
        }
    }
    private void NAR(object o)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
        }
        catch { }
        finally
        {
            o = null;
        }
    }

    public void CreateXML()
    {
        //XmlDocument xmldoc = new XmlDocument();
        //XmlNode parent_element = xmldoc.CreateElement("files");
        //xmldoc.AppendChild(parent_element);

        //XmlNode xmlnod = xmldoc.CreateElement("file");
        //xmlnod.AppendChild(xmlnod);

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("files");
        xmlDoc.AppendChild(rootNode);

        XmlNode userNode = xmlDoc.CreateElement("file");
        XmlAttribute attribute = xmlDoc.CreateAttribute("type");
        attribute.Value = "Invoices";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("document");
        userNode.InnerText = "Datapage";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "Supplier";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "Datapage";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "Description";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "ExtRef1";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "ExtRef2";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "ExtRef3";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "CurCode";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "Date";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "BarCode";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "CmpCode";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "GB174";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "AccountCode";
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "ST020187";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "AuthFirstName";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "AuthLastName";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);

        userNode = xmlDoc.CreateElement("field");
        attribute = xmlDoc.CreateAttribute("name");
        attribute.Value = "DocId";
        userNode.Attributes.Append(attribute);
        //userNode.InnerText = "John Doe";
        rootNode.AppendChild(userNode);
        //------------------------------------------------------------------------//
        attribute = null;


        XmlNode linenode = xmlDoc.CreateElement("line");
        rootNode.AppendChild(linenode);



        for (i = 1; i <= 12; i++)
        {
            createnode(xmlDoc, linenode, attribute, field, name);
            linenode.AppendChild(userNode);
        }

        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //createnode(xmlDoc, rootNode, attribute, field, name);
        //rootNode.AppendChild(userNode);

        //userNode = xmlDoc.CreateElement("field");
        //attribute = xmlDoc.CreateAttribute("name");
        //attribute.Value = "DocId";
        //userNode.Attributes.Append(attribute);
        ////userNode.InnerText = "John Doe";
        //rootNode.AppendChild(userNode);


        xmlDoc.Save("d:\\sample.xml");


    }

    private void createnode(XmlDocument xmldocnode, XmlNode rootnode, XmlAttribute attr, string field, string name)
    {

        XmlNode usernode = xmldocnode.CreateElement(field);
        attr = xmldocnode.CreateAttribute(name);
        if (i == 1)
        {
            attr.Value = Description;
        }
        else if (i == 2)
            attr.Value = AccountCode1;
        else if (i == 3)
            attr.Value = AccountCode2;
        else if (i == 4)
            attr.Value = AccountCode3;
        else if (i == 5)
            attr.Value = AccountCode4;
        else if (i == 6)
            attr.Value = AccountCode5;
        else if (i == 7)
            attr.Value = AccountCode6;
        else if (i == 8)
            attr.Value = AccountCode7;
        else if (i == 9)
            attr.Value = AccountCode8;
        else if (i == 10)
            attr.Value = LineTax;
        else if (i == 11)
            attr.Value = LineValue;
        else if (i == 12)
        {
            attr.Value = LineSense;

        }
        usernode.Attributes.Append(attr);
        if (i == 10)
            usernode.InnerText = "0.00";
        else if (i == 12)
            usernode.InnerText = "debit";
        else

            usernode.InnerText = "Null";

        rootnode.AppendChild(usernode);


    }


}
