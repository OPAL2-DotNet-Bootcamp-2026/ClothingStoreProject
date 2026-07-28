using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repos
{
    public class UserRepo
    {
        private ClothingStoreContext context;

        public UserRepo(ClothingStoreContext context)

        {
           this.context = context;
        }


        // Add User
        public void RegisterUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }


        // Get All Users
        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }


        // Get User By Id
        public User ? GetUserById(int id)
        {
            return context.Users.FirstOrDefault(u => u.userId == id);
        }


        // Get User By Username
        public User? GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault(u => u.userName == username);
        }


        // Get User By Email
        public User? GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.email == email);
        }



        // Get Users By Role
        public List<User> GetByRole(Enums.Role role)
        {
            return context.Users
                          .Where(u => u.role == role)
                          .ToList();
        }


        // Check Email Exists
        public bool EmailExists(string email)
        {
            return context.Users.Any(u => u.email == email);
        }

        // Check Username Exists
        public bool UsernameExists(string username)
        {
            return context.Users.Any(u => u.userName == username);
        }


        // Update User
        public void UpdateUser()
        {
            context.SaveChanges();
        }


        // Delete User
        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }










        }
}
