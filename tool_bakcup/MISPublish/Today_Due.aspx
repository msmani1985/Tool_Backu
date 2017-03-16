<%@ page language="C#" autoeventwireup="true" inherits="Todate_Due, App_Web_vlobbbje" %>
 
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
    <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
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
<asp:BoundField DataField="JOB_NUMBER" HeaderText="Job No" />
<asp:BoundField DataField="JOB_STAGE" HeaderText="Stage" />
<asp:BoundField DataField="JOB_TYPE" HeaderText="Type" />

    <asp:BoundField DataField="NAME" HeaderText="Name" />
    <asp:BoundField DataField="ASSIGNED" HeaderText="Assigned" />
    <asp:BoundField DataField="DEPARTMENT" HeaderText="Dept." />
    <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Rec. Date" />
    <asp:BoundField DataField="CATS_DUE_DATE" HeaderText="Due. Date" />
    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Cust. Name" />
        <asp:BoundField DataField="MAIL_RECEIVED_TIME" HeaderText="Rec.Time" />
    <asp:BoundField DataField="MAIL_RECEIVED_TIME" HeaderText="Upd.Time" />

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
