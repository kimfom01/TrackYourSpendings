using TrackYourSpendings.Application.Dtos.Common;

namespace TrackYourSpendings.Application.Dtos;

public class UserDto : BaseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}