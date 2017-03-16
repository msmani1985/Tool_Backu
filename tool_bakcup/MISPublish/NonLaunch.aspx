<%@ page language="C#" autoeventwireup="true" inherits="NonLaunch, App_Web_zfrrxy20" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Non Launch Form</title>
    <link href="scripts/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.ui.timepicker.js" type="text/javascript"></script>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>

    <script type = "text/javascript">
        function BlockUI(elementID)
        {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function ()
            {
                $("#" + elementID).block(
                    {
                    message: '<table align = "center"><tr><td>' +
                            '<img src="images/loadingAnim.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS:
                        {
                            backgroundColor: '#000000', opacity: 0.6
                        }
                    }
                );
            });
            prm.add_endRequest(function ()
            {
                $("#" + elementID).unblock();
            });
        }
       <%-- $(document).ready(function () {

            BlockUI("<%=pnlAddEdit.ClientID %>");
            $.blockUI.defaults.css = {};
        });--%>
        function Hidepopup()
        {
            $find("popup").hide();
            return false;
        }

       <%--function CheckAll(Checkbox)
        {
            var gvJobTrack = document.getElementById("<%=gvJobTrack.ClientID %>");
            for (i = 1; i < gvJobTrack.rows.length; i++)
            {
                gvJobTrack.rows[i].cells[9].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }--%>
    </script> 
    
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
            document.form1.hfP_ID.value = val;
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
        function imgBD_editor_onclick()
        {
            if (document.form1.drpProjectcustomer != null && document.form1.drpProjectcustomer.value != "0")
                window.open("NonLaunchContacts.aspx?form=Projects&type=0&trgname=txtProjectEditor&trgid=hfprojectEditorId&cid=" + document.form1.drpProjectcustomer.value + "&lid=" + document.form1.DropLocation.value, "Contacts", "width=800,height=600,status=yes, scrollbars=yes");
            else alert("Select a customer");
        }
        $('#timepicke').timepicker({
            hours: { starts: 6, ends: 19 },
            minutes: { interval: 15 },
            rows: 3,
            showPeriodLabels: true,
        minuteText: 'Min'
        });
    </script>
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
        
         .auto-style1 {
             width: 287px;
         }
         .auto-style2 {
             width: 874px;
         }
         .auto-style3 {
             width: 322px;
         }
         .auto-style4 {
             width: 209px;
         }
        
         .auto-style5 {
             width: 125px;
         }
         .auto-style6 {
             width: 275px;
         }
         .auto-style7 {
             width: 482px;
         }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
        <table>
            <tr>
                <td>
                    <div>
                        <table class="content">
                            <tr class="dpJobGreenHeader">
                                <td colspan="3">
                                    <img alt="" src="images/tools/search.png" />&nbsp;<strong>Search Project</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Customer</strong>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="325px" TabIndex = "1"></asp:DropDownList>&nbsp;&nbsp;
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex = "4" CssClass="dpbutton" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnNew" runat="server" Text="New" TabIndex = "4" CssClass="dpbutton" OnClick="btnNew_Click"   />
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
                                    </asp:DropDownList>&nbsp;&nbsp;
                                    <strong>Year</strong>&nbsp;
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
                                <td></td>
                                <td>                    
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <ol id="toc">
                        <li id="miGeneral" runat="server">
                            <asp:LinkButton ID="lnkGeneral" runat="server" TabIndex="1" OnClick="lnkGeneral_Click">General</asp:LinkButton></li>
                        <li id="miLaunchDetails" runat="server">
                            <asp:LinkButton ID="lnkLaunchdetails" runat="server" TabIndex="2" OnClick="lnkLaunchdetails_Click">Launch Details</asp:LinkButton></li>
                        <li id="miFileDetails" runat="server">
                            <asp:LinkButton ID="lnkFiledetails" runat="server" TabIndex="3" OnClick="lnkFiledetails_Click" >File Details</asp:LinkButton></li>
                        <li id="miJobTracking" runat="server">
                            <asp:LinkButton ID="lnkJobTracking" runat="server" TabIndex="4" OnClick="lnkJobTracking_Click">Job Tracking</asp:LinkButton></li>
                        <li id="miLoggedEvent" runat="server">
                            <asp:LinkButton ID="lnkLoggedEvent" runat="server" TabIndex="4" OnClick="lnkLoggedEvent_Click">Logged Events</asp:LinkButton></li>
                    </ol>
                    <div class="content" id="tabGeneral" runat="server">
                        <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:900px">
                                        <img id="Img8" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblProjectSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                        <td align="right" style="padding-right:10px; background-image: url(images/green-noise-background.png); height: 32px;">
                                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
                                        <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server" ToolTip="Save"  TabIndex="41" OnClick="cmd_Save_Click" />
                                    </td>
                                </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GvNL" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground"  OnRowDataBound="GvNL_RowDataBound" OnRowCommand="GvNL_RowCommand" Width="931px" >
                                            <HeaderStyle CssClass="darkbackground"  />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                            <Columns>
                                                 <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                               <br />
                                                        <asp:HiddenField ID="hfgvNLID" runat="server" Value='<%# Eval("NL_ID") %>' />
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
                                                        <asp:Label ID="lblgvPDueTimeFrom" runat="server" Text='<%# Eval("due_dateTO") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="Status" ItemStyle-Width="50"  ItemStyle-HorizontalAlign="Center">
                                                     <ItemTemplate>
                                                         <asp:DropDownList ID="DropStatus" Width="60"  runat="server">
                                                         </asp:DropDownList>
                                                     </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                   <ItemTemplate>
                                                        <asp:ImageButton ID="BtnSave" AlternateText="Save" ToolTip="Save" ImageUrl="~/images/tools/yes.png" runat="server" 
                                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' CommandName="Save"/>
                                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                </td>
                            </tr>
                         </table>
                    </div>
                    <div class="content" id="tabNonLaunch" runat="server">
                             <table id="Table5" border="0" cellpadding="2" cellspacing="0">
                                        <tr bgcolor="#f0fff0">
                                        <td class="dpJobGreenHeader" colspan="4">
                                            <img id="imgNLHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblNLHeader" runat="server" Text="Label">New Launch</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">
                                            <asp:Label ID="Label1" runat="server" Text="Project Title:"></asp:Label>
                                            <span style="font-size: 9pt; color: #ff0000">*</span>
                                        </td>
                                        <td class="auto-style7" >
                                            <asp:TextBox ID="txtProjectTitle" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label11" runat="server" Text="Job ID:"></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtJobid" Enabled="false" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">Customer:<span  style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td class="auto-style7" >
                                            <asp:DropDownList ID="drpProjectcustomer" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drpProjectcustomer_SelectedIndexChanged">
                                            </asp:DropDownList>           
                                        </td>
                                        <td class="auto-style4">Location:</td>
                                        <td class="auto-style2">        
                                            <asp:DropDownList ID="DropLocation" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DropLocation_SelectedIndexChanged"></asp:DropDownList>
                                            <img  alt="Location" border="0" height="20" onclick="javascript:calendar_window=window.open('NonLaunchLocation.aspx?formname=txtProjectEditor','calendar_window','width=750,height=250,left=350,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/tools/new.png" style="cursor: pointer;" id="img3"  runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style6">
                                            Project Editor:
                                        </td>
                                        <td class="auto-style7" >
                                            <asp:TextBox ID="txtProjectEditor" runat="server" CssClass="TxtBox" Width="180px"></asp:TextBox>
                                            <img id="imgBD_editor" src="images/tools/user_go.png" language="javascript" runat="server"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select PE"/>
                                            <asp:HiddenField ID="HiddenField1" runat="server"  />
                                            </td>
                                        <td class="auto-style4">
                                            <asp:Label ID="TarRecDate" runat="server" Text="Target Rec. Date:"></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtRecDate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtRecDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img9"  runat="server" />
                                        </td>
                                    </tr>
                                <tr>
                                    <td class="auto-style6">
                                        Due Date:
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblDueFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtdueFromdate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueFromdate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img6"  runat="server" />
                                        <asp:Label ID="lblDueTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtdueTodate" runat="server" CssClass="TxtBox" Visible="false" Width="80px"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueTodate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="calenderTo"  runat="server" visible="false" />
                                        <asp:CheckBox ID="chkYTC" Text="YTC" runat="server" />
                                        <asp:CheckBox ID="chkDueDate" Text="Staggered Delivery" runat="server" AutoPostBack="True" OnCheckedChanged="chkDueDate_CheckedChanged" ClientIDMode="AutoID" />
                                        </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" >Due Time:</td>
                                    <td class="auto-style3" colspan="3">
                                        <asp:Label ID="lblFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="DropDueTimeFrom" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeFrom_SelectedIndexChanged" >
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
                                        </asp:DropDownList><asp:DropDownList ID="DropDueMinFrom" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMinFrom_SelectedIndexChanged" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="DropDueTimeZoneFrom" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneFrom_SelectedIndexChanged"  >
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
                                        <asp:DropDownList ID="DropDueTimeTo" runat="server" Width="40px" AutoPostBack="True"  Visible="False" OnSelectedIndexChanged="DropDueTimeTo_SelectedIndexChanged" >
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
                                        </asp:DropDownList><asp:DropDownList ID="DropDueMinTo" runat="server" Width="40px" AutoPostBack="True"  Visible="False" OnSelectedIndexChanged="DropDueMinTo_SelectedIndexChanged" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="DropDueTimeZoneTo" runat="server" Width="50px" AutoPostBack="True"  Visible="False" OnSelectedIndexChanged="DropDueTimeZoneTo_SelectedIndexChanged" >
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
                                        <asp:CheckBox ID="chkDueTime" Text="Staggered Delivery" runat="server" AutoPostBack="True" Width="133px" OnCheckedChanged="chkDueTime_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;IST Time :&nbsp;&nbsp;
                                        <asp:Label ID="lblIndFrom" runat="server" Text="(From)" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtIndFrom" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblIndTo" runat="server" Text="(To)" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtIndTo" runat="server" Width="80px" Visible="False" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6">
                                        Task:
                                    </td>
                                    <td class="auto-style7" >
                                        <asp:ListBox ID="lboxtask" Width="130px" runat="server" SelectionMode="Multiple" AutoPostBack="True" OnSelectedIndexChanged="lboxtask_SelectedIndexChanged" ></asp:ListBox>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblformat" runat="server" Text="Format:"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:ListBox ID="lboxformat" Width="130px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" >Input Files Received:</td>
                                    <td class="auto-style7" >
                                        <asp:ListBox ID="linputfile" runat="server" SelectionMode="Multiple" Width="130px">
                                            <asp:ListItem Value="FTP">FTP</asp:ListItem>
                                            <asp:ListItem Value="Mail Attachment">Mail Attachment</asp:ListItem>
                                            <asp:ListItem Value="Skype">Skype</asp:ListItem>
                                            <asp:ListItem Value="DropBox">DropBox</asp:ListItem>
                                            <asp:ListItem Value="LanguageDirector">LanguageDirector</asp:ListItem>
                                            <asp:ListItem Value="Client portal">Client portal</asp:ListItem>
                                        </asp:ListBox>&nbsp;
                                    </td>
                                    <td class="auto-style4">
                                        <asp:Label ID="lblsource" runat="server" Text="Source Type:"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="DropSource" runat="server">
                                                    <asp:ListItem ></asp:ListItem>
                                                    <asp:ListItem>Editable</asp:ListItem>
                                                    <asp:ListItem>Scanned</asp:ListItem>
                                                    <asp:ListItem Value="Editable and Scanned">Editable and Scanned </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                <td class="auto-style6" > Platform:</td>
                                    <td class="auto-style7" >
                                        <asp:DropDownList ID="dropSwPlat" runat="server" >
                                                <asp:ListItem Value="1">MAC</asp:ListItem>
                                                <asp:ListItem Value="2">PC</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style4">
                                        <asp:Label ID="tempID" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="auto-style5" colspan="2">
                                        <asp:Label ID="lbltarLang" runat="server" Text="Languages & Software Details:"  Width="120px"></asp:Label>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" >

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
                                                            <asp:CheckBoxList  Width="160px" ID="lboxlang" SelectionMode="Multiple" runat="server"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <%--<div style="width:100px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">--%>
                                                            <asp:CheckBoxList ID="CheckBoxTask" runat="server" Width="70px" Height="16px"></asp:CheckBoxList>
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
                                                            <asp:Button CssClass="dpbutton" ID="btnlangadd" runat="server" Text="Add" OnClick="btnlangadd_Click" />
                                                            &nbsp;
                                                            <asp:Button CssClass="dpbutton" ID="btnlangdel" runat="server" Text="Remove" OnClick="btnlangdel_Click" />
                                                            <asp:Button ID="xlang" TabIndex = "4" CssClass="dpbutton" runat="server" Text="" 
                                                                Visible="true" style="display:none;"  OnClick="xlang_Click"/>
                                                        </td>
                                                        <td>
                                                            <div style="width:180px; height:100px; padding:2px; overflow:auto; border: 1px solid #ccc;">
                                                            <asp:CheckBoxList id="lboxlangused" runat="server" Width="160px"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                             </ContentTemplate>
                                         </asp:UpdatePanel>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td class="auto-style6" >

                                    </td>
                                     <td class="auto-style7">
                                         <asp:LinkButton ID="lnkSWDetails" runat="server" OnClick="lnkSWDetails_Click1">Software Details : Source & Target</asp:LinkButton>
                                     </td>
                                </tr>
                                <tr>
                                    <td class="auto-style6" >
                                        <asp:Label ID="lblSWVerUsed" runat="server" Text="Software &amp; Version to be Used:"  Width="120px" Visible="false"></asp:Label>
                                    </td>
                                    <td class="auto-style1" colspan="3" >

                                        <asp:GridView ID="gv_Soft"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                        CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_Soft_RowDataBound" Width="250px" Height="141px" Visible="false">
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
                                                            <asp:ListBox  ID="lboxSoft" AutoPostBack="true"  SelectionMode="Multiple"  OnSelectedIndexChanged="lboxSoft_SelectedIndexChanged" runat="server" ></asp:ListBox>
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
                                        <asp:TextBox ID="txtFiles" runat="server" Visible="False"></asp:TextBox>
                                    &nbsp;</td></tr>
                                <tr><td colspan="4"></td></tr>
                            
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnJobInfo" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Save" OnClick="cmd_Save_Launch_Click"/>
                                        <%--<asp:Button ID="btnJobClear" TabIndex = "4" CssClass="dpbutton" runat="server" Text="Clear" OnClick="btnFileInfo_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    <div class="content" id="tabFileDetails" runat="server">
                        <table id="Table4" border="0" cellpadding="2" cellspacing="0">
                            <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:900px">
                                    <img id="img1" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;
                                    <asp:Label ID="Label2" runat="server" Text="Label">File Information</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Target File Name Convention:&nbsp;&nbsp;
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
                                                            <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
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
                                            <asp:Label ID="Label3" runat="server" Text="File Name & Pages Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
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
                                                        <asp:Label ID="Label14" runat="server" Visible="false" Text=""></asp:Label>
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
								                            <asp:HiddenField ID="hid_LP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
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
                                <%--<tr>
                                    <td colspan="4" class="auto-style2">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="chkDueDate1_CheckedChanged">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvFileInfo" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                                 CssClass="lightbackground" width="85%" OnRowDataBound="gvFileInfo_RowDataBound" OnRowCommand="gvFileInfo_RowCommand" >
                                                    <HeaderStyle CssClass="darkbackground"  />
                                                    <AlternatingRowStyle BackColor="#F2F2F2" />
                                                    <Columns>
                                                        <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                                                <asp:HiddenField ID="hid_NL_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
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

                                                <asp:Panel ID="pnlAddEdit" Width="700" Height="320" runat="server" CssClass="modalPopup" style = "display:none">
                                                <asp:Label ID="Label3" runat="server" Text="File Name & Pages Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <br />
                                                <table align = "center">
                                                   <tr>
                                                       <td>
                                                           <asp:Label ID="NL_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="NTLS_ID" runat="server" Visible="false" Text=""></asp:Label>
                                                           <asp:Label ID="txtFiles" runat="server" Visible="false" Text=""></asp:Label>
                                                       </td>
                                                        <td>
                                                             <asp:GridView ID="gv_FilePages"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                                                      CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_FilePages_RowDataBound" Width="250px">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="File No." >
                                                                            <ItemTemplate>
                                                                                <asp:Label Width="60" Enabled="false" ID="lbl_File"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Files_ID") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hid_NL_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
                                                                                <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                                                            </ItemTemplate>
                                                                        <HeaderStyle Wrap="False" />
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
                                                                        <asp:TemplateField HeaderText="File Name">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt_Name" Width="200" runat="server" Text='<%# Eval("Files_name") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pages">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt_Pages" Width="60" runat="server" Text='<%# Eval("Pages") %>'></asp:TextBox>
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
                                                           <asp:Button ID="btnSave" runat="server" Text="Save" OnClick = "Save"  CssClass="dpbutton"/>
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
                                                <asp:AsyncPostBackTrigger ControlID = "btnSave" />
                                            </Triggers> 
                                        </asp:UpdatePanel> 
                                    </td>
                                </tr>--%>
                        </table>
                    </div>
                    <div class="content" id="tabJobTracking" runat="server">
                        <table id="Table4" border="0" cellpadding="2" cellspacing="0">
                            <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:900px">
                                    <img id="img2" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;
                                    <asp:Label ID="Label4" runat="server" Text="Label">Job Tracking</asp:Label>
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
                                                        <asp:HiddenField ID="hid_NL_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
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
                                                <asp:Label ID="Label8" runat="server" Text="Delivery Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <table align = "center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="Project Title:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="DelProName" runat="server" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" Text="Job ID:"></asp:Label>
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
                                                        <%--<td>
                                                            Stage:
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lboxDelSatge" runat="server" SelectionMode="Multiple" Width="130px"></asp:ListBox>
                                                        </td>--%>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4"> 
                                                            Delivery Time:
                                                            <asp:TextBox ID="txtdeldateAll" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                            <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdeldateAll','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                                src="images/Calendar.jpg" style="cursor: pointer;" id="img4"  runat="server" />
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
                                                                <asp:ListItem Value="CET">CET</asp:ListItem>
                                                                <asp:ListItem Value="CST">CST</asp:ListItem>
                                                                <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                                <asp:ListItem Value="EST">EST</asp:ListItem>
                                                                <asp:ListItem Value="JST">JST</asp:ListItem>
                                                                <asp:ListItem Value="BST">BST</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="Label12" runat="server" Text="Delivery Time IST"></asp:Label>
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
                                                <asp:Label ID="Label5" runat="server" Text="File Name & Pages Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                                <table align = "center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Project Title:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtProjectTitle1" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:TextBox ID="txtFP_ID" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Job ID:"></asp:Label>
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
                                                                        src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueFromdate"  runat="server" />
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
                    <div class="content" id="tabLoggedEvents" runat="server">
                        <table id="Table4" border="0" cellpadding="2" cellspacing="0">
                            <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px; background-image: url(images/green-noise-background.png);width:900px">
                                    <img id="img5" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;
                                    <asp:Label ID="Label13" runat="server" Text="Label">Logged Events</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="gvLoggedEvents" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  EmptyDataText="No Data Found.." 
                                        CssClass="lightbackground" Width="874px" ClientIDMode="Static" >
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
                                            
                </td>
            </tr>
        </table>
    </div>
                                    <asp:HiddenField ID="hfP_ID" runat="server"/>
                                    <asp:HiddenField ID="hfP_Name" runat="server"/>
                                    <asp:HiddenField ID="hfprojectEditorId" runat="server"/>
    </form>
</body>
</html>
