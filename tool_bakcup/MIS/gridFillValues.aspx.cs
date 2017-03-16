using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gridFillValues : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
    //    function Calculation() {  
    //var grid = document.getElementById("<%= GVReue.ClientID%>");  
    //for (var i = 0; i < grid.rows.length - 1; i++) {  
    //var txtAmountReceive = $("input[id*=txtQty]")  
    //if (txtAmountReceive[i].value != '') {  
    //alert(txtAmountReceive[i].value);  
    //  }  
    //  }  
        string strjscript = "<script language='javascript'>";
        strjscript += "try{var temp = window.opener.document.getElementById('" + HttpContext.Current.Request.QueryString["recv"];
        strjscript += "');var txtreceived = window.opener.document.getElementsByClassName('RECV');var txtdues = window.opener.document.getElementsByClassName('DUE');var chksFILL = window.opener.document.getElementsByClassName('FILL');var drpTSKs = window.opener.document.getElementsByClassName('TSK');for(var i=0;i<temp.rows.length-1;i++){if(chksFILL[i].checked == true){txtreceived[i].value='" + txtReceived.Text + "';txtdues[i].value = '" + txtDue.Text + "';drpTSKs[i].value='"+drpTask.SelectedValue.ToString()+"'} } setTimeout(window.close(), 10);}catch(ex){alert(ex)}";
        strjscript += "<" + "/script" + ">";
        Literal1.Text = strjscript;
    }
}