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
using System.Net;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        testmac.Value = Request.UserHostAddress.ToString();
        //testmac.Value ="Test " + Request.ServerVariables["REMOTE_HOST"].ToString();
        if (Request.Url.ToString().IndexOf("203.101.73.205:2080") > 0)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Open", "<script language='javascript'>alert('SanLucasIP');</script>");
            img_mis1.Src = "images/Sanlucasimg1.jpg";
            img_mis2.Src = "images/Sanlucasimg2.gif";
            div_content_Sanlucas.Visible = true;
            div_content.Visible = false;
            content_sanlucas.Style.Add(HtmlTextWriterStyle.Display, "");
            
            //img_mis3.Src = "images/Sanlucasimg3.gif";
        }
        else
        { div_content_Sanlucas.Visible = false; div_content.Visible = true; img_mis1.Style.Add(HtmlTextWriterStyle.Display, ""); btnforgetpass.Visible = true; }
        if (Request.Url.ToString().IndexOf("203.101.73.205") > 0)
            btnforgetpass.Visible = false;

        //indiatime.Text = DateTime.Now.ToString() ;
        //dublintime.Text = (DateTime.UtcNow.AddHours(1)).ToString()  ;
        try
        {
        string sClientAddress = "";
        //sClientAddress += "UserHostAddress: "+ Request.UserHostAddress + "; HostName: ";
        //sClientAddress += Request.UserHostName + "; Browser: ";
        sClientAddress += "Browser: " + Request.Browser.Browser + "; MajorVersion: ";
        sClientAddress += Request.Browser.MajorVersion + "; MinorVersion: ";
        sClientAddress += Request.Browser.MinorVersion + "; Version: ";
        sClientAddress += Request.Browser.Version + "; Remote IP: ";
        //sClientAddress += Request.Browser.Type + "; Remote IP: ";
        sClientAddress += Request.ServerVariables["REMOTE_HOST"] ;
        IPAddress ipadd = IPAddress.Parse(Request.ServerVariables["REMOTE_HOST"]);
        //IPHostEntry ipHS = Dns.GetHostByAddress(ipadd); //method obsoleted 
        IPHostEntry ipHS = Dns.GetHostEntry(ipadd);  
        divClientDetails.InnerHtml = sClientAddress + "; HostName: " + ipHS.HostName ;
        }
        catch(Exception ex)
        {
            divClientDetails.InnerHtml = "";
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtusername.Text.Trim() == "") 
            return;
        if (txtpassword.Text.Trim() == "")
            return;
        biz_emp_menu_mgmt biz = new biz_emp_menu_mgmt();
        DataSet ds = new DataSet();
        ds = biz.validatelogin(txtusername.Text.Trim(), txtpassword.Text.Trim(),testmac.Value.ToString());
        if (ds != null)
        {
            if (ds.Tables["USERINFO"].Rows.Count > 0)
            {
                if (ds.Tables["USERINFO"].Rows.Count == 1)
                {
                    Session["Shift"] = ds.Tables["USERINFO"].Rows[0]["Shift"].ToString();
                    Session["username"] = ds.Tables["USERINFO"].Rows[0]["username"].ToString();
                    Session["password"] = ds.Tables["USERINFO"].Rows[0]["password"].ToString();
                    Session["employeeid"] = ds.Tables["USERINFO"].Rows[0]["employee_id"].ToString();
                    Session["fname"] = ds.Tables["USERINFO"].Rows[0]["fname"].ToString();
                    Session["sname"] = ds.Tables["USERINFO"].Rows[0]["surname"].ToString();
                    Session["fullname"] = Session["fname"].ToString() + ' ' + Session["sname"].ToString(); 
                    Session["designationid"] = ds.Tables["USERINFO"].Rows[0]["designation_id"].ToString();
                   // Session["Report_To"] = ds.Tables["USERINFO"].Rows[0]["Report_To"].ToString();
                    Session["departmentid"] = ds.Tables["USERINFO"].Rows[0]["department_id"].ToString();
                    //Session["departmentid"] = ds.Tables["USERINFO"].Rows[0]["DNO"].ToString();
                    Session["departmentname"] = ds.Tables["USERINFO"].Rows[0]["employee_team_name"].ToString();
                    Session["employeenumber"] = ds.Tables["USERINFO"].Rows[0]["employee_number"].ToString();
                    Session["employeename"] = ds.Tables["USERINFO"].Rows[0]["fname"].ToString().Trim() + "  " + ds.Tables["USERINFO"].Rows[0]["surname"].ToString().Trim();
                    Session["employeeemail"] = ds.Tables["USERINFO"].Rows[0]["email_address"].ToString();
                    Session["locationid"] = ds.Tables["USERINFO"].Rows[0]["location_id"].ToString();
                    Session["webuser"] = ds.Tables["USERINFO"].Rows[0]["web_user"].ToString();
                    Session["timesheet"] = ds.Tables["USERINFO"].Rows[0]["Time_Sheet"].ToString();
                    Session["designationname"] = ds.Tables["USERINFO"].Rows[0]["designation_name"].ToString();
                    Session["firstvisit"] = ds.Tables["USERINFO"].Rows[0]["last_visit"].ToString();
                    Session["date_of_birth"] = ds.Tables["USERINFO"].Rows[0]["date_of_birth"].ToString();
                    //subbu 10June2009  For Live Work
                   // Session["Emp_DNO"] = ds.Tables["USERINFO"].Rows[0]["DNO"].ToString();
                    Session["SQL_empno"] = ds.Tables["USERINFO"].Rows[0]["empno"].ToString();
                    //royson 03feb09
                    Session["employeeteamid"] = ds.Tables["USERINFO"].Rows[0]["employee_team_id"].ToString();
                    //
                    //subbu 26Oct2010 for sanlucas customer
                    if (ds.Tables["USERINFO"].Rows[0]["location_id"] != null && ds.Tables["USERINFO"].Rows[0]["location_id"].ToString() == "5")
                        Session["customerid"] = ds.Tables["USERINFO"].Rows[0]["customer_id"].ToString();
                    Session["useraccess"] = null;
                    if (Session["locationid"].ToString() == "1")
                        Session["location"] = "d";
                    else
                        Session["location"] = "i";
                    Session["accesslevel"] = (ds.Tables[0].Rows[0]["access_level"] != null && ds.Tables[0].Rows[0]["access_level"].ToString() != "") ? ds.Tables[0].Rows[0]["access_level"].ToString() : "0";
                        ds = null;
                    errMsg.Visible = true;
                    errMsg.Text = "";
                    if (Convert.ToBoolean(Session["webuser"].ToString()) == true)
                    {
                        if (Request.Url.ToString().IndexOf("203.101.73.205:2080") > 0 && Session["customerid"] == null)
                        { errMsg.Text = "You are not authenticate to use this URL"; return; }
                        if (Session["locationid"].ToString() == "2")
                        {
                            if (Request.Url.ToString().IndexOf("203.101.73.205:2040") > 0)
                                //errMsg.Text = "You are not authenticate to use this URL";
                                Response.Redirect("default.aspx",true );
                            else
                                Response.Redirect("default.aspx", true);
                        }
                        else
                        {
                            if (Request.Url.ToString().IndexOf("203.101.73.205:2040") < 0)
                                //errMsg.Text = "You are not authenticate to use this URL";
                                Response.Redirect("default.aspx", true);
                            else
                                Response.Redirect("default.aspx", true);
                        }

                    }
                    else
                    {
                        if (Request.Url.ToString().IndexOf("203.101.73.205") > 0)
                            errMsg.Text = "You are not authenticate to use this URL";
                            //Response.Redirect("default.aspx", true);
                        else
                            Response.Redirect("default.aspx", true);
                    }
                }
                else
                {
                    errMsg.Text = "more than one entry found for user: " + ds.Tables["USERINFO"].Rows[0]["username"].ToString();
                    errMsg.Visible = true;
                    return;
                }
            }
            else
            {
                errMsg.Text = "Incorrect Username or password";
                errMsg.Visible = true;
            }
        }
        else
        {
            errMsg.Text = "Incorrect Username or password";
            errMsg.Visible = true; 
        }
    }
}
