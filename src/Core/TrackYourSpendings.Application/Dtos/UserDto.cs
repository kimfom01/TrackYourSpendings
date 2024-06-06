using TrackYourSpendings.Application.Dtos.Common;

namespace TrackYourSpendings.Application.Dtos;

public class UserDto : BaseDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}