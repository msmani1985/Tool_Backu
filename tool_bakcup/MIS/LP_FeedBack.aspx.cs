using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class LP_FeedBack : System.Web.UI.Page
{
    public int id = 1;
    Non_Launch nonLa = new Non_Launch();
    datasourceSQL SqlObj = new datasourceSQL();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind(); 
        if (!Page.IsPostBack)
        {
            this.popscreen();
        }
    }
    public void popscreen()
    {
        DataSet ds = new DataSet();
        ds = nonLa.getAllCustomers();
        drpCustomer.DataSource = ds;
        drpCustomer.DataTextField = ds.Tables[0].Columns[1].ToString();
        drpCustomer.DataValueField = ds.Tables[0].Columns[0].ToString();
        drpCustomer.DataBind();
        drpCustomer.Items.Insert(0, new ListItem("-- select --", "0"));
        drpLocation.Items.Insert(0, new ListItem("-- select --", "0"));

        DataSet sw = nonLa.getAllSoftware();
        lboxSW.DataSource = sw;
        lboxSW.DataTextField = sw.Tables[0].Columns[1].ToString();
        lboxSW.DataValueField = sw.Tables[0].Columns[0].ToString();
        lboxSW.DataBind();

        Load();
        this.showPanel(div_Summary_details);
    }
    private void showPanel(HtmlGenericControl panel)
    {
        switch (panel.ID)
        {
            case "div_Summary_details":
                miSummaryDetails.Attributes.Add("class", "current");
                miFeedbackDetails.Attributes.Add("class", "");
                this.div_Summary_details.Visible = true;
                this.div_Feedback_details.Visible = false;
                break;
            case "div_Feedback_details":
                miSummaryDetails.Attributes.Add("class", "");
                miFeedbackDetails.Attributes.Add("class", "current");
                this.div_Summary_details.Visible = false;
                this.div_Feedback_details.Visible = true;
                break;
        }
    }
    protected void drpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = nonLa.GetLocationCust(drpCustomer.SelectedValue);
        if (ds != null)
        {
            drpLocation.Enabled = true;
            drpLocation.DataSource = ds;
            drpLocation.DataValueField = ds.Tables[0].Columns[3].ToString();
            drpLocation.DataTextField = ds.Tables[0].Columns[4].ToString();
            drpLocation.DataBind();
        }
        else
        {
            drpLocation.Items.Insert(0, new ListItem("-- select --", "0"));
            drpLocation.SelectedValue = "0";
            drpLocation.Enabled = false;
        }
    }
    public void Load()
    {
        DataSet ds = new DataSet();
        ds = nonLa.GetLP_FBSummary();
        if (ds != null)
        {
            GvFB.DataSource = ds.Tables[0];
            GvFB.DataBind();
            dtable=ds.Tables[0].Copy();
        }
        else
        {
            GvFB.DataSource = null;
            GvFB.DataBind();
        }
    }
    protected void txtJobID_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = nonLa.getProNameLP(txtJobID.Text.Trim());
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
            }
        }
    }
    private bool validateScreen()
    {
        int i = 1;
        string sMessage = "";
        if (drpCustomer.SelectedItem.Value == "0") sMessage += i++ + ". Select a Customer\\r\\n";
        if (txtProjectName.Text.Trim() == "") sMessage += i++ + ". Enter Project Name\\r\\n";
        if (txtPEName.Text.Trim() == "") sMessage += i++ + ". Enter PE Name\\r\\n";
        if (txtPdate.Text.Trim() == "") sMessage += i++ + ". Enter Date\\r\\n";
       
        if (i > 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + sMessage + "');</script>");
            return false;
        }
        return true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (validateScreen())
            {
                string joinstring = "/";
                string newdate = String.Empty;
                string newdate1 = String.Empty;
                if (txtPdate.Text != "")
                {
                    string[] tempsplit = txtPdate.Text.Split('/');
                    newdate = tempsplit[1] + joinstring + tempsplit[0] + joinstring + tempsplit[2];
                    DateTime NextDate = Convert.ToDateTime(newdate);
                }
                else
                {
                    newdate = "";
                }
                if (txtInformDate.Text != "")
                {
                    string[] tempsplit1 = txtInformDate.Text.Split('/');
                    newdate1 = tempsplit1[1] + joinstring + tempsplit1[0] + joinstring + tempsplit1[2];
                    DateTime NextDate1 = Convert.ToDateTime(newdate1);
                }
                else
                {
                    newdate1 = "";
                }
                string informperids = String.Empty;
                if (txtInformPersons.Text != "")
                    informperids = hfInfEmpId.Value.ToString().Substring(0, hfInfEmpId.Value.Length - 2).Replace(", ", "-");
                else
                    informperids = "";

                if (hfFBID.Value == "" && btnSave.Text == "Save")
                {
                    string inproc = "spInsertLPFeedBack";
                    string[,] pname = { 
                            {"@Custno", drpCustomer.SelectedValue}, {"@Location_id",drpLocation.SelectedValue}, 
                            {"@FP_PN",dropFeedBackYN.SelectedValue }, {"@JobID",txtJobID.Text}, 
                            {"@ProjectName",txtProjectName.Text },{"@RecDate",newdate.ToString()},
                            {"@Conno",hfprojectEditorId.Value},{"@FeedBack",txtFB.Text},{"@KeyElements",txtKeyEle.Text},
                            {"@Domain",dropDomain.SelectedValue},{"@ResPersonNames",txtResPerson.Text},{"@ResPersonIDs",hfEmpId.Value.ToString().Substring(0, hfEmpId.Value.Length-2).Replace(", ","-")},
                            {"@Remarks",dropRemarks.SelectedValue},{"@ErrorAnalysis",txtErrorAns.Text},{"@PreAction",txtPreAction.Text},
                            {"@InformedYN",dropInformStatus.SelectedValue},{"@InformedTime",newdate1.ToString()},
                            {"@CreatedBy",Session["employeeid"].ToString()},{"@IsExist","OUTPUT"},{"@InformedPerNames",txtInformPersons.Text},
                            {"@InformedPerIDs",informperids},{"@InformedBy",txtInformPersons.Text},
                            {"@InvPerNames",txtInvPer.Text},{"@InvPerIDs",hfInvEmpID.Value.ToString().Substring(0, hfInvEmpID.Value.Length-2).Replace(", ","-")}
                           };
                    int val = this.SqlObj.ExcSP(inproc, pname, CommandType.StoredProcedure);

                    string taskitem = "";
                    for (int j = 0; j < lboxSW.Items.Count; j++)
                    {
                        if (lboxSW.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxSW.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxSW.Items[j].Value;
                        }
                    }
                    string taskproc = "spAdd_PlatForm_FB";
                    string[,] tname = { { "@Platform", taskitem }, { "@FB_ID", val.ToString() } };
                    int task12 = this.SqlObj.ExcSP(taskproc, tname, CommandType.StoredProcedure);
                    btnClear_Click(sender, e);
                    btnSave.Text = "Update";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Successfully Inserted');</script>");
                }
                else
                {
                    string inproc = "spUpdateLPFeedBack";
                    string[,] pname = { 
                            {"@FB_ID", hfFBID.Value},{"@Custno", drpCustomer.SelectedValue}, {"@Location_id",drpLocation.SelectedValue}, 
                            {"@FP_PN",dropFeedBackYN.SelectedValue }, {"@JobID",txtJobID.Text}, 
                            {"@ProjectName",txtProjectName.Text },{"@RecDate",newdate.ToString()},
                            {"@Conno",hfprojectEditorId.Value},{"@FeedBack",txtFB.Text},{"@KeyElements",txtKeyEle.Text},
                            {"@Domain",dropDomain.SelectedValue},{"@ResPersonNames",txtResPerson.Text},{"@ResPersonIDs",hfEmpId.Value.ToString().Substring(0, hfEmpId.Value.Length-2).Replace(", ","-")},
                            {"@Remarks",dropRemarks.SelectedValue},{"@ErrorAnalysis",txtErrorAns.Text},{"@PreAction",txtPreAction.Text},
                            {"@InformedYN",dropInformStatus.SelectedValue},{"@InformedTime",newdate1.ToString()},
                            {"@IsExist","OUTPUT"},{"@InformedPerNames",txtInformPersons.Text},
                            {"@InformedPerIDs",informperids},{"@InformedBy",txtInformPersons.Text},
                            {"@InvPerNames",txtInvPer.Text},{"@InvPerIDs",hfInvEmpID.Value.ToString().Substring(0, hfInvEmpID.Value.Length-2).Replace(", ","-")}
                           };
                    int val = this.SqlObj.ExcSP(inproc, pname, CommandType.StoredProcedure);
                    string taskitem = "";
                    for (int j = 0; j < lboxSW.Items.Count; j++)
                    {
                        if (lboxSW.Items[j].Selected == true)
                        {
                            if (taskitem == "")
                                taskitem = lboxSW.Items[j].Value;
                            else
                                taskitem = taskitem + '-' + lboxSW.Items[j].Value;
                        }
                    }
                    string taskproc = "spAdd_PlatForm_FB";
                    string[,] tname = { { "@Platform", taskitem }, { "@FB_ID", hfFBID.Value } };
                    int task12 = this.SqlObj.ExcSP(taskproc, tname, CommandType.StoredProcedure);
                    btnClear_Click(sender, e);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Updated Successfully');</script>");
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtJobID.Text = "";
        txtProjectName.Text = "";
        txtResPerson.Text = "";
        txtFB.Text = "";
        txtKeyEle.Text = "";
        hfprojectEditorId.Value = "";
        txtErrorAns.Text = "";
        txtPreAction.Text = "";
        txtPreAction.Text = "";
        txtInvPer.Text = "";
        txtInformDate.Text = "";
        txtPdate.Text = "";
        txtInformPersons.Text = "";
        txtPEName.Text = "";
        hfEmpId.Value = "";
        hfInfEmpId.Value = "";
        hfFBID.Value = "";
        hfInvEmpID.Value = "";
        lboxSW.ClearSelection();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hfFBID.Value != "")
        {
            string inproc = "spFinalChkFeedback";
            string[,] pname = { 
                                {"@FB_ID", hfFBID.Value}, {"@Empid",Session["employeeid"].ToString()}
                              };
            int val = this.SqlObj.ExcSP(inproc, pname, CommandType.StoredProcedure);
            //nonLa.UpdateFBApproved(hfFBID.Value, "2461");//Session["employeeid"].ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "<script language='javascript'>alert('Updated Successfully');</script>");
            lnkFeedbackDetails_Click(sender, e);
        }
    }
    protected void LinkSummary_Click(object sender, EventArgs e)
    {
        Load();
        this.showPanel(div_Summary_details);
    }
    public void LoadFB(string sJobID)
    {
        lboxSW.ClearSelection();
        DataSet dsFB = nonLa.GetLP_FB_ByID(sJobID);
        DataRow row = dsFB.Tables[0].Rows[0];
        txtJobID.Text = row["JOBID"].ToString();
        txtProjectName.Text = row["ProjectName"].ToString();
        txtResPerson.Text = row["ResPersonNames"].ToString();
        txtFB.Text = row["FeedBack"].ToString();
        txtKeyEle.Text = row["KeyElements"].ToString();
        hfprojectEditorId.Value = row["Conno"].ToString();
        txtErrorAns.Text = row["ErrorAnalysis"].ToString();
        txtPreAction.Text = row["PreAction"].ToString();
        txtInvPer.Text = row["Invpernames"].ToString();
        txtInformDate.Text = row["InformedTime"].ToString();
        txtPdate.Text = row["RecDate"].ToString();
        txtInformPersons.Text = row["InformedPerNames"].ToString();
        txtPEName.Text = row["PENAME"].ToString();
        hfEmpId.Value = row["ResPersonIDs"].ToString().Trim()+", ";
        hfInfEmpId.Value = row["InformedPerIDs"].ToString().Trim() + ", ";
        hfInvEmpID.Value = row["InvPerIDs"].ToString().Trim() + ", ";
        drpCustomer.SelectedValue = row["CUSTNO"].ToString();
        DataSet ds = nonLa.GetLocationCust(drpCustomer.SelectedValue);
        drpLocation.Enabled = true;
        drpLocation.DataSource = ds;
        drpLocation.DataValueField = ds.Tables[0].Columns[3].ToString();
        drpLocation.DataTextField = ds.Tables[0].Columns[4].ToString();
        drpLocation.DataBind();
        drpLocation.SelectedValue = row["Location_ID"].ToString();
        dropRemarks.SelectedValue = row["Remarks"].ToString();
        dropInformStatus.SelectedValue = row["InformedYN"].ToString();
        dropFeedBackYN.SelectedValue = row["FP_PN"].ToString();
        dropDomain.SelectedValue = row["Domain"].ToString();
        DataSet ds1 = nonLa.GetFBPlatform(sJobID);
        if (ds1 != null)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    lboxSW.Items[lboxSW.Items.IndexOf(lboxSW.Items.FindByValue(ds1.Tables[0].Rows[i]["Soft_ID"].ToString()))].Selected = true;
            }
        }
        btnSave.Text = "Update";
        if (row["ApprovedBy"].ToString() != "")
        {
            btnSave.Visible = false;
            btnSubmit.Visible = false;
            btnClear.Visible = false;
        }
        else
        {
            btnSave.Visible = true;
            btnSubmit.Visible = true;
            btnClear.Visible = true;
        }
    }
    protected void lnkFeedbackDetails_Click(object sender, EventArgs e)
    {
        if (hfFBID.Value != "")
        {
            LoadFB(hfFBID.Value);
        }
        this.showPanel(div_Feedback_details);
    }
    protected void GvFB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Style.Add("cursor", "pointer");
            e.Row.Attributes["onmouseover"] =
                "javascript:setMouseOverColor(this);";
            e.Row.Attributes["onmouseout"] =
                "javascript:setMouseOutColor(this);";
            e.Row.Attributes["onclick"] =
                "javascript:setColor(this,\"" + ((HiddenField)e.Row.FindControl("hfgvFBID")).Value.Trim() + "\");";

            Label lblAppBy = e.Row.FindControl("lblAppBy") as Label;
            if(lblAppBy.Text=="")
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
        }
    }
    protected void exportExl_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int i = 1;
            if (dtable != null && dtable.Rows.Count > 0)
            {
                StringBuilder sbData = new StringBuilder();
                sbData.Append("<table border='1'>");
                sbData.Append("<tr valign='top'><td colspan='19' align='center'><h4>Launch FeedBack Report</h4></td><tr>");
                sbData.Append("<tr valign='top'><td bgcolor='silver'><b>Sl. No.</b></td><td bgcolor='silver'><b>Jobid</b></td><td bgcolor='silver'><b>Customer</b></td><td bgcolor='silver'><b>Project Title</b></td><td bgcolor='silver'><b>Project Editor</b></td><td bgcolor='silver'><b>Date</b></td><td bgcolor='silver'><b>Platform</b></td><td bgcolor='silver'><b>FeedBack P/N?</b></td><td bgcolor='silver'><b>FeedBack</b></td><td bgcolor='silver'><b>KeyElements</b></td><td bgcolor='silver'><b>Domain</b></td><td bgcolor='silver'><b>Remarks</b></td><td bgcolor='silver'><b>ErrorAnalysis</b></td><td bgcolor='silver'><b>PreAction</b></td><td bgcolor='silver'><b>InformedYN</b></td><td bgcolor='silver'><b>InformedTime</b></td><td bgcolor='silver'><b>Invpernames</b></td><td bgcolor='silver'><b>ResPersonNames</b></td><td bgcolor='silver'><b>InformedPerNames</b></td></tr>");
                foreach (DataRow r in dtable.Rows)
                {
                    sbData.Append("<tr valign='top'>");
                    sbData.Append("<td>" + i + "</td>");
                    sbData.Append("<td>" + r["Jobid"] + "</td>");
                    sbData.Append("<td>" + r["CUSTNAME"] + "</td>");
                    sbData.Append("<td>" + r["Projectname"] + "</td>");
                    sbData.Append("<td>" + r["PENAME"] + "</td>");
                    sbData.Append("<td>" + r["Dates"] + "</td>");
                    sbData.Append("<td>" + r["Platform"] + "</td>");
                    sbData.Append("<td>" + r["PN_Status"] + "</td>");
                    sbData.Append("<td>" + r["FeedBack"] + "</td>");
                    sbData.Append("<td>" + r["KeyElements"] + "</td>");
                    sbData.Append("<td>" + r["Domain"] + "</td>");
                    sbData.Append("<td>" + r["Remarks"] + "</td>");
                    sbData.Append("<td>" + r["ErrorAnalysis"] + "</td>");
                    sbData.Append("<td>" + r["PreAction"] + "</td>");
                    sbData.Append("<td>" + r["InformedYN"] + "</td>");
                    sbData.Append("<td>" + r["InformedTime"] + "</td>");
                    sbData.Append("<td>" + r["Invpernames"] + "</td>");
                    sbData.Append("<td>" + r["ResPersonNames"] + "</td>");
                    sbData.Append("<td>" + r["InformedPerNames"] + "</td>");
                    sbData.Append("</tr>");
                    i = i + 1;
                }
                sbData.Append("</table>");
                Response.Clear();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Launch_Received_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls"));
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                Response.Write(sbData.ToString());
                Response.Flush();
                Response.Close();
            }
        }
        catch (Exception Ex)
        {
            
        }
    }
}