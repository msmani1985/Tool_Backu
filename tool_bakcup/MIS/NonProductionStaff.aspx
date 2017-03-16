<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NonProductionStaff.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="NonProductionStaff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
     function imgHelp() {
         alert("55555");
         window.open("NonProductionHelp.aspx", "Contacts", "width=800,height=600,status=no, scrollbars=yes");
     }
    </script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=GvEmp.ClientID %>').Scrollable({
                ScrollHeight: 350
            });
        });

        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }


        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('TextBox1');
            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
            else {
                //Condition to check textbox contains ten numbers or not
                if (phn.value < 5) {
                    
                    return true;
                }
                else {
                    alert("Enter number must be less then 5");
                    return false;
                }
            }
        }
        function val(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('TextBox1');
            //comparing pressed keycodes
            //if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            //    return false;
            //}
            //else {
                //Condition to check textbox contains ten numbers or not
                if (phn.value < 5) {

                    return true;
                }
                else {
                    alert("Enter number must be less then 5");
                    return false;
                }
            }
        }
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
                    <%--Month Year--%>
                </td>
                <td>
                    <myp:MonthYearPicker ID="mypMonthYear" runat="server" PanelCss="DatePanel" MinYear="2015"
                        MaxYear="2025" MaxMonth="13" MinMonth="1"  Visible="false" />
                </td>
                <td>
                    <asp:RadioButtonList ID="rbLocation" Visible="false" runat="server" RepeatDirection="Horizontal" Width="264px" OnSelectedIndexChanged="rbLocation_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="2">Chennai</asp:ListItem>
                        <asp:ListItem Value="3" Selected="True">Coimbatore</asp:ListItem>
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
                                                        <asp:HiddenField ID="hfReviewPeriod" runat="server" Value='<%# Eval("ReviewPeriod") %>' />
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
                                                <asp:TemplateField SortExpression="ReviewPeriod" HeaderText="ReviewPeriod" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRevPer"  runat="server" Text='<%# Eval("ReviewPeriod") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                 </tr>
                            </table>
                        </div>
                        <div class="borderdiv" id="div_employee_details" runat="server" style="text-align:center;width:900px;" >
                            <table id="editemployee" style="width:100%;" border="1">
                                <tr>
                                    <td class="dpGreenHeader" align="center" colspan="3" style="height: 30px">NON-PRODUCTIVE APPRAISAL FORMS</td>
                                     <td align="right"  class="dpGreenHeader">
                                         <img id="imgHelp" alt="" src="images/wysiwyg_images/help_on.gif" OnClick=" window.open('NonProductionHelp.aspx', 'test','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no,width=900,height=800')" style="cursor: pointer" title="Help"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Emp ID:</td>
                                    <td align="left">
                                        <asp:Label ID="txtEmpid" Enabled="false" runat="server" Height="20"></asp:Label>
                                    </td>
                                    <td align="left">Emp Name:</td>
                                    <td align="left">
                                        <asp:Label ID="txtEmpName" Enabled="false" runat="server"></asp:Label>
                                        <asp:Image ID="imgPhoto" runat="server" Width="16px"  Height="16px" Visible="false"/> 
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">Designation:</td>
                                    <td align="left">
                                        <asp:Label ID="txtDesign" Enabled="false" runat="server" Height="20"></asp:Label>
                                    </td>
                                    <td align="left">Department:</td>
                                    <td align="left">            
                                        <asp:Label ID="txtDepart" Enabled="false" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        REVIEW DATE:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRecDate" runat="server" Height="16px" Width="105px" ></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtRecDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="img6"  runat="server" />
                                        <%--<asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtRecDate" runat="server"></asp:CalendarExtender>--%>
                                    </td>
                                    <td align="left">
                                        APPRAISER NAME:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="dropAppName" runat="server">
                                        </asp:DropDownList> 
                                       <%-- <asp:TextBox ID="txtAppName" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        REPORTING TO:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtReportTo" runat="server" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        REVIEW PERIOD:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtReviewPrd" runat="server" Enabled="false"></asp:TextBox>
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
                                                    <b>LEADERSHIP SKILLS</b>
                                                </td>
                                                <td  style="text-align:center;">
                                                    <b>APPRAISER</b>
                                                </td>
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <b>COMMENT BY APPRAISER</b>
                                                </td>
                                                <td>
                                                    <b>55</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   1
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Written Communication
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
                                                   <%-- <asp:TextBox ID="TextBox1"  onkeypress="return val(event)" runat="server"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                                        ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                        ControlToValidate="TextBox1" />--%>
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
                                                    Oral Communication
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
                                                    <asp:TextBox ID="A2Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   3
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Team Building
                                                </td>
                                               
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
                                                   4
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Assigning Responsibilities
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A4_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A4Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   5
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Decision Making
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A5_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A5Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   6
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Problem Solving
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A6_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A6Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   7
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Production Planning and Allocation
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A7" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A7_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A7Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   8
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Change Management
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A8" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A8_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A8Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   9
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Quality Management
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A9" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A9_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A9Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   10
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    MIS Management
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A10" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A10_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A10Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   11
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Production / Job Tracking
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="A11" runat="server" AutoPostBack="true" OnSelectedIndexChanged="A11_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="A11Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtATotal" Text="0.0" Width="50" runat="server" Enabled="false"  Font-Bold="True"></asp:TextBox>
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
                                                   12
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Flexible & Adaptable
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B12" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B12_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="B12Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   13
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Self Initiative, Process Improvement, Team Participation
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B13" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B13_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="B13Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   14
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Relationship with Managers / Colleagues
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B14" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B14_SelectedIndexChanged">
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
                                                   <asp:TextBox ID="B14Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   15
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Response to Comments
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="B15" runat="server" AutoPostBack="true" OnSelectedIndexChanged="B15_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="B15Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtBTotal" Text="0.0" Width="50" runat="server" Enabled="false"  Font-Bold="True"></asp:TextBox>
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
                                                    <b>GENERAL</b>
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
                                                   16
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                    Job Knowledge & Technical Expertise
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="C16" runat="server" AutoPostBack="true" OnSelectedIndexChanged="C16_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="C16Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   17
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                   Knowledge Transfer
                                                </td>
                                               
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="C17" runat="server" AutoPostBack="true" OnSelectedIndexChanged="C17_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="C17Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   18
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                   Work place Ethics & Adherences to Policies
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="C18" runat="server" AutoPostBack="true" OnSelectedIndexChanged="C18_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="C18Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtCTotal" Text="0.0"  Width="50" runat="server" Enabled="false"  Font-Bold="True"></asp:TextBox>
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
                                                    <b>ATTENDANCE</b>
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
                                                    Leave Management<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">(Click)</asp:LinkButton>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="D19" runat="server" AutoPostBack="true" OnSelectedIndexChanged="D19_SelectedIndexChanged">
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
                                                   <asp:TextBox ID="D19Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                   20
                                                </td>
                                                <td style="text-align:left;" class="auto-style5">
                                                   Punctuality<asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">(Click)</asp:LinkButton>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:DropDownList ID="D20" runat="server" AutoPostBack="true" OnSelectedIndexChanged="D20_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="D20Desc" runat="server" TextMode="MultiLine" onkeyup="setHeight(this);" onkeydown="setHeight(this);"/>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="text-align:center;" class="auto-style5">
                                                    <b>Total</b>
                                                </td>
                                                
                                                <td  style="text-align:center;" class="auto-style4">
                                                    <asp:TextBox ID="txtDTotal" Text="0.0"  Width="50" runat="server" Enabled="false"  Font-Bold="True"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtOverAll"  Width="70" runat="server" Enabled="false" Font-Bold="True"></asp:TextBox>
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
                                                    <asp:Label ID="lblGrade" runat="server" Text=""  Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <u><b>Strength :</b></u>
                                    </td>
                                    <td colspan="2" align="left">
                                        <u><b>Weakness :</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:TextBox ID="txtStrength" TextMode="MultiLine" runat="server" Width="350px" Height="100px"></asp:TextBox>
                                    </td>
                                    <td colspan="2" align="center">
                                        <asp:TextBox ID="txtWeakness" TextMode="MultiLine" runat="server" Width="350px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td  align="left"  colspan="4">
                                       <u><b>Remarks By Appraiser :</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:TextBox ID="txtAppRemarks" TextMode="MultiLine" runat="server" Width="730px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"  align="left">
                                       <u><b>Action Plan / Training & Development Goals:(If applicable, summarize and specific projects, performance objectives or training and development for the next review period)</b></u>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:TextBox ID="txtActionPlan" TextMode="MultiLine" runat="server" Width="730px" Height="100px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"  align="left">
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
