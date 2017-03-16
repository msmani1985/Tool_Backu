<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpLateMinYTD.aspx.cs" Inherits="EmpLateMinYTD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <%--<script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grvLate.ClientID %>').Scrollable({
                ScrollHeight: 440
            });
        });

</script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Late(Mins)</div>
    <div class="content" id="divReports" runat="server" >
    <table>
         <tr>
             <td>
                 <asp:GridView ID="grvLate" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  Font-Size="9pt" Width="645px"  >
                    <HeaderStyle CssClass="GVFixedHeader" />
                    <AlternatingRowStyle BackColor="#F2F2F2" />
                    <Columns>
                        <asp:TemplateField HeaderText="Employee ID">
                            <ItemTemplate>
                                <asp:Label ID="EmpID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Empid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmpName">
                            <ItemTemplate>
                                <asp:Label ID="EmpName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmpName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Month\Year" >
                            <ItemTemplate>
                                <asp:Label ID="Months" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Months") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total(Late)">
                            <ItemTemplate>
                                <asp:Label ID="Total" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Times") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Late">
                            <ItemTemplate>
                                <asp:Label ID="Late" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LateIn") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Permission">
                            <ItemTemplate>
                                <asp:Label ID="PerMins" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PerMins") %>' >
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
