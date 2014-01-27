Enable-Migrations -ContextTypeName Nashotelru.Areas.ru.Models.NashotelDBContext -force
Enable-Migrations -ContextTypeName Nashotelru.Models.ApplicationDbContext -force
update-database -verbose