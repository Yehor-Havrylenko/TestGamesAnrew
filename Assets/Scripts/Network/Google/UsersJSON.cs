using System;
using System.Collections.Generic;

namespace Network.Google
{
    [Serializable]
    public class UsersJSON
    {
        public List<UserJSON> users;
        public UsersJSON(UsersJSON value) => users = new(value.users);
        public UsersJSON()
        {
        }
    }
}