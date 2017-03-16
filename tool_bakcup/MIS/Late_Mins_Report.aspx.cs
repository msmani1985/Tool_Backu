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
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;

public partial class Late_Mins_Report : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    Attendance SqlAtt = new Attendance();
    int empid;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = SqlObj.GetAllEmpLate(Convert.ToInt16(Session["employeeid"].ToString()), Convert.ToInt16(Session["locationid"].ToString()));
            grvLvl.DataSource = ds;
            grvLvl.DataBind();
        }
    }
    protected void grvLvl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int day = DateTime.Now.Day;
            string sdate = "", edate = "";
            sdate = Convert.ToString(((DateTime.Now.Month))) + "/" + Convert.ToString(((DateTime.Now.Day))) + "/" + Convert.ToString(DateTime.Now.Year);
            edate = Convert.ToString(((DateTime.Now.Month))) + "/" + Convert.ToString(((DateTime.Now.Day))) + "/" + Convert.ToString(DateTime.Now.Year);
            Label Late = (Label)e.Row.FindControl("Late");
            Label Date = (Label)e.Row.FindControl("Date");
            Label empid = (Label)e.Row.FindControl("EmpID");
            DateTime sd = DateTime.Parse(sdate.ToString());
            DateTime ed = DateTime.Parse(edate.ToString());
            DataSet ds = new DataSet();
            string[,] param = { { "@empid", empid.Text }, { "@sdate", sd.ToShortDateString() }, { "@edate", ed.ToShortDateString() } };
            ds = SqlAtt.GetEmployeeLateMins("TotalLateMins3", param, CommandType.StoredProcedure);
            if (ds!=null)
            {
                Late.Text = ds.Tables[0].Rows[0]["LateIn_HH:MM"].ToString();
                Date.Text = ds.Tables[0].Rows[0]["PDate"].ToString();
            }
        }
    }
}