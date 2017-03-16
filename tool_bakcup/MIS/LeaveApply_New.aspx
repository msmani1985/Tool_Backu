<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveApply_New.aspx.cs" Inherits="LeaveApply_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="default.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
     <script language="javascript" type="text/javascript">
       function Validation()
       {
            if(document.getElementById("FromTxt").value=="")
            {
                alert("Please Select From Date");
                document.getElementById("FromTxt").style.borderColor="Red";
                return false;
            }
            else
            {
                document.getElementById("FromTxt").style.borderColor="Black";
            }
            if(document.getElementById("ToTxt").value=="")
            {
                alert("Please Select To Date");
                document.getElementById("ToTxt").style.borderColor="Red";
                return false;
            }
            else
            {
                document.getElementById("ToTxt").style.borderColor="Black";
            }
            return true;
       }
      
       function preventInput(evnt) {
           //Checked In IE9,Chrome,FireFox
           if (evnt.which != 9) evnt.preventDefault();
       }
    </script>
    <style type="text/css">
.FixedHeader {
            font-weight:bold; color:White; background-color:Green;
        }   
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="dptitle">
        Leave Application
    </div>
    <br />
    <div id="LeaveDetaildiv" runat="server" align="center"></div>
    <br />
        <table align="center" class="bordertable" cellpadding="2" cellspacing="5" style="width: 812px">
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
                    Leave Required 
                </td>
                <td>:</td>
                <td>
                    From
                </td>
                <td>
                    <asp:TextBox ID="FromTxt" runat="server"  onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar5.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td >
                    To
                </td>
                <td>
                    <asp:TextBox ID="ToTxt" runat="server" Width="113px"  onkeydown="javascript:preventInput(event);" onpaste="return false;" ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar5.aspx?formname=ToTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus();"  src="images/Calendar.jpg" height="20px" border="0" />
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="DateListBtn_Click" OnClientClick="return Validation();" CssClass="dpbutton" /></td>
            </tr>
            
            <tr>
                <td style="height: 74px">
                    Leave Details&nbsp;<br />
                    
                </td>
                <td style="height: 74px">:</td>
                <td colspan="4" style="height: 74px">
                <asp:GridView ID="grvLeave" runat="server"  autogeneratecolumns="false"  OnRowDataBound="grvLeave_RowDataBound" 
                  EmptyDataText="No data available."  HeaderStyle-CssClass="FixedHeader" Font-Size="9pt" Height="90px" Width="425px" >
                  <AlternatingRowStyle Width="100px" BackColor="#F2F2F2" />
                  <Columns>
                     <asp:TemplateField HeaderText="Date" >
                         <ItemTemplate>
                             <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Date") %>'></asp:Label>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="First Half" >
                         <ItemTemplate>
                              <asp:CheckBox ID="ckFirstHalf" runat="server" AutoPostBack="true" OnCheckedChanged="ckFirstHalf_CheckedChanged"></asp:CheckBox>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Leave Type" >
                         <ItemTemplate>
                              <asp:DropDownList ID="LeavetypeFirst" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LeavetypeFirst_SelectedIndexChanged" >
                             </asp:DropDownList>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Second Half" >
                         <ItemTemplate>
                             <asp:CheckBox ID="ckSecondHalf" runat="server" AutoPostBack="true" OnCheckedChanged="ckSecondHalf_CheckedChanged"> </asp:CheckBox>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Leave Type" >
                         <ItemTemplate>
                             <asp:DropDownList ID="LeavetypeSecond" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="LeavetypeSecond_SelectedIndexChanged">
                             </asp:DropDownList>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:Label ID="lblDays" runat="server" />
                             <asp:DropDownList ID="PermissionMins" runat="server" Visible="false" >
                                 <asp:ListItem Value="60">60</asp:ListItem>
                                 <asp:ListItem Value="30">30</asp:ListItem>
                             </asp:DropDownList>
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" Width="50px" />
                     </asp:TemplateField>
                  </Columns>
                    <HeaderStyle CssClass="FixedHeader" />
                </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    Reason for Applying Leave
                </td>
                <td>:</td>
                <td colspan="4">
                    <asp:TextBox ID="LeaveReasonTxt" runat="server" Height="34px" Width="206px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="SaveBtn" runat="server" Text="Save" CssClass="dpbutton"  OnClick="SaveBtn_Click" />
                </td>
            </tr>
              <tr>
                <td colspan="6" align="center">
                    <asp:Label ID="lblStatus" runat="server" Font-Size="Small" ForeColor="Red" Visible="False"  />
                </td>
            </tr>
            </table>
        <asp:HiddenField ID="PLLeaveHF" runat="server" />
        <asp:HiddenField ID="SLLeaveHF" runat="server" />
        <asp:HiddenField ID="CLLeaveHF" runat="server" />
    </div>
    </form>
</body>
</html>
