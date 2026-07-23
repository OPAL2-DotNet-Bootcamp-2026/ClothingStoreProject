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
        public List<User> GetAllUser()
        {
            return repo.GetAllUser();
        }

        // Get User By Id
        public User GetUserById(int id)
        {
            return repo.GetUserById(id);
        }

        // Get User By Username
        public User GetUserByUsername(string username)
        {
            return repos.GetUserByUsername(username);
        }

        // Get User By Email
        public User GetUserByEmail(string email)
        {
            return repo.GetUserByEmail(email);
        }

        // Add User
        public void RegisterUser(User user)
        {
            repo.AddUser(user);
        }

        // Update User
        public void Update(User user)
        {
            repo.Update(user);
        }

        // Delete User
        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}





















    }
}
