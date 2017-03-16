<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaunchEventLogger.aspx.cs" Inherits="LaunchEventLogger" %>
<meta http-equiv="refresh" content="60">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="dptitle" runat="server" id="divheader">Event Logger of</div>
    <div>
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:800px;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ClientIDMode="Static">
                        <ContentTemplate>
                            <asp:GridView ID="agvLogEvents" HorizontalAlign="Center" runat="server" Width="95%" 
                                AutoGenerateColumns="False" HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground"
                                RowStyle-CssClass="lightbackground" CellPadding="4" EmptyDataText="No Data Found.." OnRowDataBound="agvLogEvents_RowDataBound" OnRowCommand="agvLogEvents_RowCommand"  >
         
                                <AlternatingRowStyle CssClass="dullbackground"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="Slno" HeaderText="Sl.No" />
                                    <asp:BoundField DataField="JobID" HeaderText="Customer/Job ID" />
                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" >
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TaskName" HeaderText="Task"/>
                                    <asp:BoundField DataField="Lang_Name" HeaderText="Language"/>
                                    <asp:BoundField DataField="Soft_Name" HeaderText="Software"/>
                                    <asp:BoundField DataField="Files_name" HeaderText="File Name"/>
                                    <asp:BoundField DataField="AmendName" HeaderText="Stage"/>
                                    <asp:TemplateField HeaderText="WorkFlow">
                                        <ItemTemplate>
                                            <asp:Label ID="txtWorkFlow" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WorkFLow") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Pages From">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPFrom" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem,"PagesFrom") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pages To">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPTo" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem,"PagesTo") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Pages">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPages" runat="server" Width="40px" Text='<%# DataBinder.Eval(Container.DataItem,"Pages") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="File Status">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_FileStatus" runat="server"  Width="150px">
                                                <%--<asp:ListItem Text = "No Correction" Value = "2"  Selected= "True"></asp:ListItem>
                                                <asp:ListItem Text = "Correction" Value = "1" ></asp:ListItem>
                                                <asp:ListItem Text = "Rejected for Re-Process" Value = "3" ></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcomments" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"Remarks") %>'></asp:TextBox>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="Completed(?)">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddl_WorkStatus" runat="server" Width="150px">
                                                <asp:ListItem Text = "--Select--" Value = "0" Selected= "True"></asp:ListItem>
                                                <asp:ListItem Text = "Finished" Value = "1" ></asp:ListItem>
                                                <asp:ListItem Text = "Pending - Break" Value = "2"></asp:ListItem>
                                                <asp:ListItem Text = "Pending - To handle another priority job" Value = "3" ></asp:ListItem>
                                                <asp:ListItem Text = "Pending - To attend meeting" Value = "4" ></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEndLog" runat="server" Text="End" CssClass="dpbutton"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"eno") %>' 
                                                CommandName="EndLogEvent" />
                                            <asp:HiddenField ID="hf_logid" runat="server" 
                                                Value='<%# DataBinder.Eval(Container.DataItem,"eno") %>' />
                                            <asp:HiddenField ID="hf_NL_ID" runat="server" 
                                                Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
                                            <asp:HiddenField ID="hf_FP_ID" runat="server" 
                                                Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' />
                                            <asp:HiddenField ID="hf_Job_His_ID" runat="server" 
                                                Value='<%# DataBinder.Eval(Container.DataItem,"Job_His_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DueDate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDueDate" runat="server" Width="80px" 
                                                Text='<%# DataBinder.Eval(Container.DataItem,"DueDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Time">
                                        <ItemTemplate>
                                            <asp:Label ID="txtSTime" runat="server" Width="80px" 
                                                Text='<%# DataBinder.Eval(Container.DataItem,"ESTARTDATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Time">
                                        <ItemTemplate>
                                            <asp:Label ID="txtETime" runat="server" Width="80px" 
                                                Text='<%# DataBinder.Eval(Container.DataItem,"EENDDATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prv. Comments">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCMT" runat="server" 
                                                Text ="Comments"  OnClick = "OnComments"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Path">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFILE_PATH" runat="server" Width="80px" 
                                                Text='<%# DataBinder.Eval(Container.DataItem,"FILE_PATH") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Folder Path" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPath" runat="server" 
                                                Text ="Link" CommandName="link"></asp:LinkButton>
                                            <asp:HiddenField ID="hf_lnkPath" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="darkbackground"></HeaderStyle>
                                <RowStyle CssClass="lightbackground"></RowStyle>
                            </asp:GridView>
                            <asp:Panel ID="pnlAddEdit1" Width="900" Height="400" runat="server" CssClass="modalPopup" style = "display:none">
                                <table align="right">
                                    <tr >
                                        <td>
                                            <asp:ImageButton ImageUrl="images/tools/no.png" runat="server" ID="imgFQ"  ToolTip="Save" OnClick="imgbtnFinalQuoteSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                                            <asp:Label ID="Label5" runat="server" Text="File Event Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                <table align = "center">
                                    <tr>
                                        <td>
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
                                            <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
                                            <script type="text/javascript" src="Scripts/jquery.min.js"></script>
                                            <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
                                            <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
                                            <script type="text/javascript">
                                                $(document).ready(function () {
                                                    gridviewScroll();
                                                });
                                                function gridviewScroll() {
                                                    $('#<%=gvLoggedEvents.ClientID%>').gridviewScroll({
                                                        width: 850,
                                                        height: 20,
                                                        startHorizontal: 0,
                                                        barhovercolor: "#848484",
                                                        barcolor: "#848484"
                                                    });
                                                }
                                            </script>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:LinkButton ID="lnkFake1" runat="server"></asp:LinkButton>
                            <asp:ModalPopupExtender ID="DelPopUp" runat="server" DropShadow="false"
                                PopupControlID="pnlAddEdit1" TargetControlID = "lnkFake1"
                                BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID = "agvLogEvents" />
                        </Triggers> 
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
