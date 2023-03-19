module "shared"{
    source = "./shared"

    project_name = var.project_name
    location     = var.location

    service_principal_id     = var.service_principal_id
    service_principal_secret = var.service_principal_secret
}