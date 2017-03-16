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

public partial class ProductionStatusReport : System.Web.UI.Page
{
    datasourceSQL oSQL = new datasourceSQL();
    DataSet rds = new DataSet();
    DataSet oDs = new DataSet();
    DataSet dst_stage = new DataSet();
    DataTable oTbl = new DataTable();
    DataTable oTable = new DataTable();
    DataTable dt_temp = new DataTable();
    string emp_id = "";
    string emp_team_id = "";
    string Today = "";

    string nextday1 = "";
    string nextday2 = "";

    static int CE_count = 0;
    static int PRE_count = 0;
    static int Tagging_count = 0;
    static int Pagination_count = 0;
    static int QC_count = 0;
    static int admin_count = 0;

    string isToday = System.DateTime.Now.DayOfWeek.ToString();  

    protected void Page_Init(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo newCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        newCulture.DateTimeFormat.DateSeparator = "/";
        System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        else
        {
            emp_id = Session["employeeid"].ToString();
            emp_team_id = Session["employeeteamid"].ToString();

            if (!Page.IsPostBack)
            {

                string[,] oParams = new string[,]{
                {"@jobid", "0" },
                {"@jobtypeid", "0"},
                {"@employeeid",emp_id},
                {"@employeeteamid",emp_team_id}
                

                   
            };
                oDs = oSQL.ExcProcedure("spGet_ProductionStatusReport", oParams, CommandType.StoredProcedure);


                // rds.Tables.Add(new DataTable() );




                //Convert.ToDateTime(originateDate).ToString("dd/MMM/yyyy HH:mm:ss"); 
                if (isToday != "Saturday" || isToday != "Sunday")
                {
                    Today = DateTime.Now.ToString("dd/MM/yyyy").Trim();

                    //string Today1 = String.Format("{0:d/M/yyyy}", Today);

                    //if (isToday == "Thursday")
                    //{
                    //    nextday1 = DateTime.Today.AddDays(1).Date.ToString("dd/MM/yyyy").Trim();
                    //}
                    if (isToday == "Friday")
                    {
                        nextday1 = DateTime.Today.AddDays(3).Date.ToString("dd/MM/yyyy").Trim();
                    }
                        
                   //else if (isToday == "Thursday")
                   // {
                   //     nextday1 = DateTime.Today.AddDays(4).Date.ToString("dd/MM/yyyy").Trim();
                   // }
                    else

                        nextday1 = DateTime.Today.AddDays(1).Date.ToString("dd/MM/yyyy").Trim();

                    if (isToday == "Friday")
                    {
                        nextday2 = DateTime.Today.AddDays(4).Date.ToString("dd/MM/yyyy").Trim();
                    }
                    else
                        nextday2 = DateTime.Today.AddDays(2).Date.ToString("dd/MM/yyyy").Trim();
                }
                Session["LiveDS"] = oDs.Tables[0];

                string sFilterText = "";
                dst_stage = oSQL.GetStages();
                //  DataSet dst_team = oSQL.GetTeamdept();




                //Session["viewTable"] = oDs.Tables[0];
                FilterReport();
                //ProductionStatusReport_Report.Caption = "<b>" + divTitle.InnerText + " For -  " + Today + "</b>";
                Label1.Text = "<b>" + divTitle.InnerText + " For -  " + Today + "</b>";
                ProductionStatusReport_Report.DataSource = oTable;
                ProductionStatusReport_Report.DataBind();
                oTable = null;
                dt_temp = null;

                filterReport1();
                //ProductionStatusReport_Report1.Caption = "<b>" + divTitle.InnerText + " For -  " + nextday1 + "</b>";
                Label2.Text = "<b>" + divTitle.InnerText + " For -  " + nextday1 + "</b>";
                ProductionStatusReport_Report1.DataSource = oTable;
                ProductionStatusReport_Report1.DataBind();

                oTable = null;
                dt_temp = null;
                filterReport2();
                //ProductionStatusReport_Report2.Caption = "<b>" + divTitle.InnerText + " For -  " + nextday2 + "</b>";
                Label3.Text = "<b>" + divTitle.InnerText + " For -  " + nextday2 + "</b>";
                ProductionStatusReport_Report2.DataSource = oTable;
                ProductionStatusReport_Report2.DataBind();
                oTable = null;
                dt_temp = null;
                //ProductionStatusReport_Report.DataSource = oDs.Tables[0];
                //ProductionStatusReport_Report.DataBind();
                //iRowID = 1;
            }


        }
    }

