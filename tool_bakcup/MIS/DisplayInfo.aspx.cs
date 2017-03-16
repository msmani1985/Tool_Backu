using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DisplayInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         string strSQL="";
       
        if (Request.QueryString["conno"] != null)
        {
            string sconno = Request.QueryString["conno"];
            datasourceIBSQL ibSql = new datasourceIBSQL();
            DataSet dsDesig = new DataSet();
            dsDesig = ibSql.GetsDataSet("select b.contitle from contactrole_dp a inner join contacttype_dp b on  a.contypeno=b.contypeno where a.conno=" + sconno + "", CommandType.Text);
            if(dsDesig.Tables[0].Rows.Count>0)
            {
                lbldesignation.Text = dsDesig.Tables[0].Rows[0]["contitle"].ToString();
            }

            dsDesig = ibSql.GetsDataSet("select a.conno,a.contitle,a.confirstname,a.consurname," +
                         " a.conemail,a.conemail2,d.conphone,d.confax,d.conweb, " +
                         " d.consitename,d.conoffice,d.conaddress,d.concity,d.constate,d.conpocode,d.concountry  " +
                         " from contact_dp a left outer join contactsite_dp d on a.consiteno=d.consiteno  " +
                         " where a.conno= " + sconno + " ", CommandType.Text);
            if (dsDesig.Tables[0].Rows.Count > 0)
            {
                lblname.Text = dsDesig.Tables[0].Rows[0]["confirstname"].ToString() + "" + dsDesig.Tables[0].Rows[0]["consurname"].ToString();
                lblphone.Text = dsDesig.Tables[0].Rows[0]["conphone"].ToString();
                lblfax.Text = dsDesig.Tables[0].Rows[0]["confax"].ToString();
                lblurl.Text = dsDesig.Tables[0].Rows[0]["conweb"].ToString();
                lblemail1.Text = dsDesig.Tables[0].Rows[0]["conemail"].ToString();
                lblemail2.Text = dsDesig.Tables[0].Rows[0]["conemail2"].ToString();


            }
        }

    }
}