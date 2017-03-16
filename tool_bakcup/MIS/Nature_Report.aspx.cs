using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Nature_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("sp_GetSrep_Report", null, CommandType.StoredProcedure);
        grdProdReport.DataSource = dst;
        grdProdReport.DataBind();
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=SREP_Report_" + DateTime.Now.ToLongDateString() + ".xls");
            this.EnableViewState = false;
            System.IO.StringWriter strwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);

            HtmlForm htmlfrm = new HtmlForm();
            grdProdReport.Parent.Controls.Add(htmlfrm);
            htmlfrm.Attributes["runat"] = "server";
            htmlfrm.Controls.Add(grdProdReport);
            htmlfrm.RenderControl(txtwriter);
            Response.Write(strwriter);
            Response.End();

        }
        catch (Exception Ex)
        {
            
        }
    }
}