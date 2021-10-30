<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="ContactUs.aspx.vb" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
<table>

<tr>
<td><b>Name:</b></td>
<td><asp:TextBox ID="txtName" runat="server" Width="187px"></asp:TextBox></td>
</tr>
<tr>
<td><b>email:</b></td>
<td><asp:TextBox ID="txtEmail" runat="server" Width="258px"></asp:TextBox></td>
</tr>
<tr>
<td><b>Subject:</b></td>
<td><asp:TextBox ID="txtSubject" runat="server" Width="396px"></asp:TextBox></td>
</tr>
<tr>
<td valign="top"><b>Message:</b></td>
<td><asp:TextBox ID="txtMessage" runat="server" Rows="10" TextMode="MultiLine" 
        Width="628px"></asp:TextBox></td>
</tr>
<tr>
<td>
<br />
<br />
</td>
</tr>
<tr>
<td colspan="2" align="center">
    <asp:Button ID="btnSend" runat="server" Text="Send" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </td>
</tr>
<td colspan="2" align="center">
    <asp:Label ID="lblResult" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label></td>
</table>
</asp:Content>

