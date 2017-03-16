<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Datapage MIS</title>
    <link href="default.css" type="text/css" rel=stylesheet />    
</head>

<frameset rows="104px,*" FRAMEBORDER='0' FRAMESPACING='0'>
    <frame src="toppage.aspx" scrolling="no" name="top" noresize />
   
    
	<frameset name="contents" cols="170px,*" FRAMEBORDER='0' FRAMESPACING='0'>
		<frame src="menu.aspx" name="left"  noresize />
		<frame src="welcome.aspx" name="right" style="padding-top:10px;padding-left:10px" noresize/>    
	</frameset>
</frameset>
</html>
