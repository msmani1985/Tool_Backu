using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/*
/// <summary>  
/// Book Base:
/// Created by: Royson
/// Creation Date: 29 Apr 09
/// </summary>
 * */
public class Delphi_Books
{
    private Delphi_Customer oCust;
    private Delphi_Common oCom;
    private datasourceIBSQL oSql;
    private string sSql = "";
    public Delphi_Books()
    {
        oCust = new Delphi_Customer();
        oCom = new Delphi_Common();
    oSql = new datasourceIBSQL();
    }
    public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
    public DataSet getCusomerFinsite(string sCustID) { return this.oCust.getFinSiteByCustomer(sCustID); }
    public DataSet getPEName() { return this.oCust.getPEName(); }
    public DataSet getBookStages() { return this.oCom.getStagesByJobType(0); }
    public DataSet getChapterStages() { return this.oCom.getStagesByJobType(0); }
    public DataSet getFormatOrStyles() { return this.oCom.getTypesettingPlatformList(); }
    public DataSet getServiceTypes() { return this.oCom.getServiceTypes(); }
    //public DataSet getStatus() { 
    //    return this.oCom.getStatus(); 
    //}
    public DataSet getGraphicTypes() { return this.oCom.getGraphicTypes(); }
    public DataSet getDocTypes() { return this.oCom.getDocumentTypes("Book"); }
    public DataSet getStageTypes() { return this.oCom.getStageTypes("2"); }
    public DataSet getSalesGroup() { return this.oCom.getSalesJobGroups(); }

