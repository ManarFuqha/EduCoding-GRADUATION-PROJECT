using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Repository.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using System.Security.Cryptography;

namespace courseProject.Repository.GenericRepository
{
    public class UserRepository : GenericRepository1<User>,  IUserRepository
    {
 
        
        private readonly projectDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string secretKey;
        private string token1 = "";
        public UserRepository( projectDbContext dbContext ,IConfiguration configuration , IHttpContextAccessor httpContextAccessor) :base(dbContext)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
            secretKey = configuration.GetSection("Authentication")["SecretKey"]; 
        }

       

        public bool isUniqeUser(string email)
        {
            var user = dbContext.users.FirstOrDefault(x=>x.email == email);
            if(user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await dbContext.users.FirstOrDefaultAsync(x => x.email == loginRequestDTO.email);
            bool pass = false;
            if (user != null)
            {
                pass = BC.Verify(loginRequestDTO.password, user.password);
                
            }

            if (user == null || pass == false)
            {
                return new LoginResponseDTO() {
                    User = null,
                    Token = ""

                };
            }
           return generateToken(user);
        }
        public LoginResponseDTO generateToken(User? user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",user.UserId.ToString()),
                    new Claim(ClaimTypes.Email ,user.email),

                   new Claim(ClaimTypes.Role , user.role )

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = user,
                Token = tokenHandler.WriteToken(token)

            };
            token1 = loginResponseDTO.Token;
            return loginResponseDTO;
        }
        

       
     

        public async Task<User> RegisterAsync( RegistrationRequestDTO registerRequestDTO)
        {
            if(registerRequestDTO.password != registerRequestDTO.ConfirmPassword)
            {
                return null;
            }
            var passHash = BC.HashPassword(registerRequestDTO.password);
            User user = new User()
            {

                userName = registerRequestDTO.userName,
                email = registerRequestDTO.email,
                password = passHash,
                role = registerRequestDTO.role.ToLower(),
                IsVerified = false,
                dateOfAdded = DateTime.Now
            };
           await dbContext.users.AddAsync(user);
            return  user;
        }

        public async Task<User> createEmployeeAccount(User user)
        {
            var passHash = BC.HashPassword(user.password);
            user.IsVerified= true;
            user.password = passHash;
            user.role= user.role.ToLower();
            user.dateOfAdded = DateTime.Now;
            dbContext.Set<User>().AddAsync(user);
            return user;
        }

        public async Task<string> GenerateSecureVerificationCode(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return (Convert.ToBase64String(randomNumber).Substring(0, length));
            }
        }

        public async Task<User> GetUserByRoleAsync(string role)
        {
           return await dbContext.users.FirstOrDefaultAsync(x=>x.role.ToLower()==role.ToLower());
        }

        public async Task<User> getUserByIdAsync(Guid UserId)
        {
            return await dbContext.users.FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<IReadOnlyList< User>> getAllMainSubAmdinRole()
        {
           return await dbContext.users.Where(x => x.role.ToLower() == "main-subadmin").ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await dbContext.users.FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task UpdateUser(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
        }


        public async Task<User> getRoleFromToken()
        {
            try
            {

                var userIdAsString = (httpContextAccessor.HttpContext.User.FindFirst("UserId")).Value;
                Guid.TryParse(userIdAsString, out var userId);
                var user = (await dbContext.users.FirstAsync(x => x.UserId == userId));
                return user;
            }
            catch(Exception ex)
            {
                return null;
            }
        }



        public async Task<User> ViewProfileAsync(Guid id, string role)
        {
         return await dbContext.users.Where(x=>x.role.ToLower()==role.ToLower()).FirstOrDefaultAsync(x => x.UserId == id);
            
        }


        public async Task editRole(User user)
        {
            dbContext.users.Update(user);
        }
    }
}
