using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameProjectApp.Models
{
    public static class MetaData
    {
        public static List<User> Users { get; set; } = new List<User>();
        
        public static User Me(string myId)
        {
            foreach (User user in Users)
            {
                if (user.Id == myId)
                {
                    return user;
                }
            }
            return null;
        }
        public static List<User> NotMe(string myId)
        {
            List<User> result = new List<User>();
            foreach(User user in Users)
            {
                if(user.Id != myId)
                {
                    result.Add(user);
                }
            }
            return result;
        }
    }
}