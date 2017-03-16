<%@ page language="C#" autoeventwireup="true" inherits="job_email_config, App_Web_opij0lkt" maintainscrollpositiononpostback="true" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Email Configuration</title>
    <link href="default.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
<!--

    function closeModal(){
    document.getElementById ('divMasked').style.display='none';
    document.getElementById ('divPopFtp').style.display='none';
    document.getElementById ('iframetop').style.display='none';
    }
    function btnOtherDetials_onclick() {
    if(document.getElementById('drpParentJob').value=='' || document.getElementById('drpParentJob').value=='0'){
    alert('* Select a Job');
    return;
    }
    else {
    //alert(document.getElementById ('divPopFtp'));
    var sindex = document.getElementById('drpParentJob').selectedIndex;
    document.getElementById ('txtpopJob').value = document.getElementById('drpParentJob').options[sindex].text;
    document.getElementById ('divPopFtp').style.visibility='visible';
    document.getElementById ('divPopFtp').style.display='';       
    document.getElementById ('divPopFtp').style.top= '150px';
    document.getElementById ('divPopFtp').style.left='248px';
    if (typeof document.body.style.maxHeight == "undefined")
    {  
        var layer = document.getElementById ('divPopFtp');
        layer.style.display = 'block';
        var iframe = document.getElementById('iframetop');
        iframe.style.display = 'block';
        iframe.style.visibility = 'visible';
        iframe.style.top= layer.offsetTop-20;
        iframe.style.left= layer.offsetLeft-20;
        iframe.style.width=  layer.offsetWidth+20;
        iframe.style.height= layer.offsetHeight+20; 
    }else
    {        
        document.getElementById ('divMasked').style.display='';
        document.getElementById ('divMasked').style.visibility='visible';
        document.getElementById ('divMasked').style.top='0px';
        document.getElementById ('divMasked').style.left='0px';
        document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
        document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+90+ 'px';
    }
    }
}
    function saveFtp(){
    document.getElementById('divMasked').style.display='none';
    document.getElementById('divPopFtp').style.display='none';
    __doPostBack('LinkButton1','');
    }

function IMG1_onclick() {
window.open("email_group.aspx?typ=pop","Contacts","top=50,width=800,height=400,status=yes, scrollbars=yes");
}

function IMG2_onclick() {
window.open("email_group.aspx?typ=pop","Contacts","top=50,width=800,height=400,status=yes, scrollbars=yes");
}

// -->
    </script>
<style>
        iframe.divMasked
        {
        position:absolute;
	    padding:5px;
	    visibility:hidden;
	    border:1px solid gray;
	    font:normal 12px Verdana;
	    line-height:18px;
	    z-index:10000;
	    background-color:#ededed;
	    overflow-x:auto;
	    overflow-y:auto;
	    top:-1000px;
	    left:-1000px;
	    filter:progid:DXImageTransform.Microsoft.Shadow(color=#96A8BA,direction=135,Strength=5);
        }
        div.divMasked 
        {
        visibility: hidden;
        position:absolute;
        left:0px;
        top:0px;
        font-family:verdana;
        font-weight:bold;
        padding:40px;
        z-index:100;        
        background-image:url(images/tools/Mask.png);
        /* ieWin only stuff */
        _background-image:none;
        _filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true, sizingMethod=scale src='Mask.png');
        opacity:0.4;
        filter:alpha(opacity=70)
        }
        div.ModalPopup {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 11px;
        font-style: normal;
        background-color: #fff;
        position:absolute;
        /* set z-index higher than possible */
        z-index:10000;
        visibility: hidden;
         color: Black;
        border-style: solid;
        border-color: #999999;
        border-width: 1px;
        width: 350px;
        height :auto;
        }
    </style>
