using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Todate_Due : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridData();

        }
    }
    protected void AutoRefreshGrid(object sender, EventArgs e)
    {
        BindGridData();
        Label1.Text = DateTime.Now.ToString() ;
    }

    public void BindGridData()
    {
        datasourceIBSQL objds = new datasourceIBSQL();
        DataSet ds = new DataSet();
        ds = objds.GetDataSet("spGET_LIVE_Job_Todate", "Ds", CommandType.StoredProcedure);
        gridview.DataSource = ds.Tables[0];
        gridview.DataBind();
    }
}