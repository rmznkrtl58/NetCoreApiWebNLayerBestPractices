using App.Application.Extensions;
using App.Bus.Extensions;
using App.Persistence.Extensions;
using App.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Extensions Yapýlandýrmasý
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
//Extensions Yapýlandýrmasý
builder.Services.AddControllersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt().AddCachingExt().AddBusExt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

//Extensions Yapýlandýrmasý
app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
