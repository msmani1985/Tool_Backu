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
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;

public partial class WIPToday_New : System.Web.UI.Page
{

    int iRowID = 1;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private static DropDownList ddlSAMJournal = new DropDownList();
    private string sPrevText = "";
    string emp_id = "";
    string emp_team_id = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            emp_id = Session["employeeid"].ToString();
            emp_team_id = Session["employeeteamid"].ToString();

            if (!Page.IsPostBack)
            {

                datasourceIBSQL oSQL = new datasourceIBSQL();
                DataSet oDs = new DataSet();
                string[,] oParams = new string[,]{
                {"@jobid", "0" },
                {"@jobtypeid", "0"},
                {"@employeeid",emp_id},
                {"@employeeteamid",emp_team_id}
            };
                oDs = oSQL.ExcProcedure("spGET_LIVE_JOB_AMO", oParams, CommandType.StoredProcedure);
                oDs.Tables[0].Merge(oDs.Tables[1], true);
                Session["LiveDS"] = oDs.Tables[0];
                Session["viewTable"] = oDs.Tables[0];
                gvWIPtoday.DataSource = oDs.Tables[0];
                gvWIPtoday.DataBind();
                iRowID = 1;
            }
            //if (emp_id == "1847" || emp_id == "2056" || emp_id == "1908" || emp_id == "1135")
            //{
            //    gvWIPtoday.Columns[16].Visible = true;
            //}
            //else
            //{
            //    gvWIPtoday.Columns[16].Visible = false;
            //}
         
        }
    }

    protected void gvWIPtoday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int empid = Convert.ToInt32(Session["employeeid"]);
            DataTable oTable = (DataTable)Session["LiveDS"];
            if (e.Row.RowType == DataControlRowType.Header && !Page.IsPostBack)
            {
                if (Session["LiveDS"] != null)
                {
                    bindDDL(oTable, ddlJobType, "JOB_TYPE", "JOB_TYPE");
                    bindDDL(oTable, ddlparentjobid, "ASSIGNED", "ASSIGNED");
                    bindDDL(oTable, ddldept, "DEPARTMENT", "DEPARTMENT");
                    bindDDL(oTable, ddlstage, "JOB_STAGE", "JOB_STAGE");
                    bindDDL(oTable, ddlcustomer, "CUSTOMER_NAME", "CUSTOMER_NAME");
                    bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
                    bindDDL(oTable, ddlIsSAM, "SAM_JOURNAL", "SAM_JOURNAL");
                    bindDDL(oTable, ddlIsCopyEdit, "CE_JOURNAL", "CE_JOURNAL");
                    bindDDL(oTable, ddlVendorname, "VENDOR_NAME", "VENDOR_NAME");
                    bindDDL(oTable, ddlIsPreEdit, "Ispreedit", "Ispreedit");
                }
               //if (emp_id == "1847" || emp_id == "2056" || emp_id == "1908" || emp_id == "1135")
               // {
               //     e.Row.Cells[16].Visible = true;
               // }
               // else
               // {
               //     e.Row.Cells[16].Visible = false;
               // }
		
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = iRowID.ToString();
                iRowID++;
                if (e.Row.Cells[12].Text.ToLower() != "&nbsp;")
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "pink");
                }
               // if (e.Row.Cells[17].Text.ToLower() != "0" && e.Row.Cells[12].Text.ToLower() == "&nbsp;")
               // {
               //     e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FF0000");
               //     e.Row.Style.Add(HtmlTextWriterStyle.BorderStyle, "red");
               // }
               
                //////if (emp_id == "1847" || emp_id == "2056" || emp_id == "1908" || emp_id == "1135")
                //////{
                //////    e.Row.Cells[16].Visible = true;
                //////}
                //////else
                //////{
                //////    e.Row.Cells[16].Visible = false;
                //////}
		
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

     public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    } 

    protected void gvWIPtoday_OnSort(object sender, GridViewSortEventArgs e)
    {
        String sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING); 
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }
    }

    private void SortGridView(string sortExpression, string direction)
    {
        //  You can cache the DataTable for improving performance 
        DataView dv = GetVoidViewTable();
        dv.Sort = sortExpression + direction;
        gvWIPtoday.DataSource = dv;
        gvWIPtoday.DataBind();

    } 

    private DropDownList bindDDL(DataTable  oTable, DropDownList oList, string sFilter, string sColName)
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
            for (int k = 0 ; k < oList.Items.Count ; k++)
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
        oList.SelectedIndexChanged +=new EventHandler(oList_SelectedIndexChanged);
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

    private DataView GetVoidViewTable()
    {
        string sFilterText = "";
        DataTable oTable = (DataTable)(Session["LiveDS"]);
        DataView oview = oTable.DefaultView;
        oview.RowFilter = "";
        if (ddlJobType.SelectedValue != "0")
            sFilterText = "JOB_TYPE='" + ddlJobType.SelectedValue + "'";
        if (ddlparentjobid.SelectedValue != "0")
        {
            //if (ddlJobType.SelectedValue != "0")
              //  sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "ASSIGNED='" + ddlparentjobid.SelectedValue + "'";
        }
        if (ddldept.SelectedValue != "0")
        {
            //if (ddlJobType.SelectedValue != "0" || ddlparentjobid.SelectedValue != "0")
              //  sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "DEPARTMENT='" + ddldept.SelectedValue + "'";
        }
        if (ddlonhold.SelectedValue != "0")
        {
            //if (ddldept.SelectedValue != "0" || ddlJobType.SelectedValue != "0" || ddlparentjobid.SelectedValue != "0" || ddlstage.SelectedValue != "0")
              //  sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            if (ddlonhold.SelectedValue == "1")
                sFilterText += "ONHOLD_HISTORY_ID IS NOT NULL ";
            else
                sFilterText += "ONHOLD_HISTORY_ID IS NULL ";
        }
        if (ddlstage.SelectedValue != "0")
        {
            //if (ddlJobType.SelectedValue != "0" || ddlparentjobid.SelectedValue != "0" || ddlonhold.SelectedValue != "0")
              //  sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "JOB_STAGE='" + ddlstage.SelectedValue + "'";
        }
        if (ddlcustomer.SelectedValue != "0")
        {
            //if (ddlJobType.SelectedValue != "0" || ddlparentjobid.SelectedValue != "0" || ddlonhold.SelectedValue != "0" || ddlstage.SelectedValue != "0")
            //    sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "CUSTOMER_NAME='" + ddlcustomer.SelectedValue + "'";
        }
        if (ddlcatsduedate.SelectedValue != "0")
        {
            //if (ddlcustomer.SelectedValue != "0" || ddlJobType.SelectedValue != "0" || ddlparentjobid.SelectedValue != "0" || ddlonhold.SelectedValue != "0" || ddlstage.SelectedValue != "0")
            //    sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "CATS_DUE_DATE='" + ddlcatsduedate.SelectedValue + "'";
        }
        if (ddlVendorname.SelectedValue != "0")
        {
            //if (ddlcustomer.SelectedValue != "0" || ddlJobType.SelectedValue != "0" || ddlparentjobid.SelectedValue != "0" || ddlonhold.SelectedValue != "0" || ddlstage.SelectedValue != "0")
            //    sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "VENDOR_NAME='" + ddlVendorname.SelectedValue + "'";
        }
        if (ddlIsSAM.SelectedValue != "0")
        {
            //if (ddlcustomer.SelectedValue != "0")
            //    sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "SAM_JOURNAL='" + ddlIsSAM.SelectedValue + "'";
        }
        if (ddlIsPreEdit.SelectedValue != "0")
        {
            //if (ddlcustomer.SelectedValue != "0")
            //    sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "IsPreEdit='" + ddlIsPreEdit.SelectedValue + "'";
        }
        if (ddlIsCopyEdit.SelectedValue != "0")
        {
            //if (ddlcustomer.SelectedValue != "0")
            //    sFilterText += " AND ";
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "CE_JOURNAL='" + ddlIsCopyEdit.SelectedValue + "'";
        }
        oview.RowFilter = sFilterText;
        return oview;
    }

    protected void oList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable oTable = new DataTable();
            oTable = GetVoidViewTable().Table;
            gvWIPtoday.DataSource = oTable;
            gvWIPtoday.DataBind();
            iRowID = 1;
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }

    }

    protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged (sender, e);

        try
        {
            string sFilterText = "";
            DropDownList oList = new DropDownList();
            if (sender != null)
                oList = (DropDownList)sender;
            DataTable oTable = (DataTable)(Session["LiveDS"]);
            DataView oview = oTable.DefaultView;
            oview.RowFilter = "";
            if (ddlJobType.SelectedValue != "0")
                sFilterText = "JOB_TYPE='" + ddlJobType.SelectedValue + "'";

            oview.RowFilter = sFilterText;

            //oview = oview.Table.DefaultView.ToTable(true, "ASSIGNED").DefaultView;
            //ddlparentjobid.Items.Clear();
            //ddlparentjobid.DataSource = oview.Table;
            //ddlparentjobid.DataTextField = "ASSIGNED";
            //ddlparentjobid.DataValueField = "ASSIGNED";
            //ddlparentjobid.DataBind();
            //ddlparentjobid.Items.Insert(0, new ListItem("--NO FILTER--", "0"));
            bindDDL(oTable, ddlparentjobid, "ASSIGNED", "ASSIGNED");
            bindDDL(oview.Table, ddldept, "DEPARTMENT", "DEPARTMENT");
            bindDDL(oview.Table, ddlstage, "JOB_STAGE", "JOB_STAGE");
            bindDDL(oTable, ddlcustomer, "CUSTOMER_NAME", "CUSTOMER_NAME");
            bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
            bindDDL(oTable, ddlIsSAM, "SAM_JOURNAL", "SAM_JOURNAL");
            bindDDL(oTable, ddlIsCopyEdit, "CE_JOURNAL", "CE_JOURNAL");
            bindDDL(oTable, ddlVendorname, "VENDOR_NAME", "VENDOR_NAME");
            bindDDL(oTable, ddlIsPreEdit, "IsPreEdit", "IsPreEdit");



        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

    protected void ddlparentjobid_SelectedIndexChanged(object sender, EventArgs e)
    {
        //file on change of index
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;

        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //file on change of index
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;

        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
    }

    protected void ddljobstage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //file on change of index
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;

        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
    }

    protected void ddlonhold_SelectedIndexChanged(object sender, EventArgs e)
    {
        //file on change of index
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
    }

    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //file on change of index
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlJobType, "JOB_TYPE", "JOB_TYPE");
        bindDDL(oTable, ddlparentjobid, "ASSIGNED", "ASSIGNED");
        bindDDL(oTable, ddldept, "DEPARTMENT", "DEPARTMENT");
        bindDDL(oTable, ddlstage, "JOB_STAGE", "JOB_STAGE");
        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
        bindDDL(oTable, ddlIsSAM, "SAM_JOURNAL", "SAM_JOURNAL");
        bindDDL(oTable, ddlIsCopyEdit, "CE_JOURNAL", "CE_JOURNAL");
        bindDDL(oTable, ddlVendorname, "VENDOR_NAME", "VENDOR_NAME");
        bindDDL(oTable, ddlIsPreEdit, "IsPreEdit", "IsPreEdit");

    }

    protected void onewList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //file on change of index
        oList_SelectedIndexChanged(sender, e);
    }

    protected void ddlcatsduedate_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
    }

    protected void ddlIsSAM_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
    }

    protected void ddlIsCopyEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcatsduedate, "CATS_DUE_DATE", "CATS_DUE_DATE");
    }
    protected void ddlIsPreedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlIsPreEdit, "ISPREEDIT", "ISPREEDIT");
    }
    protected void btnExcelExpt_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=WIPtodayReport.xlsx");
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            System.IO.StringWriter strwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
            HtmlForm htmlfrm = new HtmlForm();
            gvWIPtoday.Parent.Controls.Add(htmlfrm);
            htmlfrm.Attributes["runat"] = "server";
            htmlfrm.Controls.Add(gvWIPtoday);
            htmlfrm.RenderControl(txtwriter);
            Response.Write(strwriter.ToString());
            Response.End();
          
        }
        catch (Exception ex)
        {
           // Response.Write(ex.Message);
        }
    }
  

    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            datasourceIBSQL ibSql = new datasourceIBSQL();
            GetDownloadPath(gvWIPtoday, "Article Due");

            
            

            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment;filename=WIPtodayReport.xls");
            //Response.ContentType = "application/ms-excel";

            //System.IO.StringWriter strwriter = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
            //HtmlForm htmlfrm = new HtmlForm();
            //gvWIPtoday.Parent.Controls.Add(htmlfrm);
            //htmlfrm.Attributes["runat"] = "server";
            //htmlfrm.Controls.Add(gvWIPtoday);
            //htmlfrm.RenderControl(txtwriter);
            //Response.Write(strwriter.ToString());
            //Response.End();

        }
        catch (Exception ex)
        {
            // Response.Write(ex.Message);
        }
    }

    protected void exportExcel_selectedcolumns_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=WIPtodayReport.xls");
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter strwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
            HtmlForm htmlfrm = new HtmlForm();
            gvWIPtoday.Parent.Controls.Add(htmlfrm);
            htmlfrm.Attributes["runat"] = "server";
            gvWIPtoday.Columns[0].Visible = false;
            gvWIPtoday.Columns[1].Visible = false;
            gvWIPtoday.Columns[12].Visible = false;
            gvWIPtoday.Columns[12].Visible = false;
            gvWIPtoday.Columns[13].Visible = false;
            gvWIPtoday.Columns[14].Visible = false;
            //gvWIPtoday.HeaderRow.FindControl("JOB NUMBER").Visible = false;
            //gvWIPtoday.HeaderRow.FindControl("HOLD DETAILS").Visible = false;
            //gvWIPtoday.HeaderRow.FindControl("PRE-EDIT").Visible = false;
            //gvWIPtoday.HeaderRow.FindControl("EARLY XML").Visible = false;
            //gvWIPtoday.HeaderRow.FindControl("TYPESETTING").Visible = false;
            htmlfrm.Controls.Add(gvWIPtoday);
            htmlfrm.RenderControl(txtwriter);
            Response.Write(strwriter.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            // Response.Write(ex.Message);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void GetDownloadPath(GridView ProductionReport, string strFileName)
    {
        string strPath = "";
        StringBuilder strVal = new StringBuilder();
        int intColumns = 0;
        try
        {

            for (int i = 0; i < ProductionReport.Columns.Count; i++)
            {
                if (ProductionReport.Columns[i].Visible == true)
                {
                    intColumns = intColumns + 1;
                }
            }

            strVal.Append("<table width='100%' align='Center' style='border:1px solid green;'>");
            strVal.Append("<tr><td align='center'  style='font-weight: bold;font-size: 11px;font-family: Segoe UI;'colspan=" + intColumns.ToString() + ">" + strFileName + "</td></tr>");
            strVal.Append("<tr>");
            for (int i = 0; i < ProductionReport.Columns.Count; i++)
            {
                if (ProductionReport.Columns[i].Visible == true)
                {
                    strVal.Append("<td align='center' width='" + ProductionReport.Columns[i].ItemStyle.Width + "px'  style='font-weight: bold;font-size: 11px;font-family: Segoe UI;color: #FFF;background:green;'>" + ProductionReport.Columns[i].HeaderText + "</td>");
                }
            }
            strVal.Append("</tr>");

            for (int j = 0; j < ProductionReport.Rows.Count; j++)
            {
                try
                {

                    strVal.Append("<tr>");

                    Boolean blnHold = false;

                    if (Convert.ToString(ProductionReport.Rows[j].Cells[12].Text) != "&nbsp;")
                    {
                        blnHold = true;
                    }
                    

                    for (int i = 0; i < ProductionReport.Columns.Count; i++)
                    {
                        if (ProductionReport.Columns[i].Visible == true)
                        {

                            if ((j + 1) % 2 == 0)
                            {
                                if (i == 0)
                                {
                                    if (blnHold == true)
                                    {
                                        strVal.Append("<td align='center' style='border: 1px solid green;background:pink;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                                    }
                                    else
                                    {
                                        strVal.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                                    }
                                }
                                else
                                {
                                    if (blnHold == true)
                                    {
                                        if (i >= 9 && i <= 11 && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "&nbsp;" && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "")
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:pink;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToDateTime(ProductionReport.Rows[j].Cells[i].Text).ToString("dd-MM-yyyy") + "</td>");
                                        }
                                        else
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:pink;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                                        }
                                    }
                                    else
                                    {
                                        if (i >= 9 && i <= 11 && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "&nbsp;" && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "")
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToDateTime(ProductionReport.Rows[j].Cells[i].Text).ToString("dd-MM-yyyy") + "</td>");
                                        }
                                        else
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    if (blnHold == true)
                                    {
                                        strVal.Append("<td align='center' style='border: 1px solid green;background:pink;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                                    }
                                    else
                                    {
                                        strVal.Append("<td align='center' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                                    }
                                }
                                else
                                {
                                    if (blnHold == true)
                                    {
                                        if (i >= 9 && i <= 11 && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "&nbsp;" && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "")
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:pink;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToDateTime(ProductionReport.Rows[j].Cells[i].Text).ToString("dd-MM-yyyy") + "</td>");
                                        }
                                        else
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:pink;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                                        }
                                    }
                                    else
                                    {
                                        if (i >= 9 && i <= 11 && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "&nbsp;" && Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) != "")
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToDateTime(ProductionReport.Rows[j].Cells[i].Text).ToString("dd-MM-yyyy") + "</td>");
                                        }
                                        else
                                        {
                                            strVal.Append("<td align='left' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                                        }
                                    }
                                }
                            }



                        }
                    }
                }
                catch (Exception Ex)
                {
                    string strRow = j.ToString();
                }

                strVal.Append("</tr>");
            }
            strVal.Append("</table>");

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "WIPtodayReport.xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(strVal.ToString());
            Response.End();


            //return strVal.ToString();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}



