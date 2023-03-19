terraform {
  required_version = ">=0.12"

  required_providers {
   azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>3.34"
    }
    random = {
      source  = "hashicorp/random"
      version = "~>3.0"
    }
    azuredevops = {
      source = "microsoft/azuredevops"
      version = ">=0.1.0"
    }
  }

  #comment out for 1st run
  /* backend "azurerm" {
    resource_group_name  = "rg-shared"         #azurerm_resource_group.this.name
    storage_account_name = "sasharedkrylatij"  #azurerm_storage_account.this.name
    container_name       = "tfstate-shared"    #azurerm_storage_container.tfstate.name
    key                  = "terraform.tfstate"
  }  */

  backend "azurerm" {
    resource_group_name  = "rg-distrib-library-shared"         #azurerm_resource_group.this.name
    storage_account_name = "sadistriblibrary"  #azurerm_storage_account.this.name
    container_name       = "tfstate"    #azurerm_storage_container.tfstate.name
    key                  = "terraform.tfstate"
  } 
}

provider "azurerm" {
  features {}
}


provider "azurerm" {
  alias           = "paid"

  
  subscription_id = var.paid_arm_subscription_id
  tenant_id       = var.paid_arm_tenant_id
  client_id       = var.paid_arm_client_id
  client_secret   = var.paid_arm_client_secret
  features {}
}


provider "azuredevops" { 
}
