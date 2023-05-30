terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.53.0"
    }
  }

  backend "azurerm" {
    resource_group_name  = "YourTutorState"
    storage_account_name = "yourtutorstateaccount"
    container_name       = "state"
    key                  = "yourtutor.tfstate"
  }

  required_version = "1.4.5"
}

provider "azurerm" {
  features {

  }
}

resource "azurerm_resource_group" "rg" {
  name     = local.resource_group_name
  location = local.location
}

resource "azurerm_mssql_server" "sql_server" {
  name                         = local.sql_server_name
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = var.sql_username
  administrator_login_password = var.sql_password
}

resource "azurerm_mssql_firewall_rule" "firewall" {
  name             = "AllowAccessToAzureServices"
  server_id        = azurerm_mssql_server.sql_server.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "sql_db" {
  name                 = local.sql_db_name
  server_id            = azurerm_mssql_server.sql_server.id
  max_size_gb          = 40
  sku_name             = "S0"
  storage_account_type = "Local"
}

resource "azurerm_mssql_database" "sql_db_test" {
  name                 = local.sql_db_test_name
  server_id            = azurerm_mssql_server.sql_server.id
  max_size_gb          = 40
  sku_name             = "S0"
  storage_account_type = "Local"
}

resource "azurerm_app_service_plan" "service_plan" {
  name                = local.app_service_name
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  kind                = "Windows"
  sku {
    tier = "Shared"
    size = "D1"
  }
}

resource "azurerm_windows_web_app" "webapp" {
  name                = local.web_app_name
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  service_plan_id     = azurerm_app_service_plan.service_plan.id

  site_config {
    always_on = false

    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v7.0"
    }
  }

  app_settings = {
    "ConnectionStrings:DefaultConnectionString" = local.connection_string
    "SendGrid:ApiKey"                           = var.api_key
  }

}

