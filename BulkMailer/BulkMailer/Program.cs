using BulkMailer.Data;
using BulkMailer.Services.Background;
using BulkMailer.Services.Emails;
using BulkMailer.Services.Validation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration["SQLDbConnection"]));
builder.Services.AddHostedService<EmailScheduledDispatchService>();

builder.Services.AddScoped<IEmailRecipientsService, EmailRecipientService>();
builder.Services.AddScoped<IEmailAddressValidator, EmailAddressValidator>();

builder.Services.AddTransient<IEmailDispatcher, SendGridDispatcher>();
builder.Services.AddTransient<IEmailContentProvider, MockEmailContentProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.Run();
