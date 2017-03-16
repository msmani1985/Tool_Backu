<%@ page language="C#" autoeventwireup="true" inherits="job_pagination, App_Web_vlobbbje" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Pagination</title>
    <link href="default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>

    <script language="javascript">
    function setBGColor(elem){
        alert(elem.style.backgroundColor);
        elem.style.backgroundColor = '#C2C2C2';
    }
    function setMouseOverColor(element){
        //gvcolor = element.style.backgroundColor;
        //element.style.backgroundColor='#C2C2C2';
        //element.style.cursor='hand';
        element.style.textDecoration='underline';
    }
    function setMouseOutColor(element){
        //element.style.backgroundColor=gvcolor;
        element.style.textDecoration='none';
    }
    function openInsertModal(){        
        document.getElementById ('divPopInsert').style.visibility='visible';
        document.getElementById ('divPopInsert').style.display='';       
        document.getElementById ('divPopInsert').style.top= '120px';
        document.getElementById ('divPopInsert').style.left='260px'; 
        if (typeof document.body.style.maxHeight == "undefined")
        {  
            var layer = document.getElementById ('divPopInsert');
            layer.style.display = 'block';
            var iframe = document.getElementById('iframetop');
            iframe.style.display = 'block';
            iframe.style.visibility = 'visible';
            iframe.style.top= layer.offsetTop-10;
            iframe.style.left= layer.offsetLeft-10;
            iframe.style.width=  layer.offsetWidth+10;
            iframe.style.height= layer.offsetHeight+10; 
        }else
        {     
        document.getElementById ('divMasked').style.display='';
        document.getElementById ('divMasked').style.visibility='visible';
        document.getElementById ('divMasked').style.top='0px';
        document.getElementById ('divMasked').style.left='0px';
        document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
        document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px';        
        }        
    }
    function closeInsertModal(){
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopInsert').style.display='none';
        document.getElementById ('iframetop').style.display='none';
    }    
    function validSelection(){
        if(document.form1.txtRowIndex.value==''){
            alert('Select a row to insert before/after');
            //return false;
        }
        else openInsertModal();
        //if(confirm('Do you want to insert a blank record '+ document.form1.drpInsert.value +' row '+ document.form1.txtRowIndex.value)==false)
        //    return false;
    }
    function validInsert(){
    /*
        if(document.form1.txtNoofItems.value==''){
            alert('Enter the No. of Items to Insert');
            return false;
        }
        else
        if(confirm('Do you want to insert '+ document.form1.txtNoofItems.value +' '+ document.form1.drpNonArticles.value +' record(s) '+ document.form1.drpInsert.value +' row '+ document.form1.txtRowIndex.value+'?')==false)
            return false;
            */
        if(confirm('Confirm Insert?')==false)
            return false
        return true;
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
        <iframe width="0" scrolling="no" height="0" frameborder="0" class="divMasked" id="iframetop">
        </iframe>
        <div>
            <div class="dptitle">
                Job Pagination</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="width: 478px">
            <tr>
                <td align="right">
                    <strong style="font-family: 'Segoe UI'">Job Number:&nbsp; </strong>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtJobNumber" runat="server" CssClass="TxtBox" Width="105px" 
                        Font-Names="Segoe UI"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" Text="Search" OnClick="btnSearch_Click"
                        Width="75px" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="divPopInsert" class="ModalPopup" style="left: 0px; width: 276px;
                        top: 0px">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" colspan="2" style="font-family: 'Segoe UI';font-weight: bold; color: white; background-color: green">
                                    &nbsp;Insert Non Articles&nbsp;</td>
                                <td align="right" style="background-color: green; color: White; font-weight: bold;">
                                    <a href="#" title="Close" onclick="javascript:closeInsertModal();" style="color: White;">
                                        [x]</a>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="3">
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 182px;font-family: 'Segoe UI'">
                                    &nbsp;Article Type</td>
                                <td style="width: 228px">
                                    No. of Items</td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 182px;font-family: 'Segoe UI'">
                                    &nbsp;Blank:</td>
                                <td style="width: 228px">
                                    <asp:TextBox ID="txtBlankCount" runat="server" CssClass="TxtBox" Width="105px" onkeypress="javascript:return OnlyAllowNumbers(this,event);"></asp:TextBox></td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 182px;font-family: 'Segoe UI'">
                                    &nbsp;Front Cover:</td>
                                <td style="width: 228px">
                                    <asp:TextBox ID="txtFrontCovCount" runat="server" CssClass="TxtBox" Width="105px" onkeypress="javascript:return OnlyAllowNumbers(this,event);"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 182px;font-family: 'Segoe UI'">
                                    &nbsp;Back Cover:</td>
                                <td style="width: 228px;font-family: 'Segoe UI'">
                                    <asp:TextBox ID="txtBackCovCount" runat="server" CssClass="TxtBox" onkeypress="javascript:return OnlyAllowNumbers(this,event);"
                                        Width="105px"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 182px;font-family: 'Segoe UI'">
                                    &nbsp;Prelim:</td>
                                <td style="width: 228px">
                                    <asp:TextBox ID="txtPrelimCount" runat="server" CssClass="TxtBox" Width="105px" onkeypress="javascript:return OnlyAllowNumbers(this,event);"></asp:TextBox></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 182px">
                                </td>
                                <td style="width: 228px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr bgcolor="Honeydew">
                                <td align="center" style="font-family: 'Segoe UI'" colspan="3">
                                    <asp:LinkButton ID="lnkbtnInsertNonArticle" CssClass="link1" OnClientClick="javascript: return validInsert();" runat="server" Font-Bold="True" OnClick="lnkbtnInsertNonArticle_Click">Insert</asp:LinkButton>&nbsp; <a class="link1" href="#" onclick="javascript:closeInsertModal();">
                                            <strong>Cancel</strong></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <div id="divPagination" runat="server">
            <table align="center" cellpadding="1" cellspacing="1" class="bordertable">
                <tr>
                    <td align="right">
                        <div style="float: left">
                            <asp:Label ID="lblIssue" runat="server" Font-Bold="True" ForeColor="Green" 
                                Font-Size="14px" Font-Names="Segoe UI"></asp:Label></div>
                        <asp:DropDownList ID="drpInsert" runat="server">
                            <asp:ListItem Value="after">After</asp:ListItem>
                            <asp:ListItem Value="before">Before</asp:ListItem>                            
                        </asp:DropDownList>
                        Row #
                        <asp:TextBox ID="txtRowIndex" runat="server" CssClass="TxtBox" Text="" BackColor="#ECECEC"
                            Font-Bold="True" ForeColor="Maroon" Width="27px"></asp:TextBox>
                        --&gt;
                        <input id="cmdInsert" type="button" value="Insert" class="dpbutton" onclick="javascript:return validSelection();" />
                        <asp:Button ID="btnAdd" runat="server" CssClass="dpbutton" Text="Add Articles" OnClick="btnAdd_Click"
                            Width="102px" />
                        <asp:Button ID="btnPaginate" runat="server" CssClass="dpbutton" Text="Paginate" OnClick="btnPaginate_Click"
                            Width="74px" />
                        <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" Text="Save" OnClick="btnSave_Click"
                            Width="62px" />
                        <asp:Button ID="btnPreview" runat="server" CssClass="dpbutton" Text="Preview" Width="73px"
                            OnClick="btnPreview_Click" /></td>
                </tr>
                <tr>
                    <td align="right" colspan="6" style="width: 90%">
                        <asp:HiddenField ID="hfJourID" runat="server" />
                        <asp:HiddenField ID="hfJobIDs" runat="server" />
                        <asp:LinkButton ID="lnkSaveArticles" runat="server" OnClick="lnkSaveArticles_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" style="width: 90%">
                        <asp:GridView ID="gvPagination" CssClass="lightbackground" runat="server" Width="100%"
                            AutoGenerateColumns="False" ShowFooter="True" 
                            OnRowDataBound="gvPagination_RowDataBound" EnableViewState="true" 
                            Font-Names="Segoe UI" Font-Size="11px">
                            <RowStyle HorizontalAlign="Left" />
                            <%--<AlternatingRowStyle CssClass="dullbackground" />--%>
                            <Columns>
                                <asp:TemplateField HeaderText="Row #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                                        <asp:HiddenField ID="hfgvJobID" runat="server" Value='<%# Eval("job_id") %>' />
                                        <asp:HiddenField ID="hfgvDocTypeID" runat="server" Value='<%# Eval("document_type_id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manuscript ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvManuscript" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doc Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="dd_doctype" runat="server"  AppendDataBoundItems="true"
                                         OnSelectedIndexChanged="doctype_OnSelectIndexChanged" AutoPostBack="true" >
                                         <asp:ListItem Text="Select a Value" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblgvDocType" Visible="false" runat="server" Text='<%# Eval("document_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sub Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="dd_subtype" runat="server" DataTextField="document_item_name" DataValueField="document_item_type_id"  
                                         >
                                        <asp:ListItem Text="Select a Value" Value="0"></asp:ListItem></asp:DropDownList>
                                        <asp:Label ID="lblgvDocItemType" Visible="false" runat="server" Text='<%# Eval("document_item_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Page From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvFrom" runat="server" CssClass="TxtBox" Width="60px" Text='<%# Eval("page_from") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Page To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvTo" runat="server" CssClass="TxtBox" Width="60px" Text='<%# Eval("page_to") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_gvtotal" runat="server" CssClass="TxtBox" Width="60px" Text='<%# Eval("totpages") %>'></asp:TextBox>
                                        <asp:Label ID="lblgvTotal" runat="server" Visible="false" Width="60px" Text='<%# Eval("totpages") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Style Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpgvStyleType" runat="server" AppendDataBoundItems="true"
                                            DataSource="<%# getStyleTypes()%>" DataTextField="number_system_name" DataValueField="number_system_id"
                                            SelectedValue='<%# Eval("number_system_id") %>'>
                                            <asp:ListItem Text=" --select-- " Value="" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Index">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvOrderIndex" runat="server" CssClass="TxtBox" Width="40px" Text='<%# Eval("sequence") %>'
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkgvSelect" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        &nbsp;<asp:ImageButton ID="imgbtngvDelete" runat="server" ToolTip="Delete" OnClientClick="javascript: return confirm('Confirm Delete?');"
                                            ImageUrl="~/images/tools/delete.png" ImageAlign="AbsMiddle" OnClick="imgbtngvDelete_Click" />
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        &nbsp;<asp:ImageButton ID="imgbtngvDelete1" runat="server" ToolTip="Delete" OnClientClick="javascript: return confirm('Confirm Delete?');"
                                            ImageUrl="~/images/tools/delete.png" ImageAlign="AbsMiddle" OnClick="imgbtngvDelete_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" />
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        &nbsp;
    </form>
</body>
</html>
