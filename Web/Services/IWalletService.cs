using Web.Models;

namespace Web.Services;

public interface IWalletService
{
    Task<Wallet?> AddWallet(Wallet wallet, string? userId);
    Task UpdateWallet(Wallet wallet, string? userId);
    Task DeleteWallet(Wallet wallet, string? userId);
    Task<Wallet?> GetActiveWallet(string? userId);
    Task SetActiveWallet(Wallet wallet, string? userId);
    Task<IEnumerable<Wallet>> GetWallets(string? userId);
    Task<IEnumerable<Wallet>> GetInactiveWallets(string? userId);
    Task<Wallet?> GetWallet(int? walletId, string? userId);
}