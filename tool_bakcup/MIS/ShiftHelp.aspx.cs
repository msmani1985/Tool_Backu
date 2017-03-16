using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShiftHelp : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = SqlObj.GetShift(Request.QueryString["LocationID"].ToString());
            grvShift.DataSource = ds;
            grvShift.DataBind();
        }
    }
}