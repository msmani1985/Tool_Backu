<%@ Page maintainScrollPositionOnPostBack="true" Language="C#" AutoEventWireup="true" CodeFile="LaunchPage.aspx.cs" Inherits="LaunchPage" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Launch Form</title>
    <%--<script type="text/javascript" src="scripts/tabs1.js"></script>--%>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="scripts/common.js"></script>
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="datetimepicker.js">
    </script>
 <script type="text/javascript">
$(document).ready(function() {
$('#<%=txtdueFromdate.ClientID%>').attr('readonly', true);
$('#<%=txtdueTodate.ClientID%>').attr('readonly', true);
$('#<%=txtsource.ClientID%>').attr('readonly', true);
$('#<%=txttarget.ClientID%>').attr('readonly', true);
$('#<%=txttarget.ClientID%>').attr('readonly', true);
})
 function openAdvancedModal(){        
            document.getElementById ('divPopAdvancedSearch').style.visibility='visible';
            document.getElementById ('divPopAdvancedSearch').style.display='';       
            document.getElementById ('divPopAdvancedSearch').style.top= '500px';
            document.getElementById ('divPopAdvancedSearch').style.left='128px'; 
           
        }
        function closeAdvancedModal(){
            document.getElementById ('divMasked').style.display='none';
            document.getElementById ('divPopAdvancedSearch').style.display='none';
            document.getElementById ('iframetop').style.display='none';
        }

        function flashtext(ele, col) {
            var tmpColCheck = document.getElementById(ele).style.color;

            if (tmpColCheck === 'Red') {
                document.getElementById(ele).style.color = 'Blue';
            } else if (tmpColCheck === 'Blue') {
                document.getElementById(ele).style.color = col;
            } else {
                document.getElementById(ele).style.color = 'Red';
            }
        }

        setInterval(function () {
            flashtext('lnkSWDetails', 'green');
        }, 500);
</script>
<script type="text/javascript">
       
