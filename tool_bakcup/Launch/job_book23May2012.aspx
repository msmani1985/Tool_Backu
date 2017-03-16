<%@ page language="C#" autoeventwireup="true" inherits="job_book, App_Web_opij0lkt" enableeventvalidation="false" maintainscrollpositiononpostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="scripts/tabs1.js"></script>--%>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>

    <script type="text/javascript">
    var gvelem;
    var gvcolor;
    function setColor(element,val,val1){      
      //alert(gvelem);
      if(gvelem!=null){//alert(gvelem.style.backgroundColor);
        gvelem.style.backgroundColor = gvcolor;
      }
      gvelem = element;
      gvcolor = element.style.backgroundColor;
      element.style.backgroundColor = '#C2C2C2';
      document.form1.hfB_ID.value=val;
      document.form1.hfB_Name.value=val1
      if(document.getElementById("lblBookSummary"))
        document.getElementById("lblBookSummary").innerText="Book : "+val1;
      else if(document.all.lblBookSummary)
        document.all.lblBookSummary.innerText="Book : "+val1;
      else if(document.form1.lblBookSummary)
        document.form1.lblBookSummary.innerText="Book : "+val1;    
        doTimer();    
    }
    function setMouseOverColor(element)
    {
        gvcolor = element.style.backgroundColor;
        element.style.backgroundColor='#C2C2C2';
        element.style.cursor='pointer';
        element.style.textDecoration='underline';
    }
    function setMouseOutColor(element)
    {
        element.style.backgroundColor=gvcolor;
        element.style.textDecoration='none';
    }

    function imgBD_editor_onclick() {
        if(document.form1.drpBookCustomer!=null && document.form1.drpBookCustomer.value !="0")
            window.open("contacts.aspx?form=book&type=0&trgname=txtBookEditor&trgid=hfBookEditorId&cid="+document.form1.drpBookCustomer.value,"Contacts","width=800,height=600,status=yes, scrollbars=yes");
        else alert("Select a customer"); 
    }
    function vallidGraphic(){
        var msg = "";
        if(document.form1.drpGraphicType!=null && document.form1.drpGraphicType.value =="0")
            msg+="* Select a Graphic Type.\r\n";
        if(document.form1.txtGraphicCount.value=="" || document.form1.txtGraphicCount.value=="0")
            msg+="* No. of Items should be greater than zero.\r\n";
        if(msg!=""){
            alert(msg);
        return false;
        }
        return true;
    }
