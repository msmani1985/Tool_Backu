using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class JournalInformation : System.Web.UI.Page
{
    datasourceIBSQL objCom = new datasourceIBSQL();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet dsCust = objCom.Customer();
            lstCustomer.DataSource = dsCust;
            lstCustomer.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string  selectItem="";
        foreach (ListItem li in lstCustomer.Items)
        {
            if (li.Selected)
            {
                selectItem += li.Value + ",";
            }
        }
        selectItem = selectItem.Substring(0, selectItem.Length - 1);
        DataSet ds = objCom.JourInfo(selectItem);
        Session["Jourino"] = ds.Tables[0];
        grdJournal.DataSource = ds;
        grdJournal.DataBind();
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtJournal=new DataTable();
        dtJournal = (DataTable)Session["Jourino"];
        if (dtJournal != null && dtJournal.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='10' align='center'><h4>Journal Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'> <td bgcolor='silver'><b>Customer Name</b></td><td bgcolor='silver'><b>Journal Acronym</b></td><td bgcolor='silver'><b>Journal Title</b></td><td bgcolor='silver'><b>Production Editor</b></td><td bgcolor='silver'><b>PE Email(s)</b></td><td bgcolor='silver'><b>Trim Size</b></td><td bgcolor='silver'><b>ormat</b></td><td bgcolor='silver'><b>Trim Code</b></td><td bgcolor='silver'><b>Price Code</b></td><td bgcolor='silver'><b>s CopyEdit</b></td><td bgcolor='silver'><b>Is Sensitive</b></td><td bgcolor='silver'><b>Is Sensitive</b></td><td bgcolor='silver'><b>Is SAM</b></td><td bgcolor='silver'><b>SAM Follow Days</b></td><td bgcolor='silver'><b>FPM Journal</b></td></tr>");
            foreach (DataRow r in dtJournal.Rows)
            {
                if (r["Active"].ToString() == "1")
                {
                    sbData.Append("<tr valign='top'>");
                    sbData.Append("<td>" + r["CUSTNAME"] + "</td>");
                    sbData.Append("<td>" + r["JOURCODE"] + "</td>");
                    sbData.Append("<td>" + r["JOURNAME"] + "</td>");
                    sbData.Append("<td>" + r["JPRODEDITOR"] + "</td>");
                    sbData.Append("<td>" + r["PEEMAIL"] + "</td>");
                    sbData.Append("<td>" + r["TRIMSIZE"] + "</td>");
                    sbData.Append("<td>" + r["FORMAT"] + "</td>");
                    sbData.Append("<td>" + r["TRIMCODE"] + "</td>");
                    sbData.Append("<td>" + r["PCODE"] + "</td>");
                    sbData.Append("<td>" + r["ISCOPYEDIT"] + "</td>");
                    sbData.Append("<td>" + r["ISSENSITIVE"] + "</td>");
                    sbData.Append("<td>" + r["ISSAM"] + "</td>");
                    sbData.Append("<td>" + r["FOLLOW_DAYS"] + "</td>");
                    sbData.Append("<td>" + r["ISFPM"] + "</td>");
                    sbData.Append("<td>" + r["Live_Journal"] + "</td>");
                    sbData.Append("</tr>");
                }
                else
                {
                    sbData.Append("<tr valign='top'>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["CUSTNAME"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["JOURCODE"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["JOURNAME"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["JPRODEDITOR"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["PEEMAIL"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["TRIMSIZE"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["FORMAT"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["TRIMCODE"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["PCODE"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["ISCOPYEDIT"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["ISSENSITIVE"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["ISSAM"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["FOLLOW_DAYS"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["ISFPM"] + "</td>");
                    sbData.Append("<td bgcolor='#FA5BD3'>" + r["Live_Journal"] + "</td>");
                    sbData.Append("</tr>");
                }
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "OverAll_Journal_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtJournal = new DataTable();
        dtJournal = (DataTable)Session["Jourino"];
        if (dtJournal != null && dtJournal.Rows.Count > 0)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td colspan='10' align='center'><h4>Journal Summary</h4></td><tr>");
            sbData.Append("<tr valign='top'> <td bgcolor='silver'><b>Customer Name</b></td><td bgcolor='silver'><b>Journal Acronym</b></td><td bgcolor='silver'><b>Journal Title</b></td><td bgcolor='silver'><b>Production Editor</b></td><td bgcolor='silver'><b>PE Email(s)</b></td><td bgcolor='silver'><b>Trim Size</b></td><td bgcolor='silver'><b>ormat</b></td><td bgcolor='silver'><b>Trim Code</b></td><td bgcolor='silver'><b>Price Code</b></td><td bgcolor='silver'><b>s CopyEdit</b></td><td bgcolor='silver'><b>Is Sensitive</b></td><td bgcolor='silver'><b>Is Sensitive</b></td><td bgcolor='silver'><b>Is SAM</b></td><td bgcolor='silver'><b>SAM Follow Days</b></td><td bgcolor='silver'><b>FPM Journal</b></td></tr>");
            foreach (DataRow r in dtJournal.Rows)
            {
                if (r["Active"].ToString() == "1")
                {
                    sbData.Append("<tr valign='top'>");
                    sbData.Append("<td>" + r["CUSTNAME"] + "</td>");
                    sbData.Append("<td>" + r["JOURCODE"] + "</td>");
                    sbData.Append("<td>" + r["JOURNAME"] + "</td>");
                    sbData.Append("<td>" + r["JPRODEDITOR"] + "</td>");
                    sbData.Append("<td>" + r["PEEMAIL"] + "</td>");
                    sbData.Append("<td>" + r["TRIMSIZE"] + "</td>");
                    sbData.Append("<td>" + r["FORMAT"] + "</td>");
                    sbData.Append("<td>" + r["TRIMCODE"] + "</td>");
                    sbData.Append("<td>" + r["PCODE"] + "</td>");
                    sbData.Append("<td>" + r["ISCOPYEDIT"] + "</td>");
                    sbData.Append("<td>" + r["ISSENSITIVE"] + "</td>");
                    sbData.Append("<td>" + r["ISSAM"] + "</td>");
                    sbData.Append("<td>" + r["FOLLOW_DAYS"] + "</td>");
                    sbData.Append("<td>" + r["ISFPM"] + "</td>");
                    sbData.Append("<td>" + r["Live_Journal"] + "</td>");

                    sbData.Append("</tr>");
                }
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Live_Journal_summary_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";

            Response.Write(sbData.ToString());
            Response.End();
        }

    }
}