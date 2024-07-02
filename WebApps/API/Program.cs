using GMS.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var memberConnectionString = builder.Configuration.GetConnectionString("MemberConnection");
builder.Services.AddDbContext<MemberDbContext>(x => x.UseSqlServer(memberConnectionString));

var gatheringConnectionString = builder.Configuration.GetConnectionString("GatheringConnection");
builder.Services.AddDbContext<GatheringDbContext>(x => x.UseSqlServer(gatheringConnectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
