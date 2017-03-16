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
/// Delphi_Issue Base:
/// Created by: Naresh
/// Creation Date: 07 May 14
/// </summary>
 * */
public class Delphi_Issue
{
    private Delphi_Customer oCust;
    private Delphi_Common oCom;
    private datasourceIBSQL oSql;
    private datasourceSQL oSql1;
    private string sSql = "";
	public Delphi_Issue()
	{
        oCust = new Delphi_Customer();
        oCom = new Delphi_Common();
        oSql = new datasourceIBSQL();
	}
    public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
    public DataSet getIssueStages() { return this.oCom.getStagesByJobType(6); }
    public DataSet getOnHoldTypes() { return this.oCom.getOnHoldTypes(); }
    public DataSet getStageTypes() { return this.oCom.getStageTypes("6"); }
    public DataSet getSalesGroup() { return this.oCom.getSalesJobGroups(); }
    public bool IsInvoiced(string sJobID) { return oCom.IsJobInvoiced(sJobID, "6"); }



    public string UpdatePaginationJobs(System.Collections.ArrayList lstPagination, string sParentJobID)
    {
        string sSql = "";
        string sJob = string.Empty;
        int i = 0;
        object[] oPagn = new object[lstPagination.Count];
        try
        {
            foreach (string[] a in lstPagination)
            {
                sSql = "update article_dp set APAGENOFROM='" + a[1] + "', APAGENOTO='" + a[2] + "', NSNO=" + a[3] + ", AREALNOOFPAGES=" + a[5] + ", ADNO=" + a[6] + ", IPCTNO=" + a[7] + " where INO=" + sParentJobID + " and ANO=" + a[0] + ";";
                oPagn[i] = sSql;
                i++;
            }
            if (i > 0) oSql.Execute_Sql(oPagn);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return "true";
    }




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

 
    public DataSet getIssueStagesByJournal(string sJournalID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGET_NEXT_STAGE]";
            param[0] = oSql.NewParameter("@journalid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "6");
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
    public DataSet getAllIssueStagesByJournal(string sJournalID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGET_NEXT_STAGE];4";
            param[0] = oSql.NewParameter("@journalid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
            param[1] = oSql.NewParameter("@jobtypeid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "6");
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
    public DataSet getIssues(string sIssueName, char cCompleteFlag)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetIssuesList_1]";
            param[0] = oSql.NewParameter("@IssueName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIssueName);
            param[1] = oSql.NewParameter("@Completed_flag", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
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
    public DataSet getIssues(string[] aIssueDetails)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[23];
        try
        {
            sSql = "[spGetIssuesList]";
            param[0] = oSql.NewParameter("@IssueName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[0]);
            param[1] = oSql.NewParameter("@Completed_flag", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aIssueDetails[1]);
            param[2] = oSql.NewParameter("@SearchMode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[2]);
            param[3] = oSql.NewParameter("@SearchFor", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[3]);
            param[4] = oSql.NewParameter("@JourCode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[4]);
            param[5] = oSql.NewParameter("@JourCodeExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[5]);
            param[6] = oSql.NewParameter("@IssueNumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[6]);
            param[7] = oSql.NewParameter("@IssueNumberExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[7]);
            param[8] = oSql.NewParameter("@IssueOnHold", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aIssueDetails[8]);
            param[9] = oSql.NewParameter("@RecExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[9]);
            param[10] = oSql.NewParameter("@RecDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[10]);
            param[11] = oSql.NewParameter("@RecDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[11]);
            param[12] = oSql.NewParameter("@DueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[12]);
            param[13] = oSql.NewParameter("@DueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[13]);
            param[14] = oSql.NewParameter("@DueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[14]);
            param[15] = oSql.NewParameter("@HlfDueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[15]);
            param[16] = oSql.NewParameter("@HlfDueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[16]);
            param[17] = oSql.NewParameter("@HlfDueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[17]);
            param[18] = oSql.NewParameter("@CatsDueExp", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[18]);
            param[19] = oSql.NewParameter("@CatsDueDate1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[19]);
            param[20] = oSql.NewParameter("@CatsDueDate2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[20]);
            param[21] = oSql.NewParameter("@stage_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[21]);
            param[22] = oSql.NewParameter("@customer_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueDetails[22]);
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
    public DataSet getIssueDetailsByID(string sIssueID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetIssue";
            param[0] = oSql.NewParameter("@IssueID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIssueID);
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


    public DataSet getPaginateDetailsByID(string sIssueID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetJobPagination";
            param[0] = oSql.NewParameter("@IssueID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIssueID);
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



    public DataSet getIssueEvents(string sIssueID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetIssueLogEvents]";
            param[0] = oSql.NewParameter("@INO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIssueID);
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
    public DataSet getIssueComments(string sIssueID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetCommentsHistory]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIssueID);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
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
    public DataSet getArticlesAssigned(string sIssueID)
    {
        Pagination oPagi = new Pagination();
        DataSet ds = new DataSet();
        try
        {
            ds = oPagi.getJobPagination(sIssueID);
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
    public DataSet getIssueCostDetailsByID(string sIssueID, string mode)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetIssueCost]";
            param[0] = oSql.NewParameter("@IssueID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIssueID);
            param[1] = oSql.NewParameter("@Mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, mode);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
           // throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
    }
    public DataSet getIssueCostByID(string sJobInvTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetIssueCost]";
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
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "6");
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
    public string InsertIssue(string[] aIssue)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            sSql = "[spInsertIssue]";
            param[0] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[0]);
            param[1] = oSql.NewParameter("@issue_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[1]);
            param[2] = oSql.NewParameter("@issue_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[2]);
            param[3] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[3]);
            param[4] = oSql.NewParameter("@journal_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[4]);
            param[5] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aIssue[5]);
            param[6] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[6]);
            param[7] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[7]);
            param[8] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[8]);
            param[9] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[9]);
            param[10] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[10]);
            param[11] = oSql.NewParameter("@cats_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[11]);
            param[12] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[12]);
            param[13] = oSql.NewParameter("@invoice_description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aIssue[13]);
            param[14] = oSql.NewParameter("@cover_month", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[14]);
            param[15] = oSql.NewParameter("@cover_year", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[15]);
            param[16] = oSql.NewParameter("@sales_job_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[16]);
            param[17] = oSql.NewParameter("@withdraw", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[17]);

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
   
    public string UpdateIssue(string[] aIssue)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[19];
        try
        {
            sSql = "[spUpdateIssue]";
            param[0] = oSql.NewParameter("@issue_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[0]);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[1]);
            param[2] = oSql.NewParameter("@issue_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[2]);
            param[3] = oSql.NewParameter("@issue_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[3]);
            param[4] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[4]);
            param[5] = oSql.NewParameter("@journal_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[5]);
            param[6] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aIssue[6]);
            param[7] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[7]);
            param[8] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[8]);
            param[9] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[9]);
            param[10] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[10]);
            param[11] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[11]);
            param[12] = oSql.NewParameter("@cats_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[12]);
            param[13] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[13]);
            param[14] = oSql.NewParameter("@invoice_description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aIssue[14]);
            param[15] = oSql.NewParameter("@cover_month", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[15]);
            param[16] = oSql.NewParameter("@cover_year", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[16]);
            param[17] = oSql.NewParameter("@sales_job_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssue[17]);
            param[18] = oSql.NewParameter("@withdraw", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aIssue[18]);

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
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "6");

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
    public string InsertIssueCost(string[] aIssueCost)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_no", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[4]);
            param[5] = oSql.NewParameter("@quantity_addnl_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[6]);
            param[7] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[7]);

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
    public string UpdateIssueCost(string[] aIssueCost)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spUpdateJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_no", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[4]);
            param[5] = oSql.NewParameter("@quantity_addnl_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[6]);
            param[7] = oSql.NewParameter("@job_invoice_type_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[7]);
            param[8] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aIssueCost[8]);

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
    public string DeleteIssueCost(string sJobInvTypeItemID)
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
    public bool UpdateIssueCostIndex(string[] aIssueCost)
    {
        object[] oSqlData;
        string sSql = "";
        try
        {
            oSqlData = new object[aIssueCost.Length];
            foreach (string s in aIssueCost)
            {
                sSql = "update job_invoice_type_item set order_index=" + s.Split('|')[1] + " where job_invoice_type_item_id=" + s.Split('|')[0];
                oSqlData[Array.IndexOf(aIssueCost, s)] = sSql;
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
