using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PR_Issue_Dispatched : System.Web.UI.Page
{
    datasourceIBSQL ibSql = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Header.DataBind();
            if (!Page.IsPostBack)
            {
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                LoadCustomer();
                lstCustomer.SelectedValue = "0";
                RdbInvoicing.SelectedValue = "0";
                RdbIssueStage.SelectedValue = "0";
                LoadRecords(0);
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
            lstCustomer.DataValueField = "FINSITENO";
            lstCustomer.DataTextField = "CUSTNAME";
            lstCustomer.DataBind();
            lstCustomer.Items.Insert(0, new ListItem("All", "0"));
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
            LoadRecords(1);
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
            lstCustomer.SelectedValue = "0";
            RdbInvoicing.SelectedValue = "0";
            RdbIssueStage.SelectedValue = "0";
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
        string strStartDate = "";
        string strEndDate = "";
        string strCustomer = "";
        string strOptIsues = "";
        string strOpt = "";
        LblTotal.Text = Convert.ToString(0);
        lblOverDue.Text = Convert.ToString(0);
        lblAhead.Text = Convert.ToString(0);
        lblTotalPages.Text = Convert.ToString(0);
        try
        {
            if (intMode == 1)
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

                strOptIsues = RdbInvoicing.SelectedValue.ToString();
                strOpt = RdbIssueStage.SelectedValue.ToString();

            }
            dsProductionReport = ibSql.GetIssueDispatched(intMode, strStartDate, strEndDate, strCustomer, strOptIsues, strOpt);
            grdProductionReport.DataSource = dsProductionReport;
            grdProductionReport.DataBind();

            int intOverDueCount = 0;
            int intTotal = 0;
            int intAheadConubt = 0;
            int intTotalPages = 0;
            if (dsProductionReport != null)
            {
                if (dsProductionReport.Tables.Contains("GetDetails"))
                {
                    intTotal = dsProductionReport.Tables["GetDetails"].Rows.Count;
                    DataRow[] drOverDue = dsProductionReport.Tables["GetDetails"].Select("OVERDUE='Y'");
                    intOverDueCount = drOverDue.Count();
                    intAheadConubt = intTotal - intOverDueCount;
                    if (intTotal > 0)
                    {
                        intTotalPages = Convert.ToInt32(dsProductionReport.Tables["GetDetails"].Compute("Sum(TOTALISSUEPAGES)", ""));
                    }
                    LblTotal.Text = Convert.ToString(intTotal);
                    lblOverDue.Text = Convert.ToString(intOverDueCount);
                    lblAhead.Text = Convert.ToString(intAheadConubt);
                    lblTotalPages.Text = Convert.ToString(intTotalPages);
                }

            }
            //
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ibSql = new datasourceIBSQL();
            string strDownloadPath = ibSql.GetDownloadPath(grdProductionReport, "Issue Dispatched");
            lblError.Text = "File download in to " + strDownloadPath.ToString();
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error in file downloading";
        }
        //Response.Clear();
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ArticleReceived_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
        //Response.ContentType = "application/ms-excel";
        //Response.Write(strVal.ToString());
        //Response.End();

    }
    protected void grdProductionReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblPages = (Label)e.Row.FindControl("lblPages");
        //        Label lblPages1 = (Label)e.Row.FindControl("lblPages1");
        //        Label lblPages2 = (Label)e.Row.FindControl("lblPages2");
        //        if (Convert.ToString(e.Row.Cells[8]) == "470" || Convert.ToString(e.Row.Cells[8]) == "472" || Convert.ToString(e.Row.Cells[8]) == "10063" || Convert.ToString(e.Row.Cells[8]) == "10091")
        //        {
        //            lblPages.Visible = false;
        //            lblPages1.Visible = false;
        //            lblPages2.Visible = true;
        //        }
        //        else if (Convert.ToString(e.Row.Cells[8]) == "10048")
        //        {
        //            lblPages.Visible = false;
        //            lblPages1.Visible = true;
        //            lblPages2.Visible = false;
        //        }
        //        else
        //        {
        //            lblPages.Visible = true;
        //            lblPages1.Visible = false;
        //            lblPages2.Visible = false;
        //        }


        //    }
        //}
        //catch (Exception Ex)
        //{
        //    throw Ex;
        //}
    }
}