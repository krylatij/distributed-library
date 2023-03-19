resource "azurerm_resource_group" "this" {
  location = var.location
  name     = local.rg_name
}