    public bool IsInvoiced(string sJobID) { return oCom.IsJobInvoiced(sJobID, "2"); }
    public DataSet getOnHoldTypes() { return this.oCom.getOnHoldTypes(); }
    public DataSet getBooks(string sBookName, char cCompleteFlag,string sCustomerID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetBookDetails_Delphi";
            param[0] = oSql.NewParameter("@Bookid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookName);
            param[1] = oSql.NewParameter("@Completed_flag", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
            param[2] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBooks(string sBookName, char cCompleteFlag, string sCustomerID, string sSearchMode
        , string sSearchFor, string sSearchExp, string sDate1, string sDate2, string sTypesetStyleIDs
        , string sCustomerIDs,string sJobStageIDs)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[11];
        try
        {
            sSql = "spGetBooksList";
            param[0] = oSql.NewParameter("@BookName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookName);
            param[1] = oSql.NewParameter("@Completed_flag", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
            param[2] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[3] = oSql.NewParameter("@SearchMode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sSearchMode);
            param[4] = oSql.NewParameter("@SearchFor", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sSearchFor);
            param[5] = oSql.NewParameter("@SearchExpression", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sSearchExp);
            param[6] = oSql.NewParameter("@Date1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDate1);
            param[7] = oSql.NewParameter("@Date2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDate2);
            param[8] = oSql.NewParameter("@typesetplatform_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sTypesetStyleIDs);
            param[9] = oSql.NewParameter("@customer_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerIDs);
            param[10] = oSql.NewParameter("@job_stage_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobStageIDs);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBookDetailsByID(string sBookID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetBook";
            param[0] = oSql.NewParameter("@Bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBookStageByID(string sBookID, string sStageID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetBookStage]";
            param[0] = oSql.NewParameter("@BookID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            param[1] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStageID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }

    public DataSet getBookChapterComments(string sBookID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCommentsHistory]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBookChapterEvents(string sBookID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[SpGetBookLogEvents]";
            param[0] = oSql.NewParameter("@bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getChapterNumbers(string sBookID, string sDoctypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetChapterNumbers]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            param[1] = oSql.NewParameter("@document_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDoctypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getChapterDetails(string sBookID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetChapter]";
            param[0] = oSql.NewParameter("@Bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getChapterDetailsByID(string sBookID,string sChapterID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetChapterDetailsbyID]";
            param[0] = oSql.NewParameter("@Bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            param[1] = oSql.NewParameter("@cno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChapterID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBookCostDetailsByID(string sBookID, string mode)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetBookCost]";
            param[0] = oSql.NewParameter("@BookID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            param[1] = oSql.NewParameter("@Mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, mode);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBookCostByID(string sJobInvTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetBookCost]";
            param[0] = oSql.NewParameter("@job_invoice_type_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobInvTypeID);
            param[1] = oSql.NewParameter("@Mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "2");            
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getInvoiceTypeItem(string sInvoiceTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sInvoiceTypeID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "2");
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public string InsertJobOnHold(string sJobID, string sJobTypeID, string sOnHoldTypeID, string sDetails, string sCreatedBy)
    {
        string Status = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spInsertJobOnHold]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@onhold_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sOnHoldTypeID);
            param[3] = oSql.NewParameter("@details", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDetails);
            param[4] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCreatedBy);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateJobOnHold(string sJobID, string sJobTypeID, string sOnHoldTypeID)
    {
        string Status = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spUpdateJobOnHold]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@onhold_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sOnHoldTypeID);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string InsertBook(string[] aBook)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[98];
        try
        {
            sSql = "[SpInsertBooks]";
            
            param[0] = oSql.NewParameter("@BNUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[0]);
            param[1] = oSql.NewParameter("@BTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[1]);
            param[2] = oSql.NewParameter("@CUSTNO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[2]);
            param[3] = oSql.NewParameter("@FINSITENO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[3]);
            param[4] = oSql.NewParameter("@BSIZE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[4]);
            param[5] = oSql.NewParameter("@BCOMMENTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[5]);
            param[6] = oSql.NewParameter("@BISBN", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[6]);
            param[7] = oSql.NewParameter("@BNOOFCOLORINSERTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[7]);
            param[8] = oSql.NewParameter("@BISBN2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[8]);
            param[9] = oSql.NewParameter("@BSNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[9]);
            param[10] = oSql.NewParameter("@TEMPLATE_CREATED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[12]);
            param[11] = oSql.NewParameter("@BCREDITED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[13]);
            param[12] = oSql.NewParameter("@BCREDITED_IND", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[14]);
            param[13] = oSql.NewParameter("@DNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[15]);
            param[14] = oSql.NewParameter("@CONNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[16]);
            param[15] = oSql.NewParameter("@BCNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[17]);
            param[16] = oSql.NewParameter("@PONUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[18]);
            param[17] = oSql.NewParameter("@BBOOKSTATUS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[20]);
            param[18] = oSql.NewParameter("@STYPENO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[21]);
            param[19] = oSql.NewParameter("@STNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[22]);
            param[20] = oSql.NewParameter("@EMPNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[23]);
            param[21] = oSql.NewParameter("@BACOSTDESC1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[24]);
            param[22] = oSql.NewParameter("@BAQTY1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[25]);
            param[23] = oSql.NewParameter("@BACNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[26]);
            param[24] = oSql.NewParameter("@BACADDNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[27]);
            param[25] = oSql.NewParameter("@BNOOFPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[28]);
            param[26] = oSql.NewParameter("@BNOOFFIGS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[29]);
            param[27] = oSql.NewParameter("@BNOOFTABLES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[30]);
            param[28] = oSql.NewParameter("@BNOOFEQUATIONS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[31]);

            param[29] = oSql.NewParameter("@BACOSTDESC2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[32]);
            param[30] = oSql.NewParameter("@BAQTY2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[33]);
            param[31] = oSql.NewParameter("@BACNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[34]);
            param[32] = oSql.NewParameter("@BACADDNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[35]);

            param[33] = oSql.NewParameter("@BACOSTDESC3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[36]);
            param[34] = oSql.NewParameter("@BAQTY3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[37]);
            param[35] = oSql.NewParameter("@BACNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[38]);
            param[36] = oSql.NewParameter("@BACADDNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[39]);

            param[37] = oSql.NewParameter("@BACOSTDESC4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[40]);
            param[38] = oSql.NewParameter("@BAQTY4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[41]);
            param[39] = oSql.NewParameter("@BACNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[42]);
            param[40] = oSql.NewParameter("@BACADDNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[43]);

            param[41] = oSql.NewParameter("@BACOSTDESC5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[44]);
            param[42] = oSql.NewParameter("@BAQTY5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[45]);
            param[43] = oSql.NewParameter("@BACNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[46]);
            param[44] = oSql.NewParameter("@BACADDNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[47]);


            param[45] = oSql.NewParameter("@BFIRSTSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[48]);
            param[46] = oSql.NewParameter("@BFIRSTDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[49]);
            param[47] = oSql.NewParameter("@BFIRSTHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[50]);
            param[48] = oSql.NewParameter("@BFIRSTDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[51]);
            param[49] = oSql.NewParameter("@BFIRSTEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[52]);

            param[50] = oSql.NewParameter("@BSECONDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[53]);
            param[51] = oSql.NewParameter("@BSECONDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[54]);
            param[52] = oSql.NewParameter("@BSECONDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[55]);
            param[53] = oSql.NewParameter("@BSECONDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[56]);
            param[54] = oSql.NewParameter("@BSECONDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[57]);

            param[55] = oSql.NewParameter("@BTHIRDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[58]);
            param[56] = oSql.NewParameter("@BTHIRDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[59]);
            param[57] = oSql.NewParameter("@BTHIRDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[60]);
            param[58] = oSql.NewParameter("@BTHIRDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[61]);
            param[59] = oSql.NewParameter("@BTHIRDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[62]);

            param[60] = oSql.NewParameter("@BFOURTHSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[63]);
            param[61] = oSql.NewParameter("@BFOURTHDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[64]);
            param[62] = oSql.NewParameter("@BFOURTHHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[65]);
            param[63] = oSql.NewParameter("@BFOURTHDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[66]);
            param[64] = oSql.NewParameter("@BFOURTHEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[70]);

            param[65] = oSql.NewParameter("@LOCATIONID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[67]);
            param[66] = oSql.NewParameter("@INDIA_RECD", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[68]);
            param[67] = oSql.NewParameter("@WPDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[69]);

            param[68] = oSql.NewParameter("@BFINALSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[71]);
            param[69] = oSql.NewParameter("@BFINALDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[72]);
            param[70] = oSql.NewParameter("@BFINALHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[73]);
            param[71] = oSql.NewParameter("@BFINALDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[74]);
            param[72] = oSql.NewParameter("@BFINALEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[75]);

            param[73] = oSql.NewParameter("@BTEMPLATESTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[76]);
            param[74] = oSql.NewParameter("@BTEMPLATEDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[77]);
            param[75] = oSql.NewParameter("@BTEMPLATEHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[78]);
            param[76] = oSql.NewParameter("@BTEMPLATEDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[79]);
            param[77] = oSql.NewParameter("@BTEMPLATEEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[80]);

            param[78] = oSql.NewParameter("@BSAMPLESTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[81]);
            param[79] = oSql.NewParameter("@BSAMPLEDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[82]);
            param[80] = oSql.NewParameter("@BSAMPLEHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[10]);
            param[81] = oSql.NewParameter("@BSAMPLEDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[11]);
            param[82] = oSql.NewParameter("@BSAMPLEEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[19]);
            param[83] = oSql.NewParameter("@BCOST", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[83]);
            param[84] = oSql.NewParameter("@BTYPE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[84]);
            param[85] = oSql.NewParameter("@BEMAILTYPE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[85]);
            param[86] = oSql.NewParameter("@TSPRINTAREA", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[86]);
            param[87] = oSql.NewParameter("@OUTPRINTAREA", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[87]);
            param[88] = oSql.NewParameter("@BTYPESETCOST_2007", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[88]);
            param[89] = oSql.NewParameter("@BOUTPUTTINGCOST_2007", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[89]);
            param[90] = oSql.NewParameter("@BCCNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[90]);
            param[91] = oSql.NewParameter("@S_TypeNo", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[91]);
            param[92] = oSql.NewParameter("@Received_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[92]);
            param[93] = oSql.NewParameter("@HalfDue_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[93]);
            param[94] = oSql.NewParameter("@Due_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[94]);
            param[95] = oSql.NewParameter("@Dispatch_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[95]);
            param[96] = oSql.NewParameter("@Pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[96]);
            param[97] = oSql.NewParameter("@Template", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[97]);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdateBook(string[] aBook)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[99];
        try
        {
            sSql = "[SpUpdateBooks]";

            param[0] = oSql.NewParameter("@BNUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[0]);
            param[1] = oSql.NewParameter("@BTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[1]);
            param[2] = oSql.NewParameter("@CUSTNO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[2]);
            param[3] = oSql.NewParameter("@FINSITENO", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[3]);
            param[4] = oSql.NewParameter("@BSIZE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[4]);
            param[5] = oSql.NewParameter("@BCOMMENTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[5]);
            param[6] = oSql.NewParameter("@BISBN", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[6]);
            param[7] = oSql.NewParameter("@BNOOFCOLORINSERTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[7]);
            param[8] = oSql.NewParameter("@BISBN2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[8]);
            param[9] = oSql.NewParameter("@BSNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[9]);
            param[10] = oSql.NewParameter("@TEMPLATE_CREATED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[12]);
            param[11] = oSql.NewParameter("@BCREDITED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[13]);
            param[12] = oSql.NewParameter("@BCREDITED_IND", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[14]);
            param[13] = oSql.NewParameter("@DNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[15]);
            param[14] = oSql.NewParameter("@CONNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[16]);
            param[15] = oSql.NewParameter("@BCNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[17]);
            param[16] = oSql.NewParameter("@PONUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[18]);
            param[17] = oSql.NewParameter("@BBOOKSTATUS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[20]);
            param[18] = oSql.NewParameter("@STYPENO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[21]);
            param[19] = oSql.NewParameter("@STNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[22]);
            param[20] = oSql.NewParameter("@EMPNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[23]);
            param[21] = oSql.NewParameter("@BACOSTDESC1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[24]);
            param[22] = oSql.NewParameter("@BAQTY1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[25]);
            param[23] = oSql.NewParameter("@BACNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[26]);
            param[24] = oSql.NewParameter("@BACADDNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[27]);
            param[25] = oSql.NewParameter("@BNOOFPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[28]);
            param[26] = oSql.NewParameter("@BNOOFFIGS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[29]);
            param[27] = oSql.NewParameter("@BNOOFTABLES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[30]);
            param[28] = oSql.NewParameter("@BNOOFEQUATIONS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[31]);

            param[29] = oSql.NewParameter("@BACOSTDESC2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[32]);
            param[30] = oSql.NewParameter("@BAQTY2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[33]);
            param[31] = oSql.NewParameter("@BACNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[34]);
            param[32] = oSql.NewParameter("@BACADDNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[35]);

            param[33] = oSql.NewParameter("@BACOSTDESC3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[36]);
            param[34] = oSql.NewParameter("@BAQTY3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[37]);
            param[35] = oSql.NewParameter("@BACNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[38]);
            param[36] = oSql.NewParameter("@BACADDNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[39]);

            param[37] = oSql.NewParameter("@BACOSTDESC4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[40]);
            param[38] = oSql.NewParameter("@BAQTY4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[41]);
            param[39] = oSql.NewParameter("@BACNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[42]);
            param[40] = oSql.NewParameter("@BACADDNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[43]);

            param[41] = oSql.NewParameter("@BACOSTDESC5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[44]);
            param[42] = oSql.NewParameter("@BAQTY5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[45]);
            param[43] = oSql.NewParameter("@BACNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[46]);
            param[44] = oSql.NewParameter("@BACADDNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[47]);


            param[45] = oSql.NewParameter("@BFIRSTSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[48]);
            param[46] = oSql.NewParameter("@BFIRSTDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[49]);
            param[47] = oSql.NewParameter("@BFIRSTHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[50]);
            param[48] = oSql.NewParameter("@BFIRSTDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[51]);
            param[49] = oSql.NewParameter("@BFIRSTEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[52]);

            param[50] = oSql.NewParameter("@BSECONDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[53]);
            param[51] = oSql.NewParameter("@BSECONDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[54]);
            param[52] = oSql.NewParameter("@BSECONDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[55]);
            param[53] = oSql.NewParameter("@BSECONDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[56]);
            param[54] = oSql.NewParameter("@BSECONDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[57]);

            param[55] = oSql.NewParameter("@BTHIRDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[58]);
            param[56] = oSql.NewParameter("@BTHIRDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[59]);
            param[57] = oSql.NewParameter("@BTHIRDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[60]);
            param[58] = oSql.NewParameter("@BTHIRDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[61]);
            param[59] = oSql.NewParameter("@BTHIRDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[62]);

            param[60] = oSql.NewParameter("@BFOURTHSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[63]);
            param[61] = oSql.NewParameter("@BFOURTHDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[64]);
            param[62] = oSql.NewParameter("@BFOURTHHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[65]);
            param[63] = oSql.NewParameter("@BFOURTHDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[66]);
            param[64] = oSql.NewParameter("@BFOURTHEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[70]);

            param[65] = oSql.NewParameter("@LOCATIONID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[67]);
            param[66] = oSql.NewParameter("@INDIA_RECD", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[68]);
            param[67] = oSql.NewParameter("@WPDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[69]);

            param[68] = oSql.NewParameter("@BFINALSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[71]);
            param[69] = oSql.NewParameter("@BFINALDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[72]);
            param[70] = oSql.NewParameter("@BFINALHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[73]);
            param[71] = oSql.NewParameter("@BFINALDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[74]);
            param[72] = oSql.NewParameter("@BFINALEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[75]);

            param[73] = oSql.NewParameter("@BTEMPLATESTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[76]);
            param[74] = oSql.NewParameter("@BTEMPLATEDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[77]);
            param[75] = oSql.NewParameter("@BTEMPLATEHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[78]);
            param[76] = oSql.NewParameter("@BTEMPLATEDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[79]);
            param[77] = oSql.NewParameter("@BTEMPLATEEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[80]);

            param[78] = oSql.NewParameter("@BSAMPLESTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[81]);
            param[79] = oSql.NewParameter("@BSAMPLEDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[82]);
            param[80] = oSql.NewParameter("@BSAMPLEHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[10]);
            param[81] = oSql.NewParameter("@BSAMPLEDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[11]);
            param[82] = oSql.NewParameter("@BSAMPLEEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[19]);
            param[83] = oSql.NewParameter("@BCOST", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[83]);
            param[84] = oSql.NewParameter("@BTYPE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[84]);
            param[85] = oSql.NewParameter("@BEMAILTYPE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[85]);
            param[86] = oSql.NewParameter("@TSPRINTAREA", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[86]);
            param[87] = oSql.NewParameter("@OUTPRINTAREA", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[87]);
            param[88] = oSql.NewParameter("@BTYPESETCOST_2007", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[88]);
            param[89] = oSql.NewParameter("@BOUTPUTTINGCOST_2007", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[89]);
            param[90] = oSql.NewParameter("@BCCNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[90]);
            param[91] = oSql.NewParameter("@S_TypeNo", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[91]);
            param[92] = oSql.NewParameter("@Received_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[92]);
            param[93] = oSql.NewParameter("@HalfDue_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[93]);
            param[94] = oSql.NewParameter("@Due_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[94]);
            param[95] = oSql.NewParameter("@Dispatch_Date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[95]);
            param[96] = oSql.NewParameter("@BNO_V", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[96]);
            param[97] = oSql.NewParameter("@Pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[97]);
            param[98] = oSql.NewParameter("@template", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[98]);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public DataSet getGraphicDetails(string sId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetGraphicDetails]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sId);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public void InsertEmptyGraphics(string sID, string sType, string sGraphicType, int count)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "[spInsertBlankGraphics]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sType);
            param[2] = oSql.NewParameter("@graphic_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sGraphicType);
            param[3] = oSql.NewParameter("@total_count", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, count);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    public string UpdateGraphics(System.Collections.ArrayList lstGraphics)
    {
        sSql = "";
        int i = 0;
        object[] oGraphics = new object[lstGraphics.Count];
        try
        {
            foreach (string[] a in lstGraphics){
                sSql = "update job_graphic set graphic_name='" + a[1] + "', graphic_type_id=" + a[2] + ", graphic_desc='" + a[3] + "' where graphic_id=" + a[0];
                oGraphics[i] = sSql;
                i++;
            }
            if (i > 0) oSql.Execute_Sql(oGraphics);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return "true";
    }
    public string DeleteGraphics(string sGraphicIDs)
    {
        sSql = "";
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteGraphicDetails]";
            param[0] = oSql.NewParameter("@graphic_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sGraphicIDs);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }

    public string InsertEmptyChapters(string sID, string sChapPrefix, string sDocType, int count, 
        string sStartDate, string sDueDate, string sHlfDueDate, string sCreatedBy)
    {
        sSql = "";
        string status = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertBlankChapters]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChapPrefix);
            param[2] = oSql.NewParameter("@document_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDocType);
            param[3] = oSql.NewParameter("@total_count", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, count);
            param[4] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartDate);
            param[5] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDueDate);
            param[6] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sHlfDueDate);
            param[7] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCreatedBy);
            status = oSql.Execute_SP(sSql, param, 
                oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return status;
    }
    public string InsertChapter(string[] aChapter)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[40];
        try
        {
            sSql = "[spInsertChapter]";
            param[0] = oSql.NewParameter("@BNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[0]);
            param[1] = oSql.NewParameter("@CNAME", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[1]);
            param[2] = oSql.NewParameter("@CTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[2]);
            param[3] = oSql.NewParameter("@NO_CHAPTERS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[3]);
            param[4] = oSql.NewParameter("@COMMENTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[4]);
            param[5] = oSql.NewParameter("@NO_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[5]);
            param[6] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[6]);
            param[7] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[7]);
            param[8] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[8]);
            param[9] = oSql.NewParameter("@no_figures", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[9]);
            param[10] = oSql.NewParameter("@STNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[10]);
            param[11] = oSql.NewParameter("@START_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[11]);
            param[12] = oSql.NewParameter("@END_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[12]);
            param[13] = oSql.NewParameter("@BLANK_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[13]);
            param[14] = oSql.NewParameter("@CSTNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[14]);
            param[15] = oSql.NewParameter("@CFIRSTSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[15]);
            param[16] = oSql.NewParameter("@CFIRSTDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[16]);
            param[17] = oSql.NewParameter("@CFIRSTHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[17]);
            param[18] = oSql.NewParameter("@CFIRSTDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[18]);
            param[19] = oSql.NewParameter("@CFIRSTEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[19]);
            param[20] = oSql.NewParameter("@CSECONDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[20]);
            param[21] = oSql.NewParameter("@CSECONDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[21]);
            param[22] = oSql.NewParameter("@CSECONDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[22]);
            param[23] = oSql.NewParameter("@CSECONDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[23]);
            param[24] = oSql.NewParameter("@CSECONDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[24]);
            param[25] = oSql.NewParameter("@CTHIRDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[25]);
            param[26] = oSql.NewParameter("@CTHIRDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[26]);
            param[27] = oSql.NewParameter("@CTHIRDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[27]);
            param[28] = oSql.NewParameter("@CTHIRDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[28]);
            param[29] = oSql.NewParameter("@CTHIRDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[29]);
            param[30] = oSql.NewParameter("@CFOURTHSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[30]);
            param[31] = oSql.NewParameter("@CFOURTHDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[31]);
            param[32] = oSql.NewParameter("@CFOURTHHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[32]);
            param[33] = oSql.NewParameter("@CFOURTHDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[33]);
            param[34] = oSql.NewParameter("@CFOURTHEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[34]);
            param[35] = oSql.NewParameter("@CFINALSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[35]);
            param[36] = oSql.NewParameter("@CFINALDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[36]);
            param[37] = oSql.NewParameter("@CFINALHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[37]);
            param[38] = oSql.NewParameter("@CFINALDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[38]);
            param[39] = oSql.NewParameter("@CFINALEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[39]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdateChapter(string[] aChapter)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[41];
        try
        {
            sSql = "[SpUpdateChapter]";
            param[0] = oSql.NewParameter("@BNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[0]);
            param[1] = oSql.NewParameter("@CNAME", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[1]);
            param[2] = oSql.NewParameter("@CTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[2]);
            param[3] = oSql.NewParameter("@NO_CHAPTERS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[3]);
            param[4] = oSql.NewParameter("@COMMENTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[4]);
            param[5] = oSql.NewParameter("@NO_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[5]);
            param[6] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[6]);
            param[7] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[7]);
            param[8] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[8]);
            param[9] = oSql.NewParameter("@no_figures", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[9]);
            param[10] = oSql.NewParameter("@STNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[10]);
            param[11] = oSql.NewParameter("@START_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[11]);
            param[12] = oSql.NewParameter("@END_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[12]);
            param[13] = oSql.NewParameter("@BLANK_PAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[13]);
            param[14] = oSql.NewParameter("@CSTNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[14]);
            param[15] = oSql.NewParameter("@CFIRSTSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[15]);
            param[16] = oSql.NewParameter("@CFIRSTDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[16]);
            param[17] = oSql.NewParameter("@CFIRSTHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[17]);
            param[18] = oSql.NewParameter("@CFIRSTDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[18]);
            param[19] = oSql.NewParameter("@CFIRSTEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[19]);
            param[20] = oSql.NewParameter("@CSECONDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[20]);
            param[21] = oSql.NewParameter("@CSECONDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[21]);
            param[22] = oSql.NewParameter("@CSECONDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[22]);
            param[23] = oSql.NewParameter("@CSECONDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[23]);
            param[24] = oSql.NewParameter("@CSECONDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[24]);
            param[25] = oSql.NewParameter("@CTHIRDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[25]);
            param[26] = oSql.NewParameter("@CTHIRDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[26]);
            param[27] = oSql.NewParameter("@CTHIRDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[27]);
            param[28] = oSql.NewParameter("@CTHIRDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[28]);
            param[29] = oSql.NewParameter("@CTHIRDEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[29]);
            param[30] = oSql.NewParameter("@CFOURTHSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[30]);
            param[31] = oSql.NewParameter("@CFOURTHDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[31]);
            param[32] = oSql.NewParameter("@CFOURTHHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[32]);
            param[33] = oSql.NewParameter("@CFOURTHDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[33]);
            param[34] = oSql.NewParameter("@CFOURTHEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[34]);
            param[35] = oSql.NewParameter("@CFINALSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[35]);
            param[36] = oSql.NewParameter("@CFINALDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[36]);
            param[37] = oSql.NewParameter("@CFINALHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[37]);
            param[38] = oSql.NewParameter("@CFINALDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[38]);
            param[39] = oSql.NewParameter("@CFINALEMPLOYEE", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[39]);
            param[40] = oSql.NewParameter("@CNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[40]);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string InsertInvoiceTypeItem(string sInvoiceTypeID, string sInvoiceTypeItemName)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spInsertInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@InvoiceType_item_Name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sInvoiceTypeItemName);
            param[1] = oSql.NewParameter("@Invoice_Type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sInvoiceTypeID);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "2");

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string InsertBookCost(string[] aBookCost)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_no", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[4]);
            param[5] = oSql.NewParameter("@quantity_addnl_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[6]);
            param[7] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[7]);
            
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdateBookCost(string[] aBookCost)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spUpdateJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_no", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[4]);
            param[5] = oSql.NewParameter("@quantity_addnl_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[6]);
            param[7] = oSql.NewParameter("@job_invoice_type_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[7]);
            param[8] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBookCost[8]);

            Status = oSql.Execute_SP(sSql, param,
                  oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string DeleteBookCost(string sJobInvTypeItemID)
    {
        sSql = "";
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@job_invoice_type_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobInvTypeItemID);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public bool UpdateBookCostIndex(string[] aBookCost)
    {
        object[] oSqlData;
        string sSql = "";
        try
        {
            oSqlData=new object[aBookCost.Length];
            foreach (string s in aBookCost){
                sSql = "update job_invoice_type_item set order_index=" + s.Split('|')[1] + " where job_invoice_type_item_id=" + s.Split('|')[0];
                oSqlData[Array.IndexOf(aBookCost, s)] = sSql;
            }
            return oSql.Execute_Sql(oSqlData);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return false;
    }
    public DataSet GetEmployeeName()
    {
        return oSql.GetDataSet("select empno,rtrim(ltrim(EMP_FNAME)) + ' ' + rtrim(ltrim(EMP_SNAME)) from employee_dp ", "Employee", CommandType.Text);
    }
    public DataSet GetDepartment()
    {
        return oSql.GetDataSet("SELECT * FROM DEPARTMENT_DP  ORDER BY DNAME", "DEPARTMENT", CommandType.Text);
    }
    public DataSet getCurrentStatus()
    {
        sSql = "";
        DataSet ds = new DataSet();
        //SqlParameter[] para = new SqlParameter[1];
        try
        {
            sSql = "spGetCurrentStatus";
            ds = oSql.FillDataSet_SP(sSql, null);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBook_Hist_DetailsByID(string sBookID, string jobHistID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetBookHistory";
            param[0] = oSql.NewParameter("@bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            param[1] = oSql.NewParameter("@jobHistID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, jobHistID);
            //param[2] = oSql.NewParameter("@dispDate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, disDate);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getBookStage()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetBookStage";
            ds = oSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet GetChapterList(string sBookID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetChapList";
            param[0] = oSql.NewParameter("@bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet GetChapterListDetails(string cpno)
    {
        return oSql.GetDataSet("select * from ChapterList_DP where cpno=" + cpno, "Employee", CommandType.Text);
    }
    public DataSet GetChapterMatterDetails(string bno)
    {
        return oSql.GetDataSet("select * from Chapter_DP where bno=" + bno, "Employee", CommandType.Text);
    }
    public DataSet GetBookLogEvents(string sBookID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetBookLoggedEvents";
            param[0] = oSql.NewParameter("@bno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet GetChapJobHis(string sChapID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetChapJobHistory";
            param[0] = oSql.NewParameter("@cpno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChapID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public void DeleteChapList(string cpno)
    {
        Launch_SQL lSql = new Launch_SQL();
        lSql.ExcSProcedure("Delete from ChapterList_DP where cpno=" + cpno, null, CommandType.Text);
    }
}
