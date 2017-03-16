<%@ page language="C#" autoeventwireup="true" inherits="job_email_message, App_Web_w6b3pav3" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview - Mail Message</title>
    <link href="scripts/wysiwyg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/wysiwyg.js"></script>
    <script type="text/javascript" src="scripts/wysiwyg-settings.js"></script>
    <script type="text/javascript">
			WYSIWYG.attach('rtb1',small);			
    </script>
    <script language="javascript">
    function validEmail(elem){
    var reg = /^([A-Za-z0-9_\-\.\+])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    document.getElementById(elem).value = document.getElementById(elem).value.replace(',',';');
    var addr = document.getElementById(elem).value;
    var address = addr.split(";");   
    if(address!=null && address!="" && address.length>0){
        for(var i=0;i<address.length;i++){
           if(reg.test(address[i]) == false){
              alert('Invalid Email Address: '+address[i]);
              //alert(document.getElementById('lblError'));
              document.getElementById(elem).select();
              return false;
    }}}
    }   
    var cnt=0;
    var tt;
    var timer_is_on=0;
    var elemn;
    var h = 0;
    var max =20;
    var seed = 1;
    function timedCount(){
        //alert(document.getElementById('divfooter'));
        elemn = document.getElementById('divfooter');
	    h = elemn.style.height.replace('px','');	
	    if(timer_is_on && cnt<=max && h<=max){
	    //ctrl.value=cnt;	
	    if(h=='')h=0;
	    //alert(h);
	    elemn.style.height = parseInt(h)+seed+'px';	    
	    //alert(cnt);
	    cnt=cnt+seed;
	    tt=setTimeout("timedCount()",0);
	    }else {
	    timer_is_on=0;
	    cnt=0;
	    }
    }
    function doTimer()
    {    
    if (!timer_is_on)
      {
      timer_is_on=1;
      timedCount();
      }else timer_is_on=0;  
    }
    function ConfirmSend(msg){
        var status = false;
        if(confirm(msg)){
            status = true;
            doTimer();
        }
        return status;
    }
    </script>
    <style>
    .dpJobGreenHeader{
	    border-bottom: solid 1px Green;
	    background: #f0fff0;
	    color: GREEN;
	    font-size: 10pt;
	    font-weight: bold;
	    height: 20px;
	    vertical-align: middle;
	    text-align: left;
    }    
    .footer{	    
	    background-color: Green;
	    height: 0.1px;
	    text-align: center;
	    font-size:12px;
	    font-weight:bold;
	    color:#FFFFFF;
	    font-family:Verdana;	
	    padding-top: 0px;	        
	    width: 250px;
	    position:fixed;	
	    right: 0px;	    
	    bottom: 0px;
	    line-height:20px;
        }
    </style>
    <!--[if lte IE 6]>
    <style type="text/css">
    /*body {height:100%; overflow-y:auto;}
    html {overflow-x:auto; overflow-y:hidden;}
    * html .footer {position:absolute;}*/
	.footer{
	height: 20px;
	width: 200px;
	position:inherit;
	display:none;
	right: 0px;
	bottom: 0px;
	float:right;
	}
    </style>
    <![endif]-->
