# MvcMovie
Se realiza la tarea de la semana 1. Se cambió el Home y se agregó una imagen centrada.

Scaffold-DbContext "Server=DESKTOP-FGAVFI9; DataBase=Movies; Trusted_Connection=True; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Data

Cadena de conexion "Server=DESKTOP-FGAVFI9; DataBase=Movies; Trusted_Connection=True; TrustServerCertificate=True;"

DbContext dentro de Data:
la linea debe quedar asi
 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

APPSETTINGS.JSON

 "AllowedHosts": "*",
  "ConnectionStrings": {
    "cadenaSQL": "Server=DESKTOP-FGAVFI9; DataBase=Movies; Trusted_Connection=True; TrustServerCertificate=True;"
  
}}
PROGRAM.CS
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EcommerceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

var app = builder.Build();


