module "dev" {
   source = "./dev"

   ado_project_id = module.shared.ado_project_id

   project_name = var.project_name

   location = var.location


   providers = {
     azurerm = azurerm
     azurerm.paid = azurerm.paid
  }
}