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

public partial class task_jobtype : System.Web.UI.Page
{
    protected int id = 1;
    private Common oCom = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void dd_jobtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dd_jobtype.SelectedValue.ToString() != "0")
        {
            gv_jobtypetask.Visible = true;
            DataSet dsTask = oCom.getJobTypeTask(dd_jobtype.SelectedItem.Value.Trim());
            gv_jobtypetask.DataSource = dsTask.Tables[0];
            gv_jobtypetask.DataBind();
        }
        else
            gv_jobtypetask.Visible = false;
    }

    protected void gv_jobtypetask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((CheckBox)e.Row.FindControl("chk_assign")).Checked)
                e.Row.Style.Add("background-color", "#f0fff0");
        }
    }
    protected void img_save_Click(object sender, EventArgs e)
    {
        string sTaskIDs = "";
        try
        {
            if (dd_jobtype.SelectedItem.Value != "")
            {
                foreach (GridViewRow row in gv_jobtypetask.Rows)
                {
                    if (((CheckBox)row.FindControl("chk_assign")).Checked)
                        sTaskIDs += ((HiddenField)row.FindControl("hf_taskid")).Value + ",";
                }
                sTaskIDs = sTaskIDs.TrimEnd(',');
                string status = oCom.UpdateJobTypeTask(dd_jobtype.SelectedItem.Value, sTaskIDs);
                if (status != "true")
                {
                    Alert("Error saving records!");
                }
                else
                {
                    this.dd_jobtype_SelectedIndexChanged(null, null);
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
