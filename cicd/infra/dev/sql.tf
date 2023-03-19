resource "azurerm_mssql_server" "this" {
  count = local.mssql_enabled ? 1 : 0

  name                = local.mssql_server_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name

  version                      = "12.0"
  administrator_login          = var.mssql_admin_login
  administrator_login_password = random_password.mssql_server.result
}

resource "random_password" "mssql_server" {
  length  = 20
  special = true
}

resource "azurerm_mssql_database" "this" {
  for_each = local.mssql_dbs

  name           = "sql-${local.project}-${each.value}" 
  server_id      = azurerm_mssql_server.this[0].id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  max_size_gb    = 1
  read_scale     = false
  sku_name       = "Basic"
  zone_redundant = false

  tags = local.default_tags
}

resource "azurerm_mssql_firewall_rule" "this" {
  count = local.mssql_enabled ? 1 : 0

  name             = "SQL Server allow all"
  server_id        = azurerm_mssql_server.this[0].id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}