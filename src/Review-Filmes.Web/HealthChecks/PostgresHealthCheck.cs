
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ReviewFilmes.Api.Entity;

public class PostgresHealthCheck : IHealthCheck
{
    private readonly AppDbContext _db;

    public PostgresHealthCheck(AppDbContext db)
    {
        _db = db;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            bool connected = await _db.Database.CanConnectAsync(
                cancellationToken);

            return connected
                ? HealthCheckResult.Healthy("PostgreSQL disponível")
                : HealthCheckResult.Unhealthy("PostgreSQL indisponível");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Falha ao conectar ao PostgreSQL", ex);
        }
    }
}
