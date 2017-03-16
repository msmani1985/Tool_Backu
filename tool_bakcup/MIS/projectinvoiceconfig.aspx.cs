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

public partial class projectinvoiceconfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if (Session["customerDS"] != null)
            //{
            //    DataSet oDS = new DataSet();
            //    oDS = (DataSet)(Session["customerDS"]);
            //    ddlcustomer.DataSource = oDS;
            //    ddlcustomer.DataBind();
            //    oDS = null;
            //}
            //else
            //{
            //    string sHTML = string.Empty;
            //    sHTML += "<script language='javascript'>";
            //    sHTML += "window.open('Login.aspx','_top')";
            //    sHTML += "</script>";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", sHTML);
            //}
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Loadgrid();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_projectlist.EditIndex = e.NewEditIndex;
        Loadgrid();
       // ((DropDownList)gv_projectlist.Rows[e.NewEditIndex].FindControl("ddl_costdescription")).SelectedValue = ((Label)gv_projectlist.Rows[e.NewEditIndex].FindControl("lbl_costdes")).Text;
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_projectlist.EditIndex = -1;
        Loadgrid();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Datasource_IBSQL uobj = new Datasource_IBSQL();
        string ddlctypeid = ((DropDownList) gv_projectlist.Rows[e.RowIndex].FindControl("ddl_costtypeid")).SelectedValue.ToString();
        string ddldesid=((DropDownList) gv_projectlist.Rows[e.RowIndex].FindControl("ddl_costdescription")).SelectedValue.ToString();
        string rowid=((HiddenField)gv_projectlist.Rows[e.RowIndex].FindControl("hf_rowid")).Value;
        if (!uobj.updateinvconfig(ddlctypeid, ddldesid, rowid, rb_category.SelectedValue.ToString()))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Record is unable to update, Please try again');</script>");
        gv_projectlist.EditIndex = -1;
        Loadgrid();
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfcostid =(HiddenField) e.Row.FindControl("hf_invcostid");
            string costtype = string.Empty;
            if (hfcostid != null)
            {
                
                switch (hfcostid.Value)
                {
                    case "0":
                        costtype = "Pages"; break;
                    case "1":
                        costtype = "Hours"; break;
                    case "2":
                        costtype = "Issues"; break;
                    case "3":
                        costtype = "Slides"; break;
                    case "4":
                        costtype = "Pieces"; break;
                    case "5":
                        costtype = "KB"; break;
                    case "6":
                        costtype = "Images"; break;
                    case "7":
                        costtype = "Sets"; break;
                }
                Label lbl = (Label)e.Row.FindControl("lbl_costtype");
                if(lbl!=null)
                    lbl.Text = costtype;
            }
            hfcostid = (HiddenField)e.Row.FindControl("hf_invdes");
            if (hfcostid != null)
            {
                costtype = string.Empty;
                switch (hfcostid.Value)
                {
                    case "0":
                        costtype = "Yes"; break;
                    case "1":
                        costtype = "No"; break;
                }
                Label lbl = (Label)e.Row.FindControl("lbl_costdes");
                if(lbl!=null)
                    lbl.Text = costtype;
            }
            DropDownList ddlobj = (DropDownList)e.Row.FindControl("ddl_costtypeid");
            if (ddlobj != null)
                ddlobj.SelectedValue = ((HiddenField)e.Row.FindControl("hf_invcostid")).Value;
            DropDownList ddldesobj = (DropDownList)e.Row.FindControl("ddl_costdescription");
            if(ddldesobj!=null)
                ddldesobj.SelectedValue = ((HiddenField)e.Row.FindControl("hf_invdes")).Value;
        }
    }
    protected void Loadgrid()
    {
        Datasource_IBSQL configobj = new Datasource_IBSQL();
        DataSet cds = new DataSet();
        try
        {
            cds = configobj.GetFor_Invoiceconfig(rb_category.SelectedValue.ToString());
            if (cds != null && cds.Tables[0].Rows.Count > 0)
            {
                gv_projectlist.DataSource = cds;
                gv_projectlist.DataBind();
                div_error.Visible = false;
            }
            else
            {
                div_error.InnerHtml = "No record's found";
                div_error.Visible = true;
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { cds = null; configobj = null; }
    }
}
