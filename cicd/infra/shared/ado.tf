resource "azuredevops_project" "this" {
  name               = var.project_name
  work_item_template = "Agile"
  version_control    = "Git"
  visibility         = "private"
  description        = "Managed by Terraform"
}

resource "azuredevops_serviceendpoint_azurerm" "this" {
  project_id            = azuredevops_project.this.id
  service_endpoint_name = local.service_endpoint_name
  description           = "Managed by Terraform"
  credentials {
    serviceprincipalid  = var.service_principal_id
    serviceprincipalkey = var.service_principal_secret
  }
  azurerm_spn_tenantid      = data.azurerm_subscription.this.tenant_id
  azurerm_subscription_id   = data.azurerm_subscription.this.subscription_id
  azurerm_subscription_name = data.azurerm_subscription.this.display_name
}

resource "azuredevops_variable_group" "this" {
  project_id   = azuredevops_project.this.id
  name         = local.var_group_name
  description  = "Variables shared accross the whole project"
  allow_access = true

  variable {
    name  = "__service_endpoint_name"
    value = azuredevops_serviceendpoint_azurerm.this.service_endpoint_name   
  }
}