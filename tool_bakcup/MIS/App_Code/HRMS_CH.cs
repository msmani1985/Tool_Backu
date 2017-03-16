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
/// Summary description for HRMS_CH
/// </summary>
public class HRMS_CH
{
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    public HRMS_CH()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString;
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
    public DataSet GetState()
    {
        return GetDataSet("SELECT * FROM Mas_State ", "State", CommandType.Text);
    }
    public DataSet GetFamilyDetails(string id)
    {
        return GetDataSet("SELECT empid,desc01,desc02,desc04,Counter,Convert(varchar(30),todate,101) as DOB,datediff(year,todate,getdate()) as age,case when OPTIONYN=1 then 'true' else 'false' end as OPTIONYN,case when NOMINEEYN=1 then 'true' else 'false' end as NOMINEEYN FROM Mas_HRDetails where hrtype=5 and empid=(select empid from master_employee where refno='" + id + "') order by counter", "HRDetails", CommandType.Text);
    }
    public DataSet GetEduDetails(string id)
    {
        return GetDataSet("SELECT *,year(todate) as year FROM Mas_HRDetails where hrtype=2 and empid=(select empid from master_employee where refno='" + id + "') order by counter", "HRDetails", CommandType.Text);
    }
    public void deletefamily(string empid, int hrtype, int counter)
    {
        ExcSProcedure("delete from MAS_HRDETAILS where hrtype=" + hrtype + " and counter=" + counter + " and empid=(select empid from master_employee where refno='" + empid + "')", null, CommandType.Text);
    }
    public void deleteEduDetails(string empid, int hrtype, int counter)
    {
        ExcSProcedure("delete from MAS_HRDETAILS where hrtype=" + hrtype + " and counter=" + counter + " and empid=(select empid from master_employee where refno='" + empid + "')", null, CommandType.Text);
    }
    public void InsertFamilyDetails(string empid)
    {
        //ExcSProcedure("insert into MAS_HUMANRES(empid,Bloodgroup,Qualification,Nationality) select empid,0,0,'Indian' from mas_employee where  empid=(select empid from mas_employee where refno='" + empid + "')", null, CommandType.Text);
        ExcSProcedure("insert into MAS_HRDETAILS(empid,hrtype,counter,Desc01,optionyn,NOMINEEYN) select empid,5,1,'No Records',0,0 from master_employee where  empid=(select empid from master_employee where refno='" + empid + "')", null, CommandType.Text);
    }
    public void InsertEduDetails(string empid)
    {
        ExcSProcedure("insert into MAS_HRDETAILS(empid,hrtype,counter,Desc01,optionyn,NOMINEEYN) select empid,2,1,'No Records',0,0 from master_employee where  empid=(select empid from master_employee where refno='" + empid + "')", null, CommandType.Text);
    }
    public DataSet GetEmployeeDetail(int employee_id)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "spGetChennaiEmpDetails";
            oCmd.Parameters.Clear();
            addParamsINT(oCmd, "@empid", employee_id, SqlDbType.Int, ParameterDirection.Input);
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
    public DataSet GetEmpDetail(string employee_id)
    {
        try
        {
            opencon();
            SqlCommand oCmd = new SqlCommand();
            oCmd.Connection = oConn;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.CommandText = "sp_GetEmpDetails";
            oCmd.Parameters.Clear();
            addParamsVar(oCmd, "@empid", employee_id, SqlDbType.VarChar, ParameterDirection.Input);
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
    private void addParamsSTR(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, int sLen, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType, sLen));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
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

    private void addParamsINT(SqlCommand oCmmd, string sName, int sValue, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    private void addParamsVar(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, ParameterDirection sDirection)
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

    private void addOutPutParams(SqlCommand oCmmd, string sName, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        oCmmd.Parameters[sName].Direction = sDirection;
    }

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
        }
    }
    public int ExcuteProcedure(string sProcName, string paramcollection, string paramnames, string paramtypes, string paramdirections, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;
        string[] ParamType;
        string[] ParamDir;
        bool flg = false;
        string OutparamName = "";
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

                if (ParamType[i].ToString().ToUpper() == "INT" && ParamDir[i].ToString().ToUpper() == "INPUT")
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

    public int ExcSProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
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
    public int ExcSProc(string sProcName, string[,] sparameter, CommandType CmdType)
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
                    else if (sparameter[i, 1] == null || sparameter[i, 1].ToString() == "" )
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

    public DataSet ExcProcedure(string sProcName, string[,] sparameter, CommandType CmdType)
    {
        SqlCommand ocmd = new SqlCommand();
        DataSet ods = new DataSet();
        char[] separator = new char[] { ',' };
        string[] ParamVal;
        string[] ParamName;
        try
        {
            opencon();
            ocmd.CommandType = CmdType;
            ocmd.CommandText = sProcName;
            ocmd.Connection = oConn;
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
                    if (ParamVal[i].ToString() == "")
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
    //For CR Report
    //public EmpDetails_CH EmpDetails(string qry, string[,] param)
    //{
        
    //    EmpDetails_CH prods = new EmpDetails_CH();
    //    SqlDataAdapter proDA = null;
    //    ocmd = new SqlCommand();
    //    try
    //    {
    //        opencon();

    //        proDA = new SqlDataAdapter(qry, oConn);
    //        proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        if (param != null)
    //        {
    //            for (int param_cnt = 0; param_cnt <= param.GetUpperBound(0); param_cnt++)
    //            {
    //                if (param[param_cnt, 1] == null || param[param_cnt, 1].ToString() == "" || param[param_cnt, 1].ToString() == "0")
    //                    proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), null);
    //                else
    //                    proDA.SelectCommand.Parameters.AddWithValue(param[param_cnt, 0].ToString(), param[param_cnt, 1].ToString());
    //            }
    //        }
    //        proDA.Fill(prods);
    //        return prods;
    //    }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        proDA.Dispose();
    //        closecon();
    //    }

    //}

}
