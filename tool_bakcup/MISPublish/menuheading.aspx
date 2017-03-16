<%@ page language="C#" autoeventwireup="true" inherits="menuheading, App_Web_25d24vps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function MenuHeadingValidation()
        {
            if(document.getElementById("newmenuheading").value=="")
                {
                    alert("You must give MenuHeading");
                    document.getElementById("newmenuheading").focus();
                    return false;
                } 
            if(document.getElementById("MHorderindexTxt").value=="")
                {
                    alert("You must give MenuHeading Order Index");
                    document.getElementById("MHorderindexTxt").focus();
                    return false;
                } 
                
            return true;                           
        }
        function MenuItemValidation()
        {
            if (document.getElementById("txtItemName").value=="")
                {
                    alert("You must give ItemName");
                    document.getElementById("txtItemName").focus();
                    return false;
                }
            if (document.getElementById("txtItemFile").value=="")
                {
                    alert("You must give ItemFile");
                    document.getElementById("txtItemFile").focus();
                    return false;
                }
            if (document.getElementById("txtOrderIndex").value=="")
                {
                    alert("You must give OrderIndex");
                    document.getElementById("txtOrderIndex").focus();
                    return false;
                }
                
            if (document.getElementById("lstMenuHeading1").value=="")
                {
                    alert("Please select any one in MenuHeading");
                    document.getElementById("lstMenuHeading1").focus();
                    return false;
                }
           if (document.getElementById("lstmenugroup").value=="")
                {
                    alert("Please select any one in Menugroup");
                    document.getElementById("lstmenugroup").focus();
                    return false;
                }
                
                return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menuheadings" class="borderdiv" style="float:left;margin-left:10px;border:2px solid green;overflow:auto;width:300px;height:220px;">
        <table width="100%" >
        <tr><td class="dpGreenHeader" colspan="2">
        Create Menu Heading</td></tr>
        <tr><td align="left"  >Menu Heading:</td><td align="left"  >
            <asp:TextBox Text="" ID="newmenuheading" runat="server" ></asp:TextBox>
        </td></tr>
        <tr>
            <td>Menu Heading Order Index:</td>
            <td align="left"  >
                <asp:TextBox Text="" ID="MHorderindexTxt" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr><td align="left" >Available Menu Headings:</td><td align="left"  >
            <asp:ListBox ID="lstMenuHeading" DataTextField="menu_heading_name" DataValueField="menu_heading_id" runat="server" SelectionMode="Single" Width="120px" Height="100px">
            </asp:ListBox>  
        </td></tr>     
        <tr><td align="center" colspan="2"><asp:Button ID="btnCreateMenu" CssClass="dpbutton" Text="Create" runat="server" OnClick="btnCreateMenu_Click" OnClientClick="return MenuHeadingValidation();" />   </td></tr>              
        </table>
    </div>
    <div style="float:left;">&nbsp;</div>
    <div id="menuitems"  style="float:left;margin-left:10px;border:2px solid green;overflow:auto;width:300px;height:420px;">
        <table width="100%" >
        <tr><td class="dpGreenHeader" colspan="2">
        Create Menu Items</td></tr>
        <tr><td align="left"  >Menu item Name:</td><td align="left"  ><asp:TextBox Text="" ID="txtItemName" runat="server" ></asp:TextBox>  </td></tr>
        <tr><td align="left"  >Menu item File:</td><td align="left"  ><asp:TextBox Text="" ID="txtItemFile" runat="server" ></asp:TextBox>  </td></tr>        
        <tr><td align="left"  >Menu item Order Index:</td><td align="left"><asp:TextBox Text="" ID="txtOrderIndex" runat="server" ></asp:TextBox>  </td></tr>                
        <tr><td align="left"  >Menu item</td><td align="left"><asp:DropDownList ID="lstMenuItem" runat="server" DataTextField="menu_item_name" DataValueField="menu_item_id" AutoPostBack="True"  OnSelectedIndexChanged="lstMenuItem_SelectedIndexChanged"></asp:DropDownList></td></tr>                
        <tr><td align="left" >Add to MenuHeading:</td><td align="left"  >
            <asp:ListBox ID="lstMenuHeading1" DataTextField="menu_heading_name" DataValueField="menu_heading_id" runat="server" SelectionMode="Single" Width="120px" Height="120px">
            </asp:ListBox>  
        </td></tr>        
        <tr><td align="left" >Assign to Group(s):</td><td align="left"  >
            <asp:ListBox ID="lstmenugroup" DataTextField="menu_group_name" DataValueField="menu_group_id" runat="server" SelectionMode="Multiple" Width="120px" Height="120px">
            </asp:ListBox>  
        </td></tr>        
        <tr><td align="center" colspan="2" style="height: 23px"><asp:Button ID="btnCreatMenuItem" CssClass="dpbutton" Text="Create" runat="server" OnClick="btnCreatMenuItem_Click" OnClientClick="return MenuItemValidation();"  />
        &nbsp;<asp:Button ID="BtnUpdateMenuitem" runat="server" Text="Update" CssClass="dpbutton" OnClick="BtnUpdateMenuitem_Click" />
           </td></tr>
        </table>
    </div>
    <div style="float:left;">&nbsp;</div>    
    <div id="menuitemslist" style="float:left;border:2px solid green;margin-left:10px;overflow:auto;width:160px;height:400px;">
        <table width="100%" >
        <tr><td class="dpGreenHeader" >
        Menu Items List</td></tr>
        <tr>
        <td align="center" id="itemandheadings" runat="server" >
        
        </td></tr>
        </table>
    </div>
    </form>
</body>
</html>

