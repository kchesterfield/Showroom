using Showroom.User.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Showroom.User.Data
{
    public interface IUserData
    {
        public UserResource CreateUser(UserResource user);
    }

    public class UserData : IUserData
    {
        private readonly IConfiguration Configuration;

        public UserData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public UserResource CreateUser(UserResource user)
        {
            List<UserResource> db = GetUserDB();
            // TODO: Check for existing user
            db.Add(user);
            SaveUserDB(db);
            return user;
        }

        private string GetDBPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + Configuration["JsonUserDB"];
        }

        private List<UserResource> GetUserDB()
        {
            using var reader = new StreamReader(File.OpenRead(GetDBPath()));
            return JsonSerializer.Deserialize<List<UserResource>>(reader.ReadToEnd());
        }

        private void SaveUserDB(List<UserResource> users)
        {
            using StreamWriter file = new StreamWriter(GetDBPath());
            file.WriteLine(JsonSerializer.Serialize(users));
        }
    }
}
