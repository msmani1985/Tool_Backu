<%@ page language="C#" autoeventwireup="true" inherits="wiparticlelist, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">WIP Article List</div>
    <div >
        <table width="75%" align="center" class="bordertable"><tr><td>Category</td><td><asp:DropDownList ID="ddl_category" runat="server"><asp:ListItem Text="Journal" Value="1"></asp:ListItem><asp:ListItem Text="Book" Value="2"></asp:ListItem><asp:ListItem Text="Project" Value="3"></asp:ListItem></asp:DropDownList></td>
        <td>Customer</td><td><asp:DropDownList ID="ddl_customer" DataTextField="CUSTNAME" DataValueField="CUSTNO" runat="server"></asp:DropDownList></td><td><asp:Button ID="btn_submit" Text="Submit" CssClass="dpbutton" runat="server" OnClick="btn_submit_Click" /></td></tr>
        </table>
    </div>
    <br />
    <div style="width:95%;text-align:right;"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></div>
    <div align="center" style="text-align:left;" id="div_wiparticles" runat="server">
        <asp:GridView ID="gv_wiparticledetails" runat="server" AutoGenerateColumns="false" CssClass="lightbackground" 
        AlternatingRowStyle-CssClass="dullbackground" HeaderStyle-CssClass="darkbackground" >
        <Columns>
        <asp:BoundField DataField="confirstname" HeaderText="PE Name" />
        <asp:BoundField DataField="jourcode" HeaderText="JourCode" />
        <asp:BoundField DataField="amanuscriptid" HeaderText="Article" />
		<asp:BoundField DataField="Stage_Name" HeaderText="Stage Name" />
        <asp:BoundField DataField="INVNO" HeaderText="Invoice Number" />
        <asp:BoundField DataField="iscopyedit" HeaderText="COPYEDIT" />
        <asp:BoundField DataField="issam" HeaderText="SAM" /> 
        <asp:BoundField DataField="PAGEFORMAT" HeaderText="Format" />
        <asp:BoundField DataField="noofpages" HeaderText="Typeset Pages" />
        <asp:BoundField DataField="ce_pages" HeaderText="CopyEdit Pages" />
        <asp:BoundField DataField="sam_pages" HeaderText="SAM Pages" />
        <%--<asp:BoundField DataField="WPAGES_PRICEVAL" HeaderText="Pages Price code" />
        <asp:BoundField DataField="WCE_PRICEVAL" HeaderText="CE Price code" />
        <asp:BoundField DataField="WSAM_PRICEVAL" HeaderText="SAM Price code" />--%>
        <asp:BoundField DataField="JCNO_2010" HeaderText="Pages Price code" />
        <asp:BoundField DataField="CE_PRICECODE" HeaderText="CE Price code" />
        <asp:BoundField DataField="SAM_PRICECODE" HeaderText="SAM Price code" />
        <asp:BoundField DataField="WPAGES_VALUE" HeaderText="Pages Value" />
        <asp:BoundField DataField="WCE_VALUE" HeaderText="CE Value" />
        <asp:BoundField DataField="WSAM_VALUE" HeaderText="SAM Value" />
        </Columns>
        </asp:GridView>
    </div>
    <div id="div_error" runat="server" class="errorMsg"></div>
    </form>
</body>
</html>
