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
using System.Text;

public partial class Delphi_Job_Issue : System.Web.UI.Page
{
    protected int id = 1;
    private Delphi_Issue oIssue = new Delphi_Issue();
    private static DataTable dtIssue = new DataTable();
    private static string sSortExpression = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //Todo
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();
        else
        {
            if (oIssue.IsInvoiced(hfI_ID.Value.Trim())) this.toggleSaveOption(false);
            else this.toggleSaveOption(true);
        }
    }
    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    private void popScreen()
    {
        //pop customers
        DataSet dscust = oIssue.getCustomers();
        drpIssuecustomer.DataSource = dscust;
        drpIssuecustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        drpIssuecustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        drpIssuecustomer.DataBind();
        drpIssuecustomer.Items.Insert(0, new ListItem("-- select --", "0"));
        drpIssueJournal.Items.Insert(0, new ListItem("-- select --", "0"));
        //--
        lstAdvancedCustomer.DataSource = dscust;
        lstAdvancedCustomer.DataTextField = dscust.Tables[0].Columns[1].ToString();
        lstAdvancedCustomer.DataValueField = dscust.Tables[0].Columns[0].ToString();
        lstAdvancedCustomer.DataBind();
        //pop stages
        DataSet dsStage = oIssue.getIssueStages();
        drpIssueStage.DataSource = dsStage;
        drpIssueStage.DataTextField = dsStage.Tables[0].Columns[1].ToString();
        drpIssueStage.DataValueField = dsStage.Tables[0].Columns[0].ToString();
        drpIssueStage.DataBind();
        drpIssueStage.Items.Insert(0, new ListItem("-- select --", "0"));
        //pop advanc search stages
        DataSet dsAdvStage = oIssue.getStageTypes();
        lstAdvancedStage.DataSource = dsAdvStage;
        lstAdvancedStage.DataTextField = dsAdvStage.Tables[0].Columns[1].ToString();
        lstAdvancedStage.DataValueField = dsAdvStage.Tables[0].Columns[0].ToString();
        lstAdvancedStage.DataBind();
        //pop OnHold type
        DataSet dsHoldtyp = oIssue.getOnHoldTypes();
        drpIssueOnHoldType.DataSource = dsHoldtyp;
        drpIssueOnHoldType.DataTextField = dsHoldtyp.Tables[0].Columns[1].ToString();
        drpIssueOnHoldType.DataValueField = dsHoldtyp.Tables[0].Columns[0].ToString();
        drpIssueOnHoldType.DataBind();
        drpIssueOnHoldType.Items.Insert(0, new ListItem("-- select --", "0"));
        //pop cover month & year
        for (int i = 0; i < 12; i++) drpIssueCoverMonth.Items.Insert(i, new ListItem((i + 1).ToString(), (i + 1).ToString()));
        for (int j = 2008; j <= DateTime.Now.Year + 2; j++) drpIssueCoverYear.Items.Insert(0, new ListItem(j.ToString(), j.ToString()));
        drpIssueCoverMonth.Items.Insert(0, new ListItem("-- select --", "0"));
        drpIssueCoverYear.Items.Insert(0, new ListItem("-- select --", "0"));
        // pop sales job group
        DataSet dsSalesGrp = oIssue.getSalesGroup();
        drpIssueSalesGroup.DataSource = dsSalesGrp;
        drpIssueSalesGroup.DataTextField = dsSalesGrp.Tables[0].Columns[1].ToString();
        drpIssueSalesGroup.DataValueField = dsSalesGrp.Tables[0].Columns[0].ToString();
        drpIssueSalesGroup.DataBind();
        drpIssueSalesGroup.Items.Insert(0, new ListItem("-- select --", "0"));
        //
        txtIssueSdate.Attributes.Add("readonly", "readonly");
        txtIssueDdate.Attributes.Add("readonly", "readonly");
        txtIssueActualDdate.Attributes.Add("readonly", "readonly");
        txtIssueCDdate.Attributes.Add("readonly", "readonly");
        txtAdvRecDate1.Attributes.Add("readonly", "readonly");
        txtAdvRecDate2.Attributes.Add("readonly", "readonly");
        txtAdvDueDate1.Attributes.Add("readonly", "readonly");
        txtAdvDueDate2.Attributes.Add("readonly", "readonly");
        txtAdvHlfDueDate1.Attributes.Add("readonly", "readonly");
        txtAdvHlfDueDate2.Attributes.Add("readonly", "readonly");
        txtAdvCatsDueDate1.Attributes.Add("readonly", "readonly");
        txtAdvCatsDueDate2.Attributes.Add("readonly", "readonly");

        //pop Invoice type and cost type
        DataSet dsCost = oIssue.getIssueCostDetailsByID("", "");
        ////////////////drpCostInvoiceType.DataSource = dsCost.Tables[0];
        ////////////////drpCostInvoiceType.DataValueField = dsCost.Tables[0].Columns[0].ToString();
        ////////////////drpCostInvoiceType.DataTextField = dsCost.Tables[0].Columns[1].ToString();
        ////////////////drpCostInvoiceType.DataBind();
        ////////////////drpCostType.DataSource = dsCost.Tables[1];
        ////////////////drpCostType.DataValueField = dsCost.Tables[1].Columns[0].ToString();
        ////////////////drpCostType.DataTextField = dsCost.Tables[1].Columns[1].ToString();
        ////////////////drpCostType.DataBind();
        ////////////////drpCostInvoiceType.Items.Insert(0, new ListItem("-- select --", "0"));
        ////////////////drpCostInvoiceTypeItem.Items.Insert(0, new ListItem("-- select --", "0"));
        ////////////////drpCostType.Items.Insert(0, new ListItem("-- select --", "0"));
        ////////////////txtCostQuantity.Enabled = true;
        ////////////////txtCostQuantity.Text = "0";
       
        //show 1st panel
        if (Request.QueryString["q"] != null &&
            Request.QueryString["q"].ToString().Trim() != "")
        {
            string pageQuery = Request.QueryString["q"].ToString().Trim();
            switch (pageQuery)
            {
                case "new_issue":
                    drpIssuecustomer.Enabled = true;
                    drpIssueJournal.Enabled = true;
                    txtIssueNo.Enabled = true;
                    lblIssueHeader.Text = "New Issue";
                    imgIssueHeader.Src = "images/tools/new.png";
                    imgID_stdate.Visible = false;
                    imgID_dudate.Visible = false;
                    //imgID_hdudate.Visible = false;
                    imgID_cdudate.Visible = false;
                    chkIssueDespatch.Enabled = false;
                    this.showPanel(tabIssueDetails);
                    break;
            }
        }
        else this.showPanel(tabGeneral);
        txtSearch.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) ||(event.keyCode == 13)){document.getElementById('" + btnSearch.ClientID + "').click();return false;}} else {return true}; ");
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                if (this.hfI_Name.Value != "")
                    lblIssueSummary.Text = "Issue : " + this.hfI_Journal.Value.Trim() + " " + this.hfI_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "current");
                miIssueDetails.Attributes.Add("class", "");
                miIssueAddCost.Attributes.Add("class", "");
                miIssueEvents.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miArticlesAssigned.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabIssueDetails.Visible = false;
                this.tabIssueAddCost.Visible = false;
                this.tabIssueEvents.Visible = false;
                this.tabArticlesAssigned.Visible = false;
                this.tabComments.Visible = false;
                break;
            case "tabIssueDetails":
                if (this.hfI_Name.Value != "")
                    lblIssueHeader.Text = "Edit Issue : " + this.hfI_Journal.Value.Trim() + " " + this.hfI_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miIssueDetails.Attributes.Add("class", "current");
                miIssueAddCost.Attributes.Add("class", "");
                miIssueEvents.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miArticlesAssigned.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabIssueDetails.Visible = true;
                this.tabIssueAddCost.Visible = false;
                this.tabIssueEvents.Visible = false;
                this.tabArticlesAssigned.Visible = false;
                this.tabComments.Visible = false;
                break;
            case "tabIssueAddCost":
                if (this.hfI_Name.Value != "")
                    lblCostHeader.Text = "Issue Cost : " + this.hfI_Journal.Value.Trim() + " " + this.hfI_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miIssueDetails.Attributes.Add("class", "");
                miIssueAddCost.Attributes.Add("class", "current");
                miIssueEvents.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miArticlesAssigned.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabIssueDetails.Visible = false;
                this.tabIssueAddCost.Visible = true;
                this.tabIssueEvents.Visible = false;
                this.tabArticlesAssigned.Visible = false;
                this.tabComments.Visible = false;
                break;
            case "tabIssueEvents":
                if (this.hfI_Name.Value != "")
                    lblEventsHeader.Text = "Logged Events : " + this.hfI_Journal.Value.Trim() + " " + this.hfI_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miIssueDetails.Attributes.Add("class", "");
                miIssueAddCost.Attributes.Add("class", "");
                miIssueEvents.Attributes.Add("class", "current");
                miComments.Attributes.Add("class", "");
                miArticlesAssigned.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabIssueDetails.Visible = false;
                this.tabIssueAddCost.Visible = false;
                this.tabIssueEvents.Visible = true;
                this.tabArticlesAssigned.Visible = false;
                this.tabComments.Visible = false;
                break;
            case "tabComments":
                if (this.hfI_Name.Value != "")
                    lblCommentsHeader.Text = "Comments History : " + this.hfI_Journal.Value.Trim() + " " + this.hfI_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miIssueDetails.Attributes.Add("class", "");
                miIssueAddCost.Attributes.Add("class", "");
                miIssueEvents.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "current");
                miArticlesAssigned.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabIssueDetails.Visible = false;
                this.tabIssueAddCost.Visible = false;
                this.tabIssueEvents.Visible = false;
                this.tabArticlesAssigned.Visible = false;
                this.tabComments.Visible = true;
                break;
            default:
                if (this.hfI_Name.Value != "")
                    lblArticlesHeader.Text = "Articles Assigned for : " + this.hfI_Journal.Value.Trim() + " " + this.hfI_Name.Value.Trim();
                miGeneral.Attributes.Add("class", "");
                miIssueDetails.Attributes.Add("class", "");
                miIssueAddCost.Attributes.Add("class", "");
                miIssueEvents.Attributes.Add("class", "");
                miComments.Attributes.Add("class", "");
                miArticlesAssigned.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabIssueDetails.Visible = false;
                this.tabIssueAddCost.Visible = false;
                this.tabIssueEvents.Visible = false;
                this.tabArticlesAssigned.Visible = true;
                this.tabComments.Visible = false;
                break;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        sSortExpression = "";
        if (Request["__EVENTARGUMENT"] != null
            && Request["__EVENTARGUMENT"].ToLower() == "advanced")
        {
            char IsHold = 'N';
            string[] aIssSearch = new string[23];
            string sCompleted = "";
            if (chkAdvOnHold.Checked) IsHold = 'Y';
            if (chkAdvCompleted.Checked) sCompleted = "completed";
            txtAdvJourCode.Text = txtAdvJourCode.Text.ToUpper();
            aIssSearch[0] = "";
            aIssSearch[1] = "";
            aIssSearch[2] = "advanced";
            aIssSearch[3] = sCompleted;
            aIssSearch[4] = txtAdvJourCode.Text.Trim();
            aIssSearch[5] = drpAdvJourCodeExp.SelectedItem.Value;
            aIssSearch[6] = txtAdvIssueNum.Text.Trim();
            aIssSearch[7] = drpAdvIssueNumExp.SelectedItem.Value;
            aIssSearch[8] = IsHold.ToString();
            aIssSearch[9] = drpAdvRecExpr.SelectedItem.Value;
            aIssSearch[10] = txtAdvRecDate1.Text.Trim();
            aIssSearch[11] = txtAdvRecDate2.Text.Trim();
            aIssSearch[12] = drpAdvDueExpr.SelectedItem.Value;
            aIssSearch[13] = txtAdvDueDate1.Text.Trim();
            aIssSearch[14] = txtAdvDueDate2.Text.Trim();
            aIssSearch[15] = drpAdvHlfDueRecExpr.SelectedItem.Value;
            aIssSearch[16] = txtAdvHlfDueDate1.Text.Trim();
            aIssSearch[17] = txtAdvHlfDueDate2.Text.Trim();
            aIssSearch[18] = drpAdvCatsDueExpr.SelectedItem.Value;
            aIssSearch[19] = txtAdvCatsDueDate1.Text.Trim();
            aIssSearch[20] = txtAdvCatsDueDate2.Text.Trim();
            string sStageIDs = "";
            for (int x = 0; x < lstAdvancedStage.Items.Count; x++)
            {
                if (lstAdvancedStage.Items[x].Selected)
                    sStageIDs += lstAdvancedStage.Items[x].Value + ",";
            }
            sStageIDs = sStageIDs.TrimEnd(',');
            string sCustIDs = "";
            for (int y = 0; y < lstAdvancedCustomer.Items.Count; y++)
            {
                if (lstAdvancedCustomer.Items[y].Selected)
                    sCustIDs += lstAdvancedCustomer.Items[y].Value + ",";
            }
            sCustIDs = sCustIDs.TrimEnd(',');
            aIssSearch[21] = sStageIDs;
            aIssSearch[22] = sCustIDs;
            DataSet dsi = oIssue.getIssues(aIssSearch);
            dtIssue = dsi.Tables[0].Copy();
            gvIssues.DataSource = dsi;
            gvIssues.DataBind();
        }
        else
        {
            char CompleteFlag = 'N';
            if (chkViewCompleted.Checked) CompleteFlag = 'Y';
            DataSet dsi = oIssue.getIssues(txtSearch.Text.Trim().ToUpper(), CompleteFlag);
            dtIssue = dsi.Tables[0].Copy();
            gvIssues.DataSource = dsi;
            gvIssues.DataBind();
        }
        this.hfI_ID.Value = "";
        this.hfI_Name.Value = "";
        this.showPanel(tabGeneral);
    }
    protected void gvIssues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,'" + ((HiddenField)e.Row.FindControl("hfgvIssueID")).Value.Trim() + "','" + ((HiddenField)e.Row.FindControl("hfgvIssueName")).Value.Trim() + "','" + ((HiddenField)e.Row.FindControl("hfgvJournal")).Value.Trim() + "');";
            e.Row.Attributes["ondblclick"] =
                "javascript:__doPostBack('lnkArticledetails','')";
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            foreach (TableCell cell in e.Row.Cells)
            {
                if (cell.HasControls())
                {
                    LinkButton btn = (LinkButton)cell.Controls[0];
                    if (btn != null)
                    {
                        Image img = new Image(); img.Attributes.Add("align", "absmiddle");
                        if (sSortExpression == btn.CommandArgument && ViewState[sSortExpression] != null)
                        {
                            if ((SortDirection)ViewState[sSortExpression] == SortDirection.Ascending)
                                img.ImageUrl = "~/images/tools/arrow_up.png";
                            else
                                img.ImageUrl = "~/images/tools/arrow_down.png";
                            cell.Controls.Add(img);
                        }
                    }
                }
            }
        }
    }
    private void loadIssueDetails(string sIssueID)
    {
        sIssueID = sIssueID.Trim();
        if (sIssueID != "")
        {
            drpIssuecustomer.Enabled = false;
            drpIssueJournal.Enabled = false;
            txtIssueNo.Enabled = false;
            txtIssueSdate.Text = "";
            txtIssueDdate.Text = "";
            txtIssueActualDdate.Text = "";
            txtIssueCDdate.Text = "";
            imgID_stdate.Visible = false;
            imgID_dudate.Visible = false;
            //imgID_hdudate.Visible = false;
            imgID_cdudate.Visible = false;
            //pop details
            DataSet dsIssue = oIssue.getIssueDetailsByID(sIssueID);
            lblIssueHeader.Text = "Edit Issue";
            imgIssueHeader.Src = "images/tools/edit.png";
            DataRow row = dsIssue.Tables[0].Rows[0];
            if (row["IISSUENO"].ToString().Trim() != "") this.toggleSaveOption(false);
            else this.toggleSaveOption(true);
            drpIssuecustomer.ClearSelection();
            if (drpIssuecustomer.Items.FindByValue(row["custno"].ToString().Trim()) != null)
                drpIssuecustomer.Items.FindByValue(row["custno"].ToString().Trim()).Selected = true;
            drpIssuecustomer_SelectedIndexChanged(null, null);
            if (drpIssueJournal.Items.FindByValue(row["Journo"].ToString().Trim()) != null)
                drpIssueJournal.Items.FindByValue(row["Journo"].ToString().Trim()).Selected = true;
            txtIssueNo.Text = row["IIssueNo"].ToString().Trim();
          //  txtIssueTitle.Text = row["TITLE"].ToString().Trim();
            if (row["onhold_history_id"].ToString().Trim() != "")
                chkIssueOnHold.Checked = true;
            else chkIssueOnHold.Checked = false;
            drpIssueCoverMonth.ClearSelection();
            if (drpIssueCoverMonth.Items.FindByValue(row["ICOVERMONTH"].ToString().Trim()) != null)
                drpIssueCoverMonth.Items.FindByValue(row["ICOVERMONTH"].ToString().Trim()).Selected = true;
            drpIssueCoverYear.ClearSelection();
            if (drpIssueCoverYear.Items.FindByValue(row["ICOVERYEAR"].ToString().Trim()) != null)
                drpIssueCoverYear.Items.FindByValue(row["ICOVERYEAR"].ToString().Trim()).Selected = true;
            drpIssueSalesGroup.ClearSelection();
            if (drpIssueSalesGroup.Items.FindByValue(row["sales_job_group_id"].ToString().Trim()) != null)
                drpIssueSalesGroup.Items.FindByValue(row["sales_job_group_id"].ToString().Trim()).Selected = true;
            txtIssueComments.Text = row["comments"].ToString().Trim();
         ////////////   txtIssueInvoiceDesc.Text = row["invoice_description"].ToString().Trim();
            drpIssueStage.ClearSelection();
            if (drpIssueStage.Items.FindByValue(row["STYPENO"].ToString().Trim()) != null)
                drpIssueStage.Items.FindByValue(row["STYPENO"].ToString().Trim()).Selected = true;
          
            //drpIssueStage.Items.Insert(0, new ListItem("-- select --", "0"));
            chkIssueDespatch.Checked = false;
            drpWithdraw.SelectedValue = row["InvStatus"].ToString();
            if (dsIssue.Tables[1].Rows[0] != null)
            {
                if (dsIssue.Tables[1].Rows[0]["despatch_date"].ToString().Trim() == "")
                {
                    //txtIssueSdate.Text = dsIssue.Tables[2].Rows[0]["received_date"].ToString().Trim();
                    //txtIssueDdate.Text = dsIssue.Tables[2].Rows[0]["due_date"].ToString().Trim();
                    //txtIssueActualDdate.Text = dsIssue.Tables[2].Rows[0]["half_due_date"].ToString().Trim();
                    //txtIssueCDdate.Text = dsIssue.Tables[2].Rows[0]["cats_due_date"].ToString().Trim();
                    chkIssueDespatch.Enabled = true;
                }
                else chkIssueDespatch.Enabled = false;
            }
            else
            {
                //txtIssueSdate.Text = "";
                //txtIssueDdate.Text = "";
                //txtIssueActualDdate.Text = "";
                //txtIssueCDdate.Text = "";
                chkIssueDespatch.Enabled = false;
            }
            dgrdIssueStages.DataSource = dsIssue.Tables[1];
            dgrdIssueStages.DataBind();
        }
    }
    private void toggleSaveOption(bool IsVisible)
    {
        if (IsVisible)
        {
            cmd_Save_Issue.Visible = true;
            //Comment by subbu 31 may 2012 for testing sivaraj request
            //if (Session["invoicegroup"] != null && Session["invoicegroup"].ToString() == "true"){
            //    cmd_Save_Cost.Visible = true;
            //    cmd_Cost_orderindex.Visible = true;
            //}
            //else{
            //    cmd_Save_Cost.Visible = false;
            //    cmd_Cost_orderindex.Visible = false;
            //}
            imgbtnPaginate.Visible = true;
            chkIssueOnHold.Enabled = true;
        }
        else
        {
            cmd_Save_Issue.Visible = true;
            //cmd_Save_Cost.Visible = false;
            //cmd_Cost_orderindex.Visible = false;
            imgbtnPaginate.Visible = false;
        
        }

    }
    protected void cmd_New_Issue_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Delphi_job_issue.aspx?q=new_issue", true);
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpIssuecustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (drpIssueJournal.SelectedItem.Value == "0") sMessage += i++ + ". Select a Journal\\r\\n";
        if (txtIssueNo.Text.Trim() == "") sMessage += i++ + ". Enter Issue No.\\r\\n";
        if (hfI_ID.Value.Trim() == "")
        {
            if (drpIssueStage.SelectedItem.Value == "0")/* ||
            txtIssueSdate.Text == "" || txtIssueDdate.Text == "" || txtIssueActualDdate.Text == "")*/
            {
                //sMessage += i++ + ". Select a Job Stage, Enter start date, \\rdue date and half due date\\r\\n";
                sMessage += i++ + ". Select a Issue Stage\\r\\n";
            }
            else if (txtIssueSdate.Text == "" || txtIssueDdate.Text == "" || txtIssueActualDdate.Text == "")
            {
                //sMessage += i++ + ". Enter start date, due date and half due date\\r\\n";
            }
            else
            {
                if (DateTime.Parse(txtIssueDdate.Text) < DateTime.Parse(txtIssueSdate.Text))
                {
                    sMessage += i++ + ". Invalid Due Date\\r\\n";
                }
                if ((DateTime.Parse(txtIssueSdate.Text) > DateTime.Parse(txtIssueActualDdate.Text)) ||
                    (DateTime.Parse(txtIssueActualDdate.Text) > DateTime.Parse(txtIssueDdate.Text)))
                {
                    sMessage += i++ + ". Invalid Half Due Date\\r\\n";
                }
            }
        }
        else
        {
            if (drpIssueStage.SelectedItem.Value != "0")
            {
                if (txtIssueSdate.Text == "" || txtIssueDdate.Text == "" || txtIssueActualDdate.Text == "")
                {
                    //sMessage += i++ + ". Enter start date, due date and half due date\\r\\n";
                }
                else
                {
                    if (DateTime.Parse(txtIssueDdate.Text) < DateTime.Parse(txtIssueSdate.Text))
                    {
                        sMessage += i++ + ". Invalid Due Date\\r\\n";
                    }
                    if ((DateTime.Parse(txtIssueSdate.Text) > DateTime.Parse(txtIssueActualDdate.Text)) ||
                        (DateTime.Parse(txtIssueActualDdate.Text) > DateTime.Parse(txtIssueActualDdate.Text)))
                    {
                        sMessage += i++ + ". Invalid Half Due Date\\r\\n";
                    }
                }
            }
        }
        //if (drpIssueSalesGroup.SelectedItem.Value == "0") sMessage += i++ + ". Select a Sales Group\\r\\n";
        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }
    protected void cmd_Save_Issue_Click(object sender, ImageClickEventArgs e)
    {
        string[] aIssueDetails;
        try
        {
            if (validateScreen())
            {
                if (hfI_ID.Value.Trim() == "")
                {
                    //insert Issue
                    aIssueDetails = new string[18];
                    aIssueDetails[0] = "6"; // jobtype_id for Issue
                    aIssueDetails[1] = txtIssueNo.Text.Trim();
                    aIssueDetails[2] = txtIssueTitle.Text.Trim();
                    aIssueDetails[3] = drpIssuecustomer.SelectedItem.Value.Trim();
                    aIssueDetails[4] = drpIssueJournal.SelectedItem.Value.Trim();
                    aIssueDetails[5] = txtIssueComments.Text.Trim();
                    //stage
                    aIssueDetails[6] = drpIssueStage.SelectedItem.Value.Trim();
                    if (txtIssueSdate.Text.Trim() != "") aIssueDetails[7] = DateTime.Parse(txtIssueSdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[7] = "";
                    if (txtIssueDdate.Text.Trim() != "") aIssueDetails[8] = DateTime.Parse(txtIssueDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[8] = "";
                    if (txtIssueActualDdate.Text.Trim() != "") aIssueDetails[9] = DateTime.Parse(txtIssueActualDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[9] = "";
                    if (chkIssueDespatch.Checked) aIssueDetails[10] = DateTime.Now.ToString("MM/dd/yyyy");
                    else aIssueDetails[10] = "";
                    if (txtIssueCDdate.Text.Trim() != "") aIssueDetails[11] = DateTime.Parse(txtIssueCDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[11] = "";
                    aIssueDetails[12] = Session["employeeid"].ToString().Trim();
                    aIssueDetails[13] = txtIssueInvoiceDesc.Text.Trim();
                    aIssueDetails[14] = drpIssueCoverMonth.SelectedItem.Value.Trim();
                    aIssueDetails[15] = drpIssueCoverYear.SelectedItem.Value.Trim();
                    aIssueDetails[16] = drpIssueSalesGroup.SelectedItem.Value.Trim();
                    aIssueDetails[17] = drpWithdraw.SelectedItem.Value.Trim();
                    string msg = this.oIssue.InsertIssue(aIssueDetails);
                    if (msg.Contains("Issue creation failed!") ||
                        msg.Contains("Issue Already Exists!")) Alert(msg);
                    else
                    {
                        hfI_ID.Value = msg;
                        this.loadIssueDetails(msg);
                        Alert("Successfully Saved.");
                    }
                }
                else
                {
                    //update Issue
                    aIssueDetails = new string[19];
                    aIssueDetails[0] = hfI_ID.Value.Trim();
                    aIssueDetails[1] = "6"; // jobtype_id for Issue
                    aIssueDetails[2] = txtIssueNo.Text.Trim();
                    aIssueDetails[3] = txtIssueTitle.Text.Trim();
                    aIssueDetails[4] = drpIssuecustomer.SelectedItem.Value.Trim();
                    aIssueDetails[5] = drpIssueJournal.SelectedItem.Value.Trim();
                    aIssueDetails[6] = txtIssueComments.Text.Trim();
                    //stage
                    aIssueDetails[7] = drpIssueStage.SelectedItem.Value.Trim();
                    if (txtIssueSdate.Text.Trim() != "") aIssueDetails[8] = DateTime.Parse(txtIssueSdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[8] = "";
                    if (txtIssueDdate.Text.Trim() != "") aIssueDetails[9] = DateTime.Parse(txtIssueDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[9] = "";
                    if (txtIssueActualDdate.Text.Trim() != "") aIssueDetails[10] = DateTime.Parse(txtIssueActualDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[10] = "";
                    if (chkIssueDespatch.Checked) aIssueDetails[11] = DateTime.Now.ToString("MM/dd/yyyy");
                    else aIssueDetails[11] = "";
                    if (txtIssueCDdate.Text.Trim() != "") aIssueDetails[12] = DateTime.Parse(txtIssueCDdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else aIssueDetails[12] = "";
                    aIssueDetails[13] = Session["employeeid"].ToString().Trim();
                    aIssueDetails[14] = txtIssueInvoiceDesc.Text.Trim();
                    aIssueDetails[15] = drpIssueCoverMonth.SelectedItem.Value.Trim();
                    aIssueDetails[16] = drpIssueCoverYear.SelectedItem.Value.Trim();
                    aIssueDetails[17] = drpIssueSalesGroup.SelectedItem.Value.Trim();
                    aIssueDetails[18] = drpWithdraw.SelectedItem.Value.Trim();
                    string msg = this.oIssue.UpdateIssue(aIssueDetails);
                    if (msg.Contains("Error updating Issue:")) Alert(msg);
                    else
                    {
                        this.loadIssueDetails(msg);
                        Alert("Successfully Saved.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally { aIssueDetails = null; }
    }
    protected void cmd_Print_Issue_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        this.showPanel(tabGeneral);
    }
    protected void lnkIssuedetails_Click(object sender, EventArgs e)
    {
        if (hfI_ID.Value != "")
        {
            loadIssueDetails(hfI_ID.Value.Trim());
        }
        this.showPanel(tabIssueDetails);
    }
    protected void lnkIssueEvents_Click(object sender, EventArgs e)
    {
        if (hfI_ID.Value != "")
        {
            DataSet dsEvents = oIssue.getIssueEvents(hfI_ID.Value.Trim());
            gvEvents.DataSource = dsEvents.Tables[0];
            gvEvents.DataBind();
        }
        this.showPanel(tabIssueEvents);
    }
    protected void lnkComments_Click(object sender, EventArgs e)
    {
        if (hfI_ID.Value != "")
        {
            DataSet dsComment = oIssue.getIssueComments(hfI_ID.Value.Trim());
            gvCommHistory.DataSource = dsComment.Tables[0];
            gvCommHistory.DataBind();
        }
        this.showPanel(tabComments);
    }
    protected void lnkArticlesAssigned_Click(object sender, EventArgs e)
    {
        if (hfI_ID.Value != "")
        {
            DataSet dsAA = oIssue.getArticlesAssigned(hfI_ID.Value.Trim());
            gvArtilcesAssigned.DataSource = dsAA.Tables[1];
            gvArtilcesAssigned.DataBind();
        }
        this.showPanel(tabArticlesAssigned);
    }
    protected void lnkIssueAddCost_Click(object sender, EventArgs e)
    {
        trBCCtrls.Disabled = false;
        imgbtnBCAddInvTypeItem.Visible = false;
        if (hfI_ID.Value != "")
        {
            loadIssueCostDetails(hfI_ID.Value.Trim());
        }
        this.showPanel(tabIssueAddCost);
        this.clearIssueCost();
    }
    protected void drpIssuecustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpIssueJournal.Items.Clear();
        if (drpIssuecustomer.SelectedItem.Value != "0")
        {
            DataSet dsJour = oIssue.getJournalsByCustomer(drpIssuecustomer.SelectedItem.Value.Trim());
            drpIssueJournal.DataSource = dsJour;
            drpIssueJournal.DataTextField = dsJour.Tables[0].Columns["jourcode"].ToString();
            drpIssueJournal.DataValueField = dsJour.Tables[0].Columns["journal_id"].ToString();
            drpIssueJournal.DataBind();
        }
        drpIssueJournal.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void drpIssueJournal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //////////////////////////drpIssueStage.Items.Clear();
        //////////////////////////if (drpIssueJournal.SelectedItem.Value != "0")
        //////////////////////////{
        //////////////////////////    DataSet dsStag = oIssue.getIssueStagesByJournal(drpIssueJournal.SelectedItem.Value.Trim());
        //////////////////////////    drpIssueStage.DataSource = dsStag.Tables[0];
        //////////////////////////    drpIssueStage.DataTextField = dsStag.Tables[0].Columns["job_stage_name"].ToString();
        //////////////////////////    drpIssueStage.DataValueField = dsStag.Tables[0].Columns["qualify_job_stage_id"].ToString();
        //////////////////////////    drpIssueStage.DataBind();
        //////////////////////////}
        //////////////////////////drpIssueStage.Items.Insert(0, new ListItem("-- select --", "0"));
        //////////////////////////if (hfI_ID.Value.Trim() == "" && drpIssueStage.Items.Count > 1) drpIssueStage.Items[1].Selected = true;
    }
    protected void drpIssueStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkIssueDespatch.Checked = false;
        if (drpIssueStage.SelectedItem.Value != "0")
        {
            //if (drpIssueStage.SelectedItem.Value != "0" && hfA_ID.Value.Trim() != "")
            //{
            //DataSet dst = oArt.getIssueStageByID(hfA_ID.Value.Trim(), drpIssueStage.SelectedItem.Value.Trim());
            //if (dst.Tables[0].Rows.Count > 0){
            //    DataRow newrow = dst.Tables[0].Rows[0];
            //    txtIssueSdate.Text = newrow["received_date"].ToString();
            //    txtIssueDdate.Text = newrow["due_date"].ToString();
            //    txtIssueActualDdate.Text = newrow["half_due_date"].ToString();
            //    txtIssueCDdate.Text = newrow["cats_due_date"].ToString();
            //    imgID_stdate.Visible = false;
            //    imgID_dudate.Visible = false;
            //    //imgID_hdudate.Visible = false;
            //    chkIssueDespatch.Enabled = true;
            //}
            //else{
            txtIssueSdate.Text = "";
            txtIssueDdate.Text = "";
            txtIssueActualDdate.Text = "";
            txtIssueCDdate.Text = "";
            imgID_stdate.Visible = true;
            imgID_dudate.Visible = true;
           // imgID_hdudate.Visible = true;
            imgID_cdudate.Visible = true;
            chkIssueDespatch.Enabled = false;
            //}
        }
        else
        {
            txtIssueSdate.Text = "";
            txtIssueDdate.Text = "";
            txtIssueActualDdate.Text = "";
            txtIssueCDdate.Text = "";
            imgID_stdate.Visible = false;
            imgID_dudate.Visible = false;
            //imgID_hdudate.Visible = false;
            imgID_cdudate.Visible = false;
            chkIssueDespatch.Enabled = false;
        }
    }
    protected void lnkIssueHold_Click(object sender, EventArgs e)
    {
        try
        {
            int val = 0;
            if (hfI_ID.Value.Trim() != "")
            {
                if (chkIssueOnHold.Checked)
                {
                    string res = oIssue.InsertJobOnHold(hfI_ID.Value.Trim(), "6", drpIssueOnHoldType.SelectedItem.Value.Trim(),
                        txtIssueOnHoldReason.Text.Trim(), Session["employeeid"].ToString());
                    if (int.TryParse(res, out val))
                    {
                        this.loadIssueDetails(hfI_ID.Value.Trim());
                        Alert("Successfully saved.");
                    }
                    else Alert("Onhold process failed!");
                }
                else
                {
                    string res = oIssue.UpdateJobOnHold(hfI_ID.Value.Trim(), "6", drpIssueOnHoldType.SelectedItem.Value.Trim());
                    if (int.TryParse(res, out val))
                    {
                        this.loadIssueDetails(hfI_ID.Value.Trim());
                        Alert("Successfully saved.");
                    }
                    else Alert("Onhold process failed!");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void cmd_Cost_new_Click(object sender, ImageClickEventArgs e)
    {
        this.clearIssueCost();
        imgbtnBCAddInvTypeItem.Visible = false;
        trBCCtrls.Disabled = false;
        this.loadIssueCostDetails(hfI_ID.Value.Trim());
    }
    private void loadIssueCost(string sBCJobInvTypeID)
    {
        DataSet dsc = this.oIssue.getIssueCostByID(sBCJobInvTypeID);
        if (dsc.Tables[0].Rows.Count > 0)
        {
            this.clearIssueCost();
            DataRow r = dsc.Tables[0].Rows[0];
            hfCostInvTypeItemID.Value = r["job_invoice_type_item_id"].ToString();
            if (drpCostInvoiceType.Items.FindByValue(r["Invoice_Type_id"].ToString().Trim()) != null)
                drpCostInvoiceType.Items.FindByValue(r["Invoice_Type_id"].ToString().Trim()).Selected = true;
            this.drpCostInvoiceType_SelectedIndexChanged(null, null);
            if (drpCostInvoiceTypeItem.Items.FindByValue(r["invoicetype_item_id"].ToString().Trim()) != null)
                drpCostInvoiceTypeItem.Items.FindByValue(r["invoicetype_item_id"].ToString().Trim()).Selected = true;
            if (drpCostType.Items.FindByValue(r["cost_type_id"].ToString().Trim()) != null)
                drpCostType.Items.FindByValue(r["cost_type_id"].ToString().Trim()).Selected = true;
            txtCostQuantity.Text = r["quantity_addnl_type"].ToString().Trim();
            txtCostPriceCode.Text = r["price_code"].ToString().Trim();
            txtCostItemdesc.Text = r["description"].ToString().Trim();
        }
        else Alert("Record Does Not Exists");
    }
    private void loadIssueCostDetails(string sIssueID)
    {
        sIssueID = sIssueID.Trim();
        if (sIssueID != "")
        {
            DataSet dsbc = this.oIssue.getIssueCostDetailsByID(sIssueID, "1");
            gvIssueCost.DataSource = dsbc.Tables[0];
            gvIssueCost.DataBind();
            gvIssueCost.Visible = true;
            gvIssueCost.Columns[6].Visible = false;
            gvIssueCost.Columns[7].Visible = true;
        }
    }
    protected void cmd_Save_Cost_Click(object sender, ImageClickEventArgs e)
    {
        string[] aICost; int dummy;
        try
        {
            if (hfI_ID.Value.Trim() != "")
            {
                if (gvIssueCost.Columns[6].Visible == true)
                {
                    aICost = new string[gvIssueCost.Rows.Count];
                    foreach (GridViewRow
                        gvr in gvIssueCost.Rows)
                        aICost[gvr.RowIndex] = ((HiddenField)gvr.FindControl("hfIC_invoicetypeitem")).Value.Trim() + "|" + ((DropDownList)gvr.FindControl("drpIC_orderindex")).SelectedItem.Value;
                    if (this.oIssue.UpdateIssueCostIndex(aICost))
                    {
                        this.loadIssueCostDetails(hfI_ID.Value.Trim());
                        this.clearIssueCost();
                        trBCCtrls.Disabled = false;
                        Alert("Successfully Saved");
                    }
                }
                else if (validCost())
                {
                    if (hfCostInvTypeItemID.Value.Trim() == "")
                    {
                        aICost = new string[8];
                        aICost[0] = "";
                        aICost[1] = hfI_ID.Value.Trim();
                        aICost[2] = "6";
                        aICost[3] = drpCostInvoiceTypeItem.SelectedItem.Value.Trim();
                        aICost[4] = drpCostType.SelectedItem.Value.Trim();
                        aICost[5] = txtCostQuantity.Text.Trim();
                        aICost[6] = txtCostPriceCode.Text.Trim();
                        aICost[7] = txtCostItemdesc.Text.Trim();
                        string res = oIssue.InsertIssueCost(aICost);
                        if (int.TryParse(res, out dummy))
                        {
                            this.loadIssueCostDetails(hfI_ID.Value.Trim());
                            this.clearIssueCost();
                            Alert("Successfully Saved");
                        }
                        else Alert(res);
                    }
                    else
                    {
                        aICost = new string[9];
                        aICost[0] = "";
                        aICost[1] = hfI_ID.Value.Trim();
                        aICost[2] = "6";
                        aICost[3] = drpCostInvoiceTypeItem.SelectedItem.Value.Trim();
                        aICost[4] = drpCostType.SelectedItem.Value.Trim();
                        aICost[5] = txtCostQuantity.Text.Trim();
                        aICost[6] = txtCostPriceCode.Text.Trim();
                        aICost[7] = hfCostInvTypeItemID.Value.Trim();
                        aICost[8] = txtCostItemdesc.Text.Trim();
                        string res = oIssue.UpdateIssueCost(aICost);
                        if (int.TryParse(res, out dummy))
                        {
                            this.loadIssueCostDetails(hfI_ID.Value.Trim());
                            this.clearIssueCost();
                            Alert("Successfully Saved");
                        }
                        else Alert(res);
                    }
                }
            }
            else { Alert("Select a Issue"); }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private bool validCost()
    {
        int i = 1;
        string sMessage = "";
        if (hfI_ID.Value.Trim() == "")
        {
            Alert("Select a Issue");
            return false;
        }
        if (drpCostInvoiceType.SelectedItem.Value == "0") sMessage += i++ + ". Select a Invoice Type\\r\\n";
        if (drpCostInvoiceTypeItem.SelectedItem.Value == "0") sMessage += i++ + ". Select a Invoice Type Item\\r\\n";
        if (drpCostType.SelectedItem.Value == "0") sMessage += i++ + ". Select a Cost Type\\r\\n";
        if (drpCostInvoiceType.SelectedItem.Value == "4" && txtCostQuantity.Text.Trim() == "") sMessage += i++ + ". Enter the Quantity\\r\\n";
        if (txtCostPriceCode.Text.Trim() == "") sMessage += i++ + ". Invalid Price Code\\r\\n";
        if (i > 1)
        {
            Alert(sMessage);
            return false;
        }
        return true;
    }
    private void clearIssueCost()
    {
        hfCostInvTypeItemID.Value = "";
        drpCostInvoiceType.ClearSelection();
        drpCostInvoiceTypeItem.ClearSelection();
        drpCostType.ClearSelection();
        txtCostQuantity.Text = "0";
        txtCostPriceCode.Text = "";
        txtCostItemdesc.Text = "";
    }
    protected void cmd_Cost_orderindex_Click(object sender, ImageClickEventArgs e)
    {
        this.clearIssueCost();
        if (gvIssueCost.Columns[6].Visible == false)
        {
            gvIssueCost.Columns[6].Visible = true;
            gvIssueCost.Columns[7].Visible = false;
            trBCCtrls.Disabled = true;
        }
        else
        {
            gvIssueCost.Columns[6].Visible = false;
            gvIssueCost.Columns[7].Visible = true;
            trBCCtrls.Disabled = false;
        }
        gvIssueCost.DataSource = oIssue.getIssueCostDetailsByID(hfI_ID.Value.Trim(), "1");
        gvIssueCost.DataBind();
    }
    protected void drpCostInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sInvoicetype = drpCostInvoiceType.SelectedItem.Value.Trim();
        txtCostQuantity.Text = "0";
        //if (sInvoicetype != "4"){
        //    txtCostQuantity.Enabled = false;
        //    imgbtnBCAddInvTypeItem.Visible = false;
        //}
        //else{
        //    txtCostQuantity.Enabled = true;
        //    imgbtnBCAddInvTypeItem.Visible = true;
        //}
        DataSet dsCost = oIssue.getInvoiceTypeItem(sInvoicetype);
        drpCostInvoiceTypeItem.Items.Clear();
        drpCostInvoiceTypeItem.DataSource = dsCost;
        drpCostInvoiceTypeItem.DataValueField = dsCost.Tables[0].Columns[0].ToString();
        drpCostInvoiceTypeItem.DataTextField = dsCost.Tables[0].Columns[1].ToString();
        drpCostInvoiceTypeItem.DataBind();
        drpCostInvoiceTypeItem.Items.Insert(0, new ListItem("-- select --", "0"));
    }
    protected void lnkCostAddInvTypeItem_Click(object sender, EventArgs e)
    {
        string sInvitmtext = txtBCpopInvTypeItem.Text.Trim();
        if (drpCostInvoiceType.SelectedItem.Value.Trim() == "4" && sInvitmtext != "")
        {
            string res = this.oIssue.InsertInvoiceTypeItem(drpCostInvoiceType.SelectedItem.Value.Trim(), sInvitmtext);
            if (res == "true")
            {
                this.drpCostInvoiceType_SelectedIndexChanged(null, null);
                if (drpCostInvoiceTypeItem.Items.FindByText(sInvitmtext) != null)
                    drpCostInvoiceTypeItem.Items.FindByText(sInvitmtext).Selected = true;
            }
            else Alert(res);
        }
    }
    protected void imgbtnPaginate_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("job_pagination.aspx?jno=" + hfI_ID.Value.Trim());
    }
    protected void gvIssueCost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((Control)((GridViewCommandEventArgs)e).CommandSource).Parent.Parent;
        string sICinvoicetypeitemID = ((HiddenField)row.FindControl("hfIC_invoicetypeitem")).Value.Trim();
        if (e.CommandName == "ICEdit")
        {
            row.BackColor = System.Drawing.Color.LightGreen;
            hfCostInvTypeItemID.Value = sICinvoicetypeitemID;
            this.loadIssueCost(sICinvoicetypeitemID);
        }
        else if (e.CommandName == "ICDelete")
        {
            this.oIssue.DeleteIssueCost(sICinvoicetypeitemID);
            this.loadIssueCostDetails(hfI_ID.Value.Trim());
        }
        else { /*do nothing */ }
    }
    protected void gvIssueCost_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        if (dtIssue != null && dtIssue.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='10' align='center'><h4>Issue Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Job Number</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Issue Name</b></td><td bgcolor='silver'><b>Issue Title</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>Received Date</b></td><td bgcolor='silver'><b>Due Date</b></td><td bgcolor='silver'><b>Despatch Date</b></td><td bgcolor='silver'><b>Hold Details</b></td><td bgcolor='silver'><b>Invoice No</b></td></tr>");
            foreach (DataRow r in dtIssue.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["parent_job_id"] + "</td>");
                sbData.Append("<td>" + r["cust_name"] + "</td>");
                sbData.Append("<td>" + r["journal_code"] + " " + r["name"] + "</td>");
                sbData.Append("<td>" + r["title"] + "</td>");
                sbData.Append("<td>" + r["job_stage_name"] + "</td>");
                sbData.Append("<td>" + r["received_date"] + "</td>");
               // sbData.Append("<td>" + r["half_due_date"] + "</td>");
                sbData.Append("<td>" + r["due_date"] + "</td>");
                sbData.Append("<td>" + r["despatch_date"] + "</td>");
                sbData.Append("<td>" + r["Hold_details"] + "</td>");
                sbData.Append("<td>" + r["invoice_no"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Issue_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void gvIssues_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataView dv = new DataView();
        string di = "";
        try
        {
            SortDirection sortDirection = SortDirection.Ascending;
            if (ViewState[e.SortExpression] != null)
            {
                SortDirection currDirection = (SortDirection)ViewState[e.SortExpression];
                if (currDirection == SortDirection.Ascending) sortDirection = SortDirection.Descending;
            }
            ViewState[e.SortExpression] = sortDirection;
            if (sortDirection.Equals(SortDirection.Descending)) di = " desc"; dv.Table = dtIssue;
            dv.Sort = e.SortExpression + di;
            sSortExpression = e.SortExpression;
            gvIssues.DataSource = dv;
            gvIssues.DataBind();
        }
        catch (Exception ec)
        {
            Alert(ec.Message);
        }
        finally
        {
            dv.Dispose();
        }
    }
    protected void imgbtnEventExport_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder sbData = new StringBuilder();
        sbData.Append("<table border='1'>");
        sbData.Append("<tr valign='top'><td colspan='8'><h4>" + lblEventsHeader.Text.Trim() + "</h4></td></tr>");
        sbData.Append("<tr valign='top'><td colspan='8'><b>Issue:</b></td></tr>");
        sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Job</b></td><td bgcolor='silver'><b>Job Stage</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Start Time</b></td><td bgcolor='silver'><b>End Time</b></td><td bgcolor='silver'><b>Duration</b></td><td bgcolor='silver'><b>Employee</b></td><td bgcolor='silver'><b>Comments</b></td></tr>");
        foreach (GridViewRow r in gvEvents.Rows)
        {
            sbData.Append("<tr valign='top'>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[0].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[1].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[2].Controls[0]).Text + "</td>");
            sbData.Append("<td align='left'>" + ((DataBoundLiteralControl)r.Cells[3].Controls[0]).Text + "</td>");
            sbData.Append("<td align='left'>" + ((DataBoundLiteralControl)r.Cells[4].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[5].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[6].Controls[0]).Text + "</td>");
            sbData.Append("<td>" + ((DataBoundLiteralControl)r.Cells[7].Controls[0]).Text + "</td>");
            sbData.Append("</tr>");
        }
        sbData.Append("</table>");
        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Issue_Events_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
        Response.ContentType = "application/ms-excel";
        Response.Write(sbData.ToString());
        Response.End();
    }
}