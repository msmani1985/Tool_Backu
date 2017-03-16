<%@ page language="C#" autoeventwireup="true" inherits="coactionstagereport, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Co-Action Report Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="div_title" runat="server">
         Co-Action Publishing Report
    </div>
    <div>
        <table align="center" class="bordertable" width="650px">
            <tr><td align="right">Customer :</td>
                <td colspan="3"><b style="font-size:larger;">Co-Action Publishing</b></td>
            </tr>
            <tr>
                <td align="right">From Date :</td>
                <td><asp:TextBox ID="frmdate" runat="server" Text=""></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=frmdate','calendar_window','width=180,height=200,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td align="right">To Date :</td>
                <td><asp:TextBox ID="todate" runat="Server" Text="" ></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=todate','calendar_window','width=180,height=200,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                
            </tr>
            <tr>
                <td align="right">Journal/ManuscriptId :</td>
                <td colspan="3"><asp:TextBox ID="jourcode" runat="server" Text="" Width="354px" ToolTip="Give ManuscriptId or Journal"></asp:TextBox></td>
                <td rowspan="2"><asp:Button ID="showrpt" runat="server" Text="Submit" CssClass="dpbutton" OnClick="showrpt_Click" /></td>
            </tr>
            <tr><td colspan="4"><div id="div_submission" runat="server" >
    <table  width="425px">
    <tr><td align="right">Select submission Due: </td><td><asp:RadioButtonList ID="rb_submission" runat="server" RepeatDirection="horizontal"><asp:ListItem Text="Portico" Value="porticosubmission"></asp:ListItem><asp:ListItem Text="PMC" Value="pmcsubmission"></asp:ListItem><asp:ListItem Text="DOAJ" Value="doajsubmission"></asp:ListItem><asp:ListItem Text="DOI" Value="doisubmission"></asp:ListItem></asp:RadioButtonList></td></tr>
    </table>
    </div></td></tr>
        </table>
    </div>
    
    <br />
    <div id="divgrid" runat="server" align="center">
    <table width="760px">
        <tr>
            <td align="right">
                <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" />
            </td>
        </tr>
        <tr>    
            <td>
                <asp:GridView ID="grdcoactionsub" runat="server" AutoGenerateColumns="false" CssClass="lightbackground"
                HeaderStyle-CssClass="darkbackground" AlternatingRowStyle-CssClass="dullbackground" 
                BorderColor="Black" BorderWidth="1px" >
                <Columns>
                    <asp:BoundField DataField="jourcode" SortExpression="jourcode" HeaderText="Journal" />
                    <asp:BoundField DataField="aarticlecode" SortExpression="aarticlecode" HeaderText="ManuscriptId" />
                    <asp:BoundField DataField="adespatchdates200" SortExpression="adespatchdates200" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" 
                    ItemStyle-Wrap="false" HeaderText="S200 Despatch"   />
                    <%--<asp:BoundField DataField="aduedate" SortExpression="aduedate" HeaderText="Due Date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />--%>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="stypename" SortExpression="stypename" HeaderText="Stage" />
                      <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="Portico Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "PORTICODUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION1")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="porticosubmission" SortExpression="porticosubmission" HeaderText="Portico" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                    <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="PMC Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "PMCDUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="pmcsubmission" SortExpression="pmcsubmission" HeaderText="PMC" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                    <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="DOAJ Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "DOAJDUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION1")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="doajsubmission" SortExpression="doajsubmission" HeaderText="DOAJ" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false"/>
                    <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="DOI Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "DOIDUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION1")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="doisubmission" SortExpression="doisubmission" HeaderText="DOI" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false"/>
                    <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="PSYCINFO Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "PSYCINFODUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="psycinfo_submission" SortExpression="psycinfo_submission" HeaderText="PSYCINFO" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false"/>
                     <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="ISITR Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "ISITRDUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="isitr_submission" SortExpression="isitr_submission" HeaderText="ISITR" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false"/>
                    <asp:TemplateField ItemStyle-ForeColor="red" HeaderText="CROSSREF Due" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "CROSSREFDUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION1")) : ""%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="crossref_submission" SortExpression="crossref_submission" HeaderText="CrossRef" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false"/>
                    <asp:TemplateField HeaderText="JGate Due" ItemStyle-ForeColor="red" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "JGATEDUE_SUBMISSION")) == "Y") ? Convert.ToString(DataBinder.Eval(Container.DataItem, "DUESUBMISSION1")) : "" %>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Wrap="false" DataField="JGATE_SUBMISSION" SortExpression="JGATE_SUBMISSION" HeaderText="JGate" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
        
    </div>
    
    
    <div id="divError" runat="server" class="errorMsg">
    </div>
    </form>
</body>
</html>
