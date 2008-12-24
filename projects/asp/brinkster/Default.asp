<%@ Language=VBScript %>

<!--#Include file="Inc/Library.asp" -->
<%
    'Response.Write(strServerPath)
    'Response.End()
 %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>NNC Club</title>
</head>


<body>

<center>
<table bgcolor="#006600" width="800"  border="0">
<tr><td>
<input type="hidden" name="reqPage" value="home" />
<% reqPage = "Home" %>
<!--#Include file="Inc/Top.asp"-->
<table width="800"  border="0" height="335">
  <tr>
    <td valign="top" height="331">
    <table width="800"  border="1" bordercolor="#907319" height="330">
      <tr>
        <td valign="top" height="40" align=left><h3 class="style1">Home</h3></td>
      </tr>
      <tr>
        <td valign="top" height="278">
          <p class="style2" align="center"><font size="6">
          	
<form action="http://www45.brinkster.com/chungnn/" id="cse-search-box">
  <div>
    <input type="hidden" name="cx" value="partner-pub-4423648035171915:t57cg8-ocq2" />
    <input type="hidden" name="cof" value="FORID:10" />
    <input type="hidden" name="ie" value="UTF-8" />
    <input type="text" name="q" size="31" />
    <input type="submit" name="sa" value="Google" />
  </div>
</form>
<script type="text/javascript" src="http://www.google.com/coop/cse/brand?form=cse-search-box&amp;lang=vi"></script>
     

<div id="cse-search-results"></div>
<script type="text/javascript">
  var googleSearchIframeName = "cse-search-results";
  var googleSearchFormName = "cse-search-box";
  var googleSearchFrameWidth = 800;
  var googleSearchDomain = "www.google.com.vn";
  var googleSearchPath = "/cse";
</script>
<script type="text/javascript" src="http://www.google.com/afsonline/show_afs_search.js"></script>

     
          </font></p></td>
      </tr>
    </table></td>
  </tr>
</table>
<!--#Include file="Inc/Bottom.asp"-->
</td></tr>
<table>
</center>
</body>
</html>



