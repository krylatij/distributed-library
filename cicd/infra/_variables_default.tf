variable "project_name" {
  type    = string
  default = "distrib-library"
}

variable "location" {
  type    = string
  default = "eastus"  
}

variable "paid_arm_client_id" {
  type    = string 
  default = null
}

variable "paid_arm_client_secret" {
  type    = string
  default = null
}

variable "paid_arm_tenant_id" {
  type    = string
  default = null
}

variable "paid_arm_subscription_id" {
  type    = string
  default = null
}