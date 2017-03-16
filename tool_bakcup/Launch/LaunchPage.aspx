<%@ page maintainscrollpositiononpostback="true" language="C#" autoeventwireup="true" inherits="LaunchPage, App_Web_nq0uy5ub" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Launch Form</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="scripts/tabs1.js"></script>--%>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
<script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="scripts/common.js"></script>
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
</head>
<body>
    <form id="form1" runat="server">
    
        <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
        <div>
            <table width="100%">
                <tr>
                    <td style="background-image: url(images/green-noise-background.png); width: 971px" >
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
                    <asp:DropDownList ID="DDMonthList" runat="server">
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
                &nbsp;&nbsp;<strong>Year</strong>&nbsp;
                    <asp:DropDownList ID="DDYearList" runat="server">
                        <asp:ListItem Value="0">--All--</asp:ListItem>
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                    </asp:DropDownList>
                </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                    </td>
                                    <td colspan="2">
                                        
                                        <asp:HiddenField ID="hfP_ID" runat="server" />
                                        <asp:HiddenField ID="hfP_Name" runat="server" />
                                        &nbsp; &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <strong>&nbsp;</strong>
                        </div>
                    </td>
                </tr>
                <tr>
                    
                    <td  style="background-image: url(images/green-noise-background.png); width: 971px">
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                          
                                <asp:LinkButton ID="lnkGeneral"  runat="server" TabIndex = "4" OnClick="lnkGeneral_Click" Text="General" /></li>
                            <li id="miLaunchDetails" runat="server">
                                <asp:LinkButton ID="lnkLaunchDetails" TabIndex = "4" runat="server" OnClick="lnkLaunchDetails_Click" Text="Job Info" /></li>
                            <li id="miFileInfo" runat="server">
                                <asp:LinkButton ID="lnkFileInfo"   TabIndex = "4" runat="server" OnClick="lnkFileInfo_Click" Text="File Info" /></li>
                            <li id="miCostDetails" runat="server">
                                <asp:LinkButton ID="lnkCostInfo"  TabIndex = "4" runat="server"  Text="Quote Info" OnClick="lnkCostInfo_Click" /></li>
                             <li id="miReportDetails" runat="server">
                                <asp:LinkButton ID="lnkReportInfo"  TabIndex = "4" runat="server"  Text="Preview" OnClick="lnkReportInfo_Click"/></li>
                        </ol>
                        
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);">
                                        <img id="Img8" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblProjectSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                        <td align="right" style="padding-right:10px; background-image: url(images/green-noise-background.png); height: 32px;">
                                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
                                        </td>
                                        
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                       <table>
                                       <tr>
                                       <td align="left">
                                        <asp:GridView ID="GvProject" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground" width="100%" OnRowDataBound="GridView1_RowDataBound"  >
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
                                            
                                            </Columns>
                                        </asp:GridView>
                                       </td>
                                       </tr>
                                       </table>
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabLaunchDetails" runat="server">
                            <div id="CUSTOMER_TABLE" >
                            </div>
                            <div id="PARENT_JOB"   >
                               
                            <%-- <table id="Table3" border="0" width="100%" cellpadding="2" cellspacing="0">
                             <tr>
                             <td>
                             </td></tr></table>--%>
                                <table id="XMLTAGS" border="0" style="background-image: url(images/green-noise-background.png)"  cellpadding="2" cellspacing="4" >
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
                                            <asp:HiddenField ID="hfprojectEditorId" runat="server"  />
                                            
                                             
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
                        <div  class="content"  style="width:950px;" id="tabFileInfo" runat="server">
                          
                                        <table border="0" cellpadding="3"  cellspacing="0" width="90%" style="background-image: url(images/green-noise-background.png)">
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
                                            <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar2.aspx?formname=txtsource','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img1" runat="server" /></td>
                                              <td >
                                                  <asp:Label ID="lblTarDate" runat="server" Text="Target Received on:"></asp:Label>
                                            </td>
                                        <td >
                                            <asp:TextBox ID="txttarget" runat="server" CssClass="TxtBox" Width="101px" TabIndex = "20"></asp:TextBox>
                                            <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar2.aspx?formname=txttarget','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img2" runat="server" />
                                                <asp:CheckBox ID="CheckYTR" Text="YTR" runat="server"  />
                                                </td>
                                            
                                            </tr>
                                            <tr >
                                                <td style="width: 191px">
                                                    <asp:Label ID="lbltarLang" runat="server" Text="Target languages:" Width="119px"></asp:Label>
                                                    </td>
                                                <td >
                                                     <asp:ListBox  Width="162px" ID="lboxlang"  runat="server"  OnSelectedIndexChanged="lboxlang_SelectedIndexChanged" OnPreRender="lboxlang_PreRender" ></asp:ListBox>
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
                                                   <td>
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
                                            <td>
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
                                            <td>
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
                                            <td>
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
                                            <td>
                                                &nbsp;<asp:ListBox ID="ListComplexReason"  Width="264px" runat="server"   SelectionMode="Multiple" OnPreRender="ListComplexReason_PreRender" ></asp:ListBox>
                                              <img id="img4" src="images/tools/new.png" language="javascript" TabIndex = "17"
                                                onclick="return imgReason_editor_onclick()" style="cursor: pointer" title="New Reason" />&nbsp;
                                                <asp:ImageButton src="images/tools/Refresh.png" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click"  />
                                                
                                            </td>
                                            </tr>
                                            <tr>
                                            <td colspan="3"></td>
                                            <td>
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
                                            <td>
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
                                            <td style="height: 27px">
                                                <asp:TextBox ID="txtBleed" runat="server"></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                            
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
                                            <td>
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
                    <asp:TextBox ID="txt_langcount" ReadOnly="true" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LANG_COUNT") %>'></asp:TextBox>
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
                    <asp:TextBox ID="txt_hrate" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"H_RATE") %>'></asp:TextBox>
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
                    <asp:TextBox ID="txt_prate" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"P_RATE") %>'></asp:TextBox>
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
                               <asp:Label ID="lblFinalChk" runat="server" Text="Final Check" Font-Bold="True" Visible="False"></asp:Label></td>
                           </tr>
                            <tr>
                           <td >
                               <asp:TextBox ID="txtProjectCo" runat="server" Enabled="False"></asp:TextBox>
                           </td>
                           <td>
                           </td>
                           <td>
                               <asp:TextBox ID="txtFnlChk" runat="server" Visible="False"></asp:TextBox>
                           </td>
                           <td >
                               <asp:TextBox ID="txtFinalCheck" runat="server" Visible="False"></asp:TextBox>
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
                              <asp:Button ID="Button1" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Launch Form" OnClick="Button1_Click" Width="99px" />
                              <asp:Button ID="btnCustQuote" runat="server" Text="Quote" TabIndex = "4" CssClass="dpbutton" OnClick="btnCustQuote_Click"  />
                          </td>
                           </tr>
                           <tr>
                           <td style="width: 355px">
                           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
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
