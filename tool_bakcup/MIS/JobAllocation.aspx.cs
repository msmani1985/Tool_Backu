using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using System.Net;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;


//created by R.Mugundhan
public partial class JobAllocation : System.Web.UI.Page
{
    DropDownList drpTask;
    string dept_id = "";
    string emp_team_id = "";
    string job_type_id = "";
    string job_id = "";
    string emp_id = "";
    string stage_id;
    public Int32 iRowId = 1;
    bool isTeamLead = false;
    int i = 0;
    HtmlGenericControl shwpanel;
    public int intDept = 0;

    datasourceSQL DSQL = new datasourceSQL();

    protected void Page_Load(object sender, EventArgs e)
    {
        //WebClient web = new WebClient();
        //string url = string.Format("http://rate-exchange.appspot.com/currency?from={0}&to={1}", "EUR", "GBP");
        //string response = web.DownloadString(url);
        //Regex regex = new Regex("rhs: \\\"(\\d*.\\d*)");
        //Rate rate = new JavaScriptSerializer().Deserialize<Rate>(response);
       

        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            emp_team_id = Session["employeeteamid"].ToString();
            emp_id = Session["employeeid"].ToString();
            isTeamLead = Convert.ToBoolean(Session["timesheet"]);

            //if (Session["employeeid"] == null)
            //{
            //    Response.Redirect("login.aspx");
            //}

            if (!Page.IsPostBack)
            {
                DataSet DS = new DataSet();
                //DS = DSQL.GetTask();
                //ddl_task.DataSource = DS;
                //ddl_task.DataTextField = "task_name";
                //ddl_task.DataValueField = "task_id";
                //ddl_task.DataBind();
                //ddl_task.Items.Insert(0,new ListItem( "--select--","0"));
                //ddl_task.SelectedIndex = 0;

                DS = DSQL.GetJobItem();
                ddl_department.DataSource = DS;
                ddl_department.DataTextField = "employee_team_name";
                ddl_department.DataValueField = "employee_team_id";
                ddl_department.DataBind();
                ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
                Session["employee_team"] = DS;

                ddl_employees.Items.Insert(0, new ListItem("--select--", "0"));
                ddl_task.Items.Insert(0, new ListItem("--select--", "0"));
                //DS = DSQL.GetJobItem();
                lnkGeneral_Click(null, null);
                this.showpanel(LstGeneral);
                //gv_job_allocation_Employee.Visible = true;

                //tabGeneral.Visible = false;

                if (!isTeamLead)
                {
                    ddl_task.Enabled = false;
                    ddl_department.Enabled = false;
                    ddl_employees.Enabled = false;
                    btn_Move.Enabled = false;
                }

                if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
                {
                    ddl_employees.Visible = false;
                    lbl_employees.Visible = false;

                    ddl_task.Visible = false;
                    lbl_task.Visible = false;

                    ddl_department.Visible = true;
                    lbl_department.Visible = true;
                    Panel2.Visible = false;
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
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstGeneral";

                break;
            case "LstAMO":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "current");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstAMO";

                break;

            case "LstAMOEP":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "current");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstAMOEP";

                break;

            case "LstPreEditing":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "current");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstPreEditing";

