output "rg_name" {
  value = module.shared.rg_name
}

output "acr_name" {
  value = module.shared.acr_name
}

output "sa_name" {
  value = module.shared.sa_name
}

output "sa_access_key" {
  value     = nonsensitive(module.shared.sa_access_key)
 # sensitive = true
}

output "ado_project_id" {
  value = module.shared.ado_project_id
}

output "ado_project_name" {
  value = module.shared.ado_project_name
}