</script>

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
      document.form1.hfP_ID.value=val;
      document.form1.hfP_Name.value=val1
      if(document.getElementById("lblProjectSummary"))
        document.getElementById("lblProjectSummary").innerText="Project : "+val1;
      else if(document.all.lblProjectSummary)
        document.all.lblProjectSummary.innerText="Project : "+val1;
      else if(document.form1.lblProjectSummary)
        document.form1.lblProjectSummary.innerText="Project : "+val1;
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
    function openModal(){ 
         document.getElementById ('divPopModule').style.visibility='visible';
        document.getElementById ('divPopModule').style.display=''; document.getElementById ('divPopModule').style.bottom= '150px';
        document.getElementById ('divPopModule').style.left='280px'; 
        }
    function imgBD_editor_onclick() {
 
        if(document.form1.drpProjectcustomer!=null && document.form1.drpProjectcustomer.value !="0")
            window.open("Launchcontacts.aspx?form=Projects&type=0&trgname=txtProjectEditor&trgid=hfprojectEditorId&cid="+document.form1.drpProjectcustomer.value+"&lid="+document.form1.DropLocation.value,"Contacts","width=800,height=600,status=yes, scrollbars=yes");
        else alert("Select a customer"); 
    }
    function NewimgBD_editor_onclick() {

        if (document.form1.drpNewProjectcustomer != null && document.form1.drpNewProjectcustomer.value != "0")
            window.open("NonLaunchcontacts.aspx?form=Projects&type=0&trgname=txtNewProjectEditor&trgid=hfprojectEditorId&cid=" + document.form1.drpNewProjectcustomer.value + "&lid=" + document.form1.DropNewLocation.value, "Contacts", "width=800,height=600,status=yes, scrollbars=yes");
        else alert("Select a customer");
    }
     function imgReason_editor_onclick() {
            window.open("LaunchComplexReason.aspx?form=Projects&type=0&trgname=Dropcomplex","Contacts","width=600,height=300,status=yes, scrollbars=yes");
            Dropcomplex_SelectedIndexChanged(sender,e);
   }
       function ValidationTxt()
        {
            if(document.form1.dropnoFTP.value==0)
            {
                alert("Please give Project Name");
                document.form1.fname.focus();
                return false;
            }
            return true;
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
        .auto-style1 {
            width: 163px;
        }
        .auto-style2 {
            width: 550px;
        }
        .auto-style3 {
            width: 452px;
        }
        .auto-style4 {
            width: 289px;
        }
        .auto-style5 {
            width: 315px;
        }
        .auto-style6 {
            width: 511px;
        }
        .auto-style7 {
            height: 32px;
            width: 1075px;
        }
        .auto-style8 {
            width: 1075px;
        }
        .auto-style9 {
            height: 18px;
        }
        .auto-style11 {
            width: 67px;
        }
        .auto-style12 {
            height: 69px;
        }
    </style>
    <style type="text/css">
        .gridP
        {
        background:Orange;
        font-weight:bold;
        color:White;
        }
        .gridC
        {
        background:Gray;
        font-weight:bold;
        color:White;
        }
        .gridDel
        {
        background:Green;
        font-weight:bold;
        color:White;
        }
        .gridWIP
        {
        background:LightGreen;
        font-weight:bold;
        color:White;
        }
    </style>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=GvProject.ClientID %>').Scrollable({
        ScrollHeight: 350
    });
});
</script>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>
        <%--<iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>--%>
        <div>
            <table width="100%">
                <tr>
                    <td style="background-image: url(images/green-noise-background.png)" >
                        <div>
                            <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3" style="background-image: url(images/green-noise-background.png)">
                                        <img alt="" src="images/tools/search.png" />&nbsp;<strong>Search Project</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px;Height:30px">
                                        <strong>Customer</strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="325px" TabIndex = "1">
                                        </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex = "4" CssClass="dpbutton" OnClick="btnSearch_Click" />
                                   &nbsp;&nbsp;
                                   <asp:Button ID="btnNew" runat="server" Text="New" TabIndex = "4" CssClass="dpbutton" OnClick="cmd_New_Project_Click"  />
                                    </td>
                                </tr>
                                <tr>
                                <td></td>
                                <td >
           <strong> Month</strong>&nbsp;
                    <asp:DropDownList ID="DDMonthList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDMonthList_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">---All---</asp:ListItem>--%>
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
                &nbsp;&nbsp;<strong>Year</strong>&nbsp;
                    <asp:DropDownList ID="DDYearList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDYearList_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0">--All--</asp:ListItem>--%>
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                    </asp:DropDownList>
                                        <asp:HiddenField ID="hfP_ID" runat="server" />
                                        <asp:HiddenField ID="hfP_Name" runat="server" />
                </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td >
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral"  runat="server" TabIndex = "1" OnClick="lnkGeneral_Click" Text="General" /></li>
                            <li id="miNewJobInfo" runat="server">
                                <asp:LinkButton ID="lnkNewJobInfo" TabIndex = "2" runat="server" Text="Job Info" OnClick="lnkNewJobInfo_Click" /></li>
                            <li id="miNewFileInfo" runat="server">
                                <asp:LinkButton ID="lnkNewFileInfo"   TabIndex = "3" runat="server" Text="File Info" OnClick="lnkNewFileInfo_Click" /></li>
                            <li id="miNewCostDetails" runat="server">
                                <asp:LinkButton ID="lnkNewCostInfo"  TabIndex = "5" runat="server"  Text="Quote Info" OnClick="lnkNewCostInfo_Click" /></li>
                            <li id="miNewReportDetails" runat="server">
                                <asp:LinkButton ID="lnkNewReportInfo"  TabIndex = "6" runat="server"  Text="Preview" OnClick="lnkNewReportInfo_Click"/></li>
                            <li id="miJobTracking" runat="server">
                                <asp:LinkButton ID="lnkJobTracking" runat="server" TabIndex="4" OnClick="lnkJobTracking_Click">Job Tracking</asp:LinkButton></li>
                            <li id="miLaunchDetails" runat="server" visible="false">
                                <asp:LinkButton ID="lnkLaunchDetails" TabIndex = "7" runat="server" OnClick="lnkLaunchDetails_Click" Text="Job Info" /></li>
                            <li id="miFileInfo" runat="server" visible="false">
                                <asp:LinkButton ID="lnkFileInfo"   TabIndex = "8" runat="server" OnClick="lnkFileInfo_Click" Text="File Info" /></li>
                            <li id="miCostDetails" runat="server" visible="false">
                                <asp:LinkButton ID="lnkCostInfo"  TabIndex = "9" runat="server"  Text="Quote Info" OnClick="lnkCostInfo_Click" /></li>
                            <li id="miReportDetails" runat="server"  visible="false">
                                <asp:LinkButton ID="lnkReportInfo"  TabIndex = "10" runat="server"  Text="Preview" OnClick="lnkReportInfo_Click"/></li>
                            <li id="miLoggedEvent" runat="server">
                                <asp:LinkButton ID="lnkLoggedEvent" runat="server" TabIndex="11" OnClick="lnkLoggedEvent_Click">Logged Events</asp:LinkButton></li>
                            <li id="miAddQuote" runat="server">
                                <asp:LinkButton ID="lnkAddQuote" runat="server" TabIndex="12" OnClick="lnkAddQuote_Click">Additional Quote</asp:LinkButton></li>
                            <li id="miFinalQuote" runat="server">
                                <asp:LinkButton ID="lnkFinalQuote" runat="server" TabIndex="13" OnClick="lnkFinalQuote_Click">Final Quote</asp:LinkButton></li>
                        </ol>
                        </td>
                    </tr>
                <tr>
                    <td>
                        <div class="content" id="tabGeneral" runat="server">
                            <table border="0" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td style="background-image: url('images/green-noise-background.png');">
                                        <img id="Img8" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblProjectSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                        <td align="right" style="padding-right:10px; background-image: url('images/green-noise-background.png'); " class="auto-style6">
                                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="imgbtnSave"  ToolTip="Save" OnClick="imgbtnSave_Click" />
                                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
                                        <img  alt="Location" border="0" onclick="javascript:calendar_window=window.open('LP_ReWO.aspx','calendar_window','width=750,height=250,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/reprocess.png" style="cursor: pointer; width: 30px; height: 29px;" id="img20"  runat="server" />
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                       <table>
                                       <tr>
                                       <td align="left">
                                        <asp:GridView ID="GvProject" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground" width="100%" OnRowDataBound="GridView1_RowDataBound"  Visible="false" >
                                            <HeaderStyle CssClass="darkbackground"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                 <asp:TemplateField SortExpression="parent_job_id" HeaderText="Sl.No" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblJOB" runat="server" Text='<%# id++ %>' ></asp:Label>
                                                               <br />
                                                        <asp:HiddenField ID="hfgvProjectID" runat="server" Value='<%# Eval("pro_id") %>' />
                                                        <asp:HiddenField ID="hfgvProjectname" runat="server" Value='<%# Eval("projectname") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("cur_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField SortExpression="due_date" HeaderText="Due Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_Timefrom" HeaderText="Due Time From" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeFrom" runat="server" Text='<%# Eval("due_Timefrom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_Timeto" HeaderText="Due Time To" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeTo" runat="server" Text='<%# Eval("due_Timeto") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField SortExpression="Software" HeaderText="Software" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPSoftware" runat="server" Text='<%# Eval("Software") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                               <asp:TemplateField SortExpression="Pages" HeaderText="No. of Pages" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPPagecount" runat="server" Text='<%# Eval("Pages_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center" >
                                                     <ItemTemplate>
                                                         <asp:DropDownList ID="DropStatus" runat="server">
                                                         </asp:DropDownList>
                                                     </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                                     <ItemTemplate>
                                                         <asp:CheckBox ID="chkBoxStatus" runat="server" />
                                                     </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" ClientIDMode="Static">
                                        <ContentTemplate>
                                           <asp:GridView ID="GvNL" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                                 CssClass="lightbackground" Width="1100px" OnRowDataBound="GvNL_RowDataBound">
                                            <HeaderStyle CssClass="darkbackground"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                 <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                               <br />
                                                        <asp:HiddenField ID="hfgvNLID" runat="server" Value='<%# Eval("LP_ID") %>' />
                                                        <asp:HiddenField ID="hfgvProjectname" runat="server" Value='<%# Eval("projectname") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("CREATED_DATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField SortExpression="due_date" HeaderText="Due Date From" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueDate" runat="server" Text='<%# Eval("due_datefrom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_dateto" HeaderText="Due Date To" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueDateTo" runat="server" Text='<%# Eval("due_dateTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="due_Timefrom" HeaderText="Due Time (IST) From" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeFrom" runat="server" Text='<%# Eval("DueTimeFrom") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_Timeto" HeaderText="Due Time (IST) To" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueTimeTo" runat="server" Text='<%# Eval("DueTimeTo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="File" HeaderText="No. of Files" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvFilecount" runat="server" Text='<%# Eval("File_Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="File" HeaderText="No. of Pages" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPagecount" runat="server" Text='<%# Eval("PAGES_COUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                     <ItemTemplate>
                                                         <asp:DropDownList ID="DropStatus" runat="server">
                                                         </asp:DropDownList>
                                                     </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                                                     <ItemTemplate>
                                                         <asp:CheckBox ID="chkBoxStatus" runat="server" />
                                                     </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WO / PO Number">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblJobNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Jobno") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Follow Up">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkFU" runat="server" 
                                                            Text ="Click"  OnClick = "OnClickFU"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="TATs (DD:HH)">
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblgvTATs" runat="server" Text='<%# Eval("TATs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField Visible="false">
                                                   <ItemTemplate>
                                                        <asp:ImageButton ID="BtnSave" AlternateText="Save" ToolTip="Save" ImageUrl="~/images/tools/yes.png" runat="server" 
                                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' CommandName="Save"/>
                                                    </ItemTemplate>
                                                 </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Panel ID="pnlFU1" Width="400" Height="300" runat="server" CssClass="modalPopup" style = "display:none">
                                            <table align="right">
                                                <tr >
                                                    <td>
                                                        <asp:ImageButton ImageUrl="images/tools/no.png" runat="server" ID="ImageButton3"  ToolTip="Save" OnClick="imgbtnFUclose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="Label27" runat="server" Text="Follow Up Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                            <table align = "center">
                                                <tr >
                                                    <td>
                                                        FollowUp Date : 
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtFUDate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                         <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtFUDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="img19"  runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Remarks :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFURemarks" runat="server" CssClass="TxtBox" TextMode="MultiLine" Width="250px" Height="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Button ID="btnFUSave" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Save" OnClick="btnFUSave_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:GridView runat="server" ID="gvFU">
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:LinkButton ID="lnkFU1" runat="server"></asp:LinkButton>
                                        <asp:ModalPopupExtender ID="FUPopUp" runat="server" DropShadow="false"
                                            PopupControlID="pnlFU1" TargetControlID = "lnkFU1"
                                            BackgroundCssClass="modalBackground">
                                        </asp:ModalPopupExtender>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID = "GvNL" />
                                    </Triggers> 
                                </asp:UpdatePanel>
                                       </td>
                                       </tr>
                                       </table>
                                        <%--<link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
                                        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
                                        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
                                        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                gridviewScroll();
                                            });

                                            function gridviewScroll() {
                                                $('#<%=GvNL.ClientID%>').gridviewScroll({
                                                    width: window.innerWidth - 50,
                                                    height: window.innerHeight - 150,
                                                    startHorizontal: 0,
                                                    barhovercolor: "#848484",
                                                    barcolor: "#848484"
                                                });
                                            }
                                        </script>--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabNewLaunch" runat="server">
                             <table id="Table5" border="0" cellpadding="2" cellspacing="0">
                                        <tr bgcolor="#f0fff0">
                                        <td class="dpJobGreenHeader" colspan="4">
                                            <img id="imgNLHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblNLHeader" runat="server" Text="Label">New Launch</asp:Label></td>
                                    </tr>
                                    <tr>
                                     <td>
                                         Project Name:
                                     </td>
                                     <td>
                                         <asp:DropDownList ID="dropProjectName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropProjectName_SelectedIndexChanged">
                                         </asp:DropDownList>
                                         <asp:Label ID="lblInitalID" runat="server" Visible="false"></asp:Label>
                                     </td>
                                 </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="Label6" runat="server" Text="Project Title:"></asp:Label>
                                            <span style="font-size: 9pt; color: #ff0000">*</span>
                                        </td>
                                        <td class="auto-style3" >
                                            <asp:TextBox ID="txtNewProjectTitle" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label7" runat="server" Text="Job ID:"></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtNewJobid" Enabled="false" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">Customer:<span  style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td class="auto-style3" >
                                            <asp:DropDownList ID="drpNewProjectcustomer" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drpNewProjectcustomer_SelectedIndexChanged">
                                            </asp:DropDownList>           
                                        </td>
                                        <td class="auto-style4">Location:</td>
                                        <td class="auto-style2">        
                                            <asp:DropDownList ID="DropNewLocation" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DropNewLocation_SelectedIndexChanged"></asp:DropDownList>
                                            <img  alt="Location" border="0" height="20" onclick="javascript:calendar_window=window.open('NonLaunchLocation.aspx?formname=txtNewProjectEditor','calendar_window','width=750,height=250,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/tools/new.png" style="cursor: pointer;" id="img5"  runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            Project Editor:
                                        </td>
                                        <td class="auto-style3" >
                                            <asp:TextBox ID="txtNewProjectEditor" runat="server" CssClass="TxtBox" Width="180px"></asp:TextBox>
                                            <img id="NewimgBD_editor" src="images/tools/user_go.png" language="javascript" runat="server"
                                                onclick="return NewimgBD_editor_onclick()" style="cursor: pointer" title="Select PE"/>
                                            <asp:HiddenField ID="hfprojectEditorId" runat="server"  />
                                            </td>
                                        <td class="auto-style4">
                                            <asp:Label ID="TarRecDate" runat="server" Text="Target Rec. Date:"></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtNewRecDate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtNewRecDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img9"  runat="server" />
                                        </td>
                                    </tr>
                                 <tr>
                                     <td>
                                         WO / PO Number:
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtWOnumber" runat="server"></asp:TextBox>
                                     </td>
                                 </tr>
                                <tr>
                                    <td class="auto-style5">
                                        Due Date:
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblNewDueFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtNewdueFromdate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtNewdueFromdate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img6"  runat="server" />
                                        <asp:Label ID="lblNewDueTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtNewdueTodate" runat="server" CssClass="TxtBox" Visible="false" Width="80px"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtNewdueTodate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="calenderNewTo"  runat="server" visible="false" />
                                        <asp:CheckBox ID="chkNewYTC" Text="YTC" runat="server" OnCheckedChanged="chkNewYTC_CheckedChanged" />
                                        <asp:CheckBox ID="chkNewDueDate" Text="Staggered Delivery" runat="server" AutoPostBack="True" OnCheckedChanged="chkNewDueDate_CheckedChanged" ClientIDMode="AutoID" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblTATs" runat="server" Text="TATs:" ></asp:Label>
                                       <%-- <asp:TextBox ID="txtTATs" runat="server" Visible="False" Width="69px"></asp:TextBox>
                                        <a href="javascript:NewCal('txtTATs','MMddyyyy',true,24)"><img src="images/cal.gif" width="16" height="16" border="0" alt="Pick a date"></a>
                                        --%>
                                        <asp:DropDownList ID="dropTATsDays" runat="server" Width="40px" >
                                            <asp:ListItem Value="00">00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem></asp:DropDownList>
                                        <asp:DropDownList ID="dropTATsHrs" runat="server" Width="40px" >
                                            <asp:ListItem Value="00">00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
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
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem> </asp:DropDownList>
                                         <asp:Label runat="server" id="Label28" style="font-size: 7pt; color: #ff0000">Please fill this Format (DD:HH)</asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" >Due Time:</td>
                                    <td class="auto-style3" colspan="3">
                                        <asp:Label ID="lblNewFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="DropNewDueTimeFrom" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropNewDueTimeFrom_SelectedIndexChanged" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
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
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                        </asp:DropDownList><asp:DropDownList ID="DropNewDueMinFrom" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropNewDueMinFrom_SelectedIndexChanged" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="DropNewDueTimeZoneFrom" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropNewDueTimeZoneFrom_SelectedIndexChanged"  >
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="IST">IST</asp:ListItem>
                                            <asp:ListItem Value="PST">PST</asp:ListItem>
                                            <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                            <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                            <asp:ListItem Value="CET">CET</asp:ListItem>
                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                            <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                            <asp:ListItem Value="EST">EST</asp:ListItem>
                                            <asp:ListItem Value="JST">JST</asp:ListItem>
                                            <asp:ListItem Value="BST">BST</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblNewTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="DropNewDueTimeTo" runat="server" Width="40px" AutoPostBack="True"  Visible="False" OnSelectedIndexChanged="DropNewDueTimeTo_SelectedIndexChanged" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
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
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                        </asp:DropDownList><asp:DropDownList ID="DropNewDueMinTo" runat="server" Width="40px" AutoPostBack="True"  Visible="False" OnSelectedIndexChanged="DropNewDueMinTo_SelectedIndexChanged" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="DropNewDueTimeZoneTo" runat="server" Width="50px" AutoPostBack="True"  Visible="False" OnSelectedIndexChanged="DropNewDueTimeZoneTo_SelectedIndexChanged" >
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="IST">IST</asp:ListItem>
                                            <asp:ListItem Value="PST">PST</asp:ListItem>
                                            <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                            <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                            <asp:ListItem Value="CET">CET</asp:ListItem>
                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                            <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                            <asp:ListItem Value="EST">EST</asp:ListItem>
                                            <asp:ListItem Value="JST">JST</asp:ListItem>
                                            <asp:ListItem Value="BST">BST</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="chkNewDueTime" Text="Staggered Delivery" runat="server" AutoPostBack="True" Width="133px" OnCheckedChanged="chkNewDueTime_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;IST Time :&nbsp;&nbsp;
                                        <asp:Label ID="lblNewIndFrom" runat="server" Text="(From)" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtNewIndFrom" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblNewIndTo" runat="server" Text="(To)" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtNewIndTo" runat="server" Width="80px" Visible="False" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">
                                        Task:
                                    </td>
                                    <td class="auto-style3" >
                                        <asp:ListBox ID="lboxNewtask" Width="130px" runat="server" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lboxNewtask_SelectedIndexChanged" ></asp:ListBox>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblNewformat" runat="server" Text="Format:"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:ListBox ID="lboxNewformat" Width="130px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" >Input Files Received:</td>
                                    <td class="auto-style3" >
                                        <asp:ListBox ID="lboxNewInputFile" runat="server" SelectionMode="Multiple" Width="130px">
                                            <asp:ListItem Value="FTP">FTP</asp:ListItem>
                                            <asp:ListItem Value="Mail Attachment">Mail Attachment</asp:ListItem>
                                            <asp:ListItem Value="Skype">Skype</asp:ListItem>
                                            <asp:ListItem Value="DropBox">DropBox</asp:ListItem>
                                        </asp:ListBox>&nbsp;
                                    </td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblNewsource" runat="server" Text="Source Type:"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="DropNewSource" runat="server">
                                                    <asp:ListItem ></asp:ListItem>
                                                    <asp:ListItem>Editable</asp:ListItem>
                                                    <asp:ListItem>Scanned</asp:ListItem>
                                                    <asp:ListItem Value="Editable and Scanned">Editable and Scanned </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                
                                    <td class="auto-style4">
                                        <asp:Label ID="tempID" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="auto-style5" colspan="2">
                                        <asp:Label ID="lblNewtarLang" runat="server" Text="Languages & Software Details:"  Width="120px"></asp:Label>
                                    </td>
                                    <td class="auto-style3" >
                                        
                                    </td>
                                    <td align="center" class="auto-style4">
                                        <table>
                                            <tr>
                                                <td>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style2">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" >

                                    </td>
                                </tr>
                                 <tr>
                                     <td colspan="4" align="center" class="auto-style12">
                                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                             <ContentTemplate>
                                                 <table cellpadding="3" cellspacing="2" border="1" align="center">
                                                     <tr align="center">
                                                         <td>File Type</td>
                                                         <td align="center">Languages</td>
                                                         <td>TaskName</td>
                                                         <td>Software</td>
                                                         <td>Version</td>
                                                         
                                                         <td></td>
                                                         <td></td>
                                                     </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBoxList ID="chkFileType" runat="server" Width="70px" Height="16px">
                                                              <%--  <asp:ListItem Selected="True" Value="0">Source</asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="1">Target</asp:ListItem>--%>
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td>
                                                            <div style="width:180px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList  Width="160px" ID="lboxNewlang" SelectionMode="Multiple" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <%--<div style="width:100px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">--%>
                                                            <asp:CheckBoxList ID="CheckBoxNewTask" runat="server" Width="70px" Height="16px"></asp:CheckBoxList>
                                                            <%--</div>--%>
                                                        </td>
                                                        <td>
                                                            <div style="width:140px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList  Width="120px" ID="lboxSW" AutoPostBack="true" SelectionMode="Multiple" runat="server" OnSelectedIndexChanged="lboxSW_SelectedIndexChanged"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div style="width:120px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList  Width="100px" ID="lboxSWVer" SelectionMode="Multiple" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                        
                                                        <td class="auto-style11">
                                                            <asp:Button CssClass="dpbutton" ID="btnNewlangadd" runat="server" Text="Add" OnClick="btnNewlangadd_Click" />
                                                            &nbsp;
                                                            <asp:Button CssClass="dpbutton" ID="btnNewlangdel" runat="server" Text="Remove" OnClick="btnNewlangdel_Click" />
                                                            <asp:Button ID="xlang" TabIndex = "4" CssClass="dpbutton" runat="server" Text="" 
                                                                Visible="true" style="display:none;"  OnClick="xlang_Click"/>
                                                        </td>
                                                        <td>
                                                            <div style="width:180px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList id="lboxNewlangused" runat="server" Width="160px"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                             </ContentTemplate>
                                         </asp:UpdatePanel>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td class="auto-style5" >

                                    </td>
                                     <td>
                                         <asp:LinkButton ID="lnkSWDetails" runat="server" OnClick="lnkSWDetails_Click1">Software Details : Source & Target</asp:LinkButton>
                                     </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5" >
                                        <asp:Label ID="lblSWVerUsed" runat="server" Text="Software &amp; Version to be Used:"  Width="120px" Visible="false"></asp:Label>
                                    </td>
                                    <td class="auto-style1" colspan="3" >

                                        <asp:GridView ID="gv_SoftNew"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                        CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_SoftNew_RowDataBound" Width="250px" Height="141px" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Task Name" >
                                                        <ItemTemplate>
                                                            <asp:Label Width="60" Enabled="false" ID="txt_task"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaskName") %>'></asp:Label>
                                                            <asp:HiddenField ID="hf_taskID" runat="server" 
                                                                    Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Language">
                                                        <ItemTemplate>
                                                            <asp:Label Width="60" Enabled="false" ID="txt_Lang"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Lang_Name") %>'></asp:Label>
                                                            <asp:HiddenField ID="hf_LangID" runat="server" 
                                                                    Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTargetDate" runat="server"  Width="70" Text='<%#Eval("Target_Date") %>'></asp:TextBox>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Software">
                                                        <ItemTemplate>
                                                            <asp:ListBox  ID="lboxSoft" AutoPostBack="true"  SelectionMode="Multiple"  OnSelectedIndexChanged="lboxNewSoft_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Version">
                                                        <ItemTemplate>
                                                            <asp:ListBox  ID="lboxVer" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                            <HeaderStyle CssClass="darkbackground" />
                                        </asp:GridView>
                                    </td>
                                
                                </tr>
                                <tr>
                                    <td class="auto-style5" > Platform:</td>
                                    <td class="auto-style3" >
                                        <asp:DropDownList ID="dropNewSwPlat" runat="server" >
                                                <asp:ListItem Value="1">MAC</asp:ListItem>
                                                <asp:ListItem Value="2">PC</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>No. of Folders/Packages in FTP:</td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="dropNewNoofFolder" runat="server" Width="110px">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
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
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Package/Folder Size:</td>
                                    <td>
                                        <asp:TextBox ID="txtNewPackageSize"  runat="server" Width="69px"></asp:TextBox>
                                        <asp:DropDownList ID="dropNewPackageSize" runat="server">
                                            <asp:ListItem Value="1">Bytes</asp:ListItem>
                                            <asp:ListItem Value="2">KB</asp:ListItem>
                                            <asp:ListItem Value="3">MB</asp:ListItem>
                                            <asp:ListItem Value="4">GB</asp:ListItem>
                                            <asp:ListItem Value="5">TB</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>File Server Location:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="txtNewFileLocation" runat="server" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="2" style="height: 38px">
                                        Does the downloaded input package/folder/file size match the package/folder/file
                                        size in FTP?</td>
                                          
                                    <td>
                                        <asp:DropDownList ID="dropNewDownPackage" runat="server" Width="57px">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                    </td> 
                                    </tr>
                                <%-- <tr>
                                    <td >
                                        File Count:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFile" runat="server" AutoPostBack="True" OnTextChanged="txtFile_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="auto-style7">
                                        Page Count:
                                    </td>
                                    <td>
                                        <asp:GridView ID="gv_FilePages"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                        CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_FilePages_RowDataBound" Width="250px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="File No." >
                                                        <ItemTemplate>
                                                            <asp:Label Width="60" Enabled="false" ID="lbl_File"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Files_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="File Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pages">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Pages" Width="60" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                            <HeaderStyle CssClass="darkbackground" />
                                        </asp:GridView>
                                    </td>
                                </tr>--%>
                                <tr><td colspan="4">
                                    &nbsp;</td></tr>
                                <tr><td colspan="4"></td></tr>
                            
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnNewJobInfo" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Save" OnClick="btnNewJobInfo_Click"/>
                                        <%--<asp:Button ID="btnJobClear" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Clear" OnClick="btnFileInfo_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabNewFileDetails" runat="server">
                            <table id="Table4" border="0" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                        <td  colspan="4" style="background-image: url('images/green-noise-background.png');" class="auto-style7">
                                        <img id="img7" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;
                                        <asp:Label ID="Label8" runat="server" Text="Label">File Information</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewSourceDate" runat="server" Text="Source Received on:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewsource" runat="server" CssClass="TxtBox" Width="137px" TabIndex = "20"></asp:TextBox>
                                        <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtNewsource','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="img10" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNewTarDate" runat="server" Text="Target Received on:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewtarget" runat="server" CssClass="TxtBox" Width="101px" TabIndex = "20"></asp:TextBox>
                                        <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtNewtarget','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="img11" runat="server" />
                                            <asp:CheckBox ID="CheckNewYTR" Text="YTR" runat="server" AutoPostBack="true" OnCheckedChanged="CheckNewYTR_CheckedChanged1"  />
                                    </td> 
                                </tr>
                                <tr>
                                        <td>
                                            Target File Name Convention:
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="DropNewNameConv" runat="server" Width="250px">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Selected="True">As per source</asp:ListItem>
                                                <asp:ListItem>As per source_language code</asp:ListItem>
                                                <asp:ListItem>As per target</asp:ListItem>
                                                <asp:ListItem>As per target_language code</asp:ListItem>
                                               <%-- <asp:ListItem>File Name_Languge Code_YYYY_MM_DD_Version</asp:ListItem>
                                                <asp:ListItem>File Name_YYYY_MM_DD_Version</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                <tr>
                                    <td>
                                        Source File Information:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="auto-style8">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvFileInfo" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                                 CssClass="lightbackground" width="76%">
                                                    <HeaderStyle CssClass="darkbackground"  />
                                                    <AlternatingRowStyle BackColor="#F2F2F2" />
                                                    <Columns>
                                                        <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                                <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                                                <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="TaskName" HeaderText="TaskName"  >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
                                                                <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="80px" />
                                                        </asp:TemplateField>
                                                       <asp:TemplateField SortExpression="Lang_name" HeaderText="Language Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
                                                                <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="130px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Soft_Name"  HeaderText="Software Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSoft_Name" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
                                                                <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="90px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Files" HeaderText="No.of Files" >
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvFiles" Width="50" runat="server" Text='<%# Eval("Files") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                               <asp:LinkButton ID="lnkEdit" runat="server" Text = "Click" OnClick = "Edit"></asp:LinkButton>
                                                           </ItemTemplate>
                                                                    <HeaderStyle Width="50px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                <asp:Button ID="btnAdd" runat="server" Text="Add" Visible="false" />

                                                <asp:Panel ID="pnlAddEdit" Width="700" Height="500" runat="server" CssClass="modalPopup" style = "display:none">
                                                <asp:Label ID="Label9" runat="server" Text="File Name & Pages Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <br />
                                                <table align = "center">
                                                     <tr>
                                                        <td >
                                                             <span style="color: Red">*</span>Attach Excel file
                                                        </td>
                                                        <td >
                                                            <asp:FileUpload ID="fileBrowse" runat="server" />
                                                            <%-- <asp:FileUpload ID="fileBrowse" runat="server" />--%>
                                                        </td>
                                                        <td >
                                                            <asp:Button ID="btnUpload"  CssClass="dpbutton" runat="server" Text="Upload" OnClick="btnUpload_Click" />&nbsp;
                                                        </td>
                                                    </tr>
                                                   <tr>
                                                       <td>
                                                           <asp:Label ID="NL_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="NTLS_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="Label10" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="Task_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="Lang_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="Soft_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                       </td>
                                                        <td>
                                                             <asp:GridView ID="gv_FilePages"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                                      CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  Width="250px" GridLines="Vertical"  DataKeyNames="Files_ID"
                                                                      AllowSorting="True" AllowPaging="true" PageSize="9" CellPadding="4" OnPageIndexChanging="gv_FilePages_PageIndexChanging" >
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="File No." >
                                                                            <ItemTemplate>
                                                                                <asp:Label Width="60" Enabled="false" ID="lbl_File"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Files_ID") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                                                                <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                                                            </ItemTemplate>
                                                                        <HeaderStyle Wrap="False" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TaskName"  >
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Language Name" >
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField  HeaderText="Software Name" >
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSoft_Name" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="File Name">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt_Name"  runat="server" Text='<%# Eval("Files_name") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pages">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt_Pages" Width="50" runat="server" Text='<%# Eval("Pages") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                <AlternatingRowStyle CssClass="dullbackground" />
                                                                <HeaderStyle CssClass="darkbackground" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                           <asp:Button ID="btnFPSave" runat="server" Text="Save" OnClick = "Save"  CssClass="dpbutton"/>
                                                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="lblResult" runat="server" ForeColor="Red" Text=""></asp:Label>
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
                                                <asp:AsyncPostBackTrigger ControlID = "gvFileInfo" />
                                                <asp:AsyncPostBackTrigger ControlID = "btnFPSave" />
                                                <asp:PostBackTrigger  ControlID = "btnUpload" />
                                            </Triggers> 
                                        </asp:UpdatePanel> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTarName" runat="server" Text="Target File Information:"></asp:Label>
                                        <asp:Button ID="xxxx" TabIndex = "4" CssClass="dpbutton" runat="server" Text="" Visible="true" style="display:none;"  OnClick="xxxx_Click"/>
                                    </td>
                                </tr>
                                <tr>
	                                <td colspan="4" class="auto-style8">
		                                <asp:UpdatePanel ID="UpdatePanelTar" runat="server">
			                                <ContentTemplate>
				                                <asp:GridView ID="gvTarFileInfo" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
				                                 CssClass="lightbackground" width="76%">
					                                <HeaderStyle CssClass="darkbackground"  />
					                                <AlternatingRowStyle BackColor="#F2F2F2" />
					                                <Columns>
						                                <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
							                                <ItemTemplate>
								                                <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
								                                <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
								                                <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
							                                </ItemTemplate>
							                                <HeaderStyle Width="40px" />
						                                </asp:TemplateField>
						                                <asp:TemplateField SortExpression="TaskName" HeaderText="TaskName"  >
							                                <ItemTemplate>
								                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
								                                <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
							                                </ItemTemplate>
							                                <HeaderStyle Width="80px" />
						                                </asp:TemplateField>
					                                   <asp:TemplateField SortExpression="Lang_name" HeaderText="Language Name" >
							                                <ItemTemplate>
								                                <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
								                                <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
							                                </ItemTemplate>
							                                <HeaderStyle Width="130px" />
						                                </asp:TemplateField>
						                                <asp:TemplateField SortExpression="Soft_Name"  HeaderText="Software Name" >
							                                <ItemTemplate>
								                                <asp:Label ID="lblgvSoft_Name" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
								                                <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
							                                </ItemTemplate>
							                                <HeaderStyle Width="90px" />
						                                </asp:TemplateField>
						                                <asp:TemplateField SortExpression="Files" HeaderText="No.of Files" >
							                                <ItemTemplate>
								                                <asp:TextBox ID="lblgvFiles" Width="50" runat="server" Text='<%# Eval("TFiles") %>'></asp:TextBox>
							                                </ItemTemplate>
							                                <HeaderStyle Width="50px" />
						                                </asp:TemplateField>
						                                <asp:TemplateField HeaderText="Edit">
							                                <ItemTemplate>
							                                   <asp:LinkButton ID="lnkEdit" runat="server" Text = "Click" OnClick = "TarEdit"></asp:LinkButton>
						                                   </ItemTemplate>
									                                <HeaderStyle Width="50px" />
						                                </asp:TemplateField>
					                                </Columns>
				                                </asp:GridView>
			                                </ContentTemplate> 
			                                <Triggers>
				                                <asp:AsyncPostBackTrigger ControlID = "gvTarFileInfo" />
			                                </Triggers> 
		                                </asp:UpdatePanel> 
	                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewUsageFonts" runat="server" Text="Usage of Fonts Supplied in Source:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lboxNewusagefonts" SelectionMode="Multiple" Width="162px" runat="server" OnPreRender="lboxNewusagefonts_PreRender"></asp:ListBox>
                                    </td>
                                    <td align="center">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button CssClass="dpbutton" ID="btnNewUFontsadd" runat="server" Text="Add" OnClick="btnNewUFontsadd_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button CssClass="dpbutton" ID="btnNewUFontsRemove" runat="server" Text="Remove" OnClick="btnNewUFontsRemove_Click"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lboxNewUFonts" SelectionMode="Multiple" Width="150" runat="server" OnPreRender="lboxNewUFonts_PreRender"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtNewOtherUsageFonts" runat="server"></asp:TextBox><asp:Label runat="server" id="f1New" style="font-size: 7pt; color: #ff0000">If Others</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNewfonts" runat="server" Text="Usage of Fonts with Respect to Target Languages:" Height="29px" Width="182px"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:ListBox ID="lboxNewFonts" runat="server" SelectionMode="Multiple" Width="161px" OnPreRender="lboxNewFonts_PreRender" ></asp:ListBox>
                                    </td>
                                    <td align="center">
                                        <table>
                                            <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnNewfontsadd" runat="server" Text="Add" OnClick="btnNewfontsadd_Click"/>
                                               </td>
                                            </tr>
                                            <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnNewfontsdel" runat="server" Text="Remove" OnClick="btnNewfontsdel_Click"   />
                                               </td>
                                            </tr>
                                        </table>
                                      </td>
                                      <td>
                                            <asp:ListBox ID="lboxNewTFonts" SelectionMode="Multiple" Width="150" runat="server" OnPreRender="lboxNewTFonts_PreRender"></asp:ListBox>
                                      </td>
                                 </tr>
                                 <tr>
                                    <td>
                                                
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtNewFonts" runat="server"></asp:TextBox>
                                        <asp:Label runat="server" id="f2New" style="font-size: 7pt; color: #ff0000">If Others</asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                        <td>
                                            <asp:Label ID="lblNewmissfonts" runat="server" Text="Missing Fonts:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lboxNewMissFonts" runat="server" SelectionMode="Multiple" Width="161px" OnPreRender="lboxNewMissFonts_PreRender" ></asp:ListBox>
                                        </td>
                                        <td align="center">
                                            <table>
                                               <tr>
                                                    <td>
                                                        <asp:Button CssClass="dpbutton" ID="btnNewmissfontsadd" runat="server" Text="Add" OnClick="btnNewmissfontsadd_Click"  />
                                                    </td>
                                               </tr>
                                               <tr>
                                                   <td>
                                                       <asp:Button CssClass="dpbutton" ID="btnNewmissfontsdel" runat="server" Text="Remove" OnClick="btnNewmissfontsdel_Click"  />
                                                   </td>
                                               </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lboxNewMFonts" SelectionMode="Multiple" Width="150" runat="server" OnPreRender="lboxNewMFonts_PreRender"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>   
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtNewMissFonts" runat="server"></asp:TextBox>
                                            <asp:Label runat="server" id="f3New" style="font-size: 7pt; color: #ff0000">If Others</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNewmissfiglink" runat="server" Text="Missing Figure Links:" Width="132px"></asp:Label>
                                        </td>
                                        <td>
                                        <br />
                                            <asp:TextBox ID="txtNewfiglinks" runat="server" CssClass="TxtBox" TextMode="MultiLine" Height="50px" Width="150px" TabIndex = "18"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="width: 191px">
                                            <strong><asp:Label ID="lblNewimg" runat="server" Text="Image Details: "></asp:Label></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:UpdatePanel ID="up1" runat="server">
                                                <ContentTemplate>
                                                   <asp:GridView ID="gv_imagesNew" runat="server" AutoGenerateColumns="False" Font-Size="8pt"  
                                                        CssClass="lightbackground" Width="874px">
                                                        <HeaderStyle CssClass="darkbackground"  />
                                                        <AlternatingRowStyle BackColor="#F2F2F2" />
                                                        <Columns>
                                                        <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                                <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                                                <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                                                <asp:HiddenField ID="hid_FP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="TaskName" HeaderText="TaskName"  >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
                                                                <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Lang_name" HeaderText="Language Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
                                                                <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Soft_Name"  HeaderText="Software Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSoft_Name" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
                                                                <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Files_Name" HeaderText="File Name" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvFiles" runat="server" Text='<%# Eval("Files_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField  HeaderText="Editable">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="50px" ID="txt_edit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EDITABLE") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField  HeaderText="Scanned">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="50px" ID="txt_scan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SCANNED") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField  HeaderText="Non-localised">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="60px" ID="txt_nonlocal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Non_loc") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField  HeaderText="Localised">
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="50px" ID="txt_images" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Localised") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <strong>
                                            <asp:Label ID="lblNewtable" runat="server" Text="Table Details"></asp:Label>
                                        </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNewnotable" runat="server" Text="No. of Tables:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DropNewnooftables" runat="server" Width="75px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <strong>
                                            <asp:Label ID="lblNewcomplex" runat="server" Text="Complexity"></asp:Label>
                                        </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNewcomplexlev" runat="server" Text="Complexity Level:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropNewcomplex" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropNewcomplex_SelectedIndexChanged" Width="95px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNewReason" runat="server" Text="Reason:"></asp:Label>
                                        </td>
                                        <td><div style="width:300px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                            <asp:CheckBoxList ID="ListNewComplexReason" runat="server"   OnPreRender="ListNewComplexReason_PreRender" ></asp:CheckBoxList>
                                            </div>
                                            <img id="imgNew4" src="images/tools/new.png" language="javascript" TabIndex = "17" runat="server"
                                            onclick="return imgReason_editor_onclick()" style="cursor: pointer" title="New Reason" visible="false" />&nbsp;
                                            <asp:ImageButton src="images/tools/Refresh.png" ID="ImageButton2" runat="server" OnClick="ImageButton1_Click" Visible="false"  />
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        <strong>
                                            <asp:Label ID="lblNewpdf" runat="server" Text="PDF Creation Settings & Bleed Information:"></asp:Label>
                                        </strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNewproof" runat="server" Text="Proof:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewproof" runat="server"></asp:TextBox><br />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNewpress" runat="server" Text="Press/Print:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewpress" runat="server"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNewpage" runat="server" Text="Page Size:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewpagesize" runat="server"></asp:TextBox> 
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNewbleed" runat="server" Text="Bleed:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewBleed" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Deliveries:</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Delivery Type :
                                        </td>
                                        <td>
                                            <asp:ListBox ID="lboxNewdelivryType" runat="server" SelectionMode="Multiple" 
                                                 OnSelectedIndexChanged="lboxNewdelivryType_SelectedIndexChanged">
                                                <asp:ListItem Value="1">Application Files</asp:ListItem>
                                                <asp:ListItem Value="2">High Resolution PDF</asp:ListItem>
                                                <asp:ListItem Value="3">Low Resolution PDF</asp:ListItem>
                                            </asp:ListBox><br />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNewSoft" runat="server" Text="Software to be Used:" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:GridView ID="gv_soft1New"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                  CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_Soft1New_RowDataBound" Width="250px" Height="141px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Task Name" >
                                                    <ItemTemplate>
                                                        <asp:Label Width="60" Enabled="false" ID="txt_task"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaskName") %>'></asp:Label>
                                                        <asp:HiddenField ID="hf_taskID" runat="server" 
                                                                Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Language">
                                                    <ItemTemplate>
                                                        <asp:Label Width="60" Enabled="false" ID="txt_Lang"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Lang_Name") %>'></asp:Label>
                                                        <asp:HiddenField ID="hf_LangID" runat="server" 
                                                                Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTargetDate" runat="server"  Width="70" Text='<%#Eval("Target_Date") %>'></asp:TextBox>
                                                        <%--<asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtTargetDate" runat="server"></asp:CalendarExtender>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Software">
                                                    <ItemTemplate>
                                                        <asp:ListBox  ID="lboxSoft" AutoPostBack="true"  SelectionMode="Multiple"  OnSelectedIndexChanged="lboxNew1Soft_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Version">
                                                    <ItemTemplate>
                                                        <asp:ListBox  ID="lboxVer" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        <AlternatingRowStyle CssClass="dullbackground" />
                                        <HeaderStyle CssClass="darkbackground" />
                                    </asp:GridView>
                                            <%--<asp:GridView ID="gv_soft1New1"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_SoftNew1_RowDataBound" Width="242px" Visible="False" >
                                                <Columns>
                                                    <asp:TemplateField  HeaderText="Task Name" >
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="60"  ID="txt_task" Enabled="false"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Task") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Software">
                                                        <ItemTemplate>
                                                            <asp:ListBox ID="lboxSoft" AutoPostBack="true" Enabled="false" SelectionMode="Multiple" 
                                                                OnSelectedIndexChanged="lboxSoftNew_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Version">
                                                        <ItemTemplate>
                                                            <asp:ListBox ID="lboxVer" SelectionMode="Multiple" Enabled="false" runat="server" ></asp:ListBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                                <HeaderStyle CssClass="darkbackground" />
                                            </asp:GridView>--%>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            <strong>Queries:</strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Query:
                                        </td>
                                        <td colspan="2">
                                            Response:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                        <asp:TextBox ID="txtNewQuery" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                            Height="50px" Width="200px" TabIndex = "18"></asp:TextBox>
                                        </td>
                                        <td align="center" colspan="2" >
                                        <asp:TextBox ID="txtNewQueryans" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                            Height="50px" Width="200px" TabIndex = "18"></asp:TextBox>&nbsp;
                                            <asp:Button ID="btnNewQueries" runat="server" Text="Add" TabIndex = "4" CssClass="dpbutton" OnClick="btnNewQueries_Click"/> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                        <br />
                                            <asp:GridView ID="gv_QueriesNew" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                                HeaderStyle-CssClass="darkbackground" Width="479px">
                                                <Columns>
                                                    <asp:TemplateField  SortExpression="queries" HeaderText="Query"  >
                                                        <ItemTemplate>
                                                            <asp:Label   ID="lblgvqueries" runat="server" Text='<%# Eval("queries") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="response" HeaderText="Response"  >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvresponse" runat="server" Text='<%# Eval("response") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Special Instructions/Guidelines:
                                        </td>
                                        <td colspan="3">
                                        <br />
                                            <asp:TextBox ID="txtNewSplIns" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="476px" TabIndex = "18"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13"  runat="server" Text="Upload Mail Details:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="MailNewUpload"  runat="server" />
                                        </td>                                                
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14"  runat="server" Text="Mail Details:"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="lblNewMail"  runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="Label16" runat="server" Text="Label" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp; &nbsp;
                                            <asp:Button ID="btnNewSave" runat="server" TabIndex = "4" CssClass="dpbutton" Text="Save" OnClientClick="return ValidationTxt();" 
                                                OnClick="btnNewSave_Click" />
                                            <asp:Button ID="btnNewClearfile" runat="server" Text="Clear" TabIndex = "4" CssClass="dpbutton" 
                                                OnClick="btnNewClearfile_Click" />
                                        </td>
                                    </tr>
                            </table>
                        </div>
                        <div class="content" id="tabJobTracking" runat="server">
                        <table id="Table12" border="0" cellpadding="2" cellspacing="0">
                            <tr class="dpJobGreenHeader">
                                <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:900px" colspan="4">
                                    <img id="img13" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;
                                    <asp:Label ID="Label15" runat="server" Text="Label">Job Tracking</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" OnUnload="chkDueDate1_CheckedChanged" ClientIDMode="Static">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvJobTrack" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                            CssClass="lightbackground" Width="874px" OnRowDataBound="gvJobTrack_RowDataBound" ClientIDMode="Static" >
                                            <HeaderStyle CssClass="darkbackground"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                        <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                                        <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                                        <asp:HiddenField ID="hid_FP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="TaskName" HeaderText="TaskName"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
                                                        <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Lang_name" HeaderText="Language Name" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
                                                        <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Soft_Name"  HeaderText="Software Name" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSoft_Name" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
                                                        <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Files_Name" HeaderText="File Name" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvFiles" runat="server" Text='<%# Eval("Files_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Pages" HeaderText="Pages" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPages" Width="50" runat="server" Text='<%# Eval("Pages") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" Wrap="False" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField SortExpression="Stage" HeaderText="Stage" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvStage" Width="50" runat="server" Text='<%# Eval("AmendName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="75px" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="WorkFlow" HeaderText="WorkFlow" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvWorkFlow" Width="50" runat="server" Text='<%# Eval("WorkFlow") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="75px" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="DelStatus" HeaderText="Status" >
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="dropDelStatus" runat="server"></asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit1" runat="server" Text = "Click"  OnClick = "Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="New/Delivery" >
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkdel" runat="server" Text = "New/Delivery"  OnClick = "OnDelivery"></asp:LinkButton><br />
                                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelStatus" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" Wrap="False" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                    </asp:GridView>
                                            <asp:Panel ID="pnlDel" Width="700" Height="320" runat="server" CssClass="modalPopup" style = "display:none">
                                                <asp:Label ID="Label17" runat="server" Text="Delivery Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <table align = "center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" Text="Project Title:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="DelProName" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" Text="Job ID:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="DelJobID" Enabled="false" runat="server"></asp:TextBox>
                                                            <asp:Label ID="DelNL_ID" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="DelLoc_ID" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Delivery Details :
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="dropCurStage" AutoPostBack="true" OnSelectedIndexChanged="dropCurStage_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem Value="0" Selected="True">Current Stage</asp:ListItem>
                                                                <asp:ListItem Value="1">Next Stage</asp:ListItem>
                                                                <asp:ListItem Value="2">Next Stage + Final Package</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"> 
                                                            Delivery Time:
                                                            <asp:TextBox ID="txtdeldateAll" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdeldateAll','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img14"  runat="server" />
                                                            <asp:DropDownList ID="dropDelHrsAll" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="dropDelHrsAll_SelectedIndexChanged">
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
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
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="dropDelMinsAll" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="dropDelMinsAll_SelectedIndexChanged">
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:DropDownList ID="dropDelZoneAll" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="dropDelZoneAll_SelectedIndexChanged" >
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Value="IST">IST</asp:ListItem>
                                                                <asp:ListItem Value="PST">PST</asp:ListItem>
                                                                <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                                <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                                                <asp:ListItem Value="CET">CET</asp:ListItem>
                                                                <asp:ListItem Value="CST">CST</asp:ListItem>
                                                                <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                                <asp:ListItem Value="EST">EST</asp:ListItem>
                                                                <asp:ListItem Value="JST">JST</asp:ListItem>
                                                                <asp:ListItem Value="BST">BST</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="Label20" runat="server" Text="Delivery Time IST"></asp:Label>
                                                            <asp:TextBox ID="txtDel_ISTAll" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                        <asp:Button ID="DelSave" runat="server" Text="Save" OnClick = "onDelSave" CssClass="dpbutton"/>
                                                        <asp:Button ID="DelClose" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblResult2" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:LinkButton ID="lnkFake2" runat="server"></asp:LinkButton>
                                            <asp:ModalPopupExtender ID="DelPopUp" runat="server" DropShadow="false"
                                            PopupControlID="pnlDel" TargetControlID = "lnkFake2"
                                            BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>

                                            <asp:Panel ID="pnlAddEdit1" Width="700" Height="350" runat="server" CssClass="modalPopup" style = "display:none">
                                                <asp:Label ID="Label21" runat="server" Text="File Name & Pages Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <table align = "center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" Text="Project Title:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProjectTitle1" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="txtFP_ID" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label23" runat="server" Text="Job ID:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtJobid1" Enabled="false" runat="server"></asp:TextBox>
                                                            <asp:Label ID="NL_ID1" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Customer:</td>
                                                        <td>
                                                            <asp:TextBox ID="drpProjectcustomer1" runat="server" CssClass="TxtBox" Width="200px"  Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>Location:</td>
                                                        <td> 
                                                            <asp:TextBox ID="DropLocation1" runat="server" CssClass="TxtBox"  Enabled="false"></asp:TextBox>   
                                                            <asp:HiddenField ID="hid_Loc_ID" runat="server" Visible="false"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Project Editor:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProjectEditor1" runat="server" CssClass="TxtBox"  Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Target Rec. Date:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRecDate1" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtRecDate1','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueFromdate1"  runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Due Date:
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblDueFrom1" runat="server" Text="From:" Visible="False"></asp:Label>
                                                            <asp:TextBox ID="txtdueFromdate1" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueFromdate1','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                        src="images/Calendar.jpg" style="cursor: pointer;" id="img15"  runat="server" />
                                                            <asp:Label ID="lblDueTo1" runat="server" Text="To:" Visible="False"></asp:Label>
                                                            <asp:TextBox ID="txtdueTodate1" runat="server" CssClass="TxtBox" Visible="false" Width="80px"></asp:TextBox>
                                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueTodate1','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueTodate1"  runat="server" visible="false" />
                                                            <asp:CheckBox ID="chkYTC1" Text="YTC" runat="server" />
                                                            <asp:CheckBox ID="chkDueDate1" Text="Staggered Delivery" runat="server"  AutoPostBack="True" OnCheckedChanged="chkDueDate1_CheckedChanged" />
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Due Time:</td>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblFrom1" runat="server" Text="From:" Visible="False"></asp:Label>
                                                            <asp:DropDownList ID="DropDueTimeFrom1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTime1_SelectedIndexChanged">
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
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
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                            </asp:DropDownList><asp:DropDownList ID="DropDueMinFrom1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMin1_SelectedIndexChanged">
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:DropDownList ID="DropDueTimeZoneFrom1" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneFrom1_SelectedIndexChanged" >
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Value="IST">IST</asp:ListItem>
                                                                <asp:ListItem Value="PST">PST</asp:ListItem>
                                                                <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                                <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                                                <asp:ListItem Value="CET">CET</asp:ListItem>
                                                                <asp:ListItem Value="CST">CST</asp:ListItem>
                                                                <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                                <asp:ListItem Value="EST">EST</asp:ListItem>
                                                                <asp:ListItem Value="JST">JST</asp:ListItem>
                                                                <asp:ListItem Value="BST">BST</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblTo1" runat="server" Text="To:" Visible="False"></asp:Label>
                                                            <asp:DropDownList ID="DropDueTimeTo1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeTo1_SelectedIndexChanged" Visible="False" >
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
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
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                            </asp:DropDownList><asp:DropDownList ID="DropDueMinTo1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMinTo1_SelectedIndexChanged" Visible="False" >
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:DropDownList ID="DropDueTimeZoneTo1" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneTo1_SelectedIndexChanged" Visible="False" >
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Value="IST">IST</asp:ListItem>
                                                                <asp:ListItem Value="PST">PST</asp:ListItem>
                                                                <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                                <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                                                <asp:ListItem Value="CET">CET</asp:ListItem>
                                                                <asp:ListItem Value="CST">CST</asp:ListItem>
                                                                <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                                <asp:ListItem Value="EST">EST</asp:ListItem>
                                                                <asp:ListItem Value="JST">JST</asp:ListItem>
                                                                <asp:ListItem Value="BST">BST</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CheckBox ID="chkDueTime1" Text="Staggered Delivery" runat="server" AutoPostBack="True" OnCheckedChanged="chkDueTime1_CheckedChanged" Width="133px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;IST Time :&nbsp;&nbsp;
                                                            <asp:Label ID="lblIndFrom1" runat="server" Text="(From)" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtIndFrom1" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblIndTo1" runat="server" Text="(To)" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtIndTo1" runat="server" Width="80px" Visible="False" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Task:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lboxtask1" runat="server" CssClass="TxtBox"  Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Stage:
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lboxStage" runat="server" SelectionMode="Multiple" Width="130px"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"> 
                                                            Delivery Date & Time:
                                                            <asp:TextBox ID="txtDelDate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtDelDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgDelDate"  runat="server" />
                                                            <asp:DropDownList ID="DropDelHrs" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDelHrs_SelectedIndexChanged">
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>01</asp:ListItem>
                                                                <asp:ListItem>02</asp:ListItem>
                                                                <asp:ListItem>03</asp:ListItem>
                                                                <asp:ListItem>04</asp:ListItem>
                                                                <asp:ListItem>05</asp:ListItem>
                                                                <asp:ListItem>06</asp:ListItem>
                                                                <asp:ListItem>07</asp:ListItem>
                                                                <asp:ListItem>08</asp:ListItem>
                                                                <asp:ListItem>09</asp:ListItem>
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
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="DropDelMins" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDelMins_SelectedIndexChanged">
                                                                <asp:ListItem>00</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                                <asp:ListItem>45</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:DropDownList ID="DropDelTimeZone" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDelTimeZone_SelectedIndexChanged" >
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Value="IST">IST</asp:ListItem>
                                                                <asp:ListItem Value="PST">PST</asp:ListItem>
                                                                <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                                <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                                                <asp:ListItem Value="CET">CET</asp:ListItem>
                                                                <asp:ListItem Value="CST">CST</asp:ListItem>
                                                                <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                                <asp:ListItem Value="EST">EST</asp:ListItem>
                                                                <asp:ListItem Value="JST">JST</asp:ListItem>
                                                                <asp:ListItem Value="BST">BST</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblDelTimeIST" runat="server" Text="Delivery Time IST"></asp:Label>
                                                            <asp:TextBox ID="txtDelIST" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Status:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlStatus" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                        <asp:Button ID="btnSave1" runat="server" Text="Save" OnClick = "Save1" CssClass="dpbutton"/>
                                                        <asp:Button ID="btnCancel1" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <asp:Label ID="lblResult1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:LinkButton ID="lnkFake1" runat="server"></asp:LinkButton>
                                            <asp:ModalPopupExtender ID="popup1" runat="server" DropShadow="false"
                                            PopupControlID="pnlAddEdit1" TargetControlID = "lnkFake1"
                                            BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                        </ContentTemplate> 
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID = "gvJobTrack" />
                                            <asp:AsyncPostBackTrigger ControlID = "btnSave1" />
                                            <asp:AsyncPostBackTrigger ControlID = "DelSave" />
                                        </Triggers> 
                                    </asp:UpdatePanel> 
                                </td>
                            </tr>
                        </table>
                    </div>
                        <div class="content" id="tabNewQuote" runat="server">
                            <table >
                                <tr  class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:900px" colspan="4">
                                        <img  id="Img12" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="Label12" runat="server" Text="Project Cost"></asp:Label>
                                   </td>
                                </tr>
                            <tr>
                                <td colspan="7" style="height: 200px" >
                                    <div id="Div2" runat="server" style="width:650px;padding-bottom:10pt;margin-left: 10px;">
                                    &nbsp;
                                    <asp:GridView ID="gv_pmoduleNew" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="dullbackground"
                                         CssClass="lightbackground" HeaderStyle-CssClass="darkbackground" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Project Name" >
                                                <ItemTemplate>
                                                    <asp:TextBox Width="150px" ID="txt_name" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"projectname") %>'></asp:TextBox>
                                                     <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                                     <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
                                                     <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Process">
                                                <ItemTemplate>
                                                    <asp:TextBox Width="80px" ID="txt_des" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TASKNAME") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Software">
                                                <ItemTemplate>
                                                    <asp:TextBox Width="80px" ID="txt_soft" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Software") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Taken">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_time" Width="80px" runat="server" OnTextChanged="TimeTakenNew_TextChanged" AutoPostBack="true" Text='<%# DataBinder.Eval(Container.DataItem,"TIME_TAKEN") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Language Count">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_langcount" ReadOnly="true" Width="80px"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LANG_COUNT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Page Count" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_pagecount" ReadOnly="true" runat="server"  Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"PAGES_COUNT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Page">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_totalpage" ReadOnly="true" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"TOTAL_PAGES") %>'></asp:TextBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate(Hrs)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_hrate" runat="server" OnTextChanged="HrsNew_TextChanged" AutoPostBack="true" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"H_RATE") %>'></asp:TextBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Total Cost - Hour">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_hourrate" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"HOUR_RATE") %>'></asp:TextBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hour">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_hour" runat="server" Width="50px" Checked='<%# Convert.ToBoolean(Eval("HOURYN")) %>'></asp:CheckBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate(Page)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_prate" runat="server" OnTextChanged="PageNew_TextChanged" AutoPostBack="true" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"P_RATE") %>'></asp:TextBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Total Cost - Page">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_pagerate" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"PAGE_RATE") %>'></asp:TextBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Page">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_page" runat="server" Width="50px" Checked='<%# Convert.ToBoolean(Eval("PAGEYN")) %>'></asp:CheckBox>
                                                   </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                </td>
                            </tr>
                           <tr>
                               <td>
                                    Remarks if any:
                               </td>
                               <td>
                                    <asp:TextBox ID="txtNewquoteremark" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                        Height="50px" Width="200px" TabIndex = "18"></asp:TextBox>
                                </td>
                           </tr>
                           <tr>
                                <td class="auto-style9" ><strong>Created By</strong></td>
                               <td class="auto-style9">
                               </td>
                               <td colspan="2" visible="false" class="auto-style9">
                                   <asp:Label ID="lblNewFinalChk" runat="server" Text="Final Check" Font-Bold="True"></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td >
                                   <asp:TextBox ID="txtNewProjectCo" runat="server" Enabled="False"></asp:TextBox>
                               </td>
                               <td>
                               </td>
                               <td>
                                   <asp:TextBox ID="txtNewFinalCheck" runat="server" Enabled="False"></asp:TextBox>
                               </td>
                           </tr>
                            <tr>
                                <td colspan="2"></td>
                                <td align="right" >
                                    <asp:Button ID="btnNewQuoteSave" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Save" OnClick="btnNewQuoteSave_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNewClear" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Clear" OnClick="btnNewClear_Click" />
                                </td>
                            </tr>
                           <tr>
                               <td colspan="7">
                               </td>
                           </tr>
                        </table>
                      </div>
                        <div class="content" id="tabLaunchDetails" runat="server" visible="false">
                            <div id="CUSTOMER_TABLE" >
                            </div>
                            <div id="PARENT_JOB"   >
                               
                            <%-- <table id="Table3" border="0" width="100%" cellpadding="2" cellspacing="0">
                             <tr>
                             <td>
                             </td></tr></table>--%>
                                <table id="XMLTAGS" border="0" style="background-image: url(images/green-noise-background.png)"  cellpadding="2" cellspacing="4">
                                 <tr bgcolor="#f0fff0">
                                        <td colspan="3"  style="background-image: url(images/green-noise-background.png)" >
                                            <img id="imgProjectHeader" src="images/tools/new.png" runat="server" />&nbsp;<strong><asp:Label
                                                ID="lblProjectHeader" runat="server" Text="Label">New Project</asp:Label></strong></td>
                                        <td  style="background-image: url(images/green-noise-background.png); width: 80px;" >
                                            &nbsp; &nbsp;
                                    </td>
                                    <td style="background-image: url(images/green-noise-background.png); width: 202px;">
                                        &nbsp;</td>
                                    <td style="background-image: url(images/green-noise-background.png)">
                                    </td>
                                    </tr>
                                   <tr>
                                        <td style="width: 183px; height: 1px;" >
                                            &nbsp;</td>
                                        <td style="width: 312px; height: 1px;" >
                                            </td>
                                        
                                         <td style="width: 130px; height: 1px;" >
                                        </td>
                                        <td style="width: 80px; height: 1px;" >
                                            &nbsp;</td>
                                        <td style="width: 202px; height: 1px;">
                                    </td>
                                    <td style="height: 1px">
                                            </td>
                                    </tr>
                                    <tr>
                                    <td style="width: 183px" >
                                            <asp:Label ID="Label11" runat="server" Text="Project Title:"></asp:Label><span style="font-size: 9pt; color: #ff0000">*</span> </td>
                                        <td style="width: 312px" >
                                            <asp:TextBox ID="txtProjectTitle" runat="server" CssClass="TxtBox" Width="212px"
                                                BackColor="White" MaxLength="300" TabIndex = "7"></asp:TextBox></td>
                                        <td >
                                            Client ID:
                                        </td>
                                            <td colspan="2">
                                            <asp:TextBox ID="txtProjectid" runat="server"></asp:TextBox>
                                                &nbsp; &nbsp;&nbsp; &nbsp;Job ID:
                                        
                                           <asp:TextBox ID="txtJobID" ReadOnly="true" runat="server" Width="144px"></asp:TextBox>
                                            &nbsp; &nbsp; &nbsp;
                                  
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 183px; height: 15px;" >
                                            Customer:<span  style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td style="width: 312px; height: 15px;"  >
                                            <asp:DropDownList ID="drpProjectcustomer" runat="server" Width="216px" TabIndex = "5" AutoPostBack="True" OnSelectedIndexChanged="drpProjectcustomer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                           
                                            
                                            </td>
                                            
                                           
                                          <td style="width: 130px; height: 15px;" >
                                              Location:</td>
                                        <td style="height: 15px" colspan="3" >
                                        
                                            
                                            <asp:DropDownList ID="DropLocation" runat="server" Width="118px" OnSelectedIndexChanged="DropLocation_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList><img  alt="Location" border="0" height="20" onclick="javascript:calendar_window=window.open('LaunchLocation.aspx?formname=txtProjectDdate','calendar_window','width=750,height=250,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/tools/new.png" style="cursor: pointer;" id="img3"  runat="server" />
                                            &nbsp;&nbsp;&nbsp;
                                            Project Editor:
                                            &nbsp;&nbsp;
                                            <asp:TextBox ID="txtProjectEditor" runat="server" CssClass="TxtBox" Width="135px" TabIndex = "16"></asp:TextBox>
                                            <img id="imgBD_editor" src="images/tools/user_go.png" language="javascript" TabIndex = "17"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select Editor" />
                                            
                                            
                                             
                                        </td>
                                    </tr>
                                    <tr>
                                  <td style="width: 183px">
                                      Date:
                                  </td>
                                    <td style="width: 312px">
                                        <asp:TextBox ID="txtDate" ReadOnly="true" runat="server"></asp:TextBox>
                                    </td>
                                          <td style="height: 37px"  >
                                Due Date:
                                </td>
                                <td style="width: 312px; height: 37px;" colspan="4" >
                                <asp:Label ID="lblDueFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtdueFromdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "22"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueFromdate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueFromdate"  runat="server" />
                                <asp:Label ID="lblDueTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtdueTodate" runat="server" CssClass="TxtBox" Visible="false" Width="80px" TabIndex = "22"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueTodate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueTodate" visible="false" runat="server" />
   
                                    
                                   <asp:CheckBox ID="chkYTC" Text="YTC" runat="server" />
                                                                       
                                     <asp:CheckBox ID="chkDueDate" Text="Staggered Delivery" runat="server" AutoPostBack="True" OnCheckedChanged="chkDueDate_CheckedChanged"  /></td>
                               
                              
                                    </tr>
                                     <tr>
                                     <td style="height: 23px">
                                     
                                     </td>
                                     <td style="height: 23px">
                                     </td>
                                     <td style="height: 23px">
                                         Due Time:&nbsp;

                                         </td><td colspan="3" style="height: 23px">
                                             <asp:Label ID="lblFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                    
                                    <asp:DropDownList ID="DropDueTimeFrom" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTime_SelectedIndexChanged">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
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
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="DropDueMinFrom" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMin_SelectedIndexChanged">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="DropDueTimeZoneFrom" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneFrom_SelectedIndexChanged" >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="IST">IST</asp:ListItem>
                                        <asp:ListItem Value="PST">PST</asp:ListItem>
                                        <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                        <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                        <asp:ListItem Value="CET">CET</asp:ListItem>
                                        <asp:ListItem Value="CST">CST</asp:ListItem>
                                        <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                        <asp:ListItem Value="EST">EST</asp:ListItem>
                                        <asp:ListItem Value="JST">JST</asp:ListItem>
                                        <asp:ListItem Value="BST">BST</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="DropDueTimeTo" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeTo_SelectedIndexChanged" Visible="False" >
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
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
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="DropDueMinTo" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMinTo_SelectedIndexChanged" Visible="False" >
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="DropDueTimeZoneTo" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneTo_SelectedIndexChanged" Visible="False" >
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="IST">IST</asp:ListItem>
                                        <asp:ListItem Value="PST">PST</asp:ListItem>
                                        <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                        <asp:ListItem Value="CEST">CEST</asp:ListItem>
                                        <asp:ListItem Value="CET">CET</asp:ListItem>
                                        <asp:ListItem Value="CST">CST</asp:ListItem>
                                        <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                        <asp:ListItem Value="EST">EST</asp:ListItem>
                                        <asp:ListItem Value="JST">JST</asp:ListItem>
                                        <asp:ListItem Value="BST">BST</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:CheckBox ID="chkDueTime" Text="Staggered Delivery" runat="server" AutoPostBack="True" OnCheckedChanged="chkDueTime_CheckedChanged" Width="133px" /></td>
                                     </tr>
                                    <tr>
                                    <td>
                                    
                                    </td>
                                    <td>
                                    
                                    </td>
                                    <td>
                                    
                                    </td>
                                    <td colspan="2" >
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="TextBox2" runat="server" Width="104px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp;
                                        <asp:TextBox ID="TextBox1" runat="server" Width="104px" Visible="False"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 183px" >
                                            Project Task:</td>
                                        <td style="width: 312px" >
                                            <asp:ListBox ID="lboxtask" runat="server" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lboxtask_SelectedIndexChanged" Width="130px" ></asp:ListBox>
                                           
                                        </td>
                                       
                                        <td style="width: 130px">
                                            <asp:Label ID="lblformat" runat="server" Text="Format:"></asp:Label>
                                        
                                        </td>
                                        <td colspan="2">
                                            <asp:ListBox ID="lboxformat" runat="server" SelectionMode="Multiple" Width="152px"></asp:ListBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblsource" runat="server" Text="Source Type:" Visible="False" Width="83px"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:DropDownList ID="DropSource" runat="server" Visible="False">
                                                <asp:ListItem ></asp:ListItem>
                                                <asp:ListItem>Editable</asp:ListItem>
                                                <asp:ListItem>Scanned</asp:ListItem>
                                                <asp:ListItem Value="Editable and Scanned">Editable and Scanned </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                       
                                        <td style="width: 130px; height: 49px;" >
                                            Input Files Received:</td>
                                        <td style="height: 49px" >
                                            <asp:ListBox ID="linputfile" runat="server" SelectionMode="Multiple" Width="130px">
                                                <asp:ListItem Value="FTP">FTP</asp:ListItem>
                                                <asp:ListItem Value="Mail Attachment">Mail Attachment</asp:ListItem>
                                                <asp:ListItem Value="Skype">Skype</asp:ListItem>
                                                <asp:ListItem Value="DropBox">DropBox</asp:ListItem>
                                            </asp:ListBox>&nbsp;
                                            </td>
                                      
                                             <td style="width: 130px; height: 49px;" >
                                        No. of Folders/Packages in FTP:</td>
                                        <td style="width: 273px; height: 49px;">
                                            <asp:DropDownList ID="dropnoFTP" runat="server" Width="110px">
                                                <asp:ListItem Selected="True">0</asp:ListItem>
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
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                       
                                    </tr>
                                    
                                    <tr>
                                    <td>
                                            Software &amp; Version to be Used:
                                        </td>
                                    <td colspan="1">
                                    <asp:GridView ID="gv_Soft"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_Soft_RowDataBound" Width="242px" >
                                                   <Columns>
                                                            <asp:TemplateField  HeaderText="Task Name" >
                                                                  <ItemTemplate>
                                                                        <asp:TextBox Width="60" ReadOnly="true"  ID="txt_task"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Task") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                <HeaderStyle Wrap="False" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Software">
                                                                    <ItemTemplate>
                                                                        <asp:ListBox  ID="lboxSoft" AutoPostBack="true"  SelectionMode="Multiple" OnSelectedIndexChanged="lboxSoft_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Version">
                                                                    <ItemTemplate>
                                                                        <asp:ListBox  ID="lboxVer" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                     </Columns>
                                                    <AlternatingRowStyle CssClass="dullbackground" />
                                        <HeaderStyle CssClass="darkbackground" />
                                                   </asp:GridView>
                                    
                                    </td>
                                    <td> Platform:</td>
                                    <td>
                                         
                                            <asp:DropDownList ID="dropSwPlat" runat="server" >
                                                <asp:ListItem Value="1">MAC</asp:ListItem>
                                                <asp:ListItem Value="2">PC</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                               
                                      <tr>
                                        <td style="height: 30px; width: 183px;" >
                                            Package/Folder Size:</td>
                                        <td style="height: 30px; width: 312px;" >
                                            <asp:TextBox ID="txtSize"  runat="server" Width="69px"></asp:TextBox>
                                            <asp:DropDownList ID="DropSizeBytes" runat="server">
                                                <asp:ListItem Value="1">Bytes</asp:ListItem>
                                                <asp:ListItem Value="2">KB</asp:ListItem>
                                                <asp:ListItem Value="3">MB</asp:ListItem>
                                                <asp:ListItem Value="4">GB</asp:ListItem>
                                                <asp:ListItem Value="5">TB</asp:ListItem>
                                            </asp:DropDownList> 
                                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtSize" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                           --%>
                                        </td>
                                         <td style="width: 183px" >
                                            File Server Location:</td>
                                         <td style="width: 312px" >
                                            <asp:TextBox ID="txtFilepath" runat="server" ></asp:TextBox>
                                   <%-- <asp:FileUpload ID="txtlocation" runat="server"  />--%></td>
                                   
                                    </tr>
                                      <tr>
                                        <td  colspan="2" style="height: 38px">
                                            Does the downloaded input package/folder/file size match the package/folder/file
                                            size in FTP?</td>
                                          
                                        <td style="width: 130px; height: 38px;" >
                                            <asp:DropDownList ID="dropIpFTP" runat="server" Width="57px">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                           </td>
                                        
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                        &nbsp;</td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                    </tr>
                                   <tr>
                                   <td colspan="2" align="right" style="height: 25px">
                                   <asp:Button ID="btnJobInfo" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Save"  OnClick="btnJobInfo_Click" />
                                   </td>
                                   <td colspan="2" align="left" style="height: 25px">
                                   <asp:Button ID="btnJobClear" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Clear" OnClick="btnFileInfo_Click" />
                                   </td>
                                       </tr>
                                </table>
                            </div>
                        </div>
                        <div  class="content"   id="tabFileInfo" runat="server">
                          
                                        <table border="0" cellpadding="3"  cellspacing="0" style="background-image: url(images/green-noise-background.png); width: 417px;">
                                           <tr>
                                           <td style="width: 191px" >
                                           <strong>
                                           File Analysis :
                                           </strong>
                                           </td>
                                           </tr>
                                           
                                            <tr>
                                             <td style="width: 191px" >
                                            <asp:Label ID="lblSourceDate" runat="server" Text="Source Received on:"></asp:Label></td>
                                        <td >
                                            <asp:TextBox ID="txtsource" runat="server" CssClass="TxtBox" Width="137px" TabIndex = "20"></asp:TextBox>
                                            <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtsource','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img1" runat="server" /></td>
                                              <td >
                                                  <asp:Label ID="lblTarDate" runat="server" Text="Target Received on:"></asp:Label>
                                            </td>
                                        <td style="width: 206px" >
                                            <asp:TextBox ID="txttarget" runat="server" CssClass="TxtBox" Width="101px" TabIndex = "20"></asp:TextBox>
                                            <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txttarget','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img2" runat="server" />
                                                <asp:CheckBox ID="CheckYTR" Text="YTR" runat="server"  />
                                                </td>
                                            
                                            </tr>
                                            <tr >
                                                <td style="width: 191px">
                                                    <asp:Label ID="lbltarLang" runat="server" Text="Target languages:" Width="119px"></asp:Label>
                                                    </td>
                                                 <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:ListBox  Width="161px" ID="lboxlang"  runat="server"  OnSelectedIndexChanged="lboxlang_SelectedIndexChanged" OnPreRender="lboxlang_PreRender" ></asp:ListBox>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList ID="CheckBoxTask" runat="server" Width="107px"></asp:CheckBoxList>
                                                            </td>
                                                         </tr>
                                                      </table>
                                                   </td>
                                                   <td align="center">
                                                   <table>
                                                   <tr>
                                                   <td>
                                                    <asp:Button CssClass="dpbutton" ID="btnlangadd" runat="server" Text="Add" OnClick="btnlangadd_Click" />
                                                   </td>
                                                   </tr>
                                                   <tr>
                                                   <td>
                                                    <asp:Button CssClass="dpbutton" ID="btnlangdel" runat="server" Text="Remove" OnClick="btnlangdel_Click" />
                                                   </td>
                                                   </tr>
                                                   </table>
                                                   </td>
                                                   <td style="width: 206px">
                                                   <asp:ListBox id="lboxlangused" runat="server" Width="162px" OnPreRender="lboxlangused_PreRender"></asp:ListBox>
                                                   </td>
                                                   </tr><tr>
                                                <td >
                                                    <asp:Label ID="lblNoTarLang" runat="server" Text="No. of Target languages:" Width="149px"></asp:Label>
                                                    </td>
                                                <td >
                                                    <asp:TextBox ID="txtlangcount" ReadOnly="true" runat="server"></asp:TextBox>
                                                   </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 191px">
                                                    <asp:Label ID="lblFilePage" runat="server" Text="No. of Files & Pages per Language:" Height="26px" Width="128px"></asp:Label>
                                                   </td>
                                                <td colspan="2" >
                                                <br />
                                                <asp:GridView ID="gv_lang" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" HeaderStyle-CssClass="darkbackground" >
                                                   <Columns>
                                                            <asp:TemplateField Visible="false"  >
                                                                  <ItemTemplate>
                                                                        <asp:TextBox Width="100px" ID="txt_name" Visible="false" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Pro_ID") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                <HeaderStyle Wrap="False" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Language">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="100px" ID="txt_ID" ReadOnly="true"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LANG_NAME") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Task">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="80px" ID="txt_task" ReadOnly="true"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"taskname") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Software">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="80px" ID="txt_software" ReadOnly="true"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"software_name") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField  HeaderText="Files">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="50px" ID="txt_Files" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FILE_COUNT") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField  HeaderText="Pages">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="50px" ID="txt_Pages" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PAGES_COUNT") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                             <asp:TemplateField Visible="false" >
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Visible="false" Width="50px" ID="txt_softid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Software_id") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                          
                                                     </Columns>
                                                    <AlternatingRowStyle CssClass="dullbackground" />
                                                   </asp:GridView>
                                                    </td>
                                                    <td align="center"></td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <asp:Label ID="lblfiles" runat="server" Text="No. of Files:" Visible="False"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtFiles" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <asp:Label ID="lblUsageFonts" runat="server" Text="Usage of Fonts Supplied in Source:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lboxusagefonts" SelectionMode="Multiple" Width="162px" runat="server" OnPreRender="lboxusagefonts_PreRender"></asp:ListBox>
                                            </td>
                                            <td align="center">
                                               <table>
                                               <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnUFontsadd" runat="server" Text="Add" OnClick="btnUFontsadd_Click" />
                                               </td>
                                               </tr>
                                               <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnUFontsRemove" runat="server" Text="Remove" OnClick="btnUFontsRemove_Click" />
                                               </td>
                                               </tr>
                                               </table>
                                            </td>
                                            <td style="width: 206px">
                                                <asp:ListBox ID="lboxUFonts" SelectionMode="Multiple" Width="150" runat="server" OnPreRender="lboxUFonts_PreRender"></asp:ListBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td></td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtOtherUsageFonts" runat="server"></asp:TextBox><asp:Label runat="server" id="f1" style="font-size: 7pt; color: #ff0000">If Others</asp:Label>
                                            </td></tr>
                                            <tr>
                                            <td style="width: 191px" >
                                                <asp:Label ID="lblfonts" runat="server" Text="Usage of Fonts with Respect to Target Languages:" Height="29px" Width="182px"></asp:Label>
                                            </td>
                                            <td >
                                          
                                                <asp:ListBox ID="lboxFonts" runat="server" SelectionMode="Multiple" Width="161px" OnPreRender="lboxFonts_PreRender" ></asp:ListBox>
                                               
                                                </td>
                                                 <td align="center">
                                               <table>
                                               <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnfontsadd" runat="server" Text="Add" OnClick="btnfontsadd_Click"  />
                                               </td>
                                               </tr>
                                               <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnfontsdel" runat="server" Text="Remove" OnClick="btnfontsdel_Click"  />
                                               </td>
                                               </tr>
                                               </table>
                                            </td>
                                            <td style="width: 206px">
                                                <asp:ListBox ID="lboxTFonts" SelectionMode="Multiple" Width="150" runat="server" OnPreRender="lboxTFonts_PreRender"></asp:ListBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                                
                                            </td>
                                            <td colspan="2">
                                            <asp:TextBox ID="txtFonts" runat="server"></asp:TextBox><asp:Label runat="server" id="f2" style="font-size: 7pt; color: #ff0000">If Others</asp:Label>
                                            </td>
                                          
                                            </tr>
                                            <tr>
                                               <td >
                                                     <asp:Label ID="lblmissfonts" runat="server" Text="Missing Fonts:"></asp:Label>
                                          
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lboxMissFonts" runat="server" SelectionMode="Multiple" Width="161px" OnPreRender="lboxMissFonts_PreRender" ></asp:ListBox>
                                                
                                            </td>
                                                <td align="center">
                                               <table>
                                               <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnmissfontsadd" runat="server" Text="Add" OnClick="btnmissfontsadd_Click"  />
                                               </td>
                                               </tr>
                                               <tr>
                                               <td>
                                                   <asp:Button CssClass="dpbutton" ID="btnmissfontsdel" runat="server" Text="Remove" OnClick="btnmissfontsdel_Click"  />
                                               </td>
                                               </tr>
                                               </table>
                                            </td>
                                            <td style="width: 206px">
                                                <asp:ListBox ID="lboxMFonts" SelectionMode="Multiple" Width="150" runat="server" OnPreRender="lboxMFonts_PreRender"></asp:ListBox>
                                            </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                
                                            </td>
                                            <td colspan="2">
                                            <asp:TextBox ID="txtMissFonts" runat="server"></asp:TextBox><asp:Label runat="server" id="f3" style="font-size: 7pt; color: #ff0000">If Others</asp:Label>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                                <asp:Label ID="lblmissfiglink" runat="server" Text="Missing Figure Links:" Width="132px"></asp:Label>
                                            
                                            </td>
                                            <td>
                                            <br />
                                            <asp:TextBox ID="txtfiglinks" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="150px" TabIndex = "18"></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="4" style="width: 191px">
                                           <strong> 
                                               <asp:Label ID="lblimg" runat="server" Text="Image Details"></asp:Label></strong>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="4" align="center">
                                            <asp:GridView ID="gv_images" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" HeaderStyle-CssClass="darkbackground" Width="532px" >
                                                   <Columns>
                                                            <asp:TemplateField  Visible="false" >
                                                                  <ItemTemplate>
                                                                        <asp:TextBox ID="txt_name" Visible="false" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Pro_ID") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                <HeaderStyle Wrap="False" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Language">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="100px" ID="txt_ID" ReadOnly="true"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LANG_NAME") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Task">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="80px" ID="txt_task" ReadOnly="true"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"taskname") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Software">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="80px" ID="txt_software" ReadOnly="true"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"software_name") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                             
                                                              <asp:TemplateField  HeaderText="Editable">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="50px" ID="txt_edit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EDITABLE") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField  HeaderText="Scanned">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="50px" ID="txt_scan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SCANNED") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField  HeaderText="Non-localised">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="60px" ID="txt_nonlocal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NON_LOCAL_IMAGE") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                              <asp:TemplateField  HeaderText="Localised">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Width="50px" ID="txt_images" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"IMAGES") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                                <asp:TemplateField Visible="false" >
                                                                    <ItemTemplate>
                                                                        <asp:TextBox Visible="false" Width="50px" ID="txt_softid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Software_id") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                     </Columns>
                                                    <AlternatingRowStyle CssClass="dullbackground" />
                                                <HeaderStyle CssClass="darkbackground" />
                                                   </asp:GridView>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                            <strong>
                                                <asp:Label ID="lbltable" runat="server" Text="Table Details"></asp:Label>
                                            </strong>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                                <asp:Label ID="lblnotable" runat="server" Text="No. of Tables:"></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Dropnooftables" runat="server" Width="75px">
                                                           </asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                            <strong>
                                                <asp:Label ID="lblcomplex" runat="server" Text="Complexity"></asp:Label>
                                            </strong>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                                <asp:Label ID="lblcomplexlev" runat="server" Text="Complexity Level:"></asp:Label>
                                            
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="Dropcomplex" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Dropcomplex_SelectedIndexChanged" Width="95px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblReason" runat="server" Text="Reason:"></asp:Label>
                                            
                                            </td>
                                            <td style="width: 206px">
                                                &nbsp;<asp:ListBox ID="ListComplexReason"  Width="264px" runat="server"   SelectionMode="Multiple" OnPreRender="ListComplexReason_PreRender" ></asp:ListBox>
                                              <img id="img4" src="images/tools/new.png" language="javascript" TabIndex = "17"
                                                onclick="return imgReason_editor_onclick()" style="cursor: pointer" title="New Reason" />&nbsp;
                                                <asp:ImageButton src="images/tools/Refresh.png" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"  />
                                                
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3"></td>
                                            <td style="width: 206px">
                                                &nbsp;</td>
                                            </tr>
                                            <tr>
                                            <td colspan="4">
                                     
                                            
                                            </td>
                                            
                                            </tr>
                                            <tr>
                                            <td colspan="2">
                                            <strong>
                                                <asp:Label ID="lblpdf" runat="server" Text="PDF Creation Settings & Bleed Information:"></asp:Label>
                                            </strong>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px" >
                                                <asp:Label ID="lblproof" runat="server" Text="Proof:"></asp:Label>
                                            
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtproof" runat="server"></asp:TextBox><br />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblpress" runat="server" Text="Press/Print:"></asp:Label>
                                            
                                            </td>
                                            <td style="width: 206px">
                                                <asp:TextBox ID="txtpress" runat="server"></asp:TextBox><br />
                                            </td>
                                            </tr>
                                            <tr>
                                            
                                            <td style="width: 191px; height: 27px;">
                                                <asp:Label ID="lblpage" runat="server" Text="Page Size:"></asp:Label>
                                           
                                            </td>
                                            <td style="height: 27px">
                                                <asp:TextBox ID="txtpagesize" runat="server"></asp:TextBox> 
                                            </td>
                                            <td style="height: 27px">
                                                <asp:Label ID="lblbleed" runat="server" Text="Bleed:"></asp:Label>
                                            
                                            </td>
                                            <td style="height: 27px; width: 206px;">
                                                <asp:TextBox ID="txtBleed" runat="server"></asp:TextBox>
                                            </td>
                                            </tr>
                                            
                                            <tr>
                                            <td style="width: 191px">
                                            <strong>
                                            Deliveries:</strong>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                            Delivery Type :
                                            </td>
                                            <td >
                                          
                                                <asp:ListBox ID="lboxdelivryType" runat="server" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lboxdelivryType_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Application Files</asp:ListItem>
                                                    <asp:ListItem Value="2">High Resolution PDF</asp:ListItem>
                                                    <asp:ListItem Value="3">Low Resolution PDF</asp:ListItem>
                                                </asp:ListBox><br />
                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSoft" runat="server" Text="Software to be Used:" Visible="False"></asp:Label>
                                            </td>
                                            <td style="width: 206px">
                                                <asp:GridView ID="gv_soft1"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_Soft_RowDataBound" Width="242px" Visible="False" >
                                                   <Columns>
                                                            <asp:TemplateField  HeaderText="Task Name" >
                                                                  <ItemTemplate>
                                                                        <asp:TextBox Width="60"  ID="txt_task" Enabled="false"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Task") %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                <HeaderStyle Wrap="False" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Software">
                                                                    <ItemTemplate>
                                                                        <asp:ListBox  ID="lboxSoft" AutoPostBack="true" Enabled="false" SelectionMode="Multiple" OnSelectedIndexChanged="lboxSoft_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Version">
                                                                    <ItemTemplate>
                                                                        <asp:ListBox  ID="lboxVer" SelectionMode="Multiple" Enabled="false" runat="server" ></asp:ListBox>
                                                                    </ItemTemplate>
                                                              </asp:TemplateField>
                                                     </Columns>
                                                    <AlternatingRowStyle CssClass="dullbackground" />
                                        <HeaderStyle CssClass="darkbackground" />
                                                   </asp:GridView></td>
                                            </tr>
                                        
                                            <tr>
                                            <td style="width: 191px">
                                            File Name Convention:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DropNameConv" runat="server" Width="200px">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem>As per source</asp:ListItem>
                                                    <asp:ListItem>As per source_language code</asp:ListItem>
                                                    <asp:ListItem>As per target</asp:ListItem>
                                                    <asp:ListItem>File Name_Languge Code_YYYY_MM_DD_Version</asp:ListItem>
                                                    <asp:ListItem>File Name_YYYY_MM_DD_Version</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                            <strong>
                                            Queries:</strong>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="2">
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            Query:</td>
                                             <td colspan="2" >
                                                 Response:</td>
                                            </tr>
                                            <tr>
                                            
                                            <td align="center" colspan="2" >
                                           
                                            <asp:TextBox ID="txtQuery" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="200px" TabIndex = "18"></asp:TextBox>
                                              
                                            </td>
                                           
                                            <td align="center" colspan="2" >
                                            <asp:TextBox ID="txtQueryans" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="200px" TabIndex = "18"></asp:TextBox>&nbsp;
                                                <asp:Button ID="btnQueries" runat="server" Text="Add" TabIndex = "4" CssClass="dpbutton" OnClick="btnQueries_Click"/> 
                                            </td>
                                            </tr>
                                            <tr>
                                            <td style="width: 191px"></td>
                                            <td  colspan="4">
                                            <br />
                                                <asp:GridView ID="gv_Queries" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                         HeaderStyle-CssClass="darkbackground" Width="479px">
                                         <Columns>
                                                <asp:TemplateField  SortExpression="queries" HeaderText="Query"  >
                                                    <ItemTemplate>
                                                        <asp:Label   ID="lblgvqueries" runat="server" Text='<%# Eval("queries") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="response" HeaderText="Response"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvresponse" runat="server" Text='<%# Eval("response") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                                    <HeaderStyle CssClass="darkbackground" />
                                                    <AlternatingRowStyle CssClass="dullbackground" />
                                                </asp:GridView>
                                            </td>
                                            
                                            </tr>
                                            <tr>
                                            <td style="width: 191px">
                                            Special Instructions/Guidelines:
                                            </td>
                                            <td  colspan="3">
                                            <br />
                                            <asp:TextBox ID="txtSplIns" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="476px" TabIndex = "18"></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td>
                                                <asp:Label ID="Label4"  runat="server" Text="Upload Mail Details:"></asp:Label>
                                            </td>
                                            <td>
                                            <asp:FileUpload ID="MailUpload"  runat="server" />
                                            </td>                                                
                                            </tr>
                                            <tr>
                                            <td>
                                                <asp:Label ID="Label5"  runat="server" Text="Mail Details:"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblMail"  runat="server" ></asp:Label>
                                            </td>
                                            </tr>
                                           <tr>
                                           <td colspan="4">
                                               <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                           </td>
                                           </tr>
                                    <tr>
                                  
                       
                                    <td align="center" colspan="3">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp;
                                        <asp:Button ID="btnSave" runat="server" TabIndex = "4" CssClass="dpbutton" Text="Save" OnClientClick="return ValidationTxt();" OnClick="cmd_Save_Project_Click" />
                                        <asp:Button ID="btnClearfile" runat="server" Text="Clear" TabIndex = "4" CssClass="dpbutton" OnClick="btnClearfile_Click" />
                                    </td>
                                    </tr>
                                   <tr>
                                   <td style="width: 191px" >
                                       &nbsp;</td>
                                   <td >
                                       <asp:Label  ID="Label2" runat="server" Text="."></asp:Label>
                                   </td>
                                   </tr>
                                        </table>
                        </div>
                        <div class="content" id="tabquotedetails" runat="server">
                        <table >
                         <tr>
                                    <td  colspan="2"   >
                                        <img class="dpJobGreenHeader" id="ImgProjectCost" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="Label3" runat="server" Text="Project Cost"></asp:Label>
                                   </td>
                                  
                                </tr>
                                
           
             
                            <tr>
                                <td colspan="7" style="height: 200px" ><div id="GridDiv" runat="server" style="width:650px;padding-bottom:10pt;margin-left: 10px;">
                                    &nbsp;<asp:GridView ID="gv_pmodule" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                                    HeaderStyle-CssClass="darkbackground" ShowFooter="true">
        <Columns>
         <asp:TemplateField HeaderText="Project Name" >
                <ItemTemplate>
                    <asp:TextBox Width="150px" ID="txt_name" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"projectname") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Process">
                <ItemTemplate>
                    <asp:TextBox Width="80px" ID="txt_des" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TASKNAME") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Software">
                <ItemTemplate>
                    <asp:TextBox Width="80px" ID="txt_soft" ReadOnly="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Software") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time Taken">
                <ItemTemplate>
                    <asp:TextBox ID="txt_time" Width="80px" runat="server" OnTextChanged="TimeTaken_TextChanged" AutoPostBack="true" Text='<%# DataBinder.Eval(Container.DataItem,"TIME_TAKEN") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Language Count">
                <ItemTemplate>
                    <asp:TextBox ID="txt_langcount" ReadOnly="true" Width="80px"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LANG_COUNT") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Page Count" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="txt_pagecount" ReadOnly="true" runat="server"  Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"PAGES_COUNT") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Page">
                <ItemTemplate>
                    <asp:TextBox ID="txt_totalpage" ReadOnly="true" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"TOTAL_PAGES") %>'></asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rate(Hrs)">
                <ItemTemplate>
                    <asp:TextBox ID="txt_hrate" runat="server" OnTextChanged="Hrs_TextChanged" AutoPostBack="true" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"H_RATE") %>'></asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Total Cost - Hour">
                <ItemTemplate>
                    <asp:TextBox ID="txt_hourrate" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"HOUR_RATE") %>'></asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hour">
                <ItemTemplate>
                    <asp:CheckBox ID="chk_hour" runat="server" Width="50px" Checked='<%# Convert.ToBoolean(Eval("HOURYN")) %>'></asp:CheckBox>
                   </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rate(Page)">
                <ItemTemplate>
                    <asp:TextBox ID="txt_prate" runat="server" OnTextChanged="Page_TextChanged" AutoPostBack="true" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"P_RATE") %>'></asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Total Cost - Page">
                <ItemTemplate>
                    <asp:TextBox ID="txt_pagerate" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"PAGE_RATE") %>'></asp:TextBox>
                   </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Page">
                <ItemTemplate>
                    <asp:CheckBox ID="chk_page" runat="server" Width="50px" Checked='<%# Convert.ToBoolean(Eval("PAGEYN")) %>'></asp:CheckBox>
                   </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    </div></td>
    
                                
                            </tr>
                           <tr>
                           <td   >
                           Remarks if any:
                           </td>
                           <td >
                                            <asp:TextBox ID="txtquoteremark" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="200px" TabIndex = "18"></asp:TextBox>
                                            </td>
                           </tr>
                           <tr>
                           <td ><strong>Created By</strong></td>
                           <td>
                           </td>
                           <td colspan="2" visible="false">
                               <asp:Label ID="lblFinalChk" runat="server" Text="Final Check" Font-Bold="True"></asp:Label></td>
                           </tr>
                            <tr>
                           <td >
                               <asp:TextBox ID="txtProjectCo" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td>
                           </td>
                           
                           <td >
                               <asp:TextBox ID="txtFinalCheck" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           </tr>
                            <tr>
                            <td colspan="2" ></td>
                            <td align="right" >
                                <asp:Button ID="btnQuoteSave" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Save" OnClick="btnQuoteSave_Click" />
                                
                                </td>
                                <td>
                                <asp:Button ID="btnClear" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                </td>
                            </tr>
                            
                          
                           <tr>
                           <td colspan="7">
                           
                               
                           </td>
                           </tr>
                    </table>
                      </div>
                        <div class="content" id="tabreportdetails" runat="server">
                            <table>
                               <tr>
                                  <td>
                                      <asp:Button ID="Button1" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Launch Form" OnClick="Button1_Click" Width="99px" Visible="false"/>
                                      <asp:Button ID="btnCustQuote" runat="server" Text="Quote" TabIndex = "4" CssClass="dpbutton" OnClick="btnCustQuote_Click" Visible="false" />
                                  </td>
                               </tr>
                               <tr>
                                   <td style="width: 355px">
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                                        <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true" />
                                   </td>
                               </tr>
                           </table>
                        </div>
                         <div class="content" id="tabNewreportdetails" runat="server">
                            <table>
                               <tr>
                                  <td>
                                      <asp:Button ID="btnLPFrom" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Launch Form" Width="99px" OnClick="btnLPFrom_Click" />
                                      <asp:Button ID="btnLPQuote" runat="server" Text="Quote" TabIndex = "4" CssClass="dpbutton" OnClick="btnLPQuote_Click"/>
                                  </td>
                               </tr>
                           </table>
                        </div>
                         <div class="content" id="tabLoggedEvents" runat="server">
                        <table id="Table4" border="0" cellpadding="2" cellspacing="0">
                            <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:700px">
                                    <img id="img16" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;
                                    <asp:Label ID="Label24" runat="server" Text="Label">Logged Events</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblLogTotalTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvLoggedEvents" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  EmptyDataText="No Data Found.." 
                                        CssClass="lightbackground" Width="836px" ClientIDMode="Static" >
                                        <HeaderStyle CssClass="darkbackground"  />
                                        <AlternatingRowStyle BackColor="#F2F2F2" />
                                        <Columns>
                                            <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Files_Name" HeaderText="File Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFiles" runat="server" Text='<%# Eval("Files_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Pages" HeaderText="Pages" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPages" runat="server" Text='<%# Eval("Pages") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Stage" HeaderText="Stage" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStage" runat="server" Text='<%# Eval("AmendName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="WorkFlow" HeaderText="WorkFlow" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWorkFlow" runat="server" Text='<%# Eval("WorkFlow") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="StartTime" HeaderText="StartTime" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime"  runat="server" Text='<%# Eval("EStartDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EndTime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndTime"  runat="server" Text='<%# Eval("EEndDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Duration">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("TimeDiff") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMPNAME" runat="server" Text='<%# Eval("EMPNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                        <div class="content" id="tabFinalQuote" runat="server">
                            <table  width="1000px">
                                <tr  class="dpJobGreenHeader">
                                    <td  colspan="2">
                                        <img class="dpJobGreenHeader" id="Img17" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="Label25" runat="server" Text="Final Cost"></asp:Label>
                                   </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="imgFQ"  ToolTip="Save" OnClick="imgbtnFinalQuoteSave_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table width="1000px">
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gv_Lmodule" runat="server" AutoGenerateColumns="false" 
                                                AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                                HeaderStyle-CssClass="darkbackground" ShowFooter="true" Width="833px" OnRowDataBound="gv_Lmodule_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:TextBox Width="230px" ID="txt_des" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MPTITLe") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_qty" Width="50px" runat="server" Text='<%# Eval("NUMPAGES") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Rate" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price Code">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_pricecode" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PRICECODE") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PONumber">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_mponumber" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"MOPONUMBER") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_costtype" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"COSTTYPEID") %>'>
                                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_mCategory" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gv_Lmodule1" runat="server" AutoGenerateColumns="false"  Visible="false"
                                                AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                                HeaderStyle-CssClass="darkbackground" ShowFooter="true" Width="833px" OnRowCommand="gv_Lmodule1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label Width="230px" ID="txt_des" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddDesc" runat="server"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_qty" Width="50px" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddQty" runat="server"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_Rate" Width="50px" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddRate" runat="server"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_pricecode" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PRICECODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddPC" runat="server"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PONumber">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_mponumber" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"PONUMBER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddPO" runat="server"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_costtype" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"COSTTYPE") %>'>
                                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="Addddl_costtype" runat="server">
                                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cb_delete" runat="server"/>
                                                    <asp:HiddenField ID="hf_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                                                    <asp:HiddenField ID="hf_FQ_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FQ_ID") %>' />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add"  CssClass="dpbutton" CommandName="Add" />
                                                </FooterTemplate>
                                                <HeaderTemplate>
                                                    <asp:ImageButton ID="ibtn_Delete" ToolTip="Delete" AlternateText="Delete"  OnClick="ibtn_Delete_click" 
                                                        runat="server" ImageUrl="~/images/tools/delete.png" />
                                                </HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabAddQuote" runat="server">
                            <table  width="1000px">
                                <tr  class="dpJobGreenHeader">
                                    <td  colspan="2">
                                        <img class="dpJobGreenHeader" id="Img18" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="Label26" runat="server" Text="Additional Cost"></asp:Label>
                                   </td>
                                    <td>
                                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="imgASave"  ToolTip="Save" OnClick="imgASave_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        Description :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtADesc" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        Quantity :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAQty" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Rate :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtARate" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        Cost :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAcosttype" runat="server">
                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        WO / PO Number :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAPONum" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        Remarks :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtARemarks" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvAQuote" runat="server" AutoGenerateColumns="false" 
                                                AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                                                HeaderStyle-CssClass="darkbackground" ShowFooter="true" Width="833px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label Width="230px" ID="txt_des" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MDesc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_qty" Width="50px" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_Rate" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_pricecode" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PRICECODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PONumber">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txt_mponumber" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"PONUMBER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_costtype" runat="server" Enabled="false" 
                                                            SelectedValue='<%# DataBinder.Eval(Container.DataItem,"CostType") %>'>
                                                            <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
