using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

/// <summary>
/// Summary description for biz_emp_menu_mgmt
/// </summary>
public class biz_emp_menu_mgmt
{

	public biz_emp_menu_mgmt()
	{
	}
    public DataSet validatelogin(string uname, string password, string ipaddress)
    {
        datasourceSQL eds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = eds.validatelogin(uname, password,ipaddress);
        eds = null;
        return ds;
    }
    public DataSet GetLiveEmployees()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetLiveEmployees();
        mds = null;
        return ds;
    }
    public bool ChangePass(int employee_id, string username, string password)
    {
        bool bTest = false;
        datasourceSQL mds = new datasourceSQL();
        bTest = mds.changepass(employee_id, username, password);
        mds = null;
        return bTest;
    }
    public DataSet GetResignedEmployees()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetResignedEmployees();
        mds = null;
        return ds;
    }
    public DataSet GetLocation()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetLocation();
        mds = null;
        return ds;
    }
    public DataSet GetDepartment()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetDepartment();
        mds = null;
        return ds;
    }
    public DataSet GetTeamLeads()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetTeamLeads();
        mds = null;
        return ds;
    }
    public DataSet GetDesignation()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetDesignation();
        mds = null;
        return ds;
    }
    public DataSet GetEmpTeam()
    {
        datasourceSQL tobj = new datasourceSQL();
        DataSet tds = new DataSet();
        tds = tobj.GetEmpTeam();
        tobj = null;
        return tds;

    }
    public DataSet GetEmployeeDetail(int employee_id)
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetEmployeeDetail(employee_id);
        mds = null;
        return ds;
    }
    public DataSet GetTeamOwner()
    {
        datasourceSQL obj = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = obj.GetTeamOwner();
        obj = null;
        return ds;
    }
    public DataSet GetEmployeeByName(string sProcName, string[,] paramcollection, CommandType CmdType)
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        //ds = mds.ExcuteSelectProcedure(sProcName, paramcollection, paramnames, paramtypes, paramdirections, CmdType);
        ds = mds.ExcProcedure(sProcName, paramcollection, CmdType);
        mds = null;
        return ds;
    }


    // login authenticate menus
    public string GetMenus(int employee_id)
    {
        string sHTMLMenuTable = "";
        string sHtmlTable = ""; 
        string bDespatch = "FALSE";
        datasourceSQL mds = new datasourceSQL();
        sHTMLMenuTable = mds.GetUserMenuItems(employee_id);
        mds = null;
        try
        {
            XmlDocument oxmldom = new XmlDocument();
            oxmldom.LoadXml(sHTMLMenuTable);
            if (oxmldom != null)
            {
                if (oxmldom.SelectSingleNode("//MGI[@menu_group_id = '21']") != null)
                    bDespatch = "TRUE";
                sHtmlTable = "<DIV>" + bDespatch + "</DIV><table id='livemenutable' class='menuTable' width='99%'>";
                sHtmlTable += "<tr ><td id='Home' onclick='showNext(this)' onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' height='25px' class='menuDarker'>";
                sHtmlTable += "<a class='msglink' href='welcome.aspx' target='right'>Home</a> </td></tr>";
                foreach (XmlNode oNode in oxmldom.DocumentElement.ChildNodes)
                {
                    sHtmlTable += "<tr id='" + oNode.Attributes.Item(1).Value + "' >";
                    sHtmlTable += "<td onclick='showNext(this);' onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' height='22px' class='menuDarker'>" + oNode.Attributes.Item(1).Value + "</td></tr>";
                    if (oNode.ChildNodes.Count > 0)
                    {
                        sHtmlTable += "<tr style='display:none'><td><table class='submenuTable' border='0' cellpadding='1' cellspacing='1' width='100%'>";
                        foreach (XmlNode oNodeItem in oNode.ChildNodes)
                        {
                            if (oNodeItem.Attributes.Item(0).Value == "21")
                                sHtmlTable += "<tr><td class='menuLighter' height='15px' onclick=\"loadDialog('" + oNodeItem.Attributes.Item(3).Value + "')\">" + oNodeItem.Attributes.Item(1).Value + "</td></tr>";
                            else
                                sHtmlTable += "<tr><td class='menuLighter' style='color:White' height='15px'><a href='" + oNodeItem.Attributes.Item(3).Value.Replace("**","&") + "' target='right'>" + oNodeItem.Attributes.Item(1).Value + "</a></td></tr>";
                        }
                        sHtmlTable += "</table></td></tr>";
                    }
                }
                if(employee_id!=1687)//For sanlucas customer, do not display this menu
                    sHtmlTable += "<tr ><td id='changepword' onclick='showNext(this)' onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' height='22px' class='menuDarker'><a class='msglink' href='changepassword.aspx' target='right'>Change Password</a> </td></tr>";
                sHtmlTable += "<tr ><td id='logoff' onclick='logoff();' onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' height='22px' class='menuDarker'>Sign Out</td></tr>";
                sHtmlTable += "</table>";
            }
            else
            {
                sHtmlTable += "<DIV>" + bDespatch + "</DIV><div class='errorMsg'>" + sHTMLMenuTable + "</div>";
            }

        }
        catch (Exception oex)
        {
            sHtmlTable = "<DIV>" + bDespatch + "</DIV><div class='errorMsg'>" + oex.Message + "</div>";
        }
        return sHtmlTable;

    }

    //list of menus and its items
    public string GetMenuListHTML()
    {
        datasourceSQL mds = new datasourceSQL();
        string sTable = "";
        string sHtmlTable = "";
        sTable = mds.GetHTMLString("spGet_Menu_Items", "MENUHEADING", CommandType.StoredProcedure);
        mds = null;
        try
        {
            XmlDocument oxmldom = new XmlDocument();
            oxmldom.LoadXml(sTable);
            if (oxmldom != null)
            {
                sHtmlTable = "<table id='menu_items' style='text-align:left' width='180px'>";
                foreach (XmlNode oNode in oxmldom.DocumentElement.ChildNodes)
                {
                    sHtmlTable += "<tr id='" + oNode.Attributes.Item(1).Value + "' >";
                    sHtmlTable += "<td align='left' style='color:green;font-weight:bold' colspan='2'>" + oNode.Attributes.Item(1).Value + "</td></tr>";
                    if (oNode.ChildNodes.Count > 0)
                    {
                        foreach (XmlNode oNodeItem in oNode.ChildNodes)
                            sHtmlTable += "<tr><td width='30px;'>&nbsp;</td><td align='left'>" + oNodeItem.Attributes.Item(1).Value + "</td></tr>";
                    }
                }
                sHtmlTable += "</table>";

            }
        }
        catch (Exception oex)
        {
            sHtmlTable = oex.Message;
        }
        return sHtmlTable;
    }

    public DataSet GetMenuItems()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetMenuItems();
        mds = null;
        return ds;
    }
    public DataSet GetMenuHeadings()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetMenuHeadings();
        mds = null;
        return ds;

    }
    public DataSet GetMenuGroups()
    {
        datasourceSQL mds = new datasourceSQL();
        DataSet ds = new DataSet();
        ds = mds.GetMenuGroups();
        mds = null;
        return ds;
    }


}
