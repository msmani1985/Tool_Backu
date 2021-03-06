﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_Launch_Initial.aspx.cs" Inherits="LP_Launch_Initial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/common.js"></script>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="datetimepicker.js">
    </script>
    
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
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
            if (document.getElementById("lblProjectSummary"))
                document.getElementById("lblProjectSummary").innerText = "Project : " + val1;
            else if (document.all.lblProjectSummary)
                document.all.lblProjectSummary.innerText = "Project : " + val1;
            else if (document.form1.lblProjectSummary)
                document.form1.lblProjectSummary.innerText = "Project : " + val1;
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
        function openModal() {
            document.getElementById('divPopModule').style.visibility = 'visible';
            document.getElementById('divPopModule').style.display = ''; document.getElementById('divPopModule').style.bottom = '150px';
            document.getElementById('divPopModule').style.left = '280px';
        }
        function NewimgBD_editor_onclick() {

            if (document.form1.drpPCustomer != null && document.form1.drpPCustomer.value != "0")
                window.open("NonLaunchcontacts.aspx?form=Projects&type=0&trgname=txtProjectEditor&trgid=hfprojectEditorId&cid=" + document.form1.drpPCustomer.value + "&lid=" + document.form1.drpPLoc.value, "Contacts", "width=800,height=600,status=yes, scrollbars=yes");
            else alert("Select a customer");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
     <div class="dptitle" id="invtitle" runat="server" >Initial Launch</div>
        <div>
            <table>
                <tr>    
                    <td  style="background-image: url(images/green-noise-background.png); width: 850px">
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral"  runat="server" TabIndex = "1" OnClick="lnkGeneral_Click" Text="General" /></li>
                            <li id="miJobInfo" runat="server">
                                <asp:LinkButton ID="lnkJobInfo" TabIndex = "2" runat="server" Text="Launch" OnClick="lnkJobInfo_Click"/></li>
                            <li id="miRecAmends" runat="server">
                                <asp:LinkButton ID="lnkRecAmends" TabIndex = "2" runat="server" Text="Received Amends" OnClick="lnkRecAmends_Click"/></li>
                        </ol>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content" id="tabGeneral" runat="server">
            <table id="Table5">
                <tr class="dpJobGreenHeader">
                    <td  style="background-image: url(images/green-noise-background.png);">
                        <img id="Img8" src="images/tools/information.png" runat="server" />
                        <asp:Label ID="lblProjectSummary" runat="server" Text="Initial Launch Summary"></asp:Label>
                        <asp:HiddenField ID="hfP_ID" runat="server"/>
                        <asp:HiddenField ID="hfP_Name" runat="server" />
                    </td>     
                </tr>
                <tr>
                    <td>
                         <asp:GridView ID="GvProject" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                         CssClass="lightbackground" width="100%" OnRowDataBound="GvProject_RowDataBound">
                            <HeaderStyle CssClass="darkbackground"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                            <Columns>
                                <asp:TemplateField SortExpression="parent_job_id" HeaderText="Sl.No" >
                                    <ItemTemplate>
                                            <asp:Label ID="lblJOB" runat="server" Text='<%# id++ %>' ></asp:Label>
                                            <br />
                                    <asp:HiddenField ID="hfgvProjectID" runat="server" Value='<%# Eval("LI_ID") %>' />
                                    <asp:HiddenField ID="hfgvProjectname" runat="server" Value='<%# Eval("projectname") %>' />
                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("LP_ID") %>' />
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
                                 <asp:TemplateField SortExpression="Editor" HeaderText="Project Editor" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPEditor" runat="server" Text='<%# Eval("PEName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="received_date"  HeaderText="Quote Rec. Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("RecDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Due_Date" HeaderText="Quote Due Date"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDueDate" runat="server" Text='<%# Eval("DueDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Send_Date" HeaderText="Quote Send Date"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSendDate" runat="server" Text='<%# Eval("SendDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
            <script type="text/javascript" src="Scripts/jquery.min.js"></script>
            <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
            <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    gridviewScroll();
                });

                function gridviewScroll() {
                    $('#<%=GvProject.ClientID%>').gridviewScroll({
                        width: window.innerWidth - 10,
                        height: window.innerHeight - 120,
                        startHorizontal: 0,
                        barhovercolor: "#848484",
                        barcolor: "#848484"
                    });
                }
            </script>
        </div>
        <div class="content" id="tabLaunch" runat="server">
            <table  width="1000px">
                <tr  class="dpJobGreenHeader">
                    <td  colspan="2">
                        <img id="imgNLHeader" align="absmiddle" src="images/tools/new.png" runat="server" />
                        <asp:Label ID="lblNLHeader" runat="server" Text="Initial Launch"></asp:Label>
                    </td>
                    <td>
                        <asp:ImageButton ImageUrl="images/tools/j_save.png" runat="server" ID="imgbtnSave"  ToolTip="Save" OnClick="imgbtnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                       
                    </td>
                </tr>
            </table>
            <table width="1000px">
                <tr>
                    <td>
                        Project Name:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtProjectName" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Customer:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpPCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPCustomer_SelectedIndexChanged"   Width="200px"></asp:DropDownList>
                    </td>
                    <td>
                        Location:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpPLoc" runat="server"  Width="200px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        PE Name:
                    </td>
                    <td>
                       <asp:TextBox ID="txtProjectEditor" runat="server" CssClass="TxtBox" Width="200px"></asp:TextBox>
                        <img id="NewimgBD_editor" src="images/tools/user_go.png" language="javascript" runat="server"
                            onclick="return NewimgBD_editor_onclick()" style="cursor: pointer" title="Select PE"/>
                        <asp:HiddenField ID="hfprojectEditorId" runat="server"  />
                        
                    </td>
                    <td >
                        Quote Rec Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecDate" runat="server" ></asp:TextBox>
                        <a href="javascript:NewCal('txtRecDate','MMddyyyy',true,24)">
                            <img src="images/Calendar.jpg" width="16" height="16" border="0" alt="Pick a date"></a>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Quote Due Date: </td>
                    <td>
                        <asp:TextBox ID="txtDueDate" runat="server" ></asp:TextBox>
                        <a href="javascript:NewCal('txtDueDate','MMddyyyy',true,24)">
                            <img src="images/Calendar.jpg" width="16" height="16" border="0" alt="Pick a date"></a>
                    </td>
                        <td>
                        Quote Send Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSendDate" runat="server" ></asp:TextBox>
                        <a href="javascript:NewCal('txtSendDate','MMddyyyy',true,24)">
                            <img src="images/Calendar.jpg" width="16" height="16" border="0" alt="Pick a date"></a>
                    </td>
                </tr>
            </table>
        </div>
        <div class="content" id="tabRecAmends" runat="server">
            <table>
                <tr align="center">
                    <td>
                        Project Name/JobID :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtProName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  CssClass="dpbutton" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="11px" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="grdJobDet" AllowSorting="True" width="100%" runat="server" AutoGenerateColumns="False"
				                AllowPaging="False" DataKeyField="Jobid"  CssClass="lightbackground"
				                 OnRowCommand="grdJobDet_RowCommand" >
				                <FooterStyle BackColor="Transparent"></FooterStyle>
				                <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground"></HeaderStyle>
				                <Columns>
					                <asp:TemplateField HeaderText="JobID">
						                <ItemTemplate>
						                    <asp:LinkButton ID="Jobid" runat="server" CommandName="Display" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Pro_ID") %>'>
						                        <%# DataBinder.Eval(Container.DataItem, "Jobid")%>
                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Jobid") %>'  Visible="false"></asp:Label>
                                                <asp:Label ID="lblLocation_id" runat="server" Text='<%# Eval("Location_ID") %>'  Visible="false"></asp:Label>
                                                <asp:Label ID="lblTimeZone" runat="server" Text='<%# Eval("TimeZone") %>'  Visible="false"></asp:Label>
                                                <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JobNo") %>'  Visible="false"></asp:Label>
						                    </asp:LinkButton>
						                </ItemTemplate>
					                </asp:TemplateField>
					                <asp:TemplateField HeaderText="Project Name">
						                <ItemTemplate>
							                <asp:Label ID="lblProName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
						                </ItemTemplate>
					                </asp:TemplateField>
					                <asp:TemplateField HeaderText="Customer">
					                    <ItemTemplate>
					                        <%# DataBinder.Eval(Container.DataItem,"Cust_name") %>
					                    </ItemTemplate>
					                </asp:TemplateField>
					                <asp:TemplateField HeaderText="PE Name" >
						                <ItemTemplate>
							                <%# DataBinder.Eval(Container.DataItem, "ProjectEditor")%>
						                </ItemTemplate>
					                </asp:TemplateField>
					                <asp:TemplateField HeaderText="TaskName">
						                <ItemTemplate>
							                <%# DataBinder.Eval(Container.DataItem, "Desc1") %>
						                </ItemTemplate>
					                </asp:TemplateField>
					                <asp:TemplateField HeaderText="Pages">
						                <ItemTemplate>
							                <%# DataBinder.Eval(Container.DataItem, "Pages") %>
						                </ItemTemplate>
					                </asp:TemplateField>
				                </Columns>
			                </asp:GridView>	
                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align = "center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblproname" runat="server" Text="Project Title:" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DelProName" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblJobid" runat="server" Text="Job ID:" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="DelJobID" Enabled="false" runat="server" Visible="false"></asp:TextBox>
                                        <asp:Label ID="DelNL_ID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="DelLoc_ID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="DelJobNo" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblAmendsDet" runat="server" Text="Amends Details:" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropCurStage" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="dropCurStage_SelectedIndexChanged" runat="server">
                                            <asp:ListItem Value="5" Selected="True">-- Select --</asp:ListItem>
                                            <asp:ListItem Value="1">Amends Received</asp:ListItem>
                                            <asp:ListItem Value="2">Amends + Final Package</asp:ListItem>
                                            <asp:ListItem Value="3">Final Package</asp:ListItem>
                                            <asp:ListItem Value="0">Current Amends</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                   <td>
                                        <asp:Label ID="lblRecDate" runat="server" Text="Received Date:" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTarRecDate" runat="server" Visible="false"></asp:TextBox>
                                        <a href="javascript:NewCal('txtTarRecDate','MMddyyyy',true,24)" runat="server" id="a1">
                                            <img src="images/Calendar.jpg" width="16" height="16" border="0" alt="Pick a date"
                                                id="img10"  runat="server" Visible="false"></a>
                                    </td>                    
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDueDate" runat="server" Text="Due Date:" Visible="false"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblDelDueDateFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtDelDueDateFrom" runat="server" CssClass="TxtBox" Width="80px" Visible="false"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtDelDueDateFrom','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img1"  runat="server" Visible="false" />
                                        <asp:Label ID="lblDelDueDateTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                        <asp:TextBox ID="txtDelDueDateTo" runat="server" CssClass="TxtBox" Visible="false" Width="80px"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtDelDueDateTo','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img2"  runat="server" visible="false" />
                                        <asp:CheckBox ID="chkDelYTC" Text="YTC" runat="server" Visible="false" />
                                        <asp:CheckBox ID="chkDelStagDate" Text="Staggered Delivery" runat="server"  AutoPostBack="True" OnCheckedChanged="chkDelStagDate_CheckedChanged" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td> <asp:Label ID="lblDueTime" runat="server" Text="Due Time:" Visible="false"></asp:Label></td>
                                    <td colspan="3">
                                        <asp:Label ID="lblDelTimeFrom" runat="server" Text="From:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="dropDelTimeFrom" runat="server" Width="40px" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="dropDelTimeFrom_SelectedIndexChanged">
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
                                        <asp:DropDownList ID="dropDelMinFrom" runat="server" Width="40px" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="dropDelMinFrom_SelectedIndexChanged">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="dropDelZoneFrom" runat="server" Width="50px" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="dropDelZoneFrom_SelectedIndexChanged" >
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
                                        <asp:Label ID="lblDelTimeTo" runat="server" Text="To:" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="dropDelTimeTo" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="dropDelTimeTo_SelectedIndexChanged" Visible="False" >
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
                                        <asp:DropDownList ID="dropDelMinTo" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="dropDelMinTo_SelectedIndexChanged" Visible="False" >
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="dropDelZoneTo" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="dropDelZoneTo_SelectedIndexChanged" Visible="False" >
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
                                        <asp:CheckBox ID="chkDelstagTime" Text="Staggered Delivery" Visible="false" runat="server" AutoPostBack="True" OnCheckedChanged="chkDelstagTime_CheckedChanged" Width="133px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3">
                                        <asp:Label ID="lblDelISTFrom" runat="server" Text="(From)" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtDelISTFrom" runat="server" Width="80px" Enabled="false" Visible="false"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblDelISTTo" runat="server" Text="(To)" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtDelISTTo" runat="server" Width="80px" Visible="False" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"> 
                                        <asp:Label ID="lblDelTime" runat="server" Text="Delivery Time:" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtdeldateAll" runat="server" CssClass="TxtBox" Width="80px" Visible="false"></asp:TextBox>
                                        <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdeldateAll','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="img4"  runat="server" Visible="false" />
                                        <asp:DropDownList ID="dropDelHrsAll" runat="server" Width="40px" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="dropDelHrsAll_SelectedIndexChanged">
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
                                        <asp:DropDownList ID="dropDelMinsAll" runat="server" Width="40px" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="dropDelMinsAll_SelectedIndexChanged">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                        </asp:DropDownList>&nbsp;
                                        <asp:DropDownList ID="dropDelZoneAll" runat="server" Width="50px" AutoPostBack="True" Visible="false" OnSelectedIndexChanged="dropDelZoneAll_SelectedIndexChanged" >
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
                                        <asp:Label ID="lblDelTime_IST" runat="server" Text="Delivery Time IST" Visible="false"></asp:Label>
                                        <asp:TextBox ID="txtDel_ISTAll" runat="server" Width="80px" Enabled="false" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblDelStatusFile" runat="server" Text="Status : " Visible="false"></asp:Label>
                                        <asp:DropDownList ID="dropDelFileStatus" runat="server" Width="40px" Visible="false">
                                            <asp:ListItem>P</asp:ListItem>
                                            <asp:ListItem>C</asp:ListItem>
                                            <asp:ListItem>WIP</asp:ListItem>
                                            <asp:ListItem>Del</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                    <asp:Button ID="DelSave" runat="server" Text="Save" OnClick = "onDelSave" CssClass="dpbutton" Visible="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblResult2" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ClientIDMode="Static">
                                <ContentTemplate>
                                    <asp:GridView ID="gvJobTrackFiles" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                    CssClass="lightbackground" Width="1050px" OnRowDataBound="gvJobTrackFiles_RowDataBound" ClientIDMode="Static" >
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
                                            <asp:Label ID="lblTaskName" Width="65" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Edit" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit1" runat="server" Text = "Click" OnClick="ClickFile" Visible="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New/Delivery" >
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkdel" runat="server" Text = "New/Delivery"  OnClick = "OnDelivery" Visible="false"></asp:LinkButton><br />
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
                                            <asp:Button ID="btnSave1" runat="server" Text="Save"  OnClick = "Save1" CssClass="dpbutton"/>
                                            <asp:Button ID="btnCancel1" runat="server" Text="Close" OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
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
                                <asp:AsyncPostBackTrigger ControlID = "gvJobTrackFiles" />
                            </Triggers> 
                        </asp:UpdatePanel>
                    </div>
                        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
                        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
                        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
                        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                gridviewScroll();
                            });

                            function gridviewScroll() {
                                $('#<%=gvJobTrackFiles.ClientID%>').gridviewScroll({
                                    width: 1075,
                                    height: 300,
                                    startHorizontal: 0,
                                    barhovercolor: "#848484",
                                    barcolor: "#848484"
                                });
                            }
                        </script>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
