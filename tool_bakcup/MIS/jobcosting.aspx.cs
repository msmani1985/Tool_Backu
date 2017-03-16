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
using System.Data.Odbc;
using System.Text; 



public partial class jobcosting : System.Web.UI.Page
{
    string sSqlquery = string.Empty;
    OdbcCommand ocmd = new OdbcCommand();
    OdbcConnection oconn ;
    OdbcTransaction OTran;
    DataSet QDs = new DataSet();
    string cmdname;
    double INRValue;
    int iRowNumber = 0;
    XmlDocument JCRDom = new XmlDocument();
    XmlNode JCRNode = null;
    bool isExcel = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtEnddate.Text = DateTime.Today.ToShortDateString();
            txtStartdate.Text = DateTime.Today.ToShortDateString();
            if (Session["jobcostDS"] != null)
            {
                JobCostDetails.DataSource = (DataSet)Session["jobcostDS"];
                JobCostDetails.DataBind();
                JobCostDetails.Visible = true;
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Report JObj = new Report();
        DataSet JDs = new DataSet();
        try
        {
            if (txtStartdate.Text != "" && txtEnddate.Text != "")
            {
               // oconn = JObj.DBLiveOpen();
                //OTran = oconn.BeginTransaction();
                ocmd.Connection = oconn;
                //ocmd.Transaction = OTran;
                sSqlquery = "select c.CustName, J.JourCode, i.iIssueNo, i.iinvoiceno, i.INo, '' JCEURO, '' JCINR, '' JREURO, '' JRINR, '' PLEURO, '' PLINR, ";
                sSqlquery += " (select sum(arealnoofpages) from article_dp where Ino = i.Ino) as NoofPage from issue_dp i ";
                sSqlquery += " Inner join Journal_dp j ON j.JourNo = i.JourNo Inner join Customer_dp c ON j.CustNO = c.CustNo";
                sSqlquery += " where iinvoiced='Y' and iinvoiceno is not null and iinvoicedate between '" + txtStartdate.Text + "' and '" + txtEnddate.Text + "'";
                JDs = ExcueQuery(sSqlquery, "JobCostDetails");

                
                if (JDs != null)
                {
                    if (JDs.Tables[0].Rows.Count > 0)
                    {
                        errMessage.Visible = false;
                        JobCostDetails.DataSource = JDs.Tables[0];
                        Session["jobcostDS"] = JDs;
                        JobCostDetails.DataBind();
                        JobCostDetails.Visible = true;
                    }
                    else
                    {
                        errMessage.InnerHtml = "No Records";
                        errMessage.Visible = true;
                        JobCostDetails.Visible = false;
                    }
                }
            }
            else
            {
                errMessage.InnerHtml = "Start Date or End Date is missing";
                errMessage.Visible = true;
                JobCostDetails.Visible = false;
            }

            Session["JobCostLog"] = "PageLoad";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (oconn.State != ConnectionState.Closed)
                oconn.Close();
            oconn = null;
            ocmd = null;
            JObj = null;
            JDs = null;

        }

       
    }
  
    protected void JobCostDetails_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView grid = sender as GridView;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell rht = new TableHeaderCell();
            rht.ColumnSpan = 7;
            rht.Text = "";
            row.Cells.Add(rht);

            TableCell rht2 = new TableHeaderCell();
            rht2.ColumnSpan = 2;
            rht2.HorizontalAlign = HorizontalAlign.Center;  
            rht2.Text = "Job Cost";
            rht2.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            row.Cells.Add(rht2);

            TableCell Jrevenue = new TableHeaderCell();
            Jrevenue.ColumnSpan = 2;
            Jrevenue.HorizontalAlign = HorizontalAlign.Center; 
            Jrevenue.Text = "Job Revenue";
            Jrevenue.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            row.Cells.Add(Jrevenue);

