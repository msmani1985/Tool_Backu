<%@ page language="C#" autoeventwireup="true" inherits="invoice_pattern, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Invoice Patterns</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script src="scripts/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Invoice Patterns</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable">
            <tr>
                <td align="center" colspan="5">
                    Customer:<span style="color: #ff0000">*</span><strong> </strong>
                    <asp:DropDownList ID="drpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged">
                    </asp:DropDownList>&nbsp; Job Type:<span style="color: #ff0000">*</span><strong> </strong>
                    <asp:DropDownList ID="drpJobtype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpJobtype_SelectedIndexChanged">
                        <asp:ListItem Value="2">Books</asp:ListItem>
                        <asp:ListItem Value="4">Projects</asp:ListItem>
                    </asp:DropDownList>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Search" />
                </td>
            </tr>
        </table>
        <div id="divSummary" runat="server">
            <br />
            <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="width: 72%">
                <tr>
                    <td>
                        <table align="center" cellpadding="1" cellspacing="1" width="100%">
                            <tr id="trJournal" runat="server">
                                <td style="width: 126px" align="right">
                                    Journal:</td>
                                <td colspan="3">
                                    <asp:DropDownList ID="drpJournal" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 126px" align="right">
                                    Invoice Type Item:<span style="color: #ff0000">*</span></td>
                                <td>
                                    <asp:DropDownList ID="drpInvTypeItem" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 126px" align="right">
                                    Type of Cost:<span style="color: #ff0000">*</span></td>
                                <td>
                                    <asp:DropDownList ID="drpTypeofCost" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 126px" align="right">
                                    Service Type:<span style="color: #ff0000">*</span></td>
                                <td>
                                    <asp:DropDownList ID="drpServiceType" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 126px" align="right">
                                    Price Code:&nbsp;&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtPriceCode" runat="server" MaxLength="15" Width="70px" CssClass="TxtBox" onkeypress="javascript:return OnlyAllowNumbers(this,event);"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 126px">
                                    Description: &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="TxtBox" MaxLength="200"
                                        Width="75%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 126px">
                                    Order Index: &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrderIndex" runat="server" CssClass="TxtBox" MaxLength="10" onkeypress="javascript:return OnlyAllowNumbers(this,event);"
                                        Width="70px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 126px">
                                </td>
                                <td align="right">
                                    &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="dpbutton" OnClick="btnSave_Click"
                                        Text="Save" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="dpbutton"
                                            OnClick="btnCancel_Click" Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvJobPattern" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                            Width="100%" OnRowCommand="gvJobPattern_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Invoice Type Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInvTypeItem" runat="server" Text='<%# Eval("InvoiceType_item_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type of Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvTypeofCost" runat="server" Text='<%# Eval("cost_type_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvServiceType" runat="server" Text='<%# Eval("service_type_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPriceCode" runat="server" Text='<%# Eval("price_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Index">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOrderIndex" runat="server" Text='<%# Eval("order_index") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtngvEdit" runat="server" CommandName="pedit" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/tools/edit.png" ToolTip="Edit" />
                                        <asp:ImageButton ID="imgbtngvDelete" runat="server" CommandName="pdelete" ImageAlign="AbsMiddle"
                                            ImageUrl="~/images/tools/delete.png" OnClientClick="javascript:return confirm('Confirm delete?');" ToolTip="Delete" />
                                        <asp:HiddenField ID="hfgvPatternID" runat="server" Value='<%# Eval("job_cost_pattern_id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
                            <FooterStyle BackColor="Honeydew" />
                        </asp:GridView>
                        <asp:HiddenField ID="hfCostPatternID" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
