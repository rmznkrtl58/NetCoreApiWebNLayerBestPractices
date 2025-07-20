namespace App.WebAPI.Extensions
{
    public static class ConfigurePipelineExtensions
    {
        public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
        {
            //Exception handlerlarımızın çalısması için 
            app.UseExceptionHandler(x => { });//boş bırakma sebebim zaten exception handlerlarım mevcut fakat .net core illede içine bir şey yaz dediği için yazdım.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            return app;
        }
    }
}
