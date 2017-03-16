using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LP_DespatchTool : System.Web.UI.Page
{
    Non_Launch nonLa = new Non_Launch();
    private static DataTable dtable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (rbLaunch.SelectedValue == "1")
        {
            ds = nonLa.GetLPUnDesJobs(txtJobID.Text.Trim());
        }
        else
        {
            ds = nonLa.GetNLUnDesJobs(txtJobID.Text.Trim());
        }
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblError.Text = "";
            grdJobDes.DataSource = ds;
            grdJobDes.DataBind();
        }
        else
        {
            lblError.Text = "Files not Found...";
            grdJobDes.DataSource = null;
            grdJobDes.DataBind();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtJobID.Text = "";
        lblError.Text = "";
        grdJobDes.DataSource = null;
        grdJobDes.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grw in grdJobDes.Rows)
        {
            Label Pro_ID = (Label)grw.FindControl("lblid");
            Label FP_ID = (Label)grw.FindControl("lblFP_ID");
            CheckBox txtdepends = (CheckBox)grw.FindControl("Check");
            bool status = txtdepends.Checked;
            int d;
            if (status == true)
                d = 1;
            else
                d = 0;
            if (d == 1)
            {
                if (rbLaunch.SelectedValue == "1")
                {
                    nonLa.UpdateUnDesLP(Pro_ID.Text,FP_ID.Text);
                }
                else
                {
                    nonLa.UpdateUnDesNL(Pro_ID.Text, FP_ID.Text);
                }
            }

            DataSet ds = new DataSet();
            ds = nonLa.GetLPMaxDelAmends(Pro_ID.Text);
            if(ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del" && ds.Tables[0].Rows[0]["JobNo"].ToString().Trim() == "")
                {
                    DataSet ms = new DataSet();
                    ms = nonLa.GetMailStatusLP(Pro_ID.Text);
                    if (ms == null)
                    {
                        MailMessage oMsg = new MailMessage();
                        SmtpClient oSmtp = new SmtpClient();
                        oMsg = new MailMessage();
                        oSmtp.Host = "smtp.gmail.com";
                        oSmtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Invoicemail_port"]);
                        oMsg.From = new MailAddress("software@datapage.org");
                        oSmtp.Credentials = new System.Net.NetworkCredential(oMsg.From.ToString(), ConfigurationManager.AppSettings["accounts_password"].ToString());
                        oMsg.CC.Add("projects@datapage.org");
                        oMsg.To.Add("swapna@datapage.org");
                        oMsg.CC.Add("software@datapage.org");
                        oMsg.Subject = ds.Tables[0].Rows[0]["Jobid"].ToString().Trim() + " - Work Order number Required";
                        string strLog = string.Empty;
                        strLog = "Hi Team," + "\r\n\r\n";
                        strLog += " Please Enter the Work Order number for this Project: (" + ds.Tables[0].Rows[0]["Jobid"].ToString().Trim() + " - " + ds.Tables[0].Rows[0]["ProjectName"].ToString().Trim() + ") in Launch Overview.  \r\n\r\n";
                        strLog += "Thanks and Regards, " + "  \r\n";
                        strLog += "Technical Team,  \r\n";
                        strLog += "Datapage  \r\n";
                        oMsg.Body = strLog.ToString().Trim();
                        oSmtp.EnableSsl = true;
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        oSmtp.Send(oMsg);
                        showmessage("Mailsent Successfully");
                        if (ds.Tables[0].Rows[0]["LP"].ToString().Trim() == "Y")
                        {
                            nonLa.UpdateDespatchLP(Pro_ID.Text);
                            nonLa.UpdateMailStatusLP(Pro_ID.Text);
                        }
                        else
                        {
                            nonLa.UpdateDespatchNL(Pro_ID.Text);
                            nonLa.UpdateMailStatusNL(Pro_ID.Text);
                        }
                    }
                }
                else if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "Del" && ds.Tables[0].Rows[0]["JobNo"].ToString().Trim() != "")
                {
                    if (ds.Tables[0].Rows[0]["LP"].ToString().Trim() == "Y")
                    {
                        nonLa.UpdateDespatchLP(Pro_ID.Text);
                    }
                    else
                    {
                        nonLa.UpdateDespatchNL(Pro_ID.Text);
                    }
                }
            }
        }
        btnSearch_Click(sender, e);
    }
    private void showmessage(string msg)
    {
        if (!string.IsNullOrEmpty(msg))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('" + msg + "');</script>");
        }
    }
}