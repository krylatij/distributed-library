output "rg_name" {
  value = azurerm_resource_group.this.name
}

output "acr_name" {
  value = azurerm_container_registry.this.name
}

output "sa_name" {
  value = azurerm_storage_account.this.name
}

output "sa_access_key" {
  value     = azurerm_storage_account.this.primary_access_key
  sensitive = true
}

output "ado_project_id" {
  value = azuredevops_project.this.id
}

output "ado_project_name" {
  value = azuredevops_project.this.name
}