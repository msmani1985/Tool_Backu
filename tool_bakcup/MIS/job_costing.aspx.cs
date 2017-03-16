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
using System.Xml;
using System.Text;
/*
/// <summary>
/// Created by: Royson
/// Creation Date: Tuesday, July 6, 2010
/// </summary>
 * */
public partial class job_costing : System.Web.UI.Page
{
    private static XmlDocument oXmlDoc = null;
    private static string sInvFile = ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString();
    private static DataTable dtable = null;
    private static Hashtable htManHrs = null;
    private Costing oCost = new Costing();
    protected int id = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) this.popScreen();
    }
    void popScreen(){
        oXmlDoc = new XmlDocument();
        dtable = new DataTable();
        if (Session["CustomerName"] != null)
        {
            DataSet oDS = new DataSet();
            oDS = (DataSet)(Session["CustomerName"]);
            drpCustomer.DataSource = oDS;
            drpCustomer.DataBind();
            oDS = null;
        }
        else throw new Exception("Session Expired!");
        for (int j = 2009; j <= DateTime.Now.Year; j++) drpYears.Items.Insert(0, new ListItem(j.ToString(), j.ToString()));
        drpMonths.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["CustomerName"] == null) throw new Exception("Session Expired!");
        htManHrs = new Hashtable();
        Invoiced_IBSQL oIB = new Invoiced_IBSQL();
        DataSet ds = new DataSet();
        DataSet dsSum = new DataSet();
        string sStartDate = "", sEndDate = "";
        try
        {
            if (Request.QueryString["repid"] != null && Request.QueryString["repid"].ToString() != "")
            {
                if (drpMonths.SelectedItem.Value == "0")
                {
                    sStartDate = "1/1/" + drpYears.SelectedItem.Value;
                    sEndDate = DateTime.Parse(sStartDate).AddMonths(12).AddDays(-1).ToString("M/d/yyyy");
                }
                else
                {
                    sStartDate = "1/1/" + drpYears.SelectedItem.Value;
                    sEndDate = drpMonths.SelectedItem.Value + "/1/" + drpYears.SelectedItem.Value;
                    sEndDate = DateTime.Parse(sEndDate).AddMonths(1).AddDays(-1).ToString("M/d/yyyy");
                }
            }
            else
            {
                if (drpMonths.SelectedItem.Value == "0")
                {
                    sStartDate = "1/1/" + drpYears.SelectedItem.Value;
                    sEndDate = DateTime.Parse(sStartDate).AddMonths(12).AddDays(-1).ToString("M/d/yyyy");
                }
                else
                {
                    sStartDate = drpMonths.SelectedItem.Value + "/1/" + drpYears.SelectedItem.Value;
                    sEndDate = DateTime.Parse(sStartDate).AddMonths(1).AddDays(-1).ToString("M/d/yyyy");
                }
            }
            string sJobType = "";
            if (drpJobType.SelectedItem.Value == "1") sJobType = "6";
            else if (drpJobType.SelectedItem.Value == "2") sJobType = "2";
            else if (drpJobType.SelectedItem.Value == "3") sJobType = "4";
            else sJobType = "2,4,6";
            dsSum = oCost.getJobCostingSummary(sStartDate + " 00:00:00", sEndDate + " 23:59:59", sJobType);
            if (dsSum.Tables[0] != null && dsSum.Tables[0].Rows.Count > 0){
                DataRow roo = null;
                for (int v = 0; v < dsSum.Tables[0].Rows.Count; v++){
                    roo = dsSum.Tables[0].Rows[v];
                    if (!htManHrs.ContainsKey(roo["name"].ToString().Trim()))
                        htManHrs.Add(roo["name"].ToString().Trim(), roo["tot_mins"].ToString().Trim() + "|" + roo["cost"].ToString().Trim());
                    else
                    {
                        htManHrs[roo["name"].ToString().Trim()] = Convert.ToInt32(htManHrs[roo["name"].ToString().Trim()].ToString().Split('|')[0]) + Convert.ToInt32(roo["tot_mins"].ToString().Trim()) + "|" + (float.Parse(htManHrs[roo["name"].ToString().Trim()].ToString().Split('|')[1]) + float.Parse(roo["cost"].ToString().Trim())).ToString();
                    }
                }
            }
            oXmlDoc.Load(sInvFile);
            ds = oIB.GetInvoicedJobs3(Convert.ToInt32(drpCustomer.SelectedItem.Value), Convert.ToInt32(drpJobType.SelectedItem.Value), 0, 0, sStartDate, sEndDate);
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                dtable = ds.Tables[1].Copy();
                dtable.Columns.Add("EUROVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("MANHOURS", Type.GetType("System.String"));
                dtable.Columns.Add("JOBCOST", Type.GetType("System.Decimal"));
                dtable.Columns.Add("DIFFERENCE", Type.GetType("System.Decimal"));
                //dtable.Columns.Add("EUROVALUE_ACTUAL", Type.GetType("System.Decimal"));
                //dtable.Columns.Add("CUMULATIVE", Type.GetType("System.Decimal"));
                //dtable.Columns.Add("POUNDVALUE", Type.GetType("System.Decimal"));
                //dtable.Columns.Add("USDVALUE", Type.GetType("System.Decimal"));
                //dtable.Columns.Add("CADVALUE", Type.GetType("System.Decimal"));
                //dtable.Columns.Add("INRVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("TOTALPAGES_FINAL", Type.GetType("System.Int32"));
                //dtable.Columns.Add("MONTHNUMBER", Type.GetType("System.Int32"));
                DataRow row = null; decimal conv = 0;
                string sVal = "", sCurr = "", sCurRate = "1";
                for (int x = 0; x < dtable.Rows.Count; x++)
                {
                    sVal = ""; sCurr = ""; sCurRate = "1";
                    row = dtable.Rows[x]; row.BeginEdit();
                    string sData = this.getInvoiceNode(row["IINNO"].ToString().Trim(),
                        row["JOURCODE"].ToString().Trim(), row["IISSUENO"].ToString().Trim());
                    if (row["TOTALPAGES_NOCOVER"].ToString().Trim() == "") row["TOTALPAGES_NOCOVER"] = "0";
                    if (row["TOTALPAGES"].ToString().Trim() == "") row["TOTALPAGES"] = "0";
                    //Added extra field for noprelims by subbu
                    if (row["INO"].ToString().Trim() == "23433" || row["INO"].ToString().Trim() == "23612")//For  Psychology Press
                        row["TOTALPAGES_FINAL"] = row["TOTALPAGES_NOCOVER"].ToString().Trim();

                    else if (row["INO"].ToString().Trim() == "23705" || row["INO"].ToString().Trim() == "23743" || row["INO"].ToString().Trim() == "23648" || row["INO"].ToString().Trim() == "23667" || row["INO"].ToString().Trim() == "23650" || row["INO"].ToString().Trim() == "23480" || row["INO"].ToString().Trim() == "23757" || row["INO"].ToString().Trim() == "23686")//All r TandF
                        row["TOTALPAGES_FINAL"] = row["EDITEDDISKTFNOPRELIMS"].ToString().Trim();

                    else if (row["custno1"].ToString().Trim() == "10040" && Convert.ToInt32(row["IINNO"].ToString().Trim()) > 26064 && Convert.ToInt32(row["IINNO"].ToString().Trim()) < 27115)
                        row["TOTALPAGES_FINAL"] = row["TOTALPAGES_PSY"].ToString().Trim();

                    else if (row["custno1"].ToString().Trim() == "10040" && Convert.ToInt32(row["IINNO"].ToString().Trim()) > 27115)
                        row["TOTALPAGES_FINAL"] = row["TOTALPAGES_PSY_NOPRELIMS"].ToString().Trim();
                    else if (row["IINNO"].ToString().Trim() == "26800")//For RAPL 47/1
                        row["TOTALPAGES_FINAL"] = int.Parse(row["EDITEDDISK_RAPL"].ToString().Trim()) + int.Parse(row["EDITEDDISKTF"].ToString().Trim());
                    else if (row["journo"].ToString().Trim() == "2683")//For RAPL
                        row["TOTALPAGES_FINAL"] = int.Parse(row["EDITEDDISK_RAPL"].ToString().Trim()) + int.Parse(row["EDITEDDISKTFNOPRELIMS"].ToString().Trim());
                    else if (row["CUSTNO1"].ToString().Trim() == "2556" &&
                   Convert.ToInt32(row["IINNO"].ToString().Trim()) > 26064 && Convert.ToInt32(row["IINNO"].ToString().Trim()) < 27115)
                        row["TOTALPAGES_FINAL"] = row["EDITEDDISKTF"].ToString().Trim();
                    else if (row["custno1"].ToString().Trim() == "2556" && Convert.ToInt32(row["IINNO"].ToString().Trim()) > 27115)
                        row["TOTALPAGES_FINAL"] = row["EDITEDDISKTFNOPRELIMS"].ToString().Trim();
                    else row["TOTALPAGES_FINAL"] = row["TOTALPAGES"].ToString().Trim();

                    if (sData.LastIndexOf("INVOICEVALUE=\"") != -1)
                    {
                        sVal = sData.Substring(sData.LastIndexOf("INVOICEVALUE=\"") + "INVOICEVALUE=\"".Length);
                        sVal = sVal.Substring(0, sVal.IndexOf("\""));
                    }
                    if (sData.LastIndexOf("INVOICECURRENCY=\"") != -1)
                    {
                        sCurr = sData.Substring(sData.LastIndexOf("INVOICECURRENCY=\"") + "INVOICECURRENCY=\"".Length);
                        sCurr = sCurr.Substring(0, sCurr.IndexOf("\""));
                    }
                    if (sData.LastIndexOf("CURRENCYRATE=\"") != -1)
                    {
                        sCurRate = sData.Substring(sData.LastIndexOf("CURRENCYRATE=\"") + "CURRENCYRATE=\"".Length);
                        sCurRate = sCurRate.Substring(0, sCurRate.IndexOf("\""));
                    }
                    row["EUROVALUE"] = 0;
                    row["MANHOURS"] = 0;
                    row["JOBCOST"] = 0;
                    row["DIFFERENCE"] = 0;
                    if (sCurr != "")
                    {
                        //switch (sCurr.ToLower())
                        //{
                        //    case "stg":
                        //        row["POUNDVALUE"] = sVal;
                        //        break;
                        //    case "dollar":
                        //        row["USDVALUE"] = sVal;
                        //        break;
                        //    case "euro":
                        //        row["EUROVALUE"] = sVal;
                        //        row["EUROVALUE_ACTUAL"] = sVal;
                        //        break;
                        //    case "cad":
                        //        row["CADVALUE"] = sVal;
                        //        break;
                        //}
                        conv = Math.Round(decimal.Parse(sVal) * decimal.Parse(sCurRate));
                        if (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                            row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland"){
                            conv = Math.Round((conv / 121) * 100);
                            row["EUROVALUE"] = conv;
                        }
                        else row["EUROVALUE"] = conv;                                                
                    }                    
                    if (htManHrs.ContainsKey(row["JOBTITLE"].ToString().Trim())){
                        row["MANHOURS"] = htManHrs[row["JOBTITLE"].ToString().Trim()].ToString().Split('|')[0];
                        //row["JOBCOST"] = (Convert.ToInt32(htManHrs[row["JOBTITLE"].ToString().Trim()].ToString()) / 60) * 9;                        
                        row["JOBCOST"] = Math.Round(float.Parse(htManHrs[row["JOBTITLE"].ToString().Trim()].ToString().Split('|')[1]));
                        if (float.Parse(row["EUROVALUE"].ToString().Trim()) > 0 && float.Parse(row["JOBCOST"].ToString().Trim()) > 0)
                            row["DIFFERENCE"] = Math.Round(float.Parse(row["EUROVALUE"].ToString().Trim()) - float.Parse(row["JOBCOST"].ToString().Trim()));
                    }
                    row.EndEdit();
                    row.AcceptChanges();
                }
                dtable.AcceptChanges();
                ds.Tables[1].Rows.Clear();
                ds.Tables[1].Merge(dtable);
                gvJobCosting.DataSource = dtable;
                gvJobCosting.DataBind();
            }
            else { gvJobCosting.DataBind(); }

        }
        catch (Exception ex)
        {
           Alert(ex.Message);
        }
    }
    public void Alert(string sMessage)
    {
        sMessage = sMessage.Replace("\r", "\\r");
        sMessage = sMessage.Replace("\n", "\\n");
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    private string getInvoiceNode(string sInvNo, string sInvItem, string sIssueNo)
    {
        try
        {
            XmlNode oNode = null;
            if (sInvItem.Length > 50) sInvItem = sInvItem.Remove(50);
            oNode = oXmlDoc.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICENO='" + sInvNo + "']");
            if (oNode == null) oNode = oXmlDoc.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM='" + sInvItem + sIssueNo + "']");
            return oNode.OuterXml;
        }
        catch
        { return ""; }
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        if (gvJobCosting.Visible)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td>&nbsp;</td><td>&nbsp;</td><td colspan='4' align='center'><h2>Job Costing(" + drpMonths.SelectedItem.Text.Trim() + " " + drpYears.SelectedItem.Text.Trim() + ")</h2></td><td colspan='3'><font color='green'><b>Books</b></font>/<font color='blue'><b>Projects</b></font>/Issues</td><tr>");
            sbData.Append("<tr valign='top'><td style='background-color:silver'><b>JOB NO</b></td><td style='background-color:silver'><b>INV NO</b></td><td style='background-color:silver'><b>CUSTOMER</b></td><td style='background-color:silver'><b>JOB TITLE</b></td><td style='background-color:silver'><b>PAGES</b></td><td style='background-color:silver' align='center'><b>INVOICED VALUE (&euro;)</b></td><td style='background-color:silver'><b>MAN-HOURS (mins)</b></td><td style='background-color:silver' align='center'><b>JOB COST (&euro;)</b></td><td style='background-color:silver' align='center'><b>PROFIT/LOSS (&euro;)</b></td></tr>");
            sbData.Append("<tr><td colspan='9'>&nbsp;</td></tr>");
            //sbData.Append("<tr valign='top'><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>");
            foreach (DataRow r in dtable.Rows)
            {
                sbData.Append("<tr valign='top'>");
                if (r["CATEGORY"].ToString().Trim().ToLower() == "book"){
                    sbData.Append("<td align='left' style='color:green'><b>" + r["JOBNO"] + "</b></td>");
                    sbData.Append("<td align='left' style='color:green'><b>" + r["IINNO"] + "</b></td>");
                    sbData.Append("<td align='left' style='color:green'><b>" + r["CNAME"].ToString().Trim() + "</b></td>");
                    sbData.Append("<td align='left' style='color:green'><b>" + r["JOBTITLE"].ToString().Trim() + "</b></td>");
                    sbData.Append("<td style='color:green'><b>" + r["TOTALPAGES_FINAL"] + "</b></td>");
                }
                else if (r["CATEGORY"].ToString().Trim().ToLower() == "project"){
                    sbData.Append("<td align='left' style='color:blue'><b>" + r["JOBNO"] + "</b></td>");
                    sbData.Append("<td align='left' style='color:blue'><b>" + r["IINNO"] + "</b></td>");
                    sbData.Append("<td align='left' style='color:blue'><b>" + r["CNAME"].ToString().Trim() + "</b></td>");
                    sbData.Append("<td align='left' style='color:blue'><b>" + r["JOBTITLE"].ToString().Trim() + "</b></td>");
                    sbData.Append("<td style='color:blue'><b>" + r["TOTALPAGES_FINAL"] + "</b></td>");
                }
                else{
                    sbData.Append("<td align='left'>" + r["JOBNO"] + "</td>");
                    sbData.Append("<td align='left'>" + r["IINNO"] + "</td>");
                    sbData.Append("<td align='left'>" + r["CNAME"].ToString().Trim() + "</td>");
                    sbData.Append("<td align='left'>" + r["JOBTITLE"].ToString().Trim() + "</td>");
                    sbData.Append("<td>" + r["TOTALPAGES_FINAL"] + "</td>");
                }
                sbData.Append("<td>" + r["EUROVALUE"] + "</td>");
                sbData.Append("<td>" + r["MANHOURS"] + "</td>");
                sbData.Append("<td><b>" + r["JOBCOST"] + "</b></td>");
                if (r["DIFFERENCE"].ToString().Contains("-"))
                    sbData.Append("<td style='background-color:#FF8080'><b>" + r["DIFFERENCE"] + "</b></td>");
                //else if (r["DIFFERENCE"].ToString().Trim() != "0")
                //    sbData.Append("<td style='background-color:#CCFFCC'><b>" + r["DIFFERENCE"] + "</b></td>");
                else 
                    sbData.Append("<td><b>" + r["DIFFERENCE"] + "</b></td>");
                sbData.Append("</tr>");
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Job_Costing_" + drpMonths.SelectedItem.Text + "_" + drpYears.SelectedItem.Text.Trim() + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void gvJobCosting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow){
            if (((Label)e.Row.FindControl("lblgvDifference")).Text.Contains("-"))
                ((Label)e.Row.FindControl("lblgvDifference")).ForeColor = System.Drawing.Color.Red;
            else if (((Label)e.Row.FindControl("lblgvDifference")).Text != "0")
                ((Label)e.Row.FindControl("lblgvDifference")).ForeColor = System.Drawing.Color.Green;
            else { /**/}
        }
    }
}
