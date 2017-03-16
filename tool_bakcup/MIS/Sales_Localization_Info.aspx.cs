using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Services;

public partial class Sales_Localization_Info : System.Web.UI.Page
{
    private Sales_Local objSales = new Sales_Local();

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Session["employeeid"] == null)
        //{
        //    throw new Exception("Session Expired!");
        //}
        if (!IsPostBack)
        {
            if (Request.QueryString["slno"] != null && Request.QueryString["slno"].ToString().Trim() != "")
            {
                this.SalesLoadPublisherInfo();
                drpPublisher.SelectedValue = Request.QueryString["slno"].ToString().Trim();
                LoadPublisherDetails(Request.QueryString["slno"].ToString().Trim());
            }
            else
            {
                this.SalesLoadPublisherInfo();
            }
            
        }
    }
    [WebMethod]
    public static List<string> Article(string ID)
    {
        List<string> empResult = new List<string>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "spGetSales_State";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.AddWithValue("@ctyno", ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empResult.Add(dr["stName"].ToString());
                }
                con.Close();
                return empResult;
            }
        }
    }   
    private void SalesLoadPublisherInfo()
    {
        LoadPublisher();
        LoadJobStatus();
        LoadJobCategory();
        LoadCountry();
        drpcontactName.Items.Clear();
        drpcontactName.Items.Insert(0, new ListItem("-- Add new contact --", "0"));

        //txtCompany.ReadOnly = false;
        //txtCompany.Focus();
    }
    private bool ValidateEmail()
    {
        string email = txtEmail.Text.Trim();
        Regex regex = new Regex(@"^(|([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$");
        Match match = regex.Match(email);
        if (!match.Success)
        {
            Alert(email + " is Invalid Email Address");
            return false;
        }
        return true;
    }
    private void LoadPublisher()
    {
        int iCount; 
        
         DataSet dsPublisher = objSales.getPublishersList();
         drpPublisher.DataSource = dsPublisher;
         
         string groupCountry = String.Empty;
         drpPublisher.Items.Clear();
         int iDecCount = 0;
         for (iCount =0; iCount<= dsPublisher.Tables[0].Rows.Count - 1; iCount++)
        {
            DataRow Dr;
            Dr = dsPublisher.Tables[0].Rows[iCount];
            
            if (groupCountry != Dr[2].ToString().Trim())
            {
                iDecCount--;
                groupCountry = Dr[2].ToString().Trim();
                ListItem lstitem = new ListItem("------ " + Dr[2].ToString().Trim() + " ------", "xxx");
                lstitem.Attributes.Add("style", "background-color:#e6e6fa;");
                lstitem.Attributes.Add("disabled", "true");
                drpPublisher.Items.Add(lstitem);
                
            }
            ListItem li = new ListItem(Dr[1].ToString().Trim(), Dr[0].ToString().Trim());
            drpPublisher.Items.Add(li);
        }
         drpPublisher.Items.Insert(0, new ListItem("-- Choose a Customer --", "0"));
         drpPublisher.Attributes.Add("onchange", "javascript:if(document.form1." + drpPublisher.ClientID + ".value!='xxx')document.form1." + btndrpPostback.ClientID + ".click();");
         dsPublisher = null;
    }
    private void LoadJobStatus()
    {
         drpStatus.Items.Clear();
         DataSet dsStatus = objSales.getLeadStatusTypes();
         drpStatus.DataSource = dsStatus;
         drpStatus.DataTextField = dsStatus.Tables[0].Columns[1].ToString();
         drpStatus.DataValueField = dsStatus.Tables[0].Columns[0].ToString();
         drpStatus.DataBind();
         dsStatus = null;
    }
    private void LoadJobCategory()
    {
        lstCategory.Items.Clear();
        DataSet dsCategory = objSales.getLeadCategory();
        lstCategory.DataSource = dsCategory;
        lstCategory.DataTextField = dsCategory.Tables[0].Columns[1].ToString();
        lstCategory.DataValueField = dsCategory.Tables[0].Columns[0].ToString();
        lstCategory.DataBind();
        lstCategory.Items.Insert(0, new ListItem("-- Choose a Category --", "0"));
        dsCategory = null;
    }
    private void LoadCountry()
    {
        drpcountry.Items.Clear();
        DataSet dsCountry = objSales.getCountryList();
        drpcountry.DataSource = dsCountry;
        drpcountry.DataTextField = dsCountry.Tables[0].Columns[1].ToString();
        drpcountry.DataValueField = dsCountry.Tables[0].Columns[2].ToString();
        drpcountry.DataBind();
        drpcountry.Items.Insert(0, new ListItem("-- Choose a Country --", "0"));
        dsCountry = null;
        dropState.Items.Insert(0, new ListItem("-- Choose a State --", "0"));
    }
    private void LoadContacts()
    {
        drpcontactName.Items.Clear();
        DataSet dsContact = objSales.getCountryList();
        drpcontactName.DataSource = dsContact;
        drpcontactName.DataTextField = dsContact.Tables[0].Columns[1].ToString();
        drpcontactName.DataValueField = dsContact.Tables[0].Columns[0].ToString();
        drpcontactName.DataBind();
        drpcontactName.Items.Insert(0, new ListItem("-- Choose a Contact --", "0"));
        drpcontactName = null;
    }
    protected void ImgConNewInfo_Click(object sender, ImageClickEventArgs e)
    {
        ClearContact();
        
    }
    protected void ImgPubNewInfo_Click(object sender, ImageClickEventArgs e)
    {
        drpPublisher.Enabled = true;
        Response.Redirect("Sales_Localization_Info.aspx", true);
    }
    private void LoadPublisherDetails(string strsalesId)
    {
        string salesId;
        DataSet dsPub;
        if (strsalesId == string.Empty)
            salesId = drpPublisher.SelectedValue.ToString();
        else
        {
            
            salesId = strsalesId;
            drpPublisher.SelectedValue = salesId;
            drpPublisher.Enabled = true;
        }
        if (drpPublisher.SelectedValue == "0")
        {
            Alert("Please select a Publisher before proceeding.");
            ClearScreen();
        }
        else
        {
            
            ConInfo.Disabled = false;

            dsPub = objSales.getPublishersDetails(salesId);
            //txtCompany.ReadOnly = true;
            ClearScreen();
            if (dsPub.Tables[0].DataSet != null)
            {
                DataRow dr = dsPub.Tables[0].Rows[0];
                txtCompany.Text = dr["SLCOMPANY"].ToString().Trim();
                drpPublisher.SelectedValue = salesId;

                if (drpStatus.Items.FindByValue(dr["LEADSTATUSTYPE"].ToString().Trim())  != null) 
                    drpStatus.SelectedValue = dr["LEADSTATUSTYPE"].ToString().Trim();

                if (drpcountry.Items.FindByValue(dr["SLCOUNTRY"].ToString().Trim()) != null)
                    drpcountry.SelectedValue = dr["SLCOUNTRY"].ToString().Trim();

                dropState.Items.Clear();
                DataSet dsState = objSales.getStateList(dr["SLCOUNTRY"].ToString().Trim());
                dropState.DataSource = dsState;
                dropState.DataTextField = dsState.Tables[0].Columns[0].ToString();
                dropState.DataValueField = dsState.Tables[0].Columns[1].ToString();
                dropState.DataBind();
                dropState.Items.Insert(0, new ListItem("-- Choose a State --", "0"));

                txtAddress.Text = dr["ADRline1"].ToString().Trim();
                txtAddress1.Text = dr["ADRline2"].ToString().Trim();
                txtAddress2.Text = dr["ADRline3"].ToString().Trim();
                txtAddress3.Text = dr["ADRline4"].ToString().Trim();
                txtDescription.Text = dr["SLDESCRIPTION"].ToString().Trim();
                txtCity.Text = dr["SLCITY"].ToString().Trim();
                txtPocode.Text = dr["SLPOCODE"].ToString().Trim();

                if (dropState.Items.FindByValue(dr["SLSTATE"].ToString().Trim()) != null)
                    dropState.SelectedValue = dr["SLSTATE"].ToString().Trim();
 
                
   
            }

            for (int iCount = 0; iCount <= dsPub.Tables[1].Rows.Count - 1; iCount++)
            {
                DataRow Dr;
                Dr = dsPub.Tables[1].Rows[iCount];
                if (lstJTNO.Items.FindByValue(Dr["JTNO"].ToString()) != null)
                    lstJTNO.Items.FindByValue(Dr["JTNO"].ToString()).Selected = true;
   
            }
            for (int iCount = 0; iCount <= dsPub.Tables[2].Rows.Count - 1; iCount++)
            {
                DataRow Dr;
                Dr = dsPub.Tables[2].Rows[iCount];
                if (lstCategory.Items.FindByValue(Dr["SLCATNO"].ToString()) != null)
                    lstCategory.Items.FindByValue(Dr["SLCATNO"].ToString()).Selected = true;
            }

            drpcontactName.DataSource = dsPub.Tables[3];
            drpcontactName.DataTextField = dsPub.Tables[3].Columns[1].ToString();
            drpcontactName.DataValueField = dsPub.Tables[3].Columns[0].ToString();
            drpcontactName.DataBind();
            drpcontactName.Items.Insert(0, new ListItem("-- Add new contact --", "0"));
       
            for (int iCount = 0; iCount <= dsPub.Tables[3].Rows.Count - 1; iCount++)
            {
                DataRow dr;
                dr = dsPub.Tables[3].Rows[iCount];
             
                if (iCount == 0)
                {
                    drpcontactName.SelectedIndex = 1;
                    txtcontact.Text = dr["CI_NAME"].ToString().Trim();
                    txtPhone.Text = dr["CI_PHONE"].ToString().Trim();
                    txtFax.Text  =  dr["CI_FAX"].ToString().Trim();
                    txtWeb.Text =  dr["CI_WEB"].ToString().Trim();
                    txtEmail.Text =  dr["CI_EMAIL"].ToString().Trim();
                    txtConTitle.Text = dr["CI_TITLE"].ToString().Trim();
                    txtSkype.Text = dr["SKYPEID"].ToString().Trim();
                }
            }
        }
    }
    private void ClearScreen()
    {
        txtAddress.Text = string.Empty;
        txtDescription.Text =string.Empty;
        txtCompany.Text = string.Empty;
        txtCity.Text = string.Empty;
        txtcontact.Text = string.Empty;
        dropState.Items.Clear();
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtSkype.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtPocode.Text = string.Empty;
        txtWeb.Text = string.Empty;
        txtConTitle.Text = string.Empty;
        txtAddress1.Text = string.Empty;
        txtAddress2.Text = string.Empty;
        txtAddress3.Text = string.Empty;
        
        drpPublisher.SelectedIndex = -1;
        drpcontactName.Items.Clear();
        lstJTNO.SelectedIndex = -1;
        drpStatus.SelectedIndex = -1;
        lstCategory.SelectedIndex = -1;
        drpcountry.SelectedIndex = -1;
        drpcontactName.Items.Clear();
        drpcontactName.Items.Insert(0, new ListItem("-- Add new contact --", "0"));

    }
    protected void drpPublisher_PreRender(object sender, EventArgs e)
    {
        int cnt = 0;
        foreach (ListItem itm in drpPublisher.Items){
            if (itm.Value == "xxx")
            {
                drpPublisher.Items[cnt].Attributes.Add("disable", "true");
                drpPublisher.Items[cnt].Attributes.Add("style", "background-color:#e6e6fa;");
            }
            cnt++;
        }
    }
    protected void LoadContactDetails(string CID, string salesId)
    {
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtConTitle.Text = string.Empty;
        txtcontact.Text = string.Empty;
        txtWeb.Text = string.Empty;
        txtSkype.Text = string.Empty;

        DataSet dsContact = objSales.getContactInfo(CID, salesId);
        DataRow dr;
        dr = dsContact.Tables[0].Rows[0];

        txtPhone.Text = dr["CI_PHONE"].ToString();
        txtEmail.Text = dr["CI_EMAIL"].ToString();
        txtFax.Text = dr["CI_FAX"].ToString();
        txtConTitle.Text = dr["CI_TITLE"].ToString();
        txtcontact.Text = dr["CI_NAME"].ToString();
        txtWeb.Text = dr["CI_WEB"].ToString();
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void btndrpPostback_Click(object sender, EventArgs e)
    {
        LoadPublisherDetails("");
    }
    protected void drpcontactName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpcontactName.SelectedIndex == 0)
        {
            ClearContact();
        }
        else
        {
            drpPublisher.Enabled = true;
            LoadContactDetails(drpcontactName.SelectedValue.ToString().Trim(), drpPublisher.SelectedValue.ToString());
        }
    }
    protected void ClearContact()
    {
        if (drpPublisher.SelectedValue == "0")
        {
            Alert("Please select a Customer before proceeding.");
        }
        else
        {
            drpPublisher.Enabled = false;
        }
        drpcontactName.SelectedIndex = 0;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtConTitle.Text = string.Empty;
        txtcontact.Text = string.Empty;
        txtWeb.Text = string.Empty;
        txtSkype.Text = string.Empty;

    }
    protected void SaveNewPublisher()
    {
        string[] aSaveNewPub;
        string[] aSaveNewContact;
        string strJob = string.Empty;
        string strCategory = string.Empty;
        if (CheckPublisherValidation( 0 ))
        {
            aSaveNewPub = new string[14];

            for (int iCount = 0; iCount <= lstJTNO.Items.Count - 1; iCount++)
            {
                if (lstJTNO.Items[iCount].Selected)
                {
                    strJob = strJob + lstJTNO.Items[iCount].Value + ',';
                }
            }
            strJob = strJob.Remove(strJob.Length - 1);

            for (int iCount = 0; iCount <= lstCategory.Items.Count - 1; iCount++)
            {
                if (lstCategory.Items[iCount].Selected)
                {
                    strCategory = strCategory + lstCategory.Items[iCount].Value + ',';
                }
            }
            strCategory = strCategory.Remove(strCategory.Length - 1);            
            
            aSaveNewPub[0] = txtCompany.Text.Trim();
            aSaveNewPub[1] = drpStatus.SelectedValue.Trim();
            aSaveNewPub[2] = txtAddress.Text.Trim();
            aSaveNewPub[3] = txtCity.Text.Trim();
            aSaveNewPub[4] = dropState.SelectedValue.ToString();//txtState.Text.Trim();
            aSaveNewPub[5] = txtPocode.Text.Trim();
            aSaveNewPub[6] = drpcountry.SelectedValue.Trim();
            aSaveNewPub[7] = txtDescription.Text.Trim();
            aSaveNewPub[8] = strJob;//txtCompany.Text.Trim();
            aSaveNewPub[9] = strCategory;
            aSaveNewPub[10] = Session["employeeid"].ToString().Trim();
            aSaveNewPub[11] = txtAddress1.Text;
            aSaveNewPub[12] = txtAddress2.Text;
            aSaveNewPub[13] = txtAddress3.Text;
            
            string msg = this.objSales.InsertPublisherInfo(aSaveNewPub);
            if (msg.Contains("Error") || msg.Contains("Customer Name Already Exists!")) Alert(msg);
            else
            {
                if (txtcontact.Text.Trim() != string.Empty)
                {
                    aSaveNewContact = new string[9];
                    aSaveNewContact[0] = txtcontact.Text.Trim();
                    aSaveNewContact[1] = txtConTitle.Text.Trim();
                    aSaveNewContact[2] = txtPhone.Text.Trim();
                    aSaveNewContact[3] = txtFax.Text.Trim();
                    aSaveNewContact[4] = txtEmail.Text.Trim();
                    aSaveNewContact[5] = txtWeb.Text.Trim();
                    aSaveNewContact[6] = msg;
                    aSaveNewContact[7] = Session["employeeid"].ToString().Trim();
                    aSaveNewContact[8] = txtSkype.Text;
                    string msgcontact = this.objSales.InsertSalesContact(aSaveNewContact);
                    if (msgcontact.Contains("Error")) Alert(msgcontact);
                }
                Alert("Customer successfully saved.");
                LoadPublisher();
                this.LoadPublisherDetails(msg);
            }
        }

    }
    protected void UpdatePublisher()
    {
        string[] aUpdatePub;
        string[] aUpdateContact;
        string strJob = string.Empty;
        string strCategory = string.Empty;
        if (CheckPublisherValidation( 1 ))
        {
            aUpdatePub = new string[14];

            for (int iCount = 0; iCount <= lstJTNO.Items.Count - 1; iCount++)
            {
                if (lstJTNO.Items[iCount].Selected)
                {
                    strJob = strJob + lstJTNO.Items[iCount].Value + ',';
                }
            }
            strJob = strJob.Remove(strJob.Length - 1);

            for (int iCount = 0; iCount <= lstCategory.Items.Count - 1; iCount++)
            {
                if (lstCategory.Items[iCount].Selected)
                {
                    strCategory = strCategory + lstCategory.Items[iCount].Value + ',';
                }
            }
            strCategory = strCategory.Remove(strCategory.Length - 1);
            aUpdatePub[0] = drpPublisher.SelectedValue.Trim(); ;
            aUpdatePub[1] = drpStatus.SelectedValue.Trim();
            aUpdatePub[2] = txtAddress.Text.Trim();
            aUpdatePub[3] = txtCity.Text.Trim();
            aUpdatePub[4] = dropState.SelectedValue.ToString();
            aUpdatePub[5] = txtPocode.Text.Trim();
            aUpdatePub[6] = drpcountry.SelectedValue.Trim();
            aUpdatePub[7] = txtDescription.Text.Trim();
            aUpdatePub[8] = strJob;
            aUpdatePub[9] = strCategory;
            aUpdatePub[10] = txtAddress1.Text;
            aUpdatePub[11] = txtAddress2.Text;
            aUpdatePub[12] = txtAddress3.Text;
            aUpdatePub[13] = txtCompany.Text.Trim();

            string msg = this.objSales.UpdatePublisherInfo(aUpdatePub);
            if (msg.Contains("Error")) Alert(msg);
            else
            {

                aUpdateContact = new string[9];
                string msgcontact;
                if (drpcontactName.SelectedIndex != 0)
                {
                    //if (txtcontact.Text.Trim() != string.Empty)
                    {
                        aUpdateContact[0] = drpcontactName.SelectedValue.Trim();
                        aUpdateContact[1] = txtcontact.Text.Trim();
                        aUpdateContact[2] = txtConTitle.Text.Trim();
                        aUpdateContact[3] = txtPhone.Text.Trim();
                        aUpdateContact[4] = txtFax.Text.Trim(); ;
                        aUpdateContact[5] = txtEmail.Text.Trim();
                        aUpdateContact[6] = txtWeb.Text.Trim();
                        aUpdateContact[7] = msg;
                        aUpdateContact[8] = txtSkype.Text;
                        msgcontact = this.objSales.UpdateSalesContact(aUpdateContact);
                        if (msgcontact.Contains("Error")) Alert(msgcontact);
                    }
                }
                else
                {
                    //if (txtcontact.Text.Trim() != string.Empty)
                    {
                        aUpdateContact[0] = txtcontact.Text.Trim();
                        aUpdateContact[1] = txtConTitle.Text.Trim();
                        aUpdateContact[2] = txtPhone.Text.Trim();
                        aUpdateContact[3] = txtFax.Text.Trim();
                        aUpdateContact[4] = txtEmail.Text.Trim();
                        aUpdateContact[5] = txtWeb.Text.Trim();
                        aUpdateContact[6] = msg;
                        aUpdateContact[7] = Session["employeeid"].ToString().Trim();
                        aUpdateContact[8] = txtSkype.Text;
                        msgcontact = this.objSales.InsertSalesContact(aUpdateContact);
                        if (msgcontact.Contains("Error")) Alert(msgcontact);
                    }
                
                }
                Alert("Customer successfully Updated.");
                LoadPublisher();
                this.LoadPublisherDetails(msg);
              
            }
        }
        
    
    }
    protected bool CheckPublisherValidation(int iflag)
    {
        string sMessage = "";
        int i = 1;
        if (iflag == 0)
        {
            if (txtCompany.Text == "")
                sMessage += i++ + ". Enter a Customer Name\\r\\n";
        }
        else
        {
            if (drpPublisher.SelectedValue == "0" || drpPublisher.SelectedValue == "xxx")
                sMessage += i++ + ". Select a Customer Name\\r\\n";
        }
        if (lstJTNO.SelectedIndex <= 0 )
            sMessage += i++ + ". Select a Job Type\\r\\n";
        if (drpStatus.SelectedValue == "0")
            sMessage += i++ + ". Select a Status\\r\\n";
        if (lstCategory.SelectedIndex <= 0 )
            sMessage += i++ + ". Select a Category\\r\\n";
        if (drpcountry.SelectedValue == "0")
            sMessage += i++ + ". Select a Country\\r\\n";
        if (dropState.SelectedValue == "0")
            sMessage += i++ + ". Select a State\\r\\n";
        
        if (i >1)    
        {
            Alert(sMessage);
            return false;
        }
            return true;
    
    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        if (ValidateEmail())
        {
            if (drpPublisher.SelectedValue == "0")
            {
                SaveNewPublisher();
            }
            else
            {
                UpdatePublisher();
            }
        }
    }
    protected void drpcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropState.Items.Clear();
        DataSet dsState = objSales.getStateList(drpcountry.SelectedValue);
        dropState.DataSource = dsState;
        dropState.DataTextField = dsState.Tables[0].Columns[0].ToString();
        dropState.DataValueField = dsState.Tables[0].Columns[1].ToString();
        dropState.DataBind();
        dropState.Items.Insert(0, new ListItem("-- Choose a State --", "0"));
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        txtNewState.Text = "";
        if (txtNewState.Visible)
        {
            txtNewState.Visible = false;
            btnAddState.Visible = false;
        }
        else
        {
            txtNewState.Visible = true;
            btnAddState.Visible = true;
        }
    }
    protected void btnAddState_Click(object sender, EventArgs e)
    {
        if (txtNewState.Text != "" && drpcountry.SelectedValue!="0")
        {
            objSales.InsertState(txtNewState.Text, drpcountry.SelectedValue);
        }
        else if (txtNewState.Text == "")
        {
            Alert("Enter New State Name..");
        }
        else if( drpcountry.SelectedValue=="0")
        {
            Alert("Select Country..");
        }

        dropState.Items.Clear();
        DataSet dsState = objSales.getStateList(drpcountry.SelectedValue);
        dropState.DataSource = dsState;
        dropState.DataTextField = dsState.Tables[0].Columns[0].ToString();
        dropState.DataValueField = dsState.Tables[0].Columns[1].ToString();
        dropState.DataBind();
        dropState.Items.Insert(0, new ListItem("-- Choose a State --", "0"));
        txtNewState.Text = "";
        txtNewState.Visible = false;
        btnAddState.Visible = false;
    }
    protected void rbSorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbSorting.SelectedValue == "CustomerWise")
        {
            drpPublisher.Items.Clear();
            DataSet dsPublisher = objSales.getPublishersList();
            drpPublisher.DataSource = dsPublisher.Tables[1];
            drpPublisher.DataTextField = dsPublisher.Tables[1].Columns[1].ToString();
            drpPublisher.DataValueField = dsPublisher.Tables[1].Columns[0].ToString();
            drpPublisher.DataBind();
            drpPublisher.Items.Insert(0, new ListItem("-- Choose a Customer --", "0"));
        }
        else
        {
            LoadPublisher();
        }
    }
}
