resource "azurerm_mssql_database" "sql_db" {
  name                 = var.sql_db_name
  server_id            = var.sql_server_id
  max_size_gb          = 40
  sku_name             = "S0"
  storage_account_type = "Local"
}