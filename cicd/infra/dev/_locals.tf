locals {  
  project = "${var.env}-${var.project_name}"
  project_name_nospec_symbols = replace(substr(local.project, 0, 10), "-", "")

  rg_name  = "rg-${var.project_name}-${var.env}"
  rg_name_paid  = "rg-${var.project_name}-${var.env}-paid"

  redis_name = "redis${var.project_name}"

  var_group_name = "${var.env}_variables"
 
  default_tags = {
    env = var.env
  }
}

# Logging
locals {
  loganalytics_workspace_name = "log-${local.project}"
  appinsights_name = "appi-${local.project}"
}

# Web app
locals {
  service_plan_name = "asp-${local.project}"

  #"app-${local.project}-api"
  web_apps = toset([
    "ui"
  ])

  app_environment_vars = {
    ASPNETCORE_ENVIRONMENT = "Development",
    DOTNET_ENVIRONMENT = "Development"
  }
}

# Functions
locals {
  functions = toset([
    "notifications"
  ])

  func_environment_vars = {
    ASPNETCORE_ENVIRONMENT = "Development",
    DOTNET_ENVIRONMENT = "Development"
  }
}

# Postgres
locals { 
  postgresql_server_name = "psqls-${local.project}"

  postgres_dbs = toset([
    #"api"
  ])

  postgresql_enabled = length(local.postgres_dbs) > 0
}

# Ms SQL
locals {
  mssql_server_name = "sqls-${local.project}"

  mssql_dbs = toset([
    "backend"
  ])

  mssql_enabled = length(local.mssql_dbs) > 0
}
