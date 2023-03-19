resource "azurerm_resource_group" "this" {
  location = var.location
  name     = local.rg_name
}

resource "azurerm_resource_group" "paid" {
  provider = azurerm.paid

  location = var.location
  name     = local.rg_name_paid
}