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

public partial class LaunchJobTrack : System.Web.UI.Page
{
    int iRowID = 1;
    Launch_SQL oLaunch = new Launch_SQL();
    Non_Launch nonLa = new Non_Launch();
    datasourceSQL sql = new datasourceSQL();
    private static DataTable dtable4 = new DataTable();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = nonLa.getAllJobTracking();
            if (ds != null)
            {
                Session["LiveDS1"] = ds.Tables[0];
                Session["viewTable1"] = ds.Tables[0];
                dtable4 = ds.Tables[0].Copy();
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
                
            }
        }
    }
    private DropDownList bindDDL(DataTable oTable, DropDownList oList, string sFilter, string sColName)
    {
        string ovalue = oList.SelectedValue.ToString();
        if (Page.IsPostBack && ovalue != "" && ovalue != "0" && ovalue != "zero")
        {
            DataView oview = new DataView();
            oview = oTable.DefaultView.ToTable(true, sColName).DefaultView;
            oview.RowFilter = sFilter + " IS NOT NULL ";
            oList.Items.Clear();
            oList.DataSource = oview.Table;
            oList.DataTextField = sColName;
            oList.DataValueField = sColName;
            oList.DataBind();
            oList.EnableViewState = true;
            oList.AutoPostBack = true;
            oList.Items.Insert(0, new ListItem("--NO FILTER--", "0"));
            for (int k = 0; k < oList.Items.Count; k++)
                if (oList.Items[k].Value == ovalue)
                    oList.SelectedIndex = k;
        }
        else
        {
            DataView oview = new DataView();
            oview = oTable.DefaultView.ToTable(true, sColName).DefaultView;
            oview.RowFilter = sFilter + " IS NOT NULL";
            oList.Items.Clear();
            oList.DataSource = oview.Table;
            oList.DataTextField = sColName;
            oList.DataValueField = sColName;
            oList.DataBind();
            oList.EnableViewState = true;
            oList.AutoPostBack = true;
            oList.Items.Insert(0, new ListItem("--NO FILTER--", "0"));
        }
        return oList;
    }

    private DropDownList bindDDL(DataTable oTable, string oDDList, string sFilter, string sColName)
    {
        DataView oview = new DataView();
        oview = oTable.DefaultView.ToTable(true, sColName).DefaultView;
        oview.RowFilter = sFilter + " IS NOT NULL";
        DropDownList oList = new DropDownList();
        oList.ID = oDDList;
        oList.SelectedIndexChanged += new EventHandler(oList_SelectedIndexChanged);
        oList.Items.Clear();
        oList.DataSource = oview.Table;
        oList.DataTextField = sColName;
        oList.DataValueField = sColName;
        oList.DataBind();
        oList.EnableViewState = true;
        oList.AutoPostBack = true;
        oList.Items.Insert(0, new ListItem("--NO FILTER--", "0"));
        this.Controls.Add(oList);
        return oList;
    }
    protected void oList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable oTable = new DataTable();
            oTable = GetVoidViewTable().Table;
            gvJobTrack.DataSource = oTable;
            gvJobTrack.DataBind();
            iRowID = 1;
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }

    }
    private DataView GetVoidViewTable()
    {
        string sFilterText = "";
        DataTable oTable = (DataTable)(Session["LiveDS1"]);
        DataView oview = oTable.DefaultView;
        oview.RowFilter = "";
        if (ddlcustomer.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "CUSTNAME='" + ddlcustomer.SelectedValue + "'";
        }
        if (ddlLocation.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "Location_Name='" + ddlLocation.SelectedValue + "'";
        }
        if (ddlTask.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "TaskName='" + ddlTask.SelectedValue + "'";
        }
        if (ddlDueDate.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "DUE_DATEFROM='" + ddlDueDate.SelectedValue + "'";
        }
        if (ddlDueTime.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            
            sFilterText += "IST<=#" + Convert.ToDateTime(ddlDueTime.SelectedValue.Replace(" IST", "")).ToString().Replace(DateTime.Now.ToShortDateString(),"01/01/1900").ToString() + "#";
        }
        if (ddlStatus.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "DelStatus='" + ddlStatus.SelectedValue + "'";
        }
        oview.RowFilter = sFilterText;
        return oview;
    }
    protected void gvJobTrack_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable oTable = (DataTable)Session["LiveDS1"];
        if (e.Row.RowType == DataControlRowType.Header && !Page.IsPostBack)
        {
            if (Session["LiveDS1"] != null)
            {
                bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
                bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
                bindDDL(oTable, ddlTask, "TaskName", "TaskName");
                bindDDL(oTable, ddlDueDate, "DUE_DATEFROM", "DUE_DATEFROM");
                bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
                bindDDL(oTable, ddlStatus, "DelStatus", "DelStatus");
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = iRowID.ToString();
            iRowID++;

            HiddenField FP_ID = e.Row.FindControl("hid_FP_ID") as HiddenField;
            DataSet ds = new DataSet();
            ds = nonLa.GetFPStatus(FP_ID.Value);
            DropDownList status = e.Row.FindControl("dropDelStatus") as DropDownList;
            status.Items.Add(new ListItem("--Select--", "0"));
            status.Items.Add(new ListItem("P", "P"));
            status.Items.Add(new ListItem("C", "C"));
            status.Items.Add(new ListItem("WIP", "WIP"));
            status.Items.Add(new ListItem("Del", "Del"));
            status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["DelStatus"].ToString()))].Selected = true;

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
        lboxtask1.Text="";
        lblResult1.Text = "";
        txtDelDate.Text = "";
        lblResult1.Text = "";
    }
    protected void Click(object sender, EventArgs e)
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
            HiddenField sJobID = (HiddenField)row.Cells[1].FindControl("hid_NL_ID");
            HiddenField sFPID = (HiddenField)row.Cells[1].FindControl("hid_FP_ID");
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
            //getloc_timezone(Convert.ToInt16(r["LOCATION_ID"].ToString()));
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
            if (r["LastAmend_ID"].ToString() == "52" && r["Despatch_Date"].ToString() != "")
            {
                btnSave1.Visible = false;
            }
            else
            {
                btnSave1.Visible = true;
            }
            ddlStatus1.Items.Add(new ListItem("P", "P"));
            ddlStatus1.Items.Add(new ListItem("C", "C"));
            ddlStatus1.Items.Add(new ListItem("WIP", "WIP"));
            ddlStatus1.Items.Add(new ListItem("Del", "Del"));
            ddlStatus1.Items[ddlStatus1.Items.IndexOf(ddlStatus.Items.FindByValue(r["DelStatus"].ToString()))].Selected = true;

            if (r["DelStatus"].ToString() == "P")
                ddlStatus1.CssClass = "gridP";
            else if (r["DelStatus"].ToString() == "C")
                ddlStatus1.CssClass = "gridC";
            else if (r["DelStatus"].ToString() == "Del")
                ddlStatus1.CssClass = "gridDel";
            else if (r["DelStatus"].ToString() == "WIP")
                ddlStatus1.CssClass = "gridWIP";
            popup1.Show();
        }
    }
    protected void onDelSave(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gvJobTrack.Rows)
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

            int val = 0;
            if (chkDelStatus.Checked)
            {
                if (dropCurStage.SelectedValue == "1")
                {
                    string inproc = "spInsertNextJobHis_LP";
                    string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},
                                        {"@DelStatus",DelStatus.SelectedValue},{"@IsExist","Output"}
                                     };
                    val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }
                else
                {
                    string inproc = "spUpdateDelJobHis_LP";
                    string[,] pname ={
                                {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},
                                {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",dropDelHrsAll.SelectedValue},
                                {"@DEL_MINS",dropDelMinsAll.SelectedValue},{"@DEL_ZONE",dropDelZoneAll.SelectedValue},
                                {"@DEL_IST",txtDel_ISTAll.Text},{"@IsExist","Output"},
                                {"@DelStatus",DelStatus.SelectedValue}
                             };
                    val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                }

                if (val == 1)
                    lblResult2.Text = "Inserted Successfully";
                else
                    lblResult2.Text = "Inserted Failed!.";
            }
        }
       
        DataSet ds = new DataSet();
        ds = nonLa.getAllJobTracking();
        if (ds != null)
        {
            gvJobTrack.DataSource = ds;
            gvJobTrack.DataBind();
        }
        
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
        if (ddlStatus1.SelectedValue == "Del")
        {
            if (delDate.ToString() != "")
            {
                string inproc = "spUpdateJobHis_LP";
                string[,] pname ={
                            {"@YTCYN",ytc.ToString()},{"@DUETIMEYN",duetime.ToString()},
                            {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                            {"@DUETIMEFROM_IST",DropDueTimeFrom1.SelectedValue},{"@Due_MINFROM",DropDueMinFrom1.SelectedValue},
                            {"@TIME_ZoneFROM",DropDueTimeZoneFrom1.SelectedValue},
                            {"@Due_TimeTO",DropDueTimeTo1.SelectedValue},{"@Due_MINTO",DropDueMinTo1.SelectedValue},
                            {"@TIME_ZoneTO",DropDueTimeZoneTo1.SelectedValue},{"@MODIFIED_BY",""},
                            {"@DUETIMEFROM_IST",txtIndFrom1.Text},{"@DUETIMETO_IST",txtIndTo1.Text},
                            {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                            {"@RecDate",recDate.ToString()},{"@NL_ID",NL_ID1.Text},
                            {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",DropDelHrs.SelectedValue},
                            {"@DEL_MINS",DropDelMins.SelectedValue},{"@DEL_ZONE",DropDelTimeZone.SelectedValue},
                            {"@DEL_IST",txtDelIST.Text},{"@Amends_ID",Amends.ToString()},{"@Status",ddlStatus1.SelectedValue},
                            {"@AmendName",AmendName.ToString()},{"@FP_ID",txtFP_ID.Text},{"@LastAmend",LastAmends.ToString()}
                         };
                int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                //submit_Click(sender, e);

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
                            {"@DUETIMEFROM_IST",DropDueTimeFrom1.SelectedValue},{"@Due_MINFROM",DropDueMinFrom1.SelectedValue},
                            {"@TIME_ZoneFROM",DropDueTimeZoneFrom1.SelectedValue},
                            {"@Due_TimeTO",DropDueTimeTo1.SelectedValue},{"@Due_MINTO",DropDueMinTo1.SelectedValue},
                            {"@TIME_ZoneTO",DropDueTimeZoneTo1.SelectedValue},{"@MODIFIED_BY",""},
                            {"@DUETIMEFROM_IST",txtIndFrom1.Text},{"@DUETIMETO_IST",txtIndTo1.Text},
                            {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},
                            {"@RecDate",recDate.ToString()},{"@NL_ID",NL_ID1.Text},
                            {"@DEL_DATE",delDate.ToString()},{"@DEL_HRS",DropDelHrs.SelectedValue},
                            {"@DEL_MINS",DropDelMins.SelectedValue},{"@DEL_ZONE",DropDelTimeZone.SelectedValue},
                            {"@DEL_IST",txtDelIST.Text},{"@Amends_ID",Amends.ToString()},{"@Status",ddlStatus1.SelectedValue},
                            {"@AmendName",AmendName.ToString()},{"@FP_ID",txtFP_ID.Text},{"@LastAmend",LastAmends.ToString()}
                         };
            int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);

            if (val == 1)
                lblResult1.Text = "Inserted Successfully";
            else
                lblResult1.Text = "Inserted Failed!.";
        }

        DataSet ds = new DataSet();
        ds = nonLa.getAllJobTracking();
        if (ds != null)
        {
            gvJobTrack.DataSource = ds;
            gvJobTrack.DataBind();
        }
        if (ddlcustomer.SelectedValue != "0")
        {
            ddlcustomer_SelectedIndexChanged(sender, e);
        }
        if (ddlLocation.SelectedValue != "0")
        {
            ddlLocation_SelectedIndexChanged(sender, e);
        }
        if (ddlTask.SelectedValue != "0")
        {
            ddlTask_SelectedIndexChanged(sender, e);
        }
        if (ddlDueDate.SelectedValue != "0")
        {
            ddlDueDate_SelectedIndexChanged(sender, e);
        }
        if (ddlDueTime.SelectedValue != "0")
        {
            ddlDueTime_SelectedIndexChanged(sender, e);
        }
        if (ddlStatus.SelectedValue != "0")
        {
            ddlStatus_SelectedIndexChanged(sender, e);
        }
        popup1.Show();
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
        }
        else
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = true;
            img4.Visible = true;
            dropDelHrsAll.Enabled = true;
            dropDelMinsAll.Enabled = true;
            dropDelZoneAll.Enabled = true;
        }
        DelPopUp.Show();
    }
    public void TimeFormat(string LocID, string Hrs, string Min, string Zone)
    {
        string hrs = Hrs;
        string min = Min;
        DataSet time = nonLa.getTimeDetails(hrs, min, LocID.ToString(), Zone);
        dtable = time.Tables[0].Copy();
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
    protected void OnDelivery(object sender, EventArgs e)
    {
        //DataSet dsNL = nonLa.getJobDetailsByID(hfP_ID.Value);
        //DataRow r = dsNL.Tables[0].Rows[0];
        //DelJobID.Text = r["JOBID"].ToString();
        //DelProName.Text = r["projectname"].ToString();
        //DelNL_ID.Text = r["NL_ID"].ToString();
        //dropDelZoneAll.SelectedValue = r["TIME_ZONEFROM"].ToString();
        //DelLoc_ID.Text = r["LOCATION_ID"].ToString();
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
    }

    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "DUE_DATEFROM", "DUE_DATEFROM");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "DelStatus", "DelStatus");
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "DUE_DATEFROM", "DUE_DATEFROM");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "DelStatus", "DelStatus");
    }
    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlDueDate, "DUE_DATEFROM", "DUE_DATEFROM");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "DelStatus", "DelStatus");
    }
    protected void ddlDueDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "DelStatus", "DelStatus");
    }
    protected void ddlDueTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "DUE_DATEFROM", "DUE_DATEFROM");
        bindDDL(oTable, ddlStatus, "DelStatus", "DelStatus");
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "DUE_DATEFROM", "DUE_DATEFROM");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
         int i = 1;
         if (dtable4 != null && dtable4.Rows.Count > 0)
         {
             StringBuilder sbData = new StringBuilder();
             sbData.Append("<table border='1'>");
             sbData.Append("<tr valign='top'><td colspan='14' align='center'><h4>Launch OverAll Report</h4></td><tr>");
             sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Location</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Languages</b></td><td bgcolor='silver'><b>Software</b></td><td bgcolor='silver'><b>File Name</b></td><td bgcolor='silver'><b>Due Date</b></td><td bgcolor='silver'><b>Due Time</b></td><td bgcolor='silver'><b>Due Time(IST)</b></td><td bgcolor='silver'><b>Pages</b></td><td bgcolor='silver'><b>Stages</b></td><td bgcolor='silver'><b>Workflow</b></td><td bgcolor='silver'><b>Status</b></td><td bgcolor='silver'><b>Launch(Y/N)</b></td></tr>");
             foreach (DataRow r in dtable4.Rows)
             {
                 sbData.Append("<tr valign='top'>");
                 sbData.Append("<td>" + i + "</td>");
                 sbData.Append("<td>" + r["Jobid"] + "</td>");
                 sbData.Append("<td>" + r["Projectname"] + "</td>");
                 sbData.Append("<td>" + r["Custname"] + "</td>");
                 sbData.Append("<td>" + r["Location_Name"] + "</td>");
                 sbData.Append("<td>" + r["TaskName"] + "</td>");
                 sbData.Append("<td>" + r["Lang_name"] + "</td>");
                 sbData.Append("<td>" + r["Soft_Name"] + "</td>");
                 sbData.Append("<td>" + r["Files_Name"] + "</td>");
                 sbData.Append("<td>" + r["DUE_DATEFROM"] + "</td>");
                 //sbData.Append("<td>" + r["DUE_DATETO"] + "</td>");
                 sbData.Append("<td>" + r["DUE_TimeFrom"] + "</td>");
                 //sbData.Append("<td>" + r["DueTimeTo"] + "</td>");
                 sbData.Append("<td>" + r["DUETIMEFROM_IST"] + "</td>");
                 //sbData.Append("<td>" + r["DUETIMETO_IST"] + "</td>");
                 sbData.Append("<td>" + r["Pages"] + "</td>");
                 sbData.Append("<td>" + r["AmendName"] + "</td>");
                 sbData.Append("<td>" + r["WorkFlow"] + "</td>");
                 sbData.Append("<td>" + r["DelStatus"] + "</td>");
                 sbData.Append("<td>" + r["Launch"] + "</td>");
                 sbData.Append("</tr>");
                 i = i + 1;
             }
             sbData.Append("</table>");
             Response.Clear();
             Response.ContentType = "application/vnd.ms-excel";
             Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_JobTrack_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
             Response.ContentEncoding = Encoding.Unicode;
             Response.BinaryWrite(Encoding.Unicode.GetPreamble());
             Response.Write(sbData.ToString());
             Response.Flush();
             Response.Close();
         }
    }
}