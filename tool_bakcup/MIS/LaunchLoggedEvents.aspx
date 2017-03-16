<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaunchLoggedEvents.aspx.cs" Inherits="LaunchLoggedEvents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <title></title>
    <style type="text/css">
        .auto-style1 {
            background: rgb(180,221,180);
/* Old browsers */background: -moz-linear-gradient(top, rgba(180,221,180,1) 0%, rgba(131,199,131,1) 1%, rgba(0,138,0,1) 57%, rgba(0,112,20,1) 100%); /* FF3.6+ */;
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(180,221,180,1)), color-stop(1%,rgba(131,199,131,1)), color-stop(57%,rgba(0,138,0,1)), color-stop(100%,rgba(0,112,20,1))); /* Chrome,Safari4+ */;
            background: -webkit-linear-gradient(top, rgba(180,221,180,1) 0%,rgba(131,199,131,1) 1%,rgba(0,138,0,1) 57%,rgba(0,112,20,1) 100%); /* Chrome10+,Safari5.1+ */;
            background: -o-linear-gradient(top, rgba(180,221,180,1) 0%,rgba(131,199,131,1) 1%,rgba(0,138,0,1) 57%,rgba(0,112,20,1) 100%); /* Opera 11.10+ */;
            background: -ms-linear-gradient(top, rgba(180,221,180,1) 0%,rgba(131,199,131,1) 1%,rgba(0,138,0,1) 57%,rgba(0,112,20,1) 100%); /* IE10+ */;
            background: rgb(180,221,180); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#b4ddb4', endColorstr='#007014',GradientType=0 ); /* IE6-9 */;
            color: White;
            text-align: center;
            height: 26px;
            font-size: 12px;
            font-family: Segoe UI;
            font-weight: bold;
        }
        .auto-style2 {
            height: 36px;
        }
        .auto-style3 {
            height: 36px;
            width: 304px;
        }
    </style>
