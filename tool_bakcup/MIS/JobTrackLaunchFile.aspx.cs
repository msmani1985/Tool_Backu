using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

public partial class JobTrackLaunchFile : System.Web.UI.Page
{
    int iRowID = 1;
    Launch_SQL oLaunch = new Launch_SQL();
    Non_Launch nonLa = new Non_Launch();
    datasourceSQL sql = new datasourceSQL();
    private static DataTable dtable4 = new DataTable();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(Request.QueryString["ID"]);
            dtable4 = ds.Tables[0].Copy();
            if (ds != null)
            {
                gvJobTrackFiles.DataSource = ds;
                gvJobTrackFiles.DataBind();
            }
            hid_Loc_ID.Value = Request.QueryString["Loc_ID"];
        }
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

            if (r["DelStatus"].ToString() != "Del")
            {
                lboxStage.Enabled = true;
                btnSave1.Visible = true;
            }
            else if (r["LastAmend_ID"].ToString() == "52")
            {
                lboxStage.Enabled = true;
                btnSave1.Visible = true;
            }
            else
            {
                lboxStage.Enabled = false;
                btnSave1.Visible = false;
            }

            popup1.Show();
        }
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
        if (Request.QueryString["ID"] != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(Request.QueryString["ID"]);
            if (ds != null)
            {
                gvJobTrackFiles.DataSource = ds;
                gvJobTrackFiles.DataBind();
            }
        }
        popup1.Hide();
    }
    protected void OnDelivery(object sender, EventArgs e)
    {
        DataSet dsNL = new DataSet();
        dsNL = nonLa.GetAmends();
        //lboxDelSatge.DataSource = dsNL;
        //lboxDelSatge.DataTextField = dsNL.Tables[0].Columns[1].ToString();
        //lboxDelSatge.DataValueField = dsNL.Tables[0].Columns[0].ToString();
        //lboxDelSatge.DataBind();
        dsNL = nonLa.getDelJobDetailsByID(Request.QueryString["ID"]);
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
        DelPopUp.Show();
        
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
        dropDelTimeFrom.SelectedValue="00";
        dropDelMinFrom.SelectedValue = "00";
        lblDelTimeTo.Visible = false;
        dropDelTimeTo.Visible = false;
        dropDelMinTo.Visible = false;
        dropDelZoneTo.Visible = false;
        dropDelTimeTo.SelectedValue = "00";
        dropDelMinTo.SelectedValue = "00";
        chkDelstagTime.Visible = true;
        chkDelstagTime.Checked = false;
        lblISTTime.Visible = false;
        lblDelISTFrom.Visible = false;
        txtDelISTFrom.Visible = true;
        txtDelISTFrom.Text = "";
        lblDelISTTo.Visible = false;
        txtDelISTTo.Visible = false;
        txtDelISTTo.Text = "";
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
            lblISTTime.Visible = true;
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
            lblISTTime.Visible = true;
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
            lblISTTime.Visible = true;
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
            lblISTTime.Visible = true;
            lblDelISTFrom.Visible = true;
            txtDelISTFrom.Visible = true;
            lblDelStatusFile.Visible = true;
            dropDelFileStatus.Visible = true;
            chkDelstagTime_CheckedChanged(sender, e);
            chkDelStagDate_CheckedChanged(sender, e);
        }
        DelPopUp.Show();
    }
    protected void dropDelHrsAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelMinsAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelZoneAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DelLoc_ID.Text, dropDelHrsAll.SelectedValue, dropDelMinsAll.SelectedValue, dropDelZoneAll.SelectedValue);
        txtDel_ISTAll.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void onDelSave(object sender, EventArgs e)
    {
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
                    string inproc = "spInsertNextJobHis_LP";
                    string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                        {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                        {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                        {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                        {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@IsExist","Output"}
                                     };
                    val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                else if (dropCurStage.SelectedValue == "0")
                {
                    string inproc = "spUpdateDelJobHis_LP";
                    string[,] pname ={
                                {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",dropDelHrsAll.SelectedValue},
                                {"@DEL_MINS",dropDelMinsAll.SelectedValue},{"@DEL_ZONE",dropDelZoneAll.SelectedValue},
                                {"@DEL_IST",txtDel_ISTAll.Text},{"@IsExist","Output"},
                                {"@DelStatus",dropDelFileStatus.SelectedValue}
                             };
                    val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                else if (dropCurStage.SelectedValue == "2")
                {
                    string inproc = "spInsertNextFinalJobHis_LP";
                    string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                        {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                        {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                        {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                        {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@IsExist","Output"}
                                     };
                    val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                else if (dropCurStage.SelectedValue == "3")
                {
                    string inproc = "spInsertFinalJobHis_LP";
                    string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},{"@DueDateFrom",ddate.ToString()},
                                        {"@DueDateTo",dTodate.ToString() },{"@DueTimeFrom",dropDelTimeFrom.SelectedValue},{"@DueMinFrom",dropDelMinFrom.SelectedValue},
                                        {"@DueZoneFrom", dropDelZoneFrom.SelectedValue},{"@DueTimeTo",dropDelTimeTo.SelectedValue},{"@DueMinTo",dropDelMinTo.SelectedValue},
                                        {"@DueZoneTo",dropDelZoneTo.SelectedValue},{"@DueTimeFrom_IST",txtDelISTFrom.Text},{"@DueTimeTo_IST",txtDelISTTo.Text},
                                        {"@DueDateYN", dueDate.ToString()},{"@DueTimeYN",duetime.ToString()},{"@YTCYN",ytc.ToString()},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@IsExist","Output"}
                                     };
                    val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                if (val == 1)
                    lblResult2.Text = "Inserted Successfully";
                else
                    lblResult2.Text = "Inserted Failed!.";
            }
        }
        if (Request.QueryString["ID"] != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(Request.QueryString["ID"]);
            if (ds != null)
            {
                gvJobTrackFiles.DataSource = ds;
                gvJobTrackFiles.DataBind();
            }
        }
        DelPopUp.Hide();
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
        DelPopUp.Show();
    }
    protected void dropDelTimeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        txtDelISTTo.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelMinTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        txtDelISTTo.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelZoneTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeTo.SelectedValue, dropDelMinTo.SelectedValue, dropDelZoneTo.SelectedValue);
        txtDelISTTo.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelTimeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
        txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelMinFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
        txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
    }
    protected void dropDelZoneFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(hid_Loc_ID.Value, dropDelTimeFrom.SelectedValue, dropDelMinFrom.SelectedValue, dropDelZoneFrom.SelectedValue);
        txtDelISTFrom.Text = dtable.Rows[0]["Mins"].ToString();
        DelPopUp.Show();
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
            txtDelISTFrom.Visible = false;
            txtDelISTTo.Visible = false;
            lblDelISTFrom.Visible = false;
            lblDelISTTo.Visible = false;
        }
        DelPopUp.Show();
    }
    protected void cmd_Excel_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable4 != null && dtable4.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='11' align='center'><h4>Track File List</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>File Name</b></td><td bgcolor='silver'><b>No.of Pages</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Software</b></td><td bgcolor='silver'><b>Language</b></td><td bgcolor='silver'><b>AmendName</b></td><td bgcolor='silver'><b>Status</b></td><td bgcolor='silver'><b>WorkFlow</b></td></tr>");
            foreach (DataRow r in dtable4.Rows)
            {
                string status = r["DelStatus"].ToString();
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["Files_Name"] + "</td>");
                sbData.Append("<td>" + r["pages"] + "</td>");
                sbData.Append("<td>" + r["TaskName"] + "</td>");
                sbData.Append("<td>" + r["Soft_Name"] + "</td>");
                sbData.Append("<td>" + r["Lang_Name"] + "</td>");
                sbData.Append("<td>" + r["AmendName"] + "</td>");
                if (status.ToString() == "P")
                    sbData.Append("<td  bgcolor='orange'>" + status + "</td>");
                else if (status.ToString() == "C")
                    sbData.Append("<td  bgcolor='Gray'>" + status + "</td>");
                else if (status.ToString() == "WIP")
                    sbData.Append("<td  bgcolor='LightGreen'>" + status + "</td>");
                else if (status.ToString() == "Del")
                    sbData.Append("<td  bgcolor='green'>" + status + "</td>");
                else
                    sbData.Append("<td>" + r["DelStatus"] + "</td>");
                sbData.Append("<td>" + r["WorkFlow"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Track_File_List_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
    }
}