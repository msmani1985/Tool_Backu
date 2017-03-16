using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Data.SqlClient;
using System.Text;
using System.Web.SessionState;
using System.Collections;
using System.Net;
using System.Globalization; 


/// <summary>
/// Summary description for Datasource_IBSQL
/// </summary>
public class Datasource_IBSQL
{
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;

	public Datasource_IBSQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
        oConn = new SqlConnection(sConStr);
        oConn.Open();
    }

    private void opencon_live()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrIB_SQL_Live"].ConnectionString;
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

    public bool UpdateApproveName(int empid, int ino, int Category, string comname,string custno)
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
                if (Category != 4)
                {
                    SelectQry = "Sp_GetInvNo";
                    ds = GetDataSet(SelectQry, "INVNO", CommandType.StoredProcedure);
                    invno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    ds = null;
                }
            }
            opencon();
            if (Category == 1 && comname == "saveprint")///////////////For Journal
            {
                DataSet custds = new DataSet();
                DataSet Artds = new DataSet();
                custds = GetDataSet("select custno from issue_dp i, journal_dp j where i.journo=j.journo and ino=" + ino, "Projects", CommandType.Text);
                string ano1 = "";
                if (custds != null && custds.Tables[0].Rows[0]["custno"].ToString() == "2556")
                {
                    SelectQry = " SELECT ANO,JOURNAL_DP.JOURNO,ISSUE_DP.INO,AMANUSCRIPTID,AEXTRA_COPY_EDIT,ARTICLE_DP.ADNO, ARTICLE_DP.CATNO, isnull(AREALNOOFPAGES,0) AREALNOOFPAGES," +
                   " isnull(AINVOICEPAGES,0) AINVOICEPAGES,JOURCODE,ISARTICLE_BASED,IINVOICENO,IINVOICEDATE,ARTICLE_DP.invno " +
                   " FROM ARTICLE_DP JOIN JOURNAL_DP ON JOURNAL_DP.JOURNO=ARTICLE_DP.JOURNO JOIN ISSUE_DP ON" +
                   " ISSUE_DP.JOURNO=JOURNAL_DP.JOURNO" +
                   " AND ISSUE_DP.INO=ARTICLE_DP.INO AND ARTICLE_DP.INO=" + ino + " AND ARTICLE_DP.ADNO NOT IN (10031,10032,12,13,1,4,5,2) AND  invno is null";
                    Artds = GetDataSet(SelectQry, "INVNO", CommandType.Text);
                    //invno = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                    //ds = null;
                    if (Artds.Tables[0].Rows.Count > 0)
                    {
                        for (int dscnt = 0; dscnt < Artds.Tables[0].Rows.Count; dscnt++)
                        {
                            if (ano1 == "")
                                ano1 = Artds.Tables[0].Rows[dscnt]["ano"].ToString();
                            else
                                ano1 = ano1 + "," + Artds.Tables[0].Rows[dscnt]["ano"].ToString();
                        }

                        string upqry = "update article_dp set ainvoiceddate='" + DateTime.Now.ToShortDateString() + "',invno=" + invno + " where ano in( ";
                        UpdateQry = upqry + ano1 + " )";

                        opencon();
                        oTran = oConn.BeginTransaction();
                        oCmd.Connection = oConn;
                        oCmd.Transaction = oTran;
                        oCmd.CommandType = CommandType.Text;
                        oCmd.CommandText = UpdateQry;
                        oCmd.ExecuteNonQuery();
                        oTran.Commit();
                    }
                }

                UpdateQry = "UPDATE ISSUE_DP SET  IINVOICEDATE = '" + DateTime.Now.ToShortDateString() + "',IINVOICENO =" + invno + ", IINVOICED = 'Y', APPROVEDBY_EMPID = " + empid + " WHERE INO = " + ino;
            }
            else if (Category == 1 && comname == "Approve")
                UpdateQry = "UPDATE ISSUE_DP SET APPROVEDBY_EMPID = " + empid + " WHERE INO = " + ino;
            else if (Category == 2 && comname == "saveprint")////////////For Books
                UpdateQry = "UPDATE BOOK_DP SET BINVOICEDATE = '" + DateTime.Now.ToShortDateString() + "',BINVOICENO =" + invno + ", BINVOICED = 'Y',  APPROVEEMPNO = " + empid + " WHERE BNO = " + ino;
            else if (Category == 2 && comname == "Approve")////////////For Books
                UpdateQry = "UPDATE BOOK_DP SET APPROVEEMPNO = " + empid + " WHERE BNO = " + ino;
            else if (Category == 3 && comname == "saveprint")
            {
                UpdateQry = "UPDATE PROJECTS_DP SET PINVOICEDDATE = '" + DateTime.Now.ToShortDateString() + "',INVNO =" + invno + ", PINVOICED = 'Y',   APPROVEEMPNO = " + empid + " WHERE PROJECTNO = " + ino;
                DataSet glds = new DataSet();
                try
                {
                    glds = GetDataSet("select * from projects_dp WHERE PARENT_PROJECTNO =" + ino, "Projects", CommandType.Text);
                    if (glds != null && glds.Tables[0].Rows.Count > 0)
                    {
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
                //UpdateQry = "select ano from SP_GET_INVOICE_WIP(" + ino + ")";
                //acmd.Connection = oConn;
                //acmd.CommandType = CommandType.StoredProcedure;
                //acmd.CommandText = UpdateQry;
                //SqlDataAdapter da = new SqlDataAdapter(acmd);
                //da.Fill(upds, "Article");


                upds = GetINVOICEDWIP(ino.ToString(),custno);
                string ano = "";
                if (upds != null && upds.Tables[0].Rows.Count > 0)
                {
                    SelectQry = "Sp_GetInvNo";
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
                    string upqry = "update article_dp set ainvoiceddate='" + DateTime.Now.ToShortDateString() + "',invno=" + invno + " where ano in( ";
                    UpdateQry = upqry + ano + " )";
                }
                else
                {
                    throw new ArgumentException("No article has been found, Please check");
                }
                opencon();
                oTran = oConn.BeginTransaction();
                oCmd.Connection = oConn;
                oCmd.Transaction = oTran;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = UpdateQry;
                oCmd.ExecuteNonQuery();
                if (custno == "2556")
                {
                    UpdateQry = " insert into WIPARTICLES_DP(CONNO,WINVOICED,WINVOICENO,WINVOICEDATE,CUSTNO,WIP_ITEMNAME,APPROVEEMPNO) values(" + ino + ",'Y'," + invno + ",'" + DateTime.Now.ToShortDateString() + "',2556,'WIP_" + invno + "'," + empid + ")";
                }
                else
                {
                    UpdateQry = " insert into WIPARTICLES_DP(CONNO,WINVOICED,WINVOICENO,WINVOICEDATE,CUSTNO,WIP_ITEMNAME,APPROVEEMPNO) values(" + ino + ",'Y'," + invno + ",'" + DateTime.Now.ToShortDateString() + "'," + Convert.ToInt32(custno) + ",'WIP_" + invno + "'," + empid + ")";
                }
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
        }
        finally
        {
            closecon();
            oCmd = null;
            upds = null;
            acmd.Dispose(); acmd = null;
        }
    }
    public DataSet GetINVOICEDWIP(string INO, string custno)
    {
        string[,] param = { { "@conno", INO.ToString() } };
        if (custno == "2556")
        {
            return ExcProcedure("SP_GET_INVOICE_WIP", param, CommandType.StoredProcedure);
        }
        else
        {
            return ExcProcedure("SP_GET_INVOICE_WIP_CW", new string[,] { { "@conno", INO }, { "@custno", custno } }, CommandType.StoredProcedure);
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

            if (invoiceno != "" && invoiceno != null)
                oNode = oDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICENO='" + invoiceno.Trim() + "']");
            if (oNode == null)
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
                    sNodeValue = Convert.ToInt32(oNode.Attributes.GetNamedItem("INVNO").Value) + 1;
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
    public string GetCurrencyConversions(string sCurrencyType)
    {
        Datasource_IBSQL dib = new Datasource_IBSQL();
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
    public DataSet GetCurrencyConversion(int iCurrencyNumber)
    {
        try
        {
            WebClient web = new WebClient();
            string data = "";
            if (iCurrencyNumber == 1)
            {
                data = web.DownloadString(string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=sl1d1t1ba&e=.csv", "EUR", "USD"));
            }
            else if (iCurrencyNumber == 4)
            {
                data = web.DownloadString(string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=sl1d1t1ba&e=.csv", "USD", "EUR"));
            }
            else if (iCurrencyNumber == 5)
            {
                data = web.DownloadString(string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=sl1d1t1ba&e=.csv", "GBP", "EUR"));
            }
            else if (iCurrencyNumber == 8)
            {
                data = web.DownloadString(string.Format("http://download.finance.yahoo.com/d/quotes.csv?s={0}{1}=X&f=sl1d1t1ba&e=.csv", "CAD", "EUR"));
            }
            decimal result;
            string rate = data.Split(',')[1];
            var style = NumberStyles.Number;
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            decimal.TryParse(rate, style, culture, out result);
            string strUpdate = "update CURRENCYCONVERSION_DP set CURRVALUE='" + rate.ToString() + "' where CURRNO= " + iCurrencyNumber;
            Boolean retVal = GetBoolean(strUpdate, "Module", CommandType.Text);
        }
        catch
        {

        }
        return GetDataSet("SELECT * FROM CURRENCYCONVERSION_DP WHERE CURRNO = " + iCurrencyNumber, "CURRENCY", CommandType.Text);
    }
    public DataSet GetDispatchedjobMail_coaction(int custno, int category, int conno, int flg)
    {
        return GetDataSet("SELECT PROJECTNO,PTITLE,PCODE,invno,PEMAIL_FLAG,PEMAIL_FLAG_IND FROM PROJECTS_DP WHERE INVNO IS NOT NULL AND PEMAIL_FLAG='N' and PEMAIL_FLAG_IND='N' and conno in(" + conno + ")", "PROJECTMAILSENT", CommandType.Text);
    }
    public DataSet GetEmailconfigDetails(int Projectorbookno, int category)
    {
        if (category == 3)//For project
            return GetDataSet("SELECT PCODE,PTITLE FROM PROJECTS_DP WHERE PROJECTNO=" + Projectorbookno, "PROJECTMAILSENT", CommandType.Text);
        else if (category == 2)//For Book
            return GetDataSet("SELECT BNUMBER,BTITLE FROM BOOK_DP WHERE BNO=" + Projectorbookno, "BOOKMAILSENT", CommandType.Text);
        else
            return null;
    }
    public DataSet GetDispatchedjobMail(int custno, int category, int conno, int flg)
    {
        string sCustno = custno.ToString();
        if ((category == 1 || category == 4) && flg == 1)
        {
            string[,] param = { { "@custno",custno.ToString()},
                                { "@conno",conno.ToString()},
                                { "@catgory",category.ToString()},
                                { "@flg",flg.ToString()} };
            return ExcProcedure("spGet_DISPATCHED_APPROVED_ITEMS_J", param, CommandType.StoredProcedure);
        }
        else if ((category == 1 || category == 4) && flg == 2)
        {
            string[,] param = { { "@custno",custno.ToString()},
                                { "@conno",conno.ToString()},
                                { "@catgory",category.ToString()},
                                { "@flg",flg.ToString()} };
            return ExcProcedure("spGet_DISPATCHED_APPROVED_ITEMS_J", param, CommandType.StoredProcedure);
        }
        else if (category == 2)
        {
            return GetDataSet("SELECT BEMAIL_FLAG,BEMAIL_FLAG_IND FROM BOOK_DP WHERE BEMAIL_FLAG='N' and BEMAIL_FLAG_IND='N' and BNO=" + conno, "BOOKMAILSENT", CommandType.Text);
        }
        else if (category == 3)
        {
            return GetDataSet("SELECT PROJECTNO,PTITLE,PCODE,PEMAIL_FLAG,PEMAIL_FLAG_IND FROM PROJECTS_DP WHERE PEMAIL_FLAG='N' and PEMAIL_FLAG_IND='N' and projectno in(" + conno + ")", "PROJECTMAILSENT", CommandType.Text);
        }
        else
            return null;
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
    private bool Execute_Sql(object[] oSaveitems)
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        bool blnCheck = false;
        string strSql = "";
        int cnt = 0;
        string connString = ConfigurationManager.ConnectionStrings["conStrIBLive"].ConnectionString;
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
            //   if (trans != null) trans.Dispose();
        }
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
            ocmd.CommandTimeout = 600;
            ocmd.Parameters.Clear();
            int i;
            //for (i = 0; i < ParamName.GetLength(0); i++)
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        //flg = true;
                        //OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);

                    //ocmd.Parameters.AddWithValue(sparameter[i,0].ToString(), sparameter[i,1]);                
                }
            }
            //ocmd.ExecuteNonQuery();
            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            sqlad.Fill(ods,"Getdetails");
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

    public DataSet ExcProcedure_COMBINED(string sProcName, string[,] sparameter, CommandType CmdType)
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
            ocmd.CommandTimeout = 600;
            ocmd.Parameters.Clear();
            int i;
            //for (i = 0; i < ParamName.GetLength(0); i++)
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        //flg = true;
                        //OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" || sparameter[i, 1].ToString() == "0")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);

                    //ocmd.Parameters.AddWithValue(sparameter[i,0].ToString(), sparameter[i,1]);                
                }
            }
            //ocmd.ExecuteNonQuery();
            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            sqlad.Fill(ods, "Details");
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
    public DataSet GetDataSet(string sProcedure, string sTitle, CommandType sCmdType)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcedure;
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, sTitle);
            oCmd = null;
            return ds;
        }
        catch (Exception oEx)
        {
            return null;
        }
        finally
        {
            closecon();
        }
    }

    public TandFInvoiceDS ExcProcedure_DS(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        
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
            ocmd.CommandTimeout = 6000;
            ocmd.Parameters.Clear();
            int i;
            //for (i = 0; i < ParamName.GetLength(0); i++)
            if (sparameter != null)
            {
                for (i = 0; i < sparameter.GetLength(0); i++)
                {
                    if (sparameter[i, 1].ToString().ToUpper() == "OUTPUT")
                    {
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "").Direction = ParameterDirection.Output;
                        //flg = true;
                        //OutparamName = sparameter[i, 0].ToString();
                    }
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "")
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), DBNull.Value);
                    else
                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), sparameter[i, 1]);

                    //ocmd.Parameters.AddWithValue(sparameter[i,0].ToString(), sparameter[i,1]);                
                }
            }
            //ocmd.ExecuteNonQuery();
            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            TandFInvoiceDS ods = new TandFInvoiceDS();
            sqlad.Fill(ods, "INVOICEDITEMS");
            if (ods == null || ods.Tables[1].Rows.Count <= 0)
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

    public TandFInvoiceDS GetInvoicedJobs3Outstanding(int custno, int journo)
    {
        string sCustno = custno.ToString();
        string[,] param = { { "@custno", custno.ToString() } }; 
        //if (custno == 10066)
        //    sCustno = "NULL";
        if (journo == 0)
        {
            //return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_ALL_IBSQL", param, CommandType.StoredProcedure);
            //if (this.HasPayments(sCustno))
            //{
            //    return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_ALL_CREDITEDACCOUNT_IBSQL", param, CommandType.StoredProcedure);

            //}
            ////return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);

            if (this.HasPayments(sCustno))
            {
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_ALL_CREDITEDACCOUNT_IBSQL", param, CommandType.StoredProcedure);

            }
            else
            {
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_ALL_IBSQL", param, CommandType.StoredProcedure);
            }
        }
        else if (journo == 2)////////////For Books
        {
            if (this.HasPayments(sCustno))
            {
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_B_IBSQL", param, CommandType.StoredProcedure);
            }
            else
            return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_B", param, CommandType.StoredProcedure);
            //return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_OUTSTANDING_B (" + sCustno + ")", "INVOICEDITEMS", CommandType.StoredProcedure);
            
        }
        else if (journo == 3)/////////For Projects
        {
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            if (this.HasPayments(sCustno))
            {
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_P_IBSQL", param, CommandType.StoredProcedure);
            }
            else
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_P", param, CommandType.StoredProcedure);
        }
        else ///////////////For Journal
        {
            if (this.HasPayments(sCustno))
            {
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_J_PAYMNETACCOUNT_IBSQL", param, CommandType.StoredProcedure);
            }
            else
                return ExcProcedure_DS("P_INVOICED_ITEMS_OUTSTANDING_J_IBSQL", param, CommandType.StoredProcedure);

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
        string[,] param = { { "@custno", custno.ToString() }, 
                            { "@sdate",fdate.ToString() },
                            { "@edate", tDate.ToString() }};
        if (journo == 0)
        {
            return ExcProcedure_DS("P_INVOICED_ITEMS_PAYMENTSRECEIVED_ALL_IBSQL", param, CommandType.StoredProcedure);
        }
        else if (journo == 2)////////////For Books
        {
            return ExcProcedure_DS("P_INVOICED_ITEMS_PAYMENTSRECEIVED_B_IBSQL", param, CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
        {
            return ExcProcedure_DS("P_INVOICED_ITEMS_PAYMENTSRECEIVED_P_IBSQL", param, CommandType.StoredProcedure);
        }
        else ///////////////For Journal
        {
            return ExcProcedure_DS("P_INVOICED_ITEMS_PAYMENTSRECEIVED_J_IBSQL", param, CommandType.StoredProcedure);
        }

        /* if (custno == 10066)
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         else
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         */
    }

    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate, int location)
    {
        string sCustno = custno.ToString();
        //if (custno == 10066)
        //    sCustno = "NULL";
        string condition = "";
        string[,] param1 = { { "@custno", custno.ToString() }, 
                            { "@sdate",fdate.ToString() },
                            { "@edate", tDate.ToString() }};
        string[,] param = { { "@custno", custno.ToString() }, 
                            { "@sdate",fdate.ToString() },
                            { "@edate", tDate.ToString() }, {"@locationid",location.ToString()}};
        if (location != 0)
            condition = "where locationid=" + location;
        if (journo == 0)
        {
            //string Qry = "SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " UNION SELECT JobNo,IINNO,IDATE,CNAME,JobTitle,Category,iissueno,ino,jourcode,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED, NoofArticles,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,EDITEDDISKTF,EDITEDDISKTFNOPRELIMS,TOTALPAGES_PSY,TOTALPAGES_PSY_NOPRELIMS,EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_WIP_J (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition;
            //Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS,0 as EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_B(" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition;
            //Qry += " UNION SELECT JOBNO,IINNO,IDATE,CNAME,JOBTITLE,CATEGORY,IISSUENO,INO,JOURCODE,CONCOUNTRY,TOTALPAGES,ISARTICLEBASED,NOOFARTICLES,JOURNO,TOTALPAGES_NOCOVER,CUSTNO1,GROUPBY,0 as EDITEDDISKTF,0 as EDITEDDISKTFNOPRELIMS,0 as TOTALPAGES_PSY,0 as TOTALPAGES_PSY_NOPRELIMS, 0 as EDITEDDISK_RAPL FROM P_INVOICED_ITEMS_P(" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition + " order by 2,3  ";

            //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
            //return GetUserDefineDs(spGet_Invoice_Items, "INVOICEDITEMS", CommandType.StoredProcedure);
            return ExcProcedure_DS("spGet_Invoice_Items", param, CommandType.StoredProcedure);
        }
        else if (journo == 2)////////////For Books
        {
            return ExcProcedure_DS("P_INVOICED_ITEMS_B", param1, CommandType.StoredProcedure);
        }
        // return GetUserDefineDs("SELECT * FROM P_INVOICED_ITEMS_B (" + sCustno + ",'" + fdate + "','" + tDate + "')" + condition, "INVOICEDITEMS", CommandType.StoredProcedure);
        else if (journo == 3)/////////For Projects
        {
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            return ExcProcedure_DS("P_INVOICED_ITEMS_P", param1, CommandType.StoredProcedure);
        }
        else if (journo == 1)///////////////For Journal
            return ExcProcedure_DS("spGet_Invoice_Items_WIP", param, CommandType.StoredProcedure);
        else
            return ExcProcedure_DS("spGet_Invoice_Items_WIP", param, CommandType.StoredProcedure);

        /* if (custno == 10066)
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         else
             return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
         */
    }
    public TandFInvoiceDS GetInvoicedJobs_YTD(int custno, int journo, string fdate, string tDate)
    {
        string sCustno = custno.ToString();
        string[,] param = { { "@custno", custno.ToString() }, 
                            { "@sdate",fdate.ToString() },
                            { "@edate", tDate.ToString() }};
        return ExcProcedure_YTD("spGetInvoiceYTD", param, CommandType.StoredProcedure);
    }

    public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate)
    {
        string sCustno = custno.ToString();
        string[,] param = { { "@custno", custno.ToString() }, 
                            { "@sdate",fdate.ToString() },
                            { "@edate", tDate.ToString() }};
        //Kalimuthu
        if (journo == 0)
            return ExcProcedure_YTD("spGetYTD", param, CommandType.StoredProcedure);
        else if (journo == 2)////////////For Books
            return ExcProcedure_YTD("spGetYTDBook", param, CommandType.StoredProcedure);
        else if (journo == 3)/////////For Projects
            return ExcProcedure_YTD("spGetYTDProject", param, CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            return ExcProcedure_YTD("spGetYTDJournal", param, CommandType.StoredProcedure);
        else
            return ExcProcedure_YTD("spGetYTDJournal", param, CommandType.StoredProcedure);
        //Old
        //if (journo == 0)
        //    return ExcProcedure_YTD("P_INVOICED_ITEMS_JOB_COSTING_IBSQL_Temp", param, CommandType.StoredProcedure);
        //else if (journo == 2)////////////For Books
        //    return ExcProcedure_YTD("P_INVOICED_ITEMS_B", param, CommandType.StoredProcedure);
        //else if (journo == 3)/////////For Projects
        //    return ExcProcedure_YTD("P_INVOICED_ITEMS_P", param, CommandType.StoredProcedure);
        //else if (journo == 1)///////////////For Journal
        //    return ExcProcedure_YTD("P_INVOICED_ITEMS_IBSQL", param, CommandType.StoredProcedure);
        //else
        //    return ExcProcedure_YTD("P_INVOICED_ITEMS_IBSQL", param, CommandType.StoredProcedure);
    }
    public TandFInvoiceDS GetInvoicedJobs4(int custno, int journo, string fdate, string tDate)
    {
        string sCustno = custno.ToString();
        string[,] param = { { "@custno", custno.ToString() }, 
                            { "@sdate",fdate.ToString() },
                            { "@edate", tDate.ToString() }};
        if (journo == 0)
            return ExcProcedure_YTD("P_INVOICED_ITEMS_JOB_COSTING_IBSQL_Temp", param, CommandType.StoredProcedure);
        else if (journo == 2)////////////For Books
            return ExcProcedure_YTD("P_INVOICED_ITEMS_B", param, CommandType.StoredProcedure);
        else if (journo == 3)/////////For Projects
            return ExcProcedure_YTD("P_INVOICED_ITEMS_P", param, CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            return ExcProcedure_YTD("P_INVOICED_ITEMS_IBSQL", param, CommandType.StoredProcedure);
        else
            return ExcProcedure_YTD("P_INVOICED_ITEMS_IBSQL", param, CommandType.StoredProcedure);
    }

    //public TandFInvoiceDS GetInvoicedJobs3(int custno, int journo, string fdate, string tDate)
    //{
    //    string sCustno = custno.ToString();
    //    //if (custno == 10066)
    //    //    sCustno = "NULL";
    //    string[,] param = { { "@custno", custno.ToString() }, 
    //                        { "@sdate",fdate.ToString() },
    //                        { "@edate", tDate.ToString() }};
    //    if (journo == 0)
    //    {
    //        return ExcProcedure_DS("P_INVOICED_ITEMS_JOB_COSTING_IBSQL", param, CommandType.StoredProcedure);
    //        //return GetDataSet(Qry, "INVOICEDITEMS", CommandType.StoredProcedure);
    //    }
    //    else if (journo == 2)////////////For Books
    //        return ExcProcedure_DS("P_INVOICED_ITEMS_B", param, CommandType.StoredProcedure);
    //    else if (journo == 3)/////////For Projects
    //        //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
    //        return ExcProcedure_DS("P_INVOICED_ITEMS_P", param, CommandType.StoredProcedure);
    //    else if (journo == 1)///////////////For Journal
    //        return ExcProcedure_DS("P_INVOICED_ITEMS_IBSQL", param, CommandType.StoredProcedure);
    //    else
    //        return ExcProcedure_DS("P_INVOICED_ITEMS_IBSQL", param, CommandType.StoredProcedure);
        
    //    /* if (custno == 10066)
    //         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
    //     else
    //         return GetDataSet("SELECT * FROM P_INVOICED_ITEMS_J (" + sCustno + ",'" + fdate + "','" + tDate + "')", "INVOICEDITEMS", CommandType.StoredProcedure);
    //     */
    //}  


    public DataSet GetDespatchedJobs1(int custno, int journo)
    {
        string sCustno = custno.ToString();
        
        //if (custno == 10066)
        //    sCustno = "0";
        string[,] param = { { "@custno", sCustno.ToString() } };
        if (journo == 4)/////////////For WIP
        {
            //return ExcProcedure("P_DISPATCHED_ITEMS_WIP", param, CommandType.StoredProcedure);
            if (sCustno == "2556" || sCustno == "10066")
            {
                return ExcProcedure("P_DISPATCHED_ITEMS_WIP", param, CommandType.StoredProcedure);
            }
            else
            {
                return ExcProcedure("P_DISPATCHED_ITEMS_WIP_CW", param, CommandType.StoredProcedure);
            }
        }
        else if (journo == 2)////////////For Books
        {
            //return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_B (" + sCustno + ")", "INVOICEABLEJOBS", CommandType.StoredProcedure);

            //if (sCustno == "10066")
            //STYPENO1=10079 this is for FINAL PROOF
            return ExcProcedure("SP_SELECT_BOOKS_INVOICE_NEW", param, CommandType.StoredProcedure);

            //else
            //    //STYPENO1=10079 this is for FINAL PROOF
            //    return GetDataSet("SELECT DISTINCT * FROM SP_SELECT_BOOKS_INVOICE_NEW WHERE BINVOICED1='N' and bdespatched1='Y' AND BDISPATCH1 IS NOT NULL AND STYPENO1=10079 AND CUSTNO1 = " + sCustno + " order by BDISPATCH1 desc", "BOOKLIST", CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            return ExcProcedure("P_DISPATCHED_ITEMS_P", param, CommandType.StoredProcedure);

        else if (journo == 1)///////////////For Journal

            return ExcProcedure("P_DISPATCHED_ITEMS_J", param, CommandType.StoredProcedure);

        //return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else
            return ExcProcedure("P_DISPATCHED_ITEMS_J", param, CommandType.StoredProcedure);
        //return GetDataSet("SELECT * FROM P_DISPATCHED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
    }

    public DataSet GetDespatchedJobs2(int custno, int journo)
    {
        string sCustno = custno.ToString();
        //if (custno == 10066)
        //    sCustno = "NULL";
        string[,] param = { { "@custno", sCustno.ToString() } };
        if (journo == 4)//For WIP
            return ExcProcedure("P_DISPATCHED_APPROVED_ITEMS_WIP ", param, CommandType.StoredProcedure);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_WIP (" + sCustno + ") ", "INVOICEABLEJOBS", CommandType.StoredProcedure);
        else if (journo == 2)////////////For Books
        {
            return ExcProcedure("SP_SELECT_BOOKS_INVOICE_FINAL", param, CommandType.StoredProcedure);
        }
        else if (journo == 3)/////////For Projects
            //return GetDataSet("select custname,projectno,pcode,ptitle,pcompleteddate from projects_dp x,customer_dp y where x.custno=y.custno and x.pinvoiced='N' and PDESPATCHED='Y' AND X.CUSTNO='" + custno + "' AND APPROVEEMPNO IS NOT NULL", "INVOICEABLEJOBS", CommandType.Text);
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_P (" + sCustno + ") ORDER BY PDISPATCHDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            return ExcProcedure("P_DISPATCHED_APPROVED_ITEMS_P", param, CommandType.StoredProcedure);
        else if (journo == 1)///////////////For Journal
            //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);
            return ExcProcedure("P_DISPATCHED_APPROVED_ITEMS_J", param, CommandType.StoredProcedure);
        else
            return ExcProcedure("P_DISPATCHED_APPROVED_ITEMS_J", param, CommandType.StoredProcedure);
        //return GetDataSet("SELECT * FROM P_DISPATCHED_APPROVED_ITEMS_J (" + sCustno + ") ORDER BY LEDATE DESC", "INVOICEABLEJOBS", CommandType.StoredProcedure);

    }

    public DataSet GetAllCustomers()
    {
        return GetDataSet("SELECT CUSTNO, CUSTNAME,FINSITENO FROM CUSTOMER_DP ORDER BY CUSTNAME", "CUSTOMERS", CommandType.Text);
    }

    private TandFInvoiceDS GetUserDefineDs(string sProcName, string sDSName, CommandType sCmdType)
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

    public DataSet ExcuteProc(string sProc, string TableName)
    {
        return GetDataSet(sProc, TableName, CommandType.Text);
    }

    public DataSet ExcueQueryString(string sQuery, string TableName)
    {
        return GetDataSet(sQuery, TableName, CommandType.Text);
    }

    public DataSet GetFor_Invoiceconfig(string typeid)
    {
        string[,] param = { { "@typeid",typeid.ToString()} };
        return ExcProcedure("P_INVOICECONFIGURATION_PB", param, CommandType.StoredProcedure);
    }

    public bool RunQuery(string Qry, string dsName)
    {
        return GetBoolean(Qry, dsName, CommandType.Text);
    }

    public DataSet GetJournalcode(string Qry)
    {
        return GetDataSet(Qry, "Journal", CommandType.Text);
    }
    public DataSet getcombinedissue(string INO1)
    {
        string[,] param = { { "@ino", INO1.ToString() } };
        return ExcProcedure_COMBINED("SP_GET_INV_COMBINED_ISSUE", param, CommandType.StoredProcedure);
    }
    public DataSet InvoiceDataSet(string procname, string[,] dsname, CommandType cmdtype)
    {
        return ExcProcedure(procname, dsname, cmdtype);
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
    public DataSet Get_WipArticledetails(int category, int custno)
        {
        
        //if (custno == 10066)
        //    custno = 0;
        string scustno = custno.ToString();
        string[,] param = { { "@custno", scustno.ToString() } };
        if (category == 1)//For Journal
            return ExcProcedure("SP_GET_INVOICE_WIP_ARTICLES", param, CommandType.StoredProcedure);
        return null;
    }

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
                case "wip": //For TandF and Psychology Press WIP  
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

    //public DataSet getCustomerPaymentOnAccount(string sCustno)
    //{
    //    string[,] cust = { { sCustno.ToString() } };
    //    return this.Execute_Sql("select * from credit_on_account where custno=" + cust + " and payment_date is null and obsolete is null order by credited_date");
    //}
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
    public DataSet getAllCustomers()
    {
        return GetDataSet("spGetCustomers", "Customer", CommandType.StoredProcedure);
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
    public jobperformanceds jobperformance(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        jobperformanceds ods = new jobperformanceds();
        char[] separator = new char[] { ',' };
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.CommandTimeout = 600;
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
            sqlad.Fill(ods, "Getdetails");
            if (ods == null || ods.Tables[1].Rows.Count <= 0)
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
    public TandFInvoiceDS ExcProcedure_YTD(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        char[] separator = new char[] { ',' };
        try
        {
            //opencon();
            sConStr = ConfigurationManager.ConnectionStrings["conStrIBLive1"].ConnectionString;
            oConn = new SqlConnection(sConStr);
            oConn.Open();
            oTran = oConn.BeginTransaction();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.CommandTimeout = 6000;
            ocmd.Transaction = oTran;
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
            TandFInvoiceDS ods = new TandFInvoiceDS();
            sqlad.Fill(ods, "INVOICEDITEMS");
            if (ods == null || ods.Tables[1].Rows.Count <= 0)
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
            oTran.Commit();
            ocmd = null;
            oTran = null;
            //if (ocmd != null) ocmd.Dispose();
            //if (oTran != null) oTran.Dispose();
            closecon();
        }

    }

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
    public DataSet NONPRODUCTION(string id, string Period)
    {
        return ExcProcedure("select * from NONPRODUCTION_STAFF where Employee_ID='" + id + "' AND ReviewPeriod='" + Period + "'", null, CommandType.Text);
    }
    public DataSet PRODUCTION(string id,string Period)
    {
        return ExcProcedure("select * from PRODUCTION_STAFF where Employee_ID='" + id + "' AND ReviewPeriod='" + Period + "'", null, CommandType.Text);
    }
    public void InsertFeedback(string Employee_ID, string NL_ID, string Positive, string Negative)
    {
        ExcSProcedure("insert into FeedBack_STAFF (Employee_ID,NP_ID,Positive,Negative,Creation_Date) values(" + Employee_ID + "," + NL_ID + ",'" + Positive + "','" + Negative + "',Getdate())", null, CommandType.Text);
        
    }
    public void UpdateFeedback(string E_ID, string NL_ID, string Years)
    {
        ExcSProcedure("Update FeedBack_STAFF Set Years='" + Years + "',NP_ID=" + NL_ID + " where Years is null and Employee_ID =" + E_ID, null, CommandType.Text);
    }
    public void UpdateProdFeedback(string E_ID, string NL_ID, string Years)
    {
        ExcSProcedure("Update FeedBack_STAFF Set Years='" + Years + "',P_ID=" + NL_ID + " where Years is null and Employee_ID =" + E_ID, null, CommandType.Text);
    }
    public void InsertFB(string Employee_ID, string Positive, string Negative)
    {
        ExcSProcedure("insert into FeedBack_STAFF (Employee_ID,Positive,Negative,Creation_Date) values(" + Employee_ID + ",'" + Positive + "','" + Negative + "',Getdate())", null, CommandType.Text);
    }
    public DataSet spGetID(string id)
    {
        return ExcProcedure("select MAX(NP_ID) NP_ID from NONPRODUCTION_STAFF where Employee_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet spGetFeedback(string id, string pid)
    {
        return ExcProcedure("select row_number() over (order by ID) as slno,* from dbo.FeedBack_STAFF where EMPLOYEE_ID=" + id + " and P_ID=" + pid + " order by ID", null, CommandType.Text);
    }
    public DataSet spGetFB(string id)
    {
        return ExcProcedure("select row_number() over (order by ID desc) as slno,* from dbo.FeedBack_STAFF where EMPLOYEE_ID=" + id + " order by ID desc", null, CommandType.Text);
    }
    public void InsertProdFeedback(string Employee_ID, string NL_ID, string Positive, string Negative)
    {
        ExcSProcedure("insert into FeedBack_STAFF (Employee_ID,P_ID,Positive,Negative) values(" + Employee_ID + "," + NL_ID + ",'" + Positive + "','" + Negative + "')", null, CommandType.Text);
    }
    public DataSet spProdGetID(string id)
    {
        return ExcProcedure("select MAX(P_ID) P_ID from PRODUCTION_STAFF where Employee_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet spGetEmp(string id)
    {
        return ExcProcedure("select LTRIM(rtrim(fname))+' '+LTRIM(rtrim(surname)) EmpName,* from Employee where employee_id=(select Level2 from Employee where employee_id='" + id + "')", null, CommandType.Text);
    }
    public DataSet spGetEmployee(int id,int loc)
    {
        return ExcProcedure("select LTRIM(rtrim(fname))+' '+LTRIM(rtrim(surname)) EmpName,* from Employee where Location_id=" + loc + " and  employee_number=" + id, null, CommandType.Text);
    }
    public DataSet spGetApp(string id)
    {
        return ExcProcedure("select AppName from Employee where employee_id='" + id + "'", null, CommandType.Text);
    }
    public DataSet spGetAppList(string LocID)
    {
        string[,] param = { { "@location_Id", LocID.ToString() } };
        return ExcProcedure("spGetAppName", param, CommandType.StoredProcedure);
    }
    public DataSet spGetNonProdFeedback(string id, string pid)
    {
        return ExcProcedure("select row_number() over (order by ID) as slno,* from dbo.FeedBack_STAFF where EMPLOYEE_ID=" + id + " and NP_ID=" + pid + " order by ID", null, CommandType.Text);
    }
    public DataSet spGetDoc(string id)
    {
        return ExcProcedure("select * from Emp_DocumentFile where employee_id='" + id + "' order by CreatedDate desc", null, CommandType.Text);
    }
}