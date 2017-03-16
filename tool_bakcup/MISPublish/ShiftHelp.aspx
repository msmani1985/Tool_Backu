<%@ page language="C#" autoeventwireup="true" inherits="ShiftHelp, App_Web_25d24vps" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr class="dpJobGreenHeader">
                <td colspan="3" style="background-image: url(images/green-noise-background.png)">
                    <strong>Shift Information:</strong>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 850px">
                    <asp:GridView ID="grvShift" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  
                        Font-Size="9pt" BorderWidth="3px" CellPadding="2" 
                        BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black" >
                        <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
                        <AlternatingRowStyle BackColor="#F2F2F2" />
                        <Columns>
                            <asp:TemplateField HeaderText="Shift_ID">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Shift_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shift Name">
                                <ItemTemplate>
                                    <asp:Label ID="SID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Shift_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="InTime">
                                <ItemTemplate>
                                    <asp:Label ID="InTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OutTime">
                                <ItemTemplate>
                                    <asp:Label ID="OutTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"OutTime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        <RowStyle BackColor="White" />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                gridviewScroll();
            });

            function gridviewScroll() {
                $('#<%=grvShift.ClientID%>').gridviewScroll({
                    width: 350,
                    height: 350,
                    startHorizontal: 0,
                    barhovercolor: "#848484",
                    barcolor: "#848484"
                });
            }
        </script>
    </div>
    </form>
</body>
</html>
