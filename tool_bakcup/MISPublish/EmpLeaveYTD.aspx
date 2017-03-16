<%@ page language="C#" autoeventwireup="true" inherits="EmpLeaveYTD, App_Web_znvsjrxn" %>

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
    <div class="dptitle">Leave Information</div>
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
                                <asp:Label ID="Months" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MonthYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TotalDays">
                            <ItemTemplate>
                                <asp:Label ID="Total" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalDays") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Present">
                            <ItemTemplate>
                                <asp:Label ID="Present" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Present") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leaves">
                            <ItemTemplate>
                                <asp:Label ID="Leaves" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Leaves") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PL">
                            <ItemTemplate>
                                <asp:Label ID="PL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PL") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <asp:Label ID="SL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SL") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CL">
                            <ItemTemplate>
                                <asp:Label ID="CL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CL") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Permission">
                            <ItemTemplate>
                                <asp:Label ID="Per" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Per") %>' >
                                </asp:Label>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOP">
                            <ItemTemplate>
                                <asp:Label ID="LOP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LOP") %>' >
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

