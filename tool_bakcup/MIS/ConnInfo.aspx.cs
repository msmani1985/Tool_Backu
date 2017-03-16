using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConnInfo : System.Web.UI.Page
{
    string strsql = "";
    datasourceIBSQL ibSql = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadRecords(0, "");
        }
    }

    #region "Control Events"
    protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            clearControl(2);
            LoadRecords(1, "");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 intConsiteNo = 0;
        Int32 intCategory = 0;
        try
        {
            lblError.Text = "";
            clearControl(0);

            intConsiteNo = Convert.ToInt32(drpCompany.SelectedValue);
            intCategory = Convert.ToInt32(drpCategory.SelectedValue);

            if (intCategory == 12)
            {
                txtFirstName.ReadOnly = true;
                //txtFirstName.BackColor = System.Drawing.Color.Gray;

                txtSurName.ReadOnly = true;
                //txtSurName.BackColor = System.Drawing.Color.Gray;

                txtPhone2.ReadOnly = true;
                //txtPhone2.BackColor = System.Drawing.Color.Gray;

                txtFax2.ReadOnly = true;
                // txtFax2.BackColor = System.Drawing.Color.Gray;

                txtMobile.ReadOnly = true;
                //txtMobile.BackColor = System.Drawing.Color.Gray;

                txtInvDisplayName.ReadOnly = true;
                //txtInvDisplayName.BackColor = System.Drawing.Color.Gray;

                txtInvEmail.ReadOnly = true;
                //txtInvEmail.BackColor = System.Drawing.Color.Gray;

                lstResponsiblity.Enabled = false;
                //lstResponsiblity.BackColor = System.Drawing.Color.Gray;
                lstResponsiblity.Items.Clear();
            }
            else
            {
                txtFirstName.ReadOnly = false;
                // txtFirstName.BackColor = System.Drawing.Color.White;

                txtSurName.ReadOnly = false;
                //txtSurName.BackColor = System.Drawing.Color.White;

                txtPhone2.ReadOnly = false;
                //txtPhone2.BackColor = System.Drawing.Color.White;

                txtFax2.ReadOnly = false;
                //txtFax2.BackColor = System.Drawing.Color.White;

                txtMobile.ReadOnly = false;
                // txtMobile.BackColor = System.Drawing.Color.White;

                txtInvDisplayName.ReadOnly = false;
                //txtInvDisplayName.BackColor = System.Drawing.Color.White;

                txtInvEmail.ReadOnly = false;
                // txtInvEmail.BackColor = System.Drawing.Color.White;

                //lstResponsiblity.Enabled = true;
                // lstResponsiblity.BackColor = System.Drawing.Color.White;
                //lstResponsiblity.Items.Clear();
            }

            LoadRecords(1, "");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            addContactDetails(1);
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error while saving.";
        }

    }    
    protected void drpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblError.Text = "";
        ibSql = new datasourceIBSQL();
        DataSet dsRecords = new DataSet();
        try
        {
            string strConTypeNo = Convert.ToString(drpCategory.SelectedValue).Trim();
            string strConsitno = Convert.ToString(drpCompany.SelectedValue).Trim();
            string strConno = Convert.ToString(drpName.SelectedValue).Trim();

            dsRecords = ibSql.GetContactRecords(strConTypeNo, strConsitno, strConno);

            if (dsRecords != null)
            {
                if (dsRecords.Tables.Count > 0)
                {
                    if (dsRecords.Tables[0].Rows.Count > 0)
                    {

                        DataRow[] drContact = dsRecords.Tables[0].Select("");

                        txtFirstName.Text = Convert.ToString(drContact[0]["confirstname"]);
                        txtSurName.Text = Convert.ToString(drContact[0]["consurname"]);
                        txtPhone1.Text = Convert.ToString(drContact[0]["conphone1"]);
                        txtFax1.Text = Convert.ToString(drContact[0]["confax"]);
                        txtEmail.Text = Convert.ToString(drContact[0]["conemail"]);
                        txtMobile.Text = Convert.ToString(drContact[0]["conmobile"]);
                        txtDisplayName.Text = Convert.ToString(drContact[0]["displayname"]);
                        txtAddress.Text = Convert.ToString(drContact[0]["address"]);
                        txtPhone2.Text = Convert.ToString(drContact[0]["conphone2"]);
                        txtFax2.Text = Convert.ToString(drContact[0]["confax2"]);
                        txtInvDisplayName.Text = Convert.ToString(drContact[0]["invdisplayname"]);
                        txtInvEmail.Text = Convert.ToString(drContact[0]["invconemail"]);
                    }

                    lstResponsiblity.ClearSelection();
                    if (dsRecords.Tables.Count > 1)
                    {
                        if (dsRecords.Tables[1].Rows.Count > 0)
                        {
                            Int32 intResCount = 0;
                            for (int i = 0; i < dsRecords.Tables[1].Rows.Count; i++)
                            {
                                intResCount = Convert.ToInt32(dsRecords.Tables[1].Rows[i][0]);
                                lstResponsiblity.Items.FindByValue(intResCount.ToString()).Selected = true;

                            }
                        }
                    }

                }
            }


        }
        catch (Exception ex)
        {
            lblError.Text = "Error in loading records.";
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        addContactDetails(0);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        clearControl(3);
    }
    #endregion

    #region "User defined Fn"
    private void clearControl(int intmode)
    {
        try
        {
            if (intmode == 0)
            {
                drpCompany.SelectedValue = "0";
                drpName.SelectedValue = "0";
            }
            else if (intmode == 1)
            {
                drpName.SelectedValue = "0";
            }
            else if (intmode == 3)
            {
                drpCategory.SelectedValue = "0";
                drpCompany.SelectedValue = "0";
                drpName.SelectedValue = "0";
                drpName.Items.Clear();
            }
            txtFirstName.Text = "";
            txtSurName.Text = "";
            txtPhone1.Text = "";
            txtFax1.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtDisplayName.Text = "";
            txtAddress.Text = "";
            txtPhone2.Text = "";
            txtFax2.Text = "";
            txtInvDisplayName.Text = "";
            txtInvEmail.Text = "";
            lstResponsiblity.ClearSelection();
            lstResponsiblity.Items.FindByValue("2").Selected = true;
            lstResponsiblity.Items.FindByValue("12").Selected = true;
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error while clearing records.";
        }
    }
    private void addContactDetails(int intMode)
    {
        ibSql = new datasourceIBSQL();
        try
        {

            string contypeno = Convert.ToString(drpCategory.SelectedValue).Trim();
            string consiteno = Convert.ToString(drpCompany.SelectedValue).Trim();
            string conno = Convert.ToString(drpName.SelectedValue).Trim();
            if (intMode == 0)
            {
                conno = "0";
            }
            string strname = Convert.ToString(drpName.SelectedItem.Text).Trim();
            string confirstname = Convert.ToString(txtFirstName.Text).Trim();
            string consurname = Convert.ToString(txtSurName.Text).Trim();
            string displayname = Convert.ToString(txtDisplayName.Text).Trim();
            string invdisplayname = Convert.ToString(txtInvDisplayName.Text).Trim();
            string conphone1 = Convert.ToString(txtPhone1.Text).Trim();
            string conphone2 = Convert.ToString(txtPhone2.Text).Trim();
            string conmobile = Convert.ToString(txtMobile.Text).Trim();
            string confax = Convert.ToString(txtFax1.Text).Trim();
            string confax2 = Convert.ToString(txtFax2.Text).Trim();
            string conemail = Convert.ToString(txtEmail.Text).Trim();
            string invconemail = Convert.ToString(txtInvEmail.Text).Trim();
            string address = Convert.ToString(txtAddress.Text).Trim();
            string[] strResponsibility;



            if (Convert.ToString(contypeno).Trim() == "" || Convert.ToString(contypeno) == "0")
            {
                lblError.Text = "Please select the category.";
                return;
            }

            if (Convert.ToString(consiteno).Trim() == "" || Convert.ToString(consiteno) == "0")
            {
                lblError.Text = "Please select the company.";
                return;
            }



            if (Convert.ToString(displayname).Trim() == "")
            {
                lblError.Text = "Please enter the display name.";
                return;
            }

            if (Convert.ToString(conemail).Trim() == "")
            {
                lblError.Text = "Please enter the email.";
                return;
            }

            if (intMode != 0)
            {
                if (Convert.ToString(conno).Trim() == "" || Convert.ToString(conno) == "0")
                {
                    lblError.Text = "Please select the name.";
                    return;
                }
            }

            if (contypeno != "12")
            {


                if (Convert.ToString(confirstname).Trim() == "")
                {
                    lblError.Text = "Please enter the firstname.";
                    return;
                }

                if (Convert.ToString(consurname).Trim() == "")
                {
                    lblError.Text = "Please enter the surname.";
                    return;
                }

                if (Convert.ToString(invdisplayname).Trim() == "")
                {
                    lblError.Text = "Please enter the invoice display name.";
                    return;
                }

                if (Convert.ToString(invconemail).Trim() == "")
                {
                    lblError.Text = "Please enter the invoice email.";
                    return;
                }

                int[] intCount = lstResponsiblity.GetSelectedIndices();

                if (intCount.Length == 0)
                {
                    lblError.Text = "Please select the resposiblity.";
                    return;
                }

                string strResponsiblity = "";
                for (int i = 0; i < intCount.Length; i++)
                {
                    if (strResponsiblity.Trim().Length == 0)
                    {
                        strResponsiblity = Convert.ToString(lstResponsiblity.Items[intCount[i]].Value);
                    }
                    else
                    {
                        strResponsiblity = strResponsiblity + "," + Convert.ToString(lstResponsiblity.Items[intCount[i]].Value);
                    }
                }

            }

            DataSet dsContact = new DataSet();
            dsContact = ibSql.GetContactTable(contypeno);


            if (contypeno == "12")
            {
                DataRow dr = dsContact.Tables[0].NewRow();

                dr["PDFNO"] = conno;
                dr["PDFNAME"] = displayname;
                dr["PDFFAX"] = confax;
                dr["PDFEMAIL"] = conemail;
                dr["PDFDISPLAYNAME"] = displayname;
                dr["PDFADDRESS"] = address;
                dr["PDFPHONE"] = conphone1;
                dsContact.Tables[0].Rows.Add(dr);
            }
            else
            {
                DataRow dr = dsContact.Tables[1].NewRow();

                Int32 intCustNo = ibSql.GetCustNo(consiteno);

                dr["CONNO"] = conno;
                dr["CUSTNO"] = intCustNo;
                dr["CONSURNAME"] = consurname;
                dr["CONFIRSTNAME"] = confirstname;
                dr["CONPHONE2"] = conphone2;
                dr["CONFAX"] = confax;
                dr["CONEMAIL"] = conemail;
                dr["CONPHONE1"] = conphone1;
                dr["CONFAX2"] = confax2;
                dr["CONMOBILE"] = conmobile;
                dr["DISPLAYNAME"] = displayname;
                dr["ADDRESS"] = address;
                dr["CONSITENO"] = consiteno;
                dr["INVCONEMAIL"] = invconemail;
                dr["INVDISPLAYNAME"] = invdisplayname;
                dsContact.Tables[1].Rows.Add(dr);

            }

            int[] intResCount = lstResponsiblity.GetSelectedIndices();

            if (intResCount.Length > 0)
            {
                for (int i = 0; i < intResCount.Length; i++)
                {
                    DataRow dres = dsContact.Tables[2].NewRow();
                    dres["CONNO"] = conno;
                    dres["CONTYPENO"] = Convert.ToString(lstResponsiblity.Items[intResCount[i]].Value);
                    dsContact.Tables[2].Rows.Add(dres);
                }

            }

            string strResult = ibSql.AddContactDetails(dsContact, intMode, Convert.ToInt32(contypeno));

            if (strResult == "Error.")
            {
                lblError.Text = "Error while saving.";
            }
            else
            {
                clearControl(3);
                lblError.Text = strResult;
            }
            
        }
        catch (Exception Ex)
        {
            lblError.Text = Ex.Message.ToString(); //"Error while saving.";
        }
    }
    private void LoadRecords(int intmode, string strSeach)
    {
        ibSql = new datasourceIBSQL();
        DataSet dsCategory = new DataSet();
        DataSet dsName = new DataSet();
        DataSet dsCompanyName = new DataSet();
        DataSet dsResponsibility = new DataSet();
        Int32 intConsiteNo = 0;
        Int32 intCategory = 0;
        try
        {
            if (intmode == 0)
            {
                dsCategory = ibSql.GetContactCategory(strSeach);
                LoadDropDown(drpCategory, dsCategory, "Select Category");

                dsCompanyName = ibSql.GetContactCompany(strSeach);
                LoadDropDown(drpCompany, dsCompanyName, "Select Company");

                drpName.Items.Insert(0, new ListItem("Select Contact", "0"));
                drpName.SelectedValue = "0";

                intConsiteNo = Convert.ToInt32(drpCompany.SelectedValue);
                intCategory = Convert.ToInt32(drpCategory.SelectedValue);

            }
            else if (intmode == 1)
            {
                intConsiteNo = Convert.ToInt32(drpCompany.SelectedValue);
                intCategory = Convert.ToInt32(drpCategory.SelectedValue);

                if (intCategory != 0 && intConsiteNo != 0)
                {
                    dsName = ibSql.GetContactName(intConsiteNo, intCategory, "");
                    LoadDropDown(drpName, dsName, "Select Contact");
                }

            }

            dsResponsibility = ibSql.GetContactResponsibilty(strSeach);
            lstResponsiblity.DataSource = dsResponsibility;
            lstResponsiblity.DataValueField = dsResponsibility.Tables[0].Columns[0].ColumnName;
            lstResponsiblity.DataTextField = dsResponsibility.Tables[0].Columns[1].ColumnName;
            lstResponsiblity.DataBind();

            lstResponsiblity.Items.FindByValue("2").Selected = true;
            lstResponsiblity.Items.FindByValue("12").Selected = true;

        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    private void LoadDropDown(DropDownList drpList, DataSet ds, string strInsertedItem)
    {
        try
        {
            drpList.Items.Clear();
            drpList.DataSource = ds;
            drpList.DataValueField = ds.Tables[0].Columns[0].ColumnName;
            drpList.DataTextField = ds.Tables[0].Columns[1].ColumnName;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem(strInsertedItem, "0"));
            drpList.SelectedValue = "0";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}