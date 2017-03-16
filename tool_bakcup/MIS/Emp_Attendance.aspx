<%@ Page maintainScrollPositionOnPostBack="true" Language="C#" AutoEventWireup="true" CodeFile="Emp_Attendance.aspx.cs" Inherits="Emp_Attendance" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register assembly="PdfViewer" namespace="PdfViewer" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
     <style type="text/css">
    .FixedHeader {
                font-weight:bold; color:White; background-color:Green;
            }   
        .auto-style1 {
            height: 2px;
            width: 202px;
        }
        .auto-style2 {
            height: 45px;
        }
     </style>
     <link href="default.css" type="text/css" rel="stylesheet" />
     <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">Attendance Details</div>
        <div class="content" id="divReports" runat="server" >
           <table class="bordertable" style="width: 780px; height: 60px">
               <tr>
                   <td align="center" style="height: 2px; width: 126px;">
                       Start Date:</td>
                   <td style="height: 2px; width: 202px;">
                       <asp:TextBox ID="txtsdate" runat="server" Text=""></asp:TextBox>
                       <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                           src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                           border-left-style: none; border-bottom-style: none" /></td>
                   <td align="center" style="height: 2px; width: 118px;">
                       End Date:</td>
                   <td style="height: 2px; width: 207px;">
                       <asp:TextBox ID="txtedate" runat="server"></asp:TextBox>
                       <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                           src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                           border-left-style: none; border-bottom-style: none" />
                   </td>
                   <td rowspan="1" valign="middle" class="auto-style1">
                       <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                           Text="View" OnClick="BtnSubmit_Click"   /></td>
            
              </tr>
               <%--<tr>
                   <td colspan="5" align="center" class="auto-style2">
                       <asp:Label ID="lblTotalHrs" Text="Total Hrs:" runat="server" style="font-weight: 700" Visible="false"></asp:Label>
                       <asp:Label  ID="txtTotalHrs" Width="100" runat="server" Visible="false"></asp:Label>
               
                       <asp:Label ID="lblMis" Text="Late Mins :" runat="server" style="font-weight: 700" Visible="false"></asp:Label>
                       <asp:Label ID="txtLateMins" runat="server" Visible="false"></asp:Label>
                   </td>
               </tr>--%>
               <tr>
                    <td colspan="5" align="center"    >
                        <asp:GridView ID="grvrpt" runat="server"  autogeneratecolumns="false" 
                            EmptyDataText="No data available."  HeaderStyle-CssClass="FixedHeader" Font-Size="9pt">
                                     
                                <AlternatingRowStyle Width="100" BackColor="#F2F2F2" />
                                <Columns>
                                    <asp:boundfield datafield="UserID"   headertext="UserID"/>
                                    <asp:boundfield datafield="UserName"   headertext="UserName"/>
                                    <asp:boundfield datafield="ProcessDate"  headertext="Processdate"/>
                                    <asp:boundfield datafield="Punch1_Time"   headertext="InTime"/>
                                    <asp:boundfield datafield="Outpunch"   headertext="OutTime"/>
                                    <asp:boundfield datafield="LateIn_HHMM"   headertext="LateIn"/>
                                    <asp:boundfield datafield="EarlyOut_HHMM"  headertext="EarlyOut"/>
                                    <asp:boundfield datafield="Total_wrk_Time"   headertext="Total WorkTime"/>
                                    <asp:boundfield datafield="Outtime"   headertext="Break"/>
                                    <asp:boundfield datafield="WorkTime_HHMM"    headertext="WorkTime"/>
                                    <asp:boundfield datafield="Status"  headertext="Status"/>
                                    <asp:boundfield datafield="Permins"  headertext="Permission"/>
                                </Columns>
                            </asp:GridView>
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
               <tr>
                    <td colspan="8">
                            <%--<cc1:ShowPdf ID="ShowPdf1" Width="1000px" Height="200px" runat="server" />--%>
                        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" EnableDatabaseLogonPrompt="False" 
                        ReuseParameterValuesOnRefresh="true" HasCrystalLogo="false" HasDrillUpButton="false" HasGotoPageButton="true" 
                        HasRefreshButton="false" HasSearchButton="false" HasToggleGroupTreeButton="false"  HasZoomFactorList="false" ShowAllPageIds="True" ></cr:crystalreportviewer>
                    </td>
                </tr>
           </table>
        </div>
    </div>
    </form>
</body>
</html>
