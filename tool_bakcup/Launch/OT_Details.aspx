<%@ page language="C#" autoeventwireup="true" inherits="OT_Details, App_Web_irc2bci7" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="dptitle">
        OT Details
    </div>

    <br />
    <div id="LeaveDetaildiv" runat="server" align="center"></div>
    <br />
        <table align="center" class="bordertable" width="700px" cellpadding="2" cellspacing="5">
            <tr>
                <td>
                    Emp Code 
                </td>
                <td>:</td>
                <td colspan="2">
                    <asp:Label ID="EmpcodeLbl" runat="server" Text=""></asp:Label>
                </td>
                <td  >
                    Date 
                </td>
                <td >
                    <asp:Label ID="DateLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Name of Applicant 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="NameLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Department 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="DepartmentLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Designation 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="DesignationLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Date 
                </td>
                <td>:</td>
               
                <td>
                    <asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar2.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />&nbsp;
                    <asp:Button ID="btnChk" runat="server" CssClass="dpbutton"    Text="Work Hours" OnClick="btnChk_Click" /></td>
               
            </tr>
            <tr>
            <td>
                OT In
            </td>
            <td>
            :</td>
            <td >
                <asp:TextBox ID="txtTimeIn" runat="server" AutoPostBack="True" OnTextChanged="txtTimeIn_TextChanged">00:00</asp:TextBox>
                OT
                Out :
                <asp:TextBox ID="txtTimeOut" runat="server" AutoPostBack="True" OnTextChanged="txtTimeOut_TextChanged">00:00</asp:TextBox><asp:Label ID="Label1"
                    runat="server" Text="Ex.(24:00)" style="color: red"></asp:Label>
            </td>
            </tr>
              <tr>
                <td   >
                    OT Hrs 
                </td>
                <td>:</td>
               
                <td>
                    <asp:TextBox ID="txtOT" runat="server" Enabled="False"  ></asp:TextBox>
                    Break :<asp:DropDownList ID="dropBreak" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropBreak_SelectedIndexChanged">
                        <asp:ListItem Value="1">With Break</asp:ListItem>
                        <asp:ListItem Value="2">WithOut Break</asp:ListItem>
                    </asp:DropDownList></td>
               
            </tr>
                  <tr>
                <td   >
                    Remarks 
                </td>
                <td>:</td>
               
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server"  ></asp:TextBox>
                </td>
               
            </tr>
             <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="SaveBtn" runat="server" Text="Save" CssClass="dpbutton"  OnClick="SaveBtn_Click" />
                </td>
            </tr>
            <tr>
            <td></td>
            </tr>
      
            </table>
            </div>
     
            <div>
            <table align="center" class="bordertable" width="700px" cellpadding="2" cellspacing="5">
                  <tr>
            <td colspan="7" align="center">
             <asp:DataGrid ID="OTDetailsGrid" runat="server" AutoGenerateColumns="False" 
                    OnItemCommand="OTDetailsGrid_ItemCommand"  OnItemDataBound="OTDetailsGrid_ItemDataBound"
                    CellPadding="3"
                     AlternatingItemStyle-CssClass="dullbackground"
                        HeaderStyle-CssClass="darkbackground"  
                        CssClass="lightbackground" 
                        BorderColor="Black" BorderWidth="1px" ItemStyle-Wrap="false" >
                        <Columns>
                            <asp:BoundColumn DataField="EMPNAME"  HeaderText="Name" SortExpression="EMPNAME" />
                            <asp:BoundColumn DataField="Date" HeaderText="Date" SortExpression="Date" />
                            <asp:BoundColumn DataField="TimeIn" HeaderText="TimeIn" SortExpression="TimeIn" />
                            <asp:BoundColumn DataField="TimeOut" HeaderText="TimeOut" SortExpression="TimeOut" />
                            <asp:BoundColumn DataField="OT" HeaderText="OT(Hrs)" SortExpression="OT(Hrs)" />
                             <asp:BoundColumn DataField="OTBreak" HeaderText="Break" SortExpression="OTBreak" />
                           <asp:BoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                            <asp:BoundColumn DataField="STATUS" HeaderText="Level 1" SortExpression="STATUS_L1" />
                            <asp:BoundColumn DataField="STATUS_L2" HeaderText="Level 2" SortExpression="STATUS_L2" />
                            <asp:TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton AlternateText="Remove" ImageUrl="~/images/tools/no.png" id="btnDelete"  AccessKey="D" style="cursor :pointer"
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EMP_OT_ID")%>' CommandName="Delete1"  runat="server" ToolTip="Remove" />
                            </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <AlternatingItemStyle CssClass="dullbackground" />
                        <ItemStyle Wrap="False" />
                        <HeaderStyle CssClass="darkbackground" />
                    </asp:DataGrid>
            </td>
            </tr>
            </table>
            </div>
            
    
    </form>
</body>
</html>
