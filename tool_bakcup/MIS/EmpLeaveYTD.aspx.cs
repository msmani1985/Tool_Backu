using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmpLeaveYTD : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    Attendance SqlAtt = new Attendance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = SqlAtt.EmpLeaveYTD(Request.QueryString["ID"]);
            ds = SqlObj.EmpLeaveYTD(Request.QueryString["ID"]);
            grvLate.DataSource = ds;
            grvLate.DataBind();
        }
    }
}