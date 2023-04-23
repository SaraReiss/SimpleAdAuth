using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAdsAuth.Data
{
    public class Manager
    {
        private string _connectionString;
        public Manager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddUser(User user, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Name, Email, PasswordHash, phonenumber) VALUES " +
                "(@name, @email, @hash,@phonenumber)";

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@hash", passwordHash);
            cmd.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValid)
            {
                return null;
            }

            return user;


        }
        public User GetByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            connection.Open();
            var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new User
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Email = (string)reader["Email"],
                PasswordHash = (string)reader["PasswordHash"],
            };
        }
        public List<Ad> GetAds()

        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Ads a 
                       join users u
                       on a.UserId = u.Id
                      order by datecreated desc";
            connection.Open();
            var reader = cmd.ExecuteReader();
            var ads = new List<Ad>();
            while (reader.Read())
            {
                var ad= new Ad
                {
                    Id = (int)reader["Id"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    Details = (String)reader["Details"],
                    UserNumber = (string)reader["PhoneNumber"],
                    UserId = (int)reader["UserId"],
                    UserName = (string)reader["Name"],
                    
                    


                };
                ads.Add(ad);
            }


            return ads;

        }
        public List<Ad> GetMyAds(int userid)
        {

            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Ads  where userid= @userid order by datecreated desc";
            cmd.Parameters.AddWithValue("@userid", userid);
            connection.Open();
            var reader = cmd.ExecuteReader();
            var ads = new List<Ad>();
            while (reader.Read())
            {
                var ad = new Ad
                {
                    Id = (int)reader["Id"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    Details = (String)reader["Details"],
                    UserId = (int)reader["UserId"],


                };
                ads.Add(ad);
            }


            return ads;
        }
        public void NewAd(Ad ad, int userid)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT into Ads Values ( @datecreated, @details,@userid) ";
            cmd.Parameters.AddWithValue("@datecreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@details", ad.Details);
            cmd.Parameters.AddWithValue("@userid", userid);
            connection.Open();
            cmd.ExecuteNonQuery();


        }
        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Delete from Ads where id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            connection.Open();
            cmd.ExecuteNonQuery();

        }

    }
}
