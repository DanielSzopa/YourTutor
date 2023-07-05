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

  required_version = "~>1.4.5"
}

provider "azurerm" {
  features {

  }
}
