﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delphi_Job_Issue.aspx.cs" Inherits="Delphi_Job_Issue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head runat="server">
    <title>Issues</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>

    <script type="text/javascript">
        var gvelem;
        var gvcolor;
        function setColor(element, val, val1, val2) {
            //alert(gvelem);
            if (gvelem != null) {//alert(gvelem.style.backgroundColor);
                gvelem.style.backgroundColor = gvcolor;
            }
            gvelem = element;
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            document.form1.hfI_ID.value = val;
            document.form1.hfI_Name.value = val1
            document.form1.hfI_Journal.value = val2;
            if (document.getElementById("lblIssueSummary"))
                document.getElementById("lblIssueSummary").innerText = "Issue : " + val2 + " " + val1;
            else if (document.all.lblIssueSummary)
                document.all.lblIssueSummary.innerText = "Issue : " + val2 + " " + val1;
            else if (document.form1.lblIssueSummary)
                document.form1.lblIssueSummary.innerText = "Issue : " + val2 + " " + val1;
            doTimer();
        }
        function setMouseOverColor(element) {
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            element.style.cursor = 'hand';
            element.style.textDecoration = 'underline';
        }
        function setMouseOutColor(element) {
            element.style.backgroundColor = gvcolor;
            element.style.textDecoration = 'none';
        }
        function vallidGraphic() {
            var msg = "";
            if (document.form1.drpGFigureType != null && document.form1.drpGFigureType.value == "0")
                msg += "* Select a Figure Type.\r\n";
            if (document.form1.drpGraphicType != null && document.form1.drpGraphicType.value == "0")
                msg += "* Select a Graphic Type.\r\n";
            if (document.form1.txtGraphicCount.value == "" || document.form1.txtGraphicCount.value == "0")
                msg += "* No. of Items should be greater than zero.\r\n";
            if (msg != "") {
                alert(msg);
                return false;
            }
            return true;
        }
        function issOnhold() {
            if (document.form1.hfI_ID.value == '') {
                alert('You should first create the Issue.');
                document.form1.chkIssueOnHold.checked = false;
                return;
            }
            if (!document.form1.chkIssueOnHold.checked) {
                if (confirm('This job is currently On Hold, Do you want to release?')) {
                    document.form1.chkIssueOnHold.checked = false;
                    __doPostBack('lnkIssueHold', '');
                }
                else document.form1.chkIssueOnHold.checked = true;
            }
            else {
                document.getElementById('divPopIssOnHold').style.visibility = 'visible';
                document.getElementById('divPopIssOnHold').style.display = '';
                document.getElementById('divPopIssOnHold').style.top = '150px';
                document.getElementById('divPopIssOnHold').style.left = '248px';
                if (typeof document.body.style.maxHeight == "undefined") {
                    var layer = document.getElementById('divPopIssOnHold');
                    layer.style.display = 'block';
                    var iframe = document.getElementById('iframetop');
                    iframe.style.display = 'block';
                    iframe.style.visibility = 'visible';
                    iframe.style.top = layer.offsetTop - 10;
                    iframe.style.left = layer.offsetLeft - 10;
                    iframe.style.width = layer.offsetWidth + 10;
                    iframe.style.height = layer.offsetHeight + 10;
                } else {
                    document.getElementById('divMasked').style.display = '';
                    document.getElementById('divMasked').style.visibility = 'visible';
                    document.getElementById('divMasked').style.top = '0px';
                    document.getElementById('divMasked').style.left = '0px';
                    document.getElementById('divMasked').style.width = document.documentElement.clientWidth + 'px';
                    document.getElementById('divMasked').style.height = document.documentElement.clientHeight + 'px';
                }
                document.form1.drpIssueOnHoldType.value = '0';
                document.form1.txtIssueOnHoldReason.value = '';
            }
        }
        function closeModalArtHold() {
            document.form1.chkIssueOnHold.checked = false;
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopIssOnHold').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function openAdvancedModal() {
            document.getElementById('divPopAdvancedSearch').style.visibility = 'visible';
            document.getElementById('divPopAdvancedSearch').style.display = '';
            document.getElementById('divPopAdvancedSearch').style.top = '65px';
            document.getElementById('divPopAdvancedSearch').style.left = '128px';
            if (typeof document.body.style.maxHeight == "undefined") {
                var layer = document.getElementById('divPopAdvancedSearch');
                layer.style.display = 'block';
                var iframe = document.getElementById('iframetop');
                iframe.style.display = 'block';
                iframe.style.visibility = 'visible';
                iframe.style.top = layer.offsetTop - 10;
                iframe.style.left = layer.offsetLeft - 10;
                iframe.style.width = layer.offsetWidth + 10;
                iframe.style.height = layer.offsetHeight + 10;
            } else {
                document.getElementById('divMasked').style.display = '';
                document.getElementById('divMasked').style.visibility = 'visible';
                document.getElementById('divMasked').style.top = '0px';
                document.getElementById('divMasked').style.left = '0px';
                document.getElementById('divMasked').style.width = document.documentElement.clientWidth + 'px';
                document.getElementById('divMasked').style.height = document.documentElement.clientHeight + 'px';
            }
            showhideAdvancedEndDate(document.form1.drpAdvRecExpr, 'divAdvRecEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvDueExpr, 'divAdvDueEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvHlfDueRecExpr, 'divAdvHlfDueEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvCatsDueExpr, 'divAdvCatsDueEndDate');
        }
        function closeAdvancedModal() {
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopAdvancedSearch').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function showhideAdvancedEndDate(me, ctrl) {
            var o;
            if (document.getElementById(ctrl)) o = document.getElementById(ctrl);
            else if (document.all.ctrl) o = document.all.ctrl;
            else if (document.form1.ctrl) o = document.form1.ctrl;
            //alert(me.innerText);   
            if (me.value == 'between')
            { o.style.display = 'block' }
            else { o.style.display = 'none'; }
        }
        function validSaveItem() {
            if (document.form1.drpIssueOnHoldType.value == '0') alert('Select an hold type');
            else if (document.form1.txtIssueOnHoldReason.value == '') alert('Enter hold reason');
            else __doPostBack('lnkIssueHold', '');
        }
        function validInvoiceTypeItem() {
            if (document.form1.drpCostInvoiceType != null && document.form1.drpCostInvoiceType.value != "0" && document.form1.drpCostInvoiceType.value == "4") {
                //            alert(document.getElementById ('divPopICostInvTypeItem'));
                //            alert(document.getElementById ('divMasked'));
                document.getElementById('divPopICostInvTypeItem').style.visibility = 'visible';
                document.getElementById('divPopICostInvTypeItem').style.display = '';
                document.getElementById('divPopICostInvTypeItem').style.top = '150px';
                document.getElementById('divPopICostInvTypeItem').style.left = '248px';
                if (typeof document.body.style.maxHeight == "undefined") {
                    var layer = document.getElementById('divPopICostInvTypeItem');
                    layer.style.display = 'block';
                    var iframe = document.getElementById('iframetop');
                    iframe.style.display = 'block';
                    iframe.style.visibility = 'visible';
                    iframe.style.top = layer.offsetTop - 10;
                    iframe.style.left = layer.offsetLeft - 10;
                    iframe.style.width = layer.offsetWidth + 10;
                    iframe.style.height = layer.offsetHeight + 10;
                } else {
                    document.getElementById('divMasked').style.display = '';
                    document.getElementById('divMasked').style.visibility = 'visible';
                    document.getElementById('divMasked').style.top = '0px';
                    document.getElementById('divMasked').style.left = '0px';
                    document.getElementById('divMasked').style.width = document.documentElement.clientWidth + 'px';
                    document.getElementById('divMasked').style.height = document.documentElement.clientHeight + 'px';
                }
                document.getElementById('txtBCpopInvTypeItem').select();
            }
            else { alert("Select Invoice Type to Additional Cost"); }
        }
        function closeModalICost() {
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopICostInvTypeItem').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function popICostPreview() {
            if (document.form1.drpCostInvoiceTypeItem != null && document.form1.drpCostInvoiceTypeItem.value != "0") {
                var id = document.form1.drpCostInvoiceTypeItem.value;
                var text = document.form1.drpCostInvoiceTypeItem[document.form1.drpCostInvoiceTypeItem.selectedIndex].text;
                text = text.replace('&', 'and');
                window.open('BookCostPreview.aspx?text=' + text + '&id=iti_' + id, 'Preview', 'width=640,height=480,left=200,top=50,toolbars=no,scrollbars=yes,status=yes,resizable=yes');
            }
            else alert('Select a Invoice Type Item');
        }

        function validSaveItemCost() {
            if (document.form1.txtBCpopInvTypeItem.value == '') alert('Enter Invoice Type Item');
            else document.getElementById('lnkCostAddInvTypeItem').click();
        }
        function printIssue() { if (document.form1.hfI_ID.value == "") { alert('Select a Issue'); return false; } var w = window.open('jobbag.aspx?jobid=' + document.form1.hfI_ID.value + '&jobtypeid=6&print=1', 'Preview', 'width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes'); w.focus(); return false; }

        function clearAdvancedCtrls() {
            document.form1.chkAdvOnHold.checked = false;
            document.form1.chkAdvCompleted.checked = false;
            document.form1.drpAdvJourCodeExp.value = "Like";
            document.form1.drpAdvIssueNumExp.value = "Like";
            document.form1.txtAdvJourCode.value = "";
            document.form1.txtAdvIssueNum.value = "";
            document.form1.drpAdvRecExpr.value = "between";
            document.form1.drpAdvDueExpr.value = "between";
            document.form1.drpAdvHlfDueRecExpr.value = "between";
            document.form1.drpAdvCatsDueExpr.value = "between";
            showhideAdvancedEndDate(document.form1.drpAdvRecExpr, 'divAdvRecEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvDueExpr, 'divAdvDueEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvHlfDueRecExpr, 'divAdvHlfDueEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvCatsDueExpr, 'divAdvCatsDueEndDate');
            document.form1.txtAdvRecDate1.value = "";
            document.form1.txtAdvRecDate2.value = "";
            document.form1.txtAdvDueDate1.value = "";
            document.form1.txtAdvDueDate2.value = "";
            document.form1.txtAdvHlfDueDate1.value = "";
            document.form1.txtAdvHlfDueDate2.value = "";
            document.form1.txtAdvCatsDueDate1.value = "";
            document.form1.txtAdvCatsDueDate2.value = "";
            if (navigator.userAgent.indexOf('Firefox') != -1) {
                document.form1.lstAdvancedStage.selectedIndex = -1;
                document.form1.lstAdvancedCustomer.selectedIndex = -1;
            }
            else {
                document.form1.lstAdvancedStage.value = -1;
                document.form1.lstAdvancedCustomer.value = -1;

            }
        }
        function clearListCtrl(ctrl) {
            if (navigator.userAgent.indexOf('Firefox') != -1)
                ctrl.selectedIndex = -1;
            else ctrl.value = -1;
        }
    </script>
    <script language="javascript">
        var cnt = 0;
        var tt;
        var timer_is_on = 0;
        var elemn;
        var h = 0;
        var max = 20;
        function timedCount() {
            //alert(document.getElementById('divfooter'));
            elemn = document.getElementById('divfooter');
            h = elemn.style.height.replace('px', '');
            if (timer_is_on && cnt <= max && h <= max) {
                //ctrl.value=cnt;	
                if (h == '') h = 0;
                //alert(h);
                elemn.style.height = parseInt(h) + 5 + 'px';
                //alert(cnt);
                cnt = cnt + 5;
                tt = setTimeout("timedCount()", 0);
            } else {
                timer_is_on = 0;
                cnt = 0;
            }
        }
        function doTimer() {
            if (!timer_is_on) {
                timer_is_on = 1;
                timedCount();
            } else timer_is_on = 0;
        }
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
        border-color: #10000;
        border-width: 1px;
        width: 300px;
        height :auto;
        }
    </style>
    <style type="text/css">
        .footer{	    
	    background-color: green;
	    height: 0.1px;
	    text-align: center;
	    font-size:11px;
	    font-weight:bold;
	    color:#FFFFFF;
	    font-family:Verdana;	
	    padding-top: 0px;	    
	    width: 120px;
	    position:fixed;	    
	    right: 3px;
	    bottom: 0px;
	    cursor:pointer;
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
<body>
    <form id="form1" runat="server">
        <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
            </div>        
        <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div>
                            <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3" style="height: 20px">
                                        <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Issue</strong></td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Issue No.</strong></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="318px" Style="text-transform: uppercase"
                                            CssClass="txtIssueSearch" TabIndex="1"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="dpbutton" OnClick="btnSearch_Click" TabIndex="3" />
                                        <input id="btnAdvanced" class="dpbutton"
                                                style="width: 80pt" type="button" value="Options" onclick="javascript: openAdvancedModal();" />
                                        <asp:HiddenField ID="hfI_ID" runat="server" />
                                        <asp:HiddenField ID="hfI_Name" runat="server" />
                                        <asp:HiddenField ID="hfI_Journal" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkViewCompleted" runat="server" Font-Bold="True" Text="Show Completed" TabIndex="2" /></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="divPopAdvancedSearch" class="ModalPopup" style="left: 0px; width: 576px;
                                            top: 1176px">
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" colspan="2" style="font-weight: bold; color: white; background-color: green">
                                                        &nbsp;Search Options
                                                    </td>
                                                    <td align="right" style="background-color: green; color: White; font-weight: bold;">
                                                        <a href="#" title="Close" onclick="javascript:closeAdvancedModal();" style="color: White;">
                                                            [x]</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Show</td>
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="chkAdvCompleted" runat="server"
                                                            Font-Bold="False" Text="View Completed" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Stage</td>
                                                    <td colspan="2"><asp:ListBox ID="lstAdvancedStage" runat="server" Width="377px" SelectionMode="Multiple">
                                                    </asp:ListBox>&nbsp;<a id="A1" class="link1" href="#" onclick="javascript:clearListCtrl(document.form1.lstAdvancedStage);" >Clear</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Journal Code:</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="drpAdvJourCodeExp" runat="server">
                                                            <asp:ListItem>Like</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtAdvJourCode" style="text-transform:uppercase" runat="server" CssClass="TxtBox" MaxLength="50" Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Issue Number:</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="drpAdvIssueNumExp" runat="server">
                                                            <asp:ListItem>Like</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtAdvIssueNum" runat="server" CssClass="TxtBox" MaxLength="50" Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;On Hold</td>
                                                    <td colspan="2">
                                                        <asp:CheckBox ID="chkAdvOnHold" runat="server" Font-Bold="False" Text="View On Hold" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Received Date:</td>
                                                    <td style="width: 228px">
                                                        <asp:DropDownList ID="drpAdvRecExpr" runat="server"
                                                            onchange="javascript:showhideAdvancedEndDate(this,'divAdvRecEndDate');">
                                                            <asp:ListItem>between</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                            <asp:ListItem>&gt;=</asp:ListItem>
                                                            <asp:ListItem>&gt;</asp:ListItem>
                                                            <asp:ListItem>&lt;=</asp:ListItem>
                                                            <asp:ListItem>&lt;</asp:ListItem>
                                                            <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; Date:
                                                        <asp:TextBox ID="txtAdvRecDate1" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvRecDate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img2" runat="server"  />
                                                    </td>
                                                    <td align="left">
                                                        <div id="divAdvRecEndDate" runat="server">
                                                            and Date:
                                                            <asp:TextBox ID="txtAdvRecDate2" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvRecDate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img4" runat="server"  /></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Due Date:</td>
                                                    <td style="width: 228px">
                                                        <asp:DropDownList ID="drpAdvDueExpr" runat="server"
                                                            onchange="javascript:showhideAdvancedEndDate(this,'divAdvDueEndDate');">
                                                            <asp:ListItem>between</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                            <asp:ListItem>&gt;=</asp:ListItem>
                                                            <asp:ListItem>&gt;</asp:ListItem>
                                                            <asp:ListItem>&lt;=</asp:ListItem>
                                                            <asp:ListItem>&lt;</asp:ListItem>
                                                            <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; Date:
                                                        <asp:TextBox ID="txtAdvDueDate1" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvDueDate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img3" runat="server"  />
                                                    </td>
                                                    <td align="left">
                                                        <div id="divAdvDueEndDate" runat="server">
                                                            and Date:
                                                            <asp:TextBox ID="txtAdvDueDate2" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvDueDate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img5" runat="server"  /></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Half Due Date:</td>
                                                    <td style="width: 228px">
                                                        <asp:DropDownList ID="drpAdvHlfDueRecExpr" runat="server"
                                                            onchange="javascript:showhideAdvancedEndDate(this,'divAdvHlfDueEndDate');">
                                                            <asp:ListItem>between</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                            <asp:ListItem>&gt;=</asp:ListItem>
                                                            <asp:ListItem>&gt;</asp:ListItem>
                                                            <asp:ListItem>&lt;=</asp:ListItem>
                                                            <asp:ListItem>&lt;</asp:ListItem>
                                                            <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; Date:
                                                        <asp:TextBox ID="txtAdvHlfDueDate1" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvHlfDueDate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img9" runat="server"  />
                                                    </td>
                                                    <td align="left">
                                                        <div id="divAdvHlfDueEndDate" runat="server">
                                                            and Date:
                                                            <asp:TextBox ID="txtAdvHlfDueDate2" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvHlfDueDate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img10" runat="server"  /></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;
                                                         Due Date:</td>
                                                    <td style="width: 228px">
                                                        <asp:DropDownList ID="drpAdvCatsDueExpr" runat="server"
                                                            onchange="javascript:showhideAdvancedEndDate(this,'divAdvCatsDueEndDate');">
                                                            <asp:ListItem>between</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                            <asp:ListItem>&gt;=</asp:ListItem>
                                                            <asp:ListItem>&gt;</asp:ListItem>
                                                            <asp:ListItem>&lt;=</asp:ListItem>
                                                            <asp:ListItem>&lt;</asp:ListItem>
                                                            <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; Date:
                                                        <asp:TextBox ID="txtAdvCatsDueDate1" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvCatsDueDate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img11" runat="server"  />
                                                    </td>
                                                    <td align="left">
                                                        <div id="divAdvCatsDueEndDate" runat="server">
                                                            and Date:
                                                            <asp:TextBox ID="txtAdvCatsDueDate2" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvCatsDueDate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img12" runat="server"  /></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="1" style="width: 108px">
                                                        &nbsp;Customer:</td>
                                                    <td colspan="2">
                                                        <asp:ListBox ID="lstAdvancedCustomer" runat="server" Width="377px" SelectionMode="Multiple"></asp:ListBox>
                                                        <a id="linkAdvCustomer" class="link1" href="#" onclick="javascript:clearListCtrl(document.form1.lstAdvancedCustomer);" >Clear</a>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                    </td>
                                                    <td style="width: 228px">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr bgcolor="Honeydew">
                                                    <td align="center" colspan="3">
                                                        <a class="link1" href="#" onclick="javascript:clearAdvancedCtrls();"><strong>Clear-All</strong></a>
                                                        &nbsp; <a class="link1" href="#" onclick="javascript:__doPostBack('btnSearch','advanced');"><strong>Search</strong></a>
                                                        &nbsp; <a class="link1" href="#" onclick="javascript:closeAdvancedModal();"><strong>
                                                            Cancel</strong></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <strong>&nbsp;</strong>
                        </div>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server" OnClick="lnkGeneral_Click" TabIndex="4">General</asp:LinkButton></li>
                            <li id="miIssueDetails" runat="server">
                                <asp:LinkButton ID="lnkIssuedetails" runat="server" OnClick="lnkIssuedetails_Click" TabIndex="5">Issue Details</asp:LinkButton></li>
                            <li id="miIssueAddCost" runat="server">
                                <asp:LinkButton ID="lnkIssueAddCost" runat="server" OnClick="lnkIssueAddCost_Click" TabIndex="6" Visible="false" >Costing</asp:LinkButton></li>
                            <li id="miIssueEvents" runat="server">
                                <asp:LinkButton ID="lnkIssueEvents" runat="server" OnClick="lnkIssueEvents_Click" TabIndex="7">Events</asp:LinkButton></li>                            
                            <li id="miComments" runat="server">
                                <asp:LinkButton ID="lnkComments" runat="server" OnClick="lnkComments_Click" TabIndex="8">Comments</asp:LinkButton></li>
                            <li id="miArticlesAssigned" runat="server">
                                <asp:LinkButton ID="lnkArticlesAssigned" runat="server" OnClick="lnkArticlesAssigned_Click" TabIndex="9">Articles Assigned</asp:LinkButton></li>
                        </ol>
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblIssueSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                    <td align="right">
                                        <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                                ToolTip="Export Excel" OnClick="cmd_Excel_Export_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvIssues" runat="server" Width="100%" OnRowDataBound="gvIssues_RowDataBound"
                                            AutoGenerateColumns="false" Font-Size="8pt" CssClass="lightbackground" AllowSorting="True" OnSorting="gvIssues_Sorting">
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvIssueID" runat="server" Value='<%# Eval("parent_job_id") %>' />
                                                        <asp:HiddenField ID="hfgvIssueName" runat="server" Value='<%# Eval("name") %>' />
                                                        <asp:HiddenField ID="hfgvJournal" runat="server" Value='<%# Eval("journal_code") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job No." SortExpression="parent_job_id" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobNumber" runat="server" Text='<%# Eval("parent_job_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage" SortExpression="job_stage_name" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobStage" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Journal Code" SortExpression="journal_code" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJourCode" runat="server" Text='<%# Eval("journal_code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue No." SortExpression="name" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIssueNo" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Title" SortExpression="title" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIssueTitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rec. Date" SortExpression="received_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvARecDate" runat="server" Text='<%# Eval("received_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Due Date" SortExpression="due_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvADueDate" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disp. Date" SortExpression="despatch_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvADispDate" runat="server" Text='<%# Eval("despatch_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Copy Edit Due Date" SortExpression="Actual_due_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAActualDueDate" runat="server" Text='<%# Eval("Actual_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cats Due Date" SortExpression="cats_due_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvACatsDueDate" runat="server" Text='<%# Eval("cats_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Hold Details" SortExpression="Hold_details" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvHoldDetails" runat="server" Text='<%# Eval("Hold_details") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice No." SortExpression="invoice_no" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAInvNo" runat="server" Text='<%# Eval("invoice_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabIssueDetails" runat="server">
                            <div id="CUSTOMER_TABLE" class="boxTable">
                            </div>
                            <div id="PARENT_JOB" class="boxTable" style="">
                                <table id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                    <tr bgcolor="#f0fff0">
                                        <td colspan="4" class="dpJobGreenHeader">
                                            <img id="imgIssueHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblIssueHeader" runat="server" Text="Label">New Issue</asp:Label></td>
                                        <td class="dpJobGreenHeader">
                                            <asp:ImageButton ID="cmd_New_Issue" ImageUrl="~/images/tools/j_new.png" runat="server"
                                                ToolTip="New Issue" OnClick="cmd_New_Issue_Click" TabIndex="40" />
                                            <asp:ImageButton ID="cmd_Save_Issue" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save Issue" OnClick="cmd_Save_Issue_Click" TabIndex="41" />
                                            <asp:ImageButton ID="cmd_Print_Issue" ImageUrl="~/images/tools/j_print.png" runat="server"
                                                ToolTip="Print Preview" OnClick="cmd_Print_Issue_Click" OnClientClick="javascript:return printIssue()" TabIndex="42" />
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="drpIssuecustomer" runat="server" Width="500px" OnSelectedIndexChanged="drpIssuecustomer_SelectedIndexChanged"
                                                AutoPostBack="True" TabIndex="10">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Journal: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="drpIssueJournal" runat="server" Width="500px" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="drpIssueJournal_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Issue No.: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtIssueNo" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="108px" MaxLength="150" TabIndex="12"></asp:TextBox>
                                            <asp:CheckBox ID="chkIssueOnHold" onclick="javascript:issOnhold();" runat="server"
                                                Font-Bold="False" Text="On Hold" TabIndex="13" /></td>
                                        <td>
                                            Issue&nbsp;Stage:</td>
                                        <td>
                                            <asp:DropDownList ID="drpIssueStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpIssueStage_SelectedIndexChanged" TabIndex="19">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Issue Title:</td>
                                        <td>
                                            <asp:TextBox ID="txtIssueTitle" runat="server" CssClass="TxtBox" Width="300px"
                                                MaxLength="300" TabIndex="14"></asp:TextBox></td>
                                        <td>
                                            Start Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtIssueSdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="20" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtIssueSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgID_stdate" runat="server" tabindex="21" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cover Month:</td>
                                        <td>
                                            <asp:DropDownList ID="drpIssueCoverMonth" runat="server" TabIndex="15">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Copy Edit Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtIssueActualDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="22" BackColor="#F1F1F1"></asp:TextBox>
                                            </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cover Year:</td>
                                        <td>
                                            <asp:DropDownList ID="drpIssueCoverYear" runat="server" TabIndex="16">
                                            </asp:DropDownList></td>
                                        <td>
                                            Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtIssueDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="24" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtIssueDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgID_dudate" runat="server" tabindex="25" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comments:</td>
                                        <td rowspan="3">
                                            <asp:TextBox ID="txtIssueComments" runat="server" CssClass="TxtBox" Height="50px"
                                                TextMode="MultiLine" Width="300px" TabIndex="17"></asp:TextBox></td>
                                        <td>
                                            Cats Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtIssueCDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="26" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtIssueCDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgID_cdudate" runat="server" tabindex="27" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            </td>
                                        <td>
                                            Despatch:</td>
                                        <td>
                                            <asp:CheckBox ID="chkIssueDespatch" runat="server" TabIndex="28" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkIssueHold" runat="server" OnClick="lnkIssueHold_Click"></asp:LinkButton></td>
                                        <td align="left" colspan="3" rowspan="7" valign="top">
                                            <div style="vertical-align: top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td colspan="3" class="subheading">
                                                            <strong>Completed Stage(s)</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="subheading" colspan="3" valign="top">
                                                            <asp:DataGrid ID="dgrdIssueStages" runat="server" AutoGenerateColumns="False" CssClass="lightbackground">
                                                                <AlternatingItemStyle CssClass="dullbackground" />
                                                                <HeaderStyle CssClass="darkbackground" />
                                                                <Columns>
                                                                    <asp:TemplateColumn HeaderText="Stage">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_StageName" runat="server" Text='<%# Eval("stypename") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Start Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_StartDate" runat="server" Text='<%# Eval("received_date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Due Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_DueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Copyedit Due Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_ActualdueDate" runat="server" Text='<%# Eval("Actual_due_date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Cats. Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_catsDate" runat="server" Text='<%# Eval("cats_due_date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                      <asp:TemplateColumn HeaderText="Desp. Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_DespDate" runat="server" Text='<%# Eval("despatch_date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="AP Stage Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAPStageName" runat="server"
                                                                            Text='<%# Eval("Ap_Stage_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                 </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Invoice Desc.</td>
                                        <td rowspan="3">
                                            <asp:TextBox ID="txtIssueInvoiceDesc" runat="server" CssClass="TxtBox" Height="50px" TextMode="MultiLine"
                                                Width="300px" TabIndex="18"></asp:TextBox>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sales Group: 
                                            </td>
                                        <td>
                                            <asp:DropDownList ID="drpIssueSalesGroup" runat="server" TabIndex="18" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Withdraw:</td>
                                        <td>
                                            <asp:DropDownList ID="drpWithdraw" runat="server">
                                                <asp:ListItem Value="1">N</asp:ListItem>
                                                <asp:ListItem Value="0">Y</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td align="left" colspan="3" rowspan="1" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="divPopIssOnHold" class="ModalPopup">
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                            width: 163px;">
                                                            &nbsp;Issue On Hold
                                                        </td>
                                                        <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                            <a href="#" title="Close" onclick="javascript:closeModalArtHold();" style="color: White;">
                                                                [x]</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;OnHold Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:DropDownList ID="drpIssueOnHoldType" runat="server">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtIssueOnHoldReason" runat="server" CssClass="TxtBox" Width="180px"
                                                                MaxLength="300"></asp:TextBox></td>
                                                    </tr>
                                                    <tr bgcolor="Honeydew">
                                                        <td colspan="2" align="center">
                                                            <a class="link1" href="#" onclick="javascript:validSaveItem();"><strong>Submit</strong></a>
                                                            &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtHold();"><strong>Cancel</strong></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="content" id="tabIssueAddCost" runat="server">
                            <table id="Table1" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 657px">
                                        <img id="ImgIssueCost" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="lblCostHeader" runat="server" Text="Issue Cost"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="cmd_Cost_new" ImageUrl="~/images/tools/j_new.png" runat="server"
                                            ToolTip="New" OnClick="cmd_Cost_new_Click" />
                                        <asp:ImageButton ID="cmd_Save_Cost" ImageUrl="~/images/tools/j_save.png" runat="server"
                                            ToolTip="Save" OnClick="cmd_Save_Cost_Click" />
                                        <asp:ImageButton ID="cmd_Cost_orderindex" ImageUrl="~/images/tools/j_index.png" runat="server"
                                            ToolTip="Order Index" OnClick="cmd_Cost_orderindex_Click" /></td>
                                </tr>
                                <tr id="trBCCtrls" runat="server">
                                    <td colspan="5">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 137px; height: 21px;">
                                                    Invoice Type: <span style="font-size: 9pt; color: #ff0000">*</span>
                                                </td>
                                                <td style="width: 141px; height: 21px;">
                                                    <asp:DropDownList ID="drpCostInvoiceType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCostInvoiceType_SelectedIndexChanged"
                                                        Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 21px; width: 146px;">
                                                    &nbsp; Invoice Type Item: <span style="font-size: 9pt; color: #ff0000">*</span>&nbsp;</td>
                                                <td style="font-size: 8pt; height: 21px;">
                                                    <asp:DropDownList ID="drpCostInvoiceTypeItem" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                    <img id="imgbtnBCAddInvTypeItem" align="absMiddle" src="images/tools/add.png" style="cursor: pointer"
                                                        title="New Invoice Type Item" onclick="javascript:return validInvoiceTypeItem();" runat="server" /></td>
                                            </tr>
                                            <tr style="font-size: 8pt">
                                                <td style="width: 137px">
                                                    Type of Cost: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                <td style="width: 141px; font-size: 8pt;">
                                                    <asp:DropDownList ID="drpCostType" runat="server" Width="200px">
                                                    </asp:DropDownList></td>
                                                <td style="font-size: 8pt; width: 146px">
                                                    <span style="color: #000000">&nbsp; Quantity:</span></td>
                                                <td style="font-size: 8pt">
                                                    <asp:TextBox ID="txtCostQuantity" runat="server" Width="50px" CssClass="TxtBox" onkeypress="javascript: return OnlyAllowNumbers(this,event);"></asp:TextBox>&nbsp;
                                                    Price Code: <span style="font-size: 9pt; color: #ff0000">*
                                                        <asp:TextBox ID="txtCostPriceCode" runat="server" Width="50px" CssClass="TxtBox"
                                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" BackColor="#FFFFC0"></asp:TextBox></span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Item Description</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtCostItemdesc" runat="server" CssClass="TxtBox" MaxLength="100" ToolTip="Eg: 40204.PY9781420073669.PBHRD.XXXX"
                                                        Width="542px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:HiddenField ID="hfCostInvTypeItemID" runat="server" />
                                        <asp:LinkButton ID="lnkCostAddInvTypeItem" runat="server" OnClick="lnkCostAddInvTypeItem_Click"></asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvIssueCost" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                            Width="100%" OnRowCommand="gvIssueCost_RowCommand" OnRowDataBound="gvIssueCost_RowDataBound">
                                            <Columns>                                                
                                                <asp:TemplateField HeaderText="Invoice Type Item">
                                                    <ItemTemplate>
                                                        <%# Eval("InvoiceType_item_Name")%>
                                                        <asp:HiddenField ID="hfIC_invoicetypeitem" runat="server" Value='<%# Eval("job_invoice_type_item_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type of Cost">
                                                    <ItemTemplate>
                                                        <%# Eval("cost_type_name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <%# Eval("quantity_addnl_type")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price Code">
                                                    <ItemTemplate>
                                                        <%# Eval("price_code")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description">
                                                    <ItemTemplate>
                                                        <%# Eval("description")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Index">
                                                    <ItemTemplate>
                                                        <%# Eval("order_index")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Re-Index">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpIC_orderindex" runat="server" SelectedValue='<%# Eval("order_index") %>'>
                                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                            <asp:ListItem>7</asp:ListItem>
                                                            <asp:ListItem>8</asp:ListItem>
                                                            <asp:ListItem>9</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>11</asp:ListItem>
                                                            <asp:ListItem>12</asp:ListItem>
                                                            <asp:ListItem>13</asp:ListItem>
                                                            <asp:ListItem>14</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                            <asp:ListItem>19</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnIC_Edit" runat="server" ImageUrl="~/images/tools/edit.png"
                                                            ToolTip="Edit" CommandName="ICEdit" />
                                                        <asp:ImageButton ID="imgbtnIC_Delete" runat="server" ImageUrl="~/images/tools/delete.png"
                                                            ToolTip="Delete" CommandName="ICDelete" OnClientClick="javascript: return confirm('Confirm Delete?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <div id="divPopICostInvTypeItem" class="ModalPopup">
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                        width: 163px;">
                                                        &nbsp;New Invoice Type Item
                                                    </td>
                                                    <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                        <a href="#" title="Close" onclick="javascript:closeModalICost();" style="color: White;">
                                                            [x]</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 163px">
                                                        &nbsp;Invoice Type:</td>
                                                    <td>
                                                        <strong>Additional Cost</strong></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 163px">
                                                        &nbsp;Invoice Type Item:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtBCpopInvTypeItem" runat="server" CssClass="TxtBox" Width="120px"
                                                            MaxLength="50"></asp:TextBox></td>
                                                </tr>
                                                <tr bgcolor="Honeydew">
                                                    <td colspan="2" align="center">
                                                        <a class="link1" href="#" onclick="javascript:validSaveItemCost();">
                                                            <strong>Submit</strong></a> &nbsp; <a class="link1" href="#" onclick="javascript:closeModalICost();">
                                                                <strong>Cancel</strong></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabIssueEvents" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img7" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblEventsHeader" runat="server" Text="Logged Events"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="imgbtnEventExport" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                            ToolTip="Export Excel" OnClick="imgbtnEventExport_Click" />
                                            </td>
                                </tr>
                                <tr style="font-size: 8pt; font-family: Verdana">
                                    <td colspan="6">
                                        <div id="divEvents" style="display: block;">
                                            <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Job">
                                                        <ItemTemplate>
                                                            <%# Eval("name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Stage">
                                                        <ItemTemplate>
                                                            <%# Eval("job_stage_name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <ItemTemplate>
                                                            <%# Eval("task_name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time">
                                                        <ItemTemplate>
                                                            <%# Eval("start_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time">
                                                        <ItemTemplate>
                                                            <%# Eval("end_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration">
                                                        <ItemTemplate>
                                                            <%# Eval("total_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <%# Eval("fname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <%# Eval("comments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>                        
                        <div class="content" id="tabComments" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td class="dpJobGreenHeader" style="height: 32px">
                                        <img id="Img6" align="absmiddle" src="images/tools/comment.png" runat="server" />
                                        <asp:Label ID="lblCommentsHeader" runat="server" Text="Comments History"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divCommHistory" style="display: block;">
                                            <asp:GridView ID="gvCommHistory" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%# Eval("name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <%# Eval("comments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created by">
                                                        <ItemTemplate>
                                                            <%# Eval("fname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabArticlesAssigned" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td class="dpJobGreenHeader" style="height: 32px; width: 880px;">
                                        <img id="Img1" align="absmiddle" src="images/tools/new.png" runat="server" />
                                        <asp:Label ID="lblArticlesHeader" runat="server" Text="Articles Assigned"></asp:Label></td>
                                    <td class="dpJobGreenHeader" style="height: 32px">
                                        <asp:ImageButton ID="imgbtnPaginate" ImageUrl="~/images/tools/j_attach.png" runat="server"
                                            ToolTip="Assign/Paginate" OnClick="imgbtnPaginate_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div id="div2" style="display: block;">                                            
                                           <asp:GridView ID="gvArtilcesAssigned" CssClass="lightbackground" runat="server" Width="100%"
                                                AutoGenerateColumns="False">
                                                <RowStyle HorizontalAlign="Left" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.no">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>                                                                
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Manuscript ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvManuscript" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDocType" runat="server" Text='<%# Eval("document_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sub Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDocItemType" runat="server" Text='<%# Eval("document_item_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page From">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFrom" runat="server" Text='<%# Eval("page_from") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTo" runat="server" Text='<%# Eval("page_to") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTotal" runat="server" Width="80px" Text='<%# Eval("totpages") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                        
                                                </Columns>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkIssuedetails','');">Show Details</div>
    </form>
</body>