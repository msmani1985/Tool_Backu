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

/// <summary>
/// Summary description for NonLaunch
/// </summary>
public class Non_Launch
{
    Launch_SQL lSql = new Launch_SQL();
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    private string sSql = "";
	public Non_Launch()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQLL"].ConnectionString;
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
    private void addParamsINT(SqlCommand oCmmd, string sName, int sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    public DataSet GetTask()
    {
        return lSql.ExcProcedure("select * from LP_TASK_MASTER", null, CommandType.Text);
    }
    public DataSet getAllCustomers()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "SPGETCUSTOMERS_Launch";
            ds = lSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
        return ds;
    }
    public DataSet getAllLocation()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetLocation";
            ds = lSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally  {  }
        return ds;
    }

    public DataSet GetLocationCust(string id)
    {
        return lSql.ExcProcedure("select * from lp_customers_location l,Lp_location_master m where m.location_id=l.location_id and l.custno='" + id + "' ", null, CommandType.Text);
    }
    public DataSet GetFormats(string name)
    {
        return lSql.ExcProcedure("select * from LP_FORMAT_MASTER where taskname in ('" + name + "')", null, CommandType.Text);
    }
    public DataSet GetEmployee()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGet_AllocatedEmpLaunch";
            ds = lSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
        return ds;
    }
    public DataSet GetJobAllocationEmpByTeam(string Team_id)
    {
        try
        {

            string[,] param = { { "@team_id", Team_id.ToString() } };
            return lSql.ExcProcedure("spGet_AllocatedEmpLaunchByTeam", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetLPPageValues(string FP_id,string Job_his_id,string LP_id,string FileStatus)
    {
        try
        {

            string[,] param = { { "@FP_id", FP_id }, { "@Job_His_id", Job_his_id }, 
                                { "@LP_id", LP_id }, { "@FileStatus", FileStatus } };

            return lSql.ExcProcedure("spGetPageValues", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int MoveTask_emp_process(string strJobId, string strJobFPId, string strEmpID, string strOrder, string strJobHistoryId,
                                    string strPages, string strWorkFlow, string strPagesFrom, string strPagesTo, string FileStatus)
    {
        return lSql.ExcSProcedure("spLp_InsertEventLogs", 
                                    new string[,] { 
                                                        { "@NL_ID", strJobId }, { "@employee_id", strEmpID }, 
                                                        { "@Pages", strPages }, { "@FP_ID", strJobFPId }, 
                                                        { "@Job_Order", strOrder }, { "@Job_His_Id", strJobHistoryId }, 
                                                        { "@WorkFlow", strWorkFlow }, { "@PagesFrom", strPagesFrom },
                                                        { "@PagesTo", strPagesTo },
                                                        { "@FileStatus", FileStatus } }, CommandType.StoredProcedure);
    }
    public DataSet GetJobAllocationEmployee(string Task_id, string FileStatus)
    {
        try
        {
            string[,] param = { { "@Task_ID", Task_id.ToString() }, { "@FileStatus", FileStatus.ToString() } };
            return lSql.ExcProcedure("spGetJobAllocation_Task3", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet getAllSoftware()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetSoftware";
            ds = lSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally  {  }
        return ds;
    }
    public DataSet GetSoftVers(string id)
    {
        return lSql.ExcProcedure("select * from LP_Software_version where soft_id in (" + id + ") ", null, CommandType.Text);
    }
    public DataSet getTimeDetails(string shrs, string sMin, string sID, string sZone)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "TimeZone";
            param[0] = lSql.NewParameter("@hrs", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, shrs);
            param[1] = lSql.NewParameter("@min", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sMin);
            param[2] = lSql.NewParameter("@location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            param[3] = lSql.NewParameter("@time_zone", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sZone);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetTimeZone(int id)
    {
        return lSql.ExcProcedure("select * from LP_TIMEZONE_MASTER where Location_id='" + id + "' ", null, CommandType.Text);
    }
    public DataSet getJobDetailsByID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetNLInfo";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getPathInfo(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetPathInfo";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getDelJobDetailsByID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetDelInfo";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getJobDetailsByLPID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLPInfo";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetJobDetails(int Customerid, int month, int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGetNLAllInfo";
            oCmd.Parameters.Clear();

            addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "JOB");
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
    public DataSet GetJobDetailsLP(int Customerid, int month, int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGetLPAllInfo";
            oCmd.Parameters.Clear();

            addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "JOB");
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
    public DataSet GetLoggedEvents(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLoggedNonLaunch";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public void UpdateSoftStatus(string id)
    {
        lSql.ExcSProcedure("update NL_Soft set status=0 where NL_ID='" + id + "'", null, CommandType.Text);
    }
    public void UpdateSoftStatusLP(string id)
    {
        lSql.ExcSProcedure("update NL_Soft set status=0 where LP_ID='" + id + "'", null, CommandType.Text);
    }
    public void DeleteSoft(string id)
    {
        lSql.ExcSProcedure("delete from NL_Soft where status=0 and NL_ID='" + id + "'", null, CommandType.Text);
    }
    public void DeleteSoftLP(string id)
    {
        lSql.ExcSProcedure("delete from NL_Soft where status=0 and LP_ID='" + id + "'", null, CommandType.Text);
    }
    public bool Insert_Software(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                ocmd.CommandText = "spInsertSoft";
                ocmd.Parameters.Add(new SqlParameter("@NL_ID", ""));
                ocmd.Parameters.Add(new SqlParameter("@Task", ""));
                ocmd.Parameters.Add(new SqlParameter("@Lang", ""));
                ocmd.Parameters.Add(new SqlParameter("@TarDate", ""));
                ocmd.Parameters.Add(new SqlParameter("@Soft", ""));
                ocmd.Parameters.Add(new SqlParameter("@Ver", ""));
                ocmd.Parameters.Add(new SqlParameter("@status", ""));
                foreach (System.Collections.Hashtable ht in al)
                {
                    ocmd.Parameters["@NL_ID"].Value= ht["NL_ID"].ToString();
                    ocmd.Parameters["@Task"].Value= ht["Task_ID"].ToString();
                    ocmd.Parameters["@Lang"].Value = ht["Lang_ID"].ToString();
                    ocmd.Parameters["@TarDate"].Value = ht["TarDate"].ToString();
                    ocmd.Parameters["@Soft"].Value= ht["Software_id"].ToString();
                    ocmd.Parameters["@Ver"].Value= ht["Version_id"].ToString();
                    ocmd.Parameters["@status"].Value= ht["Status"].ToString();
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public bool Insert_SoftwareLP(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                ocmd.CommandText = "spInsertSoftLP";
                ocmd.Parameters.Add(new SqlParameter("@LP_ID", ""));
                ocmd.Parameters.Add(new SqlParameter("@Task", ""));
                ocmd.Parameters.Add(new SqlParameter("@Lang", ""));
                ocmd.Parameters.Add(new SqlParameter("@TarDate", ""));
                ocmd.Parameters.Add(new SqlParameter("@Soft", ""));
                ocmd.Parameters.Add(new SqlParameter("@Ver", ""));
                ocmd.Parameters.Add(new SqlParameter("@status", ""));
                foreach (System.Collections.Hashtable ht in al)
                {
                    ocmd.Parameters["@LP_ID"].Value = ht["LP_ID"].ToString();
                    ocmd.Parameters["@Task"].Value = ht["Task_ID"].ToString();
                    ocmd.Parameters["@Lang"].Value = ht["Lang_ID"].ToString();
                    ocmd.Parameters["@TarDate"].Value = ht["TarDate"].ToString();
                    ocmd.Parameters["@Soft"].Value = ht["Software_id"].ToString();
                    ocmd.Parameters["@Ver"].Value = ht["Version_id"].ToString();
                    ocmd.Parameters["@status"].Value = ht["Status"].ToString();
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public DataSet TaskSelected(string id)
    {
        return lSql.ExcProcedure("select * from NL_Task where NL_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet TaskSelectedLP(string id)
    {
        return lSql.ExcProcedure("select * from NL_Task where LP_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet FormatSelected(string id)
    {
        return lSql.ExcProcedure("select * from NL_Format where NL_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet FormatSelectedLP(string id)
    {
        return lSql.ExcProcedure("select * from NL_Format where LP_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet SoftSelected(string id)
    {
        return lSql.ExcProcedure("select * from NL_Soft where NL_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet SoftSelectedLP(string id)
    {
        return lSql.ExcProcedure("select * from NL_Soft where LP_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet GetTarTaskLP(string id, string task_id, string Soft_ID)
    {
        return lSql.ExcProcedure("spGetTarTaskLP", new string[,] { 
                                    { "@LP_ID", id }, { "@task_id", task_id }, { "@Soft_ID", Soft_ID }}, CommandType.StoredProcedure);
        //return lSql.ExcProcedure("select * from NL_Soft where TarFiles=1 and LP_ID ='" + id + "' and Task_ID=" + task_id, null, CommandType.Text);
    }
    public DataSet SoftSourceLP(string id,string target)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetSoftSource";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, id);
            param[1] = lSql.NewParameter("@target", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, target);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
        //return lSql.ExcProcedure("select * from NL_Soft where TarFiles=0 and LP_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet SoftTargetLP(string id)
    {
        return lSql.ExcProcedure("select * from NL_Soft where TarFiles=1 and LP_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet InputSelected(string id)
    {
        return lSql.ExcProcedure("select * from NL_Input where NL_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet InputSelectedLP(string id)
    {
        return lSql.ExcProcedure("select * from NL_Input where LP_ID ='" + id + "'", null, CommandType.Text);
    }
    public DataSet GetSelectedFormats(string name)
    {
        return lSql.ExcProcedure("select * from LP_FORMAT_MASTER where task_id in (" + name + ")", null, CommandType.Text);
    }
    public DataSet GetSelectedSoft(string id,string Task,string lang)
    {
        return lSql.ExcProcedure("select * from NL_Soft where NL_ID ='" + id + "' and Task_ID='" + Task + "' and Lang_ID='" + lang + "'", null, CommandType.Text);
    }
    public DataSet GetSelectedSoftLP(string id, string Task, string lang)
    {
        return lSql.ExcProcedure("select * from NL_Soft where LP_ID ='" + id + "' and Task_ID='" + Task + "' and Lang_ID='" + lang + "'", null, CommandType.Text);
    }
    public DataSet GetSourceSoftLP(string id, string Task, string lang,string Target)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[4];
        try
        {
            sSql = "spGetSoftSourceSelected";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, id);
            param[1] = lSql.NewParameter("@Target", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Target);
            param[2] = lSql.NewParameter("@Task_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Task);
            param[3] = lSql.NewParameter("@Lang_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, lang);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds;
        //return lSql.ExcProcedure("select * from NL_Soft where TarFiles=0 and LP_ID ='" + id + "' and Task_ID='" + Task + "' and Lang_ID='" + lang + "'", null, CommandType.Text);
    }
    public DataSet GetTargetSoftLP(string id, string Task, string lang)
    {
        return lSql.ExcProcedure("select * from NL_Soft where TarFiles=1 and LP_ID ='" + id + "' and Task_ID='" + Task + "' and Lang_ID='" + lang + "'", null, CommandType.Text);
    }
    public DataSet GetTarFilePages(string id)
    {
        return lSql.ExcProcedure("select * from NL_TarFilePages where NTLS_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet getTarLPInsertedNTLS(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTarLPInsertedNTLS";
            param[0] = lSql.NewParameter("@NTLS_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getTarTaskLangDetailsByLPID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTarTaskLangByLPID";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetTarFPStatus(string FP_ID)
    {
        return lSql.ExcProcedure("select * from NL_TarFilePages where FP_ID=" + FP_ID.ToString(), null, CommandType.Text);
    }
    public DataSet GetSelectedTask(string Task)
    {
        return lSql.ExcProcedure("select Taskname Task,* from LP_Task_Master where Task_ID in (" + Task + ")", null, CommandType.Text);
    }
    public DataSet GetNL_Launch(string Projectname)
    {
        return lSql.ExcProcedure("select * from NL_LAUNCH_DB where Projectname =" + Projectname , null, CommandType.Text);
    }
    public DataSet GetLP_Launch(string Projectname)
    {
        return lSql.ExcProcedure("select * from LP_LAUNCH_DB where Projectname =" + Projectname, null, CommandType.Text);
    }
    public bool Insert_FilePages(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    umodule = "insert into NL_FilePages(NL_ID,Files_ID,Files_Name,Pages) values ('" + ht["NL_ID"].ToString() + "','" + ht["FileNo"].ToString() + "','" + ht["FileName"].ToString() + "','" + ht["Pages"].ToString() + "')";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public void DeleteFilePages(string id)
    {
        lSql.ExcSProcedure("delete from NL_FilePages where NL_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet GetFilePages(string id)
    {
        return lSql.ExcProcedure("select * from NL_FilePages where NTLS_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet GetSelectedFilePages(string id, string Fileid)
    {
        return lSql.ExcProcedure("select * from NL_FilePages where NL_ID='" + id + "' and Files_ID='" + Fileid + "'", null, CommandType.Text);
    }
    public void UpdatePages(string id)
    {
        lSql.ExcSProcedure("update nl_Launch_db set PAGES_COUNT=(select sum(pages) from NL_FilePages where NL_ID='" + id + "')  where NL_ID='" + id + "'", null, CommandType.Text);
    }
    public void UpdateStatus(string id, string Status)
    {
        lSql.ExcSProcedure("update nl_Launch_db set Delivery_Status='" + Status + "'  where NL_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet GetLanguage()
    {
        return lSql.ExcProcedure("spGetLangDet", null, CommandType.StoredProcedure);
    }
    public DataSet GetInsertedLang(string Task_ID, string Lang_ID, string NL_ID)
    {
        return lSql.ExcProcedure("select * from LP_Lang_DB where Lang_ID =" + Lang_ID + " and NL_ID='" + NL_ID + "' and Task_ID=" + Task_ID, null, CommandType.Text);
    }
    public DataSet GetInsertedLangLP(string Task_ID, string Lang_ID, string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_Lang_DB where Lang_ID =" + Lang_ID + " and LP_ID='" + LP_ID + "' and Task_ID=" + Task_ID, null, CommandType.Text);
    }
    public void InsertLang(string Task_ID, string Lang_ID, string NL_ID)
    {
        lSql.ExcSProcedure("Insert into LP_Lang_DB(Lang_ID,Task_ID,NL_ID) values ('" + Lang_ID + "','" + Task_ID + "','" + NL_ID + "')", null, CommandType.Text);
    }
    public void InsertLangLP(string Task_ID, string Lang_ID, string LP_ID)
    {
        lSql.ExcSProcedure("Insert into LP_Lang_DB(Lang_ID,Task_ID,LP_ID) values ('" + Lang_ID + "','" + Task_ID + "','" + LP_ID + "')", null, CommandType.Text);
    }
    public void InsertSoftLP(string Task_ID, string Lang_ID, string LP_ID, string Soft_ID, string Ver_ID, string TarFiles)
    {
        lSql.ExcSProcedure("Insert into NL_Soft(Lang_ID,Task_ID,LP_ID,Soft_ID,Version_ID,TarFiles,Status) values ('" + Lang_ID + "','" + Task_ID + "','" + LP_ID + "','" + Soft_ID + "','" + Ver_ID + "','" + TarFiles + "',1)", null, CommandType.Text);
    }
    public void InsertSoft(string Task_ID, string Lang_ID, string NL_ID, string Soft_ID, string Ver_ID, string TarFiles)
    {
        lSql.ExcSProcedure("Insert into NL_Soft(Lang_ID,Task_ID,NL_ID,Soft_ID,Version_ID,TarFiles,Status) values ('" + Lang_ID + "','" + Task_ID + "','" + NL_ID + "','" + Soft_ID + "','" + Ver_ID + "','" + TarFiles + "',1)", null, CommandType.Text);
    }
    public DataSet GetTarSoftLP(string Task_ID, string Lang_ID, string LP_ID, string Soft_ID, string Ver_ID)
    {
        return lSql.ExcProcedure("select * from NL_Soft Where Lang_ID=" + Lang_ID + " and Task_ID=" + Task_ID + " and LP_ID=" + LP_ID + " and Soft_ID=" + Soft_ID + " and Version_ID=" + Ver_ID, null, CommandType.Text);
    }
    public DataSet GetTarSoft(string Task_ID, string Lang_ID, string NL_ID, string Soft_ID, string Ver_ID)
    {
        return lSql.ExcProcedure("select * from NL_Soft Where Lang_ID=" + Lang_ID + " and Task_ID=" + Task_ID + " and NL_ID=" + NL_ID + " and Soft_ID=" + Soft_ID + " and Version_ID=" + Ver_ID, null, CommandType.Text);
    }
    public void InsertTarSoftLP(string Task_ID, string Lang_ID, string LP_ID, string Soft_ID, string Ver_ID, string TarFiles)
    {
        lSql.ExcSProcedure("Insert into NL_Soft(Lang_ID,Task_ID,LP_ID,Soft_ID,Version_ID,TarFiles,Status) values ('" + Lang_ID + "','" + Task_ID + "','" + LP_ID + "','" + Soft_ID + "','" + Ver_ID + "','" + TarFiles + "',1)", null, CommandType.Text);
        lSql.ExcSProcedure("Insert into NL_Soft(Lang_ID,Task_ID,LP_ID,Soft_ID,Version_ID,TarFiles,Status) values ('" + Lang_ID + "','" + Task_ID + "','" + LP_ID + "','" + Soft_ID + "','" + Ver_ID + "',1,1)", null, CommandType.Text);
    }
    public void InsertTarSoft(string Task_ID, string Lang_ID, string NL_ID, string Soft_ID, string Ver_ID, string TarFiles)
    {
        lSql.ExcSProcedure("Insert into NL_Soft(Lang_ID,Task_ID,NL_ID,Soft_ID,Version_ID,TarFiles,Status) values ('" + Lang_ID + "','" + Task_ID + "','" + NL_ID + "','" + Soft_ID + "','" + Ver_ID + "','" + TarFiles + "',1)", null, CommandType.Text);
        lSql.ExcSProcedure("Insert into NL_Soft(Lang_ID,Task_ID,NL_ID,Soft_ID,Version_ID,TarFiles,Status) values ('" + Lang_ID + "','" + Task_ID + "','" + NL_ID + "','" + Soft_ID + "','" + Ver_ID + "',1,1)", null, CommandType.Text);
    }
    public void DeleteUsedtLang(string Lang_ID, string NL_ID)
    {
        lSql.ExcSProcedure("Delete From LP_Lang_DB where Lang_ID='" + Lang_ID + "' and NL_ID='" + NL_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Delete From NL_FilePages where Lang_ID='" + Lang_ID + "' and NL_ID='" + NL_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Delete From NL_TarFilePages where Lang_ID='" + Lang_ID + "' and NL_ID='" + NL_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Delete From NL_Soft where Lang_ID='" + Lang_ID + "' and NL_ID='" + NL_ID + "'", null, CommandType.Text);
    }
    public void DeleteUsedtLangLP(string Lang_ID, string LP_ID)
    {
        lSql.ExcSProcedure("Delete From LP_Lang_DB where Lang_ID='" + Lang_ID + "' and LP_ID='" + LP_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Delete From NL_FilePages where Lang_ID='" + Lang_ID + "' and LP_ID='" + LP_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Delete From NL_TarFilePages where Lang_ID='" + Lang_ID + "' and LP_ID='" + LP_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Delete From NL_Soft where Lang_ID='" + Lang_ID + "' and LP_ID='" + LP_ID + "'", null, CommandType.Text);
        lSql.ExcSProcedure("sp_UpdateQuote", new string[,] { { "@LP_ID", LP_ID } }, CommandType.StoredProcedure);
    }
    public DataSet GetselectedLang(string NL_ID)
    {
        return lSql.ExcProcedure("select l.lang_id,l.lang_name from LP_LANG_MASTER L,LP_Lang_DB S where s.Lang_ID=L.Lang_id and NL_ID='" + NL_ID + "' group by l.lang_id,l.lang_name", null, CommandType.Text);
    }

    public DataSet GetselectedLangLP(string NL_ID)
    {
        return lSql.ExcProcedure("select l.lang_id,l.lang_name from LP_LANG_MASTER L,LP_Lang_DB S where s.Lang_ID=L.Lang_id and LP_ID='" + NL_ID + "' group by l.lang_id,l.lang_name", null, CommandType.Text);
    }
    public void UpdateUsedtLang(string NL_IDNew,string NL_IDOld)
    {
        lSql.ExcSProcedure("Update LP_Lang_DB set NL_ID='" + NL_IDNew + "' where NL_ID='" + NL_IDOld + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Update NL_Soft set NL_ID='" + NL_IDNew + "' where NL_ID='" + NL_IDOld + "'", null, CommandType.Text);
    }
    public void UpdateUsedtLangLP(string LP_IDNew, string LP_IDOld)
    {
        lSql.ExcSProcedure("Update LP_Lang_DB set LP_ID='" + LP_IDNew + "' where LP_ID='" + LP_IDOld + "'", null, CommandType.Text);
        lSql.ExcSProcedure("Update NL_Soft set LP_ID='" + LP_IDNew + "' where LP_ID='" + LP_IDOld + "'", null, CommandType.Text);
    }
    public DataSet getTaskLangDetails(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTaskLang";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getTaskLangDetailsLP(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTaskLangLP";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getSourceDetailsLP(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetSourceSoftLP";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getTargetDetailsLP(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTargetSoftLP";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getTaskLangDetailsByID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTaskLangByID";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getTaskLangDetailsByLPID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetTaskLangByLPID";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getNTLS(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetNTLS";
            param[0] = lSql.NewParameter("@NTLS_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getLPNTLS(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLP_NTLS";
            param[0] = lSql.NewParameter("@NTLS_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getInsertedNTLS(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetInsertedNTLS";
            param[0] = lSql.NewParameter("@NTLS_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getLPInsertedNTLS(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLPInsertedNTLS";
            param[0] = lSql.NewParameter("@NTLS_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public void UpdateFilePagesStatus(string NL_ID, string NTLS_ID)
    {
        lSql.ExcSProcedure("Update Nl_FilePages set Status=0 where NL_ID='" + NL_ID + "' and NTLS_ID='" + NTLS_ID + "'", null, CommandType.Text);
    }
    public void UpdateFilePagesStatusLP(string NL_ID, string NTLS_ID)
    {
        lSql.ExcSProcedure("spUpdateFilePages", new string[,] { 
                                    { "@LP_ID", NL_ID }, { "@NTLS_ID", NTLS_ID }}, CommandType.StoredProcedure);
    }
    public void DeleteFilePagesStatus(string NL_ID, string NTLS_ID)
    {
        lSql.ExcSProcedure("Delete from Nl_FilePages where Status=0 and NL_ID='" + NL_ID + "' and NTLS_ID='" + NTLS_ID + "'", null, CommandType.Text);
    }
    public void DeleteLPFilePagesStatus(string LP_ID, string NTLS_ID)
    {
        lSql.ExcSProcedure("spDeleteFilePages", new string[,] { 
                                    { "@LP_ID", LP_ID }, { "@NTLS_ID", NTLS_ID }}, CommandType.StoredProcedure);
        //lSql.ExcSProcedure("Delete from Nl_FilePages where Status=0 and LP_ID='" + LP_ID + "' and NTLS_ID='" + NTLS_ID + "'", null, CommandType.Text);
    }
    public void UpdateFile_Count(string NTLS_ID,string NL_ID,string Files)
    {
        lSql.ExcSProcedure("update NL_Soft set Files='" + Files + "' where NL_ID='" + NL_ID + "' and NTLS_ID=" + NTLS_ID, null, CommandType.Text);
        lSql.ExcSProcedure("update NL_LAUNCH_DB set FILE_COUNT=(select sum(isnull(Files,0)) from NL_Soft where NL_ID='" + NL_ID + "') where NL_ID=" + NL_ID , null, CommandType.Text);
    }
    public void UpdateLPFile_Count(string NTLS_ID, string LP_ID, string Files)
    {
        lSql.ExcSProcedure("spUpdateFPFileCount", new string[,] { { "@NTLS_ID", NTLS_ID }, { "@LP_ID", LP_ID }, 
                                                        { "@Files", Files }}, CommandType.StoredProcedure);
        //lSql.ExcSProcedure("update NL_Soft set Files='" + Files + "' where LP_ID='" + LP_ID + "' and NTLS_ID=" + NTLS_ID, null, CommandType.Text);
        //lSql.ExcSProcedure("update LP_LAUNCH_DB set FILE_COUNT=(select sum(isnull(Files,0)) from NL_Soft where LP_ID='" + LP_ID + "') where LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public DataSet getJobTracking(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetJobTrack";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getAllJobTracking()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            //sSql = "spGetJobTrackAll";
            sSql = "spGetMainJobTrack";
            ds = lSql.FillDataSet_SP(sSql, null);
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
    public DataSet getJobTrackingByID(string NL_ID, string FP_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetJobTrackByID";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            param[1] = lSql.NewParameter("@FP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, FP_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetAmends()
    {
        return lSql.ExcProcedure("select * from LP_Amends_Master", null, CommandType.Text);
    }
    public DataSet GetFPStatus(string FP_ID)
    {
        return lSql.ExcProcedure("select * from NL_TarFilePages where FP_ID=" + FP_ID.ToString(), null, CommandType.Text);
    }
    public DataSet getEmpAllocatedJobs(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetAllocatedJob_Emp";
            param[0] = lSql.NewParameter("@EMPID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetEventog(string ENO)
    {
        return lSql.ExcProcedure("select * from LP_LOGGEDEVENTS_DP where ENO=" + ENO, null, CommandType.Text);
    }
   
    public DataSet getCustomerByID(string sLocationID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLocation";
            param[0] = lSql.NewParameter("@Location_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sLocationID);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getAllLocations()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetLocation";
            ds = lSql.FillDataSet_SP(sSql, null);
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

    public string InsertContact(string[] aContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[17];
        try
        {
            sSql = "[spInsertContactLaunch]";
            param[0] = lSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[0]);
            param[1] = lSql.NewParameter("@title", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aContactInfo[1]);
            param[2] = lSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[2]);
            param[3] = lSql.NewParameter("@last_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[3]);
            param[4] = lSql.NewParameter("@sur_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[4]);
            param[5] = lSql.NewParameter("@display_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[5]);
            param[6] = lSql.NewParameter("@address", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[6]);
            param[7] = lSql.NewParameter("@phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[7]);
            param[8] = lSql.NewParameter("@phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[8]);
            param[9] = lSql.NewParameter("@phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[9]);
            param[10] = lSql.NewParameter("@fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[10]);
            param[11] = lSql.NewParameter("@fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[11]);
            param[12] = lSql.NewParameter("@email1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[12]);
            param[13] = lSql.NewParameter("@email2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[13]);
            param[14] = lSql.NewParameter("@email3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[14]);
            param[15] = lSql.NewParameter("@description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[15]);
            param[16] = lSql.NewParameter("@Location_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[16]);

            Status = lSql.Execute_SP(sSql, param,
               lSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string UpdateContact(string[] aContactInfo)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[18];
        try
        {
            sSql = "[spUpdateContactLaunch]";
            param[0] = lSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[0]);
            param[1] = lSql.NewParameter("@title", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, aContactInfo[1]);
            param[2] = lSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[2]);
            param[3] = lSql.NewParameter("@last_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[3]);
            param[4] = lSql.NewParameter("@sur_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[4]);
            param[5] = lSql.NewParameter("@display_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[5]);
            param[6] = lSql.NewParameter("@address", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[6]);
            param[7] = lSql.NewParameter("@phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[7]);
            param[8] = lSql.NewParameter("@phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[8]);
            param[9] = lSql.NewParameter("@phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[9]);
            param[10] = lSql.NewParameter("@fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[10]);
            param[11] = lSql.NewParameter("@fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[11]);
            param[12] = lSql.NewParameter("@email1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[12]);
            param[13] = lSql.NewParameter("@email2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[13]);
            param[14] = lSql.NewParameter("@email3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aContactInfo[14]);
            param[15] = lSql.NewParameter("@description", SqlDbType.Text, int.MaxValue, ParameterDirection.Input, aContactInfo[15]);
            param[16] = lSql.NewParameter("@contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[16]);
            param[17] = lSql.NewParameter("@Location_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aContactInfo[17]);

            Status = lSql.Execute_SP(sSql, param,
               lSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public DataSet getContactsByID(string sContactID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spGetContactLaunch]";
            param[0] = lSql.NewParameter("@contact_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sContactID);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getContactsByName(string sFirstName, string sCustomerID, string sLocationID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "[spGetContactList_Launch]";
            param[0] = lSql.NewParameter("@first_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sFirstName);
            param[1] = lSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[2] = lSql.NewParameter("@location_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sLocationID);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet FontsList(string LP_ID, string Fonts)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spFontList";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LP_ID);
            param[1] = lSql.NewParameter("@Font", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Fonts);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet Chkmissfonts(string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_FONTS_Master where fonts = ('" + fonts + "')", null, CommandType.Text);
    }
    public void insertmissfonts(string fonts)
    {
        lSql.ExcSProcedure("insert into LP_FONTS_Master (fonts) values ('" + fonts + "')", null, CommandType.Text);
    }
    public DataSet Chkusagefonts(string LP_ID, string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_USAGEFONTS where fonts = ('" + fonts + "') and LP_id=" + LP_ID, null, CommandType.Text);
    }
    public void insertusagefonts(string LP_ID, string fonts)
    {
        lSql.ExcSProcedure("insert into LP_USAGEFONTS (LP_id,fonts) VALUES (" + LP_ID +",'"+fonts+"')", null, CommandType.Text);
    }
    public DataSet GetUsageFonts(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_USAGEFONTS where LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public DataSet GetMissFonts()
    {
        return lSql.ExcProcedure("select * from LP_FONTS_Master order by Fonts asc", null, CommandType.Text);
    }
    public void deleteusagefonts(string LP_ID, string fonts)
    {
        lSql.ExcSProcedure("delete from LP_USAGEFONTS where LP_id=" + LP_ID + " and fonts='" + fonts + "'", null, CommandType.Text);
    }
    public DataSet Chkfonts(string LP_ID, string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_TARGETFONTS where fonts = ('" + fonts + "') and LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public void insertfonts(string LP_ID, string fonts)
    {
        lSql.ExcSProcedure("insert into LP_TARGETFONTS (LP_ID,fonts) values (" + LP_ID + ",'" + fonts + "')", null, CommandType.Text);
    }
    public void deletefonts(string LP_ID, string fonts)
    {
        lSql.ExcSProcedure("delete from LP_TARGETFONTS where LP_ID=" + LP_ID + " and fonts='" + fonts + "'", null, CommandType.Text);
    }
    public DataSet GetFonts(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_TARGETFONTS where LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public DataSet Chkmfonts(string LP_ID, string fonts)
    {
        return lSql.ExcProcedure("select * from  LP_MISSINGFONTS where fonts = ('" + fonts + "') and LP_ID=" + LP_ID , null, CommandType.Text);
    }
    public void insertmfonts(string LP_ID, string fonts)
    {
        lSql.ExcSProcedure("insert into LP_MISSINGFONTS (LP_ID,fonts) values (" + LP_ID + ",'" + fonts + "')", null, CommandType.Text);
    }
    public void deletemfonts(string LP_ID, string fonts)
    {
        lSql.ExcSProcedure("delete from LP_MISSINGFONTS where LP_ID=" + LP_ID + " and fonts='" + fonts + "'", null, CommandType.Text);
    }
    public DataSet GetMFonts(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_MISSINGFONTS where LP_ID=" + LP_ID , null, CommandType.Text);
    }
    public void insertQueries(string LP_ID, string queries, string response)
    {
        lSql.ExcSProcedure("insert into LP_LAUNCH_QUERIES (LP_id,queries,response) values(" + LP_ID + ",'" + queries + "','" + response + "')", null, CommandType.Text);
    }
    public DataSet GetQueries(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_LAUNCH_QUERIES where LP_id=" + LP_ID, null, CommandType.Text);
    }
    public DataSet GetComplexReason(string id, string complex)
    {
        sSql = "";
        DataSet dsuf = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetReasons";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, id);
            param[1] = lSql.NewParameter("@Complex", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, complex);
            dsuf = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return dsuf;
    }
    public DataSet getComplexReason(string reason)
    {
        sSql = "";
        DataSet ds12 = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetComplexReason";
            param[0] = lSql.NewParameter("@LP_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, reason);
            ds12 = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        return ds12;
    }
	public DataSet getValues(string sProjectName)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetQuoteValues";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectName);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getValues1(string sProjectName)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetNLQuoteValues";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectName);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public void UpdateTimeTaken(string LP_ID, string task, string time, string soft)
    {
        lSql.ExcSProcedure("update lp_Launch_Quote set time_taken='" + time + "'  where LP_ID=" + LP_ID + " and task_id='" + task + "' and soft_id=" + soft, null, CommandType.Text);
    }
    public void UpdateTimeTakenNl(string LP_ID, string task, string time, string soft)
    {
        lSql.ExcSProcedure("update NL_Launch_Quote set time_taken='" + time + "'  where NL_ID=" + LP_ID + " and task_id='" + task + "' and soft_id=" + soft, null, CommandType.Text);
    }
    public void UpdateHrs(string LP_ID, string task, string hrs, string soft)
    {
        lSql.ExcSProcedure("update lp_Launch_Quote set H_Rate='" + hrs + "'  where LP_ID=" + LP_ID + " and task_id='" + task + "' and soft_id=" + soft, null, CommandType.Text);
    }
    public void UpdatePage(string LP_ID, string task, string Page, string soft)
    {
        lSql.ExcSProcedure("update lp_Launch_Quote set P_Rate='" + Page + "'  where LP_ID=" + LP_ID + " and task_id='" + task + "' and soft_id=" + soft, null, CommandType.Text);
    }
    public DataSet empname(string id)
    {
        return lSql.ExcProcedure("select fname+' '+surname as empname from dbo.LP_LAUNCH_DB l, dbo.employee e where l.CREATED_by=e.employee_id and l.LP_ID='" + id + "'", null, CommandType.Text);
    }
    public DataSet NLempname(string id)
    {
        return lSql.ExcProcedure("select fname+' '+surname as empname from dbo.NL_LAUNCH_DB l, dbo.employee e where l.CREATED_by=e.employee_id and l.NL_ID='" + id + "'", null, CommandType.Text);
    }
    public bool Update_LaunchQuote(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    string t;
                    if (ht["TIME_TAKEN"].ToString() == "")
                        t = "0";
                    else
                        t = ht["TIME_TAKEN"].ToString();
                    umodule = "update lp_launch_quote set TIME_TAKEN='"
                    + t.ToString() + "',LANG_COUNT='" + ht["LANG_COUNT"].ToString() + "',PAGES_COUNT='"
                    + ht["PAGES_COUNT"].ToString()
                    + "',HOUR_RATE='" + ht["HOUR_RATE"].ToString() + "',PAGE_RATE='" + ht["PAGE_RATE"].ToString()
                    + "',PAGEYN='" + ht["PAGEYN"].ToString() + "',HOURYN='" + ht["HOURYN"].ToString()
                    + "',P_RATE='" + ht["P_RATE"].ToString() + "',H_RATE='" + ht["H_RATE"].ToString()
                    + "' where LP_ID='" + ht["LP_ID"].ToString() + "' and  TASK_ID='" + ht["TASK_ID"].ToString() + "' and  Soft_ID='" + ht["Soft_ID"].ToString() + "'";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public bool Update_NL_LaunchQuote(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    string t;
                    if (ht["TIME_TAKEN"].ToString() == "")
                        t = "0";
                    else
                        t = ht["TIME_TAKEN"].ToString();
                    umodule = "update NL_launch_quote set TIME_TAKEN='"
                    + t.ToString() + "',LANG_COUNT='" + ht["LANG_COUNT"].ToString() + "',PAGES_COUNT='"
                    + ht["PAGES_COUNT"].ToString()
                    + "',HOUR_RATE='" + ht["HOUR_RATE"].ToString() + "',PAGE_RATE='" + ht["PAGE_RATE"].ToString()
                    + "',PAGEYN='" + ht["PAGEYN"].ToString() + "',HOURYN='" + ht["HOURYN"].ToString()
                    + "',P_RATE='" + ht["P_RATE"].ToString() + "',H_RATE='" + ht["H_RATE"].ToString()
                    + "' where NL_ID='" + ht["LP_ID"].ToString() + "' and  TASK_ID='" + ht["TASK_ID"].ToString() + "' and  Soft_ID='" + ht["Soft_ID"].ToString() + "'";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }
    public DataSet getJobTrackingLP(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetJobTrackLP";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getJobTrackingByIDLP(string NL_ID, string FP_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetJobTrackByIDLP";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            param[1] = lSql.NewParameter("@FP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, FP_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet DeliveryType(string id)
    {
        return lSql.ExcProcedure("select * from LP_DeliveryType where LP_ID='" + id + "'", null, CommandType.Text);
    }

    public bool Update_ImageLP(System.Collections.ArrayList al)
    {
        string umodule = string.Empty;
        if (al != null && al.Count > 0)
        {
            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al)
                {
                    umodule = "update NL_TarFilePages set Editable='"
                    + ht["Editable"].ToString() + "',Scanned='"
                    + ht["Scanned"].ToString()
                    + "',Non_loc='" + ht["Non_Local"].ToString() + "',Localised='" + ht["Local"].ToString()
                    + "' where FP_ID='" + ht["FP_ID"].ToString() + "'";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public LPQuoteValue LP_QuoteValue(string qry, string[,] param)
    {
        LPQuoteValue LaDS = new LPQuoteValue();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }
    }
    public LPQuoteDesc LP_QuoteDesc(string qry, string[,] param)
    {
        LPQuoteDesc LaDS = new LPQuoteDesc();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }
    }
    public LPForm LP_LaunchForm(string qry, string[,] param)
    {
        LPForm LaDS = new LPForm();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }
    }
    public LPQuery LP_LaunchQuires(string qry, string[,] param)
    {
        LPQuery LaDS = new LPQuery();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }
    }
    public LPQuote LP_LaunchQuote(string qry, string[,] param)
    {
        LPQuote LaDS = new LPQuote();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }
    }
    public LPLang LP_LaunchLang(string qry, string[,] param)
    {
        LPLang LaDS = new LPLang();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (param != null)
            {
                for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
                {
                    if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
                    else
                        proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
                }
            }
            proDA.Fill(LaDS);
            return LaDS;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            proDA.Dispose();
            closecon();
        }
    }
    //Launch OverView
    public DataSet overview(int Customerid, int LocationID, int month, int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "LP_Overview";
            oCmd.Parameters.Clear();

            addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@locationid", LocationID, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "JOB");
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
    public DataSet GetDeliveryStatus(int id)
    {
        return lSql.ExcProcedure("select * from lp_Launch_DB where  LP_id='" + id + "' ", null, CommandType.Text);
    }
    public bool Update_DeliveryStatus(System.Collections.ArrayList al1)
    {
        string umodule = string.Empty;
        if (al1 != null && al1.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                ocmd.CommandText = "sp_UpdateLP_OV_Status";
                ocmd.Parameters.Add(new SqlParameter("@LP_ID", ""));
                ocmd.Parameters.Add(new SqlParameter("@status", ""));
                ocmd.Parameters.Add(new SqlParameter("@EMPID", ""));
                ocmd.Parameters.Add(new SqlParameter("@JobNo", ""));
                foreach (System.Collections.Hashtable ht in al1)
                {
                    ocmd.Parameters["@LP_ID"].Value = ht["ID"].ToString();
                    ocmd.Parameters["@status"].Value = ht["Status"].ToString();
                    ocmd.Parameters["@EMPID"].Value = ht["EMPID"].ToString();
                    ocmd.Parameters["@JobNo"].Value = ht["JobNo"].ToString();
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;

    }

    public bool Update_NLDelStatus(System.Collections.ArrayList al1)
    {
        string umodule = string.Empty;
        if (al1 != null && al1.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.Text;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                foreach (System.Collections.Hashtable ht in al1)
                {
                    umodule = "update NL_Launch_DB set Delivery_Status='"
                    + ht["Status"].ToString() + "' where NL_id='" + ht["ID"].ToString() + "' ";
                    ocmd.CommandText = umodule;
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public DataSet GetLPLoggedEvents(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLoggedLaunch";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetAllocatedEmpPages(string ID, string FP_ID, string Job_His_ID, string FileStatus)
    {
        try
        {

            string[,] param = { { "@FP_id", FP_ID }, { "@Job_His_id", Job_His_ID }, 
                                { "@LP_id", ID }, { "@FileStatus", FileStatus } };

            return lSql.ExcProcedure("spGetAssignedPageValues", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetJobAllocatingEmployee(string ID, string FP_ID, string Job_His_ID)
    {
        try
        {

            string[,] param = { { "@id", ID.ToString() }, 
                                { "@FP_ID", FP_ID.ToString() },
                                { "@joib_His_ID", Job_His_ID.ToString() } };
            return lSql.ExcProcedure("spGetAllocatingJobDetails", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet spGetUploadFilePages(string Task_id,string Lang_ID,string Soft_ID, string LP_ID,string NTLS_ID)
    {
        try
        {
            string[,] param = { 
                                { "@LP_ID",   LP_ID.ToString()   },
                                { "@NTLS_ID", NTLS_ID.ToString() },
                                { "@Task_ID", Task_id.ToString() },
                                { "@Soft_ID", Soft_ID.ToString() },
                                { "@Lang_ID", Lang_ID.ToString() },
                              };
            return lSql.ExcProcedure("spGetUploadFilePages", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void UpdateTarDate(string Date, int YTR, string id)
    {
        lSql.ExcSProcedure("update LP_LAUNCH_DB set TargetDate='" + Date + "', YTRYN=" + YTR + " where LP_ID='" + id + "'", null, CommandType.Text);
        lSql.ExcSProcedure("update LP_JobHistory set Rec_Date='" + Date + "' where LP_ID='" + id + "'", null, CommandType.Text);
    }
    public void UpdateTarFilePagesStatusLP(string NL_ID, string NTLS_ID)
    {
        lSql.ExcSProcedure("spUpdateTarFilePages", new string[,] { 
                                    { "@LP_ID", NL_ID }, { "@NTLS_ID", NTLS_ID }}, CommandType.StoredProcedure);
       // lSql.ExcSProcedure("Update Nl_TarFilePages set Status=0 where LP_ID='" + NL_ID + "' and NTLS_ID='" + NTLS_ID + "'", null, CommandType.Text);
    }
    public void DeleteTarLPFilePagesStatus(string LP_ID, string NTLS_ID)
    {
        lSql.ExcSProcedure("spDeleteTarFilePages", new string[,] { 
                                    { "@LP_ID", LP_ID }, { "@NTLS_ID", NTLS_ID }}, CommandType.StoredProcedure);
        //lSql.ExcSProcedure("Delete from Nl_TarFilePages where Status=0 and LP_ID='" + LP_ID + "' and NTLS_ID='" + NTLS_ID + "'", null, CommandType.Text);
    }
    public void UpdateTarLPFile_Count(string NTLS_ID, string LP_ID, string Files)
    {
        lSql.ExcSProcedure("spUPdateTarTFilePages", new string[,] { 
                                    { "@LP_ID", LP_ID }, { "@NTLS_ID", NTLS_ID }, { "@Files", Files }}, CommandType.StoredProcedure);
        //lSql.ExcSProcedure("update NL_Soft set TFiles='" + Files + "' where LP_ID='" + LP_ID + "' and NTLS_ID=" + NTLS_ID, null, CommandType.Text);
        ////lSql.ExcSProcedure("update LP_LAUNCH_DB set FILE_COUNT=(select sum(isnull(TFiles,0)) from NL_Soft where LP_ID='" + LP_ID + "') where LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public DataSet GetTargetFileDetails(string Task_id, string Lang_ID, string Soft_ID, string LP_ID)
    {
        return lSql.ExcProcedure("spGetTarFilePages", new string[,] { 
                                                        { "@Task_id", Task_id }, { "@Lang_ID", Lang_ID }, 
                                                        { "@Soft_ID", Soft_ID }, { "@LP_ID", LP_ID }}, CommandType.StoredProcedure);
    }
    public DataSet GetLangDetails(string Lang_id)
    {
        return lSql.ExcProcedure("select * from lp_lang_master where  Lang_id=" + Lang_id, null, CommandType.Text);
    }
    public DataSet ComplexReasonNew(string Reason)
    {
        return lSql.ExcProcedure("select * from LP_COMPLEXITY_REASON_NEW where Complex_Level='" + Reason + "'", null, CommandType.Text);
    }
    public DataSet GetSoftVer(string Soft_ID, string Ver_ID)
    {
        return lSql.ExcProcedure("select * from LP_SOFTWARE_VERSION where Soft_ID =" + Soft_ID + " and AutoID=" + Ver_ID, null, CommandType.Text);
    }

    public DataSet GetDeliveryReport(string sDate, string eDate)
    {
        try
        {
            string[,] param = { { "@SDate", sDate.ToString() }, { "@EDate", eDate.ToString() } };
            return lSql.ExcProcedure("spGetDeliveryReport", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetLPLoggedTotalTime(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLogTotalTime";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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

    public DataSet GetLPLogEvt(string JobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLPLoggedEvents";
            param[0] = lSql.NewParameter("@JobID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetNLLogEvt(string JobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetNLLoggedEvents";
            param[0] = lSql.NewParameter("@JobID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetFilewiseAmends(string JobID)
    {
        try
        {

            string[,] param = { { "@JobID", JobID.ToString() } };
            return lSql.ExcProcedure1("spGetFilewiseAmends", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetLPEmpLogEvt(string EmpID, string sdate, string edate)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetLPEmpLogEvt";
            param[0] = lSql.NewParameter("@Empid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, EmpID);
            param[1] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[2] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetNLEmpLogEvt(string EmpID, string sdate, string edate)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetNLEmpLogEvt";
            param[0] = lSql.NewParameter("@Empid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, EmpID);
            param[1] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[2] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            ds = lSql.FillDataSet_SP(sSql, param);
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
	public DataSet GetLPJobLogEvt(string JobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLaunchProjectName";
            param[0] = lSql.NewParameter("@JobID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetNLJobLogEvt(string JobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "LP_spFindProjectEventsNL";
            param[0] = lSql.NewParameter("@JobID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
	public DataSet GetFilewiseAmendsnl(string JobID)
    {
        try
        {

            string[,] param = { { "@JobID", JobID.ToString() } };
            return lSql.ExcProcedure1("spGetFilewiseAmendsNL", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetJobID(string id)
    {
        return lSql.ExcProcedure("select * from Lp_Launch_DB  where JobID='" + id + "' ", null, CommandType.Text);
    }
    public void UpdatePage1(string LP_ID, string task, string Page, string soft)
    {
        lSql.ExcSProcedure("update NL_Launch_Quote set P_Rate='" + Page + "'  where NL_ID=" + LP_ID + " and task_id='" + task + "' and soft_id=" + soft, null, CommandType.Text);
    }
    public void UpdateHrs1(string LP_ID, string task, string hrs, string soft)
    {
        lSql.ExcSProcedure("update NL_Launch_Quote set H_Rate='" + hrs + "'  where NL_ID=" + LP_ID + " and task_id='" + task + "' and soft_id=" + soft, null, CommandType.Text);
    }
    public DataSet GetLPMaxDelAmends(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetMaxDelAmend_dp";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetLPMaxDelAmends1(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetMaxDelAmend_dp1";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public bool Update_DeliveryStatus1(System.Collections.ArrayList al1)
    {
        string umodule = string.Empty;
        if (al1 != null && al1.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                ocmd.CommandText = "sp_UpdateLP_OV_Status1";
                ocmd.Parameters.Add(new SqlParameter("@LP_ID", ""));
                ocmd.Parameters.Add(new SqlParameter("@status", ""));
                ocmd.Parameters.Add(new SqlParameter("@EMPID", ""));
                ocmd.Parameters.Add(new SqlParameter("@Jobno", ""));
                foreach (System.Collections.Hashtable ht in al1)
                {
                    ocmd.Parameters["@LP_ID"].Value = ht["ID"].ToString();
                    ocmd.Parameters["@status"].Value = ht["Status"].ToString();
                    ocmd.Parameters["@EMPID"].Value = ht["EMPID"].ToString();
                    ocmd.Parameters["@Jobno"].Value = ht["Jobno"].ToString();
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public DataSet getFinalQuoteLoad(string LP_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetFinalQuoteLoad";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LP_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getFinalQuoteVal(string LP_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetFinalQuoteValnew";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LP_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public string InsertLP_ProjectIBM(string[] aProject)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[8];
        try
        {
            sSql = "[spInsertLP_ProjectIBM]";
            param[0] = lSql.NewParameter("@PCOMPLETEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[7]);
            param[1] = lSql.NewParameter("@Pcode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[0]);
            param[2] = lSql.NewParameter("@custno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[3] = lSql.NewParameter("@FINSITENO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[2]);
            param[4] = lSql.NewParameter("@CONNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[3]);
            param[5] = lSql.NewParameter("@PONUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[4]);
            param[6] = lSql.NewParameter("@PNOOFPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[5]);
            param[7] = lSql.NewParameter("@PRECEIVEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[6]);
            Status = lSql.Execute_SP(sSql, param,
               lSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public string InsertLP_Project(string[] aProject)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[27];
        try
        {
            sSql = "[spInsertLP_Project]";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[0]);
            param[1] = lSql.NewParameter("@Pcode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[2] = lSql.NewParameter("@PTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[3] = lSql.NewParameter("@custno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[2]);
            param[4] = lSql.NewParameter("@FINSITENO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[3]);
            param[5] = lSql.NewParameter("@CONNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[4]);
            param[6] = lSql.NewParameter("@PONUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[5]);
            param[7] = lSql.NewParameter("@PNOOFPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[6]);
            param[8] = lSql.NewParameter("@PNOOFCHARGEDPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[6]);
            param[9] = lSql.NewParameter("@PRECEIVEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[7]);
            param[10] = lSql.NewParameter("@PDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[8]);
            param[11] = lSql.NewParameter("@PCOMPLETEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[8]);
            param[12] = lSql.NewParameter("@PACOSTDESC1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[9]);
            param[13] = lSql.NewParameter("@PAQTY1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[10]);
            param[14] = lSql.NewParameter("@PACNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[11]);
            param[15] = lSql.NewParameter("@PACOSTDESC2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[12]);
            param[16] = lSql.NewParameter("@PAQTY2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[13]);
            param[17] = lSql.NewParameter("@PACNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[14]);
            param[18] = lSql.NewParameter("@PACOSTDESC3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[15]);
            param[19] = lSql.NewParameter("@PAQTY3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[16]);
            param[20] = lSql.NewParameter("@PACNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[17]);
            param[21] = lSql.NewParameter("@PACOSTDESC4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[18]);
            param[22] = lSql.NewParameter("@PAQTY4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[19]);
            param[23] = lSql.NewParameter("@PACNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[20]);
            param[24] = lSql.NewParameter("@PACOSTDESC5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[21]);
            param[25] = lSql.NewParameter("@PAQTY5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[22]);
            param[26] = lSql.NewParameter("@PACNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[23]);

            Status = lSql.Execute_SP(sSql, param,
               lSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //return ex.Message;
        }
        return Status;
    }
    public DataSet GetProLP(string LP_ID)
    {
        return lSql.ExcProcedure("select Top 1 * from Projects_DP Where LP_ID=" + LP_ID + " order by PROJECTNO desc", null, CommandType.Text);
    }
    public void UpdateDespatchLP(string LP_ID)
    {
        lSql.ExcSProcedure("Update LP_Launch_DB set Despatch='Y' where LP_ID='" + LP_ID + "'", null, CommandType.Text);
    }
    public DataSet GetFinalDeljobs()
    {
        try
        {
            return lSql.ExcProcedure1("spGetFinalDelJobs", null, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void DeleteModules(string moduleno)
    {
        lSql.ExcSProcedure("update pro_modules_dp set obsolete='" + DateTime.Now.ToShortDateString() + "' where MPROJNO in(" + moduleno + ")", null, CommandType.Text);
    }

    public void UpdateDespatchNL(string NL_ID)
    {
        lSql.ExcSProcedure("Update NL_Launch_DB set Despatch='Y' where NL_ID='" + NL_ID + "'", null, CommandType.Text);
    }
    public DataSet GetIBMjobs()
    {
        try
        {
            return lSql.ExcProcedure1("spGetIBMJobs", null, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetIBMRTEjobs()
    {
        try
        {
            return lSql.ExcProcedure1("spGetIBM_RTEJobs", null, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetMailStatusLP(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_MailStatus Where LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public void UpdateMailStatusNL(string NL_ID)
    {
        lSql.ExcSProcedure("Insert into LP_MailStatus  values (null,'" + NL_ID + "',getdate())", null, CommandType.Text);
    }
    public void UpdateMailStatusLP(string LP_ID)
    {
        lSql.ExcSProcedure("Insert into LP_MailStatus  values ('" + LP_ID + "',null,getdate())", null, CommandType.Text);
    }
    public DataSet getIBMQuoteInfo(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetIBMJobsQuote";
            param[0] = lSql.NewParameter("@NL_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public bool Update_DeliveryStatusNL(System.Collections.ArrayList al1)
    {
        string umodule = string.Empty;
        if (al1 != null && al1.Count > 0)
        {

            SqlCommand ocmd = new SqlCommand();
            SqlTransaction Otr = null;
            try
            {
                opencon();
                Otr = oConn.BeginTransaction();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.Connection = oConn;
                ocmd.Transaction = Otr;
                ocmd.CommandText = "sp_UpdateNL_OV_Status";
                ocmd.Parameters.Add(new SqlParameter("@LP_ID", ""));
                ocmd.Parameters.Add(new SqlParameter("@status", ""));
                ocmd.Parameters.Add(new SqlParameter("@EMPID", ""));
                ocmd.Parameters.Add(new SqlParameter("@JobNo", ""));
                foreach (System.Collections.Hashtable ht in al1)
                {
                    ocmd.Parameters["@LP_ID"].Value = ht["ID"].ToString();
                    ocmd.Parameters["@status"].Value = ht["Status"].ToString();
                    ocmd.Parameters["@EMPID"].Value = ht["EMPID"].ToString();
                    ocmd.Parameters["@JobNo"].Value = ht["JobNo"].ToString();
                    ocmd.ExecuteNonQuery();
                }
                Otr.Commit();
            }
            catch (Exception ex)
            { Otr.Rollback(); return false; }
            finally
            {
                if (Otr != null) Otr.Dispose();
                ocmd = null; closecon();
            }
        }
        return true;
    }
    public DataSet GetLPUnDesJobs(string JobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "LP_spFindUndespatchJobsLP";
            param[0] = lSql.NewParameter("@JobID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetNLUnDesJobs(string JobID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "LP_spFindUndespatchJobsNL";
            param[0] = lSql.NewParameter("@JobID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JobID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public void UpdateUnDesLP(string LP_ID, string FP_ID)
    {
        lSql.ExcSProcedure("spDelFinalPackageLP", new string[,] { 
                        { "@LP_ID", LP_ID }, { "@FP_ID", FP_ID }}, CommandType.StoredProcedure);
    }
    public void UpdateUnDesNL(string NL_ID, string FP_ID)
    {
        lSql.ExcSProcedure("spDelFinalPackageNL", new string[,] { 
                        { "@NL_ID", NL_ID }, { "@FP_ID", FP_ID }}, CommandType.StoredProcedure);
    }
    public DataSet GetFQLP(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_Launch_FinalQuote Where obsolete is null and LP_ID=" + LP_ID, null, CommandType.Text);
    }
    private bool GetBoolean(string sProcName, string sDSName, CommandType sCmdType)
    {
        SqlCommand oCmd = new SqlCommand();
        try
        {
            opencon();
            oCmd.CommandType = sCmdType;
            oCmd.CommandText = sProcName;
            oCmd.Connection = oConn;
            oCmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception oex)
        {
            return false;
        }
        finally
        {
            closecon();
            oCmd = null;
        }
    }
    public bool DeleteFQLPModules(string moduleno)
    {
        string dmod = "update LP_Launch_FinalQuote set obsolete='" + DateTime.Now.ToShortDateString() + "' where FQ_ID in(" + moduleno + ")";
        return GetBoolean(dmod, "Module", CommandType.Text);
    }
    public DataSet getFinalQuoteVal1(string LP_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetFinalQuoteVal1";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LP_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetFileLoggedEvents(string NL_ID,string FP_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "spGetFileLoggedEvts";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            param[1] = lSql.NewParameter("@FP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, FP_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetAQLP(string LP_ID)
    {
        return lSql.ExcProcedure("select * from LP_Launch_AddQuote Where obsolete is null and LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public void InsertFU(string LP_ID,string FUDate,string Remarks,string empid)
    {
        lSql.ExcSProcedure("Insert into LP_Launch_FollowUp  values ('" + LP_ID + "','" + FUDate.ToString() + "','"+Remarks+"','"+empid+"',getdate())", null, CommandType.Text);
    }
    public DataSet GetFULP(string LP_ID)
    {
        return lSql.ExcProcedure("select convert(varchar(30),fudate,103) FollowUp_Date,Remarks,ltrim(rtrim(fname))+' '+ltrim(rtrim(surname)) as EmpName from LP_Launch_FollowUp F,Employee E where F.Created_By=E.employee_id and LP_ID=" + LP_ID, null, CommandType.Text);
    }
    public DataSet overview_new(int Customerid, int LocationID, int month, int year, string ProjectName)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "LP_OverviewNew1";
            param[0] = lSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Customerid);
            param[1] = lSql.NewParameter("@locationid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LocationID);
            param[2] = lSql.NewParameter("@month", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, month);
            param[3] = lSql.NewParameter("@year", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, year);
            param[4] = lSql.NewParameter("@ProjectName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, ProjectName);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            //closecon();
        }
        return ds;

        //try
        //{
        //    opencon();
        //    SqlCommand oCmd = new SqlCommand();
        //    oCmd.Connection = oConn;
        //    oCmd.CommandType = CommandType.StoredProcedure;
        //    oCmd.CommandText = "LP_OverviewNew1";
        //    oCmd.Parameters.Clear();

        //    addParamsINT(oCmd, "@custid", Customerid, SqlDbType.Int, ParameterDirection.Input);
        //    addParamsINT(oCmd, "@locationid", LocationID, SqlDbType.Int, ParameterDirection.Input);
        //    addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
        //    addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
        //    addParamsINT(oCmd, "@ProjectName", ProjectName, SqlDbType.VarChar, ParameterDirection.Input);
        //    SqlDataAdapter da = new SqlDataAdapter(oCmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "JOB");
        //    oCmd = null;
        //    return ds;
        //}
        //catch (Exception oEx)
        //{
        //    return null;
        //}
        //finally
        //{
        //    closecon();
        //}
    }
    public DataSet GetLOCostDetails(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGet_LaunchOverViewQuote";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetLPRecJobList(string Custno, string sdate, string edate)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetLPAllInfo_Report";
            param[0] = lSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Custno);
            param[1] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[2] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet getProNameLP(string JOBID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetProName_Lp";
            param[0] = lSql.NewParameter("@JOBID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JOBID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetLP_FB_ByID(string JOBID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLP_FB_ByID";
            param[0] = lSql.NewParameter("@FB_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, JOBID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetLP_FBSummary()
    {
        try
        {
            return lSql.ExcProcedure1("spGetLP_FBSummary", null, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetFBPlatform(string FB_ID)
    {
        return lSql.ExcProcedure("select * from LP_FB_Platform where FB_ID=" + FB_ID, null, CommandType.Text);
    }
    public void UpdateFBApproved(string FB_ID, string empid)
    {
        lSql.ExcSProcedure("Update LP_Feedback set ApprovedDate=Getdate(), ApprovedBy="+empid.ToString()+" where FB_id="+FB_ID.ToString(), null, CommandType.Text);
    }
    public DataSet GetLPEmpLogEvtTeam(string Team, string sdate, string edate)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetLPEmpLogEvtTeam";
            param[0] = lSql.NewParameter("@team", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Team);
            param[1] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[2] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetNLEmpLogEvtTeam(string Team, string sdate, string edate)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetNLEmpLogEvtTeam";
            param[0] = lSql.NewParameter("@team", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Team);
            param[1] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[2] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            ds = lSql.FillDataSet_SP(sSql, param);
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


    public DataSet LP_Summary(int Customerid, int LocationID, string sdate, string edate, string ProjectName)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[5];
        try
        {
            sSql = "LP_SummaryReport";
            param[0] = lSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Customerid);
            param[1] = lSql.NewParameter("@locationid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LocationID);
            param[2] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[3] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            param[4] = lSql.NewParameter("@ProjectName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, ProjectName);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            //closecon();
        }
        return ds;
    }
    public DataSet LP_NonInvReport(int Customerid, int LocationID, string sdate, string edate, string ProjectName, string chkStatus)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[6];
        try
        {
            sSql = "LP_NonInvReport";
            param[0] = lSql.NewParameter("@custid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Customerid);
            param[1] = lSql.NewParameter("@locationid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, LocationID);
            param[2] = lSql.NewParameter("@sdate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sdate);
            param[3] = lSql.NewParameter("@edate", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, edate);
            param[4] = lSql.NewParameter("@ProjectName", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, ProjectName);
            param[5] = lSql.NewParameter("@Status", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, chkStatus);
            ds = lSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            //closecon();
        }
        return ds;
    }
	
    public DataSet GetJobAllocationSplit(string FileStatus)
    {
        try
        {
            string[,] param = { { "@FileStatus", FileStatus.ToString() } };
            return lSql.ExcProcedure("spGet_JobAllocation_Split", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int MoveTask_emp_process1(string strJobId, string strJobFPId, string strEmpID, string strOrder, string strJobHistoryId,
                                    string strPages, string strWorkFlow, string strPagesFrom, string strPagesTo, string FileStatus, string strUA_ID)
    {
        return lSql.ExcSProcedure("spLp_InsertEventLogs1",
                                    new string[,] { 
                                                        { "@NL_ID", strJobId }, { "@employee_id", strEmpID }, 
                                                        { "@Pages", strPages }, { "@FP_ID", strJobFPId }, 
                                                        { "@Job_Order", strOrder }, { "@Job_His_Id", strJobHistoryId }, 
                                                        { "@WorkFlow", strWorkFlow }, { "@PagesFrom", strPagesFrom },
                                                        { "@PagesTo", strPagesTo },{ "@UA_ID", strUA_ID },
                                                        { "@FileStatus", FileStatus } }, CommandType.StoredProcedure);
    }
    public DataSet GetLPPageValues1(string FP_id, string Job_his_id, string UA_ID)
    {
        try
        {

            string[,] param = { { "@FP_id", FP_id }, { "@Job_His_id", Job_his_id }, 
                                { "@UA_id", UA_ID }};

            return lSql.ExcProcedure("spGetPageValues1", param, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataSet GetAllocatedEmpPages1(string ID, string FP_ID, string Job_His_ID, string FileStatus)
    {
        try
        {

            string[,] param = { { "@FP_id", FP_ID }, { "@Job_His_id", Job_His_ID }, 
                                { "@UA_id", ID }, { "@FileStatus", FileStatus } };

            return lSql.ExcProcedure("spGetAssignedPageValues1", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void InsertUnassignJobs(string NL_ID)
    {
        lSql.ExcSProcedure("spInsertUnAssignJob", new string[,] { { "@NL_ID", NL_ID } }, CommandType.StoredProcedure);
    }
	public DataSet getLPInitial()
    {
        sSql = "";
        DataSet ds = new DataSet();
        try
        {
            sSql = "spGetLaunchInitial";
            ds = lSql.FillDataSet_SP(sSql, null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
        return ds;
    }
    public DataSet getLIinitialbyID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetLaunchInitialbyID";
            param[0] = lSql.NewParameter("@LI_ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetFileDelStatus(string UA_ID)
    {
        return lSql.ExcProcedure("select case when t.Pages=(select sum(ua.Pages) from LP_UnAssigned_Jobs ua where FileStatus=6 and t.FP_ID=UA.FP_ID and T.JOB_HIS_ID=ua.Job_His_ID) then 'Y' else 'N' end del from NL_TarFilePages T where t.FP_ID=" + UA_ID, null, CommandType.Text);
    }
    public DataSet GetSessionEmpID(string Emp_ID)
    {
        return lSql.ExcProcedure("select * from MENU_GROUP_ITEM M, EMPLOYEE_MENU_GROUP E where M.menu_group_id=E.menu_group_id and menu_item_id='281' and employee_id=" + Emp_ID, null, CommandType.Text);
    }
    public DataSet GetLaunchInitial()
    {
        return lSql.ExcProcedure("select * from LP_Launch_Initial where Status='N'", null, CommandType.Text);
    }
    public DataSet getPENamebyID(string sID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetPENamebyID";
            param[0] = lSql.NewParameter("@ID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
    public DataSet GetInvSum_LO(string NL_ID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetInvSummary_LO";
            param[0] = lSql.NewParameter("@LP_ID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, NL_ID);
            ds = lSql.FillDataSet_SP(sSql, param);
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
}