</head>
<body topmargin="3">
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                <asp:Image ID="Image2" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/tools/mail_edit.png" />
                Edit Message</div>
        </div>
        <table align="center" border="0" cellpadding="1" cellspacing="1" class="bordertable"
            style="width: 777px" bgcolor="#f0fff0">
            <tr>
                <td style="width: 113px" align="right">
                    Customer:</td>
                <td>
                    <asp:Label ID="lblCustomer" runat="server" Font-Bold="True" ForeColor="Green">Unknown Customer Name</asp:Label></td>
            </tr>
            <tr>
                <td align="right" style="width: 113px">
                    <asp:Label ID="lblJobType" runat="server">Job:</asp:Label></td>
                <td>
                    <asp:Label ID="lblJob" runat="server" Font-Bold="True" ForeColor="Green">Unknown Job</asp:Label></td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    Email Config Type:</td>
                <td colspan="1">
                    <asp:Label ID="lblEmailConfigType" runat="server" Font-Bold="True" ForeColor="Green">Unknown Config Type</asp:Label></td>
            </tr>
        </table>
        <br />
        <table align="center" border="0" cellpadding="1" cellspacing="1" class="bordertable"
            style="width: 777px">
            <tr class="dpJobGreenHeader">
                <td style="width: 113px">
                </td>
                <td align="right">
                <div id="divStatus" runat="server" style="font-size: 12px; color:Green; font-weight: bold; float:left"></div>
                    <asp:Button ID="btnPrev" runat="server" CssClass="dpbutton" Text="< Prev" OnClick="btnPrev_Click" Enabled="False" />
                    <asp:Button ID="btnNext" runat="server" CssClass="dpbutton" Text="Next >" OnClick="btnNext_Click" Enabled="False" />
                    <asp:Button ID="btnSendOne" runat="server" CssClass="dpbutton" OnClientClick="javascript:return ConfirmSend('Confirm Send?');"
                        Text="Send" OnClick="btnSendOne_Click" ToolTip="Send only this Email" Enabled="False" />
                    <asp:Button ID="btnSendAll" runat="server" CssClass="dpbutton" OnClientClick="javascript:return ConfirmSend('Send All Emails?');"
                        Text="Send All" OnClick="btnSendAll_Click" ToolTip="Send all Emails" Enabled="False" />
                    <input id="Button1" class="dpbutton" style="width: 53pt; cursor: pointer" type="button"
                        value="Close" onclick="javascript:if(confirm('Do you want to close this window?'))self.close();" /></td>
            </tr>
            <tr style="width: 113px">
                <td style="width: 113px" align="right">
                    Contact Name:
                </td>
                <td>
                    <asp:DropDownList ID="drpContacts" Width="556px" runat="server" Font-Size="9pt" AutoPostBack="True" OnSelectedIndexChanged="drpContacts_SelectedIndexChanged" BackColor="#f1f1f1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    From:</td>
                <td>
                    <asp:TextBox ID="txtFrom" runat="server" CssClass="TxtBox" ReadOnly="True" Enabled="false" Width="550px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    To:</td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server" onblur="javascript:validEmail('txtTo');" CssClass="TxtBox" Width="550px" Enabled="False" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    Cc:</td>
                <td>
                    <asp:TextBox ID="txtCc" runat="server" onblur="javascript:validEmail('txtCc');" CssClass="TxtBox" Width="550px" ToolTip="Use semicolon to seperate more than one Email address"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    Bcc:</td>
                <td>
                    <asp:TextBox ID="txtBcc" runat="server" onblur="javascript:validEmail('txtBcc');" CssClass="TxtBox" Width="550px" ToolTip="Use semicolon to seperate more than one Email address"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    Subject:</td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" BackColor="#FFFFC0" CssClass="TxtBox" Width="550px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 113px" align="right">
                    Attachment:<asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/wysiwyg_images/attachment.jpg" /></td>
                <td>                    
                    <input id="FileUpload1" type="file" runat="server" style="width: 556px; border-right: #ccc 1px solid; border-top: #ccc 1px solid; border-left: #ccc 1px solid; border-bottom: #ccc 1px solid; height: 14pt;" unselectable="on" title="Choose a file to attach"/>
                    &nbsp;                    
                    <asp:Button ID="btnAttach" runat="server" CssClass="dpbutton" OnClick="btnAttach_Click" OnClientClick="javascript:if(document.form1.FileUpload1.value==''){alert('Choose a file to attach.');return false;}else doTimer();"
                        Text="Attach" /></td>
            </tr>
            <tr>
                <td style="width: 113px">
                </td>
                <td>
                    <asp:DataGrid ID="dgrdAttachment" runat="server" AutoGenerateColumns="False" GridLines="None"
                        ShowHeader="False" Width="90%" OnItemCommand="dgrdAttachment_ItemCommand" BorderStyle="Solid" BorderWidth="1px" OnItemDataBound="dgrdAttachment_ItemDataBound">
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="grdlblText" runat="server" Text='<%# Eval("Text") %>' Font-Size="X-Small"></asp:Label>
                                    <asp:Label ID="grdlblText_SaveAS" runat="server" Font-Size="X-Small" BackColor="#DFEBFF"></asp:Label>
                                    <asp:HiddenField ID="grdlblHfval" runat="server" Value='<%# Eval("Value") %>' />
                                    <asp:HiddenField ID="grdHfUploadFilename" runat="server" Value='<%# Eval("UploadFileName") %>' />
                                    <asp:HiddenField ID="grdHfMustAttach" runat="server" Value='<%# Eval("MustAttach") %>' />                                    
                                    <asp:LinkButton ID="dgrdlinkDelete" runat="server" CssClass="link1" CommandName="de1ete" Font-Size="X-Small" OnClientClick="javascript:doTimer();">Remove</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <textarea id="rtb1" name="test3" runat="server" style="width: 100%; height: 900px">                               
                            </textarea>
                </td>
            </tr>
        </table>
        <br />
        <asp:HiddenField ID="hfSleep" Value="0" runat="server" />
        <br />
        <div id="divfooter" class="footer">processing please wait...</div>
    </form>
</body>
</html>
