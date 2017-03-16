<%@ page language="C#" autoeventwireup="true" inherits="welcome, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel=stylesheet />
</head>

<body style="width:100%;height:100%">
    <form id="form1" runat="server">
    
    
    <div id="Condiv" style="width:80%;margin-left:5pt;" runat="server" >
    <div id="divwelcome"  runat="server" >Welcome Page</div>
    <font face="verdana" size="1">
        <p> Datapage's Management Information System (MIS) is a single source location for all production, sales and associated financial information. It offers a lot of useful information on all the company's production related activities as well as planned and proposed budgetary figures. Contact information of the various editors, managers and printers associated with our journals are viewable at the click of a mouse.  </p>
  
<p>The website is ordered under various categories that in turn contain links to the various types of information that they represent. As a user, you will be setup to access the website using stipulated logon information and specialized access rights that determine your areas of preview.  </p>
  
<p>Please be aware that the information revealed to you on this website is purely for statistical purposes and should under no circumstances be revealed or shared with any third-party. You will be advised to logoff from the website or close your browser whenever you are not at your desk to help protect the security of this information.  </p>
    </font>
    </div>
    
    <div id="sanlucas_Condiv" style="width:80%;margin-left:5pt;" runat="server" >
    <div id="sanlucas_divwelcome"  runat="server" style="font-family:Garamond;font-size:12pt;">Welcome Page</div>
        <p style="font-family:Garamond;font-size:10pt;"> Datapage's Customer Relationship Management (CRM) system is a single source location that offers a lot of useful information on all the San Lucas Medical journal production related activities. Contact information of the various editors, reviewers and authors associated with SLM journals are viewable at the click of a mouse.</p>
  
<p style="font-family:Garamond;font-size:10pt;">The website is ordered under various categories that in turn contain links to the various types of information that they represent. As a user, you will be setup to access the website using stipulated logon information and specialized access rights that determine your areas of preview. </p>
  
<p style="font-family:Garamond;font-size:10pt;">Please be aware that the information revealed to you on this website is purely for statistical purposes and should under no circumstances be revealed or shared with any third-party. You will be advised to logoff from the website or close your browser whenever you are not at your desk to help protect the security of this information.  </p>
    </div>
    <div align="center" style="padding-top:50px;" >
     <font color="gray" style="font-family:Garamond;font-size:9pt;">   &copy; 2011 Datapage International Limited. All rights reserved.</font>
    </div>
    <div style="height:10px;display:none "></div>
    <div id="divTeamList" runat="server" visible=false style="display:none "  >
        <asp:GridView ID="agvTeamList" CssClass="dullbackground" 
            AlternatingRowStyle-CssClass="lightbackground"
            HeaderStyle-CssClass="darkbackground" AutoGenerateColumns="false" 
            runat="server" Width="750px"
            >
           <Columns>
                <asp:BoundField DataField="EMPNO" HeaderText="EMPNO" />                
                <asp:BoundField DataField="COLUMN1" HeaderText="NAME" />
                <asp:BoundField DataField="DNAME" HeaderText="DEPARTMENT" />
                <asp:BoundField DataField="EMPPOSITION" HeaderText="DESIGNATION" />
            </Columns>            
            </asp:GridView>
    
    </div>
    <br />
    <div id="SqlTeamListDiv" runat="server" visible=false>
        <asp:GridView ID="SQLTeamList" runat="server" CssClass="dullbackground" 
            AlternatingRowStyle-CssClass="lightbackground"
            HeaderStyle-CssClass="darkbackground" AutoGenerateColumns="true" Width="750px" >
        </asp:GridView>
        
    </div>
    
    
    </form>
</body>
</html>

