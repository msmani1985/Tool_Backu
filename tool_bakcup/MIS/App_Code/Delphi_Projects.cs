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
/// Summary description for Delphi_Projects
/// </summary>
public class Delphi_Projects
{
    private Delphi_Customer oCust;
    private Delphi_Common oCom;
    private datasourceIBSQL oSql;
    private string sSql = "";
	public Delphi_Projects()
	{
        oCust = new Delphi_Customer();
        oCom = new Delphi_Common();
        oSql = new datasourceIBSQL();
	}
    public DataSet getCustomers() { return this.oCust.getAllCustomers(); }
    public DataSet getCusomerFinsite(string sCustID) { return this.oCust.getFinSiteByCustomer(sCustID); }
    public DataSet getPEName() { return this.oCust.getPEName(); }
    public DataSet getProjectStage() { return this.oCom.getProjectStage(); }
    public DataSet getTypeset() { return this.oCom.getProjectTypesetPlatform(); }

    public DataSet GetEmployeeName()
    {
        return oSql.GetDataSet("select empno,rtrim(ltrim(EMP_FNAME)) + ' ' + rtrim(ltrim(EMP_SNAME)) from employee_dp ", "Employee", CommandType.Text);
    }
    public DataSet GetDepartment()
    {
        return oSql.GetDataSet("SELECT * FROM DEPARTMENT_DP  ORDER BY DNAME", "DEPARTMENT", CommandType.Text);
    }
    public DataSet GetProductCode()
    {
        return oSql.GetDataSet("SELECT * FROM DIGITALPRODUCTS_DP ", "Product", CommandType.Text);
    }
    public DataSet getCurrentStatus()
    {
        sSql = "";
        DataSet ds = new DataSet();
        //SqlParameter[] para = new SqlParameter[1];
        try
        {
            sSql = "spGetCurrentStatus";
            ds = oSql.FillDataSet_SP(sSql, null);

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

    public DataSet getProject(string sProjectName, char cCompleteFlag, string sCustomerID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetProjects";
            param[0] = oSql.NewParameter("@Projectid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectName);
            param[1] = oSql.NewParameter("@Completed_flag", SqlDbType.Char, int.MaxValue, ParameterDirection.Input, cCompleteFlag);
            param[2] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
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
    public DataSet getProjectDetailsByID(string sProjectID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetProjectsByID";
            param[0] = oSql.NewParameter("@Projectid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectID);
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
    public DataSet getProjectArticleDetailsByID(string sProjectID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetProjectArticleByID";
            param[0] = oSql.NewParameter("@Projectid", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectID);
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
    public string InsertProject(string[] aProject)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[76];
        try
        {
            sSql = "[SpInsertProjects]";

            param[0] = oSql.NewParameter("@custno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[0]);
            param[1] = oSql.NewParameter("@finsiteno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[2] = oSql.NewParameter("@Pcode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[2]);
            param[3] = oSql.NewParameter("@PTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[3]);
            param[4] = oSql.NewParameter("@PRECEIVEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[4]);
            param[5] = oSql.NewParameter("@PDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[5]);
            param[6] = oSql.NewParameter("@PCOMPLETEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[6]);
            param[7] = oSql.NewParameter("@PADDITEMS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[7]);
            param[8] = oSql.NewParameter("@CONNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[8]);
            param[9] = oSql.NewParameter("@PFIRSTSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[9]);
            param[10] = oSql.NewParameter("@PFIRSTDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[10]);
            param[11] = oSql.NewParameter("@PFIRSTHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[11]);
            param[12] = oSql.NewParameter("@PFIRSTEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[12]);
            param[13] = oSql.NewParameter("@PFIRSTDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[13]);
            param[14] = oSql.NewParameter("@PSECONDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[14]);
            param[15] = oSql.NewParameter("@PSECONDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[15]);
            param[16] = oSql.NewParameter("@PSECONDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[16]);
            param[17] = oSql.NewParameter("@PSECONDEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[17]);
            param[18] = oSql.NewParameter("@PSECONDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[18]);
            param[19] = oSql.NewParameter("@PTHIRDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[19]);
            param[20] = oSql.NewParameter("@PTHIRDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[20]);
            param[21] = oSql.NewParameter("@PTHIRDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[21]);
            param[22] = oSql.NewParameter("@PTHIRDEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[22]);
            param[23] = oSql.NewParameter("@PTHIRDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[23]);
            param[24] = oSql.NewParameter("@PFOURTHSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[24]);
            param[25] = oSql.NewParameter("@PFOURTHDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[25]);
            param[26] = oSql.NewParameter("@PFOURTHHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[26]);
            param[27] = oSql.NewParameter("@PFOURTHEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[27]);
            param[28] = oSql.NewParameter("@PFOURTHDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[28]);
            param[29] = oSql.NewParameter("@PFINALSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[29]);
            param[30] = oSql.NewParameter("@PFINALDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[30]);
            param[31] = oSql.NewParameter("@PFINALHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[31]);
            param[32] = oSql.NewParameter("@PFINALDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[32]);
            param[33] = oSql.NewParameter("@PINVOICEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[33]);

            param[34] = oSql.NewParameter("@INVNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[34]);
            param[35] = oSql.NewParameter("@DIGITALPRODNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[35]);
            param[36] = oSql.NewParameter("@TPLATNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[36]);
            param[37] = oSql.NewParameter("@stno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[37]);
            param[38] = oSql.NewParameter("@STYPENO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[38]);
            param[39] = oSql.NewParameter("@EMPNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[39]);
            param[40] = oSql.NewParameter("@DNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[40]);
            param[41] = oSql.NewParameter("@PADDCHARGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[41]);
            param[42] = oSql.NewParameter("@PONUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[42]);
            param[43] = oSql.NewParameter("@PcreditCost", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[43]);
            param[44] = oSql.NewParameter("@PCNO_2010", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[44]);
            param[45] = oSql.NewParameter("@PINPUTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[45]);
            param[46] = oSql.NewParameter("@POUTPUTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[46]);
            param[47] = oSql.NewParameter("@PNOOFCHARACTERS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[47]);
            param[48] = oSql.NewParameter("@PISBN", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[48]);
            param[49] = oSql.NewParameter("@PCREDITED_IND", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[49]);
            param[50] = oSql.NewParameter("@PCREDITED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[50]);
            param[51] = oSql.NewParameter("@PNOOFCHARGEDPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[51]);
            param[52] = oSql.NewParameter("@PNOOFCHARGEDARTICLES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[52]);
            param[53] = oSql.NewParameter("@PDIGITAL", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[53]);
            param[54] = oSql.NewParameter("@PROJECTNUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[54]);
            param[55] = oSql.NewParameter("@PNOOFPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[55]);
            param[56] = oSql.NewParameter("@PACOSTDESC1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[56]);
            param[57] = oSql.NewParameter("@PAQTY1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[57]);
            param[58] = oSql.NewParameter("@PACNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[58]);
            param[59] = oSql.NewParameter("@PACOSTDESC2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[59]);
            param[60] = oSql.NewParameter("@PAQTY2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[60]);
            param[61] = oSql.NewParameter("@PACNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[61]);
            param[62] = oSql.NewParameter("@PACOSTDESC3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[62]);
            param[63] = oSql.NewParameter("@PAQTY3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[63]);
            param[64] = oSql.NewParameter("@PACNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[64]);
            param[65] = oSql.NewParameter("@PACOSTDESC4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[65]);
            param[66] = oSql.NewParameter("@PAQTY4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[66]);
            param[67] = oSql.NewParameter("@PACNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[67]);
            param[68] = oSql.NewParameter("@PACOSTDESC5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[68]);
            param[69] = oSql.NewParameter("@PAQTY5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[69]);
            param[70] = oSql.NewParameter("@PACNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[70]);
            param[71] = oSql.NewParameter("@LocationID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[71]);
            param[72] = oSql.NewParameter("@PCOST", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[72]);
            param[73] = oSql.NewParameter("@PDESCRIPTION", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[73]);
            param[74] = oSql.NewParameter("@PCOMMENTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[74]);
            param[75] = oSql.NewParameter("@Category", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[75]);

            Status = oSql.Execute_SP(sSql, param,
                    oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateProject(string[] aProject)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[76];
        try
        {
            sSql = "[SpUpdateProjects]";

            param[0] = oSql.NewParameter("@custno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[0]);
            param[1] = oSql.NewParameter("@finsiteno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[1]);
            param[2] = oSql.NewParameter("@Pcode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[2]);
            param[3] = oSql.NewParameter("@PTITLE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[3]);
            param[4] = oSql.NewParameter("@PRECEIVEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[4]);
            param[5] = oSql.NewParameter("@PDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[5]);
            param[6] = oSql.NewParameter("@PCOMPLETEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[6]);
            param[7] = oSql.NewParameter("@PADDITEMS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[7]);
            param[8] = oSql.NewParameter("@CONNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[8]);
            param[9] = oSql.NewParameter("@PFIRSTSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[9]);
            param[10] = oSql.NewParameter("@PFIRSTDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[10]);
            param[11] = oSql.NewParameter("@PFIRSTHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[11]);
            param[12] = oSql.NewParameter("@PFIRSTEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[12]);
            param[13] = oSql.NewParameter("@PFIRSTDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[13]);
            param[14] = oSql.NewParameter("@PSECONDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[14]);
            param[15] = oSql.NewParameter("@PSECONDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[15]);
            param[16] = oSql.NewParameter("@PSECONDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[16]);
            param[17] = oSql.NewParameter("@PSECONDEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[17]);
            param[18] = oSql.NewParameter("@PSECONDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[18]);
            param[19] = oSql.NewParameter("@PTHIRDSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[19]);
            param[20] = oSql.NewParameter("@PTHIRDDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[20]);
            param[21] = oSql.NewParameter("@PTHIRDHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[21]);
            param[22] = oSql.NewParameter("@PTHIRDEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[22]);
            param[23] = oSql.NewParameter("@PTHIRDDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[23]);
            param[24] = oSql.NewParameter("@PFOURTHSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[24]);
            param[25] = oSql.NewParameter("@PFOURTHDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[25]);
            param[26] = oSql.NewParameter("@PFOURTHHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[26]);
            param[27] = oSql.NewParameter("@PFOURTHEMPLOYEE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[27]);
            param[28] = oSql.NewParameter("@PFOURTHDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[28]);
            param[29] = oSql.NewParameter("@PFINALSTARTDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[29]);
            param[30] = oSql.NewParameter("@PFINALDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[30]);
            param[31] = oSql.NewParameter("@PFINALHALFDUEDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[31]);
            param[32] = oSql.NewParameter("@PFINALDISPATCH", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[32]);
            param[33] = oSql.NewParameter("@PINVOICEDDATE", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[33]);

            param[34] = oSql.NewParameter("@INVNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[34]);
            param[35] = oSql.NewParameter("@DIGITALPRODNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[35]);
            param[36] = oSql.NewParameter("@TPLATNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[36]);
            param[37] = oSql.NewParameter("@stno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[37]);
            param[38] = oSql.NewParameter("@STYPENO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[38]);
            param[39] = oSql.NewParameter("@EMPNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[39]);
            param[40] = oSql.NewParameter("@DNO", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[40]);
            param[41] = oSql.NewParameter("@PADDCHARGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[41]);
            param[42] = oSql.NewParameter("@PONUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[42]);
            param[43] = oSql.NewParameter("@PcreditCost", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[43]);
            param[44] = oSql.NewParameter("@PCNO_2010", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[44]);
            param[45] = oSql.NewParameter("@PINPUTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[45]);
            param[46] = oSql.NewParameter("@POUTPUTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[46]);
            param[47] = oSql.NewParameter("@PNOOFCHARACTERS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[47]);
            param[48] = oSql.NewParameter("@PISBN", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[48]);
            param[49] = oSql.NewParameter("@PCREDITED_IND", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[49]);
            param[50] = oSql.NewParameter("@PCREDITED", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[50]);
            param[51] = oSql.NewParameter("@PNOOFCHARGEDPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[51]);
            param[52] = oSql.NewParameter("@PNOOFCHARGEDARTICLES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[52]);
            param[53] = oSql.NewParameter("@PDIGITAL", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[53]);
            param[54] = oSql.NewParameter("@PROJECTNUMBER", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[54]);
            param[55] = oSql.NewParameter("@PNOOFPAGES", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[55]);
            param[56] = oSql.NewParameter("@PACOSTDESC1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[56]);
            param[57] = oSql.NewParameter("@PAQTY1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[57]);
            param[58] = oSql.NewParameter("@PACNO1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[58]);
            param[59] = oSql.NewParameter("@PACOSTDESC2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[59]);
            param[60] = oSql.NewParameter("@PAQTY2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[60]);
            param[61] = oSql.NewParameter("@PACNO2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[61]);
            param[62] = oSql.NewParameter("@PACOSTDESC3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[62]);
            param[63] = oSql.NewParameter("@PAQTY3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[63]);
            param[64] = oSql.NewParameter("@PACNO3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[64]);
            param[65] = oSql.NewParameter("@PACOSTDESC4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[65]);
            param[66] = oSql.NewParameter("@PAQTY4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[66]);
            param[67] = oSql.NewParameter("@PACNO4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[67]);
            param[68] = oSql.NewParameter("@PACOSTDESC5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[68]);
            param[69] = oSql.NewParameter("@PAQTY5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[69]);
            param[70] = oSql.NewParameter("@PACNO5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[70]);
            param[71] = oSql.NewParameter("@LocationID", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[71]);
            param[72] = oSql.NewParameter("@PCOST", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[72]);
            param[73] = oSql.NewParameter("@PDESCRIPTION", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[73]);
            param[74] = oSql.NewParameter("@PCOMMENTS", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[74]);
            param[75] = oSql.NewParameter("@Category", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aProject[75]);

            Status = oSql.Execute_SP(sSql, param,
                    oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public DataSet getProjectEvents(string sProjectID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[SpGetProjectLogEvents]";
            param[0] = oSql.NewParameter("@Projectno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sProjectID);
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
    public DataSet chkPoNumber(string PONUMBER)
    {
        return oSql.GetDataSet("SELECT * FROM PROJECTS_DP where PONUMBER like '%" + PONUMBER + "%'", "Product", CommandType.Text);
    }
}