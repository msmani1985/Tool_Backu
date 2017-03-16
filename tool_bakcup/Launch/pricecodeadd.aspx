<%@ page language="C#" autoeventwireup="true" inherits="pricecodeadd, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="default.css" type="text/css" /> 
</head>
<script type="text/javascript" language="javascript">

var intVAlue = 0;

function myMethod()
{
window.location.href="welcome.aspx";
}

</script>

<body>
    <form id="form1" runat="server">
    <div class="dptitle">Price Code Editor</div>
    <div align="center">
        <table class="bordertable"><tr><td>
            <asp:RadioButtonList ID="RB_pricecode" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="India Pricecode" Value="i" Selected="true"></asp:ListItem>
                <asp:ListItem Text="Dublin Pricecode" Value="d"></asp:ListItem>
            </asp:RadioButtonList></td><td><asp:Button ID="Btn_showpricecode" BackColor="green" Font-Bold="true" Font-Size="8pt" ForeColor="white" Text="Show Pricecode" runat="server" OnClick="Btn_showpricecode_Click" /></td></tr>
            </table>
    </div>
    <div id="div_pcfile" runat="server" style="margin-top:20px;margin-left:20px; overflow:auto ; height:35px;display:none;  ">
    <span class="error">Price Code File Name:</span> <asp:Label ID="lblFileName" Font-Underline="true" ForeColor="green" runat="server" ></asp:Label> 
    <%--<span class="error">Server:</span> <asp:Label Font-Underline="true" ForeColor="green" ID="lblServer" runat="server" ></asp:Label>--%>
    </div>
    <br />
    <center>
    <div id="div_pricecode" runat="server" style="width:600px; height:325px; border:solid 1px green; overflow:scroll;">
    <asp:datagrid id="dgPC" runat="server" 
        onUpdateCommand="UpdateXML" 
        OnCancelCommand="cancelEdit" 
        onEditCommand="setEditMode" 
        onDeleteCommand="delXML" 
        onItemCommand="doInsert" 
        AutoGenerateColumns="False" 
        ShowFooter="True" 
        HeaderStyle-CssClass="darkbackground"
        SelectedItemStyle-CssClass="editbackground"
        AlternatingItemStyle-CssClass="bullbackground"
        CssClass="lightbackground"
        DataKeyField="JCPNO"
        >
            <Columns>
                <asp:TemplateColumn HeaderText="JCPNO">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "JCPNO")%></ItemTemplate>
                    
                 </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="JCPNAME">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "JCPNAME")%></ItemTemplate>
                    
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="JCPPRICE">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "JCPPRICE")%></ItemTemplate>
                    
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="JCPPRICE2">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "JCPPRICE2")%></ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="JCPPRICE3">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "JCPPRICE3")%></ItemTemplate>
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="CURRENCY">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "CURRENCY")%></ItemTemplate>
                </asp:TemplateColumn>

                <asp:TemplateColumn HeaderText="INDIAPRICE">
                    <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "INDIAPRICE")%></ItemTemplate>
                </asp:TemplateColumn>                    
                
            </Columns>
        </asp:datagrid>
        </div>
        </center>
        <center>
        <asp:Label id="error1" runat="server" visible="False" font-names="Tahoma" bordercolor="#FFC080" font-size="Smaller" font-bold="True" forecolor="Red" tooltip="Id must be unique" backcolor="White" width="161px">Error!
            Id must be unique</asp:Label>
        </center>
        <center><asp:Label id="error2" runat="server" visible="False" font-names="Tahoma" bordercolor="#FFC080" font-size="Smaller" font-bold="True" forecolor="Red" tooltip="Id must be numeric within the range of 1 to 99999" backcolor="White" width="402px">Error!
            Id must be numeric within the range of 1 to 9999</asp:Label>
        </center>
        <center><asp:Label id="error3" runat="server" visible="False" font-names="Tahoma" bordercolor="#FFC080" font-size="Smaller" font-bold="True" forecolor="Red" tooltip="The Record you are updating has been deleted by another user!" backcolor="White" width="402px">Error! The Record you are updating has been deleted by another user!</asp:Label>
        </center>
		 <center><asp:Label id="error4" runat="server" visible="False" font-names="Tahoma" bordercolor="#FFC080" font-size="Smaller" font-bold="True" forecolor="Red" tooltip="The Record you are trying to Edit has been deleted by another user!" backcolor="White" width="402px">Error! The Record you are trying to Edit has been deleted by another user!</asp:Label>
        </center>
		 <center><asp:Label id="error5" runat="server" visible="False" font-names="Tahoma" bordercolor="#FFC080" font-size="Smaller" font-bold="True" forecolor="Red" tooltip="The Record cannot be deleted while in Edit Mode" backcolor="White" width="402px">Error! The Record cannot be deleted while in Edit Mode</asp:Label>
        </center>     
    </form>
</body>
</html>
