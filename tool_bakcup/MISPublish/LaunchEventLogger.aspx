<%@ page language="C#" autoeventwireup="true" inherits="LaunchEventLogger, App_Web_xuje0h3i" %>
<meta http-equiv="refresh" content="60">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="dptitle" runat="server" id="divheader">Event Logger of</div>
    <div>
        <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:800px;">
            <tr>
                <td>
                    <asp:GridView ID="agvLogEvents" HorizontalAlign="Center" runat="server" Width="95%" 
                        AutoGenerateColumns="False" HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground"
                        RowStyle-CssClass="lightbackground" CellPadding="4" EmptyDataText="No Data Found.." OnRowDataBound="agvLogEvents_RowDataBound" OnRowCommand="agvLogEvents_RowCommand"  >
         
                        <AlternatingRowStyle CssClass="dullbackground"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="Slno" HeaderText="Sl.No" />
                            <asp:BoundField DataField="JobID" HeaderText="Job ID" />
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
                                    <asp:DropDownList ID="ddl_FileStatus" runat="server">
                                        <asp:ListItem Text = "No Correction" Value = "2"  Selected= "True"></asp:ListItem>
                                        <asp:ListItem Text = "Correction" Value = "1" ></asp:ListItem>
                                        <asp:ListItem Text = "Rejected for Re-Process" Value = "3" ></asp:ListItem>
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
                                    <asp:DropDownList ID="ddl_WorkStatus" runat="server">
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
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"eno") %>' CommandName="EndLogEvent" />
                                    <asp:HiddenField ID="hf_logid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"eno") %>' />
                                    <asp:HiddenField ID="hf_NL_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
                                    <asp:HiddenField ID="hf_FP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' />
                                    <asp:HiddenField ID="hf_Job_His_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Job_His_ID") %>' />
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:Label ID="txtSTime" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"ESTARTDATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:Label ID="txtETime" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem,"EENDDATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Folder Path">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPath" runat="server" Text ="Link" CommandName="link"></asp:LinkButton>
                                    <asp:HiddenField ID="hf_lnkPath" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="darkbackground"></HeaderStyle>
                        <RowStyle CssClass="lightbackground"></RowStyle>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
