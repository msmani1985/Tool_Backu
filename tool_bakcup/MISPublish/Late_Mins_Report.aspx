<%@ page language="C#" autoeventwireup="true" inherits="Late_Mins_Report, App_Web_vlobbbje" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grvLvl.ClientID %>').Scrollable({
        ScrollHeight: 440
    });
});

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Late(Mins)</div>
    <div class="content" id="divReports" runat="server" >
    <table>
         <tr>
             <td>
                 <asp:GridView ID="grvLvl" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  Font-Size="9pt" OnRowDataBound="grvLvl_RowDataBound" Width="645px"  >
                                     <HeaderStyle CssClass="GVFixedHeader" />
                                     <AlternatingRowStyle BackColor="#F2F2F2" />
                                     <Columns>
                                      <asp:TemplateField HeaderText="Employee ID">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Refno") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="EmpName">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Designation" >
                                            <ItemTemplate>
                                                <asp:Label ID="Designation_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation_name") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Date" runat="server" Width="80" >
                                                </asp:Label>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Late(Mins)">
                                            <ItemTemplate>
                                                <asp:Label ID="Late" runat="server" Width="50" >
                                                </asp:Label>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
             </td>
         </tr>
    </table>
    </div>
    </form>
</body>
</html>
