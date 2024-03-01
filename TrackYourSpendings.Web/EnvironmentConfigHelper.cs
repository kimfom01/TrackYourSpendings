using Npgsql;

namespace TrackYourSpendings.Web;

public static class EnvironmentConfigHelper
{
    public static string? GetConnectionString(IConfiguration config, IWebHostEnvironment env)
    {
        string? connectionString;

        if (env.IsDevelopment())
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        else
        {
            connectionString = BuildConnectionString(Environment.GetEnvironmentVariable("DB_URI")
                                                     ?? throw new NullReferenceException("DB_URI missing"));
        }

        return connectionString;
    }

    private static string BuildConnectionString(string dbUrl)
    {
        var databaseUri = new Uri(dbUrl);

        var userInfo = databaseUri.UserInfo.Split(':');
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseUri.Host,
            Port = databaseUri.Port,
            Username = userInfo[0],
            Password = userInfo[1],
            Database = databaseUri.LocalPath.TrimStart('/'),
            SslMode = SslMode.Require,
            Pooling = true,
            MinPoolSize = 0,
            MaxPoolSize = 100,
            ConnectionLifetime = 0
        };

        return builder.ToString();
    }

    public static string GetGoogleClientId(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Google:CLIENT_ID")
                   ?? throw new NullReferenceException("CLIENT_ID missing");
        }

        return Environment.GetEnvironmentVariable("CLIENT_ID")
               ?? throw new NullReferenceException("CLIENT_ID missing");
    }

    public static string GetGoogleClientSecret(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Google:CLIENT_SECRET")
                   ?? throw new NullReferenceException("CLIENT_SECRET missing");
        }

        return Environment.GetEnvironmentVariable("CLIENT_SECRET")
               ?? throw new NullReferenceException("CLIENT_SECRET missing");
    }

    public static string GetGoogleRedirectUri(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Google:REDIRECT_URI")
                   ?? throw new NullReferenceException("REDIRECT_URI missing");
        }

        return Environment.GetEnvironmentVariable("REDIRECT_URI")
               ?? throw new NullReferenceException("REDIRECT_URI missing");
    }
}