variable "env" {
  type        = string
  default     = "dev"  
}

variable "psql_admin_login" {
  type        = string
  default     = "psqladmin" 
}

variable "mssql_admin_login" {
  type        = string
  default     = "mssqladmin" 
}