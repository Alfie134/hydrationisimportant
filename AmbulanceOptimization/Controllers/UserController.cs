﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services;

namespace AmbulanceOptimization.Controllers
{
    internal class UserController
    {
        private  UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }
        public void Add(string username,string password,int region)
        {
            _userService.CreateUser(username, password, region);
        }

        public User? GetUserByUserName(string username)
        {
            return _userService.GetUserByUsername(username);
        }
    }
}
