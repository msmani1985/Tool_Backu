<%@ page language="C#" autoeventwireup="true" inherits="createjournal, App_Web_olx2vwmy" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Journal Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function showhiddiv()
    {
        obj=document.getElementById("divjournal");
        //obj.className="div_style";
        obj.className="divjournal";
        obj2=document.getElementById("divcreatejournal");
        obj2.className="div_small";
    }
    function hidediv()
    {
        obj=document.getElementById("divjournal");
        //obj.className="div_style";
        obj.className="";
        obj2=document.getElementById("divcreatejournal");
        obj2.className="div_hide";
    }
    

    
    
    </script>
    <style>
        div.divjournal
        {
            position: absolute;
            top: 0; /* These positions makes sure that the overlay */
            bottom: 0;  /* will cover the entire parent */
            left: 0;
            width: 100%;
            opacity:0.4;
             background: LightGrey;
            filter:alpha(opacity=30)
        }
        
    </style>
</head>
<body>




    <form id="form1" runat="server">
    <div class="dptitle">Journal</div>
    <div id="divjournal" >
        <table width="90%">
            <tr>
                <td >Account</td>
                <td><asp:ListBox ID="lbaccount" runat="server" SelectionMode="multiple" DataTextField="cust_name" DataValueField="customer_id"></asp:ListBox>&nbsp;&nbsp;
                    <asp:ImageButton ID="btnavbljournal" ImageUrl="~/images/fetchrec.jpg" ToolTip="Display Journal"  runat="server" OnClick="btnavbljournal_Click" />&nbsp;&nbsp;
                    <Button ID="btncjournal" title="Create New Journal" value="" style=" vertical-align:middle; width:25px;height:25px; background-color:White;border:none; background-image:url('Images/Symbol_Add.png'); background-repeat:no-repeat;" onclick="showhiddiv();" />
                </td>
            </tr>
            <tr><td colspan="2" style="background-image:url('Images/line.gif'); background-repeat:repeat-x;" >&nbsp;</td></tr>
        </table>
    </div>
    <div id="divcreatejournal" class="div_hide"  >
        <table width="60%" style="border:solid 1px Green;">
            <tr><td colspan="2" align="center" class="HeadText" >Create Journal</td></tr>
            <tr><td>Customer</td><td><asp:DropDownList ID="ddlcustname" runat="server" DataTextField="cust_name" DataValueField="customer_id"></asp:DropDownList></td></tr>
            <tr><td  align="right">Journal Title</td><td><asp:TextBox ID="Txtjourname" runat="server"></asp:TextBox></td></tr>
            <tr><td  align="right">Journal Code</td><td><asp:TextBox ID="Txtjourcode" runat="server"></asp:TextBox></td></tr>
            <tr><td colspan="2" align="center"><button id="btnsave" value="Save" onclick="InsertJournal();" style="font-family: Verdana, Tahoma, Sans-Serif">
                Save</button>&nbsp;<button id="btnexit" value="Exit" onclick="hidediv();" style="font-family: Verdana, Tahoma, Sans-Serif">
                    Exit</button> </td></tr>
        </table>
    </div>
    <div>
    <table Width="95%">
        <tr><td align="right"><asp:ImageButton ID="imgExport_Excel" runat="server" ImageUrl="~/images/Excel.jpg" OnClick="imgExport_Excel_Click" /></td></tr>
        <tr><td>
            <asp:GridView Width="100%" ID="gvjournaldetails" AllowSorting="true" runat="server" Font-Size="7pt" AutoGenerateColumns="false"
            OnSorting="Grid_Sorting">
            <HeaderStyle CssClass="GVFixedHeader" />
            <Columns>
            <asp:BoundField DataField="cust_name" SortExpression="cust_name" HeaderText="Customer" />
            <asp:BoundField DataField="journal_code" SortExpression="journal_code" HeaderText="Journal Acronym" />
            <asp:BoundField DataField="journal_name" SortExpression="journal_name" HeaderText="Journal Title" />
            <asp:BoundField DataField="PEditor" SortExpression="PEditor" HeaderText="Production Editor" />
            <asp:BoundField DataField="PEEmail" SortExpression="PEEmail" HeaderText="PE Email(s)" />
            <%--<asp:BoundField DataField="pagetrim_size" SortExpression="pagetrim_size" HeaderText="Trim Size" />--%>
            <asp:BoundField DataField="format" SortExpression="format" HeaderText="Format" />
            <asp:BoundField DataField="pagetrim_code" SortExpression="pagetrim_code" HeaderText="Trim Code" />
            <asp:BoundField DataField="iscopyedit" SortExpression="iscopyedit"  HeaderText="Is CopyEdit" />
            <asp:BoundField DataField="issam" SortExpression="issam"  HeaderText="Is SAM" />
            <asp:BoundField DataField="issensitive" SortExpression="issensitive"  HeaderText="Is Sensitive" />
            <asp:BoundField DataField="pricecode" SortExpression="pricecode" HeaderText="Price Code" />
            <asp:TemplateField ItemStyle-Font-Bold="true" >
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem,"Edit") %></ItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </td></tr>
        </table>
    </div>
    
        
    </form>
    
    <script language="javascript">
        var xmlHTTP;
        function InsertJournal()
        {
            try
            {
                if(document.getElementById("Txtjourcode").value=="" || document.getElementById("Txtjourname").value=="")
                {
                    alert("Please give Journal Title and Journal Code");
                    return false;
                }
                // Firefox, Opera 8.0+, Safari
                xmlHTTP=new xmlHTTPRequest();
            }
            catch(e)
            {
                // Internet Explorer
                 try
                 {
                    xmlHTTP=new ActiveXObject("Msxml2.XMLHTTP");
                 }
                 catch(e)
                 {
                    try
                    {
                        xmlHTTP=new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    catch(e)
                    {
                        alert("Your browser does not support AJAX!");
                        return false;
                    }
                 }
            }
            
            var idval;
            xmlHTTP.onreadystatechange=function()
            {
                if(xmlHTTP.readyState==4)
                {
                    if(xmlHTTP.responseText=="This Journal is Already Exists")
                        alert(xmlHTTP.responseText);
                    else    
                        document.location="EditJournalDetails.aspx?" + xmlHTTP.responseText;
                }
            }
            var val="custid=" + document.getElementById("ddlcustname").value + "&jourcode=" + document.getElementById("Txtjourcode").value + "&jourtitle=" + document.getElementById("Txtjourname").value;
             xmlHTTP.open("Get","InsertJournal.aspx?" + val, true);
             xmlHTTP.send(null);
             
        }
        
    
    </script>

    
</body>
</html>
