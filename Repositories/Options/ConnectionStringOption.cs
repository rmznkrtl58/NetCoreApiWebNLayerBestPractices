

namespace App.Repositories.Options
{
    public class ConnectionStringOption
    {
        public const string key = "ConnectionStrings";
        //Tip güvenliği açısından Api->AppSettingsJson->DevelopmentJson->ConnectionStringi tanımladığımız yerde bu propumu kullanacağım.
        public string SqlServer { get; set; } = default!;
    }
}
