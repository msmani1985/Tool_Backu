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
/// Pagination :
/// Created by: Royson
/// Creation Date: 16 mar 10
/// </summary>
 * */
public class Pagination
{
    string sSql = "";
    private datasourceSQL oSql;
    private Common oCom;
    public Pagination() { oSql = new datasourceSQL(); oCom = new Common(); }
    public DataSet getNumberSytems() { return oCom.getNumberSystem(); }
    public DataSet getJobPagination(string sParentJobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobPagination]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
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
    public DataSet getJobsUnassigned(string sJournalID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobsUnassigned]";
            param[0] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
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
    public DataSet getJobsAssigned(string sJournalID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobsAssigned]";
            param[0] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJournalID);
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
    public string InsertPaginationJobs(string sParentJobID, string sJobIDs, string sMode, string sEmployeeID, string sNewindex, string sDuplicateJobID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            sSql = "[spInsertPaginationJobs]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[1] = oSql.NewParameter("@job_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobIDs);
            param[2] = oSql.NewParameter("@mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sMode);
            param[3] = oSql.NewParameter("@employee_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            param[4] = oSql.NewParameter("@newindex", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sNewindex);
            param[5] = oSql.NewParameter("@duplicate_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDuplicateJobID);

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
    public string InsertPaginationJobs(string sParentJobID, string sJobIDs, string sMode, string sEmployeeID,
        string sNewindex, string sDuplicateJobID, string sNonArticleType, string sNonArticleCount)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertPaginationJobs]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[1] = oSql.NewParameter("@job_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobIDs);
            param[2] = oSql.NewParameter("@mode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sMode);
            param[3] = oSql.NewParameter("@employee_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            param[4] = oSql.NewParameter("@newindex", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sNewindex);
            param[5] = oSql.NewParameter("@duplicate_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sDuplicateJobID);
            param[6] = oSql.NewParameter("@nonarticle_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sNonArticleType);
            param[7] = oSql.NewParameter("@nonarticle_count", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sNonArticleCount);

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
    public string UpdatePaginationJobs(System.Collections.ArrayList lstPagination, string sParentJobID)
    {
        sSql = "";
        string sJob = string.Empty;
        int i = 0;
        object[] oPagn = new object[lstPagination.Count];
        try
        {
            foreach (string[] a in lstPagination){
               // sSql = "update job_pagination set page_from='" + a[1] + "', page_to='" + a[2] + "', number_system_id=" + a[3] + ", sequence=" + a[4] + " where parent_job_id=" + sParentJobID + " and job_id=" + a[0] +";";
                sSql = "update job_pagination set page_from='" + a[1] + "', page_to='" + a[2] + "', number_system_id=" + a[3] + ", sequence=" + a[4] + " where INO=" + sParentJobID + " and ANO=" + a[0] + ";";
                //sSql += "update job set print_pages = " + a[5] + ",document_type_id=" + a[6] + ",document_item_type_id=" + a[7] + " where job_id=" + a[0] + ";";
                sSql += "update ARTICLE_DP set AREALNOOFPAGES = " + a[5] + ",document_type_id=" + a[6] + ",document_item_type_id=" + a[7] + " where ANO=" + a[0] + ";";
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
    public string DeleteJobPagination(string sParentJobID, string sJobIDs)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spDeleteJobPagination]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sParentJobID);
            param[1] = oSql.NewParameter("@job_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobIDs);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public DataSet get_jobpagination_doctype(string paginationqry ,string[,] paginationparam,CommandType cmdtype)
    {
        datasourceSQL pageobj = new datasourceSQL();
        DataSet pageds = new DataSet();
        pageds = pageobj.ExcProcedure(paginationqry, paginationparam, cmdtype);
        return pageds;
    }
}
