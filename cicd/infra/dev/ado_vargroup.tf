resource "azuredevops_variable_group" "this" {
  project_id   = var.ado_project_id
  name         = local.var_group_name
  description  = "Variables for ${var.env} environment"
  allow_access = true

  variable {
    name  = "__dotnet_environment"
    value = local.app_environment_vars.DOTNET_ENVIRONMENT   
  }

  variable {
    name  = "__appinsights_connection_string"
    secret_value = azurerm_application_insights.this.connection_string 
    is_secret    = true
  }

  dynamic "variable" {
    for_each = azurerm_linux_web_app.this
    iterator = app
    content {        
        name  = "__webapp_${app.key}"
        value = app.value.name        
    }
  }

  dynamic "variable" {
    for_each = azurerm_linux_web_app.this
    iterator = app
    content {        
        name  = "__webapp_${app.key}_hostname"
        value = "https://${app.value.default_hostname}"
    }
  }

  dynamic "variable" {
    for_each = azurerm_postgresql_flexible_server_database.this
    iterator = db
    content {      
        name  = "__pssql_${db.key}_connection_string"
        secret_value = "Host=${azurerm_postgresql_flexible_server.this.fqdn};Port=5432;Database=${db.value.name};Username=${var.psql_admin_login};Password=${random_password.postgres_server.result}"
        is_secret = true
    }
  } 

  dynamic "variable" {
    for_each = azurerm_mssql_server.this
    iterator = server
    content {      
        name  = "__mssql_${server.key}_server_name"
        value = azurerm_mssql_server.this[server.key].fully_qualified_domain_name
        is_secret = false
    }
  } 

  dynamic "variable" {
    for_each = azurerm_mssql_server.this
    iterator = server
    content {        
        name  = "__mssql_${server.key}_admin_username"
        secret_value = "${var.mssql_admin_login}"
        is_secret = true
    }
  } 

  dynamic "variable" {
    for_each = azurerm_mssql_server.this
    iterator = server
    content {        
        name  = "__mssql_${server.key}_admin_password"
        secret_value = random_password.mssql_server.result
        is_secret = true
    }
  } 

  dynamic "variable" {
    for_each = azurerm_mssql_database.this
    iterator = db
    content {        
        name  = "__mssql_${db.key}_connection_string"
        secret_value = "Server=${azurerm_mssql_server.this[0].fully_qualified_domain_name},1433;Database=${db.value.name};User ID=${azurerm_mssql_server.this[0].administrator_login};Password=${random_password.mssql_server.result};Connection Timeout=30"
        is_secret = true
    }
  } 

  dynamic "variable" {
    for_each = azurerm_mssql_database.this
    iterator = db
    content {        
        name  = "__mssql_${db.key}_database_name"
        value = db.value.name
        is_secret = false
    }
  } 

  dynamic "variable" {
    for_each = azurerm_communication_service.this
    iterator = acs
    content {        
        name  = "__communication_service_${acs.key}_connection_string"
        secret_value = acs.value.primary_connection_string
        is_secret = true
    }
  } 
}