<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Datapage Login Screen</title>
    <link href="default.css" type="text/css" rel="stylesheet" />

<script language="javascript">
  
  
function FindMACID()
{


    try
    {
        var locator = new ActiveXObject("WbemScripting.SWbemLocator");
        var service = locator.ConnectServer(".");
        var properties = service.ExecQuery("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled=true");
        var e = new Enumerator (properties);
        var Display=document.getElementById("Label1");
        var SetValue=document.getElementById("HiddenField1");
        for (;!e.atEnd();e.moveNext ())
        {
            var p = e.item ();
            
            document.getElementById("testmac").value = replaceSubstring(p.MACAddress, ":", "");
        }
        alert(document.getElementById("testmac").value);
    }
    
   
    catch(ex)
    {
    alert(ex.message);
    }
    
  
    
}


function replaceSubstring(inputString, fromString, toString)

{

    var temp = inputString;

    if (fromString == "") {

    return inputString;

    }

    if (toString.indexOf(fromString) == -1) { 

    while (temp.indexOf(fromString) != -1) {

    var toTheLeft = temp.substring(0, temp.indexOf(fromString));

    var toTheRight = temp.substring(temp.indexOf(fromString)+fromString.length, temp.length);

    temp = toTheLeft + toString + toTheRight;

}

} else { 

    var midStrings = new Array("~", "`", "_", "^", "#");

    var midStringLen = 1;

    var midString = "";

    while (midString == "") {

    for (var i=0; i < midStrings.length; i++) {

    var tempMidString = "";

    for (var j=0; j < midStringLen; j++) { tempMidString += midStrings[i]; }

    if (fromString.indexOf(tempMidString) == -1) {

    midString = tempMidString;

    i = midStrings.length + 1;

}

}

}

while (temp.indexOf(fromString) != -1) {

var toTheLeft = temp.substring(0, temp.indexOf(fromString));

var toTheRight = temp.substring(temp.indexOf(fromString)+fromString.length, temp.length);

temp = toTheLeft + midString + toTheRight;

}

while (temp.indexOf(midString) != -1) {

var toTheLeft = temp.substring(0, temp.indexOf(midString));

var toTheRight = temp.substring(temp.indexOf(midString)+midString.length, temp.length);

temp = toTheLeft + toString + toTheRight;

}

} 

return temp; 

} 


//-->

</script>


</head>

<script lang="C#" runat=server>
    protected void Page_Unload(Object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    Session["invDS"] = null;
        //}

        
        
    }
