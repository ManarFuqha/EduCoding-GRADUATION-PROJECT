using AutoMapper;
using courseProject.Core.IGenericRepository;
using courseProject.Core.Models;
using courseProject.Core.Models.DTO.LoginDTO;
using courseProject.Core.Models.DTO.RegisterDTO;
using courseProject.Core.Models.DTO.UsersDTO;
using courseProject.Emails;
using courseProject.ServiceErrors;
using ErrorOr;
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
            var verify = await unitOfWork.UserRepository.GetUserByEmail(loginRequestDTO.email);

            if (verify!=null && verify.IsVerified == false) return ErrorUser.UnVarified;


            var loginResponse = await unitOfWork.UserRepository.LoginAsync(loginRequestDTO);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
                return ErrorUser.InCorrectInput;
            return loginResponse;
        }

        public async Task<ErrorOr<Created>> Register(RegistrationRequestDTO model)
        {
            if (model.password != model.ConfirmPassword) return ErrorUser.IncorrectPassword;


            bool ifUserIsUniqe = unitOfWork.UserRepository.isUniqeUser(model.email);

            if (!ifUserIsUniqe) return ErrorUser.ExistEmail;



            using (var transaction = await unitOfWork.UserRepository.BeginTransactionAsync())
            {

                var createdUser = await unitOfWork.UserRepository.RegisterAsync(model);
                var success1 = await unitOfWork.StudentRepository.saveAsync();


            //    if (model.role.ToLower() == "student")
            //    {
            //        //var user = mapper.Map<User, Student>(createdUser);
            //        //var modelMapped = mapper.Map<User>(model);
            //       // createdUser.UserId = user.StudentId;
            ////        await unitOfWork.StudentRepository.CreateStudentAccountAsync(createdUser);
            //    }

              //  var success2 = await unitOfWork.UserRepository.saveAsync();
                string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);

                var cacheKey = $"VerificationCodeFor-{model.email}";
                memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));

                 await emailService.SendEmail(model.email, "Your Verification Code" ,EmailTexts.VerificationCode( model.userName,verificationCode));
                if (success1 > 0 )
                {
                    await transaction.CommitAsync();
                    return Result.Created;
                }

                return ErrorUser.hasError;
            }
        }


        public async Task<ErrorOr<Success>> addCodeVerification(string email, string code)
        {


            if (!memoryCache.TryGetValue($"VerificationCodeFor-{email}", out string verificationCode))
            {
                return ErrorUser.NotFound;
            }
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
            var getUser = await unitOfWork.UserRepository.GetUserByEmail(email);
            if (getUser == null) return ErrorUser.NotFound;

            if (getUser.IsVerified == true) return ErrorUser.Verified;

            string verificationCode = await unitOfWork.UserRepository.GenerateSecureVerificationCode(6);
            var cacheKey = $"VerificationCodeFor-{email}";
            memoryCache.Set(cacheKey, verificationCode, TimeSpan.FromHours(2));

            await emailService.SendEmail(email, "Your Verification Code", EmailTexts.VerificationCode(getUser.userName, verificationCode));

            return Result.Success;
        }

        public async Task<ErrorOr<Updated>> EditUserProfile(Guid id, ProfileDTO profile)
        {

            var profileToUpdate = await unitOfWork.UserRepository.getUserByIdAsync(id);
            if (profileToUpdate == null) return ErrorUser.NotFound;


                mapper.Map(profile, profileToUpdate);
            
              
                if (profile.image != null)

                {
                    profileToUpdate.ImageUrl = "Files\\" + await unitOfWork.FileRepository.UploadFile1(profile.image);
                }
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
            if (!BC.Verify(chengePasswordDTO.password, getUser.password)) return ErrorUser.IncorrectPassword;
            
            getUser.password = BC.HashPassword(chengePasswordDTO.Newpassword);
            await unitOfWork.UserRepository.UpdateUser(getUser);
            await unitOfWork.UserRepository.saveAsync();
            return Result.Updated;
        }

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
            getUser.password = BC.HashPassword(forgetPassword.password);
            await unitOfWork.UserRepository.UpdateUser(getUser);
            await unitOfWork.UserRepository.saveAsync();
            return Result.Success;
        }
    }
}

