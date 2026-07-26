using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;
using static ClothingStore.DTOs.UserDTOs;


namespace ClothingStore.Services
{
    public class UserService
    {

        private UserRepo repo;

        public UserService (UserRepo repo)
        {
            this.repo = repo;
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
        public UserResponseDto GetUserById(int id)
        {
            User user = repo.GetUserById(id);

            if (user == null)

                throw new  KeyNotFoundException("User not found.");


            return MapToResponse(user);


        }



        // Get Users By Role
        public List<UserResponseDto> GetUsersByRole(string role)
        {
         
                return repo.GetByRole(role)
                           .Select(u => MapToResponse(u))
                           .ToList();
        }
              


        // Register User

        public UserResponseDto RegisterUser(RegisterUserDto dto)


        {
            dto.email=dto.email.Trim().ToLower();

            // Check Email
            if (repo.EmailExists(dto.email))
                return null;

            // Check Username
            if (repo.UsernameExists(dto.userName))
                return null;

            User user = new User
            {
                userName = dto.userName,
                email = dto.email,
                passwordHash =BCrypt.Net.BCrypt.HashPassword(dto.password),
                fullName = dto.fullName,
                phoneNumber =dto.phoneNumber,
                address = dto.address,
                registrationDate =DateTime.Now,
                isActive =true,
                role = "Customer"
            };

            repo.RegisterUser(user);
            return MapToResponse(user);
        }


        // Login

        public UserResponseDto LoginUser(LoginDto dto)
        {

            dto.email = dto.email.Trim().ToLower();


            // Search by email
            User user = repo.GetUserByEmail(dto.email);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Check if account is active
            if (!user.isActive)

                throw new UnauthorizedAccessException("User account is inactive");

            // Check password

            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.password, user.passwordHash);
            if (!validPassword)
                throw new UnauthorizedAccessException("Invalid password");


            // Create Response DTO

            return MapToResponse(user);
        
        }





   


        // Update Profile
        public void UpdateUserProfile(int id, UpdateUserDto dto)
        {
            User user = repo.GetUserById(id);

            if (user == null)
                throw new  KeyNotFoundException("User not found.");

            if (dto.fullName != null)
                user.fullName = dto.fullName;

            if (dto.phoneNumber != null)
                user.phoneNumber = dto.phoneNumber;

            if (dto.address != null)
                user.address = dto.address;


            repo.UpdateUser(user);
        }



        // Change Password
        public void ChangeUserPassword(int id, ChangePasswordDto dto)
        {
            User user = repo.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

   
            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.currentPassword, user.passwordHash);

            if (!validPassword)
                throw new  UnauthorizedAccessException("Current password is incorrect.");

            if (dto.currentPassword == dto.newPassword)
                throw new InvalidOperationException(
                    "New password must be different from the current password.");


            user.passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.newPassword);


            repo.UpdateUser(user);
        }




        // Activate / Deactivate User
        public void SetUserActiveStatus(int id, SetActiveStatusDto dto)
        {
            User user =repo.GetUserById(id);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            user.isActive =dto.Isactive;

            repo.UpdateUser(user);













        }
}





















    }

