using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using System.Data.SqlClient;

public partial class WeeklyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private bool validatescreen()
    {
        return true;
    }
    protected void btnShowReport_Click(object sender, System.EventArgs e)
    {
        try
        {
            if ((TextBox1.Text.Trim() != "") && (TextBox2.Text.Trim() != ""))
            {
                TextBox1.Text = DateTime.Parse(TextBox1.Text.Trim()).ToString("MM/dd/yyyy");
                TextBox2.Text = DateTime.Parse(TextBox2.Text.Trim()).ToString("MM/dd/yyyy");
                if (chkLiveData.Checked == true)
                {
                    if (validatescreen())
                    {
                        WeekService oWS = new WeekService();
                        DateTime StartofWeek = DateTime.Parse(TextBox1.Text.Trim());
                        DateTime EndofWeek = DateTime.Parse(TextBox2.Text.Trim());
                        //	to be uncommented when live
                        string strData = oWS.GetTotalNos(StartofWeek, EndofWeek);
                        //	string strData = "1,2,3,4,5,6,7,8,9,10,11,12";
                        string[] arrData = strData.Split(',');
                        DataTable myTable = new DataTable("myTable");
                        //	Response.Write(arrData.Length.ToString());
                        if (arrData.Length >= 14)
                        {
                            //Response.Write(arrData.Length.ToString()+" "+arrData[0]+"^"+arrData[1]+"^"+arrData[2]+"^"+arrData[3]+"^"+arrData[4]+"^"+arrData[5]+"^"+arrData[6]+"^"+arrData[7]+"^"+arrData[8]+"^"+arrData[9]+"^");
                            DataColumn colItem1 = new DataColumn("WeekNo", Type.GetType("System.String"));
                            DataColumn colItem2 = new DataColumn("ArticlesRecd", Type.GetType("System.String"));
                            DataColumn colItem3 = new DataColumn("ArticlesDesp", Type.GetType("System.String"));
                            DataColumn colItem4 = new DataColumn("ArticlesInHand", Type.GetType("System.String"));
                            DataColumn colItem5 = new DataColumn("IssuesRecd", Type.GetType("System.String"));
                            DataColumn colItem6 = new DataColumn("IssuesDesp", Type.GetType("System.String"));
                            DataColumn colItem7 = new DataColumn("FinalIssueDesp", Type.GetType("System.String"));
                            DataColumn colItem8 = new DataColumn("FinalIssuePageDesp", Type.GetType("System.String"));
                            DataColumn colItem9 = new DataColumn("IssuesInHand", Type.GetType("System.String"));
                            DataColumn colItem10 = new DataColumn("BooksRecd", Type.GetType("System.String"));
                            DataColumn colItem11 = new DataColumn("BooksDesp", Type.GetType("System.String"));
                            DataColumn colItem12 = new DataColumn("BookFinalDesp", Type.GetType("System.String"));
                            DataColumn colItem13 = new DataColumn("BookFinalPageDesp", Type.GetType("System.String"));
                            DataColumn colItem14 = new DataColumn("BooksInHand", Type.GetType("System.String"));
                            DataColumn colItem15 = new DataColumn("ProjectsRecd", Type.GetType("System.String"));
                            DataColumn colItem16 = new DataColumn("ProjectsDesp", Type.GetType("System.String"));
                            DataColumn colItem17 = new DataColumn("ProjectsInHand", Type.GetType("System.String"));

                            myTable.Columns.Add(colItem1);
                            myTable.Columns.Add(colItem2);
                            myTable.Columns.Add(colItem3);
                            myTable.Columns.Add(colItem4);
                            myTable.Columns.Add(colItem5);
                            myTable.Columns.Add(colItem6);
                            myTable.Columns.Add(colItem7);
                            myTable.Columns.Add(colItem8);
                            myTable.Columns.Add(colItem9);
                            myTable.Columns.Add(colItem10);
                            myTable.Columns.Add(colItem11);
                            myTable.Columns.Add(colItem12);
                            myTable.Columns.Add(colItem13);
                            myTable.Columns.Add(colItem14);
                            myTable.Columns.Add(colItem15);
                            myTable.Columns.Add(colItem16);
                            myTable.Columns.Add(colItem17);

                            DataRow NewRow = myTable.NewRow();

                            NewRow["WeekNo"] = DateTime.Parse(TextBox1.Text.Trim()).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(TextBox2.Text.Trim()).ToString("dd/MM/yyyy");
                            NewRow["ArticlesRecd"] = arrData[0];
                            NewRow["ArticlesDesp"] = arrData[1];
                            NewRow["ArticlesInHand"] = arrData[2];
                            NewRow["IssuesRecd"] = arrData[3];
                            NewRow["IssuesDesp"] = arrData[4];
                            NewRow["FinalIssueDesp"] = arrData[5];
                            NewRow["FinalIssuePageDesp"] = arrData[6];
                            NewRow["IssuesInHand"] = arrData[7];
                            NewRow["BooksRecd"] = arrData[8];
                            NewRow["BooksDesp"] = arrData[9];
                            NewRow["BookFinalDesp"] = arrData[10];
                            NewRow["BookFinalPageDesp"] = arrData[11];
                            NewRow["BooksInHand"] = arrData[12];
                            NewRow["ProjectsRecd"] = arrData[13];
                            NewRow["ProjectsDesp"] = arrData[14];
                            NewRow["ProjectsInHand"] = arrData[15];

                            myTable.Rows.Add(NewRow);
                        }
                        DataGrid1.DataSource = myTable;
                        DataGrid1.DataBind();
                    }
                }
                else
                {
                    if (DateTime.Compare(Convert.ToDateTime(TextBox2.Text.ToString().Trim()), Convert.ToDateTime(TextBox1.Text.ToString().Trim())) >= 0)
                    {
                        string sXMLFilePath = "";
                        if (Radio5.Checked == true)
                            sXMLFilePath = "C:\\temp1\\dr.xml";
                        else if (Radio6.Checked == true)
                            sXMLFilePath = "C:\\temp1\\wr.xml";
                        string sCollectionText = "";
                        DateTime sFromDate = Convert.ToDateTime("1/1/1");
                        DateTime sToDate = Convert.ToDateTime("1/1/1");
                        DataTable myTable = new DataTable("myTable");


                        DataColumn colItem1 = new DataColumn("WeekNo", Type.GetType("System.String"));
                        DataColumn colItem2 = new DataColumn("ArticlesRecd", Type.GetType("System.String"));
                        DataColumn colItem3 = new DataColumn("ArticlesDesp", Type.GetType("System.String"));
                        DataColumn colItem4 = new DataColumn("ArticlesInHand", Type.GetType("System.String"));
                        DataColumn colItem5 = new DataColumn("IssuesRecd", Type.GetType("System.String"));
                        DataColumn colItem6 = new DataColumn("IssuesDesp", Type.GetType("System.String"));
                        DataColumn colItem7 = new DataColumn("FinalIssueDesp", Type.GetType("System.String"));
                        DataColumn colItem8 = new DataColumn("FinalIssuePageDesp", Type.GetType("System.String"));
                        DataColumn colItem9 = new DataColumn("IssuesInHand", Type.GetType("System.String"));
                        DataColumn colItem10 = new DataColumn("BooksRecd", Type.GetType("System.String"));
                        DataColumn colItem11 = new DataColumn("BooksDesp", Type.GetType("System.String"));
                        DataColumn colItem12 = new DataColumn("BookFinalDesp", Type.GetType("System.String"));
                        DataColumn colItem13 = new DataColumn("BookFinalPageDesp", Type.GetType("System.String"));
                        DataColumn colItem14 = new DataColumn("BooksInHand", Type.GetType("System.String"));
                        DataColumn colItem15 = new DataColumn("ProjectsRecd", Type.GetType("System.String"));
                        DataColumn colItem16 = new DataColumn("ProjectsDesp", Type.GetType("System.String"));
                        DataColumn colItem17 = new DataColumn("ProjectsInHand", Type.GetType("System.String"));

                        myTable.Columns.Add(colItem1);
                        myTable.Columns.Add(colItem2);
                        myTable.Columns.Add(colItem3);
                        myTable.Columns.Add(colItem4);
                        myTable.Columns.Add(colItem5);
                        myTable.Columns.Add(colItem6);
                        myTable.Columns.Add(colItem7);
                        myTable.Columns.Add(colItem8);
                        myTable.Columns.Add(colItem9);
                        myTable.Columns.Add(colItem10);
                        myTable.Columns.Add(colItem11);
                        myTable.Columns.Add(colItem12);
                        myTable.Columns.Add(colItem13);
                        myTable.Columns.Add(colItem14);
                        myTable.Columns.Add(colItem15);
                        myTable.Columns.Add(colItem16);
                        myTable.Columns.Add(colItem17);
                        DataRow NewRow;

                        try
                        {
                            XmlDocument XmlDoc = new XmlDocument();
                            XmlDoc.Load(sXMLFilePath);
                            XmlNodeList MyNodeList;
                            XmlNode root = XmlDoc.DocumentElement;

                            MyNodeList = root.SelectNodes("//*");

                            foreach (XmlNode MyNode in MyNodeList)
                            {

                                if (MyNode.Name == "DATE")
                                {
                                    foreach (XmlNode sNodeAttr in MyNode.Attributes)
                                    {
                                        if (sNodeAttr.Name == "From")
                                        {
                                            sFromDate = Convert.ToDateTime(sNodeAttr.Value);
                                        }
                                        else
                                        {
                                            if (sNodeAttr.Name != "to")
                                            {
                                                sFromDate = Convert.ToDateTime("1/1/1");
                                            }
                                        }

                                        if (sNodeAttr.Name == "to")
                                        {
                                            sToDate = Convert.ToDateTime(sNodeAttr.Value);
                                        }
                                        else
                                        {
                                            if (sNodeAttr.Name != "From")
                                            {
                                                sToDate = Convert.ToDateTime("1/1/1");
                                            }
                                        }
                                        //if (sFromDate >= Convert.ToDateTime("1/1/2007"))
                                        //{
                                        //}
                                    }

                                }
                                //					if ((sFromDate >= Convert.ToDateTime("1/1/2007")) && (sToDate <= Convert.ToDateTime("5/1/2007")) && (sFromDate != Convert.ToDateTime("1/1/1")) && (sToDate != Convert.ToDateTime("1/1/1")))
                                //						if (((DateTime.Compare(sFromDate,Convert.ToDateTime("1/1/2007")) > 0) || (DateTime.Compare(sFromDate,Convert.ToDateTime("1/1/2007")) == 0)) && ((DateTime.Compare(sToDate,Convert.ToDateTime("5/10/2007")) < 0) || (DateTime.Compare(sToDate,Convert.ToDateTime("5/10/2007")) == 0)) && (DateTime.Compare(sFromDate,Convert.ToDateTime("1/1/2001")) != 0) && (DateTime.Compare(sToDate,Convert.ToDateTime("1/1/2001")) != 0))
                                ////						if (((DateTime.Compare(sFromDate,Convert.ToDateTime(TextBox1.Text.ToString())) >=0) || (DateTime.Compare(sFromDate,Convert.ToDateTime(TextBox1.Text.ToString())) == 0)) && ((DateTime.Compare(sToDate,Convert.ToDateTime(TextBox2.Text.ToString())) <= 0) || (DateTime.Compare(sToDate,Convert.ToDateTime(TextBox2.Text.ToString())) == 0)) && (DateTime.Compare(sFromDate,Convert.ToDateTime("1/1/2001")) != 0) && (DateTime.Compare(sToDate,Convert.ToDateTime("1/1/2001")) != 0))
                                if ((sFromDate >= DateTime.Parse(TextBox1.Text.ToString())) && (sToDate <= DateTime.Parse(TextBox2.Text.ToString())) && (sFromDate != DateTime.Parse("1/1/2001")) && (sToDate != DateTime.Parse("1/1/2001")))
                                {
                                    if (MyNode.Name != "DATE")
                                    {
                                        sCollectionText = sCollectionText + MyNode.InnerText;

                                        if (MyNode.Name != "BookRecd")
                                        {
                                            sCollectionText = sCollectionText + "|";
                                        }
                                        if (MyNode.Name == "ProjectsInHand")
                                        {
                                            if (sCollectionText != "")
                                            {
                                                sCollectionText = sCollectionText.TrimEnd('|');
                                                string[] sColText = sCollectionText.Split('|');
                                                NewRow = myTable.NewRow();
                                                if (sColText != null && sColText.Length == 17)
                                                {
                                                    for (int v = 1; v <= sColText.Length; v++)
                                                    {
                                                        if (v == 1)
                                                            NewRow["WeekNo"] = sColText[v - 1];
                                                        if (v == 2)
                                                            NewRow["ArticlesRecd"] = sColText[v - 1];
                                                        if (v == 3)
                                                            NewRow["ArticlesDesp"] = sColText[v - 1];
                                                        if (v == 4)
                                                            NewRow["ArticlesInHand"] = sColText[v - 1];
                                                        if (v == 5)
                                                            NewRow["IssuesRecd"] = sColText[v - 1];
                                                        if (v == 6)
                                                            NewRow["IssuesDesp"] = sColText[v - 1];
                                                        if (v == 7)
                                                            NewRow["FinalIssueDesp"] = sColText[v - 1];
                                                        if (v == 8)
                                                            NewRow["FinalIssuePageDesp"] = sColText[v - 1];
                                                        if (v == 9)
                                                            NewRow["IssuesInHand"] = sColText[v - 1];
                                                        if (v == 10)
                                                            NewRow["BooksRecd"] = sColText[v - 1];
                                                        if (v == 11)
                                                            NewRow["BooksDesp"] = sColText[v - 1];
                                                        if (v == 12)
                                                            NewRow["BookFinalDesp"] = sColText[v - 1];
                                                        if (v == 13)
                                                            NewRow["BookFinalPageDesp"] = sColText[v - 1];
                                                        if (v == 14)
                                                            NewRow["BooksInHand"] = sColText[v - 1];
                                                        if (v == 15)
                                                            NewRow["ProjectsRecd"] = sColText[v - 1];
                                                        if (v == 16)
                                                            NewRow["ProjectsDesp"] = sColText[v - 1];
                                                        if (v == 17)
                                                            NewRow["ProjectsInHand"] = sColText[v - 1];
                                                    }
                                                }

                                                myTable.Rows.Add(NewRow);
                                                sCollectionText = "";
                                            }
                                        }
                                        else if (MyNode.Name == "ProjectsDesp" && MyNode.NextSibling == null)
                                        {
                                            if (sCollectionText != "")
                                            {
                                                sCollectionText = sCollectionText.TrimEnd('|');
                                                string[] sColText = sCollectionText.Split('|');
                                                NewRow = myTable.NewRow();
                                                if (sColText != null && sColText.Length == 11)
                                                {
                                                    for (int v = 1; v <= sColText.Length; v++)
                                                    {
                                                        if (v == 1)
                                                            NewRow["WeekNo"] = sColText[v - 1];
                                                        if (v == 2)
                                                            NewRow["ArticlesRecd"] = sColText[v - 1];
                                                        if (v == 3)
                                                        {
                                                            NewRow["ArticlesDesp"] = sColText[v - 1];
                                                            NewRow["ArticlesInHand"] = "n/a";
                                                        }
                                                        if (v == 4)
                                                            NewRow["IssuesRecd"] = sColText[v - 1];
                                                        if (v == 5)
                                                            NewRow["IssuesDesp"] = sColText[v - 1];
                                                        if (v == 6)
                                                        {
                                                            NewRow["FinalIssueDesp"] = sColText[v - 1];
                                                            NewRow["FinalIssuePageDesp"] = "n/a";
                                                            NewRow["IssuesInHand"] = "n/a";
                                                        }
                                                        if (v == 7)
                                                            NewRow["BooksRecd"] = sColText[v - 1];
                                                        if (v == 8)
                                                            NewRow["BooksDesp"] = sColText[v - 1];
                                                        if (v == 9)
                                                        {
                                                            NewRow["BookFinalDesp"] = sColText[v - 1];
                                                            NewRow["BookFinalPageDesp"] = "n/a";
                                                            NewRow["BooksInHand"] = "n/a";
                                                        }
                                                        if (v == 10)
                                                            NewRow["ProjectsRecd"] = sColText[v - 1];
                                                        if (v == 11)
                                                        {
                                                            NewRow["ProjectsDesp"] = sColText[v - 1];
                                                            NewRow["ProjectsInHand"] = "n/a";
                                                        }
                                                    }
                                                }
                                                else if (sColText != null && sColText.Length == 13)
                                                {
                                                    for (int v = 1; v <= sColText.Length; v++)
                                                    {
                                                        if (v == 1)
                                                            NewRow["WeekNo"] = sColText[v - 1];
                                                        if (v == 2)
                                                            NewRow["ArticlesRecd"] = sColText[v - 1];
                                                        if (v == 3)
                                                        {
                                                            NewRow["ArticlesDesp"] = sColText[v - 1];
                                                            NewRow["ArticlesInHand"] = "n/a";
                                                        }
                                                        if (v == 4)
                                                            NewRow["IssuesRecd"] = sColText[v - 1];
                                                        if (v == 5)
                                                            NewRow["IssuesDesp"] = sColText[v - 1];
                                                        if (v == 6)
                                                            NewRow["FinalIssueDesp"] = sColText[v - 1];
                                                        if (v == 7)
                                                        {
                                                            NewRow["FinalIssuePageDesp"] = sColText[v - 1];
                                                            NewRow["IssuesInHand"] = "n/a";
                                                        }
                                                        if (v == 8)
                                                            NewRow["BooksRecd"] = sColText[v - 1];
                                                        if (v == 9)
                                                            NewRow["BooksDesp"] = sColText[v - 1];
                                                        if (v == 10)
                                                            NewRow["BookFinalDesp"] = sColText[v - 1];
                                                        if (v == 11)
                                                        {
                                                            NewRow["BookFinalPageDesp"] = sColText[v - 1];
                                                            NewRow["BooksInHand"] = "n/a";
                                                        }
                                                        if (v == 12)
                                                            NewRow["ProjectsRecd"] = sColText[v - 1];
                                                        if (v == 13)
                                                        {
                                                            NewRow["ProjectsDesp"] = sColText[v - 1];
                                                            NewRow["ProjectsInHand"] = "n/a";
                                                        }

                                                    }
                                                }
                                                myTable.Rows.Add(NewRow);
                                                sCollectionText = "";
                                            }
                                        }
                                    }
                                }

                            }
                            //					DataView dv = new DataView(myTable);
                            /*					DataGrid1.GridLines = GridLines.Both;
                                                DataGrid1.CellPadding = 1;
                                                DataGrid1.ForeColor = System.Drawing.Color.Black;
                                                DataGrid1.BackColor = System.Drawing.Color.Beige;
                                                DataGrid1.AlternatingItemStyle.BackColor = System.Drawing.Color.Gainsboro;
                                                DataGrid1.HeaderStyle.BackColor = System.Drawing.Color.Brown;
                                                DataGrid1.HeaderStyle.ForeColor = System.Drawing.Color.White;
                                                DataGrid1.ItemStyle.Font.Size = 20;
                                                DataGrid1.HeaderStyle.Font.Bold=true;
                                                DataGrid1.HeaderStyle.Wrap = true;
                                                DataGrid1.ItemStyle.Wrap=true;
                                                DataGrid1.HeaderStyle.Font.Size=12;
                                                DataGrid1.Font.Size=10;
                            */
                            DataGrid1.DataSource = myTable;
                            DataGrid1.DataBind();

                        }
                        catch (Exception ec)
                        {
                            string er = ec.Message;
                        }

                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception ext)
        {
            Response.Write("<b>" + ext.Message + "</b>");
        }
    }

    private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

    }

    protected void btnExport_Click(object sender, System.EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        DataGrid1.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();


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
                    literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                }
                catch
                {
                }
                control.Parent.Controls.Remove(control);
            }
            else
                if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
        }
        return;
    }

    private void TextBox1_TextChanged(object sender, System.EventArgs e)
    {

    }
    private class WeekService
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataAdapter adap = null;
        DataSet ds = null;
        string connString = ConfigurationManager.ConnectionStrings["conStrIBLive"].ConnectionString;
        OdbcCommand myCommand;
        public WeekService()
        {
        }


        public string Test(string TTT)
        {
            return TTT;
        }
        public DateTime GetEndOfQuarter(int Year, int Number)
        {
            if (Number == 1) // 1st Quarter = January 1 to March 31
                return new DateTime(Year, 3,
                    DateTime.DaysInMonth(Year, 3), 23, 59, 59, 999);
            else if (Number == 2) // 2nd Quarter = April 1 to June 30
                return new DateTime(Year, 6,
                    DateTime.DaysInMonth(Year, 6), 23, 59, 59, 999);
            else if (Number == 3) // 3rd Quarter = July 1 to September 30
                return new DateTime(Year, 9,
                    DateTime.DaysInMonth(Year, 9), 23, 59, 59, 999);
            else // 4th Quarter = October 1 to December 31
                return new DateTime(Year, 12,
                    DateTime.DaysInMonth(Year, 12), 23, 59, 59, 999);
        }
        public DateTime GetStartOfQuarter(int Year, int Number)
        {
            if (Number == 1) // 1st Quarter = January 1 to March 31
                return new DateTime(Year, 1, 1, 0, 0, 0, 0);
            else if (Number == 2) // 2nd Quarter = April 1 to June 30
                return new DateTime(Year, 4, 1, 0, 0, 0, 0);
            else if (Number == 3) // 3rd Quarter = July 1 to September 30
                return new DateTime(Year, 7, 1, 0, 0, 0, 0);
            else // 4th Quarter = October 1 to December 31
                return new DateTime(Year, 10, 1, 0, 0, 0, 0);
        }
        public string GetTotalNos(DateTime sStartDate, DateTime sEndDate)
        {
          			
            string stDate = sStartDate.ToString("yyyy/MM/dd") + " 00:00:00";
            string enDate = sEndDate.ToString("yyyy/MM/dd") + " 23:59:59";
            conn = new SqlConnection(connString);
            //conn.ConnectionTimeout = 10000000;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                else
                {
                    return "Connection could not open";
                }

                if (conn.State == ConnectionState.Open)
                {
                    string output = "";
                 

                    string qArticlesRecd = "select Distinct journal_dp.journame, journal_dp.jourcode, aarticlecode, amanuscriptid from article_dp" +
                        " left join employee_dp on article_dp.current_employee=employee_dp.empno" +
                        " left join journal_dp on article_dp.journo = journal_dp.journo" +
                        " left outer join issue_dp on article_dp.ino = issue_dp.ino" +
                        " left outer join journalcomplexityprices_dp on journal_dp.jcno_2009 = journalcomplexityprices_dp.jcpno" +
                        " left outer join contact_dp a on journal_dp.jprodedno=a.conno" +
                        " left outer join contact_dp b on journal_dp.jprodmanno=b.conno" +
                        " left outer join pagetrim_dp on journal_dp.pagetrimno=pagetrim_dp.pagetrimno" +
                        " inner join loggedevents_dp l on article_dp.ano = l.ano" +
                        " where ( l.ledate between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + ")" +
                        " and article_dp.anon_article = 0 and l.stypeno = 10002" +
                        " and l.sno = 19 and employee_dp.empno = 61 and evno = 10013 ";//and loggedevents_dp.dno = 10035 order by l.ledate desc";

                    output = getRowsCount(qArticlesRecd, "");

                    if (output == "")
                        output += "0";
                 

                    string qArticlesDesp = "select   journal_dp.journame, journal_dp.jourcode, aarticlecode, amanuscriptid from article_dp" +
                        " left join employee_dp on article_dp.current_employee=employee_dp.empno" +
                        " left join journal_dp on article_dp.journo =  journal_dp.journo" +
                        " left outer join issue_dp on article_dp.ino = issue_dp.ino" +
                        " left outer join journalcomplexityprices_dp on journal_dp.jcno_2009 = journalcomplexityprices_dp.jcpno" +
                        " left outer join contact_dp a on journal_dp.jprodedno=a.conno" +
                        " left outer join contact_dp b on journal_dp.jprodmanno=b.conno" +
                        " left outer join pagetrim_dp on journal_dp.pagetrimno=pagetrim_dp.pagetrimno" +
                        " inner join loggedevents_dp l on article_dp.ano = l.ano" +
                        " where l.ledate between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" +
                        " and l.stypeno = 10002 and l.dno = 50 and article_dp.anon_article = 0 and l.evno = 10054" +
                        " and l.empno = 8 order by l.ledate desc";

                    output += "," + getRowsCount(qArticlesDesp, "");

                    string qArticlesInHand = "Select ARTICLE_DP.ANO,(ARTICLE_DP.ACREATIONDATE) AS ACREATIONDATE,ARTICLE_DP.AMANUSCRIPTID,AARTICLECODE,Journal_dp.JOURCODE," +
                        " journal_dp.issam, issue_dp.iinvoiced FROM ARTICLE_DP LEFT OUTER JOIN  JOURNAL_DP ON article_dp.journo=journal_dp.journo" +
                        " LEFT OUTER JOIN  ARTICLE_PRIOR_DP ON ARTICLE_DP.ARPNO=ARTICLE_PRIOR_DP.ARPNO LEFT OUTER JOIN  ISSUE_DP ON ARTICLE_DP.INO=ISSUE_DP.INO LEFT OUTER JOIN DEPARTMENT_DP ON ARTICLE_DP.CURRENT_DEPARTMENT=DEPARTMENT_DP.DNO LEFT OUTER JOIN  EMPLOYEE_DP" +
                        " ON ARTICLE_DP.CURRENT_EMPLOYEE =EMPLOYEE_DP.EMPNO LEFT OUTER JOIN STATUS_DP ON ARTICLE_DP.STNO = STATUS_DP.STNO LEFT OUTER JOIN ArticleDocType_DP ON ARTICLE_DP.ADNO=ArticleDocType_DP.ADNO LEFT OUTER JOIN IssuePositionCoverType_DP ON ARTICLE_DP.IPCTNO = IssuePositionCoverType_DP.IPCTNO LEFT OUTER JOIN Category_DP ON ARTICLE_DP.CATNO = Category_Dp.Catno LEFT OUTER JOIN ArticleProdItemType_DP ON" +
                        " ARTICLE_DP.APINO = ArticleProdItemType_DP.APINO LEFT OUTER JOIN  ArticleProdType_DP ON ARTICLE_DP.APTNO = ArticleProdType_DP.APTNO LEFT OUTER JOIN  STYPE_DP ON ARTICLE_DP.STYPENO = STYPE_DP.STYPENO LEFT OUTER JOIN  NUMBERSYSTEM_DP" +
                        " ON ARTICLE_DP.NSNO = NUMBERSYSTEM_DP.NSNO LEFT OUTER JOIN CUSTOMER_DP Customer_dp ON (Journal_dp.CUSTNO = Customer_dp.CUSTNO) where aarticlecode like '%' and article_dp.completed_flag='N' and (article_dp.adno=3 or article_dp.adno=10012)";

                    output += "," + getRowsCount(qArticlesInHand, "");

                    //					string qIssuesRecd = "select count(*) from loggedevents_dp where (ledate between " + "'" + stDate + "'" +  " And " + "'" + enDate + "'" + ") and dno=11 and ino is not null and evno=10020 and stypeno=10069";
                    string qIssuesRecd = "select   journal_dp.jourcode, issue_dp.iissueno, issue_dp.iissueno,ISSUE_DP.ICREATIONDATE, ISSUE_DP.IDUEDATE, ISSUE_DP.IP100DUE from issue_dp" +
                        " left join article_Dp on issue_dp.ino = article_dp.ino left join journal_dp on issue_dp.journo = journal_dp.journo" +
                        " left join printer_dp on journal_dp.printno = printer_dp.printno" +
                        " left join CUSTOMER_DP ON JOURNAL_DP.CUSTNO = CUSTOMER_DP.CUSTNO left join STYPE_DP ON Issue_dp.stypeno = stype_dp.stypeno" +
                        " where issue_dp.makeup_recd_date between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + " and (issue_dp.IINVOICED ='N'or issue_dp.IINVOICED ='Y')" +
                        " and (issue_dp.COMPLETED_FLAG ='N' or issue_dp.COMPLETED_FLAG ='Y') order by issue_dp.makeup_recd_date desc";

                    output += "," + getRowsCount(qIssuesRecd, "");

                    //					string qIssuesDesp = "select count(*) from loggedevents_dp where (ledate between " + "'" + stDate + "'" +  " And " + "'" + enDate + "'" + ") and dno=50 and ino is not null and evno=10033 and empno=8 and sno=28 and stypeno=10069";
                    string qIssuesDesp = "select   journal_dp.jourcode, Issue_dp.iissueno," +
                        "  (issue_dp.iCreationDate) as iCreationDate, (issue_dp.iP100Due) as ip100Due from loggedevents_dp left outer join issue_dp on ( LoggedEvents_dp.INO=issue_dp.INO)" +
                        " left outer join Journal_dp on ( Issue_dp.journo=Journal_dp.journo)" +
                        " left outer join effect_dp on ( Issue_dp.P100NO=EFFECT_dp.ENO) where ( LoggedEvents_dp.EVNO = 10033 )" +
                        " And ( LoggedEvents_dp.stypeno = 10069 ) And ( LoggedEvents_dp.LEDate between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + ")" +
                        " order by journal_dp.JourCode, issue_dp.iissueno";

                    output += "," + getRowsCount(qIssuesDesp, "");
 
                    string qFinalIssueDesp = "select Journal_dp.finsiteno,journal_dp.jourcode, Issue_dp.iissueno, (issue_dp.iCreationDate) as iCreationDate," +
                        " (issue_dp.iDueDate) as ip100Due ,(select cast( (sum(arealnoofpages)) as integer) +(select cast( (sum(arealnoofpages)) as integer)" +
                        " from article_dp where ino=issue_dp.ino and adno in(2)) +( Select count(*) from article_dp where ino=issue_dp.ino" +
                        " and adno in (1,5)) from article_dp where   adno not in (12,1,5,2,13) and ino=issue_dp.ino) as IssuePages2" +
                        " ,(select sum(arealnoofpages) +(select cast( (sum(arealnoofpages)) as integer)  from article_dp" +
                        " where ino=issue_dp.ino and adno in(2)) +( Select count(*) from article_dp where ino=issue_dp.ino and adno in (1,5))" +
                        " from article_dp where adno not in (12,13,1,5,2) and ino=issue_dp.ino) as IssuePages1" +
                        " ,(select cast( (((max(APAGENOTO)-min(APAGENOFROM))+1)) as integer) +(select cast( (sum(arealnoofpages)) as integer)" +
                        " from article_dp where ino=issue_dp.ino and adno in(2)) +( Select count(*)" +
                        " from article_dp where ino=issue_dp.ino and adno in (1,5)) from article_dp where   adno not in (12,13,1,5,2) and ino=issue_dp.ino) as IssuePages" +
                        " from loggedevents_dp" +
                        " left outer join issue_dp on ( LoggedEvents_dp.INO=issue_dp.INO)" +
                        " left outer join Journal_dp on ( Issue_dp.journo=Journal_dp.journo)" +
                        " left outer join effect_dp on ( Issue_dp.ENO=EFFECT_dp.ENO) where ( LoggedEvents_dp.EVNO = 10022 )" +
                        " And ( LoggedEvents_dp.stypeno = 10004 )" +
                        " And ( LoggedEvents_dp.LEDate between '" + stDate + "' And '" + enDate + "')" +
                        " order by journal_dp.JourCode, issue_dp.iissueno";

                    //output += "," + getRowsCount(qFinalIssueDesp, "FinalIssueDesp");
                    output += "," + getRowsCount(qFinalIssueDesp, "FinalIssueDesp");

                    string qIssuesInHand = "Select INO,JOURCODE,IISSUENO,COMPLETED_FLAG, STYPE_DP.STYPENAME FROM ISSUE_DP" +
                        " LEFT JOIN JOURNAL_DP ON journal_dp.journo=issue_dp.journo" +
                        " LEFT JOIN CUSTOMER_DP ON JOURNAL_DP.CUSTNO=CUSTOMER_DP.CUSTNO" +
                        " LEFT JOIN Status_DP ON status_dp.stno=issue_dp.stno LEFT JOIN Stage_DP ON stage_dp.sno=issue_dp.sno" +
                        " LEFT JOIN Stype_DP ON stype_dp.stypeno=issue_dp.stypeno" +
                        " LEFT JOIN Article_prior_DP ON Article_prior_DP.arpno=issue_dp.arpno where completed_flag='N'";
                    output += "," + getRowsCount(qIssuesInHand, "");
                    //16jan09					
                    //					string qBookRecd = "select DISTINCT bnumber1, india_recd1 from SP_SELECT_BOOKS_RECDDATE( " + "'" + sStartDate.ToShortDateString() + "'" +  "," + "'" + sEndDate.ToShortDateString() + "'" + ")";
                    string qBookRecd = "select DISTINCT BNO,BNUMBER1, INDIA_RECD1, BINDIADUE1, FINSITENO1, BBOOKSTATUS1, BTITLE1," +
                        " INDIA_DISP1, STAGE1, CUSTNAME1, BNOOFPAGES1, BCNO_2009,book_dp.bacno1,book_dp.bacno2,book_dp.bacno3," +
                        " book_dp.bacno4,book_dp.bacno5, BCOST, BCOMMENTS, CUSTNO ,bsno ,bstyle ,conno ,confirstname ,consurname ," +
                        " binvoiced,btype from SP_SELECT_BOOKS_RECDDATE( " + "'" + sStartDate.ToShortDateString() + "'" + "," + "'" + sEndDate.ToShortDateString() + "'" + ")" +
                        " INNER JOIN BOOK_DP ON SP_SELECT_BOOKS_RECDDATE.BNUMBER1=BOOK_DP.BNUMBER" +
                        " left outer JOIN bookstyle_dp on book_dp.bsno = bookstyle_dp.bsno" +
                        " left outer JOIN contact_dp on book_dp.conno = contact_dp.conno" +
                        " order by BNUMBER1";
                    output += "," + getRowsCount(qBookRecd, "");

                    string qBookDesp = "select  book_dp.bnoofpages,book_dp.bnumber,book_dp.btitle,stype_dp.stypename, " +
                        " customer_dp.custname,book_dp.binvoiced,(book_dp.india_recd) as india_recd,book_dp.bdespatched,blogevents_dp.bno, " +
                        " blogevents_dp.stypeno,book_dp.custno, (book_dp.bfirstdispatch) as ledate, " +
                        " book_dp.BCNO_2009,book_dp.bacno1,book_dp.bacno2,book_dp.bacno3,book_dp.bacno4,book_dp.bacno5, book_dp.BCOST, book_dp.bsno ,bstyle ,contact_dp.conno" +
                        " confirstname ,consurname,btype from blogevents_dp left join book_dp on blogevents_dp.bno=book_dp.bno left join customer_dp on " +
                        " book_dp.custno=customer_dp.custno left join stype_dp on blogevents_dp.stypeno=stype_dp.stypeno left join financialsite_dp on " +
                        " book_dp.finsiteno=financialsite_dp.finsiteno left outer JOIN bookstyle_dp on book_dp.bsno = bookstyle_dp.bsno left outer JOIN " +
                        " contact_dp on book_dp.conno = contact_dp.conno where blogevents_dp.stypeno=10071 " +
                        " and (book_dp.bfirstdispatch between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + ") and blogevents_dp.stypeno is not null group by " +
                        " blogevents_dp.bno,book_dp.bnoofpages,book_dp.bdespatched, " +
                        " blogevents_dp.stypeno,book_dp.custno,book_dp.binvoiced,book_dp.india_recd,customer_dp.custname,stype_dp.stypename,book_dp.bnumber,financialsite_dp.FINSITENO,book_dp.btitle,book_dp.bfirstdispatch,book_dp.BCNO_2009, " +
                        " book_dp.bacno1,book_dp.bacno2,book_dp.bacno3,book_dp.bacno4,book_dp.bacno5,book_dp.BCOST,book_dp.bsno ,bstyle ,contact_dp.conno ,confirstname ,consurname,btype order by book_dp.bnumber ";
                    output += "," + getRowsCount(qBookDesp, "");


                    string qFinalBookDesp = "select LENO,LEDATE,BOOK_DP.BNO,BOOK_DP.DNO,BOOK_DP.STNO,BLOGEVENTS_DP.EVNO,BLOGEVENTS_DP.EMPNO,BOOK_DP.BNOOFPAGES from blogevents_dp left join book_dp on blogevents_dp.bno=book_dp.bno  left join customer_dp on book_dp.custno=customer_dp.custno  left join stype_dp on blogevents_dp.stypeno=stype_dp.stypeno  left join financialsite_dp on book_dp.FINSITENO=financialsite_dp.FINSITENO where blogevents_dp.empno=10357   and book_dp.bdespatched='Y' and (blogevents_dp.ledate between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + ")";
                    output += "," + getRowsCount(qFinalBookDesp, "FinalBookDesp");

                   
                    string qBookInHand = "select BNUMBER as BNUMBER1,STYPENAME as STYPENAME1, (india_recd) as INDIA_RECD1,bfirstduedate as BINDIADUE1," +
                    " STATUS_DP.STDESCRIPTION as STDESCRIPTION1,BCOMMENTS as BCOMMENTS1,BBOOKSTATUS as BBOOKSTATUS1," +
                    " BTITLE as BTITLE1,BFIRSTDISPATCH as INDIA_DISP1,STYPENAME as STAGE1,customer_dp.CUSTNAME as CUSTNAME1," +
                    " BCNO_2009, BCOST, BCOMMENTS, CUSTNO, BNOOFPAGES, bsno ,bstyle ,conno ,confirstname ,consurname,binvoiced,btype" +
                    " from book_dp left join customer_dp on book_dp.custno=customer_dp.custno" +
                    " left join status_dp on book_dp.stno = status_dp.stno" +
                    " left join stype_dp on BOOK_DP.stypeno=stype_dp.stypeno" +
                    " left outer JOIN bookstyle_dp on book_dp.bsno = bookstyle_dp.bsno" +
                    " left outer JOIN contact_dp on book_dp.conno = contact_dp.conno" +
                    " where empno<> 10357 and binvoiced<> 'Y' Order by BNUMBER";
                    output += "," + getRowsCount(qBookInHand, "");

                    string qProjectsRecd = "SELECT distinct CONTACT_DP.custno,pcode,ptitle,preceiveddate FROM PROJECTS_DP LEFT JOIN CUSTOMER_DP ON PROJECTS_DP.CUSTNO=CUSTOMER_DP.CUSTNO LEFT JOIN DIGITALPRODUCTS_DP ON PROJECTS_DP.DIGITALPRODNO=DIGITALPRODUCTS_DP.DIGITALPRODNO LEFT JOIN TYPESETPLATFORM_DP ON PROJECTS_DP.TPLATNO=TYPESETPLATFORM_DP.TPLATNO LEFT JOIN CONTACT_DP ON PROJECTS_DP.CONNO=CONTACT_DP.CONNO WHERE (PRECEIVEDDATE between " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + ")";
                    output += "," + getRowsCount(qProjectsRecd, "");


                    string qProjectsDesp = "SELECT   PROJECTS_DP.PROJECTNO, PROJECTS_DP.CUSTNO, CUSTOMER_DP.CUSTNAME, PCODE, PTITLE, PRECEIVEDDATE, PDUEDATE, PCOMPLETEDDATE, PRINTNO, PPRODEDNO, PPRODMANNO, PADDITEMS, PADDCHARGES, PINVOICED, PINVOICEDDATE," +
                        " INVNO, PDESCRIPTION, PCOMMENTS, PDIGITAL, PROJECTS_DP.DIGITALPRODNO, PROJECTS_DP.TPLATNO, PROJECTS_DP.FINSITENO, PROJECTS_DP.CONNO, STYPE_DP.STYPENAME, STYPE_DP.STYPENAME," +
                        " CONTACT_DP.CONFIRSTNAME,DIGITALPRODUCTS_DP.PRODCODE, TYPESETPLATFORM_DP.TPLATCODE, PROJECTS_DP.PFINALDESPATCHED FROM PROJECTS_DP LEFT JOIN CUSTOMER_DP ON PROJECTS_DP.CUSTNO=CUSTOMER_DP.CUSTNO" +
                        " LEFT JOIN DIGITALPRODUCTS_DP ON PROJECTS_DP.DIGITALPRODNO=DIGITALPRODUCTS_DP.DIGITALPRODNO LEFT JOIN TYPESETPLATFORM_DP ON PROJECTS_DP.TPLATNO=TYPESETPLATFORM_DP.TPLATNO" +
                        " LEFT JOIN STYPE_DP ON PROJECTS_DP.STYPENO=STYPE_DP.STYPENO" +
                        " LEFT JOIN CONTACT_DP ON PROJECTS_DP.CONNO=CONTACT_DP.CONNO" +
                        " LEFT JOIN plogevents_DP ON PROJECTS_DP.projectno=plogevents_DP.projectno" +
                        " WHERE plogevents_dp.empno= 8 and Projects_dp.pdespatched='Y' and (plogevents_dp.ledate BETWEEN " + "'" + stDate + "'" + " And " + "'" + enDate + "'" + ")";
                    output += "," + getRowsCount(qProjectsDesp, "");

                    string qProjectsInHand = " SELECT PROJECTNO, PROJECTS_DP.CUSTNO, CUSTOMER_DP.CUSTNAME, PCODE, PTITLE, PRECEIVEDDATE, PDUEDATE, PCOMPLETEDDATE, PRINTNO, " +
                                            " PPRODEDNO, PPRODMANNO, PADDITEMS, PADDCHARGES, PINVOICED, PINVOICEDDATE, INVNO, PDESCRIPTION, PCOMMENTS, PDIGITAL, " +
                                            " PROJECTS_DP.DIGITALPRODNO, PROJECTS_DP.TPLATNO, PROJECTS_DP.FINSITENO, PROJECTS_DP.DNO, STATUS_DP.STNO, PROJECTS_DP.EMPNO, PROJECTS_DP.STYPENO, PCNO_2007, PCNO_2008,PROJECTS_DP.CONNO, " +
                                            " CONTACT_DP.CONFIRSTNAME,DIGITALPRODUCTS_DP.PRODCODE, TYPESETPLATFORM_DP.TPLATCODE, DEPARTMENT_DP.DNAME, STATUS_DP.STDESCRIPTION," +
                                            " EMPLOYEE_DP.EMPNAME, STYPE_DP.STYPENAME, PROJECTS_DP.PONUMBER  FROM PROJECTS_DP LEFT JOIN CUSTOMER_DP ON " +
                                            " PROJECTS_DP.CUSTNO=CUSTOMER_DP.CUSTNO LEFT JOIN DIGITALPRODUCTS_DP ON PROJECTS_DP.DIGITALPRODNO=DIGITALPRODUCTS_DP.DIGITALPRODNO " +
                                            " LEFT JOIN TYPESETPLATFORM_DP ON PROJECTS_DP.TPLATNO=TYPESETPLATFORM_DP.TPLATNO LEFT JOIN CONTACT_DP ON " +
                                            " PROJECTS_DP.CONNO=CONTACT_DP.CONNO LEFT JOIN DEPARTMENT_DP ON PROJECTS_DP.DNO=DEPARTMENT_DP.DNO LEFT JOIN STATUS_DP ON " +
                                            " PROJECTS_DP.STNO=STATUS_DP.STNO LEFT JOIN EMPLOYEE_DP ON PROJECTS_DP.EMPNO=EMPLOYEE_DP.EMPNO LEFT JOIN STYPE_DP ON " +
                                            " PROJECTS_DP.STYPENO=STYPE_DP.STYPENO where STATUS_DP.STNO<>10012 OR STATUS_DP.STNO is null " ;
                    output += "," + getRowsCount(qProjectsInHand, "");
 
                    return output;

                }
                else
                {
                    return "Problem in connection";
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);

            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        public void WriteFile(string sWPages, DateTime sFromDate, DateTime sEndDate)
        {
            string[] sHeadings = { "WeekNo", "ArticlesRecd", "ArticlesDesp", "IssuesRecd", "IssuesDesp", "FinalIssueDesp", "BooksRecd", "BooksDesp", "BookFinalDesp", "ProjectsRecd", "ProjectsDesp" };

            try
            {
                Console.WriteLine(sWPages);
                string[] asWPages = sWPages.Split(',');
                //		string ssText;				
                Console.WriteLine(asWPages[1]);
                Console.WriteLine(asWPages.Length.ToString());
                string sFilePath = "C:\\temp1\\wr.xml";
                XmlDocument XMLDom = new XmlDocument();
                if (File.Exists("C:\\temp1\\wr.xml"))
                {
                    XMLDom.Load(sFilePath);
                }

                if (asWPages.Length.ToString() == "11")
                {


                    XmlNode root = XMLDom.DocumentElement;


                    XmlElement childNode = XMLDom.CreateElement("DATE");
                    childNode.SetAttribute("From", sFromDate.ToShortDateString());
                    childNode.SetAttribute("to", sEndDate.ToShortDateString());
                    root.AppendChild(childNode);
                    for (int j = 1; j <= asWPages.Length; j++)
                    {
                        //						XmlElement childNode1 = XMLDom.CreateElement("WEEK" + j.ToString());
                        XmlElement childNode1 = XMLDom.CreateElement(sHeadings[j - 1]);
                        childNode.AppendChild(childNode1);
                        childNode1.InnerText = asWPages[j - 1].ToString();
                    }
                    XMLDom.Save(sFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
        public int WeekNumber_Entire4DayWeekRule(DateTime date)
        {
            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool ThursdayFlag = false;
            int DayOfYear = date.DayOfYear;
            int StartWeekDayOfYear =
                (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            int EndWeekDayOfYear =
                (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;
            StartWeekDayOfYear = StartWeekDayOfYear;
            EndWeekDayOfYear = EndWeekDayOfYear;
            if (StartWeekDayOfYear == 0)
                StartWeekDayOfYear = 7;
            if (EndWeekDayOfYear == 0)
                EndWeekDayOfYear = 7;
            int DaysInFirstWeek = 8 - (StartWeekDayOfYear);
            int DaysInLastWeek = 8 - (EndWeekDayOfYear);
            if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
                ThursdayFlag = true;
            int FullWeeks = (int)Math.Ceiling((DayOfYear -
                (DaysInFirstWeek)) / 7.0);
            int WeekNumber = FullWeeks;
            if (DaysInFirstWeek >= THURSDAY)
                WeekNumber = WeekNumber + 1;
            if (WeekNumber > 52 && !ThursdayFlag)
                WeekNumber = 1;
            if (WeekNumber == 0)
                WeekNumber = WeekNumber_Entire4DayWeekRule(
                    new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
            return WeekNumber;
        }

        public string getRowsCount(string strSql, string strRepName)
        {
            SqlDataAdapter adap = null;
            SqlCommand myCommand;
            DataSet ds = null;
            try
            {
                myCommand = new SqlCommand(strSql, conn);
                ds = new DataSet();
                adap = new SqlDataAdapter(myCommand);
              
                adap.Fill(ds);
                if (strRepName == "FinalIssueDesp")
                {
                    string CountandIsspages = getIssuePagesDesp(ds);//returns FinalIssueDesp & totalpagesdesp
                    
                    return CountandIsspages;
                }
                else if (strRepName == "FinalBookDesp")
                {
                    string bookpages = getBooksPagesDesp(ds);
                    return ds.Tables[0].Rows.Count.ToString() + "," + bookpages;
                }
                else
                    return ds.Tables[0].Rows.Count.ToString();
            }
            catch
            {
                return "--";
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }

        private string getIssuePagesDesp(DataSet dsIssue)
        {
            int pages = 0;
            try
            {
                DataSet dsTemp = new DataSet();
                dsTemp = dsIssue.Copy();
                if (dsIssue.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow rowTemp in dsTemp.Tables[0].Rows)
                    {
                        string jcode = rowTemp[1].ToString();
                        string issno = rowTemp[2].ToString();
                        int cnt = 0;
                        foreach (DataRow rowIss in dsIssue.Tables[0].Rows)
                        {
                            if (jcode == rowIss[1].ToString() && issno == rowIss[2].ToString())
                            {
                                cnt++;
                            }
                            if (cnt > 1)
                            {
                                dsIssue.Tables[0].Rows.Remove(rowIss);
                                break;
                            }
                        }
                    }

                    foreach (DataRow row in dsIssue.Tables[0].Rows)
                    {
                        if (row[0].ToString().Trim() == "10048" &&
                            row[6].ToString().Trim() != "")//IssuePage1
                        {
                            pages += int.Parse(row[6].ToString());
                        }
                        else if (row[0].ToString().Trim() == "470" ||
                            row[0].ToString().Trim() == "10063" ||
                            row[0].ToString().Trim() == "472" ||
                            row[0].ToString().Trim() == "10097" &&
                            row[5].ToString().Trim() != "")//IssuePage2
                        {
                            pages += int.Parse(row[5].ToString());
                        }
                        else if (row[7].ToString().Trim() != "")//IssuePage
                        {
                            pages += int.Parse(row[7].ToString());
                        }
                        else { /*do nothing*/}
                    }
                }
                 
                return dsIssue.Tables[0].Rows.Count.ToString() + "," + pages.ToString();
            }
            catch (Exception ev)
            {
                
                return ev.Message;
            }
            finally
            {
                if (dsIssue != null) dsIssue.Dispose();
            }
        }

        private string getBooksPagesDesp(DataSet dsIssue)
        {
            int pages = 0;
            try
            {
                if (dsIssue.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsIssue.Tables[0].Rows)
                    {
                        if (row[8].ToString().Trim() != "")//BNOOFPAGES
                        {
                            pages += int.Parse(row[8].ToString());
                        }
                    }
                }
               
                return pages.ToString();
            }
            catch
            {
                return "--";
            }
            finally
            {
                if (dsIssue != null) dsIssue.Dispose();
            }
        }

    }
}
