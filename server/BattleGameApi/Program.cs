var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<BATTLEGAMEContext>(options =>
    options.UseSqlServer(
        "Server=localhost;Database=Battlegame;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();           // phục vụ wwwroot
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();