</head>
<body  class="LightBackGound" style="background-repeat:no-repeat;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <table align="center" cellpadding="0" cellspacing="0" style="border:1px solid green;align:center;">
                <tr>
                    <td align="center" class="auto-style1" colspan="8" valign="middle">
                        <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="13px" Text="Logged Events"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <strong>Job ID / Project Name :</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtJobID" runat="server" Height="16px" Width="261px"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                        <asp:RadioButtonList ID="rbLaunch" runat="server" RepeatDirection="Horizontal" Visible="false">
                            <asp:ListItem Selected="True" Value="1">Launch</asp:ListItem>
                            <asp:ListItem Value="2">Non Launch</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8" align="center">
                        <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" 
                            onclick="btnSearch_Click" Text="Search" ToolTip="Search" Width="70px" />
                        <asp:Button ID="btnClear" runat="server" CssClass="dpbutton" 
                            onclick="btnClear_Click" Text="Clear" ToolTip="Clear" Width="70px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="11px" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <div id="jobdetails" runat="server">
                <table align="center" >
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
						                    <asp:LinkButton ID="Jobid" runat="server" CommandName="Display" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Jobid") %>'>
						                        <%# DataBinder.Eval(Container.DataItem, "Jobid")%>
                                                <asp:Label ID="lblid" runat="server" Text='<%# Eval("Pro_ID") %>'  Visible="false"></asp:Label>
						                    </asp:LinkButton>
						                </ItemTemplate>
					                </asp:TemplateField>
					                <asp:TemplateField HeaderText="Project Name">
						                <ItemTemplate>
							                <%# DataBinder.Eval(Container.DataItem, "ProjectName") %>
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
                </table>
            </div>
            <div id="viewAmdDet" runat="server">
                <table width="100%">
                    <tr>
                        <td colspan="2" align="center">
                            <table width="100%" cellspacing="4">
                                <tr>
                                    <td align="right" colspan="2"> 
                                        <asp:ImageButton ID="ImageButton1" Visible="false" ImageUrl="~/images/tools/no.png" runat="server" OnClientClick = "return Hidepopup()"/>
                                    </td>
                                </tr>
	                            <tr>
	                            <td colspan="2" class="HeadText">Filewise Amends Details</td>
	                            </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label1" ForeColor="Red" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton id="ImageButton2" tabIndex="41" Visible="false" runat="server" ImageUrl="~/images/tools/j_excel.png" ToolTip="Amends Duration" OnClick="amd_Excel_Click"></asp:ImageButton>
                                    </td>
                                </tr>
	                            <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="grdAmdDetails" runat="server" 
                                            EmptyDataText="No data available." Font-Size="8pt">
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                        </asp:GridView>
                                    </td>
	                            </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
                        </td>
                    </tr>
                    <tr>
	                    <td colspan="2" class="HeadText">Logged Events Details</td>
	                </tr>
                    <tr width="100%">
                        <td colspan="2" width="100%">  
                            <asp:GridView ID="gvLoggedEvents" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  EmptyDataText="No Data Found.." 
                                CssClass="lightbackground" Width="874px" OnRowDataBound="gvLoggedEvents_RowDataBound" OnRowCommand="gvLoggedEvents_RowCommand" >
                                <HeaderStyle CssClass="darkbackground"  />
                                <Columns>
                                    <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                            <asp:Label ID="lblid" runat="server" Text='<%# Eval("LP_id") %>'  Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="JobID" HeaderText="JobID" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblgvJobID" Visible="false" runat="server"  CommandName="FileDetails" Text='<%# DataBinder.Eval(Container.DataItem,"jobid") %>' ></asp:LinkButton>
                                            <asp:Label ID="lblgvJobID11" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="ProjectName" HeaderText="Project Name" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:DropDownList OnSelectedIndexChanged="dd_FileName_OnSelectedIndexChanged" AutoPostBack="true" 
                                                ID="dd_FileName" runat="server"></asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem,"Files_Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField SortExpression="Files_Name" HeaderText="File Name" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFiles" runat="server" Text='<%# Eval("Files_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField SortExpression="LangName" HeaderText="LangName" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvLangName" runat="server" Text='<%# Eval("LangName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TaskName" HeaderText="TaskName" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
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
                                    <asp:TemplateField HeaderText="Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEMPNAME" runat="server" Text='<%# Eval("EMPNAME") %>'></asp:Label>
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
                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("TimeDiff1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblmsg" runat="server"/>
                            <asp:Button ID="modelPopup" runat="server" style="display:none" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  DropShadow="false" TargetControlID="modelPopup" PopupControlID="updatePanel" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="updatePanel" Width="700" Height="350" runat="server" CssClass="modalPopup" style = "display:none">
                            <table width="100%" cellspacing="4">
                                <tr>
                                    <td align="right" colspan="2"> <asp:ImageButton ID="btnCancel1" ImageUrl="~/images/tools/no.png" runat="server" OnClientClick = "return Hidepopup()"/></td>
                                </tr>
	                            <tr style="background-color:#33CC66">
	                            <td  align="center" colspan="2"><b>Amends Details</b></td>
	                            </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblLogTotalTime" ForeColor="Red" runat="server"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton id="imgbtnAmd" tabIndex="41"  runat="server" ImageUrl="~/images/tools/j_excel.png" ToolTip="Amends Duration" OnClick="amd_Excel_Click"></asp:ImageButton>
                                    </td>
                                </tr>
	                            <tr>
                                    <td align="center" colspan="2">
                                        <asp:GridView ID="grdFilewiseAmends" runat="server" 
                                            EmptyDataText="No data available." Font-Size="8pt">
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" />
                                        </asp:GridView>
                                    </td>
	                            </tr>
                            </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>

        <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script> 
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script> 
        <script type="text/javascript" src="../gridviewScroll.min.js"></script> 
        <link href="GridviewScroll.css" rel="stylesheet" type="text/css" />--%>
        <script type="text/javascript">
            $(document).ready(function () {
                gridviewScroll();
            });

            function gridviewScroll() {
                $('#<%=gvLoggedEvents.ClientID%>').gridviewScroll({
                    width: window.innerWidth - 25,
                    height: window.innerHeight - 200,
                    startHorizontal: 0,
                    barhovercolor: "#848484",
                    barcolor: "#848484"
                });
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                gridviewScroll1();
            });

            function gridviewScroll1() {
                $('#<%=grdAmdDetails.ClientID%>').gridviewScroll({
                    width: document.getElementById("<%=grdAmdDetails.ClientID%>").offsetWidth+30,
                    height: 200,
                    startHorizontal: 0,
                    barhovercolor: "#848484",
                    barcolor: "#848484"
                });
            }
        </script>
    </form>
</body>
</html>
