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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class modulelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //Using Schema
            ReportDocument mdoc = new ReportDocument();
            Datasource_IBSQL mobj = new Datasource_IBSQL();
            try
            {
                mdoc.Load(Server.MapPath("") + @"/CrystalReports/Projectmodulerpt.rpt");
                mdoc.SetDataSource(((DataSet)mobj.projectmodulelist("SP_GET_PROJECTMODULE", CommandType.StoredProcedure)).Tables[1]);
                CV_modulelist.ReportSource = mdoc;
                if (rl_type.SelectedValue == "Received")
                    CV_modulelist.SelectionFormula = "{projectmoduleds.PRECEIVEDDATE} in Date(" + Convert.ToDateTime(txt_fromdate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Day + ") to Date(" + Convert.ToDateTime(txt_todate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Day + ")";
                //CV_modulelist.SelectionFormula = "isnull({projectmoduleds.pcompleteddate})";
                else if (rl_type.SelectedValue == "WIP")
                    CV_modulelist.SelectionFormula = "isnull({projectmoduleds.pcompleteddate}) and isnull({projectmoduleds.invno})";
                else if (rl_type.SelectedValue == "Despatched")
                    CV_modulelist.SelectionFormula = "{projectmoduleds.pcompleteddate} in Date(" + Convert.ToDateTime(txt_fromdate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Day + ") to Date(" + Convert.ToDateTime(txt_todate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Day + ")";

            }
            catch (Exception ex)
            { throw ex; }
            finally
            { mdoc = null; mobj = null; }


        }
        if (!Page.IsPostBack)
        { txt_fromdate.Text = DateTime.Now.ToShortDateString(); txt_todate.Text = DateTime.Now.ToShortDateString(); }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //ReportDocument mdoc = new ReportDocument();
        //try
        //{
        //    mdoc.Load(Server.MapPath("") + @"/CrystalReports/projectmoduleReport.rpt");
        //    mdoc.DataSourceConnections[0].SetConnection("IB_project", "", "sysdba", "masterkey");
            
        //    //mdoc.SetDatabaseLogon("sysdba", "masterkey");
        //    CV_modulelist.ReportSource = mdoc;
        //    if (rl_type.SelectedValue == "Live")
        //        CV_modulelist.SelectionFormula = "isnull({projects_dp.pcompleteddate})";
        //    else if (rl_type.SelectedValue == "Despatched")
        //        CV_modulelist.SelectionFormula = "not isnull({projects_dp.pcompleteddate})";
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //finally
        //{ mdoc = null; }

        //Using Schema
        ReportDocument mdoc = new ReportDocument();
        Datasource_IBSQL mobj = new Datasource_IBSQL();
        try
        {
            mdoc.Load(Server.MapPath("") + @"/CrystalReports/Projectmodulerpt.rpt");
            mdoc.SetDataSource(((DataSet)mobj.projectmodulelist("SP_GET_PROJECTMODULE", CommandType.StoredProcedure)).Tables[1]);
            CV_modulelist.ReportSource = mdoc;
            if (rl_type.SelectedValue == "Received")
                CV_modulelist.SelectionFormula = "{projectmoduleds.PRECEIVEDDATE} in Date(" + Convert.ToDateTime(txt_fromdate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Day + ") to Date(" + Convert.ToDateTime(txt_todate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_todate.Text.ToString()) .Day+ ")";
            //CV_modulelist.SelectionFormula = "isnull({projectmoduleds.pcompleteddate})";
            else if (rl_type.SelectedValue == "WIP")
                CV_modulelist.SelectionFormula = "isnull({projectmoduleds.pcompleteddate}) and isnull({projectmoduleds.INVNO})";
            else if (rl_type.SelectedValue == "Despatched")
                CV_modulelist.SelectionFormula = "{projectmoduleds.pcompleteddate} in Date(" + Convert.ToDateTime(txt_fromdate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_fromdate.Text.ToString()).Day + ") to Date(" + Convert.ToDateTime(txt_todate.Text.ToString()).Year + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Month + "," + Convert.ToDateTime(txt_todate.Text.ToString()).Day + ")";
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { mdoc = null; mobj = null; }

    }
}
