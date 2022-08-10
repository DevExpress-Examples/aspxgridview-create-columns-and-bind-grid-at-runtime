Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private Enum DataSourceType
		Products
		Categories
		Shippers
	End Enum

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		If Not Me.IsPostBack Then
			Session.Clear()
		End If

		grid.DataBind()
	End Sub
	Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
		TryCast(sender, ASPxGridView).DataSource = GetDataSource()
		AddColumns()
	End Sub
	Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Session("selectedDataSource") = Int32.Parse(e.Parameters)

		grid.Columns.Clear()
		grid.AutoGenerateColumns = False
		grid.DataBind()
	End Sub
	Private Function GetDataSource() As SqlDataSource
		Dim o As Object = Session("selectedDataSource")
		Dim dsType As DataSourceType = DataSourceType.Products
		If o IsNot Nothing Then
			dsType = DirectCast(o, DataSourceType)
		End If

		Select Case dsType
			Case DataSourceType.Categories
				Return dsCategories
			Case DataSourceType.Shippers
				Return dsShippers
			Case Else
				Return dsProducts
		End Select
	End Function
	Private Sub AddColumns()
		grid.Columns.Clear()
		Dim dw As DataView = CType(GetDataSource()., DataView)[Select](DataSourceSelectArguments.Empty)
		For Each c As DataColumn In dw.Table.Columns
			AddTextColumn(c.ColumnName)
		Next c

		grid.KeyFieldName = dw.Table.Columns(0).ColumnName
		grid.Columns(0).Visible = False
		AddCommandColumn()
	End Sub

	Private Sub AddTextColumn(ByVal fieldName As String)
		Dim c As New GridViewDataTextColumn()
		c.FieldName = fieldName
		grid.Columns.Add(c)
	End Sub

	Private Sub AddCommandColumn()
		Dim ds As SqlDataSource = CType(grid.DataSource, SqlDataSource)
		Dim showColumn As Boolean = Not (String.IsNullOrEmpty(ds.UpdateCommand) AndAlso String.IsNullOrEmpty(ds.InsertCommand) AndAlso String.IsNullOrEmpty(ds.DeleteCommand))

		If showColumn Then
			Dim c As New GridViewCommandColumn()
			grid.Columns.Add(c)
			c.EditButton.Visible = Not String.IsNullOrEmpty(ds.UpdateCommand)
			c.NewButton.Visible = Not String.IsNullOrEmpty(ds.InsertCommand)
			c.DeleteButton.Visible = Not String.IsNullOrEmpty(ds.DeleteCommand)
			c.CancelButton.Visible = True
			c.UpdateButton.Visible = True
		End If
	End Sub
	Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
		Throw New NotImplementedException("Operation is not allowed in demonstration mode")
	End Sub
	Protected Sub grid_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		Throw New NotImplementedException("Operation is not allowed in demonstration mode")
	End Sub
	Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
		Throw New NotImplementedException("Operation is not allowed in demonstration mode")
	End Sub
End Class
