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

/// <summary>
/// Summary description for Chapter
/// </summary>
public class Chapter
{
     private CustomerBase oCust;
    private Common oCom;
    private datasourceSQL oSql;
    private string sSql = "";
	public Chapter()
	{
		oCust = new CustomerBase();
        oCom = new Common();
        oSql = new datasourceSQL();
	}
   
    
    public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
    public DataSet getGraphicTypes() { return this.oCom.getGraphicTypes(); }
    public DataSet getFigureTypes() { return this.oCom.getFigureTypes(); }
    public DataSet getArticleStages() { return this.oCom.getStagesByJobType(0, 7); }
    public DataSet getDocTypes() { return this.oCom.getDocumentTypes(""); }
    public DataSet getDocItemTypes(string sDocTypeID) { return this.oCom.getDocumentItemTypes(sDocTypeID); }
    public DataSet getCategoryTypes() { return this.oCom.getCategoryTypes(); }
    public DataSet getOnHoldTypes() { return this.oCom.getOnHoldTypes(); }
    public DataSet getStageTypes() { return this.oCom.getStageTypes("7"); }
    public DataSet getSalesGroup() { return this.oCom.getSalesJobGroups(); }
    public bool IsInvoiced(string sJobID) { return oCom.IsJobInvoiced(sJobID, "7"); }
    public DataSet getJournalsByCustomer(string sCustomerID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJournalList]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
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
    public DataSet getArticleStagesByJournal(string sJournalID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGET_NEXT_STAGE]";
            param[0] = oSql.NewParameter("@journalid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "7");
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
    public DataSet getArticles(string sArticleName, string sArticleType, char cCompleteFlag)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetChapterList]";
            param[0] = oSql.NewParameter("@ArticleName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleName);
            param[1] = oSql.NewParameter("@ArticleType", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleType);
            param[2] = oSql.NewParameter("@Completed_flag", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
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
    public DataSet getArticles(string[] aArtDetails)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[26];
        try
        {
            sSql = "[spGetChapterList]";
            param[0] = oSql.NewParameter("@ArticleName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[0]);
            param[1] = oSql.NewParameter("@ArticleType", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aArtDetails[1]);
            param[2] = oSql.NewParameter("@Completed_flag", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[2]);
            param[3] = oSql.NewParameter("@SearchMode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[3]);
            param[4] = oSql.NewParameter("@SearchFor", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[4]);
            param[5] = oSql.NewParameter("@JourCode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[5]);
            param[6] = oSql.NewParameter("@JourCodeExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[6]);
            param[7] = oSql.NewParameter("@ArticleCode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[7]);
            param[8] = oSql.NewParameter("@ArticleCodeExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[8]);
            param[9] = oSql.NewParameter("@IssueNumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[9]);
            param[10] = oSql.NewParameter("@IssueNumberExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[10]);
            param[11] = oSql.NewParameter("@IssueOnHold", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aArtDetails[11]);
            param[12] = oSql.NewParameter("@RecExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[12]);
            param[13] = oSql.NewParameter("@RecDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[13]);
            param[14] = oSql.NewParameter("@RecDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[14]);
            param[15] = oSql.NewParameter("@DueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[15]);
            param[16] = oSql.NewParameter("@DueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[16]);
            param[17] = oSql.NewParameter("@DueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[17]);
            param[18] = oSql.NewParameter("@HlfDueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[18]);
            param[19] = oSql.NewParameter("@HlfDueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[19]);
            param[20] = oSql.NewParameter("@HlfDueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[20]);
            param[21] = oSql.NewParameter("@CatsDueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[21]);
            param[22] = oSql.NewParameter("@CatsDueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[22]);
            param[23] = oSql.NewParameter("@CatsDueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[23]);
            param[24] = oSql.NewParameter("@stage_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[24]);
            param[25] = oSql.NewParameter("@customer_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArtDetails[25]);
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
    public DataSet getArticleDetailsByID(string sArticleID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetArticle";
            param[0] = oSql.NewParameter("@ArticleID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
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
    public DataSet getArticleStageByID(string sArticleID, string sStageID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetArticleStage]";
            param[0] = oSql.NewParameter("@ArticleID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
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
    public DataSet getArticleEvents(string sArticleID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetLoggedEvents]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);

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
    public DataSet getArticleComments(string sArticleID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetCommentsHistory]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sArticleID);
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
    public string InsertArticle(string[] aArticle)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[39];
        try
        {
            sSql = "[spInsertArticle]";
            param[0] = oSql.NewParameter("@journal_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[0]);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[1]);
            param[2] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[3]);
            param[4] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[4]);
            param[5] = oSql.NewParameter("@document_item_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[5]);
            param[6] = oSql.NewParameter("@category_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[6]);
            param[7] = oSql.NewParameter("@doi", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[7]);
            param[8] = oSql.NewParameter("@author", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[8]);
            param[9] = oSql.NewParameter("@author_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[9]);
            param[10] = oSql.NewParameter("@no_authors", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[10]);
            param[11] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[11]);
            param[12] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[12]);
            param[13] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[14] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[15] = oSql.NewParameter("@no_figures", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[16] = oSql.NewParameter("@is_extra_copyedit ", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[16]);
            param[17] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aArticle[17]);
            param[18] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[18]);
            param[19] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[19]);
            param[20] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[20]);
            param[21] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[21]);
            param[22] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[22]);
            param[23] = oSql.NewParameter("@cats_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[23]);
            param[24] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[24]);
            param[25] = oSql.NewParameter("@sales_job_group_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[25]);
            param[26] = oSql.NewParameter("@ms_received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[26]);
            param[27] = oSql.NewParameter("@ms_revised_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[27]);
            param[28] = oSql.NewParameter("@ms_accepted_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[28]);
            param[29] = oSql.NewParameter("@interviewdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[29]);
            param[30] = oSql.NewParameter("@phoneno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[30]);
            param[31] = oSql.NewParameter("@faxno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[31]);
            param[32] = oSql.NewParameter("@interviewtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[32]);
            param[33] = oSql.NewParameter("@meetingplace", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[33]);
            param[34] = oSql.NewParameter("@meetingtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[34]);
            param[35] = oSql.NewParameter("@country", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[35]);
            param[36] = oSql.NewParameter("@appointmentdate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[36]);
            param[37] = oSql.NewParameter("@appointmentdate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[37]);
            param[38] = oSql.NewParameter("@zone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[38]);

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
    public string UpdateArticle(string[] aArticle)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[37];
        try
        {
            sSql = "[spUpdateArticle]";
            param[0] = oSql.NewParameter("@article_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[0]);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[1]);
            param[2] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[2]);
            param[3] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[3]);
            param[4] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[4]);
            param[5] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[5]);
            param[6] = oSql.NewParameter("@document_item_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[6]);
            param[7] = oSql.NewParameter("@category_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[7]);
            param[8] = oSql.NewParameter("@doi", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[8]);
            param[9] = oSql.NewParameter("@author", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[9]);
            param[10] = oSql.NewParameter("@author_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[10]);
            param[11] = oSql.NewParameter("@no_authors", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[11]);
            param[12] = oSql.NewParameter("@print_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[12]);
            param[13] = oSql.NewParameter("@ms_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[13]);
            param[14] = oSql.NewParameter("@is_extra_copyedit", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[14]);
            param[15] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aArticle[15]);
            param[16] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[16]);
            param[17] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[17]);
            param[18] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[18]);
            param[19] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[19]);
            param[20] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[20]);
            param[21] = oSql.NewParameter("@cats_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[21]);
            param[22] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[22]);
            param[23] = oSql.NewParameter("@sales_job_group_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aArticle[23]);
            param[24] = oSql.NewParameter("@ms_received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[24]);
            param[25] = oSql.NewParameter("@ms_revised_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[25]);
            param[26] = oSql.NewParameter("@ms_accepted_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[26]);
            param[27] = oSql.NewParameter("@interviewdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[27]);
            param[28] = oSql.NewParameter("@phoneno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[28]);
            param[29] = oSql.NewParameter("@faxno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[29]);
            param[30] = oSql.NewParameter("@interviewtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[30]);
            param[31] = oSql.NewParameter("@meetingplace", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[31]);
            param[32] = oSql.NewParameter("@meetingtime", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[32]);
            param[33] = oSql.NewParameter("@country", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[33]);
            param[34] = oSql.NewParameter("@appointmentdate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[34]);
            param[35] = oSql.NewParameter("@appointmentdate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[35]);
            param[36] = oSql.NewParameter("@zone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aArticle[36]);

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
    public void InsertEmptyGraphics(string sID, string sType, string sGraphicType, int count, string sFigureType)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spInsertBlankGraphics]";
            param[0] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sType);
            param[2] = oSql.NewParameter("@graphic_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sGraphicType);
            param[3] = oSql.NewParameter("@total_count", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, count);
            param[4] = oSql.NewParameter("@figure_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFigureType);
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
     public string UpdateGraphics(System.Collections.ArrayList lstGraphics)
    {
        sSql = "";
        int i = 0;
        object[] oGraphics = new object[lstGraphics.Count];
        try
        {
            foreach (string[] a in lstGraphics)
            {
                sSql = "update job_graphic set graphic_name='" + a[1] + "', graphic_type_id=" + a[2] + ", graphic_desc='" + a[3] + "', figure_type_id='" + a[4] + "', isredraw='" + a[5] + "' where graphic_id=" + a[0];
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
}
