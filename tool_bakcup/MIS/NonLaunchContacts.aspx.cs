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

public partial class NonLaunchContacts : System.Web.UI.Page
{
    Non_Launch oCust = new Non_Launch();
    protected int id = 0;
    string sCtrlName = "";
    string sCtrlVal = "";
    static string sEditContID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["employeeid"] == null)
        //{
        //    throw new Exception("Session Expired!");
        //}
        if (!IsPostBack) this.popScreen();
        if ((Request.QueryString["trgname"] != null && Request.QueryString["trgid"] != null) || (Request.QueryString["pop"] != null))
        {
            HtmlButton btnClosesum = new HtmlButton();
            HtmlButton btnClosemas = new HtmlButton();
            btnClosesum.InnerText = "Close";
            btnClosesum.Attributes.Add("onclick", "javascript:self.close();");
            btnClosesum.Attributes.Add("class", "dpbutton");
            btnClosemas.InnerText = "Close";
            btnClosemas.Attributes.Add("onclick", "javascript:self.close();");
            btnClosemas.Attributes.Add("class", "dpbutton");
            this.phMaster.Controls.Add(btnClosesum);
            this.phSummary.Controls.Add(btnClosemas);
        }
    }
    private void popScreen()
    {
        txtFname.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnSearch.UniqueID + "').click();return false;}} else {return true}; ");
        DataSet dscust;
        if (Session["customerid"] == null)
        {
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
        if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != "")
        {
            drpCustomer.ClearSelection();
            drpCustomer.Items.FindByValue(Request.QueryString["cid"].ToString().Trim()).Selected = true;
            drpCustomerSearch.ClearSelection();
            drpCustomerSearch.Items.FindByValue(Request.QueryString["cid"].ToString().Trim()).Selected = true;
            //drpCustomer.Enabled = false;
            drpCustomerSearch.Enabled = false;
            this.btnSearch_Click(null, null);
        }
        DataSet dscust1;
        if (Request.QueryString["locationid"] == null)
        {
            dscust1 = oCust.getAllLocations();
        }
        else
        {
            dscust1 = oCust.getCustomerByID(Session["loctionid"].ToString().Trim());
        }
        drpLocation.DataSource = dscust1;
        drpLocation.DataTextField = dscust1.Tables[0].Columns[1].ToString();
        drpLocation.DataValueField = dscust1.Tables[0].Columns[0].ToString();
        drpLocation.DataBind();
        //
        drpLocationSearch.DataSource = dscust1;
        drpLocationSearch.DataTextField = dscust1.Tables[0].Columns[1].ToString();
        drpLocationSearch.DataValueField = dscust1.Tables[0].Columns[0].ToString();
        drpLocationSearch.DataBind();

        drpLocation.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        drpLocationSearch.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        if (Request.QueryString["lid"] != null && Request.QueryString["lid"].ToString().Trim() != "")
        {
            drpLocation.ClearSelection();
            drpLocation.Items.FindByValue(Request.QueryString["lid"].ToString().Trim()).Selected = true;
            drpLocationSearch.ClearSelection();
            drpLocationSearch.Items.FindByValue(Request.QueryString["lid"].ToString().Trim()).Selected = true;
            //drpLocation.Enabled = false;
            drpLocationSearch.Enabled = false;
            this.btnSearch_Click(null, null);
        }
        if (Request.QueryString["type"] != null && Request.QueryString["type"].ToString() == "0")
        { this.tabSummary.Visible = true; this.tabMaster.Visible = false; }
        else { this.tabSummary.Visible = false; this.tabMaster.Visible = true; }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sCustID = ""; string sLocationID = "";
        this.tabMaster.Visible = false;
        this.tabSummary.Visible = true;
        if (Request.QueryString["trgname"] != null && Request.QueryString["trgid"] != null)
        {
            sCtrlName = Request.QueryString["trgname"].ToString().Trim();
            sCtrlVal = Request.QueryString["trgid"].ToString().Trim();
        }
        if (drpCustomerSearch.SelectedItem.Value != "0") sCustID = drpCustomerSearch.SelectedItem.Value.Trim();
        sLocationID = drpLocationSearch.SelectedValue;
        dgrdContacts.DataSource = oCust.getContactsByName(txtFname.Text.Trim(), sCustID, sLocationID);
        dgrdContacts.DataBind();
        dgrdContacts.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string[] aContact;
        if (validscreen())
        {
            if (sEditContID == "")
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
                aContact[16] = drpLocation.SelectedValue;
                string sContID = this.oCust.InsertContact(aContact);
                if (sContID.Trim() != "")
                {
                    this.loadContact(sContID);
                    Alert("Contact successfully created");
                }
                else Alert("Contact creation failed!");
            }
            else
            {
                aContact = new string[18];
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
                aContact[17] = drpLocation.SelectedValue;
                string sContID = this.oCust.UpdateContact(aContact);
                if (sContID.Trim() != "")
                {
                    this.loadContact(sContID);
                    Alert("Contact successfully updated");
                }
                else Alert("Contact updation failed!");
            }
        }
    }
    private bool validscreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpCustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r";
        if (txtDisplayName.Text.Trim() == "") sMessage += i++ + ". Enter Display Name\\r";
        if (txtFirstName.Text.Trim() == "") sMessage += i++ + ". Enter First Name\\r";
        if (txtSurName.Text.Trim() == "") sMessage += i++ + ". Enter Sur Name\\r";
        //if (txtLastName.Text.Trim() == "") sMessage += i++ + ". Enter Last Name\\r";
        if (txtEmail1.Text.Trim() == "") sMessage += i++ + ". Enter Email1\\r";

        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }
    protected void dgrdContacts_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (sCtrlName != "" && sCtrlVal != "")
            {
                string id = ((HiddenField)e.Item.FindControl("hfContactID")).Value.Trim();
                string name = ((Label)e.Item.FindControl("lblContactDispName")).Text.Trim();
                string email = ((Label)e.Item.FindControl("lblContactEmail1")).Text.Trim();
                name = name.Replace("'", "\\'");
                //name = name + " (" + email + " )";
                ((HtmlImage)e.Item.
                    FindControl("imgCheck")).
                    Attributes.Add("onclick", "javascript:window.opener.document.form1." + sCtrlName + ".value='" + name + "';window.opener.document.form1." + sCtrlVal + ".value='" + id + "';self.close();");
            }
            else ((HtmlImage)e.Item.FindControl("imgCheck")).Visible = false;
        }
    }
    protected void dgrdContacts_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            this.loadContact(((HiddenField)e.Item.FindControl("hfContactID")).Value.Trim());
        }
    }
    private void loadContact(string sContactID)
    {
        sEditContID = sContactID;
        DataSet dsCont = new DataSet();
        try
        {
            dsCont = oCust.getContactsByID(sContactID);
            if (dsCont.Tables[0].Rows.Count > 0)
            {
                DataRow row = dsCont.Tables[0].Rows[0];
                drpCustomer.ClearSelection();
                if (drpCustomer.Items.FindByValue(row["custno"].ToString().Trim()) != null)
                    drpCustomer.Items.FindByValue(row["custno"].ToString().Trim()).Selected = true;
                if (drpLocation.Items.FindByValue(row["Location_id"].ToString().Trim()) != null)
                    drpLocation.Items.FindByValue(row["Location_id"].ToString().Trim()).Selected = true;
                txtTitle.Text = row["CONTITLE"].ToString();
                txtDisplayName.Text = row["DISPLAYNAME"].ToString().Trim();
                txtFirstName.Text = row["CONFIRSTNAME"].ToString().Trim();
                //txtLastName.Text = row["last_name"].ToString();
                txtSurName.Text = row["CONSURNAME"].ToString().Trim();
                txtAddress.Text = row["ADDRESS"].ToString().Trim();
                txtEmail1.Text = row["CONEMAIL"].ToString().Trim();
                txtEmail2.Text = row["CONEMAIL2"].ToString().Trim();
                txtEmail3.Text = row["CONEMAIL3"].ToString().Trim();
                txtFax1.Text = row["CONFAX"].ToString().Trim();
                txtFax2.Text = row["CONFAX2"].ToString().Trim();
                txtPhone1.Text = row["CONPHONE1"].ToString().Trim();
                txtPhone2.Text = row["CONPHONE2"].ToString().Trim();
                txtPhone3.Text = row["CONPHONE3"].ToString().Trim();
                txtComments.Text = row["DESCRIPTION"].ToString().Trim();
                this.tabMaster.Visible = true;
                this.tabSummary.Visible = false;
                dgrdContacts.Visible = false;
            }
            else
            {
                Alert("Contact Not found!");
            }
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
        finally
        {
            dsCont.Clear();
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        sEditContID = "";
        this.tabMaster.Visible = true;
        this.tabSummary.Visible = false;
        if (Request.QueryString["cid"] == null) drpCustomer.ClearSelection();
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
    protected void btnFind_Click(object sender, EventArgs e)
    {
        this.tabMaster.Visible = false;
        this.tabSummary.Visible = true;
        txtFname.Focus();
        dgrdContacts.Visible = true;
        btnSearch_Click(null,null);
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + sMessage + "');</script>");
    }
}