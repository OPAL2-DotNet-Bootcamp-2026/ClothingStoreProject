using ClothingStore.Models;
using ClothingStore.Repos;
using static ClothingStore.DTOs.UserDTOs;

namespace ClothingStore.Services
{
    public class UserService
    {
        private UserRepo repo;
        private AuthService authService;

        public UserService(UserRepo repo, AuthService _authService)
        {
            this.repo = repo;
            authService = _authService;
        }

        private UserResponseDto MapToResponse(User user)
        {
            return new UserResponseDto
            {
                userId = user.userId,
                userName = user.userName,
                email = user.email,
                fullName = user.fullName,
                phoneNumber = user.phoneNumber,
                address = user.address,
                registrationDate = user.registrationDate,
                isActive = user.isActive,
                role = user.role
            };
        }


        // Get All Users
        public List<UserResponseDto> GetAllUsers()
        {
            return repo.GetAllUsers()
                       .Select(u => MapToResponse(u))
                       .ToList();
        }




        // Get User By Id
        public UserResponseDto? GetUserById(int id)
        {
            User? user = repo.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            return MapToResponse(user);

        }




        // Get Users By Role
        public List<UserResponseDto> GetUsersByRole(Enums.Role role)
        {
            return repo.GetByRole(role)
                      .Select(u => MapToResponse(u))
                      .ToList();
        }



        // Register User
        public UserResponseDto? RegisterUser(RegisterUserDto dto)
        {
            string normalizedEmail = dto.email.Trim().ToLower();

            string normalizedUsername = dto.userName.Trim();


            // Check Email
            if (repo.EmailExists(normalizedEmail))
            {
                return null;
            }

            // Check Username

            if (repo.UsernameExists(normalizedUsername))
            {
                return null;
            }


            User user = new User
            {
                userName = normalizedUsername,
                email = normalizedEmail,

                passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password),

                fullName = dto.fullName,
                phoneNumber = dto.phoneNumber,
                address = dto.address,
                registrationDate = DateTime.UtcNow,
                isActive = true,
                role = Enums.Role.Customer
            };

            repo.RegisterUser(user);

            return MapToResponse(user);







        }

        // Login User
        public LoginResponseDto? LoginUser(LoginDto dto)
        {
            string normalizedEmail = dto.email.Trim().ToLower();

            User? user = repo.GetUserByEmail(normalizedEmail);

            if (user == null)
            {
                return null;
            }

            if (!user.isActive)
            {
                return null;
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.password, user.passwordHash);

            if (!validPassword)
            {
                return null;
            }

            // Credentials valid — delegate token generation to AuthService
            string token = authService.GenerateToken(user);

            LoginResponseDto response = new LoginResponseDto();
            response.Token = token;
            response.Username = user.userName;
            response.Role = user.role;


            return response;
        }



        // Update Profile
        public UserResponseDto? UpdateUserProfile(int id, UpdateUserDto dto)
        {
            User? user = repo.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.fullName))
            {
                user.fullName = dto.fullName;
            }

            if (!string.IsNullOrWhiteSpace(dto.phoneNumber))
            {
                user.phoneNumber = dto.phoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(dto.address))
            {
                user.address = dto.address;
            }

            repo.UpdateUser();

            return MapToResponse(user);
        }




        // Change Password
        public bool ChangeUserPassword(int id, ChangePasswordDto dto)
        {
            User? user = repo.GetUserById(id);

            if (user == null)
            {
                return false;
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.currentPassword, user.passwordHash);

            if (!validPassword)
            {
                return false;
            }

            if (dto.currentPassword == dto.newPassword)
            {
                return false;
            }

            user.passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.newPassword);

            repo.UpdateUser();

            return true;
        }



        // Activate / Deactivate User
        public bool SetUserActiveStatus(int id, SetActiveStatusDto dto)
        {
            User? user = repo.GetUserById(id);

            if (user == null)
            {
                return false;
            }

            user.isActive = dto.isActive;

            repo.UpdateUser();

            return true;
        }

        public bool SetUserRole(int id, SetRoleToAdmin dto)
        {
            User? user = repo.GetUserById(id);

            if (user == null)
            {
                return false;
            }

            user.role = dto.Role;

            repo.UpdateUser();

            return true;
        }
    }
}