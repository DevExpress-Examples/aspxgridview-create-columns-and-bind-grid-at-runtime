<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>How to bind ASPxGridView with manually created columns to different data sources
		at runtime. The grid is created in design mode.</title>
</head>
<body>
	<form id="form1" runat="server">
		<table>
			<tr>
				<td valign="top">
					<dx:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" OnCustomCallback="grid_CustomCallback"
						OnDataBinding="grid_DataBinding" EnableViewState="false" AutoGenerateColumns="False" OnRowDeleting="grid_RowDeleting" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
					</dx:ASPxGridView>
				</td>
				<td valign="top">
					<dx:ASPxRadioButtonList ID="rblDatasources" runat="server">
						<ClientSideEvents SelectedIndexChanged="function(s, e) { grid.PerformCallback(s.GetSelectedIndex()); }" />
						<Items>
							<dx:ListEditItem Selected="True" Text="Products" />
							<dx:ListEditItem Text="Categories" />
							<dx:ListEditItem Text="Shippers" />
						</Items>
					</dx:ASPxRadioButtonList>
				</td>
			</tr>
		</table>        
		<asp:SqlDataSource ID="dsProducts" runat="server" ConnectionString="Data Source=(local);Initial Catalog=Northwind;Integrated Security=True"
			ProviderName="System.Data.SqlClient" DeleteCommand="DELETE FROM [Products] WHERE [ProductID] = @ProductID"
			InsertCommand="INSERT INTO [Products] ([ProductName], [QuantityPerUnit], [UnitPrice]) VALUES (@ProductName, @QuantityPerUnit, @UnitPrice)"
			SelectCommand="SELECT [ProductID], [ProductName], [QuantityPerUnit], [UnitPrice] FROM [Products]"
			UpdateCommand="UPDATE [Products] SET [ProductName] = @ProductName, [QuantityPerUnit] = @QuantityPerUnit, [UnitPrice] = @UnitPrice WHERE [ProductID] = @ProductID">
			<DeleteParameters>
				<asp:Parameter Name="ProductID" Type="Int32" />
			</DeleteParameters>
			<InsertParameters>
				<asp:Parameter Name="ProductName" Type="String" />
				<asp:Parameter Name="QuantityPerUnit" Type="String" />
				<asp:Parameter Name="UnitPrice" Type="Decimal" />
			</InsertParameters>
			<UpdateParameters>
				<asp:Parameter Name="ProductName" Type="String" />
				<asp:Parameter Name="QuantityPerUnit" Type="String" />
				<asp:Parameter Name="UnitPrice" Type="Decimal" />
				<asp:Parameter Name="ProductID" Type="Int32" />
			</UpdateParameters>
		</asp:SqlDataSource>
		<asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString="Data Source=(local);Initial Catalog=Northwind;Integrated Security=True"
			ProviderName="System.Data.SqlClient" DeleteCommand="DELETE FROM [Categories] WHERE [CategoryID] = @CategoryID"
			InsertCommand="INSERT INTO [Categories] ([CategoryName], [Description]) VALUES (@CategoryName, @Description)"
			SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]"
			UpdateCommand="UPDATE [Categories] SET [CategoryName] = @CategoryName, [Description] = @Description WHERE [CategoryID] = @CategoryID">
			<DeleteParameters>
				<asp:Parameter Name="CategoryID" Type="Int32" />
			</DeleteParameters>
			<InsertParameters>
				<asp:Parameter Name="CategoryName" Type="String" />
				<asp:Parameter Name="Description" Type="String" />
			</InsertParameters>
			<UpdateParameters>
				<asp:Parameter Name="CategoryName" Type="String" />
				<asp:Parameter Name="Description" Type="String" />
				<asp:Parameter Name="CategoryID" Type="Int32" />
			</UpdateParameters>
		</asp:SqlDataSource>
		<asp:SqlDataSource ID="dsShippers" runat="server" ConnectionString="Data Source=(local);Initial Catalog=Northwind;Integrated Security=True"
			ProviderName="System.Data.SqlClient" SelectCommand="SELECT [ShipperID], [CompanyName], [Phone] FROM [Shippers]">
		</asp:SqlDataSource>
	</form>
</body>
</html>
