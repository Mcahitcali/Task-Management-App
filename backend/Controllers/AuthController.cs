using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TaskDbContext _context;
    private readonly IAuthService _authService;

    public AuthController(TaskDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.TCKimlikNo == request.TCKimlikNo))
        {
            return BadRequest("TC Kimlik No already registered");
        }

        var user = new User
        {
            TCKimlikNo = request.TCKimlikNo,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = _authService.HashPassword(request.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _authService.GenerateJwtToken(user);

        return new AuthResponse
        {
            Token = token,
            TCKimlikNo = user.TCKimlikNo,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsAdmin = user.IsAdmin
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.TCKimlikNo == request.TCKimlikNo);

        if (user == null || !_authService.VerifyPassword(request.Password, user.Password))
        {
            return Unauthorized("Invalid TC Kimlik No or password");
        }

        var token = _authService.GenerateJwtToken(user);

        return new AuthResponse
        {
            Token = token,
            TCKimlikNo = user.TCKimlikNo,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsAdmin = user.IsAdmin
        };
    }
} 