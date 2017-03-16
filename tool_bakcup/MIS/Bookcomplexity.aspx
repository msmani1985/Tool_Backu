<%@ page language="C#" autoeventwireup="true" CodeFile="Bookcomplexity.aspx.cs" inherits="Bookcomplexity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Set Book Complexity</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Job Complexity
    </div>
    <div id="div_bookdetails" runat="server" align="center">
        <asp:GridView ID="gv_bookcomplexity" runat="server" CssClass="lightbackground" AutoGenerateColumns="false"
          HeaderStyle-CssClass="darkbackground" 
            OnRowDataBound="div_bookdetails_onRowDataBound" Font-Names="Segoe UI" 
            Font-Size="11px">
          <AlternatingRowStyle backcolor="#F0FFF0"></AlternatingRowStyle>
        <Columns>
            <asp:TemplateField HeaderText="Customer Name">
                <ItemTemplate>
                    <asp:Label ID="lbl_custname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CUSTNAME1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Book Title">
                <ItemTemplate>
                    <asp:Label ID="lbl_booktitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BTITLE1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CATNO">
            <ItemTemplate>
                <asp:Label ID="lbl_booknumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BNUMBER1") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dispatch">
                <ItemTemplate>
                    <asp:Label ID="lbl_bdespatched" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BDESPATCHED1") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Complexity">
                <ItemTemplate>
                    <asp:DropDownList ID="dd_complexity" runat="server">
                    <asp:ListItem Text="Simple" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Medium" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Complex" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>
                    <asp:ImageButton ID="img_complexitysave" runat="server" 
                        ImageUrl="~/images/tools/j_save.png" OnClick="img_complexitysave_click" 
                        Height="25px" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cb_complexity" runat="server"/><asp:HiddenField ID="hf_bno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"BNO1") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

<HeaderStyle CssClass="darkbackground"></HeaderStyle>
        </asp:GridView>
    </div>
    <div id="div_error" runat="server" class="errorMsg" 
        style="font-family: 'Segoe UI'; font-size: 11px">No Records Found</div>
    </form>
</body>
</html>
