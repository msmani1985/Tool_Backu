<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_DelReport.aspx.cs" Inherits="LP_DelReport"  EnableEventValidation="false"%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <title></title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
            <div>
                    <table align="center" cellpadding="0" cellspacing="0" style="border:1px solid green;align:center;">
                        <tr>
                            <td align="center" class="darkTitle" height="25px" valign="middle"></td>
                            <td align="center" class="darkTitle" colspan="8" height="25px" valign="middle">
                                <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                                    Font-Size="13px" Text="Delivery Report - Search"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td width="5px"></td>
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
                                            <img style="cursor:pointer; border: none" alt="Calendar" src="images/Calendar.jpg" height="20px" border="0" 
                                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
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
                                            
                                            <img style="cursor:pointer; border: none" alt="Calendar" src="images/Calendar.jpg" height="20px" border="0" 
                                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtEndDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                                        </td>
                                        <td align="left" width="5px">
                                            &nbsp;</td>
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
                                </td>
                            <td width="8px">
                                </td>
                            <td align="left" colspan="5">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnReport" runat="server" CssClass="dpbutton" 
                                                onclick="btnReport_Click" Text="Report" ToolTip="Report" Width="70px" />
                                        </td>
                                        <td width="5px">
                                        </td>
                                        <td>
                                            <asp:Button ID="btnClear" runat="server" CssClass="dpbutton" 
                                                onclick="btnClear_Click" Text="Clear" ToolTip="Clear" Width="70px" />
                                        </td>
                                        <td width="5px">
                                            </td>
                                        <td>
                                            <asp:Button ID="btnExcel" runat="server" CssClass="dpbutton" 
                                                onclick="btnExcel_Click" Text="Excel" ToolTip="Excel" Width="70px" />
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
                <table>
                    <tr>
                        <td align="left">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblResult" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text="Total Records"
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
                                        <td>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text="Pages"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text=":"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPages" runat="server" Font-Bold="True" Font-Names="Segoe UI" ForeColor="Red"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text="Total Pages"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Segoe UI" Text=":"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTPages" runat="server" Font-Bold="True" Font-Names="Segoe UI" ForeColor="Green"
                                            Font-Size="11px"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="2">
                                    <asp:GridView ID="grdDeliveryReport" runat="server" EmptyDataText="No data available." 
                                        AutoGenerateColumns="False" 
                                          OnRowDataBound="grdDeliveryReport_RowDataBound" Width="16px">
                                        <HeaderStyle CssClass="GVFixedHeader"/> 
                                        <AlternatingRowStyle BackColor="#F2F2F2" />
                                        <Columns>
                                            <asp:TemplateField SortExpression="slno" HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblslno"  runat="server" Text='<%# Eval("slno") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="JOBID" HeaderText="JOBID"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJOBID"  runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PROJECTNAME" HeaderText ="PROJECTNAME"/>
                                            <asp:TemplateField SortExpression="Files_Name" HeaderText="File Name"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFiles_Name"  runat="server" Text='<%# Eval("Files_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="AmendName" HeaderText="Amend Name"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmendName"  runat="server" Text='<%# Eval("AmendName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Pages" HeaderText="Pages"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPages"  runat="server" Text='<%# Eval("Pages") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="EmpName" HeaderText="EmpName"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName"  runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="DateTime_IST" HeaderText="Actual Due Date"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActDueDate"  runat="server" Text='<%# Eval("DateTime_ISTS") %>'></asp:Label>
                                                    <asp:Label ID="lblActDueDateS"  runat="server" Visible="false" Text='<%# Eval("DateTime_IST") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Despatch_Date" HeaderText="Despatch Date"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesDate"  runat="server" Text='<%# Eval("Despatch_DateS") %>'></asp:Label>
                                                    <asp:Label ID="lblDesDateS"  runat="server" Visible="false" Text='<%# Eval("Despatch_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Status"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus"  runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="CUSTNAME" HeaderText="Customer"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCUSTNAME"  runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="LOCATION_NAME" HeaderText="Location"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLOCATION_NAME"  runat="server" Text='<%# Eval("LOCATION_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="TASKNAME" HeaderText="Task"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTASKNAME"  runat="server" Text='<%# Eval("TASKNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:DropDownList OnSelectedIndexChanged="dd_SoftName_OnSelectedIndexChanged" AutoPostBack="true" 
                                                        ID="dd_SoftName" runat="server" ></asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"Soft_Name") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:DropDownList OnSelectedIndexChanged="dd_LANGNAME_OnSelectedIndexChanged" AutoPostBack="true" 
                                                        ID="dd_LANGNAME" runat="server" ></asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem,"LANG_NAME") %>
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
                    $('#<%=grdDeliveryReport.ClientID%>').gridviewScroll({
                        width: window.innerWidth - 10,
                        height: window.innerHeight - 100,
                        startHorizontal: 0,
                        barhovercolor: "#848484",
                        barcolor: "#848484"
                    });
                }
            </script>
        </form>
    </body>
</html>
