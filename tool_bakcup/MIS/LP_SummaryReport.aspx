<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_SummaryReport.aspx.cs" Inherits="LP_SummaryReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
    <div class="dptitle">Launch Summary</div>
       <table>
           <tr>
               <td colspan="4">
                    <table>
                        <tr>
                            <td align="center" class="auto-style2">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <strong>Customer</strong>
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="200px" TabIndex = "1" OnSelectedIndexChanged="drpCustomerSearch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <strong>Location</strong>
                                        <asp:DropDownList ID="DropLocation" runat="server" Width="150px" TabIndex = "1">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" class="auto-style3">
                                <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click"  />
                            </td>
                            <td rowspan="3">
                                <asp:GridView ID="gv_Summary" runat="server"  AutoGenerateColumns="false"
                                    EmptyDataText="No data available." Font-Size="8pt" 
                                    PageSize="7" DataKeyNames="Description" Width="318px">
                                    <HeaderStyle CssClass="GVFixedHeader" />
                                    <AlternatingRowStyle BackColor="#F2F2F2" />
                                    <Columns>
                                        <asp:BoundField DataField="Description" HeaderText="Description"  ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Jobs" HeaderText="Jobs" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Pages" HeaderText="Pages"  ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Values" HeaderText="Values"  ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="auto-style2">
                                Start Date:
                                <asp:TextBox ID="txtsdate" runat="server" Width="100px"></asp:TextBox>
                                <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                                    src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                                    border-left-style: none; border-bottom-style: none" />
                                End Date:
                                <asp:TextBox ID="txtedate" runat="server" Width="100px"></asp:TextBox>
                                <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                                   src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                                   border-left-style: none; border-bottom-style: none" />
                            </td>
                            <td class="auto-style3">

                            </td>
                        </tr>
                        <tr>
                            <td  align="CENTER">ProjectName / JobID : 
                                <asp:TextBox ID="txtProjectName" runat="server" Width="364px"></asp:TextBox>
                            </td>
                            <td></td>
                            <td align="right" colspan="2">
                                <asp:ImageButton id="cmd_Excel" tabIndex="41"  runat="server" ImageUrl="~/images/tools/j_excel.png" 
                                    ToolTip="Launch Overview" OnClick="cmd_Excel_Click"></asp:ImageButton>&nbsp;
                            </td>
                        </tr>
                    </table>
               </td>
           </tr>
           <tr>
               <td colspan="4">
                   <asp:GridView ID="grdLaunchView" runat="server"  AutoGenerateColumns="false"
                        EmptyDataText="No data available." Font-Size="8pt" 
                        PageSize="7" DataKeyNames="ProjectName">
                        <HeaderStyle CssClass="GVFixedHeader" />
                        <AlternatingRowStyle BackColor="#F2F2F2" />
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>Serial No.</HeaderTemplate>
                                <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="JobID" HeaderText="JobID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Cust_Name" HeaderText="Cust_Name" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Pages" HeaderText="Pages" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Platform" HeaderText="Platform"  ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Process" HeaderText="Process"  ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QUOTE_REQUESTED" HeaderText="QUOTE REQUESTED"   ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QUOTE_SUBMITTED" HeaderText="QUOTE SUBMITTED"   ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Quote_Approved" HeaderText="QUOTE APPROVED"  ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PROJECT_VALUE" HeaderText="PROJECT VALUE"   ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QUOTE_STATUS" HeaderText="QUOTE STATUS"   ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PROJECT_RECEIVED" HeaderText="PROJECT RECEIVED"  ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PROJECT_DueDate" HeaderText="PROJECT DUE DATE"  ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PROJECT_DesDate" HeaderText="PROJECT DELIVERED DATE"  ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="INVOICE_STATUS" HeaderText="INVOICE STATUS"  ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
               </td>
           </tr>
        </table>
    </div>
    </form>
</body>
</html>
