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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;
using System.IO;
using System.Drawing;

public partial class sqlProductivityReport : System.Web.UI.Page
{
    ReportDocument prodrpt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Txtsdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            Txtedate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            Lblemp.Text = "Employee Name:";

            if (Session["SQLTeamDS"] == null)
                Loadcombo(ddlemp, "spGet_TeamMembers", new string[,] { { "@team_owner_id", Session["employeeid"].ToString() } });
            else
                LoadfrmSession();
        }
        //else
        //    BtnSubmit_Click(sender, e);
        
        
    }
    protected void LoadfrmSession()
    {
        DataSet sds = new DataSet();
        try
        {
            sds = (DataSet)Session["SQLTeamDS"];
            ddlemp.DataSource = sds.Tables[0];
            ddlemp.DataBind();
        }
        catch (Exception ex)
        {
           
            errMessage.Visible = false;
            errMessage.InnerText = ex.Message.ToString();
        }
        finally
        { sds = null; }
    }
    protected void rbtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtn.SelectedItem.Value == "0")
        {
            Lblemp.Text = "Employee Name:";
            if(Session["SQLTeamDS"] == null)
                Loadcombo(ddlemp, "spGet_TeamMembers", new string[,] { { "@team_owner_id", Session["employeeid"].ToString() } });
            else
                LoadfrmSession();


        }
        else if (rbtn.SelectedItem.Value == "1")
        {
            Lblemp.Text = "Team:";
            Loadcombo(ddlemp, "spGet_Team", null);
        }
    }
    private void Loadcombo(DropDownList ddl,string qry,string[,] param)
    {
        datasourceSQL sobj = new datasourceSQL();
        DataSet sds = new DataSet();
        try
        {
            sds = sobj.ExcProcedure(qry, param, CommandType.StoredProcedure);
            ddl.DataSource = sds.Tables[0];
            ddl.DataBind();
        }
        catch (Exception ex)
        {
          
            errMessage.Visible = false;
            errMessage.InnerText = ex.Message.ToString();
        }
        finally { sds = null; sobj = null; }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
       // DateTime dt1,dt2;
        //For Fist Time load bcz i have call in page load 
        if (Txtsdate.Text == "" && Txtedate.Text == "")
            return;

        //if (!(DateTime.TryParse(Txtsdate.Text,out dt1)) && !(DateTime.TryParse(Txtedate.Text,out dt2)))
        //if (!(isdateFn(Txtsdate.Text)) || !(isdateFn(Txtedate.Text)))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('Please give correct date format(MM/DD/YYYY)');</script>");
        //    return;
        //}

        //sqlProductivityDS ProdDs=new sqlProductivityDS();
        DataSet ProdDs = new DataSet();
        datasourceIBSQL proobj = new datasourceIBSQL();

        

        try
        {
            string sdate = Txtsdate.Text + " 00:00:00";
            string edate = Txtedate.Text + " 23:59:59";

            DateTime dsdate = Convert.ToDateTime(sdate.ToString());
            DateTime dedate = Convert.ToDateTime(edate);

            if (rbtn.SelectedValue == "0")
                ProdDs = proobj.GetProductivityReport(ddlemp.SelectedValue.ToString(), "0", dsdate, dedate);
            else
                ProdDs = proobj.GetProductivityReport("0", ddlemp.SelectedValue.ToString(), dsdate, dedate);
          

            if (ProdDs != null && ProdDs.Tables[0].Rows.Count > 0)
            {
                ////////Div_Report.Visible = true;
                errMessage.Visible = false;
                ////////prodrpt = new ReportDocument();
                ////////prodrpt.FileName = Server.MapPath("~\\SQL - Productivity Report\\sqlproductivityReport.rpt");
                ////////prodrpt.SetDataSource(ProdDs.Tables[1]);

                ////////CRViewerProductivity.ReportSource = prodrpt;
                Session["ProdD"] = ProdDs;
                grdProdReport.DataSource = ProdDs.Tables[0];
                grdProdReport.DataBind();
            }
            else
            {
                grdProdReport.DataSource = null;
                grdProdReport.DataBind();
                
            }
            
            //CRViewerProductivity.RefreshReport();
            
            
        }
        catch (Exception ex)
        { errMessage.InnerText = ex.Message.ToString(); errMessage.Visible = true; }
        finally 
        {
            //prodrpt = null; 
        }
    }
    private Boolean isdateFn(string Date_Str)
    {
        DateTime dt;
        try
        {

            if (DateTime.TryParse(DateTime.Parse(Date_Str).ToString("MM/dd/yyyy"), out dt))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;

        }
        finally
        { }
    }
    protected void page_Unload(object sender, EventArgs e)
    {
        prodrpt.Close();
        prodrpt.Dispose();
        prodrpt = null;
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
           //// datasourceIBSQL ibSql = new datasourceIBSQL();
           ////string strDownloadPath = ibSql.GetDownloadPath(grdProdReport, "Productivity Report ");
          
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Productivity_Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdProdReport.AllowPaging = false;


                grdProdReport.DataSource = Session["ProdD"];
                grdProdReport.DataBind();

                grdProdReport.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdProdReport.HeaderRow.Cells)
                {
                    cell.BackColor = grdProdReport.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdProdReport.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdProdReport.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdProdReport.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdProdReport.RenderControl(hw);

                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            
        }
        catch (Exception Ex)
        {
           // lblStaus.Text = "Error in file downloading";
        }
    }

    protected void grdProdReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["ProdD"] != null)
        {
            grdProdReport.PageIndex = e.NewPageIndex;
            grdProdReport.DataSource = Session["ProdD"];
            grdProdReport.DataBind();
            
        }
        else
        {
            grdProdReport.PageIndex = e.NewPageIndex;
            grdProdReport.DataSource = Session["ProdD"];
            grdProdReport.DataBind();
           
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}
