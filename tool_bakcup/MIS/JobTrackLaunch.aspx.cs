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

public partial class JobTrackLaunch : System.Web.UI.Page
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
                dtable4 = ds.Tables[1].Copy();
                dtable = ds.Tables[0].Copy();
                gvJobTrack.DataSource = ds;
                gvJobTrack.DataBind();
                //ddlcustomer_SelectedIndexChanged(sender, e);
            }
            else
            {
                gvJobTrack.DataSource = null;
                gvJobTrack.DataBind();
            }
        }
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
                bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
                bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
                bindDDL(oTable, ddlStatus, "soft", "soft");
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblJOB = e.Row.FindControl("lblJOB") as Label;
            lblJOB.Text = iRowID.ToString();

           // e.Row.Cells[0].Text = iRowID.ToString();
            iRowID++;

            HiddenField ID = e.Row.FindControl("hid_NL_ID") as HiddenField;
            HiddenField Loc_ID = e.Row.FindControl("hid_loc_ID") as HiddenField;
            HiddenField DUEDATE = e.Row.FindControl("hid_Datetime_IST") as HiddenField;
            
            if (DUEDATE.Value != "")
            {
                DateTime Due = Convert.ToDateTime(DUEDATE.Value);
                if (Convert.ToDateTime(DUEDATE.Value) < DateTime.Now)
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
            
            //DataSet ds = new DataSet();
            //ds = nonLa.GetDeliveryStatus(Convert.ToInt32(ID.Value));
            //if(ds==null)
            //    ds = nonLa.getJobDetailsByID(ID.Value);
            //DropDownList status = e.Row.FindControl("dropDelStatus") as DropDownList;
            //status.Items.Add(new ListItem("--Select--", "0"));
            //status.Items.Add(new ListItem("P", "P"));
            //status.Items.Add(new ListItem("C", "C"));
            //status.Items.Add(new ListItem("WIP", "WIP"));
            //status.Items.Add(new ListItem("Del", "Del"));
            //status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["Delivery_Status"].ToString()))].Selected = true;

            //if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "P")
            //    status.CssClass = "gridP";
            //else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "C")
            //    status.CssClass = "gridC";
            //else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del")
            //    status.CssClass = "gridDel";
            //else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "WIP")
            //    status.CssClass = "gridWIP";
        }
    }
    protected void Click(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            int intId = 17;
            HiddenField sJobID = (HiddenField)row.Cells[0].FindControl("hid_NL_ID");
            HiddenField Loc_ID = (HiddenField)row.Cells[0].FindControl("hid_loc_ID");
            string strPopup = "<script language='javascript' ID='script1'>"

            // Passing intId to popup window.
            + "window.open('JobTrackLaunchFile.aspx?ID=" + HttpUtility.UrlEncode(sJobID.Value) + "&Loc_ID=" + HttpUtility.UrlEncode(Loc_ID.Value) 

            + "','new window', 'top=90, left=150, width=1100, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }
        
    }

    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "soft", "soft");
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "soft", "soft");
    }
    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlStatus, "soft", "soft");
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
        bindDDL(oTable, ddlStatus, "soft", "soft");
    }
    protected void ddlDueTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlStatus, "soft", "soft");
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
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
            dtable = oTable.Copy();
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
            sFilterText += "Date_IST='" + ddlDueDate.SelectedValue + "'";
        }
        if (ddlDueTime.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }

            sFilterText += "Time_IST<=#" + Convert.ToDateTime(ddlDueTime.SelectedValue.Replace(" IST", "")).ToString().Replace(DateTime.Now.ToShortDateString(), "01/01/1900").ToString() + "#";
        }
        if (ddlStatus.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "soft='" + ddlStatus.SelectedValue + "'";
        }
        oview.RowFilter = sFilterText;

        int P =0; int QA = 0; int QC = 0; int RFD = 0;

        int initial = 0; int FP = 0;
        int V1 = 0;  int V2 = 0;  int V3 = 0;  int V4 = 0;  int V5 = 0;
        int V6 = 0;  int V7 = 0;  int V8 = 0;  int V9 = 0;  int V10 = 0;
        int V11 = 0; int V12 = 0; int V13 = 0; int V14 = 0; int V15 = 0;
        int V16 = 0; int V17 = 0; int V18 = 0; int V19 = 0; int V20 = 0;
        int V21 = 0; int V22 = 0; int V23 = 0; int V24 = 0; int V25 = 0;

        for (int i = 0; i < oview.Count; i++)
        {
            if(oview[i]["Workflow"].ToString().Contains("Process"))
            {
                P = P + Convert.ToInt32(oview[i]["Pages"].ToString());
            }
            else if (oview[i]["Workflow"].ToString().Contains("QA"))
            {
                QA = QA + Convert.ToInt32(oview[i]["Pages"].ToString());
            }
            else if (oview[i]["Workflow"].ToString().Contains("QC"))
            {
                QC = QC + Convert.ToInt32(oview[i]["Pages"].ToString());
            }
            else if (oview[i]["Workflow"].ToString().Contains("Delivery"))
            {
                RFD = RFD + Convert.ToInt32(oview[i]["Pages"].ToString());
            }
        }
        for (int i = 0; i < oview.Count; i++)
        {
            if (oview[i]["AmendName"].ToString().Contains("Initial"))
                initial = initial + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V1 Amends"))
                V1 = V1 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V2 Amends"))
                V2 = V2 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V3 Amends"))
                V3 = V3 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V4 Amends"))
                V4 = V4 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V5 Amends"))
                V5 = V5 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V6 Amends"))
                V6 = V6 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V7 Amends"))
                V7 = V7 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V8 Amends"))
                V8 = V8 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V9 Amends"))
                V9 = V9 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V10 Amends"))
                V10 = V10 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V11 Amends"))
                V11 = V11 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V12 Amends"))
                V12 = V12 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V13 Amends"))
                V13 = V13 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V14 Amends"))
                V14 = V14 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V15 Amends"))
                V15 = V15 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V16 Amends"))
                V16 = V16 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V17 Amends"))
                V17 = V17 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V18 Amends"))
                V18 = V18 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V19 Amends"))
                V19 = V19 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V20 Amends"))
                V20 = V20 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V21 Amends"))
                V21 = V21 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V22 Amends"))
                V22 = V22 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V23 Amends"))
                V23 = V23 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V24 Amends"))
                V24 = V24 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("V25 Amends"))
                V25 = V25 + Convert.ToInt32(oview[i]["Pages"].ToString());
            else if (oview[i]["AmendName"].ToString().Contains("Final Package"))
                FP = FP + Convert.ToInt32(oview[i]["Pages"].ToString());
        }

        string amendDetails = string.Empty;
        if (initial != 0)
            amendDetails = "Initial : " + initial;

        if (amendDetails != "" && V1 != 0)
            amendDetails = amendDetails + ", V1 : " + V1;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V1 : " + V1;

        if (amendDetails != "" && V2 != 0)
            amendDetails = amendDetails + ", V2 : " + V2;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V2 : " + V2;

        if (amendDetails != "" && V3 != 0)
            amendDetails = amendDetails + ", V3 : " + V3;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V3 : " + V3;

        if (amendDetails != "" && V4 != 0)
            amendDetails = amendDetails + ", V4 : " + V4;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V4 : " + V4;

        if (amendDetails != "" && V5 != 0)
            amendDetails = amendDetails + ", V5 : " + V5;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V5 : " + V5;

        if (amendDetails != "" && V6 != 0)
            amendDetails = amendDetails + ", V6 : " + V6;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V6 : " + V6;

        if (amendDetails != "" && V7 != 0)
            amendDetails = amendDetails + ", V7 : " + V7;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V7 : " + V7;

        if (amendDetails != "" && V8 != 0)
            amendDetails = amendDetails + ", V8 : " + V8;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V8 : " + V8;

        if (amendDetails != "" && V9 != 0)
            amendDetails = amendDetails + ", V9 : " + V9;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V9 : " + V9;

        if (amendDetails != "" && V10 != 0)
            amendDetails = amendDetails + ", V10 : " + V10;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V10 : " + V10;

        if (amendDetails != "" && V11 != 0)
            amendDetails = amendDetails + ", V11 : " + V11;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V11 : " + V11;

        if (amendDetails != "" && V12 != 0)
            amendDetails = amendDetails + ", V12 : " + V12;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V12 : " + V12;

        if (amendDetails != "" && V13 != 0)
            amendDetails = amendDetails + ", V13 : " + V13;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V13 : " + V13;

        if (amendDetails != "" && V14 != 0)
            amendDetails = amendDetails + ", V14 : " + V14;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V14 : " + V14;

        if (amendDetails != "" && V15 != 0)
            amendDetails = amendDetails + ", V15 : " + V15;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V15 : " + V15;

        if (amendDetails != "" && V16 != 0)
            amendDetails = amendDetails + ", V16 : " + V16;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V16 : " + V16;

        if (amendDetails != "" && V17 != 0)
            amendDetails = amendDetails + ", V17 : " + V17;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V17 : " + V17;

        if (amendDetails != "" && V18 != 0)
            amendDetails = amendDetails + ", V18 : " + V18;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V18 : " + V18;

        if (amendDetails != "" && V19 != 0)
            amendDetails = amendDetails + ", V19 : " + V19;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V19 : " + V19;

        if (amendDetails != "" && V20 != 0)
            amendDetails = amendDetails + ", V20 : " + V20;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V20 : " + V20;

        if (amendDetails != "" && V21 != 0)
            amendDetails = amendDetails + ", V21 : " + V21;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V21 : " + V21;

        if (amendDetails != "" && V22 != 0)
            amendDetails = amendDetails + ", V22 : " + V22;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V22 : " + V22;

        if (amendDetails != "" && V23 != 0)
            amendDetails = amendDetails + ", V23 : " + V23;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V23 : " + V23;

        if (amendDetails != "" && V24 != 0)
            amendDetails = amendDetails + ", V24 : " + V24;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V24 : " + V24;

        if (amendDetails != "" && V25 != 0)
            amendDetails = amendDetails + ", V25 : " + V25;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "V25 : " + V25;

        if (amendDetails != "" && FP != 0)
            amendDetails = amendDetails + ", FP : " + FP;
        else if (amendDetails != "")
            amendDetails = amendDetails;
        else
            amendDetails = "FP : " + FP;

        string pageCount="Workflow Wise : (Process : " + P.ToString()+", QC : " + QC.ToString() +", QA : "+QA.ToString()+", RFD : "+RFD.ToString()+")             AmendWise : ("+ amendDetails+ ")      Total Pages : "+ Convert.ToString(P+QA+QC+RFD)+" ";

        lblPagesCount.Text =  pageCount;
        return oview;
    }
    protected void cmd_Excel_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable != null && dtable.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='17' align='center'><h4>Job Track</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>JOB NAME</b></td><td bgcolor='silver'><b>Location</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>File Name</b></td><td bgcolor='silver'><b>RecDate</b></td><td bgcolor='silver'><b>DUE DATE</b></td><td bgcolor='silver'><b>DUE Time</b></td><td bgcolor='silver'><b>DUE Date (IST)</b></td><td bgcolor='silver'><b>Due Time (IST)</b></td><td bgcolor='silver'><b>Pages</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>WorkFlow</b></td><td bgcolor='silver'><b>Software</b></td><td bgcolor='silver'><b>Language</b></td></tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Custname"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["Location_Name"] + "</td>");
                sbData.Append("<td>" + r["TaskName"] + "</td>");
                sbData.Append("<td>" + r["FileName"] + "</td>");
                sbData.Append("<td>" + r["Rec_Date"] + "</td>");
                sbData.Append("<td>" + r["DUE_DATEFROM"] + "</td>");
                sbData.Append("<td>" + r["DUE_TimeFrom"] + "</td>");
                sbData.Append("<td>" + r["Date_IST"] + "</td>");
                sbData.Append("<td>" + r["Times_IST"] + "</td>");
                sbData.Append("<td>" + r["Pages"] + "</td>");
                sbData.Append("<td>" + r["AmendName"] + "</td>");
                sbData.Append("<td>" + r["WorkFlow"] + "</td>");
                sbData.Append("<td>" + r["Soft"] + "</td>");
                sbData.Append("<td>" + r["Lang_name"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Job_Track_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
        }
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        int i = 1;
        if (dtable4 != null && dtable4.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='15' align='center'><h4>Job Track Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>JOB NAME</b></td><td bgcolor='silver'><b>Location</b></td><td bgcolor='silver'><b>Task</b></td><td bgcolor='silver'><b>Files</b></td><td bgcolor='silver'><b>DUE DATE</b></td><td bgcolor='silver'><b>DUE Time</b></td><td bgcolor='silver'><b>DUE Date (IST)</b></td><td bgcolor='silver'><b>Due Time (IST)</b></td><td bgcolor='silver'><b>Pages</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>Software</b></td><td bgcolor='silver'><b>Language</b></td></tr>");
            foreach (DataRow r in dtable4.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + i + "</td>");
                sbData.Append("<td>" + r["Jobid"] + "</td>");
                sbData.Append("<td>" + r["Custname"] + "</td>");
                sbData.Append("<td>" + r["Projectname"] + "</td>");
                sbData.Append("<td>" + r["Location_Name"] + "</td>");
                sbData.Append("<td>" + r["TaskName"] + "</td>");
                sbData.Append("<td>" + r["Files"] + "</td>");
                sbData.Append("<td>" + r["DUE_DATEFROM"] + "</td>");
                sbData.Append("<td>" + r["DUE_TimeFrom"] + "</td>");
                sbData.Append("<td>" + r["Date_IST"] + "</td>");
                sbData.Append("<td>" + r["Times_IST"] + "</td>");
                sbData.Append("<td>" + r["Pages"] + "</td>");
                sbData.Append("<td>" + r["AmendName"] + "</td>");
                sbData.Append("<td>" + r["Soft"] + "</td>");
                sbData.Append("<td>" + r["Lang_name"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Job_Track_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
        }
    }
}