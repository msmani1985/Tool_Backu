﻿<%@ page language="C#" autoeventwireup="true" inherits="PR_IssuesReceived, App_Web_xuje0h3i" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
    </head>
<body class="LightBackGound" style="background-repeat:no-repeat; width:100%; height:100%">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
            <ProgressTemplate>           
              <div style="position: fixed; text-align: center;  height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" 
                      ImageUrl="~/images/animation.PNG" AlternateText="Loading ..." 
                      ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" 
                      Height="40px" />
              </div>         
            </ProgressTemplate>
        </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                    <table align="center" cellpadding="0" cellspacing="0" 
                style="border:1px solid green;align:center;">
                 <tr>
                    <td align="center" class="darkTitle" height="25px" valign="middle">
                        </td>
                     <td align="center" class="darkTitle" colspan="8" height="25px" valign="middle">
                         <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                             Font-Size="13px" Text="Issues Received - Search"></asp:Label>
                     </td>
                </tr>
                 
                
                
                <tr>
                    <td>
                        </td>
                    <td>
                        </td>
                    <td width="8px" height="8px">
                        </td>
                    <td align="left">
                        </td>
                    <td align="left">
                        </td>
                    <td align="left">
                        </td>
                    <td align="left" height="8px">
                        </td>
                    <td align="left">
                        </td>
                    <td align="left">
                        </td>
                </tr>
                
                
                 <tr>
                     <td width="5px">
                         </td>
                     <td>
                         <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Start Date" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label58" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left">
                         <table cellpadding="0" cellspacing="0">
                             <tr>
                                 <td>
                                     <asp:TextBox ID="txtStartDate" runat="server" class="txtBoxMedium"></asp:TextBox>
                                     <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
                                         Enabled="True" PopupButtonID="imgCalendar" TargetControlID="txtStartDate">
                                     </asp:CalendarExtender>
                                 </td>
                                 <td>
                                     <asp:ImageButton ID="imgCalendar" runat="server" Height="20px" 
                                         ImageUrl="~/images/Calender.jpg" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                     <td align="left">
                         </td>
                     <td align="left">
                         <asp:Label ID="Label59" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="End Date" Width="75px"></asp:Label>
                     </td>
                     <td align="left"  width="8px">
                         <asp:Label ID="Label71" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left">
                         <table cellpadding="0" cellspacing="0">
                             <tr>
                                 <td align="left">
                                     <asp:TextBox ID="txtEndDate" runat="server" class="txtBoxMedium"></asp:TextBox>
                                     <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                                         Enabled="True" PopupButtonID="imgEndCalendar" TargetControlID="txtEndDate">
                                     </asp:CalendarExtender>
                                 </td>
                                 <td align="left" width="5px">
                                     <asp:ImageButton ID="imgEndCalendar" runat="server" Height="20px" 
                                         ImageUrl="~/images/Calender.jpg" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                     <td align="left" width="5px">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         </td>
                     <td width="8px">
                         </td>
                     <td align="left" height="2px">
                         </td>
                     <td align="left">
                         </td>
                     <td align="left">
                         </td>
                     <td align="left">
                         </td>
                     <td align="left">
                         </td>
                     <td align="left">
                         </td>
                 </tr>
                
                
                 <tr>
                     <td>
                         </td>
                     <td>
                         <asp:Label ID="Label64" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Customer" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label72" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left" colspan="5">
                         <asp:ListBox ID="lstCustomer" runat="server" SelectionMode="Multiple" 
                             Width="332px"></asp:ListBox>
                     </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         </td>
                     <td width="8px">
                         </td>
                     <td align="left" colspan="5" height="2px">
                         </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         <asp:Label ID="Label76" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Issue Stage"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label73" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left" colspan="5">
                         <asp:RadioButtonList ID="RdbIssueStage" runat="server" 
                             RepeatDirection="Horizontal">
                             <asp:ListItem Selected="True" Value="0">P100</asp:ListItem>
                             <asp:ListItem Value="1">P200</asp:ListItem>
                             <asp:ListItem Value="2">S300</asp:ListItem>
                         </asp:RadioButtonList>
                     </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         </td>
                     <td width="8px">
                         </td>
                     <td align="left" colspan="5" height="2px">
                         </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="5">
                         <asp:CheckBox ID="chkInHouse" runat="server" Text="Inhouse" />
                     </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         </td>
                     <td width="8px">
                         </td>
                     <td align="left" colspan="5" height="2px">
                         </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         </td>
                     <td width="8px">
                         </td>
                     <td align="left" colspan="5">
                         <table cellpadding="0" cellspacing="0">
                             <tr>
                                 <td>
                                     <asp:Button ID="btnReport" runat="server" CssClass="dpbutton" 
                                         onclick="btnReport_Click" Text="Report" ToolTip="Save" Width="70px" />
                                 </td>
                                 <td width="5px">
                                 </td>
                                 <td>
                                     <asp:Button ID="btnClear" runat="server" CssClass="dpbutton" 
                                         onclick="btnClear_Click" Text="Clear" ToolTip="Save" Width="70px" />
                                 </td>
                                 <td width="5px">
                                     </td>
                                 <td>
                                     <asp:Button ID="btnExcel" runat="server" CssClass="dpbutton" 
                                         onclick="btnExcel_Click" Text="Excel" ToolTip="Save" Width="70px" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                     <td align="left">
                         </td>
                 </tr>
                 <tr>
                     <td>
                         </td>
                     <td>
                         </td>
                     <td width="8px">
                         </td>
                     <td align="left" colspan="5" height="8px">
                         &nbsp;</td>
                     <td align="left" valign="top">
                         </td>
                 </tr>
                        <tr>
                            <td align="center" colspan="9">
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                                    Font-Size="11px" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                 </table>
              
                    <table width="100%">
                <tr>
                    <td align="left">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblResult" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text="Total"
                                        Font-Size="11px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text=":"
                                        Font-Size="11px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblTotal" runat="server" Font-Bold="True" Font-Names="Segoe UI" ForeColor="Blue"
                                        Font-Size="11px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="300px" width="100%">
                        <asp:GridView ID="grdProductionReport" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Left" 
                            CellPadding="4" Font-Names="Segoe UI" Font-Size="11px" ForeColor="#333333" 
                            GridLines="Vertical"  Width="100%" ShowHeaderWhenEmpty="True" 
                            >
                            <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" /> 
                            <rowstyle backcolor="white" />
                            <alternatingrowstyle backcolor="#F0FFF0" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="Label1" runat="server" Text="<%# iRowId++ %>"></asp:Label>--%>
                                        <asp:Label ID="Label66" runat="server" Text='<%# Eval("ROWID") %>' Width="30px"></asp:Label>
                                        <br />
                                        <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="JOURNAME" HeaderText="JOURNAME" SortExpression="JOURNAME" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="IISSUENO"  SortExpression="IISSUENO" HeaderText="Issue No" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="AREALNOOFPAGES" HeaderText="AREALNOOFPAGES" SortExpression="AREALNOOFPAGES" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="FINSITENO" HeaderText="FINSITENO" SortExpression="FINSITENO" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="AREALNOOFPAGES1" SortExpression="AREALNOOFPAGES1" HeaderText="Pages" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="NOARTICLES" HeaderText="NOARTICLES" SortExpression="NOARTICLES" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="COUNTCOLOUR" HeaderText="COUNTCOLOUR" SortExpression="COUNTCOLOUR" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="COUNTECOLOUR" HeaderText="COUNTECOLOUR" SortExpression="COUNTECOLOUR" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="MAKEUP_RECD_DATE" HeaderText="MAKEUP_RECD_DATE" SortExpression="MAKEUP_RECD_DATE" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="REVISE_SENT_DATE" HeaderText="REVISE_SENT_DATE" SortExpression="REVISE_SENT_DATE" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="REVISE_BACK_DATE" HeaderText="REVISE_BACK_DATE" SortExpression="REVISE_BACK_DATE" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="SENT_CATS" HeaderText="SENT_CATS" SortExpression="SENT_CATS" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="SENT_PRINTER" HeaderText="SENT_PRINTER" SortExpression="SENT_PRINTER" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="PRINTNAME" HeaderText="PRINTNAME" SortExpression="PRINTNAME" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="ICREATIONDATE" SortExpression="ICREATIONDATE" HeaderText="Received Date" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="IDUEDATE" SortExpression="IDUEDATE" HeaderText="Due Date" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="IP100DUE" HeaderText="IP100DUE" SortExpression="IP100DUE" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="IINVOICED" HeaderText="IINVOICED" SortExpression="IINVOICED" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="COMPLETED_FLAG" HeaderText="COMPLETED_FLAG" SortExpression="COMPLETED_FLAG" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="STYPENAME" SortExpression="STYPENAME" HeaderText="Stage" Visible="true" ></asp:BoundField>
                                <asp:BoundField DataField="CUSTNAME" SortExpression="CUSTNAME" HeaderText="Customer" Visible="true"></asp:BoundField>
                                <asp:BoundField DataField="SENT_TECHSET" HeaderText="SENT_TECHSET" SortExpression="SENT_TECHSET" Visible="false"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </td>
                </tr>
            </table>
              
            

            
        </ContentTemplate>
    </asp:UpdatePanel>        
  </form>
</body>
</html>