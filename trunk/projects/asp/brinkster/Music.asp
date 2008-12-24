<%@ Language=VBScript %>
<!--#Include file="Inc/Library.asp" -->
<%

   Dim Rs
   set  Rs = Server.CreateObject("ADODB.Recordset")
   Rs.Open "select * from Songs", Cnn,adOpenStatic    
   nCount = 0
   while not Rs.EOF
       nCount = nCount + 1
       Rs.MoveNext
   wend
    
    Redim arrUrl(nCount)
    Redim arrSongName(nCount)
    Redim arrSongWord(nCount)
    Redim arrImageFileName(nCount)
    
    rs.Close
    Rs.Open "select * from Songs", Cnn,adOpenStatic
    nCount = 0
    strSongImagePath = "Images/Songs/"
    'Response.Write(Rs.EOF)
    while not Rs.EOF
       nCount = nCount + 1
       arrSongName(nCount) = Rs("SongName")
       arrUrl(nCount) = Rs("SongUrl")
       arrSongWord(nCount) = Rs("SongWord")
       arrImageFileName(nCount) = strSongImagePath & Rs("ImageFileName")
       Rs.MoveNext
    wend
    rs.Close
    set Rs = nothing
    
    '------- Request song info --------
    strSongID = Request("SongID")
    if(strSongID="") then     
        Dim rndNum    
        Randomize
        rndNum = Round(Rnd(1) * (nCount))
        strSongID = rndNum
    end if
    
    '------- Get Url & song name to play -----------
    strPlayUrl = arrUrl(strSongID)
    strPlaySongName = arrSongName(strSongID)
    strImageFileName = arrImageFileName(strSongID)
    strPlaySongWord = arrSongWord(strSongID)
    
    'Response.Write(strPlayUrl)    
    
    
%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>NNC Club</title>
</head>
<body>
<center>
<form name="frmMedia" id="frmMedia">
<table bgcolor="#006600" width="800"  border="0">
<tr><td>
<% reqPage = "Music" %>
<!--#Include file="Inc/Top.asp"-->
<table width="800"  border="0" height="335"  bordercolor="#907319">
  <tr>
    <td valign="top" height="331">
    <table width="800"  border="1" bordercolor="#907319" height="330">
      <tr>
        <td valign="top" height="40" align=left><h3 class="style1">Music</h3></td>
      </tr>
      <tr>
        <td valign="top" height="278">
          <p class="style2" align="center"><font size="6">
          	<script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
		    </script>
		    <script type="text/javascript">
		    _uacct = "UA-914137-2";
		    urchinTracker();
		    </script>
		    
		    <table width="100%" height="131">
		        <tr>
		        <td align=left height="41">
		            <table width="100%" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor=DarkGray>                
						<tr>
						    <td align=center style="height: 19px">
						        <span style="color: lightcyan"><font size='3'>
								Ord</font></span></td>
						    <td align=left style="height: 19px">
						        <span style="color: lightcyan"><font size='3'>
								Song Name</font></span></td>
		      
		  				    <td align=center style="height: 19px">
						        <span style="color: lightcyan"><font size='3'>
								Ord</font></span></td>
						    <td align=left style="height: 19px">
						        <span style="color: lightcyan"><font size='3'>
								Song Name</font></span></td>
		
						</tr>
		                <% 
		                SongCount = 0
		                
		                for i = 1 to UBound(arrUrl)
		                    if(arrUrl(i)<>"") then  
		                        SongCount = SongCount + 1                      
		                     	 	
							if((i+1 mod 2) = 0) then 
							    Response.Write("<tr height=""25"">") 
		                    end if  	              
		                %>		               		
		                            <td width="5%" align=center><font size='3' color="#FFFFFF"><%=i%></font>&nbsp;</td>
		                            <td width="42%" align=left>
		                                <a href="javascript:fnPlaySong('<%=i%>');"> <font color="#FFFFFF" size='3'><%=arrSongName(i)%></font></a>
		                                <input type="hidden" name="txtSongUrl<%=i%>" id="txtSongUrl<%=i%>" value="<%=arrUrl(i)%>" />
		                                <input type="hidden" name="txtImageFileName<%=i%>" id="txtImageFileName" value="<%=arrImageFileName(i)%>" />
		                                <input type="hidden" name="txtSongWord<%=i%>" id="txtSongWord<%=i%>" value="<%=arrSongWord(i)%>" />
		                                <input type="hidden" name="txtSongName<%=i%>" id="txtSongName" value="<%=arrSongName(i)%>" />
		                            </td>                        
		                 <%           
		                       	if((i mod 2) = 0) then 
		                		  Response.Write("</tr>")
		                		end if
		                    end if  
		                next
		                %>
		                
		            </table>
		        </td>
		    </tr>
		   
		    <tr>
		        <td height="1">
		        </td>
		    </tr>
		    <tr>
		        <td height="1">
		        </td>
		    </tr>
		    
		</table>
		    <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="253">
		  
		  <tr>
		    <td width="100%" height="75">
		    <p align="center">
		    &nbsp;<table border="1" width="100%" id="table1">
				<tr>
					<td width="40%" align="center" valign="top"><br />
					    <input type="hidden" id="txtPlayingSongID" name="txtPlayingSongID"  value="<%=strSongID%>"/>
			     <!--            <EMBED id="MadiaObject1" name="MadiaObject1" pluginspage="http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/" 
                                src="<%=strPlayUrl%>" type="application/x-mplayer2" 
                                  ShowStatusBar="1" AutoStart="1" Loop="1" ShowControls="1" width="300" height="68">
                            </embed>
				         <br />
                -->
               
                
					    <object classid="clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6" id="'1" name="MadiaObject" width="315" height="310">
					    <param name='URL' ref value="<%=strPlayUrl%>">
					    <param name='rate' value='1'>
					    <param name='balance' value='0'>
					    <param name='currentPosition'>
					    <param name='defaultFrame' value='20'>
					    <param name='playCount' value='1'>
					    <param name='autoStart' value='-1'>
					    <param name='currentMarker' value='0'>
					    <param name='invokeURLs' value='-1'>
					    <param name='baseURL' value>
					    <param name='volume' value='100'>
					    <param name='mute' value='0'>
					    <param name='uiMode' value='full'>
					    <param name='stretchToFit' value='0'>
					    <param name='windowlessVideo' value='0'>
					    <param name='enabled' value='-1'>
					    <param name='enableContextMenu' value='-1'>
					    <param name='fullScreen' value='0'>
					    <param name='SAMIStyle' value>
					    <param name='SAMILang' value>
					    <param name='SAMIFilename' value>
					    <param name='captioningID' value>
					    <param name='enableErrorDialogs' value='0'>
					    <param name='_cx' value='9604'>
					    <param name='_cy' value='1720'>
					   </object><br />
					  
					    <br />
		                <a id="lnkDownload" target="_blank" href="<%=strPlayUrl%>"><b>
		                <span style="font-size: 14pt"><font color="silver">Click 
					here to download this song</font></span></b></a>
		    		</td>
					<td width="2%">&nbsp;</td>
					<td  width="58%">
					    <table width="100%" border="0"  bordercolor="#907319">
					        <tr>
					            <td width="2%" ></td>
					            <td width="98%" align="left" valign="top">
					                <div id="divSongName" style="height:auto; width:auto; background-color:#006600; color:White; font-size:15pt">
					                    <%=strPlaySongName%>
					                </div>	
					                <br />
					                <div id="divImageFileName">
					                    <img src="<%=strImageFileName%>" />
					                </div>	
					                					                
					                <br />
					                <div id="divSongWord" style="height:auto; width:auto; background-color:#006600; color:White; font-size:12pt">
					                   <%=strPlaySongWord%>
					                </div>			               
					            </td>			            
					            <!--<textarea cols="50" rows="10" id="txtSongWord" name="txtSongWord"><%=strPlaySongWord%></textarea>-->
					        </tr>
					    </table>
					</td>
				</tr>
			</table>
		    </td>
		  </tr>
		  <tr>
		    <td width="100%" height="59">&nbsp;</td>
		  </tr>
		</table>
		 
          </font></p></td>
      </tr>
    </table></td>
  </tr>