</head>
<body leftmargin="5" rightmargin="5" topmargin="5">
    <form id="form1" runat="server">         
        <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
        <div>
            <div class="dptitle">
                Email Configuration</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable">
            <tr>
                <td align="center" colspan="5">
                    Customer:<span style="color: #ff0000">*</span><strong> </strong>
                    <asp:DropDownList ID="drpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged"
                        Width="220px">
                    </asp:DropDownList><strong>&nbsp; </strong>Job Type:<span style="color: #ff0000">*</span><strong>
                    </strong>
                    <asp:DropDownList ID="drpJobtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpJobtype_SelectedIndexChanged">
                        <asp:ListItem Value="1">Journal</asp:ListItem>
                        <asp:ListItem Value="2">Books</asp:ListItem>
                        <asp:ListItem Value="4">Projects</asp:ListItem>
                    </asp:DropDownList>
                    Job:<span style="color: #ff0000">* </span>
                    <asp:DropDownList ID="drpParentJob" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpParentJob_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Search" /></td>
            </tr>
        </table>
        <br />
        <div id="divConfig" runat="server">
            <table id="tblConfig" runat="server" align="center" cellpadding="1" cellspacing="1"
                class="bordertable" width="90%">
                <tr style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email Config Type: &nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="drpEmailConfigType" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpEmailConfigType_SelectedIndexChanged">
                        </asp:DropDownList>&nbsp;<asp:ImageButton ID="imgbtnNew" runat="server" ImageAlign="AbsMiddle"
                            ImageUrl="~/images/tools/add.png" OnClick="imgbtnNew_Click"
                            ToolTip="New" />
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageAlign="AbsMiddle"
                            ImageUrl="~/images/tools/delete.png" OnClick="imgbtnDelete_Click" OnClientClick="javascript:return confirm('Confirm delete?');"
                            ToolTip="Delete" /></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email Stage:<span style="color: #ff0000">*</span></td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="drpEmailStage" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email Letter:<span style="color: #ff0000">*</span></td>
                    <td align="left" colspan="3" style="color: #000000">
                        <asp:DropDownList ID="drpEmailFormat" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="drpEmailFormat_SelectedIndexChanged">
                        </asp:DropDownList>
                        <a class="link1" href="#" onclick="javascript:if(this.document.form1.drpEmailFormat.value!=0)window.open('job_email_letter.aspx?id='+ this.document.form1.drpEmailFormat.value +'&m=prev','Preview','width=700,height=700,left=200,top=0,toolbars=no,scrollbars=yes,status=no,resizable=yes');else alert('Select a Email Letter');"><img border="0" align="absmiddle" src="images/tools/perview.png" title="Preview" /></a></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email Type:<span style="color: #ff0000">*</span><span style="color: #ff0000"></span></td>
                    <td align="left" colspan="3">
                        <asp:RadioButtonList ID="rblstEmailType" runat="server" BorderWidth="0px" CellPadding="0"
                            CellSpacing="0" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="CursorAdd">
                            <asp:ListItem Selected="True" Value="0">Single</asp:ListItem>
                            <asp:ListItem Value="1">Contributed</asp:ListItem>
                        </asp:RadioButtonList>&nbsp;<asp:CheckBox ID="chkAttachment" runat="server" Text="Has Attachments?"
                            CssClass="CursorAdd" /></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Additional Details: &nbsp;
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtEmailAddDetails" runat="server" CssClass="TxtBox" Width="225px"></asp:TextBox>
                        (eg: early october 2009)</td>
                </tr>
                <tr id="trFollowUp" runat="server" style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email Followup Type:<span style="color: #ff0000">*</span></td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="drpFollowUp" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr id="Tr2" runat="server" style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email Subject:</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="TxtBox" Width="550px"></asp:TextBox></td>
                </tr>
                <tr id="Tr3" runat="server" style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                        Email
                        From:<span style="color: #ff0000">*</span></td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="drpFrom" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr runat="server" style="font-size: 8pt">
                    <td align="left" style="width: 131px">
                    </td>
                    <td align="right" colspan="3">
                        <input id="btnOtherDetials" class="dpbutton" type="button" value="FTP Details" language="javascript"
                            onclick="return btnOtherDetials_onclick()" style="width: 80pt" /></td>
                </tr>
            </table>
            <br />
            <table id="tblContacts" align="center" cellpadding="1" cellspacing="1" class="bordertable"
                width="90%">
                <tr>
                    <td>
                        <strong>Contact Types:</strong><br />
                        <asp:ListBox ID="lstContTypes" runat="server" Width="370px" Height="80px" SelectionMode="Multiple">
                        </asp:ListBox></td>
                    <td>
                        <asp:Button ID="btnToadd" runat="server" CssClass="dpbutton" Font-Bold="True" Text=">"
                            Width="54px" OnClick="btnToadd_Click" /><br />
                        <asp:Button ID="btnTodel" runat="server" CssClass="dpbutton" Font-Bold="True" Text="<"
                            Width="54px" OnClick="btnTodel_Click" /></td>
                    <td>
                        <strong>To: </strong><span style="color: #ff0000">*</span><br />
                        <asp:ListBox ID="lstTo" runat="server" Width="370px" Height="80px" SelectionMode="Multiple">
                        </asp:ListBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Email Groups:
                            <img align="absMiddle" src="images/tools/add.png" style="cursor: pointer" id="IMG1" language="javascript" onclick="return IMG1_onclick()" title="New" /></strong><br />
                        <asp:ListBox ID="lstContGroups" runat="server" Width="370px" Height="80px" SelectionMode="Multiple">
                        </asp:ListBox></td>
                    <td>
                        <asp:Button ID="btnCcAdd" runat="server" CssClass="dpbutton" Font-Bold="True" Text=">"
                            Width="54px" OnClick="btnCcAdd_Click" /><br />
                        <asp:Button ID="btnCcdel" runat="server" CssClass="dpbutton" Font-Bold="True" Text="<"
                            Width="54px" OnClick="btnCcdel_Click" /></td>
                    <td>
                        <strong>Cc:</strong><br />
                        <asp:ListBox ID="lstCc" runat="server" Width="370px" Height="80px" SelectionMode="Multiple">
                        </asp:ListBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Emails:
                            <img align="absMiddle" src="images/tools/add.png" style="cursor: pointer" id="IMG2" language="javascript" onclick="return IMG2_onclick()" title="New" /></strong><br />
                        <asp:ListBox ID="lstContEmails" runat="server" Width="370px" Height="80px" SelectionMode="Multiple">
                        </asp:ListBox></td>
                    <td>
                        <asp:Button ID="btnBccAdd" runat="server" CssClass="dpbutton" Font-Bold="True" Text=">"
                            Width="54px" OnClick="btnBccAdd_Click" /><br />
                        <asp:Button ID="btnBccDel" runat="server" CssClass="dpbutton" Font-Bold="True" Text="<"
                            Width="54px" OnClick="btnBccDel_Click" /></td>
                    <td>
                        <strong>Bcc:</strong><br />
                        <asp:ListBox ID="lstBcc" runat="server" Width="370px" Height="80px" SelectionMode="Multiple">
                        </asp:ListBox></td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" OnClick="btnSave_Click"
                            Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="dpbutton" OnClick="btnCancel_Click"
                            Text="Cancel" OnClientClick="javascript: return confirm('You are about to clear this page, Are you sure?');" />
                        </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <asp:HiddenField ID="hfJobNumber" runat="server" />
                        <asp:HiddenField ID="hfJobTitle" runat="server" />
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
                        </div>
                        <div id="divPopFtp" class="ModalPopup">
                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td align="left" style="width: 40%; background-color: green; color: White; font-weight: bold"
                                        valign="top">
                                        &nbsp;Ftp Details</td>
                                    <td style="width: 60%; background-color: green; color: White; font-weight: bold;"
                                        align="right" valign="top">
                                        <a href="#" title="Close" onclick="javascript:closeModal();" style="color: White;">[x]</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>Job</strong>:</td>
                                    <td>
                                       <asp:TextBox ID="txtpopJob" runat="server" CssClass="TxtBox" Width="250px" ForeColor="Blue" ReadOnly="True" Font-Bold="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;<strong>Job Path</strong>:</td>
                                    <td>
                                        <asp:TextBox ID="txtJobPath" runat="server" CssClass="TxtBox" Width="250px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>Web Path:</strong></td>
                                    <td>
                                        <asp:TextBox ID="txtWebPath" runat="server" CssClass="TxtBox" Width="250px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;<strong>FTP Path</strong>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFTPPath" runat="server" CssClass="TxtBox" Width="250px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>
                                        User Name</strong>:</td>
                                    <td>
                                        <asp:TextBox ID="txtFtpUserName" runat="server" CssClass="TxtBox" Width="250px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        &nbsp;<strong>Password</strong>:</td>
                                    <td>
                                        <strong>
                                            <asp:TextBox ID="txtFtpPassword" runat="server" CssClass="TxtBox" Width="250px"></asp:TextBox></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr bgcolor="Honeydew">
                                    <td colspan="3" align="center">
                                        <a class="link1" href="#" onclick="javascript:saveFtp();"><strong>Update</strong></a>
                                        &nbsp; <a class="link1" href="#" onclick="javascript:closeModal();"><strong>Cancel</strong></a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
