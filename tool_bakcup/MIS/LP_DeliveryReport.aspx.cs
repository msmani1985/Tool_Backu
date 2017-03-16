using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using Tools;

public partial class LP_DeliveryReport : System.Web.UI.Page
{
    Non_Launch NL = new Non_Launch();
    string userName = "dp0934";
    string domain = "dpindia";
    string password = "KMS934";
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
            grdDeliveryReport.DataSource = ds;
            grdDeliveryReport.DataBind();

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
        try
        {
            using (new Impersonator(userName, domain, password))
            {
                string strDownloadPath = GetDownloadPath(grdDeliveryReport, "Delivery Report");
                lblError.Text = "File download in to " + strDownloadPath.ToString();
                FileInfo fi = new FileInfo(strDownloadPath.ToString());
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start(strDownloadPath.ToString());
                }
                else
                {
                    //file doesn't exist
                }
            }


        }
        catch (Exception Ex)
        {
            lblError.Text = "Error in file downloading";
        }
    }
    public string GetDownloadPath(GridView ProductionReport, string strFileName)
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
                strVal.Append("<tr>");
                for (int i = 0; i < ProductionReport.Columns.Count; i++)
                {
                    if (ProductionReport.Columns[i].Visible == true)
                    {
                        if ((j + 1) % 2 == 0)
                        {
                            if (i == 0)
                            {
                                strVal.Append("<td align='center' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                            }
                            else
                            {
                                strVal.Append("<td align='left' style='border: 1px solid green;background:#F0FFF0;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                strVal.Append("<td align='center' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(j + 1) + "</td>");
                            }
                            else
                            {
                                strVal.Append("<td align='left' style='border: 1px solid green;background:white;font-size: 11px;font-family: Segoe UI;' >" + Convert.ToString(ProductionReport.Rows[j].Cells[i].Text) + "</td>");
                            }
                        }
                    }
                }
                strVal.Append("</tr>");
            }
            strVal.Append("</table>");
            strPath = @"\\192.9.201.222\Mail\LaunchFiles\" + strFileName + "_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "") + ".xls";
            System.IO.StreamWriter file = new System.IO.StreamWriter(strPath);
            file.WriteLine(strVal);
            file.Close();
            return strPath;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}