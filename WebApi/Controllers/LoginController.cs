using Api.Dto;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Api2.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private IUserRepository _userRepository;
    public LoginController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        return Ok(new UserDto());
    }

    [HttpPost("Register")]
    public IActionResult Register(RegisterDto registerDto)
    {
        var allUsers = _userRepository.GetUsers();

        var existingUser = allUsers.FirstOrDefault(u =>
            u.Name == registerDto.Username);

        if (existingUser != null)
        {
            if (existingUser.Name == registerDto.Username)
                return BadRequest(new { message = "Имя уже занято" });
        }

        var user = new User
        {
            Name = registerDto.Username,
            Password = registerDto.Password
        };

        _userRepository.AddUser(user);

        return Ok(new
        {
            message = "Регистрация успешна!",
            user = new
            {
                user.Name
            }
        });



    }
    [HttpGet("All")]
    [Authorize]
    public IActionResult GetAllUsers()
    {
        var users = _userRepository.GetUsers();

        var safeUsers = users.Select(o => new
        {
            o.Name
        });

        return Ok(safeUsers);
    }

    [HttpPost("Login")]

    public IActionResult Login(LoginDto loginDto)
    {
        var user = _userRepository.GetUsers().FirstOrDefault(o => o.Name == loginDto.UserName);
        if (user == null)
        {
            return BadRequest(new { message = "Неверно введено имя пользователя" });

        }
        else if (loginDto.Password != user.Password)
        {
            return BadRequest(new { message = "Неверно введен пароль" });
        }
        return Ok(new
        {
            message = "Вход выполнен",
            user = new
            {
                user.Name,
                user.Password
            }
        });
    }
}

