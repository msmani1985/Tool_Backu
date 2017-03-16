<%@ page language="C#" autoeventwireup="true" inherits="payment_on_account, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment On Account</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                Payment On Account</div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 84%">
            <tr class="dpJobGreenHeader">
                <td colspan="2">
                    <b>Customer: &nbsp;</b><asp:Label ID="lblCustName" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <b>No. of rows:</b>
                    <asp:DropDownList ID="drpRows" runat="server" Width="43px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAdd" class="dpbutton" Text="Add" runat="server" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnSave" class="dpbutton" Text="Save" runat="server" OnClick="btnSave_Click" />
                    <input id="btnClose" class="dpbutton" type="button" value="Close[x]" style="cursor: pointer"
                        onclick="javascript:self.close();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                        Width="100%" ShowFooter="True" OnRowDataBound="gvPayment_RowDataBound" OnRowCommand="gvPayment_RowCommand"
                        OnRowDeleting="gvPayment_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Slno." HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSlno" Text='<%# rowid++%>' runat="server"></asp:Label>
                                    <asp:HiddenField ID="hfgvCreditID" Value='<%# Eval("creditid") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credited Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvCreditDate" Text='<%# Eval("credited_date", "{0:MM/dd/yyyy}") %>'
                                        runat="server" Width="80px" ToolTip="Format: MM/dd/yyyy"></asp:TextBox>                                    
                                    <asp:ImageButton ID="imgCalendar" style="cursor:pointer; border: none" AlternateText="Calendar" runat="server" ImageUrl="images/Calendar.jpg" Height="20px" BorderWidth="0" ImageAlign="absMiddle" />
                                    <asp:Label ID="lblgvCreditDate" Text='<%# Eval("credited_date", "{0:MM/dd/yyyy}") %>'
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credited Value" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvCreditValue" Text='<%# Eval("credited_value") %>' runat="server" ToolTip="Amount credited by the customer. Ex: -2000"></asp:TextBox>
                                    <asp:Label ID="lblgvCreditValue" Text='<%# Eval("credited_value") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton CssClass="link1" ID="lnkbtngvEdit" CommandName="editme" Text="Edit"
                                        runat="server"></asp:LinkButton>
                                    <asp:LinkButton CssClass="link1" ID="lnkbtngvDelete" CommandName="deleteme" Text="Delete"
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                No records found.</div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="darkbackground" />
                        <AlternatingRowStyle CssClass="dullbackground" />
                    </asp:GridView>
                    <asp:Label ID="lbldummy" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
