<%@ page language="C#" autoeventwireup="true" inherits="projectmodule, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Module Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
    function validation()
    {
        if(document.getElementById("ddl_projectlist").value==0)
        {
            alert("Invalid Project, Please try again");
            document.getElementById("ddl_projectlist").focus();
            return false;
        }  
        if(isNaN(document.getElementById("txt_nofoitems").value))
        {
            alert("Number of Items is Invalid, Please try again");
            document.getElementById("txt_nofoitems").focus();
            return false;
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
    Project Module
    </div>
    <br />
    <div align="center"><table class="bordertable"><tr><td>Project</td><td><asp:DropDownList ID="ddl_projectlist" DataTextField="pcode" DataValueField="projectno" runat="server" ></asp:DropDownList><asp:DropDownList ID="ddl_custlist" runat="server" DataTextField="custno" DataValueField="custno" Visible="false"></asp:DropDownList><asp:DropDownList ID="ddl_ponumber" Visible="false" runat="server" DataTextField="PONUMBER"></asp:DropDownList></td><td>No.of Items</td><td><asp:TextBox ID="txt_nofoitems" runat="server" Text="0"></asp:TextBox></td><td><asp:Button ID="btn_createmodule" Font-Bold="true"  ForeColor="white" BackColor="green" Text="Create Module" runat="server" OnClientClick="return validation();" OnClick="btn_createmodule_Click" /></td></tr></table></div>
    <br />
    <div><table width="800px" ><tr><td align="right"><asp:ImageButton ID="ibtn_save" runat="server" ImageUrl="~/images/tools/j_save.png" OnClick="ibtn_save_Click" /></td></tr></table></div>
    <div><asp:GridView ID="gv_pmodule" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
    HeaderStyle-CssClass="darkbackground" ShowFooter="true">
        <Columns>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:TextBox Width="230px" ID="txt_des" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MPTITLe") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txt_qty" Width="50px" runat="server" Text='<%# Eval("NUMPAGES") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price Code">
                <ItemTemplate>
                    <asp:TextBox ID="txt_pricecode" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PRICECODE") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PONumber">
                <ItemTemplate>
                    <asp:TextBox ID="txt_mponumber" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem,"MOPONUMBER") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cost Type">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_costtype" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"COSTTYPEID") %>'>
                    <asp:ListItem Text="Pages" Value="0"></asp:ListItem>
            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem><asp:ListItem Text="Issues" Value="2"></asp:ListItem>
            <asp:ListItem Text="Slides" Value="3"></asp:ListItem><asp:ListItem Text="Pieces" Value="4"></asp:ListItem>
            <asp:ListItem Text="KB" Value="5"></asp:ListItem><asp:ListItem Text="Images" Value="6"></asp:ListItem>
            <asp:ListItem Text="Articles" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_description" runat="server" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"PAGEDESCRIPTIONID") %>'>
                    <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                    <asp:ListItem Text="No" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="PE Contact Name">
                <ItemTemplate>
                    <asp:DropDownList ID="ddl_pename" runat="server" Width="200px" DataSource='<%# loadpename() %>'  DataTextField="column1" DataValueField="CONNO" SelectedValue='<%# DataBinder.Eval(Container.DataItem,"CONNO") %>'></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="cb_delete" runat="server"/><asp:HiddenField ID="hf_rowmoduleno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"MPROJNO") %>' />
            </ItemTemplate>
            <FooterTemplate>
                <asp:ImageButton ID="ibtn_Delete" ToolTip="Delete" OnClick="ibtn_Delete_click" AlternateText="Delete" runat="server" ImageUrl="~/images/tools/delete.png" />
            </FooterTemplate>
            <HeaderTemplate>
                <asp:ImageButton ID="ibtn_Delete" ToolTip="Delete" OnClick="ibtn_Delete_click" AlternateText="Delete" runat="server" ImageUrl="~/images/tools/delete.png" />
            </HeaderTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView></div>
    </form>
</body>
</html>
