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
using System.Net;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.OleDb;

public partial class NonLaunch : System.Web.UI.Page
{
    Launch_SQL oLaunch = new Launch_SQL();
    Non_Launch nonLa = new Non_Launch();
    datasourceSQL sql = new datasourceSQL();
    Launch la = new Launch();
    Launch_SQL oNewLa = new Launch_SQL();
    private static DataTable dtable4 = new DataTable();
    private static DataTable dtable = new DataTable();
    SqlConnection scon;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["employeeid"] == null)
        //{
        //    throw new Exception("Session Expired!");
        //}
        if (!Page.IsPostBack)
        {
            this.popScreen();
        }
       
    }
    private void popScreen()
    {
        DataSet Ds = new DataSet();
        Ds = nonLa.GetTask();
        lboxtask.DataTextField = Ds.Tables[0].Columns[1].ToString();
        lboxtask.DataValueField = Ds.Tables[0].Columns[0].ToString();
        lboxtask.DataSource = Ds;
        lboxtask.DataBind();

        //Ds = nonLa.getAllLocation();
        //DropLocation.DataSource = Ds;
        //DropLocation.DataTextField = Ds.Tables[0].Columns[1].ToString();
        //DropLocation.DataValueField = Ds.Tables[0].Columns[0].ToString();
        //DropLocation.DataBind();
        DropLocation.Items.Insert(0, new ListItem("-- select --", "0"));

        Ds = nonLa.getAllCustomers();
        drpProjectcustomer.DataSource = Ds;
        drpProjectcustomer.DataTextField = Ds.Tables[0].Columns[1].ToString();
        drpProjectcustomer.DataValueField = Ds.Tables[0].Columns[0].ToString();
        drpProjectcustomer.DataBind();
        drpProjectcustomer.Items.Insert(0, new ListItem("-- select --", "0"));

        drpCustomerSearch.DataSource = Ds;
        drpCustomerSearch.DataTextField = Ds.Tables[0].Columns[1].ToString();
        drpCustomerSearch.DataValueField = Ds.Tables[0].Columns[0].ToString();
        drpCustomerSearch.DataBind();
        drpCustomerSearch.Items.Insert(0, new ListItem("-- All Customers --", "0"));

        Ds = nonLa.GetLanguage();
        lboxlang.DataTextField = Ds.Tables[0].Columns[1].ToString();
        lboxlang.DataValueField = Ds.Tables[0].Columns[0].ToString();
        lboxlang.DataSource = Ds;
        lboxlang.DataBind();

        DataSet sw = nonLa.getAllSoftware();
        lboxSW.DataSource = sw;
        lboxSW.DataTextField = sw.Tables[0].Columns[1].ToString();
        lboxSW.DataValueField = sw.Tables[0].Columns[0].ToString();
        lboxSW.DataBind();

        DDMonthList.SelectedValue = DateTime.Now.Month.ToString();
        DDYearList.SelectedValue = DateTime.Now.Year.ToString();

        if (Request.QueryString["q"] != null && Request.QueryString["q"].ToString().Trim() != "")
        {
            string pageQuery = Request.QueryString["q"].ToString().Trim();
            switch (pageQuery)
            {
                case "lnkLaunchdetails":
                    this.showPanel(tabNonLaunch);
                    break;
            }
        }
        else 
            this.showPanel(tabGeneral);
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "tabGeneral":
                miGeneral.Attributes.Add("class", "current");
                miLaunchDetails.Attributes.Add("class", "");
                miFileDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class","");
                miLoggedEvent.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                this.tabGeneral.Visible = true;
                this.tabNonLaunch.Visible = false;
                this.tabFileDetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabreportdetails.Visible = false;
                break;
            case "tabNonLaunch":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "current");
                miFileDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class","");
                miLoggedEvent.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabNonLaunch.Visible = true;
                this.tabFileDetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabreportdetails.Visible = false;
                break;
            case "tabFileDetails":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileDetails.Attributes.Add("class", "current");
                miJobTracking.Attributes.Add("class","");
                miLoggedEvent.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabNonLaunch.Visible = false;
                this.tabFileDetails.Visible = true;
                this.tabJobTracking.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabreportdetails.Visible = false;
                break;
            case "tabJobTracking":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "current");
                miLoggedEvent.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabNonLaunch.Visible = false;
                this.tabFileDetails.Visible = false;
                this.tabJobTracking.Visible = true;
                this.tabLoggedEvents.Visible = false;
                this.tabNewQuote.Visible = false;
                this.tabreportdetails.Visible = false;
                break;
            case "tabLoggedEvents":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "current");
                miNewCostDetails.Attributes.Add("class", "");
                this.tabGeneral.Visible = false;
                this.tabNonLaunch.Visible = false;
                this.tabFileDetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabLoggedEvents.Visible = true;
                this.tabNewQuote.Visible = false;
                this.tabreportdetails.Visible = false;
                break;
            case "tabNewQuote":
                miGeneral.Attributes.Add("class", "");
                miLaunchDetails.Attributes.Add("class", "");
                miFileDetails.Attributes.Add("class", "");
                miJobTracking.Attributes.Add("class", "");
                miLoggedEvent.Attributes.Add("class", "");
                miNewCostDetails.Attributes.Add("class", "current");
                this.tabGeneral.Visible = false;
                this.tabNonLaunch.Visible = false;
                this.tabFileDetails.Visible = false;
                this.tabJobTracking.Visible = false;
                this.tabLoggedEvents.Visible = false;
                this.tabreportdetails.Visible = false;
                this.tabNewQuote.Visible = true;
                break;
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpProjectcustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtProjectTitle.Text.Trim() == "") sMessage += i++ + ". Enter Project Name\\r\\n";
        if (lboxtask.GetSelectedIndices().Length == 0) sMessage += i++ + ". Select Task\\r\\n";
        if (drpType.SelectedItem.Value == "0") sMessage += i++ + ". Select a Job Type\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    protected void drpProjectcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = nonLa.GetLocationCust(drpProjectcustomer.SelectedValue);
        if (ds != null)
        {
            DropLocation.Enabled = true;
            DropLocation.DataSource = ds;
            DropLocation.DataValueField = ds.Tables[0].Columns[3].ToString();
            DropLocation.DataTextField = ds.Tables[0].Columns[4].ToString();
            DropLocation.DataBind();
        }
        else
        {
            DropLocation.Items.Insert(0, new ListItem("-- select --", "0"));
            DropLocation.SelectedValue = "0";
            DropLocation.Enabled = false;
        }
        getloc_timezone(Convert.ToInt16(DropLocation.SelectedValue));
    }
    public void getloc_timezone(int locid)
    {
        DataSet ds = nonLa.GetTimeZone(locid);
        if (ds != null)
        {
            DropDueTimeZoneFrom.DataSource = ds;
            DropDueTimeZoneFrom.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneFrom.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneFrom.DataBind();
            DropDueTimeZoneTo.DataSource = ds;
            DropDueTimeZoneTo.DataValueField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneTo.DataTextField = ds.Tables[0].Columns[2].ToString();
            DropDueTimeZoneTo.DataBind();
            TimeFormat(locid.ToString(), DropDueTimeFrom.SelectedValue, DropDueMinFrom.SelectedValue, DropDueTimeZoneFrom.SelectedValue);
            TimeFormat(locid.ToString(), DropDueTimeTo.SelectedValue, DropDueMinTo.SelectedValue, DropDueTimeZoneTo.SelectedValue);
        }
        else
        {
            DropDueTimeZoneFrom.Items.Insert(0, new ListItem(" ", "0"));
            DropDueTimeZoneTo.Items.Insert(0, new ListItem(" ", "0"));
            DropDueTimeZoneFrom.SelectedValue = "0";
            DropDueTimeZoneTo.SelectedValue = "0";
            DropDueTimeZoneFrom.Enabled = false;
            DropDueTimeZoneTo.Enabled = false;
        }
    }
    protected void DropLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        getloc_timezone(Convert.ToInt16(DropLocation.SelectedValue));
    }
    protected void lboxtask_SelectedIndexChanged(object sender, EventArgs e)
    {
        string TaskName = "";
        string TaskValue = "";
        Session["TaskValue"] = "";
        DataTable dTable = new DataTable();
        dTable.Columns.Add("Task");
        dTable.Columns.Add("Task_ID");
        CheckBoxTask.Items.Clear();
        chkFileType.Items.Clear();
        for (int j = 0; j < lboxtask.Items.Count; j++)
        {
            if (lboxtask.Items[j].Selected == true)
            {
                if (TaskName == "")
                {
                    TaskName = lboxtask.Items[j].Text;
                    TaskValue = lboxtask.Items[j].Value;
                }
                else
                {
                    TaskName = TaskName + ',' + lboxtask.Items[j].Text;
                    TaskValue = TaskValue + ',' + lboxtask.Items[j].Value;
                }
                Session["TaskValue"] = TaskValue.ToString();
                dTable.Rows.Add(lboxtask.Items[j].Text, lboxtask.Items[j].Value);

                ListItem it = new ListItem();
                it.Value = lboxtask.Items[j].Value;
                it.Text = lboxtask.Items[j].Text;
                CheckBoxTask.Items.Add(it);
            }
        }
        DataSet Ds = new DataSet();
        Ds.Tables.Add(dTable);
        //gv_Soft.DataSource = Ds;
        //gv_Soft.DataBind();


        Ds = nonLa.GetFormats(TaskName.Replace(",", "','"));
        lboxformat.DataTextField = Ds.Tables[0].Columns[1].ToString();
        lboxformat.DataValueField = Ds.Tables[0].Columns[0].ToString();
        lboxformat.DataSource = Ds;
        lboxformat.DataBind();
        if (TaskValue.ToString() == "1" || TaskValue.ToString() == "2" || TaskValue.ToString() == "6" || TaskValue.ToString() == "1,2" || TaskValue.ToString() == "1,6" || TaskValue.ToString() == "1,2,6" || TaskValue.ToString() == "2,6")
        {
            ListItem li1 = new ListItem("Source", "0", true);
            li1.Selected = true;
            li1.Enabled = true;
            chkFileType.Items.Add(li1);
        }
        else
        {
            ListItem li1 = new ListItem("Source", "0", true);
            ListItem li2 = new ListItem("Target", "1", true);
            li1.Enabled = true;
            li2.Enabled = true;
            chkFileType.Items.Add(li1);
            chkFileType.Items.Add(li2);
        }
        if (Ds.Tables[0].Rows[0]["taskname"].ToString() == "TE")
        {
            lblsource.Visible = true;
            DropSource.Visible = true;
            lboxformat.Visible = true;
            lblformat.Visible = true;
            TarRecDate.Visible = false;
            txtRecDate.Visible = false;
            img9.Visible = false;
        }
        else if (Ds.Tables[0].Rows[0]["taskname"].ToString() == "DTP" || Ds.Tables[0].Rows[0]["taskname"].ToString() == "File Conversion")
        {
            lblsource.Visible = false;
            DropSource.Visible = false;
            lboxformat.Visible = true;
            lblformat.Visible = true;
            TarRecDate.Visible = false;
            txtRecDate.Visible = false;
            img9.Visible = false;
        }
        else
        {
            lboxformat.Visible = false;
            lblformat.Visible = false;
            lblsource.Visible = false;
            DropSource.Visible = false;
            TarRecDate.Visible = false;
            txtRecDate.Visible = false;
            img9.Visible = false;
        }
    }
    protected void gv_Soft_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)e.Row.FindControl("lboxSoft");
            TextBox txtTargetDate = (TextBox)e.Row.FindControl("txtTargetDate");
            HiddenField task = (HiddenField)e.Row.FindControl("hf_taskID");
            HiddenField lang = (HiddenField)e.Row.FindControl("hf_LangID");
            lboxSoft.DataSource = dscust1;
            lboxSoft.DataTextField = dscust1.Tables[0].Columns[1].ToString();
            lboxSoft.DataValueField = dscust1.Tables[0].Columns[0].ToString();
            lboxSoft.DataBind();
            string soft = "";
            DataSet sv = new DataSet();
            sv = nonLa.SoftSelected(hfP_ID.Value);
            if (sv != null)
            {
                DataSet empd = new DataSet();
                empd = nonLa.GetSelectedSoft(hfP_ID.Value, task.Value,lang.Value);
                if (empd != null)
                {
                    if (empd.Tables[0].Rows.Count > 0 || empd != null)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxSoft.Items[lboxSoft.Items.IndexOf(lboxSoft.Items.FindByValue(empd.Tables[0].Rows[i]["Soft_id"].ToString()))].Selected = true;

                            if (soft == "" || soft == null)
                                soft = empd.Tables[0].Rows[i]["Soft_id"].ToString();
                            else
                                soft = soft + ',' + empd.Tables[0].Rows[i]["Soft_id"].ToString();
                        }

                    }
                    ListBox lboxVer = (ListBox)e.Row.FindControl("lboxVer");
                    if (soft.ToString() != "")
                    {
                        DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
                        lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                        lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                        lboxVer.DataSource = dsSoft;
                        lboxVer.DataBind();
                    }
                    if (empd.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;

                        }
                    }
                    txtTargetDate.Text = empd.Tables[0].Rows[0]["TARGET_DATE"].ToString();
                }
            }
        }
    }
    protected void lboxSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grs in gv_Soft.Rows)
        {
            DataSet dscust1 = nonLa.getAllSoftware();
            ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
            Label task = (Label)grs.FindControl("txt_task");
            ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
            HiddenField taskID = (HiddenField)grs.FindControl("hf_taskID");
            HiddenField lang = (HiddenField)grs.FindControl("hf_LangID");
            string sv = "";
            for (int j = 0; j < lboxSoft.Items.Count; j++)
            {
                if (lboxSoft.Items[j].Selected == true)
                {
                    if (sv == "")
                        sv = lboxSoft.Items[j].Value;
                    else
                        sv = sv + ',' + lboxSoft.Items[j].Value;
                }
            }
            if (sv.ToString() != "")
            {
                DataSet dsSoft = nonLa.GetSoftVers(sv.ToString());
                lboxVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
                lboxVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
                lboxVer.DataSource = dsSoft;
                lboxVer.DataBind();
            }
            if(hfP_ID.Value!="")
            {
                DataSet empd = new DataSet();
                empd = nonLa.GetSelectedSoft(hfP_ID.Value, taskID.Value, lang.Value);
                if (empd!=null)
                {
                    for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
                    {
                        DataSet dsVer = nonLa.GetSoftVers(sv.ToString());
                        if (dsVer.Tables[0].Select("AutoID=" + empd.Tables[0].Rows[i]["Version_id"].ToString()).Length > 0)
                        {
                            lboxVer.Items[lboxVer.Items.IndexOf(lboxVer.Items.FindByValue(empd.Tables[0].Rows[i]["Version_id"].ToString()))].Selected = true;
                        }
                    }
                }
            }
        }
    }
    protected void chkDueDate_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueDate.Checked)
        {
            lblDueFrom.Visible = true;
            txtdueFromdate.Visible = true;
            lblDueTo.Visible = true;
            txtdueTodate.Visible = true;
        }
        else
        {
            lblDueFrom.Visible = false;
            txtdueFromdate.Visible = true;
            lblDueTo.Visible = false;
            txtdueTodate.Visible = false;
        }
    }
    protected void chkDueTime_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDueTime.Checked)
        {
            lblFrom.Visible = true;
            lblTo.Visible = true;
            DropDueTimeTo.Visible = true;
            DropDueMinTo.Visible = true;
            DropDueTimeZoneTo.Visible = true;
            txtIndTo.Visible = true;
            lblIndFrom.Visible = true;
            lblIndTo.Visible = true;
        }
        else
        {
            lblFrom.Visible = false;
            lblTo.Visible = false;
            DropDueTimeTo.Visible = false;
            DropDueMinTo.Visible = false;
            DropDueTimeZoneTo.Visible = false;
            txtIndTo.Visible = false;
            lblIndFrom.Visible = false;
            lblIndTo.Visible = false;
        }
    }
    public void  TimeFormat(string LocID,string Hrs, string Min,string Zone)
    {
        string hrs = Hrs;
        string min = Min;
        DataSet time = nonLa.getTimeDetails(hrs, min, LocID.ToString(), Zone);
        //DataRow row = time.Tables[0].Rows[0];
        //txtIndFrom.Text = row["Mins"].ToString();
        //txtIndFrom1.Text = row["Mins"].ToString();
        //txtIndTo.Text = row["Mins"].ToString();
        //txtIndTo1.Text = row["Mins"].ToString();
        dtable = time.Tables[0].Copy();
    }
    protected void xlang_Click(object sender, EventArgs e)
    {
        string id = "";
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else if (tempID.Text != "")
            id = tempID.Text;

        DataSet dss = new DataSet();
        dss = nonLa.GetselectedLangLP(id.ToString());
        lboxlangused.Items.Clear();
        if (dss != null)
        {
            lboxlangused.DataSource = dss;
            lboxlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
            lboxlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
            lboxlangused.DataBind();
        }
    }
    protected void lnkSWDetails_Click1(object sender, EventArgs e)
    {
        string id = "";
        if (hfP_ID.Value != "")
            id = hfP_ID.Value.Trim();
        else if (tempID.Text != "")
            id = tempID.Text;

        string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchSoftLang.aspx?LP_ID=" + HttpUtility.UrlEncode(id.ToString()) + "&TaskValue=" + HttpUtility.UrlEncode(Session["TaskValue"].ToString())
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }
    protected void lboxSW_SelectedIndexChanged(object sender, EventArgs e)
    {
        string soft = "";
        for (int j = 0; j < lboxSW.Items.Count; j++)
        {
            if (lboxSW.Items[j].Selected == true)
            {
                if (soft.ToString() == "")
                {
                    soft = lboxSW.Items[j].Value;
                }
                else
                {
                    soft = soft + ',' + lboxSW.Items[j].Value;
                }
            }
        }
        if (soft.ToString() != "")
        {
            DataSet dsSoft = nonLa.GetSoftVers(soft.ToString());
            lboxSWVer.DataTextField = dsSoft.Tables[0].Columns[2].ToString();
            lboxSWVer.DataValueField = dsSoft.Tables[0].Columns[0].ToString();
            lboxSWVer.DataSource = dsSoft;
            lboxSWVer.DataBind();
        }
    }
    protected void DropDueTimeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DropLocation.SelectedValue, DropDueTimeFrom.SelectedValue, DropDueMinFrom.SelectedValue, DropDueTimeZoneFrom.SelectedValue);
        txtIndFrom.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropDueMinFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DropLocation.SelectedValue, DropDueTimeFrom.SelectedValue, DropDueMinFrom.SelectedValue, DropDueTimeZoneFrom.SelectedValue);
        txtIndFrom.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropDueTimeZoneFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DropLocation.SelectedValue, DropDueTimeFrom.SelectedValue, DropDueMinFrom.SelectedValue, DropDueTimeZoneFrom.SelectedValue);
        txtIndFrom.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropDueTimeTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DropLocation.SelectedValue, DropDueTimeTo.SelectedValue, DropDueMinTo.SelectedValue, DropDueTimeZoneTo.SelectedValue);
        txtIndTo.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropDueMinTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DropLocation.SelectedValue, DropDueTimeTo.SelectedValue, DropDueMinTo.SelectedValue, DropDueTimeZoneTo.SelectedValue);
        txtIndTo.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void DropDueTimeZoneTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        TimeFormat(DropLocation.SelectedValue, DropDueTimeTo.SelectedValue, DropDueMinTo.SelectedValue, DropDueTimeZoneTo.SelectedValue);
        txtIndTo.Text = dtable.Rows[0]["Mins"].ToString();
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        this.showPanel(tabGeneral);
    }
    protected void lnkLaunchdetails_Click(object sender, EventArgs e)
    {
        if(hfP_ID.Value!="")
        {
            loadJobDetails(hfP_ID.Value.Trim());
            btnJobInfo.Text = "Update";
        }
        else
        {
            Random rand = new Random();
            int name = rand.Next(1,999999999);
            tempID.Text = name.ToString();// DateTime.Now.ToString().GetHashCode().ToString("x"); 
        }
        this.showPanel(tabNonLaunch);
    }
    public void loadJobDetails(string sJobID)
    {
        if (sJobID != "")
        {
            lboxformat.ClearSelection();
            lboxtask.ClearSelection();
            linputfile.ClearSelection();
            lboxlang.ClearSelection();
            CheckBoxTask.Items.Clear();
            lboxlangused.ClearSelection();
            DataSet dsNL = nonLa.getJobDetailsByID(sJobID);
            lblNLHeader.Text = "Edit Project";
            imgNLHeader.Src = "images/tools/edit.png";
            DataRow row = dsNL.Tables[0].Rows[0];
            txtJobid.Text = row["JOBID"].ToString();
            txtProjectEditor.Text = row["PROJECTEDITOR"].ToString();
            txtProjectTitle.Text = row["projectname"].ToString();
            drpProjectcustomer.SelectedValue = row["custno"].ToString();
            DropSource.SelectedValue = row["SourceType"].ToString();
            txtLaunchID.Text = row["LaunchID"].ToString();
            drpType.SelectedValue = row["PType"].ToString();
            //txtFile.Text = row["FILE_COUNT"].ToString();
            //getloc(Convert.ToInt16(row["cust_id"].ToString()));
            DataSet ds = nonLa.GetLocationCust(drpProjectcustomer.SelectedValue);
            DropLocation.DataSource = ds;
            DropLocation.DataValueField = ds.Tables[0].Columns[3].ToString();
            DropLocation.DataTextField = ds.Tables[0].Columns[4].ToString();
            DropLocation.DataBind();
            DropLocation.SelectedValue = row["LOCATION_id"].ToString();
            getloc_timezone(Convert.ToInt16(row["LOCATION_id"].ToString()));
            dropSwPlat.SelectedValue = row["platform"].ToString();
            if (row["DUEDATEYN"].ToString() == "1")
            {
                lblDueFrom.Visible = true;
                txtdueFromdate.Visible = true;
                lblDueTo.Visible = true;
                txtdueTodate.Visible = true;
                chkDueDate.Checked = true;
            }
            else
            {
                lblDueFrom.Visible = false;
                txtdueFromdate.Visible = true;
                lblDueTo.Visible = false;
                txtdueTodate.Visible = false;
                chkDueDate.Checked = false;
            }
            if (row["DUETIMEYN"].ToString() == "1")
            {
                lblFrom.Visible = true;
                lblTo.Visible = true;
                chkDueTime.Checked = true;
                DropDueTimeTo.Visible = true;
                DropDueMinTo.Visible = true;
                DropDueTimeZoneTo.Visible = true;
                txtIndTo.Visible = true;
                DropDueTimeFrom.SelectedValue = row["DUE_TIMEFROM"].ToString();
                DropDueMinFrom.SelectedValue = row["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom.SelectedValue = row["TIME_ZONEFROM"].ToString();
                DropDueTimeTo.SelectedValue = row["DUE_TIMETO"].ToString();
                DropDueMinTo.SelectedValue = row["DUE_MINTO"].ToString();
                DropDueTimeZoneTo.SelectedValue = row["TIME_ZONEFROM"].ToString();
                txtIndFrom.Text = row["duetimefrom_ist"].ToString();
                txtIndTo.Text = row["duetimeto_ist"].ToString();
                lblIndFrom.Visible = true;
                lblIndTo.Visible = true;
            }
            else
            {
                lblFrom.Visible = false;
                lblTo.Visible = false;
                chkDueTime.Checked = false;
                DropDueTimeFrom.SelectedValue = row["DUE_TIMEFROM"].ToString();
                DropDueMinFrom.SelectedValue = row["DUE_MINFROM"].ToString();
                DropDueTimeZoneFrom.SelectedValue = row["TIME_ZONEFROM"].ToString();
                DropDueTimeTo.SelectedValue = "00";
                DropDueMinTo.SelectedValue = "00";
                DropDueTimeZoneTo.SelectedValue = row["TIME_ZONEFROM"].ToString();
                txtIndFrom.Text = row["duetimefrom_ist"].ToString();
                txtIndTo.Text = "";
                lblIndFrom.Visible = false;
                lblIndTo.Visible = false;
            }
            if (row["Rec_Date"].ToString() == "")
                txtRecDate.Text = "";
            else
                txtRecDate.Text = DateTime.Parse(row["Rec_Date"].ToString()).ToShortDateString();

            if (row["ytcyn"].ToString() == "1") chkYTC.Checked = true;
            if (row["DUE_DATEFrom"].ToString() == "")
                txtdueFromdate.Text = "";
            else
                txtdueFromdate.Text = DateTime.Parse(row["DUE_DATEFrom"].ToString()).ToShortDateString();
            if (row["DUE_DATETo"].ToString() == "")
                txtdueTodate.Text = "";
            else
                txtdueTodate.Text = DateTime.Parse(row["DUE_DATETo"].ToString()).ToShortDateString();
            DataSet ds1 = new DataSet();
            ds1 = nonLa.TaskSelected(sJobID);
            string Task = "";
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    Session["TaskValue"] = "";
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        lboxtask.Items[lboxtask.Items.IndexOf(lboxtask.Items.FindByValue(ds1.Tables[0].Rows[i]["task_id"].ToString()))].Selected = true;
                        if (Task == "")
                            Task = ds1.Tables[0].Rows[i]["task_ID"].ToString();
                        else
                            Task = Task + ',' + ds1.Tables[0].Rows[i]["task_ID"].ToString();
                    }
                    Session["TaskValue"] = Task.ToString();
                }
                chkFileType.Items.Clear();
                if (Task.ToString() == "1" || Task.ToString() == "2" || Task.ToString() == "6" || Task.ToString() == "1,2" || Task.ToString() == "1,6" || Task.ToString() == "1,2,6" || Task.ToString() == "2,6")
                {
                    ListItem li1 = new ListItem("Source", "0", true);
                    li1.Selected = true;
                    li1.Enabled = true;
                    chkFileType.Items.Add(li1);
                }
                else
                {
                    ListItem li1 = new ListItem("Source", "0", true);
                    ListItem li2 = new ListItem("Target", "1", true);
                    li1.Enabled = true;
                    li2.Enabled = true;
                    chkFileType.Items.Add(li1);
                    chkFileType.Items.Add(li2);
                }
                if (Task == "1" || Task == "1,2" || Task == "1,3" || Task == "1,4" || Task == "1,5" || Task == "1,6" || Task == "1,2,3")
                {
                    lblsource.Visible = true;
                    DropSource.Visible = true;
                    lboxformat.Visible = true;
                    lblformat.Visible = true;
                    TarRecDate.Visible = false;
                    txtRecDate.Visible = false;
                    img9.Visible = false;
                }
                else if (Task.ToString() == "3" || Task.ToString() == "6")
                {
                    lblsource.Visible = false;
                    DropSource.Visible = false;
                    lboxformat.Visible = true;
                    lblformat.Visible = true;
                    TarRecDate.Visible = false;
                    txtRecDate.Visible = false;
                    img9.Visible = false;
                }
                else
                {
                    lboxformat.Visible = false;
                    lblformat.Visible = false;
                    lblsource.Visible = false;
                    DropSource.Visible = false;
                    TarRecDate.Visible = false;
                    txtRecDate.Visible = false;
                    img9.Visible = false;
                }
            }
            DataSet tn = new DataSet();
            tn = nonLa.getTaskLangDetails(sJobID);
            gv_Soft.DataSource = tn;
            gv_Soft.DataBind();
            if (Task != "")
            {
                ds1 = nonLa.GetSelectedFormats(Task);
                lboxformat.DataTextField = ds1.Tables[0].Columns[1].ToString();
                lboxformat.DataValueField = ds1.Tables[0].Columns[0].ToString();
                lboxformat.DataSource = ds1;
                lboxformat.DataBind();
            }
            ds1 = nonLa.FormatSelected(sJobID);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        lboxformat.Items[lboxformat.Items.IndexOf(lboxformat.Items.FindByValue(ds1.Tables[0].Rows[i]["Format"].ToString()))].Selected = true;
                }
            }
            ds1 = nonLa.InputSelected(sJobID);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        linputfile.Items[linputfile.Items.IndexOf(linputfile.Items.FindByValue(ds1.Tables[0].Rows[i]["input"].ToString()))].Selected = true;
                }
            }
            ds1 = nonLa.GetselectedLang(sJobID);
            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        lboxlang.Items[lboxlang.Items.IndexOf(lboxlang.Items.FindByValue(ds1.Tables[0].Rows[i]["Lang_ID"].ToString()))].Selected = true;
                }
                lboxlangused.DataSource = ds1;
                lboxlangused.DataTextField = ds1.Tables[0].Columns[1].ToString();
                lboxlangused.DataValueField = ds1.Tables[0].Columns[0].ToString();
                lboxlangused.DataBind();
            }
            
            for (int j = 0; j < lboxtask.Items.Count; j++)
            {
                if (lboxtask.Items[j].Selected == true)
                {
                    ListItem it = new ListItem();
                    it.Value = lboxtask.Items[j].Value;
                    it.Text = lboxtask.Items[j].Text;
                    CheckBoxTask.Items.Add(it);
                }
            }
            
        }
    }
    protected void txtFile_TextChanged(object sender, EventArgs e)
    {
        //DataTable dTable = new DataTable();
        //dTable.Columns.Add("Files_ID");
        //for (int j = 1; j <= Convert.ToInt16(txtFile.Text); j++)
        //{
        //    dTable.Rows.Add(j);
        //}
        //DataSet Ds = new DataSet();
        //Ds.Tables.Add(dTable);
        //gv_FilePages.DataSource = Ds;
        //gv_FilePages.DataBind();

    }
    protected void gv_FilePages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //TextBox FileName = (TextBox)e.Row.FindControl("txt_Name");
            //TextBox Pages = (TextBox)e.Row.FindControl("txt_Pages");
            //Label FileNo = (Label)e.Row.FindControl("lbl_File");
            //DataSet empd = new DataSet();
            //empd = nonLa.GetSelectedFilePages(hfP_ID.Value, FileNo.Text);
            //if (empd != null)
            //{
            //    for (int i = 0; i < empd.Tables[0].Rows.Count; i++)
            //    {
            //        FileNo.Text = empd.Tables[0].Rows[0]["Files_ID"].ToString();
            //        FileName.Text = empd.Tables[0].Rows[0]["Files_Name"].ToString();
            //        Pages.Text = empd.Tables[0].Rows[0]["Pages"].ToString();
            //    }
            //}
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("NonLaunch.aspx?q=lnkLaunchdetails", true);
        this.showPanel(tabNonLaunch);
        //if(hfP_ID.Value=="")
        //{
        //    tempID.Text = DateTime.Now.ToString().GetHashCode().ToString("x"); 
        //}
    }
    protected void cmd_Save_Launch_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            if (validateScreen1())
            {
                if (validateScreen())
                {
                    int ytc;
                    if (chkYTC.Checked)
                        ytc = 1;
                    else
                        ytc = 0;
                    int duetime;
                    if (chkDueTime.Checked)
                        duetime = 1;
                    else
                        duetime = 0;
                    int dueDate;
                    if (chkDueDate.Checked)
                        dueDate = 1;
                    else
                        dueDate = 0;
                    string ddate = "";
                    if (txtdueFromdate.Text != "")
                        ddate = DateTime.Parse(txtdueFromdate.Text.Trim()).ToString("MM/dd/yyyy");
                    else
                        ddate = "";
                    string dTodate = "";
                    if (txtdueTodate.Text != "")
                        dTodate = DateTime.Parse(txtdueTodate.Text.Trim()).ToString("MM/dd/yyyy");
                    else
                        dTodate = "";
                    string recDate = "";
                    if (txtRecDate.Text != "")
                        recDate = DateTime.Parse(txtRecDate.Text.Trim()).ToString("MM/dd/yyyy");
                    else
                        recDate = "";
                    if (btnJobInfo.Text == "Save")
                    {
                        string inproc = "spInsertNL";
                        string[,] pname ={
                                        {"@PROJECTNAME",txtProjectTitle.Text },
                                        {"@CUST_ID",drpProjectcustomer.SelectedValue},{"@Location",DropLocation.SelectedValue},
                                        {"@PLATFORM",dropSwPlat.SelectedValue},{"@YTCYN",ytc.ToString()},
                                        {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                                        {"@Due_Timefrom",DropDueTimeFrom.SelectedValue},{"@Due_MINFROM",DropDueMinFrom.SelectedValue},
                                        {"@TIME_ZoneFROM",DropDueTimeZoneFrom.SelectedValue},{"@PROJECTEDITOR",txtProjectEditor.Text},
                                        {"@Due_TimeTO",DropDueTimeTo.SelectedValue},{"@Due_MINTO",DropDueMinTo.SelectedValue},
                                        {"@TIME_ZoneTO",DropDueTimeZoneTo.SelectedValue},{"@CREATED_BY",Session["employeeid"].ToString()},
                                        {"@DUETIMEFROM_IST",txtIndFrom.Text},{"@DUETIMETO_IST",txtIndTo.Text},
                                        {"@SourceType",DropSource.SelectedValue},{"@DUETIMEYN",duetime.ToString()},
                                        {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},{"@LaunchID",txtLaunchID.Text},
                                        {"@PType",drpType.SelectedValue},{"@File",""},{"@RecDate",recDate.ToString()}
                                     };
                    int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    btnJobInfo.Text = "Update";
                    
                    DataSet ds = new DataSet();
                    ds = nonLa.GetNL_Launch("'" + txtProjectTitle.Text + "'");
                    string pn = ds.Tables[0].Rows[0]["NL_ID"].ToString();

                    string taskitem = "";
                    for (int j = 0; j < lboxtask.Items.Count; j++)
                    {
                        if (lboxtask.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxtask.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxtask.Items[j].Value;
                        }
                    }
                    string taskproc = "spInsert_NL_Task";
                    string[,] tname = { { "@NL_ID", pn }, { "@taskname", taskitem } };
                    int task12 = this.oLaunch.ExcSP(taskproc, tname, CommandType.StoredProcedure);

                    string formatitem = "";
                    for (int j = 0; j < lboxformat.Items.Count; j++)
                    {
                        if (lboxformat.Items[j].Selected == true)
                        {
                            if (formatitem == "")
                                formatitem = lboxformat.Items[j].Value;
                            else
                                formatitem = formatitem + '-' + lboxformat.Items[j].Value;
                        }
                    }
                    string formatproc = "spInsert_NL_Format";
                    string[,] forname = { { "@NL_ID", pn }, { "@format_id", formatitem } };

                    int format12 = this.oLaunch.ExcSP(formatproc, forname, CommandType.StoredProcedure);
                    string inputitem = "";
                    for (int j = 0; j < linputfile.Items.Count; j++)
                    {
                        if (linputfile.Items[j].Selected == true)
                        {
                            if (inputitem == "")
                                inputitem = linputfile.Items[j].Value;
                            else
                                inputitem = inputitem + '-' + linputfile.Items[j].Value;
                        }
                    }
                    string inputproc = "spInsert_NL_Input";
                    string[,] inname = { { "@NL_ID", pn }, { "@inputid", inputitem } };
                    int input12 = this.oLaunch.ExcSP(inputproc, inname, CommandType.StoredProcedure);

                    nonLa.UpdateUsedtLang(pn,tempID.Text);
                    tempID.Text = "";
                    hfP_ID.Value = pn;

                    //ArrayList a = new ArrayList();
                    //Hashtable s = new Hashtable();
                    //string ver1 = "";
                    //foreach (GridViewRow grs in gv_Soft.Rows)
                    //{
                    //    ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
                    //    ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
                    //    HiddenField task = (HiddenField)grs.FindControl("hf_taskID");
                    //    HiddenField Lang = (HiddenField)grs.FindControl("hf_LangID");
                    //    TextBox TarDate = (TextBox)grs.FindControl("txtTargetDate");
                    //    string soft;
                    //    int r = 0;
                    //    for (int j = 0; j < lboxSoft.Items.Count; j++)
                    //    {
                    //        if (lboxSoft.Items[j].Selected == true)
                    //        {
                    //            soft = lboxSoft.Items[j].Value;
                    //            r = r + 1;
                    //            if (r == 1)
                    //            {
                    //                for (int d = 0; d < lboxVer.Items.Count; d++)
                    //                {
                    //                    if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                        break;
                    //                    }
                    //                    else if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                    }

                    //                }
                    //            }
                    //            else
                    //            {
                    //                for (int d = 0; d < lboxVer.Items.Count; d++)
                    //                {
                    //                    if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                    }
                    //                }
                    //            }
                    //            if (TarDate.Text == "")
                    //                TarDate.Text = null;
                    //            s = new Hashtable();
                    //            s.Add("NL_ID", pn);
                    //            s.Add("Task_ID", task.Value);
                    //            s.Add("Lang_ID", Lang.Value);
                    //            s.Add("TarDate", TarDate.Text);
                    //            s.Add("Software_id", soft);
                    //            s.Add("Version_id", ver1);
                    //            s.Add("Status", "1");
                    //            a.Add(s);
                    //        }
                    //    }
                    //}
                    //nonLa.Insert_Software(a);

                    //ArrayList a1 = new ArrayList();
                    //Hashtable s1 = new Hashtable();
                    //foreach (GridViewRow grs in gv_FilePages.Rows)
                    //{
                    //    TextBox Pages = (TextBox)grs.FindControl("txt_Pages");
                    //    TextBox FileName = (TextBox)grs.FindControl("txt_Name");
                    //    Label FileNo = (Label)grs.FindControl("lbl_File");
                    //    s1 = new Hashtable();
                    //    s1.Add("NL_ID", pn);
                    //    s1.Add("FileNo", FileNo.Text);
                    //    s1.Add("FileName", FileName.Text);
                    //    s1.Add("Pages", Pages.Text);
                    //    a1.Add(s1);
                    //}
                    //nonLa.Insert_FilePages(a1);
                    //nonLa.UpdatePages(pn);
                    if (val == 0)
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Failed!.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Inserted Successfully!.');</script>");
                }
                else
                {
                    string inproc = "spUpdateNL";
                    string[,] pname ={
                                        {"@PROJECTNAME",txtProjectTitle.Text },{"@NL_ID",hfP_ID.Value},
                                        {"@CUST_ID",drpProjectcustomer.SelectedValue},{"@Location",DropLocation.SelectedValue},
                                        {"@PLATFORM",dropSwPlat.SelectedValue},{"@YTCYN",ytc.ToString()},
                                        {"@DUE_DATEFROM",ddate},{"@DUE_DATETo",dTodate},
                                        {"@Due_Timefrom",DropDueTimeFrom.SelectedValue},{"@Due_MINFROM",DropDueMinFrom.SelectedValue},
                                        {"@TIME_ZoneFROM",DropDueTimeZoneFrom.SelectedValue},{"@PROJECTEDITOR",txtProjectEditor.Text},
                                        {"@Due_TimeTO",DropDueTimeTo.SelectedValue},{"@Due_MINTO",DropDueMinTo.SelectedValue},
                                        {"@TIME_ZoneTO",DropDueTimeZoneTo.SelectedValue},{"@MODIFIED_BY",""},
                                        {"@DUETIMEFROM_IST",txtIndFrom.Text},{"@DUETIMETO_IST",txtIndTo.Text},
                                        {"@SourceType",DropSource.SelectedValue},{"@DUETIMEYN",duetime.ToString()},
                                        {"@DUEDateYN",dueDate.ToString()},{"@IsExist","OUTPUT"},{"@LaunchID",txtLaunchID.Text},
                                        {"@PType",drpType.SelectedValue},{"@File",""},{"@RecDate",recDate.ToString()}
                                     };
                    int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);

                    string taskitem = "";
                    for (int j = 0; j < lboxtask.Items.Count; j++)
                    {
                        if (lboxtask.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxtask.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxtask.Items[j].Value;
                        }
                    }
                    string taskproc = "spInsert_NL_Task";
                    string[,] tname = { { "@NL_ID", hfP_ID.Value }, { "@taskname", taskitem } };
                    int task12 = this.oLaunch.ExcSP(taskproc, tname, CommandType.StoredProcedure);

                    string formatitem = "";
                    for (int j = 0; j < lboxformat.Items.Count; j++)
                    {
                        if (lboxformat.Items[j].Selected == true)
                        {
                            if (formatitem == "")
                                formatitem = lboxformat.Items[j].Value;
                            else
                                formatitem = formatitem + '-' + lboxformat.Items[j].Value;
                        }
                    }
                    string formatproc = "spInsert_NL_Format";
                    string[,] forname = { { "@NL_ID", hfP_ID.Value }, { "@format_id", formatitem } };

                    int format12 = this.oLaunch.ExcSP(formatproc, forname, CommandType.StoredProcedure);
                    string inputitem = "";
                    for (int j = 0; j < linputfile.Items.Count; j++)
                    {
                        if (linputfile.Items[j].Selected == true)
                        {
                            if (inputitem == "")
                                inputitem = linputfile.Items[j].Value;
                            else
                                inputitem = inputitem + '-' + linputfile.Items[j].Value;
                        }
                    }
                    string inputproc = "spInsert_NL_Input";
                    string[,] inname = { { "@NL_ID", hfP_ID.Value }, { "@inputid", inputitem } };
                    int input12 = this.oLaunch.ExcSP(inputproc, inname, CommandType.StoredProcedure);

                    //nonLa.UpdateSoftStatus(hfP_ID.Value);
                    //ArrayList a = new ArrayList();
                    //Hashtable s = new Hashtable();
                    //string ver1 = "";
                    //foreach (GridViewRow grs in gv_Soft.Rows)
                    //{
                    //    ListBox lboxSoft = (ListBox)grs.FindControl("lboxSoft");
                    //    ListBox lboxVer = (ListBox)grs.FindControl("lboxVer");
                    //    HiddenField task = (HiddenField)grs.FindControl("hf_taskID");
                    //    HiddenField Lang = (HiddenField)grs.FindControl("hf_LangID");
                    //    TextBox TarDate = (TextBox)grs.FindControl("txtTargetDate");
                    //    string soft;
                    //    int r = 0;
                    //    for (int j = 0; j < lboxSoft.Items.Count; j++)
                    //    {
                    //        if (lboxSoft.Items[j].Selected == true)
                    //        {
                    //            soft = lboxSoft.Items[j].Value;
                    //            r = r + 1;
                    //            if (r == 1)
                    //            {
                    //                for (int d = 0; d < lboxVer.Items.Count; d++)
                    //                {
                    //                    if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                        break;
                    //                    }
                    //                    else if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                    }

                    //                }
                    //            }
                    //            else
                    //            {
                    //                for (int d = 0; d < lboxVer.Items.Count; d++)
                    //                {
                    //                    if (lboxVer.Items[d].Selected == true)
                    //                    {
                    //                        ver1 = lboxVer.Items[d].Value;
                    //                    }
                    //                }
                    //            }
                    //            if (TarDate.Text == "")
                    //                TarDate.Text = null;
                    //            s = new Hashtable();
                    //            s.Add("NL_ID", hfP_ID.Value);
                    //            s.Add("Task_ID", task.Value);
                    //            s.Add("Lang_ID", Lang.Value);
                    //            s.Add("TarDate", TarDate.Text);
                    //            s.Add("Software_id", soft);
                    //            s.Add("Version_id", ver1);
                    //            s.Add("Status", "1");
                    //            a.Add(s);
                    //        }
                    //    }
                    //}
                    //nonLa.Insert_Software(a);
                    //nonLa.DeleteSoft(hfP_ID.Value);

                    //ArrayList a1 = new ArrayList();
                    //Hashtable s1 = new Hashtable();
                    //foreach (GridViewRow grs in gv_FilePages.Rows)
                    //{
                    //    TextBox Pages = (TextBox)grs.FindControl("txt_Pages");
                    //    TextBox FileName = (TextBox)grs.FindControl("txt_Name");
                    //    Label FileNo = (Label)grs.FindControl("lbl_File");
                    //    s1 = new Hashtable();
                    //    s1.Add("NL_ID", hfP_ID.Value);
                    //    s1.Add("FileNo", FileNo.Text);
                    //    s1.Add("FileName", FileName.Text);
                    //    s1.Add("Pages", Pages.Text);
                    //    a1.Add(s1);
                    //}
                    //nonLa.DeleteFilePages(hfP_ID.Value);
                    //nonLa.Insert_FilePages(a1);
                    //nonLa.UpdatePages(hfP_ID.Value);
                    if (val == 1)
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Updated Successfully!.');</script>");
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Updation Failed!.');</script>");
					}
                }
            }
        }
        catch (Exception ex)
        { }
    }
    private bool validateScreen1()
    {
        if (txtProjectTitle.Text.Trim() != "")
        {
            string text1 = txtProjectTitle.Text.Trim();
            int textLength1 = text1.Length;
            if (textLength1 > 9)
            {
                string text = txtProjectTitle.Text.Trim().Substring(0, 10);
                int textLength = text.Length;
                if (text.ToString().Substring(0, 2) == "CC" || text.ToString().Substring(0, 2) == "CA")
                {
                    for (int i = 0; i < textLength; )
                    {
                        if (i <= 1)
                        {
                            if (!char.IsLetter(text[i]))
                            {
                                if (txtLaunchID.Text != "")
                                {
                                    DataSet ds = nonLa.GetJobID(txtLaunchID.Text.Trim());
                                    if (ds != null)
                                    {
                                        if (txtLaunchID.Text.Trim() != ds.Tables[0].Rows[0]["JobID"].ToString())
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                                    return false;
                                }
                                if (drpType.SelectedValue == "1" || drpType.SelectedValue == "2")
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select Correct Job Type.');</script>");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (!char.IsNumber(text[i]))
                            {
                                if (txtLaunchID.Text != "")
                                {
                                    DataSet ds = nonLa.GetJobID(txtLaunchID.Text.Trim());
                                    if (ds != null)
                                    {
                                        if (txtLaunchID.Text.Trim() != ds.Tables[0].Rows[0]["JobID"].ToString())
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                                    return false;
                                }
                                if (drpType.SelectedValue == "1" || drpType.SelectedValue == "2")
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select Correct Job Type.');</script>");
                                    return false;
                                }
                            }
                        }
                        i++;
                    }
                }
                else
                {
                    if (txtLaunchID.Text != "")
                    {
                        DataSet ds = nonLa.GetJobID(txtLaunchID.Text.Trim());
                        if (ds != null)
                        {
                            if (txtLaunchID.Text.Trim() != ds.Tables[0].Rows[0]["JobID"].ToString())
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                                return false;
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                        return false;
                    }
                    if(drpType.SelectedValue=="1"|| drpType.SelectedValue=="2")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select Correct Job Type.');</script>");
                        return false;
                    }
                }
            }
            else
            {
                if (txtLaunchID.Text != "")
                {
                    DataSet ds = nonLa.GetJobID(txtLaunchID.Text.Trim());
                    if (ds != null)
                    {
                        if (txtLaunchID.Text.Trim() != ds.Tables[0].Rows[0]["JobID"].ToString())
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                            return false;
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please enter relevant Launch ID for this Projects.');</script>");
                    return false;
                }
                if (drpType.SelectedValue == "1" || drpType.SelectedValue == "2")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select Correct Job Type.');</script>");
                    return false;
                }
            }
            //if (drpType.SelectedValue == "3")
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select Correct Job Type.');</script>");
            //    return false;
            //}
        }
        return true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.GetJobDetails(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
        GvNL.DataSource = ds;
        GvNL.DataBind();
        if(ds!=null)
        {
            dtable4 = ds.Tables[0].Copy();
        }
        this.showPanel(tabGeneral);
    }
    protected void GvNL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvNLID")).Value.Trim() + "\",\"" + ((Label)e.Row.FindControl("lblgvPtitle")).Text.Trim() + "\");";


            HiddenField NL_ID = e.Row.FindControl("hfgvNLID") as HiddenField;
            DataSet ds = new DataSet();
            ds = nonLa.getJobDetailsByID(NL_ID.Value);
            DropDownList status = e.Row.FindControl("DropStatus") as DropDownList;
            TextBox JobNo = e.Row.FindControl("lblJobNo") as TextBox;
            JobNo.Text = ds.Tables[0].Rows[0]["JobNo"].ToString();
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() != "")
            {
                status.Items.Add(new ListItem("--Select--", "0"));
                status.Items.Add(new ListItem("P", "P"));
                status.Items.Add(new ListItem("C", "C"));
                status.Items.Add(new ListItem("WIP", "WIP"));
                status.Items.Add(new ListItem("Del", "Del"));
                status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["Delivery_Status"].ToString()))].Selected = true;
            }
            else
            {
                status.Items.Add(new ListItem("--Select--", "0"));
                status.Items.Add(new ListItem("P", "P"));
                status.Items.Add(new ListItem("C", "C"));
                status.Items.Add(new ListItem("WIP", "WIP"));
                status.Items.Add(new ListItem("Del", "Del"));
            }
            if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "P")
                status.CssClass = "gridP";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "C")
                status.CssClass = "gridC";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del")
                status.CssClass = "gridDel";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "WIP")
                status.CssClass = "gridWIP";

        }
    }
    protected void GvNL_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            string NL_ID = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            DropDownList Status = (DropDownList)row.Cells[9].FindControl("DropStatus");
            nonLa.UpdateStatus(NL_ID, Status.SelectedValue);
            btnSearch_Click(sender, e);
        }
    }
    protected void gvFileInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void gvFileInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void Edit(object sender, EventArgs e)
    {
        Session["NLFilePages"] = "";
        lblResult.Text = "";
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_LP_ID");
            HiddenField hid_NTLS_ID = (HiddenField)row.Cells[0].FindControl("hid_NTLS_ID");
            HiddenField hid_Task_ID = (HiddenField)row.Cells[1].FindControl("hid_Task_ID");
            HiddenField hid_Lang_ID = (HiddenField)row.Cells[2].FindControl("hid_Lang_ID");
            HiddenField hid_Soft_ID = (HiddenField)row.Cells[3].FindControl("hid_Soft_ID");
            TextBox Files = (TextBox)row.Cells[4].FindControl("lblgvFiles");
            NL_ID.Text = sJobID.Value;
            NTLS_ID.Text = hid_NTLS_ID.Value;
            Task_ID.Text = hid_Task_ID.Value;
            Soft_ID.Text = hid_Soft_ID.Value;
            Lang_ID.Text = hid_Lang_ID.Value;
            txtFiles.Text = Files.Text;
            int File = 0;
            if (Files.Text != "")
                File = Convert.ToInt16(Files.Text);
            DataSet dss = new DataSet();
            DataTable dTable = new DataTable();
            dTable.Columns.Add("Files_ID");
            dTable.Columns.Add("LP_ID");
            dTable.Columns.Add("NTLS_ID");
            dTable.Columns.Add("TaskName");
            dTable.Columns.Add("Task_ID");
            dTable.Columns.Add("Lang_name");
            dTable.Columns.Add("Lang_ID");
            dTable.Columns.Add("Soft_Name");
            dTable.Columns.Add("Soft_ID");
            dTable.Columns.Add("Files_Name");
            dTable.Columns.Add("Pages");

            dss = nonLa.GetFilePages(hid_NTLS_ID.Value);
            if (dss == null)
            {
                dss = nonLa.getLPNTLS(hid_NTLS_ID.Value);

                for (int j = 1; j <= File; j++)
                {
                    dTable.Rows.Add(j, dss.Tables[0].Rows[0]["NL_ID"].ToString(), dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                        dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(), "", 0);
                }
            }
            else
            {
                dss = nonLa.getLPInsertedNTLS(hid_NTLS_ID.Value);
                if (dss != null)
                {
                    int k = 1;
                    for (int j = 0; j <= (File - 1); j++)
                    {
                        if (k <= dss.Tables[0].Rows.Count)
                        {
                            dTable.Rows.Add(dss.Tables[0].Rows[j]["Files_ID"].ToString(), dss.Tables[0].Rows[j]["NL_ID"].ToString(),
                                dss.Tables[0].Rows[j]["NTLS_ID"].ToString(),
                                dss.Tables[0].Rows[j]["TaskName"].ToString(), dss.Tables[0].Rows[j]["Task_ID"].ToString(),
                                dss.Tables[0].Rows[j]["Lang_name"].ToString(), dss.Tables[0].Rows[j]["Lang_ID"].ToString(),
                                dss.Tables[0].Rows[j]["Soft_Name"].ToString(), dss.Tables[0].Rows[j]["Soft_ID"].ToString(),
                                dss.Tables[0].Rows[j]["Files_Name"].ToString(), dss.Tables[0].Rows[j]["Pages"].ToString());
                        }
                        else
                        {
                            dTable.Rows.Add(k, dss.Tables[0].Rows[0]["NL_ID"].ToString(),
                                dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                                dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                                dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                                dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(),
                                "", 0);
                        }
                        k++;
                    }
                }
            }
            DataSet Ds = new DataSet();
            Ds.Tables.Add(dTable);
            gv_FilePages.DataSource = Ds;
            gv_FilePages.DataBind();
            // popup.Show();
            Session["NLFilePages"] = Ds;

            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchNLFilePages.aspx?NL_ID=" + HttpUtility.UrlEncode(NL_ID.Text) + "&NTLS_ID=" + HttpUtility.UrlEncode(NTLS_ID.Text)
            + "&Task_ID=" + HttpUtility.UrlEncode(Task_ID.Text) + "&Soft_ID=" + HttpUtility.UrlEncode(Soft_ID.Text) + "&Lang_ID=" + HttpUtility.UrlEncode(Lang_ID.Text) + "&File=" + HttpUtility.UrlEncode(File.ToString()) + "&FileConv=" + HttpUtility.UrlEncode(DropNewNameConv.SelectedValue)
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
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
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable4 != null && dtable4.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='15' align='center'><h4>Non Launch Report</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>Platform</b></td><td bgcolor='silver'><b>Pages</b></td><td bgcolor='silver'><b>Files</b></td><td bgcolor='silver'><b>Target Recived Date</b></td><td bgcolor='silver'><b>Committed Date From</b></td><td bgcolor='silver'><b>Committed Date To</b></td><td bgcolor='silver'><b>Committed Time From</b></td><td bgcolor='silver'><b>Committed Time To</b></td><td bgcolor='silver'><b>Committed Time_IST From</b></td><td bgcolor='silver'><b>Committed Time_IST To</b></td></tr>");
            foreach (DataRow r in dtable4.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Cust_name"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["ProjectEditor"] + "</td>");
                sbData.Append("<td>" + r["Platform"] + "</td>");
                sbData.Append("<td>" + r["Pages_count"] + "</td>");
                sbData.Append("<td>" + r["File_Count"] + "</td>");
                sbData.Append("<td>" + r["CREATED_DATE"] + "</td>");
                sbData.Append("<td>" + r["DUE_DATEFROM"] + "</td>");
                sbData.Append("<td>" + r["DUE_DATETO"] + "</td>");
                sbData.Append("<td>" + r["DueTimeFrom"] + "</td>");
                sbData.Append("<td>" + r["DueTimeTo"] + "</td>");
                sbData.Append("<td>" + r["DUETIMEFROM_IST"] + "</td>");
                sbData.Append("<td>" + r["DUETIMETO_IST"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Non_Launch_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();

        }
    }
    protected void btnlangadd_Click(object sender, EventArgs e)
    {
        string id = "";
        try
        {
            DataSet dss = new DataSet();
            if (hfP_ID.Value != "")
                id = hfP_ID.Value.Trim();
            else if (tempID.Text != "")
                id = tempID.Text;
            if (id != "")
            {
                int countLang = 0;
                foreach (ListItem li in lboxlang.Items)
                    if (li.Selected)
                        countLang++;

                int countTask = 0;
                foreach (ListItem li in lboxtask.Items)
                    if (li.Selected)
                        countTask++;

                int countChkTask = 0;
                foreach (ListItem li in CheckBoxTask.Items)
                    if (li.Selected)
                        countChkTask++;

                int countChkTarFiles = 0;
                foreach (ListItem li in chkFileType.Items)
                    if (li.Selected)
                        countChkTarFiles++;

                int countSW = 0;
                foreach (ListItem li in lboxSW.Items)
                    if (li.Selected)
                        countSW++;

                int countSWVer = 0;
                foreach (ListItem li in lboxSWVer.Items)
                    if (li.Selected)
                        countSWVer++;

                if (countTask > 0)
                {
                    if (countChkTask > 0 && countLang > 0 && countChkTarFiles > 0 && countSW > 0 && countSWVer > 0)
                    {
                        for (int kk = 0; kk < CheckBoxTask.Items.Count; kk++)
                        {
                            if (CheckBoxTask.Items[kk].Selected == true)
                            {
                                for (int k = 0; k < lboxlang.Items.Count; k++)
                                {
                                    if (lboxlang.Items[k].Selected == true)
                                    {
                                        for (int j = 0; j < lboxSW.Items.Count; j++)
                                        {
                                            if (lboxSW.Items[j].Selected == true)
                                            {
                                                for (int t = 0; t < lboxSWVer.Items.Count; t++)
                                                {
                                                    if (lboxSWVer.Items[t].Selected == true)
                                                    {
                                                        DataSet vs = new DataSet();
                                                        vs = nonLa.GetSoftVer(lboxSW.Items[j].Value, lboxSWVer.Items[t].Value);
                                                        if (vs != null)
                                                        {
                                                            for (int w = 0; w < chkFileType.Items.Count; w++)
                                                            {
                                                                if (chkFileType.Items[w].Selected == true)
                                                                {
                                                                    dss = nonLa.GetInsertedLang(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString());
                                                                    if (dss != null)
                                                                    {
                                                                        if (CheckBoxTask.Items[kk].Value == "1" || CheckBoxTask.Items[kk].Value == "2" || CheckBoxTask.Items[kk].Value == "6")
                                                                        {
                                                                            DataSet dt = new DataSet();
                                                                            dt = nonLa.GetTarSoft(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value);
                                                                            if (dt == null)
                                                                            {
                                                                                nonLa.InsertTarSoft(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            nonLa.InsertSoft(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                        }
                                                                        //ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Selected " + lboxlang.SelectedItem + " already inserted for " + CheckBoxTask.Items[kk].Text + " Task');</script>");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CheckBoxTask.Items[kk].Value == "1" || CheckBoxTask.Items[kk].Value == "2" || CheckBoxTask.Items[kk].Value == "6")
                                                                        {
                                                                            nonLa.InsertLang(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString());
                                                                            DataSet dt = new DataSet();
                                                                            dt = nonLa.GetTarSoft(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value);
                                                                            if (dt == null)
                                                                            {
                                                                                nonLa.InsertTarSoft(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            nonLa.InsertLang(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString());
                                                                            nonLa.InsertSoft(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString(), lboxSW.Items[j].Value, lboxSWVer.Items[t].Value, chkFileType.Items[w].Value);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (countLang == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Language');</script>");
                        else if (countChkTask == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Task for this Language');</script>");
                        else if (countSW == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Software');</script>");
                        else if (countSWVer == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Software Version');</script>");
                        else if (countChkTarFiles == 0)
                            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected File Type');</script>");
                    }
                    dss = nonLa.GetselectedLang(id.ToString());
                    if (dss != null)
                    {
                        lboxlangused.DataSource = dss;
                        lboxlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
                        lboxlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
                        lboxlangused.DataBind();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Select Task');</script>");
                }
                lboxlang.ClearSelection();
                //lboxNewtask.ClearSelection();
                CheckBoxTask.ClearSelection();
                lboxSW.ClearSelection();
                lboxSWVer.ClearSelection();
                if (Session["TaskValue"].ToString() == "1" || Session["TaskValue"].ToString() == "2" ||
                    Session["TaskValue"].ToString() == "6" || Session["TaskValue"].ToString() == "1,2" ||
                    Session["TaskValue"].ToString() == "1,6" || Session["TaskValue"].ToString() == "1,2,6" ||
                    Session["TaskValue"].ToString() == "2,6")
                {
                    //chkFileType.ClearSelection();
                }
                else
                {
                    chkFileType.ClearSelection();
                }

                dss = nonLa.getTaskLangDetails(id.ToString());
                gv_Soft.DataSource = dss;
                gv_Soft.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
        }
        //string id="";
        //try
        //{
        //    DataSet dss = new DataSet();
        //    if (hfP_ID.Value != "")
        //        id = hfP_ID.Value.Trim();
        //    else if (tempID.Text != "")
        //        id = tempID.Text;
        //    if (id != "")
        //    {
        //        int countLang = 0;
        //        foreach (ListItem li in lboxlang.Items)
        //            if (li.Selected)
        //                countLang++;

        //        int countTask = 0;
        //        foreach (ListItem li in lboxtask.Items)
        //            if (li.Selected)
        //                countTask++;

        //        int countChkTask = 0;
        //        foreach (ListItem li in CheckBoxTask.Items)
        //            if (li.Selected)
        //                countChkTask++;

        //        if (countTask > 0)
        //        {
        //            if (countChkTask > 0 && countLang > 0)
        //            {
        //                for (int kk = 0; kk < CheckBoxTask.Items.Count; kk++)
        //                {
        //                    if (CheckBoxTask.Items[kk].Selected == true)
        //                    {
        //                        for (int k = 0; k < lboxlang.Items.Count; k++)
        //                        {
        //                            if (lboxlang.Items[k].Selected == true)
        //                            {
        //                                dss = nonLa.GetInsertedLang(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString());
        //                                if (dss != null)
        //                                {
        //                                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Selected " + lboxlang.SelectedItem + " already inserted for " + CheckBoxTask.Items[kk].Text + " Task');</script>");
        //                                }
        //                                else
        //                                {
        //                                    nonLa.InsertLang(CheckBoxTask.Items[kk].Value, lboxlang.Items[k].Value, id.ToString());
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (countLang == 0)
        //                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Language');</script>");
        //                else
        //                    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Selected Task for this Language');</script>");
        //            }
        //            dss = nonLa.GetselectedLang(id.ToString());
        //            if (dss != null)
        //            {
        //                lboxlangused.DataSource = dss;
        //                lboxlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
        //                lboxlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
        //                lboxlangused.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please Select Task');</script>");
        //        }
        //        dss = nonLa.getTaskLangDetails(id.ToString());
        //        gv_Soft.DataSource = dss;
        //        gv_Soft.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
        //finally
        //{
        //}
       
    }
    protected void btnlangdel_Click(object sender, EventArgs e)
    {
        string id="";
        try
        {
            if (hfP_ID.Value != "")
                id = hfP_ID.Value.Trim();
            else if (tempID.Text != "")
                id = tempID.Text;
            if (id != "")
            {
                for (int j = 0; j < lboxlangused.Items.Count; j++)
                {
                    if (lboxlangused.Items[j].Selected == true)
                    {
                        nonLa.DeleteUsedtLang(lboxlangused.Items[j].Value,id.ToString());
                    }
                }
                DataSet dss = new DataSet();
                dss = nonLa.GetselectedLang(id.ToString());
                lboxlangused.Items.Clear();
                if (dss != null)
                {
                    lboxlangused.DataSource = dss;
                    lboxlangused.DataTextField = dss.Tables[0].Columns[1].ToString();
                    lboxlangused.DataValueField = dss.Tables[0].Columns[0].ToString();
                    lboxlangused.DataBind();
                }
                lboxSWVer.Items.Clear();
                dss = nonLa.getTaskLangDetails(id.ToString());
                gv_Soft.DataSource = dss;
                gv_Soft.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
        }
    }
    protected void lnkFiledetails_Click(object sender, EventArgs e)
    {
        if(hfP_ID.Value!="")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getTaskLangDetailsByLPID(hfP_ID.Value);
            DataSet ds1 = new DataSet();
            ds1 = nonLa.getTarTaskLangDetailsByLPID(hfP_ID.Value);
            if (hfP_ID.Value != "")
            {
                gvFileInfo.DataSource = ds;
                gvFileInfo.DataBind();
                gvTarFileInfo.DataSource = ds1;
                gvTarFileInfo.DataBind();
            }
        }
        this.showPanel(tabFileDetails);
    }
    protected void Save(object sender, EventArgs e)
    {
        nonLa.UpdateFilePagesStatus(NL_ID.Text, NTLS_ID.Text);
        foreach (GridViewRow grs in gv_FilePages.Rows)
        {
            HiddenField sJobID = (HiddenField)grs.FindControl("hid_NL_ID");
            HiddenField hid_NTLS_ID = (HiddenField)grs.FindControl("hid_NTLS_ID");
            HiddenField hid_Task_ID = (HiddenField)grs.FindControl("hid_Task_ID");
            HiddenField hid_Lang_ID = (HiddenField)grs.FindControl("hid_Lang_ID");
            HiddenField hid_Soft_ID = (HiddenField)grs.FindControl("hid_Soft_ID");
            TextBox FileName = (TextBox)grs.FindControl("txt_Name");
            TextBox Pages = (TextBox)grs.FindControl("txt_Pages");
            Label FileID = (Label)grs.FindControl("lbl_File");
            string inproc = "spInsertFilePages";
            string[,] pname ={
                                {"@NL_ID",sJobID.Value },{"@NTLS_ID",hid_NTLS_ID.Value},
                                {"@Task_ID",hid_Task_ID.Value},{"@Lang_ID",hid_Lang_ID.Value},
                                {"@Soft_ID",hid_Soft_ID.Value},{"@Files_Name",FileName.Text},
                                {"@Pages",Pages.Text},{"@Files_ID",FileID.Text},{"@ISExists","Output"}
                             };
            int val = this.oLaunch.ExcSP(inproc, pname, CommandType.StoredProcedure);
            if (val == 1)
                lblResult.Text = "Inserted Successfully";
            else
                lblResult.Text = "Inserted Failed!.";
        }
        //nonLa.UpdateFile_Count(NTLS_ID.Text,NL_ID.Text,txtFiles.Text);
        nonLa.DeleteFilePagesStatus(NL_ID.Text, NTLS_ID.Text);
        popup.Hide();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        int count = 0;
        if (fileBrowse.HasFile)
        {
            string connectionString = "";
            string fileName = Path.GetFileName(fileBrowse.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileBrowse.PostedFile.FileName);
            string fileLocation = @"\\192.9.201.222\Mail\LaunchFiles\" + Path.GetFileNameWithoutExtension(fileName) + Convert.ToString(DateTime.Now).Replace("/", "_").Replace(":", "_") + "." + fileExtension;
            fileBrowse.SaveAs(fileLocation);
            if (fileExtension == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=NO\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmds = new OleDbCommand();
            cmds.CommandType = System.Data.CommandType.Text;
            cmds.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmds);
            DataTable dtExcelRecords = new DataTable();
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName1 = string.Empty;
            getExcelSheetName1 = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            cmds.CommandText = "SELECT * FROM [" + getExcelSheetName1 + "]";
            dAdapter.SelectCommand = cmds;
            dAdapter.Fill(dtExcelRecords);
            for (int i = 0; i < dtExcelRecords.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToInt32(txtFiles.Text) >= Convert.ToInt32(dtExcelRecords.Rows[i][0]))
                    {
                        scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQL"].ConnectionString);
                        scon.Open();
                        SqlCommand cmd = new SqlCommand("spLPUploadFilePages", scon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Files_ID", SqlDbType.Int).Value = dtExcelRecords.Rows[i][0];
                        cmd.Parameters.Add("@Files_Name", SqlDbType.VarChar).Value = dtExcelRecords.Rows[i][1];
                        cmd.Parameters.Add("@Pages", SqlDbType.Int).Value = dtExcelRecords.Rows[i][2];
                        cmd.Parameters.Add("@LP_ID", SqlDbType.Int).Value = NL_ID.Text;
                        cmd.Parameters.Add("@NTLS_ID", SqlDbType.Int).Value = NTLS_ID.Text;
                        cmd.Parameters.Add("@Task_ID", SqlDbType.Int).Value = Task_ID.Text;
                        cmd.Parameters.Add("@Lang_ID", SqlDbType.Int).Value = Lang_ID.Text;
                        cmd.Parameters.Add("@Soft_ID", SqlDbType.Int).Value = Soft_ID.Text;
                        count += cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
                finally
                {
                    scon.Close();
                }
            }
        }
        DataSet ds = new DataSet();
        ds = nonLa.spGetUploadFilePages(Task_ID.Text, Lang_ID.Text, Soft_ID.Text, NL_ID.Text, NTLS_ID.Text);
        gv_FilePages.DataSource = ds;
        gv_FilePages.DataBind();
        popup.Show();
    }
    protected void lnkJobTracking_Click(object sender, EventArgs e)
    {
        if(hfP_ID.Value!="")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(hfP_ID.Value);
            if(ds!=null)
            {
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
            }
        }
        this.showPanel(tabJobTracking);
    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable4 != null && dtable4.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='19' align='center'><h4>Non Launch OverAll Report</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Location</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Software</b></td><td bgcolor='silver'><b>Platform</b></td><td bgcolor='silver'><b>Target Recived Date</b></td><td bgcolor='silver'><b>Committed Date From</b></td><td bgcolor='silver'><b>Committed Date To</b></td><td bgcolor='silver'><b>Committed Time From</b></td><td bgcolor='silver'><b>Committed Time To</b></td><td bgcolor='silver'><b>Committed Time_IST From</b></td><td bgcolor='silver'><b>Committed Time_IST To</b></td><td bgcolor='silver'><b>No.of Pages</b></td><td bgcolor='silver'><b>No.of Files</b></td><td bgcolor='silver'><b>Category</b></td></tr>");
            foreach (DataRow r in dtable4.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Custname"] + "</td>");
                sbData.Append("<td>" + r["Location_Name"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["ProjectEditor"] + "</td>");
                sbData.Append("<td>" + r["Task"] + "</td>");
                sbData.Append("<td>" + r["Soft"] + "</td>");
                sbData.Append("<td>" + r["Platform"] + "</td>");
                sbData.Append("<td>" + r["Rec_date"] + "</td>");
                sbData.Append("<td>" + r["Due_DateFrom"] + "</td>");
                sbData.Append("<td>" + r["Due_DateTo"] + "</td>");
                sbData.Append("<td>" + r["DueTimeFrom"] + "</td>");
                sbData.Append("<td>" + r["DueTimeTo"] + "</td>");
                sbData.Append("<td>" + r["DueTimeFrom_IST"] + "</td>");
                sbData.Append("<td>" + r["DueTimeTo_IST"] + "</td>");
                sbData.Append("<td>" + r["Pages_Count"] + "</td>");
                sbData.Append("<td>" + r["File_Count"] + "</td>");
                sbData.Append("<td>" + r["Category"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "NonLaunch_OverAll_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
        }
    }
    public void Clear()
    {
        NL_ID.Text = "";
        txtJobid.Text = "";
        txtProjectEditor.Text = "";
        txtdueFromdate.Text = "";
        chkYTC.Checked = false;
        txtdueTodate.Text = "";
        txtRecDate.Text = "";
        txtdueFromdate.Text = "";
        txtdueTodate.Text = "";
        chkDueDate.Checked = false;
        chkDueTime.Checked = false;
        txtIndTo.Text = "";
        txtIndFrom.Text = "";
        lboxStage.ClearSelection();
        lboxtask.ClearSelection();
        lblResult.Text = "";
        txtDelDate.Text = "";
        lblResult1.Text = "";
        ddlStatus.Items.Clear();
    }
    protected void OnDelivery(object sender, EventArgs e)
    {
        DataSet dsNL = new DataSet();
        dsNL = nonLa.GetAmends();
        //lboxDelSatge.DataSource = dsNL;
        //lboxDelSatge.DataTextField = dsNL.Tables[0].Columns[1].ToString();
        //lboxDelSatge.DataValueField = dsNL.Tables[0].Columns[0].ToString();
        //lboxDelSatge.DataBind();
        dsNL = nonLa.getJobDetailsByID(hfP_ID.Value);
        DataRow r = dsNL.Tables[0].Rows[0];
        DelJobID.Text = r["JOBID"].ToString();
        DelProName.Text = r["projectname"].ToString();
        DelNL_ID.Text = r["NL_ID"].ToString();
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
    }
    protected void Click(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            Clear();
            DataSet dsNL=new DataSet();
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
                DropDelHrs.SelectedValue ="00";
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

        string Amends = "", AmendName = "", LastAmends = "" ;
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
            if(delDate.ToString()!="")
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
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(hfP_ID.Value);
            if (ds != null)
            {
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
            }
        }
        popup1.Hide();
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
    protected void gvJobTrack_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
    protected void dropCurStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCurStage.SelectedValue=="1")
        {
            txtdeldateAll.Text = "";
            lblResult2.Text = "";
            txtdeldateAll.Enabled = false;
            img4.Visible = false;
            dropDelHrsAll.Enabled = false;
            dropDelMinsAll.Enabled = false;
            dropDelZoneAll.Enabled = false;
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
                else if (dropCurStage.SelectedValue == "0")
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
                else if (dropCurStage.SelectedValue == "2")
                {
                    string inproc = "spInsertNextFinalJobHis_LP";
                    string[,] pname ={
                                        {"@NL_ID",sJobID.Value },{"@FP_ID",hid_FP_ID.Value},
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
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.getJobTracking(hfP_ID.Value);
            if (ds != null)
            {
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
            }
        }
        DelPopUp.Hide();
    }
    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gvJobTrack.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in gvJobTrack.Rows)
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
    protected void lnkLoggedEvent_Click(object sender, EventArgs e)
    {
        if (hfP_ID.Value != "")
        {
            DataSet ds = new DataSet();
            ds = nonLa.GetLoggedEvents(hfP_ID.Value);
            gvLoggedEvents.DataSource = ds;
            gvLoggedEvents.DataBind();
            ds = nonLa.GetLPLoggedTotalTime(hfP_ID.Value);
            if (ds != null)
            {
                lblLogTotalTime.Text = "Total Time : " + ds.Tables[0].Rows[0]["TotalTime"].ToString();
            }
            else
            {
                lblLogTotalTime.Text = "";
            }
        }
        this.showPanel(tabLoggedEvents);
    }
    protected void cmd_Save_Click(object sender, ImageClickEventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;
        try
        {
            foreach (GridViewRow grw in GvNL.Rows)
            {
                val = new Hashtable();

                val.Add("ID", ((HiddenField)grw.FindControl("hfgvNLID")).Value.ToString());
                val.Add("Status", ((DropDownList)grw.FindControl("DropStatus")).SelectedValue.ToString());
                val.Add("EMPID", Session["employeeid"].ToString());
                val.Add("JobNo", ((TextBox)grw.FindControl("lblJobNo")).Text.ToString());
                al.Add(val);
            }
            if (!nonLa.Update_DeliveryStatusNL(al))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");

            DataSet ds = new DataSet();
            ds = nonLa.GetJobDetails(Convert.ToInt32(drpCustomerSearch.SelectedValue), Convert.ToInt32(DDMonthList.SelectedValue), Convert.ToInt32(DDYearList.SelectedValue));
            GvNL.DataSource = ds;
            GvNL.DataBind();
            if (ds != null)
            {
                dtable4 = ds.Tables[0].Copy();
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { nonLa = null; }
    }
    protected void gv_FilePages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)Session["NLFilePages"];

        SaveCheckedValues();
        gv_FilePages.PageIndex = e.NewPageIndex;
        gv_FilePages.DataSource = ds;
        gv_FilePages.DataBind();
        PopulateCheckedValues();
        popup.Show();
    }
    private void SaveCheckedValues()
    {
        DataTable userdetails = new DataTable();
        userdetails.Columns.Add("Files_ID");
        userdetails.Columns.Add("LP_ID");
        userdetails.Columns.Add("NTLS_ID");
        userdetails.Columns.Add("TaskName");
        userdetails.Columns.Add("Task_ID");
        userdetails.Columns.Add("Lang_name");
        userdetails.Columns.Add("Lang_ID");
        userdetails.Columns.Add("Soft_Name");
        userdetails.Columns.Add("Soft_ID");
        userdetails.Columns.Add("Files_name");
        userdetails.Columns.Add("Pages");

        foreach (GridViewRow gvrow in gv_FilePages.Rows)
        {
            string Files_ID = ((Label)gvrow.FindControl("lbl_File")).Text;
            string LP_ID = ((HiddenField)gvrow.FindControl("hid_LP_ID")).Value;
            string NTLS_ID = ((HiddenField)gvrow.FindControl("hid_NTLS_ID")).Value;
            string TaskName = ((Label)gvrow.FindControl("lblTaskName")).Text;
            string Task_ID = ((HiddenField)gvrow.FindControl("hid_Task_ID")).Value;
            string Lang_name = ((Label)gvrow.FindControl("lblLang_Name")).Text;
            string Lang_ID = ((HiddenField)gvrow.FindControl("hid_Lang_ID")).Value;
            string Soft_Name = ((Label)gvrow.FindControl("lblgvSoft_Name")).Text;
            string Soft_ID = ((HiddenField)gvrow.FindControl("hid_Soft_ID")).Value;
            string Files_name = ((TextBox)gvrow.FindControl("txt_Name")).Text;
            string Pages = ((TextBox)gvrow.FindControl("txt_Pages")).Text;

            userdetails.Rows.Add(Files_ID, LP_ID, NTLS_ID, TaskName, Task_ID, Lang_name, Lang_ID, Soft_Name, Soft_ID, Files_name, Pages);
        }
        if (userdetails != null && userdetails.Rows.Count > 0)
            Session["Files"] = userdetails;
    }
    private void PopulateCheckedValues()
    {
        DataTable userdetails = (DataTable)Session["Files"];
        if (userdetails != null && userdetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in gv_FilePages.Rows)
            {
                string Files_ID = ((Label)gvrow.FindControl("lbl_File")).Text;
                DataRow[] matches = userdetails.Select("Files_ID like '%" + Files_ID + "%'");
                foreach (DataRow row in matches)
                {
                    TextBox Name = (TextBox)gvrow.FindControl("txt_Name");
                    Name.Text = row["Files_name"].ToString();
                    TextBox Pages = (TextBox)gvrow.FindControl("txt_Pages");
                    Pages.Text = row["Pages"].ToString();
                }
            }
        }
    }
    protected void xxxx_Click(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        ds1 = nonLa.getTarTaskLangDetailsByLPID(hfP_ID.Value);
        if (hfP_ID.Value != "")
        {
            gvTarFileInfo.DataSource = ds1;
            gvTarFileInfo.DataBind();
        }
    }
    protected void TarEdit(object sender, EventArgs e)
    {
        Session["NLTarFilePages"] = "";
        lblResult.Text = "";
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_LP_ID");
            HiddenField hid_NTLS_ID = (HiddenField)row.Cells[0].FindControl("hid_NTLS_ID");
            HiddenField hid_Task_ID = (HiddenField)row.Cells[1].FindControl("hid_Task_ID");
            HiddenField hid_Lang_ID = (HiddenField)row.Cells[2].FindControl("hid_Lang_ID");
            HiddenField hid_Soft_ID = (HiddenField)row.Cells[3].FindControl("hid_Soft_ID");
            TextBox Files = (TextBox)row.Cells[4].FindControl("lblgvFiles");
            //NL_ID.Text = sJobID.Value;
            //NTLS_ID.Text = hid_NTLS_ID.Value;
            //Task_ID.Text = hid_Task_ID.Value;
            //Soft_ID.Text = hid_Soft_ID.Value;
            //Lang_ID.Text = hid_Lang_ID.Value;
            //txtFiles.Text = Files.Text;
            int File = 0;
            if (Files.Text != "")
                File = Convert.ToInt16(Files.Text);
            DataSet dss = new DataSet();
            DataTable dTable = new DataTable();
            dTable.Columns.Add("Files_ID");
            dTable.Columns.Add("LP_ID");
            dTable.Columns.Add("NTLS_ID");
            dTable.Columns.Add("TaskName");
            dTable.Columns.Add("Task_ID");
            dTable.Columns.Add("Lang_name");
            dTable.Columns.Add("Lang_ID");
            dTable.Columns.Add("Soft_Name");
            dTable.Columns.Add("Soft_ID");
            dTable.Columns.Add("Files_Name");
            dTable.Columns.Add("Pages");

            dss = nonLa.GetTarFilePages(hid_NTLS_ID.Value);
            if (dss == null)
            {
                dss = nonLa.getLPNTLS(hid_NTLS_ID.Value);

                for (int j = 1; j <= File; j++)
                {
                    dTable.Rows.Add(j, dss.Tables[0].Rows[0]["NL_ID"].ToString(), dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                        dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                        dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(), "", 0);
                }
            }
            else
            {
                dss = nonLa.getTarLPInsertedNTLS(hid_NTLS_ID.Value);
                int k = 1;
                for (int j = 0; j <= (File - 1); j++)
                {
                    if (k <= dss.Tables[0].Rows.Count)
                    {
                        dTable.Rows.Add(dss.Tables[0].Rows[j]["Files_ID"].ToString(), dss.Tables[0].Rows[j]["NL_ID"].ToString(),
                            dss.Tables[0].Rows[j]["NTLS_ID"].ToString(),
                            dss.Tables[0].Rows[j]["TaskName"].ToString(), dss.Tables[0].Rows[j]["Task_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Lang_name"].ToString(), dss.Tables[0].Rows[j]["Lang_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Soft_Name"].ToString(), dss.Tables[0].Rows[j]["Soft_ID"].ToString(),
                            dss.Tables[0].Rows[j]["Files_Name"].ToString(), dss.Tables[0].Rows[j]["Pages"].ToString());
                    }
                    else
                    {
                        dTable.Rows.Add(k, dss.Tables[0].Rows[0]["NL_ID"].ToString(),
                            dss.Tables[0].Rows[0]["NTLS_ID"].ToString(),
                            dss.Tables[0].Rows[0]["TaskName"].ToString(), dss.Tables[0].Rows[0]["Task_ID"].ToString(),
                            dss.Tables[0].Rows[0]["Lang_name"].ToString(), dss.Tables[0].Rows[0]["Lang_ID"].ToString(),
                            dss.Tables[0].Rows[0]["Soft_Name"].ToString(), dss.Tables[0].Rows[0]["Soft_ID"].ToString(),
                            "", 0);
                    }
                    k++;
                }
            }
            DataSet Ds = new DataSet();
            Ds.Tables.Add(dTable);
            gv_FilePages.DataSource = Ds;
            gv_FilePages.DataBind();
            // popup.Show();
            Session["NLTarFilePages"] = Ds;

            string strPopup = "<script language='javascript' ID='script1'>"
            + "window.open('LaunchNLTarFilesPages.aspx?NL_ID=" + HttpUtility.UrlEncode(sJobID.Value) + "&NTLS_ID=" + HttpUtility.UrlEncode(hid_NTLS_ID.Value)
            + "&Task_ID=" + HttpUtility.UrlEncode(hid_Task_ID.Value) + "&Soft_ID=" + HttpUtility.UrlEncode(hid_Soft_ID.Value) + "&Lang_ID=" + HttpUtility.UrlEncode(hid_Lang_ID.Value) + "&File=" + HttpUtility.UrlEncode(File.ToString()) + "&FileConv=" + HttpUtility.UrlEncode(DropNewNameConv.SelectedValue)
            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"
            + "</script>";
            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
    }

  

   

   
    
   

  

  

   


    protected void TimeTakenNew_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
        {
            string time = ((TextBox)grw.FindControl("txt_time")).Text;
            string task = ((HiddenField)grw.FindControl("hid_Task_ID")).Value;
            string name = ((HiddenField)grw.FindControl("hid_LP_ID")).Value;
            string soft = ((HiddenField)grw.FindControl("hid_Soft_ID")).Value;
            nonLa.UpdateTimeTakenNl(name, task, time, soft);
        }
        DataSet sds1 = new DataSet();
        sds1 = nonLa.getValues1(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmoduleNew.DataSource = sds1.Tables[0];
            gv_pmoduleNew.DataBind();
        }
        lnkNewCostInfo_Click(sender, e);
    }
    protected void HrsNew_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
        {
            string hrs = ((TextBox)grw.FindControl("txt_hrate")).Text;
            string task = ((HiddenField)grw.FindControl("hid_Task_ID")).Value;
            string name = ((HiddenField)grw.FindControl("hid_LP_ID")).Value;
            string soft = ((HiddenField)grw.FindControl("hid_Soft_ID")).Value;
            nonLa.UpdateHrs1(name, task, hrs, soft);
        }
        DataSet sds1 = new DataSet();
        sds1 = nonLa.getValues1(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmoduleNew.DataSource = sds1.Tables[0];
            gv_pmoduleNew.DataBind();
        }
        lnkNewCostInfo_Click(sender, e);
    }
    protected void PageNew_TextChanged(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";
        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
        {
            string page = ((TextBox)grw.FindControl("txt_prate")).Text;
            string task = ((HiddenField)grw.FindControl("hid_Task_ID")).Value;
            string name = ((HiddenField)grw.FindControl("hid_LP_ID")).Value;
            string soft = ((HiddenField)grw.FindControl("hid_Soft_ID")).Value;
            nonLa.UpdatePage1(name, task, page, soft);
        }
        DataSet sds1 = new DataSet();
        sds1 = nonLa.getValues1(id);
        if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
        {
            gv_pmoduleNew.DataSource = sds1.Tables[0];
            gv_pmoduleNew.DataBind();
        }
        lnkNewCostInfo_Click(sender, e);
    }

    protected void btnNewQuoteSave_Click(object sender, EventArgs e)
    {
        ArrayList al = new ArrayList();
        Hashtable val = null;

        try
        {
            bool chk;
            chk = true;
            foreach (GridViewRow grw in gv_pmoduleNew.Rows)
            {
                val = new Hashtable();
                val.Add("LP_ID", ((HiddenField)grw.FindControl("hid_LP_ID")).Value.Trim().ToString());
                val.Add("TASK_ID", ((HiddenField)grw.FindControl("hid_Task_ID")).Value.Trim().ToString());
                val.Add("TIME_TAKEN", ((TextBox)grw.FindControl("txt_time")).Text.Trim().ToString());
                val.Add("LANG_COUNT", ((TextBox)grw.FindControl("txt_langcount")).Text.Trim().ToString());
                val.Add("PAGES_COUNT", ((TextBox)grw.FindControl("txt_totalpage")).Text.Trim().ToString());
                val.Add("TOTAL_PAGES", ((TextBox)grw.FindControl("txt_totalpage")).Text.Trim().ToString());
                val.Add("HOUR_RATE", ((TextBox)grw.FindControl("txt_hourrate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("PAGE_RATE", ((TextBox)grw.FindControl("txt_pagerate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("P_RATE", ((TextBox)grw.FindControl("txt_prate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("H_RATE", ((TextBox)grw.FindControl("txt_hrate")).Text.Trim().ToString().Replace("€", "").Replace("$", ""));
                val.Add("Soft_ID", ((HiddenField)grw.FindControl("hid_Soft_ID")).Value.Trim().ToString());
                CheckBox page = (CheckBox)grw.FindControl("chk_page"); //sender;
                bool status = page.Checked;
                int p;
                if (status == true)
                    p = 1;
                else
                    p = 0;
                val.Add("PAGEYN", p.ToString());
                CheckBox hour = (CheckBox)grw.FindControl("chk_hour"); //sender;
                bool status1 = hour.Checked;
                int h;
                if (status1 == true)
                    h = 1;
                else
                    h = 0;
                val.Add("HOURYN", h.ToString());
                al.Add(val);
                if (!page.Checked && !hour.Checked)
                {
                    chk = false;
                    break;
                }
            }
            if (chk == true)
            {
                string id;
                if (hfP_ID.Value != "")
                {
                    id = hfP_ID.Value;
                }
                else
                {
                    id = "";
                }
                string FinalCheck = "";
                //if (txtFinalCheck.Text == "")
                //    FinalCheck = Session["fname"].ToString() + ' ' + Session["sname"].ToString();
                //else
                FinalCheck = Session["fname"].ToString() + ' ' + Session["sname"].ToString();

                string inproc = "spUpdateQuote_NL";
                string[,] pname ={
                    {"@QUOTEREMARKS",txtNewquoteremark.Text},{"@FINALCHECK",FinalCheck.ToString()},
                    {"@PROJECT_CO",txtNewProjectCo.Text},{"@LP_ID",id},
                    {"@IsExist","Output"},{"@employee_id",Session["employeeid"].ToString()}};
                int val3 = this.oNewLa.ExcSP(inproc, pname, CommandType.StoredProcedure);
                nonLa.InsertUnassignJobs(id.ToString());
                if (!nonLa.Update_NL_LaunchQuote(al))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Please Check Page or Hour based...');</script>");
            }
            lnkNewCostInfo_Click(sender, e);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { }
    }

    protected void lnkNewCostInfo_Click(object sender, EventArgs e)
    {
        string id;
        if (hfP_ID.Value != "")
            id = hfP_ID.Value;
        else
            id = "";

        if (id != "")
        {
            Launch la = new Launch();
            DataSet sds1 = new DataSet();
            sds1 = nonLa.getValues1(id);
            if (sds1 != null && sds1.Tables[0].Rows.Count > 0)
            {
                
                    gv_pmoduleNew.DataSource = sds1.Tables[0];
                    gv_pmoduleNew.DataBind();

                    if (sds1.Tables[0].Rows[0]["Jobid"].ToString() != "")
                    {
                        foreach (GridViewRow grw in gv_pmoduleNew.Rows)
                        {
                            TextBox Time = (TextBox)grw.FindControl("txt_time");
                            TextBox Hrate = (TextBox)grw.FindControl("txt_hrate");
                            TextBox Prate = (TextBox)grw.FindControl("txt_prate");
                            TextBox Hcost = (TextBox)grw.FindControl("txt_hourrate");
                            TextBox Pcost = (TextBox)grw.FindControl("txt_pagerate");
                            CheckBox HChk = (CheckBox)grw.FindControl("chk_hour");
                            CheckBox PChk = (CheckBox)grw.FindControl("chk_page");
                            TextBox Lang = (TextBox)grw.FindControl("txt_langcount");
                            TextBox Ptotal = (TextBox)grw.FindControl("txt_totalpage");
                            try
                            {
                                if (Session["employeeid"].ToString() == "2461" || Session["employeeid"].ToString() == "1914")
                                {
                                    Time.Enabled = true;
                                    Hrate.Enabled = true;
                                    Prate.Enabled = true;
                                    Hcost.Enabled = true;
                                    Pcost.Enabled = true;
                                    HChk.Enabled = true;
                                    PChk.Enabled = true;
                                    Lang.Enabled = true;
                                    Ptotal.Enabled = true;
                                    txtNewquoteremark.Enabled = true;
                                    btnNewQuoteSave.Enabled = true;
                                    btnNewClear.Enabled = true;
                                }
                                else
                                {
                                    Time.Enabled = false;
                                    Hrate.Enabled = false;
                                    Prate.Enabled = false;
                                    Hcost.Enabled = false;
                                    Pcost.Enabled = false;
                                    HChk.Enabled = false;
                                    PChk.Enabled = false;
                                    Lang.Enabled = false;
                                    Ptotal.Enabled = false;
                                    txtNewquoteremark.Enabled = true;
                                    btnNewQuoteSave.Enabled = true;
                                    btnNewClear.Enabled = true;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                txtNewquoteremark.Text = sds1.Tables[0].Rows[0]["QUOTEREMARKS"].ToString();
                txtNewFinalCheck.Text = sds1.Tables[0].Rows[0]["FINALCHECK"].ToString();
            }
            try
            {
                DataSet ds = new DataSet();
                ds = nonLa.NLempname(id);
                txtNewProjectCo.Text = ds.Tables[0].Rows[0]["empname"].ToString();
            }
            catch(Exception ex)
            {
                string s = ex.ToString();
            }
        }
        this.showPanel(tabNewQuote);
    }

    protected void btnNewClear_Click(object sender, EventArgs e)
    {

    }

}