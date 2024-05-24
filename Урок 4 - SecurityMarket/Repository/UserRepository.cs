﻿using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using AutoMapper.Internal;
using DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using SecurityMarket.Abstraction;
using SecurityMarket.Model;
using SecurityMarket.UserContext;

namespace SecurityMarket.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserRoleContext _context;

        public UserRepository(UserRoleContext context)
        {
            _context = context;
        }

        public void AddUser(string email, string password, UserRoleType userRoleType)
        {
            var checkUser = _context.Users.FirstOrDefault(user => user.Email == email);

            if (checkUser == null)
            {
                
                var newUser = new User() { Email = email, RoleId = userRoleType };
                newUser.Salt = new byte[16];
                new Random().NextBytes(newUser.Salt);
                var data = Encoding.UTF8.GetBytes(password).Concat(newUser.Salt).ToArray();
                newUser.Password = new SHA512Managed().ComputeHash(data);
                _context.Add(newUser);
                _context.SaveChanges();
            }
        }

        public UserRoleType CheckUser(string email, string password)
        {
            var checkUser = _context.Users.FirstOrDefault(user => user.Email == email);
            if (checkUser == null)
            {
                throw new Exception("User not found");
            }

            var data = Encoding.UTF8.GetBytes(password).Concat(checkUser.Salt).ToArray();
            var hash = new SHA512Managed().ComputeHash(data);
            if (checkUser.Password.SequenceEqual(hash))
            {
                return checkUser.RoleId;
            }


            throw new Exception("Some error");
        }
    }
}