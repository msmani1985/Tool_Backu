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
/// Summary description for Attendance
/// </summary>
public class Attendance
{
    SqlConnection oConn;
    string sConStr = "";
    SqlCommand ocmd;
    SqlTransaction oTran;
    DataSet ds;
	public Attendance()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private void opencon()
    {
        sConStr = ConfigurationManager.ConnectionStrings["conStrAtt"].ConnectionString;
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
    public DataSet GetEmployeeByName(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        DataSet ds = new DataSet();
        ds = ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
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
            ocmd.CommandTimeout = 2000;
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
    public EmpAtt EmpAttDetails(string qry, string[,] param)
    {
        EmpAtt prods = new EmpAtt();
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
    public Attendances AttendanceDetails(string qry, string[,] param)
    {
        Attendances LaDS = new Attendances();
        SqlDataAdapter proDA = null;
        ocmd = new SqlCommand();
        try
        {
            opencon();

            proDA = new SqlDataAdapter(qry, oConn);
            proDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            proDA.SelectCommand.CommandTimeout = 6000;
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
    public DataSet GetEmployeeTime(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        DataSet ds = new DataSet();
        ds = ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
    }
    public DataSet GetEmployeeLateMins(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        DataSet ds = new DataSet();
        ds = ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
    }
    public DataSet EmpLateYTD(string ID)
    {
        return ExcProcedure("spGetYTDEmpLate", new string[,] { { "@Empid", ID.ToString() } }, CommandType.StoredProcedure);
    }
    public DataSet EmpLeaveYTD(string ID)
    {
        return ExcProcedure("spGetYTDAttLeave", new string[,] { { "@Empid", ID.ToString() } }, CommandType.StoredProcedure);
    }
    public DataSet EmpAttDaily(string ID, string sdate, string edate)
    {
        return ExcProcedure("spGetCosecAttDaily", new string[,] { { "@UserID", ID.ToString() }, { "@sdate", sdate.ToString() }, { "@edate", edate.ToString() } }, CommandType.StoredProcedure);
    }

    public DataSet GetAttPunch(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        DataSet ds = new DataSet();
        ds = ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
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

    public bool Update_InOut(System.Collections.ArrayList al)
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
                    umodule = "update Mx_AtdPunch set IOtype='"
                    + ht["IOtype"].ToString()
                    + "' where Userid='" + ht["Userid"].ToString() + "' and Pdate='" + ht["Pdate"].ToString() + "' and Edatetime='" + ht["Edatetime"].ToString() + "'";
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
    public DataSet EmpInOut(string UserID, string Pdate, string edate)
    {
        return ExcProcedure("spGetInvdInOutPunchs", new string[,] { { "@UserID", UserID.ToString() }, { "@Pdate", Pdate.ToString() }, { "@edate", edate.ToString() } }, CommandType.StoredProcedure);
    }
}
