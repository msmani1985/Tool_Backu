using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LP_DelReport : System.Web.UI.Page
{
    Non_Launch NL = new Non_Launch();
    string userName = "dp0934";
    string domain = "dpindia";
    string password = "KMS934";
    private static DataTable dtable4 = new DataTable();
    static string jobLang_selectvalue = "";
    static string jobSoft_selectvalue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        if (!Page.IsPostBack)
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
        }
    }

    private void LoadRecords()
    {
        DataSet ds = new DataSet();
        string strStartDate = "";
        string strEndDate = "";
        try
        {
            if (Convert.ToString(txtStartDate.Text).Trim() == "")
            {
                lblError.Text = "Please select start date.";
                return;
            }
            else
            {
                strStartDate = Convert.ToString(txtStartDate.Text).Trim();
            }

            if (Convert.ToString(txtEndDate.Text).Trim() == "")
            {
                lblError.Text = "Please select end date.";
                return;
            }
            else
            {
                strEndDate = Convert.ToString(txtEndDate.Text).Trim();
            }
            ds = NL.GetDeliveryReport(strStartDate, strEndDate);
            if (ds != null)
            {
                Session["DelReport_Launch"] = ds.Tables[0];
                grdDeliveryReport.DataSource = ds;
                grdDeliveryReport.DataBind();
                dtable4 = ds.Tables[0].Copy();
                lblPages.Text = ds.Tables[3].Rows[0]["pages"].ToString();
                lblTPages.Text = ds.Tables[2].Rows[0]["Totalpages"].ToString();
            }
            else
            {
                grdDeliveryReport.DataSource = null;
                grdDeliveryReport.DataBind();
                dtable4 = null;
            }

            int intTotal = 0;
            if (ds != null)
            {
                if (ds.Tables.Contains("GetDetails"))
                {
                    intTotal = ds.Tables["GetDetails"].Rows.Count;
                    LblTotal.Text = Convert.ToString(intTotal);
                }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        LoadRecords();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        int i = 1;
        if (dtable4 != null && dtable4.Rows.Count > 0)
        {
            string strPath = "";
            StringBuilder strVal = new StringBuilder();
            int intColumns = 0;
            for (int j = 0; j < grdDeliveryReport.Columns.Count; j++)
            {
                if (grdDeliveryReport.Columns[j].Visible == true)
                {
                    intColumns = intColumns + 1;
                }
            }
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table width='100%' align='Center' style='border:1px solid green;'>");
            sbData.Append("<tr><td align='center'  style='font-weight: bold;font-size: 11px;font-family: Segoe UI;'colspan=" + intColumns.ToString() + ">" + "Delivery Report" + "</td></tr>");
            sbData.Append("<tr>");
            for (int k = 0; k < grdDeliveryReport.Columns.Count; k++)
            {
                if (grdDeliveryReport.Columns[k].Visible == true)
                {
                    sbData.Append("<td align='center' width='" + grdDeliveryReport.Columns[k].ItemStyle.Width + "px'  style='font-weight: bold;font-size: 11px;font-family: Segoe UI;color: #FFF;background:green;'>" + grdDeliveryReport.Columns[k].HeaderText + "</td>");
                }
            }
            sbData.Append("</tr>");
            foreach (DataRow r in dtable4.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + i + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["Jobid"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["PROJECTNAME"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["Files_Name"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["AmendName"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["Pages"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["EmpName"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["DateTime_ISTs"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["Despatch_Dates"] + "</td>");
                if (r["DateTime_IST"].ToString() != "")
                {
                    if (Convert.ToDateTime(r["Despatch_Date"].ToString()) > Convert.ToDateTime(r["DateTime_IST"].ToString()))
                    {
                        sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;color: #F90505;font-family: Segoe UI;' >" + "Delay" + "</td>");
                        //lblStatus.CssClass = "gridD";
                    }
                    else
                    {
                        sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;color: #16C103;font-family: Segoe UI;' >" + "Early" + "</td>");
                        //lblStatus.CssClass = "gridE";
                    }
                }
                else
                {
                    sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;color: #FFFFFF;font-family: Segoe UI;' >" + "" + "</td>");
                }
                
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["CUSTNAME"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["LOCATION_NAME"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["TASKNAME"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["SOFT_NAME"] + "</td>");
                sbData.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + r["LANG_NAME"] + "</td>");
                sbData.Append("</tr>");
                i = i + 1;
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Delivery_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            Response.Write(sbData.ToString());
            Response.Flush();
            Response.Close();
        }
    }
    protected void grdDeliveryReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDesDate = e.Row.FindControl("lblDesDates") as Label;
            Label lblActDueDate = e.Row.FindControl("lblActDueDates") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;
            if (lblActDueDate.Text != "")
            {
                if (Convert.ToDateTime(lblDesDate.Text) > Convert.ToDateTime(lblActDueDate.Text))
                {
                    lblStatus.Text = "Delay";
                    lblStatus.CssClass = "gridD";
                }
                else
                {
                    lblStatus.Text = "Early";
                    lblStatus.CssClass = "gridE";
                }
            }
            else
            {
                lblStatus.Text = "";
            }
            //lblDesDate.Text=Convert.ToDateTime(DateTime.ParseExact(lblDesDate.Text, "dd/MM/yyyy HH:mm:ss.fff", null)).ToString();
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList dd_Lang = (DropDownList)e.Row.FindControl("dd_LANGNAME");
            DropDownList dd_Soft = (DropDownList)e.Row.FindControl("dd_SoftName");
            DataSet dd_ds = new DataSet();
            DataTable dd_dt = null;
            DataView oV = null;
            if (Session["DelReport_Launch"] != null)
            {
                dd_dt = (DataTable)Session["DelReport_Launch"];
            }
            if (dd_Lang != null)
            {
                dd_Lang.DataTextField = "LANG_NAME";
                dd_Lang.DataValueField = "LANG_NAME";

                oV = new DataView(dd_dt);
                DataTable jstable = oV.ToTable(true, "LANG_NAME");
                DataRow selerow = jstable.NewRow();
                selerow["LANG_NAME"] = "All Language";
                jstable.Rows.InsertAt(selerow, 0);

                dd_Lang.DataSource = jstable;
                dd_Lang.DataBind();
                if (jobLang_selectvalue != "")
                    dd_Lang.SelectedValue = jobLang_selectvalue;
                else
                    dd_Lang.SelectedValue = "All Language";

            }
            if (dd_Soft != null)
            {
                dd_Soft.DataTextField = "Soft_Name";
                dd_Soft.DataValueField = "Soft_Name";

                oV = new DataView(dd_dt);
                DataTable jstable1 = oV.ToTable(true, "Soft_Name");
                DataRow selerow1 = jstable1.NewRow();
                selerow1["Soft_Name"] = "All Software";
                jstable1.Rows.InsertAt(selerow1, 0);

                dd_Soft.DataSource = jstable1;
                dd_Soft.DataBind();
                if (jobSoft_selectvalue != "")
                    dd_Soft.SelectedValue = jobSoft_selectvalue;
                else
                    dd_Soft.SelectedValue = "All Software";

            }
        }
        
    }
    protected void dd_SoftName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = new DataTable();
        DataView dv = new DataView();
        dset = (DataTable)Session["DelReport_Launch"];
        dv = dset.DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[Soft_Name]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        jobSoft_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobLang_selectvalue = "All Language";
        int p = 0;
        for (int i = 0; i < dv.Count; i++)
        {
            if (p == 0)
            {
                p = Convert.ToInt32(dv[i]["Pages"].ToString());
            }
            else
            {
                p = p + Convert.ToInt32(dv[i]["Pages"].ToString());
            }
        }
        lblTPages.Text = p.ToString();
        grdDeliveryReport.DataSource = dv;
        grdDeliveryReport.DataBind();
    }
    protected void dd_LANGNAME_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = new DataTable();
        DataView dv = new DataView();
        dset = (DataTable)Session["DelReport_Launch"];
        dv = dset.DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[LANG_NAME]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        jobLang_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobSoft_selectvalue = "All Software";

        int p = 0;
        for (int i = 0; i < dv.Count; i++)
        {
            if (p == 0)
            {
                p = Convert.ToInt32(dv[i]["Pages"].ToString());
            }
            else
            {
                p = p + Convert.ToInt32(dv[i]["Pages"].ToString());
            }
        }

        grdDeliveryReport.DataSource = dv;
        grdDeliveryReport.DataBind();
        
        lblTPages.Text = p.ToString();
    }
}