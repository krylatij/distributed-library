resource "azurerm_storage_account" "this" {
  name                     = local.sa_name
  resource_group_name      = azurerm_resource_group.this.name
  location                 = azurerm_resource_group.this.location
  account_tier             = "Standard"  
  account_replication_type = "LRS"
  access_tier              = "Cool"

  tags = local.default_tags
}

resource "azurerm_storage_container" "tfstate" {
  name                  = local.sa_tfstate_container_name
  storage_account_name  = azurerm_storage_account.this.name
  container_access_type = "private"
}