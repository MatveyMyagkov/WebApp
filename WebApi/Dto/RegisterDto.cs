using System.ComponentModel.DataAnnotations;

namespace Api.Dto;

public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}