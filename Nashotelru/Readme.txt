Enable-Migrations -ContextTypeName Nashotelru.Models.NashotelDBContext -force
Enable-Migrations -ContextTypeName Nashotelru.Models.ApplicationDbContext -force
update-database -verbose