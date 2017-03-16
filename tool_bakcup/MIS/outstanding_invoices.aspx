<%@ Page Language="C#" AutoEventWireup="true" CodeFile="outstanding_invoices.aspx.cs"
    Inherits="outstanding_invoices" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice Sales Analysis</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <style>.txtPad{padding-left:5px;}</style>
    <script language="javascript" type="text/javascript">
    function vbconfirm(str){execScript('n = msgbox("'+str+'","4132")', "vbscript");if(n == 6){doTimer();return true;};return false;}
    var cnt=0;
    var tt;
    var timer_is_on=0;
    var elemn;
    var h = 0;
    var max =20;
    var seed = 1;
    function timedCount(){
        //alert(document.getElementById('divfooter'));
        elemn = document.getElementById('divfooter');
	    h = elemn.style.height.replace('px','');	
	    if(timer_is_on && cnt<=max && h<=max){
	    //ctrl.value=cnt;	
	    if(h=='')h=0;
	    //alert(h);
	    elemn.style.height = parseInt(h)+seed+'px';	    
	    //alert(cnt);
	    cnt=cnt+seed;
	    tt=setTimeout("timedCount()",0);
	    }else {
	    timer_is_on=0;
	    cnt=0;
	    }
    }
    function doTimer()
    {    
    if (!timer_is_on)
      {
      timer_is_on=1;
      timedCount();
      }else timer_is_on=0;  
    }
    function popPayments(){
       if(document.form1.drpCustomer!=null && document.form1.drpCustomer.value !="10066" && document.form1.drpCustomer.value !="0"
                && document.form1.drpCustomer.value !="255610040"
                && document.form1.drpCustomer.value !="1003110004"
                && document.form1.drpCustomer.value !="1003110119"){
            var cname = document.form1.drpCustomer[document.form1.drpCustomer.selectedIndex].text;
            cname = cname.replace('&','%26');
            window.open("payment_on_account.aspx?cid="+document.form1.drpCustomer.value+"&cname="+cname,"Payments","width=700,height=600,top=20,left=200,status=yes, scrollbars=yes");
            }
       else alert("Invalid Customer.");
    }
    </script>
    <style>
    .footer{	    
	    background-color: Green;
	    height: 0.1px;
	    text-align:center;
	    font-size:12px;
	    font-weight:bold;
	    color:#FFFFFF;
	    font-family:Verdana;	
	    padding-top: 0px;	        
	    margin-right: 15px;	        
	    width: 200px;
	    position:fixed;	
	    right: 0px;	    
	    bottom: 0px;
	    line-height:20px;
        }
    </style>
    <!--[if lte IE 6]>
    <style type="text/css">
    /*body {height:100%; overflow-y:auto;}
    html {overflow-x:auto; overflow-y:hidden;}
    * html .footer {position:absolute;}*/
	.footer{
	height: 20px;
	width: 200px;
	position:inherit;
	display:none;
	right: 0px;
	bottom: 0px;
	float:right;
	}
    </style>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                <asp:Label ID="lblTitle" runat="server"></asp:Label></div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable">
            <tr>
                <td>
                    Customer</td>
                <td>
                    <asp:DropDownList ID="drpCustomer" runat="server" DataTextField="CUSTNAME" DataValueField="CUSTNO">
                        <asp:ListItem Text="--ALL--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis" Value="2556"></asp:ListItem>
                        <asp:ListItem Text="Taylor and Francis Scandivia" Value="10037"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Type</td>
                <td>
                    <asp:DropDownList ID="drpJobType" runat="server">
                        <asp:ListItem Text="--All--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="tdMonth1" runat="server">
                    Month</td>
                <td id="tdMonth2" runat="server">
                    <asp:DropDownList ID="drpMonths" runat="server">
                        <asp:ListItem Value="0">--All--</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="tdYear1" runat="server">
                    Year</td>
                <td id="tdYear2" runat="server">
                    <asp:DropDownList ID="drpYears" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                        Text="Submit" OnClientClick="javascript:doTimer();" /></td>
            </tr>
        </table>
        <br />
        <table id="tblReport" align="center" cellpadding="2" cellspacing="2" class="bordertable"
            style="width: 800px" runat="server">
            <tr>
                <td align="right" >
                    <img id="imgPayment" src="images/tools/payment.png" style="cursor:pointer;" width="32" align="absmiddle" runat="server" onclick="javascript:popPayments();" title="Add payment on account" />
                    <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/images/tools/j_save.png"
                        OnClick="imgbtnSave_Click" ToolTip="Save" ImageAlign="AbsMiddle" Width="35px" OnClientClick="javascript:doTimer();" /><asp:ImageButton
                            ID="cmd_Excel_Export" runat="server" ImageUrl="~/images/tools/j_excel.png" OnClick="cmd_Excel_Export_Click1"
                            ToolTip="Export Excel" ImageAlign="AbsMiddle" Width="32px"  />
                    <asp:GridView ID="gvOutstandingInv" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                        Width="100%" ShowFooter="True" OnRowDataBound="gvOutstandingInv_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Date of Invoice">
                                <ItemTemplate>
                                    <%# Eval("IDATE", "{0:MM/dd/yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvInvoiceNo" Text='<%# Eval("IINNO")%>' runat="server"></asp:Label>
                                    <asp:HiddenField ID="hfgvJobID" runat="server" Value='<%# Eval("INO")%>' />
                                    <asp:HiddenField ID="hfgvJobCategory" runat="server" Value='<%# Eval("CATEGORY")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <%# Eval("CNAME")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Title" FooterStyle-HorizontalAlign="right" FooterStyle-Height="20px">
                                <ItemTemplate>
                                    <%# Eval("JOBTITLE")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b>Total:&nbsp;&nbsp;</b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Total (€)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%# Eval("EUROVALUE_ACTUAL", "{0:0.00}")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b>€<%# tEuro %></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Total (&#163;)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%# Eval("POUNDVALUE", "{0:0.00}")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b>&#163;<%# tPound %></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Total ($)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%# Eval("USDVALUE", "{0:0.00}")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b>$<%# tDollar %></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Total (CAD$)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <%# Eval("CADVALUE", "{0:0.00}")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b>$<%# tCAD %></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%--<asp:HiddenField ID="hfgvPaymentDate" Value='<%# Eval("PAYMENT_DATE", "{0:MM/dd/yyyy}")%>' runat="server" />--%>
                                    <%# Eval("PAYMENT_DATE", "{0:MM/dd/yyyy}")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkgvPayment" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                No records found.</div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="darkbackground" />
                        <AlternatingRowStyle CssClass="dullbackground" />
                    </asp:GridView>
                    <img id="imgPayment1" src="images/tools/payment.png" style="cursor:pointer;" width="32" align="absmiddle" runat="server" onclick="javascript:popPayments();" title="Add payment on account" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/tools/j_save.png"
                        OnClick="imgbtnSave_Click" ToolTip="Save" ImageAlign="AbsMiddle" Width="35px" /><asp:ImageButton
                            ID="ImageButton2" runat="server" ImageUrl="~/images/tools/j_excel.png" OnClick="cmd_Excel_Export_Click1"
                            ToolTip="Export Excel" ImageAlign="AbsMiddle" Width="32px" />
                </td>
            </tr>
            <tr>
            <td>
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
            </td>
            </tr>
        </table>
        <br />
        <div id="divfooter" class="footer">processing request...</div>
    </form>
</body>
</html>
