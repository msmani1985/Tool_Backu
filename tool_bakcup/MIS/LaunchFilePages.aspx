<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LaunchFilePages.aspx.cs" Inherits="LaunchFilePages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="scripts/common.js"></script>
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
    $(document).ready(function () {
        $('#<%=gv_FilePages.ClientID %>').Scrollable({
        ScrollHeight: 300
    });
    });
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <div class="dptitle">File & Pages Details:</div>
        <table align = "center">
            <tr>
                <td>
                    <span style="color: Red">*</span>Attach Excel file
                </td>
                <td >
                    <asp:FileUpload ID="fileBrowse" runat="server" />
               
                    <asp:Button ID="btnUpload"  CssClass="dpbutton" runat="server" Text="Upload" OnClick="btnUpload_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="NL_ID" runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="NTLS_ID" runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="txtFiles" runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="Task_ID" runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="Lang_ID" runat="server" Visible="false" Text=""></asp:Label>
                    <asp:Label ID="Soft_ID" runat="server" Visible="false" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                            <asp:GridView ID="gv_FilePages"  runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="dullbackground" 
                                    CssClass="lightbackground" HeaderStyle-CssClass="darkbackground"  GridLines="Vertical"  DataKeyNames="Files_ID"
                                    AllowSorting="True" >
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
                <td colspan="4" align="center">
                    <asp:Button ID="btnFPSave" runat="server" Text="Save" OnClick = "Save" CssClass="dpbutton"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()" CssClass="dpbutton" OnClick="btnCancel_Click"/>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label ID="lblResult" runat="server" ForeColor="Red" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
