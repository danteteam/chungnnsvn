<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="Label3" runat="server" Text="Input"></asp:Label>
        <br />
                    
        <asp:Label ID="Label1" runat="server" Text="Input"></asp:Label>
&nbsp;&nbsp;
        <asp:TextBox ID="txtInput" runat="server" Width="452px" Font-Bold="True" 
            Font-Size="Large" Height="66px" TextMode="MultiLine"></asp:TextBox>
&nbsp;<asp:Button ID="btnTest" runat="server" onclick="Button1_Click" Text="Click me" 
            Width="84px" />
        <br />
        <asp:Label ID="Label2" runat="server" Text="output"></asp:Label>
        &nbsp;<asp:TextBox ID="txtOutput" runat="server" Width="453px"></asp:TextBox>
        <br />
        <br />
                    
    </div>
    </form>
</body>
</html>
