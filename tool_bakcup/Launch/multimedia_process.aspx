<%@ page language="C#" autoeventwireup="true" inherits="multimedia_process, App_Web_lruasnqi" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Multimedia Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="div_title" class="dptitle" runat="server">
        Multimedia Process
    </div>
    <div id="div_media" runat="server">
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="font-weight: bold;
                background-color: honeydew">
            <tr>
                <td>&nbsp;TV Station:&nbsp;<asp:DropDownList ID="drpJournal" runat="server" 
                OnSelectedIndexChanged="drpJournal_SelectedIndexChanged" >
                    </asp:DropDownList><span style="color: #ff0000"></span></td>
                               <td> <div id="divStage" runat="server" >
                        &nbsp; Status:&nbsp;<asp:DropDownList ID="drpJobStatus" runat="server">
                        </asp:DropDownList>
                    </div>
            </td><td align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" 
                        Text="Submit" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div>
      <br />
    <div id="divReport" align="center" runat="server">
        <table class="bordertable" style="width: 95%">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="imgbtnExport" runat="server" ImageUrl="~/images/tools/j_excel.png"
                         ToolTip="Export to Excel" OnClick="imgbtnExport_Click" /></td>
            </tr>
            <tr><td>
                <asp:GridView ID="gv_multimedia" runat="server"
                 AllowSorting="True" AutoGenerateColumns="False" CssClass="lightbackground" Width="100%"
                  OnSorting="gv_multimedia_sorting">
                    <AlternatingRowStyle CssClass="dullbackground" />
                    <HeaderStyle ForeColor="white" />
                    <Columns>
                      <asp:TemplateField HeaderText="S.no">
                          <ItemTemplate>
                            <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>
                       <asp:BoundField DataField="TVSTATION" HeaderText="TV Station" SortExpression="TVSTATION" />
                       <asp:BoundField DataField="author" HeaderText="Speaker" SortExpression="author"/>
                       <asp:BoundField DataField="author_email" HeaderText="Email" SortExpression="author_email"/>
                       <asp:BoundField DataField="sanlucas_phoneno" HeaderText="Tel No." SortExpression="sanlucas_phoneno" />
                       <asp:BoundField DataField="sanlucas_faxno" HeaderText="Fax No." SortExpression="sanlucas_faxno" />
                       <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title"/>
                       <asp:BoundField DataField="sanlucas_interviewdate" HeaderText="Interview Date" SortExpression="sanlucas_interviewdate" DataFormatString="{0:MM/dd/yyyy}" />
                       <asp:BoundField DataField="sanlucas_interviewtime" HeaderText="Interview Time" SortExpression="sanlucas_interviewtime" />
                       <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status"/>
                       <asp:BoundField DataField="comments" HeaderText="Comments" SortExpression="comments" />
                    </Columns>
                     <HeaderStyle CssClass="darkbackground" />
                     <EmptyDataTemplate><div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div></EmptyDataTemplate>
                </asp:GridView>
            </td></tr>    
        </table>
    </div>
    <div id="div_error" runat="server" class="error"></div>
    </form>
</body>
</html>
