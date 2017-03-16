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

public partial class LeaveApply_New : System.Web.UI.Page
{
    
    datasourceSQL SqlObj = new datasourceSQL();
    decimal p, s, c, pm, LOP, ULOP, CO;
    int Pmins;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            DataSet SqlD = new DataSet();
            int id = Convert.ToInt16(Session["locationid"].ToString());
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            {
                HRMS_CH hrCh = new HRMS_CH();
                SqlD = hrCh.GetEmployeeDetail(Convert.ToInt16(Session["employeenumber"].ToString()));
                DataRow row = SqlD.Tables[0].Rows[0];
                DepartmentLbl.Text = row["Department"].ToString();
                DesignationLbl.Text = row["Designation"].ToString();
            }
            else
            {

                HRMS_CMB hrCh = new HRMS_CMB();
                SqlD = hrCh.GetEmployeeDetail(Convert.ToInt16(Session["employeenumber"].ToString()));
                DataRow row = SqlD.Tables[0].Rows[0];
                DepartmentLbl.Text = row["Department"].ToString();
                DesignationLbl.Text = row["Designation"].ToString();
            }
            EmpcodeLbl.Text = Session["employeenumber"].ToString();
            NameLbl.Text = Session["employeename"].ToString();
            DateLbl.Text = DateTime.Now.ToShortDateString();
            GetAvailableDates("onpageload");
        }
    }
    private void GetAvailableDates(string sQueryName)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        DataSet SqlDs = new DataSet();
        try
        {
            SqlDs = SqlObj.ExcuteSelectProcedure("SPGET_MY_LEAVE_STATUS", Session["employeeid"].ToString(), "@employee_id", "int", "Input", CommandType.StoredProcedure);
            if (SqlDs != null)
            {
                string LeaveDet = "<table class='bordertable' cellpadding='2' cellspacing='5' width='300px'><tr><td colspan='3' style='font-weight:bold;color:Red;' align='center'>Leave Status</td></tr>";
                LeaveDet = LeaveDet + "<tr><th>Description</th><th>PL</th><th>SL</th><th>CL</th></tr>";

                for (int i = 0; i < SqlDs.Tables[0].Rows.Count; i++)
                {
                    LeaveDet += "<tr><td>Available Leave</td>";
                    LeaveDet += "<td>" + SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() + "</td>";
                    LeaveDet += "<td>" + SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() + "</td>";
                    LeaveDet += "<td>" + SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() + "</td></tr>";
                    if (SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() != "" && SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() != null)
                        PLLeaveHF.Value = SqlDs.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString();
                    if (SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() != "" && SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() != null)
                        SLLeaveHF.Value = SqlDs.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString();
                    if (SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() != "" && SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() != null)
                        CLLeaveHF.Value = SqlDs.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString();

                }
                LeaveDet += "</table>";
                LeaveDetaildiv.InnerHtml = LeaveDet;
            }
            FromTxt.Text = "";
            ToTxt.Text = "";
            LeaveReasonTxt.Text = "";
            grvLeave.DataSourceID = null;
            grvLeave.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            SqlDs = null;
            SqlObj = null;
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (LeaveReasonTxt.Text == "") sMessage += i++ + ". Enter the Reason Details!.\\r\\n";
        if (grvLeave.Rows.Count==0) sMessage += i++ + ". Select Date and click Submit button!.\\r\\n";
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + sMessage + " ');</script>");
            return false;
        }
        return true;
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string msg = "", msg1 = "", datedetails = "";
        bool intimate=false;
        ArrayList al = new ArrayList();
        Hashtable val = null;
        try
        {
            if (validateScreen())
            {
                
                    DataTable table2 = new DataTable("Leave");
                    table2.Columns.Add("Date");
                    table2.Columns.Add("Days");
                    table2.Columns.Add("Type");
                    table2.Columns.Add("DateDetails");
                    table2.Columns.Add("PerMins");
                    foreach (GridViewRow grw in grvLeave.Rows)
                    {
                        datedetails = "";
                        val = new Hashtable();
                        Label Date = (Label)grw.FindControl("lblDate");
                        DropDownList ddFirst = (DropDownList)grw.FindControl("LeavetypeFirst");
                        CheckBox ckFirst = (CheckBox)grw.FindControl("ckFirstHalf");
                        DropDownList ddSecond = (DropDownList)grw.FindControl("LeavetypeSecond");
                        DropDownList PermissionMins = (DropDownList)grw.FindControl("PermissionMins");
                        CheckBox ckSecond = (CheckBox)grw.FindControl("ckSecondHalf");
                        bool f = ckFirst.Checked;
                        bool s = ckSecond.Checked;

                        if (f == true && s == true)
                        {
                            if (datedetails == "")
                            {
                                string ss = Convert.ToDateTime(Date.Text).Day.ToString();
                                datedetails = ss + '-' + Convert.ToDateTime(Date.Text).ToString("MMM");
                            }
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(Date.Text).Day + '-' + Convert.ToDateTime(Date.Text).ToString("MMMMM");

                            if (ddFirst.SelectedValue == ddSecond.SelectedValue)
                            {
                                table2.Rows.Add(Date.Text, 1, ddFirst.SelectedValue, datedetails,0);
                            }
                            else
                            {
                                if (ddFirst.SelectedValue.ToString() == "3")
                                    table2.Rows.Add(Date.Text, 1.0, ddFirst.SelectedValue, datedetails + " First Half",PermissionMins.SelectedValue);
                                else
                                    table2.Rows.Add(Date.Text, 0.5, ddFirst.SelectedValue, datedetails + " First Half",0);

                                if (ddSecond.SelectedValue.ToString() == "3")
                                    table2.Rows.Add(Date.Text, 1.0, ddSecond.SelectedValue, datedetails + " Second Half", PermissionMins.SelectedValue);
                                else
                                    table2.Rows.Add(Date.Text, 0.5, ddSecond.SelectedValue, datedetails + " Second Half", 0);
                            }
                        }
                        else if (f == true && s == false)
                        {
                            if (datedetails == "")
                            {
                                string ss = Convert.ToDateTime(Date.Text).Day.ToString();
                                datedetails = ss + '-' + Convert.ToDateTime(Date.Text).ToString("MMM") + " First Half";
                            }
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(Date.Text).Day + '-' + Convert.ToDateTime(Date.Text).ToString("MMM") + " First Half";
                            if (ddFirst.SelectedValue.ToString()=="3")
                                table2.Rows.Add(Date.Text, 1.0, ddFirst.SelectedValue, datedetails, PermissionMins.SelectedValue);
                            else
                                table2.Rows.Add(Date.Text, 0.5, ddFirst.SelectedValue, datedetails, 0);
                        }
                        else if (f == false && s == true)
                        {
                            if (datedetails == "")
                            {
                                string ss = Convert.ToDateTime(Date.Text).Day.ToString();
                                datedetails = ss + '-' + Convert.ToDateTime(Date.Text).ToString("MMM") + " Second Half";
                            }
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(Date.Text).Day + '-' + Convert.ToDateTime(Date.Text).ToString("MMMMM") + " Second Half";

                            if (ddSecond.SelectedValue.ToString() == "3")
                                table2.Rows.Add(Date.Text, 1.0, ddSecond.SelectedValue, datedetails, PermissionMins.SelectedValue);
                            else
                                table2.Rows.Add(Date.Text, 0.5, ddSecond.SelectedValue, datedetails, 0);
                        }
                    }
                    DataSet lv = new DataSet("Leave");
                    lv.Tables.Add(table2);

                    DataRow[] Presult = table2.Select("Type = 1");
                    if (Presult.Length > 0)
                    {
                        foreach (DataRow row in Presult)
                        {
                            if (p == 0)
                                p = Convert.ToDecimal(row["Days"].ToString());
                            else
                                p = p + Convert.ToDecimal(row["Days"].ToString());
                        }
                    }
                    DataRow[] Sresult = table2.Select("Type = 2");
                    if (Sresult.Length > 0)
                    {
                        foreach (DataRow row in Sresult)
                        {
                            if (s == 0)
                                s = Convert.ToDecimal(row["Days"].ToString());
                            else
                                s = s + Convert.ToDecimal(row["Days"].ToString());
                        }
                    }
                    DataRow[] Cresult = table2.Select("Type = 7");
                    if (Cresult.Length > 0)
                    {
                        foreach (DataRow row in Cresult)
                        {
                            if (c == 0)
                                c = Convert.ToDecimal(row["Days"].ToString());
                            else
                                c = c + Convert.ToDecimal(row["Days"].ToString());
                        }
                    }
                    DataRow[] PMresult = table2.Select("Type = 3");
                    if (PMresult.Length > 0)
                    {
                        foreach (DataRow row in PMresult)
                        {
                            if (pm == 0)
                                pm = Convert.ToDecimal(row["Days"].ToString());
                            else
                                pm = pm + Convert.ToDecimal(row["Days"].ToString());

                            Pmins = Convert.ToInt32(row["PerMins"].ToString());
                        }
                    }

                    DataRow[] LOPresult = table2.Select("Type = 5");
                    if (LOPresult.Length > 0)
                    {
                        foreach (DataRow row in LOPresult)
                        {
                            if (LOP == 0)
                                LOP = Convert.ToDecimal(row["Days"].ToString());
                            else
                                LOP = LOP + Convert.ToDecimal(row["Days"].ToString());
                        }
                    }
                    DataRow[] ULOPresult = table2.Select("Type = 9");
                    if (ULOPresult.Length > 0)
                    {
                        foreach (DataRow row in ULOPresult)
                        {
                            if (ULOP == 0)
                                ULOP = Convert.ToDecimal(row["Days"].ToString());
                            else
                                ULOP = ULOP + Convert.ToDecimal(row["Days"].ToString());
                        }
                    }
                    DataRow[] COresult = table2.Select("Type = 4");
                    if (COresult.Length > 0)
                    {
                        foreach (DataRow row in COresult)
                        {
                            if (CO == 0)
                                CO = Convert.ToDecimal(row["Days"].ToString());
                            else
                                CO = CO + Convert.ToDecimal(row["Days"].ToString());
                        }
                    }
                    //Leave Policy Validation
                    //if (Convert.ToInt16(Session["locationid"].ToString()) != 2)
                    //{
                    //    if ((p + c + s) != 0)
                    //    {
                    //        string pp, cc, ssl;
                    //        if (PLLeaveHF.Value == "")
                    //            pp = "0.00";
                    //        else
                    //            pp = PLLeaveHF.Value;
                    //        if (CLLeaveHF.Value == "")
                    //            cc = "0.00";
                    //        else
                    //            cc = CLLeaveHF.Value;
                    //        if (SLLeaveHF.Value == "")
                    //            ssl = "0.00";
                    //        else
                    //            ssl = SLLeaveHF.Value;

                    //        decimal val1;
                    //        decimal val2 = Convert.ToDecimal(pp) + Convert.ToDecimal(cc) + Convert.ToDecimal(ssl);
                    //        string[,] paramcol1 ={
                    //    {"@empid",Session["employeeid"].ToString()},{"@Bal",val2.ToString()},{"@flg","Output"}
                    //            };
                    //        val1 = SqlObj.ExcSProcedure("spGetLeavePolicy", paramcol1, CommandType.StoredProcedure);

                    //        if ((val1 - p - c - s) < 0)
                    //        {
                    //            msg = "You are only Eligible for " + val1.ToString() + " days";
                    //            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
                    //            return;
                    //        }
                    //    }
                    //}

                    if ((p + c + s + pm + LOP + ULOP + CO) == 0)
                    {
                            msg = "Please select Leave details (First/Second Half or Full Days)";
                            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
                            return;
                    }
                    if (p > 0)
                    {
                        if ((PLLeaveHF.Value == "" || System.Convert.IsDBNull(PLLeaveHF.Value) || Convert.ToDouble(p) > Convert.ToDouble(PLLeaveHF.Value)))//For preffered Leave
                        {
                            msg = "PL";
                            intimate = true;
                        }
                    }
                    if (s > 0)
                    {
                        if ((SLLeaveHF.Value == "" || System.Convert.IsDBNull(SLLeaveHF.Value) || Convert.ToDouble(s) > Convert.ToDouble(SLLeaveHF.Value)))//For Sick Leave
                        {
                            if (msg == "")
                                msg = "SL";
                            else
                                msg = msg + "/SL";
                            intimate = true;
                        }
                    }
                    if (c > 0)
                    {
                        if ((CLLeaveHF.Value == "" || System.Convert.IsDBNull(CLLeaveHF.Value) || Convert.ToDouble(c) > Convert.ToDouble(CLLeaveHF.Value)))//For CL 
                        {
                            if (msg == "")
                                msg = "CL";
                            else
                                msg = msg + "/CL";
                            intimate = true;
                        }
                    }
                    if (pm > 0)
                    {
                        int v;
                        string[,] paramcol ={
                                                {"@empid",Session["employeeid"].ToString()},{"@val","Output"}
                                            };
                        v = SqlObj.ExcSProcedure("sp_PerHistory", paramcol, CommandType.StoredProcedure);
                        if (v == 1)
                        {
                            msg1 = "Already you have taken Permission .. Plz contact HR ";
                            intimate = true;
                        }
                        else if (v == 2)
                        {
                            if (Pmins == 60)
                            {
                                msg1 = "Already you have taken 30 mins Permission .. So you have 30 mins remaining";
                                intimate = true;
                            }
                            else
                            {
                                intimate = false;
                            }
                        }
                        
                    }
                    //intimate = true;
                    //msg1 = "Sorry... Today you cant avail this service. Please contact HR, Thanks";
                    if (intimate == false)
                    {
                        if (lv.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < lv.Tables[0].Rows.Count; i++)
                            {
                                val = new Hashtable();
                                val.Add("ID", Session["employeeid"].ToString());
                                val.Add("LeaveIn", lv.Tables[0].Rows[i]["Date"].ToString());
                                val.Add("LeaveOut", lv.Tables[0].Rows[i]["Date"].ToString());
                                val.Add("Date", DateTime.Now.ToShortDateString());
                                val.Add("LeaveType", lv.Tables[0].Rows[i]["Type"].ToString());
                                val.Add("Days", lv.Tables[0].Rows[i]["Days"].ToString());
                                val.Add("DateDetails", lv.Tables[0].Rows[i]["DateDetails"].ToString());
                                val.Add("Remarks", LeaveReasonTxt.Text);
                                val.Add("AppliedDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                                val.Add("ModeofInform", "MIS");
                                val.Add("PerMins", lv.Tables[0].Rows[i]["PerMins"].ToString());
                                al.Add(val);
                            }
                        }
                        if (!SqlObj.Insert_LeaveDetails(al))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Insert Failed');</script>");
                        }
                        else
                        {
                            GetAvailableDates("onpageload");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                        }
                    }
                    else
                    {
                        if (msg1=="")
                            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('Requested " + msg + " leave days is greater than available leave days ');</script>");
                        else
                            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg1 + "');</script>");
                    }
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(" + msg + " );</script>");
            SqlObj = null;
        }
    }
    protected void grvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {   
            DropDownList Fisrt = e.Row.FindControl("LeavetypeFirst") as DropDownList;
            DropDownList second = e.Row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList PermissionMins = e.Row.FindControl("PermissionMins") as DropDownList;
            Label Date = e.Row.FindControl("lblDate") as Label;
            CheckBox FC = e.Row.FindControl("ckFirstHalf") as CheckBox;
            CheckBox SC = e.Row.FindControl("ckSecondHalf") as CheckBox;
            FC.Checked = true;
            SC.Checked = true;
            Label Days = e.Row.FindControl("lblDays") as Label;
            Days.Text = "1";
            DataSet SqlDs = new DataSet();
            SqlDs = SqlObj.ExcuteSelectProcedure("spGet_LeaveType", "", "", "", "", CommandType.StoredProcedure);
            if (SqlDs != null)
            {
                if (Convert.ToDateTime(Date.Text).ToString("dddd").ToUpper() == "SUNDAY")
                {
                    Fisrt.DataSource = SqlDs.Tables[0];
                    Fisrt.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    Fisrt.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    Fisrt.DataBind();
                    second.DataSource = SqlDs.Tables[0];
                    second.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    second.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    second.DataBind();
                    Fisrt.SelectedValue = "5";
                    second.SelectedValue = "5";
                    FC.Checked = true;
                    SC.Checked = true;
                    FC.Enabled = false;
                    SC.Enabled = false;
                    Fisrt.Enabled = false;
                    second.Enabled = false;
                }
                else
                {
                    Fisrt.DataSource = SqlDs.Tables[0];
                    Fisrt.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    Fisrt.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    Fisrt.DataBind();
                    second.DataSource = SqlDs.Tables[0];
                    second.DataTextField = SqlDs.Tables[0].Columns[1].ToString();
                    second.DataValueField = SqlDs.Tables[0].Columns[0].ToString();
                    second.DataBind();
                }
                if(Session["locationid"].ToString()=="2")
                {
                    e.Row.Cells[5].Visible = false;
                }
                else
                {
                    e.Row.Cells[5].Visible = true;
                }
            }
        }
    }
    protected void LeavetypeFirst_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("LeavetypeFirst") as DropDownList;
            Control Second = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList F = (DropDownList)First;
            DropDownList S = (DropDownList)Second;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            Control FirstChk = row.FindControl("ckFirstHalf") as CheckBox;
            Control SecondChk = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox FC = (CheckBox)FirstChk;
            CheckBox SC = (CheckBox)SecondChk;
            if (F.SelectedValue.ToString() == "3")
            {
                SC.Checked = false;
                SC.Enabled = false;
                S.Enabled = false;
            }
            else
            {
                SC.Checked = true;
                SC.Enabled = true;
                S.Enabled = true;
            }
            if (FC.Checked)
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            if (F.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                    else
                    {
                        Days.Text = "";
                    }
                }
            }
            else
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                }
                else
                {
                    P.Visible = true;
                }
                if (SC.Checked)
                {
                    if (S.SelectedValue.ToString() != "3")
                    {
                        if (F.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            if(FC.Checked)
                                P.Visible = true;
                            else
                                P.Visible = false;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
    protected void LeavetypeSecond_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("LeavetypeFirst") as DropDownList;
            Control Second = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList F = (DropDownList)First;
            DropDownList S = (DropDownList)Second;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            Control FirstChk = row.FindControl("ckFirstHalf") as CheckBox;
            Control SecondChk = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox FC = (CheckBox)FirstChk;
            CheckBox SC = (CheckBox)SecondChk;
            if (S.SelectedValue.ToString() == "3")
            {
                FC.Checked = false;
                FC.Enabled = false;
                F.Enabled = false;
            }
            else
            {
                FC.Checked = true;
                FC.Enabled = true;
                F.Enabled = true;
            }
            if (FC.Checked)
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (SC.Checked)
                    {
                        if (S.SelectedValue.ToString() != "3")
                        {
                            if (F.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                }
            }
            else
            {
                if (F.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                }
                else
                {
                    P.Visible = true;
                }
                if (SC.Checked)
                {
                    if (S.SelectedValue.ToString() != "3")
                    {
                        if (F.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
    protected void DateListBtn_Click(object sender, EventArgs e)
    {
        string test = "", msg = ""; ;
        DateTime fdate = Convert.ToDateTime(FromTxt.Text);
        DateTime tdate = Convert.ToDateTime(ToTxt.Text);
        if (FromTxt.Text == ToTxt.Text)
        {
            if (Convert.ToDateTime(FromTxt.Text).ToString("dddd").ToUpper() == "SUNDAY")
            {
                msg = "Your Apllied Leave date is Sunday";
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
                return;
            }
        }
        int flg;
        string[,] paramcol ={
                    {"@employeeid",Session["employeeid"].ToString()},{"@leavein",FromTxt.Text},{"@leaveout",ToTxt.Text},{"@flg","Output"}
                            };
        flg = SqlObj.ExcSProcedure("SpGet_LEAVEHISTORY1", paramcol, CommandType.StoredProcedure);
        if (flg == 1)
        {
            msg = "Already Leave applied in this date!.";
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert('" + msg + "');</script>");
            return;
        }

        DataTable table1 = new DataTable("Leave");
        table1.Columns.Add("Date");
        while (fdate <= tdate)
        {
            table1.Rows.Add(fdate.ToShortDateString());
            fdate = fdate.AddDays(1);
        }
        DataSet lv = new DataSet("Leave");
        lv.Tables.Add(table1);
        grvLeave.DataSource = lv;
        grvLeave.DataBind();
    }


    protected void ckFirstHalf_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("ckFirstHalf") as CheckBox;
            Control Second = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox F = (CheckBox)First;
            CheckBox S = (CheckBox)Second;
            Control FirstDrop = row.FindControl("LeavetypeFirst") as DropDownList;
            Control SecondDrop = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList FD = (DropDownList)FirstDrop;
            DropDownList SD = (DropDownList)SecondDrop;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            if (F.Checked)
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            if (FD.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                }
            }
            else
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = true;
                }
                else
                {
                    P.Visible = false;
                }
                if (S.Checked)
                {
                    if (SD.SelectedValue.ToString() != "3")
                    {
                        if (FD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
    protected void ckSecondHalf_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grvLeave.Rows)
        {
            Control First = row.FindControl("ckFirstHalf") as CheckBox;
            Control Second = row.FindControl("ckSecondHalf") as CheckBox;
            Label Days = row.FindControl("lblDays") as Label;
            CheckBox F = (CheckBox)First;
            CheckBox S = (CheckBox)Second;
            Control FirstDrop = row.FindControl("LeavetypeFirst") as DropDownList;
            Control SecondDrop = row.FindControl("LeavetypeSecond") as DropDownList;
            DropDownList FD = (DropDownList)FirstDrop;
            DropDownList SD = (DropDownList)SecondDrop;
            Control PermissionMins = row.FindControl("PermissionMins") as DropDownList;
            DropDownList P = (DropDownList)PermissionMins;
            if (F.Checked)
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                            Days.Text = "1";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "0.5";
                        }
                    }
                    else
                        Days.Text = "0.5";
                }
                else
                {
                    P.Visible = true;
                    if (S.Checked)
                    {
                        if (SD.SelectedValue.ToString() != "3")
                        {
                            if (FD.SelectedValue.ToString() != "3")
                            {
                                P.Visible = false;
                            }
                            else
                            {
                                P.Visible = true;
                            }
                            Days.Text = "0.5";
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                    else
                    {
                        if (FD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                            Days.Text = "";
                        }
                    }
                }
            }
            else
            {
                if (FD.SelectedValue.ToString() != "3")
                {
                    P.Visible = false;
                }
                else
                {
                    P.Visible = true;
                }
                if (S.Checked)
                {
                    if (SD.SelectedValue.ToString() != "3")
                    {
                        if (FD.SelectedValue.ToString() != "3")
                        {
                            P.Visible = false;
                        }
                        else
                        {
                            P.Visible = true;
                        }
                        Days.Text = "0.5";
                    }
                    else
                    {
                        P.Visible = true;
                        Days.Text = "";
                    }
                }
                else
                    Days.Text = "";
            }
        }
    }
}
