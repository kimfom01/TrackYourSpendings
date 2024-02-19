using Web.Models;
using Web.Repositories;

namespace Web.Services.Implementation;

public class WalletService : IWalletService
{
    private readonly IUnitOfWork _unitOfWork;

    public WalletService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Wallet?> AddWallet(Wallet wallet, string? userId)
    {
        wallet.Balance = wallet.Income;
        wallet.UserId = userId;

        var entity = await _unitOfWork.Wallets.AddEntity(wallet);
        await _unitOfWork.SaveChanges();

        return entity;
    }

    public async Task UpdateWallet(Wallet wallet, string? userId)
    {
        var oldWallet = await _unitOfWork.Wallets.GetEntity(wall =>
            wall.Id == wallet.Id && wall.UserId == userId);

        if (oldWallet is null)
        {
            throw new NullReferenceException($"There is no wallet with id={wallet.Id}");
        }

        oldWallet.Name = wallet.Name;

        var incomeDifference = wallet.Income - oldWallet.Income;

        if (incomeDifference != 0)
        {
            oldWallet.Balance += incomeDifference;
        }

        oldWallet.Income = wallet.Income;

        await _unitOfWork.Wallets.Update(oldWallet);
        await _unitOfWork.SaveChanges();
    }

    public async Task DeleteWallet(Wallet wallet, string? userId)
    {
        await _unitOfWork.Wallets.RemoveEntity(wal =>
            wal.Id == wallet.Id && wal.UserId == userId);

        await _unitOfWork.SaveChanges();
    }

    public async Task<Wallet?> GetActiveWallet(string? userId)
    {
        return await _unitOfWork.Wallets.GetEntity(wall =>
            wall.Active == true && wall.UserId == userId);
    }

    public async Task SetActiveWallet(Wallet wallet, string? userId)
    {
        var oldActiveWallet = await GetActiveWallet(userId);
        if (oldActiveWallet is not null)
        {
            oldActiveWallet.Active = false;
        }
        
        wallet.Active = true;
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Wallet>> GetWallets(string? userId)
    {
        var wallets = await _unitOfWork.Wallets.GetEntities(wal =>
            wal.UserId == userId);

        return wallets ?? Enumerable.Empty<Wallet>();
    }

    public async Task<IEnumerable<Wallet>> GetInactiveWallets(string? userId)
    {
        var wallets = await _unitOfWork.Wallets.GetEntities(wal =>
            wal.UserId == userId && wal.Active == false);

        return wallets ?? Enumerable.Empty<Wallet>();
    }

    public async Task<Wallet?> GetWallet(int? walletId, string? userId)
    {
        return await _unitOfWork.Wallets.GetEntity(wall =>
            wall.Id == walletId && wall.UserId == userId);
    }
}