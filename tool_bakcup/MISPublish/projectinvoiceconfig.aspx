<%@ page language="C#" autoeventwireup="true" inherits="projectinvoiceconfig, App_Web_xuje0h3i" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Project Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Project Invoice Configuration
    </div>
    <div><table class="bordertable" width="450px" align="center"><tr>
    <td align="right" style="border-right:solid 1px green;">Category: </td>
    <td><asp:RadioButtonList RepeatDirection="Horizontal" ID="rb_category" runat="server"><asp:ListItem Text="Projects" Value="1"></asp:ListItem><asp:ListItem Text="Books" Value="2"></asp:ListItem></asp:RadioButtonList></td>
    <td><asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
    </tr>
    </table></div>
    <br />
    <div id="div_error" runat="server"></div>
    <br />
    <div align="center"><asp:GridView ID="gv_projectlist" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound"
     OnRowEditing="grd_RowEditing" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowUpdating="grd_RowUpdating" HeaderStyle-CssClass="darkbackground"
     AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground">
    <Columns>
    <asp:TemplateField >
    <ItemTemplate><asp:Label ID="lbl_rowid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PROJECTNO") %>'></asp:Label>
    <asp:HiddenField ID="hf_rowid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PROJECTNO") %>' /></ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Customer Name"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "cname")%></ItemTemplate></asp:TemplateField>
    <asp:TemplateField Visible="false" HeaderText="Title"><ItemTemplate><%# DataBinder.Eval(Container.DataItem, "ptitle")%></ItemTemplate></asp:TemplateField>
    <asp:TemplateField HeaderText="Code"><ItemTemplate><%# DataBinder.Eval(Container.DataItem,"PCODE") %></ItemTemplate></asp:TemplateField>
    
    <asp:TemplateField HeaderText="Costtype">
            <ItemTemplate><asp:Label ID="lbl_costtype" runat="server" Text=''></asp:Label></ItemTemplate>
            <EditItemTemplate>
            <asp:DropDownList ID="ddl_costtypeid" runat="server"><asp:ListItem Text="Pages" Value="0"></asp:ListItem>
            <asp:ListItem Text="Hours" Value="1" ></asp:ListItem><asp:ListItem Text="Issues" Value="2"></asp:ListItem>
            <asp:ListItem Text="Slides" Value="3"></asp:ListItem><asp:ListItem Text="Pieces" Value="4"></asp:ListItem>
            <asp:ListItem Text="KB" Value="5"></asp:ListItem><asp:ListItem Text="Images" Value="6"></asp:ListItem>
            <asp:ListItem Text="Sets" Value="7"></asp:ListItem>
            </asp:DropDownList>
            </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Description Display">
            <ItemTemplate><asp:Label ID="lbl_costdes" runat="server" Text=''></asp:Label></ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddl_costdescription" runat="server">
                <asp:ListItem Text="Yes" Value="0"></asp:ListItem>
                <asp:ListItem Text="No" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField>
        <ItemTemplate>
         <asp:LinkButton ID="Edit" runat="server" Text="Edit" CommandName="Edit" CausesValidation="false"></asp:LinkButton>
         </ItemTemplate>
         <EditItemTemplate>
            <asp:LinkButton ID="Update" runat="server" Text="Update" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"PROJECTNO") %>' CausesValidation="false"></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="Cancel" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
         </EditItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField ><ItemTemplate><asp:HiddenField ID="hf_invcostid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "INV_COSTTYPEID")%>' /></ItemTemplate></asp:TemplateField>
    <asp:TemplateField ><ItemTemplate><asp:HiddenField ID="hf_invdes" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"INV_DESCRIPTIONID") %>' /></ItemTemplate></asp:TemplateField>
    
    </Columns>
    </asp:GridView> </div>
    </form>
</body>
</html>
