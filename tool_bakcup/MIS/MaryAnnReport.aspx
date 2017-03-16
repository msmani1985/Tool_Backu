<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaryAnnReport.aspx.cs" Inherits="MaryAnnReport" %>

<!DOCTYPE html>

 <html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
      <link href="default.css" rel="stylesheet" type="text/css" />    
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Timer ID="Timer1" Interval = "2000" OnTick ="AutoRefreshGrid" runat="server">
</asp:Timer>
<asp:ScriptManager ID="ScriptManager1" runat="server">
 
</asp:ScriptManager>
    <div style="text-align:right;color:green " >
<%--    <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>--%>

        <asp:ImageButton ID="exportExcel_selectedcolumns" runat="server" Height="20px" ImageUrl="images/icon-excel2010.gif" OnClick="exportExcel_selectedcolumns_Click"  Visible="false" />

        </div>
    <div>
<asp:GridView ID="gridview" runat="server"  Width="100%" AutoGenerateColumns="False"   Font-Size="8pt" CssClass="lightbackground">
<HeaderStyle CssClass="darkbackground" />
<AlternatingRowStyle BackColor="#F2F2F2" />
<Columns>
<asp:TemplateField HeaderText="S.no">
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
    <asp:BoundField DataField="name_in" HeaderText="Name In" />
    <asp:BoundField DataField="name_in_ext" HeaderText="Name in ext" />
    <asp:BoundField DataField="date_received" HeaderText="Date Received" />
    <asp:BoundField DataField="name_out_ext" HeaderText="Name out ext" />
    <asp:BoundField DataField="date_processed" HeaderText="Date processed" />
    <asp:BoundField DataField="date_uploaded" HeaderText="Date uploaded" />
 

</Columns>
    <FooterStyle BackColor="White" ForeColor="#333333" />
    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="White" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F7F7F7" />
    <SortedAscendingHeaderStyle BackColor="#487575" />
    <SortedDescendingCellStyle BackColor="#E5E5E5" />
    <SortedDescendingHeaderStyle BackColor="#275353" />
</asp:GridView>
        </div>
</ContentTemplate>
</asp:UpdatePanel>

</div>
</form>
    
</body>
</html>
