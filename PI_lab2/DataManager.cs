using System;
using PI_lab2.Models.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PI_lab2
{
    public static class DataManager
    {
        public static string FilePath = @"\\Mac\Home\Documents\Visual Studio 2015\Projects\PI_labs\PI_lab2\Data.txt";

        #region User

        public static void AddUser(User u)
        {
            foreach (var user in GetAllUsers())
            {
                if (string.Equals(user.Login, u.Login, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception("The user with such Login already exists.");
                }
                if (string.Equals(user.Email, u.Email, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception("The user with such Email already exists.");
                }
            }

            List<User> users;
            using (var sr = new StreamReader(FilePath))
            {
                users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            }
            users.Add(u);
            using (var sw = new StreamWriter(FilePath, false))
            {
                sw.WriteLine(JsonConvert.SerializeObject(users));
            }
        }

        public static void EditUser(User u)
        {
            var user = GetUser(u.Login);
            if (user == null)
            {
                return;
            }

            List<User> users;
            using (var sr = new StreamReader(FilePath))
            {
                users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            }
            users = users.Where(us => !string.Equals(us.Login, u.Login, StringComparison.CurrentCultureIgnoreCase)).ToList();
            users.Add(u);
            using (var sw = new StreamWriter(FilePath, false))
            {
                sw.WriteLine(JsonConvert.SerializeObject(users));
            }
        }

        public static void DeleteUser(string login)
        {
            var user = GetUser(login);
            if (user == null)
            {
                return;
            }

            List<User> users;
            using (var sr = new StreamReader(FilePath))
            {
                users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            }
            users = users.Where(us => !string.Equals(us.Login, login, StringComparison.CurrentCultureIgnoreCase)).ToList();
            using (var sw = new StreamWriter(FilePath, false))
            {
                sw.WriteLine(JsonConvert.SerializeObject(users));
            }
        }

        public static List<User> GetAllUsers()
        {
            List<User> users;
            using (var sr = new StreamReader(FilePath))
            {
                users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            }
            return users;
        }
        
        public static bool ValidateUser(string login, string password)
        {
            return GetAllUsers().Any(u => string.Equals(u.Login, login, StringComparison.CurrentCultureIgnoreCase) && u.Password == password);
        }

        public static User GetUser(string login)
        {
            return GetAllUsers().FirstOrDefault(u => string.Equals(u.Login, login, StringComparison.CurrentCultureIgnoreCase));
        }

        #endregion

        #region Role 

        public static List<string> GetRoles()
        {
            return new List<string> {"Admin", "User"};
        } 

        public static string GetUserRole(string login)
        {
            return GetUser(login).IsAdmin ? "Admin" : "User";
        }

        public static void CreateRole(string roleName)
        {
            if (GetRoles().Contains(roleName))
            {
                throw new Exception("Role already exists");
            }
        }

        public static bool UserInRole(string login, string role)
        {
            return GetUserRole(login) == role;
        }

        #endregion
    }
}
