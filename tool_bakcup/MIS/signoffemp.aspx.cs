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

public partial class signoffemp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeename"] != null)
            Lbl_EmpName.Text = Session["employeename"].ToString();
        else
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>window.open('login.aspx','_top');</script>");
    }
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        datasourceSQL sqlobj = new datasourceSQL();
        DataSet tasksql = new DataSet();
        try
        {
            tasksql = sqlobj.ExcProcedure("SPGet_TimeSheetTask", new string[,] {{"@employee_id",Session["employeeid"].ToString()},
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

    }
    protected void Img_Excel_Click(object sender, ImageClickEventArgs e)
    {
        if (GVEmployeeTask.Rows.Count==0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('No Record to Export');</script>");
            return;
        }
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
        System.IO.StringWriter stringwr = new System.IO.StringWriter();
        HtmlTextWriter htmlwr = new HtmlTextWriter(stringwr);
        HtmlForm htmlfrm = new HtmlForm();
        GVEmployeeTask.Parent.Controls.Add(htmlfrm);
        htmlfrm.Attributes["runat"] = "server";
        htmlfrm.Controls.Add(GVEmployeeTask);
        htmlfrm.RenderControl(htmlwr);
        Response.Write(stringwr.ToString());
        Response.End();
    }
}
