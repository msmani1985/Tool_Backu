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
using System.Drawing;
using System.Text;

public partial class PR_Book_Progress : System.Web.UI.Page
{
    datasourceIBSQL ibSql = new datasourceIBSQL();
    private static DataTable dtable1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Header.DataBind();
            if (!Page.IsPostBack)
            {
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
        ibSql = new datasourceIBSQL();
        DataSet dsCustomer = new DataSet();
        try
        {
            dsCustomer = ibSql.GetCustomerDetails(0);
            lstCustomer.DataSource = null;
            lstCustomer.DataSource = dsCustomer;
            lstCustomer.DataValueField = "CUSTNO";
            lstCustomer.DataTextField = "CUSTNAME";
            lstCustomer.DataBind();
            lstCustomer.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            LoadCustomer();
            LoadRecords(0);
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error clear the details.";
        }
    }
    private void LoadRecords(int intMode)
    {
        ibSql = new datasourceIBSQL();
        DataSet dsProductionReport = new DataSet();
        string strCustomer = "";
        try
        {
            if (intMode == 1)
            {
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
                                strCustomer = "'" + Convert.ToString(listItem.Value) + "'";
                            }
                            else
                            {
                                strCustomer = strCustomer + ",'" + Convert.ToString(listItem.Value) + "'";
                            }
                        }
                    }
                }

                if (blnSelected == false)
                {
                    lblError.Text = "Please select customer.";
                    return;
                }
            }
            dsProductionReport = ibSql.GetProductionReportBookProgress(intMode, strCustomer);
            grdProductionReport.DataSource = dsProductionReport;
            grdProductionReport.DataBind();
            dtable1 = dsProductionReport.Tables[0].Copy();
            if (intMode == 1)
            {
                if (dsProductionReport.Tables["GetDetails"].Rows.Count == 0)
                {
                    lblError.Text = "No records found.";
                }
                else
                {
                    lblError.Text = "";
                }
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

    protected void btnRecords_Click1(object sender, EventArgs e)
    {
        try
        {
            LoadRecords(1);
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error loading records.";
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (dtable1 != null && dtable1.Rows.Count > 0)
        {
            DateTime monthStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='14' align='center'><h4>Book InProgress</h4></td><tr>");
            sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Cat#</b></td><td bgcolor='silver'><b>Book Title</b></td><td bgcolor='silver'><b>India Rec'd Date</b></td><td bgcolor='silver'><b>Despatch Date</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Stage</b></td><td bgcolor='silver'><b>No. of Pages</b></td><td bgcolor='silver'><b>Invoiced</b></td><td bgcolor='silver'><b>PE Name</b></td><td bgcolor='silver'><b>Format/Style</b></td><td bgcolor='silver'><b>Price Code</b></td><td bgcolor='silver'><b>Type of Cost</b></td><td bgcolor='silver'><b>Type</b></td></tr>");
            foreach (DataRow r in dtable1.Rows)
            {
                sbData.Append("<tr valign='top'>");
                sbData.Append("<td>" + r["ROWID"] + "</td>");
                sbData.Append("<td>" + r["Bnumber"] + "</td>");
                sbData.Append("<td>" + r["BTITLE"] + "</td>");
                if (r["RecDate"].ToString() == "")
                    sbData.Append("<td>" + r["RecDate"].ToString() + "</td>");
                else
                    sbData.Append("<td>" + DateTime.Parse(r["RecDate"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                if (r["DueDate"].ToString() == "")
                    sbData.Append("<td>" + r["DueDate"].ToString() + "</td>");
                else
                    sbData.Append("<td>" + DateTime.Parse(r["DueDate"].ToString()).ToString("MM/dd/yyyy") + "</td>");
                sbData.Append("<td>" + r["CUSTNAME"] + "</td>");
                sbData.Append("<td>" + r["STYPENAME"] + "</td>");
                sbData.Append("<td>" + r["Pages"] + "</td>");
                sbData.Append("<td>" + r["Invoiced"] + "</td>");
                sbData.Append("<td>" + r["CONSURNAME"] + "</td>");
                sbData.Append("<td>" + r["BSTYLE"] + "</td>");
                sbData.Append("<td>" + r["PriceCode"] + "</td>");
                sbData.Append("<td>" + r["TypeCost"] + "</td>");
                sbData.Append("<td>" + r["Type"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Book_InProgress_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();

        }
    }
}