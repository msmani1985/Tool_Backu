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
/// Customer Base:
/// Created by: Royson
/// Creation Date: 23 Jan 09
/// </summary>
 * */
public class CustomerBase
{
    private datasourceSQL oSql;
    private string sSql = "";
    public CustomerBase() { oSql = new datasourceSQL(); }

    /*
     * Select Customer, office, Financial details
     * 
     */
    public DataSet getAllCustomers()
    {
        sSql = "";
        DataSet ds = new DataSet();
        //SqlParameter[] para = new SqlParameter[1];
        try
        {
            sSql = "spGetCustomers";
            ds = oSql.FillDataSet_SP(sSql,null);
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
    public DataSet getCustomerByID(string sCustID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetCustomers";
            param[0] = oSql.NewParameter("@CustomerID", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCustomerDetailsByID(string sCustID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetCustomerDetails";
            param[0] = oSql.NewParameter("@CustomerId", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCustomerDetailsByID(string sCustID, string sOfficesiteID, string sFinsiteID)
    {
        DataSet ds = new DataSet();
        sSql = "";
        SqlParameter[] param = new SqlParameter[3];
        try
        {
            sSql = "spGetCustomerDetails";
            param[0] = oSql.NewParameter("@CustomerId", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
            param[1] = oSql.NewParameter("@off_contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sOfficesiteID);
            param[2] = oSql.NewParameter("@fin_contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sFinsiteID);
            ds = oSql.FillDataSet_SP(sSql, param);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    public DataSet getCustomerGroups()
    {
        sSql = "";
        DataSet ds = new DataSet();
        //SqlParameter[] para = new SqlParameter[1];
        try
        {
            sSql = "spGetCustomerGroups";
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
    public DataSet getCurrencyTypes()
    {
        sSql = "";
        DataSet ds = new DataSet();
        //SqlParameter[] para = new SqlParameter[1];
        try
        {
            sSql = "spGetCurrencyType";
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
    public DataSet getCountries()
    {
        sSql = "";
        DataSet ds = new DataSet();
        //SqlParameter[] para = new SqlParameter[1];
        try
        {
            sSql = "spGetCountry";
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
    public DataSet getOffSiteByCustomer(string sCustID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];        
        try
        {
            sSql = "spGetOfficeSite";
            param[0] = oSql.NewParameter("@CustomerId", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
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
    public DataSet getFinSiteByCustomer(string sCustID)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "spGetFinancialSite";
            param[0] = oSql.NewParameter("@CustomerId", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
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
    public DataSet getCustomerReferences(string sCustomerID, string sIBCustno)
    {
        sSql = "";
        DataSet ds = new DataSet();
        SqlParameter[] param = new SqlParameter[2];
        try
        {
            sSql = "[spGetCustomerRef]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sCustomerID);
            param[1] = oSql.NewParameter("@custno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, sIBCustno);
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

    /*
     * Insert, Update ,Delete Customer details
     * 
     */
    public string InsertCustomer(string[] aCustomer)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[49];
        try
        {
            sSql = "spInsertCustomer";
            param[0] = oSql.NewParameter("@cust_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[0]);
            param[1] = oSql.NewParameter("@cust_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[1]);
            param[2] = oSql.NewParameter("@cust_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[2]);
            param[3] = oSql.NewParameter("@cust_pdf_enabled", SqlDbType.Char, 1, ParameterDirection.Input, aCustomer[3]);
            param[4] = oSql.NewParameter("@currency_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[4]);
           
            param[5] = oSql.NewParameter("@employee_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[5]);
            param[6] = oSql.NewParameter("@cust_group_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[6]);
            //Office
            param[7] = oSql.NewParameter("@off_contact_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[7]);
            param[8] = oSql.NewParameter("@off_contact_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "8");
            param[9] = oSql.NewParameter("@off_address_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "1");
            param[10] = oSql.NewParameter("@off_address1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[8]);
            param[11] = oSql.NewParameter("@off_address2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[9]);
            param[12] = oSql.NewParameter("@off_address3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[10]);
            param[13] = oSql.NewParameter("@off_address4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[11]);
            param[14] = oSql.NewParameter("@off_address5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[12]);
            param[15] = oSql.NewParameter("@off_pincode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[13]);
            param[16] = oSql.NewParameter("@off_city", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[14]);
            param[17] = oSql.NewParameter("@off_state", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[15]);
            param[18] = oSql.NewParameter("@off_country_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[16]);
            param[19] = oSql.NewParameter("@off_phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[17]);
            param[20] = oSql.NewParameter("@off_phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[18]);
            param[21] = oSql.NewParameter("@off_phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[19]);
            param[22] = oSql.NewParameter("@off_fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[20]);
            param[23] = oSql.NewParameter("@off_fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[21]);
            param[24] = oSql.NewParameter("@off_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[22]);
            param[25] = oSql.NewParameter("@off_weburl", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[23]);
            //Finance
            param[26] = oSql.NewParameter("@fin_contact_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[24]);
            param[27] = oSql.NewParameter("@fin_contact_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "9");
            param[28] = oSql.NewParameter("@fin_address_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "2");
            param[29] = oSql.NewParameter("@fin_address1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[25]);
            param[30] = oSql.NewParameter("@fin_address2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[26]);
            param[31] = oSql.NewParameter("@fin_address3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[27]);
            param[32] = oSql.NewParameter("@fin_address4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[28]);
            param[33] = oSql.NewParameter("@fin_address5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[29]);
            param[34] = oSql.NewParameter("@fin_pincode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[30]);
            param[35] = oSql.NewParameter("@fin_city", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[31]);
            param[36] = oSql.NewParameter("@fin_state", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[32]);
            param[37] = oSql.NewParameter("@fin_country_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[33]);
            param[38] = oSql.NewParameter("@fin_phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[34]);
            param[39] = oSql.NewParameter("@fin_phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[35]);
            param[40] = oSql.NewParameter("@fin_phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[36]);
            param[41] = oSql.NewParameter("@fin_fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[37]);
            param[42] = oSql.NewParameter("@fin_fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[38]);
            param[43] = oSql.NewParameter("@fin_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[39]);
            param[44] = oSql.NewParameter("@fin_comodityno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[40]);
            param[45] = oSql.NewParameter("@fin_vatno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[41]);
            param[46] = oSql.NewParameter("@fin_login", SqlDbType.NChar, int.MaxValue, ParameterDirection.Input, aCustomer[42]);
            param[47] = oSql.NewParameter("@fin_password", SqlDbType.NChar, int.MaxValue, ParameterDirection.Input, aCustomer[43]);
            //Invoice bank details
            param[48] = oSql.NewParameter("@Invoice_bank_details", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[44]);
            Status = oSql.Execute_SP(sSql, param,
               oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string UpdateCustomer(string[] aCustomer,string sCustID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[52];
        try
        {
            sSql = "spUpdateCustomer";
            param[0] = oSql.NewParameter("@cust_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[0]);
            param[1] = oSql.NewParameter("@cust_code", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[1]);
            param[2] = oSql.NewParameter("@cust_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[2]);
            param[3] = oSql.NewParameter("@cust_pdf_enabled", SqlDbType.Char, 1, ParameterDirection.Input, aCustomer[3]);
            param[4] = oSql.NewParameter("@currency_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[4]);
            param[5] = oSql.NewParameter("@employee_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[5]);
            param[6] = oSql.NewParameter("@cust_group_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[6]);            
            //Office
            param[7] = oSql.NewParameter("@off_contact_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[7]);
            param[8] = oSql.NewParameter("@off_contact_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "8");
            param[9] = oSql.NewParameter("@off_address_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "1");
            param[10] = oSql.NewParameter("@off_address1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[8]);
            param[11] = oSql.NewParameter("@off_address2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[9]);
            param[12] = oSql.NewParameter("@off_address3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[10]);
            param[13] = oSql.NewParameter("@off_address4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[11]);
            param[14] = oSql.NewParameter("@off_address5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[12]);
            param[15] = oSql.NewParameter("@off_pincode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[13]);
            param[16] = oSql.NewParameter("@off_city", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[14]);
            param[17] = oSql.NewParameter("@off_state", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[15]);
            param[18] = oSql.NewParameter("@off_country_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[16]);
            param[19] = oSql.NewParameter("@off_phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[17]);
            param[20] = oSql.NewParameter("@off_phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[18]);
            param[21] = oSql.NewParameter("@off_phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[19]);
            param[22] = oSql.NewParameter("@off_fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[20]);
            param[23] = oSql.NewParameter("@off_fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[21]);
            param[24] = oSql.NewParameter("@off_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[22]);
            param[25] = oSql.NewParameter("@off_weburl", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[23]);
            //Finance
            param[26] = oSql.NewParameter("@fin_contact_name", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[24]);
            param[27] = oSql.NewParameter("@fin_contact_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "9");
            param[28] = oSql.NewParameter("@fin_address_type_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, "2");
            param[29] = oSql.NewParameter("@fin_address1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[25]);
            param[30] = oSql.NewParameter("@fin_address2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[26]);
            param[31] = oSql.NewParameter("@fin_address3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[27]);
            param[32] = oSql.NewParameter("@fin_address4", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[28]);
            param[33] = oSql.NewParameter("@fin_address5", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[29]);
            param[34] = oSql.NewParameter("@fin_pincode", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[30]);
            param[35] = oSql.NewParameter("@fin_city", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[31]);
            param[36] = oSql.NewParameter("@fin_state", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[32]);
            param[37] = oSql.NewParameter("@fin_country_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[33]);
            param[38] = oSql.NewParameter("@fin_phone1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[34]);
            param[39] = oSql.NewParameter("@fin_phone2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[35]);
            param[40] = oSql.NewParameter("@fin_phone3", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[36]);
            param[41] = oSql.NewParameter("@fin_fax1", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[37]);
            param[42] = oSql.NewParameter("@fin_fax2", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[38]);
            param[43] = oSql.NewParameter("@fin_email", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[39]);
            param[44] = oSql.NewParameter("@fin_comodityno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[40]);
            param[45] = oSql.NewParameter("@fin_vatno", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, aCustomer[41]);
            param[46] = oSql.NewParameter("@fin_login", SqlDbType.NChar, int.MaxValue, ParameterDirection.Input, aCustomer[42]);
            param[47] = oSql.NewParameter("@fin_password", SqlDbType.NChar, int.MaxValue, ParameterDirection.Input, aCustomer[43]);
            //keys
            param[48] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
            param[49] = oSql.NewParameter("@off_contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[44]);
            param[50] = oSql.NewParameter("@fin_contact_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[45]);
            //Invoice bank details
            param[51] = oSql.NewParameter("@Invoice_bank_details", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, aCustomer[46]);

            Status = oSql.Execute_SP(sSql, param,
                oSql.NewParameter("@Output", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Output, null));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return Status;
    }
    public string DeleteCustomer(string sCustID)
    {
        string Status = "";
        SqlParameter[] param = new SqlParameter[1];
        try
        {
            sSql = "[spDeleteCustomer]";
            param[0] = oSql.NewParameter("@customer_id", SqlDbType.Int, int.MaxValue, ParameterDirection.Input, sCustID);
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
