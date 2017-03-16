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
/// Summary description for LaunchSQL
/// </summary>
public class Launch_SQL
{
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    public Launch_SQL()
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
            ocmd.CommandTimeout = 60000;
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
    public string Execute_SP(string sSPName, SqlParameter[] sqlParamColl, SqlParameter sqlParamOutput)
    {
        int cnt = 0;
        string sOutput = "";
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlTransaction trans = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQLL"].ConnectionString;
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

    public DataSet FillDataSet_SP(string sSPName, SqlParameter[] sqlParamColl)
    {
        SqlConnection cnn = null;
        SqlCommand cmd = null;
        SqlDataAdapter adap = null;
        DataSet ds = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrSQLL"].ConnectionString;
        try
        {
            cnn = new SqlConnection(connString);
            cnn.Open();
            ds = new DataSet();
            cmd = new SqlCommand(sSPName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 600000;
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
    private void addParamsSTR(SqlCommand oCmmd, string sName, string sValue, SqlDbType sDBType, int sLen, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType, sLen));
        oCmmd.Parameters[sName].Value = sValue;
        oCmmd.Parameters[sName].Direction = sDirection;
    }
    private void addOutPutParams(SqlCommand oCmmd, string sName, SqlDbType sDBType, ParameterDirection sDirection)
    {
        oCmmd.Parameters.Add(new SqlParameter(sName, sDBType));
        //oCmmd.Parameters[sName].Value = sValue;
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
            ocmd.CommandTimeout = 1000000;
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
            ocmd.CommandTimeout = 1000000;
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
            ocmd.CommandTimeout = 1000000;
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
    public DataSet ExcProcedure1(string sProcName, string[,] sparameter, CommandType CmdType)
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
            ocmd.CommandTimeout = 1000000;
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

}
