using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.Net.Mail;
using System.Configuration;
using System.Threading;

public partial class JobAllocationLaunch : System.Web.UI.Page
{
    int iRowID = 1;
    Non_Launch NL = new Non_Launch();
    datasourceSQL dpSQL = new datasourceSQL();
    static string jobTask_selectvalue = "";
    static string jobSoft_selectvalue = "";
    static string jobCust_selectvalue = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"].ToString().Trim() == Request.QueryString["employeeid"].ToString().Trim())
        {
            DataSet sess = new DataSet();
            sess = NL.GetSessionEmpID(Session["employeeid"].ToString());
            if (sess != null)
            {
                if (sess.Tables[0].Rows.Count > 0)
                {
            if (!Page.IsPostBack)
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstGeneral);
                //getEmployee();

                ViewState["CurrentAlphabet"] = "ALL";
                ViewState["CurrentAlphabet1"] = "ALL";
                this.GenerateAlphabets();
                this.GenerateAlphabets1();
                this.BindGrid1();
                this.BindGrid2();
                this.GetJobAllocationList("0", "1");

                    }
                }
            }
        }
    }
    private void showpanel(HtmlGenericControl shwpanel)
    {
        switch (shwpanel.ID)
        {
            case "LstGeneral":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "current");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "General";
                break;
            case "LstTE":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "current");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "TE";
                break;
            case "LstDTP":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "current");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "DTP";
                break;
            case "LstPreDTP":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "current");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "PreDTP";
                break;
            case "LstDQA":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "current");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "DQA";
                break;
            case "LstOVA":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "current");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "OVA";
                break;
            case "LstQC":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "current");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "QC";
                break;
            case "LstQCcorr":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "current");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "QCcorr";
                break;
            case "LstQA":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "current");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "QA";
                break;
            case "LstQAcorr":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "current");
                LstRFD.Attributes.Add("class", "");
                Session["TabName"] = "QAcorr";
                break;
            case "LstRFD":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstTE.Attributes.Add("class", "");
                LstDTP.Attributes.Add("class", "");
                LstPreDTP.Attributes.Add("class", "");
                LstDQA.Attributes.Add("class", "");
                LstOVA.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstQCcorr.Attributes.Add("class", "");
                LstQA.Attributes.Add("class", "");
                LstQAcorr.Attributes.Add("class", "");
                LstRFD.Attributes.Add("class", "current");
                Session["TabName"] = "RFD";
                break;
        }
    }
    protected void lnkTE_Click(object sender, EventArgs e)
    {
        //getEmployee();
        BindGrid1();
        this.showpanel(LstTE);
        this.GetJobAllocationList("1", "1");
    }
    private void BindGrid1()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["conStrSQLL"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY E.fname) AS ROWID,employee_id AS EMPLOYEE_ID,"+              
                  "E.fname + ' ' + surname+' ('+cast(employee_number as varchar(10))+')' as EMPLOYEE ,"+              
                  "0 AS job_id,0 AS job_type_id,0 AS AEID,0 AS TASK_ID,NULL AS JOB_ORDER FROM EMPLOYEE E "+        
                  "join HRMS_CMB.dbo.master_employee HE on replace(replace(HE.REFNO,'SDS',''),'DDS','')=E.employee_number "+
                  "AND OBSOLETE IS NULL  and HE.DOL is null join EMPLOYEE_TEAM_MEMBER T on T.EMPLOYEE_TEAM_MEMBER_ID=E.employee_id "+
                  " and E.Location_id=3 and t.EMPLOYEE_TEAM_ID in (100,101,102,103) "+
                  " where T.TEAM_EXIT_DATE IS NULL AND e.Fname LIKE @Alphabet + '%' OR @Alphabet = 'ALL'"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Alphabet", ViewState["CurrentAlphabet"]);
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        sda.Fill(dt);
                        grdEmplyeeTask.DataSource = dt;
                        grdEmplyeeTask.DataBind();
                    }
                }
            }
        }
    }
    private void getEmployee()
    {
        try
        {
            DataSet dsEmp = new DataSet();
            dsEmp = NL.GetEmployee();
            grdEmplyeeTask.DataSource = dsEmp;
            grdEmplyeeTask.DataBind();

        }
        catch (Exception Ex)
        {
            throw Ex; ;
        }
    }
    private void GetJobAllocationList(string Taskid,string FileStatus)
    {
        DataSet dst_emp = new DataSet();
        dst_emp = NL.GetJobAllocationSplit(FileStatus);
        if (dst_emp != null)
        {
            Session["LaunchJobs_Emp"] = dst_emp.Tables[0];
        }
        if (dst_emp != null && dst_emp.Tables[0].Rows.Count > 0)
        {
            gv_job_allocation_Employee.DataSource = dst_emp.Tables[0];
            gv_job_all_Emp_RFD.DataSource = dst_emp.Tables[0];
            Session["JobLiveDS1"] = dst_emp.Tables[0];
            Session["JobviewTable1"] = dst_emp.Tables[0];
        }
        
        gv_job_allocation_Employee.DataBind();
        gv_job_all_Emp_RFD.DataBind();        
    }
    protected void lnkDTP_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstDTP);
        this.GetJobAllocationList("3", "1");
    }
    protected void lnkPreDTP_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstPreDTP);
        this.GetJobAllocationList("2", "1");
    }
    protected void lnkDQA_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstDQA);
        this.GetJobAllocationList("4", "1");
    }
    protected void lnkOVA_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstOVA);
        this.GetJobAllocationList("5", "1");
    }
    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstGeneral);
        this.GetJobAllocationList("0", "1");
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (Session["JobLiveDS1"] != null)
        {
            bindDDL1(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
            bindDDL1(oTable, ddlLocation, "Location_Name", "Location_Name");
            bindDDL1(oTable, ddlTask, "TaskName", "TaskName");
            bindDDL1(oTable, ddlDueDate, "Date_IST", "Date_IST");
            bindDDL1(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
            bindDDL1(oTable, ddlSoft, "Soft_Name", "Soft_Name");
        }
    }

    protected void btn_Move1_Click(object sender, EventArgs e)
    {
        SaveCheckedValues1();
        DataTable userdetails = (DataTable)Session["CHECKED_ITEMS1"];
        if (userdetails != null && userdetails.Rows.Count < 2)
        {
            foreach (GridViewRow grw in gv_job_allocation_Employee.Rows)
            {
                CheckBox ck = ((CheckBox)grw.FindControl("chkBoxStatus"));
                if (ck.Checked)
                {
                    string strJobId = ((HiddenField)grw.FindControl("hf_Job_ID")).Value.ToString();
                    string strJobHistoryId = ((HiddenField)grw.FindControl("job_his_id")).Value.ToString();
                    string strJobTaskId = ((HiddenField)grw.FindControl("hf_FP_ID")).Value.ToString();
                    string strUA_ID = ((HiddenField)grw.FindControl("hf_UA_ID")).Value.ToString();
                    string Pages = ((LinkButton)grw.FindControl("lnkPages")).Text.ToString();
                    string FileStatus = "";
                    bool chkCond = false;
                    bool pageCut = false;
                    bool pageRng = false;
                    int pg = 0;
                   
                    //DataTable userdetails = (DataTable)Session["CHECKED_ITEMS1"];
                    if (userdetails != null && userdetails.Rows.Count > 0)
                    {
                        if (Session["TabName"].ToString() == "General")
                            FileStatus = "1";
                        else if (Session["TabName"].ToString() == "QC")
                            FileStatus = "2";
                        else if (Session["TabName"].ToString() == "QCcorr")
                            FileStatus = "3";
                        else if (Session["TabName"].ToString() == "QA")
                            FileStatus = "4";
                        else if (Session["TabName"].ToString() == "QAcorr")
                            FileStatus = "5";
                        else if (Session["TabName"].ToString() == "RFD")
                            FileStatus = "6";
                        if (!Pages.Contains("Pages"))
                        {
                            DataRow[] matches = userdetails.Select("Status like 'N'");
                            foreach (DataRow row in matches)
                            {
                                chkCond = true;
                                pageRng = true;
                            }
                            for (int i = 0; i < userdetails.Rows.Count; i++)
                            {
                                if (pg == 0)
                                {
                                    pg = Convert.ToInt32(Pages);
                                }
                                else
                                {
                                    pg = pg + Convert.ToInt32(Pages);
                                }
                            }

                            int TotalPages = 0;
                            TotalPages = Convert.ToInt32(Pages) - pg;// -Convert.ToInt32(dsval.Tables[0].Rows[0]["Pages"].ToString());
                            if (TotalPages < 0)
                            {
                                chkCond = true;
                                pageCut = true;
                            }

                            if (chkCond == false)
                            {
                                for (int i = 0; i < userdetails.Rows.Count; i++)
                                {
                                    string strEmpID = userdetails.Rows[i]["ID"].ToString().Trim();
                                    string strOrder = "1";
                                    string strPages = Pages;
                                    string ddlWorkFLow = userdetails.Rows[i]["WorkFlow"].ToString().Trim();
                                    string strPagesFrom = "1";
                                    string strPagesTo = Pages;
                                    NL.MoveTask_emp_process1(strJobId, strJobTaskId, strEmpID, strOrder, strJobHistoryId, strPages, ddlWorkFLow, strPagesFrom, strPagesTo, FileStatus, strUA_ID);
                                }
                            }
                            else
                            {
                                if (pageRng == true)
                                {
                                    lblresultEmp1.Text = "Page From & To Details not given fully.";
                                }
                                else if (pageCut == true)
                                {
                                    lblresultEmp1.Text = "Given Page Range values are more then File Page Count.";
                                }
                                popup123.Show();
                            }
                        }
                        else
                        {
                            lblresultEmp1.Text = "Please select Unallocated jobs in allocation..";
                            popup123.Show();
                        }
                    }
                    else
                    {
                        popup123.Show();
                        lblresultEmp1.Text = "Select atlest one Employee!..";
                    }
                    Session["CHECKED_ITEMS1"] = null;
                    object o = new object();
                    EventArgs s = new EventArgs();
                }
            }
            if (Session["TabName"].ToString() == "General")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstGeneral);
                this.GetJobAllocationList("0", "1");
            }
            else if (Session["TabName"].ToString() == "TE")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstTE);
                this.GetJobAllocationList("1", "1");
            }
            else if (Session["TabName"].ToString() == "DTP")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstDTP);
                this.GetJobAllocationList("3", "1");
            }
            else if (Session["TabName"].ToString() == "PreDTP")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstPreDTP);
                this.GetJobAllocationList("2", "1");
            }
            else if (Session["TabName"].ToString() == "DQA")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstDQA);
                this.GetJobAllocationList("4", "1");
            }
            else if (Session["TabName"].ToString() == "OVA")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstOVA);
                this.GetJobAllocationList("5", "1");
            }
            else if (Session["TabName"].ToString() == "QC")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstQC);
                this.GetJobAllocationList("0", "2");
            }
            else if (Session["TabName"].ToString() == "QCcorr")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstQCcorr);
                this.GetJobAllocationList("0", "3");
            }
            else if (Session["TabName"].ToString() == "QA")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstQA);
                this.GetJobAllocationList("0", "4");
            }
            else if (Session["TabName"].ToString() == "QAcorr")
            {
                gv_job_allocation_Employee.Visible = true;
                gv_job_all_Emp_RFD.Visible = false;
                this.showpanel(LstQAcorr);
                this.GetJobAllocationList("0", "5");
            }
            else if (Session["TabName"].ToString() == "RFD")
            {
                gv_job_allocation_Employee.Visible = false;
                gv_job_all_Emp_RFD.Visible = true;
                this.showpanel(LstQAcorr);
                this.GetJobAllocationList("0", "6");
            }
            Page_Load(null, null);
            //popup123.Hide();
            //lblresultEmp1.Text = "";
        }
        else
        {
            lblresultEmp1.Text = "Select only one employee for Multiple file job allocation..";
            popup123.Show();
        }
        
    }

    protected void btn_Move_Click(object sender, EventArgs e)
    {
        string strJobId = lblID.Text.Trim();
        string strJobHistoryId = lblJob_His_ID.Text.Trim();
        string strJobTaskId =lblFP_ID.Text.Trim();
        string strUA_ID = lblUA_ID.Text.Trim();
        string FileStatus = "";
        bool chkCond = false;
        bool pageCut = false;
        bool pageRng = false;
        int pg = 0;
        SaveCheckedValues();
        DataTable userdetails = (DataTable)Session["CHECKED_ITEMS"];
        if (userdetails != null && userdetails.Rows.Count > 0)
        {
            if (Session["TabName"].ToString() == "General")
                FileStatus = "1";
            else if (Session["TabName"].ToString() == "QC")
                FileStatus = "2";
            else if (Session["TabName"].ToString() == "QCcorr")
                FileStatus = "3";
            else if (Session["TabName"].ToString() == "QA")
                FileStatus = "4";
            else if (Session["TabName"].ToString() == "QAcorr")
                FileStatus = "5";
            else if (Session["TabName"].ToString() == "RFD")
                FileStatus = "6";

            DataRow[] matches = userdetails.Select("Status like 'N'");
            foreach (DataRow row in matches)
            {
                chkCond = true;
                pageRng = true;
            }
            for (int i = 0; i < userdetails.Rows.Count; i++)
            {
                if(pg==0)
                {
                    pg = Convert.ToInt32(userdetails.Rows[i]["Pages"].ToString().Trim());
                }
                else
                {
                    pg = pg + Convert.ToInt32(userdetails.Rows[i]["Pages"].ToString().Trim());
                }
            }
            //DataSet dsval = new DataSet();
            //dsval = NL.GetLPPageValues1(strJobTaskId.ToString(), strJobHistoryId.ToString(), strUA_ID.ToString());
            int TotalPages = 0;
            //if(dsval!=null)
            {
                TotalPages = Convert.ToInt32(lblPages.Text) - pg;// -Convert.ToInt32(dsval.Tables[0].Rows[0]["Pages"].ToString());
                if(TotalPages<0)
                {
                    chkCond = true;
                    pageCut = true;
                }
            }


            if (chkCond == false)
            {
                for (int i = 0; i < userdetails.Rows.Count; i++)
                {
                    string strEmpID = userdetails.Rows[i]["ID"].ToString().Trim();
                    string strOrder = "1";
                    string strPages = userdetails.Rows[i]["Pages"].ToString().Trim();
                    string ddlWorkFLow = userdetails.Rows[i]["WorkFlow"].ToString().Trim();
                    string strPagesFrom = userdetails.Rows[i]["PageFrom"].ToString().Trim();
                    string strPagesTo = userdetails.Rows[i]["PageTo"].ToString().Trim();

                    NL.MoveTask_emp_process1(strJobId, strJobTaskId, strEmpID, strOrder, strJobHistoryId, strPages, ddlWorkFLow, strPagesFrom, strPagesTo, FileStatus, strUA_ID);
                    //int milliseconds = 3000;
                    //Thread.Sleep(milliseconds);
                }
                Session["CHECKED_ITEMS"] = null;
                object o = new object();
                EventArgs s = new EventArgs();

                if (Session["TabName"].ToString() == "General")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstGeneral);
                    this.GetJobAllocationList("0", "1");
                }
                else if (Session["TabName"].ToString() == "TE")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstTE);
                    this.GetJobAllocationList("1", "1");
                }
                else if (Session["TabName"].ToString() == "DTP")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstDTP);
                    this.GetJobAllocationList("3", "1");
                }
                else if (Session["TabName"].ToString() == "PreDTP")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstPreDTP);
                    this.GetJobAllocationList("2", "1");
                }
                else if (Session["TabName"].ToString() == "DQA")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstDQA);
                    this.GetJobAllocationList("4", "1");
                }
                else if (Session["TabName"].ToString() == "OVA")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstOVA);
                    this.GetJobAllocationList("5", "1");
                }
                else if (Session["TabName"].ToString() == "QC")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstQC);
                    this.GetJobAllocationList("0", "2");
                }
                else if (Session["TabName"].ToString() == "QCcorr")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstQCcorr);
                    this.GetJobAllocationList("0", "3");
                }
                else if (Session["TabName"].ToString() == "QA")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstQA);
                    this.GetJobAllocationList("0", "4");
                }
                else if (Session["TabName"].ToString() == "QAcorr")
                {
                    gv_job_allocation_Employee.Visible = true;
                    gv_job_all_Emp_RFD.Visible = false;
                    this.showpanel(LstQAcorr);
                    this.GetJobAllocationList("0", "5");
                }
                else if (Session["TabName"].ToString() == "RFD")
                {
                    gv_job_allocation_Employee.Visible = false;
                    gv_job_all_Emp_RFD.Visible = true;
                    this.showpanel(LstQAcorr);
                    this.GetJobAllocationList("0", "6");
                }
                Page_Load(null, null);
                popup.Hide();
                lblresultEmp.Text = "";
            }
            else
            {
                if(pageRng==true)
                {
                    lblresultEmp.Text = "Page From & To Details not given fully.";
                }
                else if (pageCut == true)
                {
                    lblresultEmp.Text = "Given Page Range values are more then File Page Count.";
                }
                popup.Show();
            }
            Session["CHECKED_ITEMS"] = null;
            //Alert("This Job was Successfully Moved");
        }
        else
        {
            popup.Show();
            //Page_Load(null, null);
            lblresultEmp.Text = "Select atlest one Employee!..";// Alert("Select atlest one Employee!..");
        }
    }
    private void Alert(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + msg + "\");</script>");
    }
    protected void ddl_Team_SelectedIndexChanged(object sender, EventArgs e)
    {
        //getEmployeeByTeam(ddl_Team.SelectedValue);
        BindGrid();
        //RePopulateCheckBoxes(); 
        popup.Show();
    }
    protected void ddl_Team1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //getEmployeeByTeam(ddl_Team.SelectedValue);
        BindGrid2();
        //RePopulateCheckBoxes(); 
        popup123.Show();
    }
    protected void Alphabet_Click(object sender, EventArgs e)
    {
        SaveCheckedValues();
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        grdEmplyeeTask.PageIndex = 0;
        //getEmployeeByTeam(ddl_Team.SelectedValue);
        BindGrid();
        PopulateCheckedValues();
        popup.Show();
    }
    protected void Alphabet1_Click(object sender, EventArgs e)
    {
        SaveCheckedValues1();
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet1"] = lnkAlphabet.Text;
        this.GenerateAlphabets1();
        grdEmplyeeTask.PageIndex = 0;
        //getEmployeeByTeam(ddl_Team.SelectedValue);
        BindGrid2();
        PopulateCheckedValues1();
        popup123.Show();
    }
    private void GenerateAlphabets()
    {
        List<ListItem> alphabets = new List<ListItem>();
        ListItem alphabet = new ListItem();
        alphabet.Value = "ALL";
        alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
        alphabets.Add(alphabet);
        for (int i = 65; i <= 90; i++)
        {
            alphabet = new ListItem();
            alphabet.Value = Char.ConvertFromUtf32(i);
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
        }
        rptAlphabets.DataSource = alphabets;
        rptAlphabets.DataBind();
    }
    private void GenerateAlphabets1()
    {
        List<ListItem> alphabets = new List<ListItem>();
        ListItem alphabet = new ListItem();
        alphabet.Value = "ALL";
        alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet1"]);
        alphabets.Add(alphabet);
        for (int i = 65; i <= 90; i++)
        {
            alphabet = new ListItem();
            alphabet.Value = Char.ConvertFromUtf32(i);
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet1"]);
            alphabets.Add(alphabet);
        }
        rptAlphabets1.DataSource = alphabets;
        rptAlphabets1.DataBind();
    }
    private void getEmployeeByTeam(string TeamID)
    {
        try
        {
            DataSet dsEmp = new DataSet();
            dsEmp = NL.GetJobAllocationEmpByTeam(TeamID.ToString());
            grdEmplyeeTask.DataSource = dsEmp;
            grdEmplyeeTask.DataBind();
            lblresultEmp.Text = "";
        }
        catch (Exception Ex)
        {
            throw Ex; ;
        }
    }
    protected void Pages_Click(object sender, EventArgs e)
    {
        string FileStatus = "";
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            if (Session["TabName"].ToString() == "General")
                FileStatus = "1";
            else if (Session["TabName"].ToString() == "QC")
                FileStatus = "2";
            else if (Session["TabName"].ToString() == "QCcorr")
                FileStatus = "3";
            else if (Session["TabName"].ToString() == "QA")
                FileStatus = "4";
            else if (Session["TabName"].ToString() == "QAcorr")
                FileStatus = "5";
            else if (Session["TabName"].ToString() == "RFD")
                FileStatus = "6";
            HiddenField hf_job_id = (HiddenField)row.Cells[9].FindControl("hf_Job_ID");
            HiddenField hf_job_FP_id = (HiddenField)row.Cells[9].FindControl("hf_FP_ID");
            HiddenField hf_job_history_id = (HiddenField)row.Cells[9].FindControl("job_his_id");
            HiddenField hf_Pages = (HiddenField)row.Cells[9].FindControl("hf_Pages");
            HiddenField hf_A_ID = (HiddenField)row.Cells[9].FindControl("hf_A_ID");
            string strJobId = Convert.ToString(hf_job_id.Value).Trim();
            string strJobHistoryId = Convert.ToString(hf_job_history_id.Value).Trim();
            string strJobTaskId = Convert.ToString(hf_job_FP_id.Value).Trim();
            string strAId = Convert.ToString(hf_A_ID.Value).Trim();
            PJobID.Text = strAId;
            PJob_His_ID.Text = strJobHistoryId;
            PFP_ID.Text = strJobTaskId;
            DataSet dsEmp = new DataSet();
            dsEmp = NL.GetAllocatedEmpPages1(strAId.ToString(), strJobTaskId.ToString(), strJobHistoryId.ToString(), FileStatus.ToString());
            gvAllocatedEmp.DataSource = dsEmp;
            gvAllocatedEmp.DataBind();

        }
        popup1.Show();
    }
    protected void gvAllocatedEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllocatedEmp.PageIndex = e.NewPageIndex;
        LoadgvAllocatedEmp();
        popup1.Show();
    }
    public void LoadgvAllocatedEmp()
    {
        string FileStatus = "";
        if (Session["TabName"].ToString() == "General")
            FileStatus = "1";
        else if (Session["TabName"].ToString() == "QC")
            FileStatus = "2";
        else if (Session["TabName"].ToString() == "QCcorr")
            FileStatus = "3";
        else if (Session["TabName"].ToString() == "QA")
            FileStatus = "4";
        else if (Session["TabName"].ToString() == "QAcorr")
            FileStatus = "5";
        else if (Session["TabName"].ToString() == "RFD")
            FileStatus = "6";
        DataSet dsEmp = new DataSet();
        dsEmp = NL.GetAllocatedEmpPages1(PJobID.Text, PFP_ID.Text, PJob_His_ID.Text, FileStatus.ToString());
        gvAllocatedEmp.DataSource = dsEmp;
        gvAllocatedEmp.DataBind();
    }
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            HiddenField hf_job_id = (HiddenField)row.Cells[9].FindControl("hf_Job_ID");
            HiddenField hf_job_FP_id = (HiddenField)row.Cells[9].FindControl("hf_FP_ID");
            HiddenField hf_job_history_id = (HiddenField)row.Cells[9].FindControl("job_his_id");
            HiddenField hf_job_Task_id = (HiddenField)row.Cells[9].FindControl("hf_Task_ID");
            HiddenField hf_Pages = (HiddenField)row.Cells[9].FindControl("hf_Pages");
            HiddenField hf_UA_ID = (HiddenField)row.Cells[9].FindControl("hf_UA_ID");
            string strJobId = Convert.ToString(hf_job_id.Value).Trim();
            string strJobHistoryId = Convert.ToString(hf_job_history_id.Value).Trim();
            string strJobFPId = Convert.ToString(hf_job_FP_id.Value).Trim();
            string strUAId = Convert.ToString(hf_UA_ID.Value).Trim();
            DataSet dsEmp = new DataSet();
            dsEmp = NL.GetJobAllocatingEmployee(strJobId, strJobFPId, strJobHistoryId);
            lblJobID.Text = dsEmp.Tables[0].Rows[0]["JobID"].ToString();
            lblProjectName.Text = dsEmp.Tables[0].Rows[0]["ProjectName"].ToString();
            lblFileName.Text = dsEmp.Tables[0].Rows[0]["Files_name"].ToString();
            lblTask.Text = dsEmp.Tables[0].Rows[0]["TaskName"].ToString();
            lblID.Text = strJobId;
            lblFP_ID.Text = strJobFPId;
            lblJob_His_ID.Text = strJobHistoryId;
            lblPages.Text = Convert.ToString(hf_Pages.Value).Trim();
            lblUA_ID.Text = strUAId.ToString();
            //getEmployeeByTeam(ddl_Team.SelectedValue);
            BindGrid();
            string FileStatus = "";
            if (Session["TabName"].ToString() == "General")
                FileStatus = "1";
            else if (Session["TabName"].ToString() == "QC")
                FileStatus = "2";
            else if (Session["TabName"].ToString() == "QCcorr")
                FileStatus = "3";
            else if (Session["TabName"].ToString() == "QA")
                FileStatus = "4";
            else if (Session["TabName"].ToString() == "QAcorr")
                FileStatus = "5";
            else if (Session["TabName"].ToString() == "RFD")
                FileStatus = "6";
            DataSet dsEmp1 = new DataSet();
            dsEmp1 = NL.GetAllocatedEmpPages1(strUAId.ToString(), strJobFPId.ToString(), strJobHistoryId.ToString(), FileStatus.ToString());
            gvEmpAllocated.DataSource = dsEmp1;
            gvEmpAllocated.DataBind();
        }
        popup.Show();
    }
    protected void EditAll(object sender, EventArgs e)
    {
        bool s = false;
        foreach (GridViewRow grw in gv_job_allocation_Employee.Rows)
        {
            CheckBox ck = ((CheckBox)grw.FindControl("chkBoxStatus"));
            if (ck.Checked)
            {
                s = true;
            }
        }
        if(s)
        {
            BindGrid2();
            popup123.Show();
        }
        else
        {
            Alert("Please select any one of the files in job allocation..");
        }
    }
    protected void grdEmplyeeTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SaveCheckedValues();
        grdEmplyeeTask.PageIndex = e.NewPageIndex;
        //getEmployeeByTeam(ddl_Team.SelectedValue);
        BindGrid();
        PopulateCheckedValues();
        popup.Show();
    }
    protected void grdEmplyeeTask1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SaveCheckedValues1();
        grdEmplyeeTask1.PageIndex = e.NewPageIndex;
        //getEmployeeByTeam(ddl_Team.SelectedValue);
        BindGrid2();
        PopulateCheckedValues1();
        popup123.Show();
    }
    private void BindGrid()
    {
        string qry = "";
        if (ddl_Team.SelectedValue != "0")
        {
            qry = "SELECT ROW_NUMBER() OVER(ORDER BY E.fname) AS ROWID,employee_id AS EMPLOYEE_ID," +
                  "E.fname + ' ' + surname+' ('+cast(employee_number as varchar(10))+')' as EMPLOYEE ," +
                  "0 AS job_id,0 AS job_type_id,0 AS AEID,0 AS TASK_ID,NULL AS JOB_ORDER FROM EMPLOYEE E " +
                  "join HRMS_CMB.dbo.master_employee HE on replace(replace(HE.REFNO,'SDS',''),'DDS','')=E.employee_number " +
                  "AND OBSOLETE IS NULL  and HE.DOL is null join EMPLOYEE_TEAM_MEMBER T on T.EMPLOYEE_TEAM_MEMBER_ID=E.employee_id " +
                  "and E.Location_id=3 and t.EMPLOYEE_TEAM_ID=" + ddl_Team.SelectedValue + " " +
                  " where T.TEAM_EXIT_DATE IS NULL AND e.Fname LIKE @Alphabet + '%' OR @Alphabet = 'ALL'";
        }
        else
        {
            qry = "SELECT ROW_NUMBER() OVER(ORDER BY E.fname) AS ROWID,employee_id AS EMPLOYEE_ID," +
                  "E.fname + ' ' + surname+' ('+cast(employee_number as varchar(10))+')' as EMPLOYEE ," +
                  "0 AS job_id,0 AS job_type_id,0 AS AEID,0 AS TASK_ID,NULL AS JOB_ORDER FROM EMPLOYEE E " +
                  "join HRMS_CMB.dbo.master_employee HE on replace(replace(HE.REFNO,'SDS',''),'DDS','')=E.employee_number " +
                  "AND OBSOLETE IS NULL  and HE.DOL is null join EMPLOYEE_TEAM_MEMBER T on T.EMPLOYEE_TEAM_MEMBER_ID=E.employee_id " +
                  " and E.Location_id=3 and t.EMPLOYEE_TEAM_ID in (100,101,102,103) " +
                  " where T.TEAM_EXIT_DATE IS NULL AND e.Fname LIKE @Alphabet + '%' OR @Alphabet = 'ALL'";
        }
        string strConnString = ConfigurationManager.ConnectionStrings["conStrSQLL"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Alphabet", ViewState["CurrentAlphabet"]);
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        sda.Fill(dt);
                        grdEmplyeeTask.DataSource = dt;
                        grdEmplyeeTask.DataBind();
                    }
                }
            }
        }
    }

    private void BindGrid2()
    {
        string qry = "";
        if (ddl_Team1.SelectedValue != "0")
        {
            qry = "SELECT ROW_NUMBER() OVER(ORDER BY E.fname) AS ROWID,employee_id AS EMPLOYEE_ID," +
                  "E.fname + ' ' + surname+' ('+cast(employee_number as varchar(10))+')' as EMPLOYEE ," +
                  "0 AS job_id,0 AS job_type_id,0 AS AEID,0 AS TASK_ID,NULL AS JOB_ORDER FROM EMPLOYEE E " +
                  "join HRMS_CMB.dbo.master_employee HE on replace(replace(HE.REFNO,'SDS',''),'DDS','')=E.employee_number " +
                  "AND OBSOLETE IS NULL  and HE.DOL is null join EMPLOYEE_TEAM_MEMBER T on T.EMPLOYEE_TEAM_MEMBER_ID=E.employee_id " +
                  "and E.Location_id=3 and t.EMPLOYEE_TEAM_ID=" + ddl_Team1.SelectedValue + " " +
                  " where T.TEAM_EXIT_DATE IS NULL AND e.Fname LIKE @Alphabet + '%' OR @Alphabet = 'ALL'";
        }
        else
        {
            qry = "SELECT ROW_NUMBER() OVER(ORDER BY E.fname) AS ROWID,employee_id AS EMPLOYEE_ID," +
                  "E.fname + ' ' + surname+' ('+cast(employee_number as varchar(10))+')' as EMPLOYEE ," +
                  "0 AS job_id,0 AS job_type_id,0 AS AEID,0 AS TASK_ID,NULL AS JOB_ORDER FROM EMPLOYEE E " +
                  "join HRMS_CMB.dbo.master_employee HE on replace(replace(HE.REFNO,'SDS',''),'DDS','')=E.employee_number " +
                  "AND OBSOLETE IS NULL  and HE.DOL is null join EMPLOYEE_TEAM_MEMBER T on T.EMPLOYEE_TEAM_MEMBER_ID=E.employee_id " +
                  " and E.Location_id=3 and t.EMPLOYEE_TEAM_ID in (100,101,102,103) " +
                  " where T.TEAM_EXIT_DATE IS NULL AND e.Fname LIKE @Alphabet + '%' OR @Alphabet = 'ALL'";
        }
        string strConnString = ConfigurationManager.ConnectionStrings["conStrSQLL"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Alphabet", ViewState["CurrentAlphabet1"]);
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        sda.Fill(dt);
                        grdEmplyeeTask1.DataSource = dt;
                        grdEmplyeeTask1.DataBind();
                        lblresultEmp1.Text = "";
                    }
                }
            }
        }
    }
    protected void gvEmpAllocated_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpAllocated.PageIndex = e.NewPageIndex;
        LoadgvEmpAllocated();
        popup.Show();
    }
    private void LoadgvEmpAllocated()
    {
        string FileStatus = "";
        if (Session["TabName"].ToString() == "General")
            FileStatus = "1";
        else if (Session["TabName"].ToString() == "QC")
            FileStatus = "2";
        else if (Session["TabName"].ToString() == "QCcorr")
            FileStatus = "3";
        else if (Session["TabName"].ToString() == "QA")
            FileStatus = "4";
        else if (Session["TabName"].ToString() == "QAcorr")
            FileStatus = "5";
        else if (Session["TabName"].ToString() == "RFD")
            FileStatus = "6";
        DataSet dsEmp1 = new DataSet();
        dsEmp1 = NL.GetAllocatedEmpPages(lblID.Text, lblFP_ID.Text, lblJob_His_ID.Text, FileStatus.ToString());
        gvEmpAllocated.DataSource = dsEmp1;
        gvEmpAllocated.DataBind();
    }
    private void PopulateCheckedValues()
    {
        DataTable userdetails = (DataTable)Session["CHECKED_ITEMS"];
        if (userdetails != null && userdetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in grdEmplyeeTask.Rows)
            {
                int index = (int)grdEmplyeeTask.DataKeys[gvrow.RowIndex].Value;
                DataRow[] matches = userdetails.Select("ID like '%" + index + "%'");
                foreach (DataRow row in matches)
                {
                    CheckBox myCheckBox = (CheckBox)gvrow.FindControl("Chk_Emp");
                    myCheckBox.Checked = true;
                    TextBox PageFrom = (TextBox)gvrow.FindControl("txtPageFrom");
                    PageFrom.Text = row["PageFrom"].ToString();
                    TextBox PageTo = (TextBox)gvrow.FindControl("txtPageTo");
                    PageTo.Text = row["PageTo"].ToString();
                    DropDownList WorkFlow = (DropDownList)gvrow.FindControl("ddlTask");
                    WorkFlow.SelectedValue = row["WorkFlow"].ToString();
                }
            }
        }
    }
    private void PopulateCheckedValues1()
    {
        DataTable userdetails = (DataTable)Session["CHECKED_ITEMS1"];
        if (userdetails != null && userdetails.Rows.Count > 0)
        {
            foreach (GridViewRow gvrow in grdEmplyeeTask1.Rows)
            {
                int index = (int)grdEmplyeeTask1.DataKeys[gvrow.RowIndex].Value;
                DataRow[] matches = userdetails.Select("ID like '%" + index + "%'");
                foreach (DataRow row in matches)
                {
                    CheckBox myCheckBox = (CheckBox)gvrow.FindControl("Chk_Emp");
                    myCheckBox.Checked = true;
                    TextBox PageFrom = (TextBox)gvrow.FindControl("txtPageFrom");
                    PageFrom.Text = row["PageFrom"].ToString();
                    TextBox PageTo = (TextBox)gvrow.FindControl("txtPageTo");
                    PageTo.Text = row["PageTo"].ToString();
                    DropDownList WorkFlow = (DropDownList)gvrow.FindControl("ddlTask");
                    WorkFlow.SelectedValue = row["WorkFlow"].ToString();
                }
            }
        }
    }
    //This method is used to save the checkedstate of values
    private void SaveCheckedValues()
    {
        DataTable userdetails = new DataTable();
        userdetails.Columns.Add("ID");
        userdetails.Columns.Add("PageFrom");
        userdetails.Columns.Add("PageTo");
        userdetails.Columns.Add("Pages");
        userdetails.Columns.Add("Status");
        userdetails.Columns.Add("WorkFlow");
        int index = -1;
        foreach (GridViewRow gvrow in grdEmplyeeTask.Rows)
        {
            int pages = 0;
            string stu = "";
            index = (int)grdEmplyeeTask.DataKeys[gvrow.RowIndex].Value;
            bool result = ((CheckBox)gvrow.FindControl("Chk_Emp")).Checked;
            bool full = ((CheckBox)gvrow.FindControl("Chk_Full")).Checked;
            string PageFrom = ((TextBox)gvrow.FindControl("txtPageFrom")).Text;
            string PageTo = ((TextBox)gvrow.FindControl("txtPageTo")).Text;
            string WorkFlow = ((DropDownList)gvrow.FindControl("ddlTask")).SelectedValue;
            if (!full)
            {
                if (PageFrom.ToString() != "" && PageTo.ToString() != "")
                {
                    pages = Convert.ToInt32(PageTo) - Convert.ToInt32(PageFrom) + 1;
                    stu = "Y";
                }
                else
                {
                    pages = 0;
                    stu = "N";
                }
            }
            else
            {
                PageFrom = "1";
                PageTo = lblPages.Text;
                pages = Convert.ToInt32(lblPages.Text);
                stu = "F";
            }

            // Check in the Session
            if (Session["CHECKED_ITEMS"] != null)
                userdetails = (DataTable)Session["CHECKED_ITEMS"];
            if (result)
            {
                if (userdetails.Select("ID like '%" + index + "%'").Length==0)
                {
                    userdetails.Rows.Add(index, PageFrom, PageTo, pages, stu, WorkFlow);
                } 
            }
            else
            {
                DataRow[] matches = userdetails.Select("ID like '%" + index + "%'");
                foreach (DataRow row in matches)
                {
                    userdetails.Rows.Remove(row);
                }
            }
        }
        if (userdetails != null && userdetails.Rows.Count > 0)
            Session["CHECKED_ITEMS"] = userdetails;
    }
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
           return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    private void SaveCheckedValues1()
    {
        DataTable userdetails = new DataTable();
        userdetails.Columns.Add("ID");
        userdetails.Columns.Add("PageFrom");
        userdetails.Columns.Add("PageTo");
        userdetails.Columns.Add("Pages");
        userdetails.Columns.Add("Status");
        userdetails.Columns.Add("WorkFlow");
        int index = -1;
        foreach (GridViewRow gvrow in grdEmplyeeTask1.Rows)
        {
            int pages = 0;
            string stu = "";
            index = (int)grdEmplyeeTask1.DataKeys[gvrow.RowIndex].Value;
            bool result = ((CheckBox)gvrow.FindControl("Chk_Emp")).Checked;
            bool full = ((CheckBox)gvrow.FindControl("Chk_Full")).Checked;
            string PageFrom = ((TextBox)gvrow.FindControl("txtPageFrom")).Text;
            string PageTo = ((TextBox)gvrow.FindControl("txtPageTo")).Text;
            string WorkFlow = ((DropDownList)gvrow.FindControl("ddlTask")).SelectedValue;
            if (!full)
            {
                if (PageFrom.ToString() != "" && PageTo.ToString() != "")
                {
                    pages = Convert.ToInt32(PageTo) - Convert.ToInt32(PageFrom) + 1;
                    stu = "Y";
                }
                else
                {
                    pages = 0;
                    stu = "N";
                }
            }
            else
            {
                PageFrom = "0";
                PageTo = "0";
                pages = 0;
                stu = "F";
            }

            // Check in the Session
            if (Session["CHECKED_ITEMS1"] != null)
                userdetails = (DataTable)Session["CHECKED_ITEMS1"];
            if (result)
            {
                if (userdetails.Select("ID like '%" + index + "%'").Length == 0)
                {
                    userdetails.Rows.Add(index, PageFrom, PageTo, pages, stu, WorkFlow);
                }
            }
            else
            {
                DataRow[] matches = userdetails.Select("ID like '%" + index + "%'");
                foreach (DataRow row in matches)
                {
                    userdetails.Rows.Remove(row);
                }
            }
        }
        if (userdetails != null && userdetails.Rows.Count > 0)
            Session["CHECKED_ITEMS1"] = userdetails;
    }
    public SortDirection dir1
    {
        get
        {
            if (ViewState["dirState1"] == null)
            {
                ViewState["dirState1"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState1"];
        }
        set
        {
            ViewState["dirState1"] = value;
        }
    }

    protected void gv_job_allocation_Employee_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = (DataTable)Session["LaunchJobs_Emp"];
        DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        //dt = ds.Tables[0];
        {
            string SortDir = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                SortDir = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                SortDir = "Asc";
            }
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + SortDir;
            gv_job_allocation_Employee.DataSource = sortedView;
            gv_job_allocation_Employee.DataBind();
        }
    }
    private DropDownList bindDDL(DataTable oTable, DropDownList oList, string sFilter, string sColName)
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
            for (int k = 0; k < oList.Items.Count; k++)
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
    private DropDownList bindDDL1(DataTable oTable, DropDownList oList, string sFilter, string sColName)
    {
        string ovalue = oList.SelectedValue.ToString();
        if (!Page.IsPostBack && ovalue != "" && ovalue != "0" && ovalue != "zero")
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
            for (int k = 0; k < oList.Items.Count; k++)
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
        oList.SelectedIndexChanged += new EventHandler(oList_SelectedIndexChanged);
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
    protected void oList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable oTable = new DataTable();
            oTable = GetVoidViewTable().Table;
            if (Session["TabName"].ToString() == "RFD")
            {
                gv_job_all_Emp_RFD.DataSource = oTable;
                gv_job_all_Emp_RFD.DataBind();
            }
            else
            {
                gv_job_allocation_Employee.DataSource = oTable;
                gv_job_allocation_Employee.DataBind();
            }
            iRowID = 1;
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }
    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlSoft, "Soft_Name", "Soft_Name");
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlSoft, "Soft_Name", "Soft_Name");
    }
    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlSoft, "Soft_Name", "Soft_Name");
    }
    protected void ddlDueDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
        bindDDL(oTable, ddlSoft, "Soft_Name", "Soft_Name");
    }
    protected void ddlDueTime_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlSoft, "Soft_Name", "Soft_Name");
    }
    protected void ddlSoft_SelectedIndexChanged(object sender, EventArgs e)
    {
        oList_SelectedIndexChanged(sender, e);
        DataTable oTable = new DataTable();
        oTable = GetVoidViewTable().Table;
        bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
        bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
        bindDDL(oTable, ddlTask, "TaskName", "TaskName");
        bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
        bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
    }
    private DataView GetVoidViewTable()
    {
        string sFilterText = "";
        DataTable oTable = (DataTable)(Session["JobLiveDS1"]);
        DataView oview = oTable.DefaultView;
        oview.RowFilter = "";
        if (ddlcustomer.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "CUSTNAME='" + ddlcustomer.SelectedValue + "'";
        }
        if (ddlLocation.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "Location_Name='" + ddlLocation.SelectedValue + "'";
        }
        if (ddlTask.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "TaskName='" + ddlTask.SelectedValue + "'";
        }
        if (ddlDueDate.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "Date_IST='" + ddlDueDate.SelectedValue + "'";
        }
        if (ddlDueTime.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }

            sFilterText += "Time_IST<=#" + Convert.ToDateTime(ddlDueTime.SelectedValue.Replace(" IST", "")).ToString().Replace(DateTime.Now.ToShortDateString(), "01/01/1900").ToString() + "#";
        }
        if (ddlSoft.SelectedValue != "0")
        {
            if (Convert.ToString(sFilterText).Trim().Length > 0)
            {
                sFilterText = sFilterText + " AND ";
            }
            sFilterText += "Soft_Name='" + ddlSoft.SelectedValue + "'";
        }

        oview.RowFilter = sFilterText;
        return oview;
    }

    protected void gv_job_allocation_Employee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (e.Row.RowType == DataControlRowType.Header && !Page.IsPostBack)
        {
            if (Session["JobLiveDS1"] != null)
            {
                bindDDL(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
                bindDDL(oTable, ddlLocation, "Location_Name", "Location_Name");
                bindDDL(oTable, ddlTask, "TaskName", "TaskName");
                bindDDL(oTable, ddlDueDate, "Date_IST", "Date_IST");
                bindDDL(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
                bindDDL(oTable, ddlSoft, "Soft_Name", "Soft_Name");
            }

            DropDownList dd_Task = (DropDownList)e.Row.FindControl("dd_TASKNAME");
            DropDownList dd_Soft = (DropDownList)e.Row.FindControl("dd_SoftName");
            DropDownList dd_custname = (DropDownList)e.Row.FindControl("dd_custname");
            DataSet dd_ds = new DataSet();
            DataTable dd_dt = null;
            DataView oV = null;
            if (Session["LaunchJobs_Emp"] != null)
            {
                dd_dt = (DataTable)Session["LaunchJobs_Emp"];
                //dd_ds.Tables.Add(dd_dt);
            }
                //dd_ds = (DataSet)Session["LaunchJobs_Emp"];


            if (dd_Task != null)
            {
                dd_Task.DataTextField = "TASKNAME";
                dd_Task.DataValueField = "TASKNAME";

                oV = new DataView(dd_dt);
                DataTable jstable = oV.ToTable(true, "TASKNAME");
                DataRow selerow = jstable.NewRow();
                selerow["TASKNAME"] = "All TaskName";
                jstable.Rows.InsertAt(selerow, 0);

                dd_Task.DataSource = jstable;
                dd_Task.DataBind();
                if (jobTask_selectvalue != "")
                    dd_Task.SelectedValue = jobTask_selectvalue;
                else
                    dd_Task.SelectedValue = "All TaskName";

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
                    dd_Task.SelectedValue = jobSoft_selectvalue;
                else
                    dd_Task.SelectedValue = "All Software";

            }
            if (dd_custname != null)
            {
                dd_custname.DataTextField = "custname";
                dd_custname.DataValueField = "custname";

                oV = new DataView(dd_dt);
                DataTable jstable2 = oV.ToTable(true, "custname");
                DataRow selerow2 = jstable2.NewRow();
                selerow2["custname"] = "All Customers";
                jstable2.Rows.InsertAt(selerow2, 0);

                dd_custname.DataSource = jstable2;
                dd_custname.DataBind();
                if (jobCust_selectvalue != "")
                    dd_custname.SelectedValue = jobCust_selectvalue;
                else
                    dd_custname.SelectedValue = "All Customers";

            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;
            
            if (Session["TabName"].ToString() == "General")
            {
                if (lblStatus.Text != "Process")
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else if (Session["TabName"].ToString() == "QC")
            {
                if (lblStatus.Text != "QC1")
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else if (Session["TabName"].ToString() == "QCcorr")
            {
                if (lblStatus.Text != "QCcorr1")
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else if (Session["TabName"].ToString() == "QA")
            {
                if (lblStatus.Text != "QA1")
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else if (Session["TabName"].ToString() == "QAcorr")
            {
                if (lblStatus.Text != "QAcorr1")
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
            else if (Session["TabName"].ToString() == "RFD")
            {
                if (lblStatus.Text != "")
                {
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
            }
        }
    }
    protected void gv_job_all_Emp_RFD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hf_FP_ID = (HiddenField)e.Row.FindControl("hf_FP_ID");
            HiddenField h_fileststus = (HiddenField)e.Row.FindControl("h_fileststus");
            Button btnDel = (Button)e.Row.FindControl("btnDel");
            CheckBox chkBoxStatus = (CheckBox)e.Row.FindControl("chkBoxStatus");
            if (h_fileststus.Value == "6")
            {
                DataSet ds = new DataSet();
                ds = NL.GetFileDelStatus(hf_FP_ID.Value);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows[0]["del"].ToString() == "Y")
                    {
                        btnDel.Visible = true;
                        chkBoxStatus.Enabled = true;
                    }
                    else
                    {
                        btnDel.Visible = false;
                        chkBoxStatus.Enabled = false;
                    }
                }
            }
        }
        //if (e.Row.RowType == DataControlRowType.Header)
        //{         

        //    DropDownList dd_Task = (DropDownList)e.Row.FindControl("dd_TASKNAME");
        //    DropDownList dd_custname = (DropDownList)e.Row.FindControl("dd_custname");
        //    DropDownList dd_SoftName = (DropDownList)e.Row.FindControl("dd_SoftName");
        //    DataSet dd_ds = new DataSet();
        //    DataTable dd_dt = null;
        //    DataView oV = null;
        //    if (Session["LaunchJobs_Emp"] != null)
        //    {
        //        dd_dt = (DataTable)Session["LaunchJobs_Emp"];
        //        //dd_ds.Tables.Add(dd_dt);
        //    }
        //    //dd_ds = (DataSet)Session["LaunchJobs_Emp"];


        //    if (dd_Task != null)
        //    {
        //        dd_Task.DataTextField = "TASKNAME";
        //        dd_Task.DataValueField = "TASKNAME";

        //        oV = new DataView(dd_dt);
        //        DataTable jstable = oV.ToTable(true, "TASKNAME");
        //        DataRow selerow = jstable.NewRow();
        //        selerow["TASKNAME"] = "All TaskName";
        //        jstable.Rows.InsertAt(selerow, 0);

        //        dd_Task.DataSource = jstable;
        //        dd_Task.DataBind();
        //        if (jobTask_selectvalue != "")
        //            dd_Task.SelectedValue = jobTask_selectvalue;
        //        else
        //            dd_Task.SelectedValue = "All TaskName";

        //    }
        //    if (dd_custname != null)
        //    {
        //        dd_custname.DataTextField = "custname";
        //        dd_custname.DataValueField = "custname";

        //        oV = new DataView(dd_dt);
        //        DataTable jstable1 = oV.ToTable(true, "custname");
        //        DataRow selerow1 = jstable1.NewRow();
        //        selerow1["custname"] = "All Customers";
        //        jstable1.Rows.InsertAt(selerow1, 0);

        //        dd_custname.DataSource = jstable1;
        //        dd_custname.DataBind();
        //        if (jobCust_selectvalue != "")
        //            dd_custname.SelectedValue = jobCust_selectvalue;
        //        else
        //            dd_custname.SelectedValue = "All Customers";

        //    }
        //    if (dd_SoftName != null)
        //    {
        //        dd_SoftName.DataTextField = "Soft_Name";
        //        dd_SoftName.DataValueField = "Soft_Name";

        //        oV = new DataView(dd_dt);
        //        DataTable jstable2 = oV.ToTable(true, "Soft_Name");
        //        DataRow selerow2 = jstable2.NewRow();
        //        selerow2["Soft_Name"] = "All Software";
        //        jstable2.Rows.InsertAt(selerow2, 0);

        //        dd_SoftName.DataSource = jstable2;
        //        dd_SoftName.DataBind();
        //        if (jobSoft_selectvalue != "")
        //            dd_SoftName.SelectedValue = jobSoft_selectvalue;
        //        else
        //            dd_SoftName.SelectedValue = "All Software";

        //    }
        //}
    }
    private void showmessage(string msg)
    {
        if (!string.IsNullOrEmpty(msg))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }
    protected void gv_job_all_Emp_RFD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
        if (e.CommandName == "Delivery")
        {
            GridViewRow row1 = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            paramCol = ((HiddenField)row.FindControl("hf_UA_ID")).Value.ToString() + "," + e.CommandArgument.ToString() + "," + ((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString() + "," + Session["employeeid"].ToString();
            paramname = "@UA_ID,@FP_ID,@LP_ID,@EmpID";
            paramtype = "int,int,int,int";
            paramdir = "Input,Input,Input,Input";
        }
        SqlObj.ExcuteProcedure("spLPUpdateDelivery1", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
        SqlObj = null;
        

        //DataSet ds = new DataSet();
        //ds = NL.GetLPMaxDelAmends(((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString());
        //if (ds.Tables.Count>0)
        //{
        //    if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del" && ds.Tables[0].Rows[0]["JobNo"].ToString() == "")
        //    {
        //        DataSet ms = new DataSet();
        //        ms = NL.GetMailStatusLP(((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString());
        //        if (ms.Tables.Count == 0)
        //        {
        //            MailMessage oMsg = new MailMessage();
        //            SmtpClient oSmtp = new SmtpClient();
        //            oMsg = new MailMessage();
        //            oSmtp.Host = "smtp.gmail.com";
        //            oSmtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"]);
        //            oMsg.From = new MailAddress("software@datapage.org");
        //            oSmtp.Credentials = new System.Net.NetworkCredential(oMsg.From.ToString(), ConfigurationManager.AppSettings["accounts_password"].ToString());
        //            oMsg.CC.Add("projects@datapage.org");
        //            oMsg.To.Add("thinakaran@datapage.org");
        //            oMsg.CC.Add("software@datapage.org");
        //            oMsg.Subject = ds.Tables[0].Rows[0]["Jobid"].ToString().Trim() + " - Work Order number Required";
        //            string strLog = string.Empty;
        //            strLog = "Hi Team," + "\r\n\r\n";
        //            strLog += " Please Enter the Work Order number for this Project: (" + ds.Tables[0].Rows[0]["Jobid"].ToString().Trim() + " - " + ds.Tables[0].Rows[0]["ProjectName"].ToString().Trim() + ") in Launch Overview.  \r\n\r\n";
        //            strLog += "Thanks and Regards, " + "  \r\n";
        //            strLog += "Technical Team,  \r\n";
        //            strLog += "Datapage  \r\n";
        //            oMsg.Body = strLog.ToString().Trim();
        //            oSmtp.EnableSsl = true;
        //            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        //            {
        //                return true;
        //            };
        //            oSmtp.Send(oMsg);
        //            showmessage("Mailsent Successfully");
        //            if (ds.Tables[0].Rows[0]["LP"].ToString().Trim() == "Y")
        //            {
        //                NL.UpdateDespatchLP(((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString());
        //                NL.UpdateMailStatusLP(((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString());
        //            }
        //            else
        //            {
        //                NL.UpdateDespatchNL(((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString());
        //                NL.UpdateMailStatusNL(((HiddenField)row.FindControl("hf_Job_ID")).Value.ToString());
        //            }
        //        }
        //    }
        //}

        if (Session["TabName"].ToString() == "General")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstGeneral);
            this.GetJobAllocationList("0", "1");
        }
        else if (Session["TabName"].ToString() == "TE")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstTE);
            this.GetJobAllocationList("1", "1");
        }
        else if (Session["TabName"].ToString() == "DTP")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstDTP);
            this.GetJobAllocationList("3", "1");
        }
        else if (Session["TabName"].ToString() == "PreDTP")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstPreDTP);
            this.GetJobAllocationList("2", "1");
        }
        else if (Session["TabName"].ToString() == "DQA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstDQA);
            this.GetJobAllocationList("4", "1");
        }
        else if (Session["TabName"].ToString() == "OVA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstOVA);
            this.GetJobAllocationList("5", "1");
        }
        else if (Session["TabName"].ToString() == "QC")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQC);
            this.GetJobAllocationList("0", "2");
        }
        else if (Session["TabName"].ToString() == "QCcorr")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQCcorr);
            this.GetJobAllocationList("0", "3");
        }
        else if (Session["TabName"].ToString() == "QA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQA);
            this.GetJobAllocationList("0", "4");
        }
        else if (Session["TabName"].ToString() == "QAcorr")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQAcorr);
            this.GetJobAllocationList("0", "5");
        }
        else if (Session["TabName"].ToString() == "RFD")
        {
            gv_job_allocation_Employee.Visible = false;
            gv_job_all_Emp_RFD.Visible = true;
            this.showpanel(LstRFD);
            this.GetJobAllocationList("0", "6");
        }
        Page_Load(null, null);
    }
    protected void grdEmplyeeTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlTask = e.Row.FindControl("ddlTask") as DropDownList;
            if (Session["TabName"].ToString() == "General")
            {
                ddlTask.SelectedValue = "1";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QC")
            {
                ddlTask.SelectedValue = "2";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QCcorr")
            {
                ddlTask.SelectedValue = "3";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QA")
            {
                ddlTask.SelectedValue = "4";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QAcorr")
            {
                ddlTask.SelectedValue = "5";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "RFD")
            {
                ddlTask.SelectedValue = "6"; 
                ddlTask.Enabled = false;

            }
        }
    }
    protected void grdEmplyeeTask1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlTask = e.Row.FindControl("ddlTask") as DropDownList;
            if (Session["TabName"].ToString() == "General")
            {
                ddlTask.SelectedValue = "1";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QC")
            {
                ddlTask.SelectedValue = "2";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QCcorr")
            {
                ddlTask.SelectedValue = "3";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QA")
            {
                ddlTask.SelectedValue = "4";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "QAcorr")
            {
                ddlTask.SelectedValue = "5";
                ddlTask.Enabled = false;
            }
            else if (Session["TabName"].ToString() == "RFD")
            {
                ddlTask.SelectedValue = "6";
                ddlTask.Enabled = false;

            }
        }
    }
    protected void dd_TASKNAME_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = new DataTable();
        DataView dv = new DataView();
        dset = (DataTable)Session["LaunchJobs_Emp"];
        dv = dset.DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[TASKNAME]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        jobTask_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobSoft_selectvalue = "All Software";
        jobCust_selectvalue = "All Customers";

        gv_job_allocation_Employee.DataSource = dv;
        gv_job_allocation_Employee.DataBind();
        gv_job_all_Emp_RFD.DataSource = dv;
        gv_job_all_Emp_RFD.DataBind();
    }
    protected void dd_custname_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = new DataTable();
        DataView dv = new DataView();
        dset = (DataTable)Session["LaunchJobs_Emp"];
        dv = dset.DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[custname]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        jobCust_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobSoft_selectvalue = "All Software";
        jobTask_selectvalue = "All TaskName";
        gv_job_allocation_Employee.DataSource = dv;
        gv_job_allocation_Employee.DataBind();
        gv_job_all_Emp_RFD.DataSource = dv;
        gv_job_all_Emp_RFD.DataBind();
    }
    protected void dd_SoftName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dset = new DataTable();
        DataView dv = new DataView();
        dset = (DataTable)Session["LaunchJobs_Emp"];
        dv = dset.DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[Soft_Name]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        jobSoft_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        jobCust_selectvalue = "All Customers";
        jobTask_selectvalue = "All TaskName";

        gv_job_allocation_Employee.DataSource = dv;
        gv_job_allocation_Employee.DataBind();
        gv_job_all_Emp_RFD.DataSource = dv;
        gv_job_all_Emp_RFD.DataBind();
    }
    protected void lnkQC_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstQC);
        this.GetJobAllocationList("0", "2");
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (Session["JobLiveDS1"] != null)
        {
            bindDDL1(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
            bindDDL1(oTable, ddlLocation, "Location_Name", "Location_Name");
            bindDDL1(oTable, ddlTask, "TaskName", "TaskName");
            bindDDL1(oTable, ddlDueDate, "Date_IST", "Date_IST");
            bindDDL1(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
            bindDDL1(oTable, ddlSoft, "Soft_Name", "Soft_Name");
        }
    }
    protected void lnkQCcorr_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstQCcorr);
        this.GetJobAllocationList("0", "3");
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (Session["JobLiveDS1"] != null)
        {
            bindDDL1(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
            bindDDL1(oTable, ddlLocation, "Location_Name", "Location_Name");
            bindDDL1(oTable, ddlTask, "TaskName", "TaskName");
            bindDDL1(oTable, ddlDueDate, "Date_IST", "Date_IST");
            bindDDL1(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
            bindDDL1(oTable, ddlSoft, "Soft_Name", "Soft_Name");
        }
    }
    protected void lnkQA_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstQA);
        this.GetJobAllocationList("0", "4");
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (Session["JobLiveDS1"] != null)
        {
            bindDDL1(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
            bindDDL1(oTable, ddlLocation, "Location_Name", "Location_Name");
            bindDDL1(oTable, ddlTask, "TaskName", "TaskName");
            bindDDL1(oTable, ddlDueDate, "Date_IST", "Date_IST");
            bindDDL1(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
            bindDDL1(oTable, ddlSoft, "Soft_Name", "Soft_Name");
        }
    }
    protected void lnkQAcorr_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = true;
        gv_job_all_Emp_RFD.Visible = false;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstQAcorr);
        this.GetJobAllocationList("0", "5");
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (Session["JobLiveDS1"] != null)
        {
            bindDDL1(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
            bindDDL1(oTable, ddlLocation, "Location_Name", "Location_Name");
            bindDDL1(oTable, ddlTask, "TaskName", "TaskName");
            bindDDL1(oTable, ddlDueDate, "Date_IST", "Date_IST");
            bindDDL1(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
            bindDDL1(oTable, ddlSoft, "Soft_Name", "Soft_Name");
        }
    }
    protected void lnkRFD_Click(object sender, EventArgs e)
    {
        gv_job_allocation_Employee.Visible = false;
        gv_job_all_Emp_RFD.Visible = true;
        //getEmployee();
        BindGrid1();
        this.showpanel(LstRFD);
        this.GetJobAllocationList("0", "6");
        DataTable oTable = (DataTable)Session["JobLiveDS1"];
        if (Session["JobLiveDS1"] != null)
        {
            bindDDL1(oTable, ddlcustomer, "CUSTNAME", "CUSTNAME");
            bindDDL1(oTable, ddlLocation, "Location_Name", "Location_Name");
            bindDDL1(oTable, ddlTask, "TaskName", "TaskName");
            bindDDL1(oTable, ddlDueDate, "Date_IST", "Date_IST");
            bindDDL1(oTable, ddlDueTime, "DUETIMEFROM_IST", "DUETIMEFROM_IST");
            bindDDL1(oTable, ddlSoft, "Soft_Name", "Soft_Name");
        }
    }
    protected void gvAllocatedEmp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        
        if (e.CommandName == "Remove")
        {
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + "," + ((Label)row.FindControl("lblENO")).Text.ToString() + "," + ((Label)row.FindControl("lblA_ID")).Text.ToString();
            paramname = "@AEID,@ENO,@A_ID";
            paramtype = "int,int,int";
            paramdir = "Input,Input,Input";
            SqlObj.ExcuteProcedure("spDelAllocatedEmp", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
        }
        
        SqlObj = null;
        string FileStatus = "";
        if (Session["TabName"].ToString() == "General")
            FileStatus = "1";
        else if (Session["TabName"].ToString() == "QC")
            FileStatus = "2";
        else if (Session["TabName"].ToString() == "QCcorr")
            FileStatus = "3";
        else if (Session["TabName"].ToString() == "QA")
            FileStatus = "4";
        else if (Session["TabName"].ToString() == "QAcorr")
            FileStatus = "5";
        else if (Session["TabName"].ToString() == "RFD")
            FileStatus = "6";
        //Label hf_job_id = (Label)row.FindControl("lblLP_ID");
        //Label hf_job_FP_id = (Label)row.FindControl("lblFP_ID");
        //Label hf_job_history_id = (Label)row.FindControl("lblJob_His_ID");
        //string strJobId = Convert.ToString(hf_job_id.Text).Trim();
        //string strJobHistoryId = Convert.ToString(hf_job_history_id.Text).Trim();
        //string strJobTaskId = Convert.ToString(hf_job_FP_id.Text).Trim();
        DataSet dsEmp = new DataSet();
        dsEmp = NL.GetAllocatedEmpPages1(PJobID.Text, PFP_ID.Text, PJob_His_ID.Text, FileStatus.ToString());
        gvAllocatedEmp.DataSource = dsEmp;
        gvAllocatedEmp.DataBind();
        if (Session["TabName"].ToString() == "General")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstGeneral);
            this.GetJobAllocationList("0", "1");
        }
        else if (Session["TabName"].ToString() == "TE")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstTE);
            this.GetJobAllocationList("1", "1");
        }
        else if (Session["TabName"].ToString() == "DTP")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstDTP);
            this.GetJobAllocationList("3", "1");
        }
        else if (Session["TabName"].ToString() == "PreDTP")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstPreDTP);
            this.GetJobAllocationList("2", "1");
        }
        else if (Session["TabName"].ToString() == "DQA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstDQA);
            this.GetJobAllocationList("4", "1");
        }
        else if (Session["TabName"].ToString() == "OVA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstOVA);
            this.GetJobAllocationList("5", "1");
        }
        else if (Session["TabName"].ToString() == "QC")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQC);
            this.GetJobAllocationList("0", "2");
        }
        else if (Session["TabName"].ToString() == "QCcorr")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQCcorr);
            this.GetJobAllocationList("0", "3");
        }
        else if (Session["TabName"].ToString() == "QA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQA);
            this.GetJobAllocationList("0", "4");
        }
        else if (Session["TabName"].ToString() == "QAcorr")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQAcorr);
            this.GetJobAllocationList("0", "5");
        }
        else if (Session["TabName"].ToString() == "RFD")
        {
            gv_job_allocation_Employee.Visible = false;
            gv_job_all_Emp_RFD.Visible = true;
            this.showpanel(LstQAcorr);
            this.GetJobAllocationList("0", "6");
        }
        Page_Load(null, null);
        popup1.Show();
    }
    protected void ibtn_Delivery_click(object sender, ImageClickEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        bool cks = false;
        foreach (GridViewRow grw in gv_job_all_Emp_RFD.Rows)
        {
            CheckBox ck = ((CheckBox)grw.FindControl("chkBoxStatus"));
            if (ck.Checked)
            {
                paramCol = ((HiddenField)grw.FindControl("hf_UA_ID")).Value.ToString() + "," + ((HiddenField)grw.FindControl("hf_FP_ID")).Value.ToString() + "," + ((HiddenField)grw.FindControl("hf_Job_ID")).Value.ToString() + "," + Session["employeeid"].ToString();
                paramname = "@UA_ID,@FP_ID,@LP_ID,@EmpID";
                paramtype = "int,int,int,int";
                paramdir = "Input,Input,Input,Input";
                SqlObj.ExcuteProcedure("spLPUpdateDelivery1", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            }
        }
        SqlObj = null;

        if (Session["TabName"].ToString() == "General")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstGeneral);
            this.GetJobAllocationList("0", "1");
        }
        else if (Session["TabName"].ToString() == "TE")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstTE);
            this.GetJobAllocationList("1", "1");
        }
        else if (Session["TabName"].ToString() == "DTP")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstDTP);
            this.GetJobAllocationList("3", "1");
        }
        else if (Session["TabName"].ToString() == "PreDTP")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstPreDTP);
            this.GetJobAllocationList("2", "1");
        }
        else if (Session["TabName"].ToString() == "DQA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstDQA);
            this.GetJobAllocationList("4", "1");
        }
        else if (Session["TabName"].ToString() == "OVA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstOVA);
            this.GetJobAllocationList("5", "1");
        }
        else if (Session["TabName"].ToString() == "QC")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQC);
            this.GetJobAllocationList("0", "2");
        }
        else if (Session["TabName"].ToString() == "QCcorr")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQCcorr);
            this.GetJobAllocationList("0", "3");
        }
        else if (Session["TabName"].ToString() == "QA")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQA);
            this.GetJobAllocationList("0", "4");
        }
        else if (Session["TabName"].ToString() == "QAcorr")
        {
            gv_job_allocation_Employee.Visible = true;
            gv_job_all_Emp_RFD.Visible = false;
            this.showpanel(LstQAcorr);
            this.GetJobAllocationList("0", "5");
        }
        else if (Session["TabName"].ToString() == "RFD")
        {
            gv_job_allocation_Employee.Visible = false;
            gv_job_all_Emp_RFD.Visible = true;
            this.showpanel(LstRFD);
            this.GetJobAllocationList("0", "6");
        }
        Page_Load(null, null);
    }
}