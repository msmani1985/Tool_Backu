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
/// Summary description for datasourceSQL
/// </summary>
public class datasourceSQL
{
    SqlConnection oConnVote;
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
	public datasourceSQL()
	{

	}

    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
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

    private void addParamsSTR(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, int sLen, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType, sLen));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }

    
   
   
    public DataSet GetMenuHeadings()
    {
        return GetDataSet("spGet_MenuHeadings", "menuheading", CommandType.StoredProcedure);
    }

    public DataSet GetMenuItems()
    {
        return GetDataSet("spGet_MenuItems", "MENUITEMS", CommandType.StoredProcedure);
    }

    public DataSet GetMenuGroups()
    {
        return GetDataSet("spGet_MenuGroups", "MENUGROUPS", CommandType.StoredProcedure);
    }
    public DataSet GetEmployeeNo(int locationid )
    {
        return GetDataSet("select max(employee_number) as empno from employee where location_id="+ locationid.ToString() +" and employee_number not in(10001,10002,10003,10004,10005,10006)", "Employee", CommandType.Text);
    }

    public DataSet GetTaskItem(string[,] param )
    {
        return ExcProcedure("spGet_TaskItem;2",param,CommandType.StoredProcedure);
    }
    
    private DataSet GetDataSet(string sProcedure, string sTitle, CommandType sCmdType)
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

    public string GetHTMLString(string sProcedure, string sTitle, CommandType sCmdType)
    {
        string sReturn = "";
        XmlReader oXmlReader;
        StringBuilder xmlData = new StringBuilder();

        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandText = sProcedure; //procedure name
            oCmd.CommandType = sCmdType;
            oXmlReader = oCmd.ExecuteXmlReader();
            while (!oXmlReader.EOF)
            {
                if (oXmlReader.IsStartElement())
                    xmlData.Append(oXmlReader.ReadOuterXml());
            }
            oXmlReader.Close();
            oXmlReader = null;
            sReturn = "<DATA>" + xmlData.ToString() + "</DATA>";
        }
        catch (Exception oex)
        {
            sReturn = oex.Message;
        }
        finally
        {
            xmlData = null;
            closecon();
        }
        return sReturn;
    }

    public string GetUserMenuItems(int employee_id)
    {
        string sReturn = "";
        XmlReader oXmlReader;
        StringBuilder xmlData = new StringBuilder();

        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandText = "SPGET_USER_MENU_ITEMS"; //procedure name
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@employee_id", employee_id, SqlDbType.Int, ParameterDirection.Input);
            oXmlReader = oCmd.ExecuteXmlReader();
            while (!oXmlReader.EOF)
            {
                if (oXmlReader.IsStartElement())
                    xmlData.Append(oXmlReader.ReadOuterXml());
            }
            oXmlReader.Close();
            oXmlReader = null;
            sReturn = "<DATA>" + xmlData.ToString() + "</DATA>";
        }
        catch (Exception oex)
        {
            sReturn = oex.Message;
        }
        finally
        {
            xmlData = null;
            closecon();
        }
        return sReturn;
    }

    public DataSet validatelogin(String uname, String pwd,String ipaddress)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;

            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spVerifyUser";
            oCmd.Parameters.Clear();

            addParamsSTR(oCmd, "@User", uname, SqlDbType.VarChar, 25, ParameterDirection.Input);
            addParamsSTR(oCmd, "@Pwd", pwd, SqlDbType.VarChar, 25, ParameterDirection.Input);
            addParamsSTR(oCmd, "@ipaddress", ipaddress, SqlDbType.VarChar, 25, ParameterDirection.Input);

            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "USERINFO");
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

    public DataSet GetLiveEmployees()
    {
        return GetDataSet("spGet_LiveEmployees", "LIVEEMP", CommandType.StoredProcedure);
    }

    public DataSet GetTeamLeads()
    {
        return GetDataSet("spGet_TeamLeads", "TEAMLEADS", CommandType.StoredProcedure);
    }

    public DataSet GetResignedEmployees()
    {
        return GetDataSet("spGet_LostEmployees", "LOSTEMP", CommandType.StoredProcedure);
    }

    public DataSet GetDesignation()
    {
        return GetDataSet("SELECT * FROM DESIGNATION WHERE OBSOLETE IS NULL ORDER BY DESIGNATION_NAME", "DESIGNATION", CommandType.Text );
    }
    public DataSet GetDepartment()
    {
        return GetDataSet("SELECT * FROM DEPARTMENT WHERE OBSOLETE IS NULL ORDER BY DEPARTMENT_NAME", "DEPARTMENT", CommandType.Text);
    }
    public DataSet GetLocation()
    {
        return GetDataSet("SELECT * FROM EMPLOYEE_LOCATION WHERE OBSOLETE IS NULL ORDER BY LOCATION_NAME", "LOCATION", CommandType.Text);
    }
    public DataSet GetEmpTeam()
    {
        return GetDataSet("SELECT * FROM EMPLOYEE_TEAM WHERE OBSOLETE IS NULL ORDER BY EMPLOYEE_TEAM_NAME", "TEAM", CommandType.Text);
    }
    
    public DataSet GetEmployeeDetail(int employee_id)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure  ;
            oCmd.CommandText = "spGet_EmployeeDetails";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@employee_no", employee_id, SqlDbType.Int, ParameterDirection.Input);  
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public bool changepass(int employee_id, string username, string password)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spChangePassword";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@employee_id", employee_id, SqlDbType.Int, ParameterDirection.Input);
            addParamsSTR(oCmd, "@username", username, SqlDbType.VarChar, 50,ParameterDirection.Input);
            addParamsSTR(oCmd, "@password", password, SqlDbType.VarChar, 30, ParameterDirection.Input);
            oCmd.ExecuteNonQuery();
            oCmd = null;
            return true;
        }
        catch (Exception oEx)
        {
            return false;
        }
        finally
        {
            closecon();
        }
    }


    private void addParamsINT(SqlCommand oCmmd, string sName, int sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    private void addParamsDATE(SqlCommand oCmmd, string sName, DateTime sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    private void addParamsDecimal(SqlCommand oCmmd, string sName, decimal sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    
    private void addOutPutParams(SqlCommand oCmmd, string sName,  SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        //oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    //<!-- royson 23jan09
    public SqlParameter NewParameter(string parameterId, SqlDbType sqlType, int parameterSize, ParameterDirection parameterDirection, object parameterValue)
    {
        if (parameterId == null)
            throw (new ArgumentNullException("parameterId"));
        if (parameterId.Length == 0)
            throw (new ArgumentOutOfRangeException("parameterId"));

        SqlParameter newSqlParam = new SqlParameter();
        newSqlParam.ParameterName = parameterId;
        newSqlParam.SqlDbType = sqlType;
        newSqlParam.Direction = parameterDirection;

        if (parameterSize > 0)
            newSqlParam.Size = parameterSize;

        if (parameterValue != null)
            newSqlParam.Value = parameterValue;

        return newSqlParam;
    }

    public DataSet FillDataSet_SP(string sSPName, SqlParameter[] sqlParamColl)
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataAdapter adap = null;
        DataSet ds = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            ds = new DataSet();
            cmd = new SqlCommand(sSPName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParamColl != null && sqlParamColl.Length > 0)
            {
                IEnumerator Paraenum1 = sqlParamColl.GetEnumerator();
                while (Paraenum1.MoveNext())
                {
                    cmd.Parameters.Add(Paraenum1.Current);
                }
            }
            adap = new SqlDataAdapter(cmd);
            adap.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
        }
    }

    public bool Execute_SP(string sSPName, SqlParameter[] sqlParamColl)
    {
        int cnt = 0;
        bool blnCheck = false;
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            trans = cnn.BeginTransaction();
            cmd = new SqlCommand(sSPName, cnn, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParamColl != null && sqlParamColl.Length > 0)
            {
                IEnumerator Paraenum1 = sqlParamColl.GetEnumerator();
                while (Paraenum1.MoveNext())
                {
                    cmd.Parameters.Add(Paraenum1.Current);
                }
            }
            cnt = cmd.ExecuteNonQuery();
            trans.Commit();
            if (cnt > 0)
                blnCheck = true;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw new Exception(ex.ToString());
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
        }
        return blnCheck;
    }

    public string Execute_SP(string sSPName, SqlParameter[] sqlParamColl, SqlParameter sqlParamOutput)
    {
        int cnt = 0;
        string sOutput = "";
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            trans = cnn.BeginTransaction();
            cmd = new SqlCommand(sSPName, cnn, trans);
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParamColl != null && sqlParamColl.Length > 0)
            {
                IEnumerator Paraenum1 = sqlParamColl.GetEnumerator();
                while (Paraenum1.MoveNext())
                {
                    cmd.Parameters.Add(Paraenum1.Current);
                }
            }
            cmd.Parameters.Add(sqlParamOutput);
            cmd.ExecuteNonQuery();
            trans.Commit();
            sOutput = sqlParamOutput.Value.ToString();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw new Exception(ex.ToString()); 
        }
        finally
        {
            if (cnn != null && cnn.State == ConnectionState.Open) cnn.Close();
        }
        return sOutput;
    }
    public bool Execute_Sql(object[] oSaveitems)
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        bool blnCheck = false;
        string strSql = "";
        int cnt = 0;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString;
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
    //-->

    //Create by SUBBU 
    
    

    public int ExcuteProcedure(string sProcName, string paramcollection,string paramnames,string paramtypes,string paramdirections, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;
        string[] ParamType;
        string[] ParamDir;
        bool flg=false;
        string OutparamName="";
        ParamVal = paramcollection.Split(separator);
        ParamName = paramnames.Split(separator);
        ParamType = paramtypes.Split(separator);
        ParamDir = paramdirections.Split(separator);
        
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.Parameters.Clear();
            int i;
            for (i = 0; i < ParamName.GetLength(0); i++)
            {
                
                if(ParamType[i].ToString().ToUpper()=="INT" && ParamDir[i].ToString().ToUpper()=="INPUT")
                    addParamsINT(ocmd, ParamName[i].ToString(), Convert.ToInt32(ParamVal[i]), SqlDbType.Int, ParameterDirection.Input);
                if (ParamType[i].ToString().ToUpper() == "INT" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                {
                    addOutPutParams(ocmd, ParamName[i].ToString(), SqlDbType.Int, ParameterDirection.Output);
                    flg = true;
                    OutparamName = ParamName[i].ToString();
                }
                if (ParamType[i].ToString().ToUpper() == "VARCHAR" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsSTR(ocmd, ParamName[i].ToString(), ParamVal[i].ToString(), SqlDbType.VarChar, 600, ParameterDirection.Input);
                if (ParamType[i].ToString().ToUpper() == "VARCHAR" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addOutPutParams(ocmd, ParamName[i].ToString(), SqlDbType.VarChar, ParameterDirection.Output);
                if (ParamType[i].ToString().ToUpper() == "DATE" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsDATE(ocmd, ParamName[i].ToString(), Convert.ToDateTime(ParamVal[i]), SqlDbType.DateTime, ParameterDirection.Input);
                if (ParamType[i].ToString().ToUpper() == "DATE" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addParamsDATE(ocmd, ParamName[i].ToString(), Convert.ToDateTime(ParamVal[i]), SqlDbType.DateTime, ParameterDirection.Output);
                if (ParamType[i].ToString().ToUpper() == "DECIMAL" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addParamsDecimal(ocmd, ParamName[i].ToString(), Convert.ToDecimal(ParamVal[i]), SqlDbType.Decimal, ParameterDirection.Output);
                if (ParamType[i].ToString().ToUpper() == "DECIMAL" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsDecimal(ocmd, ParamName[i].ToString(), Convert.ToDecimal(ParamVal[i]), SqlDbType.Decimal, ParameterDirection.Input);
            }
            ocmd.ExecuteNonQuery();
            if (flg == true)
                return Convert.ToInt32(ocmd.Parameters[OutparamName].Value);
            else
                return 0;
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

    public int invoicepatternupdate(string xmldoc, int tsrowid, int cerowid, int samrowid, int employee_id)
    {
        string[,] param ={ { "@docxml", xmldoc }, { "@typeset_rowid", tsrowid.ToString() }, { "@ce_rowid", cerowid.ToString() }, { "@sam_rowid", samrowid.ToString() }, { "@employee_id", employee_id.ToString() }, { "@er", "output" } };

        return ExcSProcedure("spInsert_invoicepattern", param, CommandType.StoredProcedure);
    }

    public int ExcSProcedure(string sProcName, string[,] sparameter , CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        SqlTransaction sqltrans = null;
        char[] separator = new char[] { ',' };
        //string[] ParamVal;
        //string[] ParamName;
        //string[] ParamType;
        //string[] ParamDir;
        bool flg = false;
        string OutparamName = "";
        //ParamVal = paramcollection.Split(separator);
        //ParamName = paramnames.Split(separator);
        //ParamType = paramtypes.Split(separator);
        //ParamDir = paramdirections.Split(separator);

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

                        ocmd.Parameters.AddWithValue(sparameter[i, 0].ToString(), "" ).Direction = ParameterDirection.Output;
                        //ocmd.Parameters[sparameter[i, 0]].Direction = ParameterDirection.Output;
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
    //created by Mugundhan

    //public DataSet GetTaskItem(string[,] param)
    //{
    //    return ExcProcedure("spGet_TaskItem;2", param, CommandType.StoredProcedure);
    //}
    public DataSet AllocatedJobs_Employee(string emp_id)
    {
        return ExcProcedure("spGet_AllocatedJobs_Employee", new string[,] { { "@employee_id", emp_id.ToString() } }, CommandType.StoredProcedure);
    }

    public DataSet AllocatedJobs_Team(string team_id)
    {
        return ExcProcedure("spGet_AllocatedJobs_Team", new string[,] { { "@employee_team_id", team_id.ToString() } }, CommandType.StoredProcedure);
    }

    public DataSet GetJobItem()
    {
        return ExcProcedure("spget_job_item", null, CommandType.StoredProcedure);

    }
    public int MoveTask_department(string dept_id, string emp_id, string job_type_id, string job_id, string task_id, string job_status_id)
    {
        return ExcSProcedure("sp_MoveTask", new string[,] { { "@department_id", dept_id }, { "@employee_id", emp_id }, { "@job_type_id", job_type_id }, { "@job_id", job_id }, { "@task_id", task_id }, {"@job_statusid",job_status_id } }, CommandType.StoredProcedure);
    }

    public int MoveTask_employee(string emp_id, string job_type_id, string job_id, string task_id, string job_status_id)
    {
        return ExcSProcedure("sp_MoveTask_Employee", new string[,] { { "@employee_id", emp_id }, { "@job_type_id", job_type_id }, { "@job_id", job_id }, { "@task_id", task_id }, { "@job_statusid", job_status_id } }, CommandType.StoredProcedure);
    }

    public int MoveTask_employee_General(string dept_id, string job_type_id, string job_id, string job_status_id)
    {
        return ExcSProcedure("sp_MoveTask_Employee_General", new string[,] { { "@department_id", dept_id }, { "@job_type_id", job_type_id }, { "@job_id", job_id }, { "@job_status_id", job_status_id } }, CommandType.StoredProcedure);
    }

    public DataSet GetJobAllocation(string dept_id, string emp_team_id, string job_type_id, string emp_id)
    {
        try
        {
            string[,] param ={ { "@department_id", dept_id.ToString() }, { "@employee_team_id", emp_team_id.ToString() }, { "@job_type_id", job_type_id.ToString() }, { "@employee_id", emp_id.ToString() } };
            return ExcProcedure("spGet_job_allocation_CompletedJobs", param, CommandType.StoredProcedure);
            //return ExcProcedure("spGet_job_allocation", null, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertLoggedEvents(string job_id, string job_history_id, string job_type_id, string task_id,string emp_id)
    {

        string[,] events ={ { "@job_id", job_id.ToString() }, { "@job_history_id", job_history_id.ToString() }, { "@job_type_id", job_type_id.ToString() }, { "@task_id", task_id.ToString() }, { "@employee_id", emp_id.ToString() }};
        return  ExcSProcedure("spInsertEventLogs", events, CommandType.StoredProcedure);

        
    }

    public DataSet GetJobAllocationEmployee(string dept_id, string emp_team_id, string job_type_id, string emp_id)
    {
        try
        {
            string[,] param ={ { "@department_id", dept_id.ToString() }, { "@employee_team_id", emp_team_id.ToString() }, { "@job_type_id", job_type_id.ToString() }, { "@employee_id", emp_id.ToString() } };
            return ExcProcedure("spGet_job_allocation_ProcessJobs", param, CommandType.StoredProcedure);
            //return ExcProcedure("spGet_job_allocation", null, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //public DataSet GetJobAllocation()
    //{
    //    return ExcProcedure("spGet_job_allocation", null, CommandType.StoredProcedure);
    //}
    public DataSet GetJobItemTask(string dept)
    {
        string[,] param ={ { "@deptid", dept.ToString() } };
        return ExcProcedure("spget_taskitem_allocation", param, CommandType.StoredProcedure);
    }

    public DataSet GetJobReportDetails()
    {
        return ExcProcedure("select * from customer", null, CommandType.Text);
    }
    public DataSet GetReprtDetails(string[,] sparameter, CommandType cmdtype)
    {
        return ExcProcedure("spGet_RptDetails", sparameter, cmdtype);
    }
    public DataSet GetRFT(string[,] sparameter, CommandType cmdtype)
    {
        return ExcProcedure("spGet_RFT_New", sparameter, CommandType.StoredProcedure);
    }
    public DataSet Get_Filter_RFT(string job_id)
    {
        return ExcProcedure("spGet_FilterRpt_RFT", new string[,] { { "@job_id", job_id } }, CommandType.StoredProcedure);
    }

    public DataSet GetCustDetails(string[,] sparameter, CommandType cmdtype)
    {
        return ExcProcedure("spGetCustomers", sparameter, cmdtype);
    }
    public DataSet GetTeamOwner()
    {
        return ExcProcedure("spGet_ReportToDetails", null, CommandType.StoredProcedure);
    }
    public DataSet GetTeamdept()
    {
        return ExcProcedure("spGet_DEPT", null, CommandType.StoredProcedure);
    }
    public DataSet GetStages()
    {
        return ExcProcedure("spGet_stages", null, CommandType.StoredProcedure);
    }

    public DataSet GetCustomerDetails()
    {
        return ExcProcedure("spGetCustomers", null, CommandType.StoredProcedure);
    }

    //**********************************
    //Created by Malathy-- TEAM CREATION
    //**********************************

   
    public DataSet chkname(string teamname, string team_id)
    {
        if (teamname != "")
            return ExcProcedure("select * from employee_team where employee_team_name= '" + teamname + "'", null, CommandType.Text);
        else if (team_id != "")
            return ExcProcedure("select * from employee_team where employee_team_id= " + team_id + "; select * from employee_team_group where team_id=" + team_id, null, CommandType.Text);

        return null;

    }
    public DataSet searchdrop()
    {
        return ExcProcedure("select * from employee_team ", null, CommandType.Text);
    }
    public void insertdet(string teamname1, string departm, string lead, string groupby, int order,string locationid)
    {
        //string[] groupvalue = groupby.Split(',');
        //for (int grcnt = 0; grcnt < groupvalue.Length; grcnt++ )
        ExcSProcedure("insert into employee_team (employee_team_name,department_id,team_owner_id,grouped_by,order_index,location_id) values ('" + teamname1 + "'," + departm + "," + lead + "," + groupby + "," + order.ToString() + "," + ((locationid!="null")?locationid:"null") + ")", null, CommandType.Text);
    }
    public void updateteam(string upteam, string updepart, string uplead, string upgroupby, int uporder, string empteam_id,string locationid)
    {
        string[] groupvalue = upgroupby.Split(',');
        ExcSProcedure("update employee_team set employee_team_name='" + upteam + "',department_id=" + updepart + ",team_owner_id=" + ((uplead!=null)?uplead : "null".ToString()) + ",grouped_by=" + groupvalue[0].ToString() + ",order_index=" + uporder.ToString() + ",location_id=" + locationid + " where employee_team_id=" + empteam_id, null, CommandType.Text);
        ExcSProcedure("update employee_team_group set obsolete='" + DateTime.Now.ToShortDateString() + "' where team_id=" + empteam_id, null, CommandType.Text);
        for (int grcnt = 0; grcnt < groupvalue.Length; grcnt++)
            ExcSProcedure("insert into employee_team_group (team_id,team_group_owner_id) values (" + empteam_id + "," + groupvalue[grcnt].ToString() + ")", null, CommandType.Text);
            
    }
    public DataSet GetDepart()
    {
        return ExcProcedure("select * from department", null, CommandType.Text);
    }
    public DataSet GetLead()
    {
        return ExcProcedure("SpGet_Teamowner_creatingteam", new string[,] { { "@locationid", HttpContext.Current.Session["locationid"].ToString() } }, CommandType.StoredProcedure);
    }
    public DataSet GetGroupby()
    {
        return ExcProcedure("SpGet_Teamowner_creatingteam", new string[,] { { "@locationid", HttpContext.Current.Session["locationid"].ToString() } }, CommandType.StoredProcedure);
    }

    //Created by Malathy---DESIGNATION

    public DataSet chkdesigname(string designame, string desig_id)
    {
        if (designame != "")
            return ExcProcedure("select * from designation where designation_name= '" + designame + "'", null, CommandType.Text);
        else if (desig_id != "")
            return ExcProcedure("select * from designation where designation_id= " + desig_id, null, CommandType.Text);
        return null;
    }
    public DataSet searchdesigname()
    {
        return ExcProcedure("select * from designation", null, CommandType.Text);
        // where designation_name like '" + designame2 + "%'"
    }
    public void insertdesig(string designame1, int timesheet)
    {
        ExcSProcedure("insert into designation (designation_name,Time_Sheet,access_level_d) values ('" + designame1 + "'," + timesheet + ",10)", null, CommandType.Text);
    }
    public void updatedesignation(string designame2, string timesheet2, string empdesig_id)
    {
        ExcSProcedure("update designation set designation_name='" + designame2 + "',Time_Sheet=" + timesheet2 + " where designation_id=" + empdesig_id, null, CommandType.Text);
    }
    //END DESIGNATION
    //Created By Malathy --TeamBuilder

    public DataSet selectTeam()
    {
        return ExcProcedure("spGet_team_locationbase", new string[,] { { "@location_id", HttpContext.Current.Session["locationid"].ToString() } },  CommandType.StoredProcedure);
    }
    public DataSet fillteam(string teamid)
    {
        //DataSet tds = new DataSet();
        //tds = ExcProcedure("select * from employee_team where employee_team_id='" + teamid + "'", null, CommandType.Text);
        
        //return ExcProcedure("select employee_id,fname + ' (' + convert(varchar(10),employee_number) + ')' as fname from employee where report_to=" + tds.Tables[0].Rows[0]["team_owner_id"].ToString() + " and obsolete is null", null, CommandType.Text);
        return ExcProcedure("spGet_Teammember", new string[,] { { "@team_id",teamid } }, CommandType.StoredProcedure);
    }

    public DataSet fillteam2(string teamid)
    {
        //DataSet tds = new DataSet();
        //tds = ExcProcedure("select * from employee_team where employee_team_id='" + teamid + "'", null, CommandType.Text);

        //return ExcProcedure("select employee_id,fname + ' (' + convert(varchar(10),employee_number) + ')' as fname from employee where report_to=" + tds.Tables[0].Rows[0]["team_owner_id"].ToString() + " and obsolete is null", null, CommandType.Text);
        return ExcProcedure("select employee_number from employee e join employee_team et on e.Report_To=et.team_owner_id where et.employee_team_id=" + teamid, null, CommandType.Text);

    }
    public DataSet Getempname(string withoutteammember)
    {
        return ExcProcedure("select employee_id,fname + ' (' + convert(varchar(10),employee_number) + ')' as fname from employee where location_id = " + ((HttpContext.Current.Session["locationid"] != null) ? HttpContext.Current.Session["locationid"].ToString() : "location_id").ToString() + " and obsolete is null and report_to not in(select team_owner_id from employee_team where employee_team_id =" + withoutteammember + ")", null, CommandType.Text);
    }

    //public DataSet Getempno(string withoutteammember)
    //{
    //    return ExcProcedure("select employee_id,employee_number + ' (' + convert(varchar(10),employee_number) + ')' as fname from employee where location_id = " + ((HttpContext.Current.Session["locationid"] != null) ? HttpContext.Current.Session["locationid"].ToString() : "location_id").ToString() + " and obsolete is null and report_to not in(select team_owner_id from employee_team where employee_team_id =" + withoutteammember + ")", null, CommandType.Text);
    //}
    public void updatenew(string empid, string teamid)
    {

        DataSet das = new DataSet();
        das = ExcProcedure("select * from employee_team where employee_team_id=" + teamid , null, CommandType.Text);
        ExcSProcedure("update employee set Report_To=" + das.Tables[0].Rows[0]["grouped_by"].ToString() + " where employee_id=" + empid, null, CommandType.Text);

        //ExcSProcedure("UPDATE A SET A.Report_To=B.team_owner_id from employee A,employee_team B where A.Report_To=B.team_owner_id='" + townerid+" '", null, CommandType.Text);
    }
    public void insertnew(string emptid, string empteammemid)
    {
        ExcSProcedure("update employee_team_member set team_exit_date='" + DateTime.Now.ToShortDateString() + "' where employee_team_member_id=" + empteammemid, null, CommandType.Text);
        ExcSProcedure("insert into employee_team_member (employee_team_id,employee_team_member_id,Team_Joined_date) values (" + emptid + "," + empteammemid + ","+DateTime.Now.ToShortDateString()+")", null, CommandType.Text);
    }
    public DataSet checkname(string teamid, string emid)
    {
        return ExcProcedure("select * from employee_team_member where employee_team_id=" + teamid + " and employee_team_member_id=" + emid + " and team_exit_date is null", null, CommandType.Text);
    }
    //End --TeamBuilder

    //created by Malathy --Online Request

    public DataSet GetTeam()
    {
        return ExcProcedure("select * from employee_team", null, CommandType.Text);
    }

    public DataSet GetEmployeeNumber()
    {
        return ExcProcedure("select * from employee", null, CommandType.Text);

    }


    public DataSet GetTask()
    {
        return ExcProcedure("select * from task", null, CommandType.Text);
    }

    public DataSet Getpriority()
    {
        return ExcProcedure("select * from priority_status", null, CommandType.Text);
    }

    public void insertonreq(string teamid, string taskid, string rq_title, string descrip, string reqfrmid, string empid, string prname)
    {
        ExcSProcedure("insert into Online_Request (Request_to_team_id,Task_id,request_title,Description,Request_from_team_id,request_from_emp_id,priority_id) values (" + teamid + "," + taskid + ",'" + rq_title + "','" + descrip + "','" + reqfrmid + "','" + empid + "','" + prname + "')", null, CommandType.Text);
    }
    //End Online Request

    // created by Malathy --Online Request Allocated

    public DataSet onlinepending(string employee_id)
    {
        return ExcProcedure("spGet_team_pendinglist", new string[,] { { "@employee_id",employee_id} }, CommandType.StoredProcedure);
    }

    public DataSet onlinegeneral(string employee_id)
    {

        return ExcProcedure("spGet_team_onlinerequestlist", new string[,] { { "@employee_id",employee_id} }, CommandType.StoredProcedure);
    }

    public DataSet onlinecomplete(string employee_id)
    {
        return ExcProcedure("spGet_team_online_completed_list",new string[,]{{"@employee_id",employee_id}},CommandType.StoredProcedure);
    }

    public DataSet redirect_to()
    {
        return ExcProcedure("select fname from employee", null, CommandType.Text);
    }

    public void insert_redirect(string taskid, string asign_date,string duedate, string assignby, string asignto, string redir_to, string comment,string onlinerqid)
    {
        ExcSProcedure("insert into online_req_allocated (Task_id,assigned_date,due_date,assigned_by,assigned_to,re_directed_to,comment,online_request_id)values(" + taskid + ",'" + asign_date + "','" + duedate + "','" + assignby + "' ," + asignto + "," + redir_to + ",'" + comment + "'," + onlinerqid + ")", null, CommandType.Text);
    }

    public DataSet GetAssignTo(string assign, string location)
    {
        string[,] param ={ { "@header_employee_id", assign.ToString() }, { "@locationid", location.ToString() } };
        return ExcProcedure("spGet_teammember_allheaderlevel", param, CommandType.StoredProcedure);
    }
    //Online Request Allocated

    public DataSet onpending(string sessempid)
    {
        return ExcProcedure("spGet_pendingholding_jobs", new string[,] { { "@empid",sessempid} }, CommandType.StoredProcedure);
    }

    public DataSet oncomplete(string sessempid)
    {
        return ExcProcedure("spGet_emp_online_completed_list", new string[,] { { "@employee_id",sessempid } }, CommandType.StoredProcedure);
    }

    public DataSet ongeneral(string sessempid)
    {

        return ExcProcedure("spGet_emp_onlinerequestlist", new string[,] { { "@employee_id", sessempid } }, CommandType.StoredProcedure);
    }

    //Allocated start date

    public void strtenddate(string requestid)
    {
        ExcSProcedure("update online_req_allocated set start_date='" + DateTime.Now.ToShortDateString() + "'" + " where online_request_id=" + requestid, null, CommandType.Text);
        //ExcSProcedure("insert into online_req_allocated(start_date,completed_date)values('" + reqstartdate + "','" + reqenddate + "')", null, CommandType.Text);
    }
    //Request complete

    public void onlinerequest_complete(string request_id)
    {
        ExcSProcedure("update online_req_allocated set completed_date='" + DateTime.Now.ToShortDateString() + "'" + " where online_request_id=" + request_id, null, CommandType.Text);
    }

    //Hold start date and reason

    public void onhold(string reson, string empid, DateTime Releasedate,string holdjobid)
    {
        ExcSProcedure("insert into online_hold(hold_reason,holded_emp_id,job_id)values('" + reson + "'," + empid + "," + holdjobid + ")", null, CommandType.Text);
    }

    public void release_hold(string release_holdjobid)
    {
        ExcSProcedure("update online_hold set Release_Date='" + DateTime.Now.ToShortDateString() + "' where job_id=" + release_holdjobid, null, CommandType.Text);
    }

    public DataSet getlocation()
    {
        return ExcProcedure("spGet_location", null, CommandType.StoredProcedure);
    }
    //created by Malathy--Online Request Report

    public DataSet GetOnline_Request_Report(string teamemployee, string frmdate, string todate)
    {
        string[,] param ={ { "@employee_id", teamemployee.ToString() }, { "@from_date", frmdate.ToString() }, { "@to_date", todate.ToString() } };
        return ExcProcedure("spGet_online_request_report", param, CommandType.StoredProcedure);
        //return ExcProcedure("select o.online_request_id,t.task_name,oa.due_date,oa.start_date,oa.completed_date,oh.hold_start_date,oh.Release_Date,oh.hold_reason,datediff(day,start_date,completed_date) as WorkingDays,datediff(day,hold_start_date,Release_Date) as HoldedDays,case when hold_start_date is not null and Release_Date is null and completed_date is null then 'Hold' when start_date is not null and completed_date is null then 'WIP' when start_date is not null and completed_date is not null then 'Completed' else '' end work_status from Online_Req_allocated oa join online_request as o on oa.online_request_id=o.online_request_id join task as t on o.Task_id=t.task_id left join online_hold as oh on o.online_request_id = oh.job_id  where oa.Assigned_to= " + teamemployee+" and oa.start_date between'"+ frmdate +"'and '"+ todate +"'",null, CommandType.Text);

    } //End Online Report.

    public DataSet GetSurveyReport(string surveyempid)
    {
        string[,] param ={ { "@employee_id", surveyempid.ToString() } };
        return ExcProcedure("spGet_Online_Employee_Survey_Report", param, CommandType.StoredProcedure);
    }
    public void insertbankdetails(int bankdetails)
    {
        ExcSProcedure("insert into customer(Invoice_bank_details)values(" + bankdetails + ")", null, CommandType.Text);

    }

    //public void insertjoblocation(int joblocation)
    //{ ExcProcedure("insert into job_parent(job_location_id)values(" + joblocation + ")", null, CommandType.Text); }
        
    public DataSet ExcProcedure(string sProcName,string[,] sparameter , CommandType CmdType)
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
            ocmd.CommandTimeout = 600000;
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
            sqlad.Fill(ods, "GetDetails");
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

    public DataSet ExcuteSelectProcedure(string sProcName, string paramcollection, string paramnames, string paramtypes, string paramdirections, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;
        string[] ParamType;
        string[] ParamDir;
        ParamVal = paramcollection.Split(separator);
        ParamName = paramnames.Split(separator);
        ParamType = paramtypes.Split(separator);
        ParamDir = paramdirections.Split(separator);

        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.Parameters.Clear();
            int i;
            for (i = 0; i < ParamType.GetLength(0); i++)
            {
                string pt = ParamType[i].ToString();
                string pdir = ParamDir[i].ToString();
                if (ParamType[i].ToString().ToUpper() == "INT" && ParamDir[i].ToString().ToUpper() == "INPUT")
                {
                    if(ParamVal[i].ToString()=="")
                        addParamsINT(ocmd, ParamName[i].ToString(), 0, SqlDbType.Int, ParameterDirection.Input);
                    else
                        addParamsINT(ocmd, ParamName[i].ToString(), Convert.ToInt32(ParamVal[i]), SqlDbType.Int, ParameterDirection.Input);
                }
                else if (ParamType[i].ToString().ToUpper() == "INT" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addOutPutParams(ocmd, ParamName[i].ToString(), SqlDbType.Int, ParameterDirection.Output);
                else if (ParamType[i].ToString().ToUpper() == "VARCHAR" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsSTR(ocmd, ParamName[i].ToString(), ParamVal[i].ToString(), SqlDbType.VarChar, 600, ParameterDirection.Input);
                else if (ParamType[i].ToString().ToUpper() == "VARCHAR" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addOutPutParams(ocmd, ParamName[i].ToString(), SqlDbType.VarChar, ParameterDirection.Output);
                else if (ParamType[i].ToString().ToUpper() == "DATE" && ParamDir[i].ToString().ToUpper() == "INPUT")
                    addParamsDATE(ocmd, ParamName[i].ToString(), Convert.ToDateTime(ParamVal[i]), SqlDbType.DateTime, ParameterDirection.Input);
                else if (ParamType[i].ToString().ToUpper() == "DATE" && ParamDir[i].ToString().ToUpper() == "OUTPUT")
                    addParamsDATE(ocmd, ParamName[i].ToString(), Convert.ToDateTime(ParamVal[i]), SqlDbType.DateTime, ParameterDirection.Output);
            }
            ocmd.ExecuteNonQuery();
            SqlDataAdapter sqlad = new SqlDataAdapter(ocmd);
            sqlad.Fill(ods, "GetDetails");
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
    public string RevertJobObjects(string[,] oParams, Boolean bBool)
    {
       // SqlCommand ocmd = new SqlCommand();
        try
        {

            SetTrans(CommandType.StoredProcedure, bBool);

            ocmd.CommandText = "MANUALUPDATE_JOB_OBJECTS";

            if (oParams != null)
            {

                ocmd.Parameters.Clear();

                for (int i = 0; i <= oParams.GetUpperBound(0); i++)
                {

                    if (oParams[i, 1] == null || oParams[i, 1].ToString() == "" || oParams[i, 1].ToString() == "0")

                        ocmd.Parameters.AddWithValue(oParams[i, 0].ToString(), null);

                    else

                        ocmd.Parameters.AddWithValue(oParams[i, 0].ToString(), oParams[i, 1].ToString());

                }

            }

            ocmd.ExecuteScalar();

            return "";

        }

        catch (Exception oex)
        {

            if (bBool)
            {

                RollbackTrans();

            }

            return oex.Message;

        }

        finally
        {

            if (bBool)
            {

                CommitTrans(bBool);

            }

        }

    }
    private void SetTrans(CommandType oType, Boolean bSetTrans)
    {
        opencon();
        ocmd = new SqlCommand();
        ocmd.Connection = oConn;
        if (bSetTrans)
        {
            if (oTran == null)
            {
                oTran = oConn.BeginTransaction();
                ocmd.Transaction = oTran;
            }
        }
        
        ocmd.CommandType = oType;
        ocmd.Parameters.Clear();
    }

    private void CommitTrans(Boolean bCommit)
    {
        if (bCommit)
        {
            if (oTran != null)
            {
                oTran.Commit();
                oTran.Dispose();
                oTran = null;
            }
            closecon();
        }
    }

    private void RollbackTrans()
    {
        if (oTran != null)
        {
            oTran.Rollback();
            oTran.Dispose();
            oTran = null;
        }
        closecon();
        ocmd = null;
    }
    //For SQL Productivity Report
    public sqlProductivityDS ProductivityDetails(string qry,string[,] param)
    {
        sqlProductivityDS prods = new sqlProductivityDS();
        SqlDataAdapter proDA=null;
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
            proDA.Fill(prods);
            return prods;
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { 
            proDA.Dispose();
            closecon();
        }
        
    }
    public void insertalert_comments(string cmtqry,string logeventid,string employee_id)
    {
        string[] logid = new string[cmtqry.Split(',').Length];
        logid = cmtqry.Split(',');
        string insertqry = string.Empty;
        try
        {
            for (int cmtint = 0; cmtint < logid.Length; cmtint++)
            {
                insertqry += "insert into alert_comments(employee_id,loggedevent_id,comment_id,create_date) values(" + employee_id + "," + logeventid + "," + logid[cmtint].ToString() + ",'" + DateTime.Now.ToShortDateString() + "');";
            }
            ExcSProcedure("spinsert_alertcomments", new string[,] { { "@comqry", insertqry } }, CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { }
    }
    public DataSet GetEmpAttendance(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        DataSet ds = new DataSet();
        ds = ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
    }

    public DataSet GetAllEmpLeave()
    {
        return GetDataSet("sp_GetLeaveOpBal", "Employee", CommandType.StoredProcedure);
    }
    public DataSet GetTeamHeads(int id)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGet_TeamHeadsDetails";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@Location", id, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Team");
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
    public DataSet GetAllEmployee(int id, int loc)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGet_AllEmployee1";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@Location", loc, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@empid", id, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public DataSet GetEmpShiftTrack(int id, int loc)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGet_EmpShifttrack";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@Location", loc, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@empid", id, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public DataSet GetEmpShiftMonthWise(int id)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGet_EmpShifttrackMonthWise";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@empid", id, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public DataSet GetEmpShiftMonthWise1(int id,int month,int year)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGet_EmpShifttrackMonthWise1";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@empid", id, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@month", month, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@year", year, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public DataSet GetAllEmpLate(int id, int loc)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGet_AllEmployee2";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@Location", loc, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@empid", id, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public DataSet GetTeamHead(int id)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "SpGet_TeamHeads";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@Empid", id, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Team");
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
    public bool Update_StageDetails(System.Collections.ArrayList al)
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
                    if (ht["lev2"].ToString() != "0" && ht["lev1"].ToString() != "0")
                    {
                        umodule = "update employee set Level1='"
                        + ht["lev1"].ToString() + "',Level2='" + ht["lev2"].ToString()
                        + "' where   employee_id='" + ht["ID"].ToString() + "'";
                        ocmd.CommandText = umodule;
                        ocmd.ExecuteNonQuery();
                    }
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
    public bool Update_ShiftDetails(System.Collections.ArrayList al)
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
                    if (ht["Shift"].ToString() != "0")
                    {
                        umodule = "update employee set Shift='"
                        + ht["Shift"].ToString()
                        + "' where   employee_id='" + ht["ID"].ToString() + "'";
                        ocmd.CommandText = umodule;
                        ocmd.ExecuteNonQuery();
                    }
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
    public DataSet GetEmpStage(int empid)
    {
        return GetDataSet("SELECT * FROM employee WHERE employee_id='" + empid + "' and OBSOLETE IS NULL ", "Emp", CommandType.Text);
    }
    public DataSet GetEmpShift(int empid)
    {
        return GetDataSet("SELECT * FROM EMpshiftTrack WHERE employee_id='" + empid + "' and OBSOLETE IS NULL and sdate=convert(varchar(30),getdate(),101)", "Shift", CommandType.Text);
    }
    public DataSet GetEmpShiftDateWise(int empid,string Date)
    {
        return GetDataSet("SELECT * FROM EMpshiftTrack WHERE employee_id='" + empid + "' and OBSOLETE IS NULL and sdate='" + Date+"'", "Shift", CommandType.Text);
    }
    public DataSet EmpShift(string shiftid,string loc)
    {
        return GetDataSet("SELECT * FROM shift_details WHERE shift_id='" + shiftid + "' and locationid='"+loc.ToString()+"'", "Shift", CommandType.Text);
    }
    public DataSet GetMonth(int empid)
    {
        return GetDataSet("select datediff(Month,joined_date,getdate()) as months from employee where employee_id='" + empid + "' and OBSOLETE IS NULL ", "Emp", CommandType.Text);
    }
    public DataSet GetShift(string loc)
    {
        return GetDataSet("select CONVERT(varchar(30),InTime,108) InTime,CONVERT(varchar(30),OutTime,108) OutTime,* from Shift_Details where locationid= '" + loc.ToString() + "' ORDER BY sHIFT_id", "Shift", CommandType.Text);
    }
    public DataSet GetLeavePend(int empid)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGetLeavePending";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@empid", empid, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public DataSet GetEmpLeaveOpBal(int empid)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "sp_GetLeaveOpBal1";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@empid", empid, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
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
    public int ExcSP(string sProcName, string[,] sparameter, CommandType CmdType)
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
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "")
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
    public DataSet GetJobAllocation(string dept_id, string emp_team_id)
    {
        try
        {
            string[,] param = { { "@department_id", dept_id.ToString() }, { "@stageid", emp_team_id.ToString() } };
            return ExcProcedure("spGet_job_allocation_AMO", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetJobAllocationEmployee(string dept_id, string stageid)
    {
        try
        {
            
            string[,] param = { { "@department_id", dept_id.ToString() }, { "@stageid", stageid.ToString() } };
            return ExcProcedure("spGet_job_allocation_AMO", param, CommandType.StoredProcedure);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet GetJobHistoryStage(string jobhistoryId)
    {
        return ExcProcedure("select job_stage_id AS job_stage_id from job_history where job_history_id=" + jobhistoryId + "", null, CommandType.Text);

    }

    
    public int MoveTask_employee_process(string emp_id, string job_type_id, string job_id, string task_id, string job_status_id, string job_process)
    {
        return ExcSProcedure("sp_MoveTask_Employee_new", new string[,] { { "@employee_id", emp_id }, { "@job_type_id", job_type_id }, { "@job_id", job_id }, { "@task_id", task_id }, { "@job_statusid", job_status_id }, { "@job_process", job_process } }, CommandType.StoredProcedure);
    }

    public int MoveTask_employee(string emp_id, string job_type_id, string job_id, string task_id, string job_status_id, string job_history_id)
    {
        return ExcSProcedure("sp_MoveTask_Employee_AMO", new string[,] { { "@employee_id", emp_id }, { "@job_type_id", job_type_id }, { "@job_id", job_id }, { "@task_id", task_id }, { "@job_statusid", job_status_id }, { "@job_history_Id", job_history_id } }, CommandType.StoredProcedure);
    }

    public DataSet GetJobHistoryStage(string jobhistoryId, string Job_Type_Id)
    {
        DataSet dsHistoryStage = new DataSet();
        try
        {
            if (Job_Type_Id == "5")
            {
                dsHistoryStage = ExcProcedure("SELECT STYPENO AS job_stage_id FROM JOB_HISTORY WHERE ANO=" + jobhistoryId + "", null, CommandType.Text);
            }
            else if (Job_Type_Id == "6")
            {
                dsHistoryStage = ExcProcedure("SELECT STYPENO AS job_stage_id FROM job_history WHERE INO=" + jobhistoryId + "", null, CommandType.Text);
            }
            return dsHistoryStage;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }

    public DataSet GetJobStage(string jobhistoryId, string Job_Type_Id)
    {
        DataSet dsHistoryStage = new DataSet();
        try
        {
            if (Job_Type_Id == "5")
            {
                dsHistoryStage = ExcProcedure("SELECT STYPENO AS job_stage_id FROM ARTICLE_DP WHERE ANO=" + jobhistoryId + "", null, CommandType.Text);
            }
            else if (Job_Type_Id == "6")
            {
                dsHistoryStage = ExcProcedure("SELECT STYPENO AS job_stage_id FROM ISSUE_DP WHERE INO=" + jobhistoryId + "", null, CommandType.Text);
            }
            return dsHistoryStage;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }

    public DataSet GetAllocatedEmployee(string intType, string teamid, string intJobType, string intJobId)
    {
        return ExcProcedure("spGet_AllocatedEmp", new string[,] { { "@intType", intType }, { "@team_id", teamid }, { "@intJobType", intJobType }, { "@intJobId", intJobId } }, CommandType.StoredProcedure);
    }


    public int MoveTask_emp_process(string job_id, string job_type_id, string job_status_id, string employee_id, string task_id, string order, string stage_id, string strJobHistoryId)
    {
        return ExcSProcedure("spMISInsertEventLogs", new string[,] { { "@job_id", job_id }, { "@job_type_id", job_type_id }, { "@job_statusid", job_status_id }, { "@employee_id", employee_id }, { "@task_id", task_id }, { "@order", order }, { "@stage_id", stage_id }, { "@strJobHistoryId",strJobHistoryId } }, CommandType.StoredProcedure);
    }

    public DataSet GetCustomer()
    {
        return GetDataSet("mis_sp_customer_dtl", "CUSTOMER_DP", CommandType.StoredProcedure);
    }

    public DataSet GetCustomerDetails(string CustNo)
    {
        DataSet dsOffice = new DataSet();
        dsOffice = ExecProcedure("mis_sp_getCustomer_dtl", new string[,] { { "@intCustNo", CustNo } }, CommandType.StoredProcedure);
        if (dsOffice != null)
        {
            if (dsOffice.Tables.Contains("GetDetails"))
            {
                dsOffice.Tables["GetDetails"].TableName = "CONTACTSITE_DP";
            }

            if (dsOffice.Tables.Contains("GetDetails1"))
            {
                dsOffice.Tables["GetDetails1"].TableName = "FINANCIALSITE_DP";
            }

            if (dsOffice.Tables.Contains("GetDetails2"))
            {
                dsOffice.Tables["GetDetails2"].TableName = "CURRENCY_DP";
            }

            if (dsOffice.Tables.Contains("GetDetails3"))
            {
                dsOffice.Tables["GetDetails3"].TableName = "COUNTRY_DP";
            }

            if (dsOffice.Tables.Contains("GetDetails4"))
            {
                dsOffice.Tables["GetDetails4"].TableName = "GROUPBY";
            }

            if (dsOffice.Tables.Contains("GetDetails5"))
            {
                dsOffice.Tables["GetDetails5"].TableName = "CUSTOMER_DETAILS";
            }
        }
        return dsOffice;
    }

    public DataSet ExecProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
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
            sqlad.Fill(ods, "GetDetails");

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
    public DataSet GetEmployeeDetails()
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        SqlParameter sqlParm = new SqlParameter();
        string strStatus = "";
        SqlDataAdapter daAdapt = new SqlDataAdapter();
        try
        {
            openconvote();
            ocmd.CommandType = CommandType.StoredProcedure;
            ocmd.CommandText = "hrm_sp_get_emp_dtl";
            ocmd.Connection = oConnVote;
            daAdapt = new SqlDataAdapter(ocmd);
            daAdapt.Fill(ods, "EmployeeDetails");


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

    public DataSet GetResultDetails()
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        SqlParameter sqlParm = new SqlParameter();
        string strStatus = "";
        SqlDataAdapter daAdapt = new SqlDataAdapter();
        try
        {
            openconvote();
            ocmd.CommandType = CommandType.StoredProcedure;
            ocmd.CommandText = "hrm_sp_get_vote_result";
            ocmd.Connection = oConnVote;
            daAdapt = new SqlDataAdapter(ocmd);
            daAdapt.Fill(ods, "EmployeeDetails");
   
    
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
    public int GetEmployeeVote(int Emp_Id)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        SqlParameter sqlParm = new SqlParameter();
        int intStatus = 0;
        SqlDataAdapter daAdapt = new SqlDataAdapter();
        try
        {
            openconvote();
            ocmd.CommandType = CommandType.StoredProcedure;
            ocmd.CommandText = "hrm_sp_get_vote_dtl";
            ocmd.Connection = oConnVote;
            sqlParm = new SqlParameter();
            sqlParm.ParameterName = "@emp_id";
            sqlParm.Value = Emp_Id;
            sqlParm.SqlDbType = SqlDbType.Int;
            sqlParm.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(sqlParm);
            sqlParm = new SqlParameter();
            sqlParm.ParameterName = "@intStatus";
            sqlParm.Value = intStatus;
            sqlParm.SqlDbType = SqlDbType.Int;
            sqlParm.Direction = ParameterDirection.Output;
            ocmd.Parameters.Add(sqlParm);
            ocmd.ExecuteNonQuery();
            intStatus =Convert.ToInt32(ocmd.Parameters["@intStatus"].Value);
            return intStatus;
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
    public void SetVoteToEmp(string intRefNo, string intMale_Emp_Id, string intFemaleEmpId)
    {
        try
        {
            SqlCommand ocmd = new SqlCommand();
            DataSet ods = new DataSet();
            SqlParameter sqlParm = new SqlParameter();
            int intStatus = 0;
            SqlDataAdapter daAdapt = new SqlDataAdapter();
            try
            {
                openconvote();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.CommandText = "hrm_sp_insert_vote";
                ocmd.Connection = oConnVote;
                sqlParm = new SqlParameter();
                sqlParm.ParameterName = "@ref_no";
                sqlParm.Value = intRefNo;
                sqlParm.SqlDbType = SqlDbType.Int;
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);
                sqlParm = new SqlParameter();
                sqlParm.ParameterName = "@male_emp_id";
                sqlParm.Value = intMale_Emp_Id;
                sqlParm.SqlDbType = SqlDbType.Int;
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);
                sqlParm = new SqlParameter();
                sqlParm.ParameterName = "@female_emp_id";
                sqlParm.Value = intFemaleEmpId;
                sqlParm.SqlDbType = SqlDbType.Int;
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);
                ocmd.ExecuteNonQuery();
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
    private void openconvote()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrVote"].ToString();
        oConnVote = new SqlConnection(sConStr);
        oConnVote.Open();
    }
    public DataSet GetEmpDetails(int empid, int locationid)
    {
        return GetDataSet("select * from employee where location_id=" + locationid.ToString() + " and employee_number =" + empid, "Employee", CommandType.Text);
    }

    public DataSet GetPendingStatus(string empid)
    {
        string[,] param = { { "@empid", empid } };
        return ExcProcedure("PendingStatus", param, CommandType.StoredProcedure);
    }
    public DataSet GetEmpPendstatus(int empid)
    {
        return GetDataSet("select count(team_owner_id) as empno from employee_team_heads where team_owner_id=" + empid, "Employee", CommandType.Text);
    }
    public bool Insert_LeaveDetails(System.Collections.ArrayList al)
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
                    umodule = "INSERT INTO EMPLOYEE_LEAVE_HISTORY(EMPLOYEE_ID,LEAVE_IN,LEAVE_OUT,DATE_IN,LEAVE_TYPE_ID,DAYS,COMMENT,DATESDETAILS,AppliedDate,ModeofInform,CreatedDate,PerMins) values ("
                        + ht["ID"].ToString() + ",'" + ht["LeaveIn"].ToString() + "','"
                        + ht["LeaveOut"].ToString() + "','" + ht["Date"].ToString() + "','"
                        + ht["LeaveType"].ToString() + "','" + ht["Days"].ToString() + "','"
                        + ht["Remarks"].ToString() + "','" + ht["DateDetails"].ToString() + "','"
                        + ht["AppliedDate"].ToString() + "','" + ht["ModeofInform"].ToString() + "',Getdate()," + ht["PerMins"].ToString() + ")";
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
    public bool Insert_LeaveDetailsTeamHead(System.Collections.ArrayList al)
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
                    if (ht["Approved"].ToString() == "Null" && ht["Recorded"].ToString() == "Null")
                    {
                        umodule = "INSERT INTO EMPLOYEE_LEAVE_HISTORY(EMPLOYEE_ID,LEAVE_IN,LEAVE_OUT,DATE_IN,LEAVE_TYPE_ID,DAYS,COMMENT,DATESDETAILS,AppliedDate,ModeofInform,PerMins) values ("
                            + ht["ID"].ToString() + ",'" + ht["LeaveIn"].ToString() + "','"
                            + ht["LeaveOut"].ToString() + "','" + ht["Date"].ToString() + "','"
                            + ht["LeaveType"].ToString() + "','" + ht["Days"].ToString() + "','"
                            + ht["Remarks"].ToString() + "','" + ht["DateDetails"].ToString() + "','"
                            + ht["AppliedDate"].ToString() + "','" + ht["ModeofInform"].ToString() + "'," + ht["PerMins"].ToString() +")";
                    }
                    else if (ht["Approved_L2"].ToString() == "Null" && ht["Recorded_L2"].ToString() == "Null")
                    {
                        umodule = "INSERT INTO EMPLOYEE_LEAVE_HISTORY(EMPLOYEE_ID,LEAVE_IN,LEAVE_OUT,DATE_IN,LEAVE_TYPE_ID,DAYS,COMMENT,DATESDETAILS,Approved,Recorded,AppliedDate,ModeofInform,PerMins) values ("
                            + ht["ID"].ToString() + ",'" + ht["LeaveIn"].ToString() + "','"
                            + ht["LeaveOut"].ToString() + "','" + ht["Date"].ToString() + "','"
                            + ht["LeaveType"].ToString() + "','" + ht["Days"].ToString() + "','"
                            + ht["Remarks"].ToString() + "','" + ht["DateDetails"].ToString() + "','"
                            + ht["Approved"].ToString() + "','" + ht["Recorded"].ToString() + "','"
                            + ht["AppliedDate"].ToString() + "','" + ht["ModeofInform"].ToString() + "'," + ht["PerMins"].ToString() + ")";
                    }
                    else
                    {
                        umodule = "INSERT INTO EMPLOYEE_LEAVE_HISTORY(EMPLOYEE_ID,LEAVE_IN,LEAVE_OUT,DATE_IN,LEAVE_TYPE_ID,DAYS,COMMENT,DATESDETAILS,Approved,Recorded,Approved_L2,Recorded_L2,AppliedDate,ModeofInform,PerMins) values ("
                              + ht["ID"].ToString() + ",'" + ht["LeaveIn"].ToString() + "','"
                              + ht["LeaveOut"].ToString() + "','" + ht["Date"].ToString() + "','"
                              + ht["LeaveType"].ToString() + "','" + ht["Days"].ToString() + "','"
                              + ht["Remarks"].ToString() + "','" + ht["DateDetails"].ToString() + "','"
                              + ht["Approved"].ToString() + "','" + ht["Recorded"].ToString() + "','"
                              + ht["Approved_L2"].ToString() + "','" + ht["Recorded_L2"].ToString() + "','"
                              + ht["AppliedDate"].ToString() + "','" + ht["ModeofInform"].ToString() + "'," + ht["PerMins"].ToString() + ")";
                    }
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
    public DataSet WordCount(string sdate, string edate)
    {
        return ExcProcedure("spGetWCRef", new string[,] { { "@sdate", sdate.ToString() }, { "@edate", edate.ToString() } }, CommandType.StoredProcedure);
    }
    public DataSet EmpLeaveYTD(string ID)
    {
        return ExcProcedure("spGetYTDLeaveTaken", new string[,] { { "@Empid", ID.ToString() } }, CommandType.StoredProcedure);
    }
    public DataSet EmpAttDaily(string ID, string sdate, string edate)
    {
        return ExcProcedure("spGetCosecAttDaily", new string[,] { { "@UserID", ID.ToString() }, { "@sdate", sdate.ToString() }, { "@edate", edate.ToString() } }, CommandType.StoredProcedure);
    }
    public DataSet GetApprLevel(int EmpID, int AppID)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "SpGet_ApprLevel";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@Empid", EmpID, SqlDbType.Int, ParameterDirection.Input);
            addParamsINT(oCmd, "@appid", AppID, SqlDbType.Int, ParameterDirection.Input);
            SqlDataAdapter da = new SqlDataAdapter(oCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Team");
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
    public DataSet GetlogPendstatus(int LENO)
    {
        return GetDataSet("select * from LOGGEDEVENTS_DP where LENO=" + LENO, "LogEvents", CommandType.Text);
    }
    public DataSet GetTaskType(string[,] param)
    {
        return ExcProcedure("spGet_TaskItem;4", param, CommandType.StoredProcedure);
    }
    public DataSet GetPlogPendstatus(int LENO)
    {
        return GetDataSet("select * from P_LOGGEDEVENTS_DP where PID=" + LENO, "LogEvents", CommandType.Text);
    }
    public DataSet GetlogPendstatusBook(int LENO)
    {
        return GetDataSet("select * from BLOGEVENTS_DP where LENO=" + LENO, "LogEvents", CommandType.Text);
    }
    public DataSet GetChapDetBook(int BNO)
    {
        return GetDataSet("select cpno,CHPTITLE from ChapterList_DP where BNO=" + BNO, "LogEvents", CommandType.Text);
    }
    public void InsertLoginHis(string MacAdd, string IpAdd, string userid, string empid, string browser, string hostname)
    {
        ExcSProcedure("spInsertUserLoginHis",
            new string[,] { { "@MACAddress", MacAdd }, { "@IPAddress", IpAdd }, { "@UserID", userid },
                            { "@Employee_id", empid }, { "@Browser", browser }, { "@HostName", hostname }}, CommandType.StoredProcedure);
    }
    public void LogOutHis(string IpAdd, string empid)
    {
        ExcSProcedure("spLogOutHis",
            new string[,] { { "@IPAddress", IpAdd }, { "@Employee_id", empid } }, CommandType.StoredProcedure);
    }
    public DataSet ExcProcedure1(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        char[] separator = new char[] { ',' };
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
            ocmd.CommandTimeout = 600000;
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
            sqlad.Fill(ods, "GetDetails");
            if (ods == null)
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
    public DataSet ChkLoginHis(string ID, string IpAdd)
    {
        return ExcProcedure1("spchkLoginHistory",
            new string[,] { { "@Employee_id", ID.ToString() }, { "@IPAddress", IpAdd.ToString() } }, CommandType.StoredProcedure);
    }
}