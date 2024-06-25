using Cronos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrackYourSpendings.Application.Contracts.Database;
using TrackYourSpendings.Domain.Entities;
using TrackYourSpendings.Infrastructure.Database.Identity;

namespace TrackYourSpendings.Infrastructure.BackgroundServices;

public class NewMonthJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public NewMonthJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var expression = CronExpression.Parse("0 0 L   * *");

        var next = expression.GetNextOccurrence(DateTime.UtcNow);

        var timeSpan = TimeSpan.FromSeconds(next!.Value.Second - DateTime.Now.Second);

        if (timeSpan < TimeSpan.Zero)
        {
            timeSpan = timeSpan.Negate();
        }

        using var timer = new PeriodicTimer(timeSpan);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await CreateNewWallet(stoppingToken);
        }
    }

    private async Task CreateNewWallet(CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var users = userManager.Users;

        var walletName = DateTime.UtcNow.AddMonths(1).ToString("MMMM");

        foreach (var user in users)
        {
            if (await WalletExists(user.Id, walletName, unitOfWork))
            {
                continue;
            }

            await unitOfWork.Wallets.AddEntity(new Wallet
            {
                UserId = user.Id,
                Name = walletName,
                Currency = "USD",
                Income = 0M,
            });
        }

        await unitOfWork.SaveChanges(cancellationToken);
    }

    private async Task<bool> WalletExists(string userId, string walletName, IUnitOfWork unitOfWork)
    {
        var wallet = await unitOfWork.Wallets.GetEntity(wal => wal.UserId == userId
                                                               && wal.Name == walletName);

        return wallet is not null;
    }
}