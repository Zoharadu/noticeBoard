using Application.Interfaces;
using Application.Services;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<NoticeService>();
builder.Services.AddScoped<INoticeRepository>(_ =>
{
    var filePath = Path.Combine(
        AppContext.BaseDirectory, 
        "Storage",
        "notices.json"
    );

    return new NoticeRepository(filePath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
