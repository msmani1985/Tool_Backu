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
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.NetworkInformation;

public partial class LeaveApplication : System.Web.UI.Page
{
    public double cl;
    public double sl;
    protected void Page_Init(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo newCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        newCulture.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
        newCulture.DateTimeFormat.DateSeparator = "/";
        System.Threading.Thread.CurrentThread.CurrentCulture = newCulture;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SaveBtn.Enabled = true;
            CaptionLbl.Visible = false;
            DataSet SqlDs = new DataSet();
            datasourceSQL SqlObj = new datasourceSQL();
            cl = 0;
            sl = 0;
            try
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
                SqlDs = SqlObj.ExcuteSelectProcedure("spGet_LeaveType", "", "", "", "", CommandType.StoredProcedure);
                if (SqlDs != null)
                {
                    LeavetypeDDList.DataSource = SqlDs.Tables[0];
                    LeavetypeDDList.DataBind();

                }
                GetAvailableDates("onpageload");
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
    }

    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();        
        string halfday="";
        int flg,p;
        string msg = "";
        decimal actulaleave = 0;
        bool intimate;
        try
        {
            if (LeaveReasonTxt.Text != "")
            {
                DataSet dm = new DataSet();
                dm = SqlObj.GetMonth(Convert.ToInt16(Session["employeeid"].ToString()));
                //DataSet dslp = new DataSet();
                //dslp = SqlObj.GetLeavePend(Convert.ToInt16(Session["employeeid"].ToString()));
                //if (dslp.Tables[0].Rows[0]["lp"].ToString()=="1" )
                //{
                //    msg = "Already Apllied Leave's are in pending list..";
                //    //return;
                //}
                //else
                //{
                //For Your Applying leave is Sunday
                if (FromTxt.Text == ToTxt.Text)
                {
                    if (Convert.ToDateTime(FromTxt.Text).ToString("dddd").ToUpper() == "SUNDAY")
                    {
                        msg = "Your Apllied Leave date is Sunday";
                        return;
                    }
                }
                string[,] param ={ {"@empid",Session["employeeid"].ToString()},
                {"@startdate",FromTxt.Text},{"@enddate",ToTxt.Text},{"@noofdays","output"}
            };
                DataSet ds1 = new DataSet();
                 ds1= SqlObj.ExcProcedure("SPGET_NOOFDAYSLEAVE", param, CommandType.StoredProcedure);
                 decimal noofdays = Convert.ToDecimal(ds1.Tables[0].Rows[0]["val"].ToString());
                actulaleave = Convert.ToDecimal(noofdays);
                
                string datedetails = "";
                int opindex = 1;
                string sessiondetail = "";
                if (lbl.Text == "")
                {
                    DateListBtn_Click(sender, e);
                    for (int k = 0; k < DateCBoxList.Items.Count; k++)
                    {
                        if (DateCBoxList.Items[k].Selected == true)
                        {
                            if (FirstCBoxList.Items[k].Selected)
                                sessiondetail = "First Session";
                            else if (SecondCBoxList.Items[k].Selected)
                                sessiondetail = "Second Session";

                            actulaleave = actulaleave - Convert.ToDecimal(0.5);
                            if (datedetails == "")
                                datedetails = datedetails + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM") + "HD" + sessiondetail;
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM") + "HD" + sessiondetail;
                        }
                        else
                        {
                            if (datedetails == "")
                                datedetails = datedetails + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM");
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM");
                        }
                        opindex = opindex + 1;
                    }
                }
                else
                {
                    for (int k = 0; k < DateCBoxList.Items.Count; k++)
                    {
                        if (DateCBoxList.Items[k].Selected == true)
                        {
                            if (FirstCBoxList.Items[k].Selected)
                                sessiondetail = "First Session";
                            else if (SecondCBoxList.Items[k].Selected)
                                sessiondetail = "Second Session";

                            actulaleave = actulaleave - Convert.ToDecimal(0.5);
                            if (datedetails == "")
                                datedetails = datedetails + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM") + "HD" + sessiondetail;
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM") + "HD" + sessiondetail;
                        }
                        else
                        {
                            if (datedetails == "")
                                datedetails = datedetails + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM");
                            else
                                datedetails = datedetails + ";" + Convert.ToDateTime(DateCBoxList.Items[k].Text).Day + '-' + Convert.ToDateTime(DateCBoxList.Items[k].Text).ToString("MMMMM");
                        }
                        opindex = opindex + 1;
                    }
                }
                //actulaleave = Convert.ToDecimal(noofdays);
                intimate = false;
                txtdays.Text = Convert.ToString(actulaleave);
                if (actulaleave == 0)
                {
                    msg = "Requested leave day is Bank Holiday";
                    intimate = true;
                }
                if (LeavetypeDDList.SelectedValue == "1" && (PLLeaveHF.Value == "" || System.Convert.IsDBNull(PLLeaveHF.Value) || Convert.ToDouble(actulaleave) > Convert.ToDouble(PLLeaveHF.Value)))//For preffered Leave
                {
                    msg = "Requested leave days is greater than available leave days ";
                    intimate = true;
                }
                else if (LeavetypeDDList.SelectedValue == "2" && (SLLeaveHF.Value == "" || System.Convert.IsDBNull(SLLeaveHF.Value) || Convert.ToDouble(actulaleave) > Convert.ToDouble(SLLeaveHF.Value)))//For Sick Leave
                {
                    msg = "Requested leave days is greater than available leave days ";
                    intimate = true;
                }
                else if (LeavetypeDDList.SelectedValue == "7" && (CLLeaveHF.Value == "" || System.Convert.IsDBNull(CLLeaveHF.Value) || Convert.ToDouble(actulaleave) > Convert.ToDouble(CLLeaveHF.Value)))//For CL 
                {
                    msg = "Requested leave days is greater than available leave days ";
                    intimate = true;
                }
                else if (LeavetypeDDList.SelectedValue == "3")//For Permission 
                {
                    string[,] paramcol ={
                    {"@empid",Session["employeeid"].ToString()},{"@val","Output"}
                };
                    p = SqlObj.ExcSProcedure("spGet_PermissionHistory", paramcol, CommandType.StoredProcedure);
                    if (p == 1)
                    {
                        msg = "Already you have taken Permission.. Plz contact HR ";
                        intimate = true;
                    }
                }
                DataSet dslp = new DataSet();
                dslp = SqlObj.GetLeavePend(Convert.ToInt16(Session["employeeid"].ToString()));
                if (dslp.Tables[0].Rows[0]["lp"].ToString() == "1")
                {
                    msg = "Already Apllied Leave's are in pending list.. ";
                    lblStatus.Visible = true;
                    lblStatus.Text = msg;
                    intimate = true;
                }
                if (intimate == false)
                {

                    if (LeavetypeDDList.SelectedValue == "3")//For Permission Fromdate and Todate r same and allowed only once per month
                    {
                        ToTxt.Text = FromTxt.Text.ToString();
                        actulaleave = 1;
                    }
                    string[,] paramcol ={
                    {"@employeeid",Session["employeeid"].ToString()},{"@leavein",FromTxt.Text},{"@leaveout",ToTxt.Text},{"@datein",DateTime.Now.ToShortDateString()},{"@halfday",halfday},
                    {"@leavetypeid",LeavetypeDDList.SelectedValue},{"@days",actulaleave.ToString()},{"@comment",LeaveReasonTxt.Text},{"@datedetail",datedetails},{"@flg","Output"}
                };
                    flg = SqlObj.ExcSProcedure("SPADD_LEAVEHISTORY", paramcol, CommandType.StoredProcedure);
                    if (flg == 0)
                        msg = "Your Record  Already Exists";
                    else
                    {
                        msg = "Successfully Inserted";
                        GetAvailableDates("onsuccessleaveapply");
                    }
                    lblStatus.Visible = false;
                    //FollowUpMailSent(datedetails.ToString());
                    //MailMessage mmsg = new MailMessage();
                    //SmtpClient smt = new SmtpClient();
                    //smt.Host = ConfigurationManager.AppSettings["Invoicemail_host"].ToString();
                    //smt.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"].ToString());
                    //mmsg = new MailMessage();
                    //if (!addmailcollection("software@datapage.org", mmsg.To))
                    //{  return; }
                    //if (!addmailcollection("", mmsg.CC))
                    //{  return; }
                    //if (!addmailcollection("", mmsg.Bcc))
                    //{  return; }
                    //mmsg.From = new MailAddress("kalimuthusc@gmail.com", "");
                    //mmsg.Subject = "Leave Applied on " + datedetails.ToString();
                    //mmsg.Body = txtBody.Text;
                    //mmsg.IsBodyHtml = true;
                    //smt.Send(mmsg);
                    ClearFunction();
                }
            }
            else
                msg = "Fill the Leave Reason Details..";
        }
       
