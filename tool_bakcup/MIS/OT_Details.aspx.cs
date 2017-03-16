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

public partial class OT_Details : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
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
            DataSet SqlD = new DataSet();
            int id = Convert.ToInt16(Session["locationid"].ToString());
            DropMonth.SelectedValue = DateTime.Now.Month.ToString();
            DropYear.SelectedValue = DateTime.Now.Year.ToString();
            if (Convert.ToInt16(Session["locationid"].ToString()) == 2)
            {
                HRMS_CH hrCh = new HRMS_CH();
                SqlD = hrCh.GetEmployeeDetail(Convert.ToInt16(Session["employeenumber"].ToString()));
                DataRow row = SqlD.Tables[0].Rows[0];
                DepartmentLbl.Text = row["Department"].ToString();
                DesignationLbl.Text = row["Designation"].ToString();
                btnChk.Visible = false;
                txtBrkMins.Visible = false;
                txtMins.Visible = false;
                dropBreak.Visible = true;
                txtIn.Visible = false;
                In.Visible = false;
                txtOut.Visible = false;
                Out.Visible = false;
            }
            else
            {

                HRMS_CMB hrCh = new HRMS_CMB();
                SqlD = hrCh.GetEmployeeDetail(Convert.ToInt16(Session["employeenumber"].ToString()));
                DataRow row = SqlD.Tables[0].Rows[0];
                DepartmentLbl.Text = row["Department"].ToString();
                DesignationLbl.Text = row["Designation"].ToString();
                btnChk.Visible = true;
                txtBrkMins.Visible = true;
                txtMins.Visible = true;
                dropBreak.Visible = false;
                txtIn.Visible = true;
                In.Visible = true;
                txtOut.Visible = true;
                Out.Visible = true;
                In.Text = "00:00:00";
                Out.Text = "00:00:00";
                txtActOT.Text = "00:00";
            }

            EmpcodeLbl.Text = Session["employeenumber"].ToString();
            NameLbl.Text = Session["employeename"].ToString();
            DateLbl.Text = DateTime.Now.ToShortDateString();
            DataSet ds = new DataSet();
            ds = SqlObj.ExcuteSelectProcedure("SpGet_EmpOTDetails", Session["employeeid"].ToString(), "@empid", "int", "Input", CommandType.StoredProcedure);
            if (ds != null)
            {
                OTDetailsGrid.DataSource = ds;
                OTDetailsGrid.DataBind();
            }
        }
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string msg = "";
        bool intimate=false;
        decimal actulaleave = 0;
        try
        {
            string[] time1 = txtOT.Text.Replace('.', ':').Split(':');
            string[] time2 = txtActOT.Text.Replace('.', ':').Split(':');
            int ot1hrs = (Convert.ToInt16(time1[0].ToString()) * 60) + Convert.ToInt16(time1[1].ToString());
            int ot2hrs = (Convert.ToInt16(time2[0].ToString()) * 60) + Convert.ToInt16(time2[1].ToString());

            string[,] param1 ={ {"@empid",Session["employeeid"].ToString()},
                {"@startdate",FromTxt.Text},{"@enddate",FromTxt.Text},{"@noofdays","output"}
            };
            DataSet ds1 = new DataSet();
            ds1 = SqlObj.ExcProcedure("SPGET_NOOFDAYSLEAVE", param1, CommandType.StoredProcedure);
            decimal noofdays = Convert.ToDecimal(ds1.Tables[0].Rows[0]["val"].ToString());
            actulaleave = Convert.ToDecimal(noofdays);

            if (Convert.ToDateTime(FromTxt.Text).ToString("dddd").ToUpper() == "SUNDAY")
            {
                intimate = true;
            }
            else if (actulaleave==0)
            {
                intimate = true;
            }
            else
            {
                intimate = true;
            }
            //else if (ot1hrs < ot2hrs)
            //{
            //    intimate = true;
            //}
            if (intimate)
            {
                if (ot1hrs < 120)
                {
                    msg = "You are not eligible OT for this day..";
                }
                else
                {
                    if (txtOT.Text != "" && FromTxt.Text != "")
                    {

                        string[,] param ={
                                 {"@employee_id",Session["employeeid"].ToString()},{"@date",FromTxt.Text},{"@ot",txtOT.Text}
                                ,{"@remarks",txtRemarks.Text},{"@IsExist","output"},{"@TimeIn",txtTimeIn.Text},{"@TimeOut",txtTimeOut.Text}
                                ,{"@Break","00:"+txtBrkMins.Text}};
                        int ot = SqlObj.ExcSProcedure("spInsert_OTDetails", param, CommandType.StoredProcedure);
                        if (ot == 1)
                            msg = "Insert Successfully";
                        else
                            msg = "Awaiting Approval..";

                    }
                    else
                    {
                        msg = "Enter Date and OT Details..";
                    }
                }
            }
            else
                msg = "OT Hours should be greater than Actual Time hours..";

            DataSet ds = new DataSet();
            ds = SqlObj.ExcuteSelectProcedure("SpGet_EmpOTDetails", Session["employeeid"].ToString(), "@empid", "int", "Input", CommandType.StoredProcedure);
            if (ds != null)
            {
                OTDetailsGrid.DataSource = ds;
                OTDetailsGrid.DataBind();
            }
            
        }
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
    protected void OTDetailsGrid_ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        datasourceSQL sqlobj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        if (e.CommandName == "Delete1")
        {
            paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Delete";
            paramname = "@id,@approveby_empid,@atype";
            paramtype = "int,int,varchar";
            paramdir = "Input,Input,Input";
            sqlobj.ExcuteProcedure("spOT_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            OTDetailsGrid.Items[e.Item.ItemIndex].Visible = false;
        }
        sqlobj = null;
    }
    protected void OTDetailsGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            //if (e.Item.ItemType.ToString() != DataControlRowType.Header.ToString())
            //    //string val=e.Item.Cells[8].FindControl("EMPLHId")
            //    e.Item.Cells[7].Controls[1].Visible = false;


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //if (Convert.ToInt32(Session["employeeteamid"]) == 21) // for HR Team only
                //{
                //    e.Item.Cells[8].Visible = true;
                //    return;
                //}
                //if (e.Item.Cells[6].Text.IndexOf("Approved By") > -1)
                //    e.Item.Cells[8].Visible = false;
                //else
                //    e.Item.Cells[8].Visible = true;

                if (DataBinder.Eval(e.Item.DataItem, "EMPLOYEE_ID").ToString() == Session["employeeid"].ToString())
                    if (e.Item.Cells[7].Text.IndexOf("Awaiting Approval") > -1 && e.Item.Cells[8].Text.IndexOf("Awaiting Approval") > -1)
                    {
                        e.Item.Cells[9].Visible = true;
                    }
                    else
                    {
                        e.Item.Cells[9].Visible = false;
                    }

            }

        }
        catch (Exception oex) { }

    }
    protected void btnChk_Click(object sender, EventArgs e)
    {
        Attendance att = new Attendance();
        DataSet da = new DataSet();
        da = att.GetEmployeeTime("spGet_EMpTimeDetails", new string[,] { { "@id", Session["employeenumber"].ToString() }, { "@sdate", FromTxt.Text } } , CommandType.StoredProcedure);
        if (da != null )
        {
            In.Text = da.Tables[0].Rows[0]["Punch1"].ToString();
            Out.Text = da.Tables[0].Rows[0]["outpunch"].ToString();
            txtOT.Text = da.Tables[0].Rows[0]["OTtime"].ToString();

            txtActOT.Text = txtOT.Text;
        }
        else
        {
            In.Text = "00:00:00";
            Out.Text = "00:00:00";
            txtActOT.Text = "00:00";
        }
    }
    protected void txtTimeIn_TextChanged(object sender, EventArgs e)
    {
        if (txtTimeOut.Text == "")
        { 
            txtTimeOut.Text = "00:00";
        }
        string[] time1 = txtTimeIn.Text.Replace('.',':').Split(':');
        string[] time2 = txtTimeOut.Text.Replace('.', ':').Split(':');
        int ot1hrs = (Convert.ToInt16(time1[0].ToString()) * 60) + Convert.ToInt16(time1[1].ToString());
        int ot2hrs = (Convert.ToInt16(time2[0].ToString())*60)+Convert.ToInt16(time2[1].ToString());
       
        DataSet da = new DataSet();
        da = GetOTTime("getOTValues1", new string[,] { { "@break", dropBreak.SelectedValue }, { "@otin", ot1hrs.ToString() }, { "@otout", ot2hrs.ToString() }, { "@mins", txtBrkMins.Text }, { "@loc", Session["locationid"].ToString() } }, CommandType.StoredProcedure);
        txtOT.Text = da.Tables[0].Rows[0]["val"].ToString();
    }
    protected void txtTimeOut_TextChanged(object sender, EventArgs e)
    {
        if (txtTimeIn.Text == "")
        {
            txtTimeIn.Text = "00:00";
        }
        string[] time1 = txtTimeIn.Text.Replace('.', ':').Split(':');
        string[] time2 = txtTimeOut.Text.Replace('.', ':').Split(':');
        int ot1hrs = (Convert.ToInt16(time1[0].ToString()) * 60) + Convert.ToInt16(time1[1].ToString());
        int ot2hrs = (Convert.ToInt16(time2[0].ToString()) * 60) + Convert.ToInt16(time2[1].ToString());
        DataSet da = new DataSet();
        da = GetOTTime("getOTValues1", new string[,] { { "@break", dropBreak.SelectedValue }, { "@otin", ot1hrs.ToString() }, { "@otout", ot2hrs.ToString() }, { "@mins", txtBrkMins.Text }, { "@loc", Session["locationid"].ToString() } }, CommandType.StoredProcedure);
        txtOT.Text = da.Tables[0].Rows[0]["val"].ToString(); ;
    }
    public DataSet GetOTTime(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        DataSet ds = new DataSet();
        ds = SqlObj.ExcProcedure(sProcName, paramcollection, CmdType);
        return ds;
    }
    protected void dropBreak_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtTimeOut.Text == "")
        {
            txtTimeOut.Text = "00:00";
        }
        string[] time1 = txtTimeIn.Text.Replace('.', ':').Split(':');
        string[] time2 = txtTimeOut.Text.Replace('.', ':').Split(':');
        int ot1hrs = (Convert.ToInt16(time1[0].ToString()) * 60) + Convert.ToInt16(time1[1].ToString());
        int ot2hrs = (Convert.ToInt16(time2[0].ToString()) * 60) + Convert.ToInt16(time2[1].ToString());

        DataSet da = new DataSet();
        da = GetOTTime("getOTValues1", new string[,] { { "@break", dropBreak.SelectedValue }, { "@otin", ot1hrs.ToString() }, { "@otout", ot2hrs.ToString() }, { "@mins", txtBrkMins.Text }, { "@loc", Session["locationid"].ToString() } }, CommandType.StoredProcedure);
        txtOT.Text = da.Tables[0].Rows[0]["val"].ToString();
    }
    protected void txtBrkMins_TextChanged(object sender, EventArgs e)
    {
        if (txtTimeOut.Text == "")
        {
            txtTimeOut.Text = "00:00";
        }
        string[] time1 = txtTimeIn.Text.Replace('.', ':').Split(':');
        string[] time2 = txtTimeOut.Text.Replace('.', ':').Split(':');
        int ot1hrs = (Convert.ToInt16(time1[0].ToString()) * 60) + Convert.ToInt16(time1[1].ToString());
        int ot2hrs = (Convert.ToInt16(time2[0].ToString()) * 60) + Convert.ToInt16(time2[1].ToString());

        DataSet da = new DataSet();
        da = GetOTTime("getOTValues1", new string[,] { { "@break", dropBreak.SelectedValue }, { "@otin", ot1hrs.ToString() }, { "@otout", ot2hrs.ToString() }, { "@mins", txtBrkMins.Text }, { "@loc", Session["locationid"].ToString() } }, CommandType.StoredProcedure);
        txtOT.Text = da.Tables[0].Rows[0]["val"].ToString();
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = GetOTTime("spGet_OTHistory", new string[,] { { "@year", DropYear.SelectedValue }, { "@month", DropMonth.SelectedValue }, { "@empid", Session["employeeid"].ToString() } }, CommandType.StoredProcedure);
        if (ds != null)
        {
            OTDetailsGrid.DataSource = ds;
            OTDetailsGrid.DataBind();
        }
        else
        {
            OTDetailsGrid.DataSource = null;
            OTDetailsGrid.DataBind();
        }
    }
}
