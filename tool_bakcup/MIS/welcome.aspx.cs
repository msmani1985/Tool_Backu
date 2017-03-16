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

public partial class welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!Page.IsPostBack)
        {
            datasourceSQL ds = new datasourceSQL();
            if (Session["firstvisit"] != null)
            {
                if (Session["firstvisit"].ToString() == "")
                    Response.Redirect("changepassword.aspx", true);
            }
            if (Request.Url.ToString().IndexOf("203.101.73.205:2080") > 0 && Session["customerid"] != null && Session["customerid"].ToString() == "124")
            { Condiv.Visible = false; sanlucas_Condiv.Visible = true; }
            else
            { Condiv.Visible = true; sanlucas_Condiv.Visible = false; }

            //kalimuthu 30/06/2014
            //DataSet vs = new DataSet();
            //vs = ds.GetEmpPendstatus( Convert.ToInt16(Session["employeeid"].ToString()));
            //if(vs.Tables[0].Rows[0]["empno"].ToString()!="0")
            //    lbl.Visible = true;
            //else
            //    lbl.Visible = false;
            //DataSet ps = new DataSet();
            //ps = ds.GetPendingStatus(Session["employeeid"].ToString());
            //if (ps != null)
            //{
            //    lbl.Text = string.Format("<marquee behavior='alternate' direction='right' scrollamount='4'>{0} </marquee>", "Pending Approval Details : (" + ps.Tables[0].Rows[0]["Leave"].ToString() + ") Leave, (" + ps.Tables[0].Rows[0]["OT"].ToString() + ") OT, (" + ps.Tables[0].Rows[0]["Shift"].ToString() + ") Shift");
                
            //}

        }
        //subbu 01/09/2009
        
        if (Session["fullname"] != null)
        {
            divwelcome.InnerHtml = "Welcome " + Session["fullname"].ToString() + ",";
            sanlucas_divwelcome.InnerHtml = "Welcome to Datapage CRM for SLM Journals!";
        }
        else
        {
            string instr = "<script language='javascript'>window.open('login.aspx','_top');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "open", instr);

        }
        try
        {
            if (Session["TeamDS"] == null)
            {
                DataSet oDS = new DataSet();
                datasourceIB dbIB = new datasourceIB();
                oDS = dbIB.GetAllTeamMember(Convert.ToInt32(Session["empno"].ToString()));
                if (oDS != null)
                {
                    Session["TeamDS"] = oDS;
                }
                oDS = null;
                dbIB = null;
            }
            DataSet oTeamDS = new DataSet();
            oTeamDS = (DataSet )Session["TeamDS"];
            agvTeamList.DataSource = oTeamDS.Tables[0];
            agvTeamList.DataBind();
            
            //This is SQL TeamList
            if (Session["SQLTeamDS"] == null)
            {
                DataSet oDS = new DataSet();
                datasourceSQL dbSQL = new datasourceSQL();
                //oDS = dbIB.GetAllTeamMember(Convert.ToInt32(Session["empno"].ToString()));
                oDS = dbSQL.ExcuteSelectProcedure("spGet_TeamMembers", Session["employeeid"].ToString(), "@team_owner_id", "Int", "Input", CommandType.StoredProcedure);
                if (oDS != null)
                {
                    Session["SQLTeamDS"] = oDS;
                }
                oDS = null;
                dbSQL = null;
            }

            oTeamDS = (DataSet)Session["SQLTeamDS"];
            SQLTeamList.DataSource = oTeamDS.Tables[0];
            SQLTeamList.DataBind();
            //if (Request.Url.ToString().IndexOf("203.101.73.205") ==-1)
            //    Response.Redirect("JobAllocation_Preview.aspx", true);
        }
        catch (Exception oex)
        { }


    }
}