            TableCell ProfitLoss = new TableHeaderCell();
            ProfitLoss.ColumnSpan = 2;
            ProfitLoss.HorizontalAlign = HorizontalAlign.Center; 
            ProfitLoss.Text = "Profit/Loss";
            ProfitLoss.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            row.Cells.Add(ProfitLoss);
            
            Table t = grid.Controls[0] as Table;
            if (t != null)
            {
                t.EnableViewState = true;
                t.Rows.AddAt(0, row);
            }

            /*
            Label JCEuro = new Label();
            JCEuro.Text = "(Euro)";
            TableCell tc = new TableHeaderCell();
            tc.Controls.Add(JCEuro);
            // tc.EnableViewState = true;
            e.Row.Cells.AddAt(7, tc);

            Label JCInr = new Label();
            TableCell tc2 = new TableHeaderCell();
            JCInr.Text = "(INR)";

            tc2.Controls.Add(JCInr);
            // tc2.EnableViewState = true;
            e.Row.Cells.AddAt(8, tc2);

            Label JREuro = new Label();
            JREuro.Text = "(Euro)";
            TableCell tc3 = new TableHeaderCell();
            tc3.Controls.Add(JREuro);
            // tc3.EnableViewState = true;
            e.Row.Cells.AddAt(9, tc3);

            Label JRInr = new Label();
            JRInr.Text = "(INR)";
            TableCell tc4 = new TableHeaderCell();
            tc4.Controls.Add(JRInr);
            //tc4.EnableViewState = true;
            e.Row.Cells.AddAt(10, tc4);

            Label PLEuro = new Label();
            PLEuro.Text = "(Euro)";
            TableCell tc5 = new TableHeaderCell();
            tc5.Controls.Add(PLEuro);
            //tc5.EnableViewState = true;
            e.Row.Cells.AddAt(11, tc5);

            Label PLInr = new Label();
            PLInr.Text = "(INR)";
            TableCell tc6 = new TableHeaderCell();
            tc6.Controls.Add(PLInr);
            //tc6.EnableViewState = true;
            e.Row.Cells.AddAt(12, tc6);
            */
        }
        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {

                string v = e.Row.Cells[4].Text;
                Label JCEuro = new Label();
                JCEuro.ID = "JCEuro";
                JCEuro.Visible = true;

                JCEuro.Text = "JCEuro";
                TableCell tc = new TableCell();
                tc.Controls.Add(JCEuro);
                // tc.EnableViewState = true;
                e.Row.Cells.AddAt(7, tc);

                Label JCInr = new Label();
                JCInr.ID = "JCInr";
                JCInr.Text = "";
                TableCell tc2 = new TableCell();
                tc2.Controls.Add(JCInr);
                //tc2.EnableViewState = true;
                e.Row.Cells.AddAt(8, tc2);

                Label JREuro = new Label();
                JREuro.ID = "JREuro";
                JREuro.Text = "JREuro";
                TableCell tc3 = new TableCell();
                tc3.Controls.Add(JREuro);
                // tc3.EnableViewState = true;
                e.Row.Cells.AddAt(9, tc3);


                Label JRInr = new Label();
                JRInr.ID = "JRInr";
                JRInr.Text = "JRInr";
                TableCell tc4 = new TableCell();
                tc4.Controls.Add(JRInr);
                //tc4.EnableViewState = true;
                e.Row.Cells.AddAt(10, tc4);

                Label PLEuro = new Label();
                PLEuro.ID = "PLEuro";
                PLEuro.Text = "PLEuro";
                TableCell tc5 = new TableCell();
                tc5.Controls.Add(PLEuro);
                // tc5.EnableViewState = true;
                e.Row.Cells.AddAt(11, tc5);

                Label PLInr = new Label();
                PLInr.ID = "PLInr";
                PLInr.Text = "PLInr";
                TableCell tc6 = new TableCell();
                tc6.Controls.Add(PLInr);
                //tc6.EnableViewState = true;
                e.Row.Cells.AddAt(12, tc6);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
         * */
                //JobCostDetails_OnRowDataBound(sender, e);
    }

    protected void JobCostDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        float ival;
        float ival2;
        double Euroval;
        float ival3;

        if (e.Row.RowType == DataControlRowType.DataRow)
            
        {
            try
            {
                e.Row.Cells[0].Text = e.Row.Cells[0].Text.Trim().ToString() ; 
                string ivno = e.Row.Cells[5].Text;
                string sPostURL = "";
                LinkButton LB = (LinkButton)(e.Row.Cells[6].FindControl("LinkButton1"));
                string val5 = LB.Text;
                LB.Text = CalculateManHours(e.Row.Cells[2].Text.Trim());
                e.Row.Cells[6].Text = Convert.ToString(Convert.ToInt32(LB.Text) / 60) + '.' + Convert.ToString(Convert.ToInt32(LB.Text) % 60);//CalculateManHours(ivno);
                Euroval = (Convert.ToDouble(8) * Convert.ToDouble(LB.Text.Trim())) / 60;
                e.Row.Cells[7].Text=Euroval.ToString();
                e.Row.Cells[9].Text = LoadJRValueXml(ivno, e.Row.Cells[1].Text.Trim().ToString() + e.Row.Cells[3].Text.Trim().ToString());

                e.Row.Cells[8].Text = Convert.ToString((float)(Convert.ToDouble(e.Row.Cells[7].Text.ToString().Trim())  * INRValue)) ;
                ival = (float)(Convert.ToDouble(e.Row.Cells[9].Text) * INRValue);

                e.Row.Cells[10].Text =ival.ToString();
                
                ival2 =(float)(Convert.ToDouble(e.Row.Cells[9].Text) - Convert.ToDouble(e.Row.Cells[7].Text));
                e.Row.Cells[11].Text = ival2.ToString();
                ival3 =(float)(Convert.ToDouble(e.Row.Cells[10].Text) - Convert.ToDouble(e.Row.Cells[8].Text) );
                e.Row.Cells[12].Text = ival3.ToString();
                sPostURL = "jobcostreport.aspx?";
                sPostURL += "custname=" + e.Row.Cells[0].Text.Trim() + "&";
                sPostURL += "jobtype=" + e.Row.Cells[1].Text.Trim() + "&";
                sPostURL += "jobid=" + e.Row.Cells[3].Text.Trim() + "&";
                sPostURL += "pages=" + e.Row.Cells[4].Text.Trim() + "&";
                sPostURL += "invNO=" + e.Row.Cells[5].Text.Trim() + "&";
                sPostURL += "manhours=" + e.Row.Cells[6].Text.Trim() + "&";
                sPostURL += "jcE=" + e.Row.Cells[7].Text.Trim() + "&";
                sPostURL += "jcI=" + e.Row.Cells[8].Text.Trim() + "&";
                sPostURL += "jrE=" + e.Row.Cells[9].Text.Trim() + "&";
                sPostURL += "jrI=" + e.Row.Cells[10].Text.Trim() + "&";
                sPostURL += "plE=" + e.Row.Cells[11].Text.Trim() + "&";
                sPostURL += "plI=" + e.Row.Cells[12].Text.Trim() + "&";
                sPostURL += "INo=" + e.Row.Cells[2].Text.Trim();
                e.Row.Cells[6].Text = "<a href='" + sPostURL.Trim() + "' target='_blank'>" + e.Row.Cells[6].Text.Trim() + "</a>";
                if (e.Row.Cells[11].Text.Trim().StartsWith("-") == true)
                {
                    e.Row.Cells[11].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

    public string LoadJRValueXml(string invoceno, string invoiceitem)
    {
        try
        {
            if (iRowNumber == 0)
            {
                QDs = ExcueQuery("select * from CURRENCYCONVERSION_DP where currno=3", "EuroToRupee");
                if (QDs.Tables[0].Rows.Count > 0)
                {
                    INRValue = Convert.ToDouble(QDs.Tables[0].Rows[0]["currvalue"]);
                }

                JCRDom.Load(MapPath("") + @"\InvoiceTemplates\Dublin\invoice_values.xml");
            }
            iRowNumber++;
            JCRNode = JCRDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICENO='" + invoceno + "']");

            if (JCRNode == null)
                JCRNode = JCRDom.DocumentElement.SelectSingleNode("ROWDATA").SelectSingleNode("ROW[@INVOICEITEM='" + invoiceitem + "']");

            if (JCRNode == null)
                return "0"; //
            //throw new ArgumentException(@"InvoiceNo is not available in InvoiceTemplates\Dublin\invoice_values.xml");
            else
            {
                /*
                if (JCRNode.Attributes.GetNamedItem("INVOICECURRENCY").Value != "Euro")
                {
                }
                else
                {
                    INRValue = 0;
                }
                */

                return JCRNode.Attributes.GetNamedItem("INVOICEVALUE").Value;
            }
        }
        catch (ArgumentException aex)
        {
            throw aex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataSet ExcueQuery(string Qry,string tablename)
    {
        //Report QObj = new Report();
        DataSet ConDs = new DataSet();
        try
        {
            ocmd.CommandType = CommandType.Text;
            ocmd.CommandText = Qry;
            OdbcDataAdapter ConDa = new OdbcDataAdapter(ocmd);
            ConDa.Fill(ConDs, tablename);
            return ConDs;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ConDs = null;              
        }
        
    }

    public string CalculateManHours(string invoiceno)
    {

        Report CalObj = new Report();
        DataSet CalDs = new DataSet();
        try
        {
           // oconn = CalObj.DBLiveOpen();
            ocmd.Connection = oconn;
            string calqry = "select SUM(CAST(((Cast(LendDate AS Time)) - (Cast(LeDate AS Time)))/60 AS INTEGER)) as ManHours" +
                            " from loggedevents_dp" + " where Lenddate is not null and IsTimeSheet = 'Y' and" +
                            " Ano IN (select ANo from Article_dp where ino =" + invoiceno + ")";

            CalDs = ExcueQuery(calqry, "CalculateManHours");
            if (CalDs.Tables[0].Rows.Count > 0)
            {
                if (CalDs.Tables[0].Rows[0][0].ToString() != "")
                    return CalDs.Tables[0].Rows[0][0].ToString();
                else
                    return "435";
            }
            else
                return "435";

        }
        catch (Exception exc)
        {
            throw exc;
        }
        finally
        {
           // CalObj.DBClose(oconn);  
            CalObj = null;
            CalDs = null;
        }

    }

    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        //export to excel
        try
        {
            isExcel = true;
            Response.Buffer = true;
            Response.Clear();  
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            ClearControls(JobCostDetails);
            System.Web.UI.HtmlControls.HtmlForm oExcelform = new System.Web.UI.HtmlControls.HtmlForm();
            Controls.Add(oExcelform);
            oExcelform.Controls.Add(JobCostDetails);
            oExcelform.RenderControl(oHtmlTextWriter); 
            Response.Write(oStringWriter.ToString());
            Response.End();
        }
        catch (Exception oex)
        {
            //throw oex;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void ClearControls(Control control)
    {
        for (int i = control.Controls.Count - 1; i >= 0; i--)
        {
            ClearControls(control.Controls[i]);
        }

        if (!(control is TableCell))
        {
            if (control.GetType().GetProperty("SelectedItem") != null)
            {
                LiteralControl literal = new LiteralControl();
                control.Parent.Controls.Add(literal);
                try
                {
                    literal.Text =
                        (string)control.GetType().GetProperty("SelectedItem").
                            GetValue(control, null);
                }
                catch
                { }
                control.Parent.Controls.Remove(control);
            }
            else if (control.GetType().GetProperty("Text") != null)
            {
                LiteralControl literal = new LiteralControl();
                control.Parent.Controls.Add(literal);
                literal.Text =
                    (string)control.GetType().GetProperty("Text").
                        GetValue(control, null);
                control.Parent.Controls.Remove(control);
            }
        }
        return;
    }


}
