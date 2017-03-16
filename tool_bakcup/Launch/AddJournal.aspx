<%@ page language="C#" autoeventwireup="true" inherits="AddJournal, App_Web_u_6pxkzu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Journal Page</title>
    <link href="default.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Javascript/Script.min.js"></script>
        <style type="text/css">
        .combowidth
        {
        width:150px;
        }
        .headerbackcolor
        {
            background-color:#EAFEE2; /*WhiteSmoke;*/
            border-bottom:solid 2px Gray;
            color:Green;
            font-size:10pt;font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="divtitle" class="dptitle">Journal Details</div>
    <div id="divjournal" runat="server">
      <table width="100%" class="bordertable" >
            <tr>
                <td class="headerbackcolor">
                    Add Journal &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 184px">
                <div id="div_journal1">
                    <table width="100%">
                       <tr><td>Journal Code</td>
                           <td colspan="5">
                               <asp:TextBox ID="txtJourCode" runat="server" Text="" Width="117px"></asp:TextBox></td>
                       </tr>
                       <tr><td>Journal Title</td><td colspan="5"><asp:TextBox ID="journal_name" Text="" runat="Server" Width="510px"></asp:TextBox></td></tr>
                       <tr>
                           <td>
                               Is Copy Edit</td>
                           <td><asp:DropDownList ID="ddCE" CssClass="combowidth" runat="server" AutoPostBack="True" Width="88px">
                           <asp:ListItem>-- Select --</asp:ListItem>
                           <asp:ListItem>Yes</asp:ListItem>
                           <asp:ListItem Selected="True">No</asp:ListItem>
                           </asp:DropDownList></td>
                           <td>
                               Is Sensitive</td>
                           <td colspan="3"><asp:DropDownList CssClass="combowidth" ID="ddlSensitive" runat="server" Width="87px">
                            <asp:ListItem>-- Select --</asp:ListItem>
                           <asp:ListItem>Yes</asp:ListItem>
                           <asp:ListItem Selected="True">No</asp:ListItem>
                           </asp:DropDownList></td>  
                       </tr>
                       <tr>
                           <td>
                               Is SAM</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="ddlSAM" runat="server" Width="86px">
                            <asp:ListItem>-- Select --</asp:ListItem>
                           <asp:ListItem>Yes</asp:ListItem>
                           <asp:ListItem Selected="True">No</asp:ListItem>
                           </asp:DropDownList></td>
                           <%--<td>Service Type</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="service_type_id" runat="server" DataTextField="service_type_name" DataValueField="service_type_id"></asp:DropDownList></td>--%>
                           <td>
                               FPM Journal</td>
                           <td colspan="3"><asp:DropDownList CssClass="combowidth" ID="ddlFPM" runat="server" Width="86px">
                            <asp:ListItem>-- Select --</asp:ListItem>
                           <asp:ListItem>Yes</asp:ListItem>
                           <asp:ListItem Selected="True">No</asp:ListItem>
                           </asp:DropDownList></td>  
                           
                       </tr>
                        <tr>
                        <td >Trim Size</td>
                           <td >
                               <asp:TextBox ID="txtTrim" runat="server"></asp:TextBox></td> 
                        <td>Production Editor</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPE_name" runat="server" Width="308px"></asp:TextBox></td>
                    </tr>
                    <tr>
                       <td style="height: 21px">
                           DOI information</td>
                       <td style="height: 21px">
                           <asp:TextBox ID="txtDOI" runat="server"></asp:TextBox></td> 
                       <td style="height: 21px">
                           AQ Cover Sheet No</td>
                       <td colspan="3" style="height: 21px">
                           <asp:TextBox ID="txtAQ_Cover" runat="server" Width="308px"></asp:TextBox></td> 
                    </tr>
                    <tr><td align="center" colspan="12"><asp:Button CssClass="dpbutton" ID="btnAdd" Text="Add" runat="server" OnClick="btnAdd_Click"   /></td></tr>
                    </table>
                    </div>
                </td>
            </tr>  
            </table>
                                            </div>
    </div>
    </form>
</body>
</html>
