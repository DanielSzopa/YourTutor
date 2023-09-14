resource "azurerm_mssql_database" "sql_db" {
  name                 = var.sql_db_name
  server_id            = var.sql_server_id
  sku_name             = "Basic"
  storage_account_type = "Local"
}