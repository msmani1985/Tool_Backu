using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmpShiftMonthWise : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = SqlObj.GetEmpShiftMonthWise(Convert.ToInt16(Request.QueryString["Employee_ID"].ToString()));
            grvShift.DataSource = ds;
            grvShift.DataBind();
            DDYearList.SelectedValue = DateTime.Now.Year.ToString();
            DDMonthList.SelectedValue = DateTime.Now.Month.ToString();
        }
    }
    protected void grvShift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Shift = (DropDownList)e.Row.FindControl("DropShift");
            Label empid = (Label)e.Row.FindControl("ID");
            Label Date = (Label)e.Row.FindControl("Edate");
            DateTime Sdate = Convert.ToDateTime(Date.Text);
            DataSet Ds1 = new DataSet();
            Ds1 = SqlObj.GetEmpStage(Convert.ToInt16(Request.QueryString["Employee_ID"].ToString()));
            Ds1 = SqlObj.GetShift(Ds1.Tables[0].Rows[0]["Location_ID"].ToString());
            Shift.DataSource = Ds1;
            Shift.DataTextField = "Shift_Name";
            Shift.DataValueField = "Shift_ID";
            Shift.DataBind();
            DataSet ds = new DataSet();
            ds = SqlObj.GetEmpShiftDateWise(Convert.ToInt16(empid.Text), Sdate.ToShortDateString());
            int i = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[i]["Shift_ID"].ToString() != "")
                {
                    Shift.Items[Shift.Items.IndexOf(Shift.Items.FindByValue(ds.Tables[0].Rows[i]["Shift_ID"].ToString()))].Selected = true;
                }
                else
                    Shift.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = SqlObj.GetEmpShiftMonthWise1(Convert.ToInt16(Request.QueryString["Employee_ID"].ToString()),Convert.ToInt16(DDMonthList.SelectedValue),Convert.ToInt16(DDYearList.SelectedValue));
        grvShift.DataSource = ds;
        grvShift.DataBind();
    }
}