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
        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(u => u.userId == id);
        }


        // Get User By Username
        public User GetUserByUsername(string username)
        {
            return context.Users.FirstOrDefault(u => u.userName == username);
        }


        // Get User By Email
        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.email == email);
        }



        // Update User
        public void UpdateUser(User user)
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
