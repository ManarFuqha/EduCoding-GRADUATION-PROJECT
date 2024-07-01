using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Emails;
using courseProject.ServiceErrors;
using ErrorOr;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.Extensions.Caching.Memory;
using BC = BCrypt.Net.BCrypt;

namespace courseProject.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;
        private readonly IEmailService emailService;

        public UserServices(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache , IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.emailService = emailService;
        }



        public async Task<ErrorOr<User>> getUserById(Guid userId)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(userId);
            if (getUser == null) return ErrorUser.NotFound;
            return getUser;
        }

        public async Task<ErrorOr<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {

            // Verify the user by email
            var verify = await unitOfWork.UserRepository.GetUserByEmail(loginRequestDTO.email);

            // Check if the user is verified
            if (verify!=null && verify.IsVerified == false) return ErrorUser.UnVarified;

            // Attempt to log in the user
            var loginResponse = await unitOfWork.UserRepository.LoginAsync(loginRequestDTO);

            // Check if the login was successful
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                return ErrorUser.InCorrectInput;
            return loginResponse;
        }

        public async Task<ErrorOr<Created>> Register(RegistrationRequestDTO model)
        {
            // Check if the password and confirm password match
            if (model.password != model.ConfirmPassword) return ErrorUser.IncorrectPassword;

            // Check if the user is unique by email
            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(model.email);

            if (!ifUserIsUniqe) return ErrorUser.ExistEmail;


            // Begin a database transaction
            using (var transaction = await unitOfWork.UserRepository.BeginTransactionAsync())
            {
                // Register the user and save the changes
                var createdUser = await unitOfWork.UserRepository.RegisterAsync(model);
                var success1 = await unitOfWork.StudentRepository.saveAsync();

                // Generate a secure verification code
                string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);

                // Cache the verification code
                var cacheKey = $"VerificationCodeFor-{model.email}";
                memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));

                // Send a verification email to the user
                await emailService.SendEmail(model.email, "Your Verification Code" ,EmailTexts.VerificationCode( model.userName,verificationCode));
                if (success1 > 0 )
                {
                    await transaction.CommitAsync();// Commit the transaction if the save was successful
                    return Result.Created;
                }

                return ErrorUser.hasError;
            }
        }


        public async Task<ErrorOr<Success>> addCodeVerification(string email, string code)
        {

            // Check if the verification code is in the cache
            if (!memoryCache.TryGetValue($"VerificationCodeFor-{email}", out string verificationCode))
            {
                return ErrorUser.NotFound;
            }

            // Check if the provided code matches the verification code
            if (verificationCode == code)
            {
                memoryCache.Remove($"VerificationCodeFor-{email}");
                var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
                getUser.IsVerified = true;
                await unitOfWork.UserRepository.UpdateUser(getUser);
                await unitOfWork.UserRepository.saveAsync();
                return Result.Success;
            }
            return ErrorUser.InCorrectCode;
        }

        public async Task<ErrorOr<Success>> reSendTheVerificationCode(string email)
        {

            // Retrieve the user by email
            var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
            if (getUser == null) return ErrorUser.NotFound;// Return an error if the user is not found

            // Check if the user is already verified
            if (getUser.IsVerified == true) return ErrorUser.Verified;

            // Generate a new verification code
            string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);
            var cacheKey = $"VerificationCodeFor-{email}";
            memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));// Cache the new verification code

            // Send a new verification email to the user
            await emailService.SendEmail(email, "Your Verification Code", EmailTexts.VerificationCode(getUser.userName, verificationCode));

            return Result.Success;
        }

        public async Task<ErrorOr<Updated>> EditUserProfile(Guid id, ProfileDTO profile)
        {
            // Retrieve the user profile to update by userId
            var profileToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (profileToUpdate == null) return ErrorUser.NotFound;

            // Map the changes from ProfileDTO to the user profile
            mapper.Map(profile, profileToUpdate);
            
              
                if (profile.image != null)

                {
                    profileToUpdate.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(profile.image);
                }

            // Update the user profile in the repository and save the changes
            await unitOfWork.UserRepository.UpdateUser(profileToUpdate);
                await unitOfWork.UserRepository.saveAsync();

                    return Result.Updated;
                

        }

        public async Task<ErrorOr<UserInfoDTO>> GetProfileInfo(Guid id)
        {
            var UserFound = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (UserFound == null) return ErrorUser.NotFound;
         
            if (UserFound.ImageUrl != null)
            {
                UserFound.ImageUrl= await unitOfWork.FileRepository.GetFileUrl(UserFound.ImageUrl);
            }
            
            return mapper.Map<UserInfoDTO>(UserFound); 
        }

        public async Task<ErrorOr<Updated>> changePassword(Guid UserId, ChengePasswordDTO chengePasswordDTO)
        {
            var getUser = await unitOfWork.UserRepository.getUserByIdAsync(UserId);
            if (getUser == null) return ErrorUser.NotFound;

            // Verify the current password
            if (!BC.Verify(chengePasswordDTO.password, getUser.password)) return ErrorUser.IncorrectPassword;

            // Update the user's password
            getUser.password = BC.HashPassword(chengePasswordDTO.Newpassword);
            await unitOfWork.UserRepository.UpdateUser(getUser);
            await unitOfWork.UserRepository.saveAsync();

            return Result.Updated;// Return a success result indicating the password was updated
        }



        /// Retrieves a user by their email address and initiates the password reset process.
        /// If the user is found, generates a secure verification code, caches it, and sends an email
        /// with the verification code for password reset.
        public async Task<ErrorOr<Success>> GetUserByEmail(string email)
        {
            var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
            if(getUser == null) return ErrorUser.NotFound;
            string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);
            var cacheKey = $"VerificationCodeFor-{email}";
            memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));

            await emailService.SendEmail(email, "Reset Password", EmailTexts.ForgetPassword(getUser.userName, verificationCode));

            return Result.Success;
        }

        public async Task<ErrorOr<Success>> forgetPassword(string email, ForgetPasswordDTO forgetPassword )
        {
            var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
            if( getUser == null) return ErrorUser.NotFound;

            // Update the user's password
            getUser.password = BC.HashPassword(forgetPassword.password);
            await unitOfWork.UserRepository.UpdateUser(getUser);
            await unitOfWork.UserRepository.saveAsync();
            return Result.Success;
        }
    }
}

