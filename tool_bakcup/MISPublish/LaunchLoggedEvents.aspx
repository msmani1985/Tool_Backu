<%@ page language="C#" autoeventwireup="true" inherits="LaunchLoggedEvents, App_Web_xuje0h3i" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <title></title>
</head>
<body  class="LightBackGound" style="background-repeat:no-repeat; width:100%; height:100%">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <table align="center" cellpadding="0" cellspacing="0" style="border:1px solid green;align:center;">
                <tr>
                    <td align="center" class="darkTitle" colspan="8" height="25px" valign="middle">
                        <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="13px" Text="Logged Events"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Job ID / Project Name :</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtJobID" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbLaunch" runat="server" RepeatDirection="Horizontal">
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
            <table width="100%">
                <tr>
                    <td colspan="2" align="right">
                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">  
                        <asp:GridView ID="gvLoggedEvents" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  EmptyDataText="No Data Found.." 
                            CssClass="lightbackground" Width="874px" ClientIDMode="Static" OnRowDataBound="gvLoggedEvents_RowDataBound" >
                            <HeaderStyle CssClass="darkbackground"  />
                            <Columns>
                                <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="JobID" HeaderText="JobID" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobID" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
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
                    width: 1175,
                    height: 430,
                    startHorizontal: 0,
                    barhovercolor: "#848484",
                    barcolor: "#848484"
                });
            }
        </script>
    </form>
</body>
</html>
