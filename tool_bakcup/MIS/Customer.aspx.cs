using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Customer : System.Web.UI.Page
{
    datasourceSQL DSQL = new datasourceSQL();
    datasourceIBSQL ibSQL = new datasourceIBSQL();
     DataSet dsCustomerDetails= new DataSet();
     DataSet dsCustomerDtls = new DataSet();
     SqlConnection oConn = null;
     string sConStr = "";
     SqlCommand ocmd;
     SqlTransaction oTran;
    #region "Form Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
               GetCustomer();
               GetCustomerOfficeDtl(0);
               bindCurrency();
               bindCountry();
               bindGroupBy();
               hdnMode.Value = "0";
               hdnFinancialMode.Value = "0";
               hdnOfficeMode.Value = "0";
            }
            txtVatNo.MaxLength = 20;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        finally
        { 
        
        }
        
    }
  #endregion

    #region "Control Events"
    protected void btnOfficialClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblError.Text = "";
            hdnOfficeMode.Value = "0";
            drpOffice.Items.Clear();
            clearOfficialDtl();
            GetCustomerOfficeDtl(Convert.ToInt32(drpCustomer.SelectedValue));
            bindOfficeName();
            bindCountry();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on clear the official details.";
        }
    }
    protected void btnFinancialClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblError.Text = "";
            hdnFinancialMode.Value = "0";
            drpFinancial.Items.Clear();
            clearFinancialDtl();
            GetCustomerOfficeDtl(Convert.ToInt32(drpCustomer.SelectedValue));
            bindFinancialName();
            bindGroupBy();

        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on clear the financial details.";
        }
    } 
    protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            drpOffice.Items.Clear();
            drpFinancial.Items.Clear();
            clearCustomerDtl();
            clearOfficialDtl();
            clearFinancialDtl();
            if (Convert.ToInt32(drpCustomer.SelectedValue) != 0)
            {
                hdnMode.Value = "1";
                hdnOfficeMode.Value = "1";
                hdnFinancialMode.Value = "1";
                GetCustomerOfficeDtl(Convert.ToInt32(drpCustomer.SelectedValue));
                FillCustomerDetails();
            }
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on loading customer details.";
        }
    }
    protected void btnCustomerClear_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            lblError.Text = "";
            hdnMode.Value = "0";
            hdnOfficeMode.Value = "0";
            hdnFinancialMode.Value = "0";
            drpCustomer.Items.Clear();
            drpOffice.Items.Clear();
            drpFinancial.Items.Clear();
            clearCustomerDtl();
            clearOfficialDtl();
            clearFinancialDtl();
            GetCustomer();
            GetCustomerOfficeDtl(0);
            bindCurrency();
            bindCountry();
            bindGroupBy();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on clear the customer details.";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            lblError.Text = "";
            clear();
            hdnMode.Value = "0";
            hdnFinancialMode.Value = "0";
            hdnOfficeMode.Value = "0";
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on clear the customer details.";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        dsCustomerDtls = new DataSet();
        try
        {
            lblError.Text = "";
            addCustomerColumns();
            addFinancialSiteColumns();
            addContactSiteColumns();
			 if (Convert.ToInt32(hdnMode.Value) == 0)
            {
                datasourceIBSQL ibSql = new datasourceIBSQL();
                DataSet dsCustCheck = new DataSet();
                dsCustCheck= ibSql.GetCustomerCheck(Convert.ToString(txtCustomer.Text).Trim());
                if (dsCustCheck != null)
                {
                    if (dsCustCheck.Tables.Count > 0)
                    {
                        if (dsCustCheck.Tables[0].Rows.Count > 0)
                        {
                            lblError.Text = "Error : Customer already exist, Can't add the customer.";
                            return;
                        }
                    }
                }
            }
            assignValuesToTable(Convert.ToInt32(hdnMode.Value), Convert.ToInt32(hdnOfficeMode.Value), Convert.ToInt32(hdnFinancialMode.Value));


        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on saving the customer details.";
        }
    }
    protected void drpOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            hdnOfficeMode.Value = "1";
            fillOfficeDetails();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on loading the official details.";
        }
    }
    protected void drpFinancial_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            hdnFinancialMode.Value = "1";
            fillFinancialDetails();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error on loading the finance details.";
        }
    }
    #endregion

    #region "User Defined Function"
        private void clear()
        {
            try
            {
                drpCustomer.Items.Clear();
                drpOffice.Items.Clear();
                drpFinancial.Items.Clear();
                clearCustomerDtl();
                clearOfficialDtl();
                clearFinancialDtl();
                GetCustomer();
                GetCustomerOfficeDtl(0);
                bindCurrency();
                bindCountry();
                bindGroupBy();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetCustomer()
        {
            DataSet dsCustomer = new DataSet();
            DSQL = new datasourceSQL();
            try
            {
                dsCustomer = DSQL.GetCustomer();
                drpCustomer.DataSource = null;
                if (dsCustomer != null)
                {
                    if (dsCustomer.Tables.Contains("CUSTOMER_DP"))
                    {
                        drpCustomer.DataSource = dsCustomer;
                        drpCustomer.DataTextField = "CUSTNAME";
                        drpCustomer.DataValueField = "CUSTNO";
                        drpCustomer.DataBind();
                        drpCustomer.Items.Insert(0, new ListItem("------------------Select Customer------------------", "0"));
                        //ddl_department.Items.Insert(0, new ListItem("--select--", "0"));

                    }
                }


            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                dsCustomer = null;
            }
        }
        private void bindOfficeName()
        {
            try
            {
                if (dsCustomerDetails.Tables.Contains("CONTACTSITE_DP"))
                {
                    drpOffice.DataSource = dsCustomerDetails.Tables["CONTACTSITE_DP"];
                    drpOffice.DataTextField = "CONSITENAME";
                    drpOffice.DataValueField = "CONSITENO";
                    drpOffice.DataBind();
                    drpOffice.Items.Insert(0, new ListItem("------------Select Office-------------", "0"));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void bindFinancialName()
        {
            try
            {
                if (dsCustomerDetails.Tables.Contains("FINANCIALSITE_DP"))
                {
                    drpFinancial.DataSource = dsCustomerDetails.Tables["FINANCIALSITE_DP"];
                    drpFinancial.DataTextField = "FINSITENAME";
                    drpFinancial.DataValueField = "FINSITENO";
                    drpFinancial.DataBind();
                    drpFinancial.Items.Insert(0, new ListItem("-----Select Financial Name------", "0"));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void bindCurrency()
        {
            try
            {
                if (dsCustomerDetails.Tables.Contains("CURRENCY_DP"))
                {
                    drpCurrency.DataSource = dsCustomerDetails.Tables["CURRENCY_DP"];
                    drpCurrency.DataTextField = "CURRNAME";
                    drpCurrency.DataValueField = "CURRNO";
                    drpCurrency.DataBind();
                    drpCurrency.Items.Insert(0, new ListItem("---Select Currency---", "0"));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        private void bindCountry()
        {
            try
            {
                if (dsCustomerDetails.Tables.Contains("COUNTRY_DP"))
                {
                    drpCountry.DataSource = dsCustomerDetails.Tables["COUNTRY_DP"];
                    drpCountry.DataTextField = "CTYNAME";
                    drpCountry.DataValueField = "CTYNO";
                    drpCountry.DataBind();
                    drpCountry.Items.Insert(0, new ListItem("---Select Country---", "0"));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void bindGroupBy()
        {
            try
            {
                if (dsCustomerDetails.Tables.Contains("GROUPBY"))
                {
                    drpGroup.DataSource = dsCustomerDetails.Tables["GROUPBY"];
                    drpGroup.DataTextField = "GROUPBY";
                    drpGroup.DataValueField = "GROUPBY";
                    drpGroup.DataBind();
                    drpGroup.Items.Insert(0, new ListItem("---Select Team/Group---", "0"));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void bindCustomerDetails()
        {
            try
            {
                if (dsCustomerDetails.Tables.Contains("CUSTOMER_DETAILS"))
                {
                    DataRow[] drCustDetails = dsCustomerDetails.Tables["CUSTOMER_DETAILS"].Select("");
                    if (drCustDetails.Count() == 0)
                    {
                        return;
                    }
                    txtCustomer.Text = Convert.ToString(drCustDetails[0]["CUSTNAME"]).Trim();
                    txtEmail.Text = Convert.ToString(drCustDetails[0]["CUSTEMAIL"]).Trim();
                    txtBookPrev.Text = Convert.ToString(drCustDetails[0]["BYEARLY_BUDGET_PREV"]).Trim();
                    txtProjectPrev.Text = Convert.ToString(drCustDetails[0]["pyearly_budget_prev"]).Trim();
                    txtPagesPrev.Text = Convert.ToString(drCustDetails[0]["bpagebudget_prev"]).Trim();
                    txtNoofBooksPrev.Text = Convert.ToString(drCustDetails[0]["bnobudget_prev"]).Trim();
                    txtNoofProjectPrev.Text = Convert.ToString(drCustDetails[0]["pnobudget_prev"]).Trim();
                    txtCreatedOn.Text = Convert.ToString(drCustDetails[0]["custcreationdate"]).Trim();
                    if (Convert.ToString(drCustDetails[0]["currno"]).Trim() == "")
                    {
                        drpCurrency.SelectedValue = "0";
                    }
                    else
                    {
                        drpCurrency.SelectedValue = Convert.ToString(drCustDetails[0]["currno"]);
                    }

                    txtBookCurrent.Text = Convert.ToString(drCustDetails[0]["byearly_budget_curr"]).Trim();
                    txtProjectCurrent.Text = Convert.ToString(drCustDetails[0]["pyearly_budget_curr"]).Trim();
                    txtPagesCurrent.Text = Convert.ToString(drCustDetails[0]["bpagebudget_curr"]).Trim();
                    txtNoofBooksCurrent.Text = Convert.ToString(drCustDetails[0]["bnobudget_curr"]).Trim();
                    txtNoofProjectCurrent.Text = Convert.ToString(drCustDetails[0]["pnobudget_curr"]).Trim();
                    if (Convert.ToString(drCustDetails[0]["custpdfenabled"]).Trim() == "")
                    {
                        drpPdfEnabled.SelectedValue = "0";
                    }
                    else
                    {
                        drpPdfEnabled.SelectedValue = Convert.ToString(drCustDetails[0]["custpdfenabled"]);
                    }

                    txtFolderName.Text = Convert.ToString(drCustDetails[0]["dpserver_custname"]).Trim();
                    txtBookNext.Text = Convert.ToString(drCustDetails[0]["byearly_budget_next"]).Trim();
                    txtProjectNext.Text = Convert.ToString(drCustDetails[0]["pyearly_budget_next"]).Trim();
                    txtPagesNext.Text = Convert.ToString(drCustDetails[0]["bpagebudget_next"]).Trim();
                    txtNoofBooksNext.Text = Convert.ToString(drCustDetails[0]["bnobudget_next"]).Trim();
                    txtNoofProjectNext.Text = Convert.ToString(drCustDetails[0]["pnobudget_next"]).Trim();

                    drpOffice.SelectedValue = Convert.ToString(drCustDetails[0]["consiteno"]);
                    txtOfficeName.Text = Convert.ToString(drCustDetails[0]["consitename"]).Trim();
                    txtAddress.Text = Convert.ToString(drCustDetails[0]["conaddress"]).Trim();
                    txtPinCode.Text = Convert.ToString(drCustDetails[0]["conpocode"]).Trim();
                    txtPhone.Text = Convert.ToString(drCustDetails[0]["conphone"]).Trim();
                    txtFax.Text = Convert.ToString(drCustDetails[0]["confax"]).Trim();
                    txtLocation.Text = Convert.ToString(drCustDetails[0]["conoffice"]).Trim();
                    txtCity.Text = Convert.ToString(drCustDetails[0]["concity"]).Trim();
                    if (Convert.ToString(drCustDetails[0]["concountry"]).Trim() == "")
                    {
                        drpCountry.SelectedValue = "0";
                    }
                    else
                    {
                        drpCountry.SelectedValue = Convert.ToString(drCustDetails[0]["concountry"]).Trim();
                    }

                    txtState.Text = Convert.ToString(drCustDetails[0]["constate"]).Trim();
                    txtWebSite.Text = Convert.ToString(drCustDetails[0]["conweb"]).Trim();

                    drpFinancial.SelectedValue = Convert.ToString(drCustDetails[0]["finsiteno"]);
                    txtFinancialName.Text = Convert.ToString(drCustDetails[0]["finsitename"]).Trim();
                    txtFinAddress.Text = Convert.ToString(drCustDetails[0]["finsiteaddress1"]).Trim();
                    txtFinAddress1.Text = Convert.ToString(drCustDetails[0]["finsiteaddress2"]).Trim();
                    txtFinAddress2.Text = Convert.ToString(drCustDetails[0]["finsiteaddress3"]).Trim();
                    txtFinAddress3.Text = Convert.ToString(drCustDetails[0]["finsiteaddress4"]).Trim();
                    txtFinAddress4.Text = Convert.ToString(drCustDetails[0]["finsiteaddress5"]).Trim();
                    txtFinAddress5.Text = Convert.ToString(drCustDetails[0]["finsiteaddress6"]).Trim();
                    txtFinPhone.Text = Convert.ToString(drCustDetails[0]["finsitephone1"]).Trim();
                    if (Convert.ToString(drCustDetails[0]["groupby"]).Trim() == "")
                    {
                        drpGroup.SelectedValue = "0";
                    }
                    else
                    {
                        drpGroup.SelectedValue = Convert.ToString(drCustDetails[0]["groupby"]);
                    }

                    txtFinfax.Text = Convert.ToString(drCustDetails[0]["finsitefax"]).Trim();
                    txtFinEmail.Text = Convert.ToString(drCustDetails[0]["finsitemail"]).Trim();
                    txtCommNo.Text = Convert.ToString(drCustDetails[0]["finsitecommodityno"]).Trim();
                    txtVatNo.Text = Convert.ToString(drCustDetails[0]["finsitevatno"]).Trim();
                    txtLogin.Text = Convert.ToString(drCustDetails[0]["custlogin"]).Trim();
                    txtPassword.Text = Convert.ToString(drCustDetails[0]["custpass"]).Trim();
                    txtLedgerId.Text = Convert.ToString(drCustDetails[0]["ledgerid"]).Trim();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetCustomerOfficeDtl(int intCustNo)
        {
            DSQL = new datasourceSQL();
            try
            {
                dsCustomerDetails = DSQL.GetCustomerDetails(Convert.ToString(intCustNo));
                

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillCustomerDetails()
        {
            try
            {
                if (dsCustomerDetails != null)
                {

                    bindOfficeName();

                    bindFinancialName();

                    bindCurrency();

                    bindCountry();

                    bindGroupBy();

                    bindCustomerDetails();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void clearCustomerDtl()
        {
            try
            {
                txtCustomer.Text = "";
                txtEmail.Text = "";
                txtBookPrev.Text = "";
                txtProjectPrev.Text = "";
                txtPagesPrev.Text = "";
                txtNoofBooksPrev.Text = "";
                txtNoofProjectPrev.Text = "";
                txtCreatedOn.Text = "";
                drpCurrency.Items.Clear();
                txtBookCurrent.Text = "";
                txtProjectCurrent.Text = "";
                txtPagesCurrent.Text = "";
                txtNoofBooksCurrent.Text = "";
                txtNoofProjectCurrent.Text = "";
                drpPdfEnabled.SelectedIndex = 0;
                txtFolderName.Text = "";
                txtBookNext.Text = "";
                txtProjectNext.Text = "";
                txtPagesNext.Text = "";
                txtNoofBooksNext.Text = "";
                txtNoofProjectNext.Text = "";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void clearOfficialDtl()
        {
            try
            {
                txtOfficeName.Text = "";
                txtAddress.Text = "";
                txtPinCode.Text = "";
                txtPhone.Text = "";
                txtFax.Text = "";
                txtLocation.Text = "";
                txtCity.Text = "";
                drpCountry.Items.Clear();
                txtState.Text = "";
                txtWebSite.Text = "";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void clearFinancialDtl()
        {
            try
            {
                txtFinancialName.Text = "";
                txtFinAddress.Text = "";
                txtFinAddress1.Text = "";
                txtFinAddress2.Text = "";
                txtFinAddress3.Text = "";
                txtFinAddress4.Text = "";
                txtFinAddress5.Text = "";
                txtFinPhone.Text = "";
                drpGroup.Items.Clear();
                txtFinfax.Text = "";
                txtFinEmail.Text = "";
                txtCommNo.Text = "";
                txtVatNo.Text = "";
                txtLogin.Text = "";
                txtPassword.Text = "";
                txtLedgerId.Text = "";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void addCustomerColumns()
        {
            DataTable dtCUSTOMER_DP = new DataTable("CUSTOMER_DP");
            try
            {
                dtCUSTOMER_DP.Columns.Add("CUSTNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CUSTNAME", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTADDRESS1", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTADDRESS2", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTADDRESS3", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTADDRESS4", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTADDRESS5", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTADDRESS6", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTPHONE1", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTPHONE2", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTPHONE3", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTFAX1", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTFAX2", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTSTARTDATE", typeof(DateTime));
                dtCUSTOMER_DP.Columns.Add("CUSTVATNO", typeof(string));
                dtCUSTOMER_DP.Columns.Add("PUBCNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CCATNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CTYNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CUSTCODE", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CSCHNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CUSTCREATIONDATE", typeof(DateTime));
                dtCUSTOMER_DP.Columns.Add("CURRNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CURRTAT", typeof(int));
                dtCUSTOMER_DP.Columns.Add("FINSITENO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CUSTPDFENABLED", typeof(string));
                dtCUSTOMER_DP.Columns.Add("DPSERVER_CUSTNAME", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CUSTEMAIL", typeof(string));
                dtCUSTOMER_DP.Columns.Add("CBCNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("CONMAINNO", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2006", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2006", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2007", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2007", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2008", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2008", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2009", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2009", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2006", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2007", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2008", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2006", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2007", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2008", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2006", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2007", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2008", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2009", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2009", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2009", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2010", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2010", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2010", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2010", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2010", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2011", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2011", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2011", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2011", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2011", typeof(int));
                dtCUSTOMER_DP.Columns.Add("GROUPBY", typeof(string));
                dtCUSTOMER_DP.Columns.Add("LEDGERID", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2012", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2012", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2012", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2012", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2012", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2013", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2013", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2013", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2013", typeof(int));
                //dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2013", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDJECT_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDJECT_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDJECT_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PPAGEBUDGET_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2014", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2015", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2015", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2015", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2015", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2015", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2016", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2016", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2016", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2016", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2016", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2017", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2017", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2017", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2017", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2017", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BYEARLY_BUDGET_2018", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PYEARLY_BUDGET_2018", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BPAGEBUDGET_2018", typeof(int));
                dtCUSTOMER_DP.Columns.Add("BNOBUDGET_2018", typeof(int));
                dtCUSTOMER_DP.Columns.Add("PNOBUDGET_2018", typeof(int));
                dsCustomerDtls.Tables.Add(dtCUSTOMER_DP);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void addFinancialSiteColumns()
        {
            DataTable dtFINANCIALSITE_DP = new DataTable("FINANCIALSITE_DP");
            try
            {
                dtFINANCIALSITE_DP.Columns.Add("FINSITENO", typeof(int));
                dtFINANCIALSITE_DP.Columns.Add("FINSITENAME", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEADDRESS1", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEADDRESS2", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEADDRESS3", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEADDRESS4", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEADDRESS5", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEADDRESS6", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEVATNO", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITECOMMODITYNO", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEPHONE1", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEFAX", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEMAIL", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("CUSTLOGIN", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("CUSTPASS", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("CUSTNO", typeof(int));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEMAIL1", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITECONTACT", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITECONTACT1", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("FINSITEMAILOPTION", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("GROUPBY", typeof(string));
                dtFINANCIALSITE_DP.Columns.Add("INV_CONCOUNTRY", typeof(string));
                dsCustomerDtls.Tables.Add(dtFINANCIALSITE_DP);

            }
            catch (Exception Ex)
            {
                throw Ex;            
            }
        }
        private void addContactSiteColumns()
        {
            DataTable dtCONTACTSITE_DP = new DataTable("CONTACTSITE_DP");
            try
            {
                dtCONTACTSITE_DP.Columns.Add("CONSITENO", typeof(int));
                dtCONTACTSITE_DP.Columns.Add("CUSTNO", typeof(int));
                dtCONTACTSITE_DP.Columns.Add("CONSITENAME", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONOFFICE", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONADDRESS", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONCITY", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONSTATE", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONPOCODE", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONCOUNTRY", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONPHONE", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONFAX", typeof(string));
                dtCONTACTSITE_DP.Columns.Add("CONWEB", typeof(string));
                dsCustomerDtls.Tables.Add(dtCONTACTSITE_DP);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void assignValuesToTable(int intMode,int intOfficeMode,int intFinanceMode)
        {
            DataRow drCustomer = dsCustomerDtls.Tables["CUSTOMER_DP"].NewRow();
            DataRow drFinanceSite = dsCustomerDtls.Tables["FINANCIALSITE_DP"].NewRow();
            DataRow drContactSite = dsCustomerDtls.Tables["CONTACTSITE_DP"].NewRow();
            try
            {
                if (intMode == 0)
                {
                    drCustomer["CUSTNO"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpCustomer.SelectedValue) != "" && Convert.ToString(drpCustomer.SelectedValue) != "0")
                    {
                        drCustomer["CUSTNO"] = Convert.ToInt32(drpCustomer.SelectedValue);
                    }
                    else
                    {
                        return;
                    }
                }

                if (Convert.ToString(txtCustomer.Text).Trim() == "")
                {
                    lblError.Text = "Please Enter the Customer Name.";
                    return;
                }
                else
                {
                    drCustomer["CUSTNAME"] = Convert.ToString(txtCustomer.Text).Trim();
                }
                
                drCustomer["CUSTEMAIL"] = Convert.ToString(txtEmail.Text).Trim();
                if (Convert.ToString(drpPdfEnabled.SelectedValue).Trim() == "")
                {
                    drCustomer["CUSTPDFENABLED"] = "0";
                }
                else
                {
                    drCustomer["CUSTPDFENABLED"] = Convert.ToString(drpPdfEnabled.SelectedValue).Trim();
                }

                if (Convert.ToString(drpCurrency.SelectedValue).Trim() == "" || Convert.ToString(drpCurrency.SelectedValue).Trim() == "0")
                {
                    lblError.Text = "Please Select the Currency.";
                    return;
                }
                else
                {
                    drCustomer["CURRNO"] = Convert.ToString(drpCurrency.SelectedValue).Trim();
                }
                
                drCustomer["DPSERVER_CUSTNAME"] = Convert.ToString(txtFolderName.Text).Trim();
                drCustomer["CUSTCREATIONDATE"] = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy")).Trim();
               
                if (intMode == 0)
                {
                    drCustomer["CUSTCODE"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpCustomer.SelectedValue) != "" && Convert.ToString(drpCustomer.SelectedValue) != "0")
                    {
                        drCustomer["CUSTCODE"] = Convert.ToInt32(drpCustomer.SelectedValue);
                    }
                    else
                    {
                        lblError.Text = "Please select the customer name.";
                        return;
                    }
                }

                if (intFinanceMode == 0)
                {
                    drCustomer["FINSITENO"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpFinancial.SelectedValue) != "" && Convert.ToString(drpFinancial.SelectedValue) != "0")
                    {
                        drCustomer["FINSITENO"] = Convert.ToInt32(drpFinancial.SelectedValue);
                    }
                    else
                    {
                        lblError.Text = "Please select the financial name.";
                        return;
                    }
                }
                drCustomer["CUSTADDRESS1"] = "";
                drCustomer["CUSTADDRESS2"] = "";
                drCustomer["CUSTPHONE1"] = "";

                if (Convert.ToString(txtNoofBooksPrev.Text).Trim() == "")
                {
                    drCustomer["byearly_budget_" + (DateTime.Now.Year - 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["byearly_budget_" + (DateTime.Now.Year - 1).ToString()] = Convert.ToString(txtNoofBooksPrev.Text).Trim();
                }

                if (Convert.ToString(txtNoofBooksCurrent.Text).Trim() == "")
                {
                    drCustomer["byearly_budget_" + (DateTime.Now.Year).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["byearly_budget_" + (DateTime.Now.Year).ToString()] = Convert.ToString(txtNoofBooksCurrent.Text).Trim();
                }

                if (Convert.ToString(txtNoofBooksNext.Text).Trim() == "")
                {
                    drCustomer["byearly_budget_" + (DateTime.Now.Year + 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["byearly_budget_" + (DateTime.Now.Year + 1).ToString()] = Convert.ToString(txtNoofBooksNext.Text).Trim();                
                }

                if (Convert.ToString(txtNoofProjectPrev.Text).Trim() == "")
                {
                    drCustomer["pyearly_budget_" + (DateTime.Now.Year - 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["pyearly_budget_" + (DateTime.Now.Year - 1).ToString()] = Convert.ToString(txtNoofProjectPrev.Text).Trim();
                }

                if (Convert.ToString(txtNoofProjectCurrent.Text).Trim() == "")
                {
                    drCustomer["pyearly_budget_" + (DateTime.Now.Year).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["pyearly_budget_" + (DateTime.Now.Year).ToString()] = Convert.ToString(txtNoofProjectCurrent.Text).Trim();
                }

                if (Convert.ToString(txtNoofProjectNext.Text).Trim() == "")
                {
                    drCustomer["pyearly_budget_" + (DateTime.Now.Year + 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["pyearly_budget_" + (DateTime.Now.Year + 1).ToString()] = Convert.ToString(txtNoofProjectNext.Text).Trim();
                }

                if (Convert.ToString(txtPagesPrev.Text).Trim() == "")
                {
                    drCustomer["bpagebudget_" + (DateTime.Now.Year - 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["bpagebudget_" + (DateTime.Now.Year - 1).ToString()] = Convert.ToString(txtPagesPrev.Text).Trim();
                }

                if (Convert.ToString(txtPagesCurrent.Text).Trim() == "")
                {
                    drCustomer["bpagebudget_" + (DateTime.Now.Year).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["bpagebudget_" + (DateTime.Now.Year).ToString()] = Convert.ToString(txtPagesCurrent.Text).Trim();
                }

                if (Convert.ToString(txtPagesNext.Text).Trim() == "")
                {
                    drCustomer["bpagebudget_" + (DateTime.Now.Year + 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["bpagebudget_" + (DateTime.Now.Year + 1).ToString()] = Convert.ToString(txtPagesNext.Text).Trim();
                }

                if (Convert.ToString(txtBookPrev.Text).Trim() == "")
                {
                    drCustomer["bnobudget_" + (DateTime.Now.Year - 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["bnobudget_" + (DateTime.Now.Year - 1).ToString()] = Convert.ToString(txtBookPrev.Text).Trim();
                }

                if (Convert.ToString(txtBookCurrent.Text).Trim() == "")
                {
                    drCustomer["bnobudget_" + (DateTime.Now.Year).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["bnobudget_" + (DateTime.Now.Year).ToString()] = Convert.ToString(txtBookCurrent.Text).Trim();
                }

                if (Convert.ToString(txtBookNext.Text).Trim() == "")
                {
                    drCustomer["bnobudget_" + (DateTime.Now.Year + 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["bnobudget_" + (DateTime.Now.Year + 1).ToString()] = Convert.ToString(txtBookNext.Text).Trim();
                }

                if (Convert.ToString(txtProjectPrev.Text).Trim() == "")
                {
                    drCustomer["pnobudget_" + (DateTime.Now.Year - 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["pnobudget_" + (DateTime.Now.Year - 1).ToString()] = Convert.ToString(txtProjectPrev.Text).Trim();
                }

                if (Convert.ToString(txtProjectCurrent.Text).Trim() == "")
                {
                    drCustomer["pnobudget_" + (DateTime.Now.Year).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["pnobudget_" + (DateTime.Now.Year).ToString()] = Convert.ToString(txtProjectCurrent.Text).Trim();
                }

                if (Convert.ToString(txtProjectNext.Text).Trim() == "")
                {
                    drCustomer["pnobudget_" + (DateTime.Now.Year + 1).ToString()] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["pnobudget_" + (DateTime.Now.Year + 1).ToString()] = Convert.ToString(txtProjectNext.Text).Trim();
                }

                if (Convert.ToString(drpGroup.SelectedValue).Trim() == "" || Convert.ToString(drpGroup.SelectedValue).Trim() == "0")
                {
                    lblError.Text = "Please Select the Team.Group Assigned.";
                    return;
                }
                else
                {
                    drCustomer["groupby"] = Convert.ToString(drpGroup.SelectedValue).Trim();
                }
                
                if (Convert.ToString(txtLedgerId.Text).Trim() == "")
                {
                    drCustomer["ledgerid"] = System.DBNull.Value;
                }
                else
                {
                    drCustomer["ledgerid"] = Convert.ToString(txtLedgerId.Text).Trim();
                }
                dsCustomerDtls.Tables["CUSTOMER_DP"].Rows.Add(drCustomer);

                if (intOfficeMode == 0)
                {
                    drContactSite["CONSITENO"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpOffice.SelectedValue) != "" && Convert.ToString(drpOffice.SelectedValue) != "0")
                    {
                        drContactSite["CONSITENO"] = Convert.ToInt32(drpOffice.SelectedValue);
                    }
                    else
                    {
                        lblError.Text = "Please select the office name.";
                        return;
                    }
                }
                if (intMode == 0)
                {
                    drContactSite["CUSTNO"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpCustomer.SelectedValue) != "" && Convert.ToString(drpCustomer.SelectedValue) != "0")
                    {
                        drContactSite["CUSTNO"] = Convert.ToInt32(drpCustomer.SelectedValue);
                    }
                    else
                    {
                        lblError.Text = "Please select the customer name.";
                        return;
                    }
                }

                if (Convert.ToString(txtOfficeName.Text).Trim() == "")
                {
                    lblError.Text = "Please Enter the Office Name.";
                    return;
                }
                else
                {
                    drContactSite["CONSITENAME"] = Convert.ToString(txtOfficeName.Text).Trim();
                }

                drContactSite["CONOFFICE"] = Convert.ToString(txtLocation.Text).Trim();
                drContactSite["CONADDRESS"] = Convert.ToString(txtAddress.Text).Trim();
                drContactSite["CONCITY"] = Convert.ToString(txtCity.Text).Trim();
                drContactSite["CONSTATE"] = Convert.ToString(txtState.Text).Trim();
                drContactSite["CONPOCODE"] = Convert.ToString(txtPinCode.Text).Trim();

                if (Convert.ToString(drpCountry.SelectedValue).Trim() == "")
                {
                    drContactSite["CONCOUNTRY"] = "0";
                }
                else
                {
                    drContactSite["CONCOUNTRY"] = Convert.ToString(drpCountry.SelectedValue).Trim();
                }

                drContactSite["CONPHONE"] = Convert.ToString(txtPhone.Text).Trim();
                drContactSite["CONFAX"] = Convert.ToString(txtFax.Text).Trim();
                drContactSite["CONWEB"] = Convert.ToString(txtWebSite.Text).Trim();
                dsCustomerDtls.Tables["CONTACTSITE_DP"].Rows.Add(drContactSite);

                if (intFinanceMode == 0)
                {
                    drFinanceSite["FINSITENO"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpFinancial.SelectedValue) != "" && Convert.ToString(drpFinancial.SelectedValue) != "0")
                    {
                        drFinanceSite["FINSITENO"] = Convert.ToInt32(drpFinancial.SelectedValue);
                    }
                    else
                    {
                        return;
                    }
                }

                if (Convert.ToString(txtFinancialName.Text).Trim() == "")
                {
                    lblError.Text = "Please Enter the Financial Name.";
                    return;
                }
                else
                {
                    drFinanceSite["FINSITENAME"] = Convert.ToString(txtFinancialName.Text).Trim();
                }
                
                drFinanceSite["FINSITEADDRESS1"] = Convert.ToString(txtFinAddress.Text).Trim();
                drFinanceSite["FINSITEADDRESS2"] = Convert.ToString(txtFinAddress1.Text).Trim();
                drFinanceSite["FINSITEADDRESS3"] = Convert.ToString(txtFinAddress2.Text).Trim();
                drFinanceSite["FINSITEADDRESS4"] = Convert.ToString(txtFinAddress3.Text).Trim();
                drFinanceSite["FINSITEADDRESS5"] = Convert.ToString(txtFinAddress4.Text).Trim();
                drFinanceSite["FINSITEADDRESS6"] = Convert.ToString(txtFinAddress5.Text).Trim();
                drFinanceSite["FINSITEVATNO"] = Convert.ToString(txtVatNo.Text).Trim(); ;
                drFinanceSite["FINSITECOMMODITYNO"] = Convert.ToString(txtCommNo.Text).Trim();
                drFinanceSite["FINSITEPHONE1"] = Convert.ToString(txtFinPhone.Text).Trim();
                drFinanceSite["FINSITEFAX"] = Convert.ToString(txtFinfax.Text).Trim();
                drFinanceSite["FINSITEMAIL"] = Convert.ToString(txtFinEmail.Text).Trim(); ;
                drFinanceSite["CUSTLOGIN"] = Convert.ToString(txtLogin.Text).Trim(); ;
                drFinanceSite["CUSTPASS"] = Convert.ToString(txtPassword.Text).Trim();
                if (intMode == 0)
                {
                    drFinanceSite["CUSTNO"] = 0;
                }
                else
                {
                    if (Convert.ToString(drpCustomer.SelectedValue) != "" && Convert.ToString(drpCustomer.SelectedValue) != "0")
                    {
                        drFinanceSite["CUSTNO"] = Convert.ToInt32(drpCustomer.SelectedValue);
                    }
                    else
                    {
                        return;
                    }
                }
                dsCustomerDtls.Tables["FINANCIALSITE_DP"].Rows.Add(drFinanceSite);
                datasourceIBSQL ibSql = new datasourceIBSQL();
                ibSql.AddCustomerDetails(dsCustomerDtls, intMode,intOfficeMode,intFinanceMode);
                //AddCustomerDetails(dsCustomerDtls, intMode, intOfficeMode, intFinanceMode);
                //clear();
                lblError.Text = "Customer details saved successfully.";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillSelectedOfficeDetails()
        {
            try
            {

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void fillOfficeDetails()
        {
            int intConsiteNo = 0;
            DataSet dsOffice = new DataSet();
            ibSQL=new datasourceIBSQL();
            try
            {
                intConsiteNo = Convert.ToInt32(drpOffice.SelectedValue);
                dsOffice = ibSQL.GetOfficeDetails(intConsiteNo);
                if (dsOffice != null)
                {
                    if (dsOffice.Tables.Contains("GetDetails"))
                    {
                        DataRow[] drOffice = dsOffice.Tables["GetDetails"].Select("");
                        txtOfficeName.Text = Convert.ToString(drOffice[0]["consitename"]);
                        txtAddress.Text = Convert.ToString(drOffice[0]["conaddress"]);
                        txtPinCode.Text = Convert.ToString(drOffice[0]["conpocode"]);
                        txtPhone.Text = Convert.ToString(drOffice[0]["conphone"]);
                        txtFax.Text = Convert.ToString(drOffice[0]["confax"]);
                        txtLocation.Text = Convert.ToString(drOffice[0]["conoffice"]);
                        txtCity.Text = Convert.ToString(drOffice[0]["concity"]);
                        drpCountry.SelectedValue = Convert.ToString(drOffice[0]["concountry"]).Trim();
                        txtState.Text = Convert.ToString(drOffice[0]["constate"]);
                        txtWebSite.Text = Convert.ToString(drOffice[0]["conweb"]);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void fillFinancialDetails()
        {
            DataSet dsFinance = new DataSet();
            ibSQL = new datasourceIBSQL();
            try
            {
                int intFinanceNo = Convert.ToInt32(drpFinancial.SelectedValue);
                dsFinance = ibSQL.GetFinanceDetails(intFinanceNo);
                if (dsFinance != null)
                {
                    if (dsFinance.Tables.Contains("GetDetails"))
                    {
                        DataRow[] drFinance = dsFinance.Tables["GetDetails"].Select("");
                        txtFinancialName.Text = Convert.ToString(drFinance[0]["finsitename"]);
                        txtFinAddress.Text = Convert.ToString(drFinance[0]["finsiteaddress1"]);
                        txtFinAddress1.Text = Convert.ToString(drFinance[0]["finsiteaddress2"]);
                        txtFinAddress2.Text = Convert.ToString(drFinance[0]["finsiteaddress3"]);
                        txtFinAddress3.Text = Convert.ToString(drFinance[0]["finsiteaddress4"]);
                        txtFinAddress4.Text = Convert.ToString(drFinance[0]["finsiteaddress5"]);
                        txtFinAddress5.Text = Convert.ToString(drFinance[0]["finsiteaddress6"]);
                        txtFinPhone.Text = Convert.ToString(drFinance[0]["finsitephone1"]);
                        drpGroup.SelectedValue = Convert.ToString(drFinance[0]["groupby2016"]);
                        txtFinfax.Text = Convert.ToString(drFinance[0]["finsitefax"]);
                        txtFinEmail.Text = Convert.ToString(drFinance[0]["finsitemail"]);
                        txtCommNo.Text = Convert.ToString(drFinance[0]["finsitecommodityno"]);
                        txtVatNo.Text = Convert.ToString(drFinance[0]["finsitevatno"]);
                        txtLogin.Text = Convert.ToString(drFinance[0]["custlogin"]);
                        txtPassword.Text = Convert.ToString(drFinance[0]["custpass"]);
                        txtLedgerId.Text = Convert.ToString(drFinance[0]["ledgerid"]);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
  #endregion
    
	    private void opencon()
	    {
            sConStr = ConfigurationManager.ConnectionStrings["conStrSQLEmp"].ToString();
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
        public string AddCustomerDetails(DataSet dsCustomer, int intMode, int intofficeMode, int intFinanceMode)
        {
            SqlCommand ocmd = new SqlCommand();
            DataSet ods = new DataSet();
            SqlParameter sqlParm = new SqlParameter();
            string strStatus = "";
            try
            {
                opencon();
                ocmd.CommandType = CommandType.StoredProcedure;
                ocmd.CommandText = "mis_sp_add_customer";
                ocmd.Connection = oConn;
                ocmd.Parameters.Clear();

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.Int;
                sqlParm.ParameterName = "@intMode";
                sqlParm.Value = intMode;
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.Int;
                sqlParm.ParameterName = "@intOfficeMode";
                sqlParm.Value = intofficeMode;
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.Int;
                sqlParm.ParameterName = "@intFinanceMode";
                sqlParm.Value = intFinanceMode;
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.Structured;
                sqlParm.ParameterName = "@tblCONTACTSITE_DP";
                sqlParm.Value = dsCustomer.Tables["CONTACTSITE_DP"];
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.Structured;
                sqlParm.ParameterName = "@tblCUSTOMER_DP";
                sqlParm.Value = dsCustomer.Tables["CUSTOMER_DP"];
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.Structured;
                sqlParm.ParameterName = "@tblFINANCIALSITE_DP";
                sqlParm.Value = dsCustomer.Tables["FINANCIALSITE_DP"];
                sqlParm.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(sqlParm);

                sqlParm = new SqlParameter();
                sqlParm.SqlDbType = SqlDbType.NVarChar;
                sqlParm.Size = -1;
                sqlParm.ParameterName = "@strStatus";
                sqlParm.Value = strStatus;
                sqlParm.Direction = ParameterDirection.Output;
                ocmd.Parameters.Add(sqlParm);

                ocmd.ExecuteNonQuery();
                strStatus = Convert.ToString(ocmd.Parameters["@strStatus"].Value);
                return strStatus;

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