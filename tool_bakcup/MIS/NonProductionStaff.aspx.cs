using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Drawing;
using System.IO;
public partial class NonProductionStaff : System.Web.UI.Page
{
    SqlConnection scon;
    HRMS_CMB cmb = new HRMS_CMB();
    HRMS_CH ch = new HRMS_CH();
    private static string sSortExpression = "";
    private static DataTable dtEmp = new DataTable();
    public double cl;
    public double sl;
    datasourceSQL SqlObj = new datasourceSQL();
    decimal p, s, c, pm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnSave.Visible = true;
            btnClear.Visible = true;
            btnSubmit.Visible = true;
            Page.Header.DataBind();    
        }
        this.showPanel(div_Summary_details);
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "div_Summary_details":
                miSummaryDetails.Attributes.Add("class", "current");
                miEmpDetails.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "");
                this.div_Summary_details.Visible = true;
                this.div_employee_details.Visible = false;
                this.div_FeedBack_details.Visible = false;
                break;
            case "div_employee_details":
                miSummaryDetails.Attributes.Add("class", "");
                miEmpDetails.Attributes.Add("class", "current");
                miFeedbackDetails.Attributes.Add("class", "");
                this.div_Summary_details.Visible = false;
                this.div_employee_details.Visible = true;
                this.div_FeedBack_details.Visible = false;
                break;
            case "div_FeedBack_details":
                miSummaryDetails.Attributes.Add("class", "");
                miEmpDetails.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "current");
                this.div_Summary_details.Visible = false;
                this.div_employee_details.Visible = false;
                this.div_FeedBack_details.Visible = true;
                break;
        }
    }
    protected void GvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvid")).Value.Trim() + "\",\"" + ((HiddenField)e.Row.FindControl("hfgvempid")).Value.Trim() + "\",\"" + ((HiddenField)e.Row.FindControl("hfReviewPeriod")).Value.Trim() + "\");";

        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet rptDs = new DataSet();
        if (rbLocation.SelectedValue == "2")
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCH"].ConnectionString);
        else
            scon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSQLHRMSCMB"].ConnectionString);

        scon.Open();
        SqlCommand cmdsu = new SqlCommand("sp_GetEmpDetailsNonProd_test", scon);
        cmdsu.CommandType = CommandType.StoredProcedure;
        cmdsu.Parameters.Add("@empid", SqlDbType.VarChar).Value = txtEmp.Text;
        cmdsu.Parameters.Add("@teamid", SqlDbType.VarChar).Value = Session["employeeid"].ToString();
        if (mypMonthYear.Value != "")
        {
            string[] monthyear = mypMonthYear.Value.Split('/');
            cmdsu.Parameters.Add("@month", SqlDbType.VarChar).Value = monthyear[0].ToString();
            cmdsu.Parameters.Add("@year", SqlDbType.VarChar).Value = monthyear[1].ToString();
        }
        else
        {
            cmdsu.Parameters.Add("@month", SqlDbType.VarChar).Value = "0";
            cmdsu.Parameters.Add("@year", SqlDbType.VarChar).Value = "0";
        }
        cmdsu.ExecuteNonQuery();
        SqlDataAdapter daRpt = new SqlDataAdapter(cmdsu);
        daRpt.Fill(rptDs);
        dtEmp = rptDs.Tables[0].Copy();
        GvEmp.DataSource = rptDs;
        GvEmp.DataBind();
        this.showPanel(div_Summary_details);
    }
    private void loadDetails()
    {
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        DataSet SqlDs = new DataSet();
        if (rbLocation.SelectedValue == "2")
        {
            HRMS_CH hrCh = new HRMS_CH();
            SqlDs = hrCh.GetEmpDetail(hfp_EmpID.Value);
        }
        else
        {
            HRMS_CMB hrCh = new HRMS_CMB();
            SqlDs = hrCh.GetEmpDetail(hfp_EmpID.Value);
        }
        
        btnClear_Click(null, null);
        DataRow row = SqlDs.Tables[0].Rows[0];
        txtEmpid.Text = row["Refno"].ToString();
        txtEmpName.Text = row["empname"].ToString();
        txtDepart.Text = row["Department"].ToString();
        txtDesign.Text = row["Designation"].ToString();
        if (hfP_Name.Value != "")
        {
            txtReviewPrd.Text = hfP_Name.Value;
        }
        DataSet ds1 = new DataSet();
        ds1 = dsSQL.spGetEmp(hfP_ID.Value);
        if (ds1 != null)
        {
            txtReportTo.Text = ds1.Tables[0].Rows[0]["EmpName"].ToString();
        }
        ds1 = dsSQL.spGetApp(hfP_ID.Value);
        if (ds1 != null || ds1.Tables[0].Rows.Count>0)
        {
            if (ds1.Tables[0].Rows[0]["AppName"].ToString() != "")
                dropAppName.SelectedValue = ds1.Tables[0].Rows[0]["AppName"].ToString();
            else
                dropAppName.SelectedValue = "0";
        }
        if (txtReviewPrd.Text != "")
        {
            //string[] monthyear = mypMonthYear.Value.Split('/');
            //txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(monthyear[1].ToString()) - 1) + '-' + monthyear[1].ToString().Substring(2,2));
        }
        else
        {
            txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));
        }
        DataSet ds = new DataSet();
        ds = dsSQL.NONPRODUCTION(hfP_ID.Value, txtReviewPrd.Text);
        if(ds!=null)
        {
            DataRow r = ds.Tables[0].Rows[0];
            if (r["ReviewDate"].ToString() == "")
                txtRecDate.Text = "";
            else
                txtRecDate.Text = DateTime.Parse(r["ReviewDate"].ToString()).ToShortDateString();

            dropAppName.SelectedValue = r["AppraiserName"].ToString();
            txtReviewPrd.Text = r["ReviewPeriod"].ToString();
            A1.SelectedValue = r["A1"].ToString();
            A1Desc.Text = r["A1Desc"].ToString();
            A2.SelectedValue = r["A2"].ToString();
            A2Desc.Text = r["A2Desc"].ToString();
            A3.SelectedValue = r["A3"].ToString();
            A3Desc.Text = r["A3Desc"].ToString();
            A4.SelectedValue = r["A4"].ToString();
            A4Desc.Text = r["A4Desc"].ToString();
            A5.SelectedValue = r["A5"].ToString();
            A5Desc.Text = r["A5Desc"].ToString();
            A6.SelectedValue = r["A6"].ToString();
            A6Desc.Text = r["A6Desc"].ToString();
            A7.SelectedValue = r["A7"].ToString();
            A7Desc.Text = r["A7Desc"].ToString();
            A8.SelectedValue = r["A8"].ToString();
            A8Desc.Text = r["A8Desc"].ToString();
            A9.SelectedValue = r["A9"].ToString();
            A9Desc.Text = r["A9Desc"].ToString();
            A10.SelectedValue = r["A10"].ToString();
            A10Desc.Text = r["A10Desc"].ToString();
            A11.SelectedValue = r["A11"].ToString();
            A11Desc.Text = r["A11Desc"].ToString();
            txtATotal.Text = r["AT"].ToString();
            B12.SelectedValue = r["B12"].ToString();
            B12Desc.Text = r["B12Desc"].ToString();
            B13.SelectedValue = r["B13"].ToString();
            B13Desc.Text = r["B13Desc"].ToString();
            B14.SelectedValue = r["B14"].ToString();
            B14Desc.Text = r["B14Desc"].ToString();
            B15.SelectedValue = r["B15"].ToString();
            B15Desc.Text = r["B15Desc"].ToString();
            txtBTotal.Text = r["BT"].ToString();
            C16.SelectedValue = r["C16"].ToString();
            C16Desc.Text = r["C16Desc"].ToString();
            C17.SelectedValue = r["C17"].ToString();
            C17Desc.Text = r["C17Desc"].ToString();
            C18.SelectedValue = r["C18"].ToString();
            C18Desc.Text = r["C18Desc"].ToString();
            txtCTotal.Text = r["CT"].ToString();
            D19.SelectedValue = r["D19"].ToString();
            D19Desc.Text = r["D19Desc"].ToString();
            D20.SelectedValue = r["D20"].ToString();
            D20Desc.Text = r["D20Desc"].ToString();
            txtDTotal.Text = r["DT"].ToString();
            txtOverAll.Text = r["OVERALL"].ToString();
            txtAppRemarks.Text = r["REMARKS"].ToString();
            txtActionPlan.Text = r["ACTION_PLAN"].ToString();
            txtEmpComments.Text = r["EMP_COMMENTS"].ToString();

            txtStrength.Text = r["Strength"].ToString();
            txtWeakness.Text = r["Weakness"].ToString();
            decimal O = Convert.ToDecimal(txtOverAll.Text);
            if (O < 21)
                lblGrade.Text = "Below Expectation (0)";
            else if (O < 41)
                lblGrade.Text = "Improvement Needed (1)";
            else if (O < 61)
                lblGrade.Text = "Meets Expectation (2)";
            else if (O < 81)
                lblGrade.Text = "Exceeds Expectation (3)";
            else if (O <= 100)
                lblGrade.Text = "Expert (4)";
            else
                lblGrade.Text = "";


            if (r["SubmitBy"].ToString() != "")
            {
                if (Session["Employeeid"].ToString() == "2056" || Session["Employeeid"].ToString() == "2518")
                {
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                btnSave.Visible = true;
                btnClear.Visible = true;
                btnSubmit.Visible = true;
            }
        }
        else
        {
            if (mypMonthYear.Value != "")
            {
                string[] monthyear = mypMonthYear.Value.Split('/');
                txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(monthyear[1].ToString()) - 1) + '-' + monthyear[1].ToString().Substring(2, 2));
            }
            else
            {
                txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));
            }
        }


        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));

        //if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
        //{
        //    if ("~/Photos/Chennai/" + row["Refno"].ToString() + ".jpg" == null)
        //        imgPhoto.ImageUrl = "~/Photos/Chennai/index.jpg";
        //    else
        //        imgPhoto.ImageUrl = "~/Photos/Chennai/" + row["Refno"].ToString() + ".jpg";
        //}
        //else
        //{
        //    if ("~/Photos/CMB/" + row["Refno"].ToString() + ".jpg" == null)
        //        imgPhoto.ImageUrl = "~/Photos/CMB/index.jpg";
        //    else
        //        imgPhoto.ImageUrl = "~/Photos/CMB/" + row["Refno"].ToString() + ".jpg";
        //}
    }
    protected void lnkSummaryDetails_Click(object sender, EventArgs e)
    {
        this.showPanel(div_Summary_details);
    }
    protected void lnkEmpDetails_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        DataSet SqlDs = new DataSet();
        if (rbLocation.SelectedValue == "2")
        {
            SqlDs = dsSQL.spGetAppList(rbLocation.SelectedValue);
            LinkButton1.Visible = false;
            LinkButton2.Visible = false;
        }
        else
        {
            SqlDs = dsSQL.spGetAppList(rbLocation.SelectedValue);
            LinkButton1.Visible = true;
            //if ((DateTime.Now.Year * 12 + DateTime.Now.Month) > 24191)
            //{
            //    LinkButton2.Visible = true;
            //}
            //else
            //{
                LinkButton2.Visible = false;
            //}
        }
        if (SqlDs != null)
        {
            dropAppName.DataSource = SqlDs;
            dropAppName.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
            dropAppName.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
            dropAppName.DataBind();
            dropAppName.Items.Insert(0, new ListItem("-- select --", "0"));
        }
        else
        {
            dropAppName.Items.Clear();
            dropAppName.DataSource = null;
            dropAppName.DataBind();
            dropAppName.Items.Insert(0, new ListItem("-- select --", "0"));
        }

        if (hfP_ID.Value != "")
        {
            loadDetails();
        }
        else
        {
            btnClear_Click(null, null);
            txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));
        }
        this.showPanel(div_employee_details);
    }
    protected void lnkFeedbackDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        DataSet SqlDs = new DataSet();
        if(hfP_ID.Value!="")
        {
            if (hfP_Name.Value != "")
            {
                ds = dsSQL.NONPRODUCTION(hfP_ID.Value, hfP_Name.Value);
            }
            else
            {
                ds = dsSQL.spGetID(hfP_ID.Value);
            }

            string pid = "";
            if (ds != null)
            {
                hfp_NP_ID.Value = ds.Tables[0].Rows[0]["NP_ID"].ToString();
                if (hfp_NP_ID.Value != "")
                {
                    pid = hfp_NP_ID.Value;
                }
                else
                {
                    pid = "0";
                }
            }
            else
            {
                pid = "0";
            }
            ds = dsSQL.spGetNonProdFeedback(hfP_ID.Value, pid);

            if (ds != null)
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
            }
            else
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
                txtPositive.Text = "";
                txtNegative.Text = "";
            }

            if (rbLocation.SelectedValue == "2")
            {
                HRMS_CH hrCh = new HRMS_CH();
                SqlDs = hrCh.GetEmpDetail(hfp_EmpID.Value);
            }
            else
            {
                HRMS_CMB hrCh = new HRMS_CMB();
                SqlDs = hrCh.GetEmpDetail(hfp_EmpID.Value);
            }
            DataRow row = SqlDs.Tables[0].Rows[0];
            lblFEmpId.Text = row["Refno"].ToString();
            lblFEmpName.Text = row["empname"].ToString();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Select Employee Details!..');</script>");
        }
        this.showPanel(div_FeedBack_details);
    }
    protected void A1_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) + 
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) + 
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A2_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A3_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A4_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A5_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A6_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A7_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A8_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A9_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A10_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void A11_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text) + Convert.ToDecimal(A4.Text) +
                    Convert.ToDecimal(A5.Text) + Convert.ToDecimal(A6.Text) + Convert.ToDecimal(A7.Text) + Convert.ToDecimal(A8.Text) +
                    Convert.ToDecimal(A9.Text) + Convert.ToDecimal(A10.Text) + Convert.ToDecimal(A11.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B12_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B12.Text) + Convert.ToDecimal(B13.Text) + 
                    Convert.ToDecimal(B14.Text) + Convert.ToDecimal(B15.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B13_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B12.Text) + Convert.ToDecimal(B13.Text) +
                    Convert.ToDecimal(B14.Text) + Convert.ToDecimal(B15.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B14_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B12.Text) + Convert.ToDecimal(B13.Text) +
                    Convert.ToDecimal(B14.Text) + Convert.ToDecimal(B15.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B15_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B12.Text) + Convert.ToDecimal(B13.Text) +
                    Convert.ToDecimal(B14.Text) + Convert.ToDecimal(B15.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void C16_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal C = Convert.ToDecimal(C16.Text) + Convert.ToDecimal(C17.Text) +
                    Convert.ToDecimal(C18.Text);
        txtCTotal.Text = C.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void C17_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal C = Convert.ToDecimal(C16.Text) + Convert.ToDecimal(C17.Text) +
                    Convert.ToDecimal(C18.Text);
        txtCTotal.Text = C.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void C18_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal C = Convert.ToDecimal(C16.Text) + Convert.ToDecimal(C17.Text) +
                    Convert.ToDecimal(C18.Text);
        txtCTotal.Text = C.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void D19_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal D = Convert.ToDecimal(D19.Text) + Convert.ToDecimal(D20.Text);
        txtDTotal.Text = D.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void D20_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal D = Convert.ToDecimal(D19.Text) + Convert.ToDecimal(D20.Text);
        txtDTotal.Text = D.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) +
                          Convert.ToDecimal(txtCTotal.Text) + Convert.ToDecimal(txtDTotal.Text);
        txtOverAll.Text = O.ToString();

        if (O < 21)
            lblGrade.Text = "Below Expectation (0)";
        else if (O < 41)
            lblGrade.Text = "Improvement Needed (1)";
        else if (O < 61)
            lblGrade.Text = "Meets Expectation (2)";
        else if (O < 81)
            lblGrade.Text = "Exceeds Expectation (3)";
        else if (O <= 100)
            lblGrade.Text = "Expert (4)";
        else
            lblGrade.Text = "";

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string rdate = "";
        if (txtRecDate.Text != "")
            rdate = DateTime.Parse(txtRecDate.Text.Trim()).ToString("MM/dd/yyyy");
        else
            rdate = "";

        string inproc = "spInsertNonProdStaff";
        string[,] pname = { { "@Employee_ID", hfP_ID.Value}, { "@Employee_Number",txtEmpid.Text }, { "@EmpName",txtEmpName.Text }, 
                            { "@ReviewDate",rdate.ToString() }, { "@ReportTo",txtReportTo.Text },{"@AppraiserName",dropAppName.SelectedValue},
                            {"@ReviewPeriod",txtReviewPrd.Text},{"@A1",A1.SelectedValue},{"@A2",A2.SelectedValue},
                            {"@A3",A3.SelectedValue},{"@A4",A4.SelectedValue},{"@A5",A5.SelectedValue},{"@A6",A6.SelectedValue},
                            {"@A7",A7.SelectedValue},{"@A8",A8.SelectedValue},{"@A9",A9.SelectedValue},{"@A10",A10.SelectedValue},
                            {"@A11",A11.SelectedValue},{"@AT",txtATotal.Text},{"@B12",B12.SelectedValue},{"@B13",B13.SelectedValue},
                            {"@B14",B14.SelectedValue},{"@B15",B15.SelectedValue},{"@BT",txtBTotal.Text},
                            {"@C16",C16.SelectedValue},{"@C17",C17.SelectedValue},{"@C18",C18.SelectedValue},
                            {"@CT",txtCTotal.Text},{"@D19",D19.SelectedValue},{"@D20",D20.SelectedValue},{"@DT",txtDTotal.Text},
                            {"@OVERALL",txtOverAll.Text},{"@REMARKS",txtAppRemarks.Text},{"@ACTION_PLAN",txtActionPlan.Text},
                            {"@EMP_COMMENTS",txtEmpComments.Text},{"@IsExist","OUTPUT"},
                            {"@A1Desc",A1Desc.Text},{"@A2Desc",A2Desc.Text},{"@A3Desc",A3Desc.Text},{"@A4Desc",A4Desc.Text},
                            {"@A5Desc",A5Desc.Text},{"@A6Desc",A6Desc.Text},{"@A7Desc",A7Desc.Text},{"@A8Desc",A8Desc.Text},
                            {"@A9Desc",A9Desc.Text},{"@A10Desc",A10Desc.Text},{"@A11Desc",A11Desc.Text},{"@B12Desc",B12Desc.Text},
                            {"@B13Desc",B13Desc.Text},{"@B14Desc",B14Desc.Text},{"@B15Desc",B15Desc.Text},{"@C16Desc",C16Desc.Text},
                            {"@C17Desc",C17Desc.Text},{"@C18Desc",C18Desc.Text},{"@D19Desc",D19Desc.Text},{"@D20Desc",D20Desc.Text},
                            {"@strength",txtStrength.Text},{"@weakness",txtWeakness.Text}

                          };
        int val = this.SqlObj.ExcSP(inproc, pname, CommandType.StoredProcedure);
        
        if (val == 1)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
        }
        else if (val == 2)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Updated');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Failed!..');</script>");
        }


        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));

        this.showPanel(div_employee_details);

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRecDate.Text = "";
        //dropAppName.SelectedValue = "";
        txtReportTo.Text = "";
        txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));
        A1.SelectedValue = "0.0";
        A1Desc.Text = "";
        A2.SelectedValue = "0.0";
        A2Desc.Text = "";
        A3.SelectedValue = "0.0";
        A3Desc.Text = "";
        A4.SelectedValue = "0.0";
        A4Desc.Text = "";
        A5.SelectedValue = "0.0";
        A5Desc.Text = "";
        A6.SelectedValue = "0.0";
        A6Desc.Text = "";
        A7.SelectedValue = "0.0";
        A7Desc.Text = "";
        A8.SelectedValue = "0.0";
        A8Desc.Text = "";
        A9.SelectedValue = "0.0";
        A9Desc.Text = "";
        A10.SelectedValue = "0.0";
        A10Desc.Text = "";
        A11.SelectedValue = "0.0";
        A11Desc.Text = "";
        txtATotal.Text = "0.0";
        B12.SelectedValue = "0.0";
        B12Desc.Text = "";
        B13.SelectedValue = "0.0";
        B13Desc.Text = "";
        B14.SelectedValue = "0.0";
        B14Desc.Text = "";
        B15.SelectedValue = "0.0";
        B15Desc.Text = "";
        txtBTotal.Text = "0.0";
        C16.SelectedValue = "0.0";
        C16Desc.Text = "";
        C17.SelectedValue = "0.0";
        C17Desc.Text = "";
        C18.SelectedValue = "0.0";
        C18Desc.Text = "";
        txtCTotal.Text = "0.0";
        D19.SelectedValue = "0.0";
        D19Desc.Text = "";
        D20.SelectedValue = "0.0";
        D20Desc.Text = "";
        txtDTotal.Text = "0.0";
        txtOverAll.Text="0.0";
        txtAppRemarks.Text="";
        txtActionPlan.Text="";
        txtEmpComments.Text = "";
        txtRecDate.Text = "";
        //txtAppName.Text = "";
        txtReportTo.Text = "";
        txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));
        lblGrade.Text = "";
        txtEmpid.Text = "";
        txtEmpName.Text = "";
        txtDesign.Text = "";
        txtDepart.Text = "";

        txtStrength.Text = "";
        txtWeakness.Text ="";
        this.showPanel(div_employee_details);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        if (hfp_NP_ID.Value != "")
        {
                
            txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));

            dsSQL.InsertFeedback(hfP_ID.Value, hfp_NP_ID.Value, txtPositive.Text.Replace("'", "''"), txtNegative.Text.Replace("'", "''"));
            dsSQL.UpdateFeedback(hfP_ID.Value, hfp_NP_ID.Value, txtReviewPrd.Text);

            DataSet ds = new DataSet();
            ds = dsSQL.spGetNonProdFeedback(hfP_ID.Value, hfp_NP_ID.Value);
            if (ds != null)
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
                txtPositive.Text="";
                txtNegative.Text = "";
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Review details Not Saved!..');</script>");
        }
        this.showPanel(div_FeedBack_details);
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {

    }
    protected void imgHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void rbLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfp_EmpID.Value = "";
        hfP_ID.Value = "";
        hfP_Name.Value = "";
        hfp_NP_ID.Value = "";
        GvEmp.DataSource = null;
        GvEmp.DataBind();
        this.showPanel(div_Summary_details);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string strPopup = "<script language='javascript' ID='script1'>"

            + "window.open('EmpLeaveYTD.aspx?ID=" + HttpUtility.UrlEncode(txtEmpid.Text)

            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string strPopup = "<script language='javascript' ID='script1'>"

            + "window.open('EmpLateMinYTD.aspx?ID=" + HttpUtility.UrlEncode(txtEmpid.Text)

            + "','new window', 'top=90, left=200, width=950, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

        if (A1Desc.Text.Length < 22)
            A1Desc.Height = 20;
        else if (A1Desc.Text.Length < 75)
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A1Desc.Text.Length));
        else
            A1Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A1Desc.Text.Length));

        if (A2Desc.Text.Length < 22)
            A2Desc.Height = 20;
        else if (A2Desc.Text.Length < 75)
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A2Desc.Text.Length));
        else
            A2Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A2Desc.Text.Length));

        if (A3Desc.Text.Length < 22)
            A3Desc.Height = 20;
        else if (A3Desc.Text.Length < 75)
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A3Desc.Text.Length));
        else
            A3Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A3Desc.Text.Length));

        if (A4Desc.Text.Length < 22)
            A4Desc.Height = 20;
        else if (A4Desc.Text.Length < 75)
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A4Desc.Text.Length));
        else
            A4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A4Desc.Text.Length));

        if (A5Desc.Text.Length < 22)
            A5Desc.Height = 20;
        else if (A5Desc.Text.Length < 75)
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A5Desc.Text.Length));
        else
            A5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A5Desc.Text.Length));

        if (A6Desc.Text.Length < 22)
            A6Desc.Height = 20;
        else if (A6Desc.Text.Length < 75)
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A6Desc.Text.Length));
        else
            A6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A6Desc.Text.Length));

        if (A7Desc.Text.Length < 22)
            A7Desc.Height = 20;
        else if (A7Desc.Text.Length < 75)
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A7Desc.Text.Length));
        else
            A7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A7Desc.Text.Length));

        if (A8Desc.Text.Length < 22)
            A8Desc.Height = 20;
        else if (A8Desc.Text.Length < 75)
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A8Desc.Text.Length));
        else
            A8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A8Desc.Text.Length));

        if (A9Desc.Text.Length < 22)
            A9Desc.Height = 20;
        else if (A9Desc.Text.Length < 75)
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A9Desc.Text.Length));
        else
            A9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A9Desc.Text.Length));

        if (A10Desc.Text.Length < 22)
            A10Desc.Height = 20;
        else if (A10Desc.Text.Length < 75)
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A10Desc.Text.Length));
        else
            A10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A10Desc.Text.Length));

        if (A11Desc.Text.Length < 22)
            A11Desc.Height = 20;
        else if (A11Desc.Text.Length < 75)
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(A11Desc.Text.Length));
        else
            A11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(A11Desc.Text.Length));

        if (B12Desc.Text.Length < 22)
            B12Desc.Height = 20;
        else if (B12Desc.Text.Length < 75)
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B12Desc.Text.Length));
        else
            B12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B12Desc.Text.Length));

        if (B13Desc.Text.Length < 22)
            B13Desc.Height = 20;
        else if (B13Desc.Text.Length < 75)
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B13Desc.Text.Length));
        else
            B13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B13Desc.Text.Length));

        if (B14Desc.Text.Length < 22)
            B14Desc.Height = 20;
        else if (B14Desc.Text.Length < 75)
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B14Desc.Text.Length));
        else
            B14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B14Desc.Text.Length));

        if (B15Desc.Text.Length < 22)
            B15Desc.Height = 20;
        else if (B15Desc.Text.Length < 75)
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B15Desc.Text.Length));
        else
            B15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B15Desc.Text.Length));


        if (C16Desc.Text.Length < 22)
            C16Desc.Height = 20;
        else if (C16Desc.Text.Length < 75)
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C16Desc.Text.Length));
        else
            C16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C16Desc.Text.Length));

        if (C17Desc.Text.Length < 22)
            C17Desc.Height = 20;
        else if (C17Desc.Text.Length < 75)
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C17Desc.Text.Length));
        else
            C17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C17Desc.Text.Length));

        if (C18Desc.Text.Length < 22)
            C18Desc.Height = 20;
        else if (C18Desc.Text.Length < 75)
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C18Desc.Text.Length));
        else
            C18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C18Desc.Text.Length));

        if (D19Desc.Text.Length < 22)
            D19Desc.Height = 20;
        else if (D19Desc.Text.Length < 75)
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D19Desc.Text.Length));
        else
            D19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D19Desc.Text.Length));

        if (D20Desc.Text.Length < 22)
            D20Desc.Height = 20;
        else if (D20Desc.Text.Length < 75)
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D20Desc.Text.Length));
        else
            D20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string inproc = "spUpdateSubmitNonProdStaff";
        string[,] pname = { { "@Employee_ID", hfP_ID.Value }, { "@SubmitBy", Session["Employeeid"].ToString() }, { "@ReviewPeriod", txtReviewPrd.Text.Trim() } };

        int val = this.SqlObj.ExcSP(inproc, pname, CommandType.StoredProcedure);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Submited Successfully');</script>");
        btnSave.Visible = false;
        btnClear.Visible = false;
        btnSubmit.Visible = false;
        this.showPanel(div_employee_details);
    }
    protected void btnClear1_Click(object sender, EventArgs e)
    {
        //UserControl ctrlB = (UserControl)Page.FindControl("mypMonthYear");
        //ctrlB.ClientID = "";
        txtEmp.Text = "";
    }
}