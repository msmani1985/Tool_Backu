<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Att_AllInOut.aspx.cs" Inherits="Att_AllInOut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="style/calendar-blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">In & Out Punchs</div>
        <div class="content" id="divReports" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="800px">
            <tr>
                <td>
                    <asp:Label ID="EmployeeLbl" runat="server" Text="Emp ID." Width="86px" Height="16px"></asp:Label>
                    <asp:TextBox ID="EmpNoTxt"  Height="16px" Width="150px" runat="server"></asp:TextBox>
               </td>
                <td>Date: </td>
                <td>
                    <asp:TextBox ID="txtPdate" runat="server" ></asp:TextBox>
                    <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" />
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"  />
                       &nbsp;
                       <asp:Button ID="btnClear" runat="server" CssClass="dpbutton"
                       Text="Clear" OnClick="btnClear_Click"   />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="grdInOut" runat="server"  AutoGenerateColumns="false"
                        EmptyDataText="No data available." Font-Size="8pt" 
                        OnRowDataBound="grdInOut_RowDataBound">
                        <HeaderStyle CssClass="GVFixedHeader" />
                        <AlternatingRowStyle BackColor="#F2F2F2" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>Serial No.</HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserID"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserID") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmpName"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpName"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmpName") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Pdate") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Punchs"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblTime"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Edatetime") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Break"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblBreak"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BreakMins") %>' ></asp:Label>
                                    <asp:Label ID="lblPunchs"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Punchs") %>' Visible="false" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
         </div>
        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                gridviewScroll();
            });

            function gridviewScroll() {
                $('#<%=grdInOut.ClientID%>').gridviewScroll({
                    width: window.innerWidth - 750,
                    height: window.innerHeight - 120,
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
