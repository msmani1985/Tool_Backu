<%@ page language="C#" autoeventwireup="true" inherits="LaunchJobTrack, App_Web_25d24vps" %>
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
<%--     <script type="text/javascript">

         $(document).ready(function () {

             try {

                 $(".divgrid").each(function () {

                     var grid = $(this).find("table")[0];

                     var ScrollHeight = $(this).height();

                     var gridWidth = $(this).width() - 20;

                     var headerCellWidths = new Array();

                     for (var i = 0; i < grid.getElementsByTagName('TH').length; i++) {

                         headerCellWidths[i] = grid.getElementsByTagName('TH')[i].offsetWidth;

                     }

                     grid.parentNode.appendChild(document.createElement('div'));

                     var parentDiv = grid.parentNode; var table = document.createElement('table');

                     for (i = 0; i < grid.attributes.length; i++) {

                         if (grid.attributes[i].specified && grid.attributes[i].name != 'id') {

                             table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);

                         }

                     }

                     table.style.cssText = grid.style.cssText;

                     table.style.width = gridWidth + 'px';

                     table.appendChild(document.createElement('tbody'));

                     table.getElementsByTagName('tbody')[0].appendChild(grid.getElementsByTagName('TR')[0]);

                     var cells = table.getElementsByTagName('TH');

                     var gridRow = grid.getElementsByTagName('TR')[0];

                     for (var i = 0; i < cells.length; i++) {

                         var width; if (headerCellWidths[i] > gridRow.getElementsByTagName('TD')[i].offsetWidth) {

                             width = headerCellWidths[i];

                         } else {

                             width = gridRow.getElementsByTagName('TD')[i].offsetWidth;

                         } cells[i].style.width = parseInt(width - 3) + 'px';

                         gridRow.getElementsByTagName('TD')[i].style.width = parseInt(width - 3) + 'px';

                     }

                     var gridHeight = grid.offsetHeight;

                     if (gridHeight < ScrollHeight)

                         ScrollHeight = gridHeight;

                     parentDiv.removeChild(grid);

                     var dummyHeader = document.createElement('div');

                     dummyHeader.appendChild(table); parentDiv.appendChild(dummyHeader);

                     var scrollableDiv = document.createElement('div');

                     if (parseInt(gridHeight) > ScrollHeight) {

                         gridWidth = parseInt(gridWidth) + 17;

                     }

                     scrollableDiv.style.cssText = 'overflow:auto;height:' + ScrollHeight + 'px;width:' + gridWidth + 'px';

                     scrollableDiv.appendChild(grid);

                     parentDiv.appendChild(scrollableDiv);

                 });

             }

             catch (err) { }

         }

     );

