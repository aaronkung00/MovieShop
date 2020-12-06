using MovieShop.Core.Entities;
using MovieShop.Core.Models.Request_Model;
using MovieShop.Core.Models.Response_Model;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class UserService : IUserService
    {
    //  private readonly ICurrentUserService _currentUserService;
    //  private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _encryptionService;
        //    private readonly IMovieService _movieService;
        //    private readonly IAsyncRepository<Purchase> _purchaseRepository;
        //    private readonly IAsyncRepository<Favorite> _favoriteRepository;
        //    private readonly IAsyncRepository<Review> _reviewRepository;
        //   private readonly IMapper _mapper;

        public UserService(ICryptoService encryptionService, IUserRepository userRepository)
        {
            _encryptionService = encryptionService;
            _userRepository = userRepository;

        }



        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // we are gonna check if the email exists in the database
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return null;


            var hashedPassword = _encryptionService.HashPassword(password, user.Salt);
            var isSuccess = user.HashedPassword == hashedPassword;

            /*    var roles = await _userRoleRepository.ListAllWithIncludesAsync(ur => ur.UserId == user.Id, role => role.Role);

                var response = _mapper.Map<UserLoginResponseModel>(user);

                var userRoles = roles.ToList();
                if (userRoles.Any())
                {
                    response.Roles = userRoles.Select(r => r.Role.Name).ToList();
                }

            */

            var response = new UserLoginResponseModel { 
                Id = user.Id, Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                DateOfBirth = user.DateOfBirth,             
            };

            return isSuccess ? response : null;

        }
        
        
        public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null && string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Email Already Exits");

            var salt = _encryptionService.CreateSalt();
            var hashedPassword = _encryptionService.HashPassword(requestModel.Password, salt);
            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName
            };
            var createdUser = await _userRepository.AddAsync(user);

            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id, Email = createdUser.Email, FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            //var response = _mapper.Map<UserRegisterResponseModel>(createdUser);
            return response;
        }

        public async Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
           
            var response = new UserRegisterResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return response;
        }
    }
}
