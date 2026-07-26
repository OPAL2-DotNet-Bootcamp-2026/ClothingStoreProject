using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;

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
        public List<UserDTOs> GetAllUsers()
        {
            return repo.GetAllUsers()

                .Select(u => new UserResponseDto
                {
                    userId = u.UserId,
                    username = u.userName,
                    email = u.email,
                    fullName = u.fullName,
                    phoneNumber = u.phoneNumber,
                    address = u.address,
                    registrationDate = u.registrationDate,
                    Isactive = u.isActive,
                    role = u.role
                })
                .ToList();
        }






















    }
}





















    }
}
