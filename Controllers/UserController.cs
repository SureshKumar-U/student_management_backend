using CrudAPi.common;
using CrudAPi.Data;
using CrudAPi.Exceptions;
using CrudAPi.Models;
using CrudAPi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using BCrypt.Net;
using CrudAPi.Dtos;

namespace CrudAPi.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext db;
    private readonly IJwtService _jwtService;
    public AuthController(AppDbContext dbcontext, IJwtService jwtService)
    {
        db = dbcontext;
        _jwtService = jwtService;
    }

    [HttpPost("login")]

    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        var user = await db.Users.FirstOrDefaultAsync(user => user.Email == loginUserDto.Email);
        
        if (user == null)
        {
            throw new UnauthorizedException("User dont have an account ");
        }
        //check password
        bool isVerified = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash);
        if (!isVerified)
        {
            throw new UnauthorizedException("User dont have an account");
        }
        var token = _jwtService.GenerateToken(user);
        // save user
        var response = new ApiResponse<LoginResponseDto>
        {
            message = "User Login successfully",
            data = new LoginResponseDto
            {
                Token = token,
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            },
            status = 200,
        };

        return StatusCode(StatusCodes.Status200OK, response);

    }


    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpUserDto signUpUserDto)
    {
        var existedUser = await db.Users.FirstOrDefaultAsync(user => user.Email == signUpUserDto.Email);

        if (existedUser != null)
        {
            throw new UserAlreadyExistsException("User Already had an account");
        }
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(signUpUserDto.Password);

        var user = new User
        {
            Email = signUpUserDto.Email,
            Name = signUpUserDto.Name,
            PasswordHash = hashedPassword,
            Role = signUpUserDto.Role,
        };

        await db.Users.AddAsync(user);
        if (signUpUserDto.Role == UserRole.STUDENT)
        {
            var student = new Student
            {
                UserId = user.Id
            };
            await db.Students.AddAsync(student);
        }

        await db.SaveChangesAsync();
        var response = new ApiResponse<object>
        {
            message = "User Created successfully",
            status = 201,
        };
        return StatusCode(StatusCodes.Status201Created, response);

    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()

    {
        var users = await db.Users
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            })
            .ToListAsync();

        var response = new ApiResponse<List<UserResponseDto>>
        {
            message = "User fetched successfully",
            status = 200,
            data = users,
        };

        return StatusCode(StatusCodes.Status200OK, response);


    }
}