using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/* Creatin Date: Monday, August 24, 2009
 * Created by:  Royson 
 */
public partial class job_contacts : System.Web.UI.Page
{
    CustomerBase oCust = new CustomerBase();
    Contact oCont = new Contact();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen(){
        tblContact.Visible = false;
        hfJobContactID.Value = "";
        btnSave.Text = "Save";
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
        drpCustomer.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        drpJobtype.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        //pop Contact types
        DataSet dsConType = oCont.getContactTypes();
        drpContType.DataTextField = dsConType.Tables[0].Columns[1].ToString();
        drpContType.DataValueField = dsConType.Tables[0].Columns[0].ToString();
        drpContType.DataSource = dsConType;
        drpContType.DataBind();
        drpContType.Items.Insert(0, new ListItem(" -- select -- ", "0"));
        if (Session["email_cust"] != null && Session["email_job_typ"] != null && Session["email_par_job"] != null){
            if (drpCustomer.Items.FindByValue(Session["email_cust"].ToString()) != null)
                drpCustomer.Items.FindByValue(Session["email_cust"].ToString()).Selected = true;
            drpCustomer_SelectedIndexChanged(null, null);
            if (drpJobtype.Items.FindByValue(Session["email_job_typ"].ToString()) != null)
                drpJobtype.Items.FindByValue(Session["email_job_typ"].ToString()).Selected = true;
            drpJobtype_SelectedIndexChanged(null, null);
            //if (drpParentJob.Items.FindByValue(Session["email_par_job"].ToString()) != null)
            //    drpParentJob.Items.FindByValue(Session["email_par_job"].ToString()).Selected = true;
        }
        txtSearchContact.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnGo.ClientID + "').click();return false;}} else {return true}; ");
    }
    protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblContact.Visible = false;
    }
    protected void drpJobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblContact.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e){
        if (validSearch()){
            hfJobContactID.Value = "";
            txtSearchContact.Text = "";
            btnSave.Text = "Save";
            gvContacts.Visible = false;
            tblContact.Visible = true;
            trContList.Visible = false;
            if (drpJobtype.SelectedItem.Value == "1"){
                lblConJob.Text = "";
                drpJob.Visible = false;
            }
            else if (drpJobtype.SelectedItem.Value == "2"){
                lblConJob.Text = "Chapter:  ";
                drpJob.Visible = true;
                //sJobType = "7";
            }
            else if (drpJobtype.SelectedItem.Value == "4"){
                lblConJob.Text = "";
                drpJob.Visible = false;
            }
            else{
                lblConJob.Text = "";
                drpJob.Visible = false;
            }
            this.loadContacts("");
            Session["email_cust"] = drpCustomer.SelectedItem.Value.Trim();
            Session["email_job_typ"] = drpJobtype.SelectedItem.Value.Trim();
            Session["email_par_job"] = "0";
        }
    }
    private bool validSearch()
    {        
        string msg = "";
        if (drpCustomer.SelectedItem.Value == "0") msg += "* Select a Customer.\\r\\n";
        if (drpJobtype.SelectedItem.Value == "0") msg += "* Select a Job Type.";
        if (msg == "") return true;
        else Alert(msg);
        return false;
    }
    private bool validSave()
    {
        string msg = "";
        if (drpParentJob.SelectedItem.Value == "0") msg += "* Select a Job.\\r\\n";
        if (drpContName.SelectedItem.Value == "0") msg += "* Select a Contact Name.\\r\\n";
        if (drpContType.SelectedItem.Value == "0") msg += "* Select a Contact Type.\\r\\n";
        if (msg == "") return true;
        else Alert(msg);
        return false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validSave())
        {
            string[] aJobContact = new string[6];            
            switch (drpJobtype.SelectedItem.Value.Trim()){
                case "1":
                    aJobContact[0] = drpParentJob.SelectedItem.Value.Trim(); //journal id
                    aJobContact[1] = "1"; // journal type
                    aJobContact[2] = null;
                    break;
                case "2":
                    if (drpJob.SelectedItem.Value.Trim() != "0"){
                        aJobContact[0] = drpJob.SelectedItem.Value.Trim();
                        aJobContact[1] = "7"; //chapter type
                    }
                    else{
                        aJobContact[0] = drpParentJob.SelectedItem.Value.Trim();
                        aJobContact[1] = "2"; //Book type
                    }
                    aJobContact[2] = null;
                    break;
                case "4":
                    aJobContact[0] = drpParentJob.SelectedItem.Value.Trim();
                    aJobContact[1] = "4"; //project type
                    aJobContact[2] = null;
                    break;
            }
            aJobContact[3] = drpContName.SelectedItem.Value.Trim();
            aJobContact[4] = drpContType.SelectedItem.Value.Trim();
            string msg = "";
            if (hfJobContactID.Value.Trim() == ""){
                aJobContact[5] = Session["employeeid"].ToString();
                msg = oCont.InsertJobContact(aJobContact);
            }
            else
            {
                aJobContact[5] = hfJobContactID.Value;
                msg = oCont.UpdateJobContact(aJobContact);
            }
            if (msg=="true"){
                hfJobContactID.Value = "";
                btnSave.Text = "Save";
                this.loadContacts(drpParentJob.SelectedItem.Value.Trim());
                drpJob.ClearSelection();
                drpContName.ClearSelection();
                drpContType.ClearSelection();
                Alert("Successfully Saved.");
            }                
            else Alert(msg);
        }
    }
    private void loadContacts(string sJobID){
        DataSet dsCont = oCont.getJobContacts(drpCustomer.SelectedItem.Value.Trim(),
                drpJobtype.SelectedItem.Value.Trim(), sJobID);
        if (sJobID == ""){
            drpContName.Items.Clear();
            drpParentJob.Items.Clear();
            drpJob.Items.Clear();
            if (dsCont.Tables[0] != null && dsCont.Tables[0].Rows.Count > 0){
                drpContName.DataTextField = dsCont.Tables[0].Columns[1].ToString();
                drpContName.DataValueField = dsCont.Tables[0].Columns[0].ToString();
                drpContName.DataSource = dsCont.Tables[0];
                drpContName.DataBind();
            }
            if (dsCont.Tables[1] != null && dsCont.Tables[1].Rows.Count > 0){
                drpParentJob.DataTextField = dsCont.Tables[1].Columns[1].ToString();
                drpParentJob.DataValueField = dsCont.Tables[1].Columns[0].ToString();
                drpParentJob.DataSource = dsCont.Tables[1];
                drpParentJob.DataBind();
            }
            drpContName.Items.Insert(0, new ListItem(" -- select -- ", "0"));
            drpParentJob.Items.Insert(0, new ListItem(" -- select -- ", "0"));
            drpJob.Items.Insert(0, new ListItem(" -- select -- ", "0"));            
        }
        else
        {
            drpContName.ClearSelection();
            drpContType.ClearSelection();
            gvContacts.Visible = true;
            gvContacts.DataSource = dsCont.Tables[0];
            gvContacts.DataBind();
        }
        
    }
    public void Alert(string sMessage){
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + sMessage + "');</script>");
    }
    protected void drpParentJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpParentJob.SelectedItem.Value != "0"){
            if (drpJobtype.SelectedItem.Value == "2"){
                DataSet dsJob = oCont.getJobsByParentJobID(drpParentJob.SelectedItem.Value.Trim(), "7");
                drpJob.DataTextField = dsJob.Tables[0].Columns["name1"].ToString();
                drpJob.DataValueField = dsJob.Tables[0].Columns["job_id"].ToString();
                drpJob.DataSource = dsJob.Tables[0];
                drpJob.DataBind();
                drpJob.Items.Insert(0, new ListItem(" -- select -- ", "0"));
            }
            this.loadContacts(drpParentJob.SelectedItem.Value.Trim());
        }
        else{
            drpJob.ClearSelection();
            drpContName.ClearSelection();
            drpContType.ClearSelection();
        }
        Session["email_par_job"] = drpParentJob.SelectedItem.Value.Trim();
    }
    protected void drpJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drpJob.SelectedItem.Value != "0"){
        //    DataSet dsTitle = oCont.getJobByJobID(drpJob.SelectedItem.Value.Trim());
        //    if (dsTitle.Tables[0].Rows.Count > 0) lblJobTitle.Text = "Title: " + dsTitle.Tables[0].Rows[0]["title"].ToString();
        //    else lblJobTitle.Text = "Title: " + "--";
        //}
        //else lblJobTitle.Text = "";
    }
    protected void gvContacts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditContact"){
            GridViewRow Grow = (GridViewRow)((Control)((GridViewCommandEventArgs)e).CommandSource).Parent.Parent;
            hfJobContactID.Value = ((HiddenField)Grow.FindControl("hfgvJobContID")).Value.Trim();
            string sParentJobID = ((HiddenField)Grow.FindControl("hfgvContParentJobID")).Value.Trim();
            string sJobID = ((HiddenField)Grow.FindControl("hfgvContJobID")).Value.Trim();
            string sContID = ((HiddenField)Grow.FindControl("hfgvContID")).Value.Trim();
            string sContTypeID = ((HiddenField)Grow.FindControl("hfgvContTypeID")).Value.Trim();
            drpParentJob.ClearSelection();
            drpJob.ClearSelection();
            drpContName.ClearSelection();
            drpContType.ClearSelection();
            if (drpParentJob.Items.FindByValue(sParentJobID) != null)                
                drpParentJob.Items.FindByValue(sParentJobID).Selected = true;            
            if (sJobID != "" && drpJob.Items.FindByValue(sJobID) != null)                
                drpJob.Items.FindByValue(sJobID).Selected = true;            
            if (drpContName.Items.FindByValue(sContID) != null)                
                drpContName.Items.FindByValue(sContID).Selected = true;            
            if (drpContType.Items.FindByValue(sContTypeID) != null)                
                drpContType.Items.FindByValue(sContTypeID).Selected = true;
            btnSave.Text = "Update";
        }
        else if (e.CommandName == "DeleteContact"){
            GridViewRow Grow = (GridViewRow)((Control)((GridViewCommandEventArgs)e).CommandSource).Parent.Parent;
            string sParentJobID = ((HiddenField)Grow.FindControl("hfgvContParentJobID")).Value.Trim();
            string sJobContID = ((HiddenField)Grow.FindControl("hfgvJobContID")).Value.Trim();
            string msg = oCont.DeleteJobContact(sJobContID);
            if (msg != "true") Alert(msg);
            else this.loadContacts(sParentJobID);
        }
        else {/*do nothing*/ }
    }
    protected void gvContacts_RowDataBound(object sender, GridViewRowEventArgs e){
        if (e.Row.RowType == DataControlRowType.DataRow){
            ((ImageButton)e.Row.FindControl("ImgBtnConDelete")).Attributes.
                Add("onclick", "javascript:return confirm('Confirm Delete?');");
            if (((HiddenField)e.Row.FindControl("hfgvContTypeID")).Value.Trim() == "17"){
                ((ImageButton)e.Row.FindControl("ImgBtnConEdit")).Visible = false;
                ((ImageButton)e.Row.FindControl("ImgBtnConDelete")).Visible = false;
            }
        }
    }
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e){
        btnSave.Text = "Save";
        hfJobContactID.Value = "";
        trContList.Visible = false;
        txtSearchContact.Text = "";
        drpParentJob_SelectedIndexChanged(null, null);
    }
    protected void btnCancel_Click(object sender, EventArgs e){
        imgbtnNew_Click(null, null);
    }
    protected void dtlstContact_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "select_contact"){
            drpContName.ClearSelection();
            if (drpContName.Items.FindByText(((LinkButton)e.Item.FindControl("dtlstlnkContact")).Text) != null)
                drpContName.Items.FindByText(((LinkButton)e.Item.FindControl("dtlstlnkContact")).Text).Selected = true;
            dtlstContact.DataSource = null;
            dtlstContact.DataBind();
            txtSearchContact.Text = "";
            trContList.Visible = false;
        }
    }
    protected void btnGo_Click1(object sender, ImageClickEventArgs e){
        //if (txtSearchContact.Text.Trim() != ""){
            ListItemCollection coll = drpContName.Items;
            if (coll != null){
                SortedList list = new SortedList();
                foreach (ListItem itm in coll){
                    if (itm.Text.ToLower() != " -- select -- " && itm.Text.ToLower().Contains(txtSearchContact.Text.ToLower().Trim()))
                        list.Add(itm.Text + itm.Value, itm.Text);
                }                
                dtlstContact.DataSource = list;
                dtlstContact.DataBind();
                trContList.Visible = true;
            }
        //}
    }
}