</script> 
<body onload="">
    <form id="form1" runat="server" style="text-align:center;">
    <table id="Table_01" align="center" width="800" border="0" style="text-align:left" cellpadding="0" cellspacing="0">
	    <tr>
		    <td colspan="5">
			    <img src="images/MIS_01.gif" width="800" id="img_mis1" name="img_mis1" runat="server"  alt="" style="display:none;">
	<div id="content_sanlucas" runat="server" style="display:none;">
		<table width="100%" border="0"><tr><td width="91px;" valign="top"><img src="images/datapage logo.gif" alt="logo" ></td>
		<td valign="middle" style="text-align:center " align="center" >
		<div style=" margin:0 0 0 0;font-family: Garamond, Calibri, Cambria, Verdana; font-size:14pt;font-weight:bold; text-align: center; no-repeat; width:100%; vertical-align: middle ;" nowrap>C<span style="font-size:80%;">USTOMER</span> R<span style="font-size:80%">ELATIONSHIP</span> M<span style="font-size:80%">ANAGEMENT</span> S<span style="font-size:80%;">YSTEM</span>&nbsp;</div></td>
		<td width="95px;"><img src="images/SLM LOGO.jpg" alt="logo"  style="margin-top:5px;">&nbsp;</td>
		</tr></table>
	</div>
			    </td>
	    </tr>
	    <tr id="img_row2" runat="server">
		    <td colspan="5">
			    <img src="images/MIS_02.gif" width="800" height="165" alt="" id="img_mis2" name="img_mis2" runat="server"></td>
	    </tr>
	    <tr>
		    <td colspan="5">
			    <img src="images/MIS_03.gif" width="800" height="54" alt="" id="img_mis3" name="img_mis3" runat="server"></td>
	    </tr>
	    <tr>
		    <td rowspan="3">
			    <img src="images/MIS_04.gif" width="10" height="184" alt=""></td>
		    <td rowspan="3" width="447" valign="top" style="background:url(images/MIS_05.gif)">
		    <div id=div_content runat="server">
			    <br />
			    <font face="verdana"  size="1"><p>Datapage's Management Information System (MIS) is a single source 
			    location for all production, sales and associated financial information. It offers a lot of useful 
			    information on all the company's production related activities as well as planned and proposed 
			    budgetary figures. Contact information of the various editors, managers and printers associated 
			    with our journals are viewable at the click of a mouse.</p>
 			    <p>The website is ordered under various categories that in turn contain links to the various types 
 			    of information that they represent. As a user, you will be setup to access the website using 
 			    stipulated logon information and specialized access rights that determine your areas of preview.
 			    </p></font>
 			    </div>
 			    <div id=div_content_Sanlucas runat="server">
			    <br />
			    <font size="2pt" style="font-family:Garamond"><p>Datapage's Customer Relationship Management (CRM) system is 
			    a single source location that offers a lot of useful information on all the San Lucas Medical journal production 
			    related activities. Contact information of the various editors, reviewers and authors associated with 
			    SLM journals are viewable at the click of a mouse.</p>
 			    <p>The website is ordered under various categories that in turn contain links to the various types of 
 			    information that they represent. As a user, you will be setup to access the website using stipulated logon 
 			    information and specialized access rights that determine your areas of preview.
 			    </p></font>
 			    </div>
 		    </td>
		    <td rowspan="3">
			    <img src="images/MIS_06.gif" width="45" height="184" alt=""></td>
		    <td colspan="2">
			    <img src="images/MIS_07.gif" width="298" height="31" alt=""></td>
	    </tr>
	    <tr>
		    <td width="240" align="center" style="background:url(images/MIS_08.gif)">
		    <p>
		    <font color="white"><b>Username</b></font>&nbsp;&nbsp;<asp:TextBox Text="" ID="txtusername" runat="server" ></asp:TextBox>
		    <br /><br />
		    <font color="white"><b>Password</b></font>&nbsp;&nbsp;<asp:TextBox Text="" TextMode="Password" ID="txtpassword" runat="server" ></asp:TextBox>
		    </p>
		    <asp:Button CssClass="dpbutton" AccessKey="E" id="btnLogin" Text="login" runat="server" OnClick="btnLogin_Click"   />
		    <br /><br />
		    <asp:LinkButton style="color:White;font-weight :bold; " ID="btnforgetpass" Text="Forget password? Click Here" runat="server" OnClientClick="javascript:alert('Please contact your team head or manager to reset your pasword.');return false;" ></asp:LinkButton>
		    </td>
		    <td><img src="images/MIS_09.gif" width="58" height="128" alt=""></td>
	    </tr>
	    <tr>
		    <td colspan="2">
			    <img src="images/MIS_10.gif" width="298" height="25" alt=""></td>
	    </tr>
	    <tr>
		    <td colspan="5">
			    <img src="images/MIS_11.gif" width="800" height="16" alt=""></td>
	    </tr>	    
    </table>    <br />
    <asp:Label ID="errMsg" Text="" runat="server" Visible="false" ForeColor="red" ></asp:Label>
    <table height="100%" width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
    <td colspan="2">
    <div id="divClientDetails" runat="server" visible="false" ></div>    
    </td>
    </tr>
    <tr height="30px" class="darkbackground" valign="middle" >
    <td colspan="4" align="right"><font face="verdana" color="white" size="1">   &copy; 2014 Datapage International Limited. All rights reserved.&nbsp;&nbsp;</font>
    <input type="text" id="testmac" value="" runat="server" style="visibility:hidden;" /></td>
    
    <%--<td style="vertical-align:middle" align="left" >&nbsp;&nbsp;India Time: <asp:Label ID="indiatime" Text="" runat="server" ></asp:Label> 
    
    </td>
    <td><input type="text" id="testmac" value="" style="visibility:hidden;"/></td>
    <td style="vertical-align:middle" colspan="2" align="right" >
    Dublin Time: <asp:Label ID="dublintime" runat="server" ></asp:Label> 
    &nbsp;&nbsp;
    </td>--%>
    </tr>    
    </table>
    <div>
    </div>
    </form>
</body>
</html>
