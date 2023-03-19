resource "azurerm_postgresql_flexible_server" "this" {
  count = local.postgresql_enabled ? 1 : 0

  name                = local.postgresql_server_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name

  version                = "12"
  administrator_login    = var.psql_admin_login
  administrator_password = random_password.postgres_server.result
  storage_mb             = 32768
  sku_name               = "B_Standard_B1ms"

  lifecycle {
    ignore_changes = [
      # Ignore changes to tags, e.g. because a management agent
      # updates these based on some ruleset managed elsewhere.
      tags,
      zone
    ]
  }

  tags = local.default_tags
}

resource "random_password" "postgres_server" {
  length  = 20
  special = true
}

resource "azurerm_postgresql_flexible_server_database" "this" {
  for_each = local.postgres_dbs
  
  name      = "psql-${local.project}-${each.value}"  
  server_id = azurerm_postgresql_flexible_server.this[0].id

  collation = "en_US.utf8"
  charset   = "utf8"
}

resource "azurerm_postgresql_flexible_server_firewall_rule" "this" {
  count = local.postgresql_enabled ? 1 : 0

  name             = "allow_all"
  server_id        = azurerm_postgresql_flexible_server.this[0].id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}