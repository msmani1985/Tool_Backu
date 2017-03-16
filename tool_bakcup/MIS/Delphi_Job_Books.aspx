<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Delphi_Job_Books.aspx.cs" Inherits="Delphi_Job_Books" 
EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Book</title>
   
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
    <link href="jquery/jquery-ui.css" rel="stylesheet" type="text/css" />  
    <script src="jquery/jquery.min.js" type="text/javascript"></script>  
    <script src="jquery/jquery-ui.min.js" type="text/javascript"></script>  
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
   
    <style type="text/css">
        .Grid{
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
            font-family: Calibri;
            color: #474747;
        }
        .Grid td{
            padding: 2px;
            border: solid 1px #c1c1c1;
        }
        .Grid th{
            padding: 4px 2px;
            color: #fff;
            background: #363670;
            border-left: solid 1px #525252;
            font-size: 0.9em;
        }
    </style>
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
	    font:normal 15px Verdana;
	    line-height:18px;
	    z-index:10000;
	    background-color:#ededed;
	    
	    top:-1000px;
	    left:-1000px;
	    
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
        
        
        }
        div.ModalPopup {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 15px;
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
         .Initial
        {
            display: block;
            
            float: left;
            background: url("images/InitialImage.png") no-repeat right top;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: White;
            background: url("Images/SelectedButton.png") no-repeat right top;
        }
        .Clicked
        {
            float: left;
            display: block;
            background: url("Images/SelectedButton.png") no-repeat right top;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
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
        .style1
        {
            width: 169px;
        }
        .auto-style6 {
            width: 119px;
        }
        .auto-style7 {
            height: 36px;
        }
        .auto-style8 {
            height: 208px;
        }
        .auto-style9 {
            width: 101px;
        }
        .auto-style11 {
            width: 99px;
        }
        .auto-style12 {
            width: 133px;
        }
        </style>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
  

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                        <asp:HiddenField ID="BNO_ID" runat="server" />
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
                                                        <asp:ListBox ID="lstAdvancedFormat" runat="server" Width="377px" 
                                                            SelectionMode="Multiple" 
                                                            onselectedindexchanged="lstAdvancedFormat_SelectedIndexChanged"></asp:ListBox>
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
                                <asp:LinkButton ID="lnkGeneral" runat="server" OnClick="lnkGeneral_Click" TabIndex="1">General</asp:LinkButton></li>
                            <li id="miBookDetails" runat="server">
                                <asp:LinkButton ID="lnkBookdetails" runat="server" OnClick="lnkBookdetails_Click"
                                    TabIndex="2">Book Details</asp:LinkButton></li>
                            <li id="miChapDetails" runat="server">
                                <asp:LinkButton ID="lnkChapterdetails" runat="server" OnClick="lnkChapterdetails_Click"
                                    TabIndex="3">Chapter Details</asp:LinkButton></li>
                            <li id="miBookBarcode" runat="server">
                                <asp:LinkButton ID="lnkBookBarcode" runat="server" TabIndex="4" onclick="lnkBookBarcode_Click">
                                    Barcode Scanner</asp:LinkButton></li>
                            <li id="miBooklogEvents" runat="server">
                                <asp:LinkButton ID="lnkBooklogEvents" runat="server" TabIndex="5" OnClick="lnkBooklogEvents_Click">
                                    Logged Events</asp:LinkButton></li>
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
                                                <asp:TemplateField HeaderText="Job No." SortExpression="parent_job_id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobno1" runat="server" Text='<%# Eval("bno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
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
                                                        <asp:Label ID="lblgvBDueDate" runat="server" Text='<%# Eval("BFINALDISPATCH","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disp. Date" SortExpression="BFOURTHDISPATCH">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBDispDate" runat="server" Text='<%# Eval("BFINALDISPATCH","{0:MM/dd/yyyy}") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="ISBN" SortExpression="BISBN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvISBN" runat="server" Text='<%# Eval("BISBN") %>'></asp:Label>
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
                                <table  id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                    <tr bgcolor="#f0fff0">
                                        <td colspan="4" class="dpJobGreenHeader">
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
                                        <td class="auto-style11">
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span>
                                        </td>
                                        <td class="auto-style6">
                                            <asp:DropDownList ID="drpBookCustomer" runat="server" Width="300px" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpBookcustfinsite_SelectedIndexChanged" Height="16px">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="2">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                           <b>Books Stage</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            Cat ID #:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td class="auto-style6">
                                            <asp:TextBox ID="txtBookNo" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="200px" MaxLength="150" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td class="auto-style9">Stage:</td>
                                        <td class="auto-style12"><asp:DropDownList ID="DropCurStageHist" Width="100" runat="server">
                                        </asp:DropDownList>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            Book Title:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td class="auto-style6">
                                            <asp:TextBox ID="txtBookTitle" runat="server" CssClass="TxtBox" Width="300px" BackColor="#FFFFC0"
                                                MaxLength="300" TabIndex="14"></asp:TextBox></td>
                                        <td class="auto-style9">Start Date:</td>
                                        <td class="auto-style12"><asp:TextBox ID="txtBStartDt" runat="server" CssClass="TxtBox" Width="100px" 
                                                TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBStartDt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img59" 
                                                runat="server" tabindex="31" /></td>
                                    </tr>
                                     <tr>
                                        <td class="auto-style11">
                                            Financial Site:<span style="font-size: 9pt; color: #ff0000">*</span>
                                        </td>
                                        <td class="auto-style6">
                                            <asp:DropDownList ID="drpBookcustfinsite" runat="server" Width="300px" TabIndex="15">
                                            </asp:DropDownList></td>
                                         <td class="auto-style9">Due Date:</td>
                                         <td class="auto-style12"><asp:TextBox ID="txtBDueDt" runat="server" CssClass="TxtBox" Width="100px" 
                                                TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBDueDt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img61" 
                                                runat="server" tabindex="31" /></td>
                                     </tr>
                                    <tr>
                                        <td class="auto-style11">Project Editor:</td>
                                        <td class="auto-style6">
                                            <asp:DropDownList ID="txtBookEditor" runat="server" CssClass="TxtBox" Width="280px" TabIndex="22" BackColor="#F1F1F1"></asp:DropDownList>
                                            <img id="img58" language="javascript" onclick="return imgReason_editor_onclick()" 
                                                src="images/tools/new.png" style="cursor: pointer; height: 16px;" 
                                                title="The Language Work Inc" />
                                            <asp:ImageButton src="images/tools/Refresh.png" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"  />
                                         </td>
                                        <td class="auto-style9">Pages:</td>
                                         <td class="auto-style12"><asp:TextBox ID="txtPagesH" runat="server" CssClass="TxtBox" Width="100px" 
                                                TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            </td>
                                     </tr>
                                    <tr>
                                        <td colspan="4">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">Size:</td>
                                        <td class="auto-style6"><asp:TextBox ID="txtBookSize" runat="server" CssClass="TxtBox" Width="200px" MaxLength="50"
                                                TabIndex="16"></asp:TextBox></td>
                                        
                                        <td class="auto-style9">Format/Style:</td>
                                        <td class="auto-style12"><asp:DropDownList ID="drpBookTypeset" runat="server" TabIndex="19">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">Print ISBN:</td>
                                        <td class="auto-style6"><asp:TextBox ID="txtBookPISBN" runat="server" CssClass="TxtBox" Width="200px" MaxLength="16"
                                                TabIndex="17"></asp:TextBox></td>
                                        <td class="auto-style9">Purchase Order:</td>
                                        <td class="auto-style12"><asp:TextBox ID="txtBookPONumber" runat="server" CssClass="TxtBox" MaxLength="100"
                                                Width="200px" TabIndex="21"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">Pages:</td>
                                        <td class="auto-style6"><asp:TextBox ID="txtPages" Width="70" runat="server"></asp:TextBox></td>
                                        <td class="auto-style9">Price No:</td>
                                        <td class="auto-style12"><asp:TextBox ID="txtPriceNo" Width="70" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Book Rec.Date:</td>
                                        <td><asp:TextBox ID="txtBookRecDate" runat="server" CssClass="TxtBox" Width="100px" 
                                                TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBookRecDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img1" 
                                                runat="server" tabindex="31" /></td>
                                        <td>Template:</td>
                                        <td><asp:TextBox ID="txtTemplate" Width="70" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td colspan="3" class="auto-style8">
                                        <table>
                                        <tr><td class="auto-style7"></td>
                                        <td class="auto-style7">Addl. Cost1:</td><td class="auto-style7"><asp:TextBox ID="txtcost1" Width="70" runat="server"></asp:TextBox></td><td class="auto-style7"></td>
                                        <td class="auto-style7">Quantity1:</td><td class="auto-style7"><asp:TextBox ID="txtQty1" Width="70" runat="server"></asp:TextBox></td><td class="auto-style7"></td>
                                        <td class="auto-style7">Price No 1:</td><td class="auto-style7"><asp:TextBox ID="txtPrice1" Width="70" runat="server"></asp:TextBox></td><td class="auto-style7"></td>
                                        <td class="auto-style7">&nbsp;</td><td class="auto-style7">&nbsp;</td><td class="auto-style7"></td>
                                        <td class="auto-style7">Type of Cost:</td><td class="auto-style7"></td><td class="auto-style7">Type:</td><td class="auto-style7"></td>
                                        <td colspan="2" class="auto-style7"></td><td class="auto-style7"></td>
                                        </tr>
                                          <tr><td></td>
                                        <td>Addl. Cost2:</td><td><asp:TextBox ID="txtcost2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Quantity2:</td><td><asp:TextBox ID="txtQty2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Price No 2:</td><td><asp:TextBox ID="txtPrice2" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                        <td rowspan="4" align="justify" >
                                            <asp:RadioButtonList ID="rbTypeCost" runat="server"  
                                                Width="125px">
                                                <asp:ListItem Selected="True" Value="0">Page Cost</asp:ListItem>
                                                <asp:ListItem Value="1">Book Cost</asp:ListItem>
                                                <asp:ListItem Value="2">Additional Cost</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </td>
                                        <td rowspan="4"></td>
                                        <td rowspan="4" colspan="2" >
                                            <asp:RadioButtonList ID="rbType" runat="server" Width="99px">
                                                <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                                <asp:ListItem Value="1">Positive</asp:ListItem>
                                                <asp:ListItem Value="2">Negative</asp:ListItem>
                                                <asp:ListItem Value="3">FullService</asp:ListItem>
                                                <asp:ListItem Value="4">Editorial QC</asp:ListItem>
                                                <asp:ListItem Value="5">KP</asp:ListItem>
                                            </asp:RadioButtonList>

                                        </td>
                                        <td rowspan="4" colspan="2">
                                            <asp:RadioButtonList ID="rbWorkFrom" runat="server" Visible="false">
                                                <asp:ListItem Value="2">Chennai</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="3">Coimbatore</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        </tr>
                                          <tr><td></td>
                                        <td>Addl. Cost3:</td><td><asp:TextBox ID="txtcost3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Quantity3:</td><td><asp:TextBox ID="txtQty3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Price No 3:</td><td><asp:TextBox ID="txtPrice3" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                        </tr>
                                          <tr><td></td>
                                        <td>Addl. Cost4:</td><td><asp:TextBox ID="txtCost4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Quantity4:</td><td><asp:TextBox ID="txtQty4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Price No 4:</td><td><asp:TextBox ID="txtPrice4" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                        </tr>
                                          <tr><td></td>
                                        <td>Addl. Cost5:</td><td><asp:TextBox ID="txtCost5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Quantity5:</td><td><asp:TextBox ID="txtQty5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>Price No 5:</td><td><asp:TextBox ID="txtPrice5" Width="70" runat="server"></asp:TextBox></td><td></td>
                                        <td>&nbsp;</td><td>&nbsp;</td><td></td>
                                        </tr>
                                        </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Comments:</td>
                                        <td><asp:TextBox ID="txtBookComments" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex="24"></asp:TextBox></td>
                                        <td colspan="2">
                                            <asp:GridView ID="gvBookHist" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                            Width="80%" OnRowDataBound="gvHistory_RowDataBound" 
                                                    onrowcommand="gvHistory_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Stage">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblgvCname0" runat="server" Text='<%# Eval("STypeNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Received Date">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblgvCno0" runat="server" Text='<%# Eval("Received_Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Half Due Date">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblgvCpage0" runat="server" Text='<%# Eval("HalfDue_Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Due Date">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblgvCFigure0" runat="server" Text='<%# Eval("Due_Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pages">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblgvCPages" runat="server" Text='<%# Eval("Pages") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Dispatch Date">
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="lblgvCFigure0" runat="server" Text='<%# Eval("Dispatch_Date") %>'></asp:Label>
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
                        </div>
                        <div class="content" id="tabChapterdetails" runat="server">
                            <table id="Table3" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr bgcolor="#f0fff0">
                                    <td colspan="4" class="dpJobGreenHeader">
                                        <img id="img5" align="absmiddle" src="images/tools/new.png" runat="server" />
                                        <asp:Label ID="lblChapHeader" runat="server" Text="Chapter Details"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <span style="font-size: 9pt; color: #ff0000"></span>
                                        <asp:ImageButton ID="imgSave_Chap" ImageUrl="~/images/tools/j_save.png" runat="server"
                                            ToolTip="Save Chapter" OnClick="cmd_Save_ChapterList_Click" TabIndex="40" />
                                        <asp:ImageButton ID="imgPrintChap" ImageUrl="~/images/tools/j_print.png" runat="server"
                                            ToolTip="Print" OnClientClick="javascript:printChapter();" TabIndex="41" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                               <tr>
                                   <td align="center">
                                    <asp:GridView ID="gvChapMatter" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                            Width="80%">
                                            <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="<%# id1++ %>"></asp:Label>
                                                    <br />
                                                    <asp:HiddenField ID="hfgvCID" runat="server" Value='<%# Eval("Cno") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chapter Name">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblgvCname" runat="server" Text='<%# Eval("cname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Chapters">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblgvCno" runat="server" Text='<%# Eval("no_chapters") %>'></asp:Label>
                                                    </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Pages">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblgvCmspage" runat="server" Text='<%# Eval("NO_PAGES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. of Figures">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblgvCFigure" runat="server" Text='<%# Eval("no_figures") %>'></asp:Label>
                                                    </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of Tables">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblgvCTablee" runat="server" Text='<%# Eval("no_Tables") %>'></asp:Label>
                                                    </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. of Equations">
                                                    <ItemTemplate>
                                                    <asp:Label ID="lblgvCEqu" runat="server" Text='<%# Eval("no_equations") %>'></asp:Label>
                                                    </ItemTemplate>
                                                <ControlStyle Width="100px" />
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
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        No.of Chapters :
                                        <asp:TextBox ID="txtNoofChap" runat="server" TabIndex="13"></asp:TextBox>
                                        <asp:Button ID="btnCreate_chaplist" runat="server" Text="Create" CssClass="dpbutton" OnClick="btnCreate_chaplist_Click" />
                                    </td>
                                </tr>
                            </table>
                           <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                               <tr>
                                   <td >
                                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvChapList" runat="server" AutoGenerateColumns="False" 
                                                    CssClass="lightbackground" OnRowDataBound="gvChapList_RowDataBound" OnRowCommand="gvChapList_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                                <br />
                                                                <asp:HiddenField ID="hfgvCpno" runat="server" Value='<%# Eval("Cpno") %>' />
                                                                <asp:HiddenField ID="hfgvbno" runat="server" Value='<%# Eval("bno") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chapter Title">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvChpTitle" runat="server" Text='<%# Eval("ChpTitle") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Chapter Name">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpChpName" runat="server">
                                                                    <asp:ListItem Value="1">Front Matter</asp:ListItem>
                                                                    <asp:ListItem Value="2">Body Matter</asp:ListItem>
                                                                    <asp:ListItem Value="3">Back Matter</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rec Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvRecDate" runat="server" Text='<%# Eval("RecDate") %>'></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="lblgvRecDate" 
                                                                    PopupButtonID="lblgvRecDate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Due Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvDueDate" runat="server" Text='<%# Eval("DueDate") %>'></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="lblgvDueDate" 
                                                                    PopupButtonID="lblgvDueDate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Des Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvDesDate" runat="server" Text='<%# Eval("desdate") %>'></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="lblgvDesDate" 
                                                                        PopupButtonID="lblgvDesDate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="100px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pages">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvPages" runat="server" Text='<%# Eval("Pages") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Figures">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvFigures" runat="server" Text='<%# Eval("Figures") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Equations">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvEquations" runat="server" Text='<%# Eval("Equations") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tables">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvTables" runat="server" Text='<%# Eval("Tables") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stage">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpStypeno" runat="server" Enabled="false"></asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="120px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" Text = "View" OnClick = "Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton  AlternateText="Remove" ImageUrl="~/images/tools/no.png" id="btnDelete"  AccessKey="D" style="cursor :pointer"
                                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Cpno")%>' CommandName="Delete"  runat="server" ToolTip="Remove" />
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
                                       
                                               <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick = "Add" Visible="false" />
 
                                                <asp:Panel ID="pnlAddEdit" runat="server" Width="550" Height="300" CssClass="modalPopup" style = "display:none">
                                                <asp:Label ID="Label9" runat="server" Text="Stage Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <br />
                                                <table align = "center">
                                                    <tr>
                                                        <td align="right">
                                                            Stage :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drpNextStypeno" runat="server"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Rec. Date :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNRecDate" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtNRecDate" 
                                                                        PopupButtonID="txtNRecDate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Due Date :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNDueDate" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtNDueDate" 
                                                                        PopupButtonID="txtNDueDate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Des. Date :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNDesDate" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtNDesDate" 
                                                                        PopupButtonID="txtNDesDate" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Pages :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNPages" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Label ID="lblNCpno" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblNBno" runat="server"  Visible="false"></asp:Label>
                                                            <asp:Button ID="btnSave" CssClass="dpbutton" runat="server" Text="Save" OnClick="btnSave_Click"/>
                                                            <asp:Button ID="btnCancel" CssClass="dpbutton" runat="server" Text="Close" OnClientClick = "return Hidepopup()"/>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gvChapStageDetails" runat="server" AlternatingRowStyle-CssClass="dullbackground" 
                                                                      CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  Width="250px" GridLines="Vertical"
                                                                      AllowSorting="True" AllowPaging="true" PageSize="9" CellPadding="4">

                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </asp:Panel>
                                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                                <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                                                PopupControlID="pnlAddEdit" TargetControlID = "lnkFake"
                                                BackgroundCssClass="modalBackground">
                                                </asp:ModalPopupExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID = "gvChapList" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                   </td>
                               </tr>
                           </table>
                        </div>
                        <div class="content" id="tabBookBarcode" runat="server">
                            <table id="Table1" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <asp:Label ID="lblBookBarcode" runat="server" Text="Barcode Scanner"></asp:Label></td>
                                </tr>
                                </table>
                                <div id="Div1" runat="server" align="center">
                                 <table>
                                 <tr>
                                 <td colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
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
                                
                                <tr>
                                <td colspan="4" align="center">
                                    <asp:Button CssClass="dpbutton" ID="btnProBarUpdate" runat="server" 
                                        Text="Update" onclick="btnProBarUpdate_Click"  />
                                </td>
                                </tr>
                                </table>
                               </div>
                                 <div id="Div2" runat="server">
                            <table id="Table6" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px;border-top: 1px solid Green;" >
                                        <asp:Label ID="Label7" runat="server" Text="Stage Dispatch"></asp:Label></td>
                                </tr>
                                </table>
                                <div id="Div3" runat="server" align="center">
                                <div style="width:50%;padding-top:2px;">
                                    
                                <asp:GridView ID="gvBookHistUpdate" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="90%" OnRowDataBound="gvHistory_RowDataBound" 
                                        onrowcommand="gvHistory_RowCommandUpdate">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Stage">
                                                        <ItemTemplate>
                                                        <asp:HiddenField ID="hfgvHistID" runat="server" Value='<%# Eval("Job_History_ID") %>' />
                                                        <asp:Label ID="lblgvCname1" runat="server" Text='<%# Eval("STypeNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCno1" runat="server" Text='<%# Eval("Received_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Half Due Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCpage1" runat="server" Text='<%# Eval("HalfDue_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Due Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCFigure1" runat="server" Text='<%# Eval("Due_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Pages">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCPages" runat="server" Text='<%# Eval("Pages") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Dispatch Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCFigure2" runat="server" Text='<%# Eval("Dispatch_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dispatch" >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDispatch" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" Wrap="False" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                 <table>
                                <tr>
                                <td>Despatch Date:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td><asp:TextBox ID="txtBDisDt" runat="server" CssClass="TxtBox" Width="100px" 
                                                TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBDisDt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img62" 
                                                runat="server" tabindex="31" /></td>
                                <td colspan="4" align="center">
                                    <asp:Button CssClass="dpbutton" ID="btnUpdtStg" runat="server" 
                                        Text="Update" onclick="btnUpdtStg_Click"   />
                                </td>
                                </tr>
                                </table>
                               </div>
                       </div>
                       </div>
                        <div class="content" id="tabLogEvents" runat="server">
                            <table id="Table6" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <asp:Label ID="Label2" runat="server" Text="Logged Events"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvLogEvents" runat="server" AutoGenerateColumns="False" 
                                            CssClass="lightbackground" Width="90%" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# l++ %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Name">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvname" runat="server" Text='<%# Eval("Acode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvTask" runat="server" Text='<%# Eval("Task_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvSDate" runat="server" Text='<%# Eval("LStartDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Date">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvEDate" runat="server" Text='<%# Eval("LEndDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Breaks">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvBreak" runat="server" Text='<%# Eval("Breaks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="ActWrkHrs">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvWrkhrs" runat="server" Text='<%# Eval("ActWrkHrs") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvEmp" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pages">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvPages" runat="server" Text='<%# Eval("PAGESCOMPLETED") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="comments">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblgvCmt" runat="server" Text='<%# Eval("comments") %>'></asp:Label>
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
                    </td>
                </tr>
            </table>
        </div>
        <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkBookdetails','');">Show Details</div>
        <div id="divfooter1" class="footer" onclick="javascript:__doPostBack('lnkChapterdetails','');">Show Details</div>
    </form>
</body>
</html>

