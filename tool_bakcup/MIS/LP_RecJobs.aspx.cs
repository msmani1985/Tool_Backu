using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LP_RecJobs : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    private static DataTable dtable4 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Page.Header.DataBind();
            if (!Page.IsPostBack)
            {
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                LoadCustomer();
            }

        }
        catch (Exception Ex)
        {
            lblError.Text = "Error loading details.";
        }
    }
    private void LoadCustomer()
    {
        DataSet Ds = new DataSet();
        try
        {
            Ds = nonLa.getAllCustomers();
            lstCustomer.DataSource = Ds;
            lstCustomer.DataTextField = Ds.Tables[0].Columns[1].ToString();
            lstCustomer.DataValueField = Ds.Tables[0].Columns[0].ToString();
            lstCustomer.DataBind();
            lstCustomer.Items.Insert(0, new ListItem("-- All --", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            LoadRecords();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error loading records.";
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            LoadCustomer();
            grdProductionReport.DataSource = null;
            grdProductionReport.DataBind();

        }
        catch (Exception Ex)
        {
            lblError.Text = "Error clear the details.";
        }
    }
    private void LoadRecords()
    {
        DataSet dsProductionReport = new DataSet();
        string strStartDate = "";
        string strEndDate = "";
        string strCustomer = "";
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

            Boolean blnSelected = false;
            foreach (ListItem listItem in lstCustomer.Items)
            {
                if (listItem.Selected)
                {
                    blnSelected = true;
                    if (listItem.Value != "0")
                    {
                        if (strCustomer.Trim().Length == 0)
                        {
                            strCustomer =  Convert.ToString(listItem.Value) ;
                        }
                        else
                        {
                            strCustomer = strCustomer + "," + Convert.ToString(listItem.Value) ;
                        }
                    }
                }
            }

            if (blnSelected == false)
            {
                lblError.Text = "Please select customer.";
                return;
            }
            dsProductionReport = nonLa.GetLPRecJobList(strCustomer, strStartDate, strEndDate);
            grdProductionReport.DataSource = dsProductionReport;
            grdProductionReport.DataBind();
            dtable4 = dsProductionReport.Tables[0].Copy();
            if (dsProductionReport.Tables[0].Rows.Count == 0)
            {
                lblError.Text = "No records found.";
            }
            else
            {
                lblError.Text = "";
            }

        }
        catch (Exception Ex)
        {

            throw Ex;
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 1;
            if (dtable4 != null && dtable4.Rows.Count > 0)
            {
                StringBuilder sbData = new StringBuilder();
                sbData.Append("<table border='1'>");
                sbData.Append("<tr valign='top'><td colspan='15' align='center'><h4>Launch Received Report</h4></td><tr>");
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
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_Received_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                Response.Write(sbData.ToString());
                Response.Flush();
                Response.Close();
            }
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error in file downloading";
        }
    }
    protected void grdProductionReport_RowDataBound(object sender, GridViewRowEventArgs e)
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

            HiddenField pro_id = e.Row.FindControl("hfgvNLID") as HiddenField;
            DropDownList status = e.Row.FindControl("DropStatus") as DropDownList;
            DataSet ds = new DataSet();
            ds = nonLa.GetDeliveryStatus(Convert.ToInt32(pro_id.Value));
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
            {
                e.Row.Cells[11].CssClass = "gridP";
            }
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "C")
            {
                e.Row.Cells[11].CssClass = "gridC";
            }
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del")
                e.Row.Cells[11].CssClass = "gridDel";
            else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "WIP")
                e.Row.Cells[11].CssClass = "gridWIP";
        }
    }
}