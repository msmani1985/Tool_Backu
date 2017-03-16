<%@ page language="C#" autoeventwireup="true" inherits="JobTrackLaunch, App_Web_zfrrxy20" %>
<meta http-equiv="refresh" content="30">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
        <script type = "text/javascript">
            function BlockUI(elementID) {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(function () {
                    $("#" + elementID).block({
                        message: '<table align = "center"><tr><td>' +
                 '<img src="images/loadingAnim.gif"/></td></tr></table>',
                        css: {},
                        overlayCSS: {
                            backgroundColor: '#000000', opacity: 0.6
                        }
                    });
                });
                prm.add_endRequest(function () {
                    $("#" + elementID).unblock();
                });
            }
            function Hidepopup() {
                $find("popup").hide();
                return false;
            }
            function Hidepopup1() {
                $find("popup1").hide();
                return false;
            }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
        <div class="dptitle" runat="server" id="divheader">Production Job Tracking:</div>
    <div>


        <div runat="server" id="grid"  class="divgrid">
        <table style="border:1px solid green;align:center;" align="center" >
            <tr>
                    <td align="center" class="darkTitle" colspan="8">Apply Filters</td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" Text="Customer :" Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
                </td>
                <td> 
                    <asp:DropDownList ID="ddlcustomer" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlcustomer_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </td>
                <td></td>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" Text="Location :" Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLocation" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlLocation_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </td>
                <td></td>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="Task :" Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
                </td>
                <td> 
                    <asp:DropDownList ID="ddlTask" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlTask_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Text="DueDate :" Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDueDate" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlDueDate_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </td>
                <td></td>
                <td align="right">
                    <asp:Label ID="Label11" runat="server" Text="DueTime :" Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDueTime" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlDueTime_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </td>
                <td></td>
                <td align="right">
                    <asp:Label ID="Label13" runat="server" Text="Status :" Font-Names="Segoe UI" Font-Size="12px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
        </table>
            <br />
        <table style="border:1px solid green;align:center;" align="center" >
        <tr>
            <td colspan="10">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvJobTrack" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                    CssClass="lightbackground" Width="874px" OnRowDataBound="gvJobTrack_RowDataBound" >
                    <HeaderStyle CssClass="darkbackground"  />
                    <AlternatingRowStyle BackColor="#F2F2F2" />
                    <Columns>
                        <asp:TemplateField SortExpression="slno" HeaderText="Sl.No">
                            <ItemTemplate>
                                <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("slno") %>' ></asp:Label> 
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text = "Click" OnClick="Click"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="JobID" HeaderText="JobID"  >
                            <ItemTemplate>
                                <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                                <asp:HiddenField ID="hid_NL_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"LP_ID") %>' />
                               <%-- <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                <asp:HiddenField ID="hid_FP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' />--%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="ProjectName" HeaderText="Project Name"  >
                            <ItemTemplate>
                                <asp:Label ID="lblProjectName"  runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Custname" HeaderText="Customer"  >
                            <ItemTemplate>
                                <asp:Label ID="lblCustname" runat="server" Text='<%# Eval("Custname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Location_Name" HeaderText="Location"  >
                            <ItemTemplate>
                                <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location_Name") %>'></asp:Label>
                                <asp:HiddenField ID="hid_loc_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Location_ID") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="TaskName" HeaderText="Task"  >
                            <ItemTemplate>
                                <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
                              <%--  <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />--%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FileName" HeaderText="File Name" >
                            <ItemTemplate>
                                <asp:Label ID="lblgvFiles" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DUE_DATEFROM" HeaderText="DUE DATE" >
                            <ItemTemplate>
                                <asp:Label ID="lblDUE_DATEFROM" Width="60" runat="server" Text='<%# Eval("DUE_DATEFROM") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DUE_TimeFrom" HeaderText="DUE Time" >
                            <ItemTemplate>
                                <asp:Label ID="lblDUE_TimeFrom" Width="60" runat="server" Text='<%# Eval("DUE_TimeFrom") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DUEDateFROM_IST" HeaderText="DUE Date (IST)" >
                            <ItemTemplate>
                                <asp:Label ID="lblDUEDateFROM_IST" Width="60" runat="server" Text='<%# Eval("Date_IST") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DUETIMEFROM_IST" HeaderText="Due Time (IST)" >
                            <ItemTemplate>
                                <asp:Label ID="lblDUETIMEFROM_IST" Width="60" runat="server" Text='<%# Eval("Times_IST") %>'></asp:Label>
                                <asp:HiddenField ID="hid_Datetime_IST" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"DateTime_IST") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Pages" HeaderText="Pages" >
                            <ItemTemplate>
                                <asp:Label ID="lblgvPages" Width="50" runat="server" Text='<%# Eval("Pages") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" Wrap="False" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="Stage" HeaderText="Stage" >
                            <ItemTemplate>
                                <asp:Label ID="lblgvStage" Width="80" runat="server" Text='<%# Eval("AmendName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" Wrap="False" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="WorkFlow" HeaderText="WorkFlow" >
                            <ItemTemplate>
                                <asp:Label ID="lblgvWorkFlow" Width="80" runat="server" Text='<%# Eval("WorkFlow") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" Wrap="False" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="DelStatus" HeaderText="Status" Visible="false">
                            <ItemTemplate>
                                <asp:DropDownList ID="dropDelStatus" runat="server" Enabled="false"></asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" Wrap="False" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                       
                    </Columns>
                    </asp:GridView>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
                    </td>
            </tr>
        </table>
        </div>
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
        $('#<%=gvJobTrack.ClientID%>').gridviewScroll({
            width: 1165,
            height: 380,
            startHorizontal: 0,
            barhovercolor: "#848484",
            barcolor: "#848484"
        });
    }
</script>
    </form>
</body>
</html>