</table>
<!--#Include file="Inc/Bottom.asp"-->
</td></tr>
<table>
</form>
</center>
</body>
</html>
<script>  
    //alert("<%=strSongID%>");  
    //alert("<%=SongCount%>");
    
    window.setInterval("AutoPlay()", 1000);
    
    function fnPlaySong(strSongID)
    {   
       
        eval("var strPlayUrl=document.all.txtSongUrl" + strSongID +".value;"); 
        eval("var strPlayUrl=document.all.txtSongUrl" + strSongID +".value;"); 
        //alert(frmMedia.txtSongUrl1.value);      
        eval("var strSongWord=document.all.txtSongWord" + strSongID +".value;");
        eval("var strSongName=document.all.txtSongName" + strSongID +".value;");
        eval("var strImageFileName=document.all.txtImageFileName" + strSongID +".value;");
        
        document.all.txtPlayingSongID.value=strSongID;       
        document.all.lnkDownload.href=strPlayUrl;  
          
        document.all.MadiaObject.URL = strPlayUrl;
        //document.all.MadiaObject1.AutoStart.value = "0";
        //document.all.MadiaObject1.src =strPlayUrl;
        document.all.divSongName.innerHTML= strSongName;        
        document.all.divImageFileName.innerHTML= "<img src='" + strImageFileName + "'>";        
        document.all.divSongWord.innerHTML= strSongWord;
        document.title = strSongName + " :: NNC Club ";      
    }
        
    function AutoPlay()
    {
    
        var nSongCount = parseInt("<%=SongCount%>");
        var nPlaySongID = parseInt(document.all.txtPlayingSongID.value);
        if(document.all.MadiaObject.playState=="1")    //1: stoped; 3:playing
        {
            //alert("current=" + nPlaySongID + ",Count=" + nSongCount);
            if(nPlaySongID<nSongCount)
                nPlaySongID = nPlaySongID + 1;
            else
                nPlaySongID = 1;
              
            //alert("next=" + nPlaySongID);
             
            eval("var strPlayUrl=document.all.txtSongUrl" + nPlaySongID +".value;");
            eval("var strSongWord=document.all.txtSongWord" + nPlaySongID +".value;");
            eval("var strSongName=document.all.txtSongName" + nPlaySongID +".value;"); 
            eval("var strImageFileName=document.all.txtImageFileName" + nPlaySongID +".value;");
            
            document.all.txtPlayingSongID.value=nPlaySongID;
            document.all.lnkDownload.href= strPlayUrl;
            document.all.MadiaObject.URL= strPlayUrl;  
            //document.all.MadiaObject1.src=strPlayUrl;
            document.all.divSongName.innerHTML= strSongName; 
            document.all.divImageFileName.innerHTML= "<img src='" + strImageFileName + "'>";
            document.all.divSongWord.innerHTML= strSongWord; 
            document.title = strSongName + " :: NNC Club ";        
                      
        }   
             
    }
        
</script>



