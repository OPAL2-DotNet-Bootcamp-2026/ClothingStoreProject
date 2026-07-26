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


        // Get All Users
        public List<UserResponseDto> GetAllUsers()
        {
            return repo.GetAllUsers()

                .Select(u => new UserResponseDto
                {
                    userId = u.userId,
                    userName = u.userName,
                    email = u.email,
                    fullName = u.fullName,
                    phoneNumber = u.phoneNumber,
                    address = u.address,
                    registrationDate = u.registrationDate,
                    isActive = u.isActive,
                    role = u.role
                })
                .ToList();
        }



        // Get User By Id
        public UserResponseDto GetUserById(int id)
        {
            User user = repo.GetUserById(id);

            if (user == null)

                throw new Exception("User not found.");


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



        // Get Users By Role
        public List<UserResponseDto> GetUsersByRole(string role)
        {
            return repo.GetByRole(role)

                .Select(u => new UserResponseDto
                {
                    userId = u.userId,
                    userName = u.userName,
                    email = u.email,
                    fullName = u.fullName,
                    phoneNumber = u.phoneNumber,
                    address = u.address,
                    registrationDate = u.registrationDate,
                    isActive = u.isActive,
                    role = u.role
                })
                .ToList();

                    }



        // Register User

        public UserResponseDto RegisterUser(RegisterUserDto dto)
        {
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
                passwordHash = dto.password,
                fullName = dto.fullName,
                phoneNumber = dto.phoneNumber,
                address = dto.address,
                registrationDate = DateTime.Now,
                isActive = true,
                role = "Customer"
            };

            repo.RegisterUser(user);

            UserResponseDto response = new UserResponseDto
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

            return response;
        }





        // Login

        public UserResponseDto LoginUser(LoginDto dto)
        {
            // Search by email
            User user = repo.GetUserByEmail(dto.email);

            if (user == null)
                return null;

            // Check if account is active
            if (!user.isActive)
                return null;

            // Check password
            if (user.passwordHash != dto.password)
                return null;

            // Create Response DTO
            UserResponseDto response = new UserResponseDto
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

            return response;
        }





   


        // Update Profile
        public void UpdateUserProfile(int id, UpdateUserDto dto)
        {
            User user = repo.GetUserById(id);

            if (user == null)
                throw new Exception("User not found.");

            user.fullName = dto.fullName;
            user.phoneNumber = dto.phoneNumber;
            user.address = dto.address;

            repo. UpdateUser(user);
        }



        // Change Password
        public void ChangeUserPassword(int id, ChangePasswordDto dto)
        {
            User user = repo.GetUserById(id);

            if (user == null)
                throw new Exception("User not found.");

            if (user.passwordHash != dto.currentPassword)
                throw new Exception("Current password is incorrect.");

            user.passwordHash = dto.newPassword;

            repo.UpdateUser(user);
        }




        // Activate / Deactivate User
        public void SetUserActiveStatus(int id, SetActiveStatusDto dto)
        {
            User user = repo.GetUserById(id);

            if (user == null)
                throw new Exception("User not found.");

            user.isActive = dto.Isactive;

            repo.UpdateUser(user);













        }
}





















    }
}
