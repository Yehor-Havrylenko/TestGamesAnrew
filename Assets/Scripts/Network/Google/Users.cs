using System;
using System.Collections.Generic;

namespace Network.Google
{
    [Serializable]
    public class Users
    {
        public List<User> users;
        public Users(Users value) => users = new(value.users);
        public Users()
        {
        }
    }
}