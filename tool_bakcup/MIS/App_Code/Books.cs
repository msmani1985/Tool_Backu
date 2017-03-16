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
public class Books
{
    private CustomerBase oCust;
    private Common oCom;
    private datasourceSQL oSql;
    private string sSql = "";
    public Books() { oCust = new CustomerBase();
    oCom = new Common();
    oSql = new datasourceSQL();
    }
    public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
    public DataSet getCusomerFinsite(string sCustID) { return this.oCust.getFinSiteByCustomer(sCustID); }
    public DataSet getBookStages() { return this.oCom.getStagesByJobType(0, 2); }
    public DataSet getChapterStages() { return this.oCom.getStagesByJobType(0, 7); }
    public DataSet getFormatOrStyles() { return this.oCom.getTypesettingPlatformList(); }
    public DataSet getServiceTypes() { return this.oCom.getServiceTypes(); }
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
            sSql = "spGetBooksList";
            param[0] = oSql.NewParameter("@BookName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookName);
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
            param[0] = oSql.NewParameter("@BookID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
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
            sSql = "[spGetLoggedEvents]";
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
    public DataSet getChapterDetailsByID(string sBookID, string sChapterID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetChapter]";
            param[0] = oSql.NewParameter("@BookID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sBookID);
            param[1] = oSql.NewParameter("@ChapterID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChapterID);
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
        SqlParameter[] param = new SqlParameter[24];
        try
        {
            sSql = "[spInsertBook]";
            param[0] = oSql.NewParameter("@jobtype_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[0]);
            param[1] = oSql.NewParameter("@book_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[1]);
            param[2] = oSql.NewParameter("@book_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[2]);
            param[3] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[3]);
            param[4] = oSql.NewParameter("@size", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[4]);
            param[5] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aBook[5]);
            param[6] = oSql.NewParameter("@isbn_print", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aBook[6]);
            param[7] = oSql.NewParameter("@isbn_online", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aBook[7]);
            param[8] = oSql.NewParameter("@service_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[8]);
            param[9] = oSql.NewParameter("@typesetting_platform_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[9]);
            param[10] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[10]);
            param[11] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[11]);
            param[12] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[12]);
            param[13] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[13]);
            param[14] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[14]);
            param[15] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[15]);
            param[16] = oSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[16]);
            param[17] = oSql.NewParameter("@invoice_description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aBook[17]);
            param[18] = oSql.NewParameter("@PONumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[18]);
            param[19] = oSql.NewParameter("@finsite_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[19]);
            param[20] = oSql.NewParameter("@sales_job_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[20]);
            param[21] = oSql.NewParameter("@author_BTW", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[21]);
            param[22] = oSql.NewParameter("@job_location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[22]);
            param[23] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[23]);
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
        SqlParameter[] param = new SqlParameter[25];
        try
        {
            sSql = "[spUpdateBook]";
            param[0] = oSql.NewParameter("@jobtype_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[0]);
            param[1] = oSql.NewParameter("@book_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[1]);
            param[2] = oSql.NewParameter("@book_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[2]);
            param[3] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[3]);
            param[4] = oSql.NewParameter("@size", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[4]);
            param[5] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aBook[5]);
            param[6] = oSql.NewParameter("@isbn_print", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aBook[6]);
            param[7] = oSql.NewParameter("@isbn_online", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aBook[7]);
            param[8] = oSql.NewParameter("@service_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[8]);
            param[9] = oSql.NewParameter("@typesetting_platform_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[9]);
            param[10] = oSql.NewParameter("@book_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[10]);
            param[11] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[11]);
            param[12] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[12]);
            param[13] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[13]);
            param[14] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[14]);
            param[15] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[15]);
            param[16] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aBook[16]);
            param[17] = oSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[17]);
            param[18] = oSql.NewParameter("@invoice_description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aBook[18]);
            param[19] = oSql.NewParameter("@PONumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[19]);
            param[20] = oSql.NewParameter("@finsite_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[20]);
            param[21] = oSql.NewParameter("@sales_job_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[21]);
            param[22] = oSql.NewParameter("@author_BTW", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[22]);
            param[23] = oSql.NewParameter("@job_location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[23]);
            param[24] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aBook[24]);
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
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            sSql = "[spInsertChapter]";
            param[0] = oSql.NewParameter("@book_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[0]);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[1]);
            param[2] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[3]);
            param[4] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[4]);
            param[5] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[5]);
            param[6] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[6]);
            param[7] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[7]);
            param[8] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[8]);
            param[9] = oSql.NewParameter("@no_figures", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[9]);
            param[10] = oSql.NewParameter("@comments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[10]);
            param[11] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[11]);
            param[12] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[12]);
            param[13] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[13]);
            param[14] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[14]);
            param[15] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[15]);
            param[16] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[16]);
            param[17] = oSql.NewParameter("@authorname", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[17]);
            param[18] = oSql.NewParameter("@authormail", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[18]);

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
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            sSql = "[spUpdateChapter]";
            param[0] = oSql.NewParameter("@chapter_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[0]);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[1]);
            param[2] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[3]);
            param[4] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aChapter[4]);
            param[5] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[5]);
            param[6] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[6]);
            param[7] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[7]);
            param[8] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[8]);
            param[9] = oSql.NewParameter("@no_figures", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[9]);
            param[10] = oSql.NewParameter("@comments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[10]);
            param[11] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[11]);
            param[12] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[12]);
            param[13] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[13]);
            param[14] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[14]);
            param[15] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[15]);
            param[16] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aChapter[16]);            

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
}
