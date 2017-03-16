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

public partial class Shift_Details : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
            DataSet Ds1 = new DataSet();
            Ds1 = SqlObj.GetShift(Session["locationid"].ToString());
            DropShift.DataSource = Ds1;
            DropShift.DataTextField = "Shift_Name";
            DropShift.DataValueField = "Shift_ID";
            DropShift.DataBind();
            DropShift.Items.Insert(0, new ListItem("--Select--", "0"));
            EmpcodeLbl.Text = Session["employeenumber"].ToString();
            NameLbl.Text = Session["employeename"].ToString();
            DateLbl.Text = DateTime.Now.ToShortDateString();
            DateTime Sdate = Convert.ToDateTime(DateLbl.Text);
            DataSet ss = new DataSet();
            ss = SqlObj.GetEmpShiftDateWise(Convert.ToInt16(Session["employeeid"].ToString()), Sdate.ToShortDateString());
            ss = SqlObj.EmpShift(ss.Tables[0].Rows[0]["Shift_ID"].ToString(), Session["locationid"].ToString());
            DropEmpShift.Text=ss.Tables[0].Rows[0]["Shift_name"].ToString();
            DataSet ds = new DataSet();
            ds = SqlObj.ExcuteSelectProcedure("SpGet_EmpShiftDetails", Session["employeeid"].ToString(), "@empid", "int", "Input", CommandType.StoredProcedure);
            if (ds != null)
            {
                ShiftDetailsGrid.DataSource = ds;
                ShiftDetailsGrid.DataBind();
            }
        }
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            int p;
            string[,] paramcol ={
                    {"@empid",Session["employeeid"].ToString()},{"@val","Output"}
                };
            p = SqlObj.ExcSProcedure("spGet_ShiftHistory", paramcol, CommandType.StoredProcedure);
            //if (p == 1)
            //{
            //    msg = "Already you have Aplied Shift Change.. Plz contact HR ";
            //}
            //else
            {
                string[,] param ={
                {"@employee_id",Session["employeeid"].ToString()},{"@fromdate",FromTxt.Text}
                ,{"@shift",DropShift.Text},{"@remarks",txtRemarks.Text},{"@IsExist","output"},{"@PrvShift",DropEmpShift.Text}
                };
                int ot = SqlObj.ExcSProcedure("spInsert_ShiftDetails", param, CommandType.StoredProcedure);
                if (ot == 1)
                    msg = "Insert Successfully";
                else
                    msg = "Insert Failed";
            }

                DataSet ds = new DataSet();
                ds = SqlObj.ExcuteSelectProcedure("SpGet_EmpShiftDetails", Session["employeeid"].ToString(), "@empid", "int", "Input", CommandType.StoredProcedure);
                if (ds != null)
                {
                    ShiftDetailsGrid.DataSource = ds;
                    ShiftDetailsGrid.DataBind();
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
    protected void ShiftDetailsGrid_ItemCommand(object sender, DataGridCommandEventArgs e)
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
            sqlobj.ExcuteProcedure("spShift_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            ShiftDetailsGrid.Items[e.Item.ItemIndex].Visible = false;
        }
        sqlobj = null;
    }
    protected void ShiftDetailsGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
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
                    if (e.Item.Cells[5].Text.IndexOf("Awaiting Approval") > -1 && e.Item.Cells[6].Text.IndexOf("Awaiting Approval") > -1)
                    {
                        e.Item.Cells[7].Visible = true;
                    }
                    else
                    {
                        e.Item.Cells[7].Visible = false;
                    }

            }

        }
        catch (Exception oex) { }

    }
}
