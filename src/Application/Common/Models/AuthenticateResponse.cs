using Domain.Entities;

namespace Application.Common.Models;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string AccessToken { get; set; }

    public AuthenticateResponse(User user, string token)
    {
        Id = user.Id;
        Name = user.Name;
        Surname = user.Surname;
        Username = user.Username;
        AccessToken = token;
    }
}
