// i have error for athorize.
// This is me Generate Token method:
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

//             Console.WriteLine("Client ID :");
//             Console.WriteLine(client.ID.ToString());

//             var token = tokenHandler.CreateToken(tokenDescriptor);
//             return tokenHandler.WriteToken(token);
//         }
// in console the 'client.ID.ToString()' is correct number.


// this is my api:
//         [HttpPost("list")]
//         public async Task<IActionResult> List()
//         {
//             var clientIdClaim = User.FindFirst("ClientID")?.Value;
//             Console.WriteLine("Check error: ");
//             Console.WriteLine(clientIdClaim);
//             Console.WriteLine("finish chkeck");

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

// and in console the 'clientIdClaim' is null.


// Authentication Service:
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