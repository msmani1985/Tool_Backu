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

public partial class wiparticlelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["CustomerName"] != null)
            {
                DataSet oDS = new DataSet();
                oDS = (DataSet)(Session["CustomerName"]);
                ddl_customer.DataSource = oDS;
                ddl_customer.DataBind();
                oDS = null;
            }
            else
            {
                string sHTML = "";
                Page page = HttpContext.Current.Handler as Page;

                sHTML += "<script language='javascript'>";
                sHTML += "window.open('Login.aspx','_top')";
                sHTML += "</script>";

                //page.RegisterStartupScript("script", sHTML);
                ClientScript.RegisterStartupScript(this.GetType(), "script", sHTML);
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Datasource_IBSQL iobj = new Datasource_IBSQL();
        DataSet ids=new DataSet();
        try
        {
            ids = iobj.Get_WipArticledetails(Convert.ToInt32(ddl_category.SelectedValue), Convert.ToInt32(ddl_customer.SelectedValue));
            if (ids != null && ids.Tables[0].Rows.Count>0)
            {
                wip_invoice wobj = new wip_invoice();
                string priceval = string.Empty;
                /*foreach (DataRow dr in ids.Tables[0].Rows)
                {
                    priceval = wobj.getjournalcodeprice(dr["JCNO_2010"].ToString(), "i", dr["JOURCODE"].ToString());
                    if (!string.IsNullOrEmpty(priceval))
                        dr["WPAGES_PRICEVAL"] = priceval.Split(',').GetValue(0);
                    dr["WPAGES_VALUE"] = (dr["WPAGES_PRICEVAL"] != null) ? Convert.ToDouble(dr["WPAGES_PRICEVAL"]) * Convert.ToInt32(dr["NOOFPAGES"]) : 0;
                    priceval=string.Empty;
                    priceval =(dr["CE_PRICECODE"].ToString()!="0" && dr["CE_PRICECODE"]!=null)? wobj.getjournalcodeprice(dr["CE_PRICECODE"].ToString(), "i", dr["JOURCODE"].ToString()): "";
                    if(!string.IsNullOrEmpty(priceval))
                        dr["WCE_PRICEVAL"] = priceval.Split(',').GetValue(0);
                    dr["WCE_VALUE"] = (dr["WCE_PRICEVAL"] != null) ? Convert.ToDouble(dr["WCE_PRICEVAL"]) * Convert.ToInt32(dr["CE_PAGES"]) : 0;
                    priceval = string.Empty;
                    priceval =(dr["SAM_PRICECODE"].ToString()!="0" && dr["SAM_PRICECODE"]!=null)? wobj.getjournalcodeprice(dr["SAM_PRICECODE"].ToString(), "i", dr["JOURCODE"].ToString()) : "";
                    if (!string.IsNullOrEmpty(priceval))
                        dr["WSAM_PRICEVAL"]=priceval.Split(',').GetValue(0);
                    dr["WSAM_VALUE"] = (dr["WSAM_PRICEVAL"] != null) ? Convert.ToDouble(dr["WSAM_PRICEVAL"]) * Convert.ToInt32(dr["SAM_PAGES"]) : 0;
                }
                 * */
                div_error.Visible = false; div_wiparticles.Visible = true;
                gv_wiparticledetails.DataSource = ids;
                gv_wiparticledetails.DataBind();
            }
            else
            { div_wiparticles.Visible = false; div_error.Visible = true; div_error.InnerText = "No Records Found"; }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        { iobj = null; ids = null; }
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=WIP_Articles.xls");
        this.EnableViewState = false;
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        //adgdispatchedlist.RenderControl(oHtmlTextWriter);
        HtmlForm htmfrm = new HtmlForm();
        gv_wiparticledetails.Parent.Controls.Add(htmfrm);
        htmfrm.Attributes["runat"] = "Server";
        htmfrm.Controls.Add(gv_wiparticledetails);
        htmfrm.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();
    }
}
