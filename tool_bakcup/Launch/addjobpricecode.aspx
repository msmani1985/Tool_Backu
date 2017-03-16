<%@ page language="C#" autoeventwireup="true" inherits="addjobpricecode, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job PriceCode Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Job Price Code
    </div>
    <div align="center" >
        <table class="bordertable" width="200px">
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="Type"></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="TypeList" runat="server" Width="98px" OnSelectedIndexChanged="TypeList_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="2">Book</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div align="center" style="width:100%">
        <table  width="100%">
            
            <tr>
                <td  align="center">
                    <asp:DataGrid ID="PriceCodeDataGrid" runat="server"  
                        AutoGenerateColumns="False"  OnCancelCommand="PriceCode_CancelCommand" 
                        OnEditCommand="PriceCode_EditCommand" OnUpdateCommand="PriceCode_UpdateCommand"
                        HeaderStyle-CssClass="darkbackground" SelectedItemStyle-CssClass="editbackground" AlternatingItemStyle-CssClass="dullbackground"
                        CssClass="lightbackground" Width="80%" >

                        <Columns>
                        <asp:TemplateColumn HeaderText="Name">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem,"jourcode")%></ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Title">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem,"journame")%></ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PriceCode-2008">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem,"jcno_2008")%></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="pricecode2008" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"jcno_2008") %>' ></asp:TextBox></EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PriceCode-2009">
                            <ItemTemplate><%# DataBinder.Eval(Container.DataItem,"jcno_2009") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="pricecode2009" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"jcno_2009") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton ID="Edit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"journo") %>' CommandName="Update" Text="Update" CausesValidation="false" runat="server"></asp:LinkButton>&nbsp;<asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="false" runat="server"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        </Columns>
                        <SelectedItemStyle CssClass="editbackground" />
                        <AlternatingItemStyle CssClass="dullbackground" />
                        <HeaderStyle CssClass="darkbackground" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
