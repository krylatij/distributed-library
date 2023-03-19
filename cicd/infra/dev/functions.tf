resource "azurerm_service_plan" "functions" {
  for_each = local.functions

  name                = "asp-${local.project}-func-${each.value}"
  resource_group_name = azurerm_resource_group.this.name
  location            = azurerm_resource_group.this.location
  os_type             = "Linux"

  sku_name = "Y1"
}


resource "azurerm_storage_account" "functions" {
  for_each = local.functions

  name                     = "sa${local.project_name_nospec_symbols}${each.value}"
  resource_group_name      = azurerm_resource_group.this.name
  location                 = azurerm_resource_group.this.location
  account_tier             = "Standard"  
  account_replication_type = "LRS"
  access_tier              = "Hot"

  tags = local.default_tags
}

resource "azurerm_linux_function_app" "this" {
  for_each = local.functions

  name                = "func-${local.project}-${each.value}"
  resource_group_name = azurerm_resource_group.this.name
  location            = azurerm_service_plan.functions[each.value].location
  service_plan_id     = azurerm_service_plan.functions[each.value].id

  storage_account_name       = azurerm_storage_account.functions[each.value].name
  storage_account_access_key = azurerm_storage_account.functions[each.value].primary_access_key

  site_config {
    always_on = false
    application_stack {
        dotnet_version = "6.0" 
        #docker_image     
    }
  }

   app_settings = local.func_environment_vars

   lifecycle {
    ignore_changes = [
      app_settings
    ]
  }
    
  tags = local.default_tags
}

