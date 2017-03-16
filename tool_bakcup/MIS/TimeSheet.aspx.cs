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
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;


public partial class TimeSheet : System.Web.UI.Page
{
    OdbcConnection con = null;
    string connectionstring = "";
    string query = "";
    OdbcCommand cmd = new OdbcCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null){
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack) this.popScreen();
    }

    private void popScreen()
    {
        try
        {
            FromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            ToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            if (Request.QueryString["q"] != null && Request.QueryString["q"].ToString() == "newmis"){
                ddlOperator.Items.Clear();
                datasourceSQL oSql = new datasourceSQL();
                SqlParameter[] param = new SqlParameter[3];
                string sSql = "[spGetEmployeeList]";
                DataSet ds = oSql.FillDataSet_SP(sSql, null);
                ddlOperator.DataSource = ds;
                ddlOperator.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlOperator.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlOperator.DataBind();
            }
            else{
                connectionstring = ConfigurationManager.ConnectionStrings["conStrIB"].ToString();
                con = new OdbcConnection(connectionstring);
                query = "select Emp_ID, f_rtrim(Emp_Fname) ||' '|| f_rtrim(Emp_sname) as Emp_FName from employee_dp where status = 1 and Emp_ID IS NOT NULL and Emp_ID <> 0 ORDER BY Emp_FName";
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                OdbcDataAdapter da = new OdbcDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ddlOperator.Items.Clear();
                //ddlOperator.DataSource = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++){
                    ListItem li = new ListItem();
                    li.Text = ds.Tables[0].Rows[i]["Column1"].ToString();
                    li.Value = ds.Tables[0].Rows[i]["Emp_ID"].ToString();
                    ddlOperator.Items.Insert(i, li);
                }
                con.Close();
                da.Dispose();
                ds.Dispose();

            }
        }
        finally
        {
            if (cmd != null) cmd.Dispose();
            if (con != null) con.Dispose();
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        Session["ts_EmpName"] = ddlOperator.SelectedItem.Text;
        Session["ts_EmpNo"] = ddlOperator.SelectedValue;
        Session["ts_StartDate"] = FromDate.Text;
        Session["ts_EndDate"] = ToDate.Text;
        if (Request.QueryString["q"] != null && Request.QueryString["q"].ToString() == "newmis") Session["ts_SqlRep"] = "true";
        else Session["ts_SqlRep"] = "false";
        Page.RegisterStartupScript("Open", "<script language = 'javascript'>window.open('TimeSheetReport.aspx','Preview','width=1000,height=650,left=10,top=10,menubar=yes,toolbars=no,scrollbars=yes,status=yes,resizable=yes');</script>");
        
    }
}
