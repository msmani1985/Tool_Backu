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


/* Creatin Date: Monday, Nov 23, 2009
 * Created by:  Royson 
 */
public class Costing
{
    private datasourceSQL oSql = new datasourceSQL();
    private string sSql = "";
	public Costing(){}
    public DataSet getInvoicePatternByCustomer(string sCustomerID, string sJobTypeID, string sPatternID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetInvoiceTypePattern]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            param[2] = oSql.NewParameter("@job_cost_pattern_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sPatternID);            
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobCostingSummary(string sStartDate,string sEndDate, string sJobTypeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetJobCosting]";
            param[0] = oSql.NewParameter("@start_time", SqlDbType.SmallDateTime, int.MaxValue, ParameterDirection.Input, sStartDate);
            param[1] = oSql.NewParameter("@end_time", SqlDbType.SmallDateTime, int.MaxValue, ParameterDirection.Input, sEndDate);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobTypeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobCostingByJobName(string sJobName)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetJobCosting]";
            param[0] = oSql.NewParameter("@job_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobName);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getJobCostingByJobEmployee(string sJobName, string sEmployeeID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetJobCosting]";
            param[0] = oSql.NewParameter("@job_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sJobName);
            param[1] = oSql.NewParameter("@employee_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sEmployeeID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public string InsertInvoicePattern(string[] aPattern)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spInsertJobInvoicePattern]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[0]);
            param[1] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.Int, 1, ParameterDirection.Input, aPattern[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[4]);
            param[5] = oSql.NewParameter("@service_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[6]);
            param[7] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[7]);
            param[8] = oSql.NewParameter("@order_index", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[8]);            
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateInvoicePattern(string[] aPattern)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[10];
        try
        {
            sSql = "[spUpdateJobInvoicePattern]";
            param[0] = oSql.NewParameter("@job_cost_pattern_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[0]);
            param[1] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[1]);
            param[2] = oSql.NewParameter("@journal_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[3]);
            param[4] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.Int, 1, ParameterDirection.Input, aPattern[4]);
            param[5] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[5]);
            param[6] = oSql.NewParameter("@service_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[6]);
            param[7] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[7]);
            param[8] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[8]);
            param[9] = oSql.NewParameter("@order_index", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aPattern[9]);            
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string DeleteInvoicePattern(string sInvPatternID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteJobInvoicePattern]";
            param[0] = oSql.NewParameter("@job_cost_pattern_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sInvPatternID);

            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
}
