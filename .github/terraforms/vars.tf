variable "sql_username" {
  type      = string
  sensitive = true
}

variable "sql_password" {
  type      = string
  sensitive = true
}

variable "api_key" {
  type      = string
  sensitive = true
}

locals {
  location = "North Europe"
}

locals {
  connection_string = "Server=tcp:${local.sql_server_name}.database.windows.net,1433;Initial Catalog=${local.sql_db_name};Persist Security Info=False;User ID=${var.sql_username};Password=${var.sql_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

locals {
  yourtutor           = "yourtutor"
  resource_group_name = "${local.yourtutor}rg"
  sql_server_name     = "${local.yourtutor}sqlserver"
  sql_db_name         = "${local.yourtutor}sqldb"
  app_service_name    = "${local.yourtutor}appservice"
  web_app_name        = "${local.yourtutor}webapp"
}