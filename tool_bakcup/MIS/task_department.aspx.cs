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
/*
/// <summary>
/// Created by: Royson
/// Creation Date: Monday, August 23, 2010
/// </summary>
 * */
public partial class task_department : System.Web.UI.Page
{
    protected int id = 1;
    private Common oCom = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen()
    {
        DataSet dsDept = oCom.getDepartment();
        drpDepartment.DataSource = dsDept.Tables[0];
        drpDepartment.DataTextField = dsDept.Tables[0].Columns["department_name"].ToString();
        drpDepartment.DataValueField = dsDept.Tables[0].Columns["department_id"].ToString();
        drpDepartment.DataBind();
        drpDepartment.Items.Insert(0, new ListItem("Heads", "-1"));
        drpDepartment.Items.Insert(0, new ListItem("Default", "0"));
        drpDepartment.Items.Insert(0, new ListItem(" -- Select --", ""));        
    }
    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDepartment.SelectedItem.Value != "")
        {
            DataSet dsTask = oCom.getDepartmentTask(drpDepartment.SelectedItem.Value.Trim());
            gvDeptTast.DataSource = dsTask.Tables[0];
            
        }
        gvDeptTast.DataBind();
    }
    protected void gvDeptTast_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow){
            if (((CheckBox)e.Row.FindControl("chkgvAssign")).Checked)
                e.Row.Style.Add("background-color", "#f0fff0");
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string sTaskIDs = "";
        try
        {
            if (drpDepartment.SelectedItem.Value != ""){
                foreach (GridViewRow row in gvDeptTast.Rows){
                    if (((CheckBox)row.FindControl("chkgvAssign")).Checked)
                        sTaskIDs += ((HiddenField)row.FindControl("hfgvTaskID")).Value + ",";
                }
                sTaskIDs = sTaskIDs.TrimEnd(',');
                string status = oCom.UpdateDepartmentTask(drpDepartment.SelectedItem.Value, sTaskIDs);
                if (status != "true")
                {
                    Alert("Error saving records!");
                }
                else
                {
                    this.drpDepartment_SelectedIndexChanged(null, null);
                    Alert("Successfully Saved");
                }
            }
        }
        catch (Exception ex)
        {
            Alert(ex.Message);
        }
    }
    public void Alert(string sMessage)
    {
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        sMessage = sMessage.Replace("'", "\'");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
}
