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
/// Project Base:
/// Created by: selva
/// Creation Date: 22 Mar 2010
/// </summary>
 * */
public class Project
{
    private CustomerBase oCust;
    private Common oCom;
    private datasourceSQL oSql;
    private string sSql = "";
    public Project()
    {
        oCust = new CustomerBase();
        oCom = new Common();
        oSql = new datasourceSQL();
    }
    public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
    public DataSet getSalesGroup() { return this.oCom.getSalesJobGroups(); }
    public bool IsInvoiced(string sJobID) { return oCom.IsJobInvoiced(sJobID, "4"); }
    public DataSet getOnHoldTypes() { return this.oCom.getOnHoldTypes(); }
    public DataSet getProjects(string sProjectName, string custid, char cCompleteFlag)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetProjectList";
           //@ProjectName, @custid, @completed_flag...
            param[0] = oSql.NewParameter("@ProjectName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectName);
            param[1] = oSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input,custid);
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
    public DataSet getFormatOrStyles() { return this.oCom.getTypesettingPlatformList(); }
    public DataSet getServiceTypes() { return this.oCom.getServiceTypes(); }
    public DataSet getProjectCostByID(string sJobInvTypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
           // sSql = "[spGetBookCost]";
            sSql = "[spGetProjectCost]";
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
            param[1] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "4");
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
    public DataSet getProjectCostDetailsByID(string sProjectId, string mode)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetProjectCost]";
            param[0] = oSql.NewParameter("@ProjectId", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectId);
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
    public string  InsertProject(string[] aProject)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[25];
        try
        {
            sSql = "[spInsertProject]";
            param[0] = oSql.NewParameter("@jobtype_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[0]);
            param[1] = oSql.NewParameter("@Project_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[2] = oSql.NewParameter("@Project_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[2]);
            param[3] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[3]);
            param[4] = oSql.NewParameter("@size", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[4]);
            param[5] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aProject[5]);
            param[6] = oSql.NewParameter("@isbn_print", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aProject[6]);
            param[7] = oSql.NewParameter("@isbn_online", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aProject[7]);
            param[8] = oSql.NewParameter("@service_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[8]);
            param[9] = oSql.NewParameter("@typesetting_platform_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[9]);
            param[10] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[10]);
            param[11] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[11]);
            param[12] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[12]);
            param[13] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[13]);
            param[14] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[14]);
            param[15] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[15]);
            param[16] = oSql.NewParameter("@contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[16]);
            param[17] = oSql.NewParameter("@invoice_description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aProject[17]);
            param[18] = oSql.NewParameter("@ponumber", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aProject[18]);
            param[19] = oSql.NewParameter("@finsite_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[19]);
            param[20] = oSql.NewParameter("@ProjectNumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[20]);
            param[21] = oSql.NewParameter("@sales_job_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[21]);
            param[22] = oSql.NewParameter("@author_BTW", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[22]);
            param[23] = oSql.NewParameter("@job_location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[23]);
            param[24] = oSql.NewParameter("@print_pages", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[24]); 
            

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
    public DataSet getProjectDetailsByID(string sProjectId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetProject";
            param[0] = oSql.NewParameter("@ProjectID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectId);
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
    public DataSet getProjectComments(string sProjectId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetCommentsHistory]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectId);
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
    public DataSet getProjectEvents(string sProjectId)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetLoggedEvents]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectId);
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
    public string  DeleteProjectCost(string sJobInvTypeItemID)
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
    public string  InsertInvoiceTypeItem(string sInvoiceTypeID, string sInvoiceTypeItemName)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spInsertInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@InvoiceType_item_Name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sInvoiceTypeItemName);
            param[1] = oSql.NewParameter("@Invoice_Type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sInvoiceTypeID);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, "4");

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
    public string  UpdateProject(string[] aProject)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[26];
        try
        {
            sSql = "[spUpdateProjectDetail]";
            param[0] = oSql.NewParameter("@jobtype_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[0]);
            param[1] = oSql.NewParameter("@project_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[2] = oSql.NewParameter("@project_title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[2]);
            param[3] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[3]);
            param[4] = oSql.NewParameter("@size", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[4]);
            param[5] = oSql.NewParameter("@comments", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aProject[5]);
            param[6] = oSql.NewParameter("@isbn_print", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aProject[6]);
            param[7] = oSql.NewParameter("@isbn_online", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aProject[7]);
            param[8] = oSql.NewParameter("@service_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[8]);
            param[9] = oSql.NewParameter("@typesetting_platform_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[9]);
            param[10] = oSql.NewParameter("@project_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[10]);
            param[11] = oSql.NewParameter("@job_stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[11]);
            param[12] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[12]);
            param[13] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[13]);
            param[14] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[14]);
            param[15] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[15]);
            param[16] = oSql.NewParameter("@created_by", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[16]);
            param[17] = oSql.NewParameter("@contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aProject[17]);
            param[18] = oSql.NewParameter("@invoice_description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aProject[18]);
            param[19] = oSql.NewParameter("@PONumber", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aProject[19]);
            param[20] = oSql.NewParameter("@finsite_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[20]);
            param[21] = oSql.NewParameter("@ProjectNumber", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[21]);
            param[22] = oSql.NewParameter("@sales_job_group_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[22]);
            param[23] = oSql.NewParameter("@author_BTW", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[23]);
            param[24] = oSql.NewParameter("@job_location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[24]);
            param[25] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[25]); 

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
    public string  UpdateProjectCost(string[] aProjectCost)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[9];
        try
        {
            sSql = "[spUpdateJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_no", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[4]);
            param[5] = oSql.NewParameter("@quantity_addnl_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[6]);
            param[7] = oSql.NewParameter("@job_invoice_type_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[7]);
            param[8] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[8]);

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
    public bool    UpdateProjectCostIndex(string[] aProjectCost)
    {
        object[] oSqlData;
        string sSql = "";
        try
        {
            oSqlData = new object[aProjectCost.Length];
            foreach (string s in aProjectCost)
            {
                sSql = "update job_invoice_type_item set order_index=" + s.Split('|')[1] + " where job_invoice_type_item_id=" + s.Split('|')[0];
                oSqlData[Array.IndexOf(aProjectCost, s)] = sSql;
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
    public string  InsertProjectCost(string[] aProjectCost)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertJobInvoiceTypeItem]";
            param[0] = oSql.NewParameter("@invoice_no", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, DBNull.Value);
            param[1] = oSql.NewParameter("@job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[1]);
            param[2] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[2]);
            param[3] = oSql.NewParameter("@invoicetype_item_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[3]);
            param[4] = oSql.NewParameter("@cost_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[4]);
            param[5] = oSql.NewParameter("@quantity_addnl_type", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[5]);
            param[6] = oSql.NewParameter("@price_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[6]);
            param[7] = oSql.NewParameter("@description", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProjectCost[7]);

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
    public string InsertEmptyModules(string sID, string sChapPrefix, string sDocType, int count,
       string sStartDate, string sDueDate, string sHlfDueDate, string sCreatedBy)
    {
        sSql = "";
        string status = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertBlankModules]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sChapPrefix);
            param[2] = oSql.NewParameter("@document_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, 2);
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
    public DataSet getModuleNumbers(string sProjectID, string sDoctypeID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetChapterNumbers]";
            param[0] = oSql.NewParameter("@parent_job_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectID);
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
    public DataSet getModuleDetailsByID(string sProjectID, string sModuleID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetProjectModule]";
            param[0] = oSql.NewParameter("@ProjectID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectID);
            param[1] = oSql.NewParameter("@ModuleID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sModuleID);
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
    public DataSet getCusomerFinsite(string sCustID) { return this.oCust.getFinSiteByCustomer(sCustID); }

    public string InsertModule(string[] aModule)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            sSql = "[spInsertModule]";
            param[0] = oSql.NewParameter("@Project_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aModule[0]);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[1]);
            param[2] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[3]);
            param[4] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aModule[4]);
            param[5] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[5]);
            param[6] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[6]);
            param[7] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[7]);
            param[8] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[8]);
            param[9] = oSql.NewParameter("@no_figures", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[9]);
            param[10] = oSql.NewParameter("@comments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[10]);
            param[11] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[11]);
            param[12] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[12]);
            param[13] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[13]);
            param[14] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[14]);
            param[15] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[15]);
            param[16] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[16]);

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
    public string UpdateModule(string[] aModule)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            sSql = "[spUpdateProjectModule]";
            param[0] = oSql.NewParameter("@module_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aModule[0]);
            param[1] = oSql.NewParameter("@name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[1]);
            param[2] = oSql.NewParameter("@title", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[2]);
            param[3] = oSql.NewParameter("@job_type_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[3]);
            param[4] = oSql.NewParameter("@document_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aModule[4]);
            param[5] = oSql.NewParameter("@print_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[5]);
            param[6] = oSql.NewParameter("@ms_pages", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[6]);
            param[7] = oSql.NewParameter("@no_tables", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[7]);
            param[8] = oSql.NewParameter("@no_equations", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[8]);
            param[9] = oSql.NewParameter("@no_figures", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[9]);
            param[10] = oSql.NewParameter("@comments", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[10]);
            param[11] = oSql.NewParameter("@job_stage_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[11]);
            param[12] = oSql.NewParameter("@received_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[12]);
            param[13] = oSql.NewParameter("@due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[13]);
            param[14] = oSql.NewParameter("@half_due_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[14]);
            param[15] = oSql.NewParameter("@despatch_date", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[15]);
            param[16] = oSql.NewParameter("@created_by", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aModule[16]);

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
    public string GetStage(int stageId)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[SPGETPROJECTSTAGE]";
            param[0] = oSql.NewParameter("@stage_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, stageId);
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
    

}
