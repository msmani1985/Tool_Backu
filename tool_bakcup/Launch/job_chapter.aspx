<%@ page language="C#" autoeventwireup="true" inherits="job_chapter, App_Web_opij0lkt" enableeventvalidation="false" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Chapter</title>
    <link href="default.css" rel="stylesheet" type="text/css" />    
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>
    <script type="text/javascript">
        var gvelem;
        var gvcolor;
        function setColor(element,val,val1,val2){      
          //alert(gvelem);
          if(gvelem!=null){//alert(gvelem.style.backgroundColor);
            gvelem.style.backgroundColor = gvcolor;
          }
          gvelem = element;
          gvcolor = element.style.backgroundColor;
          element.style.backgroundColor = '#C2C2C2';
          document.form1.hfA_ID.value=val;
          document.form1.hfA_Name.value=val1
          document.form1.hfA_Journal.value=val2;
          if(document.getElementById("lblArticleSummary"))
            document.getElementById("lblArticleSummary").innerText="Article : "+val2+val1;
          else if(document.all.lblArticleSummary)
            document.all.lblArticleSummary.innerText="Article : "+val2+val1;
          else if(document.form1.lblArticleSummary)
            document.form1.lblArticleSummary.innerText="Article : "+val2+val1;
            doTimer();
        }
        function setMouseOverColor(element)
        {
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor='#C2C2C2';
            element.style.cursor='hand';
            element.style.textDecoration='underline';
        }
        function setMouseOutColor(element)
        {
            element.style.backgroundColor=gvcolor;
            element.style.textDecoration='none';
        }
        function vallidGraphic(){
        var msg = "";
        if(document.form1.drpGFigureType!=null && document.form1.drpGFigureType.value =="0")
            msg+="* Select a Figure Type.\r\n";
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
        function artOnhold(){
            if(document.form1.hfA_ID.value==''){
                alert('You should first create an Article.');
                document.form1.chkArticlOnHold.checked = false;
                return;
            }
            if(!document.form1.chkArticlOnHold.checked){
                if(confirm('This job is currently On Hold, Do you want to release?')){
                    document.form1.chkArticlOnHold.checked = false;
                    __doPostBack('lnkArticleHold','');
                    }
                else document.form1.chkArticlOnHold.checked = true;
            }
            else
            {
                document.getElementById ('divPopArtOnHold').style.visibility='visible';
                document.getElementById ('divPopArtOnHold').style.display='';       
                document.getElementById ('divPopArtOnHold').style.top= '150px';
                document.getElementById ('divPopArtOnHold').style.left='248px';   
                if (typeof document.body.style.maxHeight == "undefined")
                {
                    var layer = document.getElementById ('divPopArtOnHold');
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
                document.form1.drpArticleOnHoldType.value='0';
                document.form1.txtArticleOnHoldReason.value='';
            }
        }
        function closeModalArtHold(){
        document.form1.chkArticlOnHold.checked = false;
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopArtOnHold').style.display='none';
        document.getElementById ('iframetop').style.display='none';
        }
        function openAdvancedModal(){        
            document.getElementById ('divPopAdvancedSearch').style.visibility='visible';
            document.getElementById ('divPopAdvancedSearch').style.display='';       
            document.getElementById ('divPopAdvancedSearch').style.top= '65px';
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
            showhideAdvancedEndDate(document.form1.drpAdvRecExpr,'divAdvRecEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvDueExpr,'divAdvDueEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvHlfDueRecExpr,'divAdvHlfDueEndDate');
            showhideAdvancedEndDate(document.form1.drpAdvCatsDueExpr,'divAdvCatsDueEndDate');
        }
        function closeAdvancedModal(){
            document.getElementById ('divMasked').style.display='none';
            document.getElementById ('divPopAdvancedSearch').style.display='none';
            document.getElementById ('iframetop').style.display='none';
        }
        function showhideAdvancedEndDate(me, ctrl){
            var o;
            if(document.getElementById(ctrl))o=document.getElementById(ctrl);
            else if(document.all.ctrl)o=document.all.ctrl;
            else if(document.form1.ctrl)o=document.form1.ctrl;
             //alert(me.innerText);   
             if(me.value=='between')
             {o.style.display = 'block'}
             else{o.style.display = 'none';}
        }
        function validSaveItem(){
            if(document.form1.drpArticleOnHoldType.value=='0')alert('Select an hold type');
            else if(document.form1.txtArticleOnHoldReason.value=='')alert('Enter hold reason');
            else __doPostBack('lnkArticleHold','');
        }
            function printArticle(){if(document.form1.hfA_ID.value==""){alert('Select a Article');return false;} var w=window.open('jobbag.aspx?jobid=' + document.form1.hfA_ID.value + '&jobtypeid=7&print=1','Preview','width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;}
        function clearAdvancedCtrls(){
        document.form1.chkAdvOnHold.checked=false;
        document.form1.rblstAdvCompleted_0.checked=true;
        document.form1.drpAdvJourCodeExp.value="Like";
        document.form1.drpAdvArtCodeExp.value="Like";
        document.form1.drpAdvIssueNumExp.value="Like";
        document.form1.txtAdvJourCode.value="";
        document.form1.txtAdvIssueNum.value="";
        document.form1.txtAdvArtCode.value="";
        document.form1.drpAdvRecExpr.value="between";
        document.form1.drpAdvDueExpr.value="between";
        document.form1.drpAdvHlfDueRecExpr.value="between";
        document.form1.drpAdvCatsDueExpr.value="between";        
        showhideAdvancedEndDate(document.form1.drpAdvRecExpr,'divAdvRecEndDate');
        showhideAdvancedEndDate(document.form1.drpAdvDueExpr,'divAdvDueEndDate');
        showhideAdvancedEndDate(document.form1.drpAdvHlfDueRecExpr,'divAdvHlfDueEndDate');
        showhideAdvancedEndDate(document.form1.drpAdvCatsDueExpr,'divAdvCatsDueEndDate');
        document.form1.txtAdvRecDate1.value="";
        document.form1.txtAdvRecDate2.value="";
        document.form1.txtAdvDueDate1.value="";
        document.form1.txtAdvDueDate2.value="";
        document.form1.txtAdvHlfDueDate1.value="";
        document.form1.txtAdvHlfDueDate2.value="";
        document.form1.txtAdvCatsDueDate1.value="";
        document.form1.txtAdvCatsDueDate2.value="";
        if(navigator.userAgent.indexOf('Firefox')!=-1){
        document.form1.lstAdvancedStage.selectedIndex=-1;
        document.form1.lstAdvancedCustomer.selectedIndex=-1;        
        }
        else{
        document.form1.lstAdvancedStage.value=-1;
        document.form1.lstAdvancedCustomer.value=-1;
        
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
                                    <td colspan="3">
                                        <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Chapter</strong></td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Chapter Name</strong></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="318px" style="text-transform:uppercase" CssClass="txtArticleSearch" TabIndex="1"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="dpbutton" OnClick="btnSearch_Click" TabIndex="4" />
                                        <input id="btnAdvanced" class="dpbutton"
                                                style="width: 80pt" type="button" value="Options" onclick="javascript:openAdvancedModal();" />
                                        <asp:HiddenField ID="hfA_ID" runat="server" />
                                        <asp:HiddenField ID="hfA_Name" runat="server" />
                                        <asp:HiddenField ID="hfA_Journal" runat="server" />                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkViewCompleted" runat="server" Font-Bold="True" Text="Show Completed" TabIndex="2" /><asp:RadioButtonList
                                            ID="rblstArticleType" runat="server" Font-Bold="True" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" TabIndex="3">
                                            <asp:ListItem Selected="True" Value="0">Interior Chapters</asp:ListItem>
                                            <asp:ListItem Value="1">All Chapters</asp:ListItem>
                                        </asp:RadioButtonList></td>
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
                                                    <td style="width: 108px">
                                                        &nbsp;Show</td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblstAdvCompleted" runat="server" CellPadding="0" CellSpacing="0"
                                                            RepeatDirection="Horizontal" RepeatLayout="Flow">                                                            
                                                            <asp:ListItem Value="inprogress" Selected="True">In Progress</asp:ListItem>
                                                            <asp:ListItem Value="completed">View Completed</asp:ListItem>
                                                            <asp:ListItem Value="all">All Chapters</asp:ListItem>
                                                        </asp:RadioButtonList></td>
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
                                                        &nbsp;Article Code:</td>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="drpAdvArtCodeExp" runat="server">
                                                            <asp:ListItem>Like</asp:ListItem>
                                                            <asp:ListItem>=</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtAdvArtCode" runat="server" CssClass="TxtBox" MaxLength="50" Style="text-transform: uppercase"
                                                            Width="120px"></asp:TextBox></td>
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
                                                        &nbsp;Cats Due Date:</td>
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
                                <asp:LinkButton ID="lnkGeneral" runat="server" OnClick="lnkGeneral_Click" TabIndex="5">General</asp:LinkButton></li>
                            <li id="miArticleDetails" runat="server">
                                <asp:LinkButton ID="lnkArticledetails" runat="server" OnClick="lnkArticledetails_Click" TabIndex="6">Chapter Details</asp:LinkButton></li>                            
                            <li id="miArticleEvents" runat="server">
                                <asp:LinkButton ID="lnkArticleEvents" runat="server" OnClick="lnkArticleEvents_Click" TabIndex="7">Events</asp:LinkButton></li>
                            <li id="miGraphics" runat="server">
                                <asp:LinkButton ID="lnkGraphics" runat="server" OnClick="lnkGraphics_Click" TabIndex="8">Graphic</asp:LinkButton></li>
                            <li id="miComments" runat="server">
                                <asp:LinkButton ID="lnkComments" runat="server" OnClick="lnkComments_Click" TabIndex="9">Comments</asp:LinkButton></li>
                        </ol>
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblArticleSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                    <td align="right">
                                        <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                                ToolTip="Export Excel" OnClick="cmd_Excel_Export_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvArticles" runat="server" Width="100%" OnRowDataBound="gvArticles_RowDataBound"
                                            AutoGenerateColumns="false" Font-Size="8pt" CssClass="lightbackground" AllowSorting="True" OnSorting="gvArticles_Sorting">
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" /> 
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvArticleID" runat="server" Value='<%# Eval("job_id") %>' />
                                                        <asp:HiddenField ID="hfgvArticleName" runat="server" Value='<%# Eval("name") %>' />
                                                        <asp:HiddenField ID="hfgvJournal" runat="server" Value='<%# Eval("journal_code") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job ID" SortExpression="job_id" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJournal" runat="server" Text='<%# Eval("job_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage" SortExpression="job_stage_name" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobStage" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chapter No." SortExpression="articlecode" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvArticleNo" runat="server" Text='<%# Eval("articlecode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rec. Date" SortExpression="received_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvARecDate" runat="server" Text='<%# Eval("received_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Half Due Date" SortExpression="half_due_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAHlfDueDate" runat="server" Text='<%# Eval("half_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Due Date" SortExpression="due_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvADueDate" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cats Due Date" SortExpression="cats_due_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvACatsDueDate" runat="server" Text='<%# Eval("cats_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disp. Date" SortExpression="despatch_date" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvADispDate" runat="server" Text='<%# Eval("despatch_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue" SortExpression="Issue" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvIssue" runat="server" Text='<%# Eval("Issue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Chapter Title" SortExpression="title" HeaderStyle-ForeColor="white">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvArticleTitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
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
                        <div class="content" id="tabArticleDetails" runat="server">
                            <div id="CUSTOMER_TABLE" class="boxTable">
                            </div>
                            <div id="PARENT_JOB" class="boxTable" style="">
                                <table id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                    <tr bgcolor="#f0fff0">
                                        <td colspan="3" class="dpJobGreenHeader">
                                            <img id="imgArticleHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblArticleHeader" runat="server" Text="Label">New chapter</asp:Label></td>
                                        <td class="dpJobGreenHeader" >
                                            <asp:ImageButton ID="cmd_Save_Article" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save Article" OnClick="cmd_Save_Article_Click" TabIndex="41" />
                                            <asp:ImageButton ID="cmd_Print_Article" ImageUrl="~/images/tools/j_print.png" runat="server"
                                                ToolTip="Print Preview" OnClick="cmd_Print_Article_Click" OnClientClick="javascript:return printArticle()" TabIndex="42" />
                                                </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="drpArticlecustomer" runat="server" Width="500px" OnSelectedIndexChanged="drpArticlecustomer_SelectedIndexChanged" AutoPostBack="True" TabIndex="10">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Book: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="drpArticleJournal" runat="server" Width="500px" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="drpArticleJournal_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mns. ID: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtArticleMnsID" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="200px" MaxLength="30" TabIndex="12"></asp:TextBox></td>
                                        <td>
                                            Chapter&nbsp;Stage:</td>
                                        <td>
                                            <asp:DropDownList ID="drpArticleStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpArticleStage_SelectedIndexChanged" TabIndex="29">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mns. Title:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleTitle" runat="server" CssClass="TxtBox" Width="300px"
                                                MaxLength="300" TabIndex="13"></asp:TextBox></td>
                                        <td>
                                            Start Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleSdate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="30" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_stdate" runat="server" tabindex="31" /></td>
                                                
                                    </tr>
                                    <tr>
                                        <td>
                                            Author Corr.:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleAuthorCorr" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="14"></asp:TextBox></td>
                                            <td>
                                            Half-Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleHDdate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="32" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleHDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_hdudate" runat="server" tabindex="33" /></td>
                                       <td>
                                        </td>
                                               
                                    </tr>
                                    <tr>
                                        <td>
                                            Author Email:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleAuthEmail" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="15"></asp:TextBox></td>
                                         <td>
                                            Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleDdate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="34" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_dudate" runat="server" tabindex="35" /></td>
                                            
                                    </tr>
                                    <tr>
                                        <td>
                                            No. of Authors:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleNoofAuth" runat="server" CssClass="TxtBox" MaxLength="300" Width="80px" onkeypress="javascript: return OnlyAllowNumbers(this,event);" TabIndex="16"></asp:TextBox>
                                            <asp:CheckBox ID="chkArticleRegCopyedit" runat="server" Font-Bold="False" Text="Regular Copyediting" TabIndex="17" />
                                            <asp:CheckBox ID="chkArticlOnHold" onclick="javascript:artOnhold();" runat="server" Font-Bold="False" Text="On Hold" TabIndex="18" /></td>
                                             <td>
                                            Cats Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleCDdate" runat="server" CssClass="TxtBox" Width="100px" TabIndex="36" BackColor="#F1F1F1"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleCDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_cdudate" runat="server" tabindex="37" /></td>
                                        <td>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            Doc Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpArticleDoctype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpArticleDoctype_SelectedIndexChanged" TabIndex="19">
                                            </asp:DropDownList></td>
                                        <td>
                                            Despatch:</td>
                                        <td>
                                            <asp:CheckBox ID="chkArticleDespatch" runat="server" TabIndex="38" /></td>
                                        
                                    </tr>
                                    <tr>
                                        <td style="height: 25px">
                                            Sub Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td style="height: 25px">
                                            <asp:DropDownList ID="drpArticleSubtype" runat="server" TabIndex="20">
                                            </asp:DropDownList></td>
                                            <td style="height: 25px">Sales Group: </td>
                                        <td style="height: 25px"><asp:DropDownList ID="drpArticleSalesGroup" runat="server" TabIndex="38" /></td>
                                            
                                            <td>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            Category:</td>
                                        <td>
                                            <asp:DropDownList ID="drpArticleCategory" runat="server" TabIndex="21">
                                            </asp:DropDownList></td>
                                            <td>Interview Date & time</td>
                                            
                                            <td><asp:TextBox ID="txtinterviewdate" runat="server" Width="100px"></asp:TextBox><img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtinterviewdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imginterview" runat="server"  />
                                            Time: <asp:TextBox ID="txtinterviewtime" runat="server" Width="100px"></asp:TextBox><asp:Label Text="(eg. 8.15-8.30)" ID="exlbl" ForeColor="red" runat="server"></asp:Label></td>
                                           
                                    </tr>
                                    <tr>
                                        <td>
                                            Book Name</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleIssueNo" runat="server" BackColor="#DFDFDF" CssClass="TxtBox" MaxLength="300"
                                                Width="80px" TabIndex="22"></asp:TextBox>
                                            Sequence:
                                            <asp:TextBox ID="txtArticleSequence" runat="server" BackColor="#DFDFDF" CssClass="TxtBox" MaxLength="300"
                                                Width="80px" TabIndex="23"></asp:TextBox></td>
                                                <td>Phone No.</td><td><asp:TextBox ID="txtphoneno" runat="server" Width="120px"></asp:TextBox></td>
                                                
                                                <td>
                                        </td>
                                                
                                    </tr>
                                    <tr>
                                        <td>
                                            No. of Images:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleNoofImages" runat="server" CssClass="TxtBox" BackColor="#dfdfdf" MaxLength="300" Width="80px" TabIndex="24"></asp:TextBox>
                                            Ms Rec. Date &nbsp;&nbsp;
                                            <asp:TextBox ID="txtArtMsRecDate" runat="server" BackColor="#F1F1F1" CssClass="TxtBox" TabIndex="30"
                                                Width="80px"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArtMsRecDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img13" runat="server" tabindex="31" /></td>
                                             <td>Fax No.</td><td><asp:TextBox ID="txtfaxno" runat="server" Width="120px"></asp:TextBox></td>   
                                                <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Mns. Pages:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleMspages" onkeypress="javascript: return OnlyAllowNumbers(this,event);" runat="server" CssClass="TxtBox" MaxLength="300" Width="80px" TabIndex="25"></asp:TextBox>
                                            Ms Rev. Date &nbsp;&nbsp;
                                            <asp:TextBox ID="txtArtMsRevDate" runat="server" BackColor="#F1F1F1" CssClass="TxtBox" TabIndex="30"
                                                Width="80px"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArtMsRevDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img14" runat="server" tabindex="31" /></td>
                                                <td>Meeting Place:</td><td><asp:TextBox ID="txt_meetingplace" runat="server" Width="120px"></asp:TextBox></td>
                                               <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Print Pages:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticlePrintpages" onkeypress="javascript: return OnlyAllowNumbers(this,event);" runat="server" CssClass="TxtBox" MaxLength="300" Width="80px" TabIndex="26"></asp:TextBox>
                                            Ms Acept. Date
                                            <asp:TextBox ID="txtArtMsAcceptDate" runat="server" BackColor="#F1F1F1" CssClass="TxtBox" TabIndex="30"
                                                Width="80px"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArtMsAcceptDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="Img15" runat="server" tabindex="31" /></td>
                                                <td style="height: 25px">Meeting Time:</td><td style="height: 25px"><asp:TextBox ID="txt_meetingtime" runat="server" Width="120px"></asp:TextBox></td>
                                                <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DOI Number:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleDOI" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="27"></asp:TextBox></td>
                                            <td>Country:</td><td><asp:TextBox ID="txt_country" runat="server" Width="120px"></asp:TextBox></td>
                                            <td>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td rowspan="4">
                                            Comments:</td>
                                        <td>
                                            <asp:TextBox ID="txtArticleComments" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex="28"></asp:TextBox>
                                            <asp:LinkButton ID="lnkArticleHold" runat="server" OnClick="lnkArticleHold_Click"></asp:LinkButton>    
                                                </td>
                                                
                                                 <td align="left" colspan="2" rowspan="8" valign="top">
                                            <div style="vertical-align: top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                                <tr><td >Zone:</td><td style="height: 21px"><asp:TextBox ID="txt_zone" runat="server" Width="120px"></asp:TextBox></td></tr>
                                                <tr><td>Appointment Date: <br />Between</td><td><asp:TextBox ID="txt_appointmentdate1" runat="server" Width="100px"></asp:TextBox><img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_appointmentdate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img16" runat="server"  />
                                                &nbsp;and&nbsp;<asp:TextBox ID="txt_appointmentdate2" runat="server" Width="100px"></asp:TextBox><img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_appointmentdate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img17" runat="server"  /></td></tr>
                                                    <tr>
                                                        <td colspan="3" class="subheading">
                                                            <strong>Completed Stage(s)</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="subheading" colspan="3" valign="top">
                                                            <asp:DataGrid ID="dgrdArticleStages" runat="server" AutoGenerateColumns="False" CssClass="lightbackground">
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
                                            <div id="divPopArtOnHold" class="ModalPopup">
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                            width: 163px;">
                                                            &nbsp;Article On Hold
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
                                                            <asp:DropDownList ID="drpArticleOnHoldType" runat="server">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtArticleOnHoldReason" runat="server" CssClass="TxtBox" Width="180px"
                                                                MaxLength="300"></asp:TextBox></td>
                                                    </tr>
                                                    <tr bgcolor="Honeydew">
                                                        <td colspan="2" align="center">
                                                            <a class="link1" href="#" onclick="javascript:validSaveItem();">
                                                                <strong>Submit</strong></a> &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtHold();">
                                                                    <strong>Cancel</strong></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="content" id="tabArticleEvents" runat="server">
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
                                                            <%# Eval("articlename")%>
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
                                        <strong>Figure Type:</strong><span style="font-size: 9pt; color: #ff0000">*
                                            <asp:DropDownList
                                            ID="drpGFigureType" runat="server">
                                            </asp:DropDownList></span><strong>&nbsp; Graphic Type:</strong><span style="font-size: 9pt; color: #ff0000">*</span>&nbsp;<asp:DropDownList
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
                                                <asp:TemplateField HeaderText="Figure Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGD_i_figtype" runat="server" Text='<%# Eval("figure_type_name") %>'
                                                            Width="98%"></asp:Label><asp:DropDownList ID="drpGD_i_figtype" runat="server" DataSource="<%# loadFigureType() %>"
                                                            DataTextField="figure_type_name" DataValueField="figure_type_id" SelectedValue='<%# Eval("figure_type_id") %>'
                                                            Width="98%" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
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
                                                            DataTextField="graphic_type_name" DataValueField="graphic_type_id" SelectedValue='<%# Eval("graphic_type_id") %>'>
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
                                                <asp:TemplateField HeaderText="Rec. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGD_i_recdate" runat="server" Text='<%# Eval("received_date") %>'
                                                            Width="98%"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Redraw?">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGD_i_IsRedraw" runat="server" Text='<%# Eval("isredraw") %>'></asp:Label>
                                                        <asp:DropDownList ID="drpGD_i_Redraw" runat="server" SelectedValue='<%# Eval("isredraw") %>'>
                                                            <asp:ListItem Value="N">N</asp:ListItem>
                                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                                        </asp:DropDownList>
                                                        
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
                                        <div id="divCommHistory" style="display: block;">
                                            <asp:GridView ID="gvCommHistory" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%# Eval("articlename")%>
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
        <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkArticledetails','');">Show Details</div>
    </form>
</body>
</html>
