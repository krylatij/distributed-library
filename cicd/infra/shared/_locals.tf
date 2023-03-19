locals {
  project_subname = "shared"
  project_name_nospec_symbols = replace(var.project_name, "-", "")

  rg_name  = "rg-${var.project_name}-${local.project_subname}"
  acr_name = "acr${local.project_name_nospec_symbols}"
  sa_name  = "sa${local.project_name_nospec_symbols}"

  service_endpoint_name = "${var.project_name} Azure RM endpoint"

  var_group_name = "${local.project_subname}_variables"

  sa_tfstate_container_name = "tfstate"

  default_tags = {
    env = local.project_subname
  }
}
