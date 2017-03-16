<%@ page language="C#" autoeventwireup="true" inherits="empcompoff, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Compoff</title>
    <link href="default.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="scripts/common.js"></script>
    <script language="javascript">
    function validHours(ctrl){
    if(ctrl!=null && ctrl.value!="" && ctrl.value<4){
        alert("Hours worked should be greater than or equal to four");
        ctrl.select();
    }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Add CompOff</div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" width="700">
            <tr>
                <td align="left" colspan="1">
                    Employee <font color="red">*</font></td>
                <td align="left" colspan="1">
                    :</td>
                <td align="left" colspan="10">
                    <asp:DropDownList ID="drpEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpEmployee_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                    Compoff Date <span style="color: #ff0000">*</span>:
                    <asp:TextBox ID="txtCODate" runat="server" Width="70px" onclick="javascript:this.value='';"
                        CssClass=""></asp:TextBox><img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtCODate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" align="absMiddle" />
                    &nbsp;
                    Hours worked <span style="color: #ff0000">* <span style="color: #000000">: </span>
                    <asp:TextBox ID="txtCOHours" runat="server" MaxLength="2" onkeypress="javascript:return OnlyAllowNumbers(this,event);" onblur="javascript:validHours(this);"
                        Width="41px"></asp:TextBox></span></td>
            </tr>
            <tr>
                <td align="left" colspan="1">
                    Reason</td>
                <td align="left" colspan="1">
                    :</td>
                <td align="left" colspan="10">
                    <span style="color: #000000"></span>
                    <asp:TextBox ID="txtCOComments" runat="server" onkeypress="javascript:return TextCounter(this,200);"
                        Width="240px" TextMode="MultiLine" Height="44px"></asp:TextBox>
                    <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="dpbutton" OnClick="btnClear_Click"
                        Text="Clear" /></td>
            </tr>
        </table>
        <br />
        <table align="center" cellpadding="2" cellspacing="5" class="bordertable" width="620">
            <tr>
                <td align="right" colspan="6">
                    <asp:ImageButton ID="imgbtnExport1" runat="server" ImageUrl="~/images/tools/doc_excel.png"
                        OnClick="imgbtnExport1_Click" ToolTip="Export to Excel" /></td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:GridView ID="gvCompoff" runat="server" AutoGenerateColumns="False" Width="800px" OnRowDataBound="gvCompoff_RowDataBound"
                        OnRowCommand="gvCompoff_RowCommand" CssClass="lightbackground">
                        <RowStyle HorizontalAlign="Left" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sno.">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblsno" runat="server" Text="<%# id=id+1 %>"></asp:Label>
                                    <asp:HiddenField ID="gvHfcompoffid" runat="server" Value='<%# Eval("compoff_id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblName" runat="server" Text='<%# Eval("employee_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblCOdate" runat="server" Text='<%# Eval("Compoff_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText=" Hours Worked">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblCOhours" runat="server" Text='<%# Eval("compoff_hours") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave taken on">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblCOLeavedate" runat="server" Text='<%# Eval("leave_taken_on") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="gvlblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img class="CursorAdd" title='<%# Eval("comments") %>' src="images/tools/comment.png"
                                        alt="info" />
                                    <asp:ImageButton ID="gvImgbtnTrash" runat="server" ImageUrl="~/images/tools/delete.png"
                                        CommandName="de1ete" ToolTip="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Left" CssClass="darkbackground" />
                        <AlternatingRowStyle CssClass="dullbackground" />
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="6">
                    <asp:ImageButton ID="imgbtnExport2" runat="server" ImageUrl="~/images/tools/doc_excel.png"
                        OnClick="imgbtnExport1_Click" ToolTip="Export to Excel" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
