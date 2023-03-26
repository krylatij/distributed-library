resource "azurerm_communication_service" "this" {
  count = local.communication_service_enabled ? 1 : 0

  name                = local.communication_service_name
  resource_group_name = local.rg_name
  data_location       = local.communication_service_location

  tags = local.default_tags
}