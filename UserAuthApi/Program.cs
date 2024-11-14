using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Menambahkan DbContext dan konfigurasi Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfigurasi Identity dengan aturan password sederhana
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;               
    options.Password.RequireNonAlphanumeric = false;    
    options.Password.RequireLowercase = false;         
    options.Password.RequireUppercase = false;          
    options.Password.RequireDigit = false;              
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllers();

// Menambahkan layanan untuk Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mengaktifkan middleware untuk autentikasi dan otorisasi
app.UseAuthentication();
app.UseAuthorization();

// Mengaktifkan middleware untuk Swagger jika dalam mode pengembangan
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Swagger UI akan muncul di /swagger
}

app.MapControllers(); // Mengatur routing untuk controller API

app.Run();
