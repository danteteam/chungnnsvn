<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonInfo.ascx.cs" Inherits="UControl_CommonInfo" %>
<table bgcolor="#ffffcc" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid;
    border-bottom: 1px solid" width="100%">
    <tr>
        <td style="text-align: center" colspan="3">
            <asp:Label ID="Label1" runat="server" Text="Số người online: "></asp:Label>
            <asp:Label ID="lblOnlineNumbers" runat="server" Font-Bold="True" ForeColor="Red"
                Text="0000"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="text-align: center">
            <asp:Label ID="Label4" runat="server" Text="Lượt truy cập: "></asp:Label><asp:Label ID="lblVisitedNumbers" runat="server" Font-Bold="True" Font-Size="Medium"
                ForeColor="#0000C0" Text="0000"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="text-align: center">
            <asp:Label ID="lblFromDate" runat="server" Text="From date"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="text-align: center">
            <asp:Label ID="lblChart" runat="server" Text="lblChart"></asp:Label></td>
    </tr>
    <tr>
        <td width="68%">
        </td>
        <td width="2%">
        </td>
        <td width="30%">
        </td>
    </tr>
</table>
