using Npgsql;
using TrackYourSpendings.Application.Exceptions;

namespace TrackYourSpendings.Web.Utils;

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
            connectionString = BuildConnectionString(Environment.GetEnvironmentVariable("NEON_DB_URI")
                                                     ?? throw new NotFoundException("NEON_DB_URI not found"));
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
                   ?? throw new NotFoundException("CLIENT_ID not found");
        }

        return Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")
               ?? throw new NotFoundException("GOOGLE_CLIENT_ID not found");
    }

    public static string GetGoogleClientSecret(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Google:CLIENT_SECRET")
                   ?? throw new NotFoundException("CLIENT_SECRET not found");
        }

        return Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")
               ?? throw new NotFoundException("GOOGLE_CLIENT_SECRET not found");
    }

    public static string GetGoogleRedirectUri(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Google:REDIRECT_URI")
                   ?? throw new NotFoundException("REDIRECT_URI not found");
        }

        return Environment.GetEnvironmentVariable("GOOGLE_REDIRECT_URI")
               ?? throw new NotFoundException("GOOGLE_REDIRECT_URI not found");
    }

    public static string GetEmailPassword(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Email:PASSWORD")
                   ?? throw new NotFoundException("PASSWORD not found");
        }

        return Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
               ?? throw new NotFoundException("EMAIL_PASSWORD not found");
    }

    public static string GetEmailHost(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Email:HOST")
                   ?? throw new NotFoundException("HOST not found");
        }

        return Environment.GetEnvironmentVariable("EMAIL_HOST")
               ?? throw new NotFoundException("EMAIL_HOST not found");
    }

    public static string GetEmailSenderEmail(IConfiguration config, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            return config.GetValue<string>("Email:SENDER_EMAIL")
                   ?? throw new NotFoundException("SENDER_EMAIL not found");
        }

        return Environment.GetEnvironmentVariable("EMAIL_SENDER")
               ?? throw new NotFoundException("EMAIL_SENDER not found");
    }

    public static int GetEmailPort(IConfiguration config, IWebHostEnvironment env)
    {
        int port;
        if (env.IsDevelopment())
        {
            port = config.GetValue<int>("Email:PORT");

            if (port == 0)
            {
                throw new NotFoundException("PORT not found");
            }

            return port;
        }

        return int.TryParse(Environment.GetEnvironmentVariable("EMAIL_PORT"), out port)
            ? port
            : throw new NotFoundException("EMAIL_PORT not found");
    }
}