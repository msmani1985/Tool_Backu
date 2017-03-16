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
using System.IO;

public partial class leavehistory : System.Web.UI.Page
{
    datasourceSQL SqlObj = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet LeaveHisDataset1 = new DataSet();
        if (!Page.IsPostBack)
        {
            if (Convert.ToInt32(Session["employeeteamid"]) == 21) // for HR only
            {
                div_Teamdetails.Visible = true;
            }
            else
            {
                div_Teamdetails.Visible = false;
            }
            DropMonth.SelectedValue = DateTime.Now.Month.ToString();
            DropYear.SelectedValue = DateTime.Now.Year.ToString();
            leave_history();
            string[,] pname1 ={ { "@empid", Session["employeeid"].ToString() }, { "@month", Convert.ToString(DateTime.Now.Month) }, { "@year", Convert.ToString(DateTime.Now.Year) } };
            LeaveHisDataset1 = SqlObj.ExcProcedure("[spGet_LeaveHistory];3", pname1, CommandType.StoredProcedure);
            if (LeaveHisDataset1 != null)
            {
                LeaveHistoryGrid.DataSource = LeaveHisDataset1;
                LeaveHistoryGrid.DataBind();
            }
            SqlObj = null;
        }
    }
    public void leave_history()
    {
        DataSet LeaveHisDataset = new DataSet();
        string LHis = "<table border='1' width='300px' class='bordertable'>";
        string[,] pname = { { "@employee_id", Session["employeeid"].ToString() } };
        LeaveHisDataset = SqlObj.ExcProcedure("SPGET_MY_LEAVE_STATUS", pname, CommandType.StoredProcedure);

        if (LeaveHisDataset != null)
            for (int i = 0; i < LeaveHisDataset.Tables[0].Rows.Count; i++)
            {
                LHis += "<tr><th>Name: " + LeaveHisDataset.Tables[0].Rows[i]["FNAME"].ToString() + "</th>";
                LHis += "<th>PL</th><th>SL</th><th>CL</th></tr>";
                LHis += "<tr><td>Total Leave</td><td>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["PL_TOTAL_LEAVE"].ToString() + "</td>";
                LHis += "<td>" + LeaveHisDataset.Tables[0].Rows[i]["SL_TOTAL_LEAVE"].ToString() + "</td>";
                LHis += "<td>" + LeaveHisDataset.Tables[0].Rows[i]["CL_TOTAL_LEAVE"].ToString() + "</td></tr>";
                LHis += "<tr><td>Leave Taken</td><td>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["PL_LEAVE_DAYS"].ToString() + "</td>";
                LHis += "<td>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["SL_LEAVE_DAYS"].ToString() + "</td>";
                LHis += "<td>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["CL_LEAVE_DAYS"].ToString() + "</td></tr>";
                LHis += "<tr><td>Available Leave</td>";
                LHis += "<td>" + LeaveHisDataset.Tables[0].Rows[i]["PL_AVAILABLE_LEAVE"].ToString() + "</td>";
                LHis += "<td>" + LeaveHisDataset.Tables[0].Rows[i]["SL_AVAILABLE_LEAVE"].ToString() + "</td>";
                LHis += "<td>" + LeaveHisDataset.Tables[0].Rows[i]["CL_AVAILABLE_LEAVE"].ToString() + "</td></tr>";
                LHis += "<tr><td>";
                LHis += "Permission</td><td colspan='2'>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["PERMISSION"].ToString() + "</td></tr>";
                LHis += "<tr><td>";
                LHis += "Loss Of Pay</td><td colspan='2'>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["LOSS_OF_PAY"].ToString() + "</td></tr>";
                LHis += "<tr><td>";
                LHis += "Late LOP</td><td colspan='2'>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["LLOP"].ToString() + "</td></tr>";
                LHis += "<tr><td>";
                LHis += "Uninformed LOP</td><td colspan='2'>";
                LHis += LeaveHisDataset.Tables[0].Rows[i]["ULOP"].ToString() + "</td></tr>";
            }
        LHis += "</table>";
        LeaveHeaderDiv.InnerHtml = LHis;
    }
    protected void exportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition", "attachment;filename=Report.xls");
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            LeaveHistoryGrid.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.End();
        }
        catch (Exception exe)
        {
        }

    }
    protected void LeaveHistoryGrid_ItemCommand(object sender, DataGridCommandEventArgs e)
    {
        datasourceSQL sqlobj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        if (e.CommandName == "Delete1")
        {
            paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Delete,0";
            paramname = "@Lhisid,@approveby_empid,@atype,@redirectid";
            paramtype = "int,int,varchar,Int";
            paramdir = "Input,Input,Input,Input";
            sqlobj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            LeaveHistoryGrid.Items[e.Item.ItemIndex].Visible = false;
        }
        leave_history();
        sqlobj = null;
    }
    protected void LeaveHistory_ItemDataBound(object sender,DataGridItemEventArgs e)
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
                //    e.Item.Cells[9].Visible = true;
                //    return;
                //}
                //if (e.Item.Cells[7].Text.IndexOf("Approved By") > -1)
                //    e.Item.Cells[9].Visible = false;
                //else
                //    e.Item.Cells[9].Visible = true;

                if (DataBinder.Eval(e.Item.DataItem, "EMPLOYEE_ID").ToString() == Session["employeeid"].ToString())
                    if ((e.Item.Cells[7].Text.IndexOf("Awaiting Approval") > -1) && (e.Item.Cells[8].Text.IndexOf("Awaiting Approval") > -1))
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

    protected void Btn_submit_Click(object sender, EventArgs e)
    {
        DataSet LeaveHisDataset = new DataSet();
        datasourceSQL SqlObj = new datasourceSQL();
        try
        {
            LeaveHisDataset = SqlObj.ExcProcedure("spGet_LeaveHistory;2", new string[,] { { "@empno", txt_employeeno.Text.ToString().Trim() } }, CommandType.StoredProcedure);
            if (LeaveHisDataset != null)
            {
                div_errormsg.Visible = false;
                LeaveHistoryGrid.Visible = true;
                LeaveHistoryGrid.DataSource = LeaveHisDataset;
                LeaveHistoryGrid.DataBind();
            }
            else
            {
                LeaveHistoryGrid.Visible = false;
                div_errormsg.InnerHtml = "<font color='red'><b>No Record Found</b></font>";
                div_errormsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            SqlObj = null;
            LeaveHisDataset = null;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        DataSet LeaveHisDataset = new DataSet();
        string[,] pname ={ { "@empid", Session["employeeid"].ToString() }, { "@month", DropMonth.SelectedValue }, { "@year", DropYear.SelectedValue } };
        LeaveHisDataset = SqlObj.ExcProcedure("[spGet_LeaveHistory];3", pname, CommandType.StoredProcedure);
        if (LeaveHisDataset != null)
        {
            LeaveHistoryGrid.DataSource = LeaveHisDataset;
            LeaveHistoryGrid.DataBind();
        }
        else
        {
            LeaveHistoryGrid.DataSource = null;
            LeaveHistoryGrid.DataBind();
        }
    }
}

