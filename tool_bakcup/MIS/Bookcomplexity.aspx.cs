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

public partial class Bookcomplexity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            datasourceSQL cobj = new datasourceSQL();
            DataSet cds = new DataSet();
            try
            {
                //cds=cobj.GetsDataSet("SP_GET_BOOKSNOTINVOICE", CommandType.StoredProcedure);
                div_bookdetails.Visible = false;
                div_error.Visible = false;
                if (cds != null)
                {
                    gv_bookcomplexity.DataSource = cds;
                    gv_bookcomplexity.DataBind();
                    div_bookdetails.Visible = true;
                }
                else
                { div_error.Visible = true; }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cobj = null; cds = null; }
        }
    }
    protected void img_complexitysave_click(object sender, EventArgs e)
    {
        string comqry = string.Empty;
        datasourceIB bobj = new datasourceIB();
        try
        {
            foreach (GridViewRow gr in gv_bookcomplexity.Rows)
            {
                CheckBox chx = (CheckBox)gr.Cells[5].FindControl("cb_complexity");
                if (chx != null)
                {
                    if (chx.Checked)
                        comqry = comqry + " update book_dp set complexitytype=" + ((DropDownList)gr.Cells[4].FindControl("dd_complexity")).SelectedValue.ToString()
                            + " where bno=" + ((HiddenField)gr.Cells[5].FindControl("hf_bno")).Value.ToString() + ";";
                }
            }
            bobj.Updatebookcomplexity(comqry);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Successfully saved');</script>");
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { bobj = null; }

        
    }
    protected void div_bookdetails_onRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList com = (DropDownList)e.Row.Cells[4].FindControl("dd_complexity");
        if (com != null)
            com.SelectedValue = DataBinder.Eval(e.Row.DataItem, "COMPLEXITYTYPE").ToString();
    }
}
