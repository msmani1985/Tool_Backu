<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttManualCorr.aspx.cs" Inherits="AttManualCorr" %>

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
    
    <script type="text/javascript">
        function fnShowCalendar(ClientID, width, height) {
            var popup = null;
            settings = 'width=' + width + ',height=' + height + ',location=no,directories=no,menubar=no,toolbar=no,status=no,scrollbars=no,resizable=no,dependent=no';
            popup = window.open('DatePicker.aspx?Ctl=' + ClientID, 'DatePicker', settings);
            popup.focus();
        }
        
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 282px;
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">Manual Corrections</div>
        <div class="content" id="divReports" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table width="800px" align="center">
            <tr>
                <td  class="auto-style1">
                    <asp:Label ID="EmployeeLbl" runat="server" Text="Emp ID." Width="86px" Height="16px"></asp:Label>
                    <asp:TextBox ID="EmpNoTxt"  Height="16px" Width="150px" runat="server"></asp:TextBox>
               </td>
                <td>Date: </td>
                <td>
                    <asp:TextBox ID="txtPdate" runat="server" ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPdate" 
                                            PopupButtonID="txtPdate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
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
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Punch Date:" Width="86px" Height="16px"></asp:Label>
                    
                    <asp:TextBox ID="txtAddPdate" runat="server" Height="16px" Width="150px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtAddPdate" 
                                            PopupButtonID="txtAddPdate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <td>Date & Time :</td>
                <td>
                    <asp:TextBox ID="txtAddPunch" runat="server"></asp:TextBox>
                    <img src="images/calender.png" />
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" CssClass="dpbutton"
                       Text="Add" OnClick="btnAdd_Click"   />
                     <script type="text/javascript">
                         $(document).ready(function () {
                             $("#<%=txtAddPunch.ClientID %>").dynDateTime({
                                showsTime: true,
                                ifFormat: "%Y-%m-%d %H:%M",
                                daFormat: "%l;%M %p, %e %m,  %Y",
                                align: "BR",
                                electric: false,
                                singleClick: false,
                                displayArea: ".siblings('.dtcDisplayArea')",
                                button: ".next()"
                            });
                        });
                    </script>
                </td>
            </tr>
        </table>
          <%--  </div>
        <div class="content" id="div1" runat="server" >--%>
        <table width="800px" align="center">
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvEmployeeDetails" runat="server" Width="80%" AutoGenerateColumns="false"
                        ShowFooter="true"  OnRowDeleting="gvEmployeeDetails_RowDeleting" 
                        OnRowUpdating="gvEmployeeDetails_RowUpdating" OnRowCancelingEdit="gvEmployeeDetails_RowCancelingEdit"
                        OnRowEditing="gvEmployeeDetails_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="UserID"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblEditUserID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:Label>
                                    <asp:Label ID="lblEditID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pdate") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditPdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pdate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditPdate" 
                                                          PopupButtonID="txtEditPdate" Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Punch"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPunch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Punch") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="txtEditPunch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Punch") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ToolTip="Edit" CommandName="Edit" ImageUrl="~/Images/icon-edit.png"
                                        Height="22px" Width="22px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" CommandName="Update" ImageUrl="~/Images/icon-update.png" 
                                        Height="22px" Width="22px"/>
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ToolTip="Cancel" ImageUrl="~/Images/icon-Cancel.png" 
                                        Height="22px" Width="22px"/>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
