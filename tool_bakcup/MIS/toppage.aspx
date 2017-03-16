<%@ Page Language="C#" AutoEventWireup="true" CodeFile="toppage.aspx.cs" Inherits="toppage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Datapage Menu</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<script>
function bookmarksite(title)
{
    try
    {
        var url =  location.href.substring(0, location.href.lastIndexOf(":"));
        if (window.sidebar) // firefox
            window.sidebar.addPanel(title, url, "");
        else //if(document.all)// ie
            window.external.AddFavorite(url, title);
    }
    catch(ex)
    {
        //alert(ex.message);    
    }
}


</script>
<body >
    <form id="form1" runat="server">
     <div id="div_sanlucas" runat="server">
		<table width="100%" border="0"><tr><td width="91px;" valign="top"><img src="images/datapage logo.gif" alt="logo" ></td>
		<td style="text-align:center; ; " align="center" valign="top">
    		<div style="border:1px solid white; margin:0 0 0 0;font-family: Garamond, Calibri, Cambria, Verdana; font-size:14pt;font-weight:bold; text-align: center; background:url('images/Sanlucasimg4.GIF') repeat-x; width:100%; height:91px; vertical-align: middle ;color:white; " nowrap><p valign="middle" height="60px" style="line-height:70px;" nowrap>&nbsp;C<span style="font-size:80%;">USTOMER</span> R<span style="font-size:80%">ELATIONSHIP</span> M<span style="font-size:80%">ANAGEMENT</span> S<span style="font-size:80%;">YSTEM</span>&nbsp;</p></div>
		</td>
		<td width="88px;"><img src="images/SLM LOGO.jpg" alt="logo"  style="margin-top:5px;"></td>
		</tr></table>
	</div>
	<div id="div_datapage" runat="server">
    <TABLE style=";WIDTH:100%">
        <TR><td style="width:10%"><img src="images/logo.GIF" /></td>
        <td style="background:url('images/celebration.gif');width:50%;text-align:center;">
            <img src="images/birthdayIMG.gif" alt="" height="65" runat="server" id="birthdayImg" />
        </td>
        <td align="Right" ><img src="images/INDEX1.GIF" /></td></TR>
        <tr>
        <td colspan="3" style="background-image:url('images/line.gif');background-repeat:repeat-x;" height="5px"></td>
</tr>

        <tr><td colspan="3" align="Right" >
        
            <div style="background:WHITE; display:none;">
                <img src="images/bookmark.jpg" alt="Bookmark this site" height="18px" onclick="bookmarksite('Datapage New MIS')" />&nbsp;&nbsp;&nbsp;
                <asp:ImageButton AlternateText="Add to Trusted Site" ID="trusted" Height="15px" ImageUrl="images/trustedsite.jpg" runat="server" OnClick="trusted_Click" />&nbsp;&nbsp;&nbsp;|&nbsp;
                <asp:Label runat="server" ID="lblConstring" Text=""></asp:Label>&nbsp;|&nbsp;
                <asp:Label id="username" Text=""  runat="server"></asp:Label>&nbsp;|&nbsp;
                <a href="changepassword.aspx" target="right">Change Password</a>&nbsp;|&nbsp;
                <a href="myaccount.aspx" target="right">My Account</a>&nbsp;|&nbsp;
                <asp:LinkButton ID="logoff" Text="Sign Out"  runat="server" OnClick="logoff_Click"></asp:LinkButton>&nbsp;
            </div>
        </td></tr>
    </TABLE>
    </div>
    </form>
</body>
</html>
