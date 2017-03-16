<%@ page language="C#" autoeventwireup="true" enableeventvalidation="false" inherits="DespatchSummary, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link type="text/css" rel="stylesheet" href="default.css" />
    <script language="javascript" type="text/javascript" >
        function Validation()
        {
            if (divPageTitle.innerHTML == "Despatch Summary")
            {
                if(document.getElementById("fromdate").value=="")
                {
                    alert("Please Select From Date");
                    document.getElementById("fromdate").style.borderColor="Red";
                    return false;
                }
                else
                {
                    document.getElementById("fromdate").style.borderColor="Black";
                }
                if(document.getElementById("todate").value=="")
                {
                    alert("Please Select To Date");
                    document.getElementById("todate").style.borderColor="Red";
                    return false;
                }
                else
                {
                    document.getElementById("todate").style.borderColor="Black";
                }
            }
        }
    </script>
<script lang="C#" runat=server>
    protected void Page_Unload(Object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["invDS"] = null;
        }
    }
</script>    
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPageTitle" class="dptitle" runat="server" >Despatch Summary</div>
    <div id="divDespatchSubmit" runat="server" >
            <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>Customer</td><td>
                <asp:DropDownList ID="ddlcustomer" DataTextField="CUSTNAME" DataValueField="CUSTNO" runat="server" >
                </asp:DropDownList>
                </td>
                <td>Type</td><td>
                    <asp:DropDownList ID="ddljobtype" runat="server" OnSelectedIndexChanged="ddljobtype_SelectedIndexChanged" >
                        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="3"></asp:ListItem>
                    </asp:DropDownList> 
                </td>           </tr>
                </table>
                <div id="DivDate" runat="server">
                <table class="bordertable" align="center" cellpadding="2" cellspacing="2" style="width:450px;" >
                <tr><td>From</td><td>
                <asp:TextBox id="fromdate" runat="server" />
                <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td id="Td1" rowspan="2" colspan="2" valign="bottom" runat="server"   >
                    <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClientClick="return Validation()" OnClick="btnSubmit_Click" />
                </td></tr>
                <tr><td>To</td><td><asp:TextBox id="todate" runat="server" />
                <img style="cursor:pointer; border:none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=todate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td></tr>            
            </table>
            </div>
            <div id="DivLiveSub" runat="server" align="center">
                <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmitLive" OnClientClick="return Validation()" OnClick="btnSubmit_Click"  />
            </div>    
    </div>
    <div id="divLiveSubmit" runat="server" >
    &nbsp;
    </div>
    <div id="div1" runat="server" align="right" style="width:750px" >
    <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="ImageButton1" OnClick="exportExcel_Click" />             
    </div>
                    <asp:GridView HorizontalAlign="Center" GridLines="Horizontal" ID="DespatchSummaryList" 
        AllowSorting="True" width="90%" runat="server" AutoGenerateColumns="false"
		AllowPaging="False" CellPadding="3" 
		OnSorting="Grid_Sorting" OnRowDataBound="DespatchSum_OnRowDataBound" >
		<FooterStyle BackColor="Transparent"></FooterStyle>
		<AlternatingRowStyle  CssClass="lightbackground"></AlternatingRowStyle>
		<RowStyle CssClass="lightbackground"></RowStyle>
		<HeaderStyle ForeColor="white" CssClass="darkbackground"></HeaderStyle>
		<Columns>
		    <asp:BoundField DataField="CUSTNAME" HeaderText="Cust Name" SortExpression="CUSTNAME" />

            <asp:BoundField SortExpression="JOURCODE" HeaderText="Jourcode" DataField="JOURCODE" />
		    
		    <asp:BoundField  SortExpression="ANO" HeaderText="Ano" DataField="ANO" />
		    
		    <asp:BoundField SortExpression="CUSTCODE" HeaderText="Cust Code" DataField="CustCode" />
		    
		    <asp:BoundField SortExpression="STYPENAME" HeaderText="SType Name" DataField="STYPENAME"  />
		    
		    <asp:BoundField DataField="receive_date" SortExpression="receive_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Receive Date" />
		    <asp:BoundField DataField="due_date" SortExpression="due_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Due Date" />
		    <asp:BoundField DataField="halfdue_date" SortExpression="halfdue_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Half DueDate" />
		    <asp:BoundField DataField="cats_due_date" SortExpression="cats_due_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Cats DueDate" />
		    <asp:BoundField DataField="despatch_date" SortExpression="despatch_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" HeaderText="Despatch Date" />
		    <asp:TemplateField >
		        <ItemTemplate>
		           <asp:HiddenField ID="HF_OnHoldFlag" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"on_hold_flag") %>' />
		        </ItemTemplate>
		    </asp:TemplateField>
		    <asp:TemplateField >
		        <ItemTemplate>
		           <asp:HiddenField ID="HF_STNO" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"STNO") %>' />
		        </ItemTemplate>
		    </asp:TemplateField>
		    <asp:TemplateField >
		        <ItemTemplate>
		           <asp:HiddenField ID="HF_IINVOICED" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"IINVOICED") %>' />
		        </ItemTemplate>
		    </asp:TemplateField>
		     
		    <asp:TemplateField >
		        <ItemTemplate>
		           <asp:HiddenField ID="HF_CURRENT_DEP" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"CURRENT_DEPARTMENT") %>' />
		        </ItemTemplate>
		    </asp:TemplateField>
		    <asp:TemplateField >
		        <ItemTemplate>
		           <asp:HiddenField ID="HF_CURRENT_DEP_DUE" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"current_dept_due") %>' />
		        </ItemTemplate>
		    </asp:TemplateField>
		</Columns>
		</asp:GridView>    
 
    </form>
</body>
</html>
