<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QMS_StyleSheet.aspx.cs" Inherits="QMS_StyleSheet" EnableEventValidation="false" %>

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
      
          PageSize="7" OnRowDataBound="grdStyleSheet_RowDataBound" OnRowCommand="grdStyleSheet_RowCommand" DataKeyNames="Journal Acronym" OnSorting="grdStyleSheet_Sorting"  AllowSorting="True" OnRowCancelingEdit="grdStyleSheet_RowCancelingEdit" OnRowEditing="grdStyleSheet_RowEditing" OnRowUpdating="grdStyleSheet_RowUpdating">
            <HeaderStyle CssClass="GVFixedHeader"/>
            <AlternatingRowStyle BackColor="#F2F2F2" />
            <Columns>
            <asp:TemplateField ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>Serial No.</HeaderTemplate>
                <ItemTemplate>
                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:BoundField DataField="Journal_Id" HeaderText="Journal Id" Visible="false"/>
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
                 <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />
                </ItemTemplate>
                  <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
                    <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                </EditItemTemplate>
                </asp:TemplateField>
               
               
                 <%--  <asp:TemplateField HeaderText="Edit" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="lnkEdit" runat="server" 
                            CommandName="Modify" ImageUrl="~/images/QMS/Edit.gif" OnClientClick="javascript:openAdvancedModal();"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
          </Columns>
        </asp:GridView>
        &nbsp;&nbsp;
     </div>
     
     
       <div id="divPopAddUpdate"  style="left: 0px; width: 576px;
                                            top: 0px" visible="false">
                                        </div>
    </form>
</body>
</html>
