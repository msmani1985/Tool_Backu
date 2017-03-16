<%@ Page Language="C#" AutoEventWireup="true" CodeFile="11-07-2017_QMS_StyleSheet.aspx.cs" Inherits="QMS_StyleSheet" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="default.css" rel="stylesheet" type="text/css" />   
     
     <script type="text/javascript">
       function openAdvancedModal(){        
           
             document.getElementById ('divPopAddUpdate').style.visibility='visible';
            document.getElementById ('divPopAddUpdate').style.display='';       
            document.getElementById ('divPopAddUpdate').style.top= '65px';
            document.getElementById ('divPopAddUpdate').style.left='128px'; 
            if (typeof document.body.style.maxHeight == "undefined")
            {  
                var layer = document.getElementById ('divPopAddUpdate');
                layer.style.display = 'block';
                var iframe = document.getElementById('iframetop');
                iframe.style.display = 'block';
                iframe.style.visibility = 'visible';
                iframe.style.top= layer.offsetTop-10;
                iframe.style.left= layer.offsetLeft-10;
                iframe.style.width=  layer.offsetWidth+10;
                iframe.style.height= layer.offsetHeight+10; 
            }
            else
                {     
                    document.getElementById ('divMasked').style.display='';
                    document.getElementById ('divMasked').style.visibility='visible';
                    document.getElementById ('divMasked').style.top='0px';
                    document.getElementById ('divMasked').style.left='0px';
                    document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
                    document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px'; 
                }     
          
        }
     function closeAdvancedModal(){
            document.getElementById ('divMasked').style.display='none';
            document.getElementById ('divPopAddUpdate').style.display='none';
            document.getElementById ('iframetop').style.display='none';
        }
        </script>
        
           <style>
        iframe.divMasked
        {
        position:absolute;
	    padding:5px;
	    visibility:hidden;
	    border:1px solid gray;
	    font:normal 12px Verdana;
	    line-height:18px;
	    z-index:10000;
	    background-color:#ededed;
	    overflow-x:auto;
	    overflow-y:auto;
	    top:-1000px;
	    left:-1000px;
	    filter:progid:DXImageTransform.Microsoft.Shadow(color=#96A8BA,direction=135,Strength=5);
        }
        div.divMasked 
        {
        visibility: hidden;
        position:absolute;
        left:0px;
        top:0px;
        font-family:verdana;
        font-weight:bold;
        padding:40px;
        z-index:100;        
        background-image:url(images/tools/Mask.png);
        /* ieWin only stuff */
        _background-image:none;
        _filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true, sizingMethod=scale src='Mask.png');
        opacity:0.4;
        filter:alpha(opacity=70)
        }
        div.ModalPopup {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 11px;
        font-style: normal;
        background-color: #fff;
        position:absolute;
        /* set z-index higher than possible */
        z-index:10000;
        visibility: hidden;
         color: Black;
        border-style: solid;
        border-color: #999999;
        border-width: 1px;
        width: 300px;
        height :auto;
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
                
               <%-- <asp:TemplateField>
                <ItemTemplate>
              
                <asp:ImageButton ID="imgSAMProfileWord" runat="server" CommandName="imgSAMProfileWord" Visible="false" />
                <asp:ImageButton ID="imgWordStylesheet" runat="server" Visible="false" CommandName="imgWordStylesheet" />
                <asp:ImageButton ID="ïmgPdf" runat="server" CommandName="ïmgPdf" Visible="false"/>
                </ItemTemplate>
                </asp:TemplateField>--%>
          
                   <asp:TemplateField HeaderText="Edit" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="lnkEdit" runat="server" 
                            CommandName="Modify" ImageUrl="~/images/QMS/Edit.gif" OnClientClick="javascript:openAdvancedModal();"/>
                    </ItemTemplate>
                </asp:TemplateField>
          </Columns>
        </asp:GridView>
        &nbsp;&nbsp;
     </div>
     
     
       <div visible="false">
           <table border="0" cellpadding="2" cellspacing="0" visible="false">
               <tr>
                   <td align="left" colspan="2" style="font-weight: bold; color: white; background-color: green">
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
                       <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click">Search</asp:LinkButton></td>
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
    </form>
</body>
</html>
