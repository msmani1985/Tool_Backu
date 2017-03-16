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
using System.IO;

//Mugundhan R

public partial class invoice_sales_analysis : System.Web.UI.Page
{
    private static XmlDocument oXmlDoc = null;
    private static string sInvFile = ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString();
    private static DataTable dtable = null;
    private static string RepID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["employeeid"].ToString() == "3100")
            {
                drpJobType.Items.Add(new ListItem("Project", "3"));
            }
            else if (Session["employeeid"].ToString() == "2284")
            {
                drpJobType.Items.Add(new ListItem("Book", "2"));
                drpJobType.Items.Add(new ListItem("Project", "3"));
            }
            else if (Session["employeeid"].ToString() == "1859")
            {
                drpJobType.Items.Add(new ListItem("Journal", "1"));
                drpJobType.Items.Add(new ListItem("Project", "3"));
            }
            else if (Session["employeeid"].ToString() == "2277")
            {

                drpJobType.Items.Add(new ListItem("--All--", "0"));
                drpJobType.Items.Add(new ListItem("Journal", "1"));
                drpJobType.Items.Add(new ListItem("Book", "2"));
                drpJobType.Items.Add(new ListItem("Project", "3"));
            }
            else
            {
                drpJobType.Items.Add(new ListItem("--All--", "0"));
                drpJobType.Items.Add(new ListItem("Journal", "1"));
                drpJobType.Items.Add(new ListItem("Book", "2"));
                drpJobType.Items.Add(new ListItem("Project", "3"));
            } 

            this.popScreen();
        }
    }
    void popScreen()
    {
        oXmlDoc = new XmlDocument();
        dtable = new DataTable();
        RepID = "";
        if (Request.QueryString["repid"] != null)
        {
            if (Request.QueryString["repid"].ToString() == "1")
                lblTitle.Text = "YTD - Euro";
            else if (Request.QueryString["repid"].ToString() == "2")
                lblTitle.Text = "YTD - Pages";
            else if (Request.QueryString["repid"].ToString() == "3")
                lblTitle.Text = "YTD - Currency";
            RepID = Request.QueryString["repid"].ToString().Trim();
            divMonthly.Visible = false;
        }
        else
        {
            lblTitle.Text = "Monthly Sales Analysis";
            divMonthly.Visible = true;
        }
        if (Session["CustomerName"] != null)
        {
            DataSet oDS = new DataSet();
            oDS = (DataSet)(Session["CustomerName"]);

            drpCustomer.DataSource = oDS;
            string myvalue = "10066";
            drpCustomer.SelectedValue = myvalue.ToString();
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
        Invoiced_IBSQL oIB = new Invoiced_IBSQL();
        DataSet ds=new DataSet();
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
            oXmlDoc.Load(sInvFile);
            ds = oIB.GetInvoicedJobs3(Convert.ToInt32(drpCustomer.SelectedItem.Value), Convert.ToInt32(drpJobType.SelectedItem.Value), 0, 0, sStartDate, sEndDate);
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                DataView dv = ds.Tables[1].DefaultView;
                dv.RowFilter = "CATEGORY<>'Article'";
                dtable = dv.ToTable().Copy();
                //dtable = ds.Tables[1].Copy();
                dtable.Columns.Add("EUROVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("EUROVALUE_ACTUAL", Type.GetType("System.Decimal"));
                dtable.Columns.Add("CUMULATIVE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("POUNDVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("USDVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("CADVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("INRVALUE", Type.GetType("System.Decimal"));
                dtable.Columns.Add("TOTALPAGES_FINAL", Type.GetType("System.Int32"));
                dtable.Columns.Add("MONTHNUMBER", Type.GetType("System.Int32"));
                DataRow row = null; decimal conv = 0, cum = 0;
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
                    row["EUROVALUE_ACTUAL"] = 0;
                    row["POUNDVALUE"] = 0;
                    row["USDVALUE"] = 0;
                    row["CADVALUE"] = 0;
                    row["INRVALUE"] = 0;                    
                    if (sCurr != "")
                    {
                        switch (sCurr.ToLower())
                        {
                            case "stg"://Updated by subbu on 31May2011 For Richard J.Buswell (vat %)
                                row["POUNDVALUE"] = sVal;
                                if (Convert.ToDateTime(row["IDATE"]).Year <= 2011 && row["CUSTNO1"].ToString().Trim() == "10120")//For vat charges
                                    row["POUNDVALUE"] = Math.Round((decimal.Parse( sVal) / 121) * 100);
                                else if (Convert.ToDateTime(row["IDATE"]).Year >= 2012 && row["CUSTNO1"].ToString().Trim() == "10120")//For vat charges
                                    row["POUNDVALUE"] = Math.Round((decimal.Parse(sVal) / 123) * 100);
                                break;
                            case "dollar":
                                row["USDVALUE"] = sVal;
                                break;
                            case "euro":
                                row["EUROVALUE"] = sVal;
                                row["EUROVALUE_ACTUAL"] = sVal;
                                break;
                            case "cad":
                                row["CADVALUE"] = sVal;
                                break;
                        }
                        
                        //Updated by subbu on 31May2011 For 2 digit decimal format
                        conv = Math.Round(decimal.Parse(sVal) * decimal.Parse(sCurRate));                        
                        //conv = decimal.Parse(sVal) * decimal.Parse(sCurRate);                        
                        if (Convert.ToDateTime(row["IDATE"]).Year >= 2012 && (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                            row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland"))
                        {

                            if (row["FINNO"].ToString().Trim().ToLower() != "10195" && row["FINNO"].ToString().Trim().ToLower() != "10142") /* MO,Merrill Brink, ireland customer by invoiced to other country*/
                            {
                                if (row["FINNO"].ToString().Trim().ToLower() != "10174")
                                {
                                    conv = Math.Round((conv / 123) * 100);
                                }
                            }

                            //conv =(decimal)Convert.ToDouble(((conv / 121) * 100).ToString("0.00"));

                            row["EUROVALUE"] = conv;
                        }
                        else if (row["FINNO"].ToString().Trim().ToLower() == "10194") /* Lionbridge, ireland customer by invoiced to other country*/
                        {
                            conv = Math.Round((conv / 123) * 100);
                            row["EUROVALUE"] = conv;
                        }
                        else if (Convert.ToDateTime(row["IDATE"]).Year >= 2010 && (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                            row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland"))
                        {
                            conv = Math.Round((conv / 121) * 100);

                                //conv =(decimal)Convert.ToDouble(((conv / 121) * 100).ToString("0.00"));

                            row["EUROVALUE"] = conv;
                        }

                        else if (row["CONCOUNTRY"].ToString().Trim().ToLower() == "dublin" ||
                       row["CONCOUNTRY"].ToString().Trim().ToLower() == "ireland") //For Above 2009
                        {
                            conv = Math.Round((conv / Convert.ToDecimal(121.5)) * 100);

                            //conv =(decimal)Convert.ToDouble(((conv / 121) * 100).ToString("0.00"));

                            row["EUROVALUE"] = conv;
                        }
                        else row["EUROVALUE"] = conv;

                        //else row["EUROVALUE"] =(decimal) Convert.ToDouble(conv.ToString("0.00"));
                        cum = conv + cum;
                        //cum = (decimal)Convert.ToDouble(conv.ToString("0.00")) + cum;
                        row["CUMULATIVE"] = cum;
                    }
                    else row["CUMULATIVE"] = cum;
                    row["MONTHNUMBER"] = DateTime.Parse(row["IDATE"].ToString().Trim()).Month.ToString();
                    string sJobTitle = row["JOBTITLE"].ToString().Trim().ToLower();
                    if (sJobTitle.Contains("taxpoint") && (sJobTitle.Contains("issue") || sJobTitle.Contains("online")))
                        row["GROUPBY"] = "Software";
                    else if (sJobTitle.Contains("gaa") && (sJobTitle.Contains("issue") || sJobTitle.Contains("web") || sJobTitle.Contains("online")))
                        row["GROUPBY"] = "Software";
                    row.EndEdit();
                    row.AcceptChanges();
                }
                dtable.AcceptChanges();
                ds.Tables[1].Rows.Clear();
                ds.Tables[1].Merge(dtable);
                Session["dsYTD"] = ds;
                if (RepID != "") this.popReport();
                else
                {
                    gvSalesAnalysis.DataSource = dtable;
                    gvSalesAnalysis.DataBind();
                }
            }
            else if (RepID == "") 
                gvSalesAnalysis.DataBind();
            else { /*do nothing*/}
            
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
    protected void gvSalesAnalysis_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string columnValue = ((Label)e.Row.FindControl("lbleuro")).Text;
            if (columnValue.Contains("-"))
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
        }
    }
    protected void gvSalesAnalysis_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "India")
        {
            GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + " - " + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + "-" + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + " - " + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + "-" + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf");

            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script langugae='javascript'>alert('This invoice pdf is not available in the server. Please contact software team(software@datapage.org)');</script>");
        }
        if (e.CommandName == "Dublin")
        {
            GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
            if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + " - " + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + " - " + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + " - " + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + " - " + ((DataBoundLiteralControl)gvr.Cells[1].Controls[0]).Text.Trim() + ".pdf");
            else if (File.Exists(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf"))
                openfile(ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString() + "Datapage_" + ((DataBoundLiteralControl)gvr.Cells[3].Controls[0]).Text.Trim().Replace('/', '_') + ".pdf");

            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script langugae='javascript'>alert('This invoice pdf is not available in the server. Please contact software team(software@datapage.org)');</script>");
        }

    }
    private void openfile(string filename)
    {
        Response.Buffer = true;
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filename));
        string spathanem = Path.GetFileName(filename);
        Response.AddHeader("Content-Disposition", "attachment; filename='" + spathanem + "' ");
        Response.ContentType = "application/pdf";
        FileStream fs = new FileStream(filename, FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        Byte[] cnt = br.ReadBytes((int)(fs.Length - 1));
        Response.BinaryWrite(cnt);
        br.Close();
        fs.Close();
        Response.End();
    }
    protected void cmd_Excel_Export_Click(object sender, ImageClickEventArgs e)
    {
        if (gvSalesAnalysis.Visible)
        {
            StringBuilder sbData = new StringBuilder();
            sbData.Append("<table border='1'>");
            sbData.Append("<tr valign='top'><td>&nbsp;</td><td>&nbsp;</td><td colspan='5' align='center'><h2>" + drpMonths.SelectedItem.Text.ToUpper().Trim() + " INVOICING " + drpYears.SelectedItem.Text.Trim() + "</h2></td><td colspan='3'><font color='green'><b>Books</b></font>/<font color='blue'><b>Projects</b></font>/Issues</td><tr>");
            sbData.Append("<tr valign='top'><td style='color:Red'><b>JOB NO</b></td><td style='color:Red'><b>INV NO</b></td><td style='color:Red'><b>CUSTOMER</b></td><td style='color:Red'><b>JOB TITLE</b></td><td style='color:Red'><b>PAGES</b></td><td style='color:Red' align='center'><b>&euro;</b></td><td style='color:Red'><b>CUMULATIVE</b></td><td align='center'><b>&pound;</b></td><td align='center'><b>$</b></td><td align='center'><b>CAD$</b></td></tr>");
            sbData.Append("<tr><td colspan='10'>&nbsp;</td></tr>");
            //sbData.Append("<tr valign='top'><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><tr>");
            foreach (DataRow r in dtable.Rows)
            {
                if (r["EUROVALUE"].ToString() != "0")
                {
                    sbData.Append("<tr valign='top'>");
                    if (r["CATEGORY"].ToString().Trim().ToLower() == "book")
                    {
                        sbData.Append("<td align='left' style='color:green'><b>" + r["JOBNO"] + "</b></td>");
                        sbData.Append("<td align='left' style='color:green'><b>" + r["IINNO"] + "</b></td>");
                        sbData.Append("<td align='left' style='color:green'><b>" + r["CNAME"].ToString().Trim() + " " + r["SALES_SPLIT"].ToString().Trim() + "</b></td>");
                        sbData.Append("<td align='left' style='color:green'><b>" + r["JOBTITLE"].ToString().Trim() + "</b></td>");
                        sbData.Append("<td style='color:green'><b>" + r["TOTALPAGES_FINAL"] + "</b></td>");
                    }
                    else if (r["CATEGORY"].ToString().Trim().ToLower() == "project")
                    {
                        sbData.Append("<td align='left' style='color:blue'><b>" + r["JOBNO"] + "</b></td>");
                        sbData.Append("<td align='left' style='color:blue'><b>" + r["IINNO"] + "</b></td>");
                        sbData.Append("<td align='left' style='color:blue'><b>" + r["CNAME"].ToString().Trim() + " " + r["SALES_SPLIT"].ToString().Trim() + "</b></td>");
                        sbData.Append("<td align='left' style='color:blue'><b>" + r["JOBTITLE"].ToString().Trim() + "</b></td>");
                        sbData.Append("<td style='color:blue'><b>" + r["TOTALPAGES_FINAL"] + "</b></td>");
                    }
                    else
                    {
                        sbData.Append("<td align='left'>" + r["JOBNO"] + "</td>");
                        sbData.Append("<td align='left'>" + r["IINNO"] + "</td>");
                        sbData.Append("<td align='left'>" + r["CNAME"].ToString().Trim() + " " + r["SALES_SPLIT"].ToString().Trim() + "</td>");
                        sbData.Append("<td align='left'>" + r["JOBTITLE"].ToString().Trim() + "</td>");
                        sbData.Append("<td>" + r["TOTALPAGES_FINAL"] + "</td>");
                    }
                    sbData.Append("<td>" + r["EUROVALUE"] + "</td>");
                    sbData.Append("<td>" + r["CUMULATIVE"] + "</td>");
                    sbData.Append("<td><b>" + r["POUNDVALUE"] + "</b></td>");
                    sbData.Append("<td><b>" + r["USDVALUE"] + "</b></td>");
                    sbData.Append("<td><b>" + r["CADVALUE"] + "</b></td>");
                    sbData.Append("</tr>");
                }
            }
            sbData.Append("</table>");
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Sales_" + drpMonths.SelectedItem.Text + "_" + drpYears.SelectedItem.Text.Trim() +".xls"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Write(sbData.ToString());
            Response.End();
        }
    }
    protected void popReport()
    {
        Session["YTDID"] = RepID;
        Session["YTDYear"] = drpYears.SelectedItem.Value.Substring(2);
        if (RepID == "") Alert("Invalid Page Request!");
        else Page.RegisterStartupScript("Open", "<script language='javascript'>window.open('invoice_sales_analysis_preview.aspx','Preview','width=1000,height=700,left=5,top=0,toolbars=no,scrollbars=yes,status=no,resizable=yes');</script>");        
    }
}
