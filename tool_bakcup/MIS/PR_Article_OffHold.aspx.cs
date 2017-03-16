using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PR_Article_OffHold : System.Web.UI.Page
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
                LoadRecords(0);
            }
        }
        catch (Exception Ex)
        {
            lblError.Text = "Error loading details.";
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

            }

            dsProductionReport = ibSql.GetProductionReportArticleOffhold(intMode, strStartDate, strEndDate, strCustomer);
            grdProductionReport.DataSource = dsProductionReport;
            grdProductionReport.DataBind();
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
            string strDownloadPath =ibSql.GetDownloadPath(grdProductionReport, "Article Off Hold");
            //lblError.Text = "File download in to " + strDownloadPath.ToString();
           
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

   
}