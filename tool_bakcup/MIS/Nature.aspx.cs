using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nature : System.Web.UI.Page
{
    datasourceIBSQL objCom = new datasourceIBSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet sds = new DataSet();

        sds = objCom.GetDataSet("sp_get_Nature_email", "data", CommandType.Text);
        adgdispatchedlist.DataSource = sds;
        adgdispatchedlist.DataBind();
    }
    protected void Grid_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
}