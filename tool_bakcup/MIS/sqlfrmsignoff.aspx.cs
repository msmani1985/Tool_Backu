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

public partial class sqlfrmsignoff : System.Web.UI.Page
{
    Boolean signoffvisible = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        Update_Btn.Enabled = true;
        btnexit.Disabled = false;
        if (!Page.IsPostBack)
        {
            signoff_btn.Enabled = false;
            colorrowindex.Value = "";
            DataSet oDS = new DataSet();
            SDateTxt.Text = DateTime.Now.ToString("MM/dd/yyyy");
            EDateTxt.Text = DateTime.Now.ToString("MM/dd/yyyy");
            datasourceSQL oSQL = new datasourceSQL();
            try
            {
                //if (Session["SQLTeamDS"] == null)
                {
                    //oDS = oSQL.ExcuteSelectProcedure("spGet_TeamMembers", "1607", "@Team_owner_id", "Int", "Input", CommandType.StoredProcedure);
                    oDS = oSQL.ExcProcedure("spGet_TeamMembers", new string[,] { { "@Team_owner_id", Session["employeeid"].ToString() } }, CommandType.StoredProcedure);
                    Employeeddlist.DataSource = oDS;
                    Employeeddlist.DataBind();

                    ////Session["SQLTeamDS"] = oDS;
                    //if (oDS != null)
                    //{
                    //    Employeeddlist.DataSource = oDS;
                    //    Employeeddlist.DataBind();
                    //}
                    //oSQL = null;
                }
                //else if (Session["SQLTeamDS"] != null)
                //{
                //    oDS = (DataSet)(Session["SQLTeamDS"]);
                //    if (oDS != null)
                //    {
                //        Employeeddlist.DataSource = oDS;
                //        Employeeddlist.DataBind();
                //    }
                //}

            }
            catch (Exception ex)
            {
                DivError.InnerHtml = ex.Message;
            }
            finally
            {
                oDS = null;
                oSQL = null;
            }
        }
    }
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        datasourceSQL sqlobj = new datasourceSQL();
        DataSet tasksql=new DataSet();
        signoffvisible = false;
        try
        {
            tasksql = sqlobj.ExcProcedure(" ", new string[,] {{"@employee_id",Employeeddlist.SelectedValue.ToString()},
            {"@startdate",SDateTxt.Text + " 00:00:00"},{"@enddate",EDateTxt.Text + " 23:59:59"} }, CommandType.StoredProcedure);
            //if (tasksql.Tables[0].Rows.Count>0)
            if (tasksql != null)
            {
                DivEmp.Visible = true;
                DivError.Visible = false;
                GVEmployeeTask.DataSource = tasksql;
                GVEmployeeTask.DataBind();
            }
            else
            {
                DivError.InnerText = "No Records Found";
                DivError.Visible = true;
                DivEmp.Visible = false;
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally { tasksql = null; sqlobj = null; }
        if (signoffvisible == true)
            signoff_btn.Enabled = true;
        else
            signoff_btn.Enabled = false;

    }
    protected void GVEmployeeTask_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        //Set Background Color
        GridViewRow gvrow = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        colorrowindex.Value = gvrow.RowIndex.ToString();
        if (colorrowindex.Value == "")
        {
            gvrow.BackColor = System.Drawing.Color.LightPink;
            colorrowindex.Value = gvrow.RowIndex.ToString();
        }
        else
        {
            GridViewRow gr = GVEmployeeTask.Rows[Convert.ToInt32(colorrowindex.Value)];
            gr.BackColor = System.Drawing.Color.Pink;
        }
        if (e.CommandName == "Edit1")
        {
            USDateTxt.Text=gvrow.Cells[3].Text;
            UEDateTxt.Text = gvrow.Cells[4].Text;
            loggedid.Value = e.CommandArgument.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Show", "showhiddiv('employeetimesheet','UpdateDiv');", true);
        }
        if (e.CommandName == "Split")
        {
            USDateTxt.Text = gvrow.Cells[3].Text;
            UEDateTxt.Text = gvrow.Cells[4].Text;
            Update_Btn.Enabled = false;
            btnexit.Disabled = true;
            loggedid.Value = e.CommandArgument.ToString();
            ISDateTxt.Text = gvrow.Cells[3].Text;
            IEDateTxt.Text = gvrow.Cells[4].Text;
            TxtPages.Text = (gvrow.Cells[6].Text != "" && gvrow.Cells[6].Text != "&nbsp;")?gvrow.Cells[6].Text:"0";
            Txtcmnts.Text = gvrow.Cells[7].Text;
            JobIdHField.Value = e.CommandArgument.ToString();
            

            datasourceSQL sobj = new datasourceSQL();
            DataSet sds = new DataSet();
            try
            {
                string[,] TI_param ={ { "@employee_id", Session["employeeid"].ToString() }, { "@dept_id", Session["departmentid"].ToString() } };
                sds = sobj.GetTaskItem(TI_param);
                ddlstyles.DataSource = sds;
                ddlstyles.DataBind();
                ddltask_type.DataSource = sds;
                ddltask_type.DataBind();
            }
            catch (Exception ex) { throw ex; }
            finally { sobj = null; sds = null; }
            
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Show", "showsplit();", true);
        }
        //For Delete
        if (e.CommandName == "cmd_Delete")
        {
            Delete_record(Convert.ToInt32(e.CommandArgument));
            colorrowindex.Value = "";
        }
        Submit_Btn_Click(sender, e);
    }
    private void Delete_record(int logevent_id)
    {
        datasourceSQL delete_obj = new datasourceSQL();
        string delete_msg = "";
        try
        {
            delete_obj.ExcSProcedure("spDelete_loggedevents", new string[,] { { "@loggedevent_id", logevent_id.ToString() } }, CommandType.StoredProcedure);
            delete_msg = "Your record deleted successfully";
        }
        catch (Exception ex)
        { delete_msg=ex.Message.ToString().Replace("'",""); }
        finally
        { delete_obj = null; messagebox(delete_msg); }
    }
    protected void GVEmployeeTask_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (colorrowindex.Value != "")
            {
                GridViewRow gr = GVEmployeeTask.Rows[Convert.ToInt32(colorrowindex.Value)];
                gr.BackColor = System.Drawing.Color.Pink;
            }
        }
        catch (Exception ex) { }

    }
    protected void GvEmployeeTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField AEid = (HiddenField)e.Row.FindControl("HFAppEmpid");
            if (e.Row.RowType == DataControlRowType.DataRow && AEid != null && AEid.Value != "" && AEid.Value != "&nbsp;")
            {
                ((ImageButton)e.Row.Cells[8].FindControl("btnEdit")).Visible = false;
                ((ImageButton)e.Row.Cells[9].FindControl("btnSplit")).Visible = false;
                ((ImageButton)e.Row.Cells[10].FindControl("Btn_Delete")).Visible = false;

            }
            else if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
                signoffvisible = true;
        }
        catch (Exception ex)
        { DivError.InnerText = ex.ToString(); DivError.Visible = true; }
        finally
        { }
    }
    protected void Update_Btn_Click(object sender, EventArgs e)
    {
        DateTime outdate3, outdate4;
        if (!(DateTime.TryParse(USDateTxt.Text, out outdate3)) || !(DateTime.TryParse(UEDateTxt.Text, out outdate4)) )
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "javascript", "javascript:validate('update');", true);
            return;
        }
        datasourceSQL uobj = new datasourceSQL();
        int output=0;
        string msg="";
        try
        {
            output = uobj.ExcSProcedure("spUpdate_Loggedevents", new string[,] { { "@loggevent", loggedid.Value }, 
            { "@starttime", USDateTxt.Text }, { "@endtime", UEDateTxt.Text }, 
            { "@update","output" } }, CommandType.StoredProcedure);
            msg = "Updated Successfully";

        }
        catch (Exception ex)
        { //msg = ex.Message.ToString(); msg = msg.Replace("'", " "); 
            msg = "Updated Fail";
        }
        finally 
        {
            uobj = null;
            messagebox(msg);
        }
        //Comment for test 27 May 2010
        if (output == 1)
            Submit_Btn_Click(sender, e);

    }
    protected void InsertBtn_Click(object sender, EventArgs e)
    {
        if (txtJobNumber.Text == "" && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "4" && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "5")// Equal to 4,5 is Time Sheet (break,absent,Meeting) so it may be no jobno
        {
            messagebox("Job Number is empty");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Show", "showsplit();", true);
            return;
        }

        DateTime outdate1, outdate2, outdate3, outdate4;
        if (!(DateTime.TryParse(USDateTxt.Text,out outdate3))|| !(DateTime.TryParse(UEDateTxt.Text,out outdate4)) || !(DateTime.TryParse(ISDateTxt.Text, out outdate1)) || !(DateTime.TryParse(IEDateTxt.Text, out outdate2)))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "javascript", "javascript:validate('insert');", true);
            return;
        }
  
        datasourceSQL taskobj = new datasourceSQL();
        DataSet taskds = new DataSet();
        string msg="";
        string[,] lparam;
        try
        {
            lparam = new string[,] { { "@job_id", txtJobNumber.Text }, { "@job_type", ddljobtype.SelectedValue.ToString() } };
            taskds = taskobj.ExcProcedure("spGet_JobNo_Details", lparam, CommandType.StoredProcedure);
            DivError.InnerHtml = "";
            lparam = null;

            if (taskds == null && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "4" && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "5")
            {
                msg = "Job Number is not valid";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Show", "showsplit();", true);
                return;
            }
            taskobj.ExcSProcedure("spInsert_LoggedEvents", new string[,]{{"@task_id",ddlstyles.SelectedValue},
            {"@job_id",txtJobNumber.Text.Trim()},{"@job_type_id",(ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "4" && ddltask_type.Items[ddlstyles.SelectedIndex].Value.ToString() != "5") ? ddljobtype.SelectedValue.ToString() : ""},
            {"@job_history_id",(taskds!=null && taskds.Tables[0].Rows.Count>0)?taskds.Tables[0].Rows[0]["job_history_id"].ToString():"" },{"@emp_id",""},{"@type","2"},
            {"@logid",JobIdHField.Value},{"@stime",ISDateTxt.Text},{"@etime",IEDateTxt.Text},
            {"@comments",Txtcmnts.Text},{"@pages",TxtPages.Text}}, CommandType.StoredProcedure);
            msg = "Inserted Successfully";
        }
        catch (Exception ex)
        { //throw ex;
            msg = "Insert Fail";
        }
        finally 
        { 
            taskds = null; taskobj = null;
            messagebox(msg);
            
        }
        Update_Btn_Click(sender, e);
        //Submit_Btn_Click(sender, e);
    }
    protected void signoff_btn_Click(object sender, EventArgs e)
    {
        datasourceSQL sobj = new datasourceSQL();
        string msg = "";
        try
        {
            //Session["employeeid"] = null;
            if (Session["employeeid"] == null)
                throw new ArgumentException("Session Expired, Please relogin");

            sobj.ExcSProcedure("spUpdate_Signoff", new string[,] { { "@app_emp_id", Session["employeeid"].ToString() }, { "@employee_id", Employeeddlist.SelectedValue.ToString() }, { "@startdate", SDateTxt.Text + " 00:00:00" }, { "@enddate", EDateTxt.Text + " 23:59:59" } }, CommandType.StoredProcedure);
            msg = "Updated Successfully";
        }
        catch (ArgumentException aex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + aex.Message.ToString() + "');window.open('login.aspx','_top');</script>");
            //msg = aex.Message.ToString();
        }
        catch (Exception ex)
        { //msg = ex.Message.Replace("'", "\\'"); 
            msg = "Updated Fail";
        }
        finally
        {
            sobj = null;
            messagebox(msg);
        }
        Submit_Btn_Click(sender, e);
    }
    private void messagebox(string display_msg)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + display_msg + "');</script>");
    }
}
