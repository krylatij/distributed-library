resource "azurerm_service_plan" "apps" {
  name                = local.service_plan_name
  resource_group_name = azurerm_resource_group.this.name
  location            = azurerm_resource_group.this.location
  os_type             = "Linux"
  sku_name            = "F1"  
}

resource "azurerm_linux_web_app" "this" {
  for_each = local.web_apps

  name                = "app-${local.project}-${each.value}"
  resource_group_name = azurerm_resource_group.this.name
  location            = azurerm_service_plan.apps.location
  service_plan_id     = azurerm_service_plan.apps.id

  site_config {
    always_on = false
    application_stack {
        dotnet_version = "6.0" 
        #docker_image     
    }
  }

  app_settings = local.app_environment_vars
  
  tags = local.default_tags
}