                break;
            case "LstCollation":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "current");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstCollation";

                break;

            case "LstDeft":
                Session["TabName"] = null;
                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "current");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstDeft";

                break;

            case "LstArtWork":
                Session["TabName"] = null;

                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "current");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");

                Session["TabName"] = "LstArtWork";
                break;

            case "LstCE":
                Session["TabName"] = null;

                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "current");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");

                Session["TabName"] = "LstCE";
                break;

            case "LstPagination":
                Session["TabName"] = null;

                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "current");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstPagination";

                break;

            case "LstQC":
                Session["TabName"] = null;

                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "current");
                LstEPD.Attributes.Add("class", "");
                Session["TabName"] = "LstQC";

                break;

            case "LstEPD":
                Session["TabName"] = null;

                LstGeneral.Attributes.Add("class", "");
                LstAMO.Attributes.Add("class", "");
                LstAMOEP.Attributes.Add("class", "");
                LstPreEditing.Attributes.Add("class", "");
                LstCollation.Attributes.Add("class", "");
                LstDeft.Attributes.Add("class", "");
                LstArtWork.Attributes.Add("class", "");
                LstCE.Attributes.Add("class", "");
                LstPagination.Attributes.Add("class", "");
                LstQC.Attributes.Add("class", "");
                LstEPD.Attributes.Add("class", "current");

                Session["TabName"] = "LstEPD";
                break;


        }

    }


    //private void Data()
    //{
    //    DataSet DSET = new DataSet();
    //    DSET = DSQL.GetJobAllocation();

    //    tabGeneral.Visible = true;

    //    gv_job_allocation.DataSource = DSET;
    //    gv_job_allocation.DataBind();

    //}

    private void GetJobAllocationList(string deptid, string stageid)
    {
        if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
        {

            DataSet dst_dept = new DataSet();
            dst_dept = DSQL.GetJobAllocation(deptid, stageid);

            gv_job_allocation.DataSource = null;
            if (dst_dept != null && dst_dept.Tables[0].Rows.Count > 0)
            {
                gv_job_allocation.DataSource = dst_dept.Tables[0];
            }
            gv_job_allocation.DataBind();
            gv_job_allocation.Visible = true;

        }

        else if (ddl_MoveSelection.SelectedItem.Value.ToString() == "2")
        {
            DataSet dst_emp = new DataSet();
            //DataSet dst = new DataSet();
            dst_emp = DSQL.GetJobAllocationEmployee(deptid, stageid);
            if (dst_emp != null)
            {
                Session["Jobs_Employee"] = dst_emp.Tables[0];
            }
            if (dst_emp != null && dst_emp.Tables[0].Rows.Count > 0)
            {
                gv_job_allocation_Employee.DataSource = dst_emp.Tables[0];
            }
            gv_job_allocation_Employee.DataBind();
            gv_job_allocation_Employee.Visible = true;

        }



    }

    private void GetJobAllocationList(string deptid, string empteamid, string jobtypeid, string empid)
    {




        //dst = DSQL.GetJobAllocation(Session["department_id"], Session["employee_team_id"], dd);
        //DataTable dt = new DataTable();
        //int activeTabIndex = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["LstGeneral"].Value));
        // if (this.showpanel(LstPagination))

        //if(LstPagination)
        //{
        //    dept_id = "81";
        //}

        empid = emp_id;
        empteamid = emp_team_id;

        //DataSet dst = new DataSet();

        //if (ddl_MoveSelection.Visible == true)
        //{

        if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
        {
            //DataSet dst = new DataSet();
            DataSet dst_dept = new DataSet();
            dst_dept = DSQL.GetJobAllocation(deptid, empteamid, jobtypeid, empid);

            //string[,] param ={ { "@department_id", dept_id.ToString() }, { "@employee_team_id", emp_team_id.ToString() }, { "@job_type_id", job_type_id.ToString() }, { "@employee_id", emp_id.ToString() } };
            //dst = DSQL.ExcProcedure("spGet_job_allocation", param, CommandType.StoredProcedure);

            //gv_job_allocation.DataSource = dst;
            //gv_job_allocation.DataBind();
            gv_job_allocation.DataSource = null;
            if (dst_dept != null && dst_dept.Tables[0].Rows.Count > 0)
            {
                gv_job_allocation.DataSource = dst_dept.Tables[0];
            }
            gv_job_allocation.DataBind();
            gv_job_allocation.Visible = true;

        }

        else if (ddl_MoveSelection.SelectedItem.Value.ToString() == "2")
        {
            DataSet dst_emp = new DataSet();
            //DataSet dst = new DataSet();
            dst_emp = DSQL.GetJobAllocationEmployee(deptid, empteamid, jobtypeid, empid);
            if (dst_emp != null)
            {
                Session["Jobs_Employee"] = dst_emp.Tables[0];
            }
            if (dst_emp != null && dst_emp.Tables[0].Rows.Count > 0)
            {
                gv_job_allocation_Employee.DataSource = dst_emp.Tables[0];
            }
            gv_job_allocation_Employee.DataBind();
            gv_job_allocation_Employee.Visible = true;

           
        }

        // int activeTabIndex = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["tabIndex"].Value));
        //int ex = Convert.ToInt32(Server.HtmlEncode(Request.Cookies[
        //}
        //else
        //{
        //    dst = DSQL.GetJobAllocationEmployee(deptid, empteamid, jobtypeid, empid);

        //    if (dst != null && dst.Tables[0].Rows.Count > 0)
        //    {
        //        gv_job_allocation_Employee.DataSource = dst.Tables[0];
        //    }
        //    gv_job_allocation_Employee.DataBind();
        //    gv_job_allocation_Employee.Visible = true;
        //}

    }


    protected void lnkGeneral_Click(object sender, EventArgs e)
    {
        intDept = 0;
        ddl_MoveSelection.Visible = false;
        lbl_selection.Visible = false;
        this.GetJobAllocationList("", emp_team_id, job_type_id, emp_id);
        ddl_MoveSelection_SelectedIndexChanged(null, null);
        ddl_employees.Items.Clear();
        ddl_employees.Items.Insert(0, new ListItem("--select--", "0"));
        ddl_task.Items.Clear();
        ddl_task.Items.Insert(0, new ListItem("--select--", "0"));
        showpanel(LstGeneral);
        getEmployee();
        //Session["employee_team"] = DS;
        ddl_department.DataSource = Session["employee_team"];
        ddl_department.DataBind();
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));


        if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
        {
            ddl_department.Visible = true;
            lbl_department.Visible = true;

            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            Panel1.Visible = true;
            Panel2.Visible = false;

            gv_job_allocation.Visible = true;
            gv_job_allocation_Employee.Visible = false;


        }
        else
        {
            ddl_department.Visible = true;
            lbl_department.Visible = true;
            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            gv_job_allocation.Visible = false;
            gv_job_allocation_Employee.Visible = true;
        }

        PanelAllign(0);
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**
        //DataSet dst = new DataSet();
        //string[,] param ={ { "@department_id", dept_id.ToString() }, { "@employee_team_id", emp_team_id.ToString() }, { "@job_type_id", job_type_id.ToString() }, { "@employee_id", emp_id.ToString() } };
        //dst = DSQL.ExcProcedure("spGet_job_allocation", param, CommandType.StoredProcedure);
    }

    private void FilterJobItem(string filter)
    {
        DataSet Dset = new DataSet();
        DataView DV = new DataView();

        Dset = (DataSet)Session["employee_team"];
        DV = new DataView(Dset.Tables[0]);
        DV.RowFilter = filter;
        ddl_department.DataSource = DV;
        ddl_department.DataBind();

    }

    protected void TaskList(string task_id)
    {
        DataSet DSET = new DataSet();
        DSET = DSQL.GetJobItemTask(task_id);
        ddl_task.DataTextField = "task_name";
        ddl_task.DataValueField = "task_id";
        ddl_task.DataSource = DSET;
        ddl_task.DataBind();
        ddl_task.Items.Insert(0, new ListItem("--select--", "0"));
    }

    protected void AllotedTaskList(DropDownList drpTask,string task_id)
    {
        DataSet DSET = new DataSet();
        DSET = DSQL.GetJobItemTask(task_id);
        drpTask.DataTextField = "task_name";
        drpTask.DataValueField = "task_id";
        drpTask.DataSource = DSET;
        drpTask.DataBind();
        drpTask.Items.Insert(0, new ListItem("--select--", "0"));
    }

    protected void EmployeeList(string emp_dept_id)
    {

        if (emp_team_id != "89")
        {
            DataSet DSET = new DataSet();
            DSET = DSQL.fillteam(emp_dept_id);
            ddl_employees.DataTextField = "employee_fullname";
            ddl_employees.DataValueField = "employee_id";
            ddl_employees.DataSource = DSET;
            ddl_employees.DataBind();
            ddl_employees.Items.Insert(0, new ListItem("--select--", "0"));
        }
        else
        {
            DataSet DSET = new DataSet();
            DSET = DSQL.fillteam("89");
            ddl_employees.DataTextField = "employee_fullname";
            ddl_employees.DataValueField = "employee_id";
            ddl_employees.DataSource = DSET;
            ddl_employees.DataBind();
            ddl_employees.Items.Insert(0, new ListItem("--select--", "0"));
        }
    }

    protected void lnkDEFT_Click(object sender, EventArgs e)
    {
        visibility();
        int deptid = 82;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        showpanel(LstDeft);
        getEmployee();
        FilterJobItem("employee_team_id<>82");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**

        //if (showpanel(shwpanel.ID==""))
        //{

        //}

        PanelAllign(1);

        //Data();

    }


    protected void lnkArtWork_Click(object sender, EventArgs e)
    {
        visibility();
        showpanel(LstArtWork);
        int deptid = 84;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>84");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**
        PanelAllign(1);

        //Data();

    }

    protected void lnkAMO_Click(object sender, EventArgs e)
    {
        visibility();
        int deptid1 = 82;
        int stageid1 = 10106;
        intDept = 82;
        string dept_id = Convert.ToString(deptid1);
        string stage_id = Convert.ToString(stageid1);
        this.GetJobAllocationList(dept_id, stage_id);
        showpanel(LstAMO);
        getEmployee();
        FilterJobItem("employee_team_id<>82");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        PanelAllign(1);
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**
    }
    protected void lnkAMOEP_Click(object sender, EventArgs e)
    {
        intDept = 83;
        visibility();
        showpanel(LstAMOEP);
        int deptid1 = 83;
        int stageid1 = 81;
        string dept_id = Convert.ToString(deptid1);
        string stage_id = Convert.ToString(stageid1);
        this.GetJobAllocationList(dept_id, stage_id);
        getEmployee();
        FilterJobItem("employee_team_id<>83");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**
        PanelAllign(1);
    }
    protected void lnkCollation_Click(object sender, EventArgs e)
    {
        intDept = 0;
        visibility();
        showpanel(LstCollation);
        int deptid = 91;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>91");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**
        PanelAllign(1);
    }
    protected void lnkCE_Click(object sender, EventArgs e)
    {
        intDept = 0;
        visibility();
        showpanel(LstCE);
        int deptid = 89;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>89");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
        gv_job_allocation_Employee.Columns[10].Visible = true;
        //**-**End Added By Subash.T on 25-08-2014**-**

        PanelAllign(1);
        //Data();

    }

    protected void lnkPE_Click(object sender, EventArgs e)
    {
        intDept = 0;
        visibility();
        showpanel(LstPreEditing);
        int deptid = 73;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>73");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
        gv_job_allocation_Employee.Columns[10].Visible = true;
        //**-**End Added By Subash.T on 25-08-2014**-**
        PanelAllign(1);
        //Data();

    }

    protected void lnkPagination_Click(object sender, EventArgs e)
    {
        intDept = 0;
        visibility();
        showpanel(LstPagination);
        int deptid = 81;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>81");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**

        PanelAllign(1);
        //Data();

    }

    protected void lnkQC_Click(object sender, EventArgs e)
    {
        intDept = 0;
        visibility();
        showpanel(LstQC);
        int deptid = 85;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>85");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**
        PanelAllign(1);

        //Data();

    }

    protected void lnkEPD_Click(object sender, EventArgs e)
    {
        intDept = 0;
        visibility();
        showpanel(LstEPD);
        int deptid = 83;
        string dept_id = Convert.ToString(deptid);
        this.GetJobAllocationList(dept_id, emp_team_id, job_type_id, emp_id);
        getEmployee();
        FilterJobItem("employee_team_id<>83");
        ddl_department.Items.Insert(0, new ListItem("--select--", "0"));
        EmployeeList(dept_id);
        TaskList(dept_id);
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;
        //**-**Begin Added By Subash.T on 25-08-2014**-**
       // gv_job_allocation_Employee.Columns[10].Visible = false;
        //**-**End Added By Subash.T on 25-08-2014**-**s
        PanelAllign(1);


        //Data();

    }



    private SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }

    }

    protected void gv_general_Sorting(object sender, GridViewSortEventArgs e)
    {

        {
            string sortingDirection = string.Empty;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                sortingDirection = "Asc";
            }
            DataSet dst_sort = new DataSet();

            //    //dst = DSQL.GetJobAllocation();
            //    DataTable datatable = gv_job_allocation.DataSource as DataTable;
            DataTable datatable = gv_job_allocation.DataSource as DataTable;
            dst_sort = DSQL.GetJobAllocation(dept_id, emp_team_id, job_type_id, emp_id);
            datatable = dst_sort.Tables[0];
            if (datatable != null)
            {
                DataView sortedView = new DataView(datatable);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                gv_job_allocation.DataSource = sortedView;
                gv_job_allocation.DataBind();
            }
        }
    }

    //protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet DSET = new DataSet();
    //    DSET = DSQL.GetJobItemTask(ddl_department.SelectedValue.ToString());
    //    ddl_task.DataTextField = "task_name";
    //    ddl_task.DataValueField = "task_id";
    //    ddl_task.DataSource = DSET;
    //    ddl_task.DataBind();
    //    DSET = DSQL.fillteam(ddl_department.SelectedValue.ToString());
    //    ddl_employees.DataTextField = "employee_fullname";
    //    ddl_employees.DataValueField = "employee_id";
    //    ddl_employees.DataSource = DSET;
    //    ddl_employees.DataBind();

    //    ddl_task.Items.Insert(0, new ListItem("--select--", "0"));
    //    ddl_employees.Items.Insert(0, new ListItem("--select--", "0"));

    //}

    protected void ibtnExcel_Export_Click(object sender, ImageClickEventArgs e)
    {

        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename= JobAllocation.xls");
        this.EnableViewState = false;
        System.IO.StringWriter strwriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
        HtmlForm htmlfrm = new HtmlForm();
        gv_job_allocation.Parent.Controls.Add(htmlfrm);
        htmlfrm.Attributes["runat"] = "server";
        //gv_job_allocation.Controls.Remove((CheckBoxList)gv_job_allocation.HeaderRow.FindControl("CheckBox1"));
        //gv_job_allocation.HeaderRow.FindControl("CheckBox1").Visible = false;
        //gv_job_allocation.HeaderRow.Cells[3].Text = "CHECKBOX";
        htmlfrm.Controls.Add(gv_job_allocation);
        htmlfrm.RenderControl(txtwriter);
        Response.Write(strwriter);
        Response.End();

    }

    protected void btnMove_Click(object sender, EventArgs e)
     {
        string job_status = "1";
        string jobstatus_emp = "2";
        bool isGeneral = false;
        string strJobProcess = "";
        if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
        {

            if (gv_job_allocation.Rows.Count > 0)
            {
                foreach (GridViewRow itm in gv_job_allocation.Rows)
                {
                    CheckBox chx = (CheckBox)itm.Cells[7].FindControl("CheckBox_MoveTask");

                    if (chx != null && chx.Checked)
                    {
                        HiddenField hf_job_id = (HiddenField)itm.Cells[7].FindControl("hiddenMoveTask");
                        HiddenField hf_job_type_id = (HiddenField)itm.Cells[7].FindControl("hf_job_type_id");

                        if (hf_job_id != null && hf_job_type_id != null)
                        {
                            //onsql.strtenddate(hf.Value.ToString());
                            DSQL.MoveTask_department(ddl_department.SelectedValue.ToString(), ddl_employees.SelectedValue.ToString(), hf_job_type_id.Value.ToString(), hf_job_id.Value.ToString(), ddl_task.SelectedValue.ToString(), job_status);



                        }


                    }
                }
                // Page_Load(null,null);
                Alert("This Job was Successfully Moved");

            }
        }
        DataSet dsstrSQL = new DataSet();

        if (ddl_MoveSelection.Visible == true)
        {

            if (ddl_MoveSelection.SelectedItem.Value.ToString() == "2")
            {
                if (gv_job_allocation_Employee.Rows.Count > 0)
                {
                    int intSlNo = 0;
                    Boolean blnEmpSelection = false;
                    Boolean blnjobSelection = false;
                    
                    foreach (GridViewRow RwEmp in gv_job_allocation_Employee.Rows)
                    {
                        CheckBox chkEmp = (CheckBox)RwEmp.Cells[4].FindControl("CheckBox_MoveTask");
                        if (chkEmp.Checked)
                        {
                            blnjobSelection = true;

                        }

                    }

                    if (blnjobSelection == false)
                    {
                        Alert("Please select the job.");
                        return;
                    }

                    List<int> lstJobOrder = new List<int>();

                    foreach (GridViewRow RwTask in grdEmplyeeTask.Rows)
                    {
                        CheckBox chkEmp = (CheckBox)RwTask.Cells[4].FindControl("Chk_Emp");
                        if (chkEmp.Checked)
                        {
                            blnEmpSelection = true;

                            Label lblSlNo = (Label)RwTask.Cells[0].FindControl("lblSlNo");
                            intSlNo = Convert.ToInt32(lblSlNo.Text);

                            DropDownList drpTask = (DropDownList)RwTask.Cells[2].FindControl("ddl_EmpTask");
                            if (drpTask.SelectedValue.ToString() == "0")
                            {
                                Alert("Please select the task of Sl.No. " + intSlNo.ToString() + ".");
                                return;
                            }

                            TextBox txtOrder = (TextBox)RwTask.Cells[3].FindControl("txtOrder");
                            if (Convert.ToString(txtOrder.Text).Trim() == "")
                            {
                                Alert("Please enter the order of Sl.No. " + intSlNo.ToString() + ".");
                                return;
                            }
                            else
                            {
                                if (lstJobOrder.Count == 0)
                                {
                                    lstJobOrder.Add(Convert.ToInt32(txtOrder.Text));
                                }
                                else
                                {
                                    if (lstJobOrder.Contains(Convert.ToInt32(txtOrder.Text)))
                                    {
                                        Alert("Job order repeates of Sl.No. " + intSlNo.ToString() + ".");
                                        return;
                                    }
                                    lstJobOrder.Add(Convert.ToInt32(txtOrder.Text));
                                }
                            }
                        }

                    }

                    //if (lstJobOrder.Count > 0)
                    //{
                    //    int intExpectedVale = 0;
                    //    int intActualvalue = 0;
                    //    int intCount = 0;

                    //    lstJobOrder.Sort();
                    //    intCount = lstJobOrder.Count;

                    //    for (int i = 0; i < intCount; i++)
                    //    {
                    //        intExpectedVale = intExpectedVale + 1;
                    //        intActualvalue = lstJobOrder[i];
                    //        if (intExpectedVale != intActualvalue)
                    //        {
                    //            Alert("Missing the job order of " + intExpectedVale.ToString() + ".");
                    //            return;
                    //        }
                    //    }
                    //}

                    

                    if (blnEmpSelection == false)
                    {
                        Alert("Please select the employee.");
                        return;
                    }

                    foreach (GridViewRow itm in gv_job_allocation_Employee.Rows)
                    {
                        CheckBox chx = (CheckBox)itm.Cells[7].FindControl("CheckBox_MoveTask");

                        if (chx != null && chx.Checked)
                        {
                            HiddenField hf_job_id = (HiddenField)itm.Cells[7].FindControl("hiddenMoveTask");
                            HiddenField hf_job_type_id = (HiddenField)itm.Cells[7].FindControl("hf_job_type_id");
                            HiddenField hf_job_history_id = (HiddenField)itm.Cells[7].FindControl("hf_job_history_id");
                            Boolean blnEmpAllocated = false;

                            string strJobId = Convert.ToString(hf_job_id.Value).Trim();
                            string strJobTypeId = Convert.ToString(hf_job_type_id.Value).Trim();
                            string strJobHistoryId = Convert.ToString(hf_job_history_id.Value).Trim();
                            //DSQL.MoveTask_emp_process(strJobId, strJobTypeId, "15", "0", "0", "0", "0");

                            foreach (GridViewRow RwTask in grdEmplyeeTask.Rows)
                            {
                                CheckBox chkEmp = (CheckBox)RwTask.Cells[4].FindControl("Chk_Emp");
                                if (chkEmp.Checked)
                                {
                                    blnEmpAllocated = true;

                                    HiddenField hfEmpId = (HiddenField)RwTask.Cells[4].FindControl("hf_emp_id");
                                    DropDownList drpTask = (DropDownList)RwTask.Cells[2].FindControl("ddl_EmpTask");
                                    TextBox txtOrder = (TextBox)RwTask.Cells[3].FindControl("txtOrder");
                                                                        
                                    string strJobStatusID = Convert.ToString(jobstatus_emp).Trim();
                                    string strEmpID = Convert.ToString(hfEmpId.Value).Trim();
                                    string strTaskID = Convert.ToString(drpTask.SelectedValue).Trim();
                                    string strOrder = Convert.ToString(txtOrder.Text).Trim();

                                    if (Session["TabName"].ToString() == "LstAMO")
                                    {
                                        dsstrSQL = DSQL.GetJobHistoryStage(strJobId, strJobTypeId);
                                    }
                                    else if (Session["TabName"].ToString() == "LstAMOEP")
                                    {
                                        dsstrSQL = DSQL.GetJobHistoryStage(strJobId, strJobTypeId);
                                    }
                                    else
                                    {
                                        dsstrSQL = DSQL.GetJobStage(strJobId, strJobTypeId);
                                    }

                                    if (dsstrSQL.Tables.Count > 0)
                                        stage_id = dsstrSQL.Tables[0].Rows[0]["job_stage_id"].ToString(); //Columns["job_stage_id"]

                                    DSQL.MoveTask_emp_process(strJobId, strJobTypeId, strJobStatusID, strEmpID, strTaskID, strOrder, stage_id, strJobHistoryId);
                                }

                            }

                            if (blnEmpAllocated == true)
                            {
                                DSQL.MoveTask_emp_process(strJobId, strJobTypeId, "16", "0", "0", "0", "0","0");
                            }

                            strJobProcess = "0";
                            
                            

                            //if (hf_job_id != null && hf_job_type_id != null && stage_id != "81")
                            //{
                            //    DSQL.MoveTask_employee_process(ddl_employees.SelectedValue.ToString(), hf_job_type_id.Value.ToString(), hf_job_id.Value.ToString(), ddl_task.SelectedValue.ToString(), jobstatus_emp, strJobProcess);
                            //    DSQL.InsertLoggedEvents(hf_job_id.Value.ToString(), hf_job_history_id.Value.ToString(), hf_job_type_id.Value.ToString(), ddl_task.SelectedValue.ToString(), ddl_employees.SelectedValue.ToString());
                            //}
                            //else if (hf_job_id != null && hf_job_type_id != null && stage_id == "81")
                            //{
                            //    DSQL.MoveTask_employee(ddl_employees.SelectedValue.ToString(), hf_job_type_id.Value.ToString(), hf_job_id.Value.ToString(), ddl_task.SelectedValue.ToString(), jobstatus_emp, hf_job_history_id.Value.ToString());
                            //    DSQL.InsertLoggedEvents(hf_job_id.Value.ToString(), hf_job_history_id.Value.ToString(), hf_job_type_id.Value.ToString(), ddl_task.SelectedValue.ToString(), ddl_employees.SelectedValue.ToString());
                            //}

                        }
                    }
                    object o = new object();
                    EventArgs s = new EventArgs();

                    if (Session["TabName"].ToString() == "LstGeneral")
                        lnkGeneral_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstPreEditing")
                        lnkPE_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstCE")
                        lnkCE_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstEPD")
                        lnkEPD_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstQC")
                        lnkQC_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstPagination")
                        lnkPagination_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstDeft")
                        lnkDEFT_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstArtWork")
                        lnkArtWork_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstAMO")
                        lnkAMO_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstAMOEP")
                        lnkAMOEP_Click(o, s);
                    else if (Session["TabName"].ToString() == "LstCollation")
                        lnkCollation_Click(o, s);

                    Page_Load(null, null);
                    Alert("This Job was Successfully Moved");

                }
            }
        }
        else if (ddl_MoveSelection.Visible == false)
        {
            if (ddl_MoveSelection.SelectedItem.Value.ToString() == "2")
            {

                if (gv_job_allocation_Employee.Rows.Count > 0)
                {
                    foreach (GridViewRow itm in gv_job_allocation_Employee.Rows)
                    {
                        CheckBox chx = (CheckBox)itm.Cells[7].FindControl("CheckBox_MoveTask");

                        if (chx != null && chx.Checked)
                        {
                            HiddenField hf_job_id = (HiddenField)itm.Cells[7].FindControl("hiddenMoveTask");
                            HiddenField hf_job_type_id = (HiddenField)itm.Cells[7].FindControl("hf_job_type_id");
                            // HiddenField hf_job_history_id = (HiddenField)itm.Cells[7].FindControl("hf_job_history_id");

                            if (hf_job_id != null && hf_job_type_id != null)
                            {
                                //onsql.strtenddate(hf.Value.ToString());

                                DSQL.MoveTask_employee_General(ddl_department.SelectedValue.ToString(), hf_job_type_id.Value.ToString(), hf_job_id.Value.ToString(), job_status);

                            }

                        }
                    }
                }
                //        //Page_Load(null, null);
                //        Alert("This Job was Successfully Moved");

            }

        }
        //Alert("This Job was Successfully Moved!);
    }

    private void Alert(string msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + msg + "\");</script>");
    }

    protected void gv_job_allocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (!isTeamLead)
        {
            CheckBox chkbox = (CheckBox)e.Row.FindControl("CheckBox_MoveTask");
            if (chkbox != null)
                chkbox.Enabled = false;
        }


    }
    protected void ddl_MoveSelection_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
        {
            ddl_department.Visible = true;
            lbl_department.Visible = true;

            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            Panel1.Visible = true;
            Panel2.Visible = false;
            pnlAllocatedEmp .Visible =false;

            GetJobAllocationList("", "", "", "");
            this.showpanel(LstGeneral);
            gv_job_allocation.Visible = true;
            gv_job_allocation_Employee.Visible = false;

            //**-**Begin Added By Subash.T on 25-08-2014**-**
           // gv_job_allocation_Employee.Columns[10].Visible = false;
            //**-**End Added By Subash.T on 25-08-2014**-**
        }
        else if (ddl_MoveSelection.SelectedItem.Value.ToString() == "2" && ddl_employees.Visible == true)
        {
            ddl_department.Visible = false;
            lbl_department.Visible = false;

            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            Panel1.Visible = false;
            Panel2.Visible = true;
            pnlAllocatedEmp.Visible = true;

            GetJobAllocationList("", "", "", "");
            this.showpanel(LstGeneral);
            gv_job_allocation.Visible = false;
            gv_job_allocation_Employee.Visible = true;

            //**-**Begin Added By Subash.T on 25-08-2014**-**
           // gv_job_allocation_Employee.Columns[10].Visible = false;
            //**-**End Added By Subash.T on 25-08-2014**-**
        }

        else if (ddl_MoveSelection.SelectedItem.Value.ToString() == "2" && ddl_employees.Visible == false)
        {
            ddl_department.Visible = false;
            lbl_department.Visible = false;

            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            Panel1.Visible = false;
            Panel2.Visible = true;

            GetJobAllocationList("", "", "", "");
            this.showpanel(LstGeneral);
            gv_job_allocation.Visible = false;
            gv_job_allocation_Employee.Visible = true;

            //**-**Begin Added By Subash.T on 25-08-2014**-**
           // gv_job_allocation_Employee.Columns[10].Visible = false;
            //**-**End Added By Subash.T on 25-08-2014**-**
        }

    }
    protected void gv_job_allocation_employee_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (!isTeamLead)
        {
            CheckBox chkbox = (CheckBox)e.Row.FindControl("CheckBox_MoveTask");
            if (chkbox != null)
                chkbox.Enabled = false;
        }


        //DataSet dt = null;
        //if (Session["Jobs_Employee"] != null)


        DataTable dt = new DataTable();
        dt = (DataTable)Session["Jobs_Employee"];
        DataTable dt1 = new DataTable();

        DataSet dst = new DataSet();
        dt1 = dt.Copy();
        dst.Tables.Add(dt1);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (i < dst.Tables[0].Rows.Count)
            {
                if (dst.Tables[0].Rows[i]["job_status_id"].ToString() == "5")
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "pink");
                }
                i++;
            }


        }

        //TextBox txtProcess = (TextBox)e.Row.Cells[10].FindControl("txtProcess");
        //if (txtProcess != null)
        //{
        //    int intNoofProcess = 0;

        //    if (Convert.ToString(txtProcess.Text) != "")
        //    {
        //        intNoofProcess = Convert.ToInt32(txtProcess.Text);
        //    }

        //    if (intNoofProcess > 0)
        //    {
        //        txtProcess.Enabled = false;
        //    }
        //    else
        //    {
        //        txtProcess.Enabled = true;
        //    }
        //}

    }

    protected void visibility()
    {
        ddl_MoveSelection.Visible = true;
        lbl_selection.Visible = true;

        if (ddl_MoveSelection.SelectedItem.Value.ToString() == "1")
        {
            ddl_department.Visible = true;
            lbl_department.Visible = true;

            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            Panel1.Visible = true;
            Panel2.Visible = false;

            gv_job_allocation.Visible = true;
            gv_job_allocation_Employee.Visible = false;


        }
        else
        {
            ddl_department.Visible = false;
            lbl_department.Visible = false;
            ddl_employees.Visible = false;
            lbl_employees.Visible = false;

            ddl_task.Visible = false;
            lbl_task.Visible = false;

            gv_job_allocation.Visible = false;
            gv_job_allocation_Employee.Visible = true;
        }
    }

    protected void gv_general_Sorting_employee(object sender, GridViewSortEventArgs e)
    {
        {
            string sortingDirection = string.Empty;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                sortingDirection = "Asc";
            }
            DataSet dst_sort_emp = new DataSet();

            //    //dst = DSQL.GetJobAllocation();
            //    DataTable datatable = gv_job_allocation.DataSource as DataTable;
            DataTable datatable = gv_job_allocation_Employee.DataSource as DataTable;
            dst_sort_emp = DSQL.GetJobAllocation(dept_id, emp_team_id, job_type_id, emp_id);
            datatable = dst_sort_emp.Tables[0];
            if (datatable != null)
            {
                DataView sortedView = new DataView(datatable);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                gv_job_allocation_Employee.DataSource = sortedView;
                gv_job_allocation_Employee.DataBind();
            }
        }
    }
    protected void gv_job_allocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        //int rowindex = rowSelect.RowIndex;   
    }
    protected void gv_job_allocation_employee_RowDataCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdEmplyeeTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intDeptId=0;
            drpTask = (DropDownList)e.Row.Cells[2].FindControl("ddl_EmpTask");
            if (Session["TabName"].ToString() == "LstPreEditing")
                intDeptId = 89;
            else if (Session["TabName"].ToString() == "LstCE")
               intDeptId=89;
            else if (Session["TabName"].ToString() == "LstEPD")
               intDeptId=83;
            else if (Session["TabName"].ToString() == "LstQC")
               intDeptId=85;
            else if (Session["TabName"].ToString() == "LstPagination")
               intDeptId=81;
            else if (Session["TabName"].ToString() == "LstDeft")
               intDeptId=82;
            else if (Session["TabName"].ToString() == "LstArtWork")
               intDeptId=84;
            else if (Session["TabName"].ToString() == "LstAMO")
               intDeptId=82;
            else if (Session["TabName"].ToString() == "LstAMOEP")
               intDeptId=83;
            else if (Session["TabName"].ToString() == "LstCollation")
               intDeptId=91;

               //AllotedTaskList(drpTask,intDeptId.ToString());
            if (drpTask != null)
            {
                DataSet DSET = new DataSet();
                DSET = DSQL.GetJobItemTask(intDeptId.ToString());
                drpTask.DataTextField = "task_name";
                drpTask.DataValueField = "task_id";
                drpTask.DataSource = DSET;
                drpTask.DataBind();
                drpTask.Items.Insert(0, new ListItem("--select--", "0"));

            }

            if (!isTeamLead)
            {
                CheckBox chkbox = (CheckBox)e.Row.FindControl("Chk_Emp");
                if (chkbox != null)
                    chkbox.Enabled = false;

                if (drpTask != null)
                    drpTask.Enabled = false;

                TextBox txtOrder = (TextBox)e.Row.FindControl("txtOrder");
                if (txtOrder != null)
                    txtOrder.Enabled = false;

            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void getEmployee()
    {
        try
        {
            DataSet dsEmp = new DataSet();
            if (Convert.ToString(Session["TabName"]) != "")
            {

                int intDeptId = 0;
                if (Session["TabName"].ToString() == "LstPreEditing")
                    intDeptId = 89;
                else if (Session["TabName"].ToString() == "LstCE")
                    intDeptId = 89;
                else if (Session["TabName"].ToString() == "LstEPD")
                    intDeptId = 83;
                else if (Session["TabName"].ToString() == "LstQC")
                    intDeptId = 85;
                else if (Session["TabName"].ToString() == "LstPagination")
                    intDeptId = 81;
                else if (Session["TabName"].ToString() == "LstDeft")
                    intDeptId = 82;
                else if (Session["TabName"].ToString() == "LstArtWork")
                    intDeptId = 84;
                else if (Session["TabName"].ToString() == "LstAMO")
                    intDeptId = 82;
                else if (Session["TabName"].ToString() == "LstAMOEP")
                    intDeptId = 83;
                else if (Session["TabName"].ToString() == "LstCollation")
                    intDeptId = 91;

                //AllotedTaskList(drpTask, intDeptId.ToString());
                dsEmp = DSQL.GetAllocatedEmployee("0", intDeptId.ToString(), "0", "0");
                grdEmplyeeTask.DataSource = dsEmp;
                grdEmplyeeTask.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw Ex; ;
        }



    }

    private void PanelAllign(int intType)
    {
        //if (intType == 0)
        //{
        //    pnlAllocatedEmp.Visible = false;
        //    Panel2.Width = 1170;
        //}
        //else
        //{
        //    pnlAllocatedEmp.Visible = true;
        //    Panel2.Width = 770;
        //}

        if (Panel1.Visible == true)
        {
            pnlAllocatedEmp.Visible = false;
        }
        else if (Panel2.Visible == true)
        {
            pnlAllocatedEmp.Visible = true;
        }

    }
}

public class Rate
{
    public string to { get; set; }
    public string from { get; set; }
    public double rate { get; set; }
}