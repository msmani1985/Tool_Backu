<%@ page language="C#" autoeventwireup="true" inherits="jobsearch, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
<div class="dptitle" id="invtitle" runat="server" >Job Search</div>
    <div>
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>Search:</td><td><asp:TextBox ID="txtsearch" runat="server" ></asp:TextBox>&nbsp;
                <asp:CheckBox ID="chkviewcompleted" Text="View Completed" runat="server" />
                </td>
                </tr>
                <tr><td>Customer</td><td>
                <asp:DropDownList ID="ddlcustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO"  ></asp:DropDownList>
                </td>
                <td rowspan="2" style="width: 132px">
                    <asp:RadioButtonList ID="rblstJournal" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Value="A" Selected="True">Article</asp:ListItem>
                        <asp:ListItem Value="I">Issue</asp:ListItem>
                    </asp:RadioButtonList><asp:RadioButtonList ID="rblstBook" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Value="B" Selected="True">Book</asp:ListItem>
                        <asp:ListItem Value="C">Chapter</asp:ListItem>
                    </asp:RadioButtonList><asp:RadioButtonList ID="rblstProject" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Value="P" Selected="True">Project</asp:ListItem>
                        <asp:ListItem Value="M">Module</asp:ListItem>
                    </asp:RadioButtonList></td>                
                
                </tr><tr>
                <td>Type</td><td>
                <asp:DropDownList ID="ddljobtype" runat="server" OnSelectedIndexChanged="ddljobtype_SelectedIndexChanged" AutoPostBack="True" >
                    <asp:ListItem Value="1" text="Journal" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" text="Book"></asp:ListItem>
                    <asp:ListItem Value="3" text="Project"></asp:ListItem>                    
                </asp:DropDownList>                 
                </td>
                </tr>
                <tr><td id="Td1" align=center colspan="3" valign="bottom" runat="server"   >
                    <asp:Button Text="Search..." CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click" />
                </td></tr>
            </table>
    </div>   
    
    <asp:DataGrid HorizontalAlign="Center" GridLines="Horizontal" ID="adgsearchlist" AllowSorting="True" width="90%" runat="server" 
     AutoGenerateColumns="true" 
		AllowPaging="False" DataKeyField="CUSTNO" CellPadding="3">
		<FooterStyle BackColor="Transparent"></FooterStyle>
		<AlternatingItemStyle CssClass="dullbackground"></AlternatingItemStyle>
		<ItemStyle CssClass="lightbackground"></ItemStyle>
		<HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground"></HeaderStyle>
		<PagerStyle CssClass="darkbackground" HorizontalAlign="right" Visible="False" NextPageText="Next Page" PrevPageText="Prev Page"></PagerStyle>
	</asp:DataGrid>
    <div id="divmessage" runat="server" >
    &nbsp;&nbsp;
        
        &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp;
        </div>      
    </form>
</body>
</html>
