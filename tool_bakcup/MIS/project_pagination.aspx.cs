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


public partial class project_pagination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_childproject.Visible = false;
            biz_IB pobj = new biz_IB();
            DataSet pds = new DataSet();
            try
            {
                pds = pobj.GetDespatchedJobs1(10125, 3);//10125 for Global Language, 3 For Projects
                dd_parentproject.DataSource = pds;
                dd_parentproject.DataBind();
                dd_parentproject.Items.Insert(0, new ListItem("-- Select Project --","0"));
                Cache["p_project"] = pds;
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { pobj = null; pds = null; }
        }
    }
    protected void dd_parentproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet childproject = new DataSet();
        DataView dv_child = new DataView();
        try
        {
            childproject = (DataSet)Cache["p_project"];
            dv_child = childproject.Tables[0].DefaultView;
            //DropDownList ddproject = sender as DropDownList;
            dv_child.RowFilter = "projectno<>" + dd_parentproject.SelectedItem.Value.ToString();
            gv_childproject.DataSource = dv_child;
            gv_childproject.DataBind();
            div_childproject.Visible = true;
         }
        catch (Exception ex)
        { throw ex; }
        finally
        { childproject = null; }

    }
    protected void img_assign_click(object sender, EventArgs e)
    {
        string child_project=string.Empty;
        foreach (GridViewRow gr in gv_childproject.Rows)
        {
            if (((CheckBox)gr.Cells[4].FindControl("cb_project")).Checked)
                child_project += ((HiddenField)gr.Cells[4].FindControl("hf_projectno")).Value.ToString().Trim() + ",";
        }
        datasourceIB ibobj = new datasourceIB();
        if (ibobj.assignchildtoparentproject(child_project.TrimEnd(','),dd_parentproject.SelectedItem.Value.ToString()))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Successfully assigned');</script>");
        else
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Assign failed');</script>");

    }
}
