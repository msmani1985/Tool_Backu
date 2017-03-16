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

public partial class menu : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //Subbu 01/09/2009
        if (Request.QueryString["q"] != null && Request.QueryString["q"].ToString() == "logoff")
        {
            logoff();
            return;
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        DataSet oDS = new DataSet();
        DataSet oDS_SQL = new DataSet();
        //if (!Page.IsPostBack)
        //{
            if (Session["employeeid"] != null)
            {
                 biz_emp_menu_mgmt biz = new biz_emp_menu_mgmt();
                //subbu 01/09/2009
                //string msg = messagediv.InnerHtml;
                Session["despatchgroup"] = "false";
                string sdespatched = biz.GetMenus(Convert.ToInt32(Session["employeeid"].ToString()));
                
                     
                    
                if (sdespatched.ToUpper().IndexOf("<DIV>TRUE</DIV>") != -1)
                    Session["despatchgroup"] = "true";
                sdespatched = sdespatched.Replace("<DIV>TRUE</DIV>", "");
                livemenus.InnerHtml = sdespatched.Replace("<DIV>FALSE</DIV>", "");
                oDS = biz.GetTeamLeads();
                if (oDS != null)
                {
                    Session["teamLeads"] = oDS;
                }
            }

            datasourceIB dbIB = new datasourceIB();
            datasourceSQL dbSQL = new datasourceSQL();
            Datasource_IBSQL dbIB_SQL = new Datasource_IBSQL();
            try
            {
                oDS_SQL = dbIB_SQL.GetAllCustomers();
                if (oDS_SQL != null)
                {
                    Session["CustomerName"] = oDS_SQL;
                }


                oDS = dbIB.ExcueQueryString("SELECT SNO, SNAME FROM STAGE_DP WHERE SNO > 68 ", "JOBSTAGES");
                if (oDS != null)
                {
                    Session["stageDS"] = oDS;
                }
                oDS = dbIB.GetAllCustomers();
                if (oDS != null)
                {
                    Session["CustomerDS"] = oDS;
                }

                if (Convert.ToInt64(Session["employeenumber"].ToString()) > 10000)
                {
                    Session["empno"] = Session["SQL_empno"].ToString();
                    Session["departmentno"] = "10037";
                }
                else
                {
                    oDS = dbIB_SQL.ExcueQueryString("SELECT * FROM EMPLOYEE WHERE EMPLYOEE_ID = " + Session["employeenumber"].ToString(), "DNAME");
                    if (oDS != null)
                    {
                        Session["empno"] = Session["SQL_empno"].ToString();
                        Session["departmentno"] = "10037";
                        //Session["departmentno"] = oDS.Tables[0].Rows[0]["DNO"].ToString();
                        //Session["empno"] = oDS.Tables[0].Rows[0]["EMPNO"].ToString();
                    }
                }
                //oDS = dbIB.GetAllTeamMember(Convert.ToInt32(Session["empno"].ToString()));
                //if (oDS != null)
                //{
                //    Session["TeamDS"] = oDS;
                //}
                oDS = dbSQL.ExcuteSelectProcedure("spGet_TeamMembers", Session["employeeid"].ToString(), "@team_owner_id", "int", "Input", CommandType.StoredProcedure);
                if (oDS != null)
                    Session["SQLTeamDS"] = oDS;
                if (Convert.ToBoolean(Session["webuser"]) == true)
                {
                    Session["dublinPCfile"] = ConfigurationManager.ConnectionStrings["dublinPCXML"].ToString();
                    Session["indiaPCfile"] = ConfigurationManager.ConnectionStrings["indiaPCXML"].ToString();
                    Session["indiaINVfile"] = ConfigurationManager.ConnectionStrings["indiaINVXML"].ToString();
                    Session["dublinINVfile"] = ConfigurationManager.ConnectionStrings["dublinINVXML"].ToString();
                    Session["PDFFilePathDub"] = ConfigurationManager.ConnectionStrings["PDFFilePathDub"].ToString();
                    Session["PDFFilePathInd"] = ConfigurationManager.ConnectionStrings["PDFFilePathInd"].ToString();

                }

            }
            catch (Exception oex)
            { }
            finally
            {
                oDS = null;
                dbIB = null;
                dbSQL = null;
            }
        //}
    }
    protected void logoff_Click(object sender, EventArgs e)
    {
        Session["fullname"] = null;
        Session.Clear();
        Session.Abandon();

        string sHTML = "";
        Page page = HttpContext.Current.Handler as Page;

        sHTML += "<script language='javascript'>";
        sHTML += "window.open('Login.aspx','_top')";
        sHTML += "</script>";

        page.RegisterStartupScript("script", sHTML);

    }
    private void logoff()
    {
        Session["fullname"] = null;
        Session.Clear();
        Session.Abandon();

        string sHTML = "";
        Page page = HttpContext.Current.Handler as Page;

        sHTML += "<script language='javascript'>";
        sHTML += "window.open('Login.aspx','_top')";
        sHTML += "</script>";

        page.RegisterStartupScript("script", sHTML);
    }
}

