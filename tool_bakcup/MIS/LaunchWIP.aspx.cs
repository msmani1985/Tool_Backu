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

public partial class LaunchWIP : System.Web.UI.Page
{
    LaunchSQL oLaunch = new LaunchSQL();
    Launch la = new Launch();
    datasourceSQL sql = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
   
    protected void rdWIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ov = new DataSet();
        ov = la.LaunchWIP(rdWIP.SelectedValue, DDMonthList.SelectedValue, DDYearList.SelectedValue);
        if (ov.Tables[0].Rows.Count > 0)
        {
            GvProject.DataSource = ov;
            GvProject.DataBind();
        }
        else
        {
            GvProject.DataSource = null;
            GvProject.DataBind();
        }
        
    }
   
    protected void GvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowState == DataControlRowState.Edit || (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)))
        //{
        //    ImageButton i1 = (ImageButton)e.Row.FindControl("imgBD_Deldate");
        //    TextBox t1 = (TextBox)e.Row.FindControl("lblDelDate");
        //    i1.Attributes.Add("OnClick", "return f1(" + t1.ClientID + ")");
        //} 

        //else 
         if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label pro_id = e.Row.FindControl("lblid") as Label;
            DataSet ds = new DataSet();
            ds = la.GetStageWIP(Convert.ToInt16(pro_id.Text));
            DropDownList status = e.Row.FindControl("DropStatus") as DropDownList;
            DropDownList Stage = e.Row.FindControl("DropStage") as DropDownList;
            TextBox DelDate = e.Row.FindControl("DelDate") as TextBox;

            DataSet dstask = la.GetTask();
            Stage.DataSource = dstask;
            Stage.DataTextField = dstask.Tables[0].Columns[1].ToString();
            Stage.DataValueField = dstask.Tables[0].Columns[0].ToString();
            Stage.DataBind();
            Stage.Items.Insert(0, new ListItem("-- Select --", ""));
            
                
            status.Items.Add(new ListItem("--Select--", "0"));
            status.Items.Add(new ListItem("P", "P"));
            status.Items.Add(new ListItem("C", "C"));
            status.Items.Add(new ListItem("WIP", "WIP"));
            status.Items.Add(new ListItem("Del", "Del"));
            status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[0].Rows[0]["Delivery"].ToString()))].Selected = true;
            if (ds.Tables[1].Rows.Count > 0)
            {
                Stage.Items[Stage.Items.IndexOf(Stage.Items.FindByValue(ds.Tables[1].Rows[0]["Stage"].ToString()))].Selected = true;
                status.Items[status.Items.IndexOf(status.Items.FindByValue(ds.Tables[1].Rows[0]["Status"].ToString()))].Selected = true;
                DelDate.Text = ds.Tables[1].Rows[0]["Delivery_date"].ToString();
            }
            
        }
    }
    protected void GvProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Save")
        {
            string Pro_id = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            DropDownList Status = (DropDownList)row.Cells[18].FindControl("DropStatus");
            DropDownList Stage = (DropDownList)row.Cells[5].FindControl("DropStage");
            //Label Pro_id = (Label)row.FindControl("lblid");
            TextBox DelDate = (TextBox)row.Cells[16].FindControl("DelDate");
            TextBox DelTime = (TextBox)row.Cells[17].FindControl("DelTime");
            la.insertStageWIP(Pro_id, Stage.SelectedValue, Status.SelectedValue, DelDate.Text, DelTime.Text);
            rdWIP_SelectedIndexChanged(sender, e);
        }
    }
    
    protected void DDMonthList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ov = new DataSet();
        ov = la.LaunchWIP(rdWIP.SelectedValue, DDMonthList.SelectedValue, DDYearList.SelectedValue);
        if (ov.Tables[0].Rows.Count > 0)
        {
            GvProject.DataSource = ov;
            GvProject.DataBind();
        }
    }
    protected void DDYearList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ov = new DataSet();
        ov = la.LaunchWIP(rdWIP.SelectedValue, DDMonthList.SelectedValue, DDYearList.SelectedValue);
        if (ov.Tables[0].Rows.Count > 0)
        {
            GvProject.DataSource = ov;
            GvProject.DataBind();
        }
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        DataSet ov = new DataSet();
        ov = la.LaunchWIP(rdWIP.SelectedValue, DDMonthList.SelectedValue, DDYearList.SelectedValue);
        if (ov.Tables[0].Rows.Count > 0)
        {
            GvProject.DataSource = ov;
            GvProject.DataBind();
        }
    }
}
