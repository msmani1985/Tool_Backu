using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class TimeSheetReport : System.Web.UI.Page
{
    SqlConnection con = null;
    string connectionstring = "";
    string query = "";
    SqlCommand cmd = new SqlCommand();
    DataSet ds = new DataSet();
    static string sExData = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["employeeid"] == null) throw new Exception("Session Expired!");
            if (!IsPostBack)
            {
                if (Session["ts_SqlRep"].ToString() != "false"){
                    datasourceIBSQL oSql = new datasourceIBSQL();
                    SqlParameter[] param = new SqlParameter[3];
                    string sSql = "[spGetEmployeeTimesheet]";
                    param[0] = oSql.NewParameter("@employee_id", SqlDbType.VarChar, int.MaxValue, ParameterDirection.Input, Session["ts_EmpNo"].ToString());
                    param[1] = oSql.NewParameter("@start_time", SqlDbType.SmallDateTime, int.MaxValue, ParameterDirection.Input, Session["ts_StartDate"].ToString() + " 00:00:00");
                    param[2] = oSql.NewParameter("@end_time", SqlDbType.SmallDateTime, int.MaxValue, ParameterDirection.Input, Session["ts_EndDate"].ToString() + " 23:59:59");
                    ds = oSql.FillDataSet_SP(sSql, param);
                    this.genReportSql(ds);
                }
                ////////////else
                ////////////{
                ////////////    connectionstring = ConfigurationManager.ConnectionStrings["conStrIB"].ToString();
                ////////////    con = new SqlConnection(connectionstring);
                ////////////    query = "SELECT * FROM SP_TIMESHEET(" + Session["ts_EmpNo"].ToString() + ",'" + Session["ts_StartDate"].ToString() + " 00:00:00','" + Session["ts_EndDate"].ToString() + " 23:59:59')";
                ////////////    con.Open();
                ////////////    cmd.Connection = con;
                ////////////    cmd.CommandType = CommandType.Text;
                ////////////    cmd.CommandText = query;
                ////////////    OdbcDataAdapter da = new OdbcDataAdapter(query, con);
                ////////////    da.Fill(ds);
                ////////////    /*
                ////////////    ReportDocument rpt = new ReportDocument();
                ////////////    CrystalReportViewer1.Visible = true;
                ////////////    rpt.FileName = Server.MapPath("TimeSheetReport.rpt");
                ////////////    rpt.SetDataSource(ds.Tables[0]);
                ////////////    rpt.SetParameterValue("RecordCount", ds.Tables[0].Rows.Count);
                ////////////    rpt.SetParameterValue("StartDate", Convert.ToDateTime(Session["StartDate"].ToString()));
                ////////////    rpt.SetParameterValue("EndDate", Convert.ToDateTime(Session["EndDate"].ToString()));
                ////////////    rpt.SetParameterValue("SessionID", Session.SessionID.ToString() + ".xls");
                ////////////    CrystalReportViewer1.ReportSource = rpt;
                ////////////    //rpt.ExportToDisk(ExportFormatType.Excel, Server.MapPath("") + @"\" + Session.SessionID.ToString() + ".xls");  
                ////////////     * */
                ////////////    con.Close();
                ////////////    this.genReport(ds);
                ////////////}
            }
        }
        catch (Exception exp)
        {
            lblError.Visible = true;
            lblError.Text = exp.Message;
        }
        finally
        {
            if (con!=null && con.State == ConnectionState.Open) con.Dispose();
            if (cmd != null) cmd.Dispose();
            ds.Dispose();
        }
    }
    private void genReport(DataSet dst)
    {
        sExData = "";
        List<
            string
            > lstLeDate
            = new List
            <
            string
            >();
        List<
            string
            > lstJobChrg
            = new List
            <
            string
            >();
        List<
            string
            > lstNonChrg
            = new List
            <
            string
            >();
        List<
            string
            > lstOthers
            = new List
            <
            string
            >();
        ArrayList arrJobChrg = new ArrayList();
        ArrayList arrData = new ArrayList();
        Hashtable htMerge = new Hashtable();
        int id = 1;
        try{
            lblEmpName.Text = Session["ts_EmpName"].ToString();
            lblStartdate.Text = Session["ts_StartDate"].ToString();
            lblEnddate.Text = Session["ts_EndDate"].ToString();
            foreach (DataRow row in dst.Tables[0].Rows){
                string Ledate = DateTime.Parse(row["ledate"].ToString().Trim()).ToString("MM/dd/yyyy");
                string Jobid = row["column1"].ToString().Trim();
                DateTime dt1 = DateTime.Parse(row["LEDATE"].ToString().Trim());
                DateTime dt2 = DateTime.Parse(row["LENDDATE"].ToString().Trim());
                TimeSpan ts = dt2 - dt1;
                if (row["flag"].ToString().Trim() == "C"){
                    if (!lstLeDate.Contains(Ledate))
                        lstLeDate.Add(Ledate);
                    if (!lstJobChrg.Contains(Jobid))
                        lstJobChrg.Add(Jobid);
                    if (!htMerge.ContainsKey(Jobid + "|" + Ledate)){
                        htMerge.Add(Jobid + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    }
                    else{
                        TimeSpan tstemp = (TimeSpan)htMerge[Jobid + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[Jobid + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + tstemp.ToString()));
                    }

                }
                else if (row["flag"].ToString().Trim() == "N"){
                    if (!lstNonChrg.Contains(Jobid))
                        lstNonChrg.Add(Jobid);
                    //arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    if (!htMerge.ContainsKey(Jobid + "|" + Ledate)){
                        htMerge.Add(Jobid + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    }
                    else{
                        TimeSpan tstemp = (TimeSpan)htMerge[Jobid + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[Jobid + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + tstemp.ToString()));
                    }
                }
                else{
                    if (!lstOthers.Contains(Jobid))
                        lstOthers.Add(Jobid);
                    //arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    if (!htMerge.ContainsKey(Jobid + "|" + Ledate)){
                        htMerge.Add(Jobid + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    }
                    else{
                        TimeSpan tstemp = (TimeSpan)htMerge[Jobid + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[Jobid + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + tstemp.ToString()));
                    }
                }
                id++;
            }
            lstLeDate.Sort();
            lstJobChrg.Sort();
            lstNonChrg.Sort();
            lstOthers.Sort();
            int chrgcnt = lstJobChrg.Count;
            //foreach (string item in lstLeDate)
            //    Response.Write(item + "<br/>");
            //Response.Write("<br/>");
            //foreach (string item in lstJobChrg)
            //    Response.Write(item.ToString() + "<br/>");
            //Response.Write("<br/>");
            //foreach (string item in lstNonChrg)
            //    Response.Write(item.ToString() + "<br/>");
            //Response.Write("<br/>");
            string sHeader = "";
            string sRows = "";
            string sOutput = "<table border='1' cellspacing='0px' style='border-collapse:collapse'><tr style='height:40px;'><td></td><td colspan='" + chrgcnt + "' bgcolor='silver' align='center'><b>Chargeable Hours</b></td><td colspan='" + lstOthers.Count + "' bgcolor='silver' align='center'><b>Other</b></td><td colspan='" + lstNonChrg.Count + "' bgcolor='silver' align='center'><b>Non-Chargeable Hours</b></td></tr>";
            foreach (string jobid in lstJobChrg){
                sHeader += "<td bgcolor='#F0F8FF' align='center' style='writing-mode:tb-rl;'>" + jobid + "</td>";
            }
            lstJobChrg.AddRange(lstOthers);
            foreach (string jobid in lstOthers){
                sHeader += "<td bgcolor='#EEF3E2' align='center' style='writing-mode:tb-rl;'>" + jobid + "</td>";
            }
            lstJobChrg.AddRange(lstNonChrg);
            foreach (string jobid in lstNonChrg){
                sHeader += "<td bgcolor='#FFFFF2' align='center' style='writing-mode:tb-rl;'>" + jobid + "</td>";
            }
            TimeSpan tsTot = new TimeSpan();
            foreach (string ledate in lstLeDate){
                sRows += "<tr><td bgcolor='#F0F8FF'>" + ledate + "</td>";
                string[] aValues = new string[lstJobChrg.Count];
                TimeSpan tsRTot = new TimeSpan();
                foreach (ListItem item in arrJobChrg){
                    string val = "";
                    string[] aItemval = item.Value.Split('|');
                    if (ledate.Trim() == item.Text){
                        val = lstJobChrg.IndexOf(aItemval[0]).ToString();
                        //aValues[int.Parse(val)] = "<td align='center'>" + aItemval[1] + "</td>";
                        if (aValues[int.Parse(val)] == null)
                            //aValues[int.Parse(val)] = TimeSpan.Parse(aItemval[1]).Hours.ToString() + ":" + TimeSpan.Parse(aItemval[1]).Minutes.ToString() + ":" + TimeSpan.Parse(aItemval[1]).Seconds.ToString();
                            aValues[int.Parse(val)] = aItemval[1];
                        else{
                            TimeSpan tssum = TimeSpan.Parse(aValues[int.Parse(val)]);
                            aValues[int.Parse(val)] = tssum.Add(TimeSpan.Parse(aItemval[1])).ToString();
                        }
                        //object ots = aItemval[1];
                        //tsTot = tsTot.Add(new TimeSpan(TimeSpan.Parse(aItemval[1]).Hours, TimeSpan.Parse(aItemval[1]).Minutes, TimeSpan.Parse(aItemval[1]).Seconds));
                        tsRTot = tsRTot.Add(TimeSpan.Parse(aItemval[1]));
                    }
                }
                string sCurrRow = "";
                foreach (string str in aValues){
                    if (str == null) sCurrRow += "<td align='center'>&nbsp;</td>";
                    else sCurrRow += "<td align='center'>" + FormatTimeSpan(TimeSpan.Parse(str)) + "</td>";
                }
                //sRows += sCurrRow + "<td>" + tsRTot.ToString() + "</td></tr>";
                sRows += sCurrRow + "<td align='center'>" + FormatTimeSpan(tsRTot) + "</td></tr>";
                tsTot = tsTot.Add(tsRTot);
            }
            sHeader = "<tr><td></td>" + sHeader + "<td><b>Total</b></td></tr>";
            sHeader = sHeader + sRows;
            //sOutput += sHeader + "<tr valign='top'><td></td><td colspan='" + (chrgcnt + lstOthers.Count + lstNonChrg.Count) + "'></td><td>" + tsTot.Hours.ToString("hh") + ":" + tsTot.Minutes.ToString("mm") + ":" + tsTot.Seconds.ToString("ss") + "</td></tr></table>";
            sOutput += sHeader + "<tr valign='top'><td></td><td colspan='" + (chrgcnt + lstOthers.Count + lstNonChrg.Count) + "'></td><td style='border-top:2px solid black' align='center'><b>" + FormatTimeSpan(tsTot) + "</b></td></tr></table>";
            //Response.Write(sOutput);
            divRep.InnerHtml = sOutput;
            sExData = "<table><tr><td align='right'>Employee Name:</td><td align='left'><b>" + Session["ts_EmpName"].ToString() + "</b></td></tr>" +
                    "<tr><td align='right'>Start Date:</td><td align='left'><b>" + Session["ts_StartDate"].ToString() + "</b></td></tr>" +
                    "<tr><td align='right'>End Date:</td><td align='left'><b>" + Session["ts_EndDate"].ToString() + "</b></td></tr></table>" + sOutput;
        }
        catch (Exception exc)
        {
            Response.Write("<br/>Error loading report... <!--" + exc.Message + "-->");
        }
        finally
        {
            lstLeDate = null;
            lstJobChrg = null;
            lstNonChrg = null;
            lstOthers = null;
            arrJobChrg = null;
            arrData = null;
            htMerge = null;
        }
    }
    private void genReportSql(DataSet dst){
        string sEmpName = "", sEmpDesig = "", sEmpTeam = "", sEmpNo = "";        
        List<
            string
            > lstLeDate
            = new List
            <
            string
            >();
        List<
            string
            > lstJobChrg
            = new List
            <
            string
            >();
        List<
            string
            > lstNonChrg
            = new List
            <
            string
            >();
        List<
            string
            > lstOthers
            = new List
            <
            string
            >();
        ArrayList arrJobChrg = new ArrayList();
        ArrayList arrData = new ArrayList();
        Hashtable htMerge = new Hashtable();        
        int id = 1;
        sExData = "";
        try{
            lblEmpName.Text = Session["ts_EmpName"].ToString();
            lblStartdate.Text = Session["ts_StartDate"].ToString();
            lblEnddate.Text = Session["ts_EndDate"].ToString();
            if(dst.Tables[1]!=null && dst.Tables[1].Rows.Count>0){
            DataRow re = dst.Tables[1].Rows[0];
            sEmpName = re["emp_name"].ToString();
            sEmpDesig = re["emp_desig"].ToString();
            sEmpTeam = re["emp_team"].ToString();
            sEmpNo = re["emp_no"].ToString();
            }
            foreach (DataRow row in dst.Tables[0].Rows){
                string Ledate = DateTime.Parse(row["LSTARTDATE"].ToString().Trim()).ToString("MM/dd/yyyy");
                string Jobid = row["job_name"].ToString().Trim();
                string taskname = row["task_name"].ToString().Trim();
                DateTime dt1 = DateTime.Parse(row["LSTARTDATE"].ToString().Trim());
                DateTime dt2 = DateTime.Parse(row["LENDDATE"].ToString().Trim());
                TimeSpan ts = dt2 - dt1;
                if (!lstLeDate.Contains(Ledate))lstLeDate.Add(Ledate);
                if (row["flag"].ToString().Trim() == "C"){                    
                    if (!lstJobChrg.Contains(Jobid))
                        lstJobChrg.Add(Jobid);
                    if (!htMerge.ContainsKey(Jobid + "|" + Ledate)){
                        htMerge.Add(Jobid + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString() + "|C"));
                    }
                    else{
                        TimeSpan tstemp = (TimeSpan)htMerge[Jobid + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[Jobid + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString() + "|C"));
                    }

                }
                else if (row["flag"].ToString().Trim() == "N"){
                    //old code
                    /*
                    if (!lstNonChrg.Contains(Jobid))
                        lstNonChrg.Add(Jobid);
                    //arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    if (!htMerge.ContainsKey(Jobid + "|" + Ledate))
                    {
                        htMerge.Add(Jobid + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    }
                    else
                    {
                        TimeSpan tstemp = (TimeSpan)htMerge[Jobid + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[Jobid + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + tstemp.ToString()));
                    }
                     * */
                    //new dev
                    if (!lstNonChrg.Contains(taskname))
                        lstNonChrg.Add(taskname);
                    //arrJobChrg.Add(new ListItem(Ledate, taskname + "|" + ts.ToString()));
                    if (!htMerge.ContainsKey(taskname + "|" + Ledate)){
                        htMerge.Add(taskname + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, taskname + "|" + ts.ToString() + "|N"));
                    }
                    else{
                        TimeSpan tstemp = (TimeSpan)htMerge[taskname + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[taskname + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, taskname + "|" + ts.ToString() + "|N"));
                    }
                }
                else{
                    //old code
                    /*
                    if (!lstOthers.Contains(Jobid))
                        lstOthers.Add(Jobid);
                    //arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    if (!htMerge.ContainsKey(Jobid + "|" + Ledate))
                    {
                        htMerge.Add(Jobid + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + ts.ToString()));
                    }
                    else
                    {
                        TimeSpan tstemp = (TimeSpan)htMerge[Jobid + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[Jobid + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, Jobid + "|" + tstemp.ToString()));
                    }
                     * */
                    //new dev 
                    if (!lstOthers.Contains(taskname))
                        lstOthers.Add(taskname);
                    //arrJobChrg.Add(new ListItem(Ledate, taskname + "|" + ts.ToString()));
                    if (!htMerge.ContainsKey(taskname + "|" + Ledate)){
                        htMerge.Add(taskname + "|" + Ledate, ts);
                        arrJobChrg.Add(new ListItem(Ledate, taskname + "|" + ts.ToString() + "|O"));
                    }
                    else{
                        TimeSpan tstemp = (TimeSpan)htMerge[taskname + "|" + Ledate];
                        tstemp = ts.Add(tstemp);
                        htMerge[taskname + "|" + Ledate] = tstemp;
                        arrJobChrg.Add(new ListItem(Ledate, taskname + "|" + ts.ToString() + "|O"));
                    }
                }
                id++;
            }
            lstLeDate.Sort();
            //lstJobChrg.Sort();
            //lstNonChrg.Sort();
            //lstOthers.Sort();
            int chrgcnt = lstJobChrg.Count;
            string[] atsCols = new string[chrgcnt + lstOthers.Count + lstNonChrg.Count];
            //foreach (string item in lstLeDate)
            //    Response.Write(item + "<br/>");
            //Response.Write("<br/>");
            //foreach (string item in lstJobChrg)
            //    Response.Write(item.ToString() + "<br/>");
            //Response.Write("<br/>");
            //foreach (string item in lstNonChrg)
            //    Response.Write(item.ToString() + "<br/>");
            //Response.Write("<br/>");
            string sHeader = "";
            string sRows = "";
            string sOutput = "<table border='1' cellspacing='0px' style='border-collapse:collapse'><tr style='height:40px;'><td>&nbsp;</td><td colspan='2'>&nbsp;</td>";
            if (chrgcnt > 0) sOutput += "<td colspan='" + chrgcnt + "' bgcolor='#dddddd' align='center'><b>Chargeable Hours</b></td>";            
            if (lstOthers.Count > 0) sOutput += "<td colspan='" + lstOthers.Count + "' bgcolor='#dddddd' align='center'><b>Other</b></td>";
            if (lstNonChrg.Count > 0) sOutput += "<td colspan='" + lstNonChrg.Count + "' bgcolor='#dddddd' align='center'><b>Non-Chargeable Hours</b></td>";
            sOutput += "</tr>";
            foreach (string jobid in lstJobChrg){
                sHeader += "<td bgcolor='#F0F8FF' align='center' style='writing-mode:tb-rl;-webkit-transform: rotate(-90deg);'>" + jobid + "</td>";
            }
            lstJobChrg.AddRange(lstOthers);
            foreach (string jobid in lstOthers){
                sHeader += "<td bgcolor='#EEF3E2' align='center' style='writing-mode:tb-rl;-webkit-transform: rotate(-90deg);'>" + jobid + "</td>";
            }
            lstJobChrg.AddRange(lstNonChrg);
            foreach (string jobid in lstNonChrg){
                sHeader += "<td bgcolor='#FFFFF2' align='center' style='writing-mode:tb-rl;-webkit-transform: rotate(-90deg);'>" + jobid + "</td>";
            }
            string sEmpOut = "";
            sEmpOut += "<tr><td align='center'><b>" + sEmpNo + "</b></td><td colspan='2' align='center'><nobr><b>" + sEmpName + "</b></nobr></td>";
            for (int t = 0; t <= (chrgcnt + lstOthers.Count + lstNonChrg.Count); t++) sEmpOut += "<td>&nbsp;</td>";
            sEmpOut += "</tr>";
            sEmpOut += "<tr><td>&nbsp;</td><td align='center'><b>" + sEmpDesig + "</b></td><td align='center'><b>" + sEmpTeam + "</b></td>";
            for (int u = 0; u <= (chrgcnt + lstOthers.Count + lstNonChrg.Count); u++) sEmpOut += "<td>&nbsp;</td>";
            sEmpOut += "</tr>";
            TimeSpan tsTot = new TimeSpan();
            foreach (string ledate in lstLeDate){
                sRows += "<tr><td>" + DateTime.Parse(ledate).DayOfWeek.ToString() + "</td><td>" + ledate + "</td><td>&nbsp;</td>";
                string[] aValues = new string[lstJobChrg.Count];
                TimeSpan tsRTot = new TimeSpan();
                foreach (ListItem item in arrJobChrg){
                    string val = "";
                    string[] aItemval = item.Value.Split('|');
                    if (ledate.Trim() == item.Text){
                        val = lstJobChrg.IndexOf(aItemval[0]).ToString();
                        //aValues[int.Parse(val)] = "<td align='center'>" + aItemval[1] + "</td>";
                        if (aValues[int.Parse(val)] == null)
                            //aValues[int.Parse(val)] = TimeSpan.Parse(aItemval[1]).Hours.ToString() + ":" + TimeSpan.Parse(aItemval[1]).Minutes.ToString() + ":" + TimeSpan.Parse(aItemval[1]).Seconds.ToString();
                            aValues[int.Parse(val)] = aItemval[1];
                        else{
                            TimeSpan tssum = TimeSpan.Parse(aValues[int.Parse(val)]);
                            aValues[int.Parse(val)] = tssum.Add(TimeSpan.Parse(aItemval[1])).ToString();
                        }
                        //object ots = aItemval[1];
                        //tsTot = tsTot.Add(new TimeSpan(TimeSpan.Parse(aItemval[1]).Hours, TimeSpan.Parse(aItemval[1]).Minutes, TimeSpan.Parse(aItemval[1]).Seconds));
                        if (aItemval[2].Equals("C")) tsRTot = tsRTot.Add(TimeSpan.Parse(aItemval[1]));
                        if (atsCols[int.Parse(val)] != null) atsCols[int.Parse(val)] = (TimeSpan.Parse(atsCols[int.Parse(val)]) + TimeSpan.Parse(aItemval[1])).ToString();
                        else atsCols[int.Parse(val)] = aItemval[1];
                    }
                }
                string sCurrRow = "";
                foreach (string str in aValues){
                    if (str == null || str == "00:00:00") sCurrRow += "<td align='center'>&nbsp;</td>";
                    else sCurrRow += "<td align='center'>" + FormatTimeSpan(TimeSpan.Parse(str)) + "</td>";
                }
                //sRows += sCurrRow + "<td>" + tsRTot.ToString() + "</td></tr>";
                sRows += sCurrRow + "<td align='center'>" + FormatTimeSpan(tsRTot) + "</td></tr>";
                tsTot = tsTot.Add(tsRTot);
            }
            sHeader = "<tr><td align='center'>Operator No.</td><td colspan='2' align='center'>Operator</td>" + sHeader + "<td><b>Total(hr:min)</b></td></tr>";
            sHeader = sHeader + sEmpOut + sRows;
            //sOutput += sHeader + "<tr valign='top'><td></td><td colspan='" + (chrgcnt + lstOthers.Count + lstNonChrg.Count) + "'></td><td>" + tsTot.Hours.ToString("hh") + ":" + tsTot.Minutes.ToString("mm") + ":" + tsTot.Seconds.ToString("ss") + "</td></tr></table>";
            sOutput += sHeader + "<tr valign='top'><td>&nbsp;</td><td align='center'><b>Total</b></td><td>&nbsp;</td>";
            for (int o = 0; o < (chrgcnt + lstOthers.Count + lstNonChrg.Count); o++){
                if (atsCols[o] == null || atsCols[o] == "00:00:00") sOutput += "<td style='border-top:1px solid black' align='center'><b>&nbsp;</b></td>";
                else sOutput += "<td style='border-top:1px solid black' align='center'><b>" + FormatTimeSpan(TimeSpan.Parse(atsCols[o])) + "</b></td>";
            }
            sOutput += "<td style='border-top:2px solid black' align='center'><b>" + FormatTimeSpan(tsTot) + "</b></td></tr></table>";
            //Response.Write(sOutput);
            divRep.InnerHtml = sOutput;
            //sExData = "<table><tr><td align='right'>Employee Name:</td><td align='left'><b>" + Session["ts_EmpName"].ToString() + "</b></td></tr>" +
            //        "<tr><td align='right'>Start Date:</td><td align='left'><b>" + Session["ts_StartDate"].ToString() + "</b></td></tr>" +
            //        "<tr><td align='right'>End Date:</td><td align='left'><b>" + Session["ts_EndDate"].ToString() + "</b></td></tr></table>" + sOutput;
            sExData = sOutput;
        }
        catch (Exception exc){
            Response.Write("<br/>Error loading report... <!--" + exc.Message + "-->");
        }
        finally{
            lstLeDate = null;
            lstJobChrg = null;
            lstNonChrg = null;
            lstOthers = null;
            arrJobChrg = null;
            arrData = null;
            htMerge = null;
        }
    }
    private static string FormatTimeSpan(TimeSpan span){
        /*
        return span.Days.ToString("00") + "." +
               span.Hours.ToString("00") + ":" +
               span.Minutes.ToString("00") + ":" +
               span.Seconds.ToString("00");
               /*+ "." +
               span.Milliseconds.ToString("000");
                * */
        int days = span.Days; string sTime = "";
        int tothours = span.Hours + (24 * days);
        sTime = tothours.ToString() + ":" +
               span.Minutes.ToString("00");
        if (sTime.Contains("-")) { sTime = sTime.Replace("-", ""); sTime = "-" + sTime; }
        return sTime;
        // + ":" + span.Seconds.ToString("00");
    }

    protected void btnExport_Click(object sender, EventArgs e){
        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Timesheet_" + DateTime.Now.ToString("MM_dd_yyyy") + ".xls"));
        Response.ContentType = "application/ms-excel";
        Response.Write(sExData);
        Response.End();
    }
}
