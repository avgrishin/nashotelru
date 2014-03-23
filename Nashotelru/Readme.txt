Enable-Migrations -ContextTypeName Nashotelru.Models.NashotelDBContext -force
Enable-Migrations -ContextTypeName Nashotelru.Models.ApplicationDbContext -force
Add-Migration Update1
update-database -verbose