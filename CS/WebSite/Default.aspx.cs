using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page
{
	enum DataSourceType {
		Products,
		Categories,
		Shippers
	}

	protected void Page_Init(object sender, EventArgs e) {
		if (!this.IsPostBack)
			Session.Clear();

		grid.DataBind();
	}
	protected void grid_DataBinding(object sender, EventArgs e) {
		(sender as ASPxGridView).DataSource = GetDataSource();
		AddColumns();
	}
	protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
		Session["selectedDataSource"] = Int32.Parse(e.Parameters);

		grid.Columns.Clear();
		grid.AutoGenerateColumns = false;
		grid.DataBind();
	}
	private SqlDataSource GetDataSource() {
		object o = Session["selectedDataSource"];
		DataSourceType dsType = DataSourceType.Products;
		if (o != null)
			dsType = (DataSourceType)o;

		switch (dsType) {
			case DataSourceType.Categories:
				return dsCategories;
			case DataSourceType.Shippers:
				return dsShippers;
			default:
				return dsProducts;
		}
	}
	private void AddColumns() {
		grid.Columns.Clear();
		DataView dw = (DataView)GetDataSource().Select(DataSourceSelectArguments.Empty);
		foreach (DataColumn c in dw.Table.Columns)
			AddTextColumn(c.ColumnName);

		grid.KeyFieldName = dw.Table.Columns[0].ColumnName;
		grid.Columns[0].Visible = false;
		AddCommandColumn();
	}

	private void AddTextColumn(string fieldName) {
		GridViewDataTextColumn c = new GridViewDataTextColumn();
		c.FieldName = fieldName;
		grid.Columns.Add(c);
	}

	private void AddCommandColumn() {
		SqlDataSource ds = (SqlDataSource)grid.DataSource;
		bool showColumn = !(String.IsNullOrEmpty(ds.UpdateCommand) && String.IsNullOrEmpty(ds.InsertCommand) &&
			String.IsNullOrEmpty(ds.DeleteCommand));

		if (showColumn) {
			GridViewCommandColumn c = new GridViewCommandColumn();
			grid.Columns.Add(c);
			c.ShowEditButton = !String.IsNullOrEmpty(ds.UpdateCommand);
			c.ShowNewButton = !String.IsNullOrEmpty(ds.InsertCommand);
			c.ShowDeleteButton = !String.IsNullOrEmpty(ds.DeleteCommand);
			c.ShowCancelButton = true;
			c.ShowUpdateButton = true;
		}
	}
	protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) {
		throw new NotImplementedException("Operation is not allowed in demonstration mode");
	}
	protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) {
		throw new NotImplementedException("Operation is not allowed in demonstration mode");
	}
	protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
		throw new NotImplementedException("Operation is not allowed in demonstration mode");
	}
}
