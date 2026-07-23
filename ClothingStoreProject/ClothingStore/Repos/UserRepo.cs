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
            context.users.Add(user);
            context.SaveChanges();
        }


        // Get All Users
        public List<User> GetAllUsers()
        {
            return context.users.ToList();
        }


        // Get User By Id
        public User GetUserById(int id)
        {
            return context.users.FirstOrDefault(u => u.userId == id);
        }


        // Get User By Username
        public User GetUserByUsername(string username)
        {
            return context.users.FirstOrDefault(u => u.userName == username);
        }


        // Get User By Email
        public User GetUserByEmail(string email)
        {
            return context.users.FirstOrDefault(u => u.email == email);
        }



        // Update User
        public void UpdateUser(User user)
        {
            context.SaveChanges();
        }


        // Delete User
        public void DeleteUser(User user)
        {
            context.users.Remove(user);
            context.SaveChanges();
        }




        }
}