    public void create_DTable()
    {
        oTable = new DataTable("TABLE1");
        oTable.Columns.Add(new DataColumn("STAGE").ToString());
        oTable.Columns.Add(new DataColumn("CE/QA").ToString());
        oTable.Columns.Add(new DataColumn("PRE").ToString());
        oTable.Columns.Add(new DataColumn("TAGGING").ToString());
        oTable.Columns.Add(new DataColumn("3B2").ToString());
        oTable.Columns.Add(new DataColumn("QC").ToString());
        oTable.Columns.Add(new DataColumn("ADMIN").ToString());
        oTable.Columns.Add(new DataColumn("TOTAL").ToString());

    }

    private DataView dtview()
    {
        DataView dtv = new DataView(dt_temp);
        return dtv;
    }

    protected void FilterReport()
    {
        //int CE_count_Proof = 0;
        //int CE_count_Proof = 0;
        //int CE_count_Proof = 0;
        //int CE_count_Proof = 0;
        //int CE_count_Proof = 0;


        string sFilterText = "";
        oTbl = (DataTable)(Session["LiveDS"]);

        DataView dv = new DataView(oTbl);
        dv.RowFilter = "";
        sFilterText = "CATS_DUE_DATE='" + Today + "'";
        //sFilterText = "STAGE_ID='1'";
        dv.RowFilter = sFilterText;


        dt_temp = dv.Table.Clone();
        //dt_temp.AcceptChanges();

        foreach (DataRowView dtv in dv)
        {
            dt_temp.ImportRow(dtv.Row);

        }
        dt_temp.AcceptChanges();

        create_DTable();

        for (int dscnt = 0; dscnt < dst_stage.Tables[0].Rows.Count; dscnt++)
        {


            string filter = "";
            DataView DV = new DataView();

            DV = dtview();

            DV.RowFilter = "";
            //filter = "DEPT_ID='89' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='89'";

            DV.RowFilter = filter;
            CE_count = DV.Count;
            DV = null;

            DV = dtview();

            DV.RowFilter = "";
            //filter = "DEPT_ID='89' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='73'";

            DV.RowFilter = filter;
            PRE_count = DV.Count;
            DV = null;
    
            //DV = oTableLive.DefaultView;    
            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='88' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='82'";
            DV.RowFilter = filter;
            Tagging_count = DV.Count;
            DV = null;

            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='81' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='81'";
            DV.RowFilter = filter;
            Pagination_count = DV.Count;
            DV = null;

            //DV = oTableLive.DefaultView;          
            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='85' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='85'";
            DV.RowFilter = filter;
            QC_count = DV.Count;

            //DV = oTableLive.DefaultView;          
            DV = dtview();
            DV.RowFilter = "";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPARTMENT='NULL'";
            //JOB_TYPE='Articles' AND JOB_STAGE='Proof'     
            DV.RowFilter = filter;
            admin_count = DV.Count;

			DV = dtview();
            DV.RowFilter = "";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPARTMENT='50'";
            //JOB_TYPE='Articles' AND JOB_STAGE='Proof'     
            DV.RowFilter = filter;
            admin_count = DV.Count;


            DataRow drow = oTable.NewRow();
            drow["STAGE"] = dst_stage.Tables[0].Rows[dscnt]["JOB_STAGE_NAME_ALIAS"].ToString().ToUpper();
            drow["CE/QA"] = CE_count.ToString();
            drow["PRE"] = PRE_count.ToString();
            drow["TAGGING"] = Tagging_count.ToString();
            drow["3B2"] = Pagination_count.ToString();
            drow["QC"] = QC_count.ToString();
            drow["ADMIN"] = admin_count.ToString();

            int Total = CE_count + Tagging_count + Pagination_count + QC_count + admin_count;
            drow["TOTAL"] = Total.ToString();
            oTable.Rows.Add(drow);

            CE_count = 0;
            PRE_count = 0;
            Tagging_count = 0;
            Pagination_count = 0;
            QC_count = 0;
            admin_count = 0;

        }

    }
    private void filterReport1()
    {

        string sFilterText = "";
        oTbl = (DataTable)(Session["LiveDS"]);
        DataView dv = new DataView(oTbl);
        dv.RowFilter = "";
        sFilterText = "CATS_DUE_DATE='" + nextday1 + "'";
        //sFilterText = "STAGE_ID='1'";
        dv.RowFilter = sFilterText;
        //DataTable dt_temp = new DataTable();

        dt_temp = dv.Table.Clone();
        //dt_temp.AcceptChanges();

        foreach (DataRowView dtv in dv)
        {
            dt_temp.ImportRow(dtv.Row);

        }
        dt_temp.AcceptChanges();

        create_DTable();

        for (int dscnt = 0; dscnt < dst_stage.Tables[0].Rows.Count; dscnt++)
        {

            string filter = "";
            DataView DV = new DataView();
            DV = dtview();

            DV.RowFilter = "";
            //filter = "DEPT_ID='89' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='89'";
            DV.RowFilter = filter;
            CE_count = DV.Count;
            DV = null;

            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='89' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='73'";
            DV.RowFilter = filter;
            PRE_count = DV.Count;
            DV = null;


            //DV = oTableLive.DefaultView;    
            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='88' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='82'";
            DV.RowFilter = filter;
            Tagging_count = DV.Count;
            DV = null;

            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='81' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='81'";
            DV.RowFilter = filter;
            Pagination_count = DV.Count;
            DV = null;

            //DV = oTableLive.DefaultView;          
            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='85' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='85'";
            DV.RowFilter = filter;
            QC_count = DV.Count;

            //DV = oTableLive.DefaultView;          
            DV = dtview();
            DV.RowFilter = "";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPARTMENT='NULL'";
            //JOB_TYPE='Articles' AND JOB_STAGE='Proof'     
            DV.RowFilter = filter;
            admin_count = DV.Count;



            DataRow drow = oTable.NewRow();
            drow["STAGE"] = dst_stage.Tables[0].Rows[dscnt]["JOB_STAGE_NAME_ALIAS"].ToString().ToUpper();
            drow["CE/QA"] = CE_count.ToString();
            drow["PRE"] = PRE_count.ToString();
            drow["TAGGING"] = Tagging_count.ToString();
            drow["3B2"] = Pagination_count.ToString();
            drow["QC"] = QC_count.ToString();
            drow["ADMIN"] = admin_count.ToString();

            int Total = CE_count + Tagging_count + Pagination_count + QC_count + admin_count;
            drow["TOTAL"] = Total.ToString();
            oTable.Rows.Add(drow);

            CE_count = 0;
            PRE_count = 0;
            Tagging_count = 0;
            Pagination_count = 0;
            QC_count = 0;
            admin_count = 0;
        }
    }
    private void filterReport2()
    {

        string sFilterText = "";
        oTbl = (DataTable)(Session["LiveDS"]);
        DataView dv = new DataView(oTbl);
        dv.RowFilter = "";
        sFilterText = "CATS_DUE_DATE='" + nextday2 + "'";
        //sFilterText = "STAGE_ID='1'";
        dv.RowFilter = sFilterText;
        //DataTable dt_temp = new DataTable();

        dt_temp = dv.Table.Clone();
        //dt_temp.AcceptChanges();

        foreach (DataRowView dtv in dv)
        {
            dt_temp.ImportRow(dtv.Row);

        }
        dt_temp.AcceptChanges();
        create_DTable();
        for (int dscnt = 0; dscnt < dst_stage.Tables[0].Rows.Count; dscnt++)
        {

            string filter = "";
            DataView DV = new DataView();
            DV = dtview();

            DV.RowFilter = "";
            //filter = "DEPT_ID='89' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='89'";
            DV.RowFilter = filter;
            CE_count = DV.Count;
            DV = null;

            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='89' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='73'";
            DV.RowFilter = filter;
            PRE_count = DV.Count;
            DV = null;

            //DV = oTableLive.DefaultView;    
            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='88' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='82'";
            DV.RowFilter = filter;
            Tagging_count = DV.Count;
            DV = null;

            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='81' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='81'";
            DV.RowFilter = filter;
            Pagination_count = DV.Count;
            DV = null;

            //DV = oTableLive.DefaultView;          
            DV = dtview();
            DV.RowFilter = "";
            //filter = "DEPT_ID='85' AND " + "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPT_ID='85'";
            DV.RowFilter = filter;
            QC_count = DV.Count;

            //DV = oTableLive.DefaultView;          
            DV = dtview();
            DV.RowFilter = "";
            filter = "STAGE_ID='" + dst_stage.Tables[0].Rows[dscnt]["job_stage_id"].ToString() + "'AND " + "DEPARTMENT='NULL'";
            //JOB_TYPE='Articles' AND JOB_STAGE='Proof'     
            DV.RowFilter = filter;
            admin_count = DV.Count;



            DataRow drow = oTable.NewRow();
            drow["STAGE"] = dst_stage.Tables[0].Rows[dscnt]["JOB_STAGE_NAME_ALIAS"].ToString().ToUpper();
            drow["CE/QA"] = CE_count.ToString();
            drow["PRE"] = PRE_count.ToString();
            drow["TAGGING"] = Tagging_count.ToString();
            drow["3B2"] = Pagination_count.ToString();
            drow["QC"] = QC_count.ToString();
            drow["ADMIN"] = admin_count.ToString();

            int Total = CE_count + Tagging_count + Pagination_count + QC_count + admin_count;
            drow["TOTAL"] = Total.ToString();
            oTable.Rows.Add(drow);

            CE_count = 0;
            PRE_count = 0;
            Tagging_count = 0;
            Pagination_count = 0;
            QC_count = 0;
            admin_count = 0;
        }
        //for(int deptcnt = 0; deptcnt>=12; deptcnt++)
        //{

        //    for (int dscnt = 0; dscnt < oTable.Rows.Count; dscnt++)
        //    {


        //        if (dst_filter.Tables[0].Rows[i]["STAGE_ID"].ToString() == "1" && dst_filter.Tables[0].Rows[i]["DEPT_ID"].ToString() == "89")
        //        {
        //            CE_count += 1;
        //        }
        //        else if (dst_filter.Tables[0].Rows[i]["STAGE_ID"].ToString() == "1" && dst_filter.Tables[0].Rows[i]["DEPT_ID"].ToString() == "88")
        //        {
        //            Tagging_count += 1;
        //        }
        //        else if (dst_filter.Tables[0].Rows[i]["STAGE_ID"].ToString() == "1" && dst_filter.Tables[0].Rows[i]["DEPT_ID"].ToString() == "81")
        //        {
        //            Pagination_count += 1;
        //        }

        //        else if (dst_filter.Tables[0].Rows[i]["STAGE_ID"].ToString() == "1" && dst_filter.Tables[0].Rows[i]["DEPT_ID"].ToString() == "85")
        //        {
        //            QC_count += 1;
        //        }

        //        else if (dst_filter.Tables[0].Rows[i]["STAGE_ID"].ToString() == "1" && dst_filter.Tables[0].Rows[i]["DEPT_ID"].ToString() == " ")
        //        {
        //            admin_count += 1;
        //        }



        //        rds.Tables[0].Rows[dscnt]["CE/QE"] = CE_count.ToString();
        //        rds.Tables[0].Rows[dscnt]["PAGINATION"] = Tagging_count.ToString();
        //        rds.Tables[0].Rows[dscnt]["3B2"] = Pagination_count.ToString();
        //        rds.Tables[0].Rows[dscnt]["QC"] = QC_count.ToString();
        //        rds.Tables[0].Rows[dscnt]["ADMIN"] = admin_count.ToString();
        //        int Total = CE_count + Tagging_count + Pagination_count + QC_count + admin_count;
        //        rds.Tables[0].Rows[dscnt]["TOTAL"] = Total.ToString();
        //        CE_count = 0;
        //        Tagging_count = 0;
        //        Pagination_count = 0;
        //        QC_count = 0;
        //        admin_count = 0;
        //    }

        //}
    }






