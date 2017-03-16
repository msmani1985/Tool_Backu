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
using System.Globalization;
using Microsoft.VisualBasic; 

public partial class Sales_Calender_Events : System.Web.UI.Page
{
    int iDays;
    int iWeeks;
    Sales oSales = new Sales();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["employeeid"] == null)
        {
            throw new Exception("Session Expired!");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["currentDate"] != null && Request.QueryString["currentDate"].ToString().Trim() != "")
            {
                DateTime dt = Convert.ToDateTime(Request.QueryString["currentDate"].ToString().Trim());
                popMonthYear(Convert.ToString(dt.Year));
                drpCalender.SelectedValue = Convert.ToString(dt.Month);
            }
            else
            {
                popMonthYear(Convert.ToString(System.DateTime.Today.Year));
                drpCalender.SelectedValue = Convert.ToString(System.DateTime.Today.Month);
            }
            
            ShowCalenderEvents();

        }
    }
    

    public void Alert(string sMessage)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>alert(\"" + sMessage + "\");</script>");
    }
    protected void drpCalender_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpCalender.SelectedValue != "0")
        {
            ShowCalenderEvents();
            
        }
    }
    private void ShowCalenderEvents()
    {
        string syear = GetYear(drpCalender.SelectedItem.Text.Trim());
        iDays = DateTime.DaysInMonth(Convert.ToInt32(syear), Convert.ToInt32(drpCalender.SelectedValue));
        int iFirstDay = GetFirstDayOfMonth(syear, Convert.ToInt32(drpCalender.SelectedValue));
        iWeeks = Getweeks(iDays, iFirstDay);
        CreateTable(tabRunTime, iWeeks, iDays, iFirstDay, Convert.ToInt32(syear), Convert.ToInt32(drpCalender.SelectedValue));
    }
    private int Getweeks(int iDays, int iFirstDay)
    {
        int offset = (iDays + iFirstDay) / 7;
        int remainder = (iDays + iFirstDay) % 7;
        if (remainder > 0)
            offset++;
        return offset;
    }
    private int GetFirstDayOfMonth(string syear, int iMonth)
    {
        //DateTime dt = Convert.ToDateTime(syear).Year;
        string[] sday = { "sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday" };
        DateTime dtFrom = new DateTime(Convert.ToInt32(syear), iMonth, 1);
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));
        string sDate = dtFrom.ToShortDateString();
        string[] dateParts = sDate.Split('/');

        DateTime dtFirstTemp = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]));
        string sFirstDay = dtFirstTemp.DayOfWeek.ToString();
        for (int iCnt =0 ; iCnt < sday.Length;iCnt++)
        {
            if (sday[iCnt] == sFirstDay.ToLower())
               return iCnt;
        }
        return 0;
    }
    private void CreateTable( HtmlTable tableObj, int iTotalRows, int iDays , int iFirstDay, int year, int month)
    {
        int col = 7;
        int iDaysCount = 1;
        string cellText = string.Empty;
        DataSet Ds = new DataSet();
        string sFirstDate = string.Empty;
        string sLastDate = string.Empty;
        sFirstDate = Convert.ToString(month) + "/1/" + Convert.ToString(year);
        sLastDate = Convert.ToString(month) + "/" + Convert.ToString(iDays) + "/" + Convert.ToString(year);
        Ds = oSales.getEventsList(sFirstDate, sLastDate);
        for (int iCount = 0; iCount < iTotalRows; iCount++)
        {
            HtmlTableRow Tr = new HtmlTableRow();
            Tr.VAlign = "text-top";
            for (int iColumn = 0; iColumn < col; iColumn++)
            {
                HtmlTableCell tc = new HtmlTableCell();
                tc.Attributes.Add("bordercolor", "#dddddd");
                if (iCount == 0)
                {
                    if (iColumn < iFirstDay)
                    {
                        tc.InnerHtml = "&nbsp;";
                        tc.Attributes.Add("style", "width:120px;Height:80px;vertical-align:text-top;padding:2px 2px 2px 2px;");
                        Tr.Cells.Add(tc);
                        continue;
                    }

                }
                if (iDaysCount > iDays)
                {
                    tc.InnerHtml = "&nbsp;";
                    tc.Attributes.Add("style", "width:120px;Height:80px;vertical-align:text-top;padding:2px 2px 2px 2px;");
                    Tr.Cells.Add(tc);
                    continue;
                }
                HyperLink h = new HyperLink();
                cellText = Convert.ToString(iDaysCount);
                
               
                int RowCount = Ds.Tables[0].Rows.Count;
                for (int iCnt = 0; iCnt < RowCount; iCnt++)
                {
                    DataRow Dr = Ds.Tables[0].Rows[iCnt];
                    if (Dr["e_day"].ToString().Trim() == Convert.ToString(iDaysCount))
                    {
                        cellText = Convert.ToString(iDaysCount) + "<br>" + Dr["e_desc_short"].ToString().Trim();
                        break;
                    }
                }
                //System.Drawing.Color.PapayaWhip
                h.Text = cellText;

                DateTime Currdt = System.DateTime.Today;
                DateTime gndt = new DateTime(year, month, iDaysCount);
                //DateTime dt = new DateTime(,m,d);
                string dt = Convert.ToString(month) + "/" + Convert.ToString(iDaysCount) + "/" + Convert.ToString(year);
                iDaysCount++;
                h.NavigateUrl = "Sales_Calender_Events_Update.aspx?currentDate=" + dt;
                h.Attributes.Add("class", "link1");
                tc.Controls.Add(h);
               // tc.Attributes.Add("style", "width:120px;Height:80px;vertical-align:text-top;");
       
                if (Currdt == gndt)
                    tc.Attributes.Add("style", "width:120px;Height:80px;vertical-align:text-top;background-color:#F2F2F2;padding:2px 2px 2px 2px;");
                else
                    tc.Attributes.Add("style", "width:120px;Height:80px;vertical-align:text-top;padding:2px 2px 2px 2px;");
                Tr.Cells.Add(tc);
                
            }
            
            tableObj.Rows.Add(Tr);
        }
    }
    void popMonthYear(string sYear)
    {

        string stYear = "";
        drpCalender.Items.Clear();
        if (String.IsNullOrEmpty(sYear)) stYear = DateTime.Now.Year.ToString();

        else stYear = sYear;

        for (int i = 12; i >= 1; i--)
        {

            drpCalender.Items.Insert(0, new ListItem(DateTime.Parse(i + "/1/" + stYear).ToString("MMMMM") + " " + stYear, i.ToString()));

        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        string syear;
        syear = GetYear(drpCalender.SelectedItem.Text.Trim());
        if (drpCalender.SelectedValue == "1")
        {
            syear = Convert.ToString(Convert.ToUInt32(syear) -1);
            popMonthYear(syear);
            drpCalender.SelectedIndex = 11;
        }
        else
            drpCalender.SelectedIndex = drpCalender.SelectedIndex -1;
        ShowCalenderEvents();

    }
    protected void lnkNexr_Click(object sender, EventArgs e)
    {
        string syear;
        syear = GetYear(drpCalender.SelectedItem.Text.Trim()) ;
        if (drpCalender.SelectedValue == "12")
        {
            syear = Convert.ToString(Convert.ToUInt32(syear) + 1);
            popMonthYear(syear);
            drpCalender.SelectedIndex = 0;
        }
        else
            drpCalender.SelectedIndex = drpCalender.SelectedIndex + 1;
        ShowCalenderEvents();

    }
    private string GetYear(string sgivenyear)
    {
        string syear =string.Empty;
        syear = sgivenyear.Substring(sgivenyear.Length - 4);
        
        return syear;
    }
}
