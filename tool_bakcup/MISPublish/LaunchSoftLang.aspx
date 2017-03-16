<%@ page language="C#" autoeventwireup="true" inherits="LaunchSoftLang, App_Web_xuje0h3i" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href=default.css type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dptitle">Languages & Software Details:</div>
        <div>
            <table align="center" class="bordertable" cellpadding="5" cellspacing="2" border="0" style="width: 59%" >
                <tr>
                    <td>
                        <asp:Label ID="lblSource" runat="server" Text="Source Details"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTarget" runat="server" Text="Target Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gv_Source"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                            CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_Source_RowDataBound" OnRowCommand="gv_Source_RowCommand">
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
                                            <asp:ListBox  ID="lboxSoft" Enabled="false" AutoPostBack="true"  SelectionMode="Multiple"  OnSelectedIndexChanged="lboxSourceSoft_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version">
                                        <ItemTemplate>
                                            <asp:ListBox  ID="lboxVer" Enabled="false" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BtnUpdate" AlternateText="Update1" ToolTip="Update" ImageUrl="~/images/tools/yes.png" runat="server" 
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' CommandName="Update1"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BtnDelete" ToolTip="Delete" AlternateText="Delete1" ImageUrl="~/images/tools/no.png" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' CommandName="Delete1"
                                            runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle CssClass="darkbackground" />
                        </asp:GridView>
                    </td>
                    <td>
                        <asp:GridView ID="gv_Target"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                            CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gv_Target_RowDataBound" OnRowCommand="gv_Target_RowCommand">
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
                                            <asp:ListBox  ID="lboxSoft" Enabled="false" AutoPostBack="true"  SelectionMode="Multiple"  OnSelectedIndexChanged="lboxTargetSoft_SelectedIndexChanged" runat="server" ></asp:ListBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version">
                                        <ItemTemplate>
                                            <asp:ListBox  ID="lboxVer" Enabled="false" SelectionMode="Multiple" runat="server" ></asp:ListBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BtnUpdate" AlternateText="Update1" ToolTip="Update" ImageUrl="~/images/tools/yes.png" runat="server" 
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' CommandName="Update1"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="BtnDelete" ToolTip="Delete" AlternateText="Delete1" ImageUrl="~/images/tools/no.png" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' CommandName="Delete1"
                                            runat="server"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle CssClass="darkbackground" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
