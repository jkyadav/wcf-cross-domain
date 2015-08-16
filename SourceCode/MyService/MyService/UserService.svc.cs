using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using MyService.DB;

namespace MyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : IUser
    {
        private SampleEntities db = new SampleEntities();

        public List<User> GetUsers()
        {
            var usrs = db.Users;
            List<User> userList = new List<User>();

            foreach (var x in usrs)
            {
                userList.Add(
                   new User { UserId = x.Id, UserName = x.UserName, Password = x.Password }
                    );
            }
            return userList;
        }

        public User InsertUser(User user)
        {
            var _user = new DB.User();
            _user.UserName = user.UserName;
            _user.Password = user.Password;
            db.Users.Add(_user);
            db.SaveChanges();
            user.UserId = _user.Id;
            return user;
        }


        public bool UpdateUser(User user)
        {
            var _user = db.Users.Find(user.UserId);
            if (_user != null)
            {
                _user.UserName = user.UserName;
                _user.Password = user.Password;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeleteUser(int userId)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
