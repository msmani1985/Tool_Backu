using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class LP_Launch_Initial : System.Web.UI.Page
{
    public int id = 1;
    Non_Launch nonLa = new Non_Launch();
    Launch_SQL oLaunch = new Launch_SQL();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataSet Ds = nonLa.getAllCustomers();
            drpPCustomer.DataSource = Ds;
            drpPCustomer.DataTextField = Ds.Tables[0].Columns[1].ToString();
            drpPCustomer.DataValueField = Ds.Tables[0].Columns[0].ToString();
            drpPCustomer.DataBind();
            drpPCustomer.Items.Insert(0, new ListItem("-- select --", "0"));
            drpPCustomer_SelectedIndexChanged(sender, e);
            lnkGeneral_Click(sender,e);
            this.showPanel(tabGeneral);
        }
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                miGeneral.Attributes.Add("class", "current");
                miJobInfo.Attributes.Add("class", "");
                miRecAmends.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabLaunch.Visible = false;
                this.tabRecAmends.Visible = false;
                break;
            case "tabLaunch":
                miGeneral.Attributes.Add("class", "");
                miJobInfo.Attributes.Add("class", "current");
                miRecAmends.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabLaunch.Visible = true;
                this.tabRecAmends.Visible = false;
                break;
            case "tabRecAmends":
                miGeneral.Attributes.Add("class", "");
                miJobInfo.Attributes.Add("class", "");
                miRecAmends.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabLaunch.Visible = false;
                this.tabRecAmends.Visible = true;
                break;
        }
    }
    protected void drpPCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = nonLa.GetLocationCust(drpPCustomer.SelectedValue);
        if (ds != null)
        {
            drpPLoc.Enabled = true;
            drpPLoc.DataSource = ds;
            drpPLoc.DataValueField = ds.Tables[0].Columns[3].ToString();
            drpPLoc.DataTextField = ds.Tables[0].Columns[4].ToString();
            drpPLoc.DataBind();
        }
        else
        {
            drpPLoc.Items.Insert(0, new ListItem("-- select --", "0"));
            drpPLoc.SelectedValue = "0";
            drpPLoc.Enabled = false;
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpPCustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtProjectName.Text.Trim() == "") sMessage += i++ + ". Enter Project Name\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    private bool validateScreen1()
    {
        int i = 1;
        string sMessage = "";
        if (dropCurStage.SelectedItem.Value == "5") sMessage += i++ + ". Select a Amends Details\\r\\n";
        if (txtTarRecDate.Text.Trim() == "") sMessage += i++ + ". Enter Rec. Date\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (validateScreen())
        {
            if (hfP_ID.Value == "")
            {
                string inproc = "spInsertLI";
                string[,] pname ={
                                        {"@PROJECTNAME",txtProjectName.Text },
                                        {"@CUST_ID",drpPCustomer.SelectedValue},{"@Location",drpPLoc.SelectedValue},
                                        {"@PROJECTEDITOR",txtProjectEditor.Text},{"@RecDate",txtRecDate.Text},
                                        {"@DueDate",txtDueDate.Text},{"@SendDate",txtSendDate.Text},
                                        {"@EmpID",Session["employeeid"].ToString()},{"@IsExist","OUTPUT"}};

                int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                if (val == 0)
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Failed!.');</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Successfully!.');</script>");
            }
            else
            {
                string inproc = "spUpdateLI";
                string[,] pname ={
                                        {"@LI_ID",hfP_ID.Value},
                                        {"@PROJECTEDITOR",txtProjectEditor.Text},{"@RecDate",txtRecDate.Text.Replace("/","-")},
                                        {"@DueDate",txtDueDate.Text.Replace("/","-")},{"@SendDate",txtSendDate.Text.Replace("/","-")},
                                        {"@EmpID",Session["employeeid"].ToString()},{"@IsExist","OUTPUT"}};

                int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                if (val == 0)
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Failed!.');</script>");
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Updated Successfully!.');</script>");
                loadJobDetails(hfP_ID.Value);
            }
        }
        this.showPanel(tabLaunch);
    }
    protected void chkDueTime1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueTime1.Checked)
        {
            lblFrom1.Visible = true;
            lblTo1.Visible = true;
            DropDueTimeTo1.Visible = true;
            DropDueMinTo1.Visible = true;
            DropDueTimeZoneTo1.Visible = true;
            txtIndTo1.Visible = true;
            lblIndFrom1.Visible = true;
            lblIndTo1.Visible = true;
        }
        else
        {
            lblFrom1.Visible = false;
            lblTo1.Visible = false;
            DropDueTimeTo1.Visible = false;
            DropDueMinTo1.Visible = false;
            DropDueTimeZoneTo1.Visible = false;
            txtIndTo1.Visible = false;
            lblIndFrom1.Visible = false;
            lblIndTo1.Visible = false;
        }
        popup1.Show();
    }
    protected void chkDueDate1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueDate1.Checked)
        {
            lblDueFrom1.Visible = true;
            txtdueFromdate1.Visible = true;
            lblDueTo1.Visible = true;
            txtdueTodate1.Visible = true;
            imgBD_dueTodate1.Visible = true;
        }
        else
        {
            lblDueFrom1.Visible = false;
            txtdueFromdate1.Visible = true;
            lblDueTo1.Visible = false;
            txtdueTodate1.Visible = false;
            imgBD_dueTodate1.Visible = false;
        }
        popup1.Show();
    }
    protected void Save1(object sender, EventArgs e)
    {
        int ytc;
        if (chkYTC1.Checked)
            ytc = 1;
        else
            ytc = 0;
        int duetime;
        if (chkDueTime1.Checked)
            duetime = 1;
        else
            duetime = 0;
        int dueDate;
        if (chkDueDate1.Checked)
            dueDate = 1;
        else
            dueDate = 0;
        string ddate = "";
        if (txtdueFromdate1.Text != "")
            ddate = DateTime.Parse(txtdueFromdate1.Text.Trim()).ToString("MM/dd/yyyy");
        else
            ddate = "";
        string dTodate = "";
        if (txtdueTodate1.Text != "")
            dTodate = DateTime.Parse(txtdueTodate1.Text.Trim()).ToString("MM/dd/yyyy");
        else
            dTodate = "";
        string recDate = "";
        if (txtRecDate1.Text != "")
            recDate = DateTime.Parse(txtRecDate1.Text.Trim()).ToString("MM/dd/yyyy");
        else
            recDate = "";
        string delDate = "";
        if (txtDelDate.Text != "")
            delDate = DateTime.Parse(txtDelDate.Text.Trim()).ToString("MM/dd/yyyy");
        else
            delDate = "";

        string Amends = "", AmendName = "", LastAmends = "";
        for (int j = 0; j < lboxStage.Items.Count; j++)
        {
            if (lboxStage.Items[j].Selected == true)
            {
                if (Amends == "")
                {
                    Amends = lboxStage.Items[j].Value;
                    AmendName = lboxStage.Items[j].Text;
                }
                else
                {
                    Amends = Amends + ',' + lboxStage.Items[j].Value;
                    AmendName = AmendName + ',' + lboxStage.Items[j].Text;
                }
                LastAmends = lboxStage.Items[j].Value;
            }
        }
        if (ddlStatus.SelectedValue == "Del")
        {
            if (delDate.ToString() != "")
            {
                string inproc = "spUpdateJobHis_LP";
                string[,] pname ={
                            {"@YTCYN",ytc.ToString()},{"@DUETIMEYN",duetime.ToString()},
                            {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                            {"@Due_Timefrom",DropDueTimeFrom1.SelectedValue},{"@Due_MINFROM",DropDueMinFrom1.SelectedValue},
                            {"@TIME_ZoneFROM",DropDueTimeZoneFrom1.SelectedValue},
                            {"@Due_TimeTO",DropDueTimeTo1.SelectedValue},{"@Due_MINTO",DropDueMinTo1.SelectedValue},
                            {"@TIME_ZoneTO",DropDueTimeZoneTo1.SelectedValue},{"@MODIFIED_BY",""},
                            {"@DUETIMEFROM_IST",txtIndFrom1.Text},{"@DUETIMETO_IST",txtIndTo1.Text},
                            {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                            {"@RecDate",recDate.ToString()},{"@NL_ID",NL_ID1.Text},
                            {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",DropDelHrs.SelectedValue},
                            {"@DEL_MINS",DropDelMins.SelectedValue},{"@DEL_ZONE",DropDelTimeZone.SelectedValue},
                            {"@DEL_IST",txtDelIST.Text},{"@Amends_ID",Amends.ToString()},{"@Status",ddlStatus.SelectedValue},
                            {"@AmendName",AmendName.ToString()},{"@FP_ID",txtFP_ID.Text},{"@LastAmend",LastAmends.ToString()}
                         };
                int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);

                if (val == 1)
                    lblResult1.Text = "Inserted Successfully";
                else
                    lblResult1.Text = "Inserted Failed!.";
            }
            else
            {
                lblResult1.Text = "Please check Delivery Date!..";
            }
        }
        else
        {
            string inproc = "spUpdateJobHis_LP";
            string[,] pname ={
                            {"@YTCYN",ytc.ToString()},{"@DUETIMEYN",duetime.ToString()},
                            {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                            {"@Due_Timefrom",DropDueTimeFrom1.SelectedValue},{"@Due_MINFROM",DropDueMinFrom1.SelectedValue},
                            {"@TIME_ZoneFROM",DropDueTimeZoneFrom1.SelectedValue},
                            {"@Due_TimeTO",DropDueTimeTo1.SelectedValue},{"@Due_MINTO",DropDueMinTo1.SelectedValue},
                            {"@TIME_ZoneTO",DropDueTimeZoneTo1.SelectedValue},{"@MODIFIED_BY",""},
                            {"@DUETIMEFROM_IST",txtIndFrom1.Text},{"@DUETIMETO_IST",txtIndTo1.Text},
                            {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                            {"@RecDate",recDate.ToString()},{"@NL_ID",NL_ID1.Text},
                            {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",DropDelHrs.SelectedValue},
                            {"@DEL_MINS",DropDelMins.SelectedValue},{"@DEL_ZONE",DropDelTimeZone.SelectedValue},
                            {"@DEL_IST",txtDelIST.Text},{"@Amends_ID",Amends.ToString()},{"@Status",ddlStatus.SelectedValue},
                            {"@AmendName",AmendName.ToString()},{"@FP_ID",txtFP_ID.Text},{"@LastAmend",LastAmends.ToString()}
                         };
            int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);

            if (val == 1)
                lblResult1.Text = "Inserted Successfully";
            else
                lblResult1.Text = "Inserted Failed!.";
        }
        if (DelNL_ID.Text != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(DelNL_ID.Text);
            if (ds != null)
            {
                gvJobTrackFiles.DataSource = ds;
                gvJobTrackFiles.DataBind();
            }
        }
        popup1.Hide();
    }
    protected void DropDueMin1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
        txtIndFrom1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTime1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
        txtIndFrom1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTimeZoneFrom1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
        txtIndFrom1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTimeTo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);
        txtIndTo1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueMinTo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);
        txtIndTo1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDueTimeZoneTo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);
        txtIndTo1.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDelHrs_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDelHrs.SelectedValue, DropDelMins.SelectedValue, DropDelTimeZone.SelectedValue);
        txtDelIST.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDelMins_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDelHrs.SelectedValue, DropDelMins.SelectedValue, DropDelTimeZone.SelectedValue);
        txtDelIST.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void DropDelTimeZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, DropDelHrs.SelectedValue, DropDelMins.SelectedValue, DropDelTimeZone.SelectedValue);
        txtDelIST.Text = dtable.Rows[0]["Mins"].ToString();
        popup1.Show();
    }
    protected void GvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvProjectID")).Value.Trim() + "\",\"" + ((HiddenField)e.Row.FindControl("hfgvProjectname")).Value.Trim() + "\");";


            if (((HiddenField)e.Row.FindControl("hfStatus")).Value.Trim() != "")
                e.Row.BackColor = System.Drawing.Color.LightPink;

        }
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.getLPInitial();
        if (ds != null)
        {
            GvProject.DataSource = ds.Tables[0];
            GvProject.DataBind();
        }
        else
        {
            GvProject.DataSource = null;
            GvProject.DataBind();
        }
        this.showPanel(tabGeneral);
    }
    protected void lnkJobInfo_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            loadJobDetails(hfP_ID.Value.Trim());
        }
        this.showPanel(tabLaunch);
    }
    public void loadJobDetails(string sJobID)
    {
        if (sJobID != "")
        {
            DataSet dsNL = nonLa.getLIinitialbyID(sJobID);
            lblNLHeader.Text = "Edit Project";
            imgNLHeader.Src = "images/tools/edit.png";
            DataRow row = dsNL.Tables[0].Rows[0];
            txtProjectName.Text = row["projectname"].ToString();
            drpPCustomer.SelectedValue = row["custno"].ToString();
            DataSet ds = nonLa.GetLocationCust(drpPCustomer.SelectedValue);
            drpPLoc.DataSource = ds;
            drpPLoc.DataValueField = ds.Tables[0].Columns[3].ToString();
            drpPLoc.DataTextField = ds.Tables[0].Columns[4].ToString();
            drpPLoc.DataBind();
            drpPLoc.SelectedValue = row["LOCATION_id"].ToString();
            txtProjectEditor.Text = row["PEname"].ToString();
            txtRecDate.Text = row["RecDate"].ToString();
            txtDueDate.Text = row["DueDate"].ToString();
            txtSendDate.Text = row["SendDate"].ToString();
            if (txtSendDate.Text != "")
                imgbtnSave.Visible = false;
            else
                imgbtnSave.Visible = true;
        }
    }
    protected void lnkRecAmends_Click(object sender, EventArgs e)
    {

        this.showPanel(tabRecAmends);
    }
    public void Clear()
    {
        NL_ID1.Text = "";
        txtJobid1.Text = "";
        txtProjectEditor1.Text = "";
        txtdueFromdate1.Text = "";
        chkYTC1.Checked = false;
        txtdueTodate1.Text = "";
        txtRecDate1.Text = "";
        txtdueFromdate1.Text = "";
        txtdueTodate1.Text = "";
        chkDueDate1.Checked = false;
        chkDueTime1.Checked = false;
        txtIndTo1.Text = "";
        txtIndFrom1.Text = "";
        lboxStage.ClearSelection();
        lboxtask1.Text = "";
        lblResult1.Text = "";
        txtDelDate.Text = "";
        lblResult1.Text = "";
        ddlStatus.Items.Clear();
        txtTarRecDate.Text = "";
    }
    protected void gvJobTrackFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField FP_ID = e.Row.FindControl("hid_FP_ID") as HiddenField;
            DataSet ds = new DataSet();
            ds = nonLa.GetTarFPStatus(FP_ID.Value);
            DropDownList status = e.Row.FindControl("dropDelStatus") as DropDownList;
            LinkButton lnkBtnClick = e.Row.FindControl("lnkEdit1") as LinkButton;
            status.Items.Add(new ListItem("--Select--", "0"));
            status.Items.Add(new ListItem("P", "P"));
            status.Items.Add(new ListItem("C", "C"));
            status.Items.Add(new ListItem("WIP", "WIP"));
            status.Items.Add(new ListItem("Del", "Del"));
            status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["DelStatus"].ToString()))].Selected = true;

            //if (ds.Tables[0].Rows[0]["DelStatus"].ToString() != "Del")
            //    lnkBtnClick.Enabled = true;
            //else
            //    lnkBtnClick.Enabled = false;

            if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "P")
                status.CssClass = "gridP";
            else if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "C")
                status.CssClass = "gridC";
            else if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "Del")
                status.CssClass = "gridDel";
            else if (ds.Tables[0].Rows[0]["DelStatus"].ToString() == "WIP")
                status.CssClass = "gridWIP";
        }
    }
    public void getloc_timezone(int locid)
    {
        DataSet ds = nonLa.GetTimeZone(locid);
        if (ds != null)
        {
            DropDueTimeZoneFrom1.DataSource = ds;
            DropDueTimeZoneFrom1.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneFrom1.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneFrom1.DataBind();
            DropDueTimeZoneTo1.DataSource = ds;
            DropDueTimeZoneTo1.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneTo1.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneTo1.DataBind();
            dropDelZoneAll.DataSource = ds;
            dropDelZoneAll.DataValueField = ds.Tables[0].Columns[2].ToString();
            dropDelZoneAll.DataTextField = ds.Tables[0].Columns[2].ToString();
            dropDelZoneAll.DataBind();
            DropDelTimeZone.DataSource = ds;
            DropDelTimeZone.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDelTimeZone.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDelTimeZone.DataBind();
            dropDelZoneFrom.DataSource = ds;
            dropDelZoneFrom.DataValueField = ds.Tables[0].Columns[2].ToString();
            dropDelZoneFrom.DataTextField = ds.Tables[0].Columns[2].ToString();
            dropDelZoneFrom.DataBind();
            dropDelZoneTo.DataSource = ds;
            dropDelZoneTo.DataValueField = ds.Tables[0].Columns[2].ToString();
            dropDelZoneTo.DataTextField = ds.Tables[0].Columns[2].ToString();
            dropDelZoneTo.DataBind();
            TimeFormat(locid.ToString(), DropDueTimeFrom1.SelectedValue, DropDueMinFrom1.SelectedValue, DropDueTimeZoneFrom1.SelectedValue);
            TimeFormat(locid.ToString(), DropDueTimeTo1.SelectedValue, DropDueMinTo1.SelectedValue, DropDueTimeZoneTo1.SelectedValue);

            TimeFormat(locid.ToString(), dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
            TimeFormat(locid.ToString(), dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        }
        else
        {
            DropDueTimeZoneFrom1.Items.Insert(0, new ListItem(" ", "0"));
            DropDueTimeZoneTo1.Items.Insert(0, new ListItem(" ", "0"));
            DropDueTimeZoneFrom1.SelectedValue = "0";
            DropDueTimeZoneTo1.SelectedValue = "0";
            DropDueTimeZoneFrom1.Enabled = false;
            DropDueTimeZoneTo1.Enabled = false;
        }
    }
    public void TimeFormat(string LocID, string Hrs, string Min, string Zone)
    {
        string hrs = Hrs;
        string min = Min;
        DataSet time = nonLa.getTimeDetails(hrs, min, LocID.ToString(), Zone);
        dtable = time.Tables[0].Copy();
    }
    protected void ClickFile(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            Clear();
            DataSet dsNL = new DataSet();
            dsNL = nonLa.GetAmends();
            lboxStage.DataSource = dsNL;
            lboxStage.DataTextField = dsNL.Tables[0].Columns[1].ToString();
            lboxStage.DataValueField = dsNL.Tables[0].Columns[0].ToString();
            lboxStage.DataBind();
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_NL_ID");
            HiddenField sFPID = (HiddenField)row.Cells[0].FindControl("hid_FP_ID");
            dsNL = nonLa.getJobTrackingByID(sJobID.Value.ToString(), sFPID.Value.ToString());
            DataRow r = dsNL.Tables[0].Rows[0];
            txtFP_ID.Text = sFPID.Value.ToString();
            NL_ID1.Text = r["NL_ID"].ToString();
            txtJobid1.Text = r["JOBID"].ToString();
            txtProjectEditor1.Text = r["PROJECTEDITOR"].ToString();
            txtProjectTitle1.Text = r["projectname"].ToString();
            drpProjectcustomer1.Text = r["CUSTNAME"].ToString().Trim();
            DropLocation1.Text = r["LOCATION_NAME"].ToString();
            hid_Loc_ID.Value = r["LOCATION_ID"].ToString();
            getloc_timezone(Convert.ToInt16(r["LOCATION_ID"].ToString()));
            if (r["YTCYN"].ToString() == "1") chkYTC1.Checked = true;
            if (r["DUE_DATEFROM"].ToString() == "")
                txtdueFromdate1.Text = "";
            else
                txtdueFromdate1.Text = DateTime.Parse(r["DUE_DATEFROM"].ToString()).ToShortDateString();
            if (r["DUE_DATETO"].ToString() == "")
                txtdueTodate1.Text = "";
            else
                txtdueTodate1.Text = DateTime.Parse(r["DUE_DATETO"].ToString()).ToShortDateString();
            if (r["Rec_Date"].ToString() == "")
                txtRecDate1.Text = "";
            else
                txtRecDate1.Text = DateTime.Parse(r["Rec_Date"].ToString()).ToShortDateString();

            if (r["DUEDATEYN"].ToString() == "1")
            {
                lblDueFrom1.Visible = true;
                txtdueFromdate1.Visible = true;
                lblDueTo1.Visible = true;
                txtdueTodate1.Visible = true;
                chkDueDate1.Checked = true;
                imgBD_dueFromdate1.Visible = true;
                imgBD_dueTodate1.Visible = true;
            }
            else
            {
                lblDueFrom1.Visible = false;
                txtdueFromdate1.Visible = true;
                lblDueTo1.Visible = false;
                txtdueTodate1.Visible = false;
                chkDueDate1.Checked = false;
                imgBD_dueTodate1.Visible = false;
            }
            if (r["DUETIMEYN"].ToString() == "1")
            {
                lblFrom1.Visible = true;
                lblTo1.Visible = true;
                chkDueTime1.Checked = true;
                DropDueTimeTo1.Visible = true;
                DropDueMinTo1.Visible = true;
                DropDueTimeZoneTo1.Visible = true;
                txtIndTo1.Visible = true;
                DropDueTimeFrom1.SelectedValue = r["DUE_HrsFROM"].ToString();
                DropDueMinFrom1.SelectedValue = r["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom1.SelectedValue = r["TIME_ZONEFROM"].ToString();
                DropDueTimeTo1.SelectedValue = r["DUE_HrsTO"].ToString();
                DropDueMinTo1.SelectedValue = r["DUE_MINTO"].ToString();
                DropDueTimeZoneTo1.SelectedValue = r["TIME_ZONETO"].ToString();
                DropDelTimeZone.SelectedValue = r["TIME_ZONETO"].ToString();
                txtIndFrom1.Text = r["DUETIMEFROM_IST"].ToString();
                txtIndTo1.Text = r["DUETIMETO_IST"].ToString();
                lblIndFrom1.Visible = true;
                lblIndTo1.Visible = true;
            }
            else
            {
                lblFrom1.Visible = false;
                lblTo1.Visible = false;
                chkDueTime1.Checked = false;
                DropDueTimeFrom1.SelectedValue = r["DUE_HrsFROM"].ToString();
                DropDueMinFrom1.SelectedValue = r["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom1.SelectedValue = r["TIME_ZONEFROM"].ToString();
                DropDueTimeTo1.SelectedValue = "00";
                DropDueMinTo1.SelectedValue = "00";
                DropDueTimeZoneTo1.SelectedValue = r["TIME_ZONEFROM"].ToString();
                DropDelTimeZone.SelectedValue = r["TIME_ZONEFROM"].ToString();
                txtIndFrom1.Text = r["DUETIMEFROM_IST"].ToString();
                txtIndTo1.Text = "";
                lblIndFrom1.Visible = false;
                lblIndTo1.Visible = false;
                txtIndTo1.Visible = false;
                DropDueTimeTo1.Visible = false;
                DropDueMinTo1.Visible = false;
                DropDueTimeZoneTo1.Visible = false;
            }
            lboxtask1.Text = r["TASKNAME"].ToString();
            string[] s = r["Amend_ID"].ToString().Split(',');
            foreach (string stage in s)
            {
                lboxStage.Items[lboxStage.Items.IndexOf(lboxStage.Items.FindByValue(stage))].Selected = true;
            }
            if (r["Despatch_Date"].ToString() == "")
            {
                txtDelDate.Text = "";
                txtDelIST.Text = "";
                DropDelHrs.SelectedValue = "00";
                DropDelMins.SelectedValue = "00";
            }
            else
            {
                txtDelDate.Text = DateTime.Parse(r["Despatch_Date"].ToString()).ToShortDateString();
                DropDelHrs.SelectedValue = r["DEL_HRS"].ToString();
                DropDelMins.SelectedValue = r["DEL_MINS"].ToString();
                DropDelTimeZone.SelectedValue = r["DEL_ZONE"].ToString();
                txtDelIST.Text = r["DEL_IST"].ToString();
            }
            //if (r["LastAmend_ID"].ToString() == "27" && r["Despatch_Date"].ToString() != "")
            //{
            //    btnSave1.Visible = false;
            //}
            //else
            //{
            //    btnSave1.Visible = true;
            //}

            ddlStatus.Items.Add(new ListItem("P", "P"));
            ddlStatus.Items.Add(new ListItem("C", "C"));
            ddlStatus.Items.Add(new ListItem("WIP", "WIP"));
            ddlStatus.Items.Add(new ListItem("Del", "Del"));
            ddlStatus.Items[ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(r["DelStatus"].ToString()))].Selected = true;

            if (r["DelStatus"].ToString() == "P")
                ddlStatus.CssClass = "gridP";
            else if (r["DelStatus"].ToString() == "C")
                ddlStatus.CssClass = "gridC";
            else if (r["DelStatus"].ToString() == "Del")
                ddlStatus.CssClass = "gridDel";
            else if (r["DelStatus"].ToString() == "WIP")
                ddlStatus.CssClass = "gridWIP";
            popup1.Show();
        }
    }
    protected void OnDelivery(object sender, EventArgs e)
    {
        DataSet dsNL = new DataSet();
        dsNL = nonLa.GetAmends();
        //lboxDelSatge.DataSource = dsNL;
        //lboxDelSatge.DataTextField = dsNL.Tables[0].Columns[1].ToString();
        //lboxDelSatge.DataValueField = dsNL.Tables[0].Columns[0].ToString();
        //lboxDelSatge.DataBind();
        dsNL = nonLa.getDelJobDetailsByID(DelNL_ID.Text);
        DataRow r = dsNL.Tables[0].Rows[0];
        DelJobID.Text = r["JOBID"].ToString();
        DelProName.Text = r["projectname"].ToString();
        DelNL_ID.Text = r["NL_ID"].ToString();
        getloc_timezone(Convert.ToInt32(hid_Loc_ID.Value));
        dropDelZoneAll.SelectedValue = r["TIME_ZONEFROM"].ToString();
        DelLoc_ID.Text = r["LOCATION_ID"].ToString();
        txtdeldateAll.Text = "";
        txtDel_ISTAll.Text = "";
        lblResult2.Text = "";
        dropCurStage.SelectedValue = "0";
        txtdeldateAll.Enabled = true;
        img4.Visible = true;
        dropDelHrsAll.Enabled = true;
        dropDelMinsAll.Enabled = true;
        dropDelZoneAll.Enabled = true;
        //DelPopUp.Show();

        lblDueDate.Visible = true;
        lblDelDueDateFrom.Visible = false;
        txtDelDueDateFrom.Visible = true;
        txtDelDueDateFrom.Text = "";
        img1.Visible = true;
        lblDelDueDateTo.Visible = false;
        txtDelDueDateTo.Visible = false;
        txtDelDueDateTo.Text = "";
        img2.Visible = false;
        chkDelYTC.Visible = true;
        chkDelYTC.Checked = false;
        chkDelStagDate.Visible = true;
        chkDelStagDate.Checked = false;
        lblDueTime.Visible = true;
        lblDelTimeFrom.Visible = false;
        dropDelTimeFrom.Visible = true;
        dropDelMinFrom.Visible = true;
        dropDelZoneFrom.Visible = true;
        dropDelTimeFrom.SelectedValue = "00";
        dropDelMinFrom.SelectedValue = "00";
        lblDelTimeTo.Visible = false;
        dropDelTimeTo.Visible = false;
        dropDelMinTo.Visible = false;
        dropDelZoneTo.Visible = false;
        dropDelTimeTo.SelectedValue = "00";
        dropDelMinTo.SelectedValue = "00";
        chkDelstagTime.Visible = true;
        chkDelstagTime.Checked = false;
        lblDelISTFrom.Visible = false;
        txtDelISTFrom.Visible = true;
        txtDelISTFrom.Text = "";
        lblDelISTTo.Visible = false;
        txtDelISTTo.Visible = false;
        txtDelISTTo.Text = "";
        txtTarRecDate.Text = "";
        txtTarRecDate.Visible = true;
        img10.Visible = true;
        lblRecDate.Visible = true;
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvJobTrackFiles.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in gvJobTrackFiles.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkDelStatus");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void dropCurStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCurStage.SelectedValue == "1")
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img4.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
            lblDueDate.Visible = true;
            lblDelDueDateFrom.Visible = true;
            txtDelDueDateFrom.Visible = true;
            img1.Visible = true;
            chkDelYTC.Visible = true;
            chkDelStagDate.Visible = true;
            chkDelYTC.Checked = false;
            chkDelStagDate.Checked = false;
            lblDueTime.Visible = true;
            lblDelTimeFrom.Visible = true;
            dropDelTimeFrom.Visible = true;
            dropDelMinFrom.Visible = true;
            dropDelZoneFrom.Visible = true;
            chkDelstagTime.Visible = true;
            chkDelstagTime.Checked = false;
            lblDelISTFrom.Visible = true;
            txtDelISTFrom.Visible = true;
            lblDelStatusFile.Visible = false;
            dropDelFileStatus.Visible = false;
            chkDelstagTime_CheckedChanged(sender, e);
            chkDelStagDate_CheckedChanged(sender, e);
        }
        else if (dropCurStage.SelectedValue == "2")
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img4.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
            lblDueDate.Visible = true;
            lblDelDueDateFrom.Visible = true;
            txtDelDueDateFrom.Visible = true;
            img1.Visible = true;
            chkDelYTC.Visible = true;
            chkDelStagDate.Visible = true;
            chkDelYTC.Checked = false;
            chkDelStagDate.Checked = false;
            lblDueTime.Visible = true;
            lblDelTimeFrom.Visible = true;
            dropDelTimeFrom.Visible = true;
            dropDelMinFrom.Visible = true;
            dropDelZoneFrom.Visible = true;
            chkDelstagTime.Visible = true;
            chkDelstagTime.Checked = false;
            lblDelISTFrom.Visible = true;
            txtDelISTFrom.Visible = true;
            lblDelStatusFile.Visible = false;
            dropDelFileStatus.Visible = false;
            chkDelstagTime_CheckedChanged(sender, e);
            chkDelStagDate_CheckedChanged(sender, e);
        }
        else if (dropCurStage.SelectedValue == "3")
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img4.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
            lblDueDate.Visible = true;
            lblDelDueDateFrom.Visible = true;
            txtDelDueDateFrom.Visible = true;
            img1.Visible = true;
            chkDelYTC.Visible = true;
            chkDelStagDate.Visible = true;
            chkDelYTC.Checked = false;
            chkDelStagDate.Checked = false;
            lblDueTime.Visible = true;
            lblDelTimeFrom.Visible = true;
            dropDelTimeFrom.Visible = true;
            dropDelMinFrom.Visible = true;
            dropDelZoneFrom.Visible = true;
            chkDelstagTime.Visible = true;
            chkDelstagTime.Checked = false;
            lblDelISTFrom.Visible = true;
            txtDelISTFrom.Visible = true;
            lblDelStatusFile.Visible = false;
            dropDelFileStatus.Visible = false;
            chkDelstagTime_CheckedChanged(sender, e);
            chkDelStagDate_CheckedChanged(sender, e);
        }
        else
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img4.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
            lblDueDate.Visible = true;
            lblDelDueDateFrom.Visible = true;
            txtDelDueDateFrom.Visible = true;
            img1.Visible = true;
            chkDelYTC.Visible = true;
            chkDelStagDate.Visible = true;
            chkDelYTC.Checked = false;
            chkDelStagDate.Checked = false;
            lblDueTime.Visible = true;
            lblDelTimeFrom.Visible = true;
            dropDelTimeFrom.Visible = true;
            dropDelMinFrom.Visible = true;
            dropDelZoneFrom.Visible = true;
            chkDelstagTime.Visible = true;
            chkDelstagTime.Checked = false;
            lblDelISTFrom.Visible = true;
            txtDelISTFrom.Visible = true;
            lblDelStatusFile.Visible = true;
            dropDelFileStatus.Visible = true;
            chkDelstagTime_CheckedChanged(sender, e);
            chkDelStagDate_CheckedChanged(sender, e);
        }
        //DelPopUp.Show();
    }
    protected void dropDelHrsAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelMinsAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelZoneAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void chkDelstagTime_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDelstagTime.Checked)
        {
            lblDelTimeFrom.Visible = true;
            lblDelTimeTo.Visible = true;
            dropDelTimeTo.Visible = true;
            dropDelMinTo.Visible = true;
            dropDelZoneTo.Visible = true;
            txtDelISTFrom.Visible = true;
            txtDelISTTo.Visible = true;
            lblDelISTFrom.Visible = true;
            lblDelISTTo.Visible = true;
        }
        else
        {
            lblDelTimeFrom.Visible = false;
            lblDelTimeTo.Visible = false;
            dropDelTimeTo.Visible = false;
            dropDelMinTo.Visible = false;
            dropDelZoneTo.Visible = false;
            txtDelISTFrom.Visible = true;
            txtDelISTTo.Visible = false;
            lblDelISTFrom.Visible = false;
            lblDelISTTo.Visible = false;
        }
        //DelPopUp.Show();
    }
    protected void chkDelStagDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDelStagDate.Checked)
        {
            lblDelDueDateFrom.Visible = true;
            txtDelDueDateFrom.Visible = true;
            lblDelDueDateTo.Visible = true;
            txtDelDueDateTo.Visible = true;
            img2.Visible = true;
        }
        else
        {
            lblDelDueDateFrom.Visible = false;
            txtDelDueDateFrom.Visible = true;
            lblDelDueDateTo.Visible = false;
            txtDelDueDateTo.Visible = false;
            img2.Visible = false;
        }
        //DelPopUp.Show();
    }
    protected void dropDelTimeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        txtDelISTTo.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelMinTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        txtDelISTTo.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelZoneTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        txtDelISTTo.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelTimeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
        txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelMinFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
        txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void dropDelZoneFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
        txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
        //DelPopUp.Show();
    }
    protected void onDelSave(object sender, EventArgs e)
    {
        if (validateScreen1())
        {
            ArrayList al = new ArrayList();
            Hashtable val1 = null;

            foreach (GridViewRow grs in gvJobTrackFiles.Rows)
            {
                HiddenField sJobID = (HiddenField)grs.FindControl("hid_NL_ID");
                HiddenField hid_FP_ID = (HiddenField)grs.FindControl("hid_FP_ID");
                DropDownList DelStatus = (DropDownList)grs.FindControl("dropDelStatus");
                CheckBox chkDelStatus = (CheckBox)grs.FindControl("chkDelStatus");

                string delDate = "";
                if (txtdeldateAll.Text != "")
                    delDate = DateTime.Parse(txtdeldateAll.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    delDate = "";

                int ytc;
                if (chkDelYTC.Checked)
                    ytc = 1;
                else
                    ytc = 0;
                int duetime;
                if (chkDelstagTime.Checked)
                    duetime = 1;
                else
                    duetime = 0;
                int dueDate;
                if (chkDelStagDate.Checked)
                    dueDate = 1;
                else
                    dueDate = 0;
                string ddate = "";
                if (txtDelDueDateFrom.Text != "")
                    ddate = DateTime.Parse(txtDelDueDateFrom.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    ddate = "";
                string dTodate = "";
                if (txtDelDueDateTo.Text != "")
                    dTodate = DateTime.Parse(txtDelDueDateTo.Text.Trim()).ToString("MM/dd/yyyy");
                else
                    dTodate = "";

                int val = 0;
                if (chkDelStatus.Checked)
                {
                    if (dropCurStage.SelectedValue == "1")
                    {
                        string inproc = "spInsertNextJobHis_LP1";
                        string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                        {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                        {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                        {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                        {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@RecDate", txtTarRecDate.Text},{"@IsExist","Output"}
                                     };
                        val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    }
                    else if (dropCurStage.SelectedValue == "0")
                    {
                        string inproc = "spUpdateDelJobHis_LP1";
                        string[,] pname ={
                                {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",dropDelHrsAll.SelectedValue},
                                {"@DEL_MINS",dropDelMinsAll.SelectedValue},{"@DEL_ZONE",dropDelZoneAll.SelectedValue},
                                {"@DEL_IST",txtDel_ISTAll.Text},{"@IsExist","Output"},
                                {"@RecDate", txtTarRecDate.Text},{"@DelStatus",dropDelFileStatus.SelectedValue}
                             };
                        val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    }
                    else if (dropCurStage.SelectedValue == "2")
                    {
                        string inproc = "spInsertNextFinalJobHis_LP1";
                        string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                        {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                        {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                        {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                        {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@RecDate", txtTarRecDate.Text},{"@IsExist","Output"}
                                     };
                        val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    }
                    else if (dropCurStage.SelectedValue == "3")
                    {
                        string inproc = "spInsertFinalJobHis_LP1";
                        string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                        {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                        {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                        {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                        {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@RecDate", txtTarRecDate.Text},{"@IsExist","Output"}
                                     };
                        val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    }
                    if (val == 1)
                        lblResult2.Text = "Inserted Successfully";
                    else
                        lblResult2.Text = "Inserted Failed!.";


                    val1 = new Hashtable();
                    val1.Add("ID", sJobID.Value);
                    val1.Add("Status", "WIP");
                    val1.Add("EMPID", Session["employeeid"].ToString());
                    val1.Add("Jobno", DelJobNo.Text);
                    al.Add(val1);

                    if (!nonLa.Update_DeliveryStatus1(al))
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");

                }
            }
            if (DelNL_ID.Text != "")
            {
                DataSet ds = new DataSet();
                ds = nonLa.getJobTracking(DelNL_ID.Text);
                if (ds != null)
                {
                    gvJobTrackFiles.DataSource = ds;
                    gvJobTrackFiles.DataBind();
                }
            }
        }
        //DelPopUp.Hide();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtProName.Text != "")
        {
            grdJobDet.Visible = true;
            DataSet ds = new DataSet();
            ds = nonLa.GetLPJobLogEvt(txtProName.Text.Trim());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblError.Text = "";
                grdJobDet.DataSource = ds;
                grdJobDet.DataBind();
            }
            else
            {
                lblError.Text = "Project Name/JobID not Found...";
                grdJobDet.DataSource = null;
                grdJobDet.DataBind();
            }
            hidepopup();
            gvJobTrackFiles.Visible = false;
        }
        else
        {
            lblError.Text = "Enter Project Name/JobID...";
        }
    }
    protected void grdJobDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.getJobTracking(e.CommandArgument.ToString());
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            //Label lblid = (Label)grdJobDet.FooterRow.FindControl("lblid");
            //Label lblLocation_id = (Label)grdJobDet.FooterRow.FindControl("lblLocation_id");
            //Label lblProName = (Label)grdJobDet.FooterRow.FindControl("lblProName");
            hid_Loc_ID.Value = ((Label)row.FindControl("lblLocation_id")).Text.ToString();
            DelProName.Text = ((Label)row.FindControl("lblProName")).Text.ToString();
            DelJobID.Text = ((Label)row.FindControl("lblid")).Text.ToString();
            DelNL_ID.Text = e.CommandArgument.ToString();
            DelLoc_ID.Text = hid_Loc_ID.Value;
            DelJobNo.Text = ((Label)row.FindControl("lblJobNo")).Text.ToString();
            getloc_timezone(Convert.ToInt32(hid_Loc_ID.Value));
            //dropDelZoneFrom.SelectedValue = ((Label)row.FindControl("lblTimeZone")).Text.ToString();
            TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
            if (dtable.Rows.Count>0)
            {
                txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
            }
            viewpopup();
            gvJobTrackFiles.DataSource = ds;
            gvJobTrackFiles.DataBind();
            lblError.Text = "";
            dtable = ds.Tables[0].Copy();
            grdJobDet.Visible = false;
        }
        else
        {
            hidepopup();
            lblError.Text = "Logged Events not Found...";
            gvJobTrackFiles.DataSource = null;
            gvJobTrackFiles.DataBind();
            grdJobDet.Visible = true;
        }
    }
    public void viewpopup()
    {
        txtTarRecDate.Text = "";
        txtTarRecDate.Visible = true;
        img10.Visible = true;
        lblRecDate.Visible = true;
        lblproname.Visible = true;
        DelProName.Visible = true;
        lblJobid.Visible = true;
        DelJobID.Visible = true;
        lblAmendsDet.Visible = true;
        dropCurStage.Visible = true;
        lblDueDate.Visible = true;
        txtDelDueDateFrom.Visible = true;
        img1.Visible = true;
        chkDelYTC.Visible = true;
        chkDelStagDate.Visible = true;
        lblDueTime.Visible = true;
        dropDelTimeFrom.Visible = true;
        dropDelMinFrom.Visible = true;
        dropDelZoneFrom.Visible = true;
        chkDelstagTime.Visible = true;
        txtDelISTFrom.Visible = true;
        lblDelTime.Visible = false;
        txtdeldateAll.Visible = false;
        img4.Visible = false;
        dropDelHrsAll.Visible = false;
        dropDelMinsAll.Visible = false;
        dropDelZoneAll.Visible = false;
        lblDelTime_IST.Visible = false;
        txtDel_ISTAll.Visible = false;
        lblDelStatusFile.Visible = false;
        dropDelFileStatus.Visible = false;
        gvJobTrackFiles.Visible = true;
        DelSave.Visible = true;
    }
    public void hidepopup()
    {
        txtTarRecDate.Text = "";
        txtTarRecDate.Visible = false;
        img10.Visible = false;
        lblRecDate.Visible = false;
        DelProName.Text = "";
        DelJobID.Text = "";
        txtDelDueDateFrom.Text = "";
        txtDelDueDateTo.Text = "";
        chkDelYTC.Checked = false;
        chkDelStagDate.Checked = false;
        chkDelstagTime.Checked = false;
        txtDelISTFrom.Text = "";
        txtDelISTTo.Text = "";

        lblproname.Visible = false;
        DelProName.Visible = false;
        lblJobid.Visible = false;
        DelJobID.Visible = false;
        lblAmendsDet.Visible = false;
        dropCurStage.Visible = false;
        lblDueDate.Visible = false;
        txtDelDueDateFrom.Visible = false;
        img1.Visible = false;
        chkDelYTC.Visible = false;
        chkDelStagDate.Visible = false;
        lblDueTime.Visible = false;
        dropDelTimeFrom.Visible = false;
        dropDelMinFrom.Visible = false;
        dropDelZoneFrom.Visible = false;
        chkDelstagTime.Visible = false;
        txtDelISTFrom.Visible = false;
        lblDelTime.Visible = false;
        txtdeldateAll.Visible = false;
        img4.Visible = false;
        dropDelHrsAll.Visible = false;
        dropDelMinsAll.Visible = false;
        dropDelZoneAll.Visible = false;
        lblDelTime_IST.Visible = false;
        txtDel_ISTAll.Visible = false;
        lblDelStatusFile.Visible = false;
        dropDelFileStatus.Visible = false;
        gvJobTrackFiles.Visible = false;
        DelSave.Visible = false;
    }
}