<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="test.aspx.vb" Inherits="test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
    <table>
<tr>
<td> <asp:TextBox ID="TextBox1" runat="server" 
        Text="Server=gamblersanon.db.6715321.hostedresource.com;uid=gamblersanon;pwd=S3rv!c3;database=gamblersanon" 
        Width="718px"></asp:TextBox>
</td>
<td>
    <asp:Button ID="btnTest1" runat="server" Text="Test" />
</td>
</tr>
<tr>
<td>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
</tr>
<tr>
<td><br /><br />
</td>
</tr>
<tr>
<td> <asp:TextBox ID="TextBox2" runat="server" 
        Text="Server=ViperBaseball.db.10225534.hostedresource.com;uid=ViperBaseball;pwd=Vipers@4206;database=ViperBaseball" 
        Width="718px"></asp:TextBox>
</td>
<td>
    <asp:Button ID="ButtbtnTest2on1" runat="server" Text="Test" />
</td>
</tr>
<tr>
<td>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</td>
</tr>
<tr>
<td>

    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
</tr>
</table>
   

</asp:Content>

