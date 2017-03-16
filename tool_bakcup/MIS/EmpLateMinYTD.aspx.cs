using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EmpLateMinYTD : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    Attendance SqlAtt = new Attendance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = SqlAtt.EmpLateYTD(Request.QueryString["ID"]);
            grvLate.DataSource = ds;
            grvLate.DataBind();
        }
    }
}