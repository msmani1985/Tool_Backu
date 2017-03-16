<%@ page language="C#" autoeventwireup="true" inherits="ibjobhistory, App_Web_opij0lkt" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IB Job History Page</title>
   <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="divTitle" runat="server">Job History Report</div>
    <div>
         <table  align="center" width="660px" style="border:solid 1px green;" >
            <tr bordercolor="green" >
              
                <td width="65px" align="right">Job Type : </td>
                <td width="125px"><asp:DropDownList ID="ddjobtype" runat="server">
                    <asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Article" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                    <%--<asp:ListItem Text="Chapter" Value="7"></asp:ListItem>--%>
                    <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                    <%--<asp:ListItem Text="Module" Value="8"></asp:ListItem>--%>
                </asp:DropDownList></td>
                <td align="right">Customer Name : </td><td><asp:DropDownList ID="ddcustname" DataValueField="CUSTNO" DataTextField="CUSTNAME" runat="server" Width="200px"></asp:DropDownList></td>
                <td rowspan="4" valign="middle"><asp:Button ID="Btnviewrpt" Width="100px" CssClass="dpbutton" Text="View Report" runat="server" OnClick="Btnviewrpt_Click"  /></td>
            </tr>
            
            <tr><td colspan="4" height="2px" style="background-image:url('Images/line.gif');background-repeat:repeat-x;"></td>
                
            </tr>
            <tr >
                <td width="60px" align="right" >From : </td>
                <td ><asp:TextBox ID="Txtfrmdate" runat="server" Text="" Width="100px"></asp:TextBox><img style="cursor:pointer; border: none" onclick="javascript:window.open('calendar.aspx?formname=Txtfrmdate','Calendar_window','width=130,height=190,left=350,top=300,toolbars=no,scrollbars=no,status=no,resizable=no')" alt="Calendar" src="Images/calendar.jpg" height="20px" border="0" /></td>
                <td align="right">To : </td><td><asp:TextBox ID="Txttodate" runat="server" Width="100px" Text=""></asp:TextBox><img style="cursor:pointer;border:none;" onclick="javascript:window.open('calendar.aspx?formname=Txttodate','Calendar_window','width=130,height=190,left=350,top=300,toolbars=no,scrollbars=no,status=no,resizable=no')" alt="Calendar" src="Images/calendar.jpg" height="20px" border="0" /></td>            
            </tr>
            <tr><td colspan="5"><asp:CheckBox ID=chkissue Text=" Not assign to Issue" runat="server" Checked="true" /></td></tr>
        </table>
    </div>
    <br />
    <div>
    <table width="750px"><tr>
    <td><div id="divrcount" runat="server"></div></td>
    <td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" /></td></tr>
           <tr><td colspan="2">
                 <asp:GridView width="750px" ID="JobworkinprogGV" runat="Server" AllowSorting="True" AutoGenerateColumns="false"
     OnSorting="Grid_Sorting"  >
        <HeaderStyle BackColor="lightgray" ForeColor="Black" Font-Bold="true"  />
            <Columns>
                
                <asp:BoundField DataField="BNUMBER1" SortExpression="BNUMBER1" HeaderText="Job Code" />
                <asp:BoundField DataField="BTITLE1" SortExpression="BTITLE1" HeaderText="Title" />
                <asp:BoundField DataField="CUSTNAME1" SortExpression="CUSTNAME1" HeaderText="Customer Name" />
                <asp:BoundField DataField="INDIA_RECD1" SortExpression="INDIA_RECD1" HeaderText="Received Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="BNOOFPAGES1" SortExpression="BNOOFPAGES1" HeaderText="Pages" />
                <asp:BoundField DataField="jcno_2009" SortExpression="jcno_2009" HeaderText="Price Code" />
              
                <%--
                <asp:BoundField DataField="STAGE1" SortExpression="STAGE1" HeaderText="Stage" ItemStyle-Wrap=false />
                <asp:BoundField DataField="BINDIADUE1" SortExpression="BINDIADUE1" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="INDIA_DISP1" SortExpression="INDIA_DISP1" HeaderText="Despatch Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="BINVOICEDATE" SortExpression="BINVOICEDATE" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="CATSDUEDATE" SortExpression="CATSDUEDATE" HeaderText="CATS Due Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                --%>
                
            </Columns>
            </asp:GridView>
           
            <asp:GridView ID="jobrecdesGV" runat="server" AllowSorting="true" AutoGenerateColumns="false" OnSorting="Grid_Sorting">
            <HeaderStyle BackColor="lightgray"  ForeColor="black" Font-Bold="true" />
                <Columns>
                    <asp:BoundField DataField="CUSTNAME1" SortExpression="CUSTNAME1" HeaderText="Customer Name" />
                    <asp:BoundField DataField="BNUMBER1" SortExpression="BNUMBER1" HeaderText="Job Code" />
                    <asp:BoundField DataField="BTITLE1" SortExpression="BTITLE1" HeaderText="Title" />
                    <asp:BoundField DataField="BNOOFPAGES1" SortExpression="BNOOFPAGES1" HeaderText="Pages" />
                    <asp:BoundField DataField="STAGE1" SortExpression="STAGE1" HeaderText="Stage" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="INDIA_RECD1" SortExpression="INDIA_RECD1" HeaderText="Received Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="BINDIADUE1" SortExpression="BINDIADUE1" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="INDIA_DISP1" SortExpression="INDIA_DISP1" HeaderText="Despatch Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="BINVOICEDATE" SortExpression="BINVOICEDATE" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="CATSDUEDATE" SortExpression="CATSDUEDATE" HeaderText="CATS Due Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="porticosubmission" SortExpression="porticosubmission" HeaderText="PORTICO" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="pmcsubmission" SortExpression="pmcsubmission" HeaderText="PMC" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="doajsubmission" SortExpression="doajsubmission" HeaderText="DOAJ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="doisubmission" SortExpression="doisubmission" HeaderText="DOI" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="psycinfo_submission" SortExpression="psycinfo_submission" HeaderText="PSYCINFO" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="crossref_submission" SortExpression="crossref_submission" HeaderText="CROSSREF" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="jgate_submission" SortExpression="jgate_submission" HeaderText="JGATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="isitr_submission" SortExpression="isitr_submission" HeaderText="ISI Thompson" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="iissueno" SortExpression="iissueno" HeaderText="Issue No." />
                </Columns>
            </asp:GridView>
           </td></tr>
    </table>
   </div>
    <div id="diverror" runat="server" class="errorMsg"></div>
    </form>
</body>
</html>
