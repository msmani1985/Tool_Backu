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

public partial class JobAllocation_Preview : System.Web.UI.Page
{
    datasourceSQL DSQL = new datasourceSQL();
    static string jobprocess_selectvalue = "";
    string dept_id = "";
    string emp_team_id = "";//85
    string job_type_id = "";
    string job_id = "";
    string emp_id = "";//1834
    public Int32 iRowId = 1;
    static string WORK_STATUS_selectvalue = "";

    protected void Page_Load(object sender, EventArgs e)
      {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            emp_id = Session["employeeid"].ToString();
            emp_team_id = Session["employeeteamid"].ToString();

            if (!Page.IsPostBack)
            {

                this.showpanel(LstMyJobsList);
                GetAllocatedJobsList_Employee();
                datasourceSQL oSQL = new datasourceSQL();
                DataSet oDs = new DataSet();
                string[,] oParams = new string[,]
            {
               
                {"@employeeid",emp_id},
                {"@employeeteamid",emp_team_id}
            };

                //showpanel(LstMyJobsList);
                //lnkMyJobList_Click(sender, e);
                //GetAllocatedJobsList_Employee();
            }
        }
        //if (Session["employeeid"])
        //{
        //    throw new Exception("Session Expired!");
        //}


        //emp_team_id = Session["employeeteamid"].ToString();
        //emp_id = Convert.ToString(temp);
        //emp_id = Session["employeeid"].ToString();

        //



    }


    private void showpanel(HtmlGenericControl shwpanel)
    {
        switch (shwpanel.ID)
        {
            case "LstMyJobsList":
                LstMyJobsList.Attributes.Add("class", "current");
                LstMyTeamJobsList.Attributes.Add("class", "");
                break;

            case "LstMyTeamJobsList":
                LstMyJobsList.Attributes.Add("class", "");
                LstMyTeamJobsList.Attributes.Add("class", "current");
                break;
        }
    }



    protected void lnkMyJobList_Click(object sender, EventArgs e)
    {
        showpanel(LstMyJobsList);
        GetAllocatedJobsList_Employee();
    }
    protected void lnkMyTeamJobList_Click(object sender, EventArgs e)
    {
        showpanel(LstMyTeamJobsList);
        GetAllocatedJobsList_Team();
    }

    private void GetAllocatedJobsList_Employee()
    {
        // emp_id = empid;
        DataSet dst = new DataSet();
        //dst = DSQL.AllocatedJobs_Employee(emp_id);
        dst = DSQL.AllocatedJobs_Employee(emp_id);
        gv_job_allocation.DataSource = null;
        if (dst != null && dst.Tables[0].Rows.Count > 0)
        {
            gv_job_allocation.DataSource = dst.Tables[0];
            gv_job_allocation.Columns[7].Visible = false;
        }
        Session["WORKSTATUS"] = dst;
        gv_job_allocation.DataBind();
        gv_job_allocation.Visible = true;

    }

    private void GetAllocatedJobsList_Team()
    {

        DataSet dst = new DataSet();
        //dst = DSQL.AllocatedJobs_Team(emp_team_id);
        dst = DSQL.AllocatedJobs_Team(emp_team_id);
        gv_job_allocation.DataSource = null;
        if (dst != null && dst.Tables[0].Rows.Count > 0)
        {
            gv_job_allocation.DataSource = dst.Tables[0];
            gv_job_allocation.Columns[7].Visible = true;
        }
        Session["WORKSTATUS"] = dst;
        gv_job_allocation.DataBind();
        gv_job_allocation.Visible = true;
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
            DataSet dst = new DataSet();

            //    //dst = DSQL.GetJobAllocation();
            //    DataTable datatable = gv_job_allocation.DataSource as DataTable;
            DataTable datatable = gv_job_allocation.DataSource as DataTable;
       
            LinkButton lbtn1 =(LinkButton) FindControl("LnkMyJobsLst");
            LinkButton lbtn2 = (LinkButton)FindControl("LnkMyTeamJobsLst");
            
            //dst = DSQL.AllocatedJobs_Team(emp_id, emp_team_id);
            
            datatable = dst.Tables[0];
            if (datatable != null)
            {
                DataView sortedView = new DataView(datatable);
                sortedView.Sort = e.SortExpression + " " + sortingDirection;
                gv_job_allocation.DataSource = sortedView;
                gv_job_allocation.DataBind();
            }
        }
    }
    protected void gv_joballocation_preview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList dd_list = (DropDownList)e.Row.FindControl("dd_jobstage");
            DataSet dd_ds = null;
            if (Session["WORKSTATUS"] != null)
                dd_ds = (DataSet)Session["WORKSTATUS"];

            DataView oV = null;
            if (dd_list != null)
            {

                dd_list.DataTextField = "work_status";
                dd_list.DataValueField = "work_status";

                oV = new DataView(dd_ds.Tables[0]);
                DataTable jstable = oV.ToTable(true, "work_status");
                DataRow selerow = jstable.NewRow();
                selerow["work_status"] = "All Status";

                jstable.Rows.InsertAt(selerow, 0);
                
                dd_list.DataSource = jstable;
                dd_list.DataBind();
                //dd_list.SelectedValue = dd_list.SelectedItem.Value;
                //WORK_STATUS_selectvalue = dd_list.SelectedItem.Value;
                if (WORK_STATUS_selectvalue != "")
                    dd_list.SelectedValue = WORK_STATUS_selectvalue;

            }
          }
    }
    protected void job_process_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Dset = new DataSet();
        DataView dv = new DataView();
        Dset = (DataSet)Session["WORKSTATUS"];
        dv = Dset.Tables[0].DefaultView;
        if (((DropDownList)sender).SelectedIndex != 0)
            dv.RowFilter = "[work_status]='" + ((DropDownList)sender).SelectedItem.Text.ToString() + "'";
        else
            dv.RowFilter = "";
        DropDownList drpdn = (DropDownList)sender;
        WORK_STATUS_selectvalue = ((DropDownList)sender).SelectedItem.Text.ToString();
        gv_job_allocation.DataSource = dv;
        gv_job_allocation.DataBind();

    }


}

