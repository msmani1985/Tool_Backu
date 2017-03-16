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
using System.IO;


public partial class bookinxml : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_error.Visible = false;
            div_xmljobdetails.Visible = false;
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        datasourceSQL xmlobj = new datasourceSQL();
        DataSet xds = new DataSet();
        div_xmljobdetails.Visible = false;
        div_error.Visible = false;
        try
        {
            xds = xmlobj.ExcProcedure("spGet_Job_Bookin", new string[,] { { "@jobtype",rb_jobtype.SelectedValue.ToString() } }, CommandType.StoredProcedure);
            if (xds != null)
            {
                div_xmljobdetails.Visible = true;
                gv_xmljob.DataSource = xds;
                gv_xmljob.DataBind();
            }
            else
            {
                div_error.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        { xmlobj = null; xds = null; }
    }
    protected void ibtn_dispatch_click(object sender, EventArgs e)
    {
        string rstr = string.Empty;
        foreach (GridViewRow rw in gv_xmljob.Rows)
        {
            CheckBox chid = (CheckBox)rw.Cells[6].FindControl("cb_dispatch");
            if (chid != null && chid.Checked)
            {
                rstr = (string.IsNullOrEmpty(rstr)) ? ((HiddenField)rw.Cells[6].FindControl("hf_job_id")).Value.ToString() : rstr + "," + ((HiddenField)rw.Cells[6].FindControl("hf_job_id")).Value.ToString();
            }
        }
        if (string.IsNullOrEmpty(rstr))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select the record before dispatch');</script>");
            return;
        }
        datasourceSQL dobj = new datasourceSQL();
        try
        {
            dobj.ExcSProcedure("spupdate_job_dispatch", new string[,] { { "@jobdispatch", "update job_history set despatch_date='" + DateTime.Now.ToShortDateString() + "', despatched_by=" + Session["employeeid"].ToString() + " where job_history_id in(" + rstr + ")" } }, CommandType.StoredProcedure);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Successfully Dispatched');</script>");
            btn_submit_Click(sender, e);
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { 
            dobj = null;
        }
    }
    protected void ibtn_delete_click(object sender, EventArgs e)
    {
        string del_str = string.Empty;
        foreach (GridViewRow gr in gv_xmljob.Rows)
        {
            CheckBox del_chk = (CheckBox)gr.Cells[7].FindControl("cb_delete");
            if (del_chk != null && del_chk.Checked)
                del_str = (string.IsNullOrEmpty(del_str)) ? ((HiddenField)gr.Cells[6].FindControl("hf_jobid")).Value.ToString() : del_str + "," + ((HiddenField)gr.Cells[6].FindControl("hf_jobid")).Value.ToString();
            if (string.IsNullOrEmpty(del_str))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please select the record before delete');</script>");
                return;
            }
            datasourceSQL delobj = new datasourceSQL();
            try
            {
                delobj.ExcSProcedure("spdelete_jobrecord", new string[,] { { "@jobdelete", del_str } }, CommandType.StoredProcedure);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Successfully Deleted');</scipt>");
                btn_submit_Click(sender, e);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { del_chk = null; }
        }
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=Booking-In-XML.xls");
            this.EnableViewState = false;
            StringWriter stwr = new StringWriter();
            HtmlTextWriter hwr = new HtmlTextWriter(stwr);
            HtmlForm hfrm = new HtmlForm();
            gv_xmljob.Parent.Controls.Add(hfrm);
            gv_xmljob.Columns[6].Visible = false;
            hfrm.Attributes["runat"] = "server";
            hfrm.Controls.Add(gv_xmljob);
            hfrm.RenderControl(hwr);
            Response.Write(stwr);
            Response.End();
        }
        catch (Exception ex)
        { throw ex; }
    }
}
