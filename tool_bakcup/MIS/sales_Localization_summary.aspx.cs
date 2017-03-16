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
using System.Text;
/*
/// <summary>
/// Created by: Royson
/// Creation Date: Tuesday, July 14, 2010
/// </summary>
 * */
public partial class sales_Localization_summary : System.Web.UI.Page
{
    protected int id = 1;
    private Sales_Local oSales = new Sales_Local();
    private static DataTable dtPub;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    private void popScreen(){
        cmd_Excel_Export.Enabled = false;
        dtPub = new DataTable();
        DataSet dsCountry = oSales.getCountryList();
        drpCountry.DataSource = dsCountry;
        drpCountry.DataValueField = dsCountry.Tables[0].Columns[2].ToString();
        drpCountry.DataTextField = dsCountry.Tables[0].Columns[1].ToString();
        drpCountry.DataBind();
        drpCountry.Items.Insert(0, new ListItem(" --- All --- ", ""));        
    }
    protected void btnSubmit_Click(object sender, EventArgs e){
        cmd_Excel_Export.Enabled = true;
        gvPubSummary.DataSource = oSales.getPublishersSummary1(drpCountry.SelectedItem.Value.Trim());
        gvPubSummary.DataBind();
        dtPub = ((DataSet)gvPubSummary.DataSource).Tables[0].Copy();
    }
    protected void gvPubSummary_RowDataBound(object sender, GridViewRowEventArgs e){
        if (e.Row.RowType == DataControlRowType.DataRow){
            e.Row.ToolTip = ((HiddenField)e.Row.FindControl("hfgvpubDesc")).Value;
            //if (((HiddenField)e.Row.FindControl("hfgvpubWeb")).Value.Trim() != "")
            //{
            //    e.Row.Attributes.Add("onclick", "javascript:popWeb('" + ((HiddenField)e.Row.FindControl("hfgvpubWeb")).Value + "');");
            //    e.Row.Style.Add("cursor", "pointer");
            //}
        }
    }
    protected void cmd_Excel_Export_Click(object sender, EventArgs e)
    {
        if (gvPubSummary.Visible)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td style='background-color:silver'><b>Customer Name</b></td><td style='background-color:silver'><b>Address</b></td><td style='background-color:silver'><b>City</b></td><td style='background-color:silver'><b>State</b></td><td style='background-color:silver'><b>Pin Code</b></td><td style='background-color:silver'><b>Country</b></td><td style='background-color:silver'><b>Description</b></td><td style='background-color:silver'><b>Type</b></td><td style='background-color:silver'><b>Category</b></td><td style='background-color:silver'><b>Contact Name</b></td><td style='background-color:silver'><b>Contact Title</b></td><td style='background-color:silver'><b>Phone</b></td><td style='background-color:silver'><b>Fax</b></td><td style='background-color:silver'><b>E-Mail</b></td><td style='background-color:silver'><b>FollowUp Details</b></td><td style='background-color:silver'><b>Web</b></td></tr>");                        
            foreach (DataRow r in dtPub.Rows){
                sbData.Append("<tr style='height:18px;' valign='top'>");
                sbData.Append("<td>" + r["slcompany"] + "</td>");
                sbData.Append("<td>" + r["slfulladdress"] + "</td>");
                sbData.Append("<td>" + r["slcity"] + "</td>");
                sbData.Append("<td>" + r["slstate"] + "</td>");
                sbData.Append("<td>" + r["slpocode"] + "</td>");
                sbData.Append("<td>" + r["ctyname"] + "</td>");
                sbData.Append("<td>" + r["sldescription"] + "</td>");
                sbData.Append("<td>" + r["jtname"] + "</td>");
                sbData.Append("<td>" + r["slcatname"] + "</td>");
                sbData.Append("<td>" + r["ci_name"] + "</td>");
                sbData.Append("<td>" + r["ci_title"] + "</td>");
                sbData.Append("<td>" + r["ci_phone"] + "</td>");
                sbData.Append("<td>" + r["ci_fax"] + "</td>");
                sbData.Append("<td>" + r["ci_email"] + "</td>");
                sbData.Append("<td>" + r["followup"] + "</td>"); 
                sbData.Append("<td>" + r["ci_web"] + "</td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Publisher_Information_" + DateTime.Now.ToString("MM_dd_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();            
        }
    }
}