//    function vallidChapter(){
//        var msg = "";
//        if(document.form1.drpChapterName!=null && document.form1.drpChapterName.value =="0")
//            msg+="* Select a Chapter Name.\r\n";
//        if(document.form1.txtChapCount.value=="" || document.form1.txtChapCount.value=="0")
//            msg+="* No. of Items should be greater than zero.\r\n";
//        if(msg!=""){
//            alert(msg);
//        return false;
//        }
//        return true;
//    }
    function showhideDiv(me,ctrl){//alert(document.getElementById("divCommHistory"));
    var o;
        if(document.getElementById(ctrl))o=document.getElementById(ctrl);
        else if(document.all.ctrl)o=document.all.ctrl;
        else if(document.form1.ctrl)o=document.form1.ctrl;
         //alert(me.innerText);   
        if(o.style.display=='' || o.style.display=='block'){
            o.style.display = 'none';
            if(document.all)me.innerText='(Show)'; else me.textContent='(Show)';
        }
        else {
            o.style.display = 'block'
            if(document.all)me.innerText='(Hide)'; else me.textContent='(Hide)';
        }
    }
    function showhideAdvancedEndDate(me, ctrl)
    {
        var o;
        if(document.getElementById(ctrl))o=document.getElementById(ctrl);
        else if(document.all.ctrl)o=document.all.ctrl;
        else if(document.form1.ctrl)o=document.form1.ctrl;
         //alert(me.innerText);   
         if(me.value=='between')
         {o.style.display = 'block'}
         else{o.style.display = 'none';}
    }
     function openModal(){ 
        if(document.getElementById ('drpChapterName').value=='' || document.getElementById ('drpChapterName').value=='0'){
        alert('* Select a Chapter Name');
        return;
        }
        if(document.getElementById ('drpChapterName').value=='1')document.getElementById ('txtChapPrefix').value='Toc';
        else if(document.getElementById ('drpChapterName').value=='2')document.getElementById ('txtChapPrefix').value='Chapter';
        else document.getElementById ('txtChapPrefix').value='Appendix';        
        document.getElementById ('txtChapCount').value='';
        document.getElementById ('txtChapSdateNew').value='';
        document.getElementById ('txtChapDdateNew').value='';
        document.getElementById ('txtChapHDdateNew').value='';
        document.getElementById ('divPopChapter').style.visibility='visible';
        document.getElementById ('divPopChapter').style.display='';       
        document.getElementById ('divPopChapter').style.top= '150px';
        document.getElementById ('divPopChapter').style.left='248px'; 
        if (typeof document.body.style.maxHeight == "undefined")
        {  
            var layer = document.getElementById ('divPopChapter');
            layer.style.display = 'block';
            var iframe = document.getElementById('iframetop');
            iframe.style.display = 'block';
            iframe.style.visibility = 'visible';
            iframe.style.top= layer.offsetTop-10;
            iframe.style.left= layer.offsetLeft-10;
            iframe.style.width=  layer.offsetWidth+10;
            iframe.style.height= layer.offsetHeight+10; 
        }else
        {     
        document.getElementById ('divMasked').style.display='';
        document.getElementById ('divMasked').style.visibility='visible';
        document.getElementById ('divMasked').style.top='0px';
        document.getElementById ('divMasked').style.left='0px';
        document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
        document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px';        
        }
        document.getElementById ('txtChapPrefix').select();
    }
    function closeModal(){
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopChapter').style.display='none';
        document.getElementById ('iframetop').style.display='none';
    }
    function openAdvancedModal(){        
        document.getElementById ('divPopAdvancedSearch').style.visibility='visible';
        document.getElementById ('divPopAdvancedSearch').style.display='';       
        document.getElementById ('divPopAdvancedSearch').style.top= '85px';
        document.getElementById ('divPopAdvancedSearch').style.left='128px'; 
        if (typeof document.body.style.maxHeight == "undefined")
        {  
            var layer = document.getElementById ('divPopAdvancedSearch');
            layer.style.display = 'block';
            var iframe = document.getElementById('iframetop');
            iframe.style.display = 'block';
            iframe.style.visibility = 'visible';
            iframe.style.top= layer.offsetTop-10;
            iframe.style.left= layer.offsetLeft-10;
            iframe.style.width=  layer.offsetWidth+10;
            iframe.style.height= layer.offsetHeight+10; 
        }else
        {     
        document.getElementById ('divMasked').style.display='';
        document.getElementById ('divMasked').style.visibility='visible';
        document.getElementById ('divMasked').style.top='0px';
        document.getElementById ('divMasked').style.left='0px';
        document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
        document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px';        
        }
        showhideAdvancedEndDate(document.form1.drpAdvancedExpression,'divAdvencedEndDate');
    }
    function closeAdvancedModal(){
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopAdvancedSearch').style.display='none';
        document.getElementById ('iframetop').style.display='none';
    }    
     function validChapter(){
     var msg = "";
        if(document.getElementById('txtChapCount').value=='' || document.getElementById('txtChapCount').value =="0")
            msg+="* No. of Chapters should be greater than zero.\r\n";
        if(document.getElementById('txtChapSdateNew').value=='')
            msg+="* Select a Start Date.\r\n";
        if(document.getElementById('txtChapDdateNew').value=='')
            msg+="* Select a Due Date.\r\n";
        //if(document.getElementById('txtChapHDdateNew').value=='')
          //  msg+="* Select a HalfDue Date.\r\n";
        if(msg!="") alert(msg);
        else{
            document.getElementById ('divMasked').style.display='none';
            document.getElementById ('divPopChapter').style.display='none';
            document.getElementById ('lnkChapAdd').click();
        }
    }
    function validInvoiceTypeItem(){
        if(document.form1.drpCostInvoiceType!=null && document.form1.drpCostInvoiceType.value !="0" && document.form1.drpCostInvoiceType.value =="4"){
//            alert(document.getElementById ('divPopBCostInvTypeItem'));
//            alert(document.getElementById ('divMasked'));
            document.getElementById ('divPopBCostInvTypeItem').style.visibility='visible';
            document.getElementById ('divPopBCostInvTypeItem').style.display='';       
            document.getElementById ('divPopBCostInvTypeItem').style.top= '150px';
            document.getElementById ('divPopBCostInvTypeItem').style.left='248px'; 
            if (typeof document.body.style.maxHeight == "undefined")
            {
                var layer = document.getElementById ('divPopBCostInvTypeItem');
                layer.style.display = 'block';
                var iframe = document.getElementById('iframetop');
                iframe.style.display = 'block';
                iframe.style.visibility = 'visible';
                iframe.style.top= layer.offsetTop-10;
                iframe.style.left= layer.offsetLeft-10;
                iframe.style.width=  layer.offsetWidth+10;
                iframe.style.height= layer.offsetHeight+10;                
            }else
            {
                document.getElementById ('divMasked').style.display='';
                document.getElementById ('divMasked').style.visibility='visible';
                document.getElementById ('divMasked').style.top='0px';
                document.getElementById ('divMasked').style.left='0px';
                document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
                document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px'; 
            }  
            document.getElementById ('txtBCpopInvTypeItem').select();         
        }
        else {alert("Select Invoice Type to Additional Cost");}
    }
    function validSaveItem(){
        if(document.form1.txtBCpopInvTypeItem.value=='')alert('Enter Invoice Type Item');
        else document.getElementById ('lnkCostAddInvTypeItem').click();
    }
    function closeModalBCost(){
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopBCostInvTypeItem').style.display='none';
        document.getElementById ('iframetop').style.display='none';
    }
    function printBook(){if(document.form1.hfB_ID.value==""){alert('Select a Book');return false;} var w=window.open('Print_jobbag.aspx?jobid=' + document.form1.hfB_ID.value + '&jobtypeid=2&print=1','Preview','width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;}
    function printChapter(){if(document.form1.drpChapterNo.value=="0"){alert('Select a Chapter');return false;} var w=window.open('Print_jobbag.aspx?jobid=' + document.form1.drpChapterNo.value + '&jobtypeid=7&print=1','Preview','width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;}

    function popBCostPreview(){
        if(document.form1.drpCostInvoiceTypeItem!=null && document.form1.drpCostInvoiceTypeItem.value!="0"){
            var id = document.form1.drpCostInvoiceTypeItem.value;
            var text = document.form1.drpCostInvoiceTypeItem[document.form1.drpCostInvoiceTypeItem.selectedIndex].text;
            text = text.replace('&','and');
            window.open('BookCostPreview.aspx?text='+text+'&id=iti_'+id,'Preview','width=640,height=480,left=200,top=50,toolbars=no,scrollbars=yes,status=yes,resizable=yes');
            }
        else alert('Select a Invoice Type Item');
    }
    function clearAdvancedCtrls(){
        document.form1.rblstAdvanced_0.checked=true;
        document.form1.drpAdvancedExpression.value="between";
        showhideAdvancedEndDate(document.form1.drpAdvancedExpression,'divAdvencedEndDate');
        document.form1.txtAdvancedDate1.value="";
        document.form1.txtAdvancedDate2.value="";
        if(navigator.userAgent.indexOf('Firefox')!=-1){        
        document.form1.lstAdvancedFormat.selectedIndex=-1;
        document.form1.lstAdvancedCustomer.selectedIndex=-1;
        document.form1.lstAdvancedStage.selectedIndex=-1;
        }
        else{        
        document.form1.lstAdvancedFormat.value=-1;
        document.form1.lstAdvancedCustomer.value=-1;
        document.form1.lstAdvancedStage.value=-1;
        }
    }
    function clearListCtrl(ctrl){
        if(navigator.userAgent.indexOf('Firefox')!=-1)
            ctrl.selectedIndex=-1;
        else ctrl.value=-1;
    }
    </script>
     <script language="javascript">    
    var cnt=0;
    var tt;
    var timer_is_on=0;
    var elemn;
    var h = 0;
    var max =20;
    function timedCount(){
        //alert(document.getElementById('divfooter'));
        elemn = document.getElementById('divfooter');
	    h = elemn.style.height.replace('px','');	
	    if(timer_is_on && cnt<=max && h<=max){
	    //ctrl.value=cnt;	
	    if(h=='')h=0;
	    //alert(h);
	    elemn.style.height = parseInt(h)+5+'px';	    
	    //alert(cnt);
	    cnt=cnt+5;
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
                                            OnClick="btnSearch_Click" TabIndex="4" />&nbsp;<input id="btnAdvanced" class="dpbutton"
                                                style="width: 80pt" type="button" value="Options" onclick="javascript:openAdvancedModal();" />
                                        <asp:HiddenField ID="hfB_ID" runat="server" />
                                        <asp:HiddenField ID="hfB_Name" runat="server" />
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
                                                        <asp:ListBox ID="lstAdvancedStage" runat="server" Width="377px" SelectionMode="Multiple"></asp:ListBox>
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
                            <li id="miBookAddCost" runat="server">
                                <asp:LinkButton ID="lnkBookAddCost" runat="server" OnClick="lnkBookAddCost_Click"
                                    TabIndex="7">Book Cost</asp:LinkButton></li>
                            <li id="miChapDetails" runat="server">
                                <asp:LinkButton ID="lnkChapterdetails" runat="server" OnClick="lnkChapterdetails_Click"
                                    TabIndex="8">Chapter Details</asp:LinkButton></li>
                            <li id="miBookEvents" runat="server">
                                <asp:LinkButton ID="lnkBookEvents" runat="server" OnClick="lnkBookEvents_Click" TabIndex="9">Events</asp:LinkButton></li>
                            <li id="miGraphics" runat="server">
                                <asp:LinkButton ID="lnkGraphics" runat="server" OnClick="lnkGraphics_Click" TabIndex="10">Graphic</asp:LinkButton></li>
                            <li id="miComments" runat="server">
                                <asp:LinkButton ID="lnkComments" runat="server" OnClick="lnkComments_Click" TabIndex="11">Comments</asp:LinkButton></li>
                        </ol>
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblBookSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                    <td align="right">
                                        <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                            ToolTip="Export Excel" OnClick="cmd_Excel_Export_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvBooks" runat="server" Width="100%" OnRowDataBound="gvBooks_RowDataBound"
                                            AutoGenerateColumns="False" Font-Size="8pt" CssClass="lightbackground" AllowSorting="True"
                                            OnSorting="gvBooks_Sorting" OnRowCreated="gvBooks_RowCreated">
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvBookID" runat="server" Value='<%# Eval("parent_job_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job No." SortExpression="parent_job_id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobno" runat="server" Text='<%# Eval("parent_job_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer" SortExpression="cust_name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Book No." SortExpression="name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBnumber" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Book Title" SortExpression="title">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBtitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editor" SortExpression="display_name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBEditor" runat="server" Text='<%# Eval("display_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage" SortExpression="job_stage_name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBStage" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rec. Date" SortExpression="received_date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBRecDate" runat="server" Text='<%# Eval("received_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Half Due Date" SortExpression="half_due_date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBHlfDueDate" runat="server" Text='<%# Eval("half_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Due Date" SortExpression="due_date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBDueDate" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disp. Date" SortExpression="despatch_date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBDispDate" runat="server" Text='<%# Eval("despatch_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle ForeColor="White" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice No." SortExpression="invoice_no">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBInvNo" runat="server" Text='<%# Eval("invoice_no") %>'></asp:Label>
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
                                        <td>
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpBookCustomer" runat="server" Width="306px" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpBookcustfinsite_SelectedIndexChanged" TabIndex="12">
                                            </asp:DropDownList></td>
                                        <td>
                                            Book&nbsp;Stage:</td>
                                        <td>
                                            <asp:DropDownList ID="drpBookStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpBookStage_SelectedIndexChanged"
                                                TabIndex="26">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cat ID #:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtBookName" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="200px" MaxLength="150" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td style="font-size: 8pt">
                                            Start Date:</td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtBookSdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="27" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBookSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_stdate" runat="server"
                                                tabindex="28" /></td>
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
                                            Half-Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookHDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="29" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBookHDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_hdudate" runat="server"
                                                tabindex="30" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Financial Site</td>
                                        <td>
                                            <asp:DropDownList ID="drpBookcustfinsite" runat="server" Width="306px" TabIndex="15">
                                            </asp:DropDownList></td>
                                        <td>
                                            Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="31" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtBookDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dudate" runat="server"
                                                tabindex="32" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Size:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookSize" runat="server" CssClass="TxtBox" Width="200px" MaxLength="50"
                                                TabIndex="16"></asp:TextBox></td>
                                        <td>
                                            Despatch:</td>
                                        <td>
                                            &nbsp;<asp:CheckBox ID="chkBookDespatch" runat="server" TabIndex="33" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Print ISBN:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookPISBN" runat="server" CssClass="TxtBox" Width="200px" MaxLength="16"
                                                TabIndex="17"></asp:TextBox></td>
                                        <td>
                                            Sales Group: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpBookSalesGroup" runat="server" TabIndex="33" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Online ISBN:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookOISBN" runat="server" CssClass="TxtBox" Width="200px" MaxLength="16"
                                                TabIndex="18"></asp:TextBox></td>
                                        <td colspan="3" rowspan="9">
                                            <div style="vertical-align: top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td colspan="3" class="subheading">
                                                            <strong>Completed Stage(s)</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="subheading" colspan="3" valign="top">
                                                            <asp:DataGrid ID="dgrdBookStages" runat="server" AutoGenerateColumns="False" CssClass="lightbackground">
                                                                <AlternatingItemStyle CssClass="dullbackground" />
                                                                <HeaderStyle CssClass="darkbackground" />
                                                                <Columns>
                                                                    <asp:TemplateColumn HeaderText="Stage">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_StageName" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
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
                                                                    <asp:TemplateColumn HeaderText="Half Due Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_HalfdueDate" runat="server" Text='<%# Eval("half_due_date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Desp. Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBD_DespDate" runat="server" Text='<%# Eval("despatch_date") %>'></asp:Label>
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
                                            Format/Style:</td>
                                        <td>
                                            <asp:DropDownList ID="drpBookTypeset" runat="server" TabIndex="19">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Service Type:</td>
                                        <td>
                                            <asp:DropDownList ID="drpBookServicetyp" runat="server" TabIndex="20">
                                            </asp:DropDownList>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Purchase Order:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookPONumber" runat="server" CssClass="TxtBox" MaxLength="100"
                                                Width="200px" TabIndex="21"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Author:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookAuthor" runat="server" CssClass="TxtBox" MaxLength="100"
                                                TabIndex="21" Width="200px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Project Editor:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookEditor" runat="server" CssClass="TxtBox" Width="280px" TabIndex="22" BackColor="#F1F1F1"></asp:TextBox>
                                            <img id="imgBD_editor" align="absMiddle" src="images/tools/user_go.png" language="javascript"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select Editor"
                                                tabindex="23" />
                                            <asp:HiddenField ID="hfBookEditorId" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comments:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookComments" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex="24"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Invoice Description:</td>
                                        <td>
                                            <asp:TextBox ID="txtBookInvoiceDesc" runat="server" Height="50px" TextMode="MultiLine"
                                                Width="300px" CssClass="TxtBox" TabIndex="25"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="content" id="tabBookAddCost" runat="server">
                            <table id="Table1" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 657px">
                                        <img id="ImgBookCost" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="lblCostHeader" runat="server" Text="Book Cost"></asp:Label></td>
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
                                                        title="New Invoice Type Item" onclick="javascript:return validInvoiceTypeItem();"
                                                        runat="server" />
                                                    <img id="imgBCPreview" title="Preview" align="absMiddle" style="cursor: pointer"
                                                        src="images/tools/perview.png" onclick="javascript:popBCostPreview();" /></td>
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
                                                    <asp:TextBox ID="txtCostItemdesc" runat="server" CssClass="TxtBox" MaxLength="100"
                                                        ToolTip="Eg: 40204.PY9781420073669.PBHRD.XXXX" Width="542px"></asp:TextBox></td>
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
                                        <asp:GridView ID="gvBookCost" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                            Width="100%" OnRowCommand="gvBookCost_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Invoice Type Item">
                                                    <ItemTemplate>
                                                        <%# Eval("InvoiceType_item_Name")%>
                                                        <asp:HiddenField ID="hfBC_invoicetypeitem" runat="server" Value='<%# Eval("job_invoice_type_item_id") %>' />
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
                                                        <asp:DropDownList ID="drpBC_orderindex" runat="server" SelectedValue='<%# Eval("order_index") %>'>
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
                                                        <asp:ImageButton ID="imgbtnBC_Edit" runat="server" ImageUrl="~/images/tools/edit.png"
                                                            ToolTip="Edit" CommandName="BCEdit" />
                                                        <asp:ImageButton ID="imgbtnBC_Delete" runat="server" ImageUrl="~/images/tools/delete.png"
                                                            ToolTip="Delete" CommandName="BCDelete" OnClientClick="javascript: return confirm('Confirm Delete?');" />
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
                                        <div id="divPopBCostInvTypeItem" class="ModalPopup">
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                        width: 163px;">
                                                        &nbsp;New Invoice Type Item
                                                    </td>
                                                    <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                        <a href="#" title="Close" onclick="javascript:closeModalBCost();" style="color: White;">
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
                                                        <a class="link1" href="#" onclick="javascript:validSaveItem();"><strong>Submit</strong></a>
                                                        &nbsp; <a class="link1" href="#" onclick="javascript:closeModalBCost();"><strong>Cancel</strong></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
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
                                    <td colspan="5">
                                        <div id="divPopChapter" class="ModalPopup">
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" style="width: 40%; background-color: green; color: White; font-weight: bold"
                                                        valign="top">
                                                        &nbsp;Add Chapters</td>
                                                    <td style="width: 60%; background-color: green; color: White; font-weight: bold;"
                                                        align="right" valign="top">
                                                        <a href="#" title="Close" onclick="javascript:closeModal();" style="color: White;">[x]</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;Chapter Prefix:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChapPrefix" runat="server" CssClass="TxtBox" MaxLength="20" Width="120px"
                                                            ToolTip="Use Chapter Prefix to create more than one Chapter, Toc, Appendix, Prelim, Reference, Notes, Annex, Overview etc.,"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;No. of Chapters:<span style="font-size: 9pt; color: #ff0000">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtChapCount" runat="server" CssClass="TxtBox" MaxLength="3" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                                            Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;Stage:</td>
                                                    <td>
                                                        <strong>Page Proof</strong></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;Start Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChapSdateNew" runat="server" CssClass="TxtBox" Width="80px" MaxLength="10" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtChapSdateNew','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img3" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;Half-Due Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChapHDdateNew" runat="server" CssClass="TxtBox" Width="80px"
                                                            MaxLength="10" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtChapHDdateNew','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img11" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;Due Date:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtChapDdateNew" runat="server" CssClass="TxtBox" Width="80px" MaxLength="10" BackColor="#F1F1F1"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtChapDdateNew','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img10" runat="server" /></td>
                                                </tr>
                                                <tr bgcolor="Honeydew">
                                                    <td colspan="2" align="center">
                                                        <a class="link1" href="#" onclick="javascript:validChapter();"><strong>Submit</strong></a>
                                                        &nbsp; <a class="link1" href="#" onclick="javascript:closeModal();"><strong>Cancel</strong></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Chapter Name:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                    <td style="width: 314px">
                                        <asp:DropDownList ID="drpChapterName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpChapterName_SelectedIndexChanged"
                                            Width="204px" TabIndex="12">
                                        </asp:DropDownList>&nbsp;
                                        <asp:LinkButton ID="lnkChapAdd" runat="server" OnClick="lnkChapAdd_Click"></asp:LinkButton></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Chapter No.:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                    <td style="width: 314px">
                                        <asp:DropDownList ID="drpChapterNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpChapterNo_SelectedIndexChanged"
                                            Width="204px" TabIndex="13">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Chapter Stage:</td>
                                    <td>
                                        <asp:DropDownList ID="drpChapterStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpChapterStage_SelectedIndexChanged"
                                            TabIndex="23">
                                        </asp:DropDownList></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                    </td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapterNo" runat="server" BackColor="#FFFFC0" CssClass="TxtBox"
                                            Width="200px" ToolTip="Enter Chapter No." MaxLength="30" TabIndex="14"></asp:TextBox>
                                        <img align="absMiddle" border="0" src="images/tools/add.png" style="cursor: pointer"
                                            onclick="javascript:openModal();" title="Add multiple Chapters" tabindex="15" id="btnaddMultipleChap" runat="server" /></td>
                                    <td>
                                        Start Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapSdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="24" BackColor="#F1F1F1"></asp:TextBox>
                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtChapSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgCD_stdate" runat="server"
                                            tabindex="25" /></td>
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
                                        Half-Due Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapHDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="26" BackColor="#F1F1F1"></asp:TextBox>
                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtChapHDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgCD_hdudate" runat="server"
                                            tabindex="27" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        MS Pages:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapMSpages" runat="server" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15" TabIndex="17"></asp:TextBox></td>
                                    <td>
                                        Due Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtChapDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex="28" BackColor="#F1F1F1"></asp:TextBox>
                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtChapDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgCD_dudate" runat="server"
                                            tabindex="29" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Typeset Pages:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapTypesetPages" runat="server" CssClass="TxtBox" Width="200px"
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" MaxLength="15"
                                            TabIndex="18"></asp:TextBox></td>
                                    <td>
                                        Despatch:</td>
                                    <td>
                                        <asp:CheckBox ID="chkChapDespatch" runat="server" TabIndex="30" /></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr><td>
                                            Author Name:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleAuthorName" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="14"></asp:TextBox></td>
                                        <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>    
                                </tr>
                                <tr>
                                    <td>Author Email:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleAuthEmail" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="15"></asp:TextBox></td>
                                     <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>         
                                    </tr> 
                                <tr>
                                    <td style="width: 105px">
                                        Figures:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapFigures" runat="server" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15" TabIndex="19"></asp:TextBox></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Tables:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapTables" runat="server" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15" TabIndex="20"></asp:TextBox></td>
                                    <td colspan="3" rowspan="3" valign="top" align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="3" class="subheading" valign="top">
                                                    <strong>Completed Stage(s)</strong></td>
                                            </tr>
                                            <tr>
                                                <td class="subheading" colspan="3" valign="top">
                                                    <asp:DataGrid ID="dgrdChapStages" runat="server" AutoGenerateColumns="False" CssClass="lightbackground">
                                                        <AlternatingItemStyle CssClass="dullbackground" />
                                                        <HeaderStyle CssClass="darkbackground" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Stage">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCD_StageName" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCD_StartDate" runat="server" Text='<%# Eval("received_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Due Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCD_DueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Half Due Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCD_HalfdueDate" runat="server" Text='<%# Eval("half_due_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Desp. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCD_DespDate" runat="server" Text='<%# Eval("despatch_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Equations:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapEquations" runat="server" CssClass="TxtBox" Width="200px"
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" MaxLength="15"
                                            TabIndex="21"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Comments:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtChapComments" runat="server" CssClass="TxtBox" Height="60px"
                                            TextMode="MultiLine" Width="300px" TabIndex="22"></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabBookEvents" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img7" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblEventsHeader" runat="server" Text="Logged Events"></asp:Label>
                                        </td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="imgbtnEventExport" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                            ToolTip="Export Excel" OnClick="imgbtnEventExport_Click" />
                                            </td>
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
                                                    <asp:TemplateField HeaderText="Job">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
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
                                <tr style="font-size: 8pt; font-family: Verdana">
                                    <td colspan="6" style="width: 100px">
                                        <strong>Chapter: <a id="aEvents1" href="#" onclick="javascript:showhideDiv(this,'divEvents1');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div id="divEvents1" style="display: block;">
                                            <asp:GridView ID="gvEvents1" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Job">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
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
                        <div class="content" id="tabGraphics" runat="server">
                            <table id="Table2" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img1" align="absmiddle" src="images/tools/new.png" runat="server" />
                                        <asp:Label ID="lblGraphicHeader" runat="server" Text="Graphic Details"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="cmdGD_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                                            ToolTip="Save" OnClick="cmdGD_Save_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <strong>Graphic Type:</strong><span style="font-size: 9pt; color: #ff0000">*</span>&nbsp;<asp:DropDownList
                                            ID="drpGraphicType" runat="server">
                                        </asp:DropDownList>
                                        &nbsp; <strong>No. of Items:</strong><span style="font-size: 9pt; color: #ff0000">*</span>
                                        <asp:TextBox ID="txtGraphicCount" runat="server" CssClass="TxtBox" MaxLength="2"
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" Width="31px"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnGraphicsAdd" runat="server" ImageUrl="~/images/tools/add.png"
                                            OnClick="imgbtnGraphicsAdd_Click" OnClientClick="javascript:return vallidGraphic();"
                                            ImageAlign="AbsMiddle" ToolTip="Add" /></td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvGraphicsDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                            OnRowDataBound="gvGraphicsDetails_RowDataBound" CssClass="lightbackground" ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.no.">
                                                    <ItemTemplate>
                                                        <%# id++ %>
                                                        <asp:HiddenField ID="hfGD_GraphicId" runat="server" Value='<%# Eval("graphic_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Graphic Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGD_i_graphicname" runat="server" Text='<%# Eval("graphic_name") %>'
                                                            Width="98%"></asp:Label>
                                                        <asp:TextBox ID="txtGD_i_graphicname" runat="server" CssClass="" Width="97%" Text='<%# Eval("graphic_name") %>'
                                                            MaxLength="50"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Graphic Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGD_i_graphictype" runat="server" Text='<%# Eval("graphic_type_name") %>'
                                                            Width="98%"></asp:Label>
                                                        <asp:DropDownList ID="drpGD_i_graphictype" runat="server" DataSource="<%# loadGraphicType() %>"
                                                            DataTextField="graphic_type_name" DataValueField="graphic_type_id" SelectedValue='<%# Eval("graphic_type_id") %>'
                                                            Width="98%">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Graphic Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGD_i_draphic_desc" runat="server" Text='<%# Eval("graphic_desc") %>'
                                                            Width="98%"></asp:Label><asp:TextBox ID="txtGD_i_Graphicdesc" runat="server" Text='<%# Eval("graphic_desc") %>'
                                                                Width="98%" CssClass="" MaxLength="100"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkGD_i_editordelete" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="imbtnGraghicEdit" runat="server" ImageUrl="~/images/tools/edit.png"
                                                            ImageAlign="AbsMiddle" OnClick="imbtnGraghicEdit_Click" ToolTip="Edit" />
                                                        <asp:ImageButton ID="imgbtn_GD_delete" runat="server" ImageUrl="~/images/tools/delete.png"
                                                            ImageAlign="AbsMiddle" OnClick="imgbtn_GD_delete_Click" OnClientClick="return confirm('Confirm Delete?');"
                                                            ToolTip="Delete" />
                                                    </FooterTemplate>
                                                    <HeaderTemplate>
                                                        <asp:ImageButton ID="imbtnGraghicEdit" runat="server" ImageUrl="~/images/tools/edit.png"
                                                            ImageAlign="AbsMiddle" OnClick="imbtnGraghicEdit_Click" ToolTip="Edit" />
                                                        <asp:ImageButton ID="imgbtn_GD_delete" runat="server" ImageUrl="~/images/tools/delete.png"
                                                            ImageAlign="AbsBottom" OnClick="imgbtn_GD_delete_Click" OnClientClick="return confirm('Confirm Delete?');"
                                                            ToolTip="Delete" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="darkbackground" />
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                        </asp:GridView>
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
                                        <strong>Book: <a id="alinkcomments" href="#" onclick="javascript:showhideDiv(this,'divCommHistory');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divCommHistory" style="display: block;">
                                            <asp:GridView ID="gvCommHistory" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
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
                                <tr>
                                    <td style="width: 100px">
                                        <strong>Chapter: <a id="alinkcomments1" href="#" onclick="javascript:showhideDiv(this,'divCommHistory1');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divCommHistory1" style="display: block;">
                                            <asp:GridView ID="gvCommHistory1" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
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
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkBookdetails','');">Show Details</div>
    </form>
</body>
</html>
