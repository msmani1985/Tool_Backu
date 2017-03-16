<%@ page language="C#" autoeventwireup="true" inherits="LeaveApplication, App_Web_lh5hhzby" %>

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
      
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Leave Application
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
                    Leave Required 
                </td>
                <td>:</td>
                <td>
                    From
                </td>
                <td>
                    <asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td >
                    To
                </td>
                <td>
                    <asp:TextBox ID="ToTxt" runat="server" Width="113px"  ></asp:TextBox>&nbsp;<img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=ToTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus();"  src="images/Calendar.jpg" height="20px" border="0" />
                </td>
            </tr>
            <tr>
                <td>
                        Number of Days
                </td>
                <td>:</td>
                <td colspan="4">
                    <asp:Label ID="txtdays" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 74px">
                    Half Days(If any)<asp:Label ID="lbl" runat="server" Visible="false"></asp:Label>&nbsp;<br />
                    
                </td>
                <td style="height: 74px">:</td>
                <td colspan="2" style="height: 74px">
                    <table width="300px" class="borderblack" >
                    <tr>
                        <td >
                            <%--<asp:ListBox ID="DateListBox" runat="server" >
                            
                            </asp:ListBox>--%>
                            <asp:CheckBoxList ID="DateCBoxList" runat="server"    >
                            <asp:ListItem  Value="0">None</asp:ListItem>
                        </asp:CheckBoxList> 
                        </td>
                        <td>
                            <asp:CheckBoxList ID="FirstCBoxList" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="FirstCBoxList_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="SecondCBoxList" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="SecondCBoxList_SelectedIndexChanged">
                            </asp:CheckBoxList>
                        </td>
                        </tr>
                    </table>
                    </td>
                <td colspan="2" style="height: 74px">
                    <asp:Button ID="DateListBtn" runat="server" Text="DateList" OnClick="DateListBtn_Click" OnClientClick="return Validation();" CssClass="dpbutton" />    
            <asp:Label ID="CaptionLbl" ForeColor="red" runat="server" Text="(Select Date only if half Day Leave)"></asp:Label>
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
                <td>
                        Nature of Leave 
                </td>
                <td>:</td>
                <td colspan="4">
                    <asp:DropDownList ID="LeavetypeDDList" runat="server" DataTextField="Leave_Type_Name" DataValueField="Leave_Type_Id">
                    </asp:DropDownList>
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
            <tr>
            <td>
                <asp:TextBox ID="txtTo" Visible="false" runat="server"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td colspan="6">
            <asp:TextBox Visible="false" id="txtBody" cols="50" rows = "10"  style="border:solid 1px gray;Height:250px;Width:100%;" TextMode = "MultiLine" runat="server"></asp:TextBox>
            </td>
            </tr>
        </table>
        <asp:HiddenField ID="PLLeaveHF" runat="server" />
        <asp:HiddenField ID="SLLeaveHF" runat="server" />
        <asp:HiddenField ID="CLLeaveHF" runat="server" />
    </form>
</body>
</html>

