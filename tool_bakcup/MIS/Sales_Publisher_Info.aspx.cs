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

public partial class Sales_Publisher_Info : System.Web.UI.Page
{
    private Sales objSales = new Sales();
    //ArrayList arLsitPubVal = new ArrayList();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
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
    private void SalesLoadPublisherInfo()
    {
        LoadPublisher();
        //Alert(arLsitPubVal.Count.ToString());
        //LoadJobTypes();
        LoadJobStatus();
        LoadJobCategory();
        LoadCountry();
        //LoadContacts();
        drpcontactName.Items.Clear();
        drpcontactName.Items.Insert(0, new ListItem("-- Add new contact --", "0"));

        txtCompany.ReadOnly = false;
        txtCompany.Focus();
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
            //arLsitPubVal.Add(iCount);
             //arLsitPubVal.Inser
        }
         drpPublisher.Items.Insert(0, new ListItem("-- Choose a Publisher --", "0"));
         drpPublisher.Attributes.Add("onchange", "javascript:if(document.form1." + drpPublisher.ClientID + ".value!='xxx')document.form1." + btndrpPostback.ClientID + ".click();");
         //arLsitPubVal.Add(0);

         dsPublisher = null;
    }
    /*private void LoadJobTypes()
    {
        lstJTNO.Items.Clear();
        lstJTNO.Items.Add(new ListItem("-- Choose a Job --", "0"));
        lstJTNO.Items.Add(new ListItem("Books", "1"));
        lstJTNO.Items.Add(new ListItem("Journals", "2"));
        lstJTNO.Items.Add(new ListItem("Projects", "3"));
    }*/
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
        //ConInfo.Disabled = true;
        //ClearScreen();
        Response.Redirect("Sales_Publisher_Info.aspx", true);

        
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
            txtCompany.ReadOnly = true;
            /*
            if ( dsPub.Tables[0].Columns[2].ToString() != null )
                dsPub.Tables[0].Columns[2].ToString();
             */
            ClearScreen();
            if (dsPub.Tables[0].DataSet != null)
            {
                DataRow dr = dsPub.Tables[0].Rows[0];
                txtCompany.Text = dr["SLCOMPANY"].ToString().Trim();

                //if (drpPublisher.Items.IndexOf(new ListItem(dr["SLC"].ToString().Trim())) != -1)
                drpPublisher.SelectedValue = salesId;

                if (drpStatus.Items.FindByValue(dr["LEADSTATUSTYPE"].ToString().Trim())  != null) 
                    drpStatus.SelectedValue = dr["LEADSTATUSTYPE"].ToString().Trim();
                
                txtAddress.Text = dr["SLFULLADDRESS"].ToString().Trim();
                txtDescription.Text = dr["SLDESCRIPTION"].ToString().Trim();
                txtCity.Text = dr["SLCITY"].ToString().Trim();
                txtPocode.Text = dr["SLPOCODE"].ToString().Trim();
                txtState.Text = dr["SLSTATE"].ToString().Trim();
 
                if (drpcountry.Items.FindByValue(dr["SLCOUNTRY"].ToString().Trim()) != null) 
                    drpcountry.SelectedValue = dr["SLCOUNTRY"].ToString().Trim();
   
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

            //if (drpcontactName.Items.Count == 0)
            //    drpPublisher.Enabled = false;

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
                }
                //lstCategory.Items[Convert.ToInt32(Dr["SLCATNO"].ToString())].Selected = true;
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
        txtState.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtPocode.Text = string.Empty;
        txtWeb.Text = string.Empty;
        txtConTitle.Text = string.Empty;
        
        drpPublisher.SelectedIndex = -1;
        drpcontactName.Items.Clear();
        lstJTNO.SelectedIndex = -1;
        drpStatus.SelectedIndex = -1;
        lstCategory.SelectedIndex = -1;
        drpcountry.SelectedIndex = -1;
        //drpcontactName.SelectedIndex = -1;
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

        DataSet dsContact = objSales.getContactInfo(CID, salesId);
        DataRow dr;
        dr = dsContact.Tables[0].Rows[0];

        txtPhone.Text = dr["CI_PHONE"].ToString();
        txtEmail.Text = dr["CI_EMAIL"].ToString();
        txtFax.Text = dr["CI_FAX"].ToString();
        txtConTitle.Text = dr["CI_TITLE"].ToString();
        txtcontact.Text = dr["CI_NAME"].ToString();
        txtWeb.Text = dr["CI_WEB"].ToString();

        //drpcontactName = null;
       
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
            Alert("Please select a Publisher before proceeding.");
        }
        else
        {
            drpPublisher.Enabled = false;
            //  ConInfo.Disabled = false;
        }
        drpcontactName.SelectedIndex = 0;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtFax.Text = string.Empty;
        txtConTitle.Text = string.Empty;
        txtcontact.Text = string.Empty;
        txtWeb.Text = string.Empty;

    }
    protected void SaveNewPublisher()
    {
        string[] aSaveNewPub;
        string[] aSaveNewContact;
        string strJob = string.Empty;
        string strCategory = string.Empty;
        if (CheckPublisherValidation( 0 ))
        {
            aSaveNewPub = new string[11];

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
            aSaveNewPub[4] = txtState.Text.Trim();
            aSaveNewPub[5] = txtPocode.Text.Trim();
            aSaveNewPub[6] = drpcountry.SelectedValue.Trim();
            aSaveNewPub[7] = txtDescription.Text.Trim();
            aSaveNewPub[8] = strJob;//txtCompany.Text.Trim();
            aSaveNewPub[9] = strCategory;
            aSaveNewPub[10] = Session["employeeid"].ToString().Trim();
            
            string msg = this.objSales.InsertPublisherInfo(aSaveNewPub);
            if (msg.Contains("Error")) Alert(msg);
            else
            {
                if (txtcontact.Text.Trim() != string.Empty)
                {
                    aSaveNewContact = new string[8];
                    aSaveNewContact[0] = txtcontact.Text.Trim();
                    aSaveNewContact[1] = txtConTitle.Text.Trim();
                    aSaveNewContact[2] = txtPhone.Text.Trim();
                    aSaveNewContact[3] = txtFax.Text.Trim();
                    aSaveNewContact[4] = txtEmail.Text.Trim();
                    aSaveNewContact[5] = txtWeb.Text.Trim();
                    aSaveNewContact[6] = msg;
                    aSaveNewContact[7] = Session["employeeid"].ToString().Trim();
                    string msgcontact = this.objSales.InsertSalesContact(aSaveNewContact);
                    if (msgcontact.Contains("Error")) Alert(msgcontact);
                }
                Alert("Publisher successfully saved.");
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
            aUpdatePub = new string[10];

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
            aUpdatePub[4] = txtState.Text.Trim();
            aUpdatePub[5] = txtPocode.Text.Trim();
            aUpdatePub[6] = drpcountry.SelectedValue.Trim();
            aUpdatePub[7] = txtDescription.Text.Trim();
            aUpdatePub[8] = strJob;
            aUpdatePub[9] = strCategory;

            string msg = this.objSales.UpdatePublisherInfo(aUpdatePub);
            if (msg.Contains("Error")) Alert(msg);
            else
            {

                aUpdateContact = new string[8];
                string msgcontact;
                if (drpcontactName.SelectedIndex != 0)
                {
                    if (txtcontact.Text.Trim() != string.Empty)
                    {
                        aUpdateContact[0] = drpcontactName.SelectedValue.Trim();
                        aUpdateContact[1] = txtcontact.Text.Trim();
                        aUpdateContact[2] = txtConTitle.Text.Trim();
                        aUpdateContact[3] = txtPhone.Text.Trim();
                        aUpdateContact[4] = txtFax.Text.Trim(); ;
                        aUpdateContact[5] = txtEmail.Text.Trim();
                        aUpdateContact[6] = txtWeb.Text.Trim();
                        aUpdateContact[7] = msg;
                        msgcontact = this.objSales.UpdateSalesContact(aUpdateContact);
                        if (msgcontact.Contains("Error")) Alert(msgcontact);
                    }
                }
                else
                {
                    if (txtcontact.Text.Trim() != string.Empty)
                    {
                        aUpdateContact[0] = txtcontact.Text.Trim();
                        aUpdateContact[1] = txtConTitle.Text.Trim();
                        aUpdateContact[2] = txtPhone.Text.Trim();
                        aUpdateContact[3] = txtFax.Text.Trim();
                        aUpdateContact[4] = txtEmail.Text.Trim();
                        aUpdateContact[5] = txtWeb.Text.Trim();
                        aUpdateContact[6] = msg;
                        aUpdateContact[7] = Session["employeeid"].ToString().Trim();
                        msgcontact = this.objSales.InsertSalesContact(aUpdateContact);
                        if (msgcontact.Contains("Error")) Alert(msgcontact);
                    }
                
                }
                Alert("Publisher successfully saved.");
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
                sMessage += i++ + ". Enter a Publisher Name\\r\\n";
        }
        else
        {
            if (drpPublisher.SelectedValue == "0" || drpPublisher.SelectedValue == "xxx")
                sMessage += i++ + ". Select a Publisher Name\\r\\n";
        }
        if (lstJTNO.SelectedIndex <= 0 )
            sMessage += i++ + ". Select a Job Type\\r\\n";
        if (drpStatus.SelectedValue == "0")
            sMessage += i++ + ". Select a Status\\r\\n";
        if (lstCategory.SelectedIndex <= 0 )
            sMessage += i++ + ". Select a Category\\r\\n";
        if (drpcountry.SelectedValue == "0")
            sMessage += i++ + ". Select a Country\\r\\n";
        
        if (i >1)    
        {
            Alert(sMessage);
            return false;
        }
            return true;
    
    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        if ( drpPublisher.SelectedValue == "0")
        {
            SaveNewPublisher();
        }
        else
        {
            UpdatePublisher();
        }
    }
}