</script> --%>
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
     <style type="text/css">
        /*.divgrid
        {
            height: 390px;
            width: 1150px;
        }
        .divgrid table
        {
            width: 350px;
        }
        .divgrid table th
        {
            background-color: Green;
            color: #fff;
        }*/
    </style> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnableScriptGlobalization="true"></asp:ScriptManager>
        <div class="dptitle" runat="server" id="divheader">Production Job Tracking:</div>
    <div>
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
        
            <table>
                <tr>
                    <td colspan="4" align="right">
                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Both"  Height="415px" Width="1185px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" OnUnload="chkDueDate1_CheckedChanged">
                            <ContentTemplate>
                                <div runat="server" id="grid"  class="divgrid">
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
                            
                                    <asp:TemplateField SortExpression="JobID" HeaderText="JobID"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                                            <asp:HiddenField ID="hid_NL_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NL_ID") %>' />
                                            <asp:HiddenField ID="hid_NTLS_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"NTLS_ID") %>' />
                                            <asp:HiddenField ID="hid_FP_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"FP_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="ProjectName" HeaderText="Project Name"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblProjectName" Width="130px" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
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
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="TaskName" HeaderText="Task"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskName" runat="server" Text='<%# Eval("TaskName") %>'></asp:Label>
                                            <asp:HiddenField ID="hid_Task_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Task_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Lang_name" HeaderText="Language" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblLang_Name" runat="server" Text='<%# Eval("Lang_name") %>'></asp:Label>
                                            <asp:HiddenField ID="hid_Lang_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Lang_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Soft_Name"  HeaderText="Software" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSoft_Name" Width="70" runat="server" Text='<%# Eval("Soft_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="hid_Soft_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Soft_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Files_Name" HeaderText="File Name" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvFiles" Width="140px" runat="server" Text='<%# Eval("Files_Name") %>'></asp:Label>
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
                                    <asp:TemplateField SortExpression="DUETIMEFROM_IST" HeaderText="DUE Time (IST)" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblDUETIMEFROM_IST" Width="60" runat="server" Text='<%# Eval("DUETIMEFROM_IST") %>'></asp:Label>
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
                                    <asp:TemplateField SortExpression="DelStatus" HeaderText="Status" >
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dropDelStatus" runat="server" Enabled="false"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" Wrap="False" HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit1" runat="server" Text = "Click"  OnClick = "Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="New/Delivery" Visible="false" >
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="lnkdel" runat="server" Text = "New/Delivery"  OnClick = "OnDelivery"></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDelStatus" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" Wrap="False" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                </div>
                                <asp:Panel ID="pnlDel" Width="700" Height="320" runat="server" CssClass="modalPopup" style = "display:none">
                                    <asp:Label ID="Label8" runat="server" Text="Delivery Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                    <table align = "center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text="Project Title:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DelProName" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Text="Job ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="DelJobID" Enabled="false" runat="server"></asp:TextBox>
                                                <asp:Label ID="DelNL_ID" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="DelLoc_ID" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Stage Details :
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="dropCurStage" AutoPostBack="true" OnSelectedIndexChanged="dropCurStage_SelectedIndexChanged" runat="server">
                                                    <asp:ListItem Value="0" Selected="True">Current Stage</asp:ListItem>
                                                    <asp:ListItem Value="1">Next Stage</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"> 
                                                Delivery Time:
                                                <asp:TextBox ID="txtdeldateAll" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdeldateAll','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="img4"  runat="server" />
                                                <asp:DropDownList ID="dropDelHrsAll" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="dropDelHrsAll_SelectedIndexChanged">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="dropDelMinsAll" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="dropDelMinsAll_SelectedIndexChanged">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="dropDelZoneAll" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="dropDelZoneAll_SelectedIndexChanged" >
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="IST">IST</asp:ListItem>
                                                    <asp:ListItem Value="PST">PST</asp:ListItem>
                                                    <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                    <asp:ListItem Value="CET">CET</asp:ListItem>
                                                    <asp:ListItem Value="CST">CST</asp:ListItem>
                                                    <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                    <asp:ListItem Value="EST">EST</asp:ListItem>
                                                    <asp:ListItem Value="JST">JST</asp:ListItem>
                                                    <asp:ListItem Value="BST">BST</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="Label12" runat="server" Text="Delivery Time IST"></asp:Label>
                                                <asp:TextBox ID="txtDel_ISTAll" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                            <asp:Button ID="DelSave" runat="server" Text="Save" OnClick = "onDelSave" CssClass="dpbutton"/>
                                            <asp:Button ID="DelClose" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()" CssClass="dpbutton"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblResult2" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFake2" runat="server"></asp:LinkButton>
                                <asp:ModalPopupExtender ID="DelPopUp" runat="server" DropShadow="false"
                                PopupControlID="pnlDel" TargetControlID = "lnkFake2"
                                BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>

                                <asp:Panel ID="pnlAddEdit1" Width="700" Height="350" runat="server" CssClass="modalPopup" style = "display:none">
                                    <asp:Label ID="Label5" runat="server" Text="File Name & Pages Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                                    <table align = "center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text="Project Title:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProjectTitle1" runat="server" Enabled="false"></asp:TextBox>
                                                <asp:TextBox ID="txtFP_ID" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text="Job ID:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJobid1" Enabled="false" runat="server"></asp:TextBox>
                                                <asp:Label ID="NL_ID1" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Customer:</td>
                                            <td>
                                                <asp:TextBox ID="drpProjectcustomer1" runat="server" CssClass="TxtBox" Width="200px"  Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>Location:</td>
                                            <td> 
                                                <asp:TextBox ID="DropLocation1" runat="server" CssClass="TxtBox"  Enabled="false"></asp:TextBox>   
                                                <asp:HiddenField ID="hid_Loc_ID" runat="server" Visible="false"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Project Editor:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProjectEditor1" runat="server" CssClass="TxtBox"  Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                Target Rec. Date:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRecDate1" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtRecDate1','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueFromdate1"  runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Due Date:
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lblDueFrom1" runat="server" Text="From:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtdueFromdate1" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueFromdate1','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueFromdate"  runat="server" />
                                                <asp:Label ID="lblDueTo1" runat="server" Text="To:" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtdueTodate1" runat="server" CssClass="TxtBox" Visible="false" Width="80px"></asp:TextBox>
                                                <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtdueTodate1','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dueTodate1"  runat="server" visible="false" />
                                                <asp:CheckBox ID="chkYTC1" Text="YTC" runat="server" />
                                                <asp:CheckBox ID="chkDueDate1" Text="Staggered Delivery" runat="server"  AutoPostBack="True" OnCheckedChanged="chkDueDate1_CheckedChanged" />
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>Due Time:</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblFrom1" runat="server" Text="From:" Visible="False"></asp:Label>
                                                <asp:DropDownList ID="DropDueTimeFrom1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTime1_SelectedIndexChanged">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                </asp:DropDownList><asp:DropDownList ID="DropDueMinFrom1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMin1_SelectedIndexChanged">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="DropDueTimeZoneFrom1" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneFrom1_SelectedIndexChanged" >
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="IST">IST</asp:ListItem>
                                                    <asp:ListItem Value="PST">PST</asp:ListItem>
                                                    <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                    <asp:ListItem Value="CET">CET</asp:ListItem>
                                                    <asp:ListItem Value="CST">CST</asp:ListItem>
                                                    <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                    <asp:ListItem Value="EST">EST</asp:ListItem>
                                                    <asp:ListItem Value="JST">JST</asp:ListItem>
                                                    <asp:ListItem Value="BST">BST</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblTo1" runat="server" Text="To:" Visible="False"></asp:Label>
                                                <asp:DropDownList ID="DropDueTimeTo1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeTo1_SelectedIndexChanged" Visible="False" >
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                </asp:DropDownList><asp:DropDownList ID="DropDueMinTo1" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDueMinTo1_SelectedIndexChanged" Visible="False" >
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="DropDueTimeZoneTo1" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDueTimeZoneTo1_SelectedIndexChanged" Visible="False" >
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="IST">IST</asp:ListItem>
                                                    <asp:ListItem Value="PST">PST</asp:ListItem>
                                                    <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                    <asp:ListItem Value="CET">CET</asp:ListItem>
                                                    <asp:ListItem Value="CST">CST</asp:ListItem>
                                                    <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                    <asp:ListItem Value="EST">EST</asp:ListItem>
                                                    <asp:ListItem Value="JST">JST</asp:ListItem>
                                                    <asp:ListItem Value="BST">BST</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CheckBox ID="chkDueTime1" Text="Staggered Delivery" runat="server" AutoPostBack="True" OnCheckedChanged="chkDueTime1_CheckedChanged" Width="133px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;&nbsp;&nbsp;&nbsp;IST Time :&nbsp;&nbsp;
                                                <asp:Label ID="lblIndFrom1" runat="server" Text="(From)" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtIndFrom1" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblIndTo1" runat="server" Text="(To)" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtIndTo1" runat="server" Width="80px" Visible="False" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Task:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lboxtask1" runat="server" CssClass="TxtBox"  Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                Stage:
                                            </td>
                                            <td>
                                                <asp:ListBox ID="lboxStage" runat="server" SelectionMode="Multiple" Width="130px"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"> 
                                                Delivery Date & Time:
                                                <asp:TextBox ID="txtDelDate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                                                <img  alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=txtDelDate','calendar_window','width=250,height=250,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                    src="images/Calendar.jpg" style="cursor: pointer;" id="imgDelDate"  runat="server" />
                                                <asp:DropDownList ID="DropDelHrs" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDelHrs_SelectedIndexChanged">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DropDelMins" runat="server" Width="40px" AutoPostBack="True" OnSelectedIndexChanged="DropDelMins_SelectedIndexChanged">
                                                    <asp:ListItem>00</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                </asp:DropDownList>&nbsp;
                                                <asp:DropDownList ID="DropDelTimeZone" runat="server" Width="50px" AutoPostBack="True" OnSelectedIndexChanged="DropDelTimeZone_SelectedIndexChanged" >
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="IST">IST</asp:ListItem>
                                                    <asp:ListItem Value="PST">PST</asp:ListItem>
                                                    <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                                    <asp:ListItem Value="CET">CET</asp:ListItem>
                                                    <asp:ListItem Value="CST">CST</asp:ListItem>
                                                    <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                                    <asp:ListItem Value="EST">EST</asp:ListItem>
                                                    <asp:ListItem Value="JST">JST</asp:ListItem>
                                                    <asp:ListItem Value="BST">BST</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblDelTimeIST" runat="server" Text="Delivery Time IST"></asp:Label>
                                                <asp:TextBox ID="txtDelIST" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Status:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlStatus1" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                            <asp:Button ID="btnSave1" runat="server" Text="Save" OnClick = "Save1" CssClass="dpbutton"/>
                                            <asp:Button ID="btnCancel1" runat="server" Text="Close" OnClientClick = "return Hidepopup1()" CssClass="dpbutton"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="lblResult1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFake1" runat="server"></asp:LinkButton>
                                <asp:ModalPopupExtender ID="popup1" runat="server" DropShadow="false"
                                PopupControlID="pnlAddEdit1" TargetControlID = "lnkFake1"
                                BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                            </ContentTemplate> 
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID = "gvJobTrack" />
                                <asp:AsyncPostBackTrigger ControlID = "btnSave1" />
                                <asp:AsyncPostBackTrigger ControlID = "DelSave" />
                            </Triggers> 
                        </asp:UpdatePanel> 
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