    private DataView GetViewTable()
    {
        string sFilterText = "";
        DataTable Table1 = (DataTable)(Session["LiveDS"]);
        DataView oview1 = Table1.DefaultView;
        oview1.RowFilter = "";
        sFilterText += "CATS_DUE_DATE='" + Today + "'";
        oview1.RowFilter = sFilterText;
        return oview1;

    }

    private DataView GetViewTable1()
    {
        string sFilterText = "";
        DataTable Table2 = (DataTable)(Session["LiveDS"]);
        DataView oview2 = Table2.DefaultView;
        oview2.RowFilter = "";
        sFilterText += "CATS_DUE_DATE='" + nextday1 + "'";
        oview2.RowFilter = sFilterText;
        return oview2;

    }

    private DataView GetViewTable2()
    {
        string sFilterText = "";
        DataTable Table3 = (DataTable)(Session["LiveDS"]);
        DataView oview3 = Table3.DefaultView;
        oview3.RowFilter = "";
        sFilterText += "CATS_DUE_DATE='" + nextday2 + "'";
        oview3.RowFilter = sFilterText;
        return oview3;

    }

    protected void ibtnExcel_Export_Click(object sender, ImageClickEventArgs e)
    {

        //Response.Buffer = true;
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.xls";
        //Response.AddHeader("content-disposition", "attachment;filename=" + "RFT" + ".xls");
        //this.EnableViewState = false;
        //System.IO.StringWriter strwriter = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter txtwriter = new HtmlTextWriter(strwriter);
        //HtmlForm htmlfrm = new HtmlForm();
        //RFT_Report.Parent.Controls.Add(htmlfrm);
        //htmlfrm.Attributes["runat"] = "server";
        ////jobreceived.Controls.Remove((DropDownList)jobreceived.HeaderRow.FindControl("dd_jobstage"));

        //htmlfrm.Controls.Add(RFT_Report);
        //htmlfrm.RenderControl(txtwriter);
        //Response.Write(strwriter);
        //Response.End();
    }

    public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
    {
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }

    protected void ibtnRefrsh_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ProductionStatusReport.aspx");

    }
}
