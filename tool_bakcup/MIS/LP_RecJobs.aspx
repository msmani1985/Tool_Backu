<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LP_RecJobs.aspx.cs" Inherits="LP_RecJobs" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .gridP
        {
        background:Orange;
        font-weight:bold;
        color:White;
        }
        .gridC
        {
        background:Gray;
        font-weight:bold;
        color:White;
        }
        .gridDel
        {
        background:Green;
        font-weight:bold;
        color:White;
        }
        .gridWIP
        {
        background:LightGreen;
        font-weight:bold;
        color:White;
        }
        </style>
</head>
<body class="LightBackGound" style="background-repeat:no-repeat; width:100%; height:100%">
    <form id="form1" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"> </asp:ToolkitScriptManager>
    <%-- <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
            <ProgressTemplate>           
              <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" 
                      ImageUrl="~/images/animation.PNG" AlternateText="Loading ..." 
                      ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" 
                      Height="40px" />
              </div>         
            </ProgressTemplate>
        </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table align="center" cellpadding="0" cellspacing="0" 
                style="border:1px solid green;align:center;">
                 <tr>
                    <td align="center" class="darkTitle" height="25px" valign="middle">
                        &nbsp;</td>
                     <td align="center" class="darkTitle" colspan="4" height="25px" valign="middle">
                         <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                             Font-Size="13px" Text="Search"></asp:Label>
                     </td>
                     <td align="center" class="darkTitle" height="25px" valign="middle">
                         &nbsp;</td>
                </tr>
                 
                
                
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td width="8px">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                    <td width="5px">
                        
                        &nbsp;</td>
                    <td width="5px">
                        &nbsp;</td>
                </tr>
                
                
                 <tr>
                     <td>
                         &nbsp;</td>
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
                     <td width="5px">
                     </td>
                     <td width="5px">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                     <td width="5px">
                         &nbsp;</td>
                     <td width="5px">
                         &nbsp;</td>
                 </tr>
                
                
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="Label59" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="End Date" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label60" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
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
                     <td width="5px" align="left">
                       <%--  <asp:ImageButton ID="imgEndCalendar" runat="server" Height="20px" 
                             ImageUrl="~/images/Calender.jpg" />--%>
                     </td>
                     <td align="left" width="5px">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                     <td align="left" width="5px">
                         &nbsp;</td>
                     <td align="left" width="5px">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="Label64" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Customer" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label65" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left" colspan="2">
                           <asp:ListBox ID="lstCustomer" runat="server" Width="350px" Height="70px"></asp:ListBox>
                         </td>
                   
                     <td align="left">
                         &nbsp;</td>
                   
                 </tr>
                
                
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
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
                                <td>
                                    &nbsp;</td>
                                <td>
                                   <asp:Button ID="btnExcel" runat="server" CssClass="dpbutton" 
                                        onclick="btnExcel_Click" Text="Excel" ToolTip="Save" Width="70px" />
                                </td>
                            </tr>
                        </table>    
                     </td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
            </table>   

            <table width="100%">
                <tr>
                    <td></td>
                    <td align="center">
                        
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="11px" ForeColor="Red"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" ClientIDMode="Static">
                            <ContentTemplate>
                    <%--<asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="340px" width="100%">--%>
                        <asp:GridView ID="grdProductionReport" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                CssClass="lightbackground" Width="1100px" OnRowDataBound="grdProductionReport_RowDataBound">
                            <HeaderStyle CssClass="darkbackground"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                            <Columns>
                                 <asp:TemplateField SortExpression="slno" HeaderText="Sl.No" >
                                    <ItemTemplate>
                                            <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label>
                                            <br />
                                    <asp:HiddenField ID="hfgvNLID" runat="server" Value='<%# Eval("LP_ID") %>' />
                                    <asp:HiddenField ID="hfgvProjectname" runat="server" Value='<%# Eval("projectname") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="jobid" HeaderText="JOBID"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvJobid" runat="server" Text='<%# Eval("Jobid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("CREATED_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                              
                                <asp:TemplateField SortExpression="due_date" HeaderText="Due Date From" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDueDate" runat="server" Text='<%# Eval("due_datefrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="due_dateto" HeaderText="Due Date To" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDueDateTo" runat="server" Text='<%# Eval("due_dateTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="due_Timefrom" HeaderText="Due Time (IST) From" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDueTimeFrom" runat="server" Text='<%# Eval("DueTimeFrom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="due_Timeto" HeaderText="Due Time (IST) To" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPDueTimeTo" runat="server" Text='<%# Eval("DueTimeTo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="File" HeaderText="No. of Files" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvFilecount" runat="server" Text='<%# Eval("File_Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="File" HeaderText="No. of Pages" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPagecount" runat="server" Text='<%# Eval("PAGES_COUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DropStatus" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WO / PO Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblJobNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Jobno") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                    </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID = "grdProductionReport" />
                            </Triggers> 
                        </asp:UpdatePanel>
                    <%--</asp:Panel>--%>
                        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
                        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
                        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
                        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                gridviewScroll();
                            });

                            function gridviewScroll() {
                                $('#<%=grdProductionReport.ClientID%>').gridviewScroll({
                                    width: window.innerWidth - 10,
                                    height: window.innerHeight - 200,
                                    startHorizontal: 0,
                                    barhovercolor: "#848484",
                                    barcolor: "#848484"
                                });
                            }
                        </script>
                    </td>
                </tr>
            </table>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>  --%>
    </form>
</body>
</html>

