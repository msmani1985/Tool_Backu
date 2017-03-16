<%@ page language="C#" autoeventwireup="true" maintainscrollpositiononpostback="true" inherits="ProductionStaff, App_Web_xuje0h3i" %>

<%@ Register Src="~/UserControl/MonthYearPicker.ascx" TagName="MonthYearPicker" TagPrefix="myp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .DatePanel
        {
            position: absolute;
            background-color: #FFFFFF;
            border: 1px solid #646464;
            color: #000000;
            z-index: 1;
            font-family: tahoma,verdana,helvetica;
            font-size: 11px;
            padding: 4px;
            text-align: center;
            cursor: default;
            line-height: 20px;
        }
    </style>
     <link href="default.css" type="text/css" rel="stylesheet" />
     <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="scripts/common.js"></script>
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setHeight(source) {
            var txtContent = source.value;
            if (txtContent.length < 22) {
                source.style.height = 20;
            }
            else if (txtContent.length < 75) {
                source.style.height = ((20 / 22) * (txtContent.length)).toString() + 'px';
            }
            else if (txtContent.length < 150) {
                source.style.height = ((14.8 / 22) * (txtContent.length)).toString() + 'px';
            }
            else {
                source.style.height = ((14 / 22) * (txtContent.length)).toString() + 'px';
            }
        }
    </script>
 <script type="text/javascript">
     var gvelem;
     var gvcolor;
     function setColor(element, id,val, val1) {
         //alert(gvelem);
         if (gvelem != null) {//alert(gvelem.style.backgroundColor);
             gvelem.style.backgroundColor = gvcolor;
         }
         gvelem = element;
         gvcolor = element.style.backgroundColor;
         element.style.backgroundColor = '#C2C2C2';
         document.form1.hfP_ID.value = id;
         document.form1.hfp_EmpID.value = val;
         document.form1.hfP_Name.value = val1

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
    </script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=GvEmp.ClientID %>').Scrollable({
                ScrollHeight: 350
            });
        });
    </script>
    <style type="text/css">
        .auto-style4 {
            width: 231px;
        }
        .auto-style5 {
            width: 42%;
        }
        </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="borderdiv" style="text-align:left;width:900px;">
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="hfP_ID" runat="server" />
                    <asp:HiddenField ID="hfp_EmpID" runat="server" />
                    <asp:HiddenField ID="hfP_Name" runat="server" />
                    <asp:HiddenField ID="hfp_NP_ID" runat="server" />
                </td>
            </tr>
        </table>
        <table style="width: 889px">
            <tr class="dpJobGreenHeader">
                <td colspan="6" style="background-image: url(images/green-noise-background.png)">
                    <img alt="" src="images/tools/search.png" />&nbsp;<strong>Search Employee</strong>
                </td>
            </tr>
            <tr>
                <td>
                    Month Year
                </td>
                <td>
                    <myp:MonthYearPicker ID="mypMonthYear" runat="server" PanelCss="DatePanel" MinYear="2015"
                        MaxYear="2025" MaxMonth="13" MinMonth="1" />
                </td>
                <td>
                    <asp:RadioButtonList ID="rbLocation" runat="server" RepeatDirection="Horizontal" Width="264px" OnSelectedIndexChanged="rbLocation_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="2">Chennai</asp:ListItem>
                        <asp:ListItem Value="3">Coimbatore</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    Employee Name/No.
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtEmp" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" CssClass="dpbutton" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClear1" CssClass="dpbutton" runat="server" Text="Clear" OnClick="btnClear1_Click" />
                </td>
            </tr>
            <%--<tr>
                <td>
                    Month/Year
                </td>
                <td>
                    <asp:TextBox ID="txtMonth" runat="server" Width="33px"></asp:TextBox>/
                    <asp:TextBox ID="txtYear" runat="server" Width="52px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnFilter" CssClass="dpbutton" runat="server" Text="Filter" OnClick="btnFilter_Click" />
                </td>
                <td></td>
                <td>
                    <asp:Label Text="(eg. 10/2014)" ID="exlbl" ForeColor="red" runat="server"></asp:Label>
                </td>
            </tr>--%>
        </table>
    </div>
     <div>
         <table>
             <tr>
                <td>
                    <ol id="toc">
                        <li id="miSummaryDetails" runat="server">
                            <asp:LinkButton ID="LinkSummary" TabIndex = "4" runat="server" OnClick="lnkSummaryDetails_Click"  >Summary</asp:LinkButton></li>
                        <li id="miEmpDetails" runat="server">
                            <asp:LinkButton ID="lnkEmpDetails" TabIndex = "4" runat="server" OnClick="lnkEmpDetails_Click"  >Review</asp:LinkButton></li>
                        <li id="miFeedbackDetails" runat="server">
                            <asp:LinkButton ID="lnkFeedbackDetails" TabIndex = "4" runat="server" OnClick="lnkFeedbackDetails_Click"  >Feedback Details</asp:LinkButton></li>
                       
                    </ol>
                        <div class="borderdiv" id="div_Summary_details" runat="server" style="text-align:center;width:900px;">
                        <table>
                        <tr class="dpJobGreenHeader">
                            <td align="left" style="width: 450px">
                                <asp:Label ID="lblEmployeeSummary" runat="server" Text="Employee Summary"></asp:Label>
                            </td>
                             <td align="center">
                                 &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GvEmp" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground" Width="900px" AllowSorting="True"   OnRowDataBound="GvEmp_RowDataBound" >
                                            <HeaderStyle CssClass="GVFixedHeader"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField SortExpression="sl" HeaderText="Sl. No."  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvslno" runat="server" Width="20" Text='<%# Eval("sl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="empid" HeaderText="Empid"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempid" runat="server" Text='<%# Eval("refno") %>'></asp:Label>
                                                         <br />
                                                        <asp:HiddenField ID="hfgvid" runat="server" Value='<%# Eval("employee_id") %>' />
                                                        <asp:HiddenField ID="hfgvempid" runat="server" Value='<%# Eval("refno") %>' />
                                                        <asp:HiddenField ID="hfgvrefno" runat="server" Value='<%# Eval("empname") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="EmpName" HeaderText="Emp Name"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvempName" runat="server" Width="180" Text='<%# Eval("empname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField SortExpression="Designation" HeaderText="Designation" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDesignation"  runat="server" Text='<%# Eval("designation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Department"  HeaderText="Department" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDepartment"  runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="DOJ" HeaderText="DOJ" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDOJ" Width="90" runat="server" Text='<%# Eval("DOJ") %>'></asp:Label>
                                                         <asp:HiddenField ID="hfgvAppMonth" runat="server" Value='<%# Eval("AppMonth") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="DOL" HeaderText="DOL" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvDOl" Width="90" runat="server" Text='<%# Eval("DOL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="sex" HeaderText="Gender" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgender"  runat="server" Text='<%# Eval("sex") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                 </tr>
                            </table>
                        </div>
                        <div class="borderdiv" id="div_employee_details" runat="server" style="text-align:center;width:900px;" >
                            <table id="editemployee" style="width:100%;text-align:left;" border="1">
                                <tr>
                                    <td class="dpGreenHeader" align="center" colspan="3" style="height: 30px">PRODUCTIVE APPRAISAL FORMS </td>
                                     <td align="right"  class="dpGreenHeader">
                                         <img id="imgHelp" alt="" src="images/wysiwyg_images/help_on.gif" OnClick=" window.open('ProductionHelp.aspx', 'test','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no,width=900,height=800')" style="cursor: pointer" title="Help"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Emp ID:</td>
                                    <td>
                                        <asp:Label ID="txtEmpid" Enabled="false" runat="server" Height="20"></asp:Label>
                                    </td>
                                    <td>Emp Name:</td>
                                    <td>
                                        <asp:Label ID="txtEmpName" Enabled="false" runat="server"></asp:Label>
                                        <asp:Image ID="imgPhoto" runat="server" Width="16px"  Height="16px" Visible="false"/> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>Designation:</td>
                                    <td>
                                        <asp:Label ID="txtDesign" Enabled="false" runat="server" Height="20"></asp:Label>
                                    </td>
                                    <td>Department:</td>
                                    <td>  
                                              
                                        <asp:Label ID="txtDepart" Enabled="false" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        REVIEW DATE:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRecDate" runat="server" Height="16px" Width="105px" ></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtRecDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="img6"  runat="server" />
                                        <%--<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtRecDate" runat="server"></asp:CalendarExtender>--%>
                                    </td>
                                    <td>
                                        APPRAISER NAME:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropAppName" runat="server">
                                        </asp:DropDownList> 
                                       <%-- <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        REPORTING TO:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReportTo" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        REVIEW PERIOD:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReviewPrd" runat="server"  Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width:100%;" border="1">
                                            <tr>
                                                <td>
                                                   <b>A</b>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>PRODUCTION</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>15</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   1
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Achievement of target
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A1" runat="server" OnSelectedIndexChanged="A1_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                     <asp:TextBox ID="A1Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   2
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Work completed on schedule
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A2" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="A2_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="A2Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   3
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Planning, prioritising and decision-making</td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A3_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="A3Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtATotal" Text="0.0"  Width="50" runat="server" Enabled="false" Font-Bold="True"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <b>B</b>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>QUALITY</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>15</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   4
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Internal feedback</td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B4_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="B4Desc" runat="server" TextMode="MultiLine"  onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   5
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Customer feedback</td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B5_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="B5Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   6
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Consistency in quality</td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B6_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="B6Desc" runat="server" TextMode="MultiLine"  onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtBTotal"  Font-Bold="True" Text="0.0"  Width="50" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <b>C</b>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>DISCIPLINE</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>15</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   7
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Leave management<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">(Click)</asp:LinkButton>
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="C7" runat="server" AutoPostBack="true" OnSelectedIndexChanged="C7_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="C7Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   8
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Punctuality<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">(Click)</asp:LinkButton>
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="C8" runat="server" AutoPostBack="true" OnSelectedIndexChanged="C8_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="C8Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   9
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Work place ethics &amp; adherence to policies </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="C9" runat="server" AutoPostBack="true" OnSelectedIndexChanged="C9_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="C9Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtCTotal"  Font-Bold="True" Text="0.0"  Width="50" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <b>D</b>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>ATTITUDE</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>20</b>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                   10
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Responsiveness to requests for service</td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="D10" runat="server" AutoPostBack="true" OnSelectedIndexChanged="D10_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="D10Desc" runat="server" TextMode="MultiLine"  onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   11
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Response to feedback
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="D11" runat="server" AutoPostBack="true" OnSelectedIndexChanged="D11_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="D11Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   12
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Team participation</td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="D12" runat="server" AutoPostBack="true" OnSelectedIndexChanged="D12_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="D12Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    13
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Relationship with managers &amp; colleagues
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="D13" runat="server" AutoPostBack="true" OnSelectedIndexChanged="D13_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="D13Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtDTotal"  Font-Bold="True" Text="0.0" Width="50" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                   <b>E</b>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>TECHNICAL SKILLS</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>25</b>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    14
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Job knowledge &amp; technical expertise
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="E14" runat="server" AutoPostBack="true" OnSelectedIndexChanged="E14_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="E14Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    15
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                   Knowledge sharing and on-the-job training
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="E15" runat="server" AutoPostBack="true" OnSelectedIndexChanged="E15_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="E15Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    16
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Interest to acquire additional training and development
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="E16" runat="server" AutoPostBack="true" OnSelectedIndexChanged="E16_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="E16Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    17
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                   Problem analysis and initiative to streamline processes
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="E17" runat="server" AutoPostBack="true" OnSelectedIndexChanged="E17_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="E17Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    18
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                   Implementation of tools and adherence to workflow procedures
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="E18" runat="server" AutoPostBack="true" OnSelectedIndexChanged="E18_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="E18Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtETotal"  Font-Bold="True" Text="0.0"  Width="50" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   <b>F</b>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>COMMUNICATION SKILLS</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>10</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   19
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Oral communication</td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="F19" runat="server" AutoPostBack="true" OnSelectedIndexChanged="F19_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <asp:TextBox ID="F19Desc" runat="server" TextMode="MultiLine"  onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   20
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Written communication</td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="F20" runat="server" AutoPostBack="true" OnSelectedIndexChanged="F20_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True">0.0</asp:ListItem>
                                                        <asp:ListItem>0.5</asp:ListItem>
                                                        <asp:ListItem>1.0</asp:ListItem>
                                                        <asp:ListItem>1.5</asp:ListItem>
                                                        <asp:ListItem>2.0</asp:ListItem>
                                                        <asp:ListItem>2.5</asp:ListItem>
                                                        <asp:ListItem>3.0</asp:ListItem>
                                                        <asp:ListItem>3.5</asp:ListItem>
                                                        <asp:ListItem>4.0</asp:ListItem>
                                                        <asp:ListItem>4.5</asp:ListItem>
                                                        <asp:ListItem>5.0</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 <td  style="text-align:center;">
                                                    <asp:TextBox ID="F20Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);" />
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtFTotal"  Font-Bold="True" Text="0.0"  Width="50" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">

                                                </td>
                                            </tr>
                                            

                                            <tr>
                                                <td></td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <h4>OVERALL TOTAL</h4>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtOverAll"  Font-Bold="True" Width="70" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td  style="text-align:center;">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <h3>Grade (Appraiser)</h3>
                                                </td>
                                                <td  colspan="2" style="text-align:center;">
                                                    <asp:Label ID="lblGrade"  Font-Bold="True" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                       <u><b>Remarks by Appraiser :</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:TextBox ID="txtAppRemarks" TextMode="MultiLine" runat="server" Width="730px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                       <u><b>Action Plan / Training & Development Goals:(If applicable, summarize and specific projects, performance objectives or training and development for the next review period)</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:TextBox ID="txtActionPlan" TextMode="MultiLine" runat="server" Width="730px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                       <u><b>Employee Comments / Reactions:</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:TextBox ID="txtEmpComments" TextMode="MultiLine" runat="server" Width="730px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="dpbutton" OnClick="btnSave_Click"/>
                                        <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="dpbutton" OnClick="btnClear_Click"/>
                                         <asp:Button ID="btnSubmit" runat="server" Text="Submit"  CssClass="dpbutton" OnClick="btnSubmit_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="borderdiv" id="div_FeedBack_details" runat="server" style="text-align:center;width:900px;" >
                            <table id="editemployee" style="width:100%;text-align:left;" border="1">
                                <tr>
                                    <td class="dpGreenHeader" align="center" colspan="4" style="height: 30px">FeedBack</td>
                                </tr>
                                <tr>
                                    <td align="left">Emp ID:</td>
                                    <td align="left">
                                        <asp:Label ID="lblFEmpId" Enabled="false" runat="server" Height="20"></asp:Label>
                                    </td>
                                    <td align="left">Emp Name:</td>
                                    <td align="left">
                                        <asp:Label ID="lblFEmpName" Enabled="false" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <u><b>Positive:</b></u>
                                    </td>
                                    <td colspan="2">
                                       <u><b> Negative:</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:TextBox ID="txtPositive" TextMode="MultiLine" runat="server" Width="300px" Height="70px"></asp:TextBox>
                                    </td>
                                    <td colspan="2" align="center">
                                       <asp:TextBox ID="txtNegative" TextMode="MultiLine" runat="server" Width="300px" Height="70px"></asp:TextBox>
                                    </td>
                                </tr>
                               
                                <tr>
                                   <td colspan="4" align="center">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add"  CssClass="dpbutton" OnClick="btnAdd_Click"/>
                                    </td>
                                </tr>
                                 <tr>
                                    <td  colspan="4" align="center">
                                        <asp:GridView ID="gvFeedback" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                            CssClass="lightbackground" Width="500px" AllowSorting="True">
                                            <HeaderStyle CssClass="GVFixedHeader"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                <asp:TemplateField SortExpression="sl" HeaderText="Sl.No."  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvslno" runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Positive" HeaderText="Positive"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPositive" runat="server" Text='<%# Eval("Positive") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" Wrap="True" />
                                                    <ItemStyle Width="200px" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Negative" HeaderText="Negative"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNegative" runat="server" Text='<%# Eval("Negative") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" Wrap="True" />
                                                    <ItemStyle Width="200px" Wrap="True" />
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
