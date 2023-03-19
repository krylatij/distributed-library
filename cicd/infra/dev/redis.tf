# NOTE: the Name used for Redis needs to be globally unique
/* resource "azurerm_redis_cache" "example" {
  name                = var.redis_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  capacity            = 0
  family              = "C"
  sku_name            = "Basic"
  enable_non_ssl_port = false
  minimum_tls_version = "1.2"

   redis_configuration {
  }

  tags = local.default_tags

 
} */