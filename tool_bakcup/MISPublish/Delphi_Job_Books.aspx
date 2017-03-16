<%@ page language="C#" autoeventwireup="true" inherits="Delphi_Job_Books, App_Web_25d24vps" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Book</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="scripts/tabs1.js"></script>--%>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>

    <script type="text/javascript">
        var gvelem;
        var gvcolor;
        function setColor(element, val, val1) {
            //alert(gvelem);
            if (gvelem != null) {//alert(gvelem.style.backgroundColor);
                gvelem.style.backgroundColor = gvcolor;
            }
            gvelem = element;
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            document.form1.hfB_ID.value = val;
            document.form1.hfB_Name.value = val1
            if (document.getElementById("lblBookSummary"))
                document.getElementById("lblBookSummary").innerText = "Book : " + val1;
            else if (document.all.lblBookSummary)
                document.all.lblBookSummary.innerText = "Book : " + val1;
            else if (document.form1.lblBookSummary)
                document.form1.lblBookSummary.innerText = "Book : " + val1;
            doTimer();
        }
        function setColor1(element, val, val1) {
            //alert(gvelem);
            if (gvelem != null) {//alert(gvelem.style.backgroundColor);
                gvelem.style.backgroundColor = gvcolor;
            }
            gvelem = element;
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            document.form1.hfC_ID.value = val;
            document.form1.hfB_Name.value = val1
            if (document.getElementById("lblBookSummary"))
                document.getElementById("lblBookSummary").innerText = "Book : " + val1;
            else if (document.all.lblBookSummary)
                document.all.lblBookSummary.innerText = "Book : " + val1;
            else if (document.form1.lblBookSummary)
                document.form1.lblBookSummary.innerText = "Book : " + val1;
            doTimer1();
        }
        function setMouseOverColor(element) {
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            element.style.cursor = 'pointer';
            element.style.textDecoration = 'underline';
        }
        function setMouseOutColor(element) {
            element.style.backgroundColor = gvcolor;
            element.style.textDecoration = 'none';
        }

        function imgReason_editor_onclick() {
            window.open("PEName.aspx?form=Projects&type=0&trgname=Dropcomplex", "Projects", "width=600,height=250,status=yes, scrollbars=yes");

        }
        
        function showhideDiv(me, ctrl) {//alert(document.getElementById("divCommHistory"));
            var o;
            if (document.getElementById(ctrl)) o = document.getElementById(ctrl);
            else if (document.all.ctrl) o = document.all.ctrl;
            else if (document.form1.ctrl) o = document.form1.ctrl;
            //alert(me.innerText);   
            if (o.style.display == '' || o.style.display == 'block') {
                o.style.display = 'none';
                if (document.all) me.innerText = '(Show)'; else me.textContent = '(Show)';
            }
            else {
                o.style.display = 'block'
                if (document.all) me.innerText = '(Hide)'; else me.textContent = '(Hide)';
            }
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
        function openModal() {
            if (document.getElementById('drpChapterName').value == '' || document.getElementById('drpChapterName').value == '0') {
                alert('* Select a Chapter Name');
                return;
            }
            if (document.getElementById('drpChapterName').value == '1') document.getElementById('txtChapPrefix').value = 'Toc';
            else if (document.getElementById('drpChapterName').value == '2') document.getElementById('txtChapPrefix').value = 'Chapter';
            else document.getElementById('txtChapPrefix').value = 'Appendix';
            document.getElementById('txtChapCount').value = '';
            document.getElementById('txtChapSdateNew').value = document.getElementById('txtChapSdate').value;
            document.getElementById('txtChapDdateNew').value = document.getElementById('txtChapDdate').value;
            document.getElementById('txtChapHDdateNew').value = document.getElementById('txtChapHDdate').value;
            document.getElementById('divPopChapter').style.visibility = 'visible';
            document.getElementById('divPopChapter').style.display = '';
            document.getElementById('divPopChapter').style.top = '150px';
            document.getElementById('divPopChapter').style.left = '248px';
            if (typeof document.body.style.maxHeight == "undefined") {
                var layer = document.getElementById('divPopChapter');
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
            document.getElementById('txtChapPrefix').select();
        }
        function closeModal() {
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopChapter').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function openAdvancedModal() {
            document.getElementById('divPopAdvancedSearch').style.visibility = 'visible';
            document.getElementById('divPopAdvancedSearch').style.display = '';
            document.getElementById('divPopAdvancedSearch').style.top = '85px';
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
            showhideAdvancedEndDate(document.form1.drpAdvancedExpression, 'divAdvencedEndDate');
        }
        function closeAdvancedModal() {
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopAdvancedSearch').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function validChapter() {
            var msg = "";
            if (document.getElementById('txtChapCount').value == '' || document.getElementById('txtChapCount').value == "0")
                msg += "* No. of Chapters should be greater than zero.\r\n";
            if (document.getElementById('txtChapSdateNew').value == '')
                msg += "* Select a Start Date.\r\n";
            if (document.getElementById('txtChapDdateNew').value == '')
                msg += "* Select a Due Date.\r\n";
            //if(document.getElementById('txtChapHDdateNew').value=='')
            //  msg+="* Select a HalfDue Date.\r\n";
            if (msg != "") alert(msg);
            else {
                document.getElementById('divMasked').style.display = 'none';
                document.getElementById('divPopChapter').style.display = 'none';
                document.getElementById('lnkChapAdd').click();
            }
        }
        function validInvoiceTypeItem() {
            if (document.form1.drpCostInvoiceType != null && document.form1.drpCostInvoiceType.value != "0" && document.form1.drpCostInvoiceType.value == "4") {
                //            alert(document.getElementById ('divPopBCostInvTypeItem'));
                //            alert(document.getElementById ('divMasked'));
                document.getElementById('divPopBCostInvTypeItem').style.visibility = 'visible';
                document.getElementById('divPopBCostInvTypeItem').style.display = '';
                document.getElementById('divPopBCostInvTypeItem').style.top = '150px';
                document.getElementById('divPopBCostInvTypeItem').style.left = '248px';
                if (typeof document.body.style.maxHeight == "undefined") {
                    var layer = document.getElementById('divPopBCostInvTypeItem');
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
        function validSaveItem() {
            if (document.form1.txtBCpopInvTypeItem.value == '') alert('Enter Invoice Type Item');
            else document.getElementById('lnkCostAddInvTypeItem').click();
        }
        function closeModalBCost() {
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopBCostInvTypeItem').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function printBook() { if (document.form1.hfB_ID.value == "") { alert('Select a Book'); return false; } var w = window.open('Print_jobbag.aspx?jobid=' + document.form1.hfB_ID.value + '&jobtypeid=2&print=1', 'Preview', 'width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes'); w.focus(); return false; }
        function printChapter() { if (document.form1.drpChapterNo.value == "0") { alert('Select a Chapter'); return false; } var w = window.open('Print_jobbag.aspx?jobid=' + document.form1.drpChapterNo.value + '&jobtypeid=7&print=1', 'Preview', 'width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes'); w.focus(); return false; }

        function popBCostPreview() {
            if (document.form1.drpCostInvoiceTypeItem != null && document.form1.drpCostInvoiceTypeItem.value != "0") {
                var id = document.form1.drpCostInvoiceTypeItem.value;
                var text = document.form1.drpCostInvoiceTypeItem[document.form1.drpCostInvoiceTypeItem.selectedIndex].text;
                text = text.replace('&', 'and');
                window.open('BookCostPreview.aspx?text=' + text + '&id=iti_' + id, 'Preview', 'width=640,height=480,left=200,top=50,toolbars=no,scrollbars=yes,status=yes,resizable=yes');
            }
            else alert('Select a Invoice Type Item');
        }
        function clearAdvancedCtrls() {
            document.form1.rblstAdvanced_0.checked = true;
            document.form1.drpAdvancedExpression.value = "between";
            showhideAdvancedEndDate(document.form1.drpAdvancedExpression, 'divAdvencedEndDate');
            document.form1.txtAdvancedDate1.value = "";
            document.form1.txtAdvancedDate2.value = "";
            if (navigator.userAgent.indexOf('Firefox') != -1) {
                document.form1.lstAdvancedFormat.selectedIndex = -1;
                document.form1.lstAdvancedCustomer.selectedIndex = -1;
                document.form1.lstAdvancedStage.selectedIndex = -1;
            }
            else {
                document.form1.lstAdvancedFormat.value = -1;
                document.form1.lstAdvancedCustomer.value = -1;
                document.form1.lstAdvancedStage.value = -1;
            }
        }
        function clearListCtrl(ctrl) {
            if (navigator.userAgent.indexOf('Firefox') != -1)
                ctrl.selectedIndex = -1;
            else ctrl.value = -1;
        }
        function isBOnhold() {
            if (document.form1.hfB_ID.value == '') {
                alert('You should first create the Book.');
                document.form1.chkBookOnHold.checked = false;
                return;
            }
            if (!document.form1.chkBookOnHold.checked) {
                if (confirm('This job is currently On Hold, Do you want to release?')) {
                    document.form1.chkBookOnHold.checked = false;
                    __doPostBack('lnkBookHold', '');
                }
                else document.form1.chkBookOnHold.checked = true;
            }
            else {
                document.getElementById('divPopIsBOnHold').style.visibility = 'visible';
                document.getElementById('divPopIsBOnHold').style.display = '';
                document.getElementById('divPopIsBOnHold').style.top = '150px';
                document.getElementById('divPopIsBOnHold').style.left = '248px';
                if (typeof document.body.style.maxHeight == "undefined") {
                    var layer = document.getElementById('divPopIsBOnHold');
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
                document.form1.drpBookOnHoldType.value = '0';
                document.form1.txtBookOnHoldReason.value = '';
            }
        }
        function closeModalArtHold() {
            document.form1.chkBookOnHold.checked = false;
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopIsBOnHold').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function validSaveItem_hold() {
            if (document.form1.drpBookOnHoldType.value == '0') alert('Select an hold type');
            else if (document.form1.txtBookOnHoldReason.value == '') alert('Enter hold reason');
            else __doPostBack('lnkBookHold', '');
        }
        function isCOnhold() {
            alert(document.form1.hfB_jobidforhold.value);
            if (document.form1.hfB_ID.value == '') {
                alert('You should first create the Book.');
                document.form1.chkChapterOnHold.checked = false;
                return;
            }
            if (document.form1.hfB_jobidforhold.value == '' || document.form1.hfB_jobidforhold.value == 0) {
                alert('You should first create the Chapter.');
                document.form1.chkChapterOnHold.checked = false;
                return;
            }
            if (!document.form1.chkChapterOnHold.checked) {
                if (confirm('This job is currently On Hold, Do you want to release?')) {
                    document.form1.chkChapterOnHold.checked = false;
                    __doPostBack('lnkChapterHold', '');
                }
                else document.form1.chkChapterOnHold.checked = true;
            }
            else {
                document.getElementById('divPopIsCOnHold').style.visibility = 'visible';
                document.getElementById('divPopIsCOnHold').style.display = '';
                document.getElementById('divPopIsCOnHold').style.top = '150px';
                document.getElementById('divPopIsCOnHold').style.left = '248px';
                if (typeof document.body.style.maxHeight == "undefined") {
                    var layer = document.getElementById('divPopIsCOnHold');
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
                document.form1.drpChapterOnHoldType.value = '0';
                document.form1.txtChapterOnHoldReason.value = '';
            }
        }
        function closeModalArtCHold() {
            document.form1.chkChapterOnHold.checked = false;
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopIsCOnHold').style.display = 'none';
            document.getElementById('iframetop').style.display = 'none';
        }
        function validSaveItem_Chold() {
            if (document.form1.drpChapterOnHoldType.value == '0') alert('Select an hold type');
            else if (document.form1.txtChapterOnHoldReason.value == '') alert('Enter hold reason');
            else __doPostBack('lnkChapterHold', '');
        }
    </script>
     <script type="text/javascript">
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
         function timedCount1() {
             //alert(document.getElementById('divfooter'));
             elemn = document.getElementById('divfooter1');
             h = elemn.style.height.replace('px', '');
             if (timer_is_on && cnt <= max && h <= max) {
                 //ctrl.value=cnt;	
                 if (h == '') h = 0;
                 //alert(h);
                 elemn.style.height = parseInt(h) + 5 + 'px';
                 //alert(cnt);
                 cnt = cnt + 5;
                 tt = setTimeout("timedCount1()", 0);
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
         function doTimer1() {
             if (!timer_is_on) {
                 timer_is_on = 1;
                 timedCount1();
             } else timer_is_on = 0;
         }
    </script>
    <style type="text/css">
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
        .rotate {

/* Safari */
-webkit-transform: rotate(-90deg);

/* Firefox */
-moz-transform: rotate(-90deg);

/* IE */
-ms-transform: rotate(-90deg);

/* Opera */
-o-transform: rotate(-90deg);
/* Internet Explorer */
filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);

font-family: arial;
font-size:10px;
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
        <iframe width="0" scrolling="no" height="0" frameborder="0" class="divMasked" id="iframetop">
        </iframe>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div>
                            <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3" style="height: 20px">
                                        <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Book</strong></td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Customer</strong></td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="325px" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="chkViewCompleted" runat="server" Font-Bold="True" Text="Show Completed Jobs"
                                            TabIndex="2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Book No.</strong></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="318px" CssClass="TxtBoxSearch"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="width: 80pt" CssClass="dpbutton"
                                            OnClick="btnSearch_Click" TabIndex="4" />&nbsp;
                                            <%--<input id="btnAdvanced" class="dpbutton"
                                                style="width: 80pt" type="button" value="Options" visible="false" onclick="javascript:openAdvancedModal();" />--%>
                                        <asp:HiddenField ID="hfB_ID" runat="server" />
                                        <asp:HiddenField ID="hfB_Name" runat="server" />
                                        <asp:HiddenField ID="hfB_typeid" runat="server" />
                                        <asp:HiddenField ID="hfB_jobidforhold" runat="server" />
                                        <asp:HiddenField ID="hfC_ID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="divPopAdvancedSearch" class="ModalPopup" style="left: 0px; width: 576px;
                                            top: 0px">
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
                                                    <td align="left" colspan="3">
                                                        <asp:RadioButtonList ID="rblstAdvanced" runat="server" Font-Bold="False" RepeatDirection="Horizontal"
                                                            CellSpacing="0" RepeatLayout="Flow">
                                                            <asp:ListItem Value="completed" Selected="True">View Completed</asp:ListItem>
                                                            <asp:ListItem Value="despatched">Despatched</asp:ListItem>
                                                            <asp:ListItem Value="invoiced">Invoiced</asp:ListItem>
                                                            <asp:ListItem Value="all">All Books</asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 108px">
                                                        &nbsp;Date Range:</td>
                                                    <td style="width: 228px">
                                                        <asp:DropDownList ID="drpAdvancedExpression" runat="server"
                                                            onchange="javascript:showhideAdvancedEndDate(this,'divAdvencedEndDate');">
                                                            <asp:ListItem>between</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                            <asp:ListItem>&gt;=</asp:ListItem>
                                                            <asp:ListItem>&gt;</asp:ListItem>
                                                            <asp:ListItem>&lt;=</asp:ListItem>
                                                            <asp:ListItem>&lt;</asp:ListItem>
                                                            <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                                                        </asp:DropDownList>&nbsp; Date:
                                                        <asp:TextBox ID="txtAdvancedDate1" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvancedDate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img2" runat="server"  />
                                                    </td>
                                                    <td align="left">
                                                        <div id="divAdvencedEndDate" runat="server">
                                                            and Date:
                                                            <asp:TextBox ID="txtAdvancedDate2" runat="server" CssClass="TxtBox" Width="80px" ondblclick="javascript:this.value='';" BackColor="#F1F1F1"></asp:TextBox>
                                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtAdvancedDate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img4" runat="server"  /></div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="1" style="width: 108px">
                                                        &nbsp;Format/Style:</td>
                                                    <td colspan="2">
                                                        <asp:ListBox ID="lstAdvancedFormat" runat="server" Width="377px" SelectionMode="Multiple"></asp:ListBox>
                                                        <a id="linkAdvFormat" class="link1" href="#" onclick="javascript:clearListCtrl(document.form1.lstAdvancedFormat);" >Clear</a>
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
                                                    <td colspan="1" style="width: 108px">
                                                        &nbsp;Stage:</td>
                                                    <td colspan="2">
                                                        <asp:ListBox ID="lstAdvancedStage" runat="server" Width="377px" 
                                                            SelectionMode="Multiple" 
                                                            ></asp:ListBox>
                                                        <a id="linkAdvStage" class="link1" href="#" onclick="javascript:clearListCtrl(document.form1.lstAdvancedStage);" >Clear</a>
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server" OnClick="lnkGeneral_Click" TabIndex="5">General</asp:LinkButton></li>
                            <li id="miBookDetails" runat="server">
                                <asp:LinkButton ID="lnkBookdetails" runat="server" OnClick="lnkBookdetails_Click"
                                    TabIndex="6">Book Details</asp:LinkButton></li>
                            
                            <li id="miChapDetails" runat="server">
                                <asp:LinkButton ID="lnkChapterdetails" runat="server" OnClick="lnkChapterdetails_Click"
                                    TabIndex="8">Chapter Details</asp:LinkButton></li>
                            <li id="miContents" runat="server">
                                <asp:LinkButton ID="lnkContents" runat="server"  TabIndex="11" 
                                    onclick="lnkContents_Click">Contents</asp:LinkButton></li>
                             <li id="miChapContent" runat="server">
                                <asp:LinkButton ID="lnkChapContent" runat="server"  TabIndex="11" 
                                     onclick="lnkChapContent_Click">Chapter Contents</asp:LinkButton></li>
                            <li id="miBookEvents" runat="server">
                                <asp:LinkButton ID="lnkBookEvents" runat="server" OnClick="lnkBookEvents_Click" TabIndex="9">Book Logged Events</asp:LinkButton></li>
                            <li id="miChapterEvents" runat="server">
                                <asp:LinkButton ID="lnkChapterEvents" runat="server"  TabIndex="10" 
                                    onclick="lnkChapterEvents_Click">Chapter Logged Events</asp:LinkButton></li>
                           
                            <li id="mijobtrack" runat="server">
                                <asp:LinkButton ID="lnkJobTrack" runat="server" OnClick="lnkJobTrack_Click" TabIndex="12">Art Details</asp:LinkButton>
                            </li>    
                            <li id="miBookBarcode" runat="server">
                                <asp:LinkButton ID="lnkBookBarcode" runat="server" TabIndex="9" onclick="lnkBookBarcode_Click"  
                                   >Barcode Scanner</asp:LinkButton></li>
                        </ol>
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblBookSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                    <td align="right">
                                        <asp:ImageButton ID="cmd_Excel_Export" runat="server" 
                                            ImageUrl="~/images/tools/j_excel.png" OnClick="cmd_Excel_Export_Click" 
                                            ToolTip="Export Excel" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvBooks" runat="server" Width="100%" 
                                            AutoGenerateColumns="False" Font-Size="8pt" CssClass="lightbackground" AllowSorting="True"
                                            OnSorting="gvBooks_Sorting" onrowdatabound="gvBooks_RowDataBound" >
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvBookID" runat="server" Value='<%# Eval("bno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Job No." SortExpression="parent_job_id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobno" runat="server" Text='<%# Eval("bno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Customer" SortExpression="CUSTNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Book No." SortExpression="BNUMBER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBnumber" runat="server" Text='<%# Eval("BNUMBER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Book Title" SortExpression="BTITLE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBtitle" runat="server" Text='<%# Eval("BTITLE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Editor" SortExpression="CONFULLNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBEditor" runat="server" Text='<%# Eval("CONFULLNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pagination Platform" SortExpression="BSTYLE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBpagination" runat="server" Text='<%# Eval("BSTYLE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage" SortExpression="STYPENAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBStage" runat="server" Text='<%# Eval("STYPENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" SortExpression="STDESCRIPTION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBStatus" runat="server" Text='<%# Eval("STDESCRIPTION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rec. Date" SortExpression="BFIRSTSTARTDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBRecDate" runat="server" Text='<%# Eval("BFIRSTSTARTDATE","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Due Date" SortExpression="BFOURTHDUEDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBDueDate" runat="server" Text='<%# Eval("BFOURTHDUEDATE","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disp. Date" SortExpression="BFOURTHDISPATCH">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBDispDate" runat="server" Text='<%# Eval("BFOURTHDISPATCH","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proof Pages" SortExpression="Bnoofpages">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBproof" runat="server" Text='<%# Eval("Bnoofpages") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Templates" SortExpression="Template_Created">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBTemp" runat="server" Text='<%# Eval("Template_Created") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Trim" SortExpression="bsize">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBTrim" runat="server" Text='<%# Eval("bsize") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Leader" SortExpression="firstemployee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBPL" runat="server" Text='<%# Eval("firstemployee") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Barcode" SortExpression="BBarcode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBarcode" runat="server" Text='<%# Eval("BBarcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Type" SortExpression="BType">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
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
                        <div class="content" id="tabBookDetails" runat="server">
                            <div id="CUSTOMER_TABLE" class="boxTable">
                            </div>
                            <div id="PARENT_JOB" class="boxTable" style="">
                                <table id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                    <tr bgcolor="#f0fff0">
                                        <td colspan="3" class="dpJobGreenHeader">
                                            <img id="imgBookHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblBookHeader" runat="server" Text="Label">New Book</asp:Label></td>
                                        <td class="dpJobGreenHeader">
                                            <asp:ImageButton ID="cmd_New_Book" ImageUrl="~/images/tools/j_new.png" runat="server"
                                                ToolTip="New Book" OnClick="cmd_New_Book_Click" TabIndex="40" />
                                            <asp:ImageButton ID="cmd_Save_Book" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save Book" OnClick="cmd_Save_Book_Click" TabIndex="41" />
                                            <asp:ImageButton ID="cmd_Print_Book" ImageUrl="~/images/tools/j_print.png" runat="server"
                                                ToolTip="Print Preview" OnClick="cmd_Print_Book_Click" OnClientClick="javascript:return printBook()"
                                                TabIndex="42" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpBookCustomer" runat="server" Width="306px" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpBookcustfinsite_SelectedIndexChanged" TabIndex="12">
                                            </asp:DropDownList></td>
                                        <td>
                                            Size:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookSize" runat="server" CssClass="TxtBox" Width="200px" MaxLength="50"
                                                TabIndex="16"></asp:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cat ID #:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtBookNo" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="200px" MaxLength="150" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td style="font-size: 8pt">
                                            Print ISBN:</td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtBookPISBN" runat="server" CssClass="TxtBox" Width="200px" MaxLength="16"
                                                TabIndex="17"></asp:TextBox></td>
                                        <td style="font-size: 8pt">
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt">
                                        <td>
                                            Book Title:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtBookTitle" runat="server" CssClass="TxtBox" Width="300px" BackColor="#FFFFC0"
                                                MaxLength="300" TabIndex="14"></asp:TextBox></td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Financial Site:<span 
                                                style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpBookcustfinsite" runat="server" Width="306px" TabIndex="15">
                                            </asp:DropDownList></td>
                                        <td>
                                            Format/Style:</td>
                                        <td>
                                            <asp:DropDownList ID="drpBookTypeset" runat="server" TabIndex="19">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                       
                                    <tr>
                                        <td>
                                            Project Editor:</td>
                                        <td>
                                            <asp:DropDownList ID="txtBookEditor" runat="server" CssClass="TxtBox" Width="280px" TabIndex="22" BackColor="#F1F1F1"></asp:DropDownList>
                                            <%--<asp:TemplateField HeaderText="Job No." SortExpression="parent_job_id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobno" runat="server" Text='<%# Eval("bno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>--%>
                                            <img id="img58" language="javascript" 
                                                onclick="return imgReason_editor_onclick()" src="images/tools/new.png" 
                                                style="cursor: pointer; height: 16px;" tabindex="17" 
                                                title="The Language Work Inc" /><asp:ImageButton src="images/tools/Refresh.png" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"  />
                                                
                                            </td>
                                           <td>
                                            Purchase Order:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookPONumber" runat="server" CssClass="TxtBox" MaxLength="100"
                                                Width="200px" TabIndex="21"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                    <td colspan="5">
                                    <table>
                                    <tr>
                                    <td></td>
                                    <td></td>
                                    <td align="center">Start Date</td>
                                    <td></td>
                                    <td align="center">Due Date</td>
                                    <td></td>
                                    <td align="center">Half Due Date</td>
                                    <td></td>
                                    <td align="center">Employee</td>
                                    <td></td>
                                    <td align="center">Actual Dispatch Date</td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                    <td>Templates:</td>
                                    <td>
                                        <asp:TextBox ID="txtStartTemp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartTemp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_stdate" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueTemp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueTemp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img5" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfTemp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfTemp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img9" runat="server" tabindex="31" /></td><td></td>
                                                <td><asp:DropDownList ID="dropEmpTemp" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispTemp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispTemp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img12" runat="server" tabindex="31" /></td>
                                    </tr>
                                     <tr>
                                    <td></td>
                                    <td>Sample Templates:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartSample" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartSample','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img13" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueSample" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueSample','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img14" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfSample" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfSample','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img15" runat="server" tabindex="31" /></td><td></td>
                                          <td><asp:DropDownList ID="dropEmpSample" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispSample" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispSample','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img16" runat="server" tabindex="31" /></td>
                                    </tr>
                                     <tr>
                                    <td></td>
                                    <td>Page Proof:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartPProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartPProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img17" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDuePProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDuePProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img18" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfPProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfPProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img19" runat="server" tabindex="31" /></td><td></td>
                                         <td><asp:DropDownList ID="dropEmpPProof" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispPProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispPProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img20" runat="server" tabindex="31" /></td>
                                    </tr>
                                     <tr>
                                    <td></td>
                                    <td>1st Revises:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img21" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img22" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img23" runat="server" tabindex="31" /></td><td></td>
                                          <td><asp:DropDownList ID="dropEmpFirst" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispFirst" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispFirst','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img24" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>2nd Revises:</td>
                                     <td>
                                        <asp:TextBox ID="txtStartSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img25" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img26" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img27" runat="server" tabindex="31" /></td><td></td>
                                           <td><asp:DropDownList ID="dropEmpSecond" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispSecond" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispSecond','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img28" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>3rd Revises:</td>
                                    <td>
                                        <asp:TextBox ID="txtStartThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img29" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img30" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img31" runat="server" tabindex="31" /></td><td></td>
                                       <td><asp:DropDownList ID="dropEmpThird" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                       <td><asp:TextBox ID="txtDispThird" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispThird','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img32" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>Final Proof:</td>
                                    <td>
                                        <asp:TextBox ID="txtStartFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img33" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtDueFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDueFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img34" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtHalfFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtHalfFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img35" runat="server" tabindex="31" /></td><td></td>
                                           <td><asp:DropDownList ID="dropEmpFinal" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtDispFProof" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtDispFProof','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img36" runat="server" tabindex="31" /></td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="5">
                                    <table>
                                    <tr>
                                    <td>Pages:</td>
                                    <td><asp:TextBox ID="txtPages" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Figures:</td>
                                    <td><asp:TextBox ID="txtFig" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Tables:</td>
                                    <td><asp:TextBox ID="txtTable" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Equations:</td>
                                    <td><asp:TextBox ID="txtEqu" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Credited for India:</td>
                                    <td>
                                        <asp:DropDownList ID="DropCrdInd" runat="server">
                                            <asp:ListItem>N</asp:ListItem>
                                            <asp:ListItem>Y</asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td>
                                    <td>Credited:</td>
                                    <td><asp:DropDownList ID="DropCrd" runat="server">
                                        <asp:ListItem>N</asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                        </asp:DropDownList></td><td></td>
                                    </tr>
                                    <tr>
                                    <td>Current Status:</td>
                                    <td><asp:DropDownList ID="dropCurStatus"  Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Current Stage:</td>
                                    <td><asp:DropDownList ID="DropCurStage" Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Elec File Disp:</td>
                                    <td><asp:DropDownList Width="50" ID="dropFileDisp" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Y</asp:ListItem>
                                        <asp:ListItem>N</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>Book Received:</td>
                                    <td colspan="3"><asp:TextBox ID="txtBookRecived" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBookRecived','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img38" runat="server" tabindex="31" />
                                           </td>
                                  <td>Email Type:</td>
                                    
                                    </tr>          
                                     <tr>
                                    <td>Employee:</td>
                                    <td><asp:DropDownList ID="DropEmpnameMain" Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Department:</td>
                                    <td><asp:DropDownList ID="DropDepartment" Width="100" runat="server">
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Wet Pages:</td>
                                    <td><asp:TextBox ID="TextBox35" Width="70" runat="server"></asp:TextBox> </td><td></td>
                                    <td>W Disp Date:</td>
                                    <td colspan="3"><asp:TextBox ID="txtWDispDate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtWDispDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img37" runat="server" tabindex="31" />
                                                  
                                                  <td rowspan="2" colspan="2">
                                        <asp:RadioButtonList  ID="rbEmailType" runat="server" Width="99px">
                                            <asp:ListItem Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1">Single</asp:ListItem>
                                            <asp:ListItem Value="2">Contributed</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                                </td>
                                    </tr>
                                    <tr>
                                    <td>Color Inserts:</td>
                                    <td><asp:TextBox ID="txtcolorInsert" Width="70" runat="server"></asp:TextBox>
                                    </td><td></td>
                                      <td>Live/Sample:</td>
                                    <td><asp:DropDownList ID="dropLiveSamp" runat="server">
                                        <asp:ListItem>L</asp:ListItem>
                                        <asp:ListItem>S</asp:ListItem>
                                        </asp:DropDownList>
                                    </td><td></td>
                                      <td>Template Created:</td>
                                    <td><asp:DropDownList ID="dropTempCreated" runat="server">
                                        <asp:ListItem>Y</asp:ListItem>
                                        <asp:ListItem>N</asp:ListItem>
                                        </asp:DropDownList></td><td></td>
                                    <td>ISBN2:</td>
                                    <td><asp:TextBox ID="txtISBN2" Width="70" runat="server"></asp:TextBox>
                                           </td><td></td>
                                    </tr>
                                     <tr>
                                     <td>Template Cost:</td>
                                    <td><asp:TextBox ID="txtTempCost" Width="90" runat="server"></asp:TextBox>
                                    </td><td></td>
                                     <td>Email:</td>
                                    <td><asp:TextBox ID="txtEmail" Width="90" runat="server"></asp:TextBox>
                                    </td><td></td>
                                    <td>Price No:</td>
                                    <td><asp:TextBox ID="txtPriceNo" Width="70" runat="server"></asp:TextBox>
                                    </td><td></td>
                                      <td>Credited Price No:</td>
                                    <td>
                                        <asp:TextBox ID="crdPriceNo" Width="70" runat="server"></asp:TextBox>
                                    </td><td></td>
                                      <td>&nbsp;</td>
                                    <td>&nbsp;</td><td></td>
                                    <td>&nbsp;</td>
                                    <td colspan="2">&nbsp;</td>
                                    </tr>
                                    <tr><td>Typesetting Print Area:</td><td><asp:TextBox ID="txtTypesetPrint" Width="90" 
                                            runat="server"></asp:TextBox>
                                        </td><td></td><td>Outputting Print Area:</td><td><asp:TextBox ID="txtOutputPrint" 
                                            Width="90" runat="server"></asp:TextBox>
                                        </td><td></td><td>&nbsp;</td><td>
                                        <asp:TextBox ID="txtPriceNo1" Width="90" 
                                            runat="server" Visible="False"></asp:TextBox>
                                        </td><td></td><td>&nbsp;</td><td><asp:TextBox ID="txtPriceNo2" Width="70" 
                                            runat="server" Visible="False"></asp:TextBox></td></tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="3">
                                    <table>
                                    <tr><td></td>
                                    <td>Addl. Cost1:</td><td><asp:TextBox ID="txtcost1" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity1:</td><td><asp:TextBox ID="txtQty1" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 1:</td><td><asp:TextBox ID="txtPrice1" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Add Price No 1:</td><td><asp:TextBox ID="txtAPrice1" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Type of Cost:</td><td></td><td>Work From:</td><td></td>
                                    <td colspan="2">Type:</td><td></td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost2:</td><td><asp:TextBox ID="txtcost2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity2:</td><td><asp:TextBox ID="txtQty2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 2:</td><td><asp:TextBox ID="txtPrice2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Add Price No 2:</td><td><asp:TextBox ID="txtAprice2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td rowspan="2" >
                                        <asp:RadioButtonList ID="rbTypeCost" runat="server" Height="75px" 
                                            Width="125px">
                                            <asp:ListItem Selected="True" Value="0">Page Cost</asp:ListItem>
                                            <asp:ListItem Value="1">Book Cost</asp:ListItem>
                                            <asp:ListItem Value="2">Additional Cost</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </td>
                                    <td></td>
                                    <td rowspan="2" colspan="2" >
                                        <asp:RadioButtonList ID="rbWorkFrom" runat="server" Width="99px">
                                            <asp:ListItem Value="2">Chennai</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="3">Coimbatore</asp:ListItem>
                                        </asp:RadioButtonList>

                                    </td>
                                    <td rowspan="2" colspan="2">
                                    <asp:RadioButtonList ID="rbType" runat="server" Width="99px">
                                            <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                            <asp:ListItem Value="1">Positive</asp:ListItem>
                                            <asp:ListItem Value="2">Negative</asp:ListItem>
                                            <asp:ListItem Value="3">FullService</asp:ListItem>
                                            <asp:ListItem Value="4">Editorial QC</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost3:</td><td><asp:TextBox ID="txtcost3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity3:</td><td><asp:TextBox ID="txtQty3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 3:</td><td><asp:TextBox ID="txtPrice3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Add Price No 3:</td><td><asp:TextBox ID="txtAPrice3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost4:</td><td><asp:TextBox ID="txtCost4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity4:</td><td><asp:TextBox ID="txtQty4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 4:</td><td><asp:TextBox ID="txtPrice4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Add Price No 4:</td><td><asp:TextBox ID="txtAPrice4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    </tr>
                                      <tr><td></td>
                                    <td>Addl. Cost5:</td><td><asp:TextBox ID="txtCost5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Quantity5:</td><td><asp:TextBox ID="txtQty5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Price No 5:</td><td><asp:TextBox ID="txtPrice5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    <td>Add Price No 5:</td><td><asp:TextBox ID="txtAPrice5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comments:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookComments" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex="24"></asp:TextBox></td>
                                    </tr>
                       
                                </table>
                            </div>
                        </div>
                       
                        <div class="content" id="tabChapterdetails" runat="server">
                            <table id="Table3" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr bgcolor="#f0fff0">
                                    <td colspan="4" class="dpJobGreenHeader">
                                        <img id="imgChapterHeader" align="absmiddle" src="images/tools/new.png" runat="server" />
                                        <asp:Label ID="lblChapHeader" runat="server" Text="Chapter Details"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <span style="font-size: 9pt; color: #ff0000"></span>
                                        <asp:ImageButton ID="cmd_Save_Chapter" ImageUrl="~/images/tools/j_save.png" runat="server"
                                            ToolTip="Save Chapter" OnClick="cmd_Save_Chapter_Click" TabIndex="40" />
                                        <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/tools/j_print.png" runat="server"
                                            ToolTip="Print" OnClientClick="javascript:printChapter();" TabIndex="41" />
                                    </td>
                                </tr>
                                <tr>
                                    <%-- <img id="imgBD_editor" align="absMiddle" src="images/tools/user_go.png" language="javascript"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select Editor"
                                                tabindex="23" />
                                            <asp:HiddenField ID="hfBookEditorId" runat="server" />--%>
                                </tr>
                                <tr>
                                <td colspan="8">
                                <asp:GridView ID="gvChapter" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="80%" OnRowDataBound="gvChapter_RowDataBound" 
                                        onrowcommand="gvChapter_RowCommand">
                                                <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvCID" runat="server" Value='<%# Eval("Cno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chapter Name">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCname" runat="server" Text='<%# Eval("cname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chapter No.">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCno" runat="server" Text='<%# Eval("no_chapters") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page Proof">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCpage" runat="server" Text='<%# Eval("no_pages") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Figures">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCFigure" runat="server" Text='<%# Eval("no_figures") %>'></asp:Label>
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
                                    <td style="width: 105px">
                                        Chapter Name:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                    <td style="width: 314px">
                                        <asp:DropDownList ID="drpChapterName" runat="server" 
                                            Width="204px" TabIndex="12" AutoPostBack="True" 
                                            onselectedindexchanged="drpChapterName_SelectedIndexChanged">
                                            <asp:ListItem>Front Matter</asp:ListItem>
                                            <asp:ListItem>Body Matter </asp:ListItem>
                                            <asp:ListItem>Back Matter </asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        </td>
                                    <td>
                                        Proof Pages:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapPages" runat="server" CssClass="TxtBox" Width="200px"
                                             MaxLength="15"
                                            TabIndex="18"></asp:TextBox></td>
                                    <td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Chapter No.:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="drpChapterNo" runat="server" Width="204px" TabIndex="13">
                                        </asp:TextBox>
                                    </td>
                                    <td>
                                        MS Pages:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapMSpages" runat="server" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15" TabIndex="17"></asp:TextBox></td>
                                    <td>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td style="width: 105px">
                                        Chapter Title:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapterTitle" runat="server" CssClass="TxtBox" Width="300px"
                                            ToolTip="Enter Chapter Title" MaxLength="300" TabIndex="16"></asp:TextBox></td>
                                    <td>
                                        Figures:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapFigures" runat="server" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15" TabIndex="19"></asp:TextBox></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Equations:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapEquations" runat="server" CssClass="TxtBox" Width="200px"
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" MaxLength="15"
                                            TabIndex="21"></asp:TextBox></td>
                                    <td>
                                        Tables:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapTables" runat="server" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15" TabIndex="20"></asp:TextBox></td>
                                    <td>
                                    </td>
                                </tr>
                            <tr>
                            <td>
                            Start Page:
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartPages" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            End Page:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndPages" runat="server"></asp:TextBox>
                            </td>
                        
                            </tr>
                                <tr>
                            <td>
                           Blank Page No:
                            </td>
                            <td>
                                <asp:TextBox ID="txtBlankPage" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            Chapter Pages:
                            </td>
                            <td>
                                <asp:TextBox ID="txtChapterPage" runat="server"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td colspan="5">
                            <table>
                            <tr>
                                    <td></td>
                                    <td></td>
                                    <td align="center">Start Date</td>
                                    <td></td>
                                    <td align="center">Due Date</td>
                                    <td></td>
                                    <td align="center">Half Due Date</td>
                                    <td></td>
                                    <td align="center">Employee</td>
                                    <td></td>
                                    <td align="center">Actual Dispatch Date</td>
                                    </tr>
                              <tr>
                                    <td></td>
                                    <td>Page Proof:</td>
                                     <td>
                                        <asp:TextBox ID="txtPageStart" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPageStart','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img3" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtPageDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPageDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img10" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtPageHalfDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPageHalfDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img11" runat="server" tabindex="31" /></td><td></td>
                                         <td><asp:DropDownList ID="dropPageEmp" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtPageDisp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPageDisp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img39" runat="server" tabindex="31" /></td>
                                    </tr>
                                     <tr>
                                    <td></td>
                                    <td>1st Revises:</td>
                                     <td>
                                        <asp:TextBox ID="txtFirstStart" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFirstStart','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img40" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtFirstDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFirstDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img41" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtFirstHalfDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFirstHalfDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img42" runat="server" tabindex="31" /></td><td></td>
                                          <td><asp:DropDownList ID="dropFirstEmp" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtFirstDisp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFirstDisp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img43" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>2nd Revises:</td>
                                     <td>
                                        <asp:TextBox ID="txtSecondStart" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtSecondStart','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img44" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtSecondDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtSecondDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img45" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtSecondHalfDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtSecondHalfDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img46" runat="server" tabindex="31" /></td><td></td>
                                           <td><asp:DropDownList ID="dropSecondEMp" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtSecondDisp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtSecondDisp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img47" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>3rd Revises:</td>
                                    <td>
                                        <asp:TextBox ID="txtThirdStart" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtThirdStart','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img48" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtThirdDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtThirdDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img49" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtTHirdHalfDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtTHirdHalfDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img50" runat="server" tabindex="31" /></td><td></td>
                                       <td><asp:DropDownList ID="dropTHirdEmp" Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                       <td><asp:TextBox ID="txtThirdDisp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtThirdDisp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img51" runat="server" tabindex="31" /></td>
                                    </tr>
                                      <tr>
                                    <td></td>
                                    <td>Final Proof:</td>
                                    <td>
                                        <asp:TextBox ID="txtFinalStart" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFinalStart','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img52" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtFinalDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFinalDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img53" runat="server" tabindex="31" /></td>
                                        <td></td>
                                        <td><asp:TextBox ID="txtFinalHalfDue" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFinalHalfDue','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img54" runat="server" tabindex="31" /></td><td></td>
                                           <td><asp:DropDownList ID="dropFinalEmp"  Width="100" runat="server">
                                                    </asp:DropDownList>
                                           </td><td></td>
                                        <td><asp:TextBox ID="txtFinalDisp" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFinalDisp','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img55" runat="server" tabindex="31" /></td>
                                    </tr>
                            </table>
                            </td>
                            </tr>
                                <tr>
                                    <td style="width: 105px">
                                        &nbsp;</td>
                                    <td style="width: 314px">
                                        &nbsp;</td>
                            
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        &nbsp;</td>
                                    <td style="width: 314px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Comments:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapComments" runat="server" CssClass="TxtBox" Height="60px"
                                            TextMode="MultiLine" Width="300px" TabIndex="22"></asp:TextBox></td>
                                </tr>
                                <tr><td colspan="2">
                                 <div id="divPopIsCOnHold" class="ModalPopup">
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                            width: 163px;">
                                                            &nbsp;Chapter On Hold
                                                        </td>
                                                        <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                            <a href="#" title="Close" onclick="javascript:closeModalArtCHold();" style="color: White;">
                                                                [x]</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;OnHold Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:DropDownList ID="drpChapterOnHoldType" runat="server">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtChapterOnHoldReason" runat="server" CssClass="TxtBox" Width="180px"
                                                                MaxLength="300"></asp:TextBox></td>
                                                    </tr>
                                                    <tr bgcolor="Honeydew">
                                                        <td colspan="2" align="center">
                                                            <a class="link1" href="#" onclick="javascript:validSaveItem_Chold();"><strong>Submit</strong></a>
                                                            &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtCHold();"><strong>Cancel</strong></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                </td></tr>
                            </table>
                        </div>
                        <div class="content" id="tabContents" runat="server">
                            <table id="Table2" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img1" align="absmiddle" src="images/tools/new.png" runat="server" />
                                        <asp:Label ID="lblContentsHeader" runat="server" Text="Contents"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        &nbsp;</td>
                                </tr>
                               <tr>
                               <td colspan="8">
                               <asp:GridView ID="gvContents" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="80%" >
                                                <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvCID" runat="server" Value='<%# Eval("Cno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chapter Name">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCname" runat="server" Text='<%# Eval("cname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chapter No.">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCno" runat="server" Text='<%# Eval("no_chapters") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page Proof">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCpage" runat="server" Text='<%# Eval("no_pages") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Figures">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCFigure" runat="server" Text='<%# Eval("no_figures") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Received Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCReceDate" runat="server" Text='<%# Eval("INDIA_RECD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="First Desp Date">
                                                        <ItemTemplate>
                                                         <asp:Label ID="lblgvCFirstDespDate" runat="server" Text='<%# Eval("First_Desp_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCRemarks" runat="server" Text='<%# Eval("BCOMMENTS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revised Recd. Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCRevRecdDate" runat="server" Text='<%# Eval("Revised_Recd_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revised Desp Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCRevisedDespDate" runat="server" Text='<%# Eval("Revised_Desp_Date") %>'></asp:Label>
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
                                
                            </table>
                        </div>
                        <div class="content" id="tabChapContent" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td class="dpJobGreenHeader" style="height: 32px">
                                        <img id="Img6" align="absmiddle" src="images/tools/comment.png" runat="server" />
                                        <asp:Label ID="lblChapContentHeader" runat="server" Text="Chapter Contents"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Book: <a id="alinkcomments" href="#" onclick="javascript:showhideDiv(this,'divCommHistory');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                
                                
                            </table>
                        </div>
                        <div class="content" id="tabBookEvents" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img7" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblEventsHeader" runat="server" Text="Book Logged Events"></asp:Label>
                                        </td>
                                    <td class="dpJobGreenHeader">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <strong>Book: <a id="aEvents" href="#" onclick="javascript:showhideDiv(this,'divEvents');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr style="font-size: 8pt; font-family: Verdana">
                                    <td colspan="6">
                                        <div id="divEvents" style="display: block;">
                                            <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Stage">
                                                        <ItemTemplate>
                                                            <%# Eval("STYPENAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <%# Eval("LEDATE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <%# Eval("EMP_FNAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Events">
                                                        <ItemTemplate>
                                                            <%# Eval("EVDESCRIPTION")%>
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
                        <div class="content" id="tabChapterEvents" runat="server">
                        <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img56" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblChapEventHeader" runat="server" Text="Chapter Logged Events"></asp:Label>
                                        </td>
                                    <td class="dpJobGreenHeader">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                <td colspan="8">
                                <asp:GridView ID="gvChapEvent" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="80%" >
                                                <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvCID" runat="server" Value='<%# Eval("Cno") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chapter Name">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCname" runat="server" Text='<%# Eval("cname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chapter No.">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCno" runat="server" Text='<%# Eval("no_chapters") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stage">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCStage" runat="server" Text='<%# Eval("STYPENAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rec. Date">
                                                        <ItemTemplate>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Due Date">
                                                        <ItemTemplate>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of MS Pages">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCmspage" runat="server" Text='<%# Eval("Ms_pages") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page Proof">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCpage" runat="server" Text='<%# Eval("no_pages") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Figures">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCFigure" runat="server" Text='<%# Eval("no_figures") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="No. of Tables">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCTablee" runat="server" Text='<%# Eval("no_Tables") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="No. of Equations">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCEqu" runat="server" Text='<%# Eval("no_equations") %>'></asp:Label>
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
                                </table>
                        </div>
                        <div class="content" id="tabjobtrack" runat="server">
                            <table id="Table5" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <asp:Label ID="lblJobTrackHeader" runat="server" Text="Job Track"></asp:Label></td>
                                </tr>
                                <tr><td>
                                
                                </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabBookBarcode" runat="server">
                            <table id="Table1" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <asp:Label ID="Label2" runat="server" Text="Barcode Scanner"></asp:Label></td>
                                </tr>
                                </table>
                                <div id="Div1" runat="server" align="center">
                                 <table>
                                 <tr>
                                 <td colspan="4" align="center">Login:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropBarcodeLogin" runat="server">
                                     <asp:ListItem Value="10061">BEGIN Stage for BOOK</asp:ListItem>
                                     <asp:ListItem Value="10062">END Stage for BOOK</asp:ListItem>
                                     <asp:ListItem Value="10057">Book Dispatched</asp:ListItem>
                                     <asp:ListItem Value="10069">Book Final Dispatched </asp:ListItem>
                                     </asp:DropDownList></td>
                                 </tr>
                                     
                                <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Barcode No.:"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtProBarcode" runat="server"></asp:TextBox>
                                </td>
                                <td><asp:Label ID="Label4" runat="server" Text="CAT #:"></asp:Label>
                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProCat" runat="server"></asp:TextBox>
                                </td>
                                </tr>
                                <tr><td><asp:Label ID="Label5" runat="server" Text="Department:"></asp:Label>
                                
                                </td>
                                <td>
                                    <asp:DropDownList ID="dropBarcodeDepart" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td><asp:Label ID="Label6" runat="server" Text="Stage:"></asp:Label>
                                
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropBarcodeStage" runat="server">
                                    </asp:DropDownList>
                                </td>
                                </tr>
                                <tr>
                                <td colspan="4" align="center">
                                    <asp:Button CssClass="dpbutton" ID="btnProBarUpdate" runat="server" 
                                        Text="Update" onclick="btnProBarUpdate_Click"  />
                                </td>
                                </tr>
                                </table>
                               </div>
                       </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkBookdetails','');">Show Details</div>
        <div id="divfooter1" class="footer" onclick="javascript:__doPostBack('lnkChapterdetails','');">Show Details</div>
    </form>
</body>
</html>

