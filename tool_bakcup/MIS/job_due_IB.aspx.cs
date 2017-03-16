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

public partial class job_due_IB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if (Session["CustomerDS"] != null)
            //{
                Datasource_IBSQL ibobj = new Datasource_IBSQL();
                DataSet oDS = new DataSet();
                oDS = ibobj.getAllCustomers();
                ddlcustomer.DataSource = oDS;
                ddlcustomer.DataBind();
                oDS = null;
            //}
            //else
            //{
            //    string sHTML = "";
            //    sHTML += "<script language='javascript'>";
            //    sHTML += "window.open('Login.aspx','_top')";
            //    sHTML += "</script>";

            //    ClientScript.RegisterStartupScript(this.GetType(), "Script", sHTML);
            //}
        }
            
    }
    protected void Submit_btn_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL ibobj = new Datasource_IBSQL();
        DataSet ibds = new DataSet();
        try
        {
            string[,] param = { { "@WCUSTNO", ddlcustomer.SelectedValue.ToString()}, 
                                { "@JOBTYPE", ddljobtype.SelectedValue.ToString() } };
            ibds = ibobj.InvoiceDataSet("SP_GET_JOBS_DUE_ISSUE", param, CommandType.StoredProcedure);

            if (ibds != null && ibds.Tables[0].Rows.Count > 0)
            {
                gv_duereport.Visible = true;
                gv_duereport.DataSource = ibds;
                gv_duereport.DataBind();
            }
            else
            {
                gv_duereport.Visible = false;
                div_error.Visible = true;
                div_error.InnerText = "No Records Found";
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { ibobj = null; ibds = null; }
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        //adgdispatchedlist.RenderControl(oHtmlTextWriter);
        HtmlForm htmfrm = new HtmlForm();
        gv_duereport.Parent.Controls.Add(htmfrm);
        htmfrm.Attributes["runat"] = "Server";
        htmfrm.Controls.Add(gv_duereport);
        htmfrm.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}