        //}
        catch (Exception exe)
        {
            throw exe;
        }
        finally
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(' " + msg + "');</script>");
            SqlObj = null;
        }
        
    }
    private bool addmailcollection(string mailaddress, MailAddressCollection mac)
    {
        if (mailaddress != "")
        {
            string[] address = mailaddress.Split(';');
            for (int maddcnt = 0; address.Length > maddcnt; maddcnt++)
            {
                if (address[maddcnt].ToString().Trim() != "")
                {
                    if (!Regex.IsMatch(address[maddcnt].Trim().ToString(), @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                        return false;
                    else
                        mac.Add(address[maddcnt].Trim().ToString());
                }
            }
        }
        return true;
    }
    private void ClearFunction()
    {
        FromTxt.Text = "";
        ToTxt.Text = "";
        LeaveReasonTxt.Text = "";
        DateCBoxList.Items.Clear();
        ListItem item = new ListItem();
        item.Value = "0";
        item.Text = "None";
        DateCBoxList.Items.Add(item);
        SaveBtn.Enabled = true;
        FirstCBoxList.Items.Clear();
        SecondCBoxList.Items.Clear();

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

    protected void DateListBtn_Click(object sender, EventArgs e)
    {
        DateCBoxList.Items.Clear();
        FirstCBoxList.Items.Clear();
        SecondCBoxList.Items.Clear();
        string test = "";
        DateTime fdate =Convert.ToDateTime(FromTxt.Text);
        DateTime tdate = Convert.ToDateTime(ToTxt.Text);
        int i = 1,x=0;
        test =test + fdate.Day;
        while (fdate <= tdate)
        {
            ListItem it = new ListItem();
            it.Value =i.ToString();
            it.Text = fdate.ToShortDateString();

            //if (fdate.ToString("dddd") == "Sunday")
            //{
                if (CheckholidayDate(fdate) == 1)
                {
                    DateCBoxList.Items.Add(it);
                    FirstCBoxList.Items.Add(new ListItem("Session I", "0"));
                    SecondCBoxList.Items.Add(new ListItem("Session II", "1"));
                    
                }

            //}
            ////else
            ////{
            //    if (CheckholidayDate(fdate) == 1)
            //    {
            //        if (fdate.ToString("dddd") == "Sunday")
            //        {
            //            DateCBoxList.Items.Add(it);
            //        }
            //        else
            //        {
            //            DateCBoxList.Items.Add(it);
            //            FirstCBoxList.Items.Add(new ListItem("Session I", "0"));
            //            SecondCBoxList.Items.Add(new ListItem("Session II", "1"));
            //        }
            //    }
            ////}
            test = test + "," + fdate.Day;
            i = i + 1;
            x = x + 1;
            fdate = fdate.AddDays(1);
            
        }
        //DateCBoxList.Visible=false;
        //    FirstCBoxList.Visible=false;
        //    SecondCBoxList.Visible = false;
        //txtdays.Text = x.ToString();
        SaveBtn.Enabled = true;
        CaptionLbl.Visible = true;
        lbl.Text = ".";
    }
    private int CheckholidayDate(DateTime TestDate)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        int lflg;
        

        try
        {
           string[,] paramcol={{"@leavedate",TestDate.ToShortDateString()},{"@lflg","output"}};
            //lflg = SqlObj.ExcuteProcedure("spGet_BankLeave", TestDate.ToShortDateString(), "@leavedate,@lflg", "date,int", "input,output", CommandType.StoredProcedure);
           lflg = SqlObj.ExcSProcedure("spGet_BankLeave", paramcol, CommandType.StoredProcedure);
           return lflg;
        }
        catch (Exception ex)
        {
            return 2;
            //throw ex;
        }
        finally
        {
            SqlObj = null;
        }
    }


    protected void FirstCBoxList_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int k = 0; k < FirstCBoxList.Items.Count; k++)
        {
            if (FirstCBoxList.Items[k].Selected)
                SecondCBoxList.Items[k].Selected = false;

            if (FirstCBoxList.Items[k].Selected == true || SecondCBoxList.Items[k].Selected == true)
                DateCBoxList.Items[k].Selected = true;
            else if (FirstCBoxList.Items[k].Selected == false && SecondCBoxList.Items[k].Selected == false)
                DateCBoxList.Items[k].Selected = false;
        }

    }
    protected void SecondCBoxList_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int g = 0; g < SecondCBoxList.Items.Count; g++)
        {
            if (SecondCBoxList.Items[g].Selected)
                FirstCBoxList.Items[g].Selected = false;

            if (FirstCBoxList.Items[g].Selected == true || SecondCBoxList.Items[g].Selected == true)
                DateCBoxList.Items[g].Selected = true;
            else if (FirstCBoxList.Items[g].Selected == false && SecondCBoxList.Items[g].Selected == false)
                DateCBoxList.Items[g].Selected = false;

        }
    }
    protected void FollowUpMailSent(string dates)
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGet_EmpEmailDetails", new string[,] { { "@empid", Session["employeeid"].ToString() } }, CommandType.StoredProcedure);
        //string fromaddress = ConfigurationManager.AppSettings["SAMFollowupmail_fromaddress"].ToString();

        if (dst != null)
        {

            try
            {
                foreach (DataRow row in dst.Tables[0].Rows)
                {
                    xd.Load(Server.MapPath("").ToString() + @"\LeaveApplyMail.xml");

                    //xn = xd.DocumentElement.SelectSingleNode("//followup").SelectSingleNode("customer[@custno='" + Request.QueryString["CUSTNO"].ToString().Trim() + "']");

                    xn = xd.DocumentElement.SelectSingleNode("//Employee").FirstChild;

                    if ((xn != null) && (row != null))
                    {
                        childnode = xn.SelectSingleNode("to");
                        // lblToAddress.Text = "software@datapage.org";
                        txtTo.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        // lblToAddress.Text = (Request.QueryString["AEMAIL"].ToString().Trim() != "") ? Request.QueryString["AEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        txtTo.Text = row["Email_App"].ToString().Trim();
                        //childnode = xn.SelectSingleNode("cc");
                        //lblCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        //lblCCAddress.Text = row["INVCONEMAIL"].ToString().Trim();
                        //childnode = xn.SelectSingleNode("bcc");
                        //lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        ////lblBCCAddress.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        childnode = xn.SelectSingleNode("body");
                        txtBody.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        txtBody.Text = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                        txtBody.Text = txtBody.Text.Replace("[NAME]", row["App"].ToString().Trim());
                        txtBody.Text = txtBody.Text.Replace("[Dates]", dates.ToString().Trim());
                        txtBody.Text = txtBody.Text.Replace("[EmpName]", row["EmpName"].ToString().Trim());
                        
                    }

                }
            }

            catch (Exception ex)
            { throw ex; }
            finally
            { xd = null; childnode = null; xn = null; }
        }
        //else
            //alert("No Records Found");
    }



}

