<%@ page language="C#" autoeventwireup="true" inherits="EmpAttDaily, App_Web_xuje0h3i" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
    <style type="text/css">
         .FixedHeader {
                font-weight:bold; color:White; background-color:Green;
                      }   
        .auto-style1 {
            width: 256px;
        }
        .auto-style2 {
            width: 286px;
        }
     </style>
     <style type="text/css">
        .gridE
        {
        background:Green;
        font-weight:bold;
        color:White;
        }
        .gridD
        {
        background:Red;
        font-weight:bold;
        color:White;
        }
        .gridN
        {
            background:white;
            font-weight:normal;
            color:black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Attendance Details</div>
    <div class="content" id="divReports" runat="server" >
       <table class="bordertable" >
           <tr>
               <td  class="auto-style2" >
                   Start Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:TextBox ID="Txtsdate" runat="server" Text="" Height="16px" Width="100px"></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" /></td>
               <td class="auto-style1" >
                   End Date:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="Txtedate"  Height="16px" Width="100px" runat="server"></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" />
               </td>
          </tr>
          <tr >      
               <td  class="auto-style2">
                    <asp:Label ID="EmployeeLbl" runat="server" Text="Emp Name/No." Width="111px"></asp:Label>
                    <asp:TextBox ID="EmpNoNameTxt"  Height="16px" Width="150px" runat="server"></asp:TextBox>
               </td>
               <td rowspan="1" valign="middle" class="auto-style1">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"  />
                       &nbsp;
                       <asp:Button ID="btnClear" runat="server" CssClass="dpbutton"
                       Text="Clear" OnClick="btnClear_Click"   />
                    &nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgbtnEventExport" runat="server" ImageUrl="~/images/QMS/excel.png" 
                    ToolTip="Export Excel" Width="25px" OnClick="imgbtnEventExport_Click" />
               </td>
           </tr>
           <%-- <tr>
                <td colspan="6" ></td>
            </tr>
            <tr >
                <td align="right" class="auto-style2">
                    <asp:Label ID="lblTotalHrs"  runat="server" Text="Total Working Hours:" Visible="False" Height="16px" Width="169px" Font-Bold="True"></asp:Label>
                </td>
                <td class="auto-style1" >
                    <asp:Label ID="txtTotalHrs" runat="server" Visible="False" Width="72px" Font-Bold="True" Height="23px" ></asp:Label>
                </td>
            </tr>--%>
           </table>
            <table>
                <tr >
                    <td colspan="8" align="center" >
                        <table>
                            <tr>
                                <td align="left"  >
                                    <asp:GridView ID="grvrpt" runat="server"  CaptionAlign="Left"
                                        AutoGenerateColumns="False" CellPadding="4" 
                                        ForeColor="#333333" BorderStyle="Solid"  
                                        GridLines="Vertical" Font-Names="Segoe UI" Font-Size="11px" 
                                        EmptyDataText="No data available."  OnRowDataBound="grvrpt_RowDataBound">
                                         <HeaderStyle CssClass="header" />
                                         <rowstyle backcolor="white"/>
                                        <alternatingrowstyle backcolor="#F0FFF0"/>
                                         <Columns>
                                             <asp:TemplateField HeaderText="UserID" ItemStyle-HorizontalAlign="Center" >
                                             <ItemTemplate>
                                                 <asp:Label ID="UserID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserID") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UserName">
                                             <ItemTemplate>
                                                 <asp:Label ID="UserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmpName") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Processdate" ItemStyle-HorizontalAlign="Center" >
                                             <ItemTemplate>
                                                 <asp:Label ID="ProcessDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PDate") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField> 
                                            <asp:boundfield datafield="InTime"  headertext="In"/>
                                            <asp:boundfield datafield="OutTime"  headertext="Out"/>
                                            <asp:boundfield datafield="Totalmins"  headertext="Total Hrs"/>
                                            <asp:boundfield datafield="BreakMins"  headertext="Break"/>
                                            <asp:boundfield datafield="WrkMins"  headertext="Working Hrs"/>
                                            <asp:boundfield datafield="Status"  headertext="Status"/>
                                             <asp:TemplateField HeaderText="LateIn">
                                             <ItemTemplate>
                                                 <asp:Label ID="LateIn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LateIn") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EarlyOut" ItemStyle-HorizontalAlign="Center" >
                                             <ItemTemplate>
                                                 <asp:Label ID="EarlyOut" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EarlyOut") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField> 
                                            <%--<asp:boundfield datafield="LateIn"  headertext="LateIn"/>
                                            <asp:boundfield datafield="EarlyOut"  headertext="EarlyOut"/>--%>
                                            <asp:boundfield datafield="OT"  headertext="OverTime"/>
                                            <asp:boundfield datafield="Designation"   headertext="Designation"/>
                                            <asp:TemplateField HeaderText="ShiftName" ItemStyle-HorizontalAlign="Center" >
                                             <ItemTemplate>
                                                 <asp:Label ID="ShiftName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ShiftName") %>'></asp:Label>
                                                 <asp:Label ID="ShiftSta" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"SftStatus") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField> 
                                            <%--<asp:boundfield datafield="ShiftName"   headertext="Shift Name"/>--%>
                                        </Columns>
                                        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
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
                                $('#<%=grvrpt.ClientID%>').gridviewScroll({
                                    width: 1150,
                                    height: 360,
                                    startHorizontal: 0,
                                    barhovercolor: "#848484",
                                    barcolor: "#848484"
                                });
                            }
                        </script>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
