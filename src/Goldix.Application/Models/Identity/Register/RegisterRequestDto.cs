namespace Goldix.Application.Models.Identity.Register;

public class RegisterRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
}
