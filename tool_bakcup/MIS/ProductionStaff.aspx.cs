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

public partial class ProductionStaff : System.Web.UI.Page
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
            Page.Header.DataBind();
            btnSave.Visible = true;
            btnClear.Visible = true;
            btnSubmit.Visible = true;
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
        SqlCommand cmdsu = new SqlCommand("sp_GetEmpDetailsProd_test", scon);
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
        btnClear_Click(null,null);
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
        ds = dsSQL.PRODUCTION(hfP_ID.Value, txtReviewPrd.Text);
        if (ds != null)
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
            B4.SelectedValue = r["B4"].ToString();
            B4Desc.Text = r["B4Desc"].ToString();
            B5.SelectedValue = r["B5"].ToString();
            B5Desc.Text = r["B5Desc"].ToString();
            B6.SelectedValue = r["B6"].ToString();
            B6Desc.Text = r["B6Desc"].ToString();
            C7.SelectedValue = r["C7"].ToString();
            C7Desc.Text = r["C7Desc"].ToString();
            C8.SelectedValue = r["C8"].ToString();
            C8Desc.Text = r["C8Desc"].ToString();
            C9.SelectedValue = r["C9"].ToString();
            C9Desc.Text = r["C9Desc"].ToString();
            D10.SelectedValue = r["D10"].ToString();
            D10Desc.Text = r["D10Desc"].ToString();
            D11.SelectedValue = r["D11"].ToString();
            D11Desc.Text = r["D11Desc"].ToString();
            D12.SelectedValue = r["D12"].ToString();
            D12Desc.Text = r["D12Desc"].ToString();
            D13.SelectedValue = r["D13"].ToString();
            D13Desc.Text = r["D13Desc"].ToString();
            E14.SelectedValue = r["E14"].ToString();
            E14Desc.Text = r["E14Desc"].ToString();
            E15.SelectedValue = r["E15"].ToString();
            E15Desc.Text = r["E15Desc"].ToString();
            E16.SelectedValue = r["E16"].ToString();
            E16Desc.Text = r["E16Desc"].ToString();
            E17.SelectedValue = r["E17"].ToString();
            E17Desc.Text = r["E17Desc"].ToString();
            E18.SelectedValue = r["E18"].ToString();
            E18Desc.Text = r["E18Desc"].ToString();
            F19.SelectedValue = r["F19"].ToString();
            F19Desc.Text = r["F19Desc"].ToString();
            F20.SelectedValue = r["F20"].ToString();
            F20Desc.Text = r["F20Desc"].ToString();

            txtATotal.Text = r["AT"].ToString();
            txtBTotal.Text = r["BT"].ToString();
            txtCTotal.Text = r["CT"].ToString();
            txtDTotal.Text = r["DT"].ToString();
            txtETotal.Text = r["ET"].ToString();
            txtFTotal.Text = r["FT"].ToString();
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
            if (Session["Employeeid"].ToString() == "1847")
            {
                if (r["SubmitBy_M"].ToString() != "")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    btnSubmit.Visible = true;
                }
            }
            if (Session["Employeeid"].ToString() == "2056" || Session["Employeeid"].ToString() == "2518")
            {
                if (r["SubmitBy_GM"].ToString() != "")
                {
                    btnSave.Visible = false;
                    btnClear.Visible = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    btnSubmit.Visible = true;
                }
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        
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
        if (hfP_ID.Value != "")
        {
            if (hfP_Name.Value != "")
            {
                ds = dsSQL.PRODUCTION(hfP_ID.Value, hfP_Name.Value);
            }
            else
            {
                ds = dsSQL.spProdGetID(hfP_ID.Value);
            }

            string pid = "";
            if (ds != null)
            {
                hfp_NP_ID.Value = ds.Tables[0].Rows[0]["P_ID"].ToString();
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
            ds = dsSQL.spGetFeedback(hfP_ID.Value, pid);
            if (ds != null)
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
            }
            else
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
                txtPositive.Text="";
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
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void A1_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                           Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));

        this.showPanel(div_employee_details);
    }
    protected void A2_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                           Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length)); 
        
        this.showPanel(div_employee_details);
    }
    protected void A3_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal A = Convert.ToDecimal(A1.Text) + Convert.ToDecimal(A2.Text) + Convert.ToDecimal(A3.Text);
        txtATotal.Text = A.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                   Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B4_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B4.Text) + Convert.ToDecimal(B5.Text) + Convert.ToDecimal(B6.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                   Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B5_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B4.Text) + Convert.ToDecimal(B5.Text) + Convert.ToDecimal(B6.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                    Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void B6_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal B = Convert.ToDecimal(B4.Text) + Convert.ToDecimal(B5.Text) + Convert.ToDecimal(B6.Text);
        txtBTotal.Text = B.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                   Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void C7_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal C = Convert.ToDecimal(C7.Text) + Convert.ToDecimal(C8.Text) + Convert.ToDecimal(C9.Text);
        txtCTotal.Text = C.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                   Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void C8_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal C = Convert.ToDecimal(C7.Text) + Convert.ToDecimal(C8.Text) + Convert.ToDecimal(C9.Text);
        txtCTotal.Text = C.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                   Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));

        this.showPanel(div_employee_details);
    }
    protected void C9_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal C = Convert.ToDecimal(C7.Text) + Convert.ToDecimal(C8.Text) + Convert.ToDecimal(C9.Text);
        txtCTotal.Text = C.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                   Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void D13_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal D = Convert.ToDecimal(D13.Text) + Convert.ToDecimal(D10.Text) + 
                    Convert.ToDecimal(D11.Text) + Convert.ToDecimal(D12.Text);
        txtDTotal.Text = D.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void D10_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal D = Convert.ToDecimal(D13.Text) + Convert.ToDecimal(D10.Text) +
                    Convert.ToDecimal(D11.Text) + Convert.ToDecimal(D12.Text);
        txtDTotal.Text = D.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void D11_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal D = Convert.ToDecimal(D13.Text) + Convert.ToDecimal(D10.Text) +
                    Convert.ToDecimal(D11.Text) + Convert.ToDecimal(D12.Text);
        txtDTotal.Text = D.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void D12_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal D = Convert.ToDecimal(D13.Text) + Convert.ToDecimal(D10.Text) +
                    Convert.ToDecimal(D11.Text) + Convert.ToDecimal(D12.Text);
        txtDTotal.Text = D.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void E16_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal E = Convert.ToDecimal(E14.Text) + Convert.ToDecimal(E15.Text) +
                    Convert.ToDecimal(E16.Text) + Convert.ToDecimal(E17.Text) + Convert.ToDecimal(E18.Text);
        txtETotal.Text = E.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length)); 
        this.showPanel(div_employee_details);
    }
    protected void E14_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal E = Convert.ToDecimal(E14.Text) + Convert.ToDecimal(E15.Text) +
                    Convert.ToDecimal(E16.Text) + Convert.ToDecimal(E17.Text) + Convert.ToDecimal(E18.Text);
        txtETotal.Text = E.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void E15_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal E = Convert.ToDecimal(E14.Text) + Convert.ToDecimal(E15.Text) +
                    Convert.ToDecimal(E16.Text) + Convert.ToDecimal(E17.Text) + Convert.ToDecimal(E18.Text);
        txtETotal.Text = E.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void E17_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal E = Convert.ToDecimal(E14.Text) + Convert.ToDecimal(E15.Text) +
                    Convert.ToDecimal(E16.Text) + Convert.ToDecimal(E17.Text) + Convert.ToDecimal(E18.Text);
        txtETotal.Text = E.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void E18_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal E = Convert.ToDecimal(E14.Text) + Convert.ToDecimal(E15.Text) +
                    Convert.ToDecimal(E16.Text) + Convert.ToDecimal(E17.Text) + Convert.ToDecimal(E18.Text);
        txtETotal.Text = E.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
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
        B4.SelectedValue = "0.0";
        B4Desc.Text = "";
        B5.SelectedValue = "0.0";
        B5Desc.Text = "";
        B6.SelectedValue = "0.0";
        B6Desc.Text = "";
        C7.SelectedValue = "0.0";
        C7Desc.Text = "";
        C8.SelectedValue = "0.0";
        C8Desc.Text = "";
        C9.SelectedValue = "0.0";
        C9Desc.Text = "";
        D10.SelectedValue = "0.0";
        D10Desc.Text = "";
        D11.SelectedValue = "0.0";
        D11Desc.Text = "";
        D12.SelectedValue = "0.0";
        D12Desc.Text = "";
        D13.SelectedValue = "0.0";
        D13Desc.Text = "";
        E14.SelectedValue = "0.0";
        E14Desc.Text = "";
        E15.SelectedValue = "0.0";
        E15Desc.Text = "";
        E16.SelectedValue = "0.0";
        E16Desc.Text = "";
        E17.SelectedValue = "0.0";
        E17Desc.Text = "";
        E18.SelectedValue = "0.0";
        E18Desc.Text = "";
        F19.SelectedValue = "0.0";
        F19Desc.Text = "";
        F20.SelectedValue = "0.0";
        F20Desc.Text = "";
        
        txtATotal.Text ="0.0";
        txtBTotal.Text = "0.0";
        txtCTotal.Text ="0.0";
        txtDTotal.Text = "0.0";
        txtETotal.Text = "0.0";
        txtFTotal.Text = "0.0";
        txtOverAll.Text = "0.0";
        txtAppRemarks.Text = "";
        txtActionPlan.Text = "";
        txtEmpComments.Text = "";
        lblGrade.Text = "";
        txtEmpid.Text = "";
        txtEmpName.Text = "";
        txtDesign.Text = "";
        txtDepart.Text = "";
        txtStrength.Text = "";
        txtWeakness.Text = "";
        this.showPanel(div_employee_details);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string rdate = "";
        if (txtRecDate.Text != "")
            rdate = DateTime.Parse(txtRecDate.Text.Trim()).ToString("MM/dd/yyyy");
        else
            rdate = "";

        string inproc = "spInsertProdStaff";
        string[,] pname = { { "@Employee_ID", hfP_ID.Value}, { "@Employee_Number",txtEmpid.Text }, { "@EmpName",txtEmpName.Text }, 
                            { "@ReviewDate",rdate.ToString() }, { "@ReportTo",txtReportTo.Text },{"@AppraiserName",dropAppName.SelectedValue},
                            {"@ReviewPeriod",txtReviewPrd.Text},{"@A1",A1.SelectedValue},{"@A2",A2.SelectedValue},
                            {"@A3",A3.SelectedValue},{"@AT",txtATotal.Text},{"@B4",B4.SelectedValue},{"@B5",B5.SelectedValue},
                            {"@B6",B6.SelectedValue},{"@BT",txtBTotal.Text},{"@C7",C7.SelectedValue},{"@C8",C8.SelectedValue},
                            {"@C9",C9.SelectedValue},{"@D10",D10.SelectedValue},{"@D11",D11.SelectedValue},
                            {"@D12",D12.SelectedValue},{"@D13",D13.SelectedValue},{"@E14",E14.SelectedValue},
                            {"@E15",E15.SelectedValue},{"@E16",E16.SelectedValue},{"@E17",E17.SelectedValue},
                            {"@E18",E18.SelectedValue},{"@F19",F19.SelectedValue},{"@F20",F20.SelectedValue},
                            
                            {"@CT",txtCTotal.Text},{"@DT",txtDTotal.Text},{"@ET",txtETotal.Text},{"@FT",txtFTotal.Text},
                            {"@OVERALL",txtOverAll.Text},{"@REMARKS",txtAppRemarks.Text},{"@ACTION_PLAN",txtActionPlan.Text},
                            {"@EMP_COMMENTS",txtEmpComments.Text},{"@IsExist","OUTPUT"},
                            {"@A1Desc",A1Desc.Text},{"@A2Desc",A2Desc.Text},{"@A3Desc",A3Desc.Text},{"@B4Desc",B4Desc.Text},
                            {"@B5Desc",B5Desc.Text},{"@B6Desc",B6Desc.Text},{"@C7Desc",C7Desc.Text},{"@C8Desc",C8Desc.Text},
                            {"@C9Desc",C9Desc.Text},{"@D10Desc",D10Desc.Text},{"@D11Desc",D11Desc.Text},{"@D12Desc",D12Desc.Text},
                            {"@D13Desc",D13Desc.Text},{"@E14Desc",E14Desc.Text},{"@E15Desc",E15Desc.Text},{"@E16Desc",E16Desc.Text},{"@E17Desc",E17Desc.Text}
                            ,{"@E18Desc",E18Desc.Text},{"@F19Desc",F19Desc.Text},{"@F20Desc",F20Desc.Text},
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
        A1Desc.Focus();
        A2Desc.Focus();
        this.showPanel(div_employee_details);

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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL dsSQL = new Datasource_IBSQL();
        if (hfp_NP_ID.Value != "")
        {
            txtReviewPrd.Text = Convert.ToString(Convert.ToString(Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1) + '-' + DateTime.Now.ToString("yy"));

            dsSQL.InsertProdFeedback(hfP_ID.Value, hfp_NP_ID.Value, txtPositive.Text.Replace("'", "''"), txtNegative.Text.Replace("'", "''"));
            dsSQL.UpdateProdFeedback(hfP_ID.Value, hfp_NP_ID.Value, txtReviewPrd.Text);
            DataSet ds = new DataSet();
            ds = dsSQL.spGetFeedback(hfP_ID.Value, hfp_NP_ID.Value);
            if (ds != null)
            {
                gvFeedback.DataSource = ds;
                gvFeedback.DataBind();
                txtPositive.Text = "";
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));

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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));

        this.showPanel(div_employee_details);
    }


    protected void F19_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal F = Convert.ToDecimal(F19.Text) + Convert.ToDecimal(F20.Text);
        txtFTotal.Text = F.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void F20_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal F = Convert.ToDecimal(F19.Text) + Convert.ToDecimal(F20.Text) ;
        txtFTotal.Text = F.ToString();
        decimal O = Convert.ToDecimal(txtATotal.Text) + Convert.ToDecimal(txtBTotal.Text) + Convert.ToDecimal(txtCTotal.Text) +
                  Convert.ToDecimal(txtDTotal.Text) + Convert.ToDecimal(txtETotal.Text) + Convert.ToDecimal(txtFTotal.Text);
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

        if (B4Desc.Text.Length < 22)
            B4Desc.Height = 20;
        else if (B4Desc.Text.Length < 75)
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B4Desc.Text.Length));
        else
            B4Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B4Desc.Text.Length));

        if (B5Desc.Text.Length < 22)
            B5Desc.Height = 20;
        else if (B5Desc.Text.Length < 75)
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B5Desc.Text.Length));
        else
            B5Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B5Desc.Text.Length));

        if (B6Desc.Text.Length < 22)
            B6Desc.Height = 20;
        else if (B6Desc.Text.Length < 75)
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(B6Desc.Text.Length));
        else
            B6Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(B6Desc.Text.Length));

        if (C7Desc.Text.Length < 22)
            C7Desc.Height = 20;
        else if (C7Desc.Text.Length < 75)
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C7Desc.Text.Length));
        else
            C7Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C7Desc.Text.Length));

        if (C8Desc.Text.Length < 22)
            C8Desc.Height = 20;
        else if (C8Desc.Text.Length < 75)
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C8Desc.Text.Length));
        else
            C8Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C8Desc.Text.Length));

        if (C9Desc.Text.Length < 22)
            C9Desc.Height = 20;
        else if (C9Desc.Text.Length < 75)
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(C9Desc.Text.Length));
        else
            C9Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(C9Desc.Text.Length));

        if (D10Desc.Text.Length < 22)
            D10Desc.Height = 20;
        else if (D10Desc.Text.Length < 75)
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D10Desc.Text.Length));
        else
            D10Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D10Desc.Text.Length));

        if (D11Desc.Text.Length < 22)
            D11Desc.Height = 20;
        else if (D11Desc.Text.Length < 75)
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D11Desc.Text.Length));
        else
            D11Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D11Desc.Text.Length));

        if (D12Desc.Text.Length < 22)
            D12Desc.Height = 20;
        else if (D12Desc.Text.Length < 75)
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D12Desc.Text.Length));
        else
            D12Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D12Desc.Text.Length));

        if (D13Desc.Text.Length < 22)
            D13Desc.Height = 20;
        else if (D13Desc.Text.Length < 75)
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(D13Desc.Text.Length));
        else
            D13Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(D13Desc.Text.Length));

        if (E14Desc.Text.Length < 22)
            E14Desc.Height = 20;
        else if (E14Desc.Text.Length < 75)
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E14Desc.Text.Length));
        else
            E14Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E14Desc.Text.Length));

        if (E15Desc.Text.Length < 22)
            E15Desc.Height = 20;
        else if (E15Desc.Text.Length < 75)
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E15Desc.Text.Length));
        else
            E15Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E15Desc.Text.Length));

        if (E16Desc.Text.Length < 22)
            E16Desc.Height = 20;
        else if (E16Desc.Text.Length < 75)
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E16Desc.Text.Length));
        else
            E16Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E16Desc.Text.Length));

        if (E17Desc.Text.Length < 22)
            E17Desc.Height = 20;
        else if (E17Desc.Text.Length < 75)
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E17Desc.Text.Length));
        else
            E17Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E17Desc.Text.Length));

        if (E18Desc.Text.Length < 22)
            E18Desc.Height = 20;
        else if (E18Desc.Text.Length < 75)
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(E18Desc.Text.Length));
        else
            E18Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(E18Desc.Text.Length));

        if (F19Desc.Text.Length < 22)
            F19Desc.Height = 20;
        else if (F19Desc.Text.Length < 75)
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F19Desc.Text.Length));
        else
            F19Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F19Desc.Text.Length));

        if (F20Desc.Text.Length < 22)
            F20Desc.Height = 20;
        else if (F20Desc.Text.Length < 75)
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.9) * Convert.ToDouble(F20Desc.Text.Length));
        else
            F20Desc.Height = Convert.ToInt32(Convert.ToDouble(0.7) * Convert.ToDouble(F20Desc.Text.Length));
        this.showPanel(div_employee_details);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string inproc = "spUpdateSubmitProdStaff";
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

    }
}