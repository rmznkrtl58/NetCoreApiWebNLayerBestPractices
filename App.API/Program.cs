using App.Repositories.Extensions;
using App.Services.Extensions;
using App.Services.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Fluent Validation Filter Yapýlandýrmasý
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add< FluentValidationFilter >();
    //referans tipler için null kontrolünü gerçekleþtirmeyecek
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes=true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Extensions Yapýlandýrmasý
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

//Apinin .net core tarafýnda default validasyon hata mesajlarýný kapatman
builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

//Exception handlerlarýmýzýn çalýsmasý için 
app.UseExceptionHandler(x => { });//boþ býrakma sebebim zaten exception handlerlarým mevcut fakat .net core illede içine bir þey yaz dediði için yazdým.
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
