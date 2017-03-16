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

public partial class Contacts : System.Web.UI.Page
{
    CustomerBase oCust = new CustomerBase();
    Contact oCont = new Contact();
    protected int id = 0;
    string sCtrlName = "";
    string sCtrlVal = "";
    static string sEditContID = "";
    protected void Page_Load(object sender, EventArgs e){
        if (Session["employeeid"] == null){
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();
        if ((Request.QueryString["trgname"] != null && Request.QueryString["trgid"] != null) || (Request.QueryString["pop"] != null)){
            HtmlButton btnClosesum = new HtmlButton();
            HtmlButton btnClosemas = new HtmlButton();
            btnClosesum.InnerText = "Close";
            btnClosesum.Attributes.Add("onclick", "javascript:self.close();");
            btnClosesum.Attributes.Add("class", "dpButton");
            btnClosemas.InnerText = "Close";
            btnClosemas.Attributes.Add("onclick", "javascript:self.close();");
            btnClosemas.Attributes.Add("class", "dpButton");
            this.phMaster.Controls.Add(btnClosesum);
            this.phSummary.Controls.Add(btnClosemas);
        }
    }
    private void popScreen(){
        txtFname.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ");
        DataSet dscust;
        if (Session["customerid"] == null){
            dscust = oCust.getAllCustomers();
        }
        else
        {
            dscust = oCust.getCustomerByID(Session["customerid"].ToString().Trim());
        }
        drpCustomer.DataSource = dscust;
        drpCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpCustomer.DataBind();
        //
        drpCustomerSearch.DataSource = dscust;
        drpCustomerSearch.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpCustomerSearch.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpCustomerSearch.DataBind();

        drpCustomer.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        drpCustomerSearch.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != ""){
            drpCustomer.ClearSelection();
            drpCustomer.Items.FindByValue(Request.QueryString["cid"].ToString().Trim()).Selected = true;
            drpCustomerSearch.ClearSelection();
            drpCustomerSearch.Items.FindByValue(Request.QueryString["cid"].ToString().Trim()).Selected = true;
            drpCustomer.Enabled = false;
            drpCustomerSearch.Enabled = false;
            this.btnSearch_Click(null, null);
        }
        if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "0")
        { this.tabSummary.Visible = true; this.tabMaster.Visible = false; }
        else { this.tabSummary.Visible = false; this.tabMaster.Visible = true; }
    }
    protected void btnSearch_Click(object sender, EventArgs e){
        string sCustID = "";
        this.tabMaster.Visible = false;
        this.tabSummary.Visible = true;
        if (Request.QueryString["trgname"] != null && Request.QueryString["trgid"] != null){
            sCtrlName = Request.QueryString["trgname"].ToString().Trim();
            sCtrlVal = Request.QueryString["trgid"].ToString().Trim();
        }
        if (drpCustomerSearch.SelectedItem.Value != "0") sCustID = drpCustomerSearch.SelectedItem.Value.Trim();
        dgrdContacts.DataSource = oCont.getContactsByName(txtFname.Text.Trim(), sCustID);
        dgrdContacts.DataBind();
        dgrdContacts.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e){
        string[] aContact;
        if (validscreen()){
            if (sEditContID == ""){
                aContact = new string[16];
                aContact[0] = drpCustomer.SelectedItem.Value.Trim();
                aContact[1] = txtTitle.Text.Trim();
                aContact[2] = txtFirstName.Text.Trim();
                aContact[3] = txtLastName.Text.Trim();
                aContact[4] = txtSurName.Text.Trim();
                aContact[5] = txtDisplayName.Text.Trim();
                aContact[6] = txtAddress.Text.Trim();
                aContact[7] = txtPhone1.Text.Trim();
                aContact[8] = txtPhone2.Text.Trim();
                aContact[9] = txtPhone3.Text.Trim();
                aContact[10] = txtFax1.Text.Trim();
                aContact[11] = txtFax2.Text.Trim();
                aContact[12] = txtEmail1.Text.Trim();
                aContact[13] = txtEmail2.Text.Trim();
                aContact[14] = txtEmail3.Text.Trim();
                aContact[15] = txtComments.Text.Trim();
                string sContID = this.oCont.InsertContact(aContact);
                if (sContID.Trim() != "")
                {
                    this.loadContact(sContID);
                    Alert("Contact successfully created");
                }
                else Alert("Contact creation failed!");
            }
            else
            {
                aContact = new string[17];
                aContact[0] = drpCustomer.SelectedItem.Value.Trim();
                aContact[1] = txtTitle.Text.Trim();
                aContact[2] = txtFirstName.Text.Trim();
                aContact[3] = txtLastName.Text.Trim();
                aContact[4] = txtSurName.Text.Trim();
                aContact[5] = txtDisplayName.Text.Trim();
                aContact[6] = txtAddress.Text.Trim();
                aContact[7] = txtPhone1.Text.Trim();
                aContact[8] = txtPhone2.Text.Trim();
                aContact[9] = txtPhone3.Text.Trim();
                aContact[10] = txtFax1.Text.Trim();
                aContact[11] = txtFax2.Text.Trim();
                aContact[12] = txtEmail1.Text.Trim();
                aContact[13] = txtEmail2.Text.Trim();
                aContact[14] = txtEmail3.Text.Trim();
                aContact[15] = txtComments.Text.Trim();
                aContact[16] = sEditContID;
                string sContID = this.oCont.UpdateContact(aContact);
                if (sContID.Trim() != ""){
                    this.loadContact(sContID);
                    Alert("Contact successfully updated");
                }
                else Alert("Contact updation failed!");
            }
        }
    }
    private bool validscreen(){
        int i = 1;
        string sMessage = "";
        if (drpCustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r";
        if (txtDisplayName.Text.Trim() == "") sMessage += i++ + ". Enter Display Name\\r";
        if (txtFirstName.Text.Trim() == "") sMessage += i++ + ". Enter First Name\\r";
        if (txtSurName.Text.Trim() == "") sMessage += i++ + ". Enter Sur Name\\r";
        //if (txtLastName.Text.Trim() == "") sMessage += i++ + ". Enter Last Name\\r";
        if (txtEmail1.Text.Trim() == "") sMessage += i++ + ". Enter Email1\\r";
        
        if (i > 1){
            Alert(sMessage);
            return false;
        }
        return true;
    }
    protected void dgrdContacts_ItemDataBound(object sender, DataGridItemEventArgs e){
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem){
            if (sCtrlName != "" && sCtrlVal != ""){
                string id = ((HiddenField)e.Item.FindControl("hfContactID")).Value.Trim();
                string name = ((Label)e.Item.FindControl("lblContactDispName")).Text.Trim();
                string email = ((Label)e.Item.FindControl("lblContactEmail1")).Text.Trim();
                name = name.Replace("'","\\'");
                name = name + " (" + email + ")";
                ((HtmlImage)e.Item.
                    FindControl("imgCheck")).
                    Attributes.Add("onclick", "javascript:window.opener.document.form1." + sCtrlName + ".value='" + name + "';window.opener.document.form1." + sCtrlVal + ".value='" + id + "';self.close();");
            }
            else ((HtmlImage)e.Item.FindControl("imgCheck")).Visible = false;
        }
    }
    protected void dgrdContacts_ItemCommand(object source, DataGridCommandEventArgs e){
        if (e.CommandName == "Edit"){
            this.loadContact(((HiddenField)e.Item.FindControl("hfContactID")).Value.Trim());            
        }
    }
    private void loadContact(string sContactID){
        sEditContID = sContactID;
        DataSet dsCont=new DataSet();
        try{
            dsCont = oCont.getContactsByID(sContactID);
            if (dsCont.Tables[0].Rows.Count > 0){
                DataRow row = dsCont.Tables[0].Rows[0];
                drpCustomer.ClearSelection();
                if (drpCustomer.Items.FindByValue(row["customer_id"].ToString().Trim()) != null)
                    drpCustomer.Items.FindByValue(row["customer_id"].ToString().Trim()).Selected = true;
                txtTitle.Text = row["title"].ToString();
                txtDisplayName.Text = row["display_name"].ToString();
                txtFirstName.Text = row["first_name"].ToString();
                txtLastName.Text = row["last_name"].ToString();
                txtSurName.Text = row["sur_name"].ToString();
                txtAddress.Text = row["address"].ToString();
                txtEmail1.Text = row["email1"].ToString();
                txtEmail2.Text = row["email2"].ToString();
                txtEmail3.Text = row["email3"].ToString();
                txtFax1.Text = row["fax1"].ToString();
                txtFax2.Text = row["fax2"].ToString();
                txtPhone1.Text = row["phone1"].ToString();
                txtPhone2.Text = row["phone2"].ToString();
                txtPhone3.Text = row["phone3"].ToString();
                txtComments.Text = row["description"].ToString();
                this.tabMaster.Visible = true;
                this.tabSummary.Visible = false;
                dgrdContacts.Visible = false;
            }
            else
            {
                Alert("Contact Not found!");
            }
        }
        catch(Exception ex)
        {
            Alert(ex.Message);
        }
        finally
        {
            dsCont.Clear();
        }
    }
    protected void btnNew_Click(object sender, EventArgs e){
        sEditContID = "";
        this.tabMaster.Visible = true;
        this.tabSummary.Visible = false;
        if (Request.QueryString["cid"]==null) drpCustomer.ClearSelection();
        txtTitle.Text = "";
        txtDisplayName.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtSurName.Text = "";
        txtAddress.Text = "";
        txtEmail1.Text = "";
        txtEmail2.Text = "";
        txtEmail3.Text = "";
        txtFax1.Text = "";
        txtFax1.Text = "";
        txtPhone1.Text = "";
        txtPhone2.Text = "";
        txtPhone3.Text = "";
        txtComments.Text = "";
        drpCustomer.Focus();
    }
    protected void btnFind_Click(object sender, EventArgs e){
        this.tabMaster.Visible = false;
        this.tabSummary.Visible = true;
        txtFname.Focus();
    }
    public void Alert(string sMessage){
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + sMessage + "');</script>");
    }
}
