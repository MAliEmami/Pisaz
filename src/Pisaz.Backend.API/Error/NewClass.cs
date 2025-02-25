// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Pisaz.Backend.API.Error
// {
//     public class NewClass
//     {
//         this is an asp.net core Project.
// i have a little error. just find as i say.
// i write my PhoneNumber i login api and its give me Correct Token and i write like Bearer <Token>, and its succesefull and when i want to list Adderss/list the server give me 401 error but i dont know where is the problem.

// Program.cs:
// using Pisaz.Backend.API.Extensions;
// using Pisaz.Backend.API.Services;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddMySagger();

// builder.Services.AddMyControllers();

// builder.Services.AddApplicationServices(builder.Configuration);
// builder.Services.AddMyAuthentication(builder.Configuration);

// var app = builder.Build();

// app.UseCors();

// app.UseRouting();

// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllers();

// app.UseSwaggerIfDevelopment();

// app.Run();

// Extensions:
//     public static class ApplicationServices
//     {
//         public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
//         {
//             services.AddScoped<IRepository<Client>, ClientRepository>();
//             services.AddScoped<IRepository<Address>, AddressRepository>();
//             services.AddScoped<LoginRequestRepository>();

//             services.AddScoped<IGeneralService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>, ClientService>();
//             services.AddScoped<IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>, AddressService>();
//             services.AddScoped<IListService<DiscountCode, DiscountCodeDTO>, DiscountService>();
//             services.AddScoped<AuthService>();

//             services.AddCors(options =>
//             {
//                 options.AddDefaultPolicy(policy =>
//                 {
//                     policy.AllowAnyHeader()
//                     .AllowAnyMethod()
//                     .AllowAnyOrigin();
//                 });
//             });

//             services.AddDbContext<PisazDB>(oprions =>
//             {
//                 oprions.UseSqlServer(configuration.GetConnectionString("Pisaz"));
//             });
//         }
//         public static IServiceCollection AddMyControllers(this IServiceCollection services)
//         {
//             services.AddControllers();

//             services.AddFluentValidationAutoValidation();
//             services.AddFluentValidationClientsideAdapters();

//             return services;
//         }

//         public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
//         {
//             services.AddAuthentication(x =>
//             {
//                 x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                 x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//                 x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//             }).AddJwtBearer(x =>
//             {
//                 x.TokenValidationParameters = new TokenValidationParameters
//                 {
//                     ValidIssuer = configuration["JwtSettings:Issuer"],
//                     ValidAudience = configuration["JwtSettings:Audience"],
//                     IssuerSigningKey = new SymmetricSecurityKey
//                         (Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
//                     ValidateIssuer = true,
//                     ValidateAudience = true,
//                     ValidateLifetime = true,
//                     ValidateIssuerSigningKey = true
//                 };
//                 x.Events = new JwtBearerEvents
//                 {
//                     OnAuthenticationFailed = context =>
//                     {
//                         Console.WriteLine("Authentication failed: " + context.Exception.Message);
//                         return Task.CompletedTask;
//                     },
//                     OnTokenValidated = context =>
//                     {
//                         Console.WriteLine("Token validated successfully.");
//                         return Task.CompletedTask;
//                     }
//                 };
//             });

//             services.AddAuthorization();

//             return services;
//         }
//     }

// Login api:
//     [ApiController]
//     [Route("Auth")]
//     public class LoginRequestApiController : ControllerBase
//     {
//         private readonly AuthService _authService;

//         public LoginRequestApiController(AuthService authService)
//         {
//             _authService = authService;
//         }

//         [HttpPost("login")]
//         public IActionResult Login([FromBody] string phoneNumber)
//         {
//             var token = _authService.Authenticate(phoneNumber);
//             if (token == null)
//             {
//                 return Unauthorized();
//             }

//             return Ok(new { Token = token });
//         }
//     }

// Address List api:
//         [Authorize]
//         [HttpPost("list")]
//         public async Task<IActionResult> List()
//         {
//             var clientIdClaim = User.FindFirst("ClientID")?.Value;

//             if (string.IsNullOrEmpty(clientIdClaim))
//             {
//                 return Unauthorized("User ID not found in token.");
//             }

//             // Parse the ID to an integer
//             if (!int.TryParse(clientIdClaim, out var id))
//             {
//                 return BadRequest("Invalid user ID in token.");
//             }

//             // Use the ID to fetch the address
//             var address = await _service.ListAsync(id);
//             if (address == null)
//             {
//                 return NotFound();
//             }

//             return Ok(address);
//         }

// AuthService:
//     public class AuthService
//     {
//         private readonly LoginRequestRepository _loginRequestRepository;
//         private readonly IConfiguration _configuration;

//         public AuthService(LoginRequestRepository loginRequestRepository, IConfiguration configuration)
//         {
//             _loginRequestRepository = loginRequestRepository;
//             _configuration = configuration;
//         }

//         public string Authenticate(string phoneNumber)
//         {
//             var client = _loginRequestRepository.GetClientByPhoneNumber(phoneNumber);
//             if (client == null)
//             {
//                 return null;
//             }

//             // Generate JWT token
//             var tokenHandler = new JwtSecurityTokenHandler();
//             var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
//             var tokenDescriptor = new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(new Claim[]
//                 {
//                     new Claim("ClientPhoneNumber", phoneNumber), 
//                     new Claim("ClientID", client.ID.ToString())
//                 }),
//                 Expires = DateTime.UtcNow.AddDays(7),
//                 Issuer = _configuration["JwtSettings:Issuer"],
//                 Audience = _configuration["JwtSettings:Audience"],
//                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//             };
//             var token = tokenHandler.CreateToken(tokenDescriptor);
//             return tokenHandler.WriteToken(token);
//         }
//     }

// LoginRequestRepository:
//     public class LoginRequestRepository(PisazDB db)
//     {
//         private readonly PisazDB _db = db;

//         public Client? GetClientByPhoneNumber(string phoneNumber)
//         {
//             var sql = $"SELECT * FROM Client WHERE PhoneNumber = @phoneNumber";

//             var parameters = new[]
//             {
//                 new SqlParameter("@phoneNumber", phoneNumber)
//             };
//             return  _db.Clients.FromSqlRaw(sql, parameters)
//                                .FirstOrDefault();
//         }
//     }
//     }
// }