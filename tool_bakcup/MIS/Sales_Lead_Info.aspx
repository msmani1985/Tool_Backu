<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_Lead_Info.aspx.cs"
    Inherits="Sales_Lead_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Sales Lead Management</div>
            <table cellspacing="1" cellpadding="1" border="0" width="100%" class="bordertable">
                <tr class="dpJobGreenHeader">
                    <td colspan="4" style="height: 25px">
                        Contact Information</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Publisher <font color="red">*</font></td>
                    <td style="width: 80%" colspan="3">
                        <asp:DropDownList ID="drpCompany" runat="server" Width="450px" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnUpdatePubInfo" runat="server" ImageUrl="~/images/tools/edit.png" ImageAlign="AbsMiddle" OnClick="imgbtnUpdatePubInfo_Click" ToolTip="Edit Publisher" />
                        <asp:ImageButton ID="imgbtnNewPubInfo" runat="server" ImageUrl="~/images/tools/new.png" ImageAlign="AbsMiddle" OnClick="imgbtnNewPubInfo_Click" ToolTip="New Publisher" /></td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Lead status</td>
                    <td style="width: 80%" colspan="3">
                        <asp:DropDownList ID="drpleadstatustype" runat="server" Width="450px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Contact Name <font color="red">*</font></td>
                    <td style="width: 80%" colspan="3">
                        <asp:DropDownList ID="drpcontname" runat="server" Width="450px" AutoPostBack="True" OnSelectedIndexChanged="drpcontname_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnNewContact" runat="server" ImageUrl="~/images/tools/new.png" ImageAlign="AbsMiddle" OnClick="imgbtnNewContact_Click" ToolTip="New Contact" /></td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Contact History By Dates</td>
                    <td style="width: 80%" colspan="3">
                        <asp:DropDownList ID="drpcontactdate" runat="server" Width="210px" AutoPostBack="True" OnSelectedIndexChanged="drpcontactdate_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;<asp:ImageButton ID="imgbtnNewCommn" runat="server" ImageUrl="~/images/tools/new.png" ImageAlign="AbsMiddle" OnClick="imgbtnNewCommn_Click" ToolTip="New" /></td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Communcation Date <span style="color: #ff0000">*</span></td>
                    <td colspan="3" style="width: 80%">
                        <asp:TextBox ID="txtcontactdate" runat="server" CssClass="TxtBox" Width="80px" BackColor="#FFFFC0"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtcontactdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_stdate" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Communication By <span style="color: #ff0000">*</span></td>
                    <td style="width: 30%" colspan="3">
                        <asp:TextBox ID="txtcommunicationby" runat="server" Width="205px" MaxLength="36" CssClass="TxtBox" BackColor="#FFFFC0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        Communication Type</td>
                    <td style="width: 30%" colspan="3">
                        <asp:DropDownList ID="drpcommunicationtype" runat="server" Width="210px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Summary of Communication<br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td valign="top" colspan="3">
                        <asp:TextBox ID="txtCommunication" Width="92%" Height="80px" TextMode="MultiLine"
                            runat="server" CssClass="TxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
                <tr class="dpJobGreenHeader">
                    <td colspan="4" style="border-top-color: Green; border-top-style: solid; border-top-width: 1px; height: 25px;">
                        Follow-up Criteria</td>
                </tr>
                <tr>
                    <td>
                        Follow-up
                        Date</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtFollowupDate" runat="server" CssClass="TxtBox" Width="80px"></asp:TextBox>
                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtFollowupDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                            src="images/Calendar.jpg" style="cursor: pointer;" id="img1" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Follow-up Type</td>
                    <td colspan="3">
                        <asp:DropDownList ID="drpfollowuptype" runat="server" Width="210px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        Comments<br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                    <td valign="top" colspan="3">
                        <asp:TextBox ID="txtComments" Width="92%" Height="80px" TextMode="MultiLine" runat="server"
                            CssClass="TxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" CssClass="dpbutton"
                            Width="110px" OnClick="btnSaveDetails_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
