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
/*
/// <summary>
 *  Page Design: Selva
 *  Code Behing: Royson
 *  Creation Date: 05 Jun 2010
/// </summary>
 * */
public partial class Sales_Lead_Info : System.Web.UI.Page
{
    Sales oSales = new Sales();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen()
    {
        //pop publishers
        DataSet dsPub = oSales.getPublishersList('Y');
        drpCompany.DataSource = dsPub.Tables[0];
        drpCompany.DataValueField = dsPub.Tables[0].Columns[0].ToString();
        drpCompany.DataTextField = dsPub.Tables[0].Columns[1].ToString();
        drpCompany.DataBind();
        drpCompany.Items.Insert(0, new ListItem(" -- Choose a Publisher --", "0"));
        //pop lead status
        DataSet dsStat = oSales.getLeadStatusTypes();
        drpleadstatustype.DataSource = dsStat.Tables[0];
        drpleadstatustype.DataValueField = dsStat.Tables[0].Columns[0].ToString();
        drpleadstatustype.DataTextField = dsStat.Tables[0].Columns[1].ToString();
        drpleadstatustype.DataBind();
        //pop communication type
        DataSet dsComm = oSales.getCommunicationTypes();
        drpcommunicationtype.DataSource = dsComm.Tables[0];
        drpcommunicationtype.DataValueField = dsComm.Tables[0].Columns[0].ToString();
        drpcommunicationtype.DataTextField = dsComm.Tables[0].Columns[1].ToString();
        drpcommunicationtype.DataBind();
        drpcommunicationtype.Items.Insert(0, new ListItem(" -- Choose a Type --", "0"));
        //
        drpfollowuptype.DataSource = dsComm.Tables[0];
        drpfollowuptype.DataValueField = dsComm.Tables[0].Columns[0].ToString();
        drpfollowuptype.DataTextField = dsComm.Tables[0].Columns[1].ToString();
        drpfollowuptype.DataBind();
        drpfollowuptype.Items.Insert(0, new ListItem(" -- Choose a Type --", "0"));
        //
        txtcontactdate.Attributes.Add("readonly", "readonly");
        txtFollowupDate.Attributes.Add("readonly", "readonly");
        drpcontname.Items.Insert(0, new ListItem(" -- Choose a Contact -- ", "0"));
        drpcontactdate.Items.Insert(0, new ListItem(" -- Choose a Date --", "0"));

    }
    protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.clearCommunication();
        drpcontname.Items.Clear();
        drpcontactdate.Items.Clear();
        if (drpCompany.SelectedItem.Value != "0"){
            DataSet dspubInfo = oSales.getPublishersDetails(drpCompany.SelectedItem.Value.Trim());
            if (dspubInfo.Tables[0].Rows.Count > 0){
                drpleadstatustype.ClearSelection();
                string statustype = dspubInfo.Tables[0].Rows[0]["leadstatustype"].ToString();
                if (drpleadstatustype.Items.FindByValue(statustype) != null)
                    drpleadstatustype.Items.FindByValue(statustype).Selected = true;
            }
            DataSet dsCont = oSales.getContactList(drpCompany.SelectedItem.Value.Trim());
            drpcontname.DataSource = dsCont.Tables[0];
            drpcontname.DataValueField = dsCont.Tables[0].Columns[0].ToString();
            drpcontname.DataTextField = dsCont.Tables[0].Columns[1].ToString();
            drpcontname.DataBind();            
            if(dsCont.Tables[0].Rows.Count>0){
                drpcontname.Items[0].Selected = true;
                drpcontname_SelectedIndexChanged(null, null);
            }
            else drpcontactdate.Items.Insert(0, new ListItem(" -- Choose a Date --", "0"));
        }
        drpcontname.Items.Insert(0, new ListItem(" -- Choose a Contact --", "0"));        
    }
    protected void drpcontname_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpcontactdate.Items.Clear();
        if (drpcontname.SelectedItem.Value != "0")
        {
            DataSet dsHist = oSales.getCommuncationList(drpcontname.SelectedItem.Value.Trim());
            drpcontactdate.Items.Clear();
            drpcontactdate.DataSource = dsHist.Tables[0];
            drpcontactdate.DataValueField = dsHist.Tables[0].Columns["co_id"].ToString();
            drpcontactdate.DataTextField = dsHist.Tables[0].Columns["co_date"].ToString();
            drpcontactdate.DataBind();            
        }
        else this.clearCommunication();
        drpcontactdate.Items.Insert(0, new ListItem(" -- Choose a Date --", "0"));
    }
    protected void drpcontactdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpcontactdate.SelectedItem.Value != "0"){
            this.loadCommunicationDetails(drpcontactdate.SelectedItem.Value.Trim());
        }
        else this.clearCommunication();
    }
    private void loadCommunicationDetails(string sCommnID)
    {
        DataSet dsCommn = oSales.getCommuncationDetails(sCommnID);
        if (dsCommn.Tables[0].Rows.Count > 0){
            DataRow ro = dsCommn.Tables[0].Rows[0];
            txtcommunicationby.Text = ro["CO_BY"].ToString();
            drpcommunicationtype.ClearSelection();
            if (drpcommunicationtype.Items.FindByValue(ro["CO_TYPE"].ToString()) != null)
                drpcommunicationtype.Items.FindByValue(ro["CO_TYPE"].ToString()).Selected = true;
            txtCommunication.Text = ro["communication"].ToString().Trim();
            txtFollowupDate.Text = ro["FDATE"].ToString();
            drpfollowuptype.ClearSelection();
            if (drpfollowuptype.Items.FindByValue(ro["FTYPE"].ToString()) != null)
                drpfollowuptype.Items.FindByValue(ro["FTYPE"].ToString()).Selected = true;
            txtComments.Text = ro["REPLY"].ToString().Trim();
        }
        else this.clearCommunication();
    }
    private void clearCommunication(){
        txtcontactdate.Text = "";
        txtcommunicationby.Text = "";
        drpcommunicationtype.ClearSelection();
        drpfollowuptype.ClearSelection();
        txtCommunication.Text = "";
        txtFollowupDate.Text = "";
        txtComments.Text = "";
    }
    protected void imgbtnNewContact_Click(object sender, ImageClickEventArgs e)
    {
        if (drpCompany.SelectedItem.Value != "0")
            Response.Redirect("sales_publisher_info.aspx?slno=" + drpCompany.SelectedItem.Value.Trim(), true);
        else Alert("Select a Publisher.");
    }
    public void Alert(string sMessage){
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void imgbtnNewCommn_Click(object sender, ImageClickEventArgs e)
    {
        this.drpcontactdate.ClearSelection();
        this.drpcontactdate.Items[0].Selected = true;
        this.drpcontactdate_SelectedIndexChanged(null, null);
        txtcontactdate.Focus();
    }
    private bool validSave()
    {
        int i = 1;
        string sMessage = "";
        if (drpCompany.SelectedItem.Value == "0") sMessage += i++ + ". Select a Publisher\\r\\n";
        if (drpcontname.SelectedItem.Value == "0") sMessage += i++ + ". Select a Contact Name\\r\\n";
        if (drpCompany.SelectedItem.Value != "0" && drpcontname.SelectedItem.Value != "0"
            && drpcontactdate.SelectedItem.Value == "0"){
                if (txtcontactdate.Text.Trim() == "") sMessage += i++ + ". Enter Communication Date\\r\\n";
                if (txtcommunicationby.Text.Trim() == "") sMessage += i++ + ". Enter Communication By\\r\\n";
        }

        if (i > 1){
            Alert(sMessage);
            return false;
        }
        return true;
    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string[] aCommn = null;
            if (validSave())
            {
                if (drpcontactdate.SelectedIndex == 0)
                {
                    aCommn = new string[11];
                    aCommn[0] = txtcommunicationby.Text.Trim();
                    aCommn[1] = drpCompany.SelectedItem.Value.Trim();
                    aCommn[2] = drpleadstatustype.SelectedItem.Value.Trim();
                    aCommn[3] = drpcontname.SelectedItem.Value.Trim();
                    aCommn[4] = drpcommunicationtype.SelectedItem.Value.Trim();
                    aCommn[5] = drpfollowuptype.SelectedItem.Value.Trim();
                    aCommn[6] = txtcontactdate.Text.Trim();
                    aCommn[7] = txtFollowupDate.Text.Trim();
                    aCommn[8] = txtComments.Text.Trim();
                    aCommn[9] = txtCommunication.Text.Trim();
                    aCommn[10] = Session["employeeid"].ToString();
                    string msg = oSales.InsertCommunication(aCommn);
                    if (msg.ToLower().Contains("error")) Alert(msg);
                    else
                    {
                        this.drpcontname_SelectedIndexChanged(null, null);
                        if (drpcontactdate.Items.FindByValue(msg) != null){
                            drpcontactdate.Items.FindByValue(msg).Selected = true;
                            this.loadCommunicationDetails(msg);
                        }
                        Alert("Successfully Saved");
                    }
                }
                else
                {
                    aCommn = new string[9];
                    aCommn[0] = drpcontactdate.SelectedItem.Value.Trim();
                    aCommn[1] = txtcommunicationby.Text.Trim();
                    aCommn[2] = drpCompany.SelectedItem.Value.Trim();
                    aCommn[3] = drpleadstatustype.SelectedItem.Value.Trim();
                    aCommn[4] = drpcommunicationtype.SelectedItem.Value.Trim();
                    aCommn[5] = drpfollowuptype.SelectedItem.Value.Trim();
                    aCommn[6] = txtFollowupDate.Text.Trim();
                    aCommn[7] = txtComments.Text.Trim();
                    aCommn[8] = txtCommunication.Text.Trim();
                    string msg = oSales.UpdateCommunication(aCommn);
                    if (msg.ToLower().Contains("error")) Alert(msg);
                    else
                    {
                        this.drpcontname_SelectedIndexChanged(null, null);
                        if (drpcontactdate.Items.FindByValue(msg) != null)
                        {
                            drpcontactdate.Items.FindByValue(msg).Selected = true;
                            this.loadCommunicationDetails(msg);
                        }
                        Alert("Successfully Saved");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Alert(ex.Message);
            throw new Exception(ex.Message);
        }
    }
    protected void imgbtnNewPubInfo_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("sales_publisher_info.aspx", true);
    }
    protected void imgbtnUpdatePubInfo_Click(object sender, ImageClickEventArgs e)
    {
        if (drpCompany.SelectedItem.Value != "0")
            Response.Redirect("sales_publisher_info.aspx?slno=" + drpCompany.SelectedItem.Value.Trim(), true);
        else Alert("Select a Publisher.");
    }
}
