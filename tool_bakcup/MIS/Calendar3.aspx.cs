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

public partial class Calendar3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack) calDate.SelectedDate = DateTime.Today;
        if (!IsPostBack) this.popScreen();
    }
    //Royson  - mod: 11june2010
    private void popScreen()
    {
        //pop month
        drpMonth.Items.Insert(0, new ListItem("December", "12"));
        drpMonth.Items.Insert(0, new ListItem("November", "11"));
        drpMonth.Items.Insert(0, new ListItem("October", "10"));
        drpMonth.Items.Insert(0, new ListItem("September", "9"));
        drpMonth.Items.Insert(0, new ListItem("August", "8"));
        drpMonth.Items.Insert(0, new ListItem("July", "7"));
        drpMonth.Items.Insert(0, new ListItem("June", "6"));
        drpMonth.Items.Insert(0, new ListItem("May", "5"));
        drpMonth.Items.Insert(0, new ListItem("April", "4"));
        drpMonth.Items.Insert(0, new ListItem("March", "3"));
        drpMonth.Items.Insert(0, new ListItem("February", "2"));
        drpMonth.Items.Insert(0, new ListItem("January", "1"));
        drpMonth.ClearSelection();
        drpMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        //pop year
        for (int j = 1999; j <= DateTime.Now.Year; j++) drpYear.Items.Insert(0, new ListItem(j.ToString(), j.ToString()));

    }
    protected void Change_Date(Object sender, System.EventArgs e)
    {
        string strjscript = "<script language='javascript'>";
        strjscript += "window.opener.document.getElementById('" + HttpContext.Current.Request.QueryString["formname"];
        strjscript += "').value = '" + calDate.SelectedDate.ToString("MM/dd/yyyy") + "'; window.close();";
        strjscript += "<" + "/script" + ">";
        Literal1.Text = strjscript;
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        //if (e.Day.Date <= DateTime.Now.Date.AddDays(-4))
        //{
        //    e.Day.IsSelectable = false;
        //    e.Cell.ForeColor = System.Drawing.Color.Gray;
        //}
    }
    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        calDate.VisibleDate = DateTime.Parse(drpMonth.SelectedItem.Value + "/1/" + drpYear.SelectedItem.Value);
    }
    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        calDate.VisibleDate = DateTime.Parse(drpMonth.SelectedItem.Value + "/1/" + drpYear.SelectedItem.Value);
    }
}

