<%@ page language="C#" autoeventwireup="true" inherits="sales_publisher_summary, App_Web_opij0lkt" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Publisher Summary</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript">
    function popWeb(url){
        url = trim(url);
        url = url.toLowerCase();
        if(url!=''){
            if(url.indexOf("http://")==-1 && url.indexOf("https://")==-1){
               url = "http://"+url           
            }
           window.open(url,'_blank') ;
        } 
    }
    function trim(s) {
    return s.replace( /^\s*/, "" ).replace( /\s*$/, "" );}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                <asp:Label ID="lblTitle" runat="server">Publisher Summary</asp:Label></div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 70%">
            <tr>
                <td align="center" colspan="10" rowspan="1">
                    Country: 
                    <asp:DropDownList ID="drpCountry" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                        Text="Submit" />
                    <asp:Button ID="cmd_Excel_Export" runat="server" CssClass="dpbutton" OnClick="cmd_Excel_Export_Click"
                        Text="Export to Excel" Width="142px" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvPubSummary" runat="server" CssClass="lightbackground" Width="100%"
            AutoGenerateColumns="false" OnRowDataBound="gvPubSummary_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Job No.">
                    <ItemTemplate>
                        <%# id++%>
                        <asp:HiddenField ID="hfgvpubDesc" Value='<%# Eval("sldescription")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Publisher Name">
                    <ItemTemplate>
                        <a class="link1" href="#pop" onclick="javascript:window.location='sales_publisher_info.aspx?slno=<%# Eval("salesleadno")%>'"><%# Eval("slcompany")%></a>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <%# Eval("jtname")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <%# Eval("slcatname")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <%# Eval("slfulladdress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City">
                    <ItemTemplate>
                        <%# Eval("slcity")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <%# Eval("slstate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Code">
                    <ItemTemplate>
                        <%# Eval("slpocode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country">
                    <ItemTemplate>
                        <%# Eval("ctyname")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Person">
                    <ItemTemplate>
                        <%# Eval("ci_name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact Title">
                    <ItemTemplate>
                        <%# Eval("ci_title")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <%# Eval("ci_phone")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fax">
                    <ItemTemplate>
                        <%# Eval("ci_fax")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="E-Mail">
                    <ItemTemplate>
                        <%# Eval("ci_email")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Web">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfgvpubWeb" Value='<%# Eval("ci_web")%>' runat="server" />
                        <a class="link1" href="#pop" onclick="javascript:popWeb('<%# Eval("ci_web")%>');"><%# Eval("ci_web")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                    No records found.</div>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="darkbackground" />
            <AlternatingRowStyle CssClass="dullbackground" />
        </asp:GridView>
    </form>
</body>
</html>
