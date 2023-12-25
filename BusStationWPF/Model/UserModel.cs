using BusStationWPF.Model.Enum;
using DAL.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationWPF.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public TypeUser TypeUser { get; set; }
        public string Password { get; set; }
        public UserModel() { }
        public UserModel(User user)
        {
            this.Id = user.Id;
            this.Login = user.Login;
            this.TypeUser = user.TypeOfUserId == 1 ? TypeUser.SimpleUser : TypeUser.Admin;
            this.Password = user.Password;
        }
        public User GetUser()
        {
            User user = new User()
            {
                Id = this.Id,
                Login = this.Login,
                Password = this.Password,
                TypeOfUserId = this.TypeUser == TypeUser.SimpleUser ? 1 : 2
            };
            return user;
        }
    }
}
