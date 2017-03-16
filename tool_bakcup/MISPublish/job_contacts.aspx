<%@ page language="C#" autoeventwireup="true" inherits="job_contacts, App_Web_xuje0h3i" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Contacts</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
<!--

function btnNew_onclick() {
if(document.form1.drpCustomer!=null && document.form1.drpCustomer.value !="0")
            window.open("contacts.aspx?type=1&pop=1&cid="+document.form1.drpCustomer.value,"Contacts","width=800,height=600,status=yes, scrollbars=yes");
        else alert("Select a customer"); 
}

// -->
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            &nbsp;<div class="dptitle">
                Assign Contacts</div>
        </div>
        <div style="text-align: center">
            <table cellpadding="1" cellspacing="1" class="bordertable" align="center">
                <tr>
                    <td align="left" colspan="5">
                        Customer:<span style="color: #ff0000">*</span><strong> </strong>
                        <asp:DropDownList ID="drpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged">
                        </asp:DropDownList>&nbsp; Job Type:<span style="color: #ff0000">*</span><strong> </strong>
                        <asp:DropDownList ID="drpJobtype" runat="server" Width="155px" AutoPostBack="True"
                            OnSelectedIndexChanged="drpJobtype_SelectedIndexChanged">
                            <asp:ListItem Value="1">Journal</asp:ListItem>
                            <asp:ListItem Value="2">Books</asp:ListItem>
                            <asp:ListItem Value="4">Projects</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" Text="Search" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
        </div>
        <br />
        <table cellpadding="1" cellspacing="1" class="bordertable" align="center" id="tblContact"
            runat="server" width="90%">
            <tr style="font-size: 8pt">
                <td align="right" style="width: 102px">
                    Job:<span style="color: #ff0000">*</span></td>
                <td align="left" colspan="3">
                    <asp:DropDownList ID="drpParentJob" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpParentJob_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp;<asp:ImageButton ID="imgbtnNew" runat="server" ImageAlign="AbsMiddle"
                        ImageUrl="~/images/tools/reload.png" OnClick="imgbtnNew_Click" ToolTip="Reset" /></td>
            </tr>
            <tr style="font-size: 8pt">
                <td align="right" style="width: 102px">
                    <asp:Label ID="lblConJob" runat="server"></asp:Label>&nbsp;</td>
                <td align="left" colspan="3">
                    <asp:DropDownList ID="drpJob" runat="server" OnSelectedIndexChanged="drpJob_SelectedIndexChanged" Width="650px">
                </asp:DropDownList></td>
            </tr>
            <tr id="trContName" style="font-size: 8pt" runat="server">
                <td align="right" style="width: 102px">
                    Contact Name:<span style="color: #ff0000">*</span></td>
                <td align="left" colspan="3">
                    <asp:DropDownList ID="drpContName" runat="server" Width="650px">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearchContact" runat="server" CssClass="TxtBox"></asp:TextBox>
                    <asp:ImageButton ID="btnGo" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/tools/search.png"
                        OnClick="btnGo_Click1" ToolTip="Quick Find" /></td>
            </tr>
            <tr id="trContList" style="font-size: 8pt" runat="server">
                <td align="right" style="width: 102px">
                </td>
                <td align="left" colspan="3">
                    <div style="height: 91px; width: 648px; border-right: silver 1px solid; border-top: silver 1px solid; overflow-y: scroll; border-left: silver 1px solid; border-bottom: silver 1px solid;"> 
                        <asp:DataList ID="dtlstContact" runat="server" OnItemCommand="dtlstContact_ItemCommand" CellPadding="1" ForeColor="#333333">
                            <ItemTemplate>
                                <asp:LinkButton ID="dtlstlnkContact" runat="server" CssClass="link1" Font-Size="8pt" CommandName="select_contact" Text='<%# Eval("value") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                            <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        </asp:DataList>
                    </div>
                </td>
            </tr>
            <tr style="font-size: 8pt">
                <td align="right" style="width: 102px">
                    Contact Type:<span style="color: #ff0000">*</span></td>
                <td align="left" colspan="3">
                    <asp:DropDownList ID="drpContType" runat="server">
                </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 8pt">
                <td align="right" colspan="4">
                    <asp:Label ID="lblJobTitle" runat="server" ForeColor="Blue"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="dpbutton" Text="Cancel" OnClick="btnCancel_Click" />
                    <input id="btnNew" class="dpbutton" type="button" value="New Contact" language="javascript" onclick="return btnNew_onclick()" style="width: 79pt" />
                </td>
            </tr>
            <tr style="font-size: 8pt">
                <td align="right" colspan="4">
                    &nbsp;&nbsp;</td>
            </tr>
            <tr style="font-size: 8pt">
                <td align="right" colspan="4">
                    <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                        Width="100%" OnRowCommand="gvContacts_RowCommand" OnRowDataBound="gvContacts_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Job">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvJob" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvContName" runat="server" Text='<%# Eval("display_name") %>'></asp:Label>
                                    <asp:HiddenField ID="hfgvJobContID" runat="server" Value='<%# Eval("job_contact_id") %>' />
                                    <asp:HiddenField ID="hfgvContParentJobID" runat="server" Value='<%# Eval("parent_job_id") %>' />
                                    <asp:HiddenField ID="hfgvContJobID" runat="server" Value='<%# Eval("job_id") %>' />
                                    <asp:HiddenField ID="hfgvContID" runat="server" Value='<%# Eval("contact_id") %>' />
                                    <asp:HiddenField ID="hfgvContTypeID" runat="server" Value='<%# Eval("contact_type_id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvContType" runat="server" Text='<%# Eval("contact_type_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvContEmail" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtnConEdit" runat="server" ToolTip="Edit" ImageUrl="~/images/tools/edit.png" CommandName="EditContact" />
                                    <asp:ImageButton ID="ImgBtnConDelete" runat="server" ToolTip="Delete" ImageUrl="~/images/tools/delete.png" CommandName="DeleteContact" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="darkbackground" />
                        <AlternatingRowStyle CssClass="dullbackground" />
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:HiddenField ID="hfJobContactID" runat="server" />                    
                </td>
            </tr>
        </table>
        <br />
        <br />
    </form>
</body>
</html>
