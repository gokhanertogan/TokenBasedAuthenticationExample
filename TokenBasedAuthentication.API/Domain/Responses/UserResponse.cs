using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Domain.Responses
{
    public class UserResponse :BaseResponse
    {
        public User User {get;set;}

        private UserResponse(Boolean success,string message,User user):base(success,message)
        {
            this.User=user;
        }

        public UserResponse(User user):this(true,String.Empty,user){}

        public UserResponse(string message):this(false,message,null){}

    }
}