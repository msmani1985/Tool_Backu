<%@ Page Language="C#" AutoEventWireup="true" CodeFile="15-07-2014_QMS_StyleSheet.aspx.cs" Inherits="QMS_StyleSheet" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="default.css" rel="stylesheet" type="text/css" />   
        <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
            <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grdStyleSheet.ClientID %>').Scrollable({
        ScrollHeight: 550
    });
});
</script>
     <script language="javascript">    
    var cnt=0;
    var tt;
    var timer_is_on=0;
    var elemn;
    var h = 0;
    var max =20;
    function timedCount(){
        //alert(document.getElementById('divfooter'));
        elemn = document.getElementById('divfooter');
	    h = elemn.style.height.replace('px','');	
	    if(timer_is_on && cnt<=max && h<=max){
	    //ctrl.value=cnt;	
	    if(h=='')h=0;
	    //alert(h);
	    elemn.style.height = parseInt(h)+5+'px';	    
	    //alert(cnt);
	    cnt=cnt+5;
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
    
    
    
    var gvelem;
        var gvcolor;
        function setColor(element, val) {
            //alert(gvelem);
            if (gvelem != null) {//alert(gvelem.style.backgroundColor);
                gvelem.style.backgroundColor = gvcolor;
            }
            gvelem = element;
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            document.form1.hfA_Journal.value = val;
            if (document.getElementById("lblJournalSummary"))
                document.getElementById("lblJournalSummary").innerText = "Journal : " + val ;
            else if (document.all.lblJournalSummary)
                document.all.lblJournalSummary.innerText = "Journal : " + val ;
            else if (document.form1.lblArticleSummary)
                document.form1.lblJournalSummary.innerText = "Journal : " + val ;
            doTimer();
        }
    
       function setMouseOverColor(element) {
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            element.style.cursor = 'hand';
            element.style.textDecoration = 'underline';
        }
        function setMouseOutColor(element) {
            element.style.backgroundColor = gvcolor;
            element.style.textDecoration = 'none';
        }
    </script>

        
        <style>
           
           .GVFixedHeader
        {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }
    </style>
      <style type="text/css">
        .footer{	    
	    background-color: green;
	    height: 0.1px;
	    text-align: center;
	    font-size:11px;
	    font-weight:bold;
	    color:#FFFFFF;
	    font-family:Verdana;	
	    padding-top: 0px;	    
	    width: 120px;
	    position:fixed;	    
	    right: 3px;
	    bottom: 0px;
	    cursor:pointer;
	    line-height:20px;
        }
    </style>
     
</head>
<body>
    <form id="form1" runat="server">
        <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
        </div>
        
       <div>
            <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
       <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
       <tr>
         <td>
          <ol id="toc">
            <li id="miGeneral" runat="server"><asp:LinkButton ID="lnkGeneral" runat="server"  TabIndex="5" OnClick="lnkGeneral_Click">General</asp:LinkButton></li>
            <li id="miJournal" runat="server">
                                <asp:LinkButton ID="lnkJournaldetails" runat="server"  TabIndex="5" OnClick="lnkJournaldetails_Click">Journal Details</asp:LinkButton></li>
                                </ol>
                                    <asp:HiddenField ID="hfgvJournal" runat="server" />      
                                </td>
       </tr>
         <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblJournalSummary" runat="server" Text="Journal Summary"></asp:Label></td>
                                  
                                </tr>
       </table>
         <div class="content" id="tabGeneral" runat="server">
        <asp:GridView ID="grdStyleSheet" runat="server"  AutoGenerateColumns="false"
           EmptyDataText="No data available." Font-Size="8pt" 
         
          PageSize="7" OnRowDataBound="grdStyleSheet_RowDataBound" OnRowCommand="grdStyleSheet_RowCommand" DataKeyNames="Journal Acronym" OnSorting="grdStyleSheet_Sorting"  AllowSorting="True" OnRowEditing="grdStyleSheet_RowEditing" OnRowCancelingEdit="grdStyleSheet_RowCancelingEdit">
            <HeaderStyle CssClass="GVFixedHeader"/>
            <AlternatingRowStyle BackColor="#F2F2F2" />
            <Columns>
            <asp:TemplateField ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>Serial No.</HeaderTemplate>
                <ItemTemplate>
                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                <asp:HiddenField ID="hfgvJournal" runat="server" Value='<%# Eval("Journal Acronym") %>' />
                </ItemTemplate>
            </asp:TemplateField>

                <asp:BoundField DataField="Journal Acronym"  SortExpression="Journal Acronym" HeaderText="Journal Acronym" ItemStyle-Width="11px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Journal Title" HeaderText="Journal Title" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="250px"/>
                <asp:BoundField DataField="Production Editor" HeaderText="Production Editor" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="Trim Size" HeaderText="Trim Size" ItemStyle-Width="70px" />
                <asp:BoundField DataField="Is CopyEdit" SortExpression="Is CopyEdit" HeaderText="Is CopyEdit" ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Is Sensitive" SortExpression="Is Sensitive" HeaderText="Is Sensitive" ItemStyle-Width="20px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Is SAM"  SortExpression="Is SAM" HeaderText="Is SAM"  ItemStyle-Width="20px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="FPM Journal" SortExpression="FPM Journal" HeaderText="FPM Journal" ItemStyle-Width="20px"  ItemStyle-HorizontalAlign="Center"/>
                <%--<asp:BoundField DataField="SAM Profile" HeaderText="SAM Profile" />--%>
                
                 <asp:TemplateField HeaderText="SAM Profile" ItemStyle-Width="10px"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                 <asp:ImageButton ID="imgSAMProfile" runat="server" CommandName="SAM Profile" ImageUrl="~/images/QMS/word.png" />
                  
                 </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="Style Sheet" HeaderText="Style Sheet" />--%>
               <asp:TemplateField HeaderText="Style Sheet" ItemStyle-Width="10px"  ItemStyle-HorizontalAlign="Center">
                 <ItemTemplate>
                   <asp:ImageButton ID="imgStyleSheet" runat="server" ImageUrl="~/images/QMS/word.png" CommandName="Style Sheet" /> 
                 </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="Markup Sample" HeaderText="Markup Sample" />--%>
                
                   <asp:TemplateField HeaderText="Markup Sample"  ItemStyle-Width="10px"   ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                   <asp:ImageButton ID="imgMarkupSample" runat="server"  ImageUrl="~/images/QMS/pdf.png" CommandName="Markup Sample"/>
                 </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="AQ Cover Sheet Number"  ItemStyle-Width="10px"   ItemStyle-HorizontalAlign="Center"> 
                 <ItemTemplate>
                   <asp:LinkButton ID="lnkAQ_Cover_Sheet_No" runat="server"   CommandName="AQ_Cover_Sheet_No" Text='<%# Eval("AQ_Cover_Sheet_No") %>'></asp:LinkButton>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DOI" HeaderText="References style with DOI information" ItemStyle-Width="100px"/>
                 <asp:BoundField DataField="Revised stylesheet received date" HeaderText="Revised stylesheet received date"  ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Template update date" HeaderText="Template update date" ItemStyle-Width="100px"/>
                
 
              
          
          </Columns>
        </asp:GridView>
        &nbsp;&nbsp;
        </div>
        
        <div class="content" id="tabJournaldetails" runat="server">
        
           <table border="0" cellpadding="2" cellspacing="0" visible="false">
               <tr>
                   <td align="left" colspan="2" style="font-weight: bold; color: white; background-color: green; height: 17px;">
                       &nbsp;Add New Journal</td>
               </tr>
               <tr>
                   <td colspan="2">
                       &nbsp;</td>
               </tr>
               <tr>
                   <td style="width: 215px; height: 23px;">
                       &nbsp;Journal Acronym :</td>
                   <td colspan="1" style="width: 232px; height: 23px">
                       <asp:TextBox ID="txtJournalCode" runat="server" CssClass="TxtBox" MaxLength="50"
                           Style="text-transform: uppercase" Width="120px"></asp:TextBox>&nbsp;
                       <asp:LinkButton ID="lnkSearch" runat="server" >Search</asp:LinkButton></td>
               </tr>
               <tr style="color: #000000">
                   <td colspan="2" style="height: 23px">
                       <asp:GridView ID="grdJourcode" runat="server" AutoGenerateColumns="false">
                           <Columns>
                                   <asp:TemplateField HeaderText="Edit">
                     <ItemTemplate>
                     <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument ="<%((GridviewRow) Container).RowIndex %>"></asp:LinkButton>
                      </ItemTemplate>
                      </asp:TemplateField>
                           <asp:BoundField DataField="Journal_Code" HeaderText="Journal Code" />
                               
                           </Columns>
                       </asp:GridView>
                   </td>
               </tr>
               <tr>
                   <td style="width: 215px; height: 23px">
                       &nbsp;Journal Title :</td>
                   <td colspan="1" style="height: 23px; width: 232px;">
                       <asp:TextBox ID="txtJournalTitle" runat="server" CssClass="TxtBox" MaxLength="50"
                           Style="text-transform: uppercase" TextMode="MultiLine" Width="120px"></asp:TextBox></td>
               </tr>
               <tr>
                   <td style="width: 215px">
                       &nbsp;Production Editor :</td>
                   <td colspan="1" style="width: 232px">
                       <asp:TextBox ID="txtProdEdit" runat="server" CssClass="TxtBox" MaxLength="50" Style="text-transform: uppercase"
                           Width="120px"></asp:TextBox></td>
               </tr>
               <tr>
                   <td style="width: 215px">
                       &nbsp;Trim Size :</td>
                   <td colspan="1" style="width: 232px">
                       <asp:TextBox ID="txtTrimSize" runat="server" CssClass="TxtBox" MaxLength="50" Style="text-transform: uppercase"
                           Width="120px"></asp:TextBox></td>
               </tr>
               <tr>
                   <td style="width: 215px">
                       &nbsp;Is Copyedit :</td>
                   <td colspan="1" style="width: 232px">
                       <asp:DropDownList ID="ddlCopyedit" runat="server">
                           <asp:ListItem></asp:ListItem>
                           <asp:ListItem>=</asp:ListItem>
                           <asp:ListItem>&gt;=</asp:ListItem>
                           <asp:ListItem>&gt;</asp:ListItem>
                           <asp:ListItem>&lt;=</asp:ListItem>
                           <asp:ListItem>&lt;</asp:ListItem>
                           <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                       </asp:DropDownList></td>
               </tr>
               <tr>
                   <td style="width: 215px; height: 23px">
                       &nbsp;Is Sensitive :</td>
                   <td colspan="1" style="height: 23px; width: 232px;">
                       <asp:DropDownList ID="ddlSensitive" runat="server">
                           <asp:ListItem></asp:ListItem>
                           <asp:ListItem>=</asp:ListItem>
                           <asp:ListItem>&gt;=</asp:ListItem>
                           <asp:ListItem>&gt;</asp:ListItem>
                           <asp:ListItem>&lt;=</asp:ListItem>
                           <asp:ListItem>&lt;</asp:ListItem>
                           <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                       </asp:DropDownList></td>
               </tr>
               <tr>
                   <td style="width: 215px">
                       &nbsp;Is SAM :</td>
                   <td style="width: 232px">
                       <asp:DropDownList ID="ddlSAM" runat="server">
                           <asp:ListItem></asp:ListItem>
                           <asp:ListItem>=</asp:ListItem>
                           <asp:ListItem>&gt;=</asp:ListItem>
                           <asp:ListItem>&gt;</asp:ListItem>
                           <asp:ListItem>&lt;=</asp:ListItem>
                           <asp:ListItem>&lt;</asp:ListItem>
                           <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                       </asp:DropDownList>
                       &nbsp;&nbsp;</td>
               </tr>
               <tr>
                   <td style="width: 215px">
                       &nbsp;FPM Journal :</td>
                   <td style="width: 232px">
                       <asp:DropDownList ID="ddlFPM" runat="server">
                           <asp:ListItem></asp:ListItem>
                           <asp:ListItem>=</asp:ListItem>
                           <asp:ListItem>&gt;=</asp:ListItem>
                           <asp:ListItem>&gt;</asp:ListItem>
                           <asp:ListItem>&lt;=</asp:ListItem>
                           <asp:ListItem>&lt;</asp:ListItem>
                           <asp:ListItem Value="&lt;&gt;"></asp:ListItem>
                       </asp:DropDownList>&nbsp;
                   </td>
               </tr>
               <tr>
                   <td style="width: 215px">
                       &nbsp;AQ Cover Sheet Number :</td>
                   <td style="width: 232px">
                       <asp:TextBox ID="txtAQsheet" runat="server" CssClass="TxtBox" MaxLength="50" Style="text-transform: uppercase"
                           Width="120px"></asp:TextBox></td>
               </tr>
               <tr>
                   <td style="width: 215px; height: 26px">
                       &nbsp;Revised stylesheet received date :</td>
                   <td style="width: 232px; height: 26px">
                       <asp:TextBox ID="txtRevisedDate" runat="server" CssClass="TxtBox" MaxLength="50"
                           Style="text-transform: uppercase" Width="120px"></asp:TextBox>
                       <img id="Img1" runat="server" align="absMiddle" alt="Calendar" border="0" height="20"
                           onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtRevisedDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                           src="images/Calendar.jpg" style="cursor: pointer" /></td>
               </tr>
               <tr>
                   <td colspan="1" style="width: 215px">
                       &nbsp;Template update date :</td>
                   <td colspan="1" style="width: 232px">
                       <asp:TextBox ID="txtTeplateDate" runat="server" CssClass="TxtBox" MaxLength="50"
                           Style="text-transform: uppercase" Width="120px"></asp:TextBox>
                       <img id="Img12" runat="server" align="absMiddle" alt="Calendar" border="0" height="20"
                           onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtTeplateDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                           src="images/Calendar.jpg" style="cursor: pointer" /></td>
               </tr>
               <tr>
                   <td style="width: 215px">
                   </td>
                   <td style="width: 232px">
                   </td>
               </tr>
               <tr bgcolor="honeydew">
                   <td align="center" colspan="2">
                       <a class="link1" href="#" onclick="javascript:clearAdvancedCtrls();"><strong>Add</strong></a>
                       &nbsp; <a class="link1" href="#" onclick="javascript:__doPostBack('btnSearch','advanced');">
                           <strong>Search</strong></a> &nbsp; <a class="link1" href="#" onclick="javascript:closeAdvancedModal();">
                               <strong>Cancel</strong></a>
                   </td>
               </tr>
           </table>
        </div>
        
     </div>
     
     
       <div visible="false">
           &nbsp;</div>
             <div id="divfooter" class="footer" onclick="javascript:__doPostBack('lnkArticledetails','');">Show Details</div>
    </form>
    
</body>
</html>
