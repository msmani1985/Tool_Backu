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
using System.Collections.Generic;

public partial class leaveapprove : System.Web.UI.Page
{
    DataSet SqlDs = new DataSet();
    //DataSet oTeamDS = new DataSet();
    datasourceSQL SqlObj = new datasourceSQL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Approval_SelectedIndexChanged(sender, e);
        }
    }
    protected void LeaveApprove_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol="";
        string paramname="";
        string paramtype="";
        string paramdir="";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in LeaveApproveGrid.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + "," + ((TextBox)row2.FindControl("txtRemarks1")).Text.ToString();
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,Int,Int,Varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //LeaveApproveGrid.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + "," + ((TextBox)row2.FindControl("txtRemarks1")).Text.ToString();
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,int,Int,Varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //LeaveApproveGrid.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                   
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + "," + ((TextBox)row.FindControl("txtRemarks1")).Text.ToString();
                paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                paramtype = "int,int,varchar,Int,Int,Varchar";
                paramdir = "Input,Input,Input,Input,Input,Input";
                LeaveApproveGrid.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + "," + ((TextBox)row.FindControl("txtRemarks1")).Text.ToString();
                paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                paramtype = "int,int,varchar,int,Int,Varchar";
                paramdir = "Input,Input,Input,Input,Input,Input";
                LeaveApproveGrid.Rows[row.DataItemIndex].Visible = false;
            }
            SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            SqlObj = null;
        }
    }

    protected void LeaveApprove1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in LeaveApproveGridMgr.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;

                    if (e.CommandName == "imgApprove")
                    {
                        GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + "," + ((TextBox)row2.FindControl("txtRemarks1")).Text.ToString();
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,Int,Int,varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //LeaveApproveGridMgr.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + "," + ((TextBox)row2.FindControl("txtRemarks1")).Text.ToString();
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,int,int,Varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //LeaveApproveGridMgr.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                    
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;

        
        if (e.CommandName == "Approve")
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + "," + ((TextBox)row.FindControl("txtRemarks1")).Text.ToString();
            paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
            paramtype = "int,int,varchar,Int,Int,varchar";
            paramdir = "Input,Input,Input,Input,Input,Input";
            LeaveApproveGridMgr.Rows[row.DataItemIndex].Visible = false;
        }
        if (e.CommandName == "Reject")
        {
            GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + "," + ((TextBox)row.FindControl("txtRemarks1")).Text.ToString();
            paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
            paramtype = "int,int,varchar,int,int,Varchar";
            paramdir = "Input,Input,Input,Input,Input,Input";
            LeaveApproveGridMgr.Rows[row.DataItemIndex].Visible = false;
        }
        SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
        SqlObj = null;
      }
    }

    protected void Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet dss = new DataSet(); 
        DataSet ds3 = new DataSet();
        dss = SqlObj.GetTeamHead( Convert.ToInt16(Session["employeeid"].ToString()));
        if (dss.Tables[0].Rows.Count > 0)
        {
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
            {
                if (Approval.SelectedValue=="1")
                {
                    LeaveApproveGrid.Visible = true;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = true;
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    divMessage.Visible = true;
                    diverrorMsg2.Visible = false;
                    diverrorMsg3.Visible = false;
                    diverrorMsg4.Visible = false;

                }
                else if (Approval.SelectedValue == "2")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = true;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = false;
                    Div1.Visible = true;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    divMessage.Visible = false;
                    diverrorMsg2.Visible = true;
                    diverrorMsg3.Visible = false;
                    diverrorMsg4.Visible = false;
                }
                else if (Approval.SelectedValue == "3")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = true;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = false;
                    Div1.Visible = false;
                    Div2.Visible = true;
                    Div3.Visible = false;
                    divMessage.Visible = false;
                    diverrorMsg2.Visible = false;
                    diverrorMsg3.Visible = true;
                    diverrorMsg4.Visible = false;
                }
                else if (Approval.SelectedValue == "4")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = true;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = false;
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = true;
                    divMessage.Visible = false;
                    diverrorMsg2.Visible = false;
                    diverrorMsg3.Visible = false;
                    diverrorMsg4.Visible = true;
                } 
                
                //LeaveApproveGrid.Visible = true;
                //LeaveApproveGridMgr.Visible = false;
                //OTApproveL1.Visible = true;
                //OTApproveL2.Visible = false;
                //ShiftApproveL1.Visible = true;
                //ShiftApproveL2.Visible = false;
                SqlDs = SqlObj.ExcuteSelectProcedure("spGet_LeavePendingApprove", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                ds1 = SqlObj.ExcuteSelectProcedure("spGet_OTApprove", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                ds2 = SqlObj.ExcuteSelectProcedure("spGet_ShiftApprove", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                ds3 = SqlObj.ExcuteSelectProcedure("spGet_LeavePendingPermission", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                if (SqlDs != null)
                {
                    if (SqlDs.Tables[0].Rows.Count > 0)
                    {
                        LeaveApproveGrid.DataSource = SqlDs;
                        LeaveApproveGrid.DataBind();
                    }
                    else
                    {
                        LeaveApproveGrid.DataSource = SqlDs;
                        LeaveApproveGrid.DataBind();
                        divMessage.InnerHtml = "No Records Found.";
                    }
                }
                else
                    divMessage.InnerHtml = "Unable to Fetch Records or No Records Found";

                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        OTApproveL1.DataSource = ds1;
                        OTApproveL1.DataBind();
                    }
                    else
                    {
                        OTApproveL1.DataSource = ds1;
                        OTApproveL1.DataBind();
                        diverrorMsg2.InnerHtml = "No Records Found.";
                    }
                }
                else
                    diverrorMsg2.InnerHtml = "Unable to Fetch Records or No Records Found";

                if (ds2 != null)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ShiftApproveL1.DataSource = ds2;
                        ShiftApproveL1.DataBind();
                    }
                    else
                    {
                        ShiftApproveL1.DataSource = ds2;
                        ShiftApproveL1.DataBind();
                        diverrorMsg3.InnerHtml = "No Records Found.";
                    }
                }
                else
                    diverrorMsg3.InnerHtml = "Unable to Fetch Records or No Records Found";
                if (ds3 != null)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        PermissionLevel1.DataSource = ds3;
                        PermissionLevel1.DataBind();
                    }
                    else
                    {
                        PermissionLevel1.DataSource = ds3;
                        PermissionLevel1.DataBind();
                        diverrorMsg4.InnerHtml = "No Records Found.";
                    }
                }
                else
                    diverrorMsg4.InnerHtml = "Unable to Fetch Records or No Records Found";

                SqlObj = null;
                SqlDs = null;
                ds1 = null;
            }
            else
            {
                if (Approval.SelectedValue == "1")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = true;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = true;
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    divMessage.Visible = true;
                    diverrorMsg2.Visible = false;
                    diverrorMsg3.Visible = false;
                    diverrorMsg4.Visible = false;
                }
                else if (Approval.SelectedValue == "2")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = true;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = false;
                    Div1.Visible = true;
                    Div2.Visible = false;
                    Div3.Visible = false;
                    divMessage.Visible = false;
                    diverrorMsg2.Visible = true;
                    diverrorMsg3.Visible = false;
                    diverrorMsg4.Visible = false;
                }
                else if (Approval.SelectedValue == "3")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = true;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = false;
                    TitleDiv.Visible = false;
                    Div1.Visible = false;
                    Div2.Visible = true;
                    Div3.Visible = false;
                    divMessage.Visible = false;
                    diverrorMsg2.Visible = false;
                    diverrorMsg3.Visible = true;
                    diverrorMsg4.Visible = false;
                }
                else if (Approval.SelectedValue == "4")
                {
                    LeaveApproveGrid.Visible = false;
                    LeaveApproveGridMgr.Visible = false;
                    OTApproveL1.Visible = false;
                    OTApproveL2.Visible = false;
                    ShiftApproveL1.Visible = false;
                    ShiftApproveL2.Visible = false;
                    PermissionLevel1.Visible = false;
                    PermissionLevel2.Visible = true;
                    TitleDiv.Visible = false;
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Div3.Visible = true;
                    divMessage.Visible = false;
                    diverrorMsg2.Visible = false;
                    diverrorMsg3.Visible = false;
                    diverrorMsg4.Visible = true;
                } 

                //LeaveApproveGrid.Visible = false;
                //LeaveApproveGridMgr.Visible = true;
                //OTApproveL1.Visible = false;
                //OTApproveL2.Visible = true;
                //ShiftApproveL1.Visible = false;
                //ShiftApproveL2.Visible = true;
                SqlDs = SqlObj.ExcuteSelectProcedure("spGet_LeavePendinApprove1", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                ds1 = SqlObj.ExcuteSelectProcedure("spGet_OTApprove1", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                ds2 = SqlObj.ExcuteSelectProcedure("spGet_ShiftApprove1", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                ds3 = SqlObj.ExcuteSelectProcedure("spGet_LeavePendinPermission1", Session["employeeid"].ToString(), "@empid", "int", "input", CommandType.StoredProcedure);
                if (SqlDs != null)
                {
                    if (SqlDs.Tables[0].Rows.Count > 0)
                    {
                        LeaveApproveGridMgr.DataSource = SqlDs;
                        LeaveApproveGridMgr.DataBind();
                    }
                    else
                    {
                        LeaveApproveGridMgr.DataSource = SqlDs;
                        LeaveApproveGridMgr.DataBind();
                        divMessage.InnerHtml = "No Records Found.";
                    }
                }
                else
                    divMessage.InnerHtml = "Unable to Fetch Records or No Records Found";

                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        OTApproveL2.DataSource = ds1;
                        OTApproveL2.DataBind();
                    }
                    else
                    {
                        OTApproveL2.DataSource = ds1;
                        OTApproveL2.DataBind();
                        diverrorMsg2.InnerHtml = "No Records Found.";
                    }
                }
                else
                    diverrorMsg2.InnerHtml = "Unable to Fetch Records or No Records Found";
                if (ds2 != null)
                {
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        ShiftApproveL2.DataSource = ds2;
                        ShiftApproveL2.DataBind();
                    }
                    else
                    {
                        ShiftApproveL2.DataSource = ds2;
                        ShiftApproveL2.DataBind();
                        diverrorMsg3.InnerHtml = "No Records Found.";
                    }
                }
                else
                    diverrorMsg3.InnerHtml = "Unable to Fetch Records or No Records Found";
                if (ds3 != null)
                {
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        PermissionLevel2.DataSource = ds3;
                        PermissionLevel2.DataBind();
                    }
                    else
                    {
                        PermissionLevel2.DataSource = ds3;
                        PermissionLevel2.DataBind();
                        diverrorMsg4.InnerHtml = "No Records Found.";
                    }
                }
                else
                    diverrorMsg4.InnerHtml = "Unable to Fetch Records or No Records Found";
                SqlObj = null;
                SqlDs = null;
            }
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
    protected void FollowUpMailSent(string Status)
    {
        XmlDocument xd = new XmlDocument();
        XmlNode childnode = null;
        XmlNode xn = null;
        DataSet dst = new DataSet();
        datasourceSQL dsql = new datasourceSQL();
        dst = dsql.ExcProcedure("spGet_EmpEmailDetails", new string[,] { { "@empid", Session["employeeid"].ToString() } }, CommandType.StoredProcedure);
        //string fromaddress = ConfigurationManager.AppSettings["SAMFollowupmail_fromaddress"].ToString();
        DataSet ds1 = new DataSet();
        if (dst != null)
        {

            try
            {
                foreach (DataRow row in dst.Tables[0].Rows)
                {
                    xd.Load(Server.MapPath("").ToString() + @"\LeaveApplyMail.xml");
                    xn = xd.DocumentElement.SelectSingleNode("//Employee").FirstChild;

                    if ((xn != null) && (row != null))
                    {
                        childnode = xn.SelectSingleNode("to");
                        txtTo.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        // lblToAddress.Text = (Request.QueryString["AEMAIL"].ToString().Trim() != "") ? Request.QueryString["AEMAIL"].ToString().Trim() : (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        txtTo.Text = row["Email_Off"].ToString().Trim();
                        childnode = xn.SelectSingleNode("from");
                        txtFrom.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        txtFrom.Text = row["Email_App"].ToString().Trim();
                        childnode = xn.SelectSingleNode("body1");
                        txtBody.Text = (childnode != null && childnode.InnerText != "") ? childnode.InnerText.ToString() : "";
                        txtBody.Text = (childnode != null && childnode.InnerXml != "") ? childnode.InnerXml.ToString() : "";
                        txtBody.Text = txtBody.Text.Replace("[EmpName]", row["EmpName"].ToString().Trim());
                        txtBody.Text = txtBody.Text.Replace("[NAME]", row["App"].ToString().Trim());
                    }

                }
            }

            catch (Exception ex)
            { throw ex; }
            finally
            { xd = null; childnode = null; xn = null; }
        }
    }
    protected void OTApproveL1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();

        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in OTApproveL1.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,Int";
                        paramdir = "Input,Input,Input,Input";
                        //OTApproveL1.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,Int";
                        paramdir = "Input,Input,Input,Input";
                        //OTApproveL1.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spOT_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                    
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,Int";
                paramdir = "Input,Input,Input,Input";
                OTApproveL1.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,Int";
                paramdir = "Input,Input,Input,Input";
                OTApproveL1.Rows[row.DataItemIndex].Visible = false;
            }
            SqlObj.ExcuteProcedure("spOT_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            SqlObj = null;
        }
    }

    protected void OTApproveL2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();

        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in OTApproveL2.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    try
                    {
                        int ses = Convert.ToInt16(Session["employeeid"].ToString());
                        dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), ses);
                    }
                    catch(Exception ex)
                    {

                    }
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,Int";
                        paramdir = "Input,Input,Input,Input";
                        //OTApproveL2.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,int";
                        paramdir = "Input,Input,Input,Input";
                        //OTApproveL2.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spOT_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                    //SqlObj = null;
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,Int";
                paramdir = "Input,Input,Input,Input";
                OTApproveL2.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,int";
                paramdir = "Input,Input,Input,Input";
                OTApproveL2.Rows[row.DataItemIndex].Visible = false;
            }
            SqlObj.ExcuteProcedure("spOT_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            SqlObj = null;
        }
    }
    protected void ShiftApproveL1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in ShiftApproveL1.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,Int";
                        paramdir = "Input,Input,Input,Input";
                        //ShiftApproveL1.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,Int";
                        paramdir = "Input,Input,Input,Input";
                        //ShiftApproveL1.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spShift_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                   
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,Int";
                paramdir = "Input,Input,Input,Input";
                ShiftApproveL1.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,Int";
                paramdir = "Input,Input,Input,Input";
                ShiftApproveL1.Rows[row.DataItemIndex].Visible = false;
            }
            SqlObj.ExcuteProcedure("spShift_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            SqlObj = null;
        }
    }

    protected void ShiftApproveL2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in ShiftApproveL2.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,Int";
                        paramdir = "Input,Input,Input,Input";
                        //ShiftApproveL2.Rows[row.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                        paramname = "@id,@approveby_empid,@atype,@Status";
                        paramtype = "int,int,varchar,int";
                        paramdir = "Input,Input,Input,Input";
                        //ShiftApproveL2.Rows[row.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spShift_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                   
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,Int";
                paramdir = "Input,Input,Input,Input";
                ShiftApproveL2.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject" + "," + i;
                paramname = "@id,@approveby_empid,@atype,@Status";
                paramtype = "int,int,varchar,int";
                paramdir = "Input,Input,Input,Input";
                ShiftApproveL2.Rows[row.DataItemIndex].Visible = false;
            }
            SqlObj.ExcuteProcedure("spShift_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            SqlObj = null;
        }
    }
    protected void PermissionLevel1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();
        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";

        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;
        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in PermissionLevel1.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + ",Permission";
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,Int,Int,Varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //PermissionLevel1.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + ",Permission";
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,int,Int,Varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //PermissionLevel1.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                    
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + ",Permission";
                paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                paramtype = "int,int,varchar,Int,Int,Varchar";
                paramdir = "Input,Input,Input,Input,Input,Input";
                PermissionLevel1.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + ",Permission";
                paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                paramtype = "int,int,varchar,int,Int,Varchar";
                paramdir = "Input,Input,Input,Input,Input,Input";
                PermissionLevel1.Rows[row.DataItemIndex].Visible = false;
            }
            SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
            SqlObj = null;
        }
    }

    protected void PermissionLevel2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        datasourceSQL SqlObj = new datasourceSQL();

        string paramCol = "";
        string paramname = "";
        string paramtype = "";
        string paramdir = "";
        GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).Parent.Parent;

        if (e.CommandName == "imgApprove" || e.CommandName == "imgReject")
        {
            foreach (GridViewRow row2 in PermissionLevel2.Rows)
            {
                HtmlInputCheckBox chk = ((HtmlInputCheckBox)row2.FindControl("chkFillAll"));
                ImageButton hisId = ((ImageButton)row2.FindControl("BtnApprove"));
                if (chk.Checked == true)
                {
                    string Empid = ((Label)row2.FindControl("txtEmpid")).Text.ToString();
                    int i;
                    DataSet dss = new DataSet();
                    dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
                    if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                        i = 1;
                    else
                        i = 2;
                    if (e.CommandName == "imgApprove")
                    {
                        //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + ",Permission";
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,Int,Int,varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //PermissionLevel2.Rows[row2.DataItemIndex].Visible = false;
                    }
                    if (e.CommandName == "imgReject")
                    {
                        //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                        paramCol = hisId.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + ",Permission";
                        paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                        paramtype = "int,int,varchar,int,int,Varchar";
                        paramdir = "Input,Input,Input,Input,Input,Input";
                        //PermissionLevel2.Rows[row2.DataItemIndex].Visible = false;
                    }
                    SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
                    
                }
            }
            SqlObj = null;
            Status_SelectedIndexChanged(sender, e);
        }
        else
        {
            string Empid = ((Label)row.FindControl("txtEmpid")).Text.ToString();
            int i;
            DataSet dss = new DataSet();
            dss = SqlObj.GetApprLevel(Convert.ToInt16(Empid), Convert.ToInt16(Session["employeeid"].ToString()));
            if (dss.Tables[0].Rows[0]["Position"].ToString() == "TL")
                i = 1;
            else
                i = 2;
            if (e.CommandName == "Approve")
            {
                //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Approve,0" + "," + i + ",Permission";
                paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                paramtype = "int,int,varchar,Int,Int,varchar";
                paramdir = "Input,Input,Input,Input,Input,Input";
                PermissionLevel2.Rows[row.DataItemIndex].Visible = false;
            }
            if (e.CommandName == "Reject")
            {
                //GridViewRow row1 = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                paramCol = e.CommandArgument.ToString() + "," + Session["employeeid"].ToString() + ",Reject,0" + "," + i + ",Permission";
                paramname = "@Lhisid,@approveby_empid,@atype,@redirectid,@Status,@Remarks";
                paramtype = "int,int,varchar,int,int,Varchar";
                paramdir = "Input,Input,Input,Input,Input,Input";
                PermissionLevel2.Rows[row.DataItemIndex].Visible = false;
            }

        SqlObj.ExcuteProcedure("spLeave_Approve", paramCol, paramname, paramtype, paramdir, CommandType.StoredProcedure);
        SqlObj = null;
    }
	}
    protected void Approval_SelectedIndexChanged(object sender, EventArgs e)
    {
        Status_SelectedIndexChanged(sender, e);
    }
}


