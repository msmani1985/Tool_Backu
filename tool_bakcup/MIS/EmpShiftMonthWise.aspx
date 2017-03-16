<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpShiftMonthWise.aspx.cs" Inherits="EmpShiftMonthWise" %>

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
                <td  align="center">
                    <strong> Month</strong>&nbsp;
                    <asp:DropDownList ID="DDMonthList" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <strong>Year</strong>&nbsp;
                    <asp:DropDownList ID="DDYearList" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Submit" TabIndex = "4" CssClass="dpbutton" OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 850px">
                    <asp:GridView ID="grvShift" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  
                        Font-Size="9pt" OnRowDataBound="grvShift_RowDataBound" BorderWidth="3px" CellPadding="2" 
                        BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black" >
                        <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
                        <AlternatingRowStyle BackColor="#F2F2F2" />
                        <Columns>
                            <asp:TemplateField Visible="false" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="SID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"sl") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="Designation_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="Department" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Department") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="Date" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Sdate") %>'></asp:Label>
                                    <asp:Label ID="Edate" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"Edate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shift Details">
                                <ItemTemplate>
                                    <asp:DropDownList  ID="DropShift" Enabled="false" runat="server">
                                    </asp:DropDownList>  
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
                    width: 750,
                    height: 400,
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
