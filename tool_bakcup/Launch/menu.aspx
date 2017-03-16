<%@ page language="C#" autoeventwireup="true" inherits="menu, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Menu</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>

<script type="text/javascript" language="javascript">
var oSubMenu ;
var oMenuId = "";
var otherMenu;
function changemenuOver(obj)
{
   // alert(obj.className);
   //document.getElementById('TextBox1').value=obj.className;
    if (obj.className == "menuDarker")
    {obj.className = "menumoseover";}
    else{obj.className = "menuDarker";}
    
    
}
function loadDialog(oPage)
{
    window.open(oPage, '_blank', 'channelmode=no, directory=no, height=500, left=50, top=20, width=800, location=no, menubar=no, resizable=no, scrollbars=no, status=no, titlebar=no');
}
function showNext(obj)
{
    var oRow="";
    obj.className="menuDarker";
    //For  check Browser
     var mozilla = document.getElementById&&!document.all;
    //For Not a Regular Menu
    if(otherMenu!=null )
    {
        if(otherMenu.id == "Home" || otherMenu.id  == "logoff" || otherMenu.id  == "changepword" )
        {
            document.getElementById(otherMenu.id).className="menuDarker";
        }
    }
    otherMenu=obj;
	if (obj.id == "Home" || obj.id == "logoff" || obj.id == "changepword") // not a regular menu
	{
	    
		if (oSubMenu != null && oMenuId !=null && oMenuId !="")
		{
		     if (oMenuId == oRow.id)
			    document.getElementById(oMenuId).firstChild.className =  "menumoseover";
			 else
			    document.getElementById(oMenuId).firstChild.className =  "menuDarker";
			oSubMenu.style.display = 'none';
			oMenuId = '';
		}
		return;
	}
	
	//oRow = obj.parentElement;
	//subbu 02/9/09
	oRow=mozilla?obj.parentNode:obj.parentElement;
    while(oRow.nodeType!=1)
        oRow = oRow.parentElement;

    if (oMenuId == oRow.id)
    {
		if (oSubMenu != null)
		{
			document.getElementById(oMenuId).firstChild.className =  "menumoseover";
			oSubMenu.style.display = 'none';
			oMenuId = '';
		}
		return;
	}
    if (oSubMenu != null)
    {
		if (oMenuId != '')
	        document.getElementById(oMenuId).firstChild.className =  "menuDarker";
        oSubMenu.style.display = 'none';
    }
    nextBrother=oRow.nextSibling;
    while(nextBrother.nodeType!=1)
    nextBrother = nextBrother.nextSibling;
    nextBrother.style.display= '';
    oSubMenu = nextBrother;
    oMenuId = oRow.id;
    
}

function setFrameSize(SizeTo)
{
	switch (SizeTo)
	{
		case 'small':
			parent.document.all.contents.cols = "24px, *";
    		livemenus.style.display = "none";
    		frmmenu.style.background = "white";
			break;
		case 'big':
			parent.document.all.contents.cols = "160px, *";
			livemenus.style.display = "";
			frmmenu.style.background = "#E0E0E0";
			break;
	}
}
function logoff()
{
    window.location="menu.aspx?q=logoff";
}
</script>  

<body style="background:#E0E0E0;">
    <form id="frmmenu" runat="server" >  
        
		<div style="text-align: right;background:white;">
		
		<img id="shrinkMe" src="images/tools/left_arrow.jpg" width="24px" onClick="setFrameSize('small'); this.style.display='none'; growMe.style.display=''" style="cursor:pointer;" title="Hide Menu">
		
		<img id="growMe" src="images/tools/right_arrow.jpg" width="24px" onClick="setFrameSize('big'); this.style.display='none'; shrinkMe.style.display=''" style="display: none; cursor:pointer; " title="Show Menu">
            
		</div>  
		
		<%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
		<%--<div id="homediv" style="width:auto;overflow:nonoe;">
		     <table class='menuTable' width='99%' style="margin-top:20px;">
		     <tr >
                <td  id="Home" runat="server" onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' onclick="showNext(this)" height='25px' class='menuDarker'>
                    <a class="msglink" href="welcome.aspx" target="right">Home</a>        
                </td>
            </tr>
		     </table>
		</div>--%>
        <div id="livemenus" runat="server" style="width: auto; overflow:none;margin-top:20px;" >
        Loading...
        </div>
        <%--<div id="messagediv" runat="server" style="width: auto; overflow:none;" >
        <table class='menuTable' width='99%'>
            <tr id="ChangePassword">
                <td  id="changepword" runat="server" onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' onclick="showNext(this)" height='22px' class='menuDarker'>
                    <a class="msglink" href="changepassword.aspx"
                            target="right">Change Password</a>        
                </td>
            </tr>
            <tr>
                <td  id="signoff" runat="server" onmouseover='changemenuOver(this)' onmouseout='changemenuOver(this)' height='25px' class='menuDarker'>
                    <asp:LinkButton CssClass="msglink" ID="logoff" Text="Sign Out"  runat="server" OnClick="logoff_Click"></asp:LinkButton>&nbsp;             
                </td>
            </tr>
        </table>
       </div>--%>
        
        
    </form>
</body>
</html>

