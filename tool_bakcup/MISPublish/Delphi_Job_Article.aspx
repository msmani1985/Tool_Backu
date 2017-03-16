<%@ page language="C#" autoeventwireup="true" inherits="Delphi_Job_Article, App_Web_xuje0h3i" %>

 
 

<%-- <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> --%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <head id="Head1" runat="server">
    <title>Article</title>
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
            document.form1.hfA_ID.value = val;
            document.form1.hfA_Name.value = val1
            document.form1.hfA_Journal.value = val2;
            if (document.getElementById("lblArticleSummary"))
                document.getElementById("lblArticleSummary").innerText = "Article : " + val2 + val1;
            else if (document.all.lblArticleSummary)
                document.all.lblArticleSummary.innerText = "Article : " + val2 + val1;
            else if (document.form1.lblArticleSummary)
                document.form1.lblArticleSummary.innerText = "Article : " + val2 + val1;
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
        function artOnhold() {
            if (document.form1.hfA_ID.value == '') {
                alert('You should first create an Article.');
                document.form1.chkArticlOnHold.checked = false;
                return;
            }
            if (!document.form1.chkArticlOnHold.checked) {
                if (confirm('This job is currently On Hold, Do you want to release?')) {
                    document.form1.chkArticlOnHold.checked = false;
                    __doPostBack('lnkArticleHold', '');
                }
                else document.form1.chkArticlOnHold.checked = true;
            }
            else {
                document.getElementById('divPopArtOnHold').style.visibility = 'visible';
                document.getElementById('divPopArtOnHold').style.display = '';
                document.getElementById('divPopArtOnHold').style.top = '150px';
                document.getElementById('divPopArtOnHold').style.left = '248px';
                if (typeof document.body.style.maxHeight == "undefined") {
                    var layer = document.getElementById('divPopArtOnHold');
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
                document.form1.drpArticleOnHoldType.value = '0';
                document.form1.txtArticleOnHoldReason.value = '';
            }
        }
        function closeModalArtHold() {
            document.form1.chkArticlOnHold.checked = false;
            document.getElementById('divMasked').style.display = 'none';
            document.getElementById('divPopArtOnHold').style.display = 'none';
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
            if (document.form1.drpArticleOnHoldType.value == '0') alert('Select an hold type');
            else if (document.form1.txtArticleOnHoldReason.value == '') alert('Enter hold reason');
            else __doPostBack('lnkArticleHold', '');
        }
        function printArticle() { if (document.form1.hfA_ID.value == "") { alert('Select a Article'); return false; } var w = window.open('jobbag.aspx?jobid=' + document.form1.hfA_ID.value + '&jobtypeid=5&print=1', 'Preview', 'width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes'); w.focus(); return false; }
        function clearAdvancedCtrls() {
            document.form1.chkAdvOnHold.checked = false;
            document.form1.rblstAdvCompleted_0.checked = true;
            document.form1.drpAdvJourCodeExp.value = "Like";
            document.form1.drpAdvArtCodeExp.value = "Like";
            document.form1.drpAdvIssueNumExp.value = "Like";
            document.form1.txtAdvJourCode.value = "";
            document.form1.txtAdvIssueNum.value = "";
            document.form1.txtAdvArtCode.value = "";
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
        .style1
        {
            width: 261px;
        }
    </style>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>

 
         
                <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
                </div>

                <iframe width="0" scrolling="no" height="0"
                    frameborder="0" class="divMasked" id="iframetop"></iframe>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <div>
                                    <table class="content" width="100%">
                                        <tr class="dpJobGreenHeader">
                                            <td colspan="3">
                                                <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Article</strong></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 85px">
                                                <strong>Article No.</strong></td>
                                            <td colspan="2">

                                                <asp:TextBox ID="txtSearch" runat="server" Width="318px" Style="text-transform: uppercase" CssClass="txtArticleSearch" TabIndex="1"></asp:TextBox>


                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="dpbutton" OnClick="btnSearch_Click" TabIndex="4" />
                                                <input id="btnAdvanced" class="dpbutton"
                                                    style="width: 80pt" type="button" value="Options" onclick="javascript: openAdvancedModal();" visible="false" runat="server" />
                                                <asp:HiddenField ID="hfA_ID" runat="server" />
                                                <asp:HiddenField ID="hfA_Name" runat="server" />
                                                <asp:HiddenField ID="hfA_Journal" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 85px"></td>
                                            <td colspan="2">
                                                <asp:CheckBox ID="chkViewCompleted" runat="server" Font-Bold="True" Text="Show Completed" TabIndex="2" AutoPostBack="True" OnCheckedChanged="chkViewCompleted_CheckedChanged" /><asp:RadioButtonList
                                                    ID="rblstArticleType" runat="server" Font-Bold="True" RepeatDirection="Horizontal"
                                                    RepeatLayout="Flow" TabIndex="3">
                                                    <asp:ListItem Selected="True" Value="0">Interior Articles</asp:ListItem>
                                                    <asp:ListItem Value="1">All Articles</asp:ListItem>
                                                </asp:RadioButtonList></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="lblMonth" runat="server" Text="Month" Visible="False"></asp:Label>
                                                </strong>&nbsp;
                    <asp:DropDownList ID="DDMonthList" runat="server" Visible="False">
                        <asp:ListItem Value="0">---All---</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                                                &nbsp;&nbsp;<strong><asp:Label ID="lblYear" runat="server" Text="Year" Visible="False"></asp:Label></strong>&nbsp;
                    <asp:DropDownList ID="DDYearList" runat="server" Visible="False">
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2013">2013</asp:ListItem>
                        <asp:ListItem Value="2012">2012</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                    </asp:DropDownList>
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
                                        <asp:LinkButton ID="lnkArticledetails" runat="server" OnClick="lnkArticledetails_Click" TabIndex="6">Article Details</asp:LinkButton></li>
                                    <li id="miArticleEvents" runat="server">
                                        <asp:LinkButton ID="lnkArticleEvents" runat="server" OnClick="lnkArticleEvents_Click" TabIndex="7">Events</asp:LinkButton></li>
                                    <li id="miGraphics" runat="server">
                                        <asp:LinkButton ID="lnkGraphics" runat="server" OnClick="lnkGraphics_Click" TabIndex="8" >Graphic</asp:LinkButton></li>
                                    <li id="miComments" runat="server">
                                        <asp:LinkButton ID="lnkComments" runat="server" OnClick="lnkComments_Click" TabIndex="9"  >Comments</asp:LinkButton></li>
                                    <li id="miHoldHistory" runat="server">
                                        <asp:LinkButton ID="lnkHoldHistory" runat="server" OnClick="lnkHoldHistory_Click" TabIndex="10" >Hold History</asp:LinkButton></li>
                                    <li id="miArtWork" runat="server">
                                        <asp:LinkButton ID="lnkArtWork" runat="server" TabIndex="11" OnClick="lnkArtWork_Click">Art Work</asp:LinkButton></li>
                                    <li id="miInvoiceSetup" runat="server">
                                        <asp:LinkButton ID="InvoiceSetup" runat="server" TabIndex="11" OnClick="lnkInvoicesetup_Click">Invoice setup</asp:LinkButton></li>
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
                                                        <asp:TemplateField HeaderText="Article No." SortExpression="aarticlecode" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvArticleNo" runat="server" Text='<%# Eval("aarticlecode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rec. Date" SortExpression="received_date" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvARecDate" runat="server" Text='<%# Eval("received_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S200 Created" SortExpression="S200 Created" HeaderStyle-ForeColor="white" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvS100" runat="server" Text='<%# Eval("S200_CREATED","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Due Date" SortExpression="due_date" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvADueDate" runat="server" Text='<%# Eval("due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Copy Edit Due Date" SortExpression="cats_due_date" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvAcatsDueDate" runat="server" Text='<%# Eval("cats_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Cats Due Date" SortExpression="Actual_due_date" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvActualDueDate" runat="server" Text='<%# Eval("Actual_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     

                                                        <asp:TemplateField HeaderText="Disp. Date" SortExpression="despatch_date" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvADispDate" runat="server" Text='<%# Eval("despatch_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Assign To" SortExpression="EMPNAME" HeaderStyle-ForeColor="white" Visible="false">
                                                            <ItemTemplate>
                                                                <%--   <asp:Label ID="lblgvEMPNAME" runat="server" Text='<%# Eval("EMPNAME") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DNAME" SortExpression="DNAME" HeaderStyle-ForeColor="white" Visible="false">
                                                            <ItemTemplate>
                                                                <%--  <asp:Label ID="lblgvDNAME" runat="server" Text='<%# Eval("DNAME") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Issue" SortExpression="Issue" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvIssue" runat="server" Text='<%# Eval("Issue") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hold Details" SortExpression="Hold_details" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblgvHoldDetails" runat="server" Text='<%# Eval("Hold_details") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Article Title" SortExpression="title" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvArticleTitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Journal Name" SortExpression="TITLE" HeaderStyle-ForeColor="white" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvTITLE" runat="server" Text='<%# Eval("journal_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Invoice No." SortExpression="InvNo" HeaderStyle-ForeColor="white">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvInvNo" runat="server" Text='<%# Eval("InvNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                            No records found.
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="content" id="tabArticleDetails" runat="server">
                                    <div id="CUSTOMER_TABLE" class="boxTable">
                                    </div>



                                    <table id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                        <tr bgcolor="#f0fff0">
                                            <td colspan="3" class="dpJobGreenHeader">
                                                <img id="imgArticleHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                    ID="lblArticleHeader" runat="server" Text="Label">New Article</asp:Label></td>
                                            <td class="dpJobGreenHeader">
                                                <asp:ImageButton ID="cmd_New_Article" ImageUrl="~/images/tools/j_new.png" runat="server"
                                                    ToolTip="New Article" OnClick="cmd_New_Article_Click" TabIndex="40" />
                                                <asp:ImageButton ID="cmd_Save_Article" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                    ToolTip="Save Article" OnClick="cmd_Save_Article_Click" TabIndex="41" style="height: 32px" />
                                                <asp:ImageButton ID="cmd_Print_Article" ImageUrl="~/images/tools/j_print.png" runat="server"
                                                    ToolTip="Print Preview" OnClick="cmd_Print_Article_Click" OnClientClick="javascript:return printArticle()" TabIndex="42" />
                                                <asp:ImageButton ID="cmd_XML_" ImageUrl="~/images/tools/xml.png" runat="server"
                                                    ToolTip="Generate meta xml"  TabIndex="43" Height="28px" OnClick="cmd_XML__Click" Width="36px"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="drpArticlecustomer" runat="server" Width="500px"  OnSelectedIndexChanged="drpArticlecustomer_SelectedIndexChanged" AutoPostBack="True" TabIndex="10">
                                                </asp:DropDownList></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Journal: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="drpArticleJournal" runat="server" Width="500px" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="drpArticleJournal_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>Mns. &nbsp; &nbsp; ID: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtArticleMnsID" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                    Width="200px" MaxLength="30" TabIndex="12"></asp:TextBox></td>
                                            <td>Article&nbsp;Stage:</td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleStage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpArticleStage_SelectedIndexChanged" TabIndex="29">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>Mns. Title:</td>
                                            <td>
                                                <asp:TextBox ID="txtArticleTitle" runat="server" CssClass="TxtBox" Width="300px"
                                                    MaxLength="300" TabIndex="13"></asp:TextBox></td>
                                            <td>Start Date:</td>
                                            <td>
                                                <asp:TextBox ID="txtArticleSdate" runat="server" CssClass="TxtBox"
                                                    Width="100px" TabIndex="30" BackColor="#F1F1F1" Enabled="true"></asp:TextBox>
                                                 
                                    <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_stdate"
                                        runat="server" tabindex="31" /></td>

                                        </tr>
                                        <tr>
                                            <td>Author Corr.:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtArticleAuthorCorr" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="14"></asp:TextBox></td>
                                            <td>Copy Edit Due Date:</td>
                                            <td>
                                                <asp:TextBox ID="txtArticleActDdate" runat="server" CssClass="TxtBox"
                                                    Width="100px" TabIndex="32" BackColor="#F1F1F1" Enabled="False"></asp:TextBox>
                                           <%--     <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleHDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_hdudate" runat="server" tabindex="33" enableviewstate="True"/>--%>

                                            </td>
                                            <td></td>

                                        </tr>
                                        <tr>
                                            <td>Author Email:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtArticleAuthEmail" runat="server" CssClass="TxtBox" MaxLength="300" Width="300px" TabIndex="15"></asp:TextBox></td>
                                            <td>Due Date:</td>
                                            <td>
                                                <asp:TextBox ID="txtArticleDdate" runat="server" CssClass="TxtBox"
                                                    Width="100px" TabIndex="34" BackColor="#F1F1F1"></asp:TextBox>
                                                <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_dudate" runat="server" tabindex="35" /></td>

                                        </tr>
                                        <tr>
                                            <td>No. of Authors:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtArticleNoofAuth" runat="server" CssClass="TxtBox" MaxLength="300" Width="80px" onkeypress="javascript: return OnlyAllowNumbers(this,event);" TabIndex="16"></asp:TextBox>
                                                <asp:CheckBox ID="chkArticleRegCopyedit0" runat="server" Font-Bold="False"
                                                    TabIndex="17" Text="Regular Copyediting" />
                                                <asp:CheckBox ID="chkArticlOnHold" onclick="javascript:artOnhold();" runat="server" Font-Bold="False" Text="On Hold" TabIndex="18" />
                                            </td>
                                            <td>TAT/Cats Due Date:</td>
                                            <td>
                                                <asp:TextBox ID="txtArticleCDdate" runat="server" CssClass="TxtBox"
                                                    Width="100px" TabIndex="36" BackColor="#F1F1F1"></asp:TextBox>
                                                <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArticleCDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_cdudate" runat="server" tabindex="37" /></td>
                                            <td></td>

                                        </tr>
                                        <tr>
                                            <td>Pll No:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtArticlePllNo" runat="server" CssClass="TxtBox"></asp:TextBox>
                                                &nbsp; (Elsevier Only)</td>
                                            <td>Despatch:</td>
                                            <td>
                                                <asp:CheckBox ID="chkArticleDespatch" runat="server" TabIndex="38"
                                                    Enabled="False" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Doc Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                            <td class="style1">
                                                <asp:DropDownList ID="drpArticleDoctype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpArticleDoctype_SelectedIndexChanged" TabIndex="19">
                                                </asp:DropDownList></td>
                                            <td>Sales Group: </td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleSalesGroup" runat="server" TabIndex="38" DataValueField="sales_job_group_id" DataTextField="sales_group_name">
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Sub Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                            <td class="style1">
                                                <asp:DropDownList ID="drpArticleSubtype" runat="server" TabIndex="20"
                                                    OnSelectedIndexChanged="drpArticleSubtype_SelectedIndexChanged">
                                                </asp:DropDownList></td>
                                            <td>Interview Date &amp; time</td>
                                            <td>
                                                <asp:TextBox ID="txtinterviewdate" runat="server" Width="100px" CssClass="TxtBox"></asp:TextBox>
                                                &nbsp;Time:
                                            <asp:TextBox ID="txtinterviewtime" runat="server" Width="100px" CssClass="TxtBox"></asp:TextBox>
                                                <asp:Label ID="exlbl" runat="server" ForeColor="red" Text="(eg. 8.15-8.30)"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Article Category:
                                            </td>
                                            <td class="style1">
                                                <asp:DropDownList ID="drpArticleCategory" runat="server"
                                                    AutoPostBack="True"
                                                    TabIndex="19">
                                                </asp:DropDownList></td>
                                            <td>Phone No.</td>
                                            <td>
                                                <asp:TextBox ID="txtphoneno" runat="server" Width="120px" CssClass="TxtBox"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Issue No.</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtArticleIssueNo" runat="server" BackColor="#DFDFDF"
                                                    CssClass="TxtBox" MaxLength="300"
                                                    Width="80px" TabIndex="22" Enabled="False"></asp:TextBox>
                                                <span>&nbsp;Sequence:</span>
                                                <asp:TextBox ID="txtArticleSequence" runat="server" BackColor="#DFDFDF"
                                                    CssClass="TxtBox" MaxLength="300"
                                                    Width="80px" TabIndex="23" Enabled="False"></asp:TextBox></td>
                                            <td>Fax No.</td>
                                            <td>
                                                <asp:TextBox ID="txtfaxno" runat="server" Width="120px" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <caption>
                                                &nbsp;</caption>
                            </td>
                            <tr>
                                <td>No. of Images:</td>
                                <td class="style1" colspan="1">
                                    <asp:TextBox ID="txtArticleNoofImages" runat="server" BackColor="#dfdfdf"
                                        CssClass="TxtBox" MaxLength="300" TabIndex="24" Width="80px"
                                        Enabled="False"></asp:TextBox>
                                    &nbsp;Ms Rec. Date&nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtArtMsRecDate" runat="server" BackColor="#F1F1F1"
                                                    CssClass="TxtBox" TabIndex="30" Width="80px"></asp:TextBox>
                                    <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArtMsRecDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_stdate0"
                                        runat="server" tabindex="31" /></td>
                                <td>Meeting Place:</td>
                                <td>
                                    <asp:TextBox ID="txt_meetingplace" runat="server" Width="120px" CssClass="TxtBox"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Mns. Pages:</td>
                                <td class="style1" colspan="1">
                                    <asp:TextBox ID="txtArticleMspages" runat="server" CssClass="TxtBox"
                                        MaxLength="300" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                        TabIndex="25" Width="80px"></asp:TextBox>
                                    &nbsp;Ms Rev. Date&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtArtMsRevDate" runat="server" BackColor="#F1F1F1"
                                CssClass="TxtBox" TabIndex="30" Width="80px"></asp:TextBox>
                                    <img align="absmiddle" alt="Calendar" border="0"
                                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArtMsRevDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_stdate1"
                                        runat="server" tabindex="31" />
                                </td>
                                <td>Meeting Time:</td>
                                <td>
                                    <asp:TextBox ID="txt_meetingtime" runat="server" CssClass="TxtBox" Width="120px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Actual No. Pages:</td>
                                <td class="style1" colspan="1">
                                    <asp:TextBox ID="txtArticlePrintpages" runat="server" CssClass="TxtBox"
                                        MaxLength="300" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                        TabIndex="26" Width="80px"></asp:TextBox>
                                    &nbsp;Ms Acept. Date&nbsp;
                            <asp:TextBox ID="txtArtMsAcceptDate" runat="server" BackColor="#F1F1F1"
                                CssClass="TxtBox" TabIndex="30" Width="80px"></asp:TextBox>
                                    <img align="absmiddle" alt="Calendar" border="0"
                                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtArtMsAcceptDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgAD_stdate2"
                                        runat="server" tabindex="31" />
                                </td>
                                <td>Country:</td>
                                <td>
                                    <asp:TextBox ID="txt_country" runat="server" CssClass="TxtBox" Width="120px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 25px">Comments:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txtArticleComments" runat="server" CssClass="TxtBox"
                                        Height="50px" TabIndex="28" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                       <asp:LinkButton ID="lnkArticleHold" runat="server" OnClick="lnkArticleHold_Click"></asp:LinkButton>   
                                </td>
                                <td colspan="2" style="height: 25px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="95%">
                                        <tr>
                                            <td>Zone:</td>
                                            <td style="height: 21px">
                                                <asp:TextBox ID="txt_zone" runat="server" CssClass="TxtBox" Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Appointment Date:
                                        <br />
                                                Between</td>
                                            <td>
                                                <asp:TextBox ID="txt_appointmentdate1" runat="server" CssClass="TxtBox" Width="100px"></asp:TextBox>
                                                <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_appointmentdate1','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img16" runat="server" />
                                                &nbsp;and&nbsp;<asp:TextBox ID="txt_appointmentdate2" runat="server" CssClass="TxtBox" Width="100px"></asp:TextBox>
                                                <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_appointmentdate2','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img17" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="subheading" colspan="2">
                                                <strong>Completed Stage(s)</strong></td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="height: 25px">SAM Author Query:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txt_SamAuthorQuery" runat="server" CssClass="TxtBox"
                                        Height="50px" TabIndex="27" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                </td>
                                <td colspan="2" rowspan="2">
                                    <asp:DataGrid ID="dgrdArticleStages" runat="server" AutoGenerateColumns="False"
                                        CssClass="lightbackground">
                                        <AlternatingItemStyle CssClass="dullbackground" />
                                        <HeaderStyle CssClass="darkbackground" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Stage">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBD_StageName" runat="server"
                                                        Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Start Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBD_StartDate" runat="server"
                                                        Text='<%# Eval("received_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Due Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBD_DueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                              <asp:TemplateColumn HeaderText="Cats Due Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBD_ActualdueDate" runat="server"
                                                        Text='<%# Eval("ActualdueDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                          
                                            <asp:TemplateColumn HeaderText="Copyedit Due Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBD_ACatsDueDate" runat="server"
                                                        Text='<%# Eval("cats_due_date","{0:MM/dd/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                          
                                              <asp:TemplateColumn HeaderText="Desp. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBD_DespDate" runat="server"
                                                        Text='<%# Eval("despatch_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 25px">Figure Correction Query:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txt_FigureQuery" runat="server" CssClass="TxtBox"
                                        Height="50px" TabIndex="27" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table>
                                        <tr>
                                            <td>Priority Level:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpPriority" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>Artwork Pieces:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtArtwork" runat="server" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="style3">Article Type(SGML/XML):</td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleArtType" runat="server" TabIndex="20">
                                                    <asp:ListItem>X</asp:ListItem>
                                                    <asp:ListItem>S</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Current Status:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpCurrentstatus" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>Coloured Pieces:</td>
                                            <td>
                                                <asp:TextBox ID="txtColoured" runat="server" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="style3">&nbsp;</td>
                                            <td>
                                                <asp:CheckBox ID="chkArticleRegCopyedit" runat="server" Font-Bold="False"
                                                    TabIndex="17" Text="Regular Copyediting" />
                                                <asp:CheckBox ID="chkArticleadhocedit" runat="server" Font-Bold="False"
                                                    TabIndex="17" Text="Ad-hoc Copyediting" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Issue No. &amp; Seq.</td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleIssueNo" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>Print Pages:</td>
                                            <td>
                                                <asp:TextBox ID="txtActNoPages" runat="server" CssClass="TxtBox" Enabled="False"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="style3">Production Item Type:</td>
                                            <td>
                                                <asp:DropDownList ID="drpProdItemType" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Current Stage:</td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleCurrentStatge" runat="server"
                                                    AutoPostBack="True" TabIndex="19">
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>No. of Folios:</td>
                                            <td>
                                                <asp:TextBox ID="txtnoFolios" runat="server" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="style3">Production Type:</td>
                                            <td>
                                                <asp:DropDownList ID="drpProdType" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>DOI Number:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDOINo" runat="server" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>No.of Proofs:</td>
                                            <td>
                                                <asp:TextBox ID="txtnoofproofs" runat="server" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="style3">Page From:</td>
                                            <td>
                                                <asp:TextBox ID="txtPageFrom" runat="server" Width="60px" CssClass="TxtBox"></asp:TextBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Page To:
                                        <asp:TextBox ID="txtPageto" runat="server" Width="60px" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Withdraw:</td>
                                            <td>
                                                <asp:DropDownList ID="drpWithdraw" runat="server" Enabled="false">
                                                    <asp:ListItem Value="1">N</asp:ListItem>
                                                    <asp:ListItem Value="0">Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>Effect Type:</td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleEffectType" runat="server" AutoPostBack="True"
                                                    TabIndex="19" Enabled="false">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td class="style3">Copyright Type:</td>
                                            <td>
                                                <asp:DropDownList ID="drpArticleCopyright" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                    <asp:ListItem>001</asp:ListItem>
                                                    <asp:ListItem>002</asp:ListItem>
                                                    <asp:ListItem>003</asp:ListItem>
                                                    <asp:ListItem>004</asp:ListItem>
                                                    <asp:ListItem>005</asp:ListItem>
                                                    <asp:ListItem>006</asp:ListItem>
                                                    <asp:ListItem>007</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Open Access</td>
                                            <td>
                                                <asp:DropDownList ID="drpOpenAccess" runat="server">
                                                    <asp:ListItem Value="0" Selected="True" >N</asp:ListItem>
                                                    <asp:ListItem Value="1">Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td>Journal DTD</td>
                                            <td>
                                                <asp:TextBox ID="txtjournalDTD" runat="server" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="style3">Number SYS</td>
                                            <td>
                                                <asp:DropDownList ID="drpNumSys" runat="server" AutoPostBack="True"
                                                    TabIndex="19">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Research Summary Pages</td>
                                            <td>
                                                <asp:TextBox ID="txtResearchPage" runat="server" Width="60px" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>Art Reprocess:</td>
                                            <td>
                                                <asp:TextBox ID="txtArtReprocess" runat="server" Width="60px" CssClass="TxtBox"></asp:TextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="style3">Auto Process :</td>
                                            <td>
                                                <asp:DropDownList ID="drpAutoprocess" runat="server">
                                                    <asp:ListItem Selected="True" Value="0">N</asp:ListItem>
                                                    <asp:ListItem Value="1">Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divPopArtOnHold" class="ModalPopup">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="left" style="background-color: green; color: White; font-weight: bold; width: 163px;">&nbsp;Article On Hold
                                                </td>
                                                <td align="right"
                                                    style="background-color: green; color: White; font-weight: bold">
                                                    <a href="#" onclick="javascript:closeModalArtHold();" style="color: White;"
                                                        title="Close">[x]</a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 163px">&nbsp;OnHold Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                <td>
                                                    <asp:DropDownList ID="drpArticleOnHoldType" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 163px">&nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="txtArticleOnHoldReason" runat="server" CssClass="TxtBox"
                                                        MaxLength="300" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr bgcolor="Honeydew">
                                                <td align="center" colspan="2">
                                                    <a class="link1" href="#" onclick="javascript:validSaveItem();"><strong>Submit</strong></a>
                                                    &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtHold();"><strong>Cancel</strong></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>

                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp;
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
                                            <asp:DropDownList ID="drpGFigureType" runat="server"></asp:DropDownList></span>
                                <strong>&nbsp; Graphic Type:</strong><span style="font-size: 9pt; color: #ff0000">*</span>
                                &nbsp;<asp:DropDownList ID="drpGraphicType" runat="server">
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
                                            No records found.
                                        </div>
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
                                                    <asp:TemplateField HeaderText="SAM Author Query">
                                                        <ItemTemplate>
                                                            <%# Eval("sam_author_query")%>
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

                <div class="content" id="tabHoldHistory" runat="server">
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td class="dpJobGreenHeader" style="height: 32px">
                                <img id="Img18" align="absmiddle" src="images/tools/comment.png" runat="server" />
                                <asp:Label ID="lblHoldHistoryHeader" runat="server" Text="Hold History"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="div2" style="display: block;">
                                    <asp:GridView ID="gvHoldHistory" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%# Eval("NAME")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold Details">
                                                <ItemTemplate>
                                                    <%# Eval("details")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date">
                                                <ItemTemplate>
                                                    <%# Eval("start_date")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date">
                                                <ItemTemplate>
                                                    <%# Eval("end_date")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hold by">
                                                <ItemTemplate>
                                                    <%# Eval("created_by")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                No records found.
                                            </div>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="darkbackground" />
                                        <AlternatingRowStyle CssClass="dullbackground" />

                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="content" id="tabArtWork" runat="server">
                    <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td class="dpJobGreenHeader" style="height: 32px" colspan="7">
                                <img id="Img13" align="absmiddle" src="images/tools/comment.png" runat="server" />
                                <asp:Label ID="lblArtWorkHeader" runat="server" Text="ArtWork"></asp:Label></td>
                            <td class="dpJobGreenHeader">
                                <asp:ImageButton ID="cmdArt_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                                    ToolTip="Save" OnClick="cmdArt_Save_Click1" /></td>
                        </tr>
                        <tr>
                            <td>Art Figure Type</td>
                            <td>Art File Name</td>
                            <td>Images Type</td>
                            <td>Received</td>
                            <td>Date Received</td>
                            <td>Department</td>
                            <td>Employee</td>
                            <td>Redraw?</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="dropArtFigType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropArtFigType_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtArtFileName" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="dropImgType" runat="server">
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="dropReceived" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="Y">Y</asp:ListItem>
                                    <asp:ListItem Value="N">N</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtDateReceived" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtArtEmployee" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="dropRedraw" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="Y">Y</asp:ListItem>
                                    <asp:ListItem Value="N">N</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:GridView ID="gvArtWork" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Art Figure Type">
                                            <ItemTemplate>
                                                <%# Eval("AFTNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Art File Name">
                                            <ItemTemplate>
                                                <%# Eval("ARTNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Images Type">
                                            <ItemTemplate>
                                                <%# Eval("AITNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <%# Eval("ARTRECEIVED")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Received">
                                            <ItemTemplate>
                                                <%# Eval("ARTDATERECEIVED")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <%# Eval("DNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee">
                                            <ItemTemplate>
                                                <%# Eval("EMPNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Redraw?">
                                            <ItemTemplate>
                                                <%# Eval("Redraw")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                            No records found.
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="darkbackground" />
                                    <AlternatingRowStyle CssClass="dullbackground" />

                                </asp:GridView>

                            </td>
                        </tr>
                    </table>
                </div>

                <div class="content" id="tabInvoiceSetup" runat="server" win>



                                    <table id="tblInvoiceSetup" border="0"   cellpadding="2" cellspacing="0">
                                        <tr bgcolor="#f0fff0">
                                            <td colspan="4" class="dpJobGreenHeader">
                                                <img id="imgArticleHeader0" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                    ID="lblArticleHeader0" runat="server" Text="Allenpress Invoice setup"></asp:Label>
                                                <span align="right">
                                                <asp:ImageButton ID="cmd_Invoice_Setup" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                    ToolTip="Save Article" OnClick="cmd_Invoice_Setup_Click" TabIndex="41" style="margin-left: 226px" />
                                                    </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">Pre process</td>
                                            <td class="auto-style3">:</td>
                                            <td class="style1">
                                                <asp:DropDownList ID="drpPreprocess" runat="server">
                                                    <asp:ListItem Value="0" Selected="True" >N</asp:ListItem>
                                                    <asp:ListItem Value="1">Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">Fisrt Proof</td>
                                            <td class="auto-style3">:</td>
                                            <td>
                                                <asp:DropDownList ID="drpFirstProof" runat="server">
                                                    <asp:ListItem Value="0" Selected="True" >DP</asp:ListItem>
                                                    <asp:ListItem Value="1">CW</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style4">&nbsp;</td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style1">Invoice Page No.</td>
                                            <td class="auto-style3">:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtInvoicePageCont" runat="server" CssClass="TxtBox" MaxLength="3" Width="59px" TabIndex="14"></asp:TextBox></td>
                                            <td class="auto-style4">&nbsp;</td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style1">PO Number</td>
                                            <td class="auto-style3">:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtPOnumber" runat="server" CssClass="TxtBox" MaxLength="300" Width="80px" TabIndex="15"></asp:TextBox></td>
                                            <td class="auto-style4">&nbsp;</td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style1">CO Number</td>
                                            <td class="auto-style3">:</td>
                                            <td class="style1">
                                                <asp:TextBox ID="txtCONumber" runat="server" CssClass="TxtBox" MaxLength="300" Width="80px" onkeypress="javascript: return OnlyAllowNumbers(this,event);" TabIndex="16"></asp:TextBox>
                                            </td>
                                            <td class="auto-style4"></td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style1">&nbsp;</td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td class="style1">
                                                &nbsp;</td>
                                            <td class="auto-style4">&nbsp;</td>
                                        </tr>
                                            </td>
                                                        

                        </tr>
                    </table>
                </div>
          

        <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkArticledetails','');">Show Details</div>
    </form>
</body>
</html>
