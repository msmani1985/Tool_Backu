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
/// Employee Comp-off Base:
/// Created by: Royson
/// Creation Date: 14jul09
/// </summary>
 * */
public class EmployeeCompoff
{
    private datasourceSQL oSql;
    private string sSql = "";
    public EmployeeCompoff() { oSql = new datasourceSQL(); }
    public DataSet getCompoffSummary(string sEmpID, string sCreatedBy)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetCompoffHistory]";
            param[0] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCreatedBy);
            param[1] = oSql.NewParameter("@employee_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmpID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return ds;
    }
    public DataSet getCompoffSummary(string sEmpNumber, string sStartdate, string sEnddate, string sStatus)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spGetCompoffHistory]";
            param[0] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "");
            param[1] = oSql.NewParameter("@employee_number", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmpNumber);
            param[2] = oSql.NewParameter("@startdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStartdate);
            param[3] = oSql.NewParameter("@enddate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEnddate);
            param[4] = oSql.NewParameter("@status", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sStatus);            
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return ds;
    }
    public string InsertCompoff(string[] aCompoff)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "[spInsertCompoff]";
            param[0] = oSql.NewParameter("@employee_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCompoff[0]);
            param[1] = oSql.NewParameter("@compoff_date", SqlDbType.DateTime, int.MaxValue, ParameterDirection.Input, aCompoff[1]);
            param[2] = oSql.NewParameter("@compoff_hours", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCompoff[2]);
            param[3] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCompoff[3]);
            param[4] = oSql.NewParameter("@comments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCompoff[4]);

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
    public string ApproveCompoff(string sCompoffIDs, string sApprovedBy)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spUpdateCompoff]";
            param[0] = oSql.NewParameter("@compoff_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCompoffIDs);
            param[1] = oSql.NewParameter("@approved_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sApprovedBy);

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
    public string RejectCompoff(string sCompoffIDs, string sRejectedBy)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spUpdateCompoff]";
            param[0] = oSql.NewParameter("@compoff_ids", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCompoffIDs);
            param[1] = oSql.NewParameter("@rejected_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sRejectedBy);

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
    public string DeleteCompoff(string sCompoffID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteCompoff]";
            param[0] = oSql.NewParameter("@compoff_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCompoffID);
            
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
}
