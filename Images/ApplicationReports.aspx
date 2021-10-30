<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="ApplicationReports.aspx.vb" Inherits="ApplicationReports" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">

<table width="100%">
<tr>
<td><br /><br /></td>
</tr>
<tr>
<td>
<table>
<tr>
<td align="left" valign="middle" style="width: 139px"><asp:Image ID="imgRpt" runat="server" ImageUrl="~/Images/ReportsIcon.png" /></td>
<td align="left" valign="middle"><asp:Label ID="Label1" runat="server" 
        Text="Application Reports" Font-Size="X-Large"></asp:Label></td>
</tr>
</table>
</td>

</tr>
<tr>
<td colspan="2"><br /><br /></td>
</tr>
<tr>
<td valign="top">
<table width="100%">
         <tr><td>
    <asp:GridView id="gvApplicationReport" 
	DataSourceID="sqlApplicationReport" 
	AllowSorting="True" 
	AllowPaging="True" 
	Runat="Server"
	CellPadding="10"
  BorderStyle="solid"
  BorderColor="white"
  BorderWidth="5"
  BackColor="white"
  ForeColor="black"
  HeaderStyle-BackColor="#567F19"
  HeaderStyle-ForeColor="white"
  HeaderStyle-Font-Names="Verdana"
  HeaderStyle-Font-Size="7.5pt"
  RowStyle-Font-Names="Verdana"
  RowStyle-Font-Size="7.5pt"
  AlternatingRowStyle-BackColor="#EEEEEE"
  AlternatingRowStyle-ForeColor="black"
	AutoGenerateColumns="False" PageSize="25" width="100%">
	
    <Columns>
        <asp:HyperLinkField  ControlStyle-Width="20" DataNavigateUrlFields="URL"  Target="_blank"  Text="Run Report"/>
        <asp:BoundField ControlStyle-Width="150" HeaderText="Report Name" DataField="ReportName"></asp:BoundField>
        <asp:BoundField HeaderText="Report Description" DataField="ReportDesc"></asp:BoundField>
    </Columns>
        <HeaderStyle HorizontalAlign="Left" />
   </asp:GridView>
</td></tr></table>
</td>
</tr>
</table>

    



        <asp:SqlDataSource ID="sqlApplicationReport" runat="server" />
  

</asp:Content>

