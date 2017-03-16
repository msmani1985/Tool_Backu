<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nature.aspx.cs" Inherits="Nature" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" >
        function getScrollBottom(p_oElem) {
            return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
        }
        function changeGreenImage(id) {
            if (document.getElementById('hfEmID').value != '1')//If software team then it has not change
            {
                //document.getElementById(id).src='images/te-proofgreen.gif';
                id.src = 'images/te-proofgreen.gif';

            }
        }
        function PopupWidow(url) {
            try {
                window.open(url);
                return true;
            }
            catch (err) {
                alert(err.description);
                return false;
            }
        }
        function testfunction() {
            alert("Test");
            return true;
        }
        function disablefn(obj) {
            if (confirm("Are you sure you want to save this invoice?")) {
                obj.style.visibility = "hidden";
                obj.style.cursor = 'pointer';
                document.body.style.cursor = 'wait';
            }
            else
                return false;
            return true;
        }
        function cursor_clear() { document.body.style.cursor = 'auto'; }

    </script>
    <style type="text/css">

TABLE
{
	z-index: 1000;
	font-size: 8pt
}
tr.GVFixedHeader
{
	font-weight:bold; color:White; background-color:Green;
}
                 
tr.GVFixedHeader a:link
{
font-weight: bold;
color:white;
text-decoration:none;
}
                 
A:link
{
	text-decoration: none;
}
TD
{
	vertical-align: middle;
	z-index: 1000;
	background-color: inherit;
    margin-left: 40px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
        <br />
     
    <asp:GridView HorizontalAlign="left" GridLines="Horizontal" ID="adgdispatchedlist" 
    AllowSorting="True" width="97%" runat="server" AutoGenerateColumns="False"
		AllowPaging="False" CellPadding="1"   
		OnSorting="Grid_Sorting">
        <HeaderStyle CssClass="GVFixedHeader" />
        <FooterStyle CssClass="GVFixedFooter" />         
		<Columns>
			<asp:BoundField DataField="custname" SortExpression="custname" ItemStyle-Width="170" HeaderStyle-HorizontalAlign="left" HeaderText="Customer" />
			<asp:BoundField DataField="JOURCODE" ItemStyle-Width="60" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderText="Journal" />
            <asp:BoundField DataField="AARTICLECODE" ItemStyle-Width="60" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderText="Article" />
            <asp:BoundField DataField="AMANUSCRIPTID" ItemStyle-Width="60" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderText="Amanuscriptid" />
            <asp:BoundField DataField="RECEIVE_DATE" ItemStyle-Width="60" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderText="Receive_Date" />
            <asp:BoundField DataField="DESPATCH_DATE" ItemStyle-Width="60" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderText="Despatch_Date" />
			<asp:TemplateField HeaderText="Mail">
				<ItemTemplate>
					<a target="_blank" id="firstanchor"  title="Click to Email Author" href="NatureEmail.aspx?&filename=<%# DataBinder.Eval(Container.DataItem,"pdf_name")%>&JOBID=<%# DataBinder.Eval(Container.DataItem,"job_id")%>">				
					    <img id="Img1"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email to Financial Contact" title="Email to Financial Contact" />
					</a>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
     
    </div>
    </form>
</body>
</html>
