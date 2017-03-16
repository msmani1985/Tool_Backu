﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PR_NonIssuesArticle : System.Web.UI.Page
{
    datasourceIBSQL ibSql = new datasourceIBSQL();
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
            RadioButtonList1.SelectedValue = "0";
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
        string strInvoicing = "";
        string strCustomer = "";
        try
        {
            if (intMode == 1)
            {

                strInvoicing = Convert.ToString(RadioButtonList1.SelectedValue);

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
            dsProductionReport = ibSql.GetProductionReportNIA(intMode, strInvoicing, strCustomer);
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
            string strDownloadPath = ibSql.GetDownloadPath(grdProductionReport, "Non Isssue Article");
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
}