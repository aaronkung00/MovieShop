﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Models.Response_Model
{
    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IList<string> Roles { get; set; }
    